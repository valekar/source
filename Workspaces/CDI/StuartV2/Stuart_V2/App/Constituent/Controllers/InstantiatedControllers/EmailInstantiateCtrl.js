
angular.module('constituent').controller("AddEmailInstantiateCtrl", ['$http', '$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants',
    'constituentCRUDapiService', 'dropDownService', 'ConstUtilServices', 'constituentApiService','$rootScope',
    function ($http, $scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService, ConstUtilServices, constituentApiService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.email = {
            selected: {
                emailType: "Home",
                emailTypeId: "EH",
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            emailTypes: [],
            masterId: params.masterId,
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            DWTimestamp: ConstUtilServices.getStartDate(),
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_EMAIL).success(function (result) { $scope.email.emailTypes = result; }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                var postParams = {
                    "MasterID": params.masterId,
                    "NewEmailAddress": $scope.email.emailId,
                    "EmailTypeCode": $scope.email.selected.emailTypeId,
                    "Notes": $scope.email.selected.note,
                    "SourceSystemCode": "STRX",
                    "CaseNumber": $scope.email.caseNo,
                    "ConstType": $scope.email.constType
                    
                }

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                //var emailData = constMultiDataService.getData(HOME_CONSTANTS.CONST_EMAIL);
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.EMAIL.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }


                        constituentApiService.getApiDetails(params.masterId, HOME_CONSTANTS.CONST_EMAIL).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_EMAIL);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_EMAIL);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            emailErrorPopup($rootScope, result);
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
                    emailErrorPopup($rootScope, result);
                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

       
    }]);


angular.module('constituent').controller("EditEmailInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants',
    'constituentCRUDapiService', 'dropDownService', 'ConstUtilServices', 'constituentApiService','$rootScope',
    function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService, ConstUtilServices, constituentApiService, $rootScope) {
      //  console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.email = {
            selected: {
                emailType: "Home",
                emailTypeId: "EH",
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            emailTypes: [],
            masterId: params.masterId,
            emailId : params.row.cnst_email_addr,
            sourceSys: params.row.arc_srcsys_cd,
            startDate: params.row.cnst_email_strt_ts,
            endDate : params.row.cnst_email_end_dt,
            rowCode: params.row.row_stat_cd,
            DWTimestamp: params.row.dw_srcsys_trans_ts,
            activeInd: params.row.act_ind,
            loadId: params.row.load_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_EMAIL).success(function (result) {

            $scope.email.emailTypes = result;
            angular.forEach($scope.email.emailTypes, function (v, k) {
                if (v.id == params.row.email_typ_cd) {
                    $scope.email.selected.emailType = v.value;
                    $scope.email.selected.emailTypeId = v.id;
                };

            });

        }).error(function (result) { });

        

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                var postParams = {
                    "MasterID": params.masterId,
                    "OldSourceSystemCode": params.row.arc_srcsys_cd,
                    "OldEmailTypeCode": params.row.email_typ_cd,
                    "OldBestLOSInd": params.row.cnst_email_best_los_ind,
                    "NewEmailAddress": $scope.email.emailId,
                    "ConstType": "L",
                    "EmailTypeCode": $scope.email.selected.emailTypeId,
                    "Notes": $scope.email.selected.note,
                    "SourceSystemCode": params.row.arc_srcsys_cd,
                    "BestLOS": params.row.cnst_email_best_los_ind,
                    "CaseNumber": $scope.email.caseNo,
                    "ConstType": $scope.email.constType
                }



                myApp.showPleaseWait();
                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.EMAIL.EDIT).success(function (result) {
                    //console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.email.masterId, HOME_CONSTANTS.CONST_EMAIL).success(function (result) {
                            // params.grid.data = result;
                            //we got it from parent controller
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_EMAIL);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_EMAIL);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            emailErrorPopup($rootScope, result);
                        });

                        // console.log(phoneData);                  
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
                    else if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    emailErrorPopup($rootScope, result);
                });


            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);

angular.module('constituent').controller("DeleteEmailInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'ConstUtilServices',
    'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, constituentApiService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.email = {
            selected: {
                emailType: "Home",
                emailTypeId: "EH",
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            emailTypes: [],
            masterId: params.masterId,
            emailId: params.row.cnst_email_addr,
            sourceSys: params.row.arc_srcsys_cd,
            startDate: params.row.cnst_email_strt_ts,
            endDate: params.row.cnst_email_end_dt,
            rowCode: params.row.row_stat_cd,
            DWTimestamp: params.row.dw_srcsys_trans_ts,
            activeInd: params.row.act_ind,
            loadId: params.row.load_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };


        dropDownService.getDropDown(HOME_CONSTANTS.CONST_EMAIL).success(function (result) {

            $scope.email.emailTypes = result;
            angular.forEach($scope.email.emailTypes, function (v, k) {
                if (v.id == params.row.email_typ_cd) {
                    $scope.email.selected.emailType = v.value;
                    $scope.email.selected.emailTypeId = v.id;
                };

            });

        }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                var postParams = {
                    "MasterID": params.masterId,
                    "OldSourceSystemCode": params.row.arc_srcsys_cd,
                    "OldEmailTypeCode": params.row.email_typ_cd,
                    "OldBestLOSInd": params.row.cnst_email_best_los_ind,
                    "NewEmailAddress": $scope.email.emailId,
                    "ConstType": "L",
                    "EmailTypeCode": $scope.email.selected.emailTypeId,
                    "Notes": $scope.email.selected.note,
                    "SourceSystemCode": params.row.arc_srcsys_cd,
                    "BestLOS": params.row.cnst_email_best_los_ind,
                    "CaseNumber": $scope.email.caseNo,
                    "ConstType": $scope.email.constType
                }



                myApp.showPleaseWait();
                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.EMAIL.DELETE).success(function (result) {
                    //console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.email.masterId, HOME_CONSTANTS.CONST_EMAIL).success(function (result) {
                            //we got it from parent controller
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_EMAIL);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_EMAIL);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            emailErrorPopup($rootScope, result);
                        });

                        // console.log(phoneData);                  
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
                    emailErrorPopup($rootScope, result);
                });


            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);

function emailErrorPopup($rootScope, result) {
    if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
        messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
    }
    else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
        messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
    }
    else if (result == CRUD_CONSTANTS.DB_ERROR) {
        messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
    }
}