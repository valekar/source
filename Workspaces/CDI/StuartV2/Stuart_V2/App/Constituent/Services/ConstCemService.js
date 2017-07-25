angular.module('constituent').factory('ConstCemServices', ['$q', '$timeout','$rootScope', '$window', function ($q, $timeout,$rootScope,$window) {
    
    return {

        getGridOptions: function (colDef) {
            var transaction = "Transaction";
            var softDeletedMessage = "Inactive/Soft-Deleted records cannot be edited.";
            var smallSoftDeleted = "deleted";
            var rowtpl = '<div ng-class="{\'greyClass\':(row.entity.transNotes.length > \'0\' && (row.entity.transNotes == \'' + softDeletedMessage + '\' || row.entity.transNotes.indexOf(\'' + smallSoftDeleted + '\')>0)  ) || (row.entity.row_stat_cd && row.entity.row_stat_cd ==\'L\'),maroonClass:row.entity.transNotes.length > \'0\' && (row.entity.transNotes.indexOf(\'' + transaction + '\')==0) }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
            var new_col = this.removeUserAction(colDef);
            var options =  function(){
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
                    columnDefs.splice(columnDefs.length-1, 1);              
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


angular.module('constituent').factory('StoreData', [function () {
    var StoreData = {};
    // this is for preference locator
    /*Start of preference locator */
    StoreData.PreferenceLocatorList = [];
    StoreData.PreferenceLocatorFullList = [];
    StoreData.LocatorDropDowns = [];

    

    StoreData.addPrefLocatorList = function (fullList,list) {
        StoreData.PreferenceLocatorList = list;
        StoreData.PreferenceLocatorFullList = fullList;
    };

    StoreData.getPrefLocatorList = function () {
        return StoreData.PreferenceLocatorList;
    };

    StoreData.getPrefLocatorFullList = function () {
        return StoreData.PreferenceLocatorFullList;
    };


    StoreData.addLocatorDropDowns = function (list) {
        StoreData.LocatorDropDowns = list;
    }

    StoreData.getLocatorDropDowns = function () {
        return StoreData.LocatorDropDowns;
    }

    /* End of Preference locator code*/

    /*Start of MEssage Preference Code*/
    StoreData.MessagePreferenceList = [];
    StoreData.MessagePreferenceFullList = [];
    StoreData.MessagePreferenceDropDowns = [];

    StoreData.addMessagePrefList = function (fullList,list) {
        StoreData.MessagePreferenceList = list;
        StoreData.MessagePreferenceFullList = fullList;
    };

    StoreData.getMessagePrefList = function () {
        return StoreData.MessagePreferenceList;
    };

    StoreData.getMessagePrefFullList = function () {
        return StoreData.MessagePreferenceFullList;
    };

    StoreData.getMessagePrefDropDowns = function () {
        return StoreData.MessagePreferenceDropDowns;
    }

    StoreData.addMessagePrefDropDowns = function (res) {
        StoreData.MessagePreferenceDropDowns = res;
    }

    /*End of Message Preference*/

    /*Start of DNC*/
    StoreData.DncList = [];
    StoreData.DncFullList = [];

    StoreData.DncDropDowns = [];

    StoreData.addDncList = function (fullList,list) {
        StoreData.DncList = list;
        StoreData.DncFullList = fullList;
    };

    StoreData.getDncList = function () {
        return StoreData.DncList;
    };

    StoreData.getDncFullList = function () {
        return StoreData.DncFullList;
    };

    StoreData.getDncDropDowns = function () {
        return StoreData.DncDropDowns;
    }

    StoreData.addDncDropDowns = function (res) {
        StoreData.DncDropDowns = res;
    }
    /*End of DNC*/

    /*Group Memberhsip*/
    StoreData.GroupMembershipList = [];
    StoreData.GroupMembershipFullList = [];
    StoreData.addGroupMembershipList = function (fullList, list) {
        StoreData.GroupMembershipList = list;
        StoreData.GroupMembershipFullList = fullList;
    };

    StoreData.getGroupMembershipList = function () {
        return StoreData.GroupMembershipList;
    };
    StoreData.getGroupMembershipFullList = function () {
        return StoreData.GroupMembershipFullList;
    };

    /*End of group membership*/

    /*Alternate Ids*/
    StoreData.AlternateIdsList = [];
    StoreData.AlternateIdsFullList = [];
    StoreData.addAlternateIdsList = function (fullList, list) {
        StoreData.AlternateIdsList = list;
        StoreData.AlternateIdsFullList = fullList;
    };

    StoreData.getAlternateIdsList = function () {
        return StoreData.AlternateIdsList;
    };
    StoreData.getAlternateIdsFullList = function () {
        return StoreData.AlternateIdsFullList;
    };

    /*End of Alternate Ids*/


    StoreData.cleanData = function () {
        StoreData.PreferenceLocatorList = [];
        StoreData.PreferenceLocatorFullList = [];
        StoreData.MessagePreferenceFullList = [];
        StoreData.MessagePreferenceList = [];
        StoreData.DncList = [];
        StoreData.DncFullList = [];
        StoreData.GroupMembershipList = [];
        StoreData.GroupMembershipFullList = [];
        StoreData.AlternateIdsList = [];
        StoreData.AlternateIdsFullList = [];

        StoreData.LocatorDropDowns = [];
    }



    return StoreData;

}]);

angular.module("constituent").factory("ConstCemDataServices", ["$q", "CEM_CONSTANTS","CustomHttp","uiGridConstants","$rootScope","StoreData",
    function ($q, CEM_CONSTANTS, CustomHttp, uiGridConstants,$rootScope,StoreData) {

    var masterId = null;
  
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
        getDncColumns: function(){
            return [
                { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/cem/iconGridCem.tpl.html', enableCellEdit: false },
                {
                    field: 'line_of_service_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'comm_chan', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Comm Channel',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'locator_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Locator Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'dnc_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Type',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'cnst_dnc_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'date_filter',
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
               

                {
                    field: 'cnst_dnc_end_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'date_filter',
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
             
                {
                    field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }, visible: false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    visible: false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    visible: false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                {
                    field: 'dw_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'date_filter',visible:false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.DNC_SURFACE)
                },
                 {
                     field: 'Action', cellTemplate: 'App/Constituent/Views/cem/gridCemDropDown.tpl.html', displayName: 'User Action',
                     width: "*", minWidth: 120, maxWidth: 9000, headerCellTemplate: '<div style="margin-top:4.5%;">User Actions</div>'
                 }
            ]
        },
        getMsgPrefColumns: function () {
            return [
                    { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/cem/iconGridCem.tpl.html', enableCellEdit: false },
                   
                    {
                        field: 'line_of_service_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'comm_chan', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Comm Channel',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'msg_pref_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Message Type',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'msg_pref_val', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Message Value',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                     {
                         field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System',
                         cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 120, maxWidth: 300,
                         filter: {
                             condition: uiGridConstants.filter.STARTS_WITH
                         },
                         menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                     },
                    {
                        field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Id',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 140, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'msg_pref_exp_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Expiration Date',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'comm_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Comm Type',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'msg_pref_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },

                        cellFilter: 'date_filter',
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'msg_pref_end_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },

                        cellFilter: 'date_filter',
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Trans Key',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 500,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        visible: false,
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                           

                    {
                        field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        }, visible: false,
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        visible: false,
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        visible: false,
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'dw_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 300,
                        filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                        },
                        cellFilter: 'date_filter', visible: false,
                        menuItems: this.customGridFilter(CEM_CONSTANTS.MSG_PREF)
                    },
                    {
                        field: 'Action', cellTemplate: 'App/Constituent/Views/cem/gridCemDropDown.tpl.html', displayName: 'User Action',
                        width: "*", minWidth: 120, maxWidth: 9000, headerCellTemplate: '<div style="margin-top:4.5%;">User Actions</div>'
                    }

            ]
        },
        getPrefLocatorColumns: function () {
            return [
                { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/cem/iconGridCem.tpl.html', enableCellEdit: false },
               
                {
                    field: 'line_of_service_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 50, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'pref_loc_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Type',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 50, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                 {
                     field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System',
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 120, maxWidth: 300,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                 },
                {
                    field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 180, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'pref_loc_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Locator Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'cnst_pref_loc_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 200, maxWidth: 400,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'cnst_pref_loc_end_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 200, maxWidth: 400,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },visible:false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    visible:false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    visible:false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'dw_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter', visible: false,
                    menuItems: this.customGridFilter(CEM_CONSTANTS.PREF_LOCATION)
                },
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/cem/gridCemDropDown.tpl.html', displayName: 'User Action',
                    width: "*", minWidth: 120, maxWidth: 9000, headerCellTemplate: '<div style="margin-top:4.5%;">User Actions</div>'
                }
            ]
        },
        getGrpMembrshpColumns: function () {
            return [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/cem/iconGridCem.tpl.html', enableCellEdit: false },
             
              
               {
                   field: 'grp_nm', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Name', width: "*", minWidth: 180, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'grp_cd', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Code', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               {
                   field: 'assgnmnt_mthd', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Assignment Method', width: "*", minWidth: 140, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                 {
                     field: 'line_of_service_cd', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS', width: "*", minWidth: 50, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'grp_typ', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Type', width: "*", minWidth: 140, maxWidth: 9000,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'arc_srcsys_cd', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 80, maxWidth: 9000, visible: true,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
              },
               {
                   field: 'grp_mbrshp_eff_strt_dt', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', width: "*", minWidth: 90, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellFilter: 'column_date_filter'
               },
               {
                   field: 'grp_mbrshp_eff_end_dt', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End By', width: "*", minWidth: 90, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },


               {
                   field: 'grp_owner', menuItems: this.customGridFilter(CEM_CONSTANTS.GRP_MEMBRSHP),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Owner', width: "*", minWidth: 120, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },

               {
                   field: 'row_stat_cd', width: "*", minWidth: 60, maxWidth: 9000,
                   displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/cem/gridCemDropDown.tpl.html', displayName: 'User Action',
                    width: "*", minWidth: 120, maxWidth: 9000, headerCellTemplate: '<div style="margin-top:4.5%;">User Actions</div>'
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
                        if (type == CEM_CONSTANTS.MSG_PREF) {
                            $rootScope.toggleMsgPrefSearchFilter();
                        }
                        else if (type == CEM_CONSTANTS.DNC_SURFACE) {
                            $rootScope.toggleDncSearchFilter();
                        }
                        else if (type == CEM_CONSTANTS.PREF_LOCATION) {
                            $rootScope.togglePrefLocSearchFilter();
                        }
                        else if (type == CEM_CONSTANTS.GRP_MEMBRSHP) {
                            $rootScope.toggleGrpMemSearchFilter();
                        }
                    },

                }];
        },
        getMasterId: function () {
            return masterId;
        },
        setMasterId: function (mId) {
            masterId = mId;
        },
        /*Preferred Locator*/
        getPrefLocator: function (id, enableCache) {  
            var url = CEM_CONSTANTS.GET_PREF_LOCATOR_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) {return result.data;},function (error) {return $q.reject(error);});
        },
        getAllPrefLocator: function (id, enableCache) {
            var url = CEM_CONSTANTS.GET_ALL_PREF_LOCATOR_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postAddPrefLocator:function(postParams,enableCache){
            var url = CEM_CONSTANTS.ADD_PREF_LOCATOR_URL ;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postEditPrefLocator: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.EDIT_PREF_LOCATOR_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postDeletePrefLocator: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.DELETE_PREF_LOCATOR_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },

        getLocatorDropDowns: function (id, enableCache) {
            var defered = $q.defer();
            if (StoreData.getLocatorDropDowns().length <= 0) {
                var url = CEM_CONSTANTS.GET_DROP_DOWN_PREF_LOCATOR + "" + id;

                CustomHttp.httpGet(url, enableCache).then(function (result) {
                    StoreData.addLocatorDropDowns(result.data); defered.resolve(StoreData.getLocatorDropDowns())
                },
                function (error) {
                    return defered.reject(error);
                }
               );
            }
            else {
                defered.resolve(StoreData.getLocatorDropDowns());
            }
            
            return defered.promise;
        },
        /*End of Preferred Locator*/
      
        /*Message Preference*/
        getMessagePref: function (id, enableCache) {  
            var url = CEM_CONSTANTS.GET_MSG_PREF_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) {return result.data;},function (error) {return $q.reject(error);});
        },
        getAllMessagePref: function (id, enableCache) {
            var url = CEM_CONSTANTS.GET_ALL_MSG_PREF_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postAddMessagePref: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.ADD_MSG_PREF_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postEditMessagePref: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.EDIT_MSG_PREF_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postDeleteMessagePref: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.DELETE_MSG_PREF_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        getMessagePrefDropDowns: function (id, enableCache) {
            var defered = $q.defer();
            if (StoreData.getMessagePrefDropDowns().length <= 0) {
                var url = CEM_CONSTANTS.GET_DROP_DOWN_MSG_PREF + "" + id;

                CustomHttp.httpGet(url, enableCache).then(function (result) {
                    StoreData.addMessagePrefDropDowns(result.data); defered.resolve(StoreData.getMessagePrefDropDowns());
                },
                function (error) {
                    return defered.reject(error);
                }
               );
            }
            else {
                defered.resolve(StoreData.getMessagePrefDropDowns());
            }

            return defered.promise;
        },
        /*End of Message Preference */
        /*DNC*/
        getDnc: function (id, enableCache) {
            var url = CEM_CONSTANTS.GET_DNC_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        getAllDnc: function (id, enableCache) {
            var url = CEM_CONSTANTS.GET_ALL_DNC_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postAddDnc: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.ADD_DNC_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postEditDnc: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.EDIT_DNC_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postDeleteDnc: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.DELETE_DNC_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        /*End of DNC*/
        /*GROUP MEMbership  */
        getGroupMembership: function (id, enableCache) {
            var url = CEM_CONSTANTS.GET_GROUP_MEMBERSHIP_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        getAllGroupMembership: function (id, enableCache) {
            var url = CEM_CONSTANTS.GET_ALL_GROUP_MEMBERSHIP_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postAddGroupMembership: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.ADD_GROUP_MEMBERSHIP_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postEditGroupMembership: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.EDIT_GROUP_MEMBERSHIP_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postDeleteGroupMembership: function (postParams, enableCache) {
            var url = CEM_CONSTANTS.DELETE_GROUP_MEMBERSHIP_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        }
        /* End of group membership*/
    }
    }]);


angular.module('constituent').factory('CemGridColumn', ['uiGridConstants', function (uiGridConstants) {
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
    return {
        getDncAllColumns: function () {
            return [
                {
                    field: 'dnc_mstr_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Master ID',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
             {
                 field: 'line_of_service_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS',
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 }                
             },
                {
                    field: 'comm_chan', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Comm Channel',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'locator_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Locator Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'dnc_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Type',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'cnst_dnc_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'date_filter',

                },
                {
                    field: 'cnst_dnc_end_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'date_filter', visible:false
                },
               
                 {
                     field: 'trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Trans Key',
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     }

                 },
                {
                    field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }

                },
                 {
                     field: 'notes', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Notes',
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     }

                 },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                 {
                     field: 'srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source Timestamp',
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 300,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellFilter: 'date_filter',

                 },
                {
                    field: 'dw_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'date_filter',

                },
            ]
        },
        getMsgPrefAllColumns: function () {
            return [
                 {
                     field: 'msg_pref_mstr_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Master ID',
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     }
                 },
                {
                    field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 120, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                    {
                        field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Id',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 140, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'line_of_service_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'comm_chan', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Comm Channel',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'msg_pref_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Message Type',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'msg_pref_val', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Message Value',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'msg_pref_exp_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Expiration Date',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'comm_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Comm Type',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'msg_pref_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        cellFilter: 'date_filter',

                    },
                    {
                        field: 'msg_pref_end_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 500,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },

                        cellFilter: 'date_filter',

                    },
                    {
                        field: 'trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Trans Key',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 500,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },

                    {
                        field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'App Sources',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        }
                    },
                    {
                        field: 'dw_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        cellFilter: 'date_filter',
                    },
                    {
                        field: 'srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source Timestamp',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 170, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        cellFilter: 'date_filter',
                    }
            ];
        },
        getPrefLocAllColumns: function () {
            return [
                 {
                     field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System',
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 120, maxWidth: 300,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     }
                 },
                {
                    field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 180, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'line_of_service_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 50, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'pref_loc_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Type',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 50, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'pref_loc_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Locator Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Trans Key',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'cnst_pref_loc_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 200, maxWidth: 400,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',

                },
                {
                    field: 'cnst_pref_loc_end_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 200, maxWidth: 400,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',

                },
                {
                    field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'App Sources',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'dw_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',
                },
                {
                    field: 'srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',
                }
            ];
        },
        getGrpMemAllColumns: function () {
            return [
                 {
                     field: 'arc_srcsys_cd',
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 80, maxWidth: 9000, visible: true,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                 },
              {
                  field: 'grp_key',
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Key', width: "*", minWidth: 140, maxWidth: 9000,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
               {
                   field: 'grp_mbrshp_eff_strt_dt',
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Eff Start Date', width: "*", minWidth: 90, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'grp_mbrshp_eff_end_dt',
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Eff End Date', width: "*", minWidth: 90, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               {
                   field: 'assgnmnt_mthd', 
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Assignment Method', width: "*", minWidth: 140, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                 {
                     field: 'line_of_service_cd', 
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LOS', width: "*", minWidth: 50, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
               {
                   field: 'grp_mbrshp_strt_ts',
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', width: "*", minWidth: 90, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellFilter: 'column_date_filter'
               },
               {
                   field: 'grp_mbrshp_end_dt',
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End By', width: "*", minWidth: 90, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'trans_key',
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Trans Key', width: "*", minWidth: 120, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'user_id',
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'USer Id', width: "*", minWidth: 120, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'row_stat_cd', width: "*", minWidth: 60, maxWidth: 9000,
                   displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, 
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
                {
                    field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Appl Sources',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',
                }
            ];
        }
    }

}]);

angular.module('constituent').factory('CustomHttp', ['$http','CEM_CONSTANTS', "$templateCache",function ($http, CEM_CONSTANTS,$templateCache) {
    return {
        httpGet: function (url, enableCache) {
            if (enableCache) {
                return $http({ method: 'GET', url: url, headers: CEM_CONSTANTS.HEADERS, cache: $templateCache });
            } else {
                return $http({ method: 'GET', url: url, headers: CEM_CONSTANTS.HEADERS });
            }
        },
        httpPost: function (url, enableCache, postParams) {
            if (enableCache) {
                return $http({ method: 'POST', url: url, headers: CEM_CONSTANTS.HEADERS, data: postParams, cache: $templateCache });
            } else {
                return $http({ method: 'POST', url: url, headers: CEM_CONSTANTS.HEADERS, data: postParams });
            }
        }
    }
    

}]);

angular.module('constituent').constant('CEM_CONSTANTS', {
    "GET_PREF_LOCATOR_URL": BasePath + "preference/locator/",
    "GET_ALL_PREF_LOCATOR_URL": BasePath + "preference/locator/all/",
    "ADD_PREF_LOCATOR_URL": BasePath + "preference/locator",
    "GET_DROP_DOWN_PREF_LOCATOR": BasePath + "preference/dropdowns/",
    "EDIT_PREF_LOCATOR_URL":BasePath + "preference/locator/edit",
    "DELETE_PREF_LOCATOR_URL": BasePath + "preference/locator/delete",
    // this is for message preference
    "GET_MSG_PREF_URL": BasePath + "message/preference/",
    "GET_ALL_MSG_PREF_URL": BasePath + "message/preference/all/",
    "ADD_MSG_PREF_URL": BasePath + "message/preference/add",
    "GET_DROP_DOWN_MSG_PREF": BasePath + "message/preference/options/",
    "EDIT_MSG_PREF_URL": BasePath + "message/preference/edit",
    "DELETE_MSG_PREF_URL": BasePath + "message/preference/delete",
    // this is for dnc 
    "GET_DNC_URL": BasePath + "dnc/",
    "GET_ALL_DNC_URL": BasePath + "dnc/all/",
    "ADD_DNC_URL": BasePath + "dnc/add",
    "GET_DROP_DOWN_DNC": BasePath + "preference/options/",
    "EDIT_DNC_URL": BasePath + "dnc/edit",
    "DELETE_DNC_URL": BasePath + "dnc/delete",
    /*This is for ggroup membership*/
    "GET_GROUP_MEMBERSHIP_URL": BasePath + "group/membership/",
    "GET_ALL_GROUP_MEMBERSHIP_URL": BasePath + "group/membership/all/",
    "ADD_GROUP_MEMBERSHIP_URL": BasePath + "group/membership/add",
    "EDIT_GROUP_MEMBERSHIP_URL": BasePath + "group/membership/edit",
    "DELETE_GROUP_MEMBERSHIP_URL": BasePath + "group/membership/delete",

    "HEADERS":{
            "Content-Type": "application/json",
            "Accept": "application/json"
        
    },
    "PREFERENCE_LOCATOR_ADD":"add",
    "PREFERENCE_LOCATOR_EDIT":"edit",
    "PREFERENCE_LOCATOR_DELETE": "delete",

    "MESSAGE_PREFERENCE_ADD": "add",
    "MESSAGE_PREFERENCE_EDIT": "edit",
    "MESSAGE_PREFERENCE_DELETE": "delete",

    "DNC_ADD": "add",
    "DNC_EDIT": "edit",
    "DNC_DELETE": "delete",

    "GROUP_MEMBERSHIP_ADD": "add",
    "GROUP_MEMBERSHIP_EDIT": "edit",
    "GROUP_MEMBERSHIP_DELETE": "delete",

    "TRANSACTION_SUCCESS_HEADER": "Success!",
    "TRANSACTION_SUCCESS_EDIT_MESSAGE": "The record was successfully edited.",
    "TRANSACTION_SUCCESS_DELETE_MESSAGE": "The record was successfully deleted.",
    "TRANSACTION_SUCCESS_ADD_MESSAGE": "The record was successfully added.",

    "TRANSACTION_DUPLICATE_MESSAGE": "Please choose different parameters",
    "TRANSACTION_DUPLICATE_HEADER" : "Duplicate Records",

    "ERROR_HEADER": "Failed!",
    "PLEASE_PROVIDE": "Please provide the necessary inputs",
    "DB_ERROR": "Database Error",
    "DB_ERROR_HEADER":"Error: Database Error",
    "DB_ERROR_MESSAGE": "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    "ACCESS_DENIED_MESSAGE": 'Logged in user does not have appropriate permission.',
    "ACCESS_DENIED_CONFIRM": 'Access Denied!',
    "ACCESS_DENIED": 'LoginDenied',
    "SERVICE_TIMEOUT": 'Timed out',
    "SERVICE_TIMEOUT_CONFIRM": 'Error: Timed Out',
    "SERVICE_TIMEOUT_MESSAGE": 'The service/database timed out. Please try again after some time.',
    'ADD': 'add',
    'EDIT': 'edit',
    'DELETE':'delete',
    'SHOW_ALL_RECORDS': 'Show All Records',
    'HIDE_RECORDS': 'Hide Inactive',

    'DNC_DETAILS': 'showAllDNCRecords',
    'MSG_PREF_DETAILS': 'showAllMsgPrefRecords',
    'PREF_LOC_DETAILS': 'showAllPrefLocRecords',
    'GRP_MEM_DETAILS': 'showAllGrpMemRecords',

    "MSG_PREF": "MsgPreference",
    "PREF_LOCATION": "PreferenceLoc",
    "DNC_SURFACE": "Dnc",
    "GRP_MEMBRSHP": "GroupMembership",
   

    "MESSAGE_PREF_VALIDATION_TYPE": "Please enter the Message Preference Type",
    "MESSAGE_PREF_VALIDAION_VALUE": "Please enter the Message Preference Value",
    "MESSAGE_PREF_VALIDATION_COMM_CHAN": "Please enter the Communication Channel",
    "MESSAGE_PREF_VALIDATION_COMM_TYPE": "Please enter the Communication Type",
    "MESSAGE_PREF_VALIDAION_LOS": "Please enter the Line of Service",
    "PREFERRED_LOC_VALIDATION_LOC_TYPE": "Please enter Preferred Locator Type",
    "PREFERRED_LOC_VALIDATION_LOC": "Please enter Preferred Locator",
    "GROUP_MEM_VALIDATION_GROUP_NAME":"Please enter the Group Name",
    "SELECT_CORRECT_COMBO": "Please select correct combinations of LOS, communication channel, message preference type and message preference value"
});




angular.module('constituent').factory('ModalService', ['$uibModal', '$q', function ( $uibModal, $q) {
    return {
        OpenModal: function (templ, ctrl, size, params) {
            var deferred = $q.defer();
            var CssClass = '';
            if (size != 'sm') {
                CssClass = 'app-modal-window';
            }

            var ModalInstance = ModalInstance || $uibModal.open({
                animation: true,
                templateUrl: templ,
                controller: ctrl,  // Logic in instantiated controller 
                windowClass: CssClass,
                backdrop: 'static', /*  this prevent user interaction with the background  */
                keyboard: false,
                resolve: {
                    params: function () {
                        return params;
                    }
                }
            });

            ModalInstance.result.then(function (result) {
                console.log("Modal Instance Result " + result);
                console.log("State Param before");
                //console.log($state);
                // modalMessage($rootScope, result, $state, $uibModalStack, UploadDataServices);
                deferred.resolve(result);
            });


            return deferred.promise;
        }
    }

}]);


angular.module('constituent').factory('PreferenceLocatorModal', ['CEM_CONSTANTS', 'ModalService', "$q", function (CEM_CONSTANTS, ModalService, $q) {
    return {

        getModal: function (type,results) {
            var deferred = $q.defer();
            if (type == CEM_CONSTANTS.PREFERENCE_LOCATOR_ADD) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Preference/AddLocator.tpl.html";
                var ctrl = "AddPreferenceLocatorCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.PREFERENCE_LOCATOR_EDIT) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Preference/EditLocator.tpl.html";
                var ctrl = "EditPreferenceLocatorCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.PREFERENCE_LOCATOR_DELETE) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Preference/DeleteLocator.tpl.html";
                var ctrl = "DeletePreferenceLocatorCtrl";
                var size = 'sm';
            }

            ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
                deferred.resolve(response);
            });

            return deferred.promise;
        }

    }
}]);


angular.module('constituent').factory('MessagePreferenceModal', ['CEM_CONSTANTS', 'ModalService', "$q", function (CEM_CONSTANTS, ModalService, $q) {
    return {

        getModal: function (type, results) {
            var deferred = $q.defer();
            if (type == CEM_CONSTANTS.MESSAGE_PREFERENCE_ADD) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Message/AddMessagePref.tpl.html";
                var ctrl = "AddMessagePreferenceCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.MESSAGE_PREFERENCE_EDIT) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Message/EditMessagePref.tpl.html";
                var ctrl = "EditMessagePreferenceCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.MESSAGE_PREFERENCE_DELETE) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Message/DeleteMessagePref.tpl.html";
                var ctrl = "DeleteMessagePreferenceCtrl";
                var size = 'sm';
            }

            ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
                deferred.resolve(response);
            });

            return deferred.promise;
        }

    }
}]);




angular.module('constituent').factory('DncModal', ['CEM_CONSTANTS', 'ModalService', "$q", function (CEM_CONSTANTS, ModalService, $q) {
    return {

        getModal: function (type, results) {
            var deferred = $q.defer();
            if (type == CEM_CONSTANTS.DNC_ADD) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Dnc/AddDnc.tpl.html";
                var ctrl = "AddDncCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.DNC_EDIT) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Dnc/EditDnc.tpl.html";
                var ctrl = "EditDncCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.DNC_DELETE) {
                var templUrl = BasePath + "App/Constituent/Views/cem/Dnc/DeleteDnc.tpl.html";
                var ctrl = "DeleteDncCtrl";
                var size = 'sm';
            }

            ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
                deferred.resolve(response);
            });

            return deferred.promise;
        }

    }
}]);


