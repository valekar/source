// this looks exact replica, but this will be the general grid service that are goining to come into stuart constituent tab
angular.module('constituent').factory('GridServices', ['$q', '$timeout', '$rootScope', '$window', function ($q, $timeout, $rootScope, $window) {
    return {
        getGridOptions: function (colDef) {
            var transaction = "Transaction";
            var softDeletedMessage = "Inactive/Soft-Deleted records cannot be edited.";
            var smallSoftDeleted = "deleted";
            var rowtpl = '<div ng-class="{\'greyClass\':(row.entity.transNotes.length > \'0\' && (row.entity.transNotes == \'' + softDeletedMessage + '\' || row.entity.transNotes.indexOf(\'' + smallSoftDeleted + '\')>0)  ) || (row.entity.row_stat_cd && row.entity.row_stat_cd ==\'L\'),maroonClass:row.entity.transNotes.length > \'0\' && (row.entity.transNotes.indexOf(\'' + transaction + '\')==0) }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
            var new_col = this.removeUserAction(colDef);
            var options = function () {
                return {
                    enableRowSelection: false,
                    enableRowHeaderSelection: false,
                    enableFiltering: false,
                    enableSelectAll: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 43,
                    rowTemplate: rowtpl,
                    paginationPageSize: 10,
                    paginationPageSizes: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 0,
                    enableHorizontalScrollbar: 1,
                    enableGridMenu: true,
                    showGridFooter: false,
                    columnDefs: new_col,
                    enableColumnResizing: true,
                    enableColumnMoving: true,
                    data: ""
                }
            }

            return options();
        },
        refreshGridData: function (gridOptions, res) {
            gridOptions.data = '';
            gridOptions.data.length = 0;
            gridOptions.data = res;
            //gridOptions.rowTemplate = rowtpl;
            return gridOptions;
        },
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
                if (permissions.constituent_tb_access == "R") {
                    return true;
                }
            }
            return false;
        },
    }


}]);
//Relationship surfacing 
angular.module('constituent').factory('ExtendedServices', ['uiGridConstants', '$rootScope', 'EXTENDED_CONSTANTS', function (uiGridConstants, $rootScope, EXTENDED_CONSTANTS) {

    var GRID_HEADER_TEMPLATE = '<div ng-class="{ \'sortable\': sortable }" tooltip-placement="top-left" tooltip-append-to-body="true" uib-tooltip="{{col.displayName}}">' +
                              '<div class="ui-grid-cell-contents " col-index="renderIndex" title="TOOLTIP">' +
                                  '<span class="ui-grid-header-cell-label ">{{ col.displayName CUSTOM_FILTERS }}</span>' +
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

    return {
        getConsRelationshipCols: function () {
            return [
                	{
                	    field: 'indv_mstr_id', width: "*", minWidth: 80, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
                	    displayName: "Individual Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                	    filter: {
                	        condition: uiGridConstants.filter.STARTS_WITH
                	    },
                	    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

                	},
	                {
	                    field: 'rlshp_typ', width: "*", minWidth: 100, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "Relationship Type", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible:false

	                },
	                {
	                    field: 'full_name', width: "*", minWidth: 100, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'phn_num', width: "*", minWidth: 80, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "Phone Num", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'address', width: "*", minWidth: 200, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "Address", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },

	                {
	                    field: 'email', width: "*", minWidth: 150, maxWidth: 9000, visible: true, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "Email", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'extn_phn_num', width: "*", minWidth: 160, maxWidth: 9000, visible: false, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "Extended Phone", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
	                    cellFilter: 'column_date_filter'

	                },
	                {
	                    field: 'dw_srcsys_trans_ts', width: "*", minWidth: 200, maxWidth: 9000, visible: false, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ORG_RELATIONSHIP),
	                    displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                }
            ]
        },
        customGridFilter: function (type) {
            return [
                {
                    title: 'Filter',
                    icon: 'ui-grid-icon-filter',
                    action: function ($event) {
                        // called in their respective controllers, not the efficient way of doing but it does the job
                        if (type == EXTENDED_CONSTANTS.ORG_RELATIONSHIP) {
                            $rootScope.toggleOrgRelationshipSearchFilter();
                        }

                    },

                }];
        },
    }

}]);

angular.module('constituent').factory('ExtendedDataServices', ['EXTENDED_CONSTANTS', 'CustomHttp', '$q', function (EXTENDED_CONSTANTS, CustomHttp, $q) {

    return {
        getOrgRelationship: function (id, enableCache) {
            var url = EXTENDED_CONSTANTS.GET_ORG_RELATIONSHIP_URL + "" + id;
            //console.log(url);
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        }
    }

}]);



angular.module('constituent').factory('CustomHttp', ['$http', 'EXTENDED_CONSTANTS', "$templateCache", function ($http, EXTENDED_CONSTANTS, $templateCache) {
    return {
        httpGet: function (url, enableCache) {
            if (enableCache) {
                return $http({ method: 'GET', url: url, headers: EXTENDED_CONSTANTS.HEADERS, cache: $templateCache });
            } else {
                return $http({ method: 'GET', url: url, headers: EXTENDED_CONSTANTS.HEADERS });
            }
        },
        httpPost: function (url, enableCache, postParams) {
            if (enableCache) {
                return $http({ method: 'POST', url: url, headers: EXTENDED_CONSTANTS.HEADERS, data: postParams, cache: $templateCache });
            } else {
                return $http({ method: 'POST', url: url, headers: EXTENDED_CONSTANTS.HEADERS, data: postParams });
            }
        }
    }


}]);



