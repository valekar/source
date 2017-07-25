
topAccMod.factory('constituentServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var constSharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");

    return {
        getConstituentSearchResults: function (postConstituentparams) {
            return $http.post(BasePath + "Test/search", postConstituentparams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        getConstituentAdvSearchResults: function (postConstituentparams) {
            return $http.post(BasePath + "Test/advsearch", postConstituentparams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                $http.post(BasePath + "Home/WriteConstituentRecentSearches", postConstituentparams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                });
                // console.log(result);
                return result;
            }).error(function (result) {
                console.log(result);
                return result;
            });
        },
        getConstRecentSearches: function () {
            return $http.get(BasePath + "Home/GetConstituentRecentSearches",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                        return result;
                    
                }).error(function (result) {
                    console.log(result);
                    return result;
                });
        },
        createCase: function (postCaseSearchParams) {
            return $http.post(BasePath + "CaseNative/CreateCase", postCaseSearchParams, {
                //  return $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;

            }).error(function (result) {
                console.log(result);
                return result;
            });
        },
        getCartResults: function () {
            var BasePath1 = $("base").first().attr("href");
            return $http.post(BasePath1 + "home/viewcart", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;

            }).error(function (result) {
                console.log(result);
                return result;
            });
        },
        getUnmergeCartResults: function () {
            return $http.post(BasePath + "home/ViewUnmergeCart", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;

            }).error(function (result) {
                console.log(result);
                return result;
            });
        },

        uploadCaseFiles: function (formdata) {
            var request = {
                
                url: BasePath + 'caseNative/UploadFiles/',
                
            };

            // SEND THE FILES.
            // console.log(formdata);
            if (formdata != null || formdata != undefined) {
                return $http.post(request.url, formdata, {
                    headers: {
                        'Content-Type': undefined
                    }
                }).success(function (result) {
                    return result;

                }).error(function (result) {
                    console.log(result);
                    return result;
                });
                    
            }
            //return;
        },
        getAdvCaseSearchResutls: function (postCaseSearchParams) {
            return $http.post(BasePath + "CaseNative/AdvSearch", postCaseSearchParams, {
                //  return $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                /* $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                     headers: {
                         "Content-Type": "application/json",
                         "Accept": "application/json"
                     }
                 });*/
                //   console.log(result);
                return result;
            }).error(function (result) {
                console.log(result);
                return result;
            });
        },
        addUnmergeToCart: function (postParams) {
            return $http.post(BasePath + "home/AddToUnmergeCart", postParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        addMergeToCart: function (postParams) {
            return $http.post(BasePath + 'home/addtocart', JSON.stringify(postParams), {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) { return result }).error(function (result) {
                return result;
            });
        },
        addMergeToCartPotential: function (postParams) {
            return $http.post(BasePath + 'home/AddToCartMerge', JSON.stringify(postParams), {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) { return result }).error(function (result) {
                return result;
            });
        },
        addMergeConflicts: function (postParams) {
            return $http.post(BasePath + 'home/AddMergeConflictRecords', JSON.stringify(postParams), {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });;
        },
        getMergeConflicts: function () {
            var BasePath1 = $("base").first().attr("href");
            return $http.post(BasePath1 + "home/getMergeConflict", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        removeUnmergeFromCart: function (postParams) {
            return $http.post(BasePath + "home/RemoveUnmergeCartItem", JSON.stringify(postParams), {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        removeMergeGroupFromCart: function (postParams) {
            return $http.post(BasePath + "home/RemoveMergeCartItem", JSON.stringify(postParams), {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        removeMergeFromCart: function (row) {
            return $http.get(BasePath + 'home/RemoveCartItem', {
                params: { IndexString: row.IndexString }
            })
        },
        clearUnmergeCart: function () {
            return $http.get(BasePath + 'home/ClearUnmergeCart').success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        clearMergeCart: function () {
            return $http.get(BasePath + 'home/ClearCart').success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        },
        getSearchGridOptions: function (uiGridConstants, columnDefs,enableSelection) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: enableSelection,
                enableFiltering: false,
                enableSelectAll: false,
                //disables checkbox of the row if request_transkey is greater than -1
               /* isRowSelectable: function (row) {
                    if (row.entity.request_transaction_key >= 0) {
                        return false;
                    } else {
                        return true;
                    }
                },*/
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 10,
                enablePagination: true,
                paginationPageSizes: [10,15,20,25,30,35,40,45,50],
                enablePaginationControls: false,
                enableVerticalScrollbar: 1,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs,
                enableColumnResizing: true,
                enableColumnMoving: true,
                //For selective export
                modifierKeysToMultiSelect: false,
                enableSelectAll: enableSelection,
                exporterCsvFilename: 'constituent_results.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                exporterMenuAllData : false,
                multiselect: enableSelection
            };
            gridOptions.data = '';
            return gridOptions;
        },
        getSearchResultsGridLayout: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;

           /* var max = 0;
            for (var i = 0; i < datas.length; i++) {
                if (datas[i].source_system_count.length > max) {
                    max = datas[i].source_system_count.length;
                    //console.log(datas[i].source_system_count);
                }
            }

            var lengthOfSoruceCount = max;


            //check if source system count has more data
            if (lengthOfSoruceCount > (gridOptions.rowHeight + lengthOfSoruceCount/3)) {
                gridOptions.rowHeight = lengthOfSoruceCount / 2 + 35;
            }

            //console.log(datas[0].source_system_count.length);

            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');*/
            // console.log(datas.length);


            return gridOptions;
        },
        getCartGridOptions: function (uiGridConstants, columnDefs) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: false,

                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 5,
                paginationPageSizes: [5,10,15,20,25,30,35,40,45,50],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs,
                enableColumnResizing: true,
                enableColumnMoving: true

            };
            gridOptions.data = '';
            return gridOptions;
        },
        getCartGridLayout: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            gridOptions.data.length = 0;
            gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            /*if (datas.length > 5) {
                gridOptions.enableVerticalScrollbar = uiGridConstants.scrollbars.ALWAYS;
                gridOptions.minRowsToShow = 5;
            } else {
                gridOptions.enableVerticalScrollbar = uiGridConstants.scrollbars.NEVER;
                gridOptions.minRowsToShow = datas.length;
            }*/

            return gridOptions;
        },
       
      


        getCartColumnDef: function () {
            var linkCellTemplate =
                         '<a class="text-center wordwrap" ng-href="#" ng-click="grid.appScope.setGlobalValues(row)">{{COL_FIELD}}</a>';
            return [
                {
                    field: 'constituent_id', headerTooltip: 'Master ID', displayName: 'Master Id', enableCellEdit: false, cellTemplate: linkCellTemplate,
                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                        if (grid.columns[0]) {
                            return 'first-col-style';
                        }
                    }
                },
                { field: 'name', headerTooltip: 'Name', displayName: 'Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                { field: 'address', headerTooltip: 'Address', displayName: 'Address', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                { field: 'phone_number', headerTooltip: 'Phone Number', displayName: 'Phone Number', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                { field: 'email_address', headerTooltip: 'Email', displayName: 'Email', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                { field: 'constituent_type', headerTooltip: 'Type', displayName: 'Type', width: "*", minWidth: 50, maxWidth: 9000, },
                {
                    field: 'cnst_dsp_id', headerTooltip: 'LN ID', displayName: "LN Id?", width: "*", minWidth: 50, maxWidth: 9000,
                    cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD.length> \'0\' ">Yes</div><div class="wordwrap" ng-if="COL_FIELD.length<= \'0\' || COL_FIELD==null">No</div> '
                },
                {
                    field: 'Action', headerTooltip: 'Action', displayName: 'Action', cellTemplate: '<div class="grid-action-cell">' +
                  '<a herf="#" style="cursor: pointer; color: red; margin-top: 5px; float: left;" ng-click="grid.appScope.removeMergeRow(row.entity)">Remove</a>'
                }


            ]
        },
        getUnmergeColumnDef: function (uiGridConstants) {
            //var gridColumns = new GridColumns();
            //return gridColumns.getGridColumns(uiGridConstants, $rootScope).constExtBridgeColDef;
            return  [
                { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false, visible: false },
                {
                    field: 'cnst_nm',  width: "*", minWidth: 120, maxWidth: 9000,
                     displayName: 'Constituent Name',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'address',  width: "*", minWidth: 200, maxWidth: 9000,
                     displayName: 'Constituent Address',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'source_system_cd',  width: "*", minWidth: 140, maxWidth: 9000,
                     displayName: 'Source System',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'source_system_id',  width: "*", minWidth: 140, maxWidth: 9000,
                     displayName: 'Source System ID',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },

                {
                    field: 'constituent_type',  width: "*", minWidth: 50, maxWidth: 9000,
                    displayName: 'Type',
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'srch_srcsys_id',  width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: 'Source System Secondary ID', visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'dw_srcsys_trans_ts', 
                        displayName: 'DW Timestamp', width: "*", minWidth: 100, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellFilter: 'column_date_filter'
                },
                {
                    field: 'row_stat_cd', 
                    displayName: 'Row Stat Code', width: "*", minWidth: 140, maxWidth: 9000, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                 {
                     field: 'Action', headerTooltip: 'Action', displayName: 'Action', cellTemplate: '<div class="grid-action-cell">' +
                   '<a herf="#" style="cursor: pointer; color: red; margin-top: 5px; float: left;" ng-click="grid.appScope.removeUnmergeRow(row.entity)">Remove</a>'
                 },
                {
                    field: 'close filter', displayName: ' ', minWidth: 22, width: 22
                }
            ]
        }
       
    }



}]);

topAccMod.factory('constituentDataServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var searchResultsData = [];
    var constNameDetailsData = [];
    var constBestSmryDetailsData = [];
    var cartData = [];
    var unMergeCartData = [];
    var advSearchParams = [];
    var ConstNameGrid;
    var phoneSearchData = [];
    var saveSearchParams = [];
    return {
        setSearchResutlsData: function (results) {
            searchResultsData = results;
        },
        //getSearchResultsData: function () {
        //    var dataFromQuickSearch = mainService.getSearchResultsData();
        //    if (dataFromQuickSearch != null) {
        //        return dataFromQuickSearch;
        //    }
        //    else
        //        return searchResultsData;

        //},

        setSaveSearchParams:function(searchParams){
            saveSearchParams = searchParams;
        },
        getSaveSearchParams:function(){
            return saveSearchParams;
        } ,
        setCartData: function (results) {
            CartData = results;
        },
        getCartData: function () {
            return CartData;
        },
        setUnmergeCartData: function (results) {
            unMergeCartData = results;
        },
        getUnmergeCartData: function () {
            return unMergeCartData;
        },
        clearSearchData: function () {
            searchResultsData = [];
        },
        setConstNameGrid: function (GridOpt) {
            ConstNameGrid = GridOpt;
        },
        getConstnameGrid: function () {
            return ConstNameGrid;
        },

        //store the params for export
        setAdvSearchParams: function (postParams) {
            advSearchParams = postParams;
        },
        //getAdvSearchParams: function () {
        //    var quickSearchAdvParams = mainService.getAdvSearchParams();
        //    if (quickSearchAdvParams != null) {
        //        return quickSearchAdvParams;
        //    }
        //    else {
        //        return advSearchParams;
        //    }
        //},

        setPhoneSearchParams: function (params) {
            phoneSearchData.push(params);
        },
        clearPhoneSearchParams: function () {
            phoneSearchData = [];
        },
        getPhoneSearchParams: function () {
            return phoneSearchData;
        },
        getSearchResultsColumnDef: function () {
            var linkCellTemplate =
                         '<a class="text-center wordwrap" ng-href="#" ng-click="grid.appScope.setGlobalValues(row)" style="min-width:80px;max-width:80px;">{{COL_FIELD}}</a>';
            return [{
                field: 'constituent_id', displayName: 'Master Id', enableCellEdit: false,width: "*", minWidth: 80, maxWidth: 150 ,
                cellTemplate: linkCellTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                    if (grid.columns[0]) {
                        return 'first-col-style';
                    }

                }
            },
                { field: 'name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 150, maxWidth: 9000, },
                { field: 'address', displayName: 'Address', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 350, maxWidth: 9000 },
                {
                    field: 'source_system_count', displayName: 'Source System Count',
                    cellTemplate: '<div class="wordwrap" ng-show="row.entity.source_system_count.length>=58" tooltip-placement="right" tooltip-append-to-body="true" uib-tooltip="{{COL_FIELD}}" >{{COL_FIELD}}</div> <div class="wordwrap" row.entity.source_system_count.length<58 >{{COL_FIELD}}</div>',
                    width: "*", minWidth: 200, maxWidth: 9000,

                },
                { field: 'phone_number', displayName: 'Phone Number', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',width: "*", minWidth: 80, maxWidth: 9000 },
                { field: 'email_address', displayName: 'Email', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 200, maxWidth: 9000 },
                {
                    field: 'cnst_dsp_id', headerTooltip: 'LN ID', displayName: "LN Id?", width: "*", minWidth: 40, maxWidth: 60,
                    cellTemplate: '<div class="wordwrap"  ng-if="COL_FIELD.length> \'0\' ">Yes</div><div class="wordwrap"  ng-if="COL_FIELD.length<= \'0\' || COL_FIELD==null ">No</div> '
                },
               /*{
                    field: 'request_transaction_key', displayName: 'Notes', width: "*", minWidth: 250, maxWidth: 9000,visible:false,
                    cellTemplate: '<div class="wordwrap" ng-if="COL_FIELD != \'-1\' ">Transaction #{{COL_FIELD}}. Waiting for approval.</div>'
                },*/

                { field: 'constituent_type', displayName: "Type", visible: false }


            ]
        },


        setSearchObj: function (postJSON) {
            constSharedPostParams = postJSON;
            // console.log(constSharedPostParams);
        }, getSearchObj: function () {
            try {
                return constSharedPostParams;
            }
            catch (err) {
                return null;
            }

        },

        setNameDetailsData: function (constNameData) {
            constNameDetailsData = constNameData;
        },
        pushNameDetailsData: function (pushConstNameData) {
            constNameDetailsData.push(pushConstNameData);
        },

        getNameDetailsData: function () {
            try {
                return constNameDetailsData;
            }
            catch (err) {
                return null;
            }

        },
        setBestSmryData: function (constSmryData) {
            constBestSmryDetailsData = constSmryData;
        },
        getBestSmryData: function () {
            try {
                return constBestSmryDetailsData;
            }
            catch (err) {
                return null;
            }


        },

        setNormalPageLayout: function ($scope, $localStorage) {
            $scope.constituent_headerName = $localStorage.name;
            $scope.constituent_masterId = $localStorage.masterId;

            $scope.toggleConstDetails = { "display": "block" };
            $scope.toggleHeader = !$scope.toggleHeader;
            $scope.pleaseWait = { "display": "none" };
            // $scope.toggleAdvancedSearchHeader = true;

        }
    }
}]);



var showGrids = {
    nameShowGrid: null,
    orgNameShowGrid: null,
    addressShowGrid: null,
    phoneShowGrid: null,
    emailShowGrid: null,
    extBridgeShowGrid: null,
    birthShowGrid: null,
    deathShowGrid: null,
    prefShowGrid: null,
    characteristicsShowGrid: null,
    internalBridgeShowGrid: null,
    emailDomainsGrid: null,
    naicsCodesGrid: null,
    contactsDetailsGrid: null
}
topAccMod.factory('showDetailGridService', ['uiGridConstants', '$rootScope', function (uiGridConstants, $rootScope) {
    var gridColumns = new GridColumns();
    var columnDefs = gridColumns.getGridColumns(uiGridConstants, $rootScope);

    return {
        getShowGridOptions: function (type) {

            switch (type) {
                case HOME_CONSTANTS.SHOW_CONST_NAME: {
                    showGrids.nameShowGrid = typeof null ? new Grid(columnDefs.showNameColdef) : showGrids.nameShowGrid;
                    return showGrids.nameShowGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_ORG_NAME: {
                    showGrids.orgNameShowGrid = typeof null ? new Grid(columnDefs.showOrgNameColdef) : showGrids.orgNameShowGrid;
                    return showGrids.orgNameShowGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_ADDRESS: {
                    showGrids.addressShowGrid = typeof null ? new Grid(columnDefs.showAddressColdef) : showGrids.addressShowGrid;
                    return showGrids.addressShowGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_PHONE: {
                    showGrids.phoneShowGrid = typeof null ? new Grid(columnDefs.showPhoneColdef) : showGrids.phoneShowGrid;
                    return showGrids.phoneShowGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_EMAIL: {
                    showGrids.emailShowGrid = typeof null ? new Grid(columnDefs.showEmailColdef) : showGrids.emailShowGrid;
                    return showGrids.emailShowGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_EXT_BRIDGE: {
                    showGrids.extBridgeShowGrid = typeof null ? new Grid(columnDefs.showExtBridgeColdef) : showGrids.extBridgeShowGrid;
                    return showGrids.extBridgeShowGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_BIRTH: {
                    showGrids.birthShowGrid = typeof null ? new Grid(columnDefs.showBirthColdef) : showGrids.birthShowGrid;
                    return showGrids.birthShowGrid.getGridOption(1, null, false, false, 8);

                    break;
                };
                case HOME_CONSTANTS.SHOW_CONST_DEATH: {
                    showGrids.deathShowGrid = typeof null ? new Grid(columnDefs.showDeathColdef) : showGrids.deathShowGrid;
                    return showGrids.deathShowGrid.getGridOption(1, null, false, false, 8); break;

                };
                case HOME_CONSTANTS.SHOW_CONT_PREF: {
                    showGrids.prefShowGrid = typeof null ? new Grid(columnDefs.showPrefColdef) : showGrids.prefShowGrid;
                    return showGrids.prefShowGrid.getGridOption(1, null, false, false, 8); break;
                    break;
                };
                case HOME_CONSTANTS.SHOW_CHARACTERISTICS: {
                    showGrids.characteristicsShowGrid = typeof null ? new Grid(columnDefs.showCharacteristicsColdef) : showGrids.characteristicsShowGrid;
                    return showGrids.characteristicsShowGrid.getGridOption(1, null, false, false, 8); break;
                    break;
                };
                case HOME_CONSTANTS.SHOW_INTERNAL_BRIDGE: {
                    showGrids.internalBridgeShowGrid = typeof null ? new Grid(columnDefs.showInternalBridgeColdef) : showGrids.internalBridgeShowGrid;
                    return showGrids.internalBridgeShowGrid.getGridOption(1, null, false, false, 8); break;
                    break;
                };
                case CONSTANTS.EMAIL_DOMAINS: {
                    showGrids.emailDomainsGrid = typeof null ? new Grid(columnDefs.showEmailDomainColdef) : showGrids.emailDomainsGrid;
                    return showGrids.emailDomainsGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case CONSTANTS.NAICS_CODES: {
                    showGrids.naicsCodesGrid = typeof null ? new Grid(columnDefs.showNAICSCodesColdef) : showGrids.naicsCodesGrid;
                    return showGrids.naicsCodesGrid.getGridOption(1, null, false, false, 8);
                    break;
                };
                case CONSTANTS.CONTACTS_DETAILS: {
                    showGrids.contactsDetailsGrid = typeof null ? new Grid(columnDefs.showContactsDetailsColdef) : showGrids.contactsDetailsGrid;
                    return showGrids.contactsDetailsGrid.getGridOption(1, null, false, false, 8);
                    break;
                };

            }
        }
    }

}]);

//used in constituent helper start date columns
topAccMod.filter('column_date_filter', ['$filter', function ($filter) {
    return function (value) {
        if (value != null) {
            var localDate = new Date(value);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;
            return $filter('date')(new Date(localTime + localOffset), 'MM/dd/yyyy');
        }
        else
            return null;
    }
}]);


//used in constituent helper timestamp columns
topAccMod.filter('column_date_timestamp_filter', ['$filter', function ($filter) {
    return function (value) {
        if (value != null){
            var localDate = new Date(value);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;
            return $filter('date')(new Date(localTime + localOffset), 'MM/dd/yyyy h:MM:ss a');
        }
    else
            return null;

    }
}]);


topAccMod.factory('showDetailsPostApiService', ['$http', function ($http) {
    var myURL = {};

    myURL[HOME_CONSTANTS.SHOW_CONST_BIRTH] = BasePath + "Test/showbirthdetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_DEATH] = BasePath + "Test/showdeathdetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_NAME] = BasePath + "Test/showpersonnamedetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_ORG_NAME] = BasePath + "Test/showorgnamedetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_ADDRESS] = BasePath + "Test/showaddressdetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_PHONE] = BasePath + "Test/showphonedetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_EMAIL] = BasePath + "Test/showemaildetails";
    // myURL[HOME_CONSTANTS.SHOW_CONST_DEATH] = BasePath + "Test/showdeathdetails";
    myURL[HOME_CONSTANTS.SHOW_CONT_PREF] = BasePath + "Test/showcontactpreferencedetails";
    myURL[HOME_CONSTANTS.SHOW_CHARACTERISTICS] = BasePath + "Test/showcharacteristicsdetails";
    myURL[HOME_CONSTANTS.SHOW_CONST_EXT_BRIDGE] = BasePath + "Test/showexternalbridgedetails";
    myURL[HOME_CONSTANTS.SHOW_INTERNAL_BRIDGE] = BasePath + "Test/showinternalbridgedetails";


    var postApiService = function (postParams, requestType) {
        return $http.post(myURL[requestType], postParams, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }

        }).success(function (result, status, headers, config) {
            //   console.log(result);
            return result;

        }).error(function (result, status, headers, config) { // <--- catch instead error

            //   console.log(data.statusText); //contains the error message
            return result;
        });
    }

    return {
        postApiService: function (postParams, type) {
            return postApiService(postParams, type);
        }
    }

}]);

topAccMod.factory('exportService', ['$http', '$window', function ($http, $window) {

    return {
        getSearchExportData: function (postParams, $rootScope) {
            return $http.post(BasePath + "Test/exportAdvSearchExcel/", postParams, {
                headers: {
                    "Content-type": 'application/json'

                },
                "responseType": "arraybuffer",
            }).success(function (data) {
                if (data.byteLength > 0) {
                    var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    //  var blob = new Blob([data], { type: type });
                    saveAs(blob, "SearchResults.xlsx");
                }
                else {
                    angular.element("#iErrorModal").modal('hide');
                    angular.element("#accessDeniedModal").modal();
                }
            }).error(function (result) {

                var decodedString = String.fromCharCode.apply(null, new Uint8Array(result));
                var obj = JSON.parse(decodedString);

                if (obj == CRUD_CONSTANTS.ACCESS_DENIED) {
                    messagePopup($rootScope, "Logged in user does not have appropriate permission.", "Error: Access Denied");
                }
                else if (obj == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messagePopup($rootScope, "The service/database timed out. Please try again after some time.", "Error: Timed Out");
                }
                else if (obj == CRUD_CONSTANTS.DB_ERROR) {
                    messagePopup($rootScope, "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).", "Error: Timed Out");

                }
                return obj;
            });


        },

        getConstDetailsExportData: function (id,$rootScope) {
            return $http.get(BasePath + "Test/constituentDetailsExport/" + id, {
                headers: {
                    "Content-type": 'application/json'

                },
                "responseType": "arraybuffer",
            }).success(function (data) {
                if (data.byteLength > 0) {
                    var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    //  var blob = new Blob([data], { type: type });
                    saveAs(blob, "ConstituentDetails.xlsx");
                }
                else {
                    angular.element("#iErrorModal").modal('hide');
                    angular.element("#accessDeniedModal").modal();
                }
            }).error(function (result) {

                var decodedString = String.fromCharCode.apply(null, new Uint8Array(result));
                var obj = JSON.parse(decodedString);
                //return obj;

                if (obj == CRUD_CONSTANTS.ACCESS_DENIED) {
                    messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
                }
                else if (obj == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (obj == CRUD_CONSTANTS.DB_ERROR) {
                    messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
                return obj;
            });


        }

    }
}]);

topAccMod.factory('dropDownService', ['$http', function ($http) {

    var URL = {};

    URL[HOME_CONSTANTS.CONST_ADDRESS] = "Home/getAddressType";
    URL[HOME_CONSTANTS.CONST_EMAIL] = "Home/getEmailType";
    URL[HOME_CONSTANTS.CONST_PHONE] = "Home/getPhoneType";
    URL[HOME_CONSTANTS.CONST_NAME] = "Home/getPersonNameType";
    URL[HOME_CONSTANTS.CONST_ORG_NAME] = "Home/getOrganizationNameType";
    URL[HOME_CONSTANTS.CHARACTERISTICS] = "Home/getCharacteristicsType";
    URL[HOME_CONSTANTS.CONST_PREF] = "Home/getContactPreferenceType";
    URL[HOME_CONSTANTS.CONST_PREF_VAL] = "Home/getContactPreferenceValue";
    URL[HOME_CONSTANTS.GRP_MEMBERSHIP] = "Home/getChapterGroupKeyName";
    URL[HOME_CONSTANTS.CHAPTER_SYSTEM] = "Home/getChapterSourceSystemType";
    URL[HOME_CONSTANTS.SOURCE_SYSTEM_TYPE] = "Home/getSourceSystemType";


    return {
        getDropDown: function (type) {
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
        }
    }

}]);


topAccMod.factory('Stack', [function () {
    var Stack = function () {

        this.StackArray = new Array();
        this.top = -1;
    }

    Stack.prototype.push = function (index) {
        var _self = this;
        if (_self.top == -1) {
            _self.top++;
            _self.StackArray[_self.top] = index;
        }
        else {
            _self.StackArray[++_self.top] = index;
        }
    }

    Stack.prototype.pop = function () {
        var _self = this;
        if (_self.top == -1) {
            return null;
        }
        else {
            return _self.StackArray[_self.top--];
        }
    }


    Stack.prototype.getContents = function () {
        var _self = this;
        if (_self.top == -1) {
            return null;
        }
        else {
            for (var i = 0; i < _self.StackArray.length; i++) {
                console.log(_self.StackArray[i]);
            }
        }
    }


    Stack.prototype.isEmpty = function () {
        var _self = this;
        if (_self.top == -1) {
            return true;
        }
        else {
            return false;
        }
    }

    Stack.prototype.forceEmpty = function () {
        var _self = this;
        while (_self.top != -1) {
            _self.pop();
        }
    }

    return Stack;
}]);


topAccMod.factory('MessageService', ['$q', 'ModalService', function ($q, ModalService) {
    return {
        getPopup: function (params) {
            var deferred = $q.defer();
            var templUrl = BasePath + 'App/Constituent/Views/common/MessagePopup.tpl.html';
            var controller = 'MessagePopupCtrl';
            var modalsize = 'sm';

            ModalService.OpenModal(templUrl, controller, modalsize, params).then(function (result) {

                deferred.resolve(result);
            });
            return deferred.promise;
        }

    }

}]);



topAccMod.factory('ModalService', ['$rootScope', '$uibModal', '$q', function ($rootScope, $uibModal, $q) {
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