enterpriseAccMod.factory('CaseServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var sharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");
    var _username;
    return {
        CreateCase: function (postCaseSearchParams) {
            return $http.post(BasePath + "CaseNative/CreateCase", postCaseSearchParams, {
                //  return $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                // console.log(result);
                return result;
            });
        },
        getCaseAdvSearchResults: function (postCaseSearchParams) {
            return $http.post(BasePath + "CaseNative/AdvSearch", postCaseSearchParams, {
                //  return $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
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
        getSearchResultsGridLayout: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = true;
            //  gridOptions.data.length = 0;
            // gridOptions.data = '';
            gridOptions.data = datas;
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            // console.log(datas.length);
            return gridOptions;
        },
        getCaseRecentSearches: function () {
            return $http.get(BasePath + "Home/GetCaseRecentSearches",
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


            for (i = 1; i <= maxSearchPanelCount; i++) {
                $scope.CaseParams.CaseNumber[appendString + i] = "";
                $scope.CaseParams.CaseName[appendString + i] = "";
                $scope.CaseParams.Case_CRMSystem[appendString + i] = "";
                $scope.CaseParams.case_CRMSysId[appendString + i] = "";
                $scope.CaseParams.case_Type[appendString + i] = "";
                $scope.CaseParams.case_constName[appendString + i] = "";
                $scope.CaseParams.case_userName[appendString + i] = "";
                $scope.CaseParams.SearchMeChkbx[appendString + i] = "";
                $scope.CaseParams.caseStatus[appendString + i] = "";
                $scope.CaseParams.CaseDateTo[appendString + i] = null;
                $scope.CaseParams.CaseDateFrom[appendString + i] = null;
                $scope.CaseParams.SearchMeChkbx[appendString + i] = false;
                $scope.CaseParams.caseUsernameState[appendString + i] = false;

            }
            $scope.SearchRows = defaultOpenPanelsCount;
            for (i = 1; i <= $scope.SearchRows; i++) {

                $scope.CaseParams.SearchPanel[appendString + i] = true;
                $scope.CaseParams.PanelSeparator[appendString + i] = true;
                $scope.CaseParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.CaseParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }
            for (i = $scope.SearchRows + 1; i <= maxSearchPanelCount ; i++) {
                $scope.CaseParams.SearchPanel[appendString + i] = false;
                $scope.CaseParams.PanelSeparator[appendString + i] = false;
                $scope.CaseParams.showCloseButton[appendString + i] = false;

            }
        },



        clearCreateParams: function ($scope) {

            $scope.CaseName = "";
            $scope.case_Type = "";
            $scope.case_IntakeDept = "";
            $scope.case_IntakeChannel = "";
            $scope.Case_CRMSystem = "";
            $scope.case_CRMSysId = "";
            $scope.CaseReportDate = "";
            $scope.case_Details = "";
            $scope.selConstType = "Individual";
            $scope.CaseCloseChkbx = false;
            //$scope.fileUpload = "";
            $scope.file = null;

            $scope.case_firstName = "";
            $scope.case_lastName = "";
            $scope.case_AddressLine = "";
            $scope.case_city = "";
            $scope.case_state = "";
            $scope.case_zip = "";
            $scope.case_phone = "";
            $scope.case_emailAddress = "";
            $scope.case_masterid = "";
            $scope.case_SrcSystem = "";
            $scope.constituent_chapterSys = "";
            $scope.constituent_srcSysId = "";
        },

    }
}])

enterpriseAccMod.factory('CaseClearDataService', ['CaseMultiDataService', 'CaseDataServices', function (CaseMultiDataService, CaseDataServices) {  //Temporarily commented CaseMultiDataService

    return {
        clearMultiData: function () {
            CaseMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            CaseDataServices.clearSearchData();
        }
    }

}]);
//changed by srini
var allCaseDatas = {
    caseInfoData: [],
    caseDetailsData: [],
    caseLocInfo: [],
    caseNotes: [],
    caseTransaction: []
};

enterpriseAccMod.factory('CaseMultiDataService', ['CONSTANTS', function (CONSTANTS) {
    // var bestSmryData = [];

    /*var CONSTANTS = {
        BEST_SMRY: 'BestSmry',
        CONST_NAME: 'ConstName',
        CONST_ADDRESS:'ConstAddress'
    };*/



    return {

        getData: function (type) {
            switch (type) {
                case CONSTANTS.CASE_INFO: { return allCaseDatas.caseInfoData; break; };
                case CONSTANTS.CASE_DETAILS: { return allCaseDatas.caseDetailsData; break; };
                case CONSTANTS.CASE_LOCINFO: { return allCaseDatas.caseLocInfo; break; };
                case CONSTANTS.CASE_NOTES: { return allCaseDatas.caseNotes; break; };
                case CONSTANTS.CASE_TRANSACTION: { return allCaseDatas.caseTransaction; break; };
            }
        },

        setData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.CASE_INFO: { allCaseDatas.caseInfoData = resultData; break; };
                case CONSTANTS.CASE_DETAILS: { allCaseDatas.caseDetailsData = resultData; break; };
                case CONSTANTS.CASE_LOCINFO: { allCaseDatas.caseLocInfo = resultData; break; };
                case CONSTANTS.CASE_NOTES: { allCaseDatas.caseNotes = resultData; break; };
                case CONSTANTS.CASE_TRANSACTION: { allCaseDatas.caseTransaction = resultData; break; };
            }
        },

        clearData: function () {
            allCaseDatas = {
                caseInfoData: [],
                caseDetailsData: [],
                caseLocInfo: [],
                caseNotes: [],
                caseTransaction: []
            };
        },

        pushAData: function (type, aData) {
            switch (type) {

                case CONSTANTS.CASE_INFO: { allCaseDatas.caseInfoData.push(aData); break; };
            }
        }
    }
}]);



