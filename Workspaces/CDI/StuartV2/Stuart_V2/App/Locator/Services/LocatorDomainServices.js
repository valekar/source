angular.module('locator').factory('LocatorDomainServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var sharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");
    var _username;
  
    return {

      
        getLocatorDomainAdvSearchResults: function (postLocatorDomainSearchParams) {
           // console.log(postLocatorDomainSearchParams)
            return $http.post(BasePath + "LocatorNative/LocatorDomainsearch", postLocatorDomainSearchParams, {
                
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {                
                return result;
            }).error(function (result) {                
                return result;
            })
        },

        getLocatorDomainRollback: function (postLocatorDomain) {
            
            //console.log(postLocatorDomain)
            return $http.post(BasePath + "LocatorNative/LocatorDomainRollback", postLocatorDomain, {
                
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            //$http.post(BasePath + "Home/WriteLocatorDomainRecentSearches", postLocatorDomainSearchParams, {
            //    headers: {
            //        "Content-Type": "application/json",
            //        "Accept": "application/json"
            //    }
            //});
            return result;
        }).error(function (result) {
            // console.log(result);
            return result;
        })
    },

        getSearchResultsGridLayout: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getSearchResultsGridLayout_Approved: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getSearchResultsGridLayout_Rejected: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getSearchResultsGridLayout_WFA: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;          
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
                     
            return gridOptions;
        },

        getSearchResultsGridLayout_Reverted: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        clearSearchParams: function ($scope, maxSearchPanelCount, defaultOpenPanelsCount, appendString) {
           
            $scope.LocValidDomain = "";
            $scope.LocInvalidDomain = "";
            $scope.LocStatus = "Waiting for Approval";
           
            for (i = 1; i <= $scope.SearchRows; i++) {

                $scope.LocatorParams.SearchPanel[appendString + i] = true;
                $scope.LocatorParams.PanelSeparator[appendString + i] = true;
                $scope.LocatorParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.LocatorParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }
            for (i = $scope.SearchRows + 1; i <= maxSearchPanelCount ; i++) {
                $scope.LocatorParams.SearchPanel[appendString + i] = false;
                $scope.LocatorParams.PanelSeparator[appendString + i] = false;
                $scope.LocatorParams.showCloseButton[appendString + i] = false;

            }
        },

        clearCreateParams: function ($scope) {

            $scope.locator_addr_key = "";
            $scope.cnst_email_addr = "";
            $scope.int_assessmnt_cd = "";
            $scope.ext_hygiene_result = "";
            $scope.code_category = "";
           
        },

    }
}]);

angular.module('locator').factory('LocatorDomainClearDataServices', ['LocatorDomainMultiDataService', 'LocatorDomainDataServices', function (LocatorDomainMultiDataService, LocatorDomainDataServices) {

    return {
        clearMultiData: function () {
            LocatorDomainMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            LocatorDomainDataServices.clearSearchData();
        }
    }

}]);

var allCaseDatas = {
    LocatorDomainInfoData: [],   
};

angular.module('locator').factory('LocatorDomainMultiDataService', ['CONSTANTS', function (CONSTANTS) {
    
    return {

        getData: function (type) {
            switch (type) {
                case CONSTANTS.LOCATOR_INFO: { return allCaseDatas.LocatorDomainInfoData; break; };
               
            }
        },

        setData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.LOCATOR_INFO: { allCaseDatas.LocatorDomainInfoData = resultData; break; };
            
            }
        },

        clearData: function () {
            allCaseDatas = {
                LocatorDomainInfoData: [],             
            };
        },

        pushAData: function (type, aData) {
            switch (type) {

                case CONSTANTS.LOCATOR_INFO: { allCaseDatas.LocatorDomainInfoData.push(aData); break; };
            }
        }
    }
}]);

