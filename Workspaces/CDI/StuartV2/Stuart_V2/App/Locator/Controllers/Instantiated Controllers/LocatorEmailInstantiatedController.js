
/********************* Edit Name Modal Instance ***************/
angular.module('locator').controller('EditLocatorEmailInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'pageName', 'LocatorEmailMultiDataService',  'uiGridConstants', 'LocatorEmailCRUDapiService', '$rootScope',
    'uibDateParser', 'LocatorEmailCRUDoperations', 'LocatorServices', 'LocatorEmailDataServices', 'LocatorEmailDropDownService', '$state',
    function ($scope, $filter, $uibModalInstance, $uibModal, params, pageName, LocatorEmailMultiDataService, uiGridConstants, LocatorEmailCRUDapiService, $rootScope, uibDateParser, LocatorEmailCRUDoperations, LocatorServices, LocatorEmailDataServices, LocatorEmailDropDownService, $state) {
        
        var row = params.row;
        var BASE_URL = BasePath + 'App/Locator/Views/Multi';
        
        $scope.LocatorEmail = {
            selected: {
                
                FinalCode_Ctry: row.code_category,
                FinalCode_CtryId: '',
                StatusType: row.status,
                StatusTypeCode: ''
            },        
           
            email_key: row.email_key,            
            cnst_email_addr: row.cnst_email_addr,
            LocatorReportDate: row.report_dt ? new Date(row.report_dt) : null,
            userName: row.crtd_by_usr_id
        };
   

        LocatorEmailDropDownService.getDropDown1(LOCATOR_CONSTANTS.LOCATOR_MANUALLASSESMENT_CODE).success(function (result) {

           //console.log(angular.toJson(result));
           $scope.LocatorEmail.Finalassesmentcodes = result;
            //return result;
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var LocatorReportDate = $scope.LocatorEmail.LocatorReportDate ? $filter('date')(new Date($scope.LocatorEmail.LocatorReportDate), 'MM/dd/yyyy') : null;

                var postParams = {
                    "LocEmailKey": $scope.LocatorEmail.email_key,
                    "LocEmailId": $scope.LocatorEmail.cnst_email_addr,
                    "ExtAssessCode": $scope.LocatorEmail.selected.FinalCode_CtryId,
                    "Date": LocatorReportDate,
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                //console.log($scope.LocatorEmail.email_key)
                LocatorEmailCRUDapiService.getCRUDOperationResult(postParams, LOCATOR_CRUD_CONSTANTS.LOCATOREMAILINFO).success(function (result) {
                    // console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;
                    //console.log(output_msg);
                    if (output_msg == LOCATOR_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: LOCATOR_CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }
                        
                       
                        var searchParams = {
                            "LocEmailKey": $scope.LocatorEmail.email_key,
                            "LocEmailId": null,
                            "IntAssessCode": null,
                            "ExtAssessCode": null,
                            "ExactMatch": null,
                            "Type": "Details"
                        };
                        // if (params.grid.data.length > 1) {
                        if (pageName == "SearchResults") {
                            
                            LocatorServices.getLocatorEmailAdvSearchResultsByID(searchParams).success(function (result) {
                                LocatorEmailDataServices.setSearchResutlsDataDetail(result);
                                LocatorEmailDataServices.getSearchResultsDataDetail();

                                //LocatorServices.getLocatorEmailConstAdvSearchResultsByID(searchParams).success(function (result) {
                                //    LocatorEmailDataServices.setSearchResutlsDataDetailConstituent(result);
                                //    var _result = LocatorEmailDataServices.getSearchResultsDataDetailConstituent();
                                    
                                    
                                //});
                                
                                //$scope.pleaseWait = { "display": "none" };
                                $scope.pleaseWait = { "display": "none" };
                                myApp.hidePleaseWait();
                                var key = $scope.LocatorEmail.email_key;
                                //console.log(key)
                                $state.go('locator.email.results.multi', { obj:key }, { reload: true });
                                $uibModalInstance.close(output);
                                messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SUCCESS_MESSAGE, LOCATOR_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                                }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }
                                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }

                            });
                           

                        }
                        else
                        {
                      
                            LocatorServices.getLocatorEmailAdvSearchResults(SearchResultsParam).success(function (result) {
                                LocatorEmailDataServices.setSearchResutlsData(result);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }
                                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }

                            });
                            LocatorServices.getLocatorEmailAdvSearchResults(searchparam).success(function (result) {
                                myApp.hidePleaseWait();
                               
                                params.grid.data = '';
                                params.grid.data.length = 0;
                                params.grid.data = result;
                                LocatorEmailDataServices.setLocatorDetails(result[0]["locator_desc"]);
                              

                                LocatorMultiDataService.setData(result, LOCATOR_CONSTANTS.LOCATOR_INFO);
                                $uibModalInstance.close(output);
                                messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SUCCESS_MESSAGE, LOCATOR_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                }
                                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                }
                            });
                        }
                      
                    }
                    else if (output_msg == LOCATOR_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == LOCATOR_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: LOCATOR_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,

                            ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE, LOCATOR_CRUD_CONSTANTS.FAILURE_CONFIRM);
                    }
                    //else if (result.data == LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    debugger;
                    //    var output = {
                    //        finalMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output);
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM);
                    }
                    else if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);


function modalMessage($rootScope, result) {   
    $rootScope.FinalMessage = result.finalMessage;   
    //$rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}



function messagePopup($rootScope, message, header) {
    $rootScope.FinalMessage = message;
    $rootScope.ConfirmationMessage = header;
    $rootScope.ReasonOrTransKey = '';
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}