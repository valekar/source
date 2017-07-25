angular.module('locator').controller("LocatorAddressController", ['$scope', '$location', '$log', 'LocatorAddressServices',
    '$window', 'LocatorAddressDataServices', '$localStorage', '$sessionStorage', 'LocatorAddressClearDataServices', 'mainService', '$state', '$rootScope', 'LocatorAddressDropDownService',
function ($scope, $location, $log, LocatorAddressServices, $window, LocatorAddressDataServices, $localStorage, $sessionStorage, LocatorAddressClearDataServices, mainService, $state, $rootScope, LocatorAddressDropDownService) {

    var NAVIGATE_URL = "/locator/address/results";
    var username = "";

    var SearchPanelsCount = 5; // Define the maximum number of search panels
    var defaultPanelsCount = 1; // Set the number of search panels to be opened by default

    var appendString = 'ID'; // Specify the string that needs to be appended to distinguish between each elements


    var initialize = function () {

        if (mainService.getLastLocatorAddressSearchResult())
            $scope.lastLocatorAddressSearchResultPresent = true;
        else
            $scope.lastLocatorAddressSearchResultPresent = false;

        var SearchParams = LocatorAddressDataServices.getSavedSearchParams(); //This variable holds the search params so that they can be bound when the users returns back to this page via breadcrumbs
        $scope.LocatorAddressSearchFormElements = [];
        $scope.SearchRows = defaultPanelsCount;
        for (i = 1; i <= SearchPanelsCount; i++) {
            $scope.LocatorAddressSearchFormElements.push(appendString + i);
        }



        //If you are adding an element to the HTML, then add the model names here as well
        $scope.LocatorParams = {
            SearchPanel: [],
            PanelSeparator: [],
            showCloseButton: [],
            locator_addr_key: [],
            line1_addr: [],
            line2_addr: [],
            city: [],
            state: [],
            zip_5: [],
            deliv_loc_type: [],
            dpv_cd: [],
            code_category: [],
            LocAssessCode:[],
        };

        //Dropdown list

        $scope.State_dropdown = {
            states: States(),
            chapters: [],
            sourceSystems: []
        };

        LocatorAddressDropDownService.getDropDown(LOCATOR_CONSTANTS.LOCATOR_FINALASSESMENT_CODE).success(function (result) {

            $scope.LocatorParams.Finalassesmentcode = result;
            LocatorAddressDataServices.setFinalAssesmentCodeDropDowns(result);
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        angular.forEach($scope.LocatorParams, function (paramskey, paramsvalue) {

            angular.forEach($scope.LocatorAddressSearchFormElements, function (key) {
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
       

       


        var LocatorAddressRecentSearches = LocatorAddressServices.getLocatorAddressRecentSearches().success(function (result) {
            $scope.LocatorAddressRecentSearches = result;


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
            var rootModel = SearchParams["LocatorAddressInputSearchModel"];
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
                $scope.LocatorParams.line1_addr[appendString + i] = rootModel[i - 1]["line1_addr"];
                $scope.LocatorParams.city[appendString + i] = rootModel[i - 1]["city"];
                $scope.LocatorParams.state[appendString + i] = rootModel[i - 1]["state"];
                $scope.LocatorParams.zip_5[appendString + i] = rootModel[i - 1]["zip_5"];
                $scope.LocatorParams.deliv_loc_type[appendString + i] = rootModel[i - 1]["deliv_loc_type"];
                $scope.LocatorParams.dpv_cd[appendString + i] = rootModel[i - 1]["dpv_cd"];
                $scope.LocatorParams.code_category[appendString + i] = rootModel[i - 1]["code_category"];

            }
        }
    };
    initialize();


    //Get the recentmost search result data on clicking of the button from the mainService
    $scope.fetchLastLocatorAddressSearchResult = function () {

        if (mainService.getLastLocatorAddressSearchResult()) {
            //Get back the last search result           
            LocatorAddressDataServices.setSearchResutlsData(mainService.getLastLocatorAddressSearchResult());
            $state.go('locator.address.results', {});
        }
        else {
            window.alert("Session Storage seems just to be cleared out! Please proceed with the regular search");
        }
    }

    //when clicked in search go to search results and do a post there
    $scope.searchLocatorAddress = function () {
       
        $scope.pleaseWait = { "display": "block" };
       
        var postParams = LocatorAddressDataServices.getSearchParams($scope, $scope.SearchRows, appendString);
        console.log(postParams);
        angular.forEach(postParams.LocatorAddressInputSearchModel, function (item) {
            $scope.AddressKey = item.LocAddrKey;
            $scope.Addressline = item.LocAddressLine;
            $scope.city = item.LocCity;
            $scope.state = item.LocState;
            $scope.zip = item.LocZip;
            $scope.Deliverytype = item.LocDelType;
            $scope.Delivercode = item.LocDelCode;
            $scope.Finalass_code = item.LocAssessCode;

        })

        if (!!$scope.AddressKey ||!!$scope.Addressline ||!!$scope.city ||!!$scope.state ||!!$scope.zip
           ||!!$scope.Deliverytype ||!!$scope.Delivercode ||!!$scope.Finalass_code) {
            
           callLocatorAddressService($scope, LocatorAddressServices, LocatorAddressDataServices, postParams, LocatorAddressClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope);

        } else {
            $scope.pleaseWait = { "display": "none" };
            $scope.LocatorNoResults = "Please provide at least one input criteria before search";
        }

        
            }


    $scope.LocatorAddressRecentSearch = function (qry) {
        $scope.pleaseWait = { "display": "block" };
        $('#recentLocatorAddressSearchesModal').modal('hide');
        var listPostParam = [];
        listPostParam.push(qry);
        postParams = listPostParam;
        //postParams = constituentDataServices.getSearchParams($scope);
        callLocatorAddressService($scope, LocatorAddressServices, LocatorAddressDataServices, qry, LocatorAddressClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope);
    }

    $scope.LocatorAddressClear = function () {
        LocatorAddressServices.clearSearchParams($scope, SearchPanelsCount, defaultPanelsCount, appendString);
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
    //if (LocatorAddressMultiGridService.getTabDenyPermission()) {
    //    $scope.tabSecurity = false;
    //}

}]);

angular.module('locator').filter('placeholderfuncAddress', ['LocatorAddressDataServices', function (LocatorAddressDataServices) {
    return function (input) {
        var LocatorAddressResultsBindingData = "";
        var FianlAssesmentCtryDropDown;
        FianlAssesmentCtryDropDown = LocatorAddressDataServices.getFinalAssesmentCodeDropDowns();

        angular.forEach(input.LocatorAddressInputSearchModel, function (value, key) {
            if (value.LocAddrKey != null)
                LocatorAddressResultsBindingData += "Address Key: " + value.LocAddrKey + ", ";
            if (value.LocAddressLine != null)
                LocatorAddressResultsBindingData += "Address: " + value.LocAddressLine + ", ";
            if (value.LocCity != null)
                LocatorAddressResultsBindingData += "City: " + value.LocCity + ", ";
            if (value.LocState != null)
                LocatorAddressResultsBindingData += "State: " + value.LocState + ", ";
            if (value.LocZip != null)
                LocatorAddressResultsBindingData += "Zip: " + value.LocZip + ", ";

            if (value.LocDelType != null)
                LocatorAddressResultsBindingData += "Delivery Locator Type: " + value.LocDelType + ", ";
            if (value.LocDelCode != null)
                LocatorAddressResultsBindingData += "Deliverability Code: " + value.LocDelCode + ", ";

            if (value.LocAssessCode != null)
                LocatorAddressResultsBindingData += "Final Assessment Code: " + value.LocAssessCode + ", ";

            LocatorAddressResultsBindingData += ";";
        });


        if (LocatorAddressResultsBindingData != "")
            return LocatorAddressResultsBindingData.slice(0, -3);
        else
            return LocatorAddressResultsBindingData;
    }
}]);

function callLocatorAddressService($scope, LocatorAddressServices, LocatorAddressDataServices, postParams, LocatorAddressClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope) {
    $scope.pleaseWait = { "display": "block" };
    // console.log("Into the callLocatorAddressService method");

    LocatorAddressDataServices.setSearchParams(postParams);
    // console.log(postParams)
    LocatorAddressServices.getLocatorAddressAdvSearchResults(postParams).success(function (result) {
        //clear all multi tab data , need to do for normal tab too
        //LocatorAddressClearDataServices.clearMultiData(); // TEMPORARILY COMMENTED !!
        // console.log("retreived adv search results");
        
        if (result.length == 0)
        { $scope.LocatorNoResults = "No Results Found!"; }
        LocatorAddressDataServices.setSearchResutlsData(result);
        var _result = LocatorAddressDataServices.getSearchResultsData();

        //Setting the last search result's data in the mainService

        mainService.setLastLocatorAddressSearchResult(_result);

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


