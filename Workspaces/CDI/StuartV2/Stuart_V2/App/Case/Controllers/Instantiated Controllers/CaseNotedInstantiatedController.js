



/********************* Add Case Loc Info Modal Instance ***************/
angular.module('case').controller('AddCaseNotesInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'CaseMultiDataService', 'CaseMultiGridService', 'uiGridConstants', 'CaseCRUDapiService', '$rootScope',
    'uibDateParser', '$filter', 'CaseApiService', 'CaseCRUDoperations', 'CaseServices', 'CaseDataServices', function ($scope, $filter, $uibModalInstance, $uibModal,
    params, CaseMultiDataService, CaseMultiGridService, uiGridConstants, CaseCRUDapiService, $rootScope, uibDateParser, $filter, CaseApiService, CaseCRUDoperations, CaseServices, CaseDataServices) {
       // console.log("params");
       // console.log(params);
       
        $scope.caseNotes = {
            caseKey: params.case_key,
            NotesText: "",
            StartDate: $filter('date')(new Date(), 'MM/dd/yyyy   hh:mm:ss a'),
            EndDate: "12/31/9999 12:00:00 AM"
            
        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "action": "INSERT",
                    "case_id": $scope.caseNotes.caseKey,
                    "notes_id": null,
                    "notes_text": $scope.caseNotes.NotesText
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
              //  console.log("Case Notes Add Post params");
              //  console.log(postParams);
                CaseCRUDapiService.getCRUDOperationResult(postParams, CASE_CRUD_CONSTANTS.CASENOTES.ADD).success(function (result) {

                    output_msg = result[0].o_outputMessage;

                    if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            //  ReasonOrTransKey: CASE_CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }



                        CaseApiService.getApiDetails($scope.caseNotes.caseKey, CASE_CONSTANTS.CASE_NOTES).success(function (result) {
                          //  console.log("case key");
                           // console.log($scope.caseNotes.caseKey);
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            params.grid.data = result;
                            CaseMultiDataService.setData(result, CASE_CONSTANTS.CASE_NOTES);

                            myApp.hidePleaseWait();

                            // emailData.push(PushParams);
                            $uibModalInstance.close(output);

                            // emailData.push(PushParams);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);
                            if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                            }
                            else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                            }

                        })


                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.SUCCESS_MESSAGE, CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.FAILURE_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.CASENOTES.DUPLICATE_NOTE_REASON) {
                        //var output = {
                        //    finalMessage: CASE_CRUD_CONSTANTS.CASENOTES.DUPLICATE_NOTE_REASON,
                        //    ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_MESSAGE
                        //}
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.CASENOTES.DUPLICATE_NOTE_REASON, CASE_CRUD_CONSTANTS.FAILURE_MESSAGE);
                    }
                    //else if (result.data == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    var output = {
                    //        finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output);
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);



/********************* Edit Name Modal Instance ***************/
angular.module('case').controller('EditCaseNotesInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'CaseMultiDataService', 'CaseMultiGridService', 'uiGridConstants', 'CaseCRUDapiService', '$rootScope',
    'uibDateParser', '$filter', 'CaseApiService', 'CaseCRUDoperations', 'CaseServices', 'CaseDataServices', function ($scope, $filter, $uibModalInstance, $uibModal,
    params, CaseMultiDataService, CaseMultiGridService, uiGridConstants, CaseCRUDapiService, $rootScope, uibDateParser, $filter, CaseApiService, CaseCRUDoperations, CaseServices, CaseDataServices) {
       // console.log("params");
       // console.log(params.row);
        var row = params.row;
        $scope.caseNotes = {
            caseKey: params.case_key,
            NotesText: row.case_notes,
            StartDate: $filter('date')(new Date(), 'MM/dd/yyyy   hh:mm:ss a'),
            EndDate: "12/31/9999 12:00:00 AM"

        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "action": "UPDATE",
                    "case_id": $scope.caseNotes.caseKey,
                    // "locator_id": $scope.caseNotes.caseDesc,
                    "notes_id": row.notes_key,
                    "notes_text": $scope.caseNotes.NotesText
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
               // console.log("Case Notes EDIT Post params");
               // console.log(postParams);
                CaseCRUDapiService.getCRUDOperationResult(postParams, CASE_CRUD_CONSTANTS.CASENOTES.EDIT).success(function (result) {

                    output_msg = result[0].o_outputMessage;

                    if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            //  ReasonOrTransKey: CASE_CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }



                        CaseApiService.getApiDetails($scope.caseNotes.caseKey, CASE_CONSTANTS.CASE_NOTES).success(function (result) {
                           // console.log("case key");
                           // console.log($scope.caseNotes.caseKey);
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            params.grid.data = result;
                            CaseMultiDataService.setData(result, CASE_CONSTANTS.CASE_NOTES);

                            myApp.hidePleaseWait();

                            // emailData.push(PushParams);
                            $uibModalInstance.close(output);

                            // emailData.push(PushParams);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);
                            if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                            }
                            else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                            }

                        });
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE, CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.CASENOTES.DUPLICATE_NOTE_REASON) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.CASENOTES.DUPLICATE_NOTE_REASON,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_MESSAGE
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    //else if (result.data == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    var output = {
                    //        finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output);
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                                    finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                                    ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                                }
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                    }
                    else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);




/*********************Delete Name Modal Controller****************/
angular.module('case').controller('DeleteCaseNotesInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'CaseMultiDataService', 'CaseMultiGridService', 'uiGridConstants', 'CaseCRUDapiService', 'CaseServices', '$rootScope',
    'CaseUtilServices', 'uibDateParser', 'CaseApiService', 'CaseCRUDoperations', 'CaseDataServices', function ($scope, $filter, $uibModalInstance, $uibModal,
    params, CaseMultiDataService, CaseMultiGridService, uiGridConstants, CaseCRUDapiService, CaseServices, $rootScope, CaseUtilServices, uibDateParser, CaseApiService, CaseCRUDoperations) {
       // console.log("params");
       // console.log(params);
        var row = params.row;
        $scope.caseNotes = {
            caseKey: params.case_key,
            NotesText: row.case_notes,
            StartDate: row.start_dt,
            EndDate: row.end_dt

        };

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "action": "DELETE",
                    "case_id": $scope.caseNotes.caseKey,
                    // "locator_id": $scope.caseNotes.caseDesc,
                    "notes_id": row.notes_key,
                    "notes_text": $scope.caseNotes.NotesText
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
               // console.log("Case Notes EDIT Post params");
               // console.log(postParams);
                CaseCRUDapiService.getCRUDOperationResult(postParams, CASE_CRUD_CONSTANTS.CASENOTES.DELETE).success(function (result) {

                    output_msg = result[0].o_outputMessage;

                    if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            //  ReasonOrTransKey: CASE_CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }



                        CaseApiService.getApiDetails($scope.caseNotes.caseKey, CASE_CONSTANTS.CASE_NOTES).success(function (result) {
                          //  console.log("case key");
                           // console.log($scope.caseNotes.caseKey);
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            params.grid.data = result;
                            CaseMultiDataService.setData(result, CASE_CONSTANTS.CASE_NOTES);

                            myApp.hidePleaseWait();

                            // emailData.push(PushParams);
                            $uibModalInstance.close(output);

                            // emailData.push(PushParams);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);
                            if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                            }
                            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                            }

                        });

                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE, CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.DELETE_FAILURE_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    //else if (result.data == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    var output = {
                    //        finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output);
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);
