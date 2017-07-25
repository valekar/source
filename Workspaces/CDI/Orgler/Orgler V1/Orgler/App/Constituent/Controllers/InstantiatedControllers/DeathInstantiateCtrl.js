angular.module('constituent').controller("AddDeathInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        $scope.format = 'MM/dd/yyyy';
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.popup = {
            opened: false
        };

        $scope.open = function () {
            $scope.popup.opened = true;
        };

        $scope.dateOptions = {
           // dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
           // minDate: new Date(),
            startingDay: 1
        };


        $scope.death = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,           
            masterId: params.masterId,
            deceasedCode: "N",
            deathDate:"",
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            caseNo: caseNo,
            constType: $sessionStorage.type
          //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "MasterID": $scope.death.masterId,
                    "NewDeathDate": $scope.death.deathDate,
                    "Notes": $scope.death.selected.note,
                    "NewDeceasedCode": $scope.death.deceasedCode,
                    "SourceSystemCode": $scope.death.sourceSys,
                    "CaseNumber": $scope.death.caseNo,
                    "ConstType": $scope.death.constType
                };
            
                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
              //  var emailData = constMultiDataService.getData(HOME_CONSTANTS.CONST_DEATH);
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.DEATH.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.death.masterId, HOME_CONSTANTS.CONST_DEATH).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_DEATH);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_DEATH);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            deathErrorPopup($rootScope, result);
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
                   
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    deathErrorPopup($rootScope, result);
                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);



angular.module('constituent').controller("EditDeathInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {

        //console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.format = 'MM/dd/yyyy';

        $scope.popup = {
            opened: false
        };

        $scope.open = function () {
            $scope.popup.opened = true;
        };

        $scope.dateOptions = {
            // dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            // minDate: new Date(),
            startingDay: 1
        };


        $scope.death = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.row.cnst_mstr_id,
            deceasedCode: params.row.cnst_deceased_cd,
            deathDate: params.row.cnst_death_dt,
            sourceSys: params.row.arc_srcsys_cd,
            startDate: params.row.cnst_death_strt_ts,
            rowCode: params.row.row_stat_cd,
            sourceSysId: params.row.cnst_srcsys_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "MasterID": $scope.death.masterId,
                    "NewDeathDate": $scope.death.deathDate,
                    "Notes": $scope.death.selected.note,
                    "NewDeceasedCode": $scope.death.deceasedCode,
                    "SourceSystemCode": $scope.death.sourceSys,
                    "OldSourceSystemCode": $scope.death.sourceSys,
                    "OldBestLOSInd": params.row.cnst_death_best_los_ind,
                    "CaseNumber": $scope.death.caseNo,
                    "ConstType": $scope.death.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                //  var emailData = constMultiDataService.getData(HOME_CONSTANTS.CONST_DEATH);
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.DEATH.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.death.masterId, HOME_CONSTANTS.CONST_DEATH).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_DEATH);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_DEATH);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            deathErrorPopup($rootScope, result);
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
                   
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    deathErrorPopup($rootScope, result);
                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);

angular.module('constituent').controller("DeleteDeathInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        //console.log(params.row);
        $scope.death = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.row.cnst_mstr_id,
            deceasedCode: params.row.cnst_deceased_cd,
            deathDate: params.row.cnst_death_dt,
            sourceSys: params.row.arc_srcsys_cd,
            startDate: params.row.cnst_death_strt_ts,
            rowCode: params.row.row_stat_cd,
            sourceSysId: params.row.cnst_srcsys_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "MasterID": $scope.death.masterId,
                    "NewDeathDate": $scope.death.deathDate,
                    "Notes": $scope.death.selected.note,
                    "NewDeceasedCode": $scope.death.deceasedCode,
                    "SourceSystemCode": $scope.death.sourceSys,
                    "OldSourceSystemCode": $scope.death.sourceSys,
                    "OldBestLOSInd": params.row.cnst_death_best_los_ind,
                    "CaseNumber": $scope.death.caseNo,
                    "ConstType": $scope.death.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                //  var emailData = constMultiDataService.getData(HOME_CONSTANTS.CONST_DEATH);
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.DEATH.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.death.masterId, HOME_CONSTANTS.CONST_DEATH).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_DEATH);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_DEATH);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            deathErrorPopup($rootScope, result);
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
                    
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    deathErrorPopup($rootScope, result);
                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);



function deathErrorPopup($rootScope, result) {
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