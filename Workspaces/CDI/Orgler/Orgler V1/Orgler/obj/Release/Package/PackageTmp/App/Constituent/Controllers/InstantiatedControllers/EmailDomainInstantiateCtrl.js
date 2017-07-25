angular.module('constituent').controller("AddEmailDomainInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', 'ConstituentEmailDomainService', function ($scope, $filter, $uibModalInstance, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope, service) {

        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }

        service.getEmailDomainDistinct()
            .success(function (result) {
                $rootScope.emailDistinctData = result;

            })
            .error(function (result) {
                errorPopups(result);
            });
        $scope.emaildomain = {
            masterId: params.masterId,
            emaildomain: "",
            caseNo: caseNo,
            notes: "",
        };
        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.emaildomain.masterId,
                    "EmailDomain": $scope.emailDistinctData.selected,
                    "usr_nm": "",
                    "CaseNumber": $scope.emaildomain.caseNo,
                    "ConstType": "OR",
                    "Notes": $scope.emaildomain.notes

                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.EMAIL_DOMAINS.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.emaildomain.masterId, HOME_CONSTANTS.EMAIL_DOMAINS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.EMAIL_DOMAINS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.EMAIL_DOMAINS);
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

angular.module('constituent').controller("DeleteEmailDomainInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }
        $scope.emaildomain = {
            masterId: params.masterId,
            emaildomain: params.row.email_domain,
            caseNo: params.caseNo,
            countofactInd: params.row.act_indv_email_cnt,
            countofCostituent: params.row.act_cnst_cnt,
            mostRecent: params.row.most_rcnt_email_ts,
            notes: "",
        };


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.emaildomain.masterId,
                    "EmailDomain": $scope.emaildomain.emaildomain,
                    "UserName": "",
                    "ConstType": "OR",
                    "CaseNumber": $scope.emaildomain.caseNo,
                    "Notes": $scope.emaildomain.notes

                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.EMAIL_DOMAINS.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.emaildomain.masterId, HOME_CONSTANTS.EMAIL_DOMAINS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.EMAIL_DOMAINS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.EMAIL_DOMAINS);
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
