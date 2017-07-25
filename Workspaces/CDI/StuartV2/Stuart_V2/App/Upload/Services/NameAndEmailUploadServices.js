NAME_AND_EMAIL_UPLOAD_CONSTANTS = {
    UPLOAD_CHAPTERCODE: 'chapterCode',
    UPLOAD_GROUPCODE: 'groupCode'
};

angular.module('upload').factory('NameAndEmailUploadServices', ['$http', '$rootScope', function ($http, $rootScope) {

    var BasePath = $("base").first().attr("href");   
    return {

        postNameAndEmailUploadData: function (uploadedNameAndEmailFormData) {
            return $http.post(BasePath + "NameAndEmailNative/postNameAndEmailUploadData", JSON.stringify(uploadedNameAndEmailFormData), {
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

angular.module('upload').factory('NameAndEmailUploadDataServices', ['$http', '$rootScope', '$filter', function ($http, $rootScope, $filter) {

    var nameAndEmailUploadValidationResultData = null;

    var nameAndEmailUploadResultData = null;

    var groupCodeRowDef = null;

    var groupCodeColDef = null;

    var invalidRecordsCount = null;

    var nameAndEmailUploadFormData = null;

    var nameAndEmailUploadParams = null;

    var uploadedFileMessage = null;

    var loggedInUserName = null;

    return {

        setNameAndEmailUploadValidationResult: function (result) {
            nameAndEmailUploadValidationResultData = result;
        },

        getNameAndEmailUploadValidationResult: function () {
            return nameAndEmailUploadValidationResultData;
        },

        setNameAndEmailUploadResult: function (result) {
            nameAndEmailUploadResultData = result;
        },

        getNameAndEmailUploadResult: function () {
            return nameAndEmailUploadResultData;
        },

        setInvalidRecordsCount: function (count) {
            invalidRecordsCount = count;
        },

        getInvalidRecordsCount: function () {
            return invalidRecordsCount;
        },

        setNameAndEmailUploadFormData: function (result) {
            nameAndEmailUploadFormData = result;
        },

        getNameAndEmailUploadFormData: function () {
            return nameAndEmailUploadFormData;
        },

        setNameAndEmailUploadParams: function (params) {
            nameAndEmailUploadParams = params;
        },

        setNameAndEmailUploadedFileMessage: function (message) {
            uploadedFileMessage = message;
        },

        getNameAndEmailUploadedFileMessage: function () {
            return uploadedFileMessage;
        },

        setUserName: function (strUserName) {
            loggedInUserName = strUserName;
        },

        getUserName: function () {
            return loggedInUserName;
        },

        getNameAndEmailUserUploadParams: function (nameAndEmailUploadParams) {
            return $http.post(BasePath + "NameAndEmailNative/setNameAndEmailUserUploadParams", nameAndEmailUploadParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
            })
        },

        getNameAndEmailUploadValidationResultsColumnDef: function () {
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

                        if (nameAndEmailUploadValidationResultData._invalidChapterCodes.filter(function (e) { return e == cellValue; }).length > 0) {
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

                        if (nameAndEmailUploadValidationResultData._invalidGroupCodes.filter(function (e) { return e == cellValue; }).length > 0) {
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

                        if (nameAndEmailUploadValidationResultData._invalidEmailAddresses.filter(function (e) { return e == cellValue; }).length > 0) {
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

        getNameAndEmailUploadValidationGridOptions: function (uiGridConstants, columnDefs) {

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

angular.module('upload').factory('NameAndEmailUploadDropDownService', ['$http', function ($http) {

    var URL = {};

    URL[NAME_AND_EMAIL_UPLOAD_CONSTANTS.UPLOAD_CHAPTERCODE] = "Home/getChapterCode";
    URL[NAME_AND_EMAIL_UPLOAD_CONSTANTS.UPLOAD_GROUPCODE] = "Home/getChapterGroupCodeName";

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

angular.module('upload').constant('NAME_AND_EMAIL_UPLOAD_CONSTANTS', {
    'VALIDATION_POPUP': 'NameEmailUploadValidation',
    'DATA_POPUP': 'NameEmailUserUploadData',
    'CONFIRMATION_POPUP': 'NameEmailUploadConfirmation',
    'ERROR_POPUP': 'NameEmailUploadError'
});


angular.module('upload').factory('NameEmailUploadModalService',
    ['$uibModal', '$rootScope', '$state', '$uibModalStack', 'uiGridConstants', 'UploadDataServices', 'NAME_AND_EMAIL_UPLOAD_CONSTANTS',
    function ($uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, UploadDataServices, NAME_AND_EMAIL_UPLOAD_CONSTANTS) {
        return {
            getModalPopup: function ($scope, modalSize, nameEmailUploadModaltype) {
                if (NAME_AND_EMAIL_UPLOAD_CONSTANTS.VALIDATION_POPUP == nameEmailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/NameAndEmailUploadValidation.tpl.html';
                    var controller = 'NameAndEmailUploadValidationCtrl';
                }
                else if (NAME_AND_EMAIL_UPLOAD_CONSTANTS.DATA_POPUP == nameEmailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/NameAndEmailUploadUserInput.tpl.html';
                    var controller = 'UserNameAndEmailUploadDataCtrl';
                }
                else if (NAME_AND_EMAIL_UPLOAD_CONSTANTS.CONFIRMATION_POPUP == nameEmailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/NameAndEmailUploadConfirm.tpl.html';
                    var controller = 'UserNameAndEmailUploadConfirmCtrl';
                }
                else if (NAME_AND_EMAIL_UPLOAD_CONSTANTS.ERROR_POPUP == nameEmailUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/GroupMembershipUploadError.tpl.html';
                    var controller = 'UserNameAndEmailUploadErrorCtrl';
                }

                OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, modalSize, $rootScope, $state, $uibModalStack, null, UploadDataServices);

            },
           /* clearFileInput: function () {
                var result = document.getElementsByClassName("file-input-label");         //angular.element("input[type='file']").val(null);
                angular.element(result).text('');
                angular.element("input[type='file']").val(null);
            },
            getUploadValidationGridLayout: function (gridOptions, uiGridConstants, results) {
                gridOptions.data = '';
                gridOptions.data.length = 0;
                gridOptions.data = results;

                return gridOptions;
            },
            messageUploadPopup: function (message, header) {
                $rootScope.uploadMessage = message;
                $rootScope.uploadHeader = header;
                angular.element("#iUploadErrorModal").modal();
            }*/
        }

    }]);




