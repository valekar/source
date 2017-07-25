angular.module('transaction').controller("AssociateCaseCtrl", ['$scope', '$state', '$filter', '$uibModalInstance', 'uiGridConstants', 'uibDateParser', '$rootScope', 'TransactionDataServices', 'CaseDataServices',
    'CaseServices', 'associateParams', '$location', '$uibModalStack', '$localStorage',
    function ($scope, $state, $filter, $uibModalInstance, uiGridConstants, uibDateParser, $rootScope, TransactionDataServices, CaseDataServices, CaseServices, associateParams, $location, $uibModalStack, $localStorage) {

        CaseDataServices.setAssociatedData(associateParams);

        //console.log("State Param in Associate Controller");
        //console.log($state);

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                var caseAssociatedTransKey = CaseDataServices.getAssociatedTransKey();
                var caseAssociatedCaseKey = CaseDataServices.getAssociatedCaseKey();

                var searchParams = {
                    "TransactionKey": caseAssociatedTransKey,
                    "AssociatedCaseKey": $scope.associateCaseNumber
                }

               // console.log(searchParams);

                CaseServices.setAssociateCaseData(searchParams).success(function (result) {
                    if (result.length > 0) {

                        output_msg = result[0].o_outputMessage;
                        if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                            $scope.pleaseWait = { "display": "none" };
                            var output = {
                                finalMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                                ConfirmationMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                            }
                            $uibModalInstance.close(output);
                        }
                        else if (output_msg == "Success") {
                            var output = {
                                finalMessage: "Case Key has been successfully associated",
                                ConfirmationMessage: "Success "
                            }

                           // console.log("In Associate Pop up case");
                           // console.log($state);

                            $uibModalInstance.close(output);
                        }
                        else {
                            var output = {
                                finalMessage: "Case Key association failed",
                                ConfirmationMessage: "Failure "
                            }

                            $uibModalInstance.close(output);
                        }
                    }

                }).error(function (result) {
                    $scope.pleaseWait = { "display": "none" };
                    if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                        angular.element("#accessDeniedModal").modal();
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }
                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');

        };
    }]);

angular.module('transaction').controller("CaseSearchCtrl", ['$scope', '$state', '$uibModal', '$uibModalStack', 'uiGridConstants', 'TransactionServices', 'CaseServices', 'CaseDataServices', 'TransactionDataServices', '$rootScope', '$localStorage',
    function ($scope, $state, $uibModal, $uibModalStack, uiGridConstants, TransactionServices, CaseServices, CaseDataServices, TransactionDataServices, $rootScope, $localStorage) {

        /* Case implementation starts*/
        $scope.case = {

            searchTerm: "CaseNumber",
            noResults: "No search results found!",
            toggleNoResults: false,
            searchTerms: caseSearchTerms(),
            toggleQuickSearch: true,
            togglePleaseWait: false,
            name: "",
            number: "",
            type: "",
            constName: "",
            userName: "",
            status: "",
        }

        $scope.case.quickSearch = function () {

            //Hide the previous message
            $scope.case.toggleNoResults = false;

            $scope.case.togglePleaseWait = true;

            //Before searching again reset the search criteria
            $scope.case.number = "";
            $scope.case.name = "";
            $scope.case.type = "";
            $scope.case.status = "";
            $scope.case.constName = "";
            $scope.case.userName = "";



            if ($scope.case.searchTerm == "CaseNumber") {
                $scope.case.number = $scope.case.input;
            }
            else if ($scope.case.searchTerm == "CaseName") {
                $scope.case.name = $scope.case.input;
            }
            else if ($scope.case.searchTerm == "ConstituentName") {
                $scope.case.constName = $scope.case.input;
            }
            else if ($scope.case.searchTerm == "UserName") {
                $scope.case.userName = $scope.case.input;
            }
            else if ($scope.case.searchTerm == "Status") {
                $scope.case.status = $scope.case.input;
            }
            else if ($scope.case.searchTerm == "Type") {
                $scope.case.type = $scope.case.input;
            }

            searchParams =
             {
                 "CaseId": $scope.case.number,
                 "CaseName": $scope.case.name,
                 "ReferenceSource": "",
                 "ReferenceId": "",
                 "CaseType": $scope.case.type,
                 "CaseStatus": $scope.case.status,
                 "ConstituentName": $scope.case.constName,
                 "UserName": $scope.case.userName,
                 "ReportedDateFrom": "",
                 "ReportedDateTo": "",
                 "UserId": $scope.case.userName
             }

            if ($scope.case.input) {
                var postParams = { "CaseInputSearchModel": [] };
                postParams["CaseInputSearchModel"].push(searchParams);

                CaseServices.getCaseAdvSearchResults(postParams).success(function (result) {
                    if (result.length > 0) {
                       // debugger;
                       // console.log(result);
                        if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                            $scope.case.togglePleaseWait = false;
                            angular.element("#accessDeniedModal").modal();
                        }
                        else {
                            $scope.case.togglePleaseWait = false;
                            CaseDataServices.setTransSearchResultsData(result);
                           // console.log("State Param in Case Search Controller");
                           // console.log($state);
                            getQuickCasePopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices);
                        }
                    }
                    else {
                        $scope.case.togglePleaseWait = false;
                        $scope.case.toggleNoResults = true;
                        $scope.case.noResults = "No search results found!";
                    }
                }).error(function (result) {
                    $scope.case.togglePleaseWait = false;
                    if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                        angular.element("#accessDeniedModal").modal();
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                    else {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                });

            }
            else {
                $scope.case.togglePleaseWait = false;
                $scope.case.toggleNoResults = true;
                $scope.case.noResults = "Please provide a search criteria to search!";
            }

           // console.log(postParams);
        }

        $scope.case.advSearch = function () {
            getAdvanceCasePopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices);
        }

        $scope.case.createCase = function () {
            getAddCasePopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices);
        }
    }]);

