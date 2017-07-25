angular.module('constituent').controller("AddAffiliatorInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance','globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService','$rootScope' ,function ($scope, $filter, $uibModalInstance,globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {

        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }

        $scope.affiliator = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            orgId: "",
            caseNo: caseNo,
            cnstEnterpriseOrgIDRegex: { orgId: /^[0-9]*$/ }
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "mstr_id": $scope.affiliator.masterId,
                    "new_ent_org_id": $scope.affiliator.orgId,
                    "usr_nm": "",
                    "cnst_typ": "OR",
                    "Notes": $scope.affiliator.selected.note,
                    "case_seq_num":$scope.affiliator.caseNo
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.AFFILIATOR.ADD).success(function (result) {
                    console.log(result);
                    //output_msg = result.data[0].o_outputMessage;
                    //trans_key = result.data[0].o_transaction_key;
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.affiliator.masterId, HOME_CONSTANTS.AFFILIATOR).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.AFFILIATOR);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.AFFILIATOR);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            // emailData.push(PushParams);
                            $uibModalInstance.dismiss('cancel');
                            affErrorPopup($rootScope, result)
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
                   /* else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }*/
                }).error(function (result) {
                    affErrorPopup($rootScope, result)
                });

            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);

angular.module('constituent').controller("DeleteAffiliatorInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance','globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }
        $scope.affiliator = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE
            },
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            orgId: params.row.ent_org_id,
            caseNo: caseNo
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "mstr_id": $scope.affiliator.masterId,
                    "bk_ent_org_id": params.row.ent_org_id,
                    "new_ent_org_id": params.row.ent_org_id,
                    "usr_nm": "",
                    "cnst_typ": "OR",
                    "Notes": $scope.affiliator.selected.note,
                    "case_seq_num": $scope.affiliator.caseNo
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.AFFILIATOR.DELETE).success(function (result) {
                    console.log(result);
                    //output_msg = result.data[0].o_outputMessage;
                    //trans_key = result.data[0].o_transaction_key;
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.affiliator.masterId, HOME_CONSTANTS.AFFILIATOR).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.AFFILIATOR);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.AFFILIATOR);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            // emailData.push(PushParams);
                            $uibModalInstance.dismiss('cancel');
                            affErrorPopup($rootScope, result)
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
                    else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    // emailData.push(PushParams);
                    $uibModalInstance.dismiss('cancel');
                    affErrorPopup($rootScope, result)
                });;

            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);



function affErrorPopup($rootScope, result) {
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