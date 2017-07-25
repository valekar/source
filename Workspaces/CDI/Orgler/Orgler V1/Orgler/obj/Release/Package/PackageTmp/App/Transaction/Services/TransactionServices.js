angular.module('transaction').factory('TransactionServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var sharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");

    return {
        CreateTransaction: function (postTransactionSearchParams) {
            return $http.post(BasePath + "TransactionNative/CreateTransaction", postTransactionSearchParams, {
                 headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
               // console.log(result);
                return result.data;
            }).error(function (result) {
                if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        },
        getTransactionAdvSearchResults: function (postTransactionSearchParams) {
            return $http.post(BasePath + "TransactionNative/AdvSearch", postTransactionSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                $http.post(BasePath + "Home/WriteTransactionRecentSearches", postTransactionSearchParams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                });
              //  console.log(result);
                return result.data;
            }).error(function (result) {
                if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        },
        getTransSearchResultsGridLayout: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = false;

            gridOptions.data = datas;
          //  console.log("Grid Data ");
          //  console.log(datas);
         //   console.log(gridOptions.data);
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            return gridOptions;
        },

        getTransactionRecentSearches: function () {
            return $http.get(BasePath + "Home/GetTransactionRecentSearches",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                    if (result == "" || result == null) {
                      //  console.log("Failed to get Transaction Recent Searches");
                    }
                    else {
                      //  console.log(result);
                        return result;
                    }
                }).error(function (result) {
                    if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                });
        },

        uploadCaseFiles: function (formdata) {
            var request = {

                url: BasePath + 'caseNative/UploadFiles/',

            };

            // SEND THE FILES.

            if (formdata != null || formdata != undefined) {
                return $http.post(request.url, formdata, {
                    headers: {
                        'Content-Type': undefined
                    }
                }).then(function (result) {
                    return result.data;
                });

            }

        },
       
        clearSearchParams: function ($scope, maxSearchPanelCount, defaultOpenPanelsCount, appendString) {

            for (i = 1; i <= maxSearchPanelCount; i++) {
                $scope.TransParams.transactionNumber[appendString + i] = "";
                $scope.TransParams.transactionType[appendString + i] = "";
                $scope.TransParams.transactionStatus[appendString + i] = "";
                $scope.TransParams.transactionMasterId[appendString + i] = "";
                $scope.TransParams.transactionDateFrom[appendString + i] = null;
                $scope.TransParams.transactionDateTo[appendString + i] = null;
                $scope.TransParams.transactionUserName[appendString + i] = "";
                $scope.TransParams.SearchMeChkbx[appendString + i] = "";
                $scope.TransParams.transUsernameState[appendString + i] = false;

            }

            $scope.SearchRows = defaultOpenPanelsCount;
            for (i = 1; i <= $scope.SearchRows; i++) {

                $scope.TransParams.SearchPanel[appendString + i] = true;
                $scope.TransParams.PanelSeparator[appendString + i] = true;
                $scope.TransParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.TransParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }

            for (i = $scope.SearchRows + 1; i <= maxSearchPanelCount ; i++) {
                $scope.TransParams.SearchPanel[appendString + i] = false;
                $scope.TransParams.PanelSeparator[appendString + i] = false;
                $scope.TransParams.showCloseButton[appendString + i] = false;

            }
        }
    }
}]);

//used in transaction helper mm/dd/yyyy timestamp columns
angular.module('transaction').filter("column_date_filter", ['$filter', function ($filter) {
    return function (value) {
        if (value != null)
            return $filter('date')(new Date(value), 'MM/dd/yyyy h:MM:ss a');
        else
            return null;
    }
}]);

//used in transaction helper mm/dd/yyyy date columns
angular.module('transaction').filter("date_only_filter", ['$filter', function ($filter) {
    return function (value) {
        if (value != null)
            return $filter('date')(new Date(value), 'MM/dd/yyyy');
        else
            return null;
    }
}]);

