angular.module('upload').controller('EmailOnlyUploadController', ['$scope', '$location', '$log',
    '$window', '$localStorage', 'mainService', '$state', '$rootScope', '$http', '$uibModal','CommonUploadService',
    '$uibModalStack', 'uiGridConstants', 'EmailOnlyUploadDataServices', 'EmailOnlyUploadServices', '$timeout', 'EmailUploadModalService', 'EMAIL_UPLOAD_CONSTANTS',
function ($scope, $location, $log, $window, $localStorage, mainService, $state, $rootScope, $http, $uibModal, CommonUploadService, $uibModalStack,
    uiGridConstants, EmailOnlyUploadDataServices, EmailOnlyUploadServices, $timeout, EmailUploadModalService, EMAIL_UPLOAD_CONSTANTS) {

    var username = "";

    var BasePath = $("base").first().attr("href");

    $scope.emailUploadTemplateDownload = BasePath + "App/Upload/Files/EmailOnlyUploadTemplate.xlsx";

    var initialize = function () {
        $scope.pleaseWait = { "display": "none" };

        mainService.getUsername().then(function (result) {
            username = result.data;
            EmailOnlyUploadDataServices.setUserName(username);
        });
    };

    initialize();

    $scope.EmailOnlyUpload = function () {
        $scope.pleaseWait = { "display": "none" };
    }

    $scope.file = null;

    //UPLOAD FILE CODE

    var formdata = null;

    $scope.getTheFiles = function ($files) {
        var result = document.getElementsByClassName("file-input-label");
        angular.element(result).text($files[0].name);

        formdata = new FormData();
        EmailOnlyUploadDataServices.setEmailUploadFormData(formdata);
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });

        $scope.getUserUploadData();
    };

    //GET THE USER UPLOAD DATA

    $scope.getUserUploadData = function () {
        // getEmailUserUploadDataPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, EmailOnlyUploadDataServices);
        EmailUploadModalService.getEmailPopup($scope, 'lg', EMAIL_UPLOAD_CONSTANTS.DATA_POPUP);
    };

    //EVENT FIRED ON CLOSE OF THE MODAL 

    $scope.EmailOnlyUpload.similarFileUploadModalClose = function () {
        CommonUploadService.clearFileInput();
    }

    //SEND THE FILE DATA TO SERVER

    $scope.EmailOnlyUpload.fileUpload = function () {

        function pleaseWaitTimeoutPromise() {
            return $timeout(function () {
                return $uibModalStack.dismissAll();
            }, 1000);
        }

        $scope.pleaseWait = { "display": "block" };

        var uploadedFormData = EmailOnlyUploadDataServices.getEmailUploadResult();
        var str1 = "The file ";
        var uploadedFileMessage = str1.concat(uploadedFormData.UploadedEmailFileInputInfo.fileName, " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.");        //var uploadedFileMessage ="The file has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.";

        pleaseWaitTimeoutPromise().then(function (result) {
            $scope.pleaseWait = { "display": "none" };
            EmailOnlyUploadDataServices.setEmailUploadedFileMessage(uploadedFileMessage);
            //getEmailUploadConfirmationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, EmailOnlyUploadDataServices);
            EmailUploadModalService.getEmailPopup($scope, 'sm', EMAIL_UPLOAD_CONSTANTS.CONFIRMATION_POPUP);
        });

        CommonUploadService.clearFileInput();

        EmailOnlyUploadServices.postEmailOnlyUploadData(uploadedFormData, username).
            success(function (result) {
                console.log("File Data sent to Server");
                console.log(result);
            });
    };

    // NOW VALIDATE THE UPLOADED THE FILES

    $rootScope.$on('validateEmailUploadedFilesEvent', function (event, arg) {
        $scope.validateEmailUploadFiles();
    });

    $scope.validateEmailUploadFiles = function () {

        myApp.showPleaseWait();

        var request = {
            method: 'POST',
            url: BasePath + 'EmailOnlyNative/ValidateEmailUploadFiles/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        };

        // SEND THE FILES

        if (formdata != null || formdata != undefined) {
            $http(request).success(function (response) {
                if (response != "Failed!" && response != "FileAlreadyUploaded" && response != "IncorrectTemplate" &&
                    response.EmailUploadInvalidList != null && response.EmailUploadValidList != null) {

                    if (response.EmailUploadInvalidList.EmailOnlyUploadInputList.length == 0 &&
                        response.EmailUploadValidList.EmailOnlyUploadInputList.length > 0) {

                        console.log("Succeeded validation");

                        EmailOnlyUploadDataServices.setEmailUploadResult(response.EmailUploadValidList);
                        myApp.hidePleaseWait();

                        $uibModalStack.dismissAll();

                        //Clear the file input control
                        CommonUploadService.clearFileInput();

                        angular.element("#fileUploadSuccessModal").modal();

                    }
                    else if (response.EmailUploadInvalidList.EmailOnlyUploadInputList.length > 0) {
                        myApp.hidePleaseWait();

                        console.log("Failed at validation");

                        //Clear the file input control
                        CommonUploadService.clearFileInput();

                        EmailOnlyUploadDataServices.setEmailUploadValidationResult(response);
                        EmailOnlyUploadDataServices.setEmailUploadResult(response.EmailUploadInvalidList);

                        EmailOnlyUploadDataServices.setInvalidRecordsCount(response.EmailUploadInvalidList.EmailOnlyUploadInputList.length);

                        // getEmailUploadValidationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, EmailOnlyUploadDataServices);
                        EmailUploadModalService.getEmailPopup($scope, 'lg', EMAIL_UPLOAD_CONSTANTS.VALIDATION_POPUP);
                    }
                }
                else if (response.includes("FileLimitExceeded") && response != "FileAlreadyUploaded" && response != "IncorrectTemplate") {
                    myApp.hidePleaseWait();

                    $uibModalStack.dismissAll();

                    var str1 = "The uploaded file ";
                    var uploadedFileMessage = str1.concat(response.split("||")[2], " has ", response.split("||")[1], " rows. The upload feature only supports 2000 or fewer rows. Please split the data file and retry the upload.");

                    EmailOnlyUploadDataServices.setEmailUploadedFileMessage(uploadedFileMessage);

                    // getEmailUploadErrorPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, EmailOnlyUploadDataServices);
                    EmailUploadModalService.getEmailPopup($scope, 'sm', EMAIL_UPLOAD_CONSTANTS.ERROR_POPUP);

                }
                else if (response == "FileAlreadyUploaded" && response != "IncorrectTemplate") {
                    myApp.hidePleaseWait();
                    angular.element("#fileUploadModal").modal();

                }
                else if (response == "IncorrectTemplate") {
                    myApp.hidePleaseWait();
                    angular.element("#incorrectTemplateUploadModal").modal();
                    //Clear the file input control
                    CommonUploadService.clearFileInput();
                }
                else if (response == "EmptyFileUploaded") {
                    myApp.hidePleaseWait();
                    angular.element("#emptyUploadModal").modal();
                    //Clear the file input control
                    CommonUploadService.clearFileInput();
                }
                else {
                    myApp.hidePleaseWait(); //$scope.pleaseWait = { "display": "none" };
                    console.log("Failed");
                }
            }).error(function (result) {
                myApp.hidePleaseWait();

                //Close all the modals
                $uibModalStack.dismissAll();

                //Clear the file input control
                CommonUploadService.clearFileInput();

                if (result == UPLOAD_CRUD_CONSTANTS.ACCESS_DENIED) {
                    angular.element("#accessDeniedModal").modal();
                }
                else if (result == UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    CommonUploadService.messageUploadPopup(UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == UPLOAD_CRUD_CONSTANTS.DB_ERROR) {
                    CommonUploadService.messageUploadPopup(UPLOAD_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        }
    }
}]);

angular.module('upload').controller("EmailUploadValidationCtrl",
    ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'EmailOnlyUploadDataServices',
        '$rootScope', '$localStorage', 'EmailUploadModalService','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, EmailOnlyUploadDataServices, $rootScope, $localStorage, EmailUploadModalService, CommonUploadService) {

    var columnDefs = EmailOnlyUploadDataServices.getEmailUploadValidationResultsColumnDef();

    console.log("Column Defs " + columnDefs);

    var rows;

    $scope.emailUpload = {
        totalItems: 0,
        currentPage: 1,
        totalItems: 0,
        errorCountLessThan100: true,
        gridOptions: EmailOnlyUploadDataServices.getEmailUploadValidationGridOptions(uiGridConstants, columnDefs),
        results: []
    }

    $scope.emailUpload.gridOptions.onRegisterApi = function (grid) {
        $scope.emailUpload.gridApi = grid;
    }

    $scope.emailUpload.results = EmailOnlyUploadDataServices.getEmailUploadResult();

    $scope.emailUpload.totalItems = EmailOnlyUploadDataServices.getInvalidRecordsCount();

    if ($scope.emailUpload.totalItems >= 100) {
        $scope.emailUpload.errorCountLessThan100 = false;
    }
    else {
        $scope.emailUpload.errorCountLessThan100 = true;
    }

    $scope.emailUpload.gridOptions = CommonUploadService.getUploadValidationGridLayout($scope.emailUpload.gridOptions, uiGridConstants, $scope.emailUpload.results.EmailOnlyUploadInputList);

    $scope.emailUpload.pageChanged = function (page) {
        $scope.emailUpload.gridApi.pagination.seek(page);
    };

    $scope.emailUpload.back = function () {
        $uibModalInstance.dismiss('cancel');
    }

}]);

angular.module('upload').controller("UserEmailUploadConfirmCtrl",
    ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants','CommonUploadService',
        'EmailOnlyUploadDataServices', '$rootScope', '$localStorage', '$filter', '$uibModalStack', 'EmailUploadModalService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants,CommonUploadService,
    EmailOnlyUploadDataServices, $rootScope, $localStorage, $filter, $uibModalStack, EmailUploadModalService) {

    $scope.emailUpload = {
        uploadedFileMessage: null
    }

    $scope.emailUpload.uploadedFileMessage = EmailOnlyUploadDataServices.getEmailUploadedFileMessage();

    $scope.emailUpload.back = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    }

}]);

