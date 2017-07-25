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
//alternate IDs surfacing 
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
        getConsAlternateCols: function () {
            return [
                	{
                	    field: 'arc_srcsys_cd', width: "*", minWidth: 160, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
                	    displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                	    filter: {
                	        condition: uiGridConstants.filter.STARTS_WITH
                	    },
                	    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

                	},
	                {
	                    field: 'cnst_srcsys_id', width: "*", minWidth: 130, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'cnst_srcsys_scndry_id', width: "*", minWidth: 140, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "Secondary Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'alt_arc_srcsys_cd', width: "*", minWidth: 200, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "Alternate Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'alt_cnst_srcsys_id', width: "*", minWidth: 200, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "Alternate Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'alt_cnst_srcsys_scndry_id', width: "*", minWidth: 200, maxWidth: 9000, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "Alternate Secondary Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'cnst_alt_id_strt_ts', width: "*", minWidth: 200, maxWidth: 9000, visible: false, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "Start Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

	                },
	                {
	                    field: 'cnst_alt_id_end_dt', width: "*", minWidth: 200, maxWidth: 9000, visible: false, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
	                    displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
	                    filter: {
	                        condition: uiGridConstants.filter.STARTS_WITH
	                    },
	                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
	                    cellFilter: 'column_date_filter'

	                },
	                {
	                    field: 'dw_trans_ts', width: "*", minWidth: 200, maxWidth: 9000, visible: false, menuItems: this.customGridFilter(EXTENDED_CONSTANTS.ALTERNATE_IDS),
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
                        if (type == EXTENDED_CONSTANTS.ALTERNATE_IDS) {
                            $rootScope.toggleAlternateIdsSearchFilter();
                        }
                       
                    },

                }];
        },
    }

}]);

angular.module('constituent').factory('ExtendedDataServices', ['EXTENDED_CONSTANTS', 'CustomHttp', '$q',function (EXTENDED_CONSTANTS, CustomHttp, $q) {

    return {
        getAlternateIds: function (postParams, enableCache) {
            var url = EXTENDED_CONSTANTS.GET_ALTERNATE_IDS_URL ;
            //console.log(url);
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        }
    }

}]);
angular.module('constituent').constant('EXTENDED_CONSTANTS', {
    "ALTERNATE_IDS": "AlternateIds",
    "GET_ALTERNATE_IDS_URL": BasePath + "Test/GetAlternateIds"
});