angular.module('constituent').factory('ShowDetailsModal', ['CEM_CONSTANTS', 'ModalService', "$q", function (CEM_CONSTANTS, ModalService, $q) {
    return function (results) {
            var deferred = $q.defer();

                var templUrl = BasePath + "App/Constituent/Views/common/ShowDetails.tpl.html";
                var ctrl = "ShowCemCtrl";
                var size = 'lg';
            
           
            ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
                deferred.resolve(response);
            });

            return deferred.promise;
        }

    
}]);

angular.module('constituent').factory('GrpMembrshpModal', ['CEM_CONSTANTS', 'ModalService', "$q", function (CEM_CONSTANTS, ModalService, $q) {
    return {

        getModal: function (type, results) {
            var deferred = $q.defer();
            if (type == CEM_CONSTANTS.GROUP_MEMBERSHIP_ADD) {
                var templUrl = BasePath + "App/Constituent/Views/cem/GrpMembrship/AddGrpMembrship.tpl.html";
                var ctrl = "AddGrpMembrshpCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.GROUP_MEMBERSHIP_EDIT) {
                var templUrl = BasePath + "App/Constituent/Views/cem/GrpMembrship/EditGrpMembrship.tpl.html";
                var ctrl = "EditGrpMembrshpCtrl";
                var size = 'sm';
            }
            else if (type == CEM_CONSTANTS.GROUP_MEMBERSHIP_DELETE) {
                var templUrl = BasePath + "App/Constituent/Views/cem/GrpMembrship/DeleteGrpMembrship.tpl.html";
                var ctrl = "DeleteGrpMembrshpCtrl";
                var size = 'sm';
            }

            ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
                deferred.resolve(response);
            });

            return deferred.promise;
        }

    }
}]);

