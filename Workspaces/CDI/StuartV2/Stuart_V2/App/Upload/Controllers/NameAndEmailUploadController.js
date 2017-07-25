angular.module('upload').controller('NameAndEmailUploadController',['$scope', '$location', '$log', 'NameEmailUploadModalService',
    '$window', '$localStorage', 'mainService', '$state', '$rootScope', '$http', '$uibModal', '$uibModalStack', 'uiGridConstants',
    'NameAndEmailUploadDataServices', 'NameAndEmailUploadServices', '$timeout','NAME_AND_EMAIL_UPLOAD_CONSTANTS','CommonUploadService',
function ($scope, $location, $log, NameEmailUploadModalService, $window, $localStorage, mainService, $state, $rootScope,
    $http, $uibModal, $uibModalStack, uiGridConstants, NameAndEmailUploadDataServices, NameAndEmailUploadServices, $timeout, NAME_AND_EMAIL_UPLOAD_CONSTANTS, CommonUploadService) {

    var username = "";

    var BasePath = $("base").first().attr("href");

    $scope.nameAndEmailUploadTemplateDownload = BasePath + "App/Upload/Files/NameAndEmailUploadTemplate.xlsx";

    var initialize = function(){
        $scope.pleaseWait = { "display": "none" };

        mainService.getUsername().then(function (result) {
            username = result.data;
            NameAndEmailUploadDataServices.setUserName(username);
        });
    };

    initialize();

    $scope.NameAndEmailUpload = function () {
        $scope.pleaseWait = { "display": "none" };
    }

    $scope.file = null;

    //UPLOAD FILE CODE

    var formdata = null;
    //debugger;
    $scope.getNameAndEmailFiles = function ($files) {
        var result = document.getElementsByClassName("file-input-label");
        angular.element(result).text($files[0].name);

        formdata = new FormData();
        NameAndEmailUploadDataServices.setNameAndEmailUploadFormData(formdata);
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });

        $scope.getNameAndEmailUserUploadData();
    };

    //GET THE USER UPLOAD DATA
    
    $scope.getNameAndEmailUserUploadData = function () {
       // getNameAndEmailUserUploadDataPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, NameAndEmailUploadDataServices);
        NameEmailUploadModalService.getModalPopup($scope, 'lg', NAME_AND_EMAIL_UPLOAD_CONSTANTS.DATA_POPUP);
    };

    //EVENT FIRED ON CLOSE OF THE MODAL 
   
    $scope.NameAndEmailUpload.similarFileUploadModalClose = function () {
        CommonUploadService.clearFileInput();
    }

    //SEND THE FILE DATA TO SERVER
   
    $scope.NameAndEmailUpload.fileUpload = function () {

        function pleaseWaitTimeoutPromise() {
            return $timeout(function () {
                return $uibModalStack.dismissAll();
            }, 1000);
        }

        $scope.pleaseWait = { "display": "block" };

        var uploadedFormData = NameAndEmailUploadDataServices.getNameAndEmailUploadResult();
        var str1 = "The file ";
        var uploadedFileMessage = str1.concat(uploadedFormData.UploadedNameAndEmailFileInputInfo.fileName, " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.");        //var uploadedFileMessage ="The file has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.";

        pleaseWaitTimeoutPromise().then(function (result) {
            $scope.pleaseWait = { "display": "none" };
            NameAndEmailUploadDataServices.setNameAndEmailUploadedFileMessage(uploadedFileMessage);
            //getNameAndEmailUploadConfirmationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, NameAndEmailUploadDataServices);
            NameEmailUploadModalService.getModalPopup($scope, 'sm', NAME_AND_EMAIL_UPLOAD_CONSTANTS.CONFIRMATION_POPUP);
        });

        CommonUploadService.clearFileInput();

        NameAndEmailUploadServices.postNameAndEmailUploadData(uploadedFormData, username).
            success(function (result) {
                console.log("File Data sent to Server");
                console.log(result);
            });
    };

    // NOW VALIDATE THE UPLOADED THE FILES

    $rootScope.$on('validateNameAndEmailUploadedFilesEvent', function (event, arg) {
        $scope.validateNameAndEmailUploadFiles();
    });

    $scope.validateNameAndEmailUploadFiles = function () {

        myApp.showPleaseWait();

        var request = {
            method: 'POST',
            url: BasePath + 'NameAndEmailNative/ValidateNameAndEmailUploadFiles/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        };

        // SEND THE FILES      
        if (formdata != null || formdata != undefined) {
            $http(request).success(function (response) {
                if (response != "Failed!" && response != "FileAlreadyUploaded" && response != "IncorrectTemplate" && response.NameAndEmailUploadInvalidList != null && response.NameAndEmailUploadValidList != null) {

                    if (response.NameAndEmailUploadInvalidList.NameAndEmailUploadInputList.length == 0 &&
                        response.NameAndEmailUploadValidList.NameAndEmailUploadInputList.length > 0) {

                        console.log("Succeeded validation");

                        NameAndEmailUploadDataServices.setNameAndEmailUploadResult(response.NameAndEmailUploadValidList);
                        myApp.hidePleaseWait();                       

                        $uibModalStack.dismissAll();

                        //Clear the file input control
                        CommonUploadService.clearFileInput();

                        angular.element("#fileUploadSuccessModal").modal();

                    }
                    else if (response.NameAndEmailUploadInvalidList.NameAndEmailUploadInputList.length > 0) {
                        myApp.hidePleaseWait();

                        console.log("Failed at validation");

                        //Clear the file input control
                        CommonUploadService.clearFileInput();

                        NameAndEmailUploadDataServices.setNameAndEmailUploadValidationResult(response);
                        NameAndEmailUploadDataServices.setNameAndEmailUploadResult(response.NameAndEmailUploadInvalidList);

                        NameAndEmailUploadDataServices.setInvalidRecordsCount(response.NameAndEmailUploadInvalidList.NameAndEmailUploadInputList.length);

                        //getUploadValidationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, NameAndEmailUploadDataServices);
                        NameEmailUploadModalService.getModalPopup($scope, 'lg', NAME_AND_EMAIL_UPLOAD_CONSTANTS.VALIDATION_POPUP);
                    }
                }
                else if (response.includes("FileLimitExceeded") && response != "FileAlreadyUploaded" && response != "IncorrectTemplate") {
                    myApp.hidePleaseWait();

                    $uibModalStack.dismissAll();

                    var str1 = "The uploaded file ";
                    var uploadedFileMessage = str1.concat(response.split("||")[2], " has ", response.split("||")[1], " rows. The upload feature only supports 2000 or fewer rows. Please split the data file and retry the upload.");

                    NameAndEmailUploadDataServices.setNameAndEmailUploadedFileMessage(uploadedFileMessage);

                   // getUploadErrorPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, NameAndEmailUploadDataServices);
                    NameEmailUploadModalService.getModalPopup($scope, 'sm', NAME_AND_EMAIL_UPLOAD_CONSTANTS.ERROR_POPUP);

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
                    CommonUploadService.messageUploadPopup($rootScope, UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == UPLOAD_CRUD_CONSTANTS.DB_ERROR) {
                    CommonUploadService.messageUploadPopup($rootScope, UPLOAD_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        }
    }
}]);

angular.module('upload').controller("NameAndEmailUploadValidationCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance',
    'uiGridConstants', 'NameAndEmailUploadDataServices', '$rootScope', '$localStorage','NameEmailUploadModalService','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, NameAndEmailUploadDataServices, $rootScope, $localStorage, NameEmailUploadModalService, CommonUploadService) {

    var columnDefs = NameAndEmailUploadDataServices.getNameAndEmailUploadValidationResultsColumnDef();

    console.log("Column Defs " + columnDefs);

    var rows;

    $scope.nameAndEmailUpload = {
        totalItems: 0,
        currentPage: 1,
        totalItems: 0,
        errorCountLessThan100: true,
        gridOptions: NameAndEmailUploadDataServices.getNameAndEmailUploadValidationGridOptions(uiGridConstants, columnDefs),
        results: []
    }

    $scope.nameAndEmailUpload.gridOptions.onRegisterApi = function (grid) {
        $scope.nameAndEmailUpload.gridApi = grid;
    }

    $scope.nameAndEmailUpload.results = NameAndEmailUploadDataServices.getNameAndEmailUploadResult();

    $scope.nameAndEmailUpload.totalItems = NameAndEmailUploadDataServices.getInvalidRecordsCount();

    if ($scope.nameAndEmailUpload.totalItems >= 100) {
        $scope.nameAndEmailUpload.errorCountLessThan100 = false;
    }
    else {
        $scope.nameAndEmailUpload.errorCountLessThan100 = true;
    }

    $scope.nameAndEmailUpload.gridOptions = CommonUploadService.
        getUploadValidationGridLayout($scope.nameAndEmailUpload.gridOptions, uiGridConstants, $scope.nameAndEmailUpload.results.NameAndEmailUploadInputList);

    $scope.nameAndEmailUpload.pageChanged = function (page) {
        $scope.nameAndEmailUpload.gridApi.pagination.seek(page);
    };

    $scope.nameAndEmailUpload.back = function () {
        $uibModalInstance.dismiss('cancel');
    }

}]);

angular.module('upload').controller("UserNameAndEmailUploadConfirmCtrl",
    ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'NameAndEmailUploadDataServices','CommonUploadService',
        '$rootScope', '$localStorage', '$filter', '$uibModalStack','NameEmailUploadModalService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, NameAndEmailUploadDataServices,CommonUploadService,
    $rootScope, $localStorage, $filter, $uibModalStack, NameEmailUploadModalService) {

    $scope.nameAndEmailUpload = {
        uploadedFileMessage: null
    }

    $scope.nameAndEmailUpload.uploadedFileMessage = NameAndEmailUploadDataServices.getNameAndEmailUploadedFileMessage();

    $scope.nameAndEmailUpload.back = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    }

}]);

