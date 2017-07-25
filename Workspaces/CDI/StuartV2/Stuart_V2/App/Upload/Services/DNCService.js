//added by srini 
angular.module('upload').constant('DNC_CONSTANTS', {
    'VALIDATION_POPUP': 'DNCUploadValidation',
    'DATA_POPUP': 'DNCUserUploadData',
    'CONFIRMATION_POPUP': 'DNCUploadConfirmation',
    'ERROR_POPUP': 'DNCUploadError',
    'CHAPTERCODE': 'chapterCode',
    'GROUPCODE': 'groupCode',
    'DNC_TEMPLATE_PATH': BasePath + "App/Upload/Files/DncUploadTemplate.xlsx",
    'DNC_VALIDATION':'DncValidation'
    
});


angular.module('upload').constant('UPLOAD_CONSTANTS', {
    'DNC_UPLOAD':'DNC_UPLOAD'
});

angular.module('upload').factory('DNCServices', ['$q', 'DNCModalService', 'DNC_CONSTANTS', '$rootScope', function ($q, DNCModalService, DNC_CONSTANTS,$rootScope) {
    return {
        getDNCPopup: function (dncUploadModaltype,results) {
            var deferred = $q.defer();
            if (DNC_CONSTANTS.DATA_POPUP == dncUploadModaltype) {
                var templateUrl = BasePath + 'App/Upload/Views/dnc/DNCUserInput.tpl.html';
                var controller = 'DNCUploadUserDataCtrl';
                var modalsize = 'sm';
            }
            if (DNC_CONSTANTS.VALIDATION_POPUP == dncUploadModaltype) {
                var templateUrl = BasePath + 'App/Upload/Views/dnc/DNCValidation.tpl.html';
                var controller = 'DNCValidationCtrl';
                var modalsize = 'lg';
            }
            if (DNC_CONSTANTS.CONFIRMATION_POPUP == dncUploadModaltype) {
                var templateUrl = BasePath + 'App/Upload/Views/dnc/DNCUserConfirm.tpl.html';
                var controller = 'DNCUploadUserConfirmCtrl';
                var modalsize = 'sm';
            }


            DNCModalService.OpenModal(templateUrl, controller, modalsize, results).then(function () {

                deferred.resolve();
            });
            return deferred.promise;
        }, 
        clearFileInput: function () {
            var result = document.getElementsByClassName("file-input-label");         //angular.element("input[type='file']").val(null);
            angular.element(result).text('');
            angular.element("input[type='file']").val(null);
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
                paginationPageSize: 5,
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
        messageUploadPopup: function (message, header) {
            $rootScope.uploadMessage = message;
            $rootScope.uploadHeader = header;
            angular.element("#iUploadErrorModal").modal();
        }

    }

}]);


angular.module('upload').factory('DNCDropDownService', ['$http', 'DNC_CONSTANTS', function ($http, DNC_CONSTANTS) {

    var URL = {};

    URL[DNC_CONSTANTS.CHAPTERCODE] = "Home/getChapterCode";
    URL[DNC_CONSTANTS.GROUPCODE] = "Home/getChapterGroupCodeName";

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



angular.module('upload').factory('DncDataServices', ['$http', '$q', function ($http, $q) {

    var loggedInUserName = null;
    var dncUploadedResult = null;
    var dncUploadedFileMessage = null;

    return {
        setUserName:function (strUserName) {
            loggedInUserName = strUserName;
        },

        getUserName: function () {
            return loggedInUserName;
        },
        getDNCUploadParams: function (uploadParams) {
            return $http.post(BasePath + "CemUploadNative/setDncParams", uploadParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) {
                console.log(result);
            }, function (error) { return $q.reject(error);})
        },
        postDncUploadData:function(postData,username){
            return $http.post(BasePath + "CemUploadNative/dncUploadData", JSON.stringify(""), {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) { console.log(result)}, function (error) { $q.reject(error); });
        },
        validateDncUpload: function (formdata) {
            return $http.post(BasePath + "CemUploadNative/validateFiles", formdata, {
                headers: {
                    'Content-Type': undefined
                }
            }).then(function (result) { return result}, function (error) { return $q.reject(error);})
        },

        setDncUploadFormData: function (result) {
            uploadFormData = result;
        },

        getDncUploadFormData: function () {
            return uploadFormData;
        },

        setUploadResult: function (result) {
            dncUploadedResult = result;
        },

        getUploadResult: function () {
            return dncUploadedResult;
        },
        setUploadedFileMessage: function (message) {
            dncUploadedFileMessage = message;
        },

        getUploadedFileMessage: function () {
            return dncUploadedFileMessage;
        }
        

    }
}]);


angular.module('upload').factory('DNCModalService', ['$rootScope', '$uibModal', '$q', function ($rootScope, $uibModal, $q) {
    return {
        OpenModal: function (templ, ctrl, size, results) {
            var deferred = $q.defer();
            var CssClass = '';
            if (size != 'sm') {
                CssClass = 'app-modal-window';
            }

            var ModalInstance = ModalInstance || $uibModal.open({
                animation: $rootScope.animationsEnabled,
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


var myApp;
myApp = myApp || (function () {
    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();


angular.module('upload').factory('DncColumnDefHelper', ['DNC_CONSTANTS', 'uiGridConstants', function (DNC_CONSTANTS, uiGridConstants) {
    var columnDef = [];
    return {
        getColumnDef: function (type, results) {
            switch (type) {
                case DNC_CONSTANTS.DNC_VALIDATION: {
                    columnDef = [
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
                                field: 'prefix',
                                width: "*", minWidth: 60, maxWidth: 9000,
                                filter: {
                                    condition: uiGridConstants.filter.STARTS_WITH
                                },
                                cellTemplate: '<div class="wordwrap">{{row.entity.prefix_nm}}</div>',
                                cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                    var cellValue = grid.getCellValue(row, col);

                                    if (results.hasOwnProperty('_invalidPrefix')) {
                                        if (results._invalidLastName[rowRenderIndex]) {
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
                                        if (results._invalidPrefix[rowRenderIndex]) {
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
                                field: 'created_by',
                                width: "*", minWidth: 100, maxWidth: 9000,
                                filter: {
                                    condition: uiGridConstants.filter.STARTS_WITH
                                },
                                cellTemplate: '<div class="wordwrap" >{{row.entity.created_by}}</div>',
                                displayName: 'Created By'
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
                            }
                        ]
                }
            }

            return columnDef;
        }
    }


}]);