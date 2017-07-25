angular.module('case').controller('CaseSearchResultsController', ['$scope', '$rootScope', 'CaseServices', 'CaseDataServices', 'CaseMultiDataService', 'CaseMultiGridService',
    'uiGridConstants', '$window', '$http', '$localStorage', '$sessionStorage', 'CaseClearDataService', 'mainService', 'CaseApiService', '$location', 'CaseCRUDoperations', '$uibModal', '$stateParams',
    function ($scope, $rootScope, CaseServices, CaseDataServices, CaseMultiDataService, CaseMultiGridService, uiGridConstants, $window, $http, $localStorage, $sessionStorage, CaseClearDataService, mainService, CaseApiService, $location, CaseCRUDoperations, $uibModal, $stateParams) {
        var SEARCH_URL = "/case/search";
        var SEARCH_RESULTS_URL = "/case/search/results";
        var REDIRECT_URL = "/case/search/results/multi/";
        var inialize = function () {

            //$localStorage.$reset();
            
            $scope.toggleHeader = false;
            //  $scope.$storage = $localStorage;

            $scope.toggleShowResults = { "display": "none" };
            $scope.toggleHeader = { "display": "none" };
            $scope.pleaseWait = { "display": "none" };
            // $scope.CartVisibility = { "display": "none" };
            var columnDefs = CaseDataServices.getSearchResultsColumnDef(uiGridConstants);
           // console.log("Column Definitions " + columnDefs);
            //constituentServices.getCartResults().then(function (datas) {
            //    var carItemsNumber = datas.length;
            //    //  $scope.totalItems = carItemsNumber;
            //    if (carItemsNumber > 0) {
            //        $scope.CartVisibility = { "display": "block" };
            //        $scope.CartItemsNumber = carItemsNumber;
            //    }

            //});

            $scope.searchResultsGridOptions = CaseDataServices.getSearchGridOptions(uiGridConstants, columnDefs);
            $scope.searchResultsGridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.searchResultsGridApi = gridApi;
                //this is for selection of results
                $scope.searchResultsGridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    var msg = 'row selected ' + row.isSelected;

                    rows = $scope.searchResultsGridApi.selection.getSelectedRows();

                    angular.forEach(rows, function (k, v) {
                        {
                            if (k.$$hashKey != null) {
                                k.IndexString = k.$$hashKey;
                                delete k.$$hashKey;
                            }
                        }
                    });
                    //  console.log(rows);
                });
            }


            var _previousResults = CaseDataServices.getSearchResultsData();
           // console.log(_previousResults);
            if (_previousResults.length > 0) {
                $scope.searchResultsGridOptions = CaseServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                $scope.totalItems = _previousResults.length;
               // console.log("total Items");
               // console.log($scope.totalItems);
                $scope.currentPage = 1;
                $scope.toggleShowResults = { "display": "block" };
                $scope.toggleHeader = { "display": "block" };
                $scope.pleaseWait = { "display": "none" };
            }
            else {
                if (mainService.getLastCaseSearchResult()) {
                    //Get back the last search result     
                    _previousResults = mainService.getLastCaseSearchResult();
                    $scope.searchResultsGridOptions = CaseServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                    $scope.totalItems = _previousResults.length;
                   // console.log("total Items");
                  //  console.log($scope.totalItems);
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                    //CaseDataServices.setSearchResutlsData(mainService.getLastCaseSearchResult());
                   // $window.location.href = BasePath + SEARCH_RESULTS_URL;
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

        //triggered when you click on constituent_id(masterId) in searchResults page
        $scope.setGlobalValues = function (row) {
            //$localStorage.$reset();
            $sessionStorage.case_key = row.entity.case_key;
            $sessionStorage.case_nm = row.entity.case_nm;
           
            //$rootScope.headerCaseName = row.entity.case_nm;
            //$rootScope.headerCaseKey = row.entity.case_key;
            //added by srini
            CaseMultiDataService.clearData();
           // CaseClearDataService.clearMultiData();
            $scope.pleaseWait = { "display": "block" };
            $scope.BaseUrl = BasePath;

          

            $location.url(REDIRECT_URL + row.entity.case_key + "");
        };
        var pageName = "SearchResults";


        $scope.commonDeleteGridRow = function (row, grid) {
            commonDeleteGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, CASE_CONSTANTS.CASE_RESULTS);
        }


        $scope.commonEditGridRow = function (row, grid) {
            commonEditGridRow($scope, row, grid, CaseCRUDoperations, $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, CASE_CONSTANTS.CASE_RESULTS);
        }

        $scope.commonResearchGridRow = function (row, grid) {
            commonResearchGridRow($scope, mainService, row, grid, CaseApiService, $uibModal, $stateParams, $scope.searchResultsGridOptions, CASE_CONSTANTS.CASE_RESULTS, $window);
        }


        // tab level security - to hide edit/delete buttons // added by srini
        $scope.tabSecurity = true;
        if (CaseMultiGridService.getTabDenyPermission()) {
            $scope.tabSecurity = false;
        }


    }]);