angular.module('transaction').controller("QuickCaseSearchCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'TransactionDataServices', 'CaseDataServices', 'CaseServices', '$rootScope', '$localStorage',
    function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, TransactionDataServices, CaseDataServices, CaseServices, $rootScope, $localStorage) {
        var columnDefs = CaseDataServices.getTransSearchResultsColumnDef();
       // console.log("Column Defs " + columnDefs);
        var uiScrollNever = uiGridConstants.scrollbars.NEVER;
        var uiScrollAlways = uiGridConstants.scrollbars.ALWAYS;
        var rows;

        //passing rootscope for toggling filters
        $scope.case = {
            currentPage: 1,
            totalItems: 0,
            gridOptions: CaseDataServices.getTransSearchGridOptions(uiGridConstants, columnDefs),
            results: []
        }

        $scope.case.gridOptions.onRegisterApi = function (grid) {
            $scope.case.gridApi = grid;

            $scope.case.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                var msg = 'row selected ' + row.isSelected;
                rows = $scope.case.gridApi.selection.getSelectedRows();
            });
        }

        $scope.case.results = CaseDataServices.getTransSearchResultsData();
        $scope.case.totalItems = $scope.case.results.length;
        $scope.case.gridOptions = getCaseGridLayout($scope.case.gridOptions, uiGridConstants, $scope.case.results);


        var caseAssociatedTransKey = CaseDataServices.getAssociatedTransKey();
        var caseAssociatedCaseKey = CaseDataServices.getAssociatedCaseKey();

        $scope.case.caseAssociateSubmit = function () {

            var searchParams = {
                "TransactionKey": caseAssociatedTransKey,
                "AssociatedCaseKey": rows[0].case_key
            }

            CaseServices.setAssociateCaseData(searchParams).success(function (result) {
                if (result.length > 0) {
                    $scope.case.togglePleaseWait = false;
                    $scope.case.toggleAdvSearchResults = true;
                    $scope.case.toggleAdvSearch = false;
                    $scope.case.results = result;
                    output_msg = result[0].o_outputMessage;
                    if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                        $scope.pleaseWait = { "display": "none" };
                        var output = {
                            finalMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        $uibModalInstance.close(output);
                    }
                    else if (output_msg == "Success") {
                        var output = {
                            finalMessage: "Case Key has been successfully associated",
                            ConfirmationMessage: "Success "
                        }

                        $uibModalInstance.close(output);
                    }
                    else {
                        var output = {
                            finalMessage: "Case Key association failed",
                            ConfirmationMessage: "Failure "
                        }

                        $uibModalInstance.close(output);
                    }
                    $scope.case.totalItems = $scope.case.results.length;
                    $scope.case.gridOptions = getCaseGridLayout($scope.case.gridOptions, uiGridConstants, $scope.case.results);
                }
                else {
                    $scope.case.togglePleaseWait = false;
                    $scope.toggleNoResults = true;
                }

            }).error(function (result) {
                $scope.case.togglePleaseWait = false;
                $uibModalInstance.dismiss('cancel');

                if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                    angular.element("#accessDeniedModal").modal();
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
                else {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
            });
        }

        $scope.case.pageChanged = function (page) {
            $scope.case.gridApi.pagination.seek(page);
        };

        $scope.case.back = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);


