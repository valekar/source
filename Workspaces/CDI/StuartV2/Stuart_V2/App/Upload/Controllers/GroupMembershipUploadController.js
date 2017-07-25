UploadModule.controller('GroupMembershipUploadController', ['$scope', '$location', '$log', 'CommonUploadService', 'GroupMembershipUploadService',
    '$window', '$localStorage', 'mainService', '$state', '$rootScope', '$http', '$uibModal', '$uibModalStack', 'uiGridConstants',
    'UploadDataServices', 'UploadServices', '$timeout','GROUP_UPLOAD_CONSTANTS',
function ($scope, $location, $log, CommonUploadService, GroupMembershipUploadService, $window, $localStorage, mainService,
    $state, $rootScope, $http, $uibModal, $uibModalStack, uiGridConstants, UploadDataServices, UploadServices, $timeout, GROUP_UPLOAD_CONSTANTS) {

    var username = "";

    var BasePath = $("base").first().attr("href");

    $scope.uploadTemplateDownload = BasePath + "App/Upload/Files/GroupMembershipUploadTemplate.xlsx";

    var initialize = function(){
        $scope.pleaseWait = { "display": "none" };

        mainService.getUsername().then(function (result) {
            username = result.data;
            UploadDataServices.setUserName(username);
        });
    };

    initialize();

    $scope.GroupMembershipUpload = function () {
        $scope.pleaseWait = { "display": "none" };
    }

    $scope.file = null;

    //UPLOAD FILE CODE

    var formdata = null;

    $scope.getTheFiles = function ($files) {
        var result = document.getElementsByClassName("file-input-label");
        angular.element(result).text($files[0].name);

        formdata = new FormData();
        UploadDataServices.setUploadFormData(formdata);
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });

        $scope.getUserUploadData();
    };

    //GET THE USER UPLOAD DATA

    $scope.getUserUploadData = function () {
       // getUserUploadDataPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, UploadDataServices);
        GroupMembershipUploadService.getGroupPopup($scope, 'lg', GROUP_UPLOAD_CONSTANTS.DATA_POPUP);
    };

    //EVENT FIRED ON CLOSE OF THE MODAL 

    $scope.GroupMembershipUpload.similarFileUploadModalClose = function () {
        CommonUploadService.clearFileInput();
    }

    //SEND THE FILE DATA TO SERVER

    $scope.GroupMembershipUpload.fileUpload = function () {

        function pleaseWaitTimeoutPromise() {
            return $timeout(function () {
                return $uibModalStack.dismissAll();
            }, 1000);
        }
        
        $scope.pleaseWait = { "display": "block" };

        var uploadedFormData = UploadDataServices.getUploadResult();
        var str1 = "The file ";
        var uploadedFileMessage = str1.concat(uploadedFormData.UploadedFileInputInfo.fileName, " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.");        //var uploadedFileMessage ="The file has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.";

        pleaseWaitTimeoutPromise().then(function (result) {
            $scope.pleaseWait = { "display": "none" };
            UploadDataServices.setUploadedFileMessage(uploadedFileMessage);
            //getUploadConfirmationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, UploadDataServices);
            GroupMembershipUploadService.getGroupPopup($scope, 'sm', GROUP_UPLOAD_CONSTANTS.CONFIRMATION_POPUP);
        });

        CommonUploadService.clearFileInput();
        
        UploadServices.postGroupMembershipUploadData(uploadedFormData, username).
            success(function (result) {
                console.log("File Data sent to Server");
                console.log(result);
            });
    };

    // NOW VALIDATE THE UPLOADED THE FILES

    $rootScope.$on('validateUploadedFilesEvent', function (event, arg) {
        $scope.validateFiles();
    });

    $scope.validateFiles = function () {

        myApp.showPleaseWait();//$scope.pleaseWait = { "display": "block" };

        var request = {
            method: 'POST',
            url: BasePath + 'uploadNative/ValidateFiles/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        };

        // SEND THE FILES

        if (formdata != null || formdata != undefined) {
            $http(request).success(function (response) {
                if (response != "Failed!" && response != "FileAlreadyUploaded" && response != "IncorrectTemplate" && response.GroupMembershipInvalidList != null && response.GroupMembershipValidList != null)
                {

                    if (response.GroupMembershipInvalidList.GroupMembershipUploadInputList.length == 0 &&
                        response.GroupMembershipValidList.GroupMembershipUploadInputList.length > 0) {

                        console.log("Succeeded validation");

                        UploadDataServices.setUploadResult(response.GroupMembershipValidList);
                        myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };                        

                        $uibModalStack.dismissAll();

                        //Clear the file input control
                        CommonUploadService.clearFileInput();

                        angular.element("#fileUploadSuccessModal").modal();

                    }
                    else if (response.GroupMembershipInvalidList.GroupMembershipUploadInputList.length > 0) {
                        myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };

                        console.log("Failed at validation");

                        //Clear the file input control
                        CommonUploadService.clearFileInput();

                        UploadDataServices.setUploadValidationResult(response);
                        UploadDataServices.setUploadResult(response.GroupMembershipInvalidList);

                        UploadDataServices.setInvalidRecordsCount(response.GroupMembershipInvalidList.GroupMembershipUploadInputList.length);

                        //getUploadValidationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, UploadDataServices);
                        GroupMembershipUploadService.getGroupPopup($scope, 'lg', GROUP_UPLOAD_CONSTANTS.VALIDATION_POPUP);
                    }
                }
                else if (response.includes("FileLimitExceeded") && response != "FileAlreadyUploaded" && response != "IncorrectTemplate")
                {
                    myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };

                    $uibModalStack.dismissAll();

                    var str1 = "The uploaded file ";
                    var uploadedFileMessage = str1.concat(response.split("||")[2], " has ", response.split("||")[1], " rows. The upload feature only supports 2000 or fewer rows. Please split the data file and retry the upload.");

                    UploadDataServices.setUploadedFileMessage(uploadedFileMessage);

                    //getUploadErrorPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, UploadDataServices);
                    GroupMembershipUploadService.getGroupPopup($scope, 'lg', GROUP_UPLOAD_CONSTANTS.ERROR_POPUP);

                }
                else if (response == "FileAlreadyUploaded" && response != "IncorrectTemplate") {
                    myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };
                    angular.element("#fileUploadModal").modal();

                }
                else if (response == "IncorrectTemplate") {
                    myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };
                    angular.element("#incorrectTemplateUploadModal").modal();
                }
                else if (response == "EmptyFileUploaded") {
                    myApp.hidePleaseWait();
                    angular.element("#emptyUploadModal").modal();
                }
                else {
                    myApp.hidePleaseWait(); //$scope.pleaseWait = { "display": "none" };
                    console.log("Failed");
                }
            }).error(function (result) {
                myApp.hidePleaseWait(); //$scope.pleaseWait = { "display": "none" };

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

UploadModule.controller("UploadValidationCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants',
    'UploadDataServices', '$rootScope', '$localStorage','GroupMembershipUploadService','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, UploadDataServices, $rootScope, $localStorage, GroupMembershipUploadService, CommonUploadService) {

    var columnDefs = UploadDataServices.getUploadValidationResultsColumnDef();

    console.log("Column Defs " + columnDefs);

    var rows;

    $scope.upload = {
        totalItems:0,
        currentPage: 1,
        totalItems: 0,
        errorCountLessThan100:true,
        gridOptions: UploadDataServices.getUploadValidationGridOptions(uiGridConstants, columnDefs),
        results: []
    }

    $scope.upload.gridOptions.onRegisterApi = function (grid) {
        $scope.upload.gridApi = grid;
    }

    $scope.upload.results = UploadDataServices.getUploadResult();

    $scope.upload.totalItems = UploadDataServices.getInvalidRecordsCount();

    if ($scope.upload.totalItems >= 100)
    {
        $scope.upload.errorCountLessThan100 = false;
    }
    else {
        $scope.upload.errorCountLessThan100 = true;
    }

    $scope.upload.gridOptions = CommonUploadService.
                                getUploadValidationGridLayout($scope.upload.gridOptions, uiGridConstants, $scope.upload.results.GroupMembershipUploadInputList);

    $scope.upload.pageChanged = function (page) {
        $scope.upload.gridApi.pagination.seek(page);
    };

    $scope.upload.back = function () {
        $uibModalInstance.dismiss('cancel');
    }

}]);

