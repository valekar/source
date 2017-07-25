
angular.module('case').controller('CaseSearchController', ['$scope', '$location', '$log', 'CaseServices', 'CaseMultiGridService',
    '$window', 'CaseDataServices', '$localStorage', '$sessionStorage', 'CaseClearDataService', 'mainService','$state','$rootScope', 'caseDropDownService',
function ($scope, $location, $log, CaseServices, CaseMultiGridService, $window, CaseDataServices, $localStorage, $sessionStorage, CaseClearDataService, mainService, $state, $rootScope, caseDropDownService) {
    // console.log("hittin");
    //var root = $scipe.BaseURL;
    //alert($scipe.BaseURL);


    var NAVIGATE_URL = "/case/search/results";
    var username = "";

    var SearchPanelsCount = 5; // Define the maximum number of search panels
    var defaultPanelsCount = 1; // Set the number of search panels to be opened by default
    
    var appendString = 'ID'; // Specify the string that needs to be appended to distinguish between each elements

    
    var initialize = function () {

        if (mainService.getLastCaseSearchResult())
            $scope.lastCaseSearchResultPresent = true;
        else
            $scope.lastCaseSearchResultPresent = false;

        var SearchParams = CaseDataServices.getSavedSearchParams(); //This variable holds the search params so that they can be bound when the users returns back to this page via breadcrumbs
        $scope.CaseSearchFormElements = [];
        $scope.SearchRows = defaultPanelsCount;
        for (i = 1; i <= SearchPanelsCount; i++) {
            $scope.CaseSearchFormElements.push(appendString + i);
        }

        //var HeaderCtrlValues = {
        //    DisplayStatus: "none",
        //    CaseKey: null,
        //    CaseName: null
        //}

        //CaseDataServices.setHeaderCtrlDisplayStatus(HeaderCtrlValues);

        //If you are adding an element to the HTML, then add the model names here as well
        $scope.CaseParams = {
            SearchPanel: [],
            PanelSeparator: [],
            showCloseButton: [],
            CaseNumber: [],
            CaseName: [],
            Case_CRMSystem: [],
            CRMSystems: [],
            case_CRMSysId: [],
            case_Type: [],
            CaseTypes: [],
            case_constName: [],
            case_userName: [],
            CaseDateFrom: [],
            caseDateFromPopup: [],
            caseDateToPopup: [],
            CaseDateTo: [],
            caseStatus: [],
            SearchMeChkbx: [],
            caseUsernameState: []
        };


        

        caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_CRMSYSTEM).success(function (result) {
            $scope.CaseParams.CRMSystems = result;
           // console.log("CRM Systems");
          //  console.log(angular.toJson(result));
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });


        caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_CASETYPE).success(function (result) {
            $scope.CaseParams.CaseTypes = result;
            CaseDataServices.setCaseTypeDropDowns(result);
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        angular.forEach($scope.CaseParams, function (paramskey, paramsvalue) {

            angular.forEach($scope.CaseSearchFormElements, function (key) {
                $scope.CaseParams[paramsvalue].push(key);
            });
        });

        for (i = 1; i <= $scope.SearchRows; i++) {
            
            $scope.CaseParams.SearchPanel[appendString + i] = true;
            $scope.CaseParams.PanelSeparator[appendString + i] = true;
            $scope.CaseParams.showCloseButton[appendString + i] = true;
            if (i == 1) {
                $scope.CaseParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
            }
        }

        mainService.getUsername().then(function (result) {
            username = result.data;
        });


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

        $scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();


        $scope.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        angular.forEach($scope.CaseParams.CaseDateFrom, function (key, value) {
            $scope.CaseParams.CaseDateFrom[key] = null;
            $scope.CaseParams.caseDateFromPopup[key] = {
                opened: false
            };
        });

        angular.forEach($scope.CaseParams.CaseDateTo, function (key, value) {
            $scope.CaseParams.CaseDateTo[key] = null;
            $scope.CaseParams.caseDateToPopup[key] = {
                opened: false
            };
        });


        var CaseRecentSearches = CaseServices.getCaseRecentSearches().success(function (result) {
            $scope.CaseRecentSearches = result;
            

        }).error(function (result) {
            myApp.hidePleaseWait();
            if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }

        });


        $scope.CaseParams.openCaseDateFrom = function (counter) {
            $scope.CaseParams.caseDateFromPopup[counter].opened = true;
        };

        $scope.CaseParams.openCaseDateTo = function (counter) {
            $scope.CaseParams.caseDateToPopup[counter].opened = true;
        };

        var postParams = {};
        //this is clearing localstorage contents(masterid/name)
        delete $sessionStorage.case_key;
        delete $sessionStorage.case_nm;
        $scope.pleaseWait = { "display": "none" };

        //Set the previously searched search params if navigating from breadcrumbs
        if (SearchParams) {
            var rootModel = SearchParams["CaseInputSearchModel"];
            if (rootModel.length > 1) {
                $scope.SearchRows = rootModel.length;
            }
            else {
                $scope.SearchRows = defaultPanelsCount;
            }
            for (i = 1; i <= rootModel.length; i++) {
                $scope.CaseParams.SearchPanel[appendString + i] = true;
                $scope.CaseParams.PanelSeparator[appendString + i] = true;
                $scope.CaseParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.CaseParams.showCloseButton[appendString + i] = false;
                }
                $scope.CaseParams.CaseNumber[appendString + i] = rootModel[i-1]["CaseId"];
                $scope.CaseParams.CaseName[appendString + i] = rootModel[i - 1]["CaseName"];
                $scope.CaseParams.Case_CRMSystem[appendString + i] = rootModel[i - 1]["ReferenceSource"];
                $scope.CaseParams.case_CRMSysId[appendString + i] = rootModel[i - 1]["ReferenceId"];
                $scope.CaseParams.case_Type[appendString + i] = rootModel[i - 1]["CaseType"];
                $scope.CaseParams.caseStatus[appendString + i] = rootModel[i - 1]["CaseStatus"];
                $scope.CaseParams.case_constName[appendString + i] = rootModel[i-1]["ConstituentName"];
                $scope.CaseParams.case_userName[appendString + i] = rootModel[i - 1]["UserName"];
                $scope.CaseParams.case_userName[appendString + i] = rootModel[i - 1]["UserId"];
                if (rootModel[i - 1]["ReportedDateFrom"])
                    $scope.CaseParams.CaseDateFrom[appendString + i] = new Date(rootModel[i - 1]["ReportedDateFrom"]);
                else
                    $scope.CaseParams.CaseDateFrom[appendString + i] = null;
                if (rootModel[i - 1]["ReportedDateTo"])
                    $scope.CaseParams.CaseDateTo[appendString + i] = new Date(rootModel[i - 1]["ReportedDateTo"]);
                else
                    $scope.CaseParams.CaseDateTo[appendString + i] = null;
            }
        }
    };
    initialize();


    $scope.SearchMeChkbxChange = function (SearchMeChkbxID) {
        if ($scope.CaseParams.SearchMeChkbx[SearchMeChkbxID]) {
            $scope.CaseParams.case_userName[SearchMeChkbxID] = username;
            $scope.CaseParams.caseUsernameState[SearchMeChkbxID] = true;
        }
        else {
            $scope.CaseParams.case_userName[SearchMeChkbxID] = "";
            $scope.CaseParams.caseUsernameState[SearchMeChkbxID] = false;
        }
    }

    //Get the recentmost search result data on clicking of the button from the mainService
    $scope.fetchLastCaseSearchResult = function () {

        if (mainService.getLastCaseSearchResult()) {
            //Get back the last search result           
            CaseDataServices.setSearchResutlsData(mainService.getLastCaseSearchResult());
            $state.go('case.search.results', {});
        }
        else {
            window.alert("Session Storage seems just to be cleared out! Please proceed with the regular search");
        }
    }

    //when clicked in search go to search results and do a post there
    $scope.caseSearch = function () {
        $scope.pleaseWait = { "display": "block" };
        $scope.CaseNoResults = "";
        var postParams = CaseDataServices.getSearchParams($scope, $scope.SearchRows, appendString);
       // console.log("postParams");
       //console.log(postParams);     

        angular.forEach(postParams.CaseInputSearchModel, function (item) {
            console.log(item);
            $scope.CaseId = item.CaseId;
            $scope.CaseName = item.CaseName;
            $scope.CaseType = item.CaseType;
            $scope.ConstituentName = item.ConstituentName;
            $scope.ConstituentName = item.UserName;            
            $scope.ReferenceId = item.ReferenceId;
            $scope.ReportedDateFrom = item.ReportedDateFrom;
            $scope.ReportedDateTo = item.ReportedDateTo;
            $scope.casestatus = item.CaseStatus;                    
        })

        if (!!$scope.CaseId || !!$scope.CaseName || !!$scope.CaseType || !!$scope.ConstituentName || !!$scope.UserName || !!$scope.ReferenceId || $scope.ReportedDateFrom != "" || $scope.ReportedDateTo != "" || !!$scope.casestatus)
        {
          callCaseService($scope, CaseServices, CaseDataServices, postParams, CaseClearDataService, mainService, $location, NAVIGATE_URL, $rootScope);
            
        } else {
            $scope.pleaseWait = { "display": "none" };
            $scope.CaseNoResults = "Please provide at least one input criteria before search";
        }
    }


    $scope.CaseRecentSearch = function (qry) {
        $scope.pleaseWait = { "display": "block" };
        $('#recentCaseSearchesModal').modal('hide');
        var listPostParam = [];
        listPostParam.push(qry);
        postParams = listPostParam;
        //postParams = constituentDataServices.getSearchParams($scope);
        callCaseService($scope, CaseServices, CaseDataServices, qry, CaseClearDataService, mainService, $location, NAVIGATE_URL, $rootScope);
    }

    $scope.caseClear = function () {
        CaseServices.clearSearchParams($scope, SearchPanelsCount, defaultPanelsCount, appendString);
    }

    $scope.AddNewSearchRow = function (RowCount) {
        if (RowCount < SearchPanelsCount) {
            RowCount++;
            $scope.SearchRows = RowCount; // Change the default search row count to the number of counts open 

            for (i = 1; i <= RowCount; i++) {
                $scope.CaseParams.SearchPanel[appendString + i] = true;
                $scope.CaseParams.PanelSeparator[appendString + i] = true;
                $scope.CaseParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.CaseParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }
        }
        else
        {
            alert("A maximum of only 5 search rows can be added.");
        }
    }

    $scope.RemoveRow = function (panelID) {
        $scope.CaseParams.SearchPanel[panelID] = false;
        $scope.CaseParams.PanelSeparator[panelID] = false;
        $scope.CaseParams.showCloseButton[panelID] = false;
        if($scope.SearchRows != 1)
            $scope.SearchRows--
    }


    /** added by srini for tab security**/
    $scope.tabSecuriy = true;
    if (CaseMultiGridService.getTabDenyPermission()) {
        $scope.tabSecurity = false;
    }

}]);
angular.module('case').filter('placeholderfunc', ['CaseDataServices', function (CaseDataServices) {
    return function (input) {
        var CaseResultsBindingData = "";
        var CaseTypesDropDown;
        //caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_CASETYPE).success(function (result) {
        //    CaseTypesDropDown = result;
        //}).error(function (result) {
        //    //alert("Unable to retrieve dropdown values");
        //});
        CaseTypesDropDown = CaseDataServices.getCaseTypeDropDowns();
        angular.forEach(input.CaseInputSearchModel, function(value, key){
            if (value.CaseId != null)
                CaseResultsBindingData += "Case ID: " + value.CaseId + ", ";
            if (value.CaseName != null)
                CaseResultsBindingData += "Case Name: " + value.CaseName + ", ";
            if (value.ReferenceSource != null)
                CaseResultsBindingData += "Reference Source: " + value.ReferenceSource + ", ";
            if (value.ReferenceId != null)
                CaseResultsBindingData += "Reference Id: " + value.ReferenceId + ", ";
            if (value.CaseType != null) {
               // console.log("Case Types");
                //console.log(CaseTypesDropDown);
                if (CaseTypesDropDown.length > 0) {
                    for (i = 0; i < CaseTypesDropDown.length; i++) {
                        if (value.CaseType == CaseTypesDropDown[i]["id"])
                            CaseResultsBindingData += "Case Type: " + CaseTypesDropDown[i]["value"] + ", ";
                    }
                }
                //CaseResultsBindingData += "Case Type: " + value.CaseType + ", ";
            }
             
            if (value.CaseStatus != null)
                CaseResultsBindingData += "Case Status: " + value.CaseStatus + ", "
            if (value.ConstituentName != null)
                CaseResultsBindingData += "Constituent Name: " + value.ConstituentName + ", ";
            if (value.UserName != null)
                CaseResultsBindingData += "User Name: " + value.UserName + ", ";
            CaseResultsBindingData += ";";
        });
        //console.log(CaseTypesDropDown);

        if (CaseResultsBindingData != "")
            return CaseResultsBindingData.slice(0, -3);
        else
            return CaseResultsBindingData;
    }
}]);
function callCaseService($scope, CaseServices, CaseDataServices, postParams, CaseClearDataService, mainService, $location, NAVIGATE_URL, $rootScope) {
    $scope.pleaseWait = { "display": "block" };
   // console.log("Into the callcaseService method");
    CaseDataServices.setSearchParams(postParams);
    CaseServices.getCaseAdvSearchResults(postParams).success(function (result) {
        //clear all multi tab data , need to do for normal tab too
        //CaseClearDataService.clearMultiData(); // TEMPORARILY COMMENTED !!
       // console.log("retreived adv search results");
        CaseDataServices.setSearchResutlsData(result);
        var _result = CaseDataServices.getSearchResultsData();

        //Setting the last search result's data in the mainService

        mainService.setLastCaseSearchResult(_result);

        if (_result.length > 0) {
            //if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
            //    $scope.pleaseWait = { "display": "none" };
            //    angular.element("#accessDeniedModal").modal();
            //}
            //else {
            $scope.pleaseWait = { "display": "none" };
            $location.url(NAVIGATE_URL);
            //}
        }
        else {
            //alert("no data");
            $scope.ConstNoResults = "No Results found!";
            $scope.pleaseWait = { "display": "none" };
        }
    }).error(function (result) {
        myApp.hidePleaseWait();
        $scope.pleaseWait = { "display": "none" };
        if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
            var output = {
                finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
            }
            myApp.hidePleaseWaitmessagePopup
            $scope.pleaseWait = { "display": "none" };
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



