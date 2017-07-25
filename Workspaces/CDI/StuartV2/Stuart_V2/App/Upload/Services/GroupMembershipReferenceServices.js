REFERENCE_DATA_CONSTANTS = {
    GROUP_TYPE: "GroupType",
    SUB_GROUP_TYPE: "Sub_Group_type",
    GROUP_ASSIGNMENT_METHOD: "Group Assignment Method",
    SERVICE_TIMEOUT: 'Timed out',
    SERVICE_TIMEOUT_CONFIRM: 'Error: Timed Out',
    SERVICE_TIMEOUT_MESSAGE: 'The service/database timed out. Please try again after some time.',

    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM: "Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",

    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',
};

angular.module('upload').factory('GroupMembershipReferenceServices', ['$http', '$rootScope', '$filter', 'uiGridConstants', function ($http, $rootScope, $filter, uiGridConstants) {

    var referenceDataRecord = [];

    var referencedataGroupKey = null;

    var referencedataTransKey = null;

    var referenceDataRow = null;

    return {

        setReferenceDataRecord:function(results) {
            referenceDataRecord = results;
        },

        getReferenceDataRecord: function(){
            return referenceDataRecord;
        },

        setGroupKey: function(param) {
            referencedataGroupKey = param;
        },

        getGroupKey: function () {
            return referencedataGroupKey;
        },

        setTransKey: function (param) {
            referencedataTransKey = param;
        },

        getTransKey: function () {
            return referencedataTransKey;
        },

        setReferenceDataRow: function (param) {
            referenceDataRow = param;
        },

        getReferenceDataRow: function () {
            return referenceDataRow;
        },

        setReferenceDataOutputMessage: function (message) {
            referenceDataOutputMessage = message;
        },

        getReferenceDataOutputMessage: function () {
            return referenceDataOutputMessage;
        },

        getGroupMembershipResultsGridLayout: function (gridOptions, uiGridConstants, datas) {
            gridOptions.multiSelect = false;

            gridOptions.data = datas;
            console.log("Grid Data ");
            console.log(datas);
            console.log(gridOptions.data);
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');
            return gridOptions;
        },

        getGroupMembershipReferenceData: function() {
            return $http.post(BasePath + "GroupMembershipReferenceNative/getGroupMembershipReferenceData",  {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
               // console.log(result);
            })
        },

        postAddNewGrpMbrshpRefRecord: function (addRefRecParams) {
            return $http.post(BasePath + "GroupMembershipReferenceNative/postAddNewGrpMbrshpRefRecordParams", addRefRecParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
            })
        },

        postDeleteGrpMbrshpRefRecord: function (deleteRefRecParam) {
            return $http.post(BasePath + "GroupMembershipReferenceNative/postDeleteGrpMbrshpRefRecordParam", deleteRefRecParam, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
            })
        },

        postEditGrpMbrshpRefRecord: function (editRefRecParam) {
            return $http.post(BasePath + "GroupMembershipReferenceNative/postEditGrpMbrshpRefRecordParam", editRefRecParam, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).success(function (result) {
                console.log(result);
            })
        },

        getReferenceColumnDef: function () {
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
                    field: 'grp_key', displayName: 'Group Key', enableCellEdit: false, headerTooltip: 'Group Key',
                    width: "*", minWidth: 60, maxWidth: 900,
                    headerCellTemplate: GRID_HEADER_TEMPLATE
                },
                {
                    field: 'grp_cd', displayName: 'Group Code', enableCellEdit: false, headerTooltip: 'Group Code',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 160, maxWidth: 900,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'grp_nm', headerTooltip: 'Group Name', displayName: 'Group Name',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 180, maxWidth: 900
                },
                {
                    field: 'grp_typ', displayName: 'Group Type', headerTooltip: 'Group Type', headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    width: "*", minWidth: 120, maxWidth: 900
                },
                {
                    field: 'sub_grp_typ', displayName: 'Sub Group Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,headerTooltip:'Sub Group Type',
                    width: "*", minWidth: 100, maxWidth: 900
                },
                {
                    field: 'grp_assgnmnt_mthd', displayName: 'Group Assignment Method', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,headerTooltip:'Group Assignment Method',
                    width: "*", minWidth: 160, maxWidth: 900
                },
                {
                    field: 'grp_owner', displayName: 'Group Owner', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE, headerTooltip: 'Group Owner',
                    width: "*", minWidth: 120, maxWidth: 900, visible: false
                },
                {
                    field: 'trans_key', displayName: 'Transaction Key', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE, headerTooltip: 'Transaction Key',
                    width: "*", minWidth: 100, maxWidth: 900

                },
                {
                    field: 'user_id', displayName: 'User Id',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', headerTooltip: 'User Id',
                    width: "*", minWidth: 100, maxWidth: 900
                },
                {
                    field: 'dw_srcsys_trans_ts', displayName: 'DW Transaction Timestamp',
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE, sort: { direction: uiGridConstants.DESC, priority: 1 },
                    width: "*", minWidth: 200, maxWidth: 900
                },
                {
                    field: 'row_stat_cd', displayName: 'Row Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 30, maxWidth: 900, visible:false
                },
                {
                    field: 'appl_src_cd', displayName: 'Application Source Code', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 155, maxWidth: 900,visible:false
                },

                {
                    field: 'load_id', displayName: 'Load Id', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',
                    headerCellTemplate: GRID_HEADER_TEMPLATE,
                    width: "*", minWidth: 90, maxWidth: 900, visible: false
                },

                {
                    field: 'User Actions',minWidth: 220, maxWidth: 900,
                    cellTemplate: 'App/Upload/Views/common/gridReferenceDropDown.tpl.html', width: "*"
                }

            ]
        },

        getReferenceGridOptions: function (uiGridConstants, columnDefs) {

            var rowtpl = '<div ng-class="{\'greyClass\':row.entity.row_stat_cd == \'L\'}"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';

            var gridOptions = {
                enableRowSelection: false,
                enableRowHeaderSelection: false,
                enableFiltering: false,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 50,
                multiSelect: false,

                paginationPageSize: 10,
                paginationPageSizes: [ 10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePagination: true,
                enablePaginationControls: false,
                enableVerticalScrollbar: 0,
                enableHorizontalScrollbar: 1,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs,
                rowTemplate: rowtpl

            };

            gridOptions.data = '';
            return gridOptions;
        }

    }

}]);



