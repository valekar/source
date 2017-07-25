angular.module('transaction').controller('TransactionSearchResultsController', ['$scope', '$state', 'TransactionServices', 'TransactionDataServices',
    'uiGridConstants', '$window', '$http', '$localStorage', 'TransactionClearDataService', '$location', '$uibModal', '$rootScope', '$uibModalStack', 'CaseServices', '$q',
    'uiGridExporterService','uiGridExporterConstants',
function ($scope, $state, TransactionServices, TransactionDataServices, uiGridConstants, $window, $http, $localStorage, TransactionClearDataService,
    $location, $uibModal, $rootScope, $uibModalStack, CaseServices, $q, uiGridExporterService, uiGridExporterConstants) {
    var SEARCH_URL = BasePath + "transaction/search";
    var REDIRECT_URL = "/transaction/search/results/multi/";
    var inialize = function () {

        $scope.toggleHeader = false;

        $scope.toggleShowResults = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };

        $scope.totalItems = 0;

        var columnDefs = TransactionDataServices.getTransSearchResultsColumnDef();
        //console.log("Column Definitions " + columnDefs);

        /** added by srini for tab security**/

        var newColumnDefs = TransactionDataServices.removeUserAction(columnDefs);

        /** added by srini**/
        $scope.searchResultsGridOptions = TransactionDataServices.getTransSearchGridOptions(uiGridConstants, newColumnDefs);
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
               // console.log(rows);
            });
        }


        var _previousResults = TransactionDataServices.getTransSearchResultsData();
       // console.log(_previousResults);
        if (_previousResults.length > 0 && _previousResults != "Error") {
            $scope.searchResultsGridOptions = TransactionServices.getTransSearchResultsGridLayout($scope.searchResultsGridOptions, uiGridConstants, _previousResults);
            $scope.totalItems = _previousResults.length;
           // console.log($scope.totalItems);
            $scope.currentPage = 1;
            $scope.toggleShowResults = { "display": "block" };
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };
        }
        else {
            if ($localStorage.lastTransSearchResultData) {
                _previousResults = $localStorage.lastTransSearchResultData;
            }
            $window.location.href = SEARCH_URL;
        }


    }
    inialize();

    $scope.pageChanged = function (page) {
        $scope.searchResultsGridApi.pagination.seek(page);
    };

    $scope.$on('gridRefreshResult', function (event, arg) {
        $scope.pleaseWait = { "display": "block" };

        if (angular.isUndefined(arg)) {
            arg = $localStorage.transAdvSearchParams;
        }

       // console.log("In Event Args after broadcast");
       // console.log(arg);

        //Get the Transaction Advance Search Run again to re-populate the grid
        var firstPromise = TransactionServices.getTransactionAdvSearchResults(arg).success(function (result) {
           // console.log("Trans Search Results");
           // console.log(result);
            $scope.searchResultsGridOptions.data = '';
            $scope.searchResultsGridOptions.data = result;
            TransactionDataServices.setTransSearchResultsData(result);
            $localStorage.lastTransSearchResultData = result;

        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        });

        //Once the promise is resolved , bind and show the grid
        $q.all([firstPromise]).then(function () {
            //console.log("In Q");
           // console.log($scope.pleaseWait);
            $scope.pleaseWait = { "display": "none" };
            //console.log("After Q");
           // console.log($scope.pleaseWait);
        });
    });

    //filter for search results
    $scope.toggleSearchFilter = function () {
        $scope.searchResultsGridOptions.enableFiltering = !$scope.searchResultsGridOptions.enableFiltering;
        $scope.searchResultsGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }

    //triggered when you click on trans_key in searchResults page
    $scope.setGlobalValues = function (row) {

        //console.log("Setting Global Values");
        var detailsParams = {
            "trans_key": row.trans_key,
            "trans_typ_dsc": row.trans_typ_dsc,
            "sub_trans_typ_dsc": row.sub_trans_typ_dsc,
            "trans_stat": row.trans_stat
        };
        TransactionDataServices.setTransSearchDetailsParameters(detailsParams);
       // console.log(detailsParams);
        $scope.pleaseWait = { "display": "block" };
        $scope.BaseUrl = BasePath;

        $location.url(REDIRECT_URL + row.trans_key + "");

    };

    //Case Info Modal on clicking of a case link on the search results page
    $scope.getEditCaseModal = function (trans_key, case_key) {
        $scope.pleaseWait = { "display": "block" };
        //console.log("GetEditCaseModal");
        //console.log(case_key);
       // console.log(trans_key);
        var infoParams = {
            "trans_key": trans_key,
            "case_key": case_key
        };


        var postParams = { "CaseInputSearchModel": [] };
        if (infoParams.case_key) {
            var searchParams = {
                "CaseId": infoParams.case_key,
                "CaseName": "",
                "ReferenceSource": "",
                "ReferenceId": "",
                "CaseType": "",
                "CaseStatus": "",
                "ConstituentName": "",
                "UserName": "",
                "ReportedDateFrom": "",
                "ReportedDateTo": "",
                "UserId": ""
            }
            postParams["CaseInputSearchModel"].push(searchParams);
        }

        //Go for the case details search now
        var firstPromise = CaseServices.getCaseAdvSearchResults(postParams).success(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                angular.element("#accessDeniedModal").modal();
            }
            else {
               // console.log("Result from case info details service");
                //console.log(result);
               // console.log(result[0].case_key);
               // console.log(result[0].case_nm);
                $scope.caseEditInfoResults = result;
               // console.log($scope.caseEditInfoResults);
            }
        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
            else {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });


        $q.all([firstPromise]).then(function () {

            $scope.pleaseWait = { "display": "none" };
            var modalInstance = $uibModal.open({
                templateUrl: BasePath + "App/Transaction/Views/common/CaseInfo.tpl.html",
                controller: 'CaseInfoCtrl',
                backdrop: 'static',
                resolve: {
                    infoParams: function () {
                        return $scope.caseEditInfoResults;
                    }
                }
            });

        });

    };

    setAssociateCaseModal = function (trans_key, case_key) {
        //console.log("In the opening Modal method");
       // console.log(case_key);
       // console.log(trans_key);
        var params = { "case_key": case_key, "trans_key": trans_key };
        var modalInstance = $uibModal.open({
            templateUrl: BasePath + "App/Transaction/Views/common/AssociateCase.tpl.html",
            controller: 'AssociateCaseCtrl',
            backdrop: 'static',
            resolve: {
                associateParams: function () {
                    return params;
                }
            }
        });

        modalInstance.result.then(function (result) {
            modalMessage($rootScope, result, $state, $uibModalStack, TransactionDataServices);
        })
    };

    $scope.commonEditGridRow = function (trans_key, case_key) {
        setAssociateCaseModal(trans_key, case_key);
    }

    //Added For Associate Case Template
    $rootScope.TransCaseSearchPath = BasePath + "App/Transaction/Views/common/CaseSearch.tpl.html";



    //added by srini for exporting the search results
    $scope.export = function(){
        console.log("aded export");

        var grid = $scope.searchResultsGridApi.grid;
        var rowTypes = uiGridExporterConstants.ALL;
        var colTypes = uiGridExporterConstants.ALL;
        uiGridExporterService.csvExport(grid, rowTypes, colTypes);


    }


}]);