angular.module('transaction').factory('TransactionDataServices', ['$http', '$rootScope', '$filter', '$window', function ($http, $rootScope, $filter, $window) {
    var searchTransResultsData = [];
    var searchTransParams;
    var detailsTransParameters;
    var transMultiParams;

    return {
        setTransSearchResultsData: function (results) {
            searchTransResultsData = results;
        },

        setTransSearchDetailsParameters: function (detailsParams) {
            detailsTransParameters = detailsParams;
        },

        getTransSearchDetailsParameters: function () {
            return detailsTransParameters;
        },

        getTransSearchResultsData: function () {
            return searchTransResultsData;
        },

        clearTransSearchData: function () {
            searchTransResultsData = [];
        },

        getTransSearchResultsColumnDef: function () {

           // console.log("Into Column Defs");

            var GRID_HEADER_TEMPLATE = '<div ng-class="{ \'sortable\': sortable }" tooltip-placement="top-left" tooltip-append-to-body="true" uib-tooltip="{{col.displayName}}">' +
                        '<div class="ui-grid-cell-contents " col-index="renderIndex" title="TOOLTIP">' +
                            '<span class="ui-grid-header-cell-label">{{ col.displayName CUSTOM_FILTERS }}</span>' +
                            '<span ui-grid-visible="col.sort.direction" ng-class="{ \'ui-grid-icon-up-dir\': col.sort.direction == asc, \'ui-grid-icon-down-dir\': col.sort.direction == desc, \'ui-grid-icon-blank\': !col.sort.direction }">&nbsp</span>' +
                        '</div>' +
                        '<div class="ui-grid-column-menu-button" ng-if="grid.options.enableColumnMenus && !col.isRowHeader  && col.colDef.enableColumnMenu !== false" ng-click="toggleMenu($event)" ng-class="{\'ui-grid-column-menu-button-last-col\': isLastCol}">' +
                            '<i class="ui-grid-icon-angle-down">&nbsp;</i>' +
                        '</div>' +
                        '<div ui-grid-filter></div>' +
                    '</div>';


            var GRID_FILTER_TEMPLATE = '<div >' +
                                            '<div class="ui-grid-cell-contents ">' +
                                                '&nbsp;' +
                                            '</div>' +
                                            '<div ng-click="grid.appScope.toggleFiltering(grid)" ng-if="grid.options.enableFiltering"><span class="glyphicon glyphicon-remove-circle" style="font-size:1.5em;margin-left:1%;"></span> </div>' +
                                        '</div>';

            


            var linkCellTemplate =
                         '<a class="text-center" ng-href="#" ng-click="grid.appScope.setGlobalValues(row.entity)">{{COL_FIELD}}</a>';
            return [

            {
                field: 'trans_key', displayName: 'Transaction Key', enableCellEdit: false,
                cellTemplate: linkCellTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                    if (grid.columns[0]) {
                        return 'first-col-style';
                    }
                }
            },
            { field: 'trans_typ_dsc', headerTooltip: 'Transaction Type Description', displayName: 'Transaction Type Description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
            { field: 'sub_trans_typ_dsc', displayName: 'Sub Transaction Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
            { field: 'transaction_type', displayName: 'Transaction Type', width: "*", minWidth: 300, maxWidth: 900, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
            { field: 'trans_stat', displayName: 'Transaction Status' },
            { field: 'sub_trans_actn_typ', displayName: 'Sub Transaction Action Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false },
            { field: 'trans_note', displayName: 'Transaction Note', visible: false },
            { field: 'user_id', displayName: 'DW User', visible: false },
            { field: 'trans_create_ts', displayName: 'Transaction Create Timestamp', visible: false, cellFilter: 'column_date_filter' },
            { field: 'trans_last_modified_ts', displayName: 'Last Modified Date', visible: false,  cellFilter: 'column_date_filter' },
            { field: 'case_key', displayName: 'Case Key', visible: true, cellTemplate: '<a class="text-center" ng-href="#" ng-click="grid.appScope.getEditCaseModal(row.entity.trans_key,row.entity.case_key)">{{COL_FIELD}}</a>' },
            { field: 'approved_by', displayName: 'Approved By', visible: false },
            { field: 'approved_dt', displayName: 'Approved Date', visible: false,  cellFilter: 'column_date_filter' },
            {
                field: 'Action', cellTemplate: 'App/Transaction/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                width: "*", minWidth: 100, maxWidth: 9000, headerCellTemplate: GRID_FILTER_TEMPLATE, visible: true
            }
            ]
        },



        getTransactionSearchParams: function ($scope,searchRowCount,appendString) {

            var postParams = { "TransactionSearchInputModel": [] };

            var searchParams;

            function returnNewParams() {
                searchParams = {
                    "TransactionKey": null,
                    "TransactionType": null,
                    "Status": null,
                    "MasterId": null,
                    "FromDate": null,
                    "ToDate": null,
                    "UserName": null,
                    "SubTransactionType": null
                };

                return searchParams;
            }

            returnNewParams();

            for (i = 1; i <= searchRowCount; i++) {
                if (!angular.isUndefined($scope.TransParams.transactionNumber[appendString + i]))
                    searchParams["TransactionKey"] = $scope.TransParams.transactionNumber[appendString + i];
                if (!angular.isUndefined($scope.TransParams.transactionType[appendString + i]) && $scope.TransParams.transactionType[appendString + i] != "" && $scope.TransParams.transactionType[appendString + i] != "null-null")
                    searchParams["TransactionType"] = ($scope.TransParams.transactionType[appendString + i].split("-"))[0];
                if (!angular.isUndefined($scope.TransParams.transactionStatus[appendString + i]))
                    searchParams["Status"] = $scope.TransParams.transactionStatus[appendString + i];
                if (!angular.isUndefined($scope.TransParams.transactionMasterId[appendString + i]))
                    searchParams["MasterId"] = $scope.TransParams.transactionMasterId[appendString + i];
                if (!angular.isUndefined($scope.TransParams.transactionDateFrom[appendString + i]))
                    searchParams["FromDate"] = $scope.TransParams.transactionDateFrom[appendString + i];
                if (!angular.isUndefined($scope.TransParams.transactionDateTo[appendString + i]))
                    searchParams["ToDate"] = $scope.TransParams.transactionDateTo[appendString + i];
                if (!angular.isUndefined($scope.TransParams.transactionUserName[appendString + i]))
                    searchParams["UserName"] = $scope.TransParams.transactionUserName[appendString + i];
                if (!angular.isUndefined($scope.TransParams.transactionType[appendString + i]) && $scope.TransParams.transactionType[appendString + i] != "" && $scope.TransParams.transactionType[appendString + i] != "null-null")
                    searchParams["SubTransactionType"] = ($scope.TransParams.transactionType[appendString + i].split("-"))[1];

                if ($scope.TransParams.transactionDateFrom[appendString + i]) {
                    searchParams["FromDate"] = $filter('date')(new Date($scope.TransParams.transactionDateFrom[appendString + i]), 'MM/dd/yyyy');
                }
                if ($scope.TransParams.transactionDateTo[appendString + i]) {
                    searchParams["ToDate"] = $filter('date')(new Date($scope.TransParams.transactionDateTo[appendString + i]), 'MM/dd/yyyy');
                }

                postParams["TransactionSearchInputModel"].push(searchParams);
                returnNewParams();
            }

          //  console.log("Post Params - ")
          //  console.log(postParams);
            return postParams;
        },

        setTransSearchParams: function (searchParam) {
            searchTransParams = searchParam;
        },

        getTransSavedSearchParams: function () {
            return searchTransParams;
        },

        setTransSearchObj: function (postJSON) {
            sharedPostParams = postJSON;
            // console.log(sharedPostParams);
        }, getTransSearchObj: function () {
            try {
                return sharedPostParams;
            }
            catch (err) {
                return null;
            }

        },

        setSavedTransMultiSearchParams: function (savedTransMultiParams) {
            transMultiParams = savedTransMultiParams;
        },

        getSavedTransMultiSearchParams: function() {
            return transMultiParams;
        },
      
        getTransSearchGridOptions: function (uiGridConstants, columnDefs) {

            var rowtpl = '<div ng-class="{\'greenClass\':true,\'darkGreenClass\':row.entity.trans_stat == \'Processed\',\'yellowWaitingforApprovalClass\':row.entity.trans_stat == \'Waiting for approval\', \'redClass\':row.entity.trans_stat == \'Rejected\'}"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';

            var gridOptions = {
                enableRowSelection: false,
                enableRowHeaderSelection: false,
                enableFiltering: false,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 55,

                paginationPageSize: 10,
                enablePagination: true,
                paginationPageSizes: [10,15,20,25,30,35,40,45,50],
                enablePaginationControls: false,
                enableVerticalScrollbar: 1,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs,
                rowTemplate: rowtpl,
                enableColumnResizing: true,
                enableColumnMoving: true,
                exporterCsvFilename: 'transaction_results.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),

            };
            gridOptions.data = '';
            return gridOptions;
        },
        /** added by srini **/
        //used in this service only to remove userAction column
        removeUserAction: function (columnDefs) {
            if (this.getTabDenyPermission()) {
                columnDefs.splice(columnDefs.length - 1, 1);
            }

            return columnDefs;
        },

        getUserAllPermission: function () {
            if (!angular.isUndefined($window.localStorage['Main-userPermissions'])) {
                if (typeof $window.localStorage['Main-userPermissions'] == 'string')
                    return JSON.parse($window.localStorage['Main-userPermissions']);
                else
                    return JSON.parse($window.localStorage['Main-userPermissions']);
                //console.log(permissions);               

            }
            return null;
        },

        getTabDenyPermission: function () {
            var permissions = this.getUserAllPermission();
            if (permissions != null) {
                if (permissions.transaction_tb_access == "R") {
                    return true;
                }
            }
            return false;
        },
        getDenyApprovePermission: function () {
            var permissions = this.getUserAllPermission();
            if (permissions != null) {
                if (permissions.is_approver == "0") {
                    return true;
                }
            }
            return false;
        },
        /** added by srini **/
        clearTransSearchData: function () {
            searchTransResultsData = [];
        }
    }
}]);

angular.module('transaction').factory('TransactionClearDataService', ['TransactionDataServices', function (TransactionDataServices) {
    return {
        clearTransactionData: function () {
            TransactionDataServices.clearTransSearchData();
        }
    }
}]);

var TRANSACTION_CRUD_CONSTANTS = {
    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',

    SUCCESS_MESSAGE: 'The case was successfully created!',
    FAILURE_MESSAGE: 'The case was not created.',
    FAIULRE_REASON: 'It looks like there is a similar record in the database. Please review.',

    SUCCESS_CONFIRM: 'Success!',
    FAILURE_CONFIRM: 'Failed!',

    SERVICE_TIMEOUT: 'Timed out',
    SERVICE_TIMEOUT_CONFIRM: 'Error: Timed Out',
    SERVICE_TIMEOUT_MESSAGE: 'The service/database timed out. Please try again after some time.',

    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM: "Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",


    PROCEDURE: {
        SUCCESS: 'Success',
        DUPLICATE: 'duplicate',
        NOT_PRESENT: 'The original record is not present.'
    },

};