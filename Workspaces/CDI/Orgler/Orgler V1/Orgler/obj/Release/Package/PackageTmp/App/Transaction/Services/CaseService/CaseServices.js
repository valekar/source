angular.module('transaction').factory('CaseServices', ['$http', '$rootScope', function ($http, $rootScope) {
    var sharedPostParams = {};
    var sharedMasterId = "";
    var BasePath = $("base").first().attr("href");

    return {
        CreateCase: function (postCaseSearchParams) {
            return $http.post(BasePath + "CaseNative/CreateCase", postCaseSearchParams, {
                //  return $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
               // console.log(result);
                return result.data;
            }).error(function (result) {
                if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        },
        getCaseAdvSearchResults: function (postCaseSearchParams) {
          //  console.log("Before sending to controller");
          //  console.log(postCaseSearchParams);
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
             //   console.log(result);
                return result.data;
            }).error(function (result) {
                if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        },

        setAssociateCaseData: function(postSearchParams){
          //  console.log("Before sending to controller for case association");
         //   console.log(postSearchParams);
            return $http.post(BasePath + "TransactionNative/AssociateCase", postSearchParams, {
                //  return $http.post(BasePath + "Home/WriteCaseRecentSearches", postCaseSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                $http.post(BasePath + "Home/WriteCaseRecentSearches", postSearchParams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                });
              //  console.log("From service ");
               // console.log(result.data);
                return result.data;
            }).error(function (result) {
                if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                    messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
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
                    if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                });
        }
    }
}])




angular.module('transaction').factory('CaseClearDataService', ['CaseMultiDataService', 'CaseDataServices', function (CaseMultiDataService, CaseDataServices) {  //Temporarily commented CaseMultiDataService

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



angular.module('transaction').factory('CaseMultiDataService', ['CONSTANTS', function (CONSTANTS) {
    // var bestSmryData = [];

    /*var CONSTANTS = {
        BEST_SMRY: 'BestSmry',
        CONST_NAME: 'ConstName',
        CONST_ADDRESS:'ConstAddress'
    };*/

    var allConstDatas = {
        caseInfoData: [],
        caseDetailsData: [],
        caseLocInfo: [],
        caseNotes: [],
        caseTransaction: []
    };

    return {

        getData: function (type) {
            switch (type) {
                case CONSTANTS.CASE_INFO: { return allConstDatas.caseInfoData; break; };
                case CONSTANTS.CASE_DETAILS: { return allConstDatas.caseDetailsData; break; };
                case CONSTANTS.CASE_LOCINFO: { return allConstDatas.caseLocInfo; break; };
                case CONSTANTS.CASE_NOTES: { return allConstDatas.caseNotes; break; };
                case CONSTANTS.CASE_TRANSACTION: { return allConstDatas.caseTransaction; break; };
            }
        },

        setData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.CASE_INFO: { allConstDatas.caseInfoData = resultData; break; };
                case CONSTANTS.CASE_DETAILS: { allConstDatas.caseDetailsData = resultData; break; };
                case CONSTANTS.CASE_LOCINFO: { allConstDatas.caseLocInfo = resultData; break; };
                case CONSTANTS.CASE_NOTES: { allConstDatas.caseNotes = resultData; break; };
                case CONSTANTS.CASE_TRANSACTION: { allConstDatas.caseTransaction = resultData; break; };
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

                case CONSTANTS.CASE_INFO: { allConstDatas.caseInfoData.push(aData); break; };
            }
        }
    }
}]);



angular.module('transaction').factory('CaseDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {
    var searchTransResultsData = [];
    var constNameDetailsData = [];
    var constBestSmryDetailsData = [];
    var cartData = [];
    var ConstNameGrid;
    var searchTransParams;
    var caseDetails;
    var caseAssociatedTransKey;
    var caseAssociatedCaseKey;

    return {

        setAssociatedData : function (associateParams) {
            caseAssociatedTransKey = associateParams.trans_key;
            caseAssociatedCaseKey = associateParams.case_key;
        },

        getAssociatedTransKey : function() {
            return caseAssociatedTransKey;
        },

        getAssociatedCaseKey: function () {
            return caseAssociatedCaseKey;
        },

        setTransSearchResultsData: function (results) {
            searchTransResultsData = results;
        },
        getTransSearchResultsData: function () {
            return searchTransResultsData;
        },
        clearTransSearchData: function () {
            searchTransResultsData = [];
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

        getTransSearchResultsColumnDef: function () {
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
                field: 'case_key', displayName: 'Case Number', width: "*"
            },
            {
                field: 'case_nm', displayName: 'Case Name', enableCellEdit: false,
                cellTemplate: linkCellTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                    if (grid.columns[0]) {
                        return 'first-col-style';
                    }
                }, width: "*"
            },
            { field: 'case_desc', headerTooltip: 'Case description', displayName: 'Case description', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*" },
            { field: 'ref_src_dsc', displayName: 'CRM System', width: "*" },
            { field: 'ref_id', displayName: 'CRM System ID', width: "*" },
            { field: 'typ_key_dsc', displayName: 'Type', visible: false },
            { field: 'cnst_nm', displayName: 'Constituent Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', visible: false },
            { field: 'crtd_by_usr_id', displayName: 'User Name', visible: false },
            { field: 'report_dt', displayName: 'Reported Date', visible: false },
            { field: 'status', displayName: 'Status', visible: false }
            ]
        },
        getSearchParams: function ($scope) {
            var CaseDateFrom = null;
            var CaseDateTo = null;
            if ($scope.CaseDateFrom) {
                CaseDateFrom = $filter('date')(new Date($scope.CaseDateFrom), 'dd/MM/yyyy');
            }
            if ($scope.CaseDateTo) {
                CaseDateTo = $filter('date')(new Date($scope.CaseDateTo), 'dd/MM/yyyy');
            }




            var searchParams1 = {
                "CaseId": $scope.CaseNumber,
                "CaseName": $scope.CaseName,
                "ReferenceSource": $scope.Case_CRMSystem,
                "ReferenceId": $scope.case_CRMSysId,
                "CaseType": $scope.case_Type,
                "CaseStatus": $scope.caseStatus,
                "ConstituentName": $scope.case_constName,
                "UserName": $scope.case_userName,
                "ReportedDateFrom": CaseDateFrom,
                "ReportedDateTo": CaseDateTo,
                "UserId": $scope.case_userName
            };

            var searchParams2 = {
                "CaseId": $scope.CaseNumber_2,
                "CaseName": $scope.CaseName_2,
                "ReferenceSource": $scope.Case_CRMSystem_2,
                "ReferenceId": $scope.case_CRMSysId_2,
                "CaseType": $scope.case_Type_2,
                "CaseStatus": $scope.caseStatus_2,
                "ConstituentName": $scope.case_constName_2,
                "UserName": $scope.case_userName_2,
                "ReportedDateFrom": $scope.CaseDateFrom_2,
                "ReportedDateTo": $scope.CaseDateTo_2,
                "UserId": $scope.case_userName_2
            }

            var searchParams3 = {
                "CaseId": $scope.CaseNumber_3,
                "CaseName": $scope.CaseName_3,
                "ReferenceSource": $scope.Case_CRMSystem_3,
                "ReferenceId": $scope.case_CRMSysId_3,
                "CaseType": $scope.case_Type_3,
                "CaseStatus": $scope.caseStatus_3,
                "ConstituentName": $scope.case_constName_3,
                "UserName": $scope.case_userName_3,
                "ReportedDateFrom": $scope.CaseDateFrom_3,
                "ReportedDateTo": $scope.CaseDateTo_3,
                "UserId": $scope.case_userName_3
            };

            var searchParam4 = {
                "CaseId": $scope.CaseNumber_4,
                "CaseName": $scope.CaseName_4,
                "ReferenceSource": $scope.Case_CRMSystem_4,
                "ReferenceId": $scope.case_CRMSysId_4,
                "CaseType": $scope.case_Type_4,
                "CaseStatus": $scope.caseStatus_4,
                "ConstituentName": $scope.case_constName_4,
                "UserName": $scope.case_userName_4,
                "ReportedDateFrom": $scope.CaseDateFrom_4,
                "ReportedDateTo": $scope.CaseDateTo_4,
                "UserId": $scope.case_userName_4
            };

            var searchParams5 = {
                "CaseId": $scope.CaseNumber_5,
                "CaseName": $scope.CaseName_5,
                "ReferenceSource": $scope.Case_CRMSystem_5,
                "ReferenceId": $scope.case_CRMSysId_5,
                "CaseType": $scope.case_Type_5,
                "CaseStatus": $scope.caseStatus_5,
                "ConstituentName": $scope.case_constName_5,
                "UserName": $scope.case_userName_5,
                "ReportedDateFrom": $scope.CaseDateFrom_5,
                "ReportedDateTo": $scope.CaseDateTo_5,
                "UserId": $scope.case_userName_5
            };
            var postParams = { "CaseInputSearchModel": [] };

            if ($scope.SearchPanel_1)
                postParams["CaseInputSearchModel"].push(searchParams1);
            if ($scope.SearchPanel_2)
                postParams["CaseInputSearchModel"].push(searchParams2);
            if ($scope.SearchPanel_3)
                postParams["CaseInputSearchModel"].push(searchParams3);
            if ($scope.SearchPanel_4)
                postParams["CaseInputSearchModel"].push(searchParams4);
            if ($scope.SearchPanel_5)
                postParams["CaseInputSearchModel"].push(searchParams5);



           // console.log("Search Params - ")
         //   console.log(postParams);
            return postParams;
        },
        getCreateCaseParams: function ($scope) {
            var CaseReportDate = null;
            if ($scope.CaseReportDate) {
                CaseReportDate = $filter('date')(new Date($scope.CaseReportDate), 'MM/dd/yyyy');
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
                        "crtd_by_usr_id": $scope.case_userName,
                        "status": null,
                        "report_dt": CaseReportDate,
                        "attchmnt_url": null
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
        setTransSearchObj: function (postJSON) {
            sharedPostParams = postJSON;
            // console.log(sharedPostParams);
        }, getTransSearchObj: function () {
            try {
                return sharedPostParams;
            }
            catch (err) {
                return null;
            }

        },
        setTransSearchParams: function (searchParam) {
            searchTransParams = searchParam;
        },
        getTransSavedSearchParams: function () {
            return searchTransParams;
        },

        setNormalPageLayout: function ($scope, $localStorage) {
            $scope.case_nm = $localStorage.case_nm;
            $scope.case_key = $localStorage.case_key;

            $scope.toggleConstDetails = { "display": "block" };
            $scope.toggleHeader = !$scope.toggleHeader;
            $scope.pleaseWait = { "display": "none" };
            // $scope.toggleAdvancedSearchHeader = true;

        },
        getTransSearchGridOptions: function (uiGridConstants, columnDefs) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                multiSelect:false,

                paginationPageSize: 8,
                enablePagination: true,
                paginationPageSizes: [8],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs

            };
            gridOptions.data = '';
            return gridOptions;
        },
        clearTransSearchData: function () {
            searchTransResultsData = [];
        }
    }
}]);


angular.module('transaction').factory('CaseClearDataService', ['CaseMultiDataService', 'CaseDataServices', function (CaseMultiDataService, CaseDataServices) {

    return {
        clearMultiData: function () {
            CaseMultiDataService.clearData();
            return;
        },
        clearTransSearchData: function () {
            CaseDataServices.clearTransSearchData();
        }
    }

}]);


angular.module('transaction').constant('CONSTANTS', {
    CASE_INFO: 'CaseInfo',
    CASE_DETAILS: 'CaseDetails',
    CASE_LOCINFO: 'CaseLocInfo',
    CASE_NOTES: 'CaseNotes',
    CASE_TRANSACTION: 'CaseTransaction'
});





