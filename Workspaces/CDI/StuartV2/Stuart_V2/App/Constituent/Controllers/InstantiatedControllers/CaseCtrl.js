

angular.module('constituent').controller("CaseSearchCtrl", ['$scope', 'constituentServices', 'constituentCRUDoperations', '$uibModal', 'constMultiDataService','$rootScope',
function ($scope, constituentServices, constituentCRUDoperations, $uibModal, constMultiDataService, $rootScope) {

    /* Case implementation starts*/
    $scope.case = {

        searchTerm: "caseNo",

        searchTerms: caseSearchTerms(),
        toggleQuickSearch: true,
        togglePleaseWait: false,
        name: "",
        number: "",
        // CRMSystem: "",
        // CRMSysId: "",
        type: "",
        constName: "",
        userName: "",
        // dateFrom: "",
        //dateTo: "",
        status: "",

    }



    $scope.case.quickSearch = function () {
        $scope.case.togglePleaseWait = true;
        if ($scope.case.searchTerm == "caseNo") {
            $scope.case.number = $scope.case.input;
        }
        else if ($scope.case.searchTerm == "caseName") {
            $scope.case.name = $scope.case.input;
        }
        else if ($scope.case.searchTerm == "constituentName") {
            $scope.case.constName = $scope.case.input;
        }
        else if ($scope.case.searchTerm == "userName") {
            $scope.case.userName = $scope.case.input;
        }
        else if ($scope.case.searchTerm == "status") {
            $scope.case.status = $scope.case.input;
        }
        else if ($scope.case.searchTerm == "type") {
            $scope.case.type = $scope.case.input;
        }
        var postParams = {
            "CaseInputSearchModel": [{
                "CaseId": $scope.case.number,
                "CaseName": $scope.case.name,
               // "ReferenceSource": $scope.case.CRMSystem,
                //"ReferenceId": $scope.case.CRMSysId,
                "CaseType": $scope.case.type,
                "CaseStatus": $scope.case.status,
                "ConstituentName": $scope.case.constName,
                "UserName": $scope.case.userName,
                //"ReportedDateFrom": $filter('date')(new Date($scope.case.dateFrom), 'dd/MM/yyyy'),
               // "ReportedDateTo": $filter('date')(new Date($scope.case.dateTo), 'dd/MM/yyyy'),
                "UserId": $scope.case.userName
            }]
        }
        constituentServices.getAdvCaseSearchResutls(postParams).success(function (result) {
            if (result.length > 0) {
               
                    $scope.case.togglePleaseWait = false;
                    constMultiDataService.setData(result, HOME_CONSTANTS.QUICK_CASE_SEARCH);
                    constituentCRUDoperations.getQuickCasePopup($scope, $uibModal);

            }
            else {
                $scope.case.togglePleaseWait = false;
                $scope.toggleNoResults = true;

            }
        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            $scope.case.togglePleaseWait = false;
            caseErrorPopup($rootScope, result)
        });

        console.log(postParams);
    }

    $scope.case.advSearch = function () {
        constituentCRUDoperations.getAdvanceCasePopup($scope, $uibModal);
    }

    $scope.case.createCase = function () {
        constituentCRUDoperations.getAddCasePopup($scope, $uibModal);
    }



}]);

