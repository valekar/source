angular.module('locator').controller("locatorEmailController", ['$scope', '$location', '$log', 'LocatorServices', 
    '$window', 'LocatorEmailDataServices', '$localStorage', '$sessionStorage', 'LocatorEmailClearDataServices', 'mainService', '$state', '$rootScope', 'LocatorEmailDropDownService',
function ($scope, $location, $log, LocatorServices, $window, LocatorEmailDataServices, $localStorage, $sessionStorage, LocatorEmailClearDataServices, mainService, $state, $rootScope, LocatorEmailDropDownService) {
   
    var NAVIGATE_URL = "/locator/email/results";
    var username = "";
    var SearchPanelsCount = 5; // Define the maximum number of search panels
    var defaultPanelsCount = 1; // Set the number of search panels to be opened by default

    var appendString = 'ID'; // Specify the string that needs to be appended to distinguish between each elements


    var initialize = function () {

        if (mainService.getLastLocatorEmailSearchResult())
            $scope.lastLocatorEmailSearchResultPresent = true;
        else
            $scope.lastLocatorEmailSearchResultPresent = false;

        var SearchParams = LocatorEmailDataServices.getSavedSearchParams(); //This variable holds the search params so that they can be bound when the users returns back to this page via breadcrumbs
        $scope.LocatorEmailSearchFormElements = [];
        $scope.SearchRows = defaultPanelsCount;
        for (i = 1; i <= SearchPanelsCount; i++) {
            $scope.LocatorEmailSearchFormElements.push(appendString + i);
        }

       

        //If you are adding an element to the HTML, then add the model names here as well
        $scope.LocatorParams = {
            SearchPanel: [],
            PanelSeparator: [],
            showCloseButton: [],
            locator_addr_key: [],
            cnst_email_addr: [],
            int_assessmnt_cd: [],
            ext_hygiene_result: [],
            code_category: [],         
            exactmatchChkbx: [],
           
        };

        //Dropdown list
        LocatorEmailDropDownService.getDropDown(LOCATOR_CONSTANTS.LOCATOR_FINALASSESMENT_CODE).success(function (result) {
          
            $scope.LocatorParams.Finalassesmentcode = result;
            LocatorEmailDataServices.setFinalAssesmentCodeDropDowns(result);
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        angular.forEach($scope.LocatorParams, function (paramskey, paramsvalue) {

            angular.forEach($scope.LocatorEmailSearchFormElements, function (key) {
                $scope.LocatorParams[paramsvalue].push(key);
            });
        });

        for (i = 1; i <= $scope.SearchRows; i++) {

            $scope.LocatorParams.SearchPanel[appendString + i] = true;
            $scope.LocatorParams.PanelSeparator[appendString + i] = true;
            $scope.LocatorParams.showCloseButton[appendString + i] = true;
            if (i == 1) {
                $scope.LocatorParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
            }
        }

        mainService.getUsername().then(function (result) {
            username = result.data;
        });

        var LocatorEmailRecentSearches = LocatorServices.getLocatorEmailRecentSearches().success(function (result) {
            $scope.LocatorEmailRecentSearches = result;
           

        }).error(function (result) {
            myApp.hidePleaseWait();
            if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }

        });


        

        var postParams = {};
        //this is clearing localstorage contents(masterid/name)
        delete $sessionStorage.locator_addr_key;
        delete $sessionStorage.cnst_email_addr;       
        $scope.pleaseWait = { "display": "none" };

        //Set the previously searched search params if navigating from breadcrumbs
        if (SearchParams) {
            var rootModel = SearchParams["LocatorEmailInputSearchModel"];
            if (rootModel.length > 1) {
                $scope.SearchRows = rootModel.length;
            }
            else {
                $scope.SearchRows = defaultPanelsCount;
            }
            for (i = 1; i <= rootModel.length; i++) {
                $scope.LocatorParams.SearchPanel[appendString + i] = true;
                $scope.LocatorParams.PanelSeparator[appendString + i] = true;
                $scope.LocatorParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.LocatorParams.showCloseButton[appendString + i] = false;
                }
                $scope.LocatorParams.locator_addr_key[appendString + i] = rootModel[i - 1]["locator_addr_key"];
                $scope.LocatorParams.cnst_email_addr[appendString + i] = rootModel[i - 1]["cnst_email_addr"];
                $scope.LocatorParams.int_assessmnt_cd[appendString + i] = rootModel[i - 1]["int_assessmnt_cd"];
                $scope.LocatorParams.ext_hygiene_result[appendString + i] = rootModel[i - 1]["ext_hygiene_result"];
                $scope.LocatorParams.code_category[appendString + i] = rootModel[i - 1]["code_category"];
                    
            }
        }
    };
    initialize();

    
    //Get the recentmost search result data on clicking of the button from the mainService
    $scope.fetchLastLocatorEmailSearchResult = function () {

        if (mainService.getLastLocatorEmailSearchResult()) {
            //Get back the last search result           
            LocatorEmailDataServices.setSearchResutlsData(mainService.getLastLocatorEmailSearchResult());
            $state.go('locator.email.results', {});
        }
        else {
            window.alert("Session Storage seems just to be cleared out! Please proceed with the regular search");
        }
    }

    //when clicked in search go to search results and do a post there
    $scope.searchLocatorEmail = function () {
       
        $scope.pleaseWait = { "display": "block" };       
        var postParams = LocatorEmailDataServices.getSearchParams($scope, $scope.SearchRows, appendString);
        console.log(postParams);
        angular.forEach(postParams.LocatorEmailInputSearchModel, function (item) {
            $scope.EmailKey = item.LocEmailKey;
            $scope.Email = item.LocEmailId;
            $scope.FinalAssCode = item.ExtAssessCode;
            
        })

        if (!!$scope.EmailKey || !!$scope.Email || !!$scope.FinalAssCode) {
            callLocatorEmailService($scope, LocatorServices, LocatorEmailDataServices, postParams, LocatorEmailClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope);

        } else {
            $scope.pleaseWait = { "display": "none" };
            $scope.LocatorNoResults = "Please provide at least one input criteria before search";
        }


            }


    $scope.LocatorEmailRecentSearch = function (qry) {
        $scope.pleaseWait = { "display": "block" };
        $('#recentLocatorEmailSearchesModal').modal('hide');
        var listPostParam = [];
        listPostParam.push(qry);
        postParams = listPostParam;
        //postParams = constituentDataServices.getSearchParams($scope);
        callLocatorEmailService($scope, LocatorServices, LocatorEmailDataServices, qry, LocatorEmailClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope);
    }

    $scope.locatorEmailClear = function () {
        LocatorServices.clearSearchParams($scope, SearchPanelsCount, defaultPanelsCount, appendString);
    }

    $scope.AddNewSearchRow = function (RowCount) {
        if (RowCount < SearchPanelsCount) {
            RowCount++;
            $scope.SearchRows = RowCount; // Change the default search row count to the number of counts open 

            for (i = 1; i <= RowCount; i++) {
                $scope.LocatorParams.SearchPanel[appendString + i] = true;
                $scope.LocatorParams.PanelSeparator[appendString + i] = true;
                $scope.LocatorParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.LocatorParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }
        }
        else {
            alert("A maximum of only 5 search rows can be added.");
        }
    }

    $scope.RemoveRow = function (panelID) {
        $scope.LocatorParams.SearchPanel[panelID] = false;
        $scope.LocatorParams.PanelSeparator[panelID] = false;
        $scope.LocatorParams.showCloseButton[panelID] = false;
        if ($scope.SearchRows != 1)
            $scope.SearchRows--
    }


    /** added by srini for tab security**/
    $scope.tabSecuriy = true;
    //if (LocatorEmailMultiGridService.getTabDenyPermission()) {
    //    $scope.tabSecurity = false;
    //}

}]);