angular.module('transaction').controller('AdvanceCaseCtrl', ['$scope', '$state', '$location', '$uibModalInstance', '$filter', 'uiGridConstants', 'CaseDataServices', 'CaseServices', 'mainService', '$rootScope',
     function ($scope, $state, $location, $uibModalInstance, $filter,
uiGridConstants, CaseDataServices, CaseServices, mainService, $rootScope) {

         var columnDefs = CaseDataServices.getTransSearchResultsColumnDef();
        // console.log("Column Defs " + columnDefs);
         var uiScrollNever = uiGridConstants.scrollbars.NEVER;
         var uiScrollAlways = uiGridConstants.scrollbars.ALWAYS;
         var rows;

         $scope.case = {
             name: "",
             number: "",
             CRMSystem: "",
             CRMSysId: "",
             type: "",
             constName: "",
             userName: "",
             dateFrom: "",
             dateTo: "",
             status: "",
             noResults: "No Results Found!",
             togglePleaseWait: false,
             toggleNoResults: false,
             toggleAdvSearchResults: false,
             toggleAdvSearch: true,
             results: [],
             gridOptions: CaseDataServices.getTransSearchGridOptions(uiGridConstants, columnDefs),

             totalItems: 0,
             currentPage: 1,
             dateOptions: {
                 formatYear: 'yy',
                 maxDate: new Date(2020, 5, 22),
                 minDate: new Date(),
                 startingDay: 1
             }
         };

         mainService.getUsername().then(function (result) {
             username = result.data;
         });

         $scope.case.searchMeChange = function () {
             if ($scope.case.searchMe) {
                 $scope.case.userName = username;
                 $scope.case.userNameDisabled = true;
             }
             else {
                 $scope.case.userName = "";
                 $scope.case.userNameDisabled = false;
             }
         };

         $scope.case.gridOptions.onRegisterApi = function (grid) {
             $scope.case.gridApi = grid;

             $scope.case.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                 var msg = 'row selected ' + row.isSelected;
                // console.log("Inside row selection");
                 rows = $scope.case.gridApi.selection.getSelectedRows();
               //  console.log(rows[0].case_key);
             });
         }

       //  console.log("Multiple Selected Rows " + rows);

         $scope.case.openFirstCalendar = function () {
             $scope.case.popup1.opened = true;
         };
         $scope.case.popup1 = {
             opened: false
         };

         $scope.case.openSecondCalendar = function () {
             $scope.case.popup2.opened = true;
         };
         $scope.case.popup2 = {
             opened: false
         };

         $scope.case.caseSearch = function () {

             //Hide the no results dialog if it is visible before
             $scope.case.toggleNoResults = false;

             var searchParams = {
                 "CaseId": $scope.case.number,
                 "CaseName": $scope.case.name,
                 "ReferenceSource": $scope.case.CRMSystem,
                 "ReferenceId": $scope.case.CRMSysId,
                 "CaseType": $scope.case.type,
                 "ConstituentName": $scope.case.constName,
                 "ReportedDateFrom": $scope.case.dateFrom,
                 "ReportedDateTo": $scope.case.dateTo,
                 "UserName": $scope.case.userName,
                 "CaseStatus": $scope.case.caseStatus
             }

           //  console.log("searchParams");
           //  console.log(searchParams);

           //  console.log("For Advanced Search the params are");
          //   console.log(postParams);

           //  console.log(Object.keys(searchParams).length);

             if (searchParams.CaseId != "" || searchParams.CaseName != "" || searchParams.CaseType != "" ||
                 searchParams.ReferenceId != "" || searchParams.ReferenceSource != "" ||
                 searchParams.ConstituentName != "" ||
                 searchParams.ReportedDateFrom != "" || searchParams.ReportedDateTo != "" ||
                 searchParams.UserName != "" || searchParams.CaseStatus !="") {
                 $scope.case.togglePleaseWait = true;

                 var postParams = { "CaseInputSearchModel": [] };

                 if (searchParams.ReportedDateFrom)
                     searchParams.ReportedDateFrom = $filter('date')(new Date($scope.case.dateFrom), 'MM/dd/yyyy');
                 else if (searchParams.ReportedDateTo)
                     searchParams.ReportedDateTo = $filter('date')(new Date($scope.case.dateTo), 'MM/dd/yyyy');

                 postParams["CaseInputSearchModel"].push(searchParams);

               //  console.log("Formed Case Advanced Search Params");

               //  console.log(postParams);

                 CaseServices.getCaseAdvSearchResults(postParams).success(function (result) {
                    // console.log("returned from service");
                   //  console.log(result);
                     if (result.length > 0 && result != "Error") {
                         if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                             angular.element("#accessDeniedModal").modal();
                             $scope.case.togglePleaseWait = false;
                             $scope.case.toggleAdvSearch = false;
                         }
                         else {
                             $scope.case.togglePleaseWait = false;
                             $scope.case.toggleAdvSearchResults = true;
                             $scope.case.toggleAdvSearch = false;
                             $scope.case.results = result;
                             $scope.case.totalItems = $scope.case.results.length;
                             $scope.case.gridOptions = getCaseGridLayout($scope.case.gridOptions, uiGridConstants, $scope.case.results);
                         }
                     }
                     else {
                         $scope.case.togglePleaseWait = false;
                         $scope.case.toggleNoResults = true;
                         $scope.case.noResults = "No Search Results Found!";
                     }
                 }).error(function (result) {
                     $scope.case.togglePleaseWait = false;

                     //Close the advanced search modal and show the error message pop up
                     $uibModalInstance.dismiss('cancel');

                     if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                         angular.element("#accessDeniedModal").modal();
                     }
                     else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                         messageTransPopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                     }
                     else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                         messageTransPopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                     }
                     else {
                         messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                     }

                 });
             }
             else {
                 $scope.case.togglePleaseWait = false;
                 $scope.case.toggleNoResults = true;
                 $scope.case.noResults = "Please provide a search criteria to search!";
             }
         };

         var caseAssociatedTransKey = CaseDataServices.getAssociatedTransKey();
         var caseAssociatedCaseKey = CaseDataServices.getAssociatedCaseKey();
       //  console.log("In advance search results controller --- " + caseAssociatedTransKey);

         $scope.case.caseAssociateSubmit = function () {

             var searchParams = {
                 "TransactionKey": caseAssociatedTransKey,
                 "AssociatedCaseKey": rows[0].case_key
             }

             CaseServices.setAssociateCaseData(searchParams).success(function (result) {
                 if (result.length > 0) {
                   //  console.log("After submit is pressed in search results submit... ");
                     $scope.case.togglePleaseWait = false;
                     $scope.case.toggleAdvSearchResults = true;
                     $scope.case.toggleAdvSearch = false;
                     $scope.case.results = result;
                   //  console.log("After SP execution ");
                     output_msg = result[0].o_outputMessage;
                   //  console.log("Output Message ");
                   //  console.log(output_msg);
                     if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                         $scope.pleaseWait = { "display": "none" };
                         var output = {
                             finalMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                             ConfirmationMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                         }
                         $uibModalInstance.close(output);
                     }
                     else if (output_msg == "Success") {
                         var output = {
                             finalMessage: "Case Key has been successfully associated",
                             ConfirmationMessage: "Success "
                         }

                         $uibModalInstance.close(output);
                     }
                     else {
                         var output = {
                             finalMessage: "Case Key association failed",
                             ConfirmationMessage: "Failure "
                         }

                         $uibModalInstance.close(output);
                     }
                     $scope.case.totalItems = $scope.case.results.length;
                     $scope.case.gridOptions = getCaseGridLayout($scope.case.gridOptions, uiGridConstants, $scope.case.results);
                 }
                 else {
                     $scope.case.togglePleaseWait = false;
                     $scope.toggleNoResults = true;
                 }

             }).error(function (result) {
                 $scope.case.togglePleaseWait = false;

                 //Close the modal and show the error message pop up
                 $uibModalInstance.dismiss('cancel');

                 if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                     angular.element("#accessDeniedModal").modal();
                 }
                 else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                     messageTransPopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                 }
                 else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                     messageTransPopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                 }
                 else {
                     messageTransPopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                 }
             });


         };

         $scope.case.backToSearchForm = function () {
             $scope.case.toggleAdvSearch = true;
             $scope.case.toggleAdvSearchResults = false;
         }

         $scope.case.pageChanged = function (page) {
             $scope.case.gridApi.pagination.seek(page);
         };

         $scope.case.cancel = function () {
             $uibModalInstance.dismiss('cancel');
         };

     }]);


