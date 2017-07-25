
CASE_CONSTANTS = {
    CASE_RESULTS: 'CaseResults',
    CASE_INFO: 'CaseInfo',
    CASE_LOCINFO: 'CaseLocInfo',
    CASE_NOTES: 'CaseNotes',
    CASE_TRANSACTION: 'CaseTransaction',
    CASE_INTAKECHANNEL: 'CaseIntakeChannel',
    CASE_INTAKEOWNERDEPT: 'CaseIntakeOwnerDepartment',
    CASE_CRMSYSTEM: 'CaseCRMSystem',
    CASE_CASETYPE: 'CaseType',
    CASE_STATUS: 'CaseStatus',
    CASE_SOURCESYSTEMS: 'CaseSourceSystems'
};

//var SIDE_PANEL_URL = BasePath + 'App/case/Views/CaseSidePanel.html';

HOME_REDIRECT_URL = BasePath + "case/search";
angular.module('case').controller("CaseMultiDetailsController", ['$scope', '$parse', '$http', '$localStorage', '$sessionStorage', '$stateParams', 'uiGridConstants', '$uibModal', '$stateParams', 'CaseMultiGridService',
    'CaseServices', 'CaseMultiDataService', 'CaseApiService', '$timeout', '$rootScope', 'CaseDataServices', 'CaseCRUDoperations', 'CaseCRUDapiService', '$q', 'mainService', '$window', '$rootScope',
function ($scope, $parse, $http, $localStorage, $sessionStorage, $stateParams, uiGridConstants, $uibModal, $stateParams, CaseMultiGridService, CaseServices,
    CaseMultiDataService, CaseApiService, $timeout, $rootScope, CaseDataServices, CaseCRUDoperations, CaseCRUDapiService, $q, mainService, $window, $rootScope) {

    var COMMON_URL = BasePath + 'App/Case/Views/multi/';

    var TEMPLATES = {
        CASE_INFO: 'CaseInfo.tpl.html',
        //CASE_DETAILS: 'CaseDetails.tpl.html',
        CASE_LOCINFO: 'CaseLocatorInfo.tpl.html',
        CASE_NOTES: 'CaseNotes.tpl.html',
        CASE_TRANSACTION: 'CaseTransactionDetails.tpl.html',
        CONST_MULTI_DETAILS_SIDE_PANEL: 'CaseMultiDetailsSidePanel.html'
    }

    CONSTANTS = {
        CASE_INFO: 'CaseInfo',
        //CASE_DETAILS: 'CaseDetails',
        CASE_LOCINFO: 'CaseLocInfo',
        CASE_NOTES: 'CaseNotes',
        CASE_TRANSACTION: 'CaseTransaction'
    };

    //used in templates
    $scope.CONSTANTS = {
        CASE_INFO: 'CaseInfo',
        //CASE_DETAILS: 'CaseDetails',
        CASE_LOCINFO: 'CaseLocInfo',
        CASE_NOTES: 'CaseNotes',
        CASE_TRANSACTION: 'CaseTransaction'
    };

    $scope.states = {
        toggleCRUDbuttons: function (rowEntity) {
            if (rowEntity.row_stat_cd == 'L') {
                return "";
            }
            else {
                return "App/Case/Views/gridDropDown.tpl.html";
            }
            // console.log(rowEntity);
        }
    }

    var initialize = function () {
        $scope.UserName = "Akshay";
        var url = COMMON_URL + TEMPLATES.CONST_MULTI_DETAILS_SIDE_PANEL;
        $scope.CaseMultiDetailsSidePanel = url;



        var params = $stateParams.case_key;
        //        $scope.param = params;

        if (params != null || params != "" || !angular.isUndefined(params)) {
            $scope.param = params;
            $scope.case_nm = $sessionStorage.case_nm;
            $scope.case_key = $sessionStorage.case_key;
            var HeaderCtrlValues = {
                DisplayStatus: "block",
                CaseKey: $scope.case_key,
                CaseName: $scope.case_nm
            }

            CaseDataServices.setHeaderCtrlDisplayStatus(HeaderCtrlValues);
            //getting value in constituent helper
            //$scope.constituent_type = $localStorage.type;
            //$rootScope.constituent_type = $scope.constituent_type;
        } else {
            $scope.param = null;
        }

        //$scope.getCaseInfo(true);
        //$scope.getCaseDetails(true);
        //$scope.getCaseLocInfo(true);
        //$scope.getCaseNotesInfo(true);
        //$scope.getCaseTransDetailsInfo(true);

        /*************************************CASE INFO SETTINGS***********************************/

        //$scope.testfunc();


        //toggle grid results
        CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_INFO);
        /*****set the grid options*****/
        var options = CaseMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CASE_INFO);
       // console.log("Get Case Grid OPtions");
       // console.log(options);
        $scope.caseInfoGridOptions = options;
        //$scope.$watch(function () {
        //    $scope.CaseDetails = CaseDataServices.getCaseDetails();
        //})
        $scope.caseInfoGridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }
        $scope.caseInfo = true;
        gettingCaseInfo(false);

        /*************************************CASE DETAILS SETTINGS***********************************/
        /* $scope.constAddress = false;
         //toggle grid results
         CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_DETAILS);
         
         var options = CaseMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CASE_DETAILS);
         $scope.caseDetailsGridOptions = options;
 
         $scope.caseDetailsGridOptions.onRegisterApi = function (grid) {
             $scope.caseDetailsGridApi = grid;
         }
 
         /*
         /*************************************CASE NOTES SETTINGS***********************************/
        $scope.caseNotes = false;
        //toggle grid results
        CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_NOTES);
        /*****set the grid options*****/
        var options = CaseMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CASE_NOTES);
        $scope.caseNotesGridOptions = options;
        $scope.caseNotesGridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }


        /*************************************CASE LOCATOR SETTINGS***********************************/
        $scope.caseLocInfo = false;
        //toggle grid results
        CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_LOCINFO);
        /*****set the grid options*****/
        var options = CaseMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CASE_LOCINFO);
        $scope.caseLocInfoGridOptions = options;
        $scope.caseLocInfoGridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

        /*************************************CASE TRANSACTION DETAILS SETTINGS***********************************/
        $scope.caseTransDetails = false;
        //toggle grid results
        CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_TRANSACTION);
        /*****set the grid options*****/
        var options = CaseMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CASE_TRANSACTION);
        $scope.caseTransDetailsGridOptions = options;
        $scope.caseTransDetailsGridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }


        $rootScope.toggleFiltering = function (type) {

            switch (type) {
                case CASE_CONSTANTS.CASE_LOCINFO: $scope.caseLocInfoGridOptions.enableFiltering = !$scope.caseLocInfoGridOptions.enableFiltering;
                    break;
                case CASE_CONSTANTS.CASE_INFO: $scope.caseInfoGridOptions.enableFiltering = !$scope.caseInfoGridOptions.enableFiltering;
                    break;
                case CASE_CONSTANTS.CASE_NOTES: $scope.caseNotesGridOptions.enableFiltering = !$scope.caseNotesGridOptions.enableFiltering;
                    break;
                case CASE_CONSTANTS.CASE_TRANSACTION: $scope.caseTransDetailsGridOptions.enableFiltering = !$scope.caseTransDetailsGridOptions.enableFiltering;
                    break;
            }


            $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }

        /* added by srini for hiding add buttons in both loc and notes section*/
        $scope.showAddButton = true;
        if (CaseMultiGridService.getTabDenyPermission()) {
            $scope.showAddButton = false;
        }
    };


    initialize();
    //$scope.testfunc = function () {
    //    alert("Coming inside");
    //}


    // checkbox for Case Info
    $scope.getCaseInfo = function (CaseInfo) {
        gettingCaseInfo(CaseInfo);
    }


    function gettingCaseInfo(CaseInfo) {
        var url = COMMON_URL + TEMPLATES.CASE_INFO;
        $scope.CaseInfoTemplate = url;
        if (!CaseInfo) {
            $scope.toggleCaseInfoLoader = { "display": "block" };

           // console.log("caseInfoGridOptions");
           // console.log($scope.caseInfoGridOptions);

            var finalResults = getCaseInfoOption($scope, CaseServices, $scope.param, uiGridConstants, CaseMultiDataService,
                CaseMultiGridService, CONSTANTS.CASE_INFO, $scope.caseInfoGridOptions, CaseDataServices, $rootScope);
           // console.log("Final results object");
           // console.log(finalResults);
            CaseDataServices.setCaseInfoGrid(finalResults.gridOp);

            $scope.caseInfoGridOptions = finalResults.gridOp;
            $scope.currentCaseInfoPage = 1;

            var listener = $scope.$watch(function () {

               // console.log("Conlumn definition");
                return $scope.caseInfoGridOptions.data.length;

            }, function (newLength, oldLength) {
                if (newLength > 0) {
                   // console.log($scope.caseInfoGridOptions.data.length);
                    $scope.totalCaseInfoItems = $scope.caseInfoGridOptions.data.length;
                    //unbind watch after use
                    listener();
                }
            });
        } else {
            CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_INFO);
        }
    }

    // checkbox for Case Details
    /*  $scope.getCaseDetails = function (CaseDetails) {
          var url = COMMON_URL + TEMPLATES.CASE_DETAILS;
          $scope.CaseDetailsTemplate = url;
          if (!CaseDetails) {
              $scope.toggleCaseDetailsLoader = { "display": "block" };
              var finalResults = callCaseSetup($scope, CaseApiService, $scope.param, uiGridConstants, CaseMultiDataService,
                  CaseMultiGridService, CONSTANTS.CASE_DETAILS, $scope.CaseDetailsGridOptions);
              console.log(finalResults.gridOp);
              CaseDataServices.setCaseDetailsGrid(finalResults.gridOp);
              $scope.CaseDetailsGridOptions = finalResults.gridOp;
              $scope.currentCaseDetailsPage = 1;
  
              var listener = $scope.$watch(function () {
                  return $scope.CaseDetailsGridOptions.data.length;
              }, function (newLength, oldLength) {
                  if (newLength > 0) {
                      $scope.totalCaseDetailsItems = $scope.CaseDetailsGridOptions.data.length;
                      //unbind watch after use
                      listener();
                  }
              });
          } else {
              CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_DETAILS);
          }
      }
      */

    // checkbox for Case Locator Info
    $scope.getCaseLocInfo = function (CaseLocInfo) {
        var url = COMMON_URL + TEMPLATES.CASE_LOCINFO;
        $scope.CaseLocInfoTemplate = url;
        if (!CaseLocInfo) {
            $scope.toggleCaseLocInfoLoader = { "display": "block" };
            var finalResults = callCaseSetup($scope, CaseApiService, $scope.param, uiGridConstants, CaseMultiDataService,
                CaseMultiGridService, CONSTANTS.CASE_LOCINFO, $scope.caseLocInfoGridOptions, $rootScope);
           // console.log(finalResults.gridOp);
            CaseDataServices.setCaseLocInfoGrid(finalResults.gridOp);
            $scope.caseLocInfoGridOptions = finalResults.gridOp;
            $scope.currentCaseLocInfoPage = 1;

            var listener = $scope.$watch(function () {
                return $scope.caseLocInfoGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalcaseLocInfoItems = $scope.caseLocInfoGridOptions.data.length;
                    //unbind watch after use
                    listener();
                }
            });
        } else {
            CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_LOCINFO);
        }
    }

    // checkbox for Case Notes
    $scope.getCaseNotesInfo = function (CaseNotes) {
        var url = COMMON_URL + TEMPLATES.CASE_NOTES;
        $scope.CaseNotesTemplate = url;
        if (!CaseNotes) {
            $scope.toggleCaseNotesLoader = { "display": "block" };
            var finalResults = callCaseSetup($scope, CaseApiService, $scope.param, uiGridConstants, CaseMultiDataService,
                CaseMultiGridService, CONSTANTS.CASE_NOTES, $scope.caseNotesGridOptions, $rootScope);
           // console.log(finalResults.gridOp);
            CaseDataServices.setCaseNotesGrid(finalResults.gridOp);
            $scope.caseNotesGridOptions = finalResults.gridOp;
            $scope.currentCaseNotesPage = 1;

            var listener = $scope.$watch(function () {
                return $scope.caseNotesGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalcaseNotesItems = $scope.caseNotesGridOptions.data.length;
                    //unbind watch after use
                    listener();
                }
            });
        } else {
            CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_NOTES);
        }
    }

    // checkbox for Case Transaction
    $scope.getCaseTransDetailsInfo = function (CaseTransDetails) {
        var url = COMMON_URL + TEMPLATES.CASE_TRANSACTION;
        $scope.CaseTransDetailsTemplate = url;
        if (!CaseTransDetails) {
            $scope.toggleCaseTransDetailsLoader = { "display": "block" };
            var finalResults = callCaseSetup($scope, CaseApiService, $scope.param, uiGridConstants, CaseMultiDataService,
                CaseMultiGridService, CONSTANTS.CASE_TRANSACTION, $scope.caseTransDetailsGridOptions, $rootScope);
           // console.log(finalResults.gridOp);
            CaseDataServices.setCaseTransDetailsGrid(finalResults.gridOp);
            $scope.caseTransDetailsGridOptions = finalResults.gridOp;
            $scope.currentCaseDetailsPage = 1;

            var listener = $scope.$watch(function () {
                return $scope.caseTransDetailsGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                   // console.log("Total items transaction");
                  //  console.log($scope.caseTransDetailsGridOptions.data.length);
                    $scope.totalcaseTransDetailsItems = $scope.caseTransDetailsGridOptions.data.length;
                    //unbind watch after use
                    listener();
                }
            });
        } else {
            CaseMultiGridService.getToggleDetails($scope, false, CONSTANTS.CASE_TRANSACTION);
        }
        $scope.currentcasetransdetailsPage = 1;
    }

    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.openNotes = function (row) {
        $("#CaseNotesModalDialog").modal('show');
        $scope.CaseNotes = row.entity.case_notes;
    }


    $scope.commonAddGridRow = function (type) {
       // console.log(CASE_CONSTANTS.CASE_LOCINFO);
        switch (type) {
            case CASE_CONSTANTS.CASE_INFO: {
                commonAddGridRow($scope, null, CaseCRUDoperations, $uibModal, $stateParams, type, CASE_CONSTANTS.CASE_INFO, $scope.caseInfoGridOptions);
                break;
            }
            case CASE_CONSTANTS.CASE_LOCINFO: {
                commonAddGridRow($scope, null, CaseCRUDoperations, $uibModal, $stateParams, type, CASE_CONSTANTS.CASE_LOCINFO, $scope.caseLocInfoGridOptions);

                break;
            }
            case CASE_CONSTANTS.CASE_NOTES: {
                commonAddGridRow($scope, null, CaseCRUDoperations, $uibModal, $stateParams, type, CASE_CONSTANTS.CASE_NOTES, $scope.caseNotesGridOptions);

                break;
            }
        }
    }

    var pageName = "SearchDetails";
    $scope.commonDeleteGridRow = function (row, grid) {
        var className = grid.element[0].className.match(/([^\s]+)/)[0];
        switch (className) {
            case CASE_CONSTANTS.CASE_INFO: {
                commonDeleteGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.caseInfoGridOptions, pageName, CASE_CONSTANTS.CASE_INFO);

                break;
            }
            case CASE_CONSTANTS.CASE_LOCINFO: {
                commonDeleteGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.caseLocInfoGridOptions, pageName, CASE_CONSTANTS.CASE_LOCINFO);

                break;
            }
            case CASE_CONSTANTS.CASE_NOTES: {
                commonDeleteGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.caseNotesGridOptions, pageName, CASE_CONSTANTS.CASE_NOTES);

                break;
            }
        }

    }


    $scope.commonEditGridRow = function (row, grid) {
        var className = grid.element[0].className.match(/([^\s]+)/)[0];
       // console.log("row.entity");
       // console.log(row);
        switch (className) {
            case CASE_CONSTANTS.CASE_INFO: {
                commonEditGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.caseInfoGridOptions, pageName, CASE_CONSTANTS.CASE_INFO);
                break;
            }
            case CASE_CONSTANTS.CASE_LOCINFO: {
                commonEditGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.caseLocInfoGridOptions, pageName, CASE_CONSTANTS.CASE_LOCINFO);

                break;
            }
            case CASE_CONSTANTS.CASE_NOTES: {
                commonEditGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.caseNotesGridOptions, pageName, CASE_CONSTANTS.CASE_NOTES);

                break;
            }
        }
    }

    $scope.commonResearchGridRow = function (row, grid) {
        commonResearchGridRow($scope, mainService, row, grid, CaseApiService, $uibModal, $stateParams, $scope.searchResultsGridOptions, CASE_CONSTANTS.CASE_RESULTS, $window);
    }


}]);

