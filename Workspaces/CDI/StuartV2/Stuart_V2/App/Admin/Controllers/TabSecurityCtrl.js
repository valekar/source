angular.module('admin').controller('TabSecurityCtrl', ['$scope', 'TabSecurityGridServices', '$window', 'uiGridConstants', 'TabSecurityApiServices', 'mainService', 
    'ErrorService', '$rootScope', 'TABSECURITY_CONTANTS','ModalService',
    function ($scope, TabSecurityGridServices, $window, uiGridConstants, TabSecurityApiServices, mainService,  ErrorService, $rootScope, TABSECURITY_CONTANTS, ModalService) {

        var inialize = function () {
            $scope.tabSecurity = {
                toggleShowResults: false,
                togglePleaseWait: true,
                columnDefs: TabSecurityGridServices.getColumnDef(),
                _previousResults: [],
                currentPage: 1
            }


            $scope.tabSecurity.gridOptions = TabSecurityGridServices.getGridOptions($scope.tabSecurity.columnDefs, false);

            $scope.tabSecurity.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.tabSecurity.gridApi = gridApi;
                //this is for selection of results
               /* $scope.tabSecurity.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    var msg = 'row selected ' + row.isSelected;

                    rows = $scope.tabSecurity.gridApi.selection.getSelectedRows();

                    angular.forEach(rows, function (k, v) {
                        {
                            if (k.$$hashKey != null) {
                                k.IndexString = k.$$hashKey;
                                delete k.$$hashKey;
                            }
                        }
                    });
                });*/
            }

            popupateValues();

        }
        inialize();

        $scope.pageChanged = function (page) {
            $scope.tabSecurity.gridApi.pagination.seek(page);
        };

        //filter for search results
        $rootScope.toggleSearchFilter = function () {
            $scope.tabSecurity.gridOptions.enableFiltering = !$scope.tabSecurity.gridOptions.enableFiltering;
            $scope.tabSecurity.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }



        // edit the row
        $scope.editGridRow = function (row, grid) {
            console.log(row);
            ModalService.openModal(row, grid, TABSECURITY_CONTANTS.TAB_SECURITY_EDIT).then(function (output) {
                if (output == "Success") {
                    $scope.tabSecurity.toggleShowResults = false;
                    $scope.tabSecurity.togglePleaseWait = true;
                    popupateUpdatedValues();
                    $scope.tabSecurity.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                }
            });
        }

        $scope.deleteGridRow = function (row, grid) {
            console.log(row);
            ModalService.openModal(row, grid, TABSECURITY_CONTANTS.TAB_SECURITY_DELETE).then(function (output) {
                if (output == "Success") {
                    $scope.tabSecurity.toggleShowResults = false;
                    $scope.tabSecurity.togglePleaseWait = true;
                    popupateUpdatedValues();
                    $scope.tabSecurity.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                }
            });
        }

        $scope.tabSecurity.insert = function () {
            var row = null;
            var grid = null;
            ModalService.openModal(row, grid, TABSECURITY_CONTANTS.TAB_SECURITY_INSERT).then(function (output) {
                if (output == "Success") {
                    $scope.tabSecurity.toggleShowResults = false;
                    $scope.tabSecurity.togglePleaseWait = true;
                    popupateUpdatedValues();
                    $scope.tabSecurity.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                }
            });
        }

        function popupateValues() {
            TabSecurityApiServices.getTabSecurityGetResults().then(function (result) {

                $scope.tabSecurity._previousResults = result;
                if ($scope.tabSecurity._previousResults.length > 0) {
                    $scope.tabSecurity.gridOptions = TabSecurityGridServices.getGridLayout($scope.tabSecurity.gridOptions, $scope.tabSecurity._previousResults);
                    $scope.tabSecurity.totalItems = $scope.tabSecurity._previousResults.length;
                    $scope.tabSecurity.toggleShowResults = true;
                    $scope.tabSecurity.togglePleaseWait = false;
                }
            }, function (error) {
                $scope.tabSecurity.togglePleaseWait = false;
                ErrorService.messagePopup(error);
            });
        }
       

        function popupateUpdatedValues() {
            TabSecurityApiServices.getUpdatedTabSecurityGetResults().then(function (result) {
                $scope.tabSecurity._previousResults = result;
                if ($scope.tabSecurity._previousResults.length > 0) {
                    $scope.tabSecurity.gridOptions = TabSecurityGridServices.getGridLayout($scope.tabSecurity.gridOptions, $scope.tabSecurity._previousResults);
                    $scope.tabSecurity.totalItems = $scope.tabSecurity._previousResults.length;
                    $scope.tabSecurity.toggleShowResults = true;
                    $scope.tabSecurity.togglePleaseWait = false;
                }

              
                    mainService.getUserPermissions().then(function (result) {
                        if (result.data) {
                            console.log(result.data);
                            $window.localStorage.removeItem('Main-userPermissions');
                            $window.localStorage.setItem('Main-userPermissions', JSON.stringify(result.data));
                        }
                    });
                


            }, function (error) {
                $scope.tabSecurity.togglePleaseWait = false;
                ErrorService.messagePopup(error);
            });
        }


    }]);