angular.module('upload').factory('referenceDataDropDownService', ['$http', function ($http) {

    var URL = {};

    URL[REFERENCE_DATA_CONSTANTS.GROUP_TYPE] = "Home/getGroupTypeData";
    URL[REFERENCE_DATA_CONSTANTS.SUB_GROUP_TYPE] = "Home/getSubGroupTypeData";
    URL[REFERENCE_DATA_CONSTANTS.GROUP_ASSIGNMENT_METHOD] = "Home/getChapterGroupAssignmentMethod";

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


//added by srini 
angular.module('upload').constant('GROUP_UPLOAD_CONSTANTS', {
    'VALIDATION_POPUP': 'GroupUploadValidation',
    'DATA_POPUP': 'GroupUserUploadData',
    'CONFIRMATION_POPUP': 'GroupUploadConfirmation',
    'ERROR_POPUP': 'GroupUploadError'
});


angular.module('upload').factory('GroupMembershipUploadService',
    ['$uibModal', '$rootScope', '$state', '$uibModalStack', 'uiGridConstants', 'UploadDataServices', 'GROUP_UPLOAD_CONSTANTS',
    function ($uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, UploadDataServices, GROUP_UPLOAD_CONSTANTS) {
        return {
            getGroupPopup: function ($scope, modalSize, groupUploadModaltype) {
                if (GROUP_UPLOAD_CONSTANTS.VALIDATION_POPUP == groupUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/GroupMembershipUploadValidation.tpl.html';
                    var controller = 'UploadValidationCtrl';
                }
                else if (GROUP_UPLOAD_CONSTANTS.DATA_POPUP == groupUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/GroupMembershipUserInput.tpl.html';
                    var controller = 'UserUploadDataCtrl';
                }
                else if (GROUP_UPLOAD_CONSTANTS.CONFIRMATION_POPUP == groupUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/GroupMembershipUploadConfirm.tpl.html';
                    var controller = 'UserUploadConfirmCtrl';
                }
                else if (GROUP_UPLOAD_CONSTANTS.ERROR_POPUP == groupUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/common/GroupMembershipUploadError.tpl.html';
                    var controller = 'UserUploadErrorCtrl';
                }

                OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, modalSize, $rootScope, $state, $uibModalStack, null, UploadDataServices);

            },
            /*clearFileInput: function () {
                var result = document.getElementsByClassName("file-input-label");         //angular.element("input[type='file']").val(null);
                angular.element(result).text('');
                angular.element("input[type='file']").val(null);
            },
            getGroupUploadValidationGridLayout: function (gridOptions, uiGridConstants, results) {
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


