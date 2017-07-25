upldModule.controller('EOUploadController', ['$scope', '$rootScope', 'mainService', 'uploadDataServices', 'uploadServices',
function ($scope, $rootScope, mainService, uploadDataServices, uploadServices) {

    var username = "";
    var BasePath = $("base").first().attr("href");

    $scope.uploadTemplateDownload = BasePath + "Files/Upload/EO_Upload_Template.xlsx";

    $scope.pleaseWait = { "display": "none" };

    mainService.getUsername().then(function (result) {
        username = result.data;
        uploadDataServices.setUserName(username);
    });

    $scope.file = null;
    var formdata = null;

    $rootScope.eoUploadWarningGrid = {};

    $rootScope.eoUploadWarningGrid.onRegisterApi = function (gridApi) {
        //Store the grid data against a variable so that it can be used for pagination
        $rootScope.eoUploadWarningGridApi = gridApi;
    }

    //Method which is called to paginate the search results
    $rootScope.eoUploadWarningGridPageChange = function (page) {
        $rootScope.eoUploadWarningGridApi.pagination.seek(page);
    }


    //Bind the grid data
    $rootScope.eoUploadWarningGrid = {
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
        uploadServices.uploadEoFile(formdata)
            .success(function (result) {
                if (result.strUploadResult == UPLD_CONSTANTS.NO_FILE) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EO_FAIL_HEADER, UPLD_CONSTANTS.NO_FILE_UPLD_MESSAGE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.INVALID_FORMAT) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EO_FAIL_HEADER, UPLD_CONSTANTS.INCORRECT_TEMPLATE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.FILE_DUPLICATE) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EO_FAIL_HEADER, UPLD_CONSTANTS.SIMILAR_FILE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.NOT_VALID) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.EO_FAIL_HEADER, UPLD_CONSTANTS.NO_DATE_FILE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult != null && result.strUploadResult.indexOf('Exceeds Limit') !== -1) {
                    var exceedLimitValue = result.strUploadResult.replace('Exceeds Limit -', '');
                    MessagePopup($rootScope, UPLD_CONSTANTS.EO_FAIL_HEADER, "The uploaded file " + $files[0].name + " has " + exceedLimitValue + " rows. The upload feature only supports 2000 or fewer rows. Please split the data file and retry the upload.", UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.intErrorCount > 0) {
                    $rootScope.eoUploadWarningGrid.columnDefs = [
                        {
                            field: 'strAction', displayName: 'Action', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        },
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
                            field: 'strEnterpriseOrgName', displayName: 'Enterprise Org Name', width: "*", minWidth: 200, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strEnterpriseOrgNameFlag == '0')
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
                            field: 'strCharacteristics3Code', displayName: 'Characteristics 3 Code', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strCharacteristics3CodeFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strCharacteristics3Value', displayName: 'Characteristics 3 Value', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strCharacteristics3ValueFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition1Type1', displayName: 'Affiliation Match Rule 1 Type Code 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition1Type1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition1String1', displayName: 'Affiliation Match Rule 1 String 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition1String1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition1Type2', displayName: 'Affiliation Match Rule 1 Type Code 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition1Type2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition1String2', displayName: 'Affiliation Match Rule 1 String 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition1String2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition1Type3', displayName: 'Affiliation Match Rule 1 Type Code 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition1Type3Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition1String3', displayName: 'Affiliation Match Rule 1 String 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition1String3Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition2Type1', displayName: 'Affiliation Match Rule 2 Type Code 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition2Type1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition2String1', displayName: 'Affiliation Match Rule 2 String 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition2String1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition2Type2', displayName: 'Affiliation Match Rule 2 Type Code 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition2Type2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition2String2', displayName: 'Affiliation Match Rule 2 String 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition2String2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition2Type3', displayName: 'Affiliation Match Rule 2 Type Code 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition2Type3Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition2String3', displayName: 'Affiliation Match Rule 2 String 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition2String3Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition3Type1', displayName: 'Affiliation Match Rule 3 Type Code 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition3Type1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition3String1', displayName: 'Affiliation Match Rule 3 String 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition3String1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition3Type2', displayName: 'Affiliation Match Rule 3 Type Code 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition3Type2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition3String2', displayName: 'Affiliation Match Rule 3 String 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition3String2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition3Type3', displayName: 'Affiliation Match Rule 3 Type Code 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition3Type3Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTransformCondition3String3', displayName: 'Affiliation Match Rule 3 String 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTransformCondition3String3Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTag1', displayName: 'Tag 1', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTag1Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTag2', displayName: 'Tag 2', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTag2Flag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strTag3', displayName: 'Tag 3', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if (row.entity.strTag3Flag == '0')
                                    return 'redClass';
                            }
                        }
                    ];

                    $rootScope.eoUploadWarningGrid.data = result.invalidRecordsEo;
                    $rootScope.eoUploadWarningGrid.totalItems = $rootScope.eoUploadWarningGrid.data.length;

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

    //Method to submit the EO records to the server
    $scope.uploadEORecords = function () {
        $scope.pleaseWait = { "display": "block" };
        var validRec = uploadDataServices.getuploadResult();
        //var serviceInput = {};
        //serviceInput = { input: [], strUploadFileName: validRec.strUploadFileName, intFileSize: validRec.intFileSize, strDocExtention: validRec.strDocExtention, strUserName: username };

        //Clear the file input
        uploadDataServices.clearFileInput();

        MessagePopup($rootScope, "EO File Upload Succeeded", "The file " + validRec.strUploadFileName + " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.", "Images/tick.png");
        $scope.pleaseWait = { "display": "none" };

        //Submit the file to the server
        uploadServices.submitEoFile(uploadDataServices.getFormData())
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
