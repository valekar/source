angular.module('upload').constant('MSG_PREF_CONSTANTS', {

    'MSG_PREF_TEMPLATE_PATH': BasePath + "App/Upload/Files/MessagePrefTemplate.xlsx",
    'VALIDATION_POPUP': 'MsgPrefUploadValidation',
    'DATA_POPUP': 'MsgPrefUserUploadData',
    'CONFIRMATION_POPUP': 'MsgPrefUploadConfirmation',
    'MSG_PREF_VALIDATION': 'MsgPrefValidation',
    'MSG_PREF_ADD_PARAMS_URL': BasePath + "upload/message/preference/add/params",
    'MSG_PREF_VALIDATE_URL': BasePath + "upload/message/preference/validate",
    'MSG_PREF_ADD_DATA_URL': BasePath + "upload/message/preference/add/data"
});


angular.module('upload').factory('MsgPrefDataServices', ['$http', '$q','MSG_PREF_CONSTANTS', function ($http, $q,MSG_PREF_CONSTANTS) {
    var loggedInUserName = null;
    var msgPrefUploadedResult = null;
    var msgPrefUploadedFileMessage = null;
    return {
        setMsgPrefUploadFormData: function (result) {
            uploadFormData = result;
        },

        getMsgPrefUploadFormData: function () {
            return uploadFormData;
        },
        setUserName: function (strUserName) {
            loggedInUserName = strUserName;
        },

        getUserName: function () {
            return loggedInUserName;
        },
        getMsgPrefUploadParams: function (uploadParams) {
            return $http.post(MSG_PREF_CONSTANTS.MSG_PREF_ADD_PARAMS_URL +"", uploadParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) {
                console.log(result);
            }, function (error) { return $q.reject(error); })
        },
        validateMsgPrefUpload: function (formdata) {
            return $http.post(MSG_PREF_CONSTANTS.MSG_PREF_VALIDATE_URL + "", formdata, {
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (result) { return result }, function (error) { return $q.reject(error); })
        },
        postMsgPrefUploadData: function (postParams) {
            return $http.post(MSG_PREF_CONSTANTS.MSG_PREF_ADD_DATA_URL + "", postParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) { return result }, function (error) { return $q.reject(error); })
        },
        setUploadResult: function (result) {
            msgPrefUploadedResult = result;
        },

        getUploadResult: function () {
            return msgPrefUploadedResult;
        },
        setUploadedFileMessage: function (message) {
            msgPrefUploadedFileMessage = message;
        },

        getUploadedFileMessage: function () {
            return msgPrefUploadedFileMessage;
        }
    }

}]);


angular.module('upload').factory('MsgPrefServices', ['$q', 'MsgPrefModalService', 'MSG_PREF_CONSTANTS',
    function ($q, MsgPrefModalService, MSG_PREF_CONSTANTS) {
        return {
            getMsgPrefPopup: function (msgPrefUploadModaltype, results) {
                var deferred = $q.defer();
                if (MSG_PREF_CONSTANTS.DATA_POPUP == msgPrefUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/msgPref/MsgPrefUserInput.tpl.html';
                    var controller = 'MsgPrefUploadUserDataCtrl';
                    var modalsize = 'sm';
                }
                if (MSG_PREF_CONSTANTS.VALIDATION_POPUP == msgPrefUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/msgPref/MsgPrefValidation.tpl.html';
                    var controller = 'MsgPrefValidationCtrl';
                    var modalsize = 'lg';
                }
                if (MSG_PREF_CONSTANTS.CONFIRMATION_POPUP == msgPrefUploadModaltype) {
                    var templateUrl = BasePath + 'App/Upload/Views/msgPref/MsgPrefUserConfirm.tpl.html';
                    var controller = 'MsgPrefUploadUserConfirmCtrl';
                    var modalsize = 'sm';
                }


                MsgPrefModalService.OpenModal(templateUrl, controller, modalsize, results).then(function () {

                    deferred.resolve();
                });
                return deferred.promise;
            },
            clearFileInput: function () {
                var result = document.getElementsByClassName("file-input-label");
                angular.element(result).text('');
                angular.element("input[type='file']").val(null);
            },
            messageUploadPopup: function (message, header) {
                $rootScope.uploadMessage = message;
                $rootScope.uploadHeader = header;
                angular.element("#iUploadErrorModal").modal();
            },
             getGridOptions: function (colDef,horizontal) {

            var gridOptions = {
                enableRowSelection: false,
                enableRowHeaderSelection: false,
                enableFiltering: false,
                enableSelectAll: false,

                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 10,
                paginationPageSizes: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: 0,
                enableHorizontalScrollbar: horizontal,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: colDef,
                enableColumnResizing: true,
                enableColumnMoving: true

            };
            gridOptions.data = '';
            return gridOptions;
        },

        }

    }]);