angular.module('upload').controller("UserNameAndEmailUploadErrorCtrl", ['$scope', '$state', '$uibModal',
    '$uibModalInstance', 'uiGridConstants', 'UploadDataServices', '$rootScope', '$localStorage', '$filter',
    'uploadDropDownService', '$uibModalStack','NameEmailUploadModalService','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, UploadDataServices, $rootScope, $localStorage,
    $filter, uploadDropDownService, $uibModalStack, NameEmailUploadModalService, CommonUploadService) {

    $scope.groupmembership = {
        uploadedFileMessage: null
    }

    $scope.groupmembership.uploadedFileMessage = UploadDataServices.setEmailUploadedFileMessage();

    $scope.groupmembership.back = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    }

}]);

angular.module('upload').controller("UserNameAndEmailUploadDataCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance','NameEmailUploadModalService',
    'uiGridConstants', 'NameAndEmailUploadDataServices', '$rootScope', '$localStorage', '$filter', 'NameAndEmailUploadDropDownService', '$uibModalStack','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, NameEmailUploadModalService, uiGridConstants, NameAndEmailUploadDataServices,
    $rootScope, $localStorage, $filter, NameAndEmailUploadDropDownService, $uibModalStack, CommonUploadService) {

    var groupName = null;

    $scope.nameAndEmailUpload = {
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
        invalidInput: "** Name and Email End Effective Date cannot be less than the start effective date"
    }

    $scope.nameAndEmailUpload.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.nameAndEmailUpload.format = $scope.nameAndEmailUpload.formats[0];

    $scope.nameAndEmailUpload.openEndDate = function () {
        $scope.nameAndEmailUpload.popup3.opened = true;
    };
    $scope.nameAndEmailUpload.popup3 = {
        opened: false
    };

    $scope.nameAndEmailUpload.openStartDate = function () {
        $scope.nameAndEmailUpload.popup2.opened = true;
    };
    $scope.nameAndEmailUpload.popup2 = {
        opened: false
    };

    $scope.nameAndEmailUpload.openCreatedDate = function () {
        $scope.nameAndEmailUpload.popup1.opened = true;
    };
    $scope.nameAndEmailUpload.popup1 = {
        opened: false
    };

    NameAndEmailUploadDropDownService.getDropDown(NAME_AND_EMAIL_UPLOAD_CONSTANTS.UPLOAD_CHAPTERCODE).success(function (result) {
        $scope.nameAndEmailUpload.chapterCodes = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    NameAndEmailUploadDropDownService.getDropDown(NAME_AND_EMAIL_UPLOAD_CONSTANTS.UPLOAD_GROUPCODE).success(function (result) {
        $scope.nameAndEmailUpload.groupCodes = result;
        $scope.names = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });


    $scope.nameAndEmailUpload.UploadFilter = function (item) {
        //var matcher = item.value.match(/[^\_0-9](.?)+$/);
        var matcher = item.value.match(/([a-zA-Z])+\w/);
        return matcher;
    }

    // added by vinoth
   /* $scope.nameAndEmailUpload.GroupNameTxtbox = function () {
        $scope.nameAndEmailUpload.groupNamedropdown = true;
    }
    $scope.nameAndEmailUpload.Getgroupname = function (SelectedDataid, SelectedDatavalue) {
        //console.log(SelectedDataid);
        //console.log(SelectedDatavalue);
        $scope.nameAndEmailUpload.groupName = SelectedDatavalue;
        $scope.nameAndEmailUpload.groupNamedropdown = false;
        $scope.nameAndEmailUpload.groupCode = SelectedDataid;

    }


    $scope.nameAndEmailUpload.populateGroupCode = function (item) {
        $scope.nameAndEmailUpload.groupCodes.forEach(function (index) {
            if (index.value == item) {
                $scope.nameAndEmailUpload.groupCode = index.id;
            }
        });
    };*/

    $scope.nameAndEmailUpload.compareAgainstStartDate = function (item) {
        var startDate = new Date($filter('date')(new Date($scope.nameAndEmailUpload.startDate), 'MM/dd/yyyy'));
        var endDate = new Date($filter('date')(new Date($scope.nameAndEmailUpload.endDate), 'MM/dd/yyyy'));

        if (endDate < startDate) {
            $scope.nameAndEmailUpload.toggleInvalidInput = true;
        }
        else
            $scope.nameAndEmailUpload.toggleInvalidInput = false;

        return $scope.nameAndEmailUpload.toggleInvalidInput;
    };

    $scope.nameAndEmailUpload.closeNameAndEmailUploadUserInputModal = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    };

    var loggedInUserName = NameAndEmailUploadDataServices.getUserName();
   // debugger;
    $scope.nameAndEmailUpload.submit = function () {

        if ($scope.nameAndEmailUploadUserInputForm.$valid) {
            myApp.showPleaseWait();

            $scope.nameAndEmailUserUploadParams = {
                "loggedInUserName": NameAndEmailUploadDataServices.getUserName(),
                "chapterCode": $scope.nameAndEmailUpload.chapterCode.id.split(' - ')[0],
                "groupCode": $scope.nameAndEmailUpload.groupSelected.id,
                "groupName": $scope.nameAndEmailUpload.groupSelected.value,
                "endDate": $filter('date')(new Date($scope.nameAndEmailUpload.endDate), 'MM/dd/yyyy'),
                "startDate": $filter('date')(new Date($scope.nameAndEmailUpload.startDate), 'MM/dd/yyyy')
            }

            NameAndEmailUploadDataServices.getNameAndEmailUserUploadParams($scope.nameAndEmailUserUploadParams).success(function (result) {
                //Close the modal first
                $uibModalInstance.dismiss('cancel');
                //Fire the validate event
                $rootScope.$emit('validateNameAndEmailUploadedFilesEvent', {});
            });

        }
    };

    $scope.nameAndEmailUpload.cancel = function () {
        $uibModalInstance.dismiss('cancel');
        //clearNameAndEmailFileInput();
        CommonUploadService.clearFileInput();
    };

}]);