angular.module('constituent').filter('unique', function () {

    // Take in the collection and which field
    //   should be unique
    // We assume an array of objects here
    // NOTE: We are skipping any object which
    //   contains a duplicated value for that
    //   particular key.  Make sure this is what
    //   you want!
    return function (arr, targetField) {

        var values = [],
            i,
            unique,
            l = arr.length,
            results = [],
            obj;

        // Iterate over all objects in the array
        // and collect all unique values
        for (i = 0; i < arr.length; i++) {

            obj = arr[i];

            // check for uniqueness
            unique = true;
            for (v = 0; v < values.length; v++) {
                if (obj[targetField] == values[v]) {
                    unique = false;
                }
            }

            // If this is indeed unique, add its
            //   value to our values and push
            //   it onto the returned array
            if (unique) {
                values.push(obj[targetField]);
                results.push(obj);
            }

        }
        return results;
    };
})

angular.module('constituent').filter('date_filter', ['$filter', function ($filter) {
    return function (value) {
        console.log("data");
        console.log(value);
        if (value)
            return $filter('date')(new Date(value + ""), 'MM/dd/yyyy');
        else
            value + "";
    }
}]);


angular.module('constituent').factory('Custom_Date', [function () {
    return {
        current: function () {
            var today = new Date();
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth();
            var yyyy = today.getFullYear();


            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            return new Date(yyyy, mm, dd);
        }
    }
    
}]);



