angular.module('locator').factory('LocatorAddressServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var sharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");
    var _username;
  
    return {

      
        getLocatorAddressAdvSearchResults: function (postLocatorAddressSearchParams) {
         
            return $http.post(BasePath + "LocatorNative/LocatorAddresssearch", postLocatorAddressSearchParams, {
                
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                $http.post(BasePath + "Home/WriteLocatorAddressRecentSearches", postLocatorAddressSearchParams, {
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

        getLocatorAddressAdvSearchResultsByID: function (postLocatorAddressSearchParamsByID) {
         
            return $http.post(BasePath + "LocatorNative/LocatorAddressDetailsByID", postLocatorAddressSearchParamsByID,
                {
                
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            //$http.post(BasePath + "Home/WriteLocatorAddressRecentSearches", postLocatorAddressSearchParams, {
            //    headers: {
            //        "Content-Type": "application/json",
            //        "Accept": "application/json"
            //    }
            //});
          
            return result;
        }).error(function (result){           
            return result;
        })
        },
        
        getLocatorAddressConstituentsData: function (postLocatorAddressSearchParamsByID) {

            return $http.post(BasePath + "LocatorNative/LocatorAddressConstituentsDetailsByID", postLocatorAddressSearchParamsByID,
                {

                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                }).success(function (result) {
                    //$http.post(BasePath + "Home/WriteLocatorAddressRecentSearches", postLocatorAddressSearchParams, {
                    //    headers: {
                    //        "Content-Type": "application/json",
                    //        "Accept": "application/json"
                    //    }
                    //});
                    //console.log(result);
                    return result;
                }).error(function (result) {
                   // console.log(result);
                    return result;
                })
        },

        getLocatorAddressAssessmentData: function (postLocatorAddressSearchParamsByID) {

            return $http.post(BasePath + "LocatorNative/LocatorAddressAssessmentDetailsByID", postLocatorAddressSearchParamsByID,
                {

                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                }).success(function (result) {
                    //$http.post(BasePath + "Home/WriteLocatorAddressRecentSearches", postLocatorAddressSearchParams, {
                    //    headers: {
                    //        "Content-Type": "application/json",
                    //        "Accept": "application/json"
                    //    }
                    //});
                    //console.log(result);
                    return result;
                }).error(function (result) {
                    //console.log(result);
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

        getSearchResultsGridLayoutDetail_Constituents: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getSearchResultsGridLayoutDetail_Address_Assessments: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },

        getLocatorAddressRecentSearches: function () {
            return $http.get(BasePath + "Home/GetLocatorAddressRecentSearches",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                    if (result == "" || result == null) {
                        //  console.log("Failed to get Case Recent Searches");
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
                $scope.LocatorParams.line1_addr[appendString + i] = "";
                $scope.LocatorParams.city[appendString + i] = "";
                $scope.LocatorParams.state[appendString + i] = "";
                $scope.LocatorParams.zip_5[appendString + i] = "";
                $scope.LocatorParams.deliv_loc_type[appendString + i] = "";
                $scope.LocatorParams.dpv_cd[appendString + i] = "";
                $scope.LocatorParams.code_category[appendString + i] = "";
               

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
            $scope.line1_addr = "";
            $scope.line2_addr = "";
            $scope.city = "";
            $scope.state = "";
            $scope.zip_5 = "";
            $scope.deliv_loc_type = "";
            $scope.dpv_cd = "";
            $scope.code_category = "";
           
        },

    }
}]);

angular.module('locator').factory('LocatorAddressClearDataServices', ['LocatorAddressMultiDataService', 'LocatorAddressDataServices', function (LocatorAddressMultiDataService, LocatorAddressDataServices) {

    return {
        clearMultiData: function () {
            LocatorAddressMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            LocatorAddressDataServices.clearSearchData();
        }
    }

}]);


var allCaseDatas = {
    LocatorAddressInfoData: [],   
};

angular.module('locator').factory('LocatorAddressMultiDataService', ['CONSTANTS', function (CONSTANTS) {
    
    return {

        getData: function (type) {
            switch (type) {
                case CONSTANTS.LOCATOR_INFO: { return allCaseDatas.LocatorAddressInfoData; break; };
               
            }
        },

        setData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.LOCATOR_INFO: { allCaseDatas.LocatorAddressInfoData = resultData; break; };
            
            }
        },

        clearData: function () {
            allCaseDatas = {
                LocatorAddressInfoData: [],             
            };
        },

        pushAData: function (type, aData) {
            switch (type) {

                case CONSTANTS.LOCATOR_INFO: { allCaseDatas.LocatorAddressInfoData.push(aData); break; };
            }
        }
    }
}]);

angular.module('locator').factory('LocatorAddressDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {

    var searchResultsData = [];
    var searchResultsDataDetail = [];
    var searchResultsDataDetail_Constituents = [];
    var searchResultsDataDetail_Address_Assessments = [];
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


        setSearchResutlsDataDetail_Constituents: function (results) {
            searchResultsDataDetail_Constituents = results;
        },
        getSearchResultsDataDetail_Constituents: function () {
            return searchResultsDataDetail_Constituents;
        },

        setSearchResutlsDataDetail_Address_Assessments: function (results) {
            searchResultsDataDetail_Address_Assessments = results;
        },
        getSearchResultsDataDetail_Address_Assessments: function () {
            return searchResultsDataDetail_Address_Assessments;
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
                    field: 'locator_addr_key', displayName: 'Locator Address Key', enableCellEdit: false,
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
                field: 'line1_addr', displayName: 'Address Line 1'
            },
            {
                field: 'line2_addr', displayName: 'Address Line 2', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'city', displayName: 'City', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
             {
                 field: 'state', displayName: 'State', filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
             },

              {
                  field: 'zip_5', displayName: 'Zip', filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
              },
               {
                   field: 'dpv_cd', displayName: 'Deliverability Code', filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
               },
                {
                    field: 'deliv_loc_type', displayName: 'Delivery Locator Type', filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                },
            {
                field: 'code_category', displayName: 'Assessment Code: Category', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
         
            //{
            //    field: 'Action', displayName: 'Action',cellTemplate: '<button id="btn-append-to-body" type="button" class="btn btn-default" style="background-color: #fff" ng-click="grid.appScope.commonEditGridRow(row.entity,grid)">Edit <span class="glyphicon glyphicon-pencil"></span></button>',
               

            //}
            ]
        },        

        getSearchParams: function ($scope,searchRowCount,appendString) {
           
            var postParams = { "LocatorAddressInputSearchModel": [] };
            var searchParams;
            function returnNewParams() {

                searchParams = {                    
                    "LocAddrKey": "",
                    "LocAddressLine": "",
                    "LocCity": "",
                    "LocState": "",
                    "LocZip": "",
                    "LocDelType": "",
                    "LocDelCode": "",
                    "LocAssessCode": "",
                };
                return searchParams;
            };

            returnNewParams();


            for (i = 1; i <= searchRowCount; i++) {
                searchParams["LocAddrKey"] = $scope.LocatorParams.locator_addr_key[appendString + i];
                searchParams["LocAddressLine"] = $scope.LocatorParams.line1_addr[appendString + i];               
                searchParams["LocCity"] = $scope.LocatorParams.city[appendString + i];
                searchParams["LocZip"] = $scope.LocatorParams.zip_5[appendString + i];
                if ($scope.LocatorParams.state[appendString + i])
                    searchParams["LocState"] = $scope.LocatorParams.state[appendString + i];
                else
                    searchParams["LocState"] = undefined;

                if ($scope.LocatorParams.deliv_loc_type[appendString + i])
                    searchParams["LocDelType"] = $scope.LocatorParams.deliv_loc_type[appendString + i];
                else
                    searchParams["LocDelType"] = undefined;

                if ($scope.LocatorParams.dpv_cd[appendString + i])
                    searchParams["LocDelCode"] = $scope.LocatorParams.dpv_cd[appendString + i];
                else
                    searchParams["LocDelCode"] = undefined;

                if ($scope.LocatorParams.code_category[appendString + i])                    
                    searchParams["LocAssessCode"] = $scope.LocatorParams.code_category[appendString + i];
                else
                    searchParams["LocAssessCode"] = undefined;
                

                postParams["LocatorAddressInputSearchModel"].push(searchParams);
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
            //$scope.cnst_email_addr = $localStorage.cnst_email_addr;
            $scope.locator_addr_key = $localStorage.locator_addr_key;

            $scope.toggleConstDetails = { "display": "block" };
            $scope.toggleHeader = !$scope.toggleHeader;
            
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

        clearSearchData: function () {
            searchResultsData = [];
        },

        getSearchDetailResultsColumnDef: function (uiGridConstants) {

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
                field: 'locator_addr_key', displayName: 'Address Key', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_num', displayName: 'Street Number', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_name', displayName: 'Street Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_suffix', displayName: 'Street Suffix', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_predir', displayName: 'Street Pre-Directional', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_postdir', displayName: 'Street Post-Directional',  visible: false,
                menuItems: customGridFilter("", $rootScope),
            filter: {
                condition: uiGridConstants.filter.STARTS_WITH
            },
            },
            {
                field: 'sec_sub_bldg1', displayName: 'Street Secondaryr', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'sec_sub_bldg2', displayName: 'Sub-Building Description 1', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            //{
            //    field: 'ext_disp_domain_ind', displayName: 'Street Secondary', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
            //        condition: uiGridConstants.filter.STARTS_WITH
            //    },
            //},
            //{
            //        field: 'ext_poss_disp_addr_ind', displayName: 'Sub-Building Description 2', filter: {
            //        condition: uiGridConstants.filter.STARTS_WITH
            //    },
            //},
            {
                field: ' postal_phrase', displayName: 'Postal Box Information', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'city', displayName: 'City Name', visible: false,
                    menuItems: customGridFilter("", $rootScope),
                            filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'state', displayName: 'State Name', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'zip_5', displayName: 'Postal Zip 5 Code', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'zip_4', displayName: 'Postal Zip 4 Code', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'county', displayName: 'County', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'country_cd', displayName: 'Country Code', visible: false,
                    menuItems: customGridFilter("", $rootScope),
                            filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'assessmnt_overridden', displayName: 'Assessment Overriden', visible: false,
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
                field: 'trans_key', displayName: 'Transaction Key', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'assessmnt_key', displayName: 'Assessment Key', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'prev_assessmnt_key', displayName: 'Previous Assessment Key', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'dw_srcsys_trans_ts', displayName: 'Datawarehouse Source System Transaction Time Stamp', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'row_stat_cd', displayName: 'Row Stat Code', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'locator_addr_strt_ts', displayName: 'Locator Address Start', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'locator_addr_end_ts', displayName: 'Locator Address End', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'assessmnt_cd_title', displayName: 'Final Assessment', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

             {
                 field: 'Action', displayName: 'User Action', cellTemplate: '<button id="btn-append-to-body" type="button" class="btn btn-default" style="background-color: #fff" ng-click="grid.appScope.commonEditGridRow(row.entity,grid)">Edit <span class="glyphicon glyphicon-pencil"></span></button>',


             }
            ]
        },

        getSearchDetailResultsColumnDef_Constituents: function (uiGridConstants) {
            var masterIdTemplate ='<a class="text-center wordwrap" ng-href="#" ng-click="$event.preventDefault(); grid.appScope.getConstituentDetails(row)">{{COL_FIELD}}</a>';

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
                field: 'constituent_id', displayName: 'Master Id', 
                cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                    if (grid.columns[0]) {
                        return '';
                    }
                },
            },
            {
                field: 'name', displayName: 'Name', cellTemplate: masterIdTemplate, filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'arc_srcsys_cd', displayName: 'Address Source System', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'addr_line_1', displayName: 'Address Line 1', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'addr_line_2', displayName: 'Address Line 2', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_postdir', displayName: 'City', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'state_cd', displayName: 'State	', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'zip', displayName: 'Zip', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
          
            {
                field: 'email_address', displayName: 'Email', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'phone_number', displayName: 'Phone	', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'constituent_type', displayName: 'Constituent Type', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            

             //{
             //    field: 'Action', displayName: 'Action', cellTemplate: 'App/Locator/Views/common/gridDropDownLocatorAddressInfo.tpl.html', displayName: 'User Action', headerCellTemplate: '<div>User Action</div>',
             //    width: "*", minWidth: 100, maxWidth: 9000, headerCellTemplate: GRID_FILTER_TEMPLATE, filter: {
             //        condition: uiGridConstants.filter.STARTS_WITH
             //    },
             //}
            ]
        },

        getSearchDetailResultsColumnDef_Address_Assessments: function (uiGridConstants) {

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
                field: 'locator_addr_key', displayName: 'Locator Address Key', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'deliv_loc_type', displayName: 'Delivery Locator Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'dpv_cd', displayName: 'Deliverability Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'street_suffix', displayName: 'Seed Records Indicator', 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'dpv_fn1', displayName: 'Address Foot Note 1',
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'dpv_fn2', displayName: 'Address Foot Note 2', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'dpv_fn3', displayName: 'Address Foot Note 3', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'high_rise_default', displayName: 'High Rise Indicator Flag',
                filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'mailability_score', displayName: 'Mailability Score', 
                            filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'match_cd', displayName: 'Match Code', 
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: ' postcode5_result_cd', displayName: 'Post Code 5', 
                filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'postcode4_result_cd', displayName: 'Post Code 4', visible: false,
                    menuItems: customGridFilter("", $rootScope),
                            filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'loc_result_cd', displayName: 'Locality Result Code', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'state_result_cd', displayName: 'State Result Code', visible: false,
                menuItems: customGridFilter("", $rootScope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'streetname_result_cd', displayName: 'Street Name Result Code', visible: false,
                        menuItems: customGridFilter("", $rootScope),
                filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'streetnum_result_cd', displayName: 'Street Number Result Code', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'deliv_serv_result_cd', displayName: 'Delivery Serv Result Code', visible: false,
                menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'bldg_result_cd', displayName: 'Building Result Code',visible: false,
                        menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'suite_result_cd', displayName: 'Suite Result Code', visible: false,
                        menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'org_result_cd', displayName: 'Org Result Code', visible: false,
                menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'country_result_cd', displayName: 'Country Result Code', visible: false,
                        menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'trans_key', displayName: 'Transaction Key', visible: false,
                menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'assessmnt_addr_int_strt_ts', displayName: 'Create Timestamp', visible: false,
                        menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },

            {
                field: 'assessmnt_addr_int_end_ts', displayName: 'End Timestamp', visible: false,
                menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'assessmnt_addr_int_strt_ts', displayName: 'DW Timestamp',visible: false,
                        menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            {
                field: 'row_stat_cd', displayName: 'Row Stat Code', visible: false,
                menuItems: customGridFilter("", $rootScope), filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },

            
             //{
             //    field: 'Action', displayName: 'Action', cellTemplate: 'App/Locator/Views/common/gridDropDownLocatorAddressInfo.tpl.html', displayName: 'User Action', headerCellTemplate: '<div>User Action</div>',
             //    width: "*", minWidth: 100, maxWidth: 9000, headerCellTemplate: GRID_FILTER_TEMPLATE, filter: {
             //        condition: uiGridConstants.filter.STARTS_WITH
             //    },
             //}
            ]
        },
    }
}]);

angular.module('locator').factory('LocatorAddressClearDataService', ['LocatorAddressMultiDataService', 'LocatorAddressDataServices', function (LocatorAddressMultiDataService, LocatorAddressDataServices) {

    return {
        clearMultiData: function () {
            LocatorAddressMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            LocatorAddressDataServices.clearSearchData();
        }
    }

}]);


angular.module('locator').constant('CONSTANTS', {
    LOCATOR_INFO: 'LocatorInfo',
    LOCATOR_DETAILS: 'LocatorDetails',
    
});




angular.module('locator').factory('LocatorAddressDropDownService', ['$http', function ($http) {
   
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
    LocatorAddressINFO: {
        EDIT: "EditLocator",
        DELETE: "DeleteLocator",
        DELETE_FAILURE_REASON: 'Locator Email cannot be deleted since there are transactions associated with the Email.',
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