angular.module('locator').factory('LocatorDomainDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {

    var searchResultsData = [];
    var searchResultsData_Approved = [];
    var searchResultsData_Rejected = [];
    var searchResultsData_WFA = [];
    var searchResultsData_Reverted = [];

    var searchResultsDataDetail = [];
    var constNameDetailsData = [];
    var constBestSmryDetailsData = [];
    var cartData = [];
    var ConstNameGrid;
    var searchParams;
    var LocatorDetails;
    var headerDisplayStatus;
    

    return {
        setHeaderCtrlDisplayStatus: function (status) {
            headerDisplayStatus = status;
        },
        getHeaderCtrlDisplayStatus: function () {
            return headerDisplayStatus;
        },
        setSearchResutlsData: function (results) {
            searchResultsData = results;
        },
        getSearchResultsData: function () {
            return searchResultsData;
        },

        setSearchResutlsData_Approved: function (results) {
            searchResultsData_Approved = results;
        },
        getSearchResultsData_Approved: function () {
            return searchResultsData_Approved;
        },


        setSearchResutlsData_Rejected: function (results) {
            searchResultsData_Rejected = results;
        },
        getSearchResultsData_Rejected: function () {
            return searchResultsData_Rejected;
        },


        setSearchResutlsData_WFA: function (results) {
            searchResultsData_WFA = results;
        },
        getSearchResultsData_WFA: function () {
            return searchResultsData_WFA;
        },

        setSearchResutlsData_Reverted: function (results) {
            searchResultsData_Reverted = results;
        },
        getSearchResultsData_Reverted: function () {
            return searchResultsData_Reverted;
        },
        
        //clearSearchData: function () {
        //    searchResultsData = [];
        //},  
      
        getSearchResultsColumnDef: function (uiGridConstants) {
           
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


           
            return [
                
            {
                field: 'domain_part', displayName: 'Invalid Domain'
            },
            
            {
                field: 'poss_domain_corrctn', displayName: 'Valid Domain'
            },
            {
                field: 'cnt', displayName: 'Email Count'
            },
            {
                field: 'sts', displayName: 'Status', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'trans_key', displayName: 'Transaction Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'user_id', displayName: 'User ID', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

             {
                 field: 'dw_srcsys_trans_ts', displayName: 'DW TimeStamp', filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
             },
              //{
              //    field: 'row_stat_cd', displayName: 'DW TimeStamp', filter: {
              //        condition: uiGridConstants.filter.STARTS_WITH
              //    },
              //},
            
            ]
        },

        getSearchResultsColumnDef_Approved: function (uiGridConstants) {

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



            return [
                

            {
                field: 'domain_part', displayName: 'Invalid Domain'
            },

            {
                field: 'poss_domain_corrctn', displayName: 'Valid Domain'
            },
            {
                field: 'cnt', displayName: 'Email Count'
            },
            {
                field: 'sts', displayName: 'Status',filter: {condition: uiGridConstants.filter.STARTS_WITH },
            },
            {
                field: 'trans_key', displayName: 'Transaction Key', filter: { condition: uiGridConstants.filter.STARTS_WITH },
            },
            {
                field: 'user_id', displayName: 'User ID', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

             {
                 field: 'dw_srcsys_trans_ts', displayName: 'DW TimeStamp'
             },
              //{
              //    field: 'row_stat_cd', displayName: 'DW TimeStamp', filter: {
              //        condition: uiGridConstants.filter.STARTS_WITH
              //    },
              //},
              
            ]
        },

        getSearchResultsColumnDef_Rejected: function (uiGridConstants) {

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



            return [
                
            {
                field: 'domain_part', displayName: 'Invalid Domain'
            },

            {
                field: 'poss_domain_corrctn', displayName: 'Valid Domain'
            },
            {
                field: 'cnt', displayName: 'Email Count'
            },
            {
                field: 'sts', displayName: 'Status', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'trans_key', displayName: 'Transaction Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'user_id', displayName: 'User ID', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

             {
                 field: 'dw_srcsys_trans_ts', displayName: 'DW TimeStamp', filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
             },
              //{
              //    field: 'row_stat_cd', displayName: 'DW TimeStamp', filter: {
              //        condition: uiGridConstants.filter.STARTS_WITH
              //    },
              //},

            ]
        },

        getSearchResultsColumnDef_WFA: function (uiGridConstants) {

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



            return [
                
            {
                field: 'domain_part', displayName: 'Invalid Domain'
            },

            {
                field: 'poss_domain_corrctn', displayName: 'Valid Domain'
            },
            {
                field: 'cnt', displayName: 'Email Count'
            },
            {
                field: 'sts', displayName: 'Status', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
             {
                 field: 'dw_srcsys_trans_ts', displayName: 'DW TimeStamp', filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
             },
            {
                field: 'trans_key', displayName: 'Transaction Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'user_id', displayName: 'User ID', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            
              //{
              //    field: 'row_stat_cd', displayName: 'DW TimeStamp', filter: {
              //        condition: uiGridConstants.filter.STARTS_WITH
              //    },
              //},

            ]
        },

        getSearchResultsColumnDef_Reverted: function (uiGridConstants) {

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



            return [
                
            {
                field: 'domain_part', displayName: 'Invalid Domain'
            },

            {
                field: 'poss_domain_corrctn', displayName: 'Valid Domain'
            },
            {
                field: 'cnt', displayName: 'Email Count'
            },
            {
                field: 'sts', displayName: 'Status', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'trans_key', displayName: 'Transaction Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'user_id', displayName: 'User ID', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

             {
                 field: 'dw_srcsys_trans_ts', displayName: 'DW TimeStamp', filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
             },
              //{
              //    field: 'row_stat_cd', displayName: 'DW TimeStamp', filter: {
              //        condition: uiGridConstants.filter.STARTS_WITH
              //    },
              //},

            ]
        },



        getSearchParams: function ($scope ,searchRowCount,appendString) {
           
            var postParams = { "LocatorDomainInputSearchModel": [] };
            var searchParams;
            function returnNewParams() {

                searchParams = {
                    
                    "LocValidDomain": "",
                    "LocInvalidDomain": "",
                    "LocStatus": "",                   
                };
                return searchParams;
            };

            returnNewParams();


            
            searchParams["LocValidDomain"] = $scope.LocValidDomain;
            searchParams["LocInvalidDomain"] = $scope.LocInvalidDomain;          

            if ($scope.LocStatus)
                searchParams["LocStatus"] = $scope.LocStatus;
                
            else
                searchParams["LocStatus"] = undefined;
            //console.log(searchParams);
            postParams["LocatorDomainInputSearchModel"].push(searchParams);
            returnNewParams();
            $scope.validDomain = $scope.LocValidDomain;
            $scope.InvalidDomain = $scope.LocInvalidDomain;
            $scope.maydrpvalue = $scope.LocStatus;

            return postParams;
        },

     

        setSearchObj: function (postJSON) {
            sharedPostParams = postJSON;
            // console.log(sharedPostParams);
        }, getSearchObj: function () {
            try {
                return sharedPostParams;
            }
            catch (err) {
                return null;
            }

        },
        setSearchParams: function (searchParam) {
           
            searchParams = searchParam;
        },

        setSearchParams_Approved: function (searchParam_Approved) {

            searchParams_Approved = searchParam_Approved;
        },

        setSearchParams_Rejected: function (searchParam_Rejected) {

            searchParams_Rejected = searchParam_Rejected;
        },

        setSearchParams_WFA: function (searchParam_WFA) {

            searchParams_WFA = searchParam_WFA;
        },

        setSearchParams_Reverted: function (searchParam_Reverted) {

            searchParams_Reverted = searchParam_Reverted;
        }, 

        getSavedSearchParams: function () {
            return searchParams;
        },

        setNormalPageLayout: function ($scope, $localStorage) {            $

            $scope.toggleConstDetails = { "display": "block" };
            $scope.toggleHeader = !$scope.toggleHeader;
            $scope.pleaseWait = { "display": "none" };
            // $scope.toggleAdvancedSearchHeader = true;

        },

        getSearchGridOptions: function (uiGridConstants, columnDefs) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 10,
                enablePagination: true,
                paginationPageSizes: [10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs,
                enableColumnResizing: true,
                enableColumnMoving: true,
                //For selective export
                enableGridMenu: true,
                enableSelectAll: true,
                exporterCsvFilename: 'LOCATOR_Domain_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
           // gridOptions.data = '';
            return gridOptions;
        },

        getSearchGridOptions_Approved: function (uiGridConstants, columnDefs_Approved) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 10,
                enablePagination: true,
                paginationPageSizes: [10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs_Approved,
                enableColumnResizing: true,
                enableColumnMoving: true,
                //For selective export
                enableGridMenu: true,
                enableSelectAll: true,
                exporterCsvFilename: 'LOCATOR_Domain_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
            // gridOptions.data = '';
            return gridOptions;
        },

        getSearchGridOptions_WFA: function (uiGridConstants, columnDefs_WFA) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 10,
                enablePagination: true,
                paginationPageSizes: [10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs_WFA,
                enableColumnResizing: true,
                enableColumnMoving: true,
                //For selective export
                enableGridMenu: true,
                enableSelectAll: true,
                exporterCsvFilename: 'LOCATOR_Domain_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
            // gridOptions.data = '';
            return gridOptions;
        },

        getSearchGridOptions_Rejected: function (uiGridConstants, columnDefs_Rejected) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 10,
                enablePagination: true,
                paginationPageSizes: [10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs_Rejected,
                enableColumnResizing: true,
                enableColumnMoving: true,
                //For selective export
                enableGridMenu: true,
                enableSelectAll: true,
                exporterCsvFilename: 'LOCATOR_Domain_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
            // gridOptions.data = '';
            return gridOptions;
        },

        getSearchGridOptions_Reverted: function (uiGridConstants, columnDefs_Reverted) {

             var gridOptions = {
                 enableRowSelection: true,
                 enableRowHeaderSelection: true,
                 enableFiltering: false,
                 enableSelectAll: true,
                 selectionRowHeaderWidth: 35,
                 rowHeight: 43,
                 //rowTemplate: rowtpl,
                 paginationPageSize: 10,
                 enablePagination: true,
                 paginationPageSizes: [10, 15, 20, 25, 30, 35, 40, 45, 50],
                 enablePaginationControls: false,
                 enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                 enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                 enableGridMenu: true,
                 showGridFooter: false,
                 columnDefs: columnDefs_Reverted,
                 enableColumnResizing: true,
                 enableColumnMoving: true,
                 //For selective export
                 enableGridMenu: true,
                 enableSelectAll: true,
                 exporterCsvFilename: 'LOCATOR_Domain_data.csv',
                 exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                 exporterMenuPdf: false,
                 multiselect: true

             };
             // gridOptions.data = '';
             return gridOptions;
         },

        clearSearchData: function () {
            searchResultsData = [];
            searchResultsData_Approved = [];
            searchResultsData_Rejected = [];
            searchResultsData_WFA = [];
            searchResultsData_Reverted = [];
        },

        
    }
}]);

angular.module('locator').factory('LocatorDomainClearDataService', ['LocatorDomainMultiDataService', 'LocatorDomainDataServices', function (LocatorDomainMultiDataService, LocatorDomainDataServices) {

    return {
        clearMultiData: function () {
            LocatorDomainMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            LocatorDomainDataServices.clearSearchData();
        }
    }

}]);

angular.module('locator').constant('CONSTANTS', {
    LOCATOR_INFO: 'LocatorInfo',
    LOCATOR_DETAILS: 'LocatorDetails',
    
});

var LOCATOR_CRUD_CONSTANTS = {
    EDIT_SUCCESS_MESSAGE: 'The record was successfully edited',
    DELETE_SUCCESS_MESSAGE: 'The Record was successfully deleted',
    SUCCESS_MESSAGE: 'The record was successfully inserted!',
    FAILURE_MESSAGE: 'The record was not inserted.',
    EDIT_FAILURE_MESSAGE: 'The record was not edited',
    DELETE_FAILURE_MESSAGE: 'The record was not deleted',
    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    SUCCESS_REASON: 'Transaction Key: ',
    FAIULRE_REASON: 'It looks like there is a similar record in the database. Please review.',
    SUCCESS_CONFIRM: 'Success!',
    FAILURE_CONFIRM: 'Failed!',
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',
    SOURCE_SYS: "STRX",
    ROW_CODE: 'I',
    DUPLICATE_MSG: 'Duplicate record found.',
    PROCEDURE: {
        SUCCESS: 'Success',
        DUPLICATE: 'duplicate',
        NOT_PRESENT: 'The original record is not present.'
    },
    LOCATORDOMAININFO: {
        EDIT: "EditLocator",
        DELETE: "DeleteLocator",
        DELETE_FAILURE_REASON: 'Locator Domain cannot be deleted since there are transactions associated with the Domain.',
    },

    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',
    SERVICE_TIMEOUT: 'Timed out',
    SERVICE_TIMEOUT_CONFIRM: 'Error: Timed Out',
    SERVICE_TIMEOUT_MESSAGE: 'The service/database timed out. Please try again after some time.',
    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM: "Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org)."

};