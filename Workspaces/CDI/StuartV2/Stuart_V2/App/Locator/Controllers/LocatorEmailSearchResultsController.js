

angular.module('locator').controller('LocatorEmailSearchResultsController', ['$scope', '$rootScope', 'LocatorServices', 'LocatorEmailDataServices', 'LocatorEmailMultiDataService',
    'uiGridConstants', '$window', '$http', '$localStorage', '$sessionStorage', 'LocatorEmailClearDataService', 'mainService','$location', 'LocatorEmailCRUDoperations', '$uibModal', '$stateParams',
    function ($scope, $rootScope, LocatorServices, LocatorEmailDataServices, LocatorEmailMultiDataService, uiGridConstants, $window, $http, $localStorage, $sessionStorage, LocatorEmailClearDataService, mainService, $location, LocatorEmailCRUDoperations, $uibModal, $stateParams) {

       
        var SEARCH_URL = "/locator/email";
        var SEARCH_RESULTS_URL = "/locator/email/results";
        var REDIRECT_URL = "/locator/email/results/multi/";

        var inialize = function () {
            
            //$localStorage.$reset();

            $scope.toggleHeader = false;
            //  $scope.$storage = $localStorage;

            $scope.toggleShowResults = { "display": "none" };
            $scope.toggleHeader = { "display": "none" };
            $scope.pleaseWait = { "display": "none" };
            // $scope.CartVisibility = { "display": "none" };
            var columnDefs = LocatorEmailDataServices.getSearchResultsColumnDef(uiGridConstants);
          
            $scope.searchResultsGridOptions = LocatorEmailDataServices.getSearchGridOptions(uiGridConstants, columnDefs);

            $scope.searchResultsGridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.searchResultsGridApi = gridApi;
               
            }


            var _previousResults = LocatorEmailDataServices.getSearchResultsData();           
            if (_previousResults.length > 0) {
              
                $scope.searchResultsGridOptions = LocatorServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                $scope.totalItems = _previousResults.length;
                // console.log("total Items");
                // console.log($scope.totalItems);
                $scope.currentPage = 1;
                $scope.toggleShowResults = { "display": "block" };
                $scope.toggleHeader = { "display": "block" };
                $scope.pleaseWait = { "display": "none" };
            }
            else {
                if (mainService.getLastLocatorEmailSearchResult()) {
                   
                    //Get back the last search result     
                    _previousResults = mainService.getLastLocatorEmailSearchResult();
                    $scope.searchResultsGridOptions = LocatorServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                    $scope.totalItems = _previousResults.length;                   
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                    
                }
                else {
                    $window.location.href = BasePath + SEARCH_URL;
                }
            }


        }
        inialize();

        $scope.pageChanged = function (page) {
            $scope.searchResultsGridApi.pagination.seek(page);
        };

       
        //filter for search results
        $scope.toggleSearchFilter = function () {
            $scope.searchResultsGridOptions.enableFiltering = !$scope.searchResultsGridOptions.enableFiltering;
            $scope.searchResultsGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }

        //triggered when you click on id in searchResults page
        $scope.setGlobalValues = function (row) {
            
            //$localStorage.$reset();
            $sessionStorage.locator_addr_key = row.entity.email_key;
            $sessionStorage.cnst_email_addr = row.entity.cnst_email_addr;
            
            LocatorEmailMultiDataService.clearData();
            
            $scope.pleaseWait = { "display": "block" };
            $scope.BaseUrl = BasePath;
            
            $location.url(REDIRECT_URL + row.entity.email_key + "");
            
        };
        var pageName = "SearchResults";


        $scope.commonDeleteGridRow = function (row, grid) {
            commonDeleteGridRow($scope, row, grid,  $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, LOCATOR_CONSTANTS.LOCATOR_RESULTS);
        }


        $scope.commonEditGridRow = function (row, grid) {
            
            commonEditGridRow($scope, row, grid, LocatorEmailCRUDoperations, $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, LOCATOR_CONSTANTS.LOCATOR_RESULTS);
           
        }

        $scope.commonResearchGridRow = function (row, grid) {
            commonResearchGridRow($scope, mainService, row, grid,  $uibModal, $stateParams, $scope.searchResultsGridOptions, LOCATOR_CONSTANTS.LOCATOR_RESULTS, $window);
        }


        // tab level security - to hide edit/delete buttons // added by srini
        $scope.tabSecurity = true;
        //if (locatoremailmultigridservice.gettabdenypermission()) {
        //    $scope.tabsecurity = false;
        //}


    }]);

