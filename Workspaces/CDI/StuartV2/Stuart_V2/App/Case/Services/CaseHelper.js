
function CaseGridColumns() {

};

CaseGridColumns.prototype.getGridColumns = function (uiGridConstants, $rootScope) {
    //var GRID_HEADER_TEMPLATE = BasePath + 'App/Constituent/Views/common/GridHeader.tpl.html';
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


    //  var CellTempl = '<div>{{grid.appScope.states.toggleCRUDbuttons(row.entity)}}</div>';
    var CellTempl = urlfunc();

    function urlfunc() {
        var tempValue = '<div ng-include src=grid.appScope.states.toggleCRUDbuttons(row.entity)></div>';
        // return "App/Constituent/Views/gridDropDown.tpl.html";
        return tempValue;
    }
    //return '<div> </div>';

    //console.log(CellTempl);
    var linkCellTemplate = '<a class="text-center" ng-href="#" ng-click="grid.appScope.setGlobalValues(row)">{{COL_FIELD}}</a>';
    return {

        caseInfoColDef: 
            
            //var linkCellTemplate = '<a class="text-center" ng-href="#" ng-click="grid.appScope.setGlobalValues(row)">{{COL_FIELD}}</a>';
             [{
                 field: 'case_key', displayName: 'Case Number', menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
            },
            {
                field: 'case_nm', displayName: 'Case Name', visible: true, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            //{ field: 'case_desc', headerTooltip: 'Case description', displayName: 'Case description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
            {
                field: 'intake_chan_value', displayName: 'Intake Channel ID', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'intake_owner_dept_value', displayName: 'Intake/Owner Dept ID', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ref_src_dsc', displayName: 'CRM System', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ref_id', displayName: 'CRM System ID', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'typ_key_dsc', displayName: 'Type', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'cnst_nm', displayName: 'Constituent Name', visible: true, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'crtd_by_usr_id', displayName: 'User Name', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'report_dt', displayName: 'Reported Date', visible: false, menuItems: customGridFilter(CASE_CONSTANTS.CASE_INFO, $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            { field: 'status', displayName: 'Status' },
             {
                 field: 'Action', cellTemplate: 'App/Case/Views/common/gridDropDownCaseInfo.tpl.html', displayName: 'User Action', 
                 width: "*", minWidth: 100, maxWidth: 9000
             }
            ],

        caseLocInfoColDef: [

                {
                    field: 'srch_criteria_key', width: "*", minWidth: 65, maxWidth: 9000, displayName: "Search Criteria Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                    menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'locator_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Locator ID', width: "*", minWidth: 105, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srch_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Search Type', width: "*", minWidth: 115, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srch_usr', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Search User', width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_mstr_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Master ID', width: "*", minWidth: 70, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srcsys', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Source System', width: "*", minWidth: 110, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'chpt_srcsys', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Chapter Source System', visible: false, width: "*", minWidth: 95, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Source System ID', visible: false, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'f_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'First Name', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'l_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Last Name', width: "*", minWidth: 135, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_srcsys_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Source System ID', cellTemplate: '<span class="wordwrap">{{COL_FIELD}}</span>', width: "*", minWidth: 150, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'addr_line', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Address Line', sort: { direction: uiGridConstants.DESC, priority: 0 }, visible: false, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'city', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'City', visible: false, width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'state_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'State Code', visible: false, width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'zip', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Zip', visible: false, width: "*", minWidth: 100, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'phn_num', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Phone Name', visible: false, width: "*", minWidth: 80, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'email_addr', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Email Address', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'usr_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'User Name', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'trans_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Transaction Type', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                }, {
                    field: 'trans_stat', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Transaction Status', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'case_num', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Case Number', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'from_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'From Date', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'to_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'To Date', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'org_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Organization Name', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'case_key', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Case Key', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_typ', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Constituent Type', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'trans_key', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Transaction Key', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'ent_org_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Enterprise org ID', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'ent_org_name', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Enterprise Org Name', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'fortune_num', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Fortune Number', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'source_code', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Source Code', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'source_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Source ID', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'naics', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'NAICS Code', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'key_acct_fr', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'FR Account Key', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'key_acct_hs', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'HS Account Key', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'key_acct_bio', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'BIO Account Key', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'key_acct_ent', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Ent Account Key', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'ent_org_type', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Ent Org Type', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'assessmnt_email_addr', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Email Assessment Address', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'int_assessmnt_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Internal Assessment Code', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'ext_hygiene_result', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'External Assessment Hygiene', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'ext_assessmnt_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'External Assessment Code', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'strt_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Start Date', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'end_dt', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'End Date', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'user_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'User ID', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'dw_srcsys_trans_ts', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'appl_src_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Application SRC Sys code', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'row_stat_cd', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Row Stat Code', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'load_id', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_LOCINFO, $rootScope),
                    displayName: 'Load ID', visible: false, width: "*", minWidth: 200, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'Action', cellTemplate: 'App/Case/Views/common/gridDropDown.tpl.html', displayName: 'User Action',
                    width: "*", minWidth: 100, maxWidth: 9000
                }

        ],

        caseNotesColDef: [
                {
                    field: 'case_key', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_NOTES, $rootScope),
                    displayName: 'Case Key', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    visible: false
                },
                {
                    field: 'notes_key', headerCellTemplate: GRID_HEADER_TEMPLATE, menuItems: customGridFilter(CASE_CONSTANTS.CASE_NOTES, $rootScope),
                    displayName: 'Notes Key',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'case_notes', menuItems: customGridFilter(CASE_CONSTANTS.CASE_NOTES, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Case Notes',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap"><a class="text-center" ng-href="#" ng-click="grid.appScope.openNotes(row)">{{COL_FIELD}}</a></div>'
                },
                {
                    field: 'row_stat_cd', menuItems: customGridFilter(CASE_CONSTANTS.CASE_NOTES, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Row Stat Code',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    visible: false
                },
                {
                    field: 'start_dt', menuItems: customGridFilter(CASE_CONSTANTS.CASE_NOTES, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Start Date',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    cellFilter: 'column_date_filter',
                    visible:false
                },
                {
                    field: 'end_dt', menuItems: customGridFilter(CASE_CONSTANTS.CASE_NOTES, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'End Date',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    //cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    cellFilter: 'column_date_filter',
                    visible: false
                },
                 {
                     field: 'Action', cellTemplate: 'App/Case/Views/common/gridDropDown.tpl.html', displayName: 'User Action',
                     width: "*", minWidth: 100, maxWidth: 9000
                 }
        ],
        caseTransColDef: [
               // { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                {
                    field: 'trans_key', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Key',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'transaction_type', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Type',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'trans_typ_dsc', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Type Description',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'sub_trans_actn_typ', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Sub Transaction Action Type',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'trans_stat', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Status',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'user_id', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                     headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User ID',
                     filter: {
                         condition: uiGridConstants.filter.STARTS_WITH
                     },
                     cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                 },
                {
                    field: 'case_seq', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Case Key',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'trans_cnst_note', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Constituent Note',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'trans_create_ts', menuItems: customGridFilter(CASE_CONSTANTS.CASE_TRANSACTION, $rootScope),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Create Timestamp',visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    // cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                    cellFilter: 'column_timestamp_filter'
                }
        ]

    }

  

}

//Grid options are set same to all grids in multi , feel free to change by passing parameter so that it becomes grid specific
function CaseGrid(columnDef) {
    this.columnDef = columnDef;
    var self = this;
   // var softDeletedMessage = "Inactive/Soft-Deleted records cannot be edited.";
   // var rowtpl = '<div ng-class="{\'greyClass\':row.entity.row_stat_cd == \'0\' && row.entity.transNotes == \'' + softDeletedMessage + '\',yellowClass:row.entity.transNotes.length > \'0\' && (row.entity.transNotes.indexOf(\'' + transaction + '\')==0) }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
    var rowtpl = '<div ng-class="{\'greyClass\':row.entity.row_stat_cd== \'L\' }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';

    // if you want to add more parameters just and pass them where you are calling
    this.getGridOption = function (uiScrollOption, BestIndicatorParam, paginationPageSize, enableRowSelection, enableRowHeaderSelection,
        enableSelectAll, enableColumnResize, enableGridMenu, enableFiltering) {


        return {
            enableRowSelection: typeof enableRowSelection !== 'undefined' ? enableRowSelection : false,
            enableRowHeaderSelection: typeof enableRowHeaderSelection !== 'undefined' ? enableRowHeaderSelection : false,
            enableFiltering: typeof enableFiltering !== 'undefined' ? enableFiltering : false,
            enableSelectAll: typeof enableSelectAll !== 'undefined' ? enableSelectAll : false,
            enableColumnResize: typeof enableColumnResize !== 'undefined' ? enableColumnResize : true,
            //  selectionRowHeaderWidth: 35,
            rowHeight: 43,
            rowTemplate: rowtpl,
            paginationPageSize: typeof paginationPageSize !== 'undefined' ? paginationPageSize : 5,
            paginationPageSizes: [5, 10,15,20,25,30,35,40,45,50],
            enablePaginationControls: false,
            enableVerticalScrollbar: 0,
            enableHorizontalScrollbar: typeof uiScrollOption !== 'undefined' ? uiScrollOption : 0,
            enableGridMenu: typeof enableGridMenu !== 'undefined' ? enableGridMenu : true,
            showGridFooter: false,
            columnDefs: this.columnDef,
            data: '',
            enableColumnResizing: true,
            enableColumnMoving: true
        }
    };
};


CaseGrid.prototype.getGridLayout = function (gridOptions, result, type, uiScroll) {
    //  gridOptions.multiSelect = false;
   // console.log("gridOptions");
   // console.log(gridOptions);
    gridOptions.data = '';
    gridOptions.data.length = 0;
    gridOptions.data = result;


    if (gridOptions.columnDefs.length > 6) {
        gridOptions.enableHorizontalScrollbar = 1;
    }

    angular.element(document.getElementsByClassName(type)[0]).css('height', '0px');
    // console.log(datas.length);
    /*  if (result.length > 5) {
          // gridOptions.enableVerticalScrollbar = 1// uiGridConstants.scrollbars.ALWAYS;
          gridOptions.minRowsToShow = 5;
      } else {
          // gridOptions.enableVerticalScrollbar = 0//uiGridConstants.scrollbars.NEVER;
          gridOptions.minRowsToShow = result.length;
      }*/


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