function commonAddGridRow($scope, row, CaseCRUDoperations, $uibModal, $stateParams, type, gridOption, pageName) {
   // console.log("Inside commonAddGridRow");
    //console.log($stateParams);
    CaseCRUDoperations.getAddModal($scope, $uibModal, $stateParams, null, type, gridOption, pageName);
}

function commonEditGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, gridOption, pageName, constant) {
    CaseCRUDoperations.getEditModal($scope, $uibModal, $stateParams, gridOption, row, pageName, constant);
}

function commonResearchGridRow($scope, mainService, row, grid, CaseApiService, $uibModal, $stateParams, gridOption, constant, $window, $rootScope) {
    //CaseCRUDoperations.getEditModal($scope, $uibModal, $stateParams, gridOption, row, constant);
    if (row.typ_key_dsc.indexOf('Merge Conflicts') > -1)
        getLocInfoRows($scope, mainService, row, CaseApiService, $window, true, $rootScope); // Merge Conflict Flag);
    else
        getLocInfoRows($scope, mainService, row, CaseApiService, $window, false, $rootScope);

}

function commonDeleteGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, gridOption, pageName, constant) {
    CaseCRUDoperations.getDeleteModal($scope, $uibModal, $stateParams, gridOption, row, pageName, constant);
}


function getMergeConflictedMasters(scope, row, CaseApiService, $window) {

}

