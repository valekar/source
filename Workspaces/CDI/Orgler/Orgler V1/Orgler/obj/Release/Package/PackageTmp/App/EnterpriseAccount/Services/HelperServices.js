/************************* Services - Helper *************************/
enterpriseAccMod.factory("EnterpriseAccountHelperService", [ 'uiGridConstants', 'uiGridGroupingConstants',
    function (uiGridConstants, uiGridGroupingConstants) {
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
        var filterCondition = {
            condition: uiGridConstants.filter.STARTS_WITH,
            placeholder: 'Starts with'
        };
        //Filter Condition for the grid
        var filterCondition = {
            condition: uiGridConstants.filter.STARTS_WITH,
            placeholder: 'Starts with'
        };

        var filterConditionWildcard = {
            condition: uiGridConstants.filter.CONTAINS,
            placeholder: 'Contains'
        };

        function customGridFilter(type, $scope) {
            return [{
                title: 'Filter',
                icon: 'ui-grid-icon-filter',
                action: function ($event) {
                    switch (type) {
                        case "EntOrgSearchResult":
                            $scope.searchResultsGridOptions.enableFiltering = !$scope.searchResultsGridOptions.enableFiltering;
                            $scope.searchResultsGridEnterpriseOrgs.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                            break;
                       
                    }
                }
            }];
        }
        return {
            getGridOptions: function () {
                var gridOption = {
                    enablePager: false,
                    paginationPageSize: 10,
                    enableSorting: true,
                    enableGridMenu: true,
                    enableFiltering: false,
                    paginationPageSizes: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 1,
                    enableHorizontalScrollbar: 1,
                    showGridFooter: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 43,
                    paginationCurrentPage: 1,                  
                    enableColumnResizing: true,
                    enableColumnMoving: true,                   
                   // enableRowHeaderSelection: false,
                    enableSelectAll: false,
                    exporterMenuPdf: false,
                    exporterCsvFilename: 'enterpriseaccount.csv',
                    exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),

                    //exporterMenuPdf: false,
                   // multiselect: true
                };

                gridOption.data = '';
                return gridOption;
            },
            getEnterAccGridOptions: function () {
                var gridOption = {
                    enablePager: false,
                    paginationPageSize: 10,
                    enableSorting: true,
                    enableGridMenu: true,
                    enableFiltering: false,
                    paginationPageSizes: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 1,
                    enableHorizontalScrollbar: 1,
                    showGridFooter: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 43,
                    paginationCurrentPage: 1,
                    enableColumnResizing: true,
                    enableColumnMoving: true,
                    // enableRowHeaderSelection: false,
                    enableSelectAll: false,
                    exporterMenuPdf: false,
                    exporterCsvFilename: 'enterpriseaccount.csv',
                    exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                    exporterFieldCallback: function (grid, row, col, value) {
                        if (col.field == 'listNAICSCodesAndDesc') {
                            //console.log(value);
                            if (value.length <= 0) {
                                value = "";
                            }
                            else {
                                var finalValue = "";
                                for (var i = 0; i < value.length; i++) {
                                    var newValue = "";                                  
                                    newValue = value[i].naicsCode + '-' + value[i].naicsTitle;
                                    finalValue = finalValue == "" ? newValue : finalValue + "," + newValue;
                                }
                                value = finalValue;
                            }                          
                        }
                        if (col.field == 'listTags') {
                            if (value.length <= 0) {
                                value = "";
                            }
                            else {
                                var finalValue = "";
                                for (var i = 0; i < value.length; i++) {
                                    var newValue = "";
                                    newValue = value[i].strText;
                                    finalValue = finalValue == "" ? newValue : finalValue + "," + newValue;
                                }
                                value = finalValue;
                            }
                        }
                        return value;
                    }

                    //exporterMenuPdf: false,
                    // multiselect: true
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
                    selectionRowHeaderWidth: 43,                   
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

            getEnterPriseAccountSearchResultsColDef: function ($scope) {
                return [
                     { displayName: 'Parent Ent Org ID', field: 'parent_ent_org_id', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "5%", minWidth:5, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div>{{COL_FIELD}}</div>' },
                     { displayName: 'Enterprise Org ID', field: 'ent_org_id', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "12%", minWidth: 12, maxWidth: 9000, type: 'number', menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div><a ng-click="grid.appScope.openDetails(row)">{{COL_FIELD}}</a></div>' },
                    { displayName: 'Enterprise Organization Name', field: 'ent_org_name', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", minWidth: 200, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div>{{COL_FIELD}}</div>' },
                    { displayName: 'Line Of Business Bridge Count', field: 'srcsys_cnt', headerCellTemplate: GRID_HEADER_TEMPLATE, type: 'string', filter: angular.copy(filterCondition), width: "*", minWidth: 200, maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Affiliated Masters Count', field: 'affil_cnt', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), type: 'string',width: "*", minWidth: 100, maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Affiliation Matching Rules Count', field: 'transformation_cnt', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", type: 'string', minWidth: 100, maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'NAICS Code', field: 'listNAICSCodesAndDesc', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false, filter: angular.copy(filterCondition), width: "*", type: 'string', minWidth: 200, maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: 'App/Shared/Views/EnterpriseAccount/enterpriseOrgsSearchResultSubGridNaicsCd.tpl.html' },
                    { displayName: 'Tags', field: 'listTags', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", minWidth: 200, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: 'App/Shared/Views/EnterpriseAccount/enterpriseOrgsSearchResultAffi.tpl.html' },
                    { displayName: 'Created By', field: 'created_by', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", type: 'boolean', minWidth: 100, maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Created Date', field: 'created_at', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", minWidth: 100, type: 'boolean', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD | column_date_filter}}</div>' },
                    { displayName: 'Last Modified By (Manual operation)', visible: false, field: 'last_modified_by', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "10%", minWidth: 200, type: 'boolean', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Modified At (Manual operation)', visible: false, field: 'last_modified_at', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", minWidth: 100, type: 'boolean', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD | column_date_filter}}</div>' },
                    { displayName: 'Last Modified By (Manual + Sys operations)', visible: false, field: 'last_modified_by_all', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), minWidth: 70, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Modified At (Manual + Sys operations)', visible: false, field: 'last_modified_at_all', headerCellTemplate: GRID_HEADER_TEMPLATE, filter: angular.copy(filterCondition), width: "*", minWidth: 100, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD | column_date_filter}}</div>' },
                    { displayName: 'Last Reviewed By', field: 'last_reviewed_by', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false, filter: angular.copy(filterCondition), width: "*", minWidth: 100, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Reviewed At', field: 'last_reviewed_at', headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false, filter: angular.copy(filterCondition), width: "*", minWidth: 100, type: 'string', maxWidth: 9000, menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD | column_date_filter}}</div>' },
                   
                    {
                        field: 'User Action', cellTemplate: 'App/EnterpriseAccount/Views/Shared/gridDropDown.tpl.html', displayName: 'User Action', headerCellTemplate: '<div>User Action</div>',
                    width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }

                ];
            },
            getEnterPriseAccountSearchResultsColDef1: function ($scope) {
                return [
                    //{ displayName: "Enterprise Organization Parent Id", field: 'parent_ent_org_id', grouping: { groupPriority: 0 }, sort: { priority: 0, direction: 'desc' }, headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 200, maxWidth: 9000, filter: angular.copy(filterCondition), menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div><div ng-show="!col.grouping || col.grouping.groupPriority === undefined || col.grouping.groupPriority === null || ( row.groupHeader && col.grouping.groupPriority === row.treeLevel )" class="ui-grid-cell-contents" title="TOOLTIP">{{COL_FIELD | removeAgg}}</div></div>' },
                    //{ displayName: "Enterprise Organization Id", field: 'ent_org_id', grouping: { groupPriority: 0 }, sort: { priority: 0, direction: 'desc' }, headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 200, maxWidth: 9000, filter: angular.copy(filterCondition), menuItems: customGridFilter('MasterBridgeMaster', $scope), cellTemplate: '<div><div ng-show="!col.grouping || col.grouping.groupPriority === undefined || col.grouping.groupPriority === null || ( row.groupHeader && col.grouping.groupPriority === row.treeLevel )" class="ui-grid-cell-contents" title="TOOLTIP">{{COL_FIELD | removeAgg}}</div></div>' },
                    //{ displayName: "Enterprise Organization Name", field: 'ent_org_name', grouping: { groupPriority: 0 }, sort: { priority: 0, direction: 'desc' }, headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 200, maxWidth: 9000, filter: angular.copy(filterCondition), menuItems: customGridFilter('MasterBridgeMaster', $scope), cellTemplate: '<div><div ng-show="!col.grouping || col.grouping.groupPriority === undefined || col.grouping.groupPriority === null || ( row.groupHeader && col.grouping.groupPriority === row.treeLevel )" class="ui-grid-cell-contents" title="TOOLTIP">{{COL_FIELD | removeAgg}}</div></div>' },
                    { displayName: 'Enterprise Organization Id', field: 'ent_org_id', width: "10%", minWidth: 70, maxWidth: 9000, type: 'number', cellTemplate: '<div class="wordwrap"><a ng-click="grid.appScope.openDetails(row)">{{COL_FIELD}}</a></div>' },
                    { displayName: 'Enterprise Organization Name', field: 'ent_org_name', width: "*", minWidth: 90, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: "Line Of Business Bridge Count", field: 'srcsys_cnt', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 150, maxWidth: 9000, visible: true, filter: angular.copy(filterCondition), menuItems: customGridFilter('EntOrgSearchResult', $scope), cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Source Systems', field: 'srcsys_cnt', width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Number of Affiliations', field: 'affil_cnt', width: "*", type: 'string', minWidth: 100, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Number of Transformations', field: 'transformation_cnt', width: "*", type: 'string', minWidth: 100, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'NAICS Code', field: 'listNAICSDesc', visible: false, width: "*", headerCellTemplate: GRID_HEADER_TEMPLATE, type: 'string', minWidth: 200, maxWidth: 9000, cellTemplate: 'App/Shared/Views/EnterpriseAccount/enterpriseOrgsSearchResultSubGridNaicsCd.tpl.html' },
                    { displayName: 'Tags', field: 'listTags', width: "5%", minWidth: 70, headerCellTemplate: GRID_HEADER_TEMPLATE, type: 'boolean', maxWidth: 9000, cellTemplate: 'App/Shared/Views/EnterpriseAccount/enterpriseOrgsSearchResultAffi.tpl.html' },
                    { displayName: 'Created By', field: 'created_by', width: "*", headerCellTemplate: GRID_HEADER_TEMPLATE, type: 'boolean', minWidth: 100, maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Created Date', field: 'created_at', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 100, type: 'boolean', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Modified By (Manual operation)', visible: false, field: 'last_modified_by', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "10%", minWidth: 200, type: 'boolean', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Modified At (Manual operation)', visible: false, field: 'last_modified_at', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 100, type: 'boolean', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Modified By (Manual + Sys operations)', visible: false, field: 'last_modified_by_all', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "5%", minWidth: 70, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Modified At (Manual + Sys operations)', visible: false, field: 'last_modified_at_all', headerCellTemplate: GRID_HEADER_TEMPLATE, width: "*", minWidth: 100, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Reviewed By', field: 'last_reviewed_by', visible: false, width: "*", minWidth: 100, headerCellTemplate: GRID_HEADER_TEMPLATE, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Last Reviewed At', field: 'last_reviewed_at', visible: false, width: "*", minWidth: 100, headerCellTemplate: GRID_HEADER_TEMPLATE, type: 'string', maxWidth: 9000, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    {
                        field: 'User Action', minWidth: 200, maxWidth: 9000, cellTemplate: 'App/EnterpriseAccount/Views/Shared/gridDropDown.tpl.html', displayName: 'User Action', headerCellTemplate: '<div>User Action</div>'
                    }
                       // width: "*", minWidth: 125, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
               ]
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
                    { displayName: 'Constituent ID', field: 'pot_merge_mstr_id', width: "*", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Name', field: 'pot_merge_mstr_nm', width: "*", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                    { displayName: 'Address', field: 'address', width: "*", cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' }
                ];
            },
            getDynamicGridLayout: function (gridOptions, uiGridConstants, datas, visibleRowCount) {
                gridOptions.data = datas;

                var width = angular.element(document.getElementsByClassName('grid')[0]).css('width').replace('px', '');
                var indWidth = width / visibleRowCount;

                //Get the maximum height of the grid
                var maxRowHeight = 0;
                angular.forEach(datas, function (rowVal, rowKey) {
                    angular.forEach(rowVal, function (colVal, colKey) {
                        var tempColumnLength = 0;
                        if (!angular.isUndefined(colVal)) {
                            if (colVal != null) {
                                if (angular.isArray(colVal)) {
                                    angular.forEach(colVal, function (objListValue, objListKey) {
                                        angular.forEach(objListValue, function (objListColValue, objListColKey) {
                                            var colLineCount = 0;
                                            if (angular.isDefined(objListColValue.length) && objListColValue.length > 0) {
                                                colLineCount = Math.ceil(objListColValue.length / (indWidth / 10));
                                            }
                                            tempColumnLength = tempColumnLength + (colLineCount * 16);
                                        });
                                    });
                                }
                                else {
                                    tempColumnLength = colVal.length;
                                }

                                var lineCount = 0;
                                if (angular.isDefined(tempColumnLength) && tempColumnLength > 0) {
                                    lineCount = Math.ceil(tempColumnLength / (indWidth / 10));
                                }

                                if (lineCount * 16 > maxRowHeight) {
                                    maxRowHeight = lineCount * 16;
                                }
                            }
                        }
                    });
                });

                //check if grid row has more data
                if (maxRowHeight > (gridOptions.rowHeight)) {
                    gridOptions.rowHeight = maxRowHeight / 2;
                }

                //console.log(datas[0].source_system_count.length);

                angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');

                return gridOptions;
            },
            
           
        }
    }]);