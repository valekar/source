

angular.module('locator').controller('LocatorDomainSearchResultsController', ['$scope', '$parse', '$rootScope', 'LocatorDomainServices', 'LocatorDomainDataServices', 'LocatorDomainMultiDataService',
    'uiGridConstants','$window', '$http', '$localStorage', '$sessionStorage', 'LocatorDomainClearDataServices', 'mainService','$location',  '$uibModal', '$stateParams','$state',
function ($scope, $parse, $rootScope, LocatorDomainServices, LocatorDomainDataServices, LocatorDomainMultiDataService, uiGridConstants, $window, $http, $localStorage, $sessionStorage, LocatorDomainClearDataServices, mainService, $location, $uibModal, $stateParams, $state) {

        var NAVIGATE_URL = "/locator/domain/results";
        var SEARCH_URL = "/locator/domain";
        var SEARCH_RESULTS_URL = "/locator/domain/results";
        var REDIRECT_URL = "/locator/domain/results/multi/";
        var BASE_URL = BasePath + 'App/Locator/Views/Multi';
        
        var inialize = function () {    

            $scope.toggleHeader = false;
            $scope.toggleShowResults = { "display": "none" };
            $scope.toggleHeader = { "display": "none" };
           // $scope.pleaseWait = { "display": "none" };
            $scope.selecteddata_Approved = [];
            $scope.selecteddata_WFA = [];
            var page = $sessionStorage.Dropdownvalue;
            if (page == "Approved") {
                var columnDefs_Approved = LocatorDomainDataServices.getSearchResultsColumnDef_Approved(uiGridConstants);
                $scope.searchResultsGridOptions_Approved = LocatorDomainDataServices.getSearchGridOptions_Approved(uiGridConstants, columnDefs_Approved);

                $scope.searchResultsGridOptions_Approved.onRegisterApi = function (gridApi_Approved) {

                    $scope.searchResultsGridApi_Approved = gridApi_Approved;
                    //this is for selection of results
                    $scope.searchResultsGridApi_Approved.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;
                        rows = $scope.searchResultsGridApi_Approved.selection.getSelectedRows();
                        $scope.selecteddata_Approved = [];
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Approved.push(item.email_domain_map_key);
                        });

                        angular.forEach(rows, function (k, v) {
                            {
                                if (k.$$hashKey != null) {
                                    k.IndexString = k.$$hashKey;
                                    delete k.$$hashKey;
                                }
                            }
                        });
                        //console.log(rows);
                        //console.log($scope.selecteddata_Approved)
                    });
                }
                var _previousResults_Approved = LocatorDomainDataServices.getSearchResultsData_Approved();



                if (_previousResults_Approved.length > 0) {

                    $scope.searchResultsGridOptions_Approved == LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, _previousResults_Approved);
                    $scope.totalItems_Approved = _previousResults_Approved.length;

                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    // console.log($scope.LocatorDomainTemplate_Approved)

                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Approved()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_Approved();
                        //console.log(_previousResults);
                        $scope.searchResultsGridOptions_Approved = LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, _previousResults);
                        $scope.totalItems_Approved = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "Rejected") {
                var columnDefs = LocatorDomainDataServices.getSearchResultsColumnDef_Rejected(uiGridConstants);
                $scope.searchResultsGridOptions_Rejected = LocatorDomainDataServices.getSearchGridOptions_Rejected(uiGridConstants, columnDefs);

                $scope.searchResultsGridOptions_Rejected.onRegisterApi = function (gridApi_Rejected) {

                    $scope.searchResultsGridApi_Rejected = gridApi_Rejected;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Rejected.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Rejected.selection.getSelectedRows();
                        //console.log(rows)
                        $scope.selecteddata_Rejected = [];
                        // $scope.selecteddata1.push(row);
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Rejected.push(item.email_domain_map_key);
                        });

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
                var _previousResults_Rejected = LocatorDomainDataServices.getSearchResultsData_Rejected();



                if (_previousResults_Rejected.length > 0) {

                    $scope.searchResultsGridOptions_Rejected == LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, _previousResults_Rejected);
                    $scope.totalItems_Rejected = _previousResults_Rejected.length;
                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Rejected()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_Rejected();

                        $scope.searchResultsGridOptions_Rejected = LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, _previousResults);
                        $scope.totalItems_Rejected = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "WaitingforApproval") {

                var columnDefs = LocatorDomainDataServices.getSearchResultsColumnDef_WFA(uiGridConstants);
                $scope.searchResultsGridOptions_WFA = LocatorDomainDataServices.getSearchGridOptions_WFA(uiGridConstants, columnDefs);

                $scope.searchResultsGridOptions_WFA.onRegisterApi = function (gridApi_WFA) {

                    $scope.searchResultsGridApi_WFA = gridApi_WFA;
                    //this is for selection of results


                    $scope.searchResultsGridApi_WFA.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_WFA.selection.getSelectedRows();

                        $scope.selecteddata_WFA = [];
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_WFA.push(item.email_domain_map_key);
                        });
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
                var _previousResults_WFA = LocatorDomainDataServices.getSearchResultsData_WFA();



                if (_previousResults_WFA.length > 0) {

                    $scope.searchResultsGridOptions_WFA == LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, _previousResults_WFA);
                    $scope.totalItems_WFA = _previousResults_WFA.length;
                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_WFA()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_WFA();

                        $scope.searchResultsGridOptions_WFA = LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, _previousResults);
                        $scope.totalItems_WFA = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "Reverted") {
                var columnDefs = LocatorDomainDataServices.getSearchResultsColumnDef_Reverted(uiGridConstants);
                $scope.searchResultsGridOptions_Reverted = LocatorDomainDataServices.getSearchGridOptions_Reverted(uiGridConstants, columnDefs);

                $scope.searchResultsGridOptions_Reverted.onRegisterApi = function (gridApi_Reverted) {

                    $scope.searchResultsGridApi_Reverted = gridApi_Reverted;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Reverted.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Reverted.selection.getSelectedRows();
                        //console.log(rows)
                        $scope.selecteddata_Reverted = [];

                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Reverted.push(item.email_domain_map_key);

                        });

                        angular.forEach(rows, function (k, v) {
                            {


                                if (k.$$hashKey != null) {
                                    k.IndexString = k.$$hashKey;
                                    delete k.$$hashKey;
                                }
                            }
                        });
                        //console.log($scope.datarow);
                    });
                }
                var _previousResults_Reverted = LocatorDomainDataServices.getSearchResultsData_Reverted();



                if (_previousResults_Reverted.length > 0) {

                    $scope.searchResultsGridOptions_Reverted == LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, _previousResults_Reverted);
                    $scope.totalItems_Reverted = _previousResults_Reverted.length;
                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Reverted()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_Reverted();

                        $scope.searchResultsGridOptions_Reverted = LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, _previousResults);
                        $scope.totalItems_Reverted = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "All") {

                var columnDefs_Approved = LocatorDomainDataServices.getSearchResultsColumnDef_Approved(uiGridConstants);
                $scope.searchResultsGridOptions_Approved = LocatorDomainDataServices.getSearchGridOptions_Approved(uiGridConstants, columnDefs_Approved);

                $scope.searchResultsGridOptions_Approved.onRegisterApi = function (gridApi_Approved) {

                    $scope.searchResultsGridApi_Approved = gridApi_Approved;
                    //this is for selection of results
                    $scope.searchResultsGridApi_Approved.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Approved.selection.getSelectedRows();
                        //console.log(rows);
                        $scope.selecteddata_Approved = [];
                        // $scope.selecteddata1.push(row);
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Approved.push(item.email_domain_map_key);
                        });
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
                var _previousResults_Approved = LocatorDomainDataServices.getSearchResultsData_Approved();

                if (_previousResults_Approved.length > 0) {

                    $scope.searchResultsGridOptions_Approved = LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, _previousResults_Approved);
                    $scope.totalItems_Approved = _previousResults_Approved.length;
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainApproved = BASE_URL + '/DomainApproved.tpl.html';
                    // console.log($scope.LocatorDomainTemplate_Approved)
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Approved()) {

                        //Get back the last search result     
                        previousResults_Approved = mainService.getLastLocatorDomainSearchResult_Approved();

                        $scope.searchResultsGridOptions_Approved = LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, previousResults_Approved);
                        $scope.totalItems_Approved = previousResults_Approved.length;
                        $scope.currentPage = 1;
                        $scope.DomainApproved = BASE_URL + '/DomainApproved.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }

                //MULTIPLE GRID BIND 


                var columnDefs_Rejected = LocatorDomainDataServices.getSearchResultsColumnDef_Rejected(uiGridConstants);
                $scope.searchResultsGridOptions_Rejected = LocatorDomainDataServices.getSearchGridOptions_Rejected(uiGridConstants, columnDefs_Rejected);
                $scope.searchResultsGridOptions_Rejected.onRegisterApi = function (gridApi_Rejected) {

                    $scope.searchResultsGridApi_Rejected = gridApi_Rejected;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Rejected.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Rejected.selection.getSelectedRows();

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
                var _previousResults_Rejected = LocatorDomainDataServices.getSearchResultsData_Rejected();
                if (_previousResults_Rejected.length > 0) {

                    $scope.searchResultsGridOptions_Rejected = LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, _previousResults_Rejected);
                    $scope.totalItems_Rejected = _previousResults_Rejected.length;
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainRejected = BASE_URL + '/DomainRejected.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Rejected()) {

                        //Get back the last search result     
                        previousResults_Rejected = mainService.getLastLocatorDomainSearchResult_Rejected();

                        $scope.searchResultsGridOptions_Rejected = LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, previousResults_Rejected);
                        $scope.totalItems_Rejected = previousResults_Rejected.length;
                        $scope.currentPage = 1;
                        $scope.DomainRejected = BASE_URL + '/DomainRejected.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }


                var columnDefs_WFA = LocatorDomainDataServices.getSearchResultsColumnDef_WFA(uiGridConstants);
                $scope.searchResultsGridOptions_WFA = LocatorDomainDataServices.getSearchGridOptions_WFA(uiGridConstants, columnDefs_WFA);
                $scope.searchResultsGridOptions_WFA.onRegisterApi = function (gridApi_WFA) {

                    $scope.searchResultsGridApi_WFA = gridApi_WFA;
                    //this is for selection of results


                    $scope.searchResultsGridApi_WFA.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;
                        rows = $scope.searchResultsGridApi_WFA.selection.getSelectedRows();
                        //console.log(rows)
                        $scope.selecteddata_WFA = [];
                        // $scope.selecteddata1.push(row);
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_WFA.push(item.email_domain_map_key);
                        });
                        angular.forEach(rows, function (k, v) {
                            {
                                if (k.$$hashKey != null) {
                                    k.IndexString = k.$$hashKey;
                                    delete k.$$hashKey;
                                }
                            }
                        });
                        //console.log(rows);
                    });
                }
                var _previousResults_WFA = LocatorDomainDataServices.getSearchResultsData_WFA();
                if (_previousResults_WFA.length > 0) {

                    $scope.searchResultsGridOptions_WFA = LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, _previousResults_WFA);
                    $scope.totalItems_WFA = _previousResults_WFA.length;
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainWaiting = BASE_URL + '/DomainWaitingforapproval.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_WFA()) {

                        //Get back the last search result     
                        previousResults_WFA = mainService.getLastLocatorDomainSearchResult_WFA();

                        $scope.searchResultsGridOptions_WFA = LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, previousResults_WFA);
                        $scope.totalItems_WFA = previousResults_WFA.length;
                        $scope.currentPage = 1;
                        $scope.DomainWaiting = BASE_URL + '/DomainWaitingforapproval.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }

                var columnDefs_Reverted = LocatorDomainDataServices.getSearchResultsColumnDef_Reverted(uiGridConstants);
                $scope.searchResultsGridOptions_Reverted = LocatorDomainDataServices.getSearchGridOptions_Reverted(uiGridConstants, columnDefs_WFA);
                $scope.searchResultsGridOptions_Reverted.onRegisterApi = function (gridApi_Reverted) {

                    $scope.searchResultsGridApi_Reverted = gridApi_Reverted;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Reverted.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Reverted.selection.getSelectedRows();

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
                var _previousResults_Reverted = LocatorDomainDataServices.getSearchResultsData_Reverted();
                if (_previousResults_Reverted.length > 0) {

                    $scope.searchResultsGridOptions_Reverted = LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, _previousResults_Reverted);
                    $scope.totalItems_Reverted = _previousResults_Reverted.length;
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainReverted = BASE_URL + '/DomainReverted.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Reverted()) {

                        //Get back the last search result     
                        previousResults_Reverted = mainService.getLastLocatorDomainSearchResult_Reverted();
                        $scope.searchResultsGridOptions_Reverted = LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, previousResults_Reverted);
                        $scope.totalItems_Reverted = previousResults_Reverted.length;
                        $scope.currentPage = 1;
                        $scope.DomainReverted = BASE_URL + '/DomainReverted.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        // $state.go('locator.domain', {});
                    }

                }

                $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
            }
            
        }
        

        inialize();

        $scope.pageChanged_Approved = function (page) {
            $scope.searchResultsGridApi_Approved.pagination.seek(page);
        };
       
        $scope.pageChanged_WFA = function (page) {
            $scope.searchResultsGridApi_WFA.pagination.seek(page);
        };

        $scope.pageChanged_Rejected = function (page) {
            $scope.searchResultsGridApi_Rejected.pagination.seek(page);
        };

        $scope.pageChanged_Reverted = function (page) {
            $scope.searchResultsGridApi_Reverted.pagination.seek(page);
        };
        var postParams = { "LocatorDomainInputSearchModel": [] };


        //Approve button Event
        $scope.Approved = function () {
            $scope.pleaseWait = { "display": "block" };
            var data = "";
            data = $scope.selecteddata_WFA;

            if (data.length > 0) {
                for (var i = 0; i < data.length; ++i) {
                    var searchParams = { "email_domain_map_key": data[i], "status": "Approved" };
                    postParams["LocatorDomainInputSearchModel"].push(searchParams);
                }
               
                LocatorDomainServices.getLocatorDomainRollback(postParams).success(function (result) {
                    GetUpdatedData();
                    trans_key = result[0].o_transaction_key;
                    //console.log(trans_key)
                    messagePopup($rootScope, "The transaction key associated with this request is "+ trans_key +"", "The selected records were updated succesfully.");
                                       

                }).error(function (result) {
                    $scope.pleaseWait = { "display": "none" };
                                      
                    if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        //$state.go('locator.domain.results', {});
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                            }
                    else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        // $state.go('locator.domain.results', {});
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                        //console.log(result)
                        

                    }
                    
                });
                
                postParams = { "LocatorDomainInputSearchModel": [] }
                $scope.selecteddata_WFA = [];
            }
            else {
                messagePopup($rootScope, "Please select the records", "");
               
            }
        };

        //Rollback button Event
        $scope.Rollback = function () {           
            $scope.pleaseWait = { "display": "block" };
            var data = $scope.selecteddata_Approved;
            
            if (data.length > 0 ) {
                for (var i = 0; i < data.length; ++i)
                {
                    var searchParams = { "email_domain_map_key": data[i], "status": "Reverted" };
                    postParams["LocatorDomainInputSearchModel"].push(searchParams);
                }
                
                LocatorDomainServices.getLocatorDomainRollback(postParams).success(function (result) {
                    GetUpdatedData();
                    trans_key = result[0].o_transaction_key;
                    //console.log(trans_key);                    
                    messagePopup($rootScope, "The transaction key associated with this request is " + trans_key + "", "The selected records were updated succesfully.");
                                        

                }).error(function (result) {
                    $scope.pleaseWait = { "display": "none" };

                    if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        //$state.go('locator.domain.results', {});
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        // $state.go('locator.domain.results', {});
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                        //console.log(result)


                    }

                });
             
                postParams = { "LocatorDomainInputSearchModel": [] }
                $scope.selecteddata_Approved = [];
            }
            else
            {
                messagePopup($rootScope, "Please select the records", "");

            }
        };

        //Reject button Event
        $scope.Rejected = function () {
            $scope.pleaseWait = { "display": "block" };
      
            $state.go('locator.domain.results', {});
            var data = "";
            data = $scope.selecteddata_WFA;
            //console.log(data)
            if (data.length > 0) {
                for (var i = 0; i < data.length; ++i) {
                    var searchParams = { "email_domain_map_key": data[i], "status": "Rejected" };
                    postParams["LocatorDomainInputSearchModel"].push(searchParams);
                }
                //console.log(postParams)
                LocatorDomainServices.getLocatorDomainRollback(postParams).success(function (result) {
                    GetUpdatedData();
                    trans_key = result[0].o_transaction_key;
                    //console.log(trans_key)
                    messagePopup($rootScope, "The transaction key associated with this request is " + trans_key + "", "The selected records were updated succesfully.");

                    

                }).error(function (result) {
                    $scope.pleaseWait = { "display": "none" };

                    if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        //$state.go('locator.domain.results', {});
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        // $state.go('locator.domain.results', {});
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                        //console.log(result)


                    }

                });
               
                postParams = { "LocatorDomainInputSearchModel": [] }
                $scope.selecteddata_WFA = [];
            }
            else {
                messagePopup($rootScope, "Please select the records", "");

            }
        };

        //filter for search results
        $scope.toggleSearchFilter = function () {
            $scope.searchResultsGridOptions_Approved.enableFiltering = !$scope.searchResultsGridOptions_Approved.enableFiltering;
            $scope.searchResultsGridApi_Approved.core.notifyDataChange(uiGridConstants.dataChange.ALL);

            $scope.searchResultsGridOptions_WFA.enableFiltering = !$scope.searchResultsGridOptions_WFA.enableFiltering;
            $scope.searchResultsGridApi_WFA.core.notifyDataChange(uiGridConstants.dataChange.ALL);

            $scope.searchResultsGridOptions_Rejected.enableFiltering = !$scope.searchResultsGridOptions_Rejected.enableFiltering;
            $scope.searchResultsGridApi_Rejected.core.notifyDataChange(uiGridConstants.dataChange.ALL);

            $scope.searchResultsGridOptions_Reverted.enableFiltering = !$scope.searchResultsGridOptions_Reverted.enableFiltering;
            $scope.searchResultsGridApi_Reverted.core.notifyDataChange(uiGridConstants.dataChange.ALL);

        }

        $scope.selecteddata = [];
        
        $scope.setGlobalValues = function (row) {
            
                       
        };        

        $scope.toggleSelection = function toggleSelection(row) {


            var data = $scope.selecteddata.indexOf(row);

            // is currently selected
            if (data > -1) {
                $scope.selecteddata.splice(data, 1);
            }
                // is newly selected
            else {
                $scope.selecteddata.push(row);
            }
            //console.log($scope.selecteddata)
           
        };

        var pageName = "SearchResults";

        $scope.commonResearchGridRow = function (row, grid)
        {
            commonResearchGridRow($scope, mainService, row, grid,  $uibModal, $stateParams, $scope.searchResultsGridOptions, LOCATOR_CONSTANTS.LOCATOR_RESULTS, $window);
        }


        // tab level security - to hide edit/delete buttons // added by srini
        $scope.tabSecurity = true;
        //if (LocatorDomainmultigridservice.gettabdenypermission()) {
        //    $scope.tabsecurity = false;
        //}
       

        var GetUpdatedData = function () {          

            var postParams_updated = { "LocatorDomainInputSearchModel": [] };
            var searchParams = { "LocValidDomain": $sessionStorage.validDomain, "LocInvalidDomain": $sessionStorage.InvalidDomain, "LocStatus": "All" };
            postParams_updated["LocatorDomainInputSearchModel"].push(searchParams);

            
            LocatorDomainServices.getLocatorDomainAdvSearchResults(postParams_updated).success(function (result) {

                
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
           
                BindGrid_data();
               
            });

        }
         
        var BindGrid_data = function ()
        {
            var page = $sessionStorage.Dropdownvalue;
            if (page == "Approved") {
                var columnDefs_Approved = LocatorDomainDataServices.getSearchResultsColumnDef_Approved(uiGridConstants);
                $scope.searchResultsGridOptions_Approved = LocatorDomainDataServices.getSearchGridOptions_Approved(uiGridConstants, columnDefs_Approved);

                $scope.searchResultsGridOptions_Approved.onRegisterApi = function (gridApi_Approved) {

                    $scope.searchResultsGridApi_Approved = gridApi_Approved;
                    //this is for selection of results
                    $scope.searchResultsGridApi_Approved.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;
                        rows = $scope.searchResultsGridApi_Approved.selection.getSelectedRows();
                        $scope.selecteddata_Approved = [];
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Approved.push(item.email_domain_map_key);
                        });

                        angular.forEach(rows, function (k, v) {
                            {
                                if (k.$$hashKey != null) {
                                    k.IndexString = k.$$hashKey;
                                    delete k.$$hashKey;
                                }
                            }
                        });
                        //console.log(rows);
                        //console.log($scope.selecteddata_Approved)
                    });
                }
                var _previousResults_Approved = LocatorDomainDataServices.getSearchResultsData_Approved();



                if (_previousResults_Approved.length > 0) {

                    $scope.searchResultsGridOptions_Approved == LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, _previousResults_Approved);
                    $scope.totalItems_Approved = _previousResults_Approved.length;

                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    // console.log($scope.LocatorDomainTemplate_Approved)

                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Approved()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_Approved();
                        //console.log(_previousResults);
                        $scope.searchResultsGridOptions_Approved = LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, _previousResults);
                        $scope.totalItems_Approved = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "Rejected") {
                var columnDefs = LocatorDomainDataServices.getSearchResultsColumnDef_Rejected(uiGridConstants);
                $scope.searchResultsGridOptions_Rejected = LocatorDomainDataServices.getSearchGridOptions_Rejected(uiGridConstants, columnDefs);

                $scope.searchResultsGridOptions_Rejected.onRegisterApi = function (gridApi_Rejected) {

                    $scope.searchResultsGridApi_Rejected = gridApi_Rejected;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Rejected.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Rejected.selection.getSelectedRows();
                        //console.log(rows)
                        $scope.selecteddata_Rejected = [];
                        // $scope.selecteddata1.push(row);
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Rejected.push(item.email_domain_map_key);
                        });

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
                var _previousResults_Rejected = LocatorDomainDataServices.getSearchResultsData_Rejected();



                if (_previousResults_Rejected.length > 0) {

                    $scope.searchResultsGridOptions_Rejected == LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, _previousResults_Rejected);
                    $scope.totalItems_Rejected = _previousResults_Rejected.length;
                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Rejected()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_Rejected();

                        $scope.searchResultsGridOptions_Rejected = LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, _previousResults);
                        $scope.totalItems_Rejected = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "WaitingforApproval") {

                var columnDefs = LocatorDomainDataServices.getSearchResultsColumnDef_WFA(uiGridConstants);
                $scope.searchResultsGridOptions_WFA = LocatorDomainDataServices.getSearchGridOptions_WFA(uiGridConstants, columnDefs);

                $scope.searchResultsGridOptions_WFA.onRegisterApi = function (gridApi_WFA) {

                    $scope.searchResultsGridApi_WFA = gridApi_WFA;
                    //this is for selection of results


                    $scope.searchResultsGridApi_WFA.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_WFA.selection.getSelectedRows();

                        $scope.selecteddata_WFA = [];
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_WFA.push(item.email_domain_map_key);
                        });
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
                var _previousResults_WFA = LocatorDomainDataServices.getSearchResultsData_WFA();



                if (_previousResults_WFA.length > 0) {

                    $scope.searchResultsGridOptions_WFA == LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, _previousResults_WFA);
                    $scope.totalItems_WFA = _previousResults_WFA.length;
                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_WFA()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_WFA();

                        $scope.searchResultsGridOptions_WFA = LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, _previousResults);
                        $scope.totalItems_WFA = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "Reverted") {
                var columnDefs = LocatorDomainDataServices.getSearchResultsColumnDef_Reverted(uiGridConstants);
                $scope.searchResultsGridOptions_Reverted = LocatorDomainDataServices.getSearchGridOptions_Reverted(uiGridConstants, columnDefs);

                $scope.searchResultsGridOptions_Reverted.onRegisterApi = function (gridApi_Reverted) {

                    $scope.searchResultsGridApi_Reverted = gridApi_Reverted;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Reverted.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Reverted.selection.getSelectedRows();
                        //console.log(rows)
                        $scope.selecteddata_Reverted = [];

                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Reverted.push(item.email_domain_map_key);

                        });

                        angular.forEach(rows, function (k, v) {
                            {


                                if (k.$$hashKey != null) {
                                    k.IndexString = k.$$hashKey;
                                    delete k.$$hashKey;
                                }
                            }
                        });
                        //console.log($scope.datarow);
                    });
                }
                var _previousResults_Reverted = LocatorDomainDataServices.getSearchResultsData_Reverted();



                if (_previousResults_Reverted.length > 0) {

                    $scope.searchResultsGridOptions_Reverted == LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, _previousResults_Reverted);
                    $scope.totalItems_Reverted = _previousResults_Reverted.length;
                    $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Reverted()) {

                        //Get back the last search result     
                        _previousResults = mainService.getLastLocatorDomainSearchResult_Reverted();

                        $scope.searchResultsGridOptions_Reverted = LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, _previousResults);
                        $scope.totalItems_Reverted = _previousResults.length;
                        $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
                        $scope.currentPage = 1;
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }
            }
            else if (page == "All") {
                
                var columnDefs_Approved = LocatorDomainDataServices.getSearchResultsColumnDef_Approved(uiGridConstants);
                $scope.searchResultsGridOptions_Approved = LocatorDomainDataServices.getSearchGridOptions_Approved(uiGridConstants, columnDefs_Approved);

                $scope.searchResultsGridOptions_Approved.onRegisterApi = function (gridApi_Approved) {

                    $scope.searchResultsGridApi_Approved = gridApi_Approved;
                    //this is for selection of results
                    $scope.searchResultsGridApi_Approved.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Approved.selection.getSelectedRows();
                        //console.log(rows);
                        $scope.selecteddata_Approved = [];
                        // $scope.selecteddata1.push(row);
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_Approved.push(item.email_domain_map_key);
                        });
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
                var _previousResults_Approved = LocatorDomainDataServices.getSearchResultsData_Approved();

                if (_previousResults_Approved.length > 0) {

                    $scope.searchResultsGridOptions_Approved = LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, _previousResults_Approved);
                    $scope.totalItems_Approved = _previousResults_Approved.length;
                   // console.log($scope.totalItems_Approved)
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainApproved = BASE_URL + '/DomainApproved.tpl.html';
                    // console.log($scope.LocatorDomainTemplate_Approved)
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Approved()) {

                        //Get back the last search result     
                        previousResults_Approved = mainService.getLastLocatorDomainSearchResult_Approved();

                        $scope.searchResultsGridOptions_Approved = LocatorDomainServices.getSearchResultsGridLayout_Approved($scope.searchResultsGridOptions_Approved, uiGridConstants, previousResults_Approved);
                        $scope.totalItems_Approved = previousResults_Approved.length;
                        $scope.currentPage = 1;
                        $scope.DomainApproved = BASE_URL + '/DomainApproved.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }

                //MULTIPLE GRID BIND 


                var columnDefs_Rejected = LocatorDomainDataServices.getSearchResultsColumnDef_Rejected(uiGridConstants);
                $scope.searchResultsGridOptions_Rejected = LocatorDomainDataServices.getSearchGridOptions_Rejected(uiGridConstants, columnDefs_Rejected);
                $scope.searchResultsGridOptions_Rejected.onRegisterApi = function (gridApi_Rejected) {

                    $scope.searchResultsGridApi_Rejected = gridApi_Rejected;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Rejected.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Rejected.selection.getSelectedRows();

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
                var _previousResults_Rejected = LocatorDomainDataServices.getSearchResultsData_Rejected();
                if (_previousResults_Rejected.length > 0) {

                    $scope.searchResultsGridOptions_Rejected = LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, _previousResults_Rejected);
                    $scope.totalItems_Rejected = _previousResults_Rejected.length;
                    //console.log($scope.totalItems_Rejected)
                    $scope.DomainRejected = BASE_URL + '/DomainRejected.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Rejected()) {

                        //Get back the last search result     
                        previousResults_Rejected = mainService.getLastLocatorDomainSearchResult_Rejected();

                        $scope.searchResultsGridOptions_Rejected = LocatorDomainServices.getSearchResultsGridLayout_Rejected($scope.searchResultsGridOptions_Rejected, uiGridConstants, previousResults_Rejected);
                        $scope.totalItems_Rejected = previousResults_Rejected.length;
                        $scope.currentPage = 1;
                        $scope.DomainRejected = BASE_URL + '/DomainRejected.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }


                var columnDefs_WFA = LocatorDomainDataServices.getSearchResultsColumnDef_WFA(uiGridConstants);
                $scope.searchResultsGridOptions_WFA = LocatorDomainDataServices.getSearchGridOptions_WFA(uiGridConstants, columnDefs_WFA);
                $scope.searchResultsGridOptions_WFA.onRegisterApi = function (gridApi_WFA) {

                    $scope.searchResultsGridApi_WFA = gridApi_WFA;
                    //this is for selection of results


                    $scope.searchResultsGridApi_WFA.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;
                        rows = $scope.searchResultsGridApi_WFA.selection.getSelectedRows();
                        //console.log(rows)
                        $scope.selecteddata_WFA = [];
                        // $scope.selecteddata1.push(row);
                        angular.forEach(rows, function (item) {
                            $scope.selecteddata_WFA.push(item.email_domain_map_key);
                        });
                        angular.forEach(rows, function (k, v) {
                            {
                                if (k.$$hashKey != null) {
                                    k.IndexString = k.$$hashKey;
                                    delete k.$$hashKey;
                                }
                            }
                        });
                        //console.log(rows);
                    });
                }
                var _previousResults_WFA = LocatorDomainDataServices.getSearchResultsData_WFA();
                if (_previousResults_WFA.length > 0) {

                    $scope.searchResultsGridOptions_WFA = LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, _previousResults_WFA);
                    $scope.totalItems_WFA = _previousResults_WFA.length;
                   // console.log($scope.totalItems_WFA)
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainWaiting = BASE_URL + '/DomainWaitingforapproval.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_WFA()) {

                        //Get back the last search result     
                        previousResults_WFA = mainService.getLastLocatorDomainSearchResult_WFA();

                        $scope.searchResultsGridOptions_WFA = LocatorDomainServices.getSearchResultsGridLayout_WFA($scope.searchResultsGridOptions_WFA, uiGridConstants, previousResults_WFA);
                        $scope.totalItems_WFA = previousResults_WFA.length;
                        $scope.currentPage = 1;
                        $scope.DomainWaiting = BASE_URL + '/DomainWaitingforapproval.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $state.go('locator.domain', {});
                    }

                }

                var columnDefs_Reverted = LocatorDomainDataServices.getSearchResultsColumnDef_Reverted(uiGridConstants);
                $scope.searchResultsGridOptions_Reverted = LocatorDomainDataServices.getSearchGridOptions_Reverted(uiGridConstants, columnDefs_WFA);
                $scope.searchResultsGridOptions_Reverted.onRegisterApi = function (gridApi_Reverted) {

                    $scope.searchResultsGridApi_Reverted = gridApi_Reverted;
                    //this is for selection of results

                    $scope.searchResultsGridApi_Reverted.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;

                        rows = $scope.searchResultsGridApi_Reverted.selection.getSelectedRows();

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
                var _previousResults_Reverted = LocatorDomainDataServices.getSearchResultsData_Reverted();
                if (_previousResults_Reverted.length > 0) {

                    $scope.searchResultsGridOptions_Reverted = LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, _previousResults_Reverted);
                    $scope.totalItems_Reverted = _previousResults_Reverted.length;
                    //console.log($scope.totalItems_Reverted)
                    var page = $sessionStorage.Dropdownvalue;
                    $scope.DomainReverted = BASE_URL + '/DomainReverted.tpl.html';
                    $scope.currentPage = 1;
                    $scope.toggleShowResults = { "display": "block" };
                    $scope.toggleHeader = { "display": "block" };
                    $scope.pleaseWait = { "display": "none" };
                }
                else {
                    if (mainService.getLastLocatorDomainSearchResult_Reverted()) {

                        //Get back the last search result     
                        previousResults_Reverted = mainService.getLastLocatorDomainSearchResult_Reverted();
                        $scope.searchResultsGridOptions_Reverted = LocatorDomainServices.getSearchResultsGridLayout_Reverted($scope.searchResultsGridOptions_Reverted, uiGridConstants, previousResults_Reverted);
                        $scope.totalItems_Reverted = previousResults_Reverted.length;
                        $scope.currentPage = 1;
                        $scope.DomainReverted = BASE_URL + '/DomainReverted.tpl.html';
                        $scope.toggleShowResults = { "display": "block" };
                        $scope.toggleHeader = { "display": "block" };
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        // $state.go('locator.domain', {});
                    }

                }

                $scope.LocatorDomainTemplate_Approved = BASE_URL + '/' + 'Domain' + page + '.tpl.html';
            }
        }
    }]);



function modalMessage($rootScope, result) {
    //console.log(result.finalMessage);
    $rootScope.FinalMessage = result.finalMessage;
    //console.log($rootScope.FinalMessage);
    //$rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}



function messagePopup($rootScope, message, header) {
    $rootScope.FinalMessage = message;
    $rootScope.ConfirmationMessage = header;
    $rootScope.ReasonOrTransKey = '';
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}