function getLocInfoRows(scope, mainService, row, CaseApiService, $window, MergeConflictFlag, $rootScope) {
    var param = row.case_key;
    var locInfoResults;
    var NAVIGATE_URL = BasePath + "constituent/Search/";
    CaseApiService.getApiDetails(param, CASE_CONSTANTS.CASE_LOCINFO).success(function (result) {
        locInfoResults = result;
        //$rootScope.LocInfoResearchResults = locInfoResults;
        mainService.setLocInfoResearchData(result, param, row, MergeConflictFlag);
        $window.location.href = NAVIGATE_URL;
    }).error(function (result) {
        myApp.hidePleaseWait();
        //$scope.pleaseWait = { "display": "none" };
        if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }

    });

}
function getCaseInfoOption($scope, CaseServices, searchPostObj, uiGridConstants, CaseMultiDataService,
    CaseMultiGridService, constant, gridOp, CaseDataServices, $rootScope) {

    var _caseDetailsData = CaseMultiDataService.getData(constant);
        var postParams = {
        "CaseInputSearchModel": [{
            "CaseId": searchPostObj
        }]
    }

    if (_caseDetailsData.length <= 0) {
        CaseServices.getCaseAdvSearchResults(postParams).success(function (result) {
            //set the value in cache
           // console.log("Result from case info details");
            CaseMultiDataService.setData(result, constant);

            //call the grid layout and set the data
            var caseDetails;
            if (result.length > 0) {
                caseDetails = result[0]["case_desc"];
            }
            else {
                caseDetails = "<empty>";
            }
            CaseDataServices.setCaseDetails(caseDetails);
            $scope.CaseDetails = CaseDataServices.getCaseDetails();
            gridOp = CaseMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, result, constant);
            //show the grid
            CaseMultiGridService.getToggleDetails($scope, true, constant);
        }).error(function (result) {
            myApp.hidePleaseWait();
            //$scope.pleaseWait = { "display": "none" };
            if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }

        });
    }
    else {
        //set the cache data  to grid
        gridOp = CaseMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, _caseDetailsData, constant);
        //show the grid
        CaseMultiGridService.getToggleDetails($scope, true, constant);

        //  $scope.totalConstNameItems = _constDetailsData.length;
    }

    var finalResults = {
        itemCount: CaseMultiDataService.getData(constant).length,
        gridOp: gridOp
    }
    //  console.log(finalResults);
    return finalResults;
}