angular.module('locator').filter('placeholderfuncEmail', ['LocatorEmailDataServices', function (LocatorEmailDataServices) {
    return function (input) {
        
        var LocatorEmailResultsBindingData = "";
        var FianlAssesmentCtryDropDown;       
        FianlAssesmentCtryDropDown = LocatorEmailDataServices.getFinalAssesmentCodeDropDowns();
       
        angular.forEach(input.LocatorEmailInputSearchModel, function (value, key) {
            if (value.LocEmailKey != null)
                LocatorEmailResultsBindingData += "Emial KEY: " + value.LocEmailKey + ", ";
            if (value.LocEmailId != null)
                LocatorEmailResultsBindingData += "Email Address: " + value.LocEmailId + ", ";
            if (value.ExtAssessCode != null)
                LocatorEmailResultsBindingData += "Final Assessment Code: " + value.ExtAssessCode + ", ";
            //if (value.ext_hygiene_result != null)
            //    LocatorEmailResultsBindingData += "ext_hygiene_result: " + value.ext_hygiene_result + ", ";
            //if (value.code_category != null)
            //    LocatorEmailResultsBindingData += "Code Category: " + value.code_category + ", ";
            
                LocatorEmailResultsBindingData += ";";
        });
       
        
        if (LocatorEmailResultsBindingData != "")
            return LocatorEmailResultsBindingData.slice(0, -3);
        else
            return LocatorEmailResultsBindingData;
    }
}]);

function callLocatorEmailService($scope, LocatorServices, LocatorEmailDataServices, postParams, LocatorEmailClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope) {
    $scope.pleaseWait = { "display": "block" };
    // console.log("Into the callLocatorEmailService method");
    
    LocatorEmailDataServices.setSearchParams(postParams);
   // console.log(postParams)
    LocatorServices.getLocatorEmailAdvSearchResults(postParams).success(function (result) {
        console.log(result.length);

        if (result.length == 0)
        { $scope.LocatorNoResults = "No Results Found!";}
        LocatorEmailDataServices.setSearchResutlsData(result);
        var _result = LocatorEmailDataServices.getSearchResultsData();
        
        //Setting the last search result's data in the mainService
       
        mainService.setLastLocatorEmailSearchResult(_result);

        if (_result.length > 0) {
            if (result == LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                angular.element("#accessDeniedModal").modal();
            }
            else {
            $scope.pleaseWait = { "display": "none" };
            $location.url(NAVIGATE_URL);
            }
        }
        else {
            
            $scope.ConstNoResults = "No Results found!";
            $scope.pleaseWait = { "display": "none" };
        }
    }).error(function (result) {
        myApp.hidePleaseWait();
        $scope.pleaseWait = { "display": "none" };
        if (result == LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED) {
            var output = {
                finalMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
            }
            myApp.hidePleaseWaitmessagePopup
            $scope.pleaseWait = { "display": "none" };
            $uibModalInstance.close(output);
        }
        else if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }

    });


}