angular.module('transaction').controller('AddCaseCtrl', ['$scope', '$http', '$state', '$location', '$uibModalInstance', '$filter', 'uiGridConstants', 'CaseDataServices', 'CaseServices', 'TransactionServices', '$rootScope', '$q',
     function ($scope, $http, $state, $location, $uibModalInstance, $filter,
uiGridConstants, CaseDataServices, CaseServices, TransactionServices, $rootScope, $q) {
         var initialize = function () {

             $scope.caseName = "";

             $scope.CaseDateFrom = null;
             $scope.CaseDateTo = null;
             $scope.inlineOptions = {
                 minDate: new Date(),
                 showWeeks: true
             };

             $scope.dateOptions = {
                 formatYear: 'yy',
                 maxDate: new Date(2020, 5, 22),
                 minDate: new Date(),
                 startingDay: 1
             };
             $scope.toggleMin = function () {
                 $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
                 $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
             };

             $scope.toggleMin();

             $scope.open1 = function () {
                 $scope.popup1.opened = true;
             };
             $scope.popup1 = {
                 opened: false
             };

             $scope.open2 = function () {
                 $scope.popup2.opened = true;
             };
             $scope.popup2 = {
                 opened: false
             };
             $scope.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
             $scope.format = $scope.formats[0];

             var postParams = {};



             $scope.case = {
                 name: "",
                 CRMSystem: "",
                 CRMSysId: "",
                 type: "",
                 intakeDept: "",
                 intakeChannel: "",
                 reportDate: "",
                 details: "",
                 toggleNoResults: false,
                 noResults: "Please fill in the mandatory fields",
                 togglePleaseWait: false,
                 caseClose: false
             }

         };

         initialize();

         var formdata;
         $scope.getTheFiles = function ($files) {
            // console.log($files);
             formdata = new FormData();
             angular.forEach($files, function (value, key) {
                 formdata.append(key, value);
             });
         };


         $scope.submit = function () {

             if ($scope.case.name != "" && $scope.case.type != "" && $scope.case.intakeDept != "") {

                 //Show the processing progress bar

                 //myApp.showPleaseWait();

                // console.log("Please wait status");

               //  console.log($scope.case.togglePleaseWait);

                 $scope.case.togglePleaseWait = true;

                 var caseStatusSet = "Open";

                 if ($scope.case.caseClose)
                     caseStatusSet = "Closed";

                 var postParams = {

                     "CreateCaseInput": {
                         "case_nm": $scope.case.name,
                         "case_desc": $scope.case.details,
                         "ref_src_desc": $scope.case.CRMSystem,
                         "ref_id": $scope.case.CRMSysId,
                         "typ_key_desc": $scope.case.type,
                         "intake_chan_desc": $scope.case.intakeChannel,
                         "intake_owner_dept_desc": $scope.case.intakeDept,
                         "cnst_nm": null,
                         "crtd_by_usr_id": "",
                         "status": caseStatusSet,
                         "report_dt": $filter('date')(new Date($scope.case.reportDate), 'MM/dd/yyyy'),
                         "attchmnt_url": null
                     },
                     "SaveCaseSearchInput":
                        {
                            "first_name": "",
                            "last_name": "",
                            "address_line": "",
                            "city": "",
                            "state": "",
                            "zip": "",
                            "phone_number": "",
                            "email_address": "",
                            "master_id": "",
                            "source_system": "",
                            "chapter_source_system": "",
                            "source_system_id": "",
                            "constituent_type": ""
                        }
                 };

                 angular.element('#modalCover').css("pointer-events", "none");

                 //do something
                 var output_msg;
                 var trans_key;
                 var finalMessage;
                 var ReasonOrTransKey;
                 var ConfirmationMessage;

                 var uploadCasePromise;
                 // upload case files

                 if (formdata != null || formdata != undefined) {
                     uploadCasePromise = TransactionServices.uploadCaseFiles(formdata).then(function (result) {
                         if (result === "Failed") {
                             messageTransPopup($rootScope, "Upload Failed! Case creation process aborted.", "Alert");
                             $uibModalInstance.dismiss('cancel');

                             $scope.case.toggleCaseLoader = false;
                             return;
                         }
                         else {
                             postParams.CreateCaseInput["attchmnt_url"] = result;
                             //$scope.submitData(postParams);
                         }
                     });
                 }

                 $q.all([uploadCasePromise]).then(function () {
                     CaseServices.CreateCase(postParams).success(function (result) {

                         output_msg = result[0].o_outputMessage;
                         case_key = result[0].o_case_seq;

                       //  console.log("After the case has been created");

                        // console.log(output_msg);
                        // console.log(case_key);

                         if (output_msg == TRANSACTION_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {

                             //Associate the case key 

                             var caseAssociatedTransKey = CaseDataServices.getAssociatedTransKey();
                             var caseAssociatedCaseKey = case_key;

                           //  console.log("Newly associated case key, after creation is ");
                           //  console.log(caseAssociatedCaseKey);

                             var searchParams = {
                                 "TransactionKey": caseAssociatedTransKey,
                                 "AssociatedCaseKey": case_key
                             }

                             CaseServices.setAssociateCaseData(searchParams).success(function (result) {
                                 if (result.length > 0) {

                                     $scope.case.togglePleaseWait = false;

                                     var output = {
                                         finalMessage: TRANSACTION_CRUD_CONSTANTS.SUCCESS_MESSAGE,
                                         ReasonOrTransKey: "The case sequence key is " + case_key,
                                         ConfirmationMessage: TRANSACTION_CRUD_CONSTANTS.SUCCESS_CONFIRM
                                     }

                                     $uibModalInstance.close(output);
                                 }
                             }).error(function (result) {
                                 $scope.case.togglePleaseWait = false;
                                 if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                                     angular.element("#accessDeniedModal").modal();
                                 }
                                 else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                     messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                 }
                                 else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                                     messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                                 }
                                 else {
                                     messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                 }
                             });

                         }
                         else if (output_msg == TRANSACTION_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == TRANSACTION_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                             var output = {
                                 finalMessage: TRANSACTION_CRUD_CONSTANTS.FAILURE_MESSAGE,
                                 ReasonOrTransKey: TRANSACTION_CRUD_CONSTANTS.FAIULRE_REASON,
                                 ConfirmationMessage: TRANSACTION_CRUD_CONSTANTS.FAILURE_CONFIRM
                             }
                             $scope.case.togglePleaseWait = false;
                             $uibModalInstance.close(output);
                         }
                         else if (result.data == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                             var output = {
                                 finalMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                                 ConfirmationMessage: TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                             }
                             $scope.case.togglePleaseWait = false;
                             $uibModalInstance.close(output);
                         }
                         else {//If Result is "Error"
                             $scope.case.togglePleaseWait = false;
                             $uibModalInstance.dismiss('cancel');
                             messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");

                         }

                     }).error(function (result) {
                         $scope.case.togglePleaseWait = false;
                         $uibModalInstance.dismiss('cancel');

                         if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                             angular.element("#accessDeniedModal").modal();
                         }
                         else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                             messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                         }
                         else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                             messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                         }
                         else if (result[0].o_outputMessage != "Success") {
                             messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                         }
                     });
                 });

             }
             else {
                 toggleNoResults = true;
             }
         }

         $scope.case.cancel = function () {
             $uibModalInstance.dismiss('cancel');
         };

     }]);