angular.module('upload').controller("UserEmailUploadErrorCtrl",
    ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'UploadDataServices', '$rootScope', '$localStorage', '$filter',
        'uploadDropDownService', '$uibModalStack', 'EmailUploadModalService','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, UploadDataServices, $rootScope,
    $localStorage, $filter, uploadDropDownService, $uibModalStack, EmailUploadModalService, CommonUploadService) {

    $scope.groupmembership = {
        uploadedFileMessage: null
    }

    $scope.groupmembership.uploadedFileMessage = UploadDataServices.setEmailUploadedFileMessage();

    $scope.groupmembership.back = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    }

}]);

angular.module('upload').controller("UserEmailUploadDataCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants','CommonUploadService',
    'EmailOnlyUploadDataServices', '$rootScope', '$localStorage', '$filter', 'emailUploadDropDownService', '$uibModalStack', 'EmailUploadModalService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants,CommonUploadService, EmailOnlyUploadDataServices, $rootScope, $localStorage, $filter,
    emailUploadDropDownService, $uibModalStack, EmailUploadModalService) {

    var groupName = null;

    $scope.emailUpload = {
        showCloseButton: true,
        chapterCodes: [],
        chapterCode: "",
        groupCodes: [],
        groupCode: "",
        groupName: "",
        createdBy: "",
        startDate: "",
        endDate: new Date(9999, 11, 31), //Javascript date object uses 0 indexed months
        createdDate: "",
        dateOptions: {
            formatYear: 'yy',
            maxDate: new Date(9999, 11, 31),
            minDate: new Date(2000, 0, 1),
            minDate: new Date(),
            startingDay: 1
        },
        toggleInvalidInput: false,
        invalidInput: "** Group Membership End Effective Date cannot be less than the start effective date"
    }

    $scope.emailUpload.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.emailUpload.format = $scope.emailUpload.formats[0];

    $scope.emailUpload.openEndDate = function () {
        $scope.emailUpload.popup3.opened = true;
    };
    $scope.emailUpload.popup3 = {
        opened: false
    };

    $scope.emailUpload.openStartDate = function () {
        $scope.emailUpload.popup2.opened = true;
    };
    $scope.emailUpload.popup2 = {
        opened: false
    };

    $scope.emailUpload.openCreatedDate = function () {
        $scope.emailUpload.popup1.opened = true;
    };
    $scope.emailUpload.popup1 = {
        opened: false
    };

    emailUploadDropDownService.getDropDown(UPLOAD_CONSTANTS.UPLOAD_CHAPTERCODE).success(function (result) {
        $scope.emailUpload.chapterCodes = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    emailUploadDropDownService.getDropDown(UPLOAD_CONSTANTS.UPLOAD_GROUPCODE).success(function (result) {
        $scope.emailUpload.groupCodes = result;
        $scope.names = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    $scope.emailUpload.UploadFilter = function (item) {
        //var matcher = item.value.match(/[^\_0-9](.?)+$/);
        var matcher = item.value.match(/([a-zA-Z])+\w/);
        return matcher;
    }

    //added by Vinoth
  /*  $scope.emailUpload.GroupNameTxtbox = function () {
        $scope.emailUpload.groupNamedropdown = true;
    }
    $scope.emailUpload.Getgroupname = function (SelectedDataid, SelectedDatavalue) {
        //console.log(SelectedDataid);
        //console.log(SelectedDatavalue);
        $scope.emailUpload.groupName = SelectedDatavalue;
        $scope.emailUpload.groupNamedropdown = false;
        $scope.emailUpload.groupCode = SelectedDataid;

    }

    $scope.emailUpload.populateGroupCode = function (item) {
        $scope.emailUpload.groupCodes.forEach(function (index) {
            if (index.value == item) {
                $scope.emailUpload.groupCode = index.id;
            }
        });
    };*/

    $scope.emailUpload.compareAgainstStartDate = function (item) {
        var startDate = new Date($filter('date')(new Date($scope.emailUpload.startDate), 'MM/dd/yyyy'));
        var endDate = new Date($filter('date')(new Date($scope.emailUpload.endDate), 'MM/dd/yyyy'));




        if (endDate < startDate) {
            $scope.emailUpload.toggleInvalidInput = true;
        }
        else
            $scope.emailUpload.toggleInvalidInput = false;

        return $scope.emailUpload.toggleInvalidInput;
    };

    $scope.emailUpload.closeEmailUploadUserInputModal = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    };

    var loggedInUserName = EmailOnlyUploadDataServices.getUserName();

    $scope.emailUpload.submit = function () {

        if ($scope.emailUploadUserInputForm.$valid) {
            myApp.showPleaseWait();

            $scope.emailUserUploadParams = {
                "loggedInUserName": EmailOnlyUploadDataServices.getUserName(),
                "chapterCode": $scope.emailUpload.chapterCode.id.split(' - ')[0],
                "groupCode": $scope.emailUpload.groupSelected.id,
                "groupName": $scope.emailUpload.groupSelected.value,
                "endDate": $filter('date')(new Date($scope.emailUpload.endDate), 'MM/dd/yyyy'),
                "startDate": $filter('date')(new Date($scope.emailUpload.startDate), 'MM/dd/yyyy')
            }

            EmailOnlyUploadDataServices.getEmailUserUploadParams($scope.emailUserUploadParams).success(function (result) {
                //Close the modal first
                $uibModalInstance.dismiss('cancel');
                //Fire the validate event
                $rootScope.$emit('validateEmailUploadedFilesEvent', {});
            });

        }
    };

    $scope.emailUpload.cancel = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    };

}]);

