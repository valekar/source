angular.module('constituent').controller("AddBirthInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'ConstUtilServices',
    'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
       /* $scope.format = 'MM/dd/yyyy';
        $scope.popup = {
            opened: false
        };

        $scope.open = function () {
            $scope.popup.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yyyy',
            maxDate: new Date(2016, 11, 31),
            minDate: new Date(1940,01,01),
            startingDay: 1
        };


        $scope.parseDate = function (dat) {
            var date = new Date(dat);
            $scope.birth.birthDay = date.getDate();
            $scope.birth.birthMonth = date.getMonth() + 1;
            $scope.birth.birthYear = date.getYear() + 1900;
        }*/
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }
        

        $scope.birth = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            birthDay: "",
            birthMonth: "",
            birthYear:"",
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            caseNo: caseNo,
            constType: $sessionStorage.type,

            cnstBirthRegex: { birthDay: /^(0?[1-9]|1[0-9]|2[0-9]|3[0-1])$/, birthMonth: /^(0?[1-9]|1[012])$/, birthYear: /^(?!0000)\d{4}$/ }
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.birth.masterId,
                    "NewBirthDayNumber": $scope.birth.birthDay,
                    "NewBirthMonthNumber": $scope.birth.birthMonth,
                    "NewBirthYearNumber": $scope.birth.birthYear,
                    "Notes": $scope.birth.selected.note,
                    "SourceSystemCode": $scope.birth.sourceSys,
                    "CaseNumber": $scope.birth.caseNo,
                    "ConstType": $scope.birth.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.BIRTH.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.birth.masterId, HOME_CONSTANTS.CONST_BIRTH).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_BIRTH);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_BIRTH);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();;
                            $uibModalInstance.dismiss('cancel');
                            birthErrorPopup($rootScope, result)
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                  /*  else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }*/
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    birthErrorPopup($rootScope, result)
                });;
              
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);


function caseSearchTerms() {
    return [
        "caseNo", "caseName", "constituentName", "userName", "status","type"
    ];
}

angular.module('constituent').controller("EditBirthInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', '$uibModal',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', 'constituentServices', '$rootScope',
    function ($scope, $filter, $uibModalInstance,
    $localStorage, $sessionStorage, globalServices, params, constMultiDataService, constMultiGridService, uiGridConstants,
    constituentCRUDapiService, $uibModal, ConstUtilServices, uibDateParser,
    constituentApiService, constituentCRUDoperations, constituentServices, $rootScope) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }
        $scope.birth = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            birthDay: params.row.cnst_birth_dy_num,
            birthMonth: params.row.cnst_birth_mth_num,
            birthYear: params.row.cnst_birth_yr_num,
            sourceSys: params.row.arc_srcsys_cd,
            startDate: params.row.cnst_birth_strt_ts,
            rowCode: params.row.row_stat_cd,
            caseNo: caseNo,
            constType: $sessionStorage.type,
            cnstBirthRegex: { birthDay: /^(0?[1-9]|1[0-9]|2[0-9]|3[0-1])$/, birthMonth: /^(0?[1-9]|1[012])$/, birthYear: /^(?!0000)\d{4}$/ }
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

              //  $scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.birth.masterId,
                    "NewBirthDayNumber": $scope.birth.birthDay,
                    "NewBirthMonthNumber": $scope.birth.birthMonth,
                    "NewBirthYearNumber": $scope.birth.birthYear,
                    "Notes": $scope.birth.selected.note,
                    "SourceSystemCode": $scope.birth.sourceSys,
                    "OldSourceSystemCode": $scope.birth.sourceSys,
                    "CaseNumber": $scope.birth.caseNo,
                    "ConstType": $scope.birth.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.BIRTH.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.birth.masterId, HOME_CONSTANTS.CONST_BIRTH).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_BIRTH);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_BIRTH);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();;
                            $uibModalInstance.dismiss('cancel');
                            birthErrorPopup($rootScope, result)
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                   /* else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }*/
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    birthErrorPopup($rootScope, result)
                });;

            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


       

    }]);


angular.module('constituent').controller("DeleteBirthInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'ConstUtilServices', 'uibDateParser',
    'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }
        $scope.birth = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            birthDay: params.row.cnst_birth_dy_num,
            birthMonth: params.row.cnst_birth_mth_num,
            birthYear: params.row.cnst_birth_yr_num,
            sourceSys: params.row.arc_srcsys_cd,
            startDate: params.row.cnst_birth_strt_ts,
            rowCode: params.row.row_stat_cd,
            caseNo: caseNo,
            constType: $sessionStorage.type
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //  $scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.birth.masterId,
                    "NewBirthDayNumber": $scope.birth.birthDay,
                    "NewBirthMonthNumber": $scope.birth.birthMonth,
                    "NewBirthYearNumber": $scope.birth.birthYear,
                    "Notes": $scope.birth.selected.note,
                    "SourceSystemCode": $scope.birth.sourceSys,
                    "OldSourceSystemCode": $scope.birth.sourceSys,
                    "CaseNumber": $scope.birth.caseNo,
                    "ConstType": $scope.birth.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.BIRTH.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.birth.masterId, HOME_CONSTANTS.CONST_BIRTH).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_BIRTH);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_BIRTH);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();;
                            $uibModalInstance.dismiss('cancel');
                            birthErrorPopup($rootScope, result)
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                  /*  else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }*/
                }).error(function (result) {
                    myApp.hidePleaseWait();;
                    $uibModalInstance.dismiss('cancel');
                    birthErrorPopup($rootScope, result)
                });

            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);

function birthErrorPopup($rootScope, result) {
    if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
        messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, CONSTITUENT_MESSAGES.ACCESS_DENIED_HEADER);
    }
    else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
        messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, CONSTITUENT_MESSAGES.TIMED_OUT_HEADER);
    }
    else if (result == CRUD_CONSTANTS.DB_ERROR) {
        messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, CONSTITUENT_MESSAGES.TIMED_OUT_HEADER);

    }
}