function callCaseSetup($scope, CaseApiService, searchPostObj, uiGridConstants, CaseMultiDataService,
    CaseMultiGridService, constant, gridOp, $rootScope) {

    var _caseDetailsData = CaseMultiDataService.getData(constant);
    //$scope.$watch(function () {
    //    _caseDetailsData = CaseMultiDataService.getData(constant);
    //});

    if (_caseDetailsData.length <= 0) {
        CaseApiService.getApiDetails(searchPostObj, constant).success(function (result) {
            //set the value in cache
           // console.log("Result from case trans details");
           // console.log(result);
            CaseMultiDataService.setData(result, constant);
            //call the grid layout and set the data
            gridOp = CaseMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, result, constant);
            //show the grid
            CaseMultiGridService.getToggleDetails($scope, true, constant);

        }).error(function (result) {
            myApp.hidePleaseWait();
            //$scope.pleaseWait = { "display": "none" };
            if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }

        });
    }
    else {
        //set the cache data  to grid
        gridOp = CaseMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, _caseDetailsData, constant);
        //show the grid
        CaseMultiGridService.getToggleDetails($scope, true, constant);

        //  $scope.totalConstNameItems = _constDetailsData.length;
    }

    var finalResults = {
        itemCount: CaseMultiDataService.getData(constant).length,
        gridOp: gridOp
    }
    //  console.log(finalResults);
    return finalResults;
}

var myApp;
myApp = myApp || (function () {
    //var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();