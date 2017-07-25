function TransGridColumns() {

};

TransGridColumns.prototype.getGridColumns = function (uiGridConstants, $rootScope) {

    var GRID_HEADER_TEMPLATE = '<div ng-class="{ \'sortable\': sortable }" tooltip-placement="top-left" tooltip-append-to-body="true" uib-tooltip="{{col.displayName}}">' +
                                    '<div class="ui-grid-cell-contents " col-index="renderIndex" title="TOOLTIP">' +
                                        '<span class="ui-grid-header-cell-label headerWrap">{{ col.displayName CUSTOM_FILTERS }}</span>' +
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


        transPersonNameColDef: [
            {
                field: 'cnst_prsn_prefix_nm', width: "*", displayName: "Prefix", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_first_nm', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'First Name', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_middle_nm', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Middle Name', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_last_nm', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Last Name', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_suffix_nm', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Suffix', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_nm_typ_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Name Type', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_nick_nm', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Nick Name',  width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_prsn_mom_maiden_nm', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Maiden Name',  width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'best_prsn_nm_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes</div>'
            },
            {
                field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Source System', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Source System ID', cellTemplate: '<span class="wordwrap">{{COL_FIELD}}</span>', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'cnst_nm_strt_dt', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Start Date', sort: { direction: uiGridConstants.DESC, priority: 0 }, visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter',

                visible: false
            },
            {
                field: 'cnst_prsn_nm_end_dt', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'End Date', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter',

                visible: false
            },
            {
                field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Active Indicator', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Row Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Load Id', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter',
                //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                visible: false
            },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Application Source Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Is Previous', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Transaction Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Trans Status', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Inactive Indicator', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'STRX Row Status Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Unique Trans Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

        ],


        transOrgNameColDef: [
            {
                field: 'cnst_org_nm', width: "*", displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cln_cnst_org_nm', width: "*", displayName: "Clean Name", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'best_org_nm_ind', headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,
                displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes</div>'
            },
            {
                field: 'arc_srcsys_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Source System', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_org_nm_typ_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Name Type', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,
                displayName: 'Source System ID', cellTemplate: '<span class="wordwrap">{{COL_FIELD}}</span>', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },

            {
                field: 'cnst_org_nm_strt_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,
                displayName: 'Start Date', sort: { direction: uiGridConstants.DESC, priority: 0 }, width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'cnst_org_nm_end_dt', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'End Date', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },

            {
                field: 'cnst_org_nm_seq', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Org Name Sequence', sort: { direction: uiGridConstants.DESC, priority: 0 },width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_org_nm_best_los_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Org Name Best LOS Indicator', width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },


            {
                field: 'act_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Active Indicator', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Row Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Load Id', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter',
                //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                visible: false
            },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Application Source Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Is Previous', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Transaction Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Trans Status', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Inactive Indicator', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'STRX Row Status Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Unique Trans Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
        ],

        transAddressColDef: [
             {
                 field: 'cnst_mstr_id', width: "*", displayName: "Constituent Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
             {
                 field: 'cnst_addr_line1_addr', width: "*", displayName: "Address Line 1", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
              {
                  field: 'cnst_addr_line2_addr', width: "*", displayName: "Address Line 2", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_addr_city_nm', width: "*", displayName: "City", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_addr_state_cd', width: "*", displayName: "State", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_addr_zip_5_cd', width: "*", displayName: "Zip", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_addr_zip_4_cd', width: "*", displayName: "Zip4 Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'arc_srcsys_cd', width: "*", displayName: "Source System Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_srcsys_id', width: "*", displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_addr_prefd_ind', width: "*", displayName: "Preferred Address Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'assessmnt_ctg', width: "*", displayName: "Assessment Category", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'locator_addr_key', width: "*", displayName: "Locator Address Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'addr_typ_cd', width: "*", displayName: "Address Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'dpv_cd', width: "*", displayName: "DPV Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_strt_ts', width: "*", displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter',

                  visible: false
              },
              {
                  field: 'arc_srcsys_uid', width: "*", displayName: "Address Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_end_dt', width: "*", displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter',

                  visible: false
              },
              {
                  field: 'best_addr_ind', width: "*", displayName: "Best Address Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_group_ind', width: "*", displayName: "Group Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_clssfctn_ind', width: "*", displayName: "Classification Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_carrier_route', width: "*", displayName: "Carrier Route", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_county_nm', width: "*", displayName: "County Name", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_country_cd', width: "*", displayName: "Country", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_latitude', width: "*", displayName: "Latitude", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_longitude', width: "*", displayName: "Longitude", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_non_us_pstl_c', width: "*", displayName: "Non-US Postal Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_ff_mov_ind', width: "*", displayName: "Freq Move Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_unserv_ind', width: "*", displayName: "Unserv Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_undeliv_ind', width: "*", displayName: "Undeliv Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_addr_best_los_ind', width: "*", displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'res_deliv_ind', width: "*", displayName: "Residential Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'trans_key', width: "*", displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'act_ind', width: "*", displayName: "Active Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'user_id', width: "*", displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'dw_srcsys_trans_ts', width: "*", displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter',

                  visible: false
              },
              {
                  field: 'row_stat_cd', width: "*", displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'load_id', width: "*", displayName: "Load ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Application Source Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,
                displayName: 'Is Previous', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Transaction Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,
                displayName: 'Trans Status', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Inactive Indicator', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'STRX Row Status Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Unique Trans Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

        ],

        transPhoneColDef: [
			  {
			      field: 'cnst_mstr_id', width: "*", displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'cnst_phn_num', width: "*", displayName: "Phone Number", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'cnst_phn_extsn_num', width: "*", displayName: "Phone Extension Number", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'arc_srcsys_cd', width: "*", displayName: "Source System Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'cnst_srcsys_id', width: "*", displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'phn_typ_cd', width: "*", displayName: "Phone Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'cntct_stat_typ_cd', width: "*", displayName: "Contact Status Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'cnst_phn_best_ind', width: "*", displayName: "LN Best Phone Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
			  },
			  {
			      field: 'cnst_phn_strt_ts', width: "*", displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellFilter: 'date_only_filter'
			  },
			  {
			      field: 'cnst_phn_end_dt', width: "*", displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellFilter: 'date_only_filter'
			  },
			  {
			      field: 'best_phn_ind', width: "*", displayName: "ARC Best Phone Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
			  {
			      field: 'cnst_phn_best_los_ind', width: "*", displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
			  {
			      field: 'trans_key', width: "*", displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
			  {
			      field: 'act_ind', width: "*", displayName: "Active Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
			  {
			      field: 'user_id', width: "*", displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
			  {
			      field: 'dw_srcsys_trans_ts', width: "*", displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellFilter: 'column_date_filter',

			      visible: false
			  },
			  {
			      field: 'row_stat_cd', width: "*", displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
			  {
			      field: 'load_id', width: "*", displayName: "Load ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

			      filter: {
			          condition: uiGridConstants.filter.STARTS_WITH
			      },
			      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
			  },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Application Source Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Is Previous', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Transaction Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Trans Status', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Inactive Indicator', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'STRX Row Status Code', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,
                displayName: 'Unique Trans Key', visible: false, width: "*", minWidth: 120, maxWidth:900,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

        ],


        transEmailUploadColDef: [
              {
                  field: 'constituent_id', width: "*", displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'email_address', width: "*",  displayName: "Email Address", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'name', width: "*",  displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'address', width: "*",  displayName: "Address", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'addr_line_1', width: "*",  displayName: "Address Line 1", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'addr_line_2', width: "*",  displayName: "Address Line 2", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },

              {
                  field: 'city', width: "*",  displayName: "City", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'state_cd', width: "*",  displayName: "State", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'zip', width: "*",  displayName: "Zip Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'phone_number', width: "*",  displayName: "Phone Number", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              }

        ],

        transEmailColDef: [
              {
                  field: 'cnst_mstr_id', width: "*",  displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_email_addr', width: "*",  displayName: "Email Address", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'arc_srcsys_cd', width: "*",  displayName: "Source System Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_srcsys_id', width: "*",  displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'email_typ_cd', width: "*",  displayName: "Email Type", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'email_key', width: "*",  displayName: "Email Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'domain_corrctd_ind', width: "*",  displayName: "Domain Corrected Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cntct_stat_typ_cd', width: "*",  displayName: "Contact Status Type", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_best_email_ind', width: "*",  displayName: "LN Best Email Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_email_strt_ts', width: "*",  displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },
              {
                  field: 'cnst_email_end_dt', width: "*",  displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },
              {
                  field: 'best_email_ind', width: "*",  displayName: "ARC Best Email Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_email_validtn_dt', width: "*",  displayName: "Assess Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter',

                  visible: false
              },
              {
                  field: 'cnst_email_validtn_method', width: "*",  displayName: "Assess Method", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_email_validtn_ind', width: "*",  displayName: "Assess Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'cnst_email_best_los_ind', width: "*",  displayName: "Best LOS Ind", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },

              {
                  field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'act_ind', width: "*",  displayName: "Active Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'dw_srcsys_trans_ts', width: "*", displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter',

                  visible: false
              },
              {
                  field: 'row_stat_cd', width: "*",  displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'load_id', width: "*",  displayName: "Load ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Application Source Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Is Previous', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Transaction Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Trans Status', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Inactive Indicator', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'STRX Row Status Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Unique Trans Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

        ],
        transBirthColDef: [
            {
                field: 'cnst_mstr_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Master ID', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },

            {
                field: 'cnst_birth_dy_num',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Birth Day',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',width: "*"
            },
            {
                field: 'cnst_birth_mth_num',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Birth Month',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',width: "*"
            },
            {
                field: 'cnst_birth_yr_num',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Birth Year',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*"
            },
            {
                field: 'arc_srcsys_cd',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'ARC Source System Code', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_srcsys_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'ARC Source System ID', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_birth_strt_ts',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Birth TS', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'cnst_birth_end_dt',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Birth End Date', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'cnst_birth_best_los_ind',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Birth LOS Indicator', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Trans Key', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'user_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'User ID', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'DW Trans Timestamp', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter',

                visible: false
            },

            {
                field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Row Stat Cd', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Application Source Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Load ID', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Is Previous', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Transaction Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Trans Status', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Inactive Indicator', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'STRX Row Status Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Unique Trans Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
        ],
        transDeathColDef: [
            {
                field: 'cnst_mstr_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Master ID', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },

            {
                field: 'cnst_death_dt',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Death Date',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter',

                width: "*"
            },
            {
                field: 'cnst_deceased_cd',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Deceased Code',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*"
            },
            {
                field: 'cnst_death_strt_ts',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Death Start TS',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter',

                width: "*"
            },
            {
                field: 'arc_srcsys_cd',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'ARC Source System Code', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_srcsys_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'ARC Source System ID', width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_death_end_dt',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Death End Date', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'cnst_death_best_los_ind',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Death LOS Indicator', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Trans Key', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'user_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'User ID', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dw_srcsys_trans_ts',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'DW Trans TS', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'row_stat_cd',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Row Stat Code', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Application Source Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Load ID', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Is Previous', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Transaction Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Trans Status', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Inactive Indicator', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'STRX Row Status Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Unique Trans Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
        ],


        transContactPreferenceColDef: [
              {
                  field: 'cnst_mstr_id', width: "*",  displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cntct_prefc_typ_cd', width: "*",  displayName: "Contact Preference Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cntct_prefc_val', width: "*",  displayName: "Contact Preference Value", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'act_ind', width: "*",  displayName: "Active Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'arc_srcsys_cd', width: "*",  displayName: "Source System Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_srcsys_id', width: "*",  displayName: "Source System ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_cntct_prefc_strt_ts', width: "*",  displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },
              {
                  field: 'cnst_cntct_prefc_end_ts', width: "*",  displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },
              {
                  field: 'srcsys_trans_ts', width: "*",  displayName: "Source System Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'row_stat_cd', width: "*",  displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'load_id', width: "*",  displayName: "Load ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'dw_trans_ts', width: "*",  displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter',

                  visible: false
              },

                {
                    field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    displayName: 'Application Source Code', visible: false, width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                },
                {
                    field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    displayName: 'Is Previous', visible: false, width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Transaction Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Trans Status', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Inactive Indicator', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'STRX Row Status Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Unique Trans Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

        ],

        transAffiliatorTagsColDef: [
            {
                field: 'ent_org_id', width: "*",  displayName: "Superior Enterprise Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'tag_key', width: "*",  displayName: "Subordinate Enterprise Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'tag', width: "*",  displayName: "Relationship Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'start_dt', width: "*",  displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'end_dt', width: "*",  displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'dw_trans_ts', width: "*",  displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'row_stat_cd', width: "*",  displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            }
        ],


        transAffiliatorHierarchyColDef: [
            {
                field: 'superior_ent_org_name', width: "*",  displayName: "Superior Enterprise Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'subord_ent_org_name', width: "*",  displayName: "Subordinate Enterprise Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'rlshp_typ_cd', width: "*",  displayName: "Relationship Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'rlshp_typ_desc', width: "*",  displayName: "Relationship Type Description", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'superior_ent_org_key', width: "*",  displayName: "Superior Enterprise Org ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'subord_ent_org_key', width: "*",  displayName: "Subordinate Enterprise Org ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'start_dt', width: "*",  displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'end_dt', width: "*",  displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'

            },
            {
                field: 'dw_trans_ts', width: "*",  displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'row_stat_cd', width: "*",  displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', width: "*",  displayName: "Load ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,
                displayName: 'Application Source Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            }

        ],

        transAffiliatorColDef: [
              {
                  field: 'ent_org_id', width: "*",  displayName: "Enterprise Org ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'ent_org_name', width: "*",  displayName: "Enterprise Org Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cln_cnst_org_nm', width: "*",  displayName: "Clean Org Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_mstr_id', width: "*",  displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cnst_affil_strt_ts', width: "*",  displayName: "Affiliator Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },
              {
                  field: 'cnst_affil_end_ts', width: "*",  displayName: "Affiliator End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },

              {
                  field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },

              {
                  field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'dw_srcsys_trans_ts', width: "*",  displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter'
              },
              {
                  field: 'row_stat_cd', width: "*",  displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'load_id', width: "*",  displayName: "Load ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Application Source Code', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Is Previous',  width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Transaction Key', width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Trans Status',  width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Inactive Indicator', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'STRX Row Status Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Unique Trans Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

        ],


        transUnmergeRequestColDef: [
        {
            field: 'dw_request_tracking_key',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Request Tracking Key',
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'nk_request_case_id',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Case ID',
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'subj_area_cd',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Subject Area',
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'batch_id',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Batch ID', width: "*", 
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true,
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'request_actn_typ_cd',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Action Type', width: "*", 
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 120, maxWidth: 900
        },
        {
            field: 'request_create_ts', displayName: 'Create TS', headerCellTemplate: GRID_HEADER_TEMPLATE,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellFilter: 'date_only_filter',

        },
        {
            field: 'user_requesting', displayName: 'User Requesting', headerCellTemplate: GRID_HEADER_TEMPLATE,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
        {
            field: 'request_step_cd', displayName: 'Request Step Code', headerCellTemplate: GRID_HEADER_TEMPLATE, 
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },

        {
            field: 'request_reason', displayName: 'Request Reason', headerCellTemplate: GRID_HEADER_TEMPLATE, 
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
        {
            field: 'user_approving', displayName: 'User Approving', headerCellTemplate: GRID_HEADER_TEMPLATE, 
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
        {
            field: 'request_approved_ts', displayName: 'Request Approved TS', headerCellTemplate: GRID_HEADER_TEMPLATE,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellFilter: 'column_date_filter',

        },
        {
            field: 'dw_trans_ts', displayName: 'DW Trans TS', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellFilter: 'column_date_filter',
            //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
        {
            field: 'transaction_key', displayName: 'Transaction Key', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
        {
            field: 'user_id', displayName: 'User ID', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
        {
            field: 'trans_status', displayName: 'Transaction Status', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
            width: "*",minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },

        ],


        transUnmergeProcessColDef: [
        {
            field: 'dw_request_tracking_key',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Request Tracking Key',
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'arc_srcsys_cd',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System Code',
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'srcsys_cnst_uid',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Source System UID',
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
            width: "*", minWidth: 120, maxWidth: 900
        },
        {
            field: 'cnst_mstr_id',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Master ID', width: "*",
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 120, maxWidth: 900
        },
        {
            field: 'cdi_batch_id',
            headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'CDI Batch ID', width: "*",
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 120, maxWidth: 900
        },
        {
            field: 'cnst_typ_cd', displayName: 'Constituent Type Code', headerCellTemplate: GRID_HEADER_TEMPLATE,
            width: "*", minWidth: 120, maxWidth:900,
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
        },
         {
             field: 'valid_sts_ind', displayName: 'Valid Status Indicator', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
         },
         {
             field: 'unmerge_sts_ind', displayName: 'Unmerge Status Indicator', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
         },
         {
             field: 'unmerge_msg_txt', displayName: 'Unmerge Message Text', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
         },
         {
             field: 'unmerge_del_actn_cd', displayName: 'Delete Action Code', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
         },
         {
             field: 'unmerge_mstr_grp_rec_cnt', displayName: 'Master Group Record Count', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
         },
         {
             field: 'unmerge_srcsys_grp_rec_cnt', displayName: 'Source System Group Record Count', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             visible: false
         },

         {
             field: 'persistence_ind', displayName: 'Persistence Indicator', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             visible: false
         },
         {
             field: 'dw_trans_ts', displayName: 'DW Trans TS', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellFilter: 'column_date_filter',

             visible: false
         },
         {
             field: 'load_id', displayName: 'Load ID', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             visible: false
         },

         {
             field: 'transaction_key', displayName: 'Transaction Key', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             visible: false
         },
         {
             field: 'user_id', displayName: 'User ID', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             visible: false
         },
         {
             field: 'trans_status', displayName: 'Trans Status', headerCellTemplate: GRID_HEADER_TEMPLATE,
             width: "*", minWidth: 120, maxWidth:900,
             filter: {
                 condition: uiGridConstants.filter.STARTS_WITH
             },
             cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
             visible: false
         }
        ],

        transMergeColDef:[
            {
                field: 'cnst_mstr_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Master ID',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                width: "*"
            },
            {
                field: 'cnst_dsp_id',
                headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'DSP ID',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                width: "*"
            },
                {
                    field: 'cnst_type',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Constituent Type',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*"
                },
                {
                    field: 'intnl_srcsys_grp_id',
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Internal Source System Group ID', width: "*", minWidth: 120, maxWidth:900,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    }
                },
                {
                    field: 'merge_sts_cd',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900, displayName: 'Merge Status Code', width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'alert_type_cd', displayName: 'Alert Type Code', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'appl_src_cd', displayName: 'Source System Code', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srcsys_cnst_uid', displayName: 'Source System UID', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cdi_batch_id', displayName: 'CDI Batch ID', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },


                {
                    field: 'alert_msg_txt', displayName: 'Alert Message Text', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'reprocess_ind', displayName: 'Reprocess Indicator', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'merge_msg_txt', displayName: 'Merge Message Text', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'steward_actn_cd', displayName: 'Steward Action Code', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'user_id', displayName: 'User ID', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                },
                {
                    field: 'steward_actn_dsc', displayName: 'Steward Action Description', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                },
                {
                    field: 'transaction_key', displayName: 'Transaction Key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                },
                {
                    field: 'trans_status', displayName: 'Transaction Status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                    width: "*",
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                },
        ],

        transOrgTransformColDef: [
              {
                  field: 'ent_org_id', width: "*",  displayName: "Enterprise Org ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'cdim_transform_id', width: "*",  displayName: "CDIM Transform ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'ent_org_branch', width: "*",  displayName: "Enterprise Org Branch", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'strx_transform_id', width: "*",  displayName: "STRX Transform ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'transform_condn_sql', width: "*",  displayName: "Transform Condition SQL", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },

              {
                  field: 'org_nm_transform_strt_dt', width: "*",  displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },
              {
                  field: 'org_nm_transform_end_dt', width: "*",  displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'date_only_filter'
              },

              {
                  field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'act_ind', width: "*",  displayName: "Active Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'dw_srcsys_trans_ts', width: "*",  displayName: "DW Transaction Time", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellFilter: 'column_date_filter'
              },
              {
                  field: 'row_stat_cd', width: "*",  displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'is_previous', width: "*", displayName: "Is Previous", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              }

        ],
         transGroupMembershipUploadColDef: [
            {
                field: 'grp_key', width: "*",  displayName: "Group Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_mstr_id', width: "*",  displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'assgnmnt_mthd', width: "*",  displayName: "Assignment Method", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'grp_cd', width: "*",  displayName: "Group Code", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'grp_nm', width: "*",  displayName: "Group Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },

            {
                field: 'grp_typ', width: "*",  displayName: "Group Type", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'grp_owner', width: "*",  displayName: "Group Owner", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },

            {
                field: 'grp_mbrshp_eff_strt_dt', width: "*", displayName: "Eff Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },

            {
                field: 'grp_mbrshp_eff_end_dt', width: "*", displayName: "Eff End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'grp_strt_ts', width: "*", displayName: "Group Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },



            {
                field: 'grp_end_dt', width: "*",  displayName: "Group End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'date_only_filter'
            },
            {
                field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

            {
                field: 'act_ind', width: "*",  displayName: "Activation Indicator", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

            {
                field: 'user_id', width: "*",  displayName: "User ID", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },


            {
                field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Row Code', visible: false, width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Load Id', visible: false, width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'DW Timestamp', visible: false, width: "*", 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter',

                visible: false
            },


            {
                field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Application Source Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'is_previous', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Is Previous', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'transaction_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Transaction Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'trans_status', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Trans Status', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'inactive_ind', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Inactive Indicator', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'strx_row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'STRX Row Status Code', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },
            {
                field: 'unique_trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,
                displayName: 'Unique Trans Key', visible: false, width: "*",
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
            },

         ],
         transCemDncColDef: [
            {
                field: 'dnc_mstr_id', width: "*", displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
           
            {
                field: 'init_mstr_id', width: "*", displayName: "Init Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'line_of_service_cd', width: "*", displayName: "LOS", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'comm_chan', width: "*", displayName: "Channel", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'locator_id', width: "*", displayName: "Locator Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 90, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dnc_typ', width: "*", displayName: "Dnc Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_dnc_strt_ts', width: "*", displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            
            {
                field: 'cnst_dnc_end_ts', width: "*", displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
           
            {
                field: 'notes', width: "*", displayName: "Notes", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },visible:false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'transaction_key', width: "*", displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'inactive_ind', width: "*", displayName: "active?", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: true,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'strx_row_stat_cd', width: "*", displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'unique_trans_key', width: "*", displayName: "Unique Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'srcsys_trans_ts', width: "*", displayName: "Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'appl_src_cd', width: "*", displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', width: "*", displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dw_trans_ts', width: "*", displayName: "Dw Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 130, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, visible: false,
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            }
         ],
         transCemMsgPrefColDef: [
            {
                field: 'msg_pref_mstr_id', width: "*", displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'init_mstr_id', width: "*", displayName: "Init master Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'arc_srcsys_cd', width: "*", displayName: "Source System Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_srcsys_id', width: "*", displayName: "Source System Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'line_of_service_cd', width: "*", displayName: "LOS", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'comm_chan', width: "*", displayName: "Channel", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'msg_pref_typ', width: "*", displayName: "Preference Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'msg_pref_val', width: "*", displayName: "Preference Value", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'comm_typ', width: "*", displayName: "Comm Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'msg_pref_strt_ts', width: "*", displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'msg_pref_end_ts', width: "*", displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'msg_pref_exp_ts', width: "*", displayName: "Expiry Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'user_id', width: "*", displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_status', width: "*", displayName: "Trans Status", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'notes', width: "*", displayName: "Notes", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'Status', width: "*", displayName: "Status", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'transaction_key', width: "*", displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'srcsys_trans_ts', width: "*", displayName: "Source Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'inactive_ind', width: "*", displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'strx_row_stat_cd', width: "*", displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'appl_src_cd', width: "*", displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'dw_trans_ts', width: "*", displayName: "Dw Trans Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', width: "*", displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'is_previous', width: "*", displayName: "Is Previous", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'unique_trans_key', width: "*", displayName: "Unique Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
         ],
         transCemPrefLocColDef: [
            {
                field: 'cnst_mstr_id', width: "*", displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'line_of_service_cd', width: "*", displayName: "LOS", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'pref_loc_typ', width: "*", displayName: "Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'pref_loc_id', width: "*", displayName: "Preferred Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_pref_loc_strt_ts', width: "*", displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_pref_loc_end_ts', width: "*", displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'arc_srcsys_cd', width: "*", displayName: "Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'cnst_srcsys_id', width: "*", displayName: "Source Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_key', width: "*", displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'user_id', width: "*", displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'is_previous', width: "*", displayName: "Is Previous", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
           
            {
                field: 'appl_src_cd', width: "*", displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'load_id', width: "*", displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'transaction_key', width: "*", displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'trans_status', width: "*", displayName: "Trans Status", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'inactive_ind', width: "*", displayName: "Active?", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'strx_row_stat_cd', width: "*", displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            },
            {
                field: 'unique_trans_key', width: "*", displayName: "Unique Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
            }
            
         ],
           transCemDncUploadColDef: [
              {
                  field: 'constituent_id', width: "*", displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 110, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'email_address', width: "*",  displayName: "Email Address", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'name', width: "*",  displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'address', width: "*",  displayName: "Address", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'addr_line_1', width: "*",  displayName: "Address Line 1", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'addr_line_2', width: "*",  displayName: "Address Line 2", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },

              {
                  field: 'city', width: "*",  displayName: "City", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'state_cd', width: "*",  displayName: "State", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 60, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'zip5', width: "*",  displayName: "Zip 5", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 60, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
                {
                    field: 'zip4', width: "*", displayName: "Zip 4", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 50, maxWidth: 900,

                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
              {
                  field: 'phone_number', width: "*",  displayName: "Phone Number", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'line_of_service_cd', width: "*", displayName: "LOS", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'comm_chan', width: "*", displayName: "Comm Channel", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'dnc_typ', width: "*", displayName: "DNC Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 110, maxWidth: 900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
              },
              {
                  field: 'locator_id', width: "*", displayName: "Locator Id", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible:false
              },
               {
                   field: 'status', width: "*", displayName: "Status", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
              {
                  field: 'trans_key', width: "*",  displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE,minWidth: 120, maxWidth:900,

                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              }

        ],

           transCemMsgPrefUploadColDef: [
                 {
                     field: 'constituent_id', width: "*", displayName: "Master ID", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 90, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'email_address', width: "*", displayName: "Email Address", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'name', width: "*", displayName: "Name", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'address', width: "*", displayName: "Address", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'addr_line_1', width: "*", displayName: "Address Line 1", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'addr_line_2', width: "*", displayName: "Address Line 2", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },

                 {
                     field: 'city', width: "*", displayName: "City", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'state_cd', width: "*", displayName: "State", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'zip5', width: "*", displayName: "Zip 5", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'zip4', width: "*", displayName: "Zip 4", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 50, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'phone_number', width: "*", displayName: "Phone Number", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                 {
                     field: 'msg_pref_typ', width: "*", displayName: "Message Pref Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 110, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                  {
                      field: 'msg_pref_val', width: "*", displayName: "Message Pref Value", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 100, maxWidth: 900,

                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                  },
                  {
                      field: 'line_of_service_cd', width: "*", displayName: "LOS", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 60, maxWidth: 900,

                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',visible :true
                  },
                  {
                      field: 'comm_chan', width: "*", displayName: "Comm Channel", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true
                  },
                  {
                      field: 'comm_typ', width: "*", displayName: "Comm Type", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false
                  },
                  {
                      field: 'status', width: "*", displayName: "Status", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 80, maxWidth: 900,

                      filter: {
                          condition: uiGridConstants.filter.STARTS_WITH
                      },
                      cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                  },
                 {
                     field: 'trans_key', width: "*", displayName: "Transaction Key", headerCellTemplate: GRID_HEADER_TEMPLATE, minWidth: 120, maxWidth: 900,

                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 }
                  



           ]

    };

}

//Grid options are set same to all grids in multi , feel free to change by passing parameter so that it becomes grid specific
function TransGrid(columnDef) {//Grid
    this.columnDef = columnDef;
    var self = this;
    // if you want to add more parameters just and pass them where you are calling
    this.getGridOption = function (uiScrollOption, BestIndicatorParam, paginationPageSize, enableRowSelection, enableRowHeaderSelection,
        enableSelectAll, enableColumnResize, enableGridMenu, enableFiltering) {


        return {
            enableRowSelection: typeof enableRowSelection !== 'undefined' ? enableRowSelection : false,
            enableRowHeaderSelection: typeof enableRowHeaderSelection !== 'undefined' ? enableRowHeaderSelection : false,
            enableFiltering: typeof enableFiltering !== 'undefined' ? enableFiltering : false,
            enableSelectAll: typeof enableSelectAll !== 'undefined' ? enableSelectAll : false,
            enableColumnResize: typeof enableColumnResize !== 'undefined' ? enableColumnResize : true,
            rowHeight: 45,
            paginationPageSize: typeof paginationPageSize !== 'undefined' ? paginationPageSize : 5,
            paginationPageSizes: [5, 10,15,20,25,30,35,40,45,50],
            enablePaginationControls: false,
            enableVerticalScrollbar: 0,
            enableHorizontalScrollbar: typeof uiScrollOption !== 'undefined' ? uiScrollOption : 1,
            enableGridMenu: typeof enableGridMenu !== 'undefined' ? enableGridMenu : true,
            showGridFooter: false,
            columnDefs: this.columnDef,
            data: '',
            enableColumnResizing: true,
            enableColumnMoving: true
        }
    };
};

TransGrid.prototype.getGridLayout = function (gridOptions, result, type, uiScroll) {

    gridOptions.data = '';
    gridOptions.data.length = 0;
    gridOptions.data = result;

    if (gridOptions.columnDefs.length > 6) {
        gridOptions.enableHorizontalScrollbar = 1;
    }

    angular.element(document.getElementsByClassName(type)[0]).css('height', '0px');
    return gridOptions;
};