angular.module('constituent').factory('HandleResults', ['Message', 'CEM_CONSTANTS', function (Message, CEM_CONSTANTS) {

    var func = function (result, type) {

        if (result == CEM_CONSTANTS.PLEASE_PROVIDE) {
            Message.open(CEM_CONSTANTS.ERROR_HEADER, result);
        }
            // because below three are coming from tab level security
        else if (result.data == CEM_CONSTANTS.ACCESS_DENIED) {
            Message.open(CEM_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
        }
        else if (result.data == CEM_CONSTANTS.SERVICE_TIMEOUT) {
            Message.open(CEM_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result.data == CEM_CONSTANTS.DB_ERROR) {
            Message.open(CEM_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
        else if (result.statusText == "Internal Server Error") {
            //console.log(error);
            Message.open(CEM_CONSTANTS.DB_ERROR_HEADER, CEM_CONSTANTS.DB_ERROR_MESSAGE);
        }
        else if (!angular.isUndefined(result[0].o_outputMessage)) {
            if (result[0].o_transaction_key == null) {
                Message.open(CEM_CONSTANTS.TRANSACTION_DUPLICATE_HEADER, CEM_CONSTANTS.TRANSACTION_DUPLICATE_MESSAGE, result[0].o_transaction_key);
                return false;
            }
            else {
                if (type == CEM_CONSTANTS.ADD) {
                    message = CEM_CONSTANTS.TRANSACTION_SUCCESS_ADD_MESSAGE;
                } else if (type == CEM_CONSTANTS.EDIT) {
                    message = CEM_CONSTANTS.TRANSACTION_SUCCESS_EDIT_MESSAGE;
                } else if (type == CEM_CONSTANTS.DELETE) {
                    message = CEM_CONSTANTS.TRANSACTION_SUCCESS_DELETE_MESSAGE;
                }

                Message.open(CEM_CONSTANTS.TRANSACTION_SUCCESS_HEADER, message, result[0].o_transaction_key);
                return true;
            }
            //return true;
        }


        return false;
    }


    return func;

}]);