angular.module('constituent').factory("StoreData", [function () {

    var StoreData = {};

    StoreData.relationshipList = [];
    StoreData.relationshipFullList = [];
    StoreData.setRelationshipList = function (fullList , list) {
        StoreData.relationshipList = list;
        StoreData.relationshipFullList = fullList;
    }

    StoreData.getRelationshipList = function () {
        return StoreData.relationshipList;
    }

    StoreData.getRelationshipFullList = function () {
        return StoreData.relationshipFullList;
    }

    StoreData.clearRelationshipList = function () {
        StoreData.relationshipList = [];
        StoreData.relationshipFullList = [];
    }

    return StoreData;

}]);


angular.module('constituent').factory('HandleResults', ['Message', 'EXTENDED_CONSTANTS', function (Message, EXTENDED_CONSTANTS) {

    var func = function (result, type) {

        if (result == EXTENDED_CONSTANTS.PLEASE_PROVIDE) {
            Message.open(EXTENDED_CONSTANTS.ERROR_HEADER, result);
        }
            // because below three are coming from tab level security
        else if (result.data == EXTENDED_CONSTANTS.ACCESS_DENIED) {
            Message.open(EXTENDED_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
        }
        else if (result.data == EXTENDED_CONSTANTS.SERVICE_TIMEOUT) {
            Message.open(EXTENDED_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result.data == EXTENDED_CONSTANTS.DB_ERROR) {
            Message.open(EXTENDED_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
        else if (result.statusText == "Internal Server Error") {
            //console.log(error);
            Message.open(EXTENDED_CONSTANTS.DB_ERROR_HEADER, EXTENDED_CONSTANTS.DB_ERROR_MESSAGE);
        }
        else if (!angular.isUndefined(result[0].o_outputMessage)) {
            if (result[0].o_transaction_key == null) {
                Message.open(EXTENDED_CONSTANTS.TRANSACTION_DUPLICATE_HEADER, EXTENDED_CONSTANTS.TRANSACTION_DUPLICATE_MESSAGE, result[0].o_transaction_key);
                return false;
            }
            else {
                if (type == EXTENDED_CONSTANTS.ADD) {
                    message = EXTENDED_CONSTANTS.TRANSACTION_SUCCESS_ADD_MESSAGE;
                } else if (type == EXTENDED_CONSTANTS.EDIT) {
                    message = EXTENDED_CONSTANTS.TRANSACTION_SUCCESS_EDIT_MESSAGE;
                } else if (type == EXTENDED_CONSTANTS.DELETE) {
                    message = EXTENDED_CONSTANTS.TRANSACTION_SUCCESS_DELETE_MESSAGE;
                }

                Message.open(EXTENDED_CONSTANTS.TRANSACTION_SUCCESS_HEADER, message, result[0].o_transaction_key);
                return true;
            }
            //return true;
        }


        return false;
    }


    return func;

}]);



angular.module('constituent').constant('EXTENDED_CONSTANTS', {
    "ORG_RELATIONSHIP": "OrgRelationship",
    "GET_ORG_RELATIONSHIP_URL": BasePath + "Test/GetOrgRelationship/",
    "HEADERS": {
        "Content-Type": "application/json",
        "Accept": "application/json"

    },
    "TRANSACTION_SUCCESS_HEADER": "Success!",
    "TRANSACTION_SUCCESS_EDIT_MESSAGE": "The record was successfully edited.",
    "TRANSACTION_SUCCESS_DELETE_MESSAGE": "The record was successfully deleted.",
    "TRANSACTION_SUCCESS_ADD_MESSAGE": "The record was successfully added.",

    "TRANSACTION_DUPLICATE_MESSAGE": "Please choose different parameters",
    "TRANSACTION_DUPLICATE_HEADER": "Duplicate Records",

    "ERROR_HEADER": "Failed!",
    "PLEASE_PROVIDE": "Please provide the necessary inputs",
    "DB_ERROR": "Database Error",
    "DB_ERROR_HEADER": "Error: Database Error",
    "DB_ERROR_MESSAGE": "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    "ACCESS_DENIED_MESSAGE": 'Logged in user does not have appropriate permission.',
    "ACCESS_DENIED_CONFIRM": 'Access Denied!',
    "ACCESS_DENIED": 'LoginDenied',
    "SERVICE_TIMEOUT": 'Timed out',
    "SERVICE_TIMEOUT_CONFIRM": 'Error: Timed Out',
    "SERVICE_TIMEOUT_MESSAGE": 'The service/database timed out. Please try again after some time.',
    'ADD': 'add',
    'EDIT': 'edit',
    'DELETE': 'delete',
    'SHOW_ALL_RECORDS': 'Show All Records',
    'HIDE_RECORDS': 'Hide Inactive'
});









