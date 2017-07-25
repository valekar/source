//Controller for the New Account Search functionality
newAccMod.controller('newAccountSearchController', ['$state', '$http', '$location', '$rootScope', '$scope', '$uibModal', 'uibDateParser', 'NewAccountService', 'NewAccountDataService', 'uiGridTreeViewConstants', 'NewAccountHelperService', 'uiGridConstants','mainService',
    function ($state, $http, $location, $rootScope, $scope, $uibModal, dateParser, service, dataService, uiGridTreeViewConstants, helperService, uiGridConstants,mainService) {
    
        /************************* Constants *************************/
        var BasePath = $("base").first().attr("href");
        $scope.processingOverlaySource = BasePath + "Images/Loading.gif";
        var NAVIGATE_URL = "/newaccount/search/result";
        $rootScope.selectNAICSCodeTemplate = BasePath + "App/NewAccount/Views/SelectNAICSSegments.tpl.html";

        /************************* Generic Methods *************************/
        //Method to hide the search message label
        $scope.clearSearchMessage = function () {
            $scope.showSearchMessage = false;
        }

        //Method to show the search message label
        $scope.showSearchMessage = function () {
            $scope.showSearchMessage = true;
        }

        //Method to hide the processing overlay
        $scope.hidePleaseWait = function () {
            $scope.processingOverlay = false;
        }

        //Method to show the processing overlay
        $scope.showPleaseWaitText = function () {
            $scope.processingOverlay = true;
        }

        /************************* Date Picklist configurations *************************/
        $scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];
        $scope.dateParser = dateParser;

        $scope.clickDateRangePicker = function () {
            angular.element(accountCreationDate).click();
        };

        /************************* DOM Element Data Initialization *************************/
        //Hiding the search message field
        $scope.clearSearchMessage();
        $scope.hidePleaseWait();
        
        //Declaring the search input object
        $scope.searchInput = {};

        //Defining the default search inputs
        $scope.searchInput.StewardingStatus = "All";
        $scope.searchInput.LOBType = "All";
        $scope.searchInput.accountCreatedDate = { startDate: new Date(), endDate: new Date(),maxDate:new Date() };
        $scope.searchInput.MasteringType = "All";
        $scope.searchInput.EntOrgType = "All";
        
        //Preset the Search inputs on load
        var SearchInput = {};
        SearchInput = dataService.getNewAccountSearchInput();

        
        if (!angular.isUndefined(SearchInput)) {
            $scope.searchInput.LOBType = angular.isUndefined(SearchInput.los) ? '' : SearchInput.los;
            $scope.searchInput.StewardingStatus = angular.isUndefined(SearchInput.naicsStatus) ? '' : SearchInput.naicsStatus;
            $scope.searchInput.accountCreatedDate.startDate = angular.isUndefined(SearchInput.createdDateFrom) ? '' : SearchInput.createdDateFrom;
            $scope.searchInput.accountCreatedDate.endDate = angular.isUndefined(SearchInput.createdDateTo) ? '' : SearchInput.createdDateTo;
            $scope.searchInput.MasteringType = angular.isUndefined(SearchInput.masteringType) ? '' : SearchInput.masteringType;
            $scope.searchInput.EntOrgType = angular.isUndefined(SearchInput.enterpriseOrgAssociation) ? '' : SearchInput.enterpriseOrgAssociation;
            $scope.searchInput.EnterpriseOrgId = angular.isUndefined(SearchInput.enterpriseOrgId) ? '' : SearchInput.enterpriseOrgId;            
            $scope.searchInput.selectedNaicsSegments = angular.isUndefined(SearchInput.listNaicsCodes) ? '' : SearchInput.listNaicsCodes;
            if (angular.isUndefined(SearchInput.los) && angular.isUndefined(SearchInput.naicsStatus) && angular.isUndefined(SearchInput.createdDateFrom) && angular.isUndefined(SearchInput.createdDateTo) && angular.isUndefined(SearchInput.masteringType) && angular.isUndefined(SearchInput.enterpriseOrgAssociation) && angular.isUndefined(SearchInput.enterpriseOrgId) && angular.isUndefined(SearchInput.listNaicsCodes))
            {
                //Defining the default search inputs
                $scope.searchInput.StewardingStatus = "All";
                $scope.searchInput.LOBType = "All";
                $scope.searchInput.accountCreatedDate = { startDate: new Date(), endDate: new Date(), maxDate: new Date() };
                $scope.searchInput.MasteringType = "All";
                $scope.searchInput.EntOrgType = "All";
            }
        }

        //Method to get the recent searches on load of the application
        service.getRecentSearches().success(function (result) {
            $rootScope.NewAccountRecentSearches = result;
        }).error(function (result) {
            errorPopups(result);
        });
       
        /************************* DOM Element Behaviours *************************/
        //Search method
        $scope.NewAccountSearchFormMethod = function()
        {
            //Check if the form is valid
            if($scope.searchFormName.$valid)
            {
                $scope.showPleaseWaitText();

                //Frame the inputs
                $scope.serviceSearchInput = {};
                $scope.serviceSearchInput.los = (angular.isUndefined($scope.searchInput.LOBType) ? "" : $scope.searchInput.LOBType);
                $scope.serviceSearchInput.naicsStatus = (angular.isUndefined($scope.searchInput.StewardingStatus) ? "" : $scope.searchInput.StewardingStatus);
                $scope.serviceSearchInput.createdDateFrom = (angular.isUndefined($scope.searchInput.accountCreatedDate.startDate) ? "" : moment($scope.searchInput.accountCreatedDate.startDate).format("MM/DD/YYYY"));
                $scope.serviceSearchInput.createdDateTo = (angular.isUndefined($scope.searchInput.accountCreatedDate.endDate) ? "" : moment($scope.searchInput.accountCreatedDate.endDate).format("MM/DD/YYYY"));
                $scope.serviceSearchInput.masteringType = (angular.isUndefined($scope.searchInput.MasteringType) ? "" : $scope.searchInput.MasteringType);
                $scope.serviceSearchInput.enterpriseOrgAssociation = (angular.isUndefined($scope.searchInput.EntOrgType) ? "" : $scope.searchInput.EntOrgType);
                $scope.serviceSearchInput.enterpriseOrgId = (angular.isUndefined($scope.searchInput.EnterpriseOrgId) ? "" : $scope.searchInput.EnterpriseOrgId);
                $scope.serviceSearchInput.listNaicsCodes = (angular.isUndefined($scope.searchInput.selectedNaicsSegments) ? "" : $scope.searchInput.selectedNaicsSegments);
                $scope.searchInput.selectedNaicsSegments = listSelectedNAICSCodes;
                //Store the search input criteria
                dataService.setNewAccountSearchInput($scope.serviceSearchInput);

                //Invoke the service method to place the search
                service.getNewAccountSearchResults($scope.serviceSearchInput).success(function (searchRes) {
                    //To track the recent searches
                    service.logRecentSearch($scope.serviceSearchInput).success(function (result) {
                        //To refresh the recent searches list
                        service.getRecentSearches().success(function (result) {
                            $rootScope.NewAccountRecentSearches = result;
                        });
                    });

                    var searchResultJson = searchRes;
                    $scope.hidePleaseWait();
                    $scope.showSearchMessage = false;

                    //Invalid Search
                    if (!searchResultJson.boolValidSearch) {
                        $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.INVALID_SEARCH;
                        $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.INVALID_SEARCH;
                        $scope.showSearchMessage = true;
                    }
                        //No Records Found
                    else if (searchResultJson.boolNoRecord) {
                        $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
                        $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_SEARCH_RESULTS;
                        $scope.showSearchMessage = true;
                    }
                        //Show the search results
                    else {
                        dataService.setNewAccountSearchResults(searchResultJson);
                        $location.url(NAVIGATE_URL);
                    }
                })
                .error(function (result) {
                    errorPopups(result);
                    $scope.hidePleaseWait();
                });
            }
        }

        //Clear Search method
        $scope.clearNewAccountSearch = function()
        {
            //Defining the default search inputs
            $scope.searchInput.StewardingStatus = "All";
            $scope.searchInput.LOBType = "All";
            $scope.searchInput.accountCreatedDate = { startDate: new Date(), endDate: new Date() };
            $scope.searchInput.MasteringType = "All";
            $scope.searchInput.EntOrgType = "All";
            
            //Hiding the search message field
            $scope.clearSearchMessage();
            $scope.hidePleaseWait();
        }

        //Method to perform the search
        $scope.newAccountRecentSearch = function (obj) {
            $scope.showPleaseWaitText();
            var SearchInputViewModel = {};
            angular.element(newAccountrecentSearches).modal('hide');

            SearchInputViewModel = obj;

            //Set the search input values
            if (!angular.isUndefined(SearchInputViewModel)) {
                $scope.searchInput.LOBType = angular.isUndefined(SearchInputViewModel.los) ? '' : SearchInputViewModel.los;
                $scope.searchInput.StewardingStatus = angular.isUndefined(SearchInputViewModel.naicsStatus) ? '' : SearchInputViewModel.naicsStatus;
                $scope.searchInput.accountCreatedDate.startDate = angular.isUndefined(SearchInputViewModel.createdDateFrom) ? '' : SearchInputViewModel.createdDateFrom;
                $scope.searchInput.accountCreatedDate.endDate = angular.isUndefined(SearchInputViewModel.createdDateTo) ? '' : SearchInputViewModel.createdDateTo;
                $scope.searchInput.MasteringType = angular.isUndefined(SearchInputViewModel.masteringType) ? '' : SearchInputViewModel.masteringType;
                $scope.searchInput.EntOrgType = angular.isUndefined(SearchInputViewModel.enterpriseOrgAssociation) ? '' : SearchInputViewModel.enterpriseOrgAssociation;
                
                if (angular.isUndefined(SearchInputViewModel.los) && angular.isUndefined(SearchInputViewModel.naicsStatus) && angular.isUndefined(SearchInputViewModel.createdDateFrom) && angular.isUndefined(SearchInputViewModel.createdDateTo) && angular.isUndefined(SearchInputViewModel.masteringType) && angular.isUndefined(SearchInputViewModel.enterpriseOrgAssociation)) {
                    //Defining the default search inputs
                    $scope.searchInput.StewardingStatus = "All";
                    $scope.searchInput.LOBType = "All";
                    $scope.searchInput.accountCreatedDate = { startDate: new Date(), endDate: new Date() };
                    $scope.searchInput.MasteringType = "All";
                    $scope.searchInput.EntOrgType = "All";
                }
            }

            //Store the search input criteria
            dataService.setNewAccountSearchInput(SearchInputViewModel);

            //Invoke the service method to place the search
            service.getNewAccountSearchResults(SearchInputViewModel).success(function (searchRes) {
                var searchResultJson = searchRes;
                $scope.hidePleaseWait();
                $scope.showSearchMessage = false;

                //Invalid Search
                if (!searchResultJson.boolValidSearch) {
                    $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.INVALID_SEARCH;
                    $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.INVALID_SEARCH;
                    $scope.showSearchMessage = true;
                }
                    //No Records Found
                else if (searchResultJson.boolNoRecord) {
                    $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
                    $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_SEARCH_RESULTS;
                    $scope.showSearchMessage = true;
                }
                    //Show the search results
                else {
                    dataService.setNewAccountSearchResults(searchResultJson);
                    $location.url(NAVIGATE_URL);
                }
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });

        }

        //Pop-up surfacing
        function errorPopups(result) {
            if (result == GEN_CONSTANTS.ACCESS_DENIED) {
                MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE);
            }
            else if (result == GEN_CONSTANTS.DB_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.DB_ERROR_CONFIRM, GEN_CONSTANTS.DB_ERROR_MESSAGE);
            }
            else if (result == GEN_CONSTANTS.TIMEOUT_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.TIMEOUT_ERROR_CONFIRM, GEN_CONSTANTS.TIMEOUT_ERROR_MESSAGE);
            }
        }

        function MessagePopup($rootScope, headerText, bodyText) {
            $rootScope.newAccountModalPopupHeaderText = headerText;
            $rootScope.newAccountModalPopupBodyText = bodyText;
            angular.element(newAccountMessageDialogBox).modal({ backdrop: "static" });
        }
        /************************* Sample data for NAICS Code Add functionality *************************/
        var gridOption = {
            showTreeExpandNoChildren: false,
            enableSorting: true,
            enablePager: false,
            enableGridMenu: true,
            enableFiltering: true,
            enableVerticalScrollbar: 1,
            enableHorizontalScrollbar: 0,
            enableRowSelection: true,
            multiSelect: true,
            enableSelectAll: false,
            columnDefs: [
              { displayName: 'NAICS Title', field: 'naics_indus_title', width: '*', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000 },
              { displayName: 'NAICS Code', field: 'naics_cd', width: '*', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000 }
            ]
        };

        $rootScope.searchGridOptions = gridOption;

        //Registering the method which will get executed once the grid gets loaded
        $rootScope.searchGridOptions.onRegisterApi = function (gridApi) {

            //Store the grid data against a variable so that it can be used for pagination
            $rootScope.gridApiNAICSAdd = gridApi;
        }

        //Method to bind NAICS Codes Level2 and 3
        var dataLevel2and3 = [];
        service.getNAICSCodesLevel2and3()
            .success(function (result) {
                dataLevel2and3 = result;
                var writeoutNode = function (childArray, currentLevel, dataArray) {
                    childArray.forEach(function (childNode) {
                        childNode.$$treeLevel = currentLevel;
                        dataArray.push(childNode);
                        if (childNode.children.length > 0) {
                            writeoutNode(childNode.children, currentLevel + 1, dataArray);
                        }
                    });
                };

                $rootScope.searchGridOptions.data = [];
                writeoutNode(dataLevel2and3, 0, $rootScope.searchGridOptions.data);
            })
            .error(function (result) {
                errorPopups(result);
            });
        //Method to open the add pop-up on click of the Add button
        $scope.selectNaicsSegmentNewAccount = function () {

            $scope.showPleaseWaitText();
            $rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
            $rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children
            $rootScope.gridApiNAICSAdd.grid.clearAllFilters(); //Clear all the filters
            listSelectedNAICSCodes = [];
            $scope.selectedNaicsSegments = "";
            //Open the pop-up
            angular.element(editNAICSCodePopup).modal();
            $scope.hidePleaseWait();


        }
        //Method called on click of the Submit button
        var listSelectedNAICSCodes = [];
        $scope.submitNaicsSegment = function () {
            // $scope.showPleaseWaitText();
            //Hide the pop-up
            angular.element(editNAICSCodePopup).modal('hide');
            //Refresh the grid with updated information
            angular.forEach($rootScope.gridApiNAICSAdd.selection.getSelectedRows(), function (value, key) {
                listSelectedNAICSCodes.push(value.naics_cd);
            });
            $scope.searchInput.selectedNaicsSegments = listSelectedNAICSCodes;
        }


        
}]);