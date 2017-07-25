

angular.module('locator').controller('LocatorAddressSearchResultsController', ['$scope', '$rootScope', 'LocatorAddressServices', 'LocatorAddressDataServices', 'LocatorAddressMultiDataService',
    'uiGridConstants', '$window', '$http', '$localStorage', '$sessionStorage', 'LocatorAddressClearDataService', 'mainService', '$location', 'LocatorAddressCRUDoperations', '$uibModal', '$stateParams',
    function ($scope, $rootScope, LocatorAddressServices, LocatorAddressDataServices, LocatorAddressMultiDataService, uiGridConstants, $window, $http, $localStorage, $sessionStorage, LocatorAddressClearDataService, mainService, $location, LocatorAddressCRUDoperations, $uibModal, $stateParams) {

       
        var SEARCH_URL = "/locator/address";
        var SEARCH_RESULTS_URL = "/locator/address/results";
        var REDIRECT_URL = "/locator/address/results/multi/";

        var inialize = function () {
            
            //$localStorage.$reset();

            $scope.toggleHeader = false;
            //  $scope.$storage = $localStorage;

            $scope.toggleShowResults = { "display": "none" };
            $scope.toggleHeader = { "display": "none" };
            $scope.pleaseWait = { "display": "none" };
            // $scope.CartVisibility = { "display": "none" };
            var columnDefs = LocatorAddressDataServices.getSearchResultsColumnDef(uiGridConstants);
          
            $scope.searchResultsGridOptions = LocatorAddressDataServices.getSearchGridOptions(uiGridConstants, columnDefs);

            $scope.searchResultsGridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.searchResultsGridApi = gridApi;
                //this is for selection of results
               
               
            }


            var _previousResults = LocatorAddressDataServices.getSearchResultsData();
            //console.log(_previousResults);
           
            if (_previousResults.length > 0) {
              
                $scope.searchResultsGridOptions = LocatorAddressServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                $scope.totalItems = _previousResults.length;
                // console.log("total Items");
                // console.log($scope.totalItems);
                $scope.currentPage = 1;
                $scope.toggleShowResults = { "display": "block" };
                $scope.toggleHeader = { "display": "block" };
               // $scope.pleaseWait = { "display": "none" };
            }
            else {
                if (mainService.getLastLocatorAddressSearchResult()) {
                   
                    //Get back the last search result     
                    _previousResults = mainService.getLastLocatorAddressSearchResult();
                    $scope.searchResultsGridOptions = LocatorAddressServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                    $scope.totalItems = _previousResults.length;
                    // console.log("total Items");
                    //  console.log($scope.totalItems);
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                   // $scope.pleaseWait = { "display": "none" };
                   
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
            $sessionStorage.LocAddrKey = row.entity.locator_addr_key;
           
           // $sessionStorage.cnst_email_addr = row.entity.cnst_email_addr;
            
            LocatorAddressMultiDataService.clearData();
            
            $scope.pleaseWait = { "display": "none" };
            $scope.BaseUrl = BasePath;
            
            $location.url(REDIRECT_URL + row.entity.locator_addr_key + "");
            
        };
        var pageName = "SearchResults";


        $scope.commonDeleteGridRow = function (row, grid) {
            commonDeleteGridRow($scope, row, grid,  $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, LOCATOR_CONSTANTS.LOCATOR_RESULTS);
        }


        $scope.commonEditGridRow = function (row, grid) {
            
            commonEditGridRow($scope, row, grid, LocatorAddressCRUDoperations, $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, LOCATOR_CONSTANTS.LOCATOR_RESULTS);
           
        }

        $scope.commonResearchGridRow = function (row, grid) {
            commonResearchGridRow($scope, mainService, row, grid,  $uibModal, $stateParams, $scope.searchResultsGridOptions, LOCATOR_CONSTANTS.LOCATOR_RESULTS, $window);
        }


        // tab level security - to hide edit/delete buttons // added by srini
        $scope.tabSecurity = true;

    }]);

//To bind the detailed view of the Address
angular.module('locator').controller('LocatorDetailsAddressController', ['$scope', '$rootScope', 'LocatorAddressServices', 'LocatorAddressDataServices', 'LocatorAddressMultiDataService',
        'uiGridConstants', '$window', '$http', '$localStorage', '$sessionStorage', 'LocatorAddressClearDataService', 'mainService','$location', 'LocatorAddressCRUDoperations', '$uibModal', '$stateParams',
        function ($scope, $rootScope, LocatorAddressServices, LocatorAddressDataServices, LocatorAddressMultiDataService, uiGridConstants, $window, $http, $localStorage, $sessionStorage, LocatorAddressClearDataService, mainService, $location, LocatorAddressCRUDoperations, $uibModal, $stateParams) {



            var initialize = function () {
                $scope.toggleDetails = { "display": "none" };
                $scope.pleaseWait = { "display": "none" };
                $scope.BaseURL = BasePath;
                var columnDefs = LocatorAddressDataServices.getSearchDetailResultsColumnDef(uiGridConstants);
                var columnDefs_const = LocatorAddressDataServices.getSearchDetailResultsColumnDef_Constituents(uiGridConstants);
                var columnDefs_Address_Assessments = LocatorAddressDataServices.getSearchDetailResultsColumnDef_Address_Assessments(uiGridConstants);
                
                $scope.searchResultsGridOptions_Details = LocatorAddressDataServices.getSearchGridOptions(uiGridConstants, columnDefs);
                $scope.searchResultsGridOptions_Constituents = LocatorAddressDataServices.getSearchGridOptions(uiGridConstants, columnDefs_const);
                $scope.searchResultsGridOptions_Address_Assessments = LocatorAddressDataServices.getSearchGridOptions(uiGridConstants, columnDefs_Address_Assessments);


                $scope.searchResultsGridOptions_Details.onRegisterApi = function (gridApi) {
                    //set gridApi on scope
                    $scope.searchResultsGridApi = gridApi;
                    //this is for selection of results

                    
                }
                var DetailedResults = LocatorAddressDataServices.getSearchResultsDataDetail();
                var DetailedResults_const = LocatorAddressDataServices.getSearchResultsDataDetail_Constituents();
                var DetailedResults_Address_Assessments = LocatorAddressDataServices.getSearchResultsDataDetail_Address_Assessments();
                
                $scope.searchResultsGridOptions_Details = LocatorAddressServices.getSearchResultsGridLayoutDetail($scope.searchResultsGridOptions_Details, uiGridConstants, DetailedResults);


                $scope.searchResultsGridOptions_Constituents = LocatorAddressServices.getSearchResultsGridLayoutDetail_Constituents($scope.searchResultsGridOptions_Constituents, uiGridConstants, DetailedResults_const);


                $scope.searchResultsGridOptions_Address_Assessments = LocatorAddressServices.getSearchResultsGridLayoutDetail_Address_Assessments($scope.searchResultsGridOptions_Address_Assessments, uiGridConstants, DetailedResults_Address_Assessments);

                    $scope.totalItems = DetailedResults.length;
                    // console.log("total Items");
                    // console.log($scope.totalItems);
                    $scope.currentPage = 1;
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

               // console.log(row);
                $localStorage.LocatorAddress = {
                    masterId: row.entity.constituent_id

                };
                console.log($localStorage.LocatorAddress)
                $window.location.href = BasePath + 'constituent/search';
                // $window.open(BasePath + "constituent/search");
            };
        }]);

