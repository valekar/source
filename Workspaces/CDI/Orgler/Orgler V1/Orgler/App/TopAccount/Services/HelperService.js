/************************* Services - Helper *************************/
topAccMod.factory("TopAccountHelperService", [
    function () {
        return {
            getGridOptions: function()
            {
                var gridOption = {
                    paginationPageSize: 10,
                    enableSorting: true,
                    enablePager: false,
                    enableGridMenu: true,
                    enableFiltering: false,
                    enablePaginationControls: false,
                    enablePagination: true,
                    paginationPageSizes: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
                    enableVerticalScrollbar: 1,
                    enableHorizontalScrollbar: 2,
                    showGridFooter: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 95,
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
            getPopupGridOptions: function () {
                var gridOption = {
                    paginationPageSize: 10,
                    enableSorting: true,
                    enablePager: false,
                    enableGridMenu: false,
                    enableFiltering: false,
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 1,
                    enableHorizontalScrollbar: 0,
                    showGridFooter: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 43,
                    paginationCurrentPage: 1,
                    totalItems: 0,
                    enableColumnResizing: false,
                    enableColumnMoving: false,
                    enableRowSelection: false,
                    enableRowHeaderSelection: false,
                    enableSelectAll: false
                };

                gridOption.data = '';
                return gridOption;
            },
            getDetailsPopupGridOptions: function () {
                var gridOption = {
                    paginationPageSize: 10,
                    enableSorting: true,
                    enablePager: false,
                    enableGridMenu: true,
                    enableFiltering: false,
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 1,
                    enableHorizontalScrollbar: 1,
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
            getTopAccountSearchResultsColDef: function()
            {

                return [                    
                    { displayName: 'Master ID', field: 'master_id', width: "5%", minWidth: 90, maxWidth: 9000, type: 'number', cellTemplate: 'App/Shared/Views/searchResultsMasterId.tpl.html' },
                    { displayName: 'Recent Patronage Date', field: 'mst_rcnt_patrng_dt', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellFilter: 'column_date_filter' },
                    { displayName: 'Lex ID', field: 'lexis_nexis_id', visible: false, width: "*", minWidth: 30, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    //{ displayName: 'Patronage Value', field: 'monetary_value', width: "5%", type: 'number', minWidth: 90, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    //{ displayName: 'RFM Score', field: 'rfm_score', type: 'number', width: "*", minWidth: 70, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    //{ displayName: 'Frequency Value', field: 'freq_value', type: 'number', width: "*", minWidth: 70, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Patronage Count', field: 'freq_value', type: 'number', width: "*", minWidth: 70, maxWidth: 9000, cellTemplate: '<div ng-show="row.entity.line_of_service_cd == \'BIO\'" class="wordwrap">{{COL_FIELD || \'0\'}} drive(s)</div><div ng-show="row.entity.line_of_service_cd == \'FR\'" class="wordwrap">{{COL_FIELD || \'0\'}} gift(s)</div><div ng-show="row.entity.line_of_service_cd == \'PHSS\'" class="wordwrap">{{COL_FIELD || \'0\'}} order(s)</div>' },
                    { displayName: 'Patronage Value', field: 'monetary_value', width: "*", type: 'number', minWidth: 90, maxWidth: 9000, cellTemplate: '<div ng-show="row.entity.line_of_service_cd == \'BIO\'" class="wordwrap">{{COL_FIELD || \'0\'}} blood unit(s)</div><div ng-show="row.entity.line_of_service_cd != \'BIO\'" class="wordwrap">{{COL_FIELD || \'0\' | currency}}</div>' },
                    { displayName: 'RFM Score', field: 'rfm_score', type: 'number', width: "3%", minWidth:8, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },                   
                    { displayName: 'Names', field: 'name', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridName.tpl.html' },
                    { displayName: 'Addresses', field: 'address', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridAddress.tpl.html' },
                    { displayName: 'Phone Numbers', field: 'phone', visible: false, width: "*", type: 'number', minWidth: 100, maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridPhone.tpl.html' },
                    { displayName: 'Email', field: 'email', visible: false, width: "*", minWidth: 100, maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridEmail.tpl.html' },                   
                    { displayName: 'NAICS Title', field: 'listNAICSDesc', width: "20%", minWidth: 200, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridNaicsCd.tpl.html' },
                    { displayName: 'Enterprise Org Name', field: 'ent_org_name', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Potential Merge?', field: 'has_potential_merge', width: "5%", minWidth: 70, type: 'boolean', maxWidth: 9000, cellTemplate: 'App/Shared/Views/mergeResults.tpl.html' },
                    { displayName: 'Potential Unmerge?',field: 'has_potential_unmerge', width: "5%", minWidth: 70, type: 'boolean', maxWidth: 9000, cellTemplate: 'App/Shared/Views/unmergeResults.tpl.html' },
                    { displayName: 'Confirm Account', field: 'status', visible: false, width: "5%", minWidth: 70, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/confirmAction.tpl.html' }
                ];
            },
            getTopAccountSearchResultsMonetaryFormattedColDef: function () {

                return [
                    { displayName: 'Master ID', field: 'master_id', width: "5%", minWidth: 90, maxWidth: 9000, type: 'number', cellTemplate: 'App/Shared/Views/searchResultsMasterId.tpl.html' },
                    { displayName: 'Recent Patronage Date', field: 'mst_rcnt_patrng_dt', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellFilter: 'column_date_filter' },
                    { displayName: 'Lex ID', field: 'lexis_nexis_id', visible: false, width: "*", minWidth: 70, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    //{ displayName: 'Patronage Value', field: 'monetary_value', width: "5%", type: 'number', minWidth: 90, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    //{ displayName: 'RFM Score', field: 'rfm_score', type: 'number', width: "*", minWidth: 70, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD | currency}}</div>' }, 
                    //{ displayName: 'Frequency Value',  field: 'freq_value', type: 'number', width: "*", minWidth: 70, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Patronage Count', field: 'freq_value', type: 'number', width: "*", minWidth: 70, maxWidth: 9000, cellTemplate: '<div ng-show="row.entity.line_of_service_cd == \'BIO\'" class="wordwrap">{{COL_FIELD || \'0\'}} drive(s)</div><div ng-show="row.entity.line_of_service_cd == \'FR\'" class="wordwrap">{{COL_FIELD || \'0\'}} gift(s)</div><div ng-show="row.entity.line_of_service_cd == \'PHSS\'" class="wordwrap">{{COL_FIELD || \'0\'}} order(s)</div>' },
                    { displayName: 'Patronage Value', field: 'monetary_value', width: "*", type: 'number', minWidth: 90, maxWidth: 9000, cellTemplate: '<div ng-show="row.entity.line_of_service_cd == \'BIO\'" class="wordwrap">{{COL_FIELD || \'0\'}} blood unit(s)</div><div ng-show="row.entity.line_of_service_cd != \'BIO\'" class="wordwrap">{{COL_FIELD || \'0\' | currency}}</div>' },
                    { displayName: 'RFM Score', field: 'rfm_score', type: 'number', width: "3%", minWidth: 8, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Names', field: 'name', width: "*", minWidth: 200, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridName.tpl.html' },
                    { displayName: 'Addresses', field: 'address', width: "*", minWidth: 200, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridAddress.tpl.html' },
                    { displayName: 'Phone Numbers', field: 'phone', visible: false, width: "*", type: 'number', minWidth: 100, maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridPhone.tpl.html' },
                    { displayName: 'Email', field: 'email', visible: false, width: "*", type: 'number', minWidth: 100, maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridEmail.tpl.html' },                   
                    { displayName: 'NAICS Title', field: 'listNAICSDesc', width: "20%", minWidth: 200, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/searchResultsSubGridNaicsCd.tpl.html' },
                    { displayName: 'Enterprise Org Name', field: 'ent_org_name', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Potential Merge?', field: 'has_potential_merge', width: "5%", minWidth: 70, type: 'boolean', maxWidth: 9000, cellTemplate: 'App/Shared/Views/mergeResults.tpl.html' },
                    { displayName: 'Potential Unmerge?', field: 'has_potential_unmerge', width: "5%", minWidth: 70, type: 'boolean', maxWidth: 9000, cellTemplate: 'App/Shared/Views/unmergeResults.tpl.html' },
                    { displayName: 'Confirm Account', field: 'status', visible: false, width: "5%", minWidth: 70, type: 'string', maxWidth: 9000, cellTemplate: 'App/Shared/Views/confirmAction.tpl.html' }
                ];
            },
            getAddressColDefs: function (uiGridConstants) {

                return [
                    { field: 'cnst_addr_line1_addr', displayName: 'Address Line 1', cellTemplate: '<div class="wordwrap" style="float:left;">{{COL_FIELD}}</div><div ng-show="row.entity.cnst_srcsys_id != null && row.entity.arc_srcsys_cd != null && row.entity.inactive_ind != \'-1\'" style="float:left; margin-top:10px;"><img ng-src="{{grid.appScope.getImagePath(\'new\')}}" ng-show="row.entity.cnst_srcsys_id == row.entity.sel_cnst_srcsys_id && row.entity.arc_srcsys_cd == row.entity.sel_arc_srcsys_cd" style=" float:left;margin-left:5px;" /></div>', width: "20%", minWidth: 150, maxWidth: 9000 },
                    { field: 'cnst_addr_line2_addr', displayName: 'Address Line 2', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 150, maxWidth: 9000 },
                    { field: 'cnst_addr_city_nm', displayName: 'City', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 9000 },
                    { field: 'cnst_addr_state_cd', displayName: 'State', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 9000 },
                    { field: 'cnst_addr_zip_5_cd', displayName: 'Zip', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 9000 },
                    { field: 'cnst_addr_zip_4_cd', displayName: 'Zip 4', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 70, maxWidth: 9000, visible: false },
                    { field: 'best_addr_ind', displayName: 'ARC Best', cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes</div>', width: "*", minWidth: 60, maxWidth: 9000, visible: false },
                    { field: 'addr_typ_cd', displayName: 'Address Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 70, maxWidth: 9000, visible: false },
                    { field: 'act_ind', displayName: 'Active?', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 90, maxWidth: 9000 },
                    { field: 'res_deliv_ind', displayName: 'Residential Indicator', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 110, maxWidth: 9000 },
                    { field: 'dpv_cd', displayName: 'DPV Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 90, maxWidth: 9000 },
                    { field: 'assessmnt_ctg', displayName: 'Assessment Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 120, maxWidth: 9000 },
                    { field: 'arc_srcsys_cd', displayName: 'Source System', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 140, maxWidth: 9000 },
                    { field: 'cnst_srcsys_id', displayName: 'Source System ID', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 160, maxWidth: 9000 },
                    { field: 'cnst_addr_strt_ts', displayName: 'Start Date', visible: false, width: "*", minWidth: 140, maxWidth: 9000 },
                    { field: 'cnst_addr_end_dt', displayName: 'End Date', visible: false, width: "*", minWidth: 130, maxWidth: 9000 },
                    { field: 'row_stat_cd', displayName: 'Row Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 60, maxWidth: 9000 },
                    { field: 'load_id', displayName: 'Load Id', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 80, maxWidth: 9000 },
                    { field: 'dw_srcsys_trans_ts', displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000 },
                    { field: 'Action', cellTemplate: 'App/Shared/Views/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>', width: "*", minWidth: 325, maxWidth: 9000 }
                ];
            },
            getAddressPotentialUnmergeColDefs: function (uiGridConstants) {

                return [
                    { field: 'cnst_addr_line1_addr', displayName: 'Address Line 1', cellTemplate: '<div class="wordwrap" style="float:left;">{{COL_FIELD}}</div><div ng-show="row.entity.cnst_srcsys_id != null && row.entity.arc_srcsys_cd != null && row.entity.inactive_ind != \'-1\'" style="float:left; margin-top:10px;"><img ng-src="{{grid.appScope.getImagePath(\'new\')}}" ng-show="row.entity.cnst_srcsys_id == row.entity.sel_cnst_srcsys_id && row.entity.arc_srcsys_cd == row.entity.sel_arc_srcsys_cd" style=" float:left;margin-left:5px;" /></div>', width: "20%", minWidth: 150, maxWidth: 9000 },
                    { field: 'cnst_addr_line2_addr', displayName: 'Address Line 2', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 150, maxWidth: 9000 },
                    { field: 'cnst_addr_city_nm', displayName: 'City', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 100, maxWidth: 9000 },
                    { field: 'cnst_addr_state_cd', displayName: 'State', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 9000 },
                    { field: 'cnst_addr_zip_5_cd', displayName: 'Zip', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 9000 },
                    { field: 'cnst_addr_zip_4_cd', displayName: 'Zip 4', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 70, maxWidth: 9000, visible: false },
                    { field: 'best_addr_ind', displayName: 'ARC Best', cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes</div>', width: "*", minWidth: 60, maxWidth: 9000, visible: false },
                    { field: 'addr_typ_cd', displayName: 'Address Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 70, maxWidth: 9000, visible: false },
                    { field: 'act_ind', displayName: 'Active?', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 90, maxWidth: 9000 },
                    { field: 'res_deliv_ind', displayName: 'Residential Indicator', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 110, maxWidth: 9000 },
                    { field: 'dpv_cd', displayName: 'DPV Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 90, maxWidth: 9000 },
                    { field: 'assessmnt_ctg', displayName: 'Assessment Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 120, maxWidth: 9000 },
                    { field: 'arc_srcsys_cd', displayName: 'Source System', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 140, maxWidth: 9000 },
                    { field: 'cnst_srcsys_id', displayName: 'Source System ID', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: true, width: "*", minWidth: 160, maxWidth: 9000 },
                    { field: 'cnst_addr_strt_ts', displayName: 'Start Date', visible: false, width: "*", minWidth: 140, maxWidth: 9000 },
                    { field: 'cnst_addr_end_dt', displayName: 'End Date', visible: false, width: "*", minWidth: 130, maxWidth: 9000 },
                    { field: 'row_stat_cd', displayName: 'Row Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 60, maxWidth: 9000 },
                    { field: 'load_id', displayName: 'Load Id', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 80, maxWidth: 9000 },
                    { field: 'dw_srcsys_trans_ts', displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000 },
                    { field: 'Action', cellTemplate: 'App/Shared/Views/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>', width: "*", minWidth: 325, maxWidth: 9000 },
                     { field: 'Select', cellTemplate: ' <input type="checkbox" name="checkboxAction" />', displayName: 'Select', headerCellTemplate: '<div>Select</div>', width: "*", minWidth: 80, maxWidth: 9000 }

                ];
            },
            getPhoneColDefs: function (uiGridConstants) {

                return [
                    { field: 'cnst_phn_num', displayName: 'Phone Number', cellTemplate: '<div class="wordwrap" style="float:left;">{{COL_FIELD}}</div><div ng-show="row.entity.cnst_srcsys_id != null && row.entity.arc_srcsys_cd != null && row.entity.inactive_ind != \'-1\'" style="float:left; margin-top:10px;"><img ng-src="{{grid.appScope.getImagePath(\'new\')}}" ng-show="row.entity.cnst_srcsys_id == row.entity.sel_cnst_srcsys_id && row.entity.arc_srcsys_cd == row.entity.sel_arc_srcsys_cd" style=" float:left;margin-left:5px;" /></div>', width: "10%", minWidth: 150, maxWidth: 9000 },
					{ field: 'phn_typ_cd', displayName: 'Phone Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 9000 },
					{ field: 'best_phn_ind', displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes</div>', width: "*", minWidth: 60, maxWidth: 9000 },
					{ field: 'arc_srcsys_cd', displayName: 'Source System Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 120, maxWidth: 9000 },
					{ field: 'cnst_srcsys_id', displayName: 'Source System ID', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 120, maxWidth: 9000 },
					{ field: 'cnst_phn_strt_ts', displayName: 'Start Date', visible: false, width: "*", minWidth: 100, maxWidth: 9000 },
					{ field: 'cnst_phn_end_dt', width: "*", minWidth: 100, maxWidth: 9000, displayName: 'End Date', visible: false },
					{ field: 'act_ind', displayName: 'Active?', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 90, maxWidth: 9000 },
					{ field: 'row_stat_cd', displayName: 'Row Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false, width: "*", minWidth: 60, maxWidth: 9000 },
					{ field: 'load_id', displayName: 'Load Id', visible: false, width: "*", minWidth: 100, maxWidth: 9000 },
					{ field: 'dw_srcsys_trans_ts', displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000 },
                    { field: 'Action', cellTemplate: 'App/Shared/Views/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>', width: "*", minWidth: 325, maxWidth: 9000 }
                ]
            },
            getNameColDefs: function (uiGridConstants) {

                return [
                    { field: 'cnst_org_nm', width: "20%", minWidth: 220, maxWidth: 9000, displayName: "Name", cellTemplate: '<div class="wordwrap" style="float:left;">{{COL_FIELD}}</div><div ng-show="row.entity.cnst_srcsys_id != null && row.entity.arc_srcsys_cd != null && row.entity.inactive_ind != \'-1\'" style="float:left; margin-top:10px;"><img ng-src="{{grid.appScope.getImagePath(\'new\')}}" ng-show="row.entity.cnst_srcsys_id == row.entity.sel_cnst_srcsys_id && row.entity.arc_srcsys_cd == row.entity.sel_arc_srcsys_cd" style=" float:left;margin-left:5px;" /></div>' },
					{ field: 'cln_cnst_org_nm', width: "*", minWidth: 220, maxWidth: 9000, displayName: "Clean Name", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'best_org_nm_ind', displayName: 'ARC Best', sort: { direction: uiGridConstants.DESC, priority: 1 }, width: "*", minWidth: 120, maxWidth: 9000, cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD == \'0\' " >No</div> <div class="wordwrap" ng-if="COL_FIELD == \'1\' " >Yes</div> <div class="wordwrap" ng-if="COL_FIELD == \'2\' " >Yes</div>' },
					{ field: 'arc_srcsys_cd', displayName: 'Source System', width: "*", minWidth: 140, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'cnst_srcsys_id', displayName: 'Source System ID', cellTemplate: '<span class="wordwrap">{{COL_FIELD}}</span>', width: "*", minWidth: 160, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'cnst_org_nm_typ_cd', displayName: 'Name Type', visible: false, width: "*", minWidth: 130, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'cnst_org_nm_strt_dt', displayName: 'Start Date', sort: { direction: uiGridConstants.DESC, priority: 0 }, visible: false, width: "*", minWidth: 140, maxWidth: 9000 },
					{ field: 'cnst_org_nm_end_dt', displayName: 'End Date', visible: false, width: "*", minWidth: 130, maxWidth: 9000 },
					{ field: 'act_ind', displayName: 'Active?', visible: false, width: "*", minWidth: 90, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'row_stat_cd', displayName: 'Row Code', visible: false, width: "*", minWidth: 130, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'load_id', displayName: 'Load Id', visible: false, width: "*", minWidth: 80, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
					{ field: 'dw_srcsys_trans_ts', displayName: 'DW Timestamp', visible: false, width: "*", minWidth: 180, maxWidth: 9000 },
                    { field: 'Action', cellTemplate: 'App/Shared/Views/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>', width: "*", minWidth: 325, maxWidth: 9000 }
                ];
            },
            getnaicsEditColDefs: function () {

                return [
                    { field: 'naics_cd', displayName: 'NAICS Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
					{ field: 'naics_indus_title', displayName: 'NAICS Title', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 9000 },
					{ field: 'naics_indus_dsc', displayName: 'NAICS Description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 400, maxWidth: 9000 },
					{ field: 'conf_weightg', displayName: 'Confidence Weightage', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
					{ field: 'rule_keywrd', displayName: 'Rule Keyword', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
                    { field: 'sts', displayName: 'Status', width: "*", minWidth: 50, maxWidth: 9000 },
					{ field: 'manual_sts', displayName: 'User Actions', cellTemplate: 'App/Shared/Views/gridNAICSEditDropDown.tpl.html', width: "*", minWidth: 50, maxWidth: 9000 }
                ]
            },
            getTopAccountPotentialMergeColDef: function () {

                return [
                    { displayName: 'Master ID', field: 'pot_merge_mstr_id', width: "*", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Name', field: 'pot_merge_mstr_nm', width: "*", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Address', field: 'address', width: "*", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' }
                ];
            },
            getDynamicGridLayout: function (gridOptions, uiGridConstants, datas, visibleRowCount) {
                gridOptions.data = datas;

                var width = angular.element(document.getElementsByClassName('grid')[0]).css('width').replace('px','');
                var indWidth = width / visibleRowCount;

                //Get the maximum height of the grid
                var maxRowHeight = 0;
                angular.forEach(datas, function (rowVal, rowKey) {
                    angular.forEach(rowVal, function (colVal, colKey) {
                        var tempColumnLength = 0;
                        if(!angular.isUndefined(colVal))
                        {
                            if(colVal != null)
                            {
                                if (angular.isArray(colVal))
                                {
                                    angular.forEach(colVal, function (objListValue, objListKey) {
                                        angular.forEach(objListValue, function (objListColValue, objListColKey) {
                                            var colLineCount = 0;
                                            if (angular.isDefined(objListColValue.length) && objListColValue.length > 0) {
                                                colLineCount = Math.ceil(objListColValue.length / (indWidth/10));
                                            }
                                            tempColumnLength = tempColumnLength + (colLineCount * 16);
                                        });
                                    });
                                }
                                else
                                {
                                    tempColumnLength = colVal.length;
                                }

                                var lineCount = 0;
                                if (angular.isDefined(tempColumnLength) && tempColumnLength > 0) {
                                    lineCount = Math.ceil(tempColumnLength / (indWidth / 10));
                                }

                                if (lineCount * 16 > maxRowHeight)
                                {
                                    maxRowHeight = lineCount * 16;
                                }
                            }
                        }
                    });
                });
                
                //check if grid row has more data
                if (maxRowHeight > (gridOptions.rowHeight)) {
                    gridOptions.rowHeight = maxRowHeight/2;
                }

                //console.log(datas[0].source_system_count.length);

                angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');

                return gridOptions;
            },
            selectnaicsSegmentsPopupGridOptions: function () {
                var gridOption = {
                    paginationPageSize: 10,
                    enableSorting: true,
                    enablePager: false,
                    enableGridMenu: true,
                    enableFiltering: false,
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 1,
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
            selectnaicsSegmentsColDefs: function () {

                return [
                    { field: 'naics_cd', displayName: 'NAICS Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
					{ field: 'naics_indus_title', displayName: 'NAICS Title', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 9000 },
					{ field: 'naics_indus_dsc', displayName: 'NAICS Description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 400, maxWidth: 9000 },
					{ field: 'conf_weightg', displayName: 'Confidence Weightage', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
					{ field: 'rule_keywrd', displayName: 'Rule Keyword', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 40, maxWidth: 9000 },
                    { field: 'sts', displayName: 'Status', width: "*", minWidth: 50, maxWidth: 9000 },
					{ field: 'manual_sts', displayName: 'User Actions', cellTemplate: 'App/Shared/Views/gridNAICSEditDropDown.tpl.html', width: "*", minWidth: 50, maxWidth: 9000 }
                ]
            },
        }
    }]);