enterpriseAccMod.factory('CaseDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {
    var searchResultsData = [];
    var constNameDetailsData = [];
    var constBestSmryDetailsData = [];
    var cartData = [];
    var ConstNameGrid;
    var searchParams;
    var caseDetails;
    var headerDisplayStatus;
    var caseTypeDropDowns;

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
        clearSearchData: function () {
            searchResultsData = [];
        },
        setCaseInfoGrid: function (GridOpt) {
            CaseInfoGrid = GridOpt;
        },
        getCaseInfoGrid: function () {
            return CaseInfoGrid;
        },

        setCaseDetailsGrid: function (GridOpt) {
            CaseDetailsGrid = GridOpt;
        },
        getCaseDetailsGrid: function () {
            return CaseDetailsGrid;
        },

        setCaseLocInfoGrid: function (GridOpt) {
            CaseLocInfoGrid = GridOpt;
        },
        getCaseLocInfoGrid: function () {
            return CaseLocInfoGrid;
        },
        setCaseNotesGrid: function (GridOpt) {
            CaseNotesGrid = GridOpt;
        },
        getCaseNotesGrid: function () {
            return CaseNotesGrid;
        },
        setCaseTransDetailsGrid: function (GridOpt) {
            CaseTransDetailsGrid = GridOpt;
        },
        getCaseTransDetailsGrid: function () {
            return CaseTransDetailsGrid;
        },
        setCaseDetails: function (result) {
            caseDetails = result;
        },
        getCaseDetails: function () {
            return caseDetails;
        },
        getCaseTypeDropDowns: function () {
            return caseTypeDropDowns;
        },
        setCaseTypeDropDowns: function (data) {
            caseTypeDropDowns = data;
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
            return [{
                field: 'case_key', displayName: 'Case Number'
            },
            {
                field: 'case_nm', displayName: 'Case Name', enableCellEdit: false,
                cellTemplate: linkCellTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                    // console.log("row values");
                    // console.log(row.entity);
                    if (grid.columns[0]) {
                        return 'first-col-style';
                    }
                },
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'case_desc', headerTooltip: 'Case description', displayName: 'Case description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ref_src_dsc', displayName: 'CRM System', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'ref_id', displayName: 'CRM System ID', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'typ_key_dsc', displayName: 'Type', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'cnst_nm', displayName: 'Constituent Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'crtd_by_usr_id', displayName: 'User Name', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
            {
                field: 'report_dt', displayName: 'Reported Date', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
                cellFilter: 'column_date_filter'
            },
            {
                field: 'status', displayName: 'Status', filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            },
             {
                 field: 'attchmnt_url', displayName: 'Attachment URL', cellTemplate: '<div ng-show=\"row.entity.attchmnt_url"><a href="{{COL_FIELD}}" target="_blank">Download</a></div>'
             },
            {
                field: 'Action', cellTemplate: 'App/Case/Views/common/gridDropDownCaseInfo.tpl.html', displayName: 'User Action', headerCellTemplate: '<div>User Action</div>',
                width: "*", minWidth: 100, maxWidth: 9000, headerCellTemplate: GRID_FILTER_TEMPLATE, filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },
            }
            ]
        },
        getSearchParams: function ($scope, searchRowCount, appendString) {
            var postParams = { "CaseInputSearchModel": [] };
            var searchParams;
            function returnNewParams() {
                searchParams = {
                    "CaseId": "",
                    "CaseName": "",
                    "ReferenceSource": "",
                    "ReferenceId": "",
                    "CaseType": "",
                    "CaseStatus": "",
                    "ConstituentName": "",
                    "UserName": "",
                    "ReportedDateFrom": "",
                    "ReportedDateTo": "",
                    "UserId": ""
                };
                return searchParams;
            };

            returnNewParams();

            for (i = 1; i <= searchRowCount; i++) {
                searchParams["CaseId"] = $scope.CaseParams.CaseNumber[appendString + i];
                searchParams["CaseName"] = $scope.CaseParams.CaseName[appendString + i];
                searchParams["ReferenceSource"] = $scope.CaseParams.Case_CRMSystem[appendString + i];
                searchParams["ReferenceId"] = $scope.CaseParams.case_CRMSysId[appendString + i];
                if ($scope.CaseParams.case_Type[appendString + i])
                    //changed by Srini , removed id from searchParams["CaseType"] = $scope.CaseParams.case_Type[appendString + i].id;
                    searchParams["CaseType"] = $scope.CaseParams.case_Type[appendString + i];
                else
                    searchParams["CaseType"] = undefined;
                searchParams["CaseStatus"] = $scope.CaseParams.caseStatus[appendString + i];
                searchParams["ConstituentName"] = $scope.CaseParams.case_constName[appendString + i];
                searchParams["UserName"] = $scope.CaseParams.case_userName[appendString + i];
                searchParams["UserId"] = $scope.CaseParams.case_userName[appendString + i];
                if ($scope.CaseParams.CaseDateFrom[appendString + i]) {
                    searchParams["ReportedDateFrom"] = $filter('date')(new Date($scope.CaseParams.CaseDateFrom[appendString + i]), 'MM/dd/yyyy');
                    if (!searchParams["ReportedDateFrom"])
                        searchParams["ReportedDateFrom"] = $filter('date')(new Date($scope.CaseParams.CaseDateFrom[appendString + i]), 'M/dd/yyyy');
                }
                if ($scope.CaseParams.CaseDateTo[appendString + i]) {
                    searchParams["ReportedDateTo"] = $filter('date')(new Date($scope.CaseParams.CaseDateTo[appendString + i]), 'MM/dd/yyyy');
                    if (!searchParams["ReportedDateTo"])
                        searchParams["ReportedDateTo"] = $filter('date')(new Date($scope.CaseParams.CaseDateTo[appendString + i]), 'M/dd/yyyy');
                }

                postParams["CaseInputSearchModel"].push(searchParams);
                returnNewParams();
            }





            //  console.log("Search Params - ")
            // console.log(postParams);
            return postParams;
        },
        getCreateCaseParams: function ($scope, caseFileURL) {
            var CaseReportDate = null;
            if ($scope.CaseReportDate) {
                CaseReportDate = $filter('date')(new Date($scope.CaseReportDate), 'MM/dd/yyyy');
            }
            var Status = "";
            if ($scope.CaseCloseChkbx) {
                Status = "Closed";
            }
            else {
                Status = "Open";
            }
            var postParams = {
                "CreateCaseInput":
                    {
                        "case_nm": $scope.CaseName,
                        "case_desc": $scope.case_Details,
                        "ref_src_desc": $scope.Case_CRMSystem,
                        "ref_id": $scope.case_CRMSysId,
                        "typ_key_desc": $scope.case_Type,
                        "intake_chan_desc": $scope.case_IntakeChannel,
                        "intake_owner_dept_desc": $scope.case_IntakeDept,
                        "cnst_nm": null,
                        "status": Status,
                        "report_dt": CaseReportDate,
                        "attchmnt_url": caseFileURL
                    },
                "SaveCaseSearchInput":
                    {
                        "first_name": $scope.case_firstName,
                        "last_name": $scope.case_lastName,
                        "address_line": $scope.case_AddressLine,
                        "city": $scope.case_city,
                        "state": $scope.case_state,
                        "zip": $scope.case_zip,
                        "phone_number": $scope.case_phone,
                        "email_address": $scope.case_emailAddress,
                        "master_id": $scope.case_masterid,
                        "source_system": $scope.case_SrcSystem,
                        "chapter_source_system": $scope.constituent_chapterSys,
                        "source_system_id": $scope.constituent_srcSysId,
                        "constituent_type": $scope.selConstType
                    }
            }
            //  console.log(postParams);
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
            $scope.case_nm = $localStorage.case_nm;
            $scope.case_key = $localStorage.case_key;

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
                exporterCsvFilename: 'case_data.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                exporterMenuPdf: false,
                multiselect: true

            };
            gridOptions.data = '';
            return gridOptions;
        },
        clearSearchData: function () {
            searchResultsData = [];
        }
    }
}]);