angular.module('transaction').controller('CaseInfoCtrl', ['$scope', '$state', '$location', '$uibModalInstance', '$filter', 'uiGridConstants', 'CaseDataServices', 'CaseServices', 'infoParams',
     function ($scope, $state, $location, $uibModalInstance, $filter,
uiGridConstants, CaseDataServices, CaseServices, infoParams) {

        // console.log("Info Key Param in case edit modal controller");
        // console.log(infoParams);

         $scope.caseInfo = {
             "caseKey": "",
             "caseName": "",
             "caseDesc": "",
             "intakeChannels": "",
             "intakeOwnerDepts": "",
             "CRMSystems": "",
             "CRMSystem": "",
             "caseType": "",
             "constName": "",
             "statusType": "",
             "caseReportDate": "",
             "userName": ""
         };
         $scope.caseInfo.caseKey = infoParams[0].case_key;
         $scope.caseInfo.caseName = infoParams[0].case_nm;
         $scope.caseInfo.caseDesc = infoParams[0].case_desc;
         $scope.caseInfo.intakeChannels = infoParams[0].intake_chan_value;
         $scope.caseInfo.intakeOwnerDepts = infoParams[0].intake_owner_dept_value;
         $scope.caseInfo.CRMSystems = infoParams[0].ref_src_dsc;
         $scope.caseInfo.CRMSystem = infoParams[0].ref_id;
         $scope.caseInfo.caseType = infoParams[0].typ_key_dsc;
         $scope.caseInfo.constName = infoParams[0].cnst_nm;
         $scope.caseInfo.statusType = infoParams[0].status;
         $scope.caseInfo.caseReportDate = infoParams[0].report_dt;
         $scope.caseInfo.userName = infoParams[0].crtd_by_usr_id;

         $scope.cancel = function () {
             $uibModalInstance.dismiss('cancel');
         };

     }]);