UploadModule.controller("UserUploadConfirmCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants','CommonUploadService',
    'UploadDataServices', '$rootScope', '$localStorage', '$filter', 'uploadDropDownService', '$uibModalStack','GroupMembershipUploadService',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, CommonUploadService, UploadDataServices, $rootScope,
    $localStorage, $filter, uploadDropDownService, $uibModalStack, GroupMembershipUploadService) {

    $scope.groupmembership = {
        uploadedFileMessage : null
    }

    $scope.groupmembership.uploadedFileMessage = UploadDataServices.getUploadedFileMessage();

    $scope.groupmembership.back = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    }

}]);

UploadModule.controller("UserUploadErrorCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance','GroupMembershipUploadService',
    'uiGridConstants', 'UploadDataServices', '$rootScope', '$localStorage', '$filter', 'uploadDropDownService', '$uibModalStack','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance,GroupMembershipUploadService,
    uiGridConstants, UploadDataServices, $rootScope, $localStorage, $filter, uploadDropDownService, $uibModalStack, CommonUploadService) {

    $scope.groupmembership = {
        uploadedFileMessage: null
    }

    $scope.groupmembership.uploadedFileMessage = UploadDataServices.getUploadedFileMessage();

    $scope.groupmembership.back = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    }

}]);

