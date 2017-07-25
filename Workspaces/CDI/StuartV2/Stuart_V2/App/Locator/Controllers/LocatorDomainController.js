
angular.module('locator').controller("locatorDomainController", ['$scope', '$location', '$log', 'LocatorDomainServices',
    '$window', 'LocatorDomainDataServices', '$localStorage', '$sessionStorage', 'LocatorDomainClearDataServices', 'mainService', '$state', '$rootScope',
function ($scope, $location, $log, LocatorDomainServices, $window, LocatorDomainDataServices, $localStorage, $sessionStorage, LocatorDomainClearDataServices, mainService, $state, $rootScope) {

    var NAVIGATE_URL = "/locator/domain/results";
    var username = "";
    var BASE_URL = BasePath + 'App/Locator/Views/Multi';
    var SearchPanelsCount = 5; // Define the maximum number of search panels
    var defaultPanelsCount = 1; // Set the number of search panels to be opened by default

    var appendString = 'ID'; // Specify the string that needs to be appended to distinguish between each elements
    $scope.LocStatus = "Waiting for Approval";
    $scope.pleaseWait = { "display": "none" };
    var initialize = function () {
       
        var SearchParams = LocatorDomainDataServices.getSavedSearchParams(); //This variable holds the search params so that they can be bound when the users returns back to this page via breadcrumbs
        $scope.LocatorDomainSearchFormElements = [];
        $scope.SearchRows = defaultPanelsCount;
        for (i = 1; i <= SearchPanelsCount; i++) {
            $scope.LocatorDomainSearchFormElements.push(appendString + i);
        }



        //If you are adding an element to the HTML, then add the model names here as well
        $scope.LocatorParams = {
            SearchPanel: [],
            PanelSeparator: [],
            showCloseButton: [],
            LocValidDomain: [],
            LocInvalidDomain: [],
            LocStatus: [],
            cnt: [],           
        };      

        angular.forEach($scope.LocatorParams, function (paramskey, paramsvalue) {

            angular.forEach($scope.LocatorDomainSearchFormElements, function (key) {
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
        
        var postParams = {};
       
        $scope.pleaseWait = { "display": "none" };

        //Set the previously searched search params if navigating from breadcrumbs
        if (SearchParams) {
            var rootModel = SearchParams["LocatorDomainInputSearchModel"];
            
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
                $scope.LocatorParams.LocValidDomain[appendString + i] = rootModel[i - 1]["LocValidDomain"];
                $scope.LocatorParams.LocInvalidDomain[appendString + i] = rootModel[i - 1]["LocInvalidDomain"];
                $scope.LocatorParams.LocStatus[appendString + i] = rootModel[i - 1]["LocStatus"];
               

            }
        }
        
    };
   /// initialize();


    //when clicked in search go to search results and do a post there
    $scope.searchLocatorDomain = function () {
        
        $scope.pleaseWait = { "display": "block" };

        var postParams = LocatorDomainDataServices.getSearchParams($scope, $scope.SearchRows, appendString);      

        $sessionStorage.Dropdownvalue = $scope.maydrpvalue.split(' ').join('')
        $sessionStorage.validDomain = $scope.validDomain;
        $sessionStorage.InvalidDomain = $scope.InvalidDomain;
        var Dropdownvalue = $scope.maydrpvalue.split(' ').join('');

        if (Dropdownvalue == "All")
        {
          
            callLocatorDomainServiceAll($scope, LocatorDomainServices, LocatorDomainDataServices, postParams, LocatorDomainClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope, $sessionStorage);
        }
        else
        {
            var page = $sessionStorage.Dropdownvalue;
            callLocatorDomainService($scope, LocatorDomainServices, LocatorDomainDataServices, postParams, LocatorDomainClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope, $sessionStorage,page);
        }

       
    }


   

    $scope.LocatorDomainClear = function () {
        LocatorDomainServices.clearSearchParams($scope, SearchPanelsCount, defaultPanelsCount, appendString);
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
    //if (LocatorDomainMultiGridService.getTabDenyPermission()) {
    //    $scope.tabSecurity = false;
    //}

}]);



function callLocatorDomainService($scope, LocatorDomainServices, LocatorDomainDataServices, postParams, LocatorDomainClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope,$sessionStorage,page) {
    $scope.pleaseWait = { "display": "block" };
     
    //console.log(page);
    LocatorDomainDataServices.setSearchParams(postParams);
    LocatorDomainDataServices.setSearchParams_Approved(postParams);
    LocatorDomainDataServices.setSearchParams_Rejected(postParams);
    LocatorDomainDataServices.setSearchParams_WFA(postParams);
    LocatorDomainDataServices.setSearchParams_Reverted(postParams);

    LocatorDomainServices.getLocatorDomainAdvSearchResults(postParams).success(function (result) {      
       
        LocatorDomainDataServices.setSearchResutlsData(result);
        var _result = LocatorDomainDataServices.getSearchResultsData();

        if (page == "Approved") {
            mainService.setLastLocatorDomainSearchResult_Approved(result);
            LocatorDomainDataServices.setSearchResutlsData_Approved(result);
            var _result = LocatorDomainDataServices.getSearchResultsData_Approved();
        }
        else if (page == "Rejected") {
            mainService.setLastLocatorDomainSearchResult_Rejected(result);
            LocatorDomainDataServices.setSearchResutlsData_Rejected(result);
            var _result = LocatorDomainDataServices.getSearchResultsData_Rejected();
        } else if (page == "WaitingforApproval") {
            
            mainService.setLastLocatorDomainSearchResult_WFA(result);            
            LocatorDomainDataServices.setSearchResutlsData_WFA(result);
            var _result = LocatorDomainDataServices.getSearchResultsData_WFA();
           
            
        }
        else if (page == "Reverted") {
            mainService.setLastLocatorDomainSearchResult_Reverted(result);
            LocatorDomainDataServices.setSearchResutlsData_Reverted(result);
            var _result = LocatorDomainDataServices.getSearchResultsData_Reverted();
        }


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

function callLocatorDomainServiceAll($scope, LocatorDomainServices, LocatorDomainDataServices, postParams, LocatorDomainClearDataServices, mainService, $location, NAVIGATE_URL, $rootScope, $sessionStorage) {
    $scope.pleaseWait = { "display": "block" };
    var postParams_approved = { "LocatorDomainInputSearchModel": [] };
    var searchParams_Approved = { "LocValidDomain": $scope.validDomain, "LocInvalidDomain": $scope.InvalidDomain, "LocStatus": "All" };
    postParams_approved["LocatorDomainInputSearchModel"].push(searchParams_Approved);

    //LocatorDomainDataServices.setSearchParams(postParams_approved);
    LocatorDomainDataServices.setSearchParams_Approved(postParams);
    //console.log(postParams);
    LocatorDomainServices.getLocatorDomainAdvSearchResults(postParams).success(function (result) {
        var data_approved = [];
        var data_WFA = [];
        var data_rejected = [];
        var data_reverted = [];
       
        angular.forEach(result, function (item) {
            if (item.sts == "Approved") {

                data_approved.push(item);

            }
        });

        angular.forEach(result, function (item) {
            if (item.sts == "Rejected") {

                data_rejected.push(item);

            }
        });

        angular.forEach(result, function (item) {
            if (item.sts == "Waiting for Approval") {

                data_WFA.push(item);

            }
        });

        angular.forEach(result, function (item) {
            if (item.sts == "Reverted") {

                data_reverted.push(item);

            }
        });

        mainService.setLastLocatorDomainSearchResult_Approved(data_approved);
        LocatorDomainDataServices.setSearchResutlsData_Approved(data_approved);
        var _result_Approved = LocatorDomainDataServices.getSearchResultsData_Approved();

        mainService.setLastLocatorDomainSearchResult_WFA(data_WFA);
        LocatorDomainDataServices.setSearchResutlsData_WFA(data_WFA);
        var _result_WFA = LocatorDomainDataServices.getSearchResultsData_WFA();

        mainService.setLastLocatorDomainSearchResult_Rejected(data_rejected);
        LocatorDomainDataServices.setSearchResutlsData_Rejected(data_rejected);
        var _result_Rejected = LocatorDomainDataServices.getSearchResultsData_Rejected();

        mainService.setLastLocatorDomainSearchResult_Reverted(data_reverted);
        LocatorDomainDataServices.setSearchResutlsData_Reverted(data_reverted);
        var _result_Reverted = LocatorDomainDataServices.getSearchResultsData_Reverted();

        

        
        if (_result_Approved.length > 0 || _result_WFA.length > 0 || _result_Rejected.length > 0 || _result_Reverted.length > 0) {
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