function caseSearchTerms() {
    return [
       "CaseNumber", "CaseName", "ConstituentName", "UserName", "Status", "Type"
    ];
}

function getQuickCasePopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices) {
    var templateUrl = BasePath + 'App/Transaction/Views/common/QuickCaseSearch.tpl.html';
    var controller = 'QuickCaseSearchCtrl';
    OpenModal($scope, $uibModal, null, templateUrl, controller, null, null, 'lg', $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices);
}

function getAddCasePopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices, $localStorage) {
    var templateUrl = BasePath + 'App/Transaction/Views/common/CreateCase.tpl.html';
    var controller = 'AddCaseCtrl';
    OpenModal($scope, $uibModal, null, templateUrl, controller, null, null, 'lg', $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices, $localStorage);
}

function getAdvanceCasePopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices) {
    var templateUrl = BasePath + 'App/Transaction/Views/common/AdvanceCaseSearch.tpl.html';
    var controller = 'AdvanceCaseCtrl';
    OpenModal($scope, $uibModal, null, templateUrl, controller, null, null, 'lg', $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices);
}

function OpenModal($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope, $state, $uibModalStack, uiGridConstants, TransactionDataServices, TransactionServices) {

    var CssClass = '';
    if (size === 'lg') {
        CssClass = 'app-modal-window';
    }

    var ModalInstance = ModalInstance || $uibModal.open({
        animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass
    });

    ModalInstance.result.then(function (result) {
       // console.log("Modal Instance Result " + result);
      //  console.log("State Param before");
      //  console.log($state);
        modalMessage($rootScope, result, $state, $uibModalStack, TransactionDataServices);
    })
}