UploadModule.controller("UserUploadDataCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance','GroupMembershipUploadService',
    'uiGridConstants', 'UploadDataServices', '$rootScope', '$localStorage', '$filter', 'uploadDropDownService', '$uibModalStack','CommonUploadService',
function ($scope, $state, $uibModal, $uibModalInstance,GroupMembershipUploadService,
    uiGridConstants, UploadDataServices, $rootScope, $localStorage, $filter, uploadDropDownService, $uibModalStack, CommonUploadService) {

    var groupName = null;

    $scope.groupmembership = {
        showCloseButton:true,
        chapterCodes: [],
        chapterCode: "",
        groupCodes: [],
        groupCode: "",
        groupName: "",
        createdBy: "",
        startDate: "",
        endDate: new Date(9999,11,31), //Javascript date object uses 0 indexed months
        createdDate: "",
        dateOptions: {
            formatYear: 'yy',
            maxDate: new Date(9999,11, 31),
            minDate: new Date(2000, 0, 1),
            minDate: new Date(),
            startingDay: 1
        },
        toggleInvalidInput: false,
        invalidInput : "** Group Membership End Effective Date cannot be less than the start effective date"
    }

    $scope.groupmembership.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.groupmembership.format = $scope.groupmembership.formats[0];

    $scope.groupmembership.openEndDate = function () {
        $scope.groupmembership.popup3.opened = true;
    };
    $scope.groupmembership.popup3 = {
        opened: false
    };

    $scope.groupmembership.openStartDate = function () {
        $scope.groupmembership.popup2.opened = true;
    };
    $scope.groupmembership.popup2 = {
        opened: false
    };

    $scope.groupmembership.openCreatedDate = function () {
        $scope.groupmembership.popup1.opened = true;
    };
    $scope.groupmembership.popup1 = {
        opened: false
    };

    uploadDropDownService.getDropDown(UPLOAD_CONSTANTS.UPLOAD_CHAPTERCODE).success(function (result) {
        $scope.groupmembership.chapterCodes = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    uploadDropDownService.getDropDown(UPLOAD_CONSTANTS.UPLOAD_GROUPCODE).success(function (result) {
        $scope.groupmembership.groupCodes = result;
        $scope.names = result;
        
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });


    $scope.groupmembership.UploadFilter = function (item) {
        //var matcher = item.value.match(/[^\_0-9](.?)+$/);
        var matcher = item.value.match(/([a-zA-Z])+\w/);
        return matcher;
    }


    // added by vinoth
   /* $scope.groupmembership.GroupNameTxtbox = function ()
    {
        $scope.groupmembership.groupNamedropdown = true;
    }
   
    $scope.groupmembership.Getgroupname = function (SelectedDataid, SelectedDatavalue) {
        //console.log("asdasd");
        //console.log(SelectedDatavalue);
        $scope.groupmembership.groupName = SelectedDatavalue;
        $scope.groupmembership.groupNamedropdown = false;
        $scope.groupmembership.groupCode = SelectedDataid;      
        
    }
    //end of adding 
    $scope.groupmembership.populateGroupCode = function (item) {        
                $scope.groupmembership.groupCodes.forEach(function (index) {
            if (index.value == item) {
             
                $scope.groupmembership.groupCode = index.id;
            }
        });
    };*/

    $scope.groupmembership.compareAgainstStartDate = function (item) {
        var startDate = new Date ($filter('date')(new Date($scope.groupmembership.startDate), 'MM/dd/yyyy'));
        var endDate = new Date($filter('date')(new Date($scope.groupmembership.endDate), 'MM/dd/yyyy'));

        if (endDate < startDate) {
            $scope.groupmembership.toggleInvalidInput = true;
        }
        else
            $scope.groupmembership.toggleInvalidInput = false;

        return $scope.groupmembership.toggleInvalidInput;
    };

    $scope.groupmembership.closeGroupMembershipUserInputModal = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    };

    var loggedInUserName = UploadDataServices.getUserName();

    $scope.GroupMembershipUploadDateRegex = /^((\d{2})\/(\d{2})\/(\d{4}))$/;

    $scope.groupmembership.submit = function () {

        if ($scope.groupMembershipUserInputForm.$valid) {
            myApp.showPleaseWait();//$scope.pleaseWait = { "display": "block" };

            $scope.groupmembershipUploadParams = {
                "loggedInUserName": UploadDataServices.getUserName(),
                "chapterCode": $scope.groupmembership.chapterCode.id.split(' - ')[0],
                "groupCode": $scope.groupmembership.groupSelected.id,
                "groupName": $scope.groupmembership.groupSelected.value,
                "createdBy": $scope.groupmembership.createdBy,
                "createdDate": $filter('date')(new Date($scope.groupmembership.createdDate), 'MM/dd/yyyy'),
                "endDate": $filter('date')(new Date($scope.groupmembership.endDate), 'MM/dd/yyyy'),
                "startDate": $filter('date')(new Date($scope.groupmembership.startDate), 'MM/dd/yyyy')
            }

            UploadDataServices.getGroupMembershipUploadParams($scope.groupmembershipUploadParams).success(function (result) {
                //Close the modal first
                $uibModalInstance.dismiss('cancel');
                //Fire the validate event
                $rootScope.$emit('validateUploadedFilesEvent', {});
            });

        }
    };

    $scope.groupmembership.cancel = function () {
        $uibModalInstance.dismiss('cancel');
        CommonUploadService.clearFileInput();
    };

}]);

