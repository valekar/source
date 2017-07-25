UPLOAD_CONSTANTS = {
    UPLOAD_CHAPTERCODE: 'chapterCode',
    UPLOAD_GROUPCODE: 'groupCode'
};

UploadModule.factory('UploadServices', ['$http', '$rootScope', function ($http, $rootScope) {

    var BasePath = $("base").first().attr("href");

    return {

        postGroupMembershipUploadData: function (uploadedFormData) {
            return $http.post(BasePath + "UploadNative/UploadGroupMembershipData1", JSON.stringify(uploadedFormData), {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8'                     //"Content-Type": "application/json",  //"Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
                return result.data;
            }).error(function (result) {
            });
        }

       
    }

}]);

UploadModule.factory('UploadDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {

    var uploadValidationResultData = null;

    var uploadResultData = null;

    var groupCodeRowDef = null;

    var groupCodeColDef = null;

    var invalidRecordsCount = null;

    var uploadFormData = null;

    var uploadParams = null;

    var uploadedFileMessage = null;

    var loggedInUserName = null;

    return {

        setUploadValidationResult: function (result) {
            uploadValidationResultData = result;
        },

        getUploadValidationResult: function () {
            return uploadValidationResultData;
        },

        setUploadResult: function (result) {
            uploadResultData = result;
        },

        getUploadResult: function () {
            return uploadResultData;
        },

        setInvalidRecordsCount: function (count) {
            invalidRecordsCount = count;
        },

        getInvalidRecordsCount: function () {
            return invalidRecordsCount;
        },

        setUploadFormData: function (result) {
            uploadFormData = result;
        },

        getUploadFormData: function () {
            return uploadFormData;
        },

        setGroupMembershipUploadParams: function (params) {
            uploadParams = params;
        },

        setUploadedFileMessage: function (message) {
            uploadedFileMessage = message;
        },

        getUploadedFileMessage: function () {
            return uploadedFileMessage;
        },

        setUserName:function (strUserName) {
            loggedInUserName = strUserName;
        },

        getUserName: function () {
            return loggedInUserName;
        },

        getGroupMembershipUploadParams: function(uploadParams){
            return $http.post(BasePath + "uploadNative/setGroupMembershipUploadParams", uploadParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
            })
        },

        getUploadValidationResultsColumnDef: function () {
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
                    field: 'chpt_cd', displayName: 'Chapter Code', width: "*", minWidth: 100, maxWidth: 900,
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                        var cellValue = grid.getCellValue(row, col);

                        if (uploadValidationResultData._invalidChapterCodes.filter(function (e) { return e == cellValue; }).length > 0) {
                            return 'redClass';
                        }
                    }
                },
                {
                    field: 'cnst_mstr_id', displayName: 'Master ID', enableCellEdit: false,
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                        var cellValue = grid.getCellValue(row, col);

                        if (uploadValidationResultData._invalidMasterIds.filter(function (e) { return e == cellValue; }).length > 0) {
                            return 'redClass';
                        }
                    },
                    width: "*", minWidth: 100, maxWidth: 900
                },
                {
                    field: 'cnst_prefix_nm', headerTooltip: 'Prefix Name', displayName: 'Prefix Name',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_first_nm', displayName: 'First Name', headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 140, maxWidth: 900
                },
                {
                    field: 'cnst_middle_nm', displayName: 'Middle Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_last_nm', displayName: 'Last Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'grp_cd', displayName: 'Group Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900,
                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                        var cellValue = grid.getCellValue(row, col);

                        if (uploadValidationResultData._invalidGroupCodes.filter(function (e) { return e == cellValue; }).length > 0) {
                            return 'redClass';
                        }
                    }
                },
                {
                    field: 'grp_nm', displayName: 'Group Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900

                },
                {
                    field: 'cnst_addr1_street1', displayName: 'Address Line1', 
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 140, maxWidth: 900
                },
                {
                    field: 'cnst_addr1_street2', displayName: 'Address Line2',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 140, maxWidth: 900
                },
                {
                    field: 'cnst_addr1_city', displayName: 'City', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 100, maxWidth: 900
                },
                {
                    field: 'cnst_addr1_state', displayName: 'State', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 100, maxWidth: 900
                },

                {
                    field: 'cnst_addr1_zip', displayName: 'Zip', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 90, maxWidth: 900
                },
                {
                    field: 'cnst_addr2_street1', displayName: 'Address(Work) Line1',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 140, maxWidth: 900
                },
                {
                    field: 'cnst_addr2_street2', displayName: 'Address(Work) Line2',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 140, maxWidth: 900
                },
                {
                    field: 'cnst_addr2_city', displayName: 'City(Work)', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_addr2_state', displayName: 'State(Work)', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_addr2_zip', displayName: 'Zip(Work)', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_phn1_num', displayName: 'Phone', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 100, maxWidth: 900
                },

                {
                    field: 'cnst_phn2_num', displayName: 'Phone(Work)', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_phn3_num', displayName: 'Phone(Cell)', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'cnst_email1_addr', displayName: 'Email Address',
                    width: "*", minWidth: 140, maxWidth: 900,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_email2_addr', displayName: 'Email Address(Work)',
                    width: "*", minWidth: 140, maxWidth: 900, headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'job_title', displayName: 'Job Title', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 140, maxWidth: 900
                },

                {
                    field: 'company_nm', displayName: 'Company Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 140, maxWidth: 900
                },

                {
                    field: 'rm_ind', displayName: 'Remove Indicator', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 145, maxWidth: 900
                },
                {
                    field: 'notes', displayName: 'Notes', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 90, maxWidth: 900
                },


                {
                    field: 'created_by', displayName: 'Created By', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'created_dt', displayName: 'Created Date', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 120, maxWidth: 900
                }

            ]
        },

        getUploadValidationGridOptions: function (uiGridConstants, columnDefs) {

            var gridOptions = {
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableFiltering: false,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 45,
                multiSelect: false,

                paginationPageSize: 8,
                enablePagination: true,
                paginationPageSizes: [8],
                enablePaginationControls: false,
                enableVerticalScrollbar: 1,
                enableHorizontalScrollbar: 1,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs

            };
            gridOptions.data = '';
            return gridOptions;
        }
    }


}]);

UploadModule.factory('uploadDropDownService', ['$http', function ($http) {

    var URL = {};

    URL[UPLOAD_CONSTANTS.UPLOAD_CHAPTERCODE] = "Home/getChapterCode";
    URL[UPLOAD_CONSTANTS.UPLOAD_GROUPCODE] = "Home/getChapterGroupCodeName";

    return {
        getDropDown: function (type) {
            return $http.get(URL[type], {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                return result;
            }).error(function (result) {
                return result;
            });
        }
    }

}]);

var UPLOAD_CRUD_CONSTANTS = {
    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',

    SUCCESS_MESSAGE: 'The case was successfully created!',
    FAILURE_MESSAGE: 'The case was not created.',
    FAIULRE_REASON: 'It looks like there is a similar record in the database. Please review.',

    SUCCESS_CONFIRM: 'Success!',
    FAILURE_CONFIRM: 'Failed!',

    SERVICE_TIMEOUT: 'Timed out',
    SERVICE_TIMEOUT_CONFIRM: 'Error: Timed Out',
    SERVICE_TIMEOUT_MESSAGE: 'The service/database timed out. Please try again after some time.',

    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM: "Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",


    PROCEDURE: {
        SUCCESS: 'Success',
        DUPLICATE: 'duplicate',
        NOT_PRESENT: 'The original record is not present.'
    },

};