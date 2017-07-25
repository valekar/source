angular.module('locator').factory('LocatorServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var sharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");
    var _username;
  
    return {

      
        getLocatorEmailAdvSearchResults: function (postLocatorEmailSearchParams) {
           // console.log(postLocatorEmailSearchParams)
            return $http.post(BasePath + "LocatorNative/LocatorEmailsearch", postLocatorEmailSearchParams, {
                
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                $http.post(BasePath + "Home/WriteLocatorEmailRecentSearches", postLocatorEmailSearchParams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                });
                return result;
            }).error(function (result) {
                // console.log(result);
                return result;
            })
        },

        getLocatorEmailAdvSearchResultsByID: function (postLocatorEmailSearchParamsByID) {
         
            return $http.post(BasePath + "LocatorNative/LocatorEmailDetailsByID", postLocatorEmailSearchParamsByID, {
                
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            //$http.post(BasePath + "Home/WriteLocatorEmailRecentSearches", postLocatorEmailSearchParams, {
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

        getLocatorEmailConstAdvSearchResultsByID: function (postLocatorEmailSearchParamsByID) {
            

            return $http.post(BasePath + "LocatorNative/LocatorEmailConstDetailsByID", postLocatorEmailSearchParamsByID, {

                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                //$http.post(BasePath + "Home/WriteLocatorEmailRecentSearches", postLocatorEmailSearchParams, {
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

        getSearchResultsGridLayoutDetail: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getSearchResultsGridLayoutDetailConstituent: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;            
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getLocatorEmailRecentSearches: function () {
            return $http.get(BasePath + "Home/GetLocatorEmailRecentSearches",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                    if (result == "" || result == null) {
                        //  console.log("Failed to get Locator Recent Searches");
                    }
                    else {
                        //  console.log(result);
                        return result;
                    }
                }).error(function (result) {
                    // console.log(result);
                    return result;
                })
        },

        clearSearchParams: function ($scope, maxSearchPanelCount, defaultOpenPanelsCount, appendString) {
           
            for (i = 1; i <= maxSearchPanelCount; i++)
            {
                $scope.LocatorParams.locator_addr_key[appendString + i] = "";
                $scope.LocatorParams.cnst_email_addr[appendString + i] = "";
                $scope.LocatorParams.int_assessmnt_cd[appendString + i] = "";
                $scope.LocatorParams.ext_hygiene_result[appendString + i] = "";
                $scope.LocatorParams.code_category[appendString + i] = "";
                $scope.LocatorParams.exactmatchChkbx[appendString + i] = false;
               

            }
            $scope.SearchRows = defaultOpenPanelsCount;
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

angular.module('locator').factory('LocatorEmailClearDataServices', ['LocatorEmailMultiDataService', 'LocatorEmailDataServices', function (LocatorEmailMultiDataService, LocatorEmailDataServices) {

    return {
        clearMultiData: function () {
            LocatorEmailMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            LocatorEmailDataServices.clearSearchData();
        }
    }

}]);


var allLocatorDatas = {
    locatorEmailInfoData: [],   
};

angular.module('locator').factory('LocatorEmailMultiDataService', ['CONSTANTS', function (CONSTANTS) {
    
    return {

        getData: function (type) {
            switch (type) {
                case CONSTANTS.LOCATOR_INFO: { return allCaseDatas.locatorEmailInfoData; break; };
               
            }
        },

        setData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.LOCATOR_INFO: { allCaseDatas.locatorEmailInfoData = resultData; break; };
            
            }
        },

        clearData: function () {
            allCaseDatas = {
                locatorEmailInfoData: [],             
            };
        },

        pushAData: function (type, aData) {
            switch (type) {

                case CONSTANTS.LOCATOR_INFO: { allCaseDatas.locatorEmailInfoData.push(aData); break; };
            }
        }
    }
}]);

angular.module('locator').factory('LocatorEmailDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {

    var searchResultsData = [];
    var searchResultsDataDetail = [];
    var searchResultsDataDetailConstituent = [];
    var constNameDetailsData = [];
    var constBestSmryDetailsData = [];
    var cartData = [];
    var ConstNameGrid;
    var searchParams;
    var LocatorDetails;
    var headerDisplayStatus;
    var FinalAssesmentCodeDropDowns;

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

        setSearchResutlsDataDetail: function (results) {
            searchResultsDataDetail = results;
        },
        getSearchResultsDataDetail: function () {
            return searchResultsDataDetail;
        },

        setSearchResutlsDataDetailConstituent: function (results) {
            searchResultsDataDetailConstituent = results;
        },
        getSearchResultsDataDetailConstituent: function () {
            return searchResultsDataDetailConstituent;
        },

        clearSearchData: function () {
            searchResultsData = [];
        },
        setCaseInfoGrid: function (GridOpt) {
            CaseInfoGrid = GridOpt;
        },
        getCaseInfoGrid: function () {
            return CaseInfoGrid;
        },

        setLocatorDetailsGrid: function (GridOpt) {
            LocatorDetailsGrid = GridOpt;
        },
        getLocatorDetailsGrid: function () {
            return LocatorDetailsGrid;
        },

       
        setLocatorDetails: function (result) {
            locatorDetails = result;
        },
        getLocatorDetails: function () {
            return locatorDetails;
        },

        getFinalAssesmentCodeDropDowns: function () {
            return FinalAssesmentCodeDropDowns;
        },
        setFinalAssesmentCodeDropDowns: function (data) {
            FinalAssesmentCodeDropDowns = data;
        },

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


            var linkCellTemplate =
                         '<a class="text-center" ng-href="#" ng-click="grid.appScope.setGlobalValues(row)">{{COL_FIELD}}</a>';
            return [
                {
                    field: 'cnst_email_addr', displayName: 'Email Address', enableCellEdit: false,
                    cellTemplate: linkCellTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                          if (grid.columns[0]) {
                            return 'first-col-style';
                        }
                    },
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                },
            {
                field: 'email_key', displayName: 'Email Key'
            },

            {
                field: 'cnst_email_cnt', displayName: 'Bridge Count', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'int_assessmnt_cd',  displayName: 'Internal Assessment Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_hygiene_result', displayName: 'External Hygiene Result', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'code_category', displayName: 'Final Assessment Code:Category', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
         
            //{
            //    field: 'Action', displayName: 'User Action', cellTemplate: '<button id="btn-append-to-body" type="button" class="btn btn-default" style="background-color: #fff" ng-click="grid.appScope.commonEditGridRow(row.entity,grid)">Edit <span class="glyphicon glyphicon-pencil"></span></button>',
            //               }
            ]
        },       

        getSearchParams: function ($scope ,searchRowCount,appendString) {
           
            var postParams = { "LocatorEmailInputSearchModel": [] };
            var searchParams;
            function returnNewParams() {

                searchParams = {                    
                    "LocEmailKey": "",
                    "LocEmailId":"",
                    "IntAssessCode": "",
                    "ExtAssessCode": "",
                    "ExactMatch": "",
                };
                return searchParams;
            };

            returnNewParams();


            for (i = 1; i <= searchRowCount; i++)
            {
                searchParams["LocEmailKey"] = $scope.LocatorParams.locator_addr_key[appendString + i];
                searchParams["LocEmailId"] = $scope.LocatorParams.cnst_email_addr[appendString + i];
                searchParams["IntAssessCode"] = $scope.LocatorParams.int_assessmnt_cd[appendString + i];
                //searchParams["ext_hygiene_result"] = $scope.LocatorParams.ext_hygiene_result[appendString + i];
                if ($scope.LocatorParams.code_category[appendString + i])                    
                    searchParams["ExtAssessCode"] = $scope.LocatorParams.code_category[appendString + i];
                else
                    searchParams["ExtAssessCode"] = undefined;
                searchParams["ExactMatch"] = $scope.LocatorParams.exactmatchChkbx[appendString + i]; 

                postParams["LocatorEmailInputSearchModel"].push(searchParams);
                returnNewParams();
            }
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
        getSavedSearchParams: function () {
            return searchParams;
        },

        setNormalPageLayout: function ($scope, $localStorage) {
            $scope.cnst_email_addr = $localStorage.cnst_email_addr;
            $scope.locator_addr_key = $localStorage.locator_addr_key;

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
                exporterCsvFilename: 'LOCATOR_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
           // gridOptions.data = '';
            return gridOptions;
        },

        getSearchGridOptionsDetails: function (uiGridConstants, columnDefs) {

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
                exporterCsvFilename: 'LOCATOR_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
            // gridOptions.data = '';
            return gridOptions;
        },

        getSearchGridOptionsConstituents: function (uiGridConstants, columnDefs) {

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
                exporterCsvFilename: 'LOCATOR_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
            // gridOptions.data = '';
            return gridOptions;
        },

        clearSearchData: function () {
            searchResultsData = [];
        },

        getSearchDetailResultsColumnDef: function (uiGridConstants) {

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


            return [

            {
                field: 'cnst_email_addr', displayName: 'Locator Email Address', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'int_assessmnt_cd', displayName: 'Internal Assessment Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_status_cd', displayName: 'External Status Code', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_reason_cd', displayName: 'External Reason Code', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_hygiene_result', displayName: 'External Hygiene Result', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_net_protected_ind', displayName: 'External Net Protected Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_net_protected_by', displayName: 'External Net Protected By', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_disp_domain_ind', displayName: 'External Disposable Domain Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_poss_disp_addr_ind', displayName: 'External Possible Disposable Address Indicator' , visible: false,
                menuItems: customGridFilter("", $rootScope),
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            },
            {
                field: ' ext_role_based_addr_ind', displayName: 'External Role Based Address Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'local_part', displayName: 'Local Part', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'domain_part', displayName: 'Domain Part', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_poss_domain_corrctn_ind', displayName: 'Domain Correction Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            },
            {
                field: 'ext_poss_addr_corrctn', displayName: 'External Possible Address Correction',visible: false,
                menuItems: customGridFilter("", $rootScope),
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            },
            {
                field: 'ext_pot_vulgar_domain_ind', displayName: 'External Potential Vulgar Domain Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_pot_vulgar_addr_ind', displayName: 'External Potential Vulgar Address Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ext_returned_from_cache_ind', displayName: 'External Returned From Cache Indicator', visible: false,
                menuItems: customGridFilter("", $rootScope),
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            },
            {
                field: 'assessmnt_overridden', displayName: 'Assessment Overridden',  visible: false,
                menuItems: customGridFilter("", $rootScope),
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            },
            {
                field: 'final_assessmnt_date', displayName: 'Final Assessment Date', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: ' trans_key', displayName: 'Transaction Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'assessmnt_key', displayName: 'Assessment Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'dw_srcsys_trans_ts', displayName: 'Data Warehouse Transaction Timestamp', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'row_stat_cd', displayName: 'Row Status Code', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'appl_src_cd', displayName: 'Application Source Code', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'load_id', displayName: 'Load Identifier', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'code_category', displayName: 'Final Assessment', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

             {
                 field: 'Action', displayName: 'User Action', cellTemplate: '<button id="btn-append-to-body" type="button" class="btn btn-default" style="background-color: #fff" ng-click="grid.appScope.commonEditGridRow(row.entity,grid)">Edit <span class="glyphicon glyphicon-pencil"></span></button>',
             }
            ]
        },

        getSearchConstituentsDetailResultsColumnDef: function (uiGridConstants) {
            var masterIdTemplate =
                       '<a class="text-center wordwrap" ng-href="#" ng-click="$event.preventDefault(); grid.appScope.getConstituentDetails(row)">{{COL_FIELD}}</a>';

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
                         '<a class="text-center" ng-href="#" ng-click="grid.appScope.setGlobalValues(row)">{{COL_FIELD}}</a>';
            return [
                //{
                //    field: 'constituent_id', displayName: 'Email Address', enableCellEdit: false,
                //    cellTemplate: linkCellTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                //        // console.log("row values");
                //        // console.log(row.entity);
                //        if (grid.columns[0]) {
                //            return 'first-col-style';
                //        }
                //    },
                //    filter: {
                //        condition: uiGridConstants.filter.STARTS_WITH
                //    },
                //},
              
                 {
                     field: 'constituent_id', displayName: 'Master Id', 
                     cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                         if (grid.columns[0]) {
                             return '';
                         }
                     },
                 },
            {
                field: 'name', displayName: 'Name',cellTemplate: masterIdTemplate,
            },
            {
                field: 'email_address', displayName: 'Email',

            },
            {
                field: 'addr_line_1', displayName: 'Address', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'phone_number', displayName: 'Phone', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
             {
                 field: 'constituent_type', displayName: 'Constituent Type'
             },
            
            ]
        },
    }
}]);

angular.module('locator').factory('LocatorEmailClearDataService', ['LocatorEmailMultiDataService', 'LocatorEmailDataServices', function (LocatorEmailMultiDataService, LocatorEmailDataServices) {

    return {
        clearMultiData: function () {
            LocatorEmailMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            LocatorEmailDataServices.clearSearchData();
        }
    }

}]);

angular.module('locator').constant('CONSTANTS', {
    LOCATOR_INFO: 'LocatorInfo',
    LOCATOR_DETAILS: 'LocatorDetails',
    
});

angular.module('locator').factory('LocatorEmailDropDownService', ['$http', function ($http) {
   
    var URL = {}; var URLFinal_Assement_Edit = {};
    var result = "";  
    URL[LOCATOR_CONSTANTS.LOCATOR_FINALASSESMENT_CODE] = "Home/getFinalAssesmentCodeEmail";
    URLFinal_Assement_Edit[LOCATOR_CONSTANTS.LOCATOR_MANUALLASSESMENT_CODE] = "Home/getManualEmailAssessmentCode";
 
    return {
        getDropDown: function (type) {
            //console.log(URL[type]);
            return $http.get(URL[type], {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                // console.log(result);
                return result;
            }).error(function (result) {
               return result;
            });
        },
        
        getDropDown1: function (type) {
            //console.log(URLFinal_Assement_Edit[type]);
            return $http.get(URLFinal_Assement_Edit[type], {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                // console.log(result);
                return result;
            }).error(function (result) {
                return result;
            });
        }
    }

}]);

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

