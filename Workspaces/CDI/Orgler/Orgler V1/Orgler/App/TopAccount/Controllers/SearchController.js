//Controller for the New Account Search functionality
topAccMod.controller('topAccountSearchController', ['$state', '$http', '$location', '$rootScope', '$scope', '$uibModal', 'uibDateParser', 'TopAccountService', 'TopAccountDataService', 'TopAccountHelperService', 'uiGridConstants','uiGridTreeViewConstants',
    function ($state, $http, $location, $rootScope, $scope, $uibModal, dateParser, service, dataService, uiGridTreeViewConstants, helperService, uiGridConstants) {

        /************************* Constants *************************/
        var BasePath = $("base").first().attr("href");
        $scope.processingOverlaySource = BasePath + "Images/Loading.gif";
        var NAVIGATE_URL = "/topaccount/search/result";
        $rootScope.selectNAICSCodeTemplate = BasePath + "App/TopAccount/Views/SelectNAICSSegments.tpl.html";
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

        /* section for regex*/
        $scope.RFMScoreRegex = /^(1[0-5]|[1-9])$/;

        /************************* Date Picklist configurations *************************/
        //$scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];
        //$scope.dateParser = dateParser;

        //$scope.clickDateRangePicker = function () {
        //    angular.element(accountCreationDate).click();
        //};

        /************************* DOM Element Data Initialization *************************/
        //Hiding the search message field
        $scope.clearSearchMessage();
        $scope.hidePleaseWait();

        //Declaring the search input object
        $scope.searchInput = {};

        //Defining the default search inputs
        $scope.searchInput.StewardingStatus = "All";
        $scope.searchInput.LOBType = "All";       
        $scope.searchInput.EntOrgType = "All";
        $scope.searchInput.RFMScoreValue = "";
        $scope.searchInput = {};

        //Preset the Search inputs on load
        var SearchInput = {};
        SearchInput = dataService.getTopAccountSearchInput();


        if (!angular.isUndefined(SearchInput)) {
            $scope.searchInput.LOBType = angular.isUndefined(SearchInput.los) ? '' : SearchInput.los;
            $scope.searchInput.StewardingStatus = angular.isUndefined(SearchInput.naicsStatus) ? '' : SearchInput.naicsStatus;
            $scope.searchInput.RFMScoreValue = angular.isUndefined(SearchInput.rfm_scr) ? '' : SearchInput.rfm_scr;
            $scope.searchInput.selectedNaicsSegments = angular.isUndefined(SearchInput.listNaicsCodes) ? '' : SearchInput.listNaicsCodes;
            $scope.searchInput.EntOrgType = angular.isUndefined(SearchInput.enterpriseOrgAssociation) ? '' : SearchInput.enterpriseOrgAssociation;
           // $scope.selectedNaicsSegments = listSelectedNAICSCodes.toString();
            if (angular.isUndefined(SearchInput.los) && angular.isUndefined(SearchInput.naicsStatus) && angular.isUndefined(SearchInput.rfm_scr) && angular.isUndefined(SearchInput.listNaicsCodes) &&  angular.isUndefined(SearchInput.enterpriseOrgAssociation)) {
                //Defining the default search inputs
                $scope.searchInput.StewardingStatus = "All";
                $scope.searchInput.LOBType = "All";               
                $scope.searchInput.EntOrgType = "All";
                $scope.searchInput.RFMScoreValue = "";
               // $scope.searchInput.listNaicsCodes = ""
            }
        }

        //Method to get the recent searches on load of the application
        service.getRecentSearches().success(function (result) {
            $rootScope.TopAccountRecentSearches = result;
        }).error(function (result) {
            errorPopups(result);
        });

        /************************* DOM Element Behaviours *************************/
        //Search method
        $scope.TopAccountSearchFormMethod = function () {
            //Check if the form is valid
            if ($scope.searchFormName.$valid) {
                $scope.showPleaseWaitText();

                //Frame the inputs
                $scope.serviceSearchInput = {};
                $scope.serviceSearchInput.los = (angular.isUndefined($scope.searchInput.LOBType) ? "" : $scope.searchInput.LOBType);
                $scope.serviceSearchInput.naicsStatus = (angular.isUndefined($scope.searchInput.StewardingStatus) ? "" : $scope.searchInput.StewardingStatus);
                $scope.serviceSearchInput.rfm_scr = (angular.isUndefined($scope.searchInput.RFMScoreValue) ? "" : $scope.searchInput.RFMScoreValue);
                $scope.serviceSearchInput.listNaicsCodes = (angular.isUndefined($scope.searchInput.selectedNaicsSegments) ? "" : $scope.searchInput.selectedNaicsSegments);
                $scope.serviceSearchInput.enterpriseOrgAssociation = (angular.isUndefined($scope.searchInput.EntOrgType) ? "" : $scope.searchInput.EntOrgType);
                $scope.searchInput.selectedNaicsSegments = listSelectedNAICSCodes;

                //Store the search input criteria
                dataService.setTopAccountSearchInput($scope.serviceSearchInput);

                //Invoke the service method to place the search
                service.getTopAccountSearchResults($scope.serviceSearchInput).success(function (searchRes) {
                    //To track the recent searches
                    service.logRecentSearch($scope.serviceSearchInput).success(function (result) {
                        //To refresh the recent searches list
                        service.getRecentSearches().success(function (result) {
                            $rootScope.TopAccountRecentSearches = result;
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
                        dataService.setTopAccountSearchResults(searchResultJson);
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
        $scope.clearTopAccountSearch = function () {
            //Defining the default search inputs
            $scope.searchInput.StewardingStatus = "All";
            $scope.searchInput.LOBType = "All";           
            $scope.searchInput.EntOrgType = "All";
            $scope.searchInput.RFMScoreValue ="";            
            $scope.searchInput.selectedNaicsSegments = "";

            //Hiding the search message field
            $scope.clearSearchMessage();
            $scope.hidePleaseWait();
        }

        //Method to perform the search
        $scope.topAccountRecentSearch = function (obj) {
            $scope.showPleaseWaitText();
            var SearchInputViewModel = {};
            angular.element(newAccountrecentSearches).modal('hide');

            SearchInputViewModel = obj;

            //Set the search input values
            if (!angular.isUndefined(SearchInputViewModel)) {
                $scope.searchInput.LOBType = angular.isUndefined(SearchInputViewModel.los) ? '' : SearchInputViewModel.los;
                $scope.searchInput.StewardingStatus = angular.isUndefined(SearchInputViewModel.naicsStatus) ? '' : SearchInputViewModel.naicsStatus;
                $scope.searchInput.RFMScoreValue = angular.isUndefined(SearchInputViewModel.rfm_scr) ? '' : SearchInputViewModel.rfm_scr;
                $scope.searchInput.listSelectedNAICSCodes = angular.isUndefined(SearchInputViewModel.listNaicsCodes) ? '' : SearchInputViewModel.listNaicsCodes;
                $scope.searchInput.EntOrgType = angular.isUndefined(SearchInputViewModel.enterpriseOrgAssociation) ? '' : SearchInputViewModel.enterpriseOrgAssociation;
               // $scope.selectedNaicsSegments = listSelectedNAICSCodes.toString();

                if (angular.isUndefined(SearchInputViewModel.los) && angular.isUndefined(SearchInputViewModel.naicsStatus)  && angular.isUndefined(SearchInputViewModel.enterpriseOrgAssociation)) {
                    //Defining the default search inputs
                    $scope.searchInput.StewardingStatus = "All";
                    $scope.searchInput.LOBType = "All";                  
                    $scope.searchInput.EntOrgType = "All";
                    $scope.searchInput.RFMScoreValue = "";
                    //$scope.searchInput.selectedNaicsSegments = "";
                }
            }

            //Store the search input criteria
            dataService.setTopAccountSearchInput(SearchInputViewModel);

            //Invoke the service method to place the search
            service.getTopAccountSearchResults(SearchInputViewModel).success(function (searchRes) {
                var searchResultJson = searchRes;
                $scope.hidePleaseWait();
                $scope.showSearchMessage = false;

                //Invalid Search
                if (!searchResultJson.boolValidSearch) {
                    $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.INVALID_SEARCH;
                    $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.INVALID_SEARCH;
                    $scope.showSearchMessage = true;
                }
                //    No Records Found
                else if (searchResultJson.boolNoRecord) {
                    $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
                    $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_SEARCH_RESULTS;
                    $scope.showSearchMessage = true;
                }
                    //Show the search results
                else {
                    dataService.setTopAccountSearchResults(searchResultJson);
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
            angular.element(topAccountMessageDialogBox).modal({ backdrop: "static" });
        }

        /************************* RFM Details functionality *************************/
        $scope.getRFMScore = function () {
            $scope.showPleaseWaitText();

         

            service.getRFMDetails().success(function (result) {

                $rootScope.constAddressGridOptions.data = result;

                angular.element(editAddressPopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
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
        $scope.selectNaicsSegmentTopAccount = function () {

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