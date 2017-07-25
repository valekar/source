angular.module('constituent').controller('constituentSearchResultsController',
    ['$scope', 'constituentServices', 'constituentDataServices', 'mainService', 'constMultiGridService',
    'uiGridConstants', '$window', '$http', '$localStorage', '$sessionStorage', 'constClearDataService',
    '$location', '$rootScope', 'exportService', 'globalServices', '$timeout','StoreData',
    function ($scope, constituentServices, constituentDataServices, mainService, constMultiGridService, uiGridConstants, $window, $http, $localStorage, $sessionStorage,
        constClearDataService, $location, $rootScope, exportService, globalServices, $timeout,StoreData) {
        var SEARCH_URL =  BasePath + "constituent/search";
        var REDIRECT_URL = "/constituent/search/results/multi/";
        var inialize = function () {
            // console.log("cart number" + $rootScope.CartItemsNumber);
            // $localStorage.$reset();
            //clear out the master/name/type 

            // this is done coz we do not want to see the header (masterid/name of detail section in search results)
            delete $sessionStorage.masterId;
            delete $sessionStorage.name;
            delete $sessionStorage.type;
            // tool tip for source system count column
            $scope.toggleToolTip = false;
            $scope.toggleHeader = false;
            //  $scope.$storage = $localStorage;
    
            $scope.toggleShowResults = { "display": "none" };
            $scope.toggleHeader = { "display": "none" };
            $scope.pleaseWait = { "display": "none" };

            // $rootScope.CartVisibility = { "display": "none" };
            var columnDefs = constituentDataServices.getSearchResultsColumnDef();
            
           // hide the merge button(AddtoCart) in search results
            var mergeUnmergePermission = constMultiGridService.getMergeUnmergePermission();
            $scope.showMergeButton = true;
            if (!mergeUnmergePermission) {
                $scope.showMergeButton = false;
            }
                       
            $scope.searchResultsGridOptions = constituentServices.getSearchGridOptions(uiGridConstants, columnDefs, mergeUnmergePermission);
 
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
                });
            }
            

            $scope.AddtoCart = function () {
                // console.log(JSON.stringify(rows));
                if (rows != null) {
                    constituentServices.addMergeToCart(rows).success(function (result) {
                        $rootScope.FinalMessage = CART.CONFIRMATION_MESSAGE;
                        $rootScope.ReasonOrTransKey = CART.REASON;

                        var message = {
                            finalMessage: "",
                            ReasonOrTransKey: CART.REASON,
                            ConfirmationMessage:CART.CONFIRMATION_MESSAGE
                        }
                        modalMessage($rootScope, message);
                       // $("#CartConfirmationModal").modal();
                        // $scope.CartVisibility = { "display": "block" };
                       
                        constituentServices.getCartResults().success(function (result) {
                            var carItemsNumber = result.length;
                            $rootScope.CartItemsNumber = carItemsNumber;
                            constituentServices.getUnmergeCartResults().success(function (result) {
                                constituentDataServices.setUnmergeCartData(result);
                                $rootScope.CartItemsNumber = $rootScope.CartItemsNumber + result.length;
                                if ($rootScope.CartItemsNumber > 0) {

                                    $rootScope.CartVisibility = true;
                                }
                                else {
                                    $rootScope.CartVisibility = false;
                                }
                            }).error(function (result) {
                                errorPopups(result);
                            });

                            
                        }).error(function (result) {
                            errorPopups(result);
                        });

                       
                    }).error(function (result) {
                        errorPopups(result);
                    });
                }

            };
         
            var _previousResults;
            if ($window.localStorage['Main-SearchResultsData'] != null) {
                // constituentDataServices.setSearchResutlsData($localStorage.QuickSearchResultsData);
                //$localStorage.QuickSearchResultsData = null;
                constituentDataServices.setSearchResutlsData(JSON.parse($window.localStorage['Main-SearchResultsData']));
                $window.localStorage.removeItem('Main-SearchResultsData');
            }
        
            if ($window.localStorage['Main-quickSearchParams'] != null) {
                constituentDataServices.setAdvSearchParams(JSON.parse($window.localStorage['Main-quickSearchParams']));
                $window.localStorage.removeItem('Main-quickSearchParams');
            }
            
            _previousResults = constituentDataServices.getSearchResultsData();
            if (_previousResults.length > 0) {
                $scope.searchResultsGridOptions = constituentServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                $scope.totalItems = _previousResults.length;
                //console.log($scope.totalItems);
                $scope.currentPage = 1;
                $scope.toggleShowResults = { "display": "block" };
                $scope.toggleHeader = { "display": "block" };
                $scope.pleaseWait = { "display": "none" };
            }
            else {
                if (mainService.getLastConstSearchResult()) {
                    _previousResults = mainService.getLastConstSearchResult();
                    $scope.searchResultsGridOptions = constituentServices.getSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
                    $scope.totalItems = _previousResults.length;
                    //console.log($scope.totalItems);
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    $window.location.href = SEARCH_URL;
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
            delete $sessionStorage.masterId;
            delete $sessionStorage.name;
            delete $sessionStorage.type;
            //CEM CHANGES
            StoreData.cleanData();
            $sessionStorage.masterId = row.entity.constituent_id;
            $sessionStorage.name = row.entity.name;
            $sessionStorage.type = row.entity.constituent_type;
            constClearDataService.clearMultiData();
            $scope.pleaseWait = { "display": "block" };
            $scope.BaseUrl = BasePath;
            $location.url(REDIRECT_URL + row.entity.constituent_id + "");

        };



        $scope.exportSearchResults = function () {

            $timeout(function () {
                var postParams = constituentDataServices.getAdvSearchParams();
                messagePopup($rootScope, "File will be exported shortly.", "Exporting...");
                exportService.getSearchExportData(postParams, $rootScope).success(function () {
                    messagePopupClose();
                }).error(function (result) {
                    if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                        messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
                    }
                    else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }, 500);

           
           
        }


        function errorPopups(result) {
                if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                    messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
                }
                else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == CRUD_CONSTANTS.DB_ERROR) {
                    messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
        }


    }]);


angular.module('constituent').controller('cartLinkCtrl', ['constituentServices', '$rootScope', 'constituentDataServices', '$scope', '$location','$sessionStorage',
    function (constituentServices, $rootScope, constituentDataServices, $scope, $location, $sessionStorage) {
    $rootScope.CartVisibility = false;
    constituentServices.getCartResults().success(function (result) {
        var carItemsNumber = result.length;
        $rootScope.CartItemsNumber = carItemsNumber;
  

        constituentServices.getUnmergeCartResults().success(function (result) {
            constituentDataServices.setUnmergeCartData(result);
            $rootScope.CartItemsNumber = $rootScope.CartItemsNumber + result.length;
            if ($rootScope.CartItemsNumber > 0) {

                $rootScope.CartVisibility = true;
            }
            else {
                $rootScope.CartVisibility = false;
            }
        }).error(function (result) {
            errorPopups(result);
        });
       
    }).error(function (result) {
        errorPopups(result);
    });

   


    $scope.ViewCart = function () {
        delete $sessionStorage.masterId;
        delete $sessionStorage.name;
        delete $sessionStorage.type;
        $location.url("/constituent/search/results/cart");
    }


    function errorPopups(result) {
        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
    }

}]);