//function getGridColumns(uiGridConstants, $rootScope) {

//    var GRID_HEADER_TEMPLATE = '<div ng-class="{ \'sortable\': sortable }" tooltip-placement="top-left" tooltip-append-to-body="true" uib-tooltip="{{col.displayName}}">' +
//                                    '<div class="ui-grid-cell-contents " col-index="renderIndex" title="TOOLTIP">' +
//                                        '<span class="ui-grid-header-cell-label">{{ col.displayName CUSTOM_FILTERS }}</span>' +
//                                        '<span ui-grid-visible="col.sort.direction" ng-class="{ \'ui-grid-icon-up-dir\': col.sort.direction == asc, \'ui-grid-icon-down-dir\': col.sort.direction == desc, \'ui-grid-icon-blank\': !col.sort.direction }">&nbsp</span>' +
//                                    '</div>' +
//                                    '<div class="ui-grid-column-menu-button" ng-if="grid.options.enableColumnMenus && !col.isRowHeader  && col.colDef.enableColumnMenu !== false" ng-click="toggleMenu($event)" ng-class="{\'ui-grid-column-menu-button-last-col\': isLastCol}">' +
//                                        '<i class="ui-grid-icon-angle-down">&nbsp;</i>' +
//                                    '</div>' +
//                                    '<div ui-grid-filter></div>' +
//                                '</div>';


