upldModule.controller('AffiliationUploadController', ['$scope', '$rootScope', 'mainService', 'uploadDataServices', 'uploadServices',
function ($scope, $rootScope, mainService, uploadDataServices, uploadServices) {

    var username = "";
    var BasePath = $("base").first().attr("href");

    $scope.uploadTemplateDownload = BasePath + "Files/Upload/Affiliation_Upload_Template.xlsx";

    $scope.pleaseWait = { "display": "none" };

    mainService.getUsername().then(function (result) {
        username = result.data;
        uploadDataServices.setUserName(username);
    });

    $scope.file = null;
    var formdata = null;

    $rootScope.affiliationUploadWarningGrid = {};

    $rootScope.affiliationUploadWarningGrid.onRegisterApi = function (gridApi) {
        //Store the grid data against a variable so that it can be used for pagination
        $rootScope.affiliationUploadWarningGridApi = gridApi;
    }

    //Method which is called to paginate the search results
    $rootScope.affiliationUploadWarningGridPageChange = function (page) {
        $rootScope.affiliationUploadWarningGridApi.pagination.seek(page);
    }


    //Bind the grid data
    $rootScope.affiliationUploadWarningGrid = {
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
        uploadServices.uploadAffiliationFile(formdata)
            .success(function (result) {
                if (result.strUploadResult == UPLD_CONSTANTS.NO_FILE) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.AFF_UPLD_FAIL_HEADER, UPLD_CONSTANTS.NO_FILE_UPLD_MESSAGE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.INVALID_FORMAT) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.AFF_UPLD_FAIL_HEADER, UPLD_CONSTANTS.INCORRECT_TEMPLATE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.FILE_DUPLICATE) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.AFF_UPLD_FAIL_HEADER, UPLD_CONSTANTS.SIMILAR_FILE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult == UPLD_CONSTANTS.NOT_VALID) {
                    MessagePopup($rootScope, UPLD_CONSTANTS.AFF_UPLD_FAIL_HEADER, UPLD_CONSTANTS.NO_DATE_FILE, UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.strUploadResult != null && result.strUploadResult.indexOf('Exceeds Limit') !== -1) {
                    var exceedLimitValue = result.strUploadResult.replace('Exceeds Limit -', '');
                    MessagePopup($rootScope, UPLD_CONSTANTS.AFF_UPLD_FAIL_HEADER, "The uploaded file " + $files[0].name + " has " + exceedLimitValue + " rows. The upload feature only supports 2000 or fewer rows. Please split the data file and retry the upload.", UPLD_CONSTANTS.ICON_ERROR);
                    $scope.pleaseWait = { "display": "none" };
                }
                else if(result.intErrorCount > 0)
                {
                    $rootScope.affiliationUploadWarningGrid.columnDefs = [
                        {
                            field: 'strEnterpriseOrgId', displayName: 'Enterprise Org ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if(row.entity.strEnterpriseOrgIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strMasterId', displayName: 'Master ID', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>',
                            cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {

                                var cellValue = grid.getCellValue(row, col);
                                if(row.entity.strMasterIdFlag == '0')
                                    return 'redClass';
                            }
                        },
                        {
                            field: 'strStatus', displayName: 'Status', width: "*", minWidth: 100, maxWidth: 900,
                            cellTemplate: '<div class="wordwrap"}>{{COL_FIELD}}</div>'
                        }
                    ];

                    $rootScope.affiliationUploadWarningGrid.data = result.invalidRecords;
                    $rootScope.affiliationUploadWarningGrid.totalItems = $rootScope.affiliationUploadWarningGrid.data.length;

                    angular.element(fileUploadWarningModal).modal({ backdrop: "static" });
                    $scope.pleaseWait = { "display": "none" };
                }
                else if (result.boolSuccess) {
                    uploadDataServices.setuploadResult(result);
                    angular.element(fileUploadSuccessModal).modal({backdrop: "static"});
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

    //Method to submit the affiliation records to the server
    $scope.uploadAffiliationRecords = function () {
        $scope.pleaseWait = { "display": "block" };
        var validRec = uploadDataServices.getuploadResult();
        //var serviceInput = {};
        //serviceInput = { input: validRec.validRecords, strUploadFileName: validRec.strUploadFileName , intFileSize: validRec.intFileSize, strDocExtention: validRec.strDocExtention, strUserName: username };

        //Clear the file input
        uploadDataServices.clearFileInput();

        MessagePopup($rootScope, "Affiliation File Upload Succeeded", "The file " + validRec.strUploadFileName + " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.", "Images/tick.png");
        $scope.pleaseWait = { "display": "none" };

        //Submit the file to the server
        uploadServices.submitAffiliationFile(uploadDataServices.getFormData())
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

    function MessagePopup($rootScope, headerText, bodyText, imageLoc)
    {
        $rootScope.uploadModalPopupHeaderText = headerText;
        $rootScope.uploadModalPopupBodyText = bodyText;
        $rootScope.uploadModalPopupImageLoc = imageLoc;
        angular.element(uploadMessageDialogBox).modal({backdrop: "static"});
    }
}]);