function caseErrorPopup($rootScope, result) {
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

angular.module('constituent').controller('AdvanceCaseCtrl', ['$scope', '$uibModalInstance','$filter','constMultiGridService',
     'constMultiDataService', 'uiGridConstants', 'ConstUtilServices', 'constituentApiService','constituentServices','mainService',
     function ($scope, $uibModalInstance,$filter,constMultiGridService,
    constMultiDataService, uiGridConstants, ConstUtilServices, constituentApiService, constituentServices, mainService) {
         var username = "";
         $scope.case = {
             name: "",
             number: "",
             CRMSystem: "",
             CRMSysId: "",
             type: "",
             constName: "",
             userName: "",
            // dateFrom: "",
            // dateTo: "",
             status: "",
             noResults:"No Results Found!",
             togglePleaseWait: false,
             toggleNoResults: false,
             toggleAdvSearchResults: false,
             toggleAdvSearch: true,
             results: [],
             gridOptions: constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.ADV_CASE_SEARCH),
             totalItems: 0,
             currentPage: 1,
             format:'MM/dd/yyyy',
             dateOptions :{
                 formatYear: 'yy',
                 maxDate: new Date(2020, 5, 22),
                 minDate: new Date(2000,1,1),
                 startingDay: 1
             },
             userNameDisabled:false
         };
         //srini
         //will check with later
       /*  mainService.getUsername().success(function (result) {
             username = result;
         }).error(function (result) {
             caseErrorPopup($rootScope, result);
         });*/

         mainService.getUsername().then(function (result) {
             username = result.data;

             console.log(result);
             
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
         };

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
             $scope.case.togglePleaseWait = true;

             if($scope.case.dateFrom){
                 var dateFrom =  $filter('date')(new Date($scope.case.dateFrom), "MM/dd/yyyy");
             }

             if($scope.case.dateTo){
                var dateTo = $filter('date')(new Date($scope.case.dateTo), "MM/dd/yyyy");
             }
             var postParams = {
                 "CaseInputSearchModel": [{
                     "CaseId": $scope.case.number,
                     "CaseName": $scope.case.name,
                     "ReferenceSource": $scope.case.CRMSystem,
                     "ReferenceId": $scope.case.CRMSysId,
                     "CaseType": $scope.case.type,
                     "CaseStatus": $scope.case.status,
                     "ConstituentName": $scope.case.constName,
                     "UserName": $scope.case.userName,
                     "ReportedDateFrom": dateFrom,
                     "ReportedDateTo": dateTo,
                     "UserId": $scope.case.userName
                 }]
                
             }

             console.log(postParams);
             //$scope.$destroy;
           constituentServices.getAdvCaseSearchResutls(postParams).success(function (result) {
                if (result.length > 0) {
                    /* if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                         $scope.pleaseWait = { "display": "none" };
                         $scope.case.togglePleaseWait = false;
                         $scope.case.toggleAdvSearchResults = false;
                         $scope.case.toggleAdvSearch = false;
                         angular.element("#accessDeniedModal").modal();
                     }
                     else {*/
                         $scope.case.togglePleaseWait = false;
                         $scope.case.toggleAdvSearchResults = true;
                         $scope.case.toggleAdvSearch = false;
                         $scope.case.results = result;
                         $scope.case.totalItems = $scope.case.results.length;
                         $scope.case.gridOptions =
                             constMultiGridService.getMultiGridLayout($scope.case.gridOptions, uiGridConstants, $scope.case.results, HOME_CONSTANTS.ADV_CASE_SEARCH);
                    // }
                 }
                 else {
                     $scope.case.togglePleaseWait = false;
                     $scope.case.toggleNoResults = true;

                 }
           }).error(function (result) {
               $scope.pleaseWait = { "display": "none" };
               $scope.case.togglePleaseWait = false;
               $scope.case.toggleAdvSearchResults = false;
               $scope.case.toggleAdvSearch = false;
               caseErrorPopup($rootScope, result);
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

         $scope.case.associateCase = function () {
             var row = $scope.case.gridApi.selection.getSelectedRows();
             $uibModalInstance.close(row[0].case_key);

         }

     }]);

angular.module('constituent').controller('QuickCaseCtrl', ['$scope', '$uibModalInstance', '$filter', 'constMultiGridService',
     'constMultiDataService', 'uiGridConstants', 'constituentServices','$rootScope',
     function ($scope, $uibModalInstance, $filter, constMultiGridService,
    constMultiDataService, uiGridConstants, constituentServices, $rootScope) {

        
         $scope.case = {
             gridOptions: constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.ADV_CASE_SEARCH),
             results :[]
         };

         $scope.case.gridOptions.onRegisterApi = function (grid) {
             $scope.case.gridApi = grid;
             $scope.case.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
               //  var msg = 'row selected ' + row.isSelected;

               //  rows = $scope.case.gridApi.selection.getSelectedRows();


             });
         }

       

         $scope.case.results = constMultiDataService.getData(HOME_CONSTANTS.QUICK_CASE_SEARCH);
         $scope.case.gridOptions =
                          constMultiGridService.getMultiGridLayout($scope.case.gridOptions, uiGridConstants, $scope.case.results, HOME_CONSTANTS.ADV_CASE_SEARCH);


       

         $scope.case.backToSearchForm = function () {
             $scope.case.toggleAdvSearch = true;
             $scope.case.toggleAdvSearchResults = false;
         }


         $scope.case.pageChanged = function (page) {
             $scope.case.gridApi.pagination.seek(page);
         };

         $scope.case.back = function () {
             $uibModalInstance.dismiss('cancel');
         };

         $scope.case.associateCase = function () {
             var row = $scope.case.gridApi.selection.getSelectedRows();
             $uibModalInstance.close(row[0].case_key);

         }

     }]);