//To bind the detailed view of the email
angular.module('locator').controller('LocatorDetailsEmailController', ['$scope', '$rootScope', 'LocatorServices', 'LocatorEmailDataServices', 'LocatorEmailMultiDataService',
        'uiGridConstants', '$window', '$http', '$localStorage', '$sessionStorage', 'LocatorEmailClearDataService', 'mainService', '$location', 'LocatorEmailCRUDoperations', '$uibModal', '$stateParams',
        function ($scope, $rootScope, LocatorServices, LocatorEmailDataServices, LocatorEmailMultiDataService, uiGridConstants, $window, $http, $localStorage, $sessionStorage, LocatorEmailClearDataService, mainService, $location, LocatorEmailCRUDoperations, $uibModal, $stateParams) {

             var initialize = function () {
                $scope.toggleDetails = { "display": "none" };
                $scope.pleaseWait = { "display": "none" };
                $scope.BaseURL = BasePath;

                var columnDefs = LocatorEmailDataServices.getSearchDetailResultsColumnDef(uiGridConstants);
                $scope.searchResultsGridOptionsDetails = LocatorEmailDataServices.getSearchGridOptionsDetails(uiGridConstants, columnDefs);
                    $scope.searchResultsGridOptionsDetails.onRegisterApi = function (gridApi) {
                    //set gridApi on scope
                    $scope.searchResultsGridApi = gridApi;
                }

                var DetailedResults = LocatorEmailDataServices.getSearchResultsDataDetail();
                    $scope.searchResultsGridOptionsDetails = LocatorServices.getSearchResultsGridLayoutDetail($scope.searchResultsGridOptionsDetails, uiGridConstants, DetailedResults);
                    $scope.totalItemsDetails = DetailedResults.length;
                    //console.log($scope.totalItemsDetails);
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    
               

               var columnDefsConstituent = LocatorEmailDataServices.getSearchConstituentsDetailResultsColumnDef(uiGridConstants);
                    $scope.searchResultsGridOptionsConstituents = LocatorEmailDataServices.getSearchGridOptionsConstituents(uiGridConstants, columnDefsConstituent);
                    $scope.searchResultsGridOptionsConstituents.onRegisterApi = function (gridApiConstituents) {
                        //set gridApi on scope
                        $scope.searchResultsGridApi = gridApiConstituents;
                    }

                    var DetailedResultsConstituent = LocatorEmailDataServices.getSearchResultsDataDetailConstituent();
                    $scope.searchResultsGridOptionsConstituents = LocatorServices.getSearchResultsGridLayoutDetailConstituent($scope.searchResultsGridOptionsConstituents, uiGridConstants, DetailedResultsConstituent);

                    $scope.totalItemsConstituent = DetailedResultsConstituent.length;
                    //console.log($scope.totalItemsConstituent);
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
            };
            initialize();

            //added by srini for pagination
            $scope.pageChanged = function (page) {
                $scope.gridApi.pagination.seek(page);
            };

            // this is triggered in grid
            $scope.getConstituentDetails = function (row) {
                
                //console.log(row);
                $localStorage.LocatorEmail = {
                    masterId: row.entity.constituent_id

                };
                console.log($localStorage.LocatorEmail)
                $window.location.href = BasePath + 'constituent/search';
                // $window.open(BasePath + "constituent/search");
            };
           

        }]);

