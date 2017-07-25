
function GridColumns() {

};

GridColumns.prototype.getGridColumns = function (uiGridConstants, $rootScope) {
    //var GRID_HEADER_TEMPLATE = BasePath + 'App/Constituent/Views/common/GridHeader.tpl.html';
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

        bestSmryColDef: [{ field: 'constituent_id', displayName: 'Master Id', enableCellEdit: false, visible: false, width: "*", minWidth: 100, maxWidth: 160 },
                   {
                       field: 'name', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 160, maxWidth: 300,
                       filter: {
                           condition: uiGridConstants.filter.STARTS_WITH
                       },
                       cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                       menuItems: customGridFilter(HOME_CONSTANTS.BEST_SMRY, $rootScope)
                   },

                   {
                       field: 'address',
                       headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 300, maxWidth: 9000,
                       filter: {
                           condition: uiGridConstants.filter.STARTS_WITH
                       },
                       cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                       menuItems: customGridFilter(HOME_CONSTANTS.BEST_SMRY, $rootScope),
                       displayName: 'Address', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                   },
                   {
                       field: 'phone_number', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.BEST_SMRY, $rootScope),
                       displayName: 'Phone Number', width: "*", minWidth: 100, maxWidth: 180,
                       filter: {
                           condition: uiGridConstants.filter.STARTS_WITH
                       },
                       cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                   },
                    {
                        field: 'email_address',
                        headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 150, maxWidth: 9000,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                        menuItems: customGridFilter(HOME_CONSTANTS.BEST_SMRY, $rootScope),
                        displayName: 'Email Address', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                    }
                    //,
                    //{
                    //    field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                    //}
        ],

        constNameColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
                 {
                     field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                     displayName: 'Source System', width: "*", minWidth: 100, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                 },
                {
                    field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Source System ID', cellTemplate: '<span class="wordwrap">{{COL_FIELD}}</span>', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'best_prsn_nm_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                     displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 80, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes<!--<img src="Images/Medal First Place-52.png" width="25" height="25" />--></div>'
                 },
                  {
                      field: 'cnst_prsn_nm_typ_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                      displayName: 'Name Type', width: "*", minWidth: 100, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                  },
                  
                {
                    field: 'cnst_prsn_prefix_nm', width: "*", minWidth: 60, maxWidth: 9000, displayName: "Prefix", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_prsn_first_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'First Name', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_prsn_middle_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Middle Name', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_prsn_last_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Last Name', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_prsn_suffix_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Suffix', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
              
                {
                    field: 'cnst_prsn_nick_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Nick Name', visible: false, width: "*", minWidth: 90, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_prsn_mom_maiden_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Maiden Name', visible: false, width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
               
                {
                    field: 'cnst_nm_strt_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Start Date', sort: { direction: uiGridConstants.DESC, priority: 0 }, visible: false, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    
                        cellFilter: 'column_date_filter'
                },
                {
                    field: 'cnst_prsn_nm_end_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'End Date', visible: false, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },
                 {
                     field: 'assessmnt_ctg', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                     displayName: 'Assessment Code', visible: true, width: "*", minWidth: 120, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                {
                    field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Active?', visible: false, width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Row Code', visible: false, width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'Load Id', visible: false, width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                    displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_timestamp_filter'
                },
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }
               /* {
                    field: 'close filter',displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE,minWidth:22,width:22
                },*/

        ],
        constOrgNameColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Shared/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
            {
                field: 'cnst_org_nm', width: "*", minWidth: 220, maxWidth: 9000, displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
             {
                 field: 'cln_cnst_org_nm', width: "*", minWidth: 220, maxWidth: 9000, displayName: "Clean Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
             {
                 field: 'best_org_nm_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                 displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 120, maxWidth: 9000,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes<!--<img src="Images/Medal First Place-52.png" width="25" height="25" />--></div>'
             },
             {
                 field: 'cnst_org_nm_typ_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                 displayName: 'Name Type', visible: false, width: "*", minWidth: 130, maxWidth: 9000,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
             {
                 field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                 displayName: 'Source System', width: "*", minWidth: 140, maxWidth: 9000,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
             },
            {
                field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                displayName: 'Source System ID', cellTemplate: '<span class="wordwrap">{{COL_FIELD}}</span>', width: "*", minWidth: 160, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
             {
                 field: 'cnst_org_nm_strt_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                 displayName: 'Start Date', sort: { direction: uiGridConstants.DESC, priority: 0 }, visible: false, width: "*", minWidth: 140, maxWidth: 9000,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellFilter: 'column_date_filter'
             },
            {
                field: 'cnst_org_nm_end_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                displayName: 'End Date', visible: false, width: "*", minWidth: 130, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                displayName: 'Active?', visible: false, width: "*", minWidth: 90, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                displayName: 'Row Code', visible: false, width: "*", minWidth: 130, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                displayName: 'Load Id', visible: false, width: "*", minWidth: 80, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ORG_NAME, $rootScope),
                displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_timestamp_filter'
            },
            {
                field: 'Action', cellTemplate: 'App/Shared/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
            }


        ],

        constAddressColDef: [
                //{ field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                //{
                //    field: 'cnst_addr_line1_addr', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                //    displayName: 'Address Line 1', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 150, maxWidth: 9000,
                //    filter: {
                //        condition: uiGridConstants.filter.STARTS_WITH
                //    },
                //    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                //},
                { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Shared/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
                {
                    field: 'cnst_addr_line1_addr', cellTemplate: '<div class="wordwrap" style="float:left;">{{COL_FIELD}}</div><div ng-show="row.entity.cnst_srcsys_id != null && row.entity.arc_srcsys_cd != null && row.entity.inactive_ind != \'-1\'" style="float:left; margin-top:10px;"><img ng-src="{{grid.appScope.getImagePath(\'new\')}}" ng-show="row.entity.cnst_srcsys_id == row.entity.sel_cnst_srcsys_id && row.entity.arc_srcsys_cd == row.entity.sel_arc_srcsys_cd" style=" float:left;margin-left:5px;" /></div>', 
                    displayName: 'Address Line ', width: "*", minWidth: 180, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{row.entity.cnst_addr_line1_addr}} {{row.entity.cnst_addr_line2_addr}}</div>'
                },
                {
                    field: 'cnst_addr_city_nm', menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'City', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_addr_state_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'State', width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_addr_zip_5_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Zip', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'cnst_addr_zip_4_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Zip 4', width: "*", minWidth: 70, maxWidth: 9000, visible: false,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'best_addr_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                     displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 60, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes<!--<img src="Images/Medal First Place-52.png" width="25" height="25" />--></div>'
                 },
                {
                    field: 'addr_typ_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    displayName: 'Address Type', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 130, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                     displayName: 'Source System', visible: true, width: "*", minWidth: 140, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                 },
                {
                    field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    displayName: 'Source System ID', visible: true, width: "*", minWidth: 160, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                  {
                      field: 'assessmnt_ctg', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                      displayName: 'Assessment Code', visible: true, width: "*", minWidth: 120, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                  },
                  {
                      field: 'res_deliv_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                      displayName: 'Delivery Type', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 60, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'Y\' " >Residential</div> <div class="wordwrap" ng-if="COL_FIELD == \'N\' " >Business</div>'
                  },
              
                 {
                     field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                     displayName: 'Active?', visible: false, width: "*", minWidth: 90, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                  {
                      field: 'res_deliv_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                      displayName: 'Residential Indicator', visible: false, width: "*", minWidth: 110, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                  },
                  {
                      field: 'dpv_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                      displayName: 'DPV Code', visible: false, width: "*", minWidth: 90, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                  },
                 
                 
                  {
                      field: 'cnst_addr_strt_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                      displayName: 'Start Date', visible: false, width: "*", minWidth: 140, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellFilter: 'column_date_filter'
                  },
                  {
                      field: 'cnst_addr_end_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                      displayName: 'End Date', visible: false, width: "*", minWidth: 130, maxWidth: 9000,
                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellFilter: 'column_date_filter'
                  },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    displayName: 'Row Code', visible: false, width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    displayName: 'Load Id', visible: false, width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },

                {
                    field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_ADDRESS, $rootScope),
                    displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_timestamp_filter'
                },
                {
                    field: 'Action', cellTemplate: 'App/Shared/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }

                //{
                //    field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                //}
        ],
        constPhoneColDef: [
               { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Shared/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
               {
                   field: 'cnst_phn_num', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Phone Number', width: "*", minWidth: 150, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'best_phn_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                   displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 60, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes<!--<img src="Images/Medal First Place-52.png" width="25" height="25" />--></div>'
               },
                 {
                     field: 'phn_typ_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Phone Type', width: "*", minWidth: 60, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
               {
                   field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', minWidth: 120, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
               },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'assessmnt_ctg', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                    displayName: 'Assessment Code', visible: true, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_phn_strt_ts', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', visible: false, width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'cnst_phn_end_dt', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope), width: "*", minWidth: 100, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date', visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },                 
                 {
                     field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                     displayName: 'Active?', visible: false, width: "*", minWidth: 90, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                     displayName: 'Row Code', visible: false, width: "*", minWidth: 60, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                    displayName: 'Load Id', visible: false, width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },

                {
                    field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_PHONE, $rootScope),
                    displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_timestamp_filter'
                },
                 {
                     field: 'Action', cellTemplate: 'App/Shared/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                     width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                 }
        ],
        constEmailColDef: [

              { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Shared/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
               // { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                
                {
                    field: 'cnst_email_addr', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Email Address', width: "*", minWidth: 160, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'best_email_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 40, maxWidth: 100,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes<!--<img src="Images/Medal First Place-52.png" width="25" height="25" />--></div>'
                },
                {
                    field: 'email_typ_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Email Type', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', minWidth: 120, maxWidth: 9000,
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                   
                },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'assessmnt_ctg', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    displayName: 'Assessment Code', visible: true, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'email_key', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Email Key', width: "*", minWidth: 90, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'cnst_email_strt_ts', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', visible: false, width: "*", minWidth: 140, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellFilter: 'column_date_filter'
                 },
                {
                    field: 'cnst_email_end_dt', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date', visible: false, width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'cnst_email_best_los_ind', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,visible:false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },


                {
                    field: 'cnst_email_validtn_dt', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Assess Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'

                },
               
                 {
                     field: 'cnst_email_validtn_method', width: "*", minWidth: 200, maxWidth: 9000,
                     displayName: "Assess method", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },

                 },
                {
                    field: 'domain_corrctd_ind', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Domain Correction Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,visible:false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'act_ind', width: "*", minWidth: 60, maxWidth: 80,
                    displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                   {
                       field: 'cntct_stat_typ_cd', width: "*", minWidth: 80, maxWidth: 9000,
                       displayName: "State Type", headerCellTemplate: GRID_HEADER_TEMPLATE,visible:false,
                       filter: {
                           condition: uiGridConstants.filter.STARTS_WITH
                       },

                   },
                {
                    field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },

                 {
                     field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                     displayName: 'Row Code', visible: false, width: "*", minWidth: 60, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    displayName: 'Load Id', visible: false, width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },

                {
                    field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_EMAIL, $rootScope),
                    displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_timestamp_filter'
                },
                 {
                     field: 'Action', cellTemplate: 'App/Shared/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div class="ui-grid-cell-contents wordwrap">User Actions</div>',
                     width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                 }
                 //{
                 //    field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                 //}
        ],
        constExtBridgeColDef: [
                { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false, visible: false },
                {
                    field: 'cnst_nm', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 120, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Name',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'address', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 200, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Address',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'source_system_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 140, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.source_system_cd}}</div>'
                },
                {
                    field: 'source_system_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 140, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srch_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 140, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Secondary ID', visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
            {
                field: 'dw_srcsys_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_timestamp_filter'
            },
            {
                field: 'row_stat_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Stat Code', width: "*", minWidth: 140, maxWidth: 9000, visible: true,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field:'record_type',visible:false
            },
            //{
            //    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'Status', headerCellTemplate: '<div>User Actions</div>',
            //    width: "*", minWidth: 325, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
            //}
            //,
            //     {
            //         field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
            //     }
        ],
        constBirthColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
               // { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                {
                    field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_BIRTH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_BIRTH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_birth_mth_num', menuItems: customGridFilter(HOME_CONSTANTS.CONST_BIRTH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Birth Month', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_birth_dy_num', menuItems: customGridFilter(HOME_CONSTANTS.CONST_BIRTH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Birth Day', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
                {
                    field: 'cnst_birth_yr_num', menuItems: customGridFilter(HOME_CONSTANTS.CONST_BIRTH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Birth Year', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
                {
                    field: 'cnst_birth_strt_ts', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Birth Start date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }, //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'cnst_birth_end_dt', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Birth End date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                    //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 60, maxWidth: 9000,
                    displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }
                //,
                // {
                //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                // }
        ],
        constDeathColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
               // { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                {
                    field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_DEATH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_DEATH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_death_dt', menuItems: customGridFilter(HOME_CONSTANTS.CONST_DEATH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Death Date', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'cnst_deceased_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_DEATH, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Deceased Code', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               

                {
                    field: 'cnst_death_strt_ts', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }, //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'cnst_death_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Row Stat Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }
                //,
                // {
                //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                // }
        ],
        constPrefColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
               // { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                {
                    field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PREF, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PREF, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cntct_prefc_typ_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PREF, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Preference Type', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cntct_prefc_val', menuItems: customGridFilter(HOME_CONSTANTS.CONST_PREF, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Preference Value', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
                {
                    field: 'cnst_cntct_prefc_strt_ts', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,

                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                    cellFilter: 'column_date_filter'

                },
			    {
			        field: 'cnst_cntct_prefc_end_ts', width: "*", minWidth: 100, maxWidth: 9000,
			        displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
			        filter: {
			            condition: uiGridConstants.filter.STARTS_WITH
			        }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

			        cellFilter: 'column_date_filter'

			    },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 60, maxWidth: 9000,
                    displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 125, maxWidth: 9000 //, headerCellTemplate: GRID_FILTER_TEMPLATE, 
                }

        ],
        characteristicsColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
              //  { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
               {
                   field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.CHARACTERISTICS, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 100, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
               },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CHARACTERISTICS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_chrctrstc_typ_cd', menuItems: customGridFilter(HOME_CONSTANTS.CHARACTERISTICS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Characteristic Type Code', width: "*", minWidth: 150, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_chrctrstc_val', menuItems: customGridFilter(HOME_CONSTANTS.CHARACTERISTICS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Charateristic Value', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
                {
                    field: 'cnst_chrctrstc_strt_dt', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'cnst_chrctrstc_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 60, maxWidth: 9000,
                    displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },

                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 325, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }
                //,
                // {
                //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                // }
        ],
        grpMembershipColDef: [
              //  { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
              {
                  field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 120, maxWidth: 9000, visible: true,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
              },
                {
                    field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source Sys ID', width: "*", minWidth: 100, maxWidth: 9000, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               {
                   field: 'grp_typ', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Type', width: "*", minWidth: 140, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'grp_nm', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Name', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'grp_cd', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Code', width: "*", minWidth: 100, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                {
                    field: 'assgnmnt_mthd', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Assignment Method', width: "*", minWidth: 140, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
              
                {
                    field: 'created_dt', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Created Date', width: "*", minWidth: 130, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'created_by', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Created By', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
               
                {
                    field: 'grp_owner', menuItems: customGridFilter(HOME_CONSTANTS.GRP_MEMBERSHIP, $rootScope),
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
                     field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                     width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                 }
                 //,
                 //{
                 //    field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                 //}
        ],
        transHistoryColDef: [
             //{
             //    field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
             //    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', visible: true,
             //    filter: {
             //        condition: uiGridConstants.filter.STARTS_WITH
             //    },
             //    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             //    width: "*", minWidth: 140, maxWidth: 9000
             //},
             //   {
             //       field: 'cnst_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
             //       headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', visible: true,
             //       filter: {
             //           condition: uiGridConstants.filter.STARTS_WITH
             //       },
             //       cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             //       width: "*", minWidth: 140, maxWidth: 9000
             //   },
             {
                 field: 'cnst_mstr_id', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                 headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Master ID', enableCellEdit: false,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                 width: "*", minWidth: 90, maxWidth: 9000
             },
                {
                    field: 'trans_key', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Key',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 90, maxWidth: 9000
                },
                {
                    field: 'trans_typ_dsc', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Type',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 200, maxWidth: 9000
                },
                {
                    field: 'sub_trans_typ_dsc', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Sub Transaction Type',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                    ,
                    width: "*", minWidth: 200, maxWidth: 9000
                },
                {
                    field: 'sub_trans_actn_typ', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Action Type',visible:false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 100, maxWidth: 9000
                },
                {
                    field: 'trans_stat', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Status',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 160, maxWidth: 9000
                },
                {
                    field: 'trans_note', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Note', visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 250, maxWidth: 9000
                },
                {
                    field: 'user_id', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User Name', visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 100, maxWidth: 9000
                },
                {
                    field: 'approved_by', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Approved By', visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 140, maxWidth: 9000
                },
                {
                    field: 'approved_dt', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Approved Date', visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter',
                    width: "*", minWidth: 160, maxWidth: 9000
                },
               
                {
                    field: 'trans_create_ts', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                    cellFilter: 'column_date_filter',
                    width: "*", minWidth: 140, maxWidth: 9000
                },
                {
                    field: 'trans_last_modified_ts', menuItems: customGridFilter(HOME_CONSTANTS.TRANS_HISTORY, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Last Modified Date', visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 140, maxWidth: 9000,

                    cellFilter: 'column_date_filter'
                }
                //,
                // {
                //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                // }
        ],
        anonInfoColDef: [
             //   { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
          
            {
                field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 120, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
            }, 
            {
                field: 'cnst_chrctrstc_val', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Anonymous Indicator', width: "*", minWidth: 100, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
              {
                  field: 'cnst_mstr_id', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Master ID', width: "*", minWidth: 100, maxWidth: 9000,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
            {
                field: 'cnst_chrctrstc_strt_dt', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', width: "*", minWidth: 100, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'cnst_chrctrstc_end_dt', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date', width: "*", minWidth: 140, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'line_of_service_cd', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Line of Service Code', width: "*", minWidth: 100, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load Id', width: "*", minWidth: 80, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dw_srcsys_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_timestamp_filter'
            },
            {
                field: 'row_stat_cd', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code', width: "*", minWidth: 60, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Trans Key', width: "*", minWidth: 100, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'user_id', menuItems: customGridFilter(HOME_CONSTANTS.ANON_INFO, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User Id', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            }
            //,
            //{
            //    field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
            //}
        ],
        masterAttemptColDef: [
               // { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                {
                    field: 'arc_srcsys_cd', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                },
                {
                    field: 'srcsys_unique_id', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               {
                   field: 'cnst_mstr_id', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Master Id', width: "*", minWidth: 60, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'new_cnst_ind', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'New Constituent Ind', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'min_quality_ind', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Minimum Quality Ind', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'sent_to_ln_ind', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Send to LN Ind', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_dsp_id', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LN ID', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_dsp_id_ts', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LN ID Timestamp', visible: true, width: "*", minWidth: 180, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_timestamp_filter'
                },
               
                {
                    field: 'row_stat_cd', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code', width: "*", minWidth: 60, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'load_id', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load ID', width: "*", minWidth: 60, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'dw_srcsys_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.MASTER_ATTEMPT, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    cellFilter: 'column_date_timestamp_filter'
                }
                //,
                // {
                //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                // }

        ],
        intBridgeColDef: [
              //  { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
               {
                   field: 'source_system_cd', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System', width: "*", minWidth: 120, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'source_system_id', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Id', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.arc_srcsys_cd}}</div>'
                },
                {
                    field: 'cnst_nm', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Name', width: "*", minWidth: 150, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'address', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Address', width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_act_ind', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Active Ind', width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               
                {
                    field: 'row_stat_cd', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Code', width: "*", minWidth: 60, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'load_id', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Load ID', width: "*", minWidth: 100, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'appl_src_cd', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Appl Source Code', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'dw_srcsys_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.INTERNAL_BRIDGE, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                    cellFilter: 'column_date_timestamp_filter'
                }
                //,
                // {
                //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
                // }
        ],
        mergeHistoryColDef: [
         //  { field: 'new_mstr_id', displayName: 'New Master Id', enableCellEdit: false },
           {
               field: 'cnst_mstr_id', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Old Master Id', width: "*", minWidth: 100, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'cnst_type', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Type', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'appl_src_cd', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Appl Source System', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'srcsys_cnst_uid', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'intnl_srcsys_grp_id', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group ID', width: "*", minWidth: 140, maxWidth: 9000,visible:false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'alert_type_cd', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Alert Type Code', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'alert_msg_txt', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Alert Msg Text', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'steward_actn_cd', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Steward Action Code', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'steward_actn_dsc', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Steward Action Desc', width: "*", minWidth: 140, maxWidth: 9000,visible:false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'new_mstr_id', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'New Master ID', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'cdi_batch_id', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'CDI Batch ID', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'cnst_dsp_id', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DSP ID', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'reprocess_ind', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Reprocess Ind', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'merge_sts_cd', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Merge Status Code', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'merge_msg_txt', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Merge Msg Text', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'trans_key', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Key', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'user_id', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User ID', width: "*", minWidth: 140, maxWidth: 9000, visible: true,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'trans_note', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Note', width: "*", minWidth: 140, maxWidth: 9000,visible:false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Time', width: "*", minWidth: 180, maxWidth: 9000,visible:false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellFilter: 'column_date_timestamp_filter'
           },
           {
               field: 'dw_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.MERGE_HISTORY, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000, visible: true,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellFilter: 'column_date_timestamp_filter'
           }
           //,
           // {
           //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
           // }

           //{field:'',cellTemplate:'<div class="dropdown"><button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">Dropdown Example<span class="caret"></span></button><ul class="dropdown-menu"><li><a href="#">JavaScript</a></li></ul></div>',displayName:'Edit/delete'}
        ],
        affiliatorColDef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
            {
                field: 'ent_org_id', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Enterprise Org Id', width: "*", minWidth: 140, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
           {
               field: 'cnst_mstr_id', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Master Id', width: "*", minWidth: 140, maxWidth: 9000,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
            {
                field: 'cnst_affil_strt_ts', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date', width: "*", minWidth: 140, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                cellFilter: 'column_date_filter'
            },
            {
                field: 'ent_org_name', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Enterprise Org Name', width: "*", minWidth: 200, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cln_cnst_org_nm', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Clean Org Name', width: "*", minWidth: 200, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },

            {
                field: 'row_stat_cd', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
                headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Status Code', width: "*", minWidth: 180, maxWidth: 9000,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
             {
                 field: 'dw_srcsys_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.AFFILIATOR, $rootScope),
                 headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellFilter: 'column_date_timestamp_filter'
             },

              {
                  field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDelete.tpl.html', displayName: ' ', headerCellTemplate: '<div></div>',
                  width: "*", minWidth: 325, maxWidth: 9000, headerCellTemplate: GRID_FILTER_TEMPLATE
              }
        ],
        summaryViewColDef: [
            {
                field: 'bzd_smry_typ_cd', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Summary Type", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_arc_deceased_cd', width: "*", minWidth: 80, maxWidth: 9000, displayName: "Deceased code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_prfx_nm', width: "*", minWidth: 80, maxWidth: 9000, displayName: "Prefix", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_f_nm', width: "*", minWidth: 100, maxWidth: 9000, displayName: "First Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_m_nm', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Middle Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_l_nm', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Last Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_sfx_nm', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Suffix", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_line_1_addr', width: "*", minWidth: 180, maxWidth: 9000, displayName: "Address Line 1", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_line_2_addr', width: "*", minWidth: 160, maxWidth: 9000, displayName: "Address Line 2", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_city_nm', width: "*", minWidth: 80, maxWidth: 9000, displayName: "City", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_st_cd', width: "*", minWidth: 80, maxWidth: 9000, displayName: "State", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_zip_5_cd', width: "*", minWidth: 60, maxWidth: 9000, displayName: "Zip", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_zip_4_cd', width: "*", minWidth: 60, maxWidth: 9000, displayName: "Zip 4", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_email_addr', width: "*", minWidth: 180, maxWidth: 9000, displayName: "Email Address", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.SUMMARY_VIEW, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },  
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            }
            //,
            // {
            //     field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
            // }
        ],
        masterDetailColDef: [

             {
                 field: 'cnst_mstr_id', width: "*", minWidth: 140, maxWidth: 9000, displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
              {
                  field: 'cnst_typ_cd', width: "*", minWidth: 140, maxWidth: 9000, displayName: "Constituent Type", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_dsp_id', width: "*", minWidth: 140, maxWidth: 9000, displayName: "DSP ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_hsld_id', width: "*", minWidth: 140, maxWidth: 9000, displayName: "Household ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_strt_dt', width: "*", minWidth: 140, maxWidth: 9000, displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter'
              },
              {
                  field: 'cnst_mstr_origin_dt', width: "*", minWidth: 140, maxWidth: 9000, displayName: "Origin Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter'
              },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000, displayName: "Application Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope), visible: false,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Row Status Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
             {
                 field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000, displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellFilter: 'column_date_timestamp_filter'
             },
             {
                 field: 'lst_bio_dntn_dt', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Last Bio Donation Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellFilter: 'column_date_filter'
             },
             {
                 field: 'lst_fr_dntn_dt', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Last Fundraising Donation Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellFilter: 'column_date_filter'
             },
              {
                  field: 'lst_phss_cours_cmpltn_dt', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Last PHSS Course Completion", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter'
              },
               {
                   field: 'lst_volntrng_dt', width: "*", minWidth: 100, maxWidth: 9000, displayName: "Last Volunteering Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   menuItems: customGridFilter(HOME_CONSTANTS.MASTER_DETAIL, $rootScope),
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellFilter: 'column_date_filter'
               }
        ],
        advCaseSearchColDef: [
            { field: 'case_key', displayName: 'Case Number' },
            {
                field: 'case_nm', displayName: 'Case Name', enableCellEdit: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                headerTooltip: 'Case Name',

            },
            { field: 'case_desc', headerTooltip: 'Case description', displayName: 'Case description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
            { field: 'ref_src_key', displayName: 'CRM System' },
            { field: 'ref_id', displayName: 'CRM System ID' },
            { field: 'typ_key', displayName: 'Type' },
            { field: 'cnst_nm', displayName: 'Constituent Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
            { field: 'crtd_by_usr_id', displayName: 'User Name' },
            { field: 'report_dt', displayName: 'Reported Date', cellFilter: 'column_date_filter' },
            { field: 'status', displayName: 'Status' }
        ],

        constEmailDomainColDef: [
              { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },
              {
                  field: 'email_domain', menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Email Domain', minWidth: 120, maxWidth: 9000,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
               {
                   field: 'act_indv_email_cnt', menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Count of Active Individual Emails', minWidth: 120, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'act_cnst_cnt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                    displayName: 'Count of Constituents', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'most_rcnt_email_ts', menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Most Recent', width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },                
               {
                   field: 'most_rcnt_vldtn_ts', menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Most Recent Validation', width: "*", minWidth: 150, maxWidth: 9000,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                    displayName: 'Active?', visible: false, width: "*", minWidth: 90, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.EMAIL_DOMAINS, $rootScope),
                    displayName: 'Row Code', visible: false, width: "*", minWidth: 60, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },             
                {
                    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDownEmailDomain.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }
        ],

        showBirthColdef: [
             {
                 field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
              {
                  field: 'arc_srcsys_cd', width: "*", minWidth: 120, maxWidth: 9000,
                  displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_birth_dy_num', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Birth day", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_birth_mth_num', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Birth Month", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            }
                ,
            {
                field: 'cnst_birth_yr_num', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Birth year", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_birth_strt_ts', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Birth Start date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                cellFilter: 'column_date_filter'
                
            },
            {
                field: 'cnst_birth_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Birth End date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
                //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_birth_best_los_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                //  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "DW Trans  Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_timestamp_filter'
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Row Stat Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
             {
                 field: 'user_id', width: "*", minWidth: 120, maxWidth: 9000,
                 displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             }
        ],
        showDeathColdef: [
            {
                field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_death_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Death Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },

            {
                field: 'cnst_deceased_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Deceased Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_death_strt_ts', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_death_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'
            },
            {
                field: 'cnst_death_best_los_ind', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'user_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "DW Trans timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                cellFilter: 'column_date_timestamp_filter'

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            }
        ],
        showEmailColdef: [
            {
                field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
               {
                   field: 'email_key', width: "*", minWidth: 130, maxWidth: 9000,
                   displayName: "Email Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
            {
                field: 'cnst_email_addr', width: "*", minWidth: 200, maxWidth: 9000,
                displayName: "Email Address", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
                {
                    field: 'email_typ_cd', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Email Type", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
            {
                field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
               {
                   field: 'cnst_srcsys_id', width: "*", minWidth: 150, maxWidth: 9000,
                   displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
            {
                field: 'cnst_email_best_los_ind', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'best_email_ind', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Arc Best Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_email_strt_ts', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_email_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_email_validtn_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Assess Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
             {
                 field: 'cnst_email_validtn_ind', width: "*", minWidth: 140, maxWidth: 9000,
                 displayName: "Assess Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
             {
                 field: 'cnst_email_validtn_method', width: "*", minWidth: 200, maxWidth: 9000,
                 displayName: "Assess method", headerCellTemplate: GRID_HEADER_TEMPLATE, 
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
               {
                   field: 'domain_corrctd_ind', width: "*", minWidth: 130, maxWidth: 9000,
                   displayName: "Domain Correction Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
            {
                field: 'act_ind', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
               {
                   field: 'cntct_stat_typ_cd', width: "*", minWidth: 120, maxWidth: 9000,
                   displayName: "State Type", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
               {
                   field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                   displayName: "DW Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                   cellFilter: 'column_date_timestamp_filter'

               },
                {
                    field: 'inactive_ind', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Inactive Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'is_previous', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Is Previous", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'load_id', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'strx_row_stat_cd', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "Strx Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'trans_key', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'trans_status', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Trans Status", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'user_id', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                }
        ],
        showNameColdef: [
                {
                    field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'best_prsn_nm_ind', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Best Person Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'appl_src_cd', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'arc_srcsys_cd', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_alias_in_saltn_nm', width: "*", minWidth: 200, maxWidth: 9000,
                    displayName: "Inside Salutation Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_alias_out_saltn_nm', width: "*", minWidth: 200, maxWidth: 9000,
                    displayName: "Outside Salutation Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_nm_strt_dt', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'cnst_prsn_first_nm', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "First Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_middle_nm', width: "*", minWidth: 130, maxWidth: 9000,
                    displayName: "Middle Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_last_nm', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "Last Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_full_nm', width: "*", minWidth: 200, maxWidth: 9000,
                    displayName: "Full Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_mom_maiden_nm', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "Maiden Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_nick_nm', width: "*", minWidth: 160, maxWidth: 9000,
                    displayName: "Nick Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_nm_best_los_ind', width: "*", minWidth: 90, maxWidth: 9000,
                    displayName: "LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_nm_end_dt', width: "*", minWidth:100, maxWidth: 9000,
                    displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'cnst_prsn_nm_typ_cd', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_prefix_nm', width: "*", minWidth: 130, maxWidth: 9000,
                    displayName: "Prefix Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_suffix_nm', width: "*", minWidth: 130, maxWidth: 9000,
                    displayName: "Suffix Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_prsn_seq', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Seq num", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'cnst_srcsys_id', width: "*", minWidth: 150, maxWidth: 9000,
                    displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                    displayName: "DW Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_timestamp_filter'

                },
                {
                    field: 'line_of_service_cd', width: "*", minWidth: 150, maxWidth: 9000,
                    displayName: "Line of Service", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'locator_prsn_nm_key', width: "*", minWidth: 150, maxWidth: 9000,
                    displayName: "Person Name Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'trans_key', width: "*", minWidth: 120, maxWidth: 9000,
                    displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'user_id', width: "*", minWidth: 150, maxWidth: 9000,
                    displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
        ],
        showPhoneColdef: [
             {
                 field: 'cnst_mstr_id', width: "*", minWidth: 130, maxWidth: 9000,
                 displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
            {
                field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

            },
            {
                field: 'cnst_phn_num', width: "*", minWidth: 200, maxWidth: 9000,
                displayName: "Phone Number", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_phn_extsn_num', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Extension Number", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'phn_typ_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Phone Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'best_phn_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Arc Best Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_phn_best_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "LN Best Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_phn_best_los_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_phn_strt_ts', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_filter'
            },
            {
                field: 'cnst_phn_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
                 {
                     field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(HOME_CONSTANTS.CONST_NAME, $rootScope),
                     displayName: 'Active?', width: "*", minWidth: 90, maxWidth: 9000,
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
            {
                field: 'cntct_stat_typ_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Status type", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "DW Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                cellFilter: 'column_date_timestamp_filter'
            },
            {
                field: 'locator_phn_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Phone Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Row Stat code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'user_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
        ],
        showPrefColdef: [
            {
                field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Master Id ", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'act_ind', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
             {
                 field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                 displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
              {
                  field: 'cnst_srcsys_id', width: "*", minWidth: 140, maxWidth: 9000,
                  displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

              },
              {
                  field: 'cnst_cntct_prefc_end_ts', width: "*", minWidth: 140, maxWidth: 9000,
                  displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                  cellFilter: 'column_date_filter'

              },
            {
                field: 'cnst_cntct_prefc_strt_ts', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_filter'

            },
           
            {
                field: 'cntct_prefc_typ_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cntct_prefc_val', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Pref Value", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
             {
                 field: 'dw_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                 displayName: "DW Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                 cellFilter: 'column_date_timestamp_filter'
             },
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
             {
                 field: 'row_stat_cd', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Row Stat Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
             {
                 field: 'srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                 displayName: "Source System Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                 cellFilter: 'column_date_timestamp_filter'
             },
             {
                 field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
            {
                field: 'user_id', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },              
        ],
        showInternalBridgeColdef: [
              {
                  field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                  displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },

              },
              {
                  field: 'cnst_mstr_subj_area_cd', width: "*", minWidth: 140, maxWidth: 9000,
                  displayName: "Subject Area", headerCellTemplate: GRID_HEADER_TEMPLATE,
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },

              },
               {
                   field: 'cnst_mstr_subj_area_id', width: "*", minWidth: 140, maxWidth: 9000,
                   displayName: "Subject Area Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

               },
             {
                 field: 'cnst_act_ind', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
               {
                   field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                   displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },

                   cellFilter: 'column_date_timestamp_filter'
               },
                {
                    field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                    displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Row Stat Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },
        ],

        showCharacteristicsColdef: [
             {
                 field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
             {
                 field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                 displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

            },
            {
                field: 'cnst_chrctrstc_seq', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Case Seq", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_chrctrstc_typ_cd', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_chrctrstc_val', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Value", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_chrctrstc_strt_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_chrctrstc_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
             {
                 field: 'appl_src_cd', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "Dw Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_timestamp_filter'
            },
            {
                field: 'line_of_service_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Line of Service", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Row Stat Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'user_id', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            }
               
        ],
        showExtBridgeColdef: [
            {
                field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 160, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

            },
            {
                field: 'cnst_srcsys_scndry_id', width: "*", minWidth: 160, maxWidth: 9000,
                displayName: "Secondary Source System Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_act_ind', width: "*", minWidth: 130, maxWidth: 9000,
                displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_timestamp_filter'

            },
            {
                field: 'load_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            }


        ],
        showOrgNameColdef: [
            {
                field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

            },
            {
                field: 'cnst_org_nm_typ_cd', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_org_nm_seq', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Name Seq", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_org_nm', width: "*", minWidth: 220, maxWidth: 9000,
                displayName: "Org Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cln_cnst_org_nm', width: "*", minWidth: 220, maxWidth: 9000,
                displayName: "Clean Org Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_org_nm_strt_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_org_nm_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "End Date ", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },     
            {
                field: 'best_org_nm_ind', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Arc Best Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_org_nm_best_los_ind', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_timestamp_filter'

            },
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'user_id', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
              
        ],
        showAddressColdef: [
            {
                field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Master ID ", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'arc_srcsys_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_srcsys_id', width: "*", minWidth: 160, maxWidth: 9000,
                displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'addr_typ_cd', width: "*", minWidth: 130, maxWidth: 9000,
                displayName: "Address Type", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'arc_srcsys_uid', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Arc Source Uid", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_line1_addr', width: "*", minWidth: 200, maxWidth: 9000,
                displayName: "Address Line1", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_line2_addr', width: "*", minWidth: 200, maxWidth: 9000,
                displayName: "Address Line2", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_city_nm', width: "*", minWidth: 150, maxWidth: 9000,
                displayName: "City ", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_state_cd', width: "*", minWidth: 80, maxWidth: 9000,
                displayName: "Address Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_zip_5_cd', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Zip ", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_zip_4_cd', width: "*", minWidth: 90, maxWidth: 9000,
                displayName: "Zip 4", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_county_nm', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "County Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_country_cd', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Country code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_longitude', width: "*", minWidth: 160, maxWidth: 9000,
                displayName: "Longitude", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_latitude', width: "*", minWidth: 160, maxWidth: 9000,
                displayName: "Latitude", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_non_us_pstl_c', width: "*", minWidth: 160, maxWidth: 9000,
                displayName: "Non US Postal code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_prefd_ind', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Pref Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'locator_addr_key', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Address Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_strt_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_addr_end_dt', width: "*", minWidth: 130, maxWidth: 9000,
                displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_addr_undeliv_ind', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Undelivered Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_unserv_ind', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Unserv Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_carrier_route', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Carrier Route", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_clssfctn_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Classification Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'best_addr_ind', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Arc Best Address Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_best_los_ind', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_ff_mov_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Freq Move Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'cnst_addr_group_ind', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Group Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                cellFilter: 'column_date_timestamp_filter'

            },
            {
                field: 'load_id', width: "*", minWidth: 80, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'appl_src_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
            {
                field: 'user_id', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

            },
                            
                            


        ],
        showEmailDomainColdef: [
            { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Constituent/Views/common/iconGrid.tpl.html', enableCellEdit: false, },

{
    field: 'email_domain', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "Email Domain", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'act_indv_email_cnt', width: "*", minWidth: 160, maxWidth: 9000,
    displayName: "Count of Active Individual Emails", headerCellTemplate: GRID_HEADER_TEMPLATE,
filter: {
    condition: uiGridConstants.filter.STARTS_WITH
},
cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'act_cnst_cnt', width: "*", minWidth: 130, maxWidth: 9000,
    displayName: "Count of Constituents", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'most_rcnt_email_ts', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "Most Recent", headerCellTemplate: GRID_HEADER_TEMPLATE,
filter: {
    condition: uiGridConstants.filter.STARTS_WITH
},
cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'most_rcnt_vldtn_ts', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Most Recent Validation", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
 {
     field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
     width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
 }
],
        showNAICSCodesColdef: [
{
    field: 'naics_cd', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "NAICS Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'naics_indus_title', width: "*", minWidth: 160, maxWidth: 9000,
    displayName: "Title", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'naics_indus_dsc', width: "*", minWidth: 130, maxWidth: 9000,
    displayName: "Description", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'conf_weightg', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "Confidence Weightage", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'rule_keywrd', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Rule Keyword", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'sts', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Status", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

}
],
        showContactsDetailsColdef: [
{
    field: 'indv_mstr_id', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'rlshp_typ', width: "*", minWidth: 160, maxWidth: 9000,
    displayName: "Context/Relationship", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'name', width: "*", minWidth: 130, maxWidth: 9000,
    displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'address', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "Address", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'phone_number', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Phone", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'email_address', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Email", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

}
        ]
,
        showAlternateIdsDetailsColdef: [

{
    field: 'arc_srcsys_cd', width: "*", minWidth: 160, maxWidth: 9000,
    displayName: "Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'cnst_srcsys_id', width: "*", minWidth: 130, maxWidth: 9000,
    displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'cnst_srcsys_scndry_id', width: "*", minWidth: 140, maxWidth: 9000,
    displayName: "Secondary Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'alt_arc_srcsys_cd', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Alternate Source System", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'alt_cnst_srcsys_id', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Alternate Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'alt_cnst_srcsys_scndry_id', width: "*", minWidth: 200, maxWidth: 9000,
    displayName: "Alternate Secondary Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'cnst_alt_id_strt_ts', width: "*", minWidth: 200, maxWidth: 9000, visible: false,
    displayName: "Start Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'cnst_alt_id_end_dt', width: "*", minWidth: 200, maxWidth: 9000, visible: false,
    displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
{
    field: 'dw_trans_ts', width: "*", minWidth: 200, maxWidth: 9000, visible: false,
    displayName: "DW Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
    filter: {
        condition: uiGridConstants.filter.STARTS_WITH
    },
    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'

},
        ],
        constPotentialMergeColDef: [
              { field: 'pot_merge_mstr_id', displayName: 'Master Id', enableCellEdit: false },
              {
                  field: 'pot_merge_mstr_nm', menuItems: customGridFilter(HOME_CONSTANTS.CONST_POTENTIALMERGE, $rootScope), width: "*", minWidth: 120, maxWidth: 9000,
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Name',
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'address', menuItems: customGridFilter(HOME_CONSTANTS.CONST_POTENTIALMERGE, $rootScope), width: "*", minWidth: 200, maxWidth: 9000,
                  headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Address',
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
               {
                   field: 'phone', menuItems: customGridFilter(HOME_CONSTANTS.CONST_POTENTIALMERGE, $rootScope), width: "*", minWidth: 200, maxWidth: 9000,
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Phone ',
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
                {
                    field: 'email', menuItems: customGridFilter(HOME_CONSTANTS.CONST_POTENTIALMERGE, $rootScope), width: "*", minWidth: 200, maxWidth: 9000,
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Email ',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'dsp_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_POTENTIALMERGE, $rootScope), width: "*", minWidth: 200, maxWidth: 9000,
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'LexID',
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 }
        ],
        constPotentialUnmergeColDef: [
               { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false, visible: false },
               {
                   field: 'cnst_addr_line1_addr', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 120, maxWidth: 9000,
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Address Line 1',
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'address', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 200, maxWidth: 9000,
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Address',
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'source_system_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 140, maxWidth: 9000,
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System',
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap" ng-show="row.entity.arc_srcsys_cd==\'CDIM\'">LEXNEX</div><div class="wordwrap" ng-show="row.entity.arc_srcsys_cd!=\'CDIM\'">{{row.entity.source_system_cd}}</div>'
               },
               {
                   field: 'source_system_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 140, maxWidth: 9000,
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System ID', visible: true,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
               {
                   field: 'srch_srcsys_id', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope), width: "*", minWidth: 140, maxWidth: 9000,
                   headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Secondary ID', visible: true,
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
           {
               field: 'dw_srcsys_trans_ts', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'DW Timestamp', width: "*", minWidth: 180, maxWidth: 9000, visible: false,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellFilter: 'column_date_timestamp_filter'
           },
           {
               field: 'row_stat_cd', menuItems: customGridFilter(HOME_CONSTANTS.CONST_EXT_BRIDGE, $rootScope),
               headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Stat Code', width: "*", minWidth: 140, maxWidth: 9000, visible: true,
               filter: {
                   condition: uiGridConstants.filter.STARTS_WITH
               },
               cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
           },
           {
               field: 'record_type', visible: false
           },
           //{
           //    field: 'Action', cellTemplate: 'App/Constituent/Views/common/gridDropDown.tpl.html', displayName: 'Status', headerCellTemplate: '<div>User Actions</div>',
           //    width: "*", minWidth: 325, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
           //}
           //,
           //     {
           //         field: 'close filter', displayName: ' ', headerCellTemplate: GRID_FILTER_TEMPLATE, minWidth: 22, width: 22
           //     }
        ]
    };

}

//Grid options are set same to all grids in multi , feel free to change by passing parameter so that it becomes grid specific
function Grid(columnDef) {
    this.columnDef = columnDef;
    var self = this;
    var softDeletedMessage = "Inactive/Soft-Deleted records cannot be edited.";
    var LNRecordsEditMessage = "LN Records cannot be edited.";
    var BBRecordMessage = "BB Records cannot be edited.";
    var activeChapterRecordsMessage = "Only active chapter relationships can be edited/removed.";
    var transaction = "Transaction";
    var smallSoftDeleted = "deleted";

    // if you want to add more parameters just and pass them where you are calling
    this.getGridOption = function (uiScrollOption, bestIndicatorParam, enableRowSelection, multiselect, paginationPageSize, multiSelectModifierKeys, enableRowHeaderSelection,
        enableSelectAll, enableColumnResize, enableGridMenu, enableFiltering) {
        //  var rowtpl = '<div ng-class="{\'greyClass\':true }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
        if (typeof bestIndicatorParam != 'undefined' && bestIndicatorParam != null) {
            var rowtpl = '<div ng-class="{\'greenClass\':row.entity.' + bestIndicatorParam + ' == \'2\' && !(row.entity.transNotes.length > \'0\' && !row.entity.transNotes == \'' + LNRecordsEditMessage  + '\' ),\'yellowClass\':row.entity.transNotes.length > \'0\'  && (row.entity.transNotes.indexOf(\'' + transaction + '\')==0),\'greyClass\':(row.entity.transNotes.length > \'0\' && row.entity.' + bestIndicatorParam + ' != \'2\' && (row.entity.transNotes == \'' + softDeletedMessage + '\'  || row.entity.transNotes.indexOf(\'' + smallSoftDeleted + '\')>0 )  ) || (row.entity.row_stat_cd && row.entity.' + bestIndicatorParam + ' != \'2\' && row.entity.row_stat_cd ==\'L\') }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
        }
        else {
            var rowtpl = '<div ng-class="{\'greyClass\':(row.entity.transNotes.length > \'0\' && (row.entity.transNotes == \'' + softDeletedMessage + '\' || row.entity.transNotes.indexOf(\'' + smallSoftDeleted + '\')>0)  ) || (row.entity.row_stat_cd && row.entity.row_stat_cd ==\'L\'),yellowClass:row.entity.transNotes.length > \'0\' && (row.entity.transNotes.indexOf(\'' + transaction + '\')==0) }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
        }


        return {
            enableRowSelection: typeof enableRowSelection !== 'undefined' ? enableRowSelection : false,
           // enableRowSelection: true,
            enableRowHeaderSelection: typeof enableRowHeaderSelection !== 'undefined' ? enableRowHeaderSelection : false,
            
          //  enableRowHeaderSelection: false,
            enableFiltering: typeof enableFiltering !== 'undefined' ? enableFiltering : false,
            enableSelectAll: typeof enableSelectAll !== 'undefined' ? enableSelectAll : false,
            enableColumnResize: typeof enableColumnResize !== 'undefined' ? enableColumnResize : true,
            //  selectionRowHeaderWidth: 35,
            rowHeight: 43,
            rowWidth:10000,
            rowTemplate: rowtpl,
            //disables checkbox of the row if request_transkey is greater than -1
            isRowSelectable: function (row) {
                if (row.entity.request_transaction_key >= 0) {
                    return false;
                } else {
                    return true;
                }
            },
            paginationPageSize: typeof paginationPageSize !== 'undefined' ? paginationPageSize : 10,
            paginationPageSizes:  typeof paginationPageSize == 10?[10,25,50]:[5,10,25,50],
            enablePaginationControls: false,
            enableVerticalScrollbar: 0,
            enableHorizontalScrollbar: typeof uiScrollOption !== 'undefined' ? uiScrollOption : 0,
            enableGridMenu: typeof enableGridMenu !== 'undefined' ? enableGridMenu : true,
            showGridFooter: false,
            columnDefs: this.columnDef,
            multiSelect: typeof multiselect !== 'undefined' ? multiselect : false,
            data: '',
            enableColumnResizing: true,
            enableColumnMoving: true,
            //For selective export
            modifierKeysToMultiSelect: typeof multiSelectModifierKeys !== 'undefined' ? multiSelectModifierKeys : false,
            //modifierKeysToMultiSelect: true,
            exporterCsvFilename: 'constituent_details.csv',
            exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
            exporterMenuPdf: false,
            multiselect: true
        }
    };
};


Grid.prototype.getGridLayout = function (gridOptions, result, type, uiScroll, BestIndicatorParam) {
    //  gridOptions.multiSelect = true;
    gridOptions.data = '';
    gridOptions.data.length = 0;
    gridOptions.data = result;

    if (gridOptions.columnDefs.length > 6) {
        gridOptions.enableHorizontalScrollbar = 1;
    }

    angular.element(document.getElementsByClassName(type)[0]).css('height', '0px');
    return gridOptions;
};


function customGridFilter(type, $rootScope) {
    return [
        {
            title: 'Filter',
            icon: 'ui-grid-icon-filter',
            action: function ($event) {
                //this method is declared in ConstMultiDetailsController
                // if (!angular.isUndefined(type))
                $rootScope.toggleFiltering(type);
                // else
                //  $rootScope.toggleHomeFilter();
            },

        }];
}

newAccMod.factory("ConstituentNAICSHelperService", [
    function () {
        return {            
            getnaicsEditPopupGridOptions: function () {
                var gridOption = {
                    paginationPageSize: 10,
                    enableSorting: true,
                    enablePager: false,
                    enableGridMenu: true,
                    enableFiltering: false,
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 0,
                    enableHorizontalScrollbar: 0,
                    showGridFooter: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 43,
                    paginationCurrentPage: 1,
                    totalItems: 0,
                    enableColumnResizing: true,
                    enableColumnMoving: true,
                    enableRowSelection: false,
                    enableRowHeaderSelection: false,
                    enableSelectAll: false
                };

                gridOption.data = '';
                return gridOption;
            },           
            getnaicsEditColDefs: function () {

                return [
                    { field: 'naics_cd', displayName: 'NAICS Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
					{ field: 'naics_indus_title', displayName: 'NAICS Title', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 9000 },
					{ field: 'naics_indus_dsc', displayName: 'NAICS Description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 9000 },
					{ field: 'conf_weightg', displayName: 'Confidence Weightage', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
					{ field: 'rule_keywrd', displayName: 'Rule Keyword', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
                    { field: 'sts', displayName: 'Status', width: "*", minWidth: 50, maxWidth: 9000 },
					{ field: 'manual_sts', displayName: 'User Actions', cellTemplate: 'App/Constituent/Views/common/naicsCodesCRUD/gridNAICSEditDropDown.tpl.html', width: "*", minWidth: 50, maxWidth: 9000 }
                ]
            },           
             
        }
    }]);