enterpriseAccMod.factory('CaseClearDataService', ['CaseMultiDataService', 'CaseDataServices', function (CaseMultiDataService, CaseDataServices) {

    return {
        clearMultiData: function () {
            CaseMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            CaseDataServices.clearSearchData();
        }
    }

}]);

//Timestamp filter 
enterpriseAccMod.filter('column_timestamp_filter', ['$filter', function ($filter) {
    return function (value) {
        if (value)
            return $filter('date')(new Date(value), 'MM/dd/yyyy   hh:mm:ss a');
        else
            value;
    }
}]);


//Date Filter
enterpriseAccMod.filter('column_date_filter', ['$filter', function ($filter) {
    return function (value) {
        if (value)
            return $filter('date')(new Date(value), 'MM/dd/yyyy');
        else
            value;
    }
}]);




enterpriseAccMod.constant('CONSTANTS', {
    CASE_INFO: 'CaseInfo',
    CASE_DETAILS: 'CaseDetails',
    CASE_LOCINFO: 'CaseLocInfo',
    CASE_NOTES: 'CaseNotes',
    CASE_TRANSACTION: 'CaseTransaction'
});




//enterpriseAccMod.factory('caseDropDownService', ['$http', function ($http) {

//    var URL = {};

//    URL[CASE_CONSTANTS.CASE_CRMSYSTEM] = "Home/getReferenceSources";
//    URL[CASE_CONSTANTS.CASE_CASETYPE] = "Home/c";
//    URL[CASE_CONSTANTS.CASE_INTAKECHANNEL] = "Home/getIntakeChannel";
//    URL[CASE_CONSTANTS.CASE_INTAKEOWNERDEPT] = "Home/getIntakeOwnerDepartment";
//    URL[CASE_CONSTANTS.CASE_CASETYPE] = "Home/getCaseTypes";
//    URL[CASE_CONSTANTS.CASE_SOURCESYSTEMS] = "Home/getSourceSystemType";
//    return {
//        getDropDown: function (type) {
//            return $http.get(URL[type], {
//                headers: {
//                    "Content-Type": "application/json",
//                    "Accept": "application/json"
//                }
//            }).success(function (result) {
//                // console.log(result);
//                return result;
//            }).error(function (result) {
//                return result;
//            });
//        }
//    }

//}]);



