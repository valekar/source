EMAIL_ONLY_UPLOAD_CONSTANTS = {
    EMAIL_ONLY_UPLOAD_CHAPTERCODE: 'chapterCode',
    EMAIL_ONLY_UPLOAD_GROUPCODE: 'groupCode'
};

angular.module('upload').factory('EmailOnlyUploadServices', ['$http', '$rootScope', function ($http, $rootScope) {

    var BasePath = $("base").first().attr("href");

    return {

        postEmailOnlyUploadData: function (uploadedEmailFormData) {
            return $http.post(BasePath + "EmailOnlyNative/postEmailOnlyUploadData", JSON.stringify(uploadedEmailFormData), {
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

angular.module('upload').factory('EmailOnlyUploadDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {

    var emailUploadValidationResultData = null;

    var emailUploadResultData = null;

    var groupCodeRowDef = null;

    var groupCodeColDef = null;

    var invalidRecordsCount = null;

    var emailUploadFormData = null;

    var emailUploadParams = null;

    var uploadedFileMessage = null;

    var loggedInUserName = null;

    return {

        setEmailUploadValidationResult: function (result) {
            uploadValidationResultData = result;
        },

        getEmailUploadValidationResult: function () {
            return uploadValidationResultData;
        },

        setEmailUploadResult: function (result) {
            uploadResultData = result;
        },

        getEmailUploadResult: function () {
            return uploadResultData;
        },

        setInvalidRecordsCount: function (count) {
            invalidRecordsCount = count;
        },

        getInvalidRecordsCount: function () {
            return invalidRecordsCount;
        },

        setEmailUploadFormData: function (result) {
            uploadFormData = result;
        },

        getEmailUploadFormData: function () {
            return uploadFormData;
        },

        setEmailUploadParams: function (params) {
            uploadParams = params;
        },

        setEmailUploadedFileMessage: function (message) {
            uploadedFileMessage = message;
        },

        getEmailUploadedFileMessage: function () {
            return uploadedFileMessage;
        },

        setUserName: function (strUserName) {
            loggedInUserName = strUserName;
        },

        getUserName: function () {
            return loggedInUserName;
        },

        getEmailUserUploadParams: function (uploadParams) {
            return $http.post(BasePath + "EmailOnlyNative/setEmailUserUploadParams", uploadParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
            })
        },

        getEmailUploadValidationResultsColumnDef: function () {
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
                    field: 'cnst_email_addr', displayName: 'Email Address',
                    width: "*", minWidth: 140, maxWidth: 900,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                     cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                        var cellValue = grid.getCellValue(row, col);

                        if (uploadValidationResultData._invalidEmailAddresses.filter(function (e) { return e == cellValue; }).length > 0) {
                            return 'redClass';
                        }
                    }
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
                }

            ]
        },

        getEmailUploadValidationGridOptions: function (uiGridConstants, columnDefs) {

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

angular.module('upload').factory('emailUploadDropDownService', ['$http', function ($http) {

    var URL = {};

    URL[EMAIL_ONLY_UPLOAD_CONSTANTS.EMAIL_ONLY_UPLOAD_CHAPTERCODE] = "Home/getChapterCode";
    URL[EMAIL_ONLY_UPLOAD_CONSTANTS.EMAIL_ONLY_UPLOAD_GROUPCODE] = "Home/getChapterGroupCodeName";

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

angular.module('upload').constant('EMAIL_UPLOAD_CONSTANTS', {
    'VALIDATION_POPUP': 'EmailUploadValidation',
    'DATA_POPUP': 'EmailUserUploadData',
    'CONFIRMATION_POPUP': 'EmailUploadConfirmation',
    'ERROR_POPUP': 'EmailUploadError'
});


angular.module('upload').factory('EmailUploadModalService',
    ['$uibModal', '$rootScope', '$state', '$uibModalStack', 'uiGridConstants', 'UploadDataServices', 'EMAIL_UPLOAD_CONSTANTS',
    function ($uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, UploadDataServices, EMAIL_UPLOAD_CONSTANTS) {
        return {
            getEmailPopup: function ($scope, modalSize, emailUploadModaltype) {
                if (EMAIL_UPLOAD_CONSTANTS.VALIDATION_POPUP == emailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/EmailOnlyUploadValidation.tpl.html';
                    var controller = 'EmailUploadValidationCtrl';
                }
                else if (EMAIL_UPLOAD_CONSTANTS.DATA_POPUP == emailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/EmailOnlyUploadUserInput.tpl.html';
                    var controller = 'UserEmailUploadDataCtrl';
                }
                else if (EMAIL_UPLOAD_CONSTANTS.CONFIRMATION_POPUP == emailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/EmailOnlyUploadConfirm.tpl.html';
                    var controller = 'UserEmailUploadConfirmCtrl';
                }
                else if (EMAIL_UPLOAD_CONSTANTS.ERROR_POPUP == emailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/GroupMembershipUploadError.tpl.html';
                    var controller = 'UserEmailUploadErrorCtrl';
                }

                OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, modalSize, $rootScope, $state, $uibModalStack, null, UploadDataServices);

            }
           
        }

    }]);





