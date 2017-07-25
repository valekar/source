upldModule.controller('EOSIUploadController', ['$scope', '$rootScope', 'mainService', 'uploadDataServices', 'uploadServices',
function ($scope, $rootScope, mainService, uploadDataServices, uploadServices) {

    var username = "";
    var BasePath = $("base").first().attr("href");

    $scope.uploadTemplateDownload = BasePath + "Files/Upload/EOSI_Upload_Template.xlsx";

    $scope.pleaseWait = { "display": "none" };

    mainService.getUsername().then(function (result) {
        username = result.data;
        uploadDataServices.setUserName(username);
    });

    $scope.file = null;
    var formdata = null;

    $rootScope.eosiUploadWarningGrid = {};

    $rootScope.eosiUploadWarningGrid.onRegisterApi = function (gridApi) {
        //Store the grid data against a variable so that it can be used for pagination
        $rootScope.eosiUploadWarningGridApi = gridApi;
    }

    //Method which is called to paginate the search results
    $rootScope.eosiUploadWarningGridPageChange = function (page) {
        $rootScope.eosiUploadWarningGridApi.pagination.seek(page);
    }


    //Bind the grid data
    $rootScope.eosiUploadWarningGrid = {
        enablePager: false,
        paginationPageSize: 8,
        enableSorting: true,
        enableGridMenu: true,
        enableFiltering: false,
        paginationPageSizes: [8],
        enablePaginationControls: false,
        enableVerticalScrollbar: 1,
        enableHorizontalScrollbar: 1,
        showGridFooter: false,
        selectionRowHeaderWidth: 35,
        rowHeight: 43,
        paginationCurrentPage: 1,
        totalItems: 0,
        enableColumnResizing: true,
        enableColumnMoving: true,
        enableRowSelection: false,
        enableRowHeaderSelection: false,
        enableSelectAll: false,
        data: []
    };

    //On uploading the file, store the uploaded file and call the service to validate the records
    $scope.getTheFiles = function ($files) {

        $scope.pleaseWait = { "display": "block" };

        var result = document.getElementsByClassName("file-input-label");
        angular.element(result).text($files[0].name);

        formdata = new FormData();
        uploadDataServices.setFormData(formdata);
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });

        //Send the files to the server
        uploadServices.uploadEosiFile(formdata)
            .success(function (result) {
                if (result.strUploadResult == UPLD_CONSTANTS.NO_FILE) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EOSI_FAIL_HEADER, UPLD_CONSTANTS.NO_FILE_UPLD_MESSAGE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.INVALID_FORMAT) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EOSI_FAIL_HEADER, UPLD_CONSTANTS.INCORRECT_TEMPLATE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.FILE_DUPLICATE) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EOSI_FAIL_HEADER, UPLD_CONSTANTS.SIMILAR_FILE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.NOT_VALID) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EOSI_FAIL_HEADER, UPLD_CONSTANTS.NO_DATE_FILE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult != null && result.strUploadResult.indexOf('Exceeds Limit') !== -1) {
                    var exceedLimitValue = result.strUploadResult.replace('Exceeds Limit -', '');
                    MessagePopup($rootScope, UPLD_CONSTANTS.EOSI_FAIL_HEADER, "The uploaded file " + $files[0].name + " has " + exceedLimitValue + " rows. The upload feature only supports 2000 or fewer rows. Please split the data file and retry the upload.", UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.intErrorCount > 0) {
                    $rootScope.eosiUploadWarningGrid.columnDefs = [
                        {
                            field: 'strEnterpriseOrgId', displayName: 'Enterprise Org ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strEnterpriseOrgIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strMasterId', displayName: 'Master ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strMasterIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strSourceSystemCode', displayName: 'Source Code', width: "*", minWidth: 70, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strSourceSystemCodeFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strSourceId', displayName: 'Source ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strSourceIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strSecondarySourceId', displayName: 'Secondary Source ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strSecondarySourceIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strParentEnterpriseOrgId', displayName: 'Parent Enterprise ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strParentEnterpriseOrgIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strAltSourceCode', displayName: 'Alternate Source Code', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strAltSourceId', displayName: 'Alternate Source ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strOrgName', displayName: 'Org Name', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strAddress1Street1', displayName: 'Address Street 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strAddress1Street2', displayName: 'Address Street 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strAddress1City', displayName: 'City', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strAddress1State', displayName: 'State', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strAddress1Zip', displayName: 'Zip', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strPhone1', displayName: 'Phone 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strPhone2', displayName: 'Phone 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strNaicsCode', displayName: 'NAICS Code', width: "*", minWidth: 50, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strNaicsCodeFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strCharacteristics1Code', displayName: 'Characteristics 1 Code', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strCharacteristics1CodeFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strCharacteristics1Value', displayName: 'Characteristics 1 Value', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strCharacteristics1ValueFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strCharacteristics2Code', displayName: 'Characteristics 2 Code', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strCharacteristics2CodeFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strCharacteristics2Value', displayName: 'Characteristics 2 Value', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strCharacteristics2ValueFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strRMIndicator', displayName: 'RM Indicator', width: "*", minWidth: 50, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
                        {
                            field: 'strNotes', displayName: 'Notes', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        }
                    ];

                    $rootScope.eosiUploadWarningGrid.data = result.invalidRecordsEosi;
                    $rootScope.eosiUploadWarningGrid.totalItems = $rootScope.eosiUploadWarningGrid.data.length;

                    angular.element(fileUploadWarningModal).modal({ backdrop: "static" });
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.boolSuccess) {
                    uploadDataServices.setuploadResult(result);
                    angular.element(fileUploadSuccessModal).modal({ backdrop: "static" });
                    $scope.pleaseWait = { "display": "none" };
                }
                //Clear the file input
                uploadDataServices.clearFileInput();
            })
            .error(function (result) {
                errorPopups(result);
                //Clear the file input
                uploadDataServices.clearFileInput();
                $scope.pleaseWait = { "display": "none" };
            });
    };

    //Method to submit the EOSI records to the server
    $scope.uploadEOSIRecords = function () {
        $scope.pleaseWait = { "display": "block" };
        var validRec = uploadDataServices.getuploadResult();
        //var serviceInput = {};
        //serviceInput = { input: [], strUploadFileName: validRec.strUploadFileName, intFileSize: validRec.intFileSize, strDocExtention: validRec.strDocExtention, strUserName: username };

        //Clear the file input
        uploadDataServices.clearFileInput();

        MessagePopup($rootScope, "EOSI File Upload Succeeded", "The file " + validRec.strUploadFileName + " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.", "Images/tick.png");
        $scope.pleaseWait = { "display": "none" };

        //Submit the file to the server
        uploadServices.submitEosiFile(uploadDataServices.getFormData())
        .success(function (result) {
           // console.log('File has been uploaded to server');
        });
    }

    function errorPopups(result) {
        if (result == GEN_CONSTANTS.ACCESS_DENIED) {
            MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE, UPLD_CONSTANTS.ICON_ERROR);
        }
        else if (result == GEN_CONSTANTS.DB_ERROR) {
            MessagePopup($rootScope, GEN_CONSTANTS.DB_ERROR_CONFIRM, GEN_CONSTANTS.DB_ERROR_MESSAGE, UPLD_CONSTANTS.ICON_ERROR);
        }
        else if (result == GEN_CONSTANTS.TIMEOUT_ERROR) {
            MessagePopup($rootScope, GEN_CONSTANTS.TIMEOUT_ERROR_CONFIRM, GEN_CONSTANTS.TIMEOUT_ERROR_MESSAGE);
        }
    }

    function MessagePopup($rootScope, headerText, bodyText, imageLoc) {
        $rootScope.uploadModalPopupHeaderText = headerText;
        $rootScope.uploadModalPopupBodyText = bodyText;
        $rootScope.uploadModalPopupImageLoc = imageLoc;
        angular.element(uploadMessageDialogBox).modal({ backdrop: "static" });
    }
}]);