//    var GRID_FILTER_TEMPLATE = '<div >' +
//                                    '<div class="ui-grid-cell-contents ">' +
//                                        '&nbsp;' +
//                                    '</div>' +
//                                    '<div ng-click="grid.appScope.toggleFiltering(grid)" ng-if="grid.options.enableFiltering"><span class="glyphicon glyphicon-remove-circle" style="font-size:1.5em;margin-left:1%;"></span> </div>' +
//                                '</div>';

//    return
//    [{
//        field: 'case_key', displayName: 'Case Number', width: "*"
//    },
//    {
//        field: 'case_nm', displayName: 'Case Name', enableCellEdit: false,
//        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
//        headerTooltip: 'Case Name', width: "*"

//    },
//    { field: 'case_desc', headerTooltip: 'Case description', displayName: 'Case description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*" },
//    { field: 'ref_src_key', displayName: 'CRM System', width: "*" },
//    { field: 'ref_id', displayName: 'CRM System ID', visible: false },
//    { field: 'typ_key', displayName: 'Type', visible: false },
//    { field: 'cnst_nm', displayName: 'Constituent Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false },
//    { field: 'crtd_by_usr_id', displayName: 'User Name', visible: false },
//    { field: 'report_dt', displayName: 'Reported Date', visible: false },
//    { field: 'status', displayName: 'Status', visible: false }
//    ]
//}

function getTransSearchGridOptions(uiGridConstants, columnDefs) {
    var gridOptions = {
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        enableFiltering: false,
        enableSelectAll: false,
        selectionRowHeaderWidth: 15,
        rowHeight: 45,
        paginationPageSize: 8,
        enablePagination: true,
        paginationPageSizes: [8],
        enablePaginationControls: false,
        enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableGridMenu: true,
        showGridFooter: false,
        columnDefs: columnDefs

    };
    gridOptions.data = '';
    return gridOptions;
}

function getCaseGridLayout(gridOptions, uiGridConstants, results) {

   // console.log("Setting data in grid now");
   // console.log(results);
    gridOptions.data = '';
    gridOptions.data.length = 0;
    gridOptions.data = results;

    if (gridOptions.columnDefs.length > 6) {
        gridOptions.enableHorizontalScrollbar = 1;
    }

    angular.element(document.getElementsByClassName('CaseAssociation')).css('height', '0px');
    return gridOptions;
};


function modalMessage($rootScope, result, $state, $uibModalStack, TransactionDataServices) {
    $rootScope.FinalMessage = result.finalMessage;
    $rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;

   // console.log("State Param");
   // console.log($state);

    $("#iConfirmationModal").modal({ backdrop: "static" });

    $("#iConfirmationModal").on('hidden.bs.modal', function () {
        $state.go('transaction.search.results', {});
        $uibModalStack.dismissAll();

        var searchResultsParam = TransactionDataServices.getTransSavedSearchParams();


       // console.log("After Final Modal close and redirection ...");
       // console.log(searchResultsParam);

        $rootScope.$broadcast('gridRefreshResult', searchResultsParam);

    });
}

function messageTransPopup($rootScope, message, header) {
    $rootScope.message = message;
    $rootScope.header = header;
    angular.element("#iErrorModal").modal();
}