angular.module('constituent').controller('AddCaseCtrl', ['$scope', '$filter', '$uibModalInstance', 'Upload', 'constituentServices','$rootScope',
     'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'ConstUtilServices', 'constituentApiService','$timeout',
     function ($scope, $filter, $uibModalInstance, Upload, constituentServices,$rootScope,
    constMultiDataService, constMultiGridService, uiGridConstants, ConstUtilServices, constituentApiService, $timeout) {
         //myApp.showPleaseWait();
         $scope.case = {
             details: "",
             format: 'MM/dd/yyyy',
             dateOptions : {
                 formatYear: 'yy',
                 maxDate: new Date(2020, 5, 22),
                 minDate: new Date(2000, 1, 1),
                 startingDay: 1
             },
             type: "",
             intakeDept: "",
             intakeChannel: "",
             CRMSystem: "",
             CRMSysId: "",
             reportDate: "",
             caseClose: 'Open',
             toggleCaseLoader: false

         };

         var formdata;
         $scope.case.getTheFiles = function ($files) {
             console.log($files);
             formdata = new FormData();
             angular.forEach($files, function (value, key) {
                 formdata.append(key, value);
             });
         };
         $scope.case.open = function () {
             $scope.case.popup.opened = true;
         };
         $scope.case.popup = {
            opened :false
         };        
         $scope.submit = function () {
             $scope.case.toggleCaseLoader = true;

             $timeout(function () {
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
                         "status": $scope.case.caseClose,
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
                 // upload case files
                 if (formdata != null || formdata != undefined) {
                     constituentServices.uploadCaseFiles(formdata).success(function (result) {
                         if (result === "Failed") {
                             messagePopup($rootScope, "Upload Failed! Case creation process aborted.", "Alert");
                             $uibModalInstance.dismiss('cancel');
                             //myApp.hidePleaseWait();
                             $scope.case.toggleCaseLoader = false;
                             return;
                         }
                         else {
                             postParams["attchmnt_url"] = result;
                             $scope.submitData(postParams);
                             //myApp.hidePleaseWait();
                             //$scope.case.toggleCaseLoader = false;
                         }
                     }).error(function (result) {
                         $uibModalInstance.dismiss('cancel');
                         caseErrorPopup($rootScope, result);
                     });
                 }
                 else {
                     $scope.submitData(postParams);
                   //  $scope.case.toggleCaseLoader = false;
                 }
             }, 10);                               
         }


         $scope.submitData = function (postParams) {
             constituentServices.createCase(postParams).success(function (result) {

                 output_msg = result[0].o_outputMessage;
                 trans_key = result[0].o_case_seq;



                 if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                     var output = {
                         finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                         ReasonOrTransKey: "The case sequence key is " + trans_key,
                         ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                     }
                     $scope.case.toggleCaseLoader = false;

                     $uibModalInstance.close(trans_key);

                 }
                 else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                     var result = {
                         Output: {
                             finalMessage: CRUD_CONSTANTS.FAILURE_MESSAGE,
                             ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                             ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                         }
                     }
                     $scope.case.toggleCaseLoader = false;
                     $uibModalInstance.close(result);
                 }
                /*else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                     var result = {
                         Output : {
                             finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                             ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                         }
                     }
                     $scope.case.toggleCaseLoader = false;
                     $uibModalInstance.close(result);
                 }*/

             }).error(function (result) {
                 $scope.case.toggleCaseLoader = false;
                 $uibModalInstance.dismiss('cancel');
                 caseErrorPopup($rootScope, result);
             });
         }


         $scope.case.cancel = function () {
             $uibModalInstance.dismiss('cancel');
         };

     }]);