angular.module('upload').factory('MsgPrefModalService', ['$uibModal', '$q', function ($uibModal, $q) {
    return {
        OpenModal: function (templ, ctrl, size, results) {
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
                    results: function () {
                        return results;
                    }
                }
            });

            ModalInstance.result.then(function (result) {
                console.log("Modal Instance Result " + result);
                console.log("State Param before");
                //console.log($state);
                // modalMessage($rootScope, result, $state, $uibModalStack, UploadDataServices);
                deferred.resolve();
            });


            return deferred.promise;
        }
    }

}]);


angular.module('upload').factory('MsgPrefColumnDefHelper', ['uiGridConstants', function (uiGridConstants) {

    return {
        getColumnDef: function (results) {
            return [
                        { field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/Upload/Views/common/iconGrid.tpl.html', enableCellEdit: false },
                        {
                            field: 'init_mstr_id',
                            width: "*", minWidth: 80, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.init_mstr_id}}</div>',
                            displayName: 'Master Id',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                                // console.log(results._invalidMasterIds[rowRenderIndex]);
                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidMasterIds')) {
                                    if (results._invalidMasterIds[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            }
                        },
                        {
                            field: 'arc_srcsys_cd',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.arc_srcsys_cd}}</div>',
                            displayName: 'Source System Code',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidSourceSystemCode')) {
                                    if (results._invalidSourceSystemCode[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            }
                        },
                        {
                            field: 'cnst_srcsys_id',
                            width: "*", minWidth: 150, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_srcsys_id}}</div>',
                            displayName: 'Source System Id',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidSourceSystemId')) {
                                    if (results._invalidSourceSystemId[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            }
                        },
                        {
                            field: 'line_of_service_cd',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.line_of_service_cd}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidLineOfServiceCodes')) {
                                    if (results._invalidLineOfServiceCodes[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Line of Service Code'
                        },
                        {
                            field: 'comm_chan',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.comm_chan}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidCommChannels')) {
                                    if (results._invalidCommChannels[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Communication Channel'
                        },
                        {
                            field: 'msg_prefc_typ',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.msg_prefc_typ}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidMsgPrefTypes')) {
                                    if (results._invalidMsgPrefTypes[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Pref Type'
                        },
                        {
                            field: 'msg_prefc_val',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.msg_prefc_val}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidMsgPrefValues')) {
                                    if (results._invalidMsgPrefValues[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Pref Value'
                        },

                       {
                           field: 'prefix',
                           width: "*", minWidth: 60, maxWidth: 9000,
                           filter: {
                               condition: uiGridConstants.filter.STARTS_WITH
                           },
                           cellTemplate: '<div class="wordwrap">{{row.entity.prefix_nm}}</div>',
                           cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                               var cellValue = grid.getCellValue(row, col);

                               if (results.hasOwnProperty('_invalidPrefix')) {
                                   if (results._invalidPrefix[rowRenderIndex]) {
                                       return 'redClass';
                                   }
                               }

                           },
                           displayName: 'Prefix'
                       },
                        {
                            field: 'prsn_frst_nm',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.prsn_frst_nm}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidLastName')) {
                                    if (results._invalidLastName[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'First Name',

                        },
                        {
                            field: 'prsn_mid_nm',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.prsn_mid_nm}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidMiddleName')) {
                                    if (results._invalidMiddleName[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Middle Name'
                        },
                        {
                            field: 'prsn_lst_nm',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.prsn_lst_nm}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidFirstName')) {
                                    if (results._invalidFirstName[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Last Name'
                        },
                        {
                            field: 'suffix',
                            width: "*", minWidth: 60, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.suffix_nm}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);

                                if (results.hasOwnProperty('_invalidSuffix')) {
                                    if (results._invalidSuffix[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                                }

                            },
                            displayName: 'Suffix'
                        },
                         {
                             field: 'cnst_org_nm',
                             width: "*", minWidth: 120, maxWidth: 9000,
                             filter: {
                                 condition: uiGridConstants.filter.STARTS_WITH
                             },
                             cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_org_nm}}</div>',
                             cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                 var cellValue = grid.getCellValue(row, col);

                                 if (results.hasOwnProperty('_invalidFirstName')) {
                                     if (results._invalidOrgName[rowRenderIndex]) {
                                         return 'redClass';
                                     }
                                 }

                             },
                             displayName: 'Org Name'
                         },
                        {
                            field: 'cnst_addr_line1',
                            width: "*", minWidth: 150, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_addr_line1}}</div>',
                            displayName: 'Address Line 1',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                                //console.log("aaddddrress");
                                // console.log(results._invalidAddressLine1[rowRenderIndex]);

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidAddressLine1'))
                                    if (results._invalidAddressLine1[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },
                        {
                            field: 'cnst_addr_line2',
                            width: "*", minWidth: 150, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_addr_line2}}</div>',
                            displayName: 'Address Line 2',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invaldAddressLine2'))
                                    if (results._invaldAddressLine2[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },
                        {
                            field: 'cnst_addr_city',
                            width: "*", minWidth: 60, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_addr_city}}</div>',
                            displayName: 'City',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidCity'))
                                    if (results._invalidCity[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },
                        {
                            field: 'cnst_addr_state',
                            width: "*", minWidth: 60, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_addr_state}}</div>',
                            displayName: 'State',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidState'))
                                    if (results._invalidState[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },
                        {
                            field: 'cnst_addr_zip5',
                            width: "*", minWidth: 60, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_addr_zip5}}</div>',
                            displayName: 'Zip 5',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidZip'))
                                    if (results._invalidZip[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },

                        {
                            field: 'cnst_email_addr',
                            width: "*", minWidth: 120, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap">{{row.entity.cnst_email_addr}}</div>',
                            displayName: 'Email Address',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidEmailAddresses'))
                                    if (results._invalidEmailAddresses[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },
                        {
                            field: 'cnst_phn_num',
                            width: "*", minWidth: 130, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.cnst_phn_num}}</div>',
                            displayName: 'Phone Number',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (results.hasOwnProperty('_invalidPhoneNumber'))
                                    if (results._invalidPhoneNumber[rowRenderIndex]) {
                                        return 'redClass';
                                    }
                            }
                        },

                        {
                            field: 'notes',
                            width: "*", minWidth: 220, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.notes}}</div>',
                            displayName: 'Notes'
                        },
                        
                             {
                                 field: 'msg',
                                 width: "*", minWidth: 320, maxWidth: 9000,
                                 filter: {
                                     condition: uiGridConstants.filter.STARTS_WITH
                                 },
                                 cellTemplate: '<div class="wordwrap" >{{row.entity.msg}}</div>',
                                 displayName: 'Error Messages'
                             },
                        {
                            field: 'created_by',
                            width: "*", minWidth: 100, maxWidth: 9000,
                            filter: {
                                condition: uiGridConstants.filter.STARTS_WITH
                            },
                            cellTemplate: '<div class="wordwrap" >{{row.entity.created_by}}</div>',
                            displayName: 'Created By'
                        }
            ]
        }
    }


}]);