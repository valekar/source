angular.module('upload').controller('DNCUploadCtrl', ['$scope', 'mainService', 'DNCServices', 'DNC_CONSTANTS', 'DncDataServices',
    'DncColumnDefHelper', 'DNCServices', '$rootScope','$timeout','$uibModalStack',
    function ($scope, mainService, DNCServices, DNC_CONSTANTS, DncDataServices, DncColumnDefHelper, DNCServices, $rootScope, $timeout,$uibModalStack) {
 

    var initialize = function () {

        $scope.dncUpload = {
            togglePleaseWait: false,
            dncTemplateDownload: DNC_CONSTANTS.DNC_TEMPLATE_PATH,
            message: "",
            fileName: "",
            rowCount :""
        }
        mainService.getUsername().then(function (result) {
            username = result.data;
            DncDataServices.setUserName(username);
        });
    };
    //initialize the namespace


    initialize();

    //UPLOAD FILE CODE

    var formdata = null;

    $scope.dncUpload.getDNCFiles = function ($files) {
        var result = document.getElementsByClassName("file-input-label");
        angular.element(result).text($files[0].name);

        formdata = new FormData();
        DncDataServices.setDncUploadFormData(formdata);
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });

        $scope.dncUpload.getUserUploadData();
    };

    //GET THE USER UPLOAD DATA

    $scope.dncUpload.getUserUploadData = function () {
        DNCServices.getDNCPopup(DNC_CONSTANTS.DATA_POPUP).then(function () {

            console.log('after promise');
            $scope.validateRecords();
        });
    };


    $scope.dncUpload.similarFileUploadModalClose = function () {
        DNCServices.clearFileInput();
    }


    $scope.validateRecords = function () {
       
        console.log("validation");

        DncDataServices.validateDncUpload(formdata).then(function (result) {
            console.log(result)
            DNCServices.clearFileInput();
            myApp.hidePleaseWait();
            if (result != "Failed!" && result.data != "FileAlreadyUploaded" && result.data != "IncorrectTemplate" && 
                !angular.isUndefined(result.data.dncValidList) && !angular.isUndefined(result.data.dncInvalidList) &&
                    result.data.dncInvalidList.length == 0 && result.data.dncValidList.length > 0) {
                
                if (angular.isUndefined(result.data.message)) {
                    // myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };

                    DncDataServices.setUploadResult(result.data)

                    angular.element("#fileUploadSuccessModal").modal();
                    // return;
                }

                
               
            }
            else if (!angular.isUndefined(result.data.dncInvalidList) && result.data.dncInvalidList.length > 0) {
                // myApp.hidePleaseWait();

                DNCServices.getDNCPopup(DNC_CONSTANTS.VALIDATION_POPUP, result.data);
                return;
            }

            else if (result.data == "IncorrectTemplate") {
               // myApp.hidePleaseWait();//$scope.pleaseWait = { "display": "none" };
                angular.element("#incorrectTemplateUploadModal").modal();
                return;
            }
            else if (result.data == "EmptyFileUploaded") {
               // myApp.hidePleaseWait();
                angular.element("#emptyUploadModal").modal();
                return;
            }
            else if (result.data == "FileAlreadyUploaded") {
                angular.element("#fileAlreadyUploadedModal").modal();
            }
            else if (result.data.message == "FileLimitExceeded") {
                $scope.dncUpload.fileName = result.data.fileName;
                $scope.dncUpload.message = result.data.message;
                $scope.dncUpload.rowCount = result.data.rowCount;
                angular.element("#fileLimitExceedModal").modal();

            }

           
           
            myApp.hidePleaseWait();
        },
        function (error) {
            myApp.hidePleaseWait(); //$scope.pleaseWait = { "display": "none" };

            //Close all the modals
           // $uibModalStack.dismissAll();

            //Clear the file input control
            DNCServices.clearFileInput();

            if (error.data == UPLOAD_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (error.data == UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                DNCServices.messageUploadPopup(UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (error.data == UPLOAD_CRUD_CONSTANTS.DB_ERROR) {
                DNCServices.messageUploadPopup( UPLOAD_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    }

    //upload the results to service 
    $scope.dncUpload.fileUpload = function () {

        function pleaseWaitTimeoutPromise() {
            return $timeout(function () {
                return $uibModalStack.dismissAll();
            }, 1000);
        }

        $scope.dncUpload.togglePleaseWait = true;

        var dncUploadedFormData = DncDataServices.getUploadResult();
        var str1 = "The file ";
        var uploadedFileMessage = str1.concat(dncUploadedFormData.dncUploadedFileOutputInfo.fileName, " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.");      

        pleaseWaitTimeoutPromise().then(function (result) {
            $scope.dncUpload.togglePleaseWait = false;
            DncDataServices.setUploadedFileMessage(uploadedFileMessage);
            //getUploadConfirmationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, UploadDataServices);
            DNCServices.getDNCPopup(DNC_CONSTANTS.CONFIRMATION_POPUP);
        });

        DNCServices.clearFileInput();

        DncDataServices.postDncUploadData(dncUploadedFormData, username)
            .then(function (result) {
                console.log("File Data sent to Server");
                console.log(result);
            });
    };
}]);


angular.module('upload').controller("DNCUploadUserDataCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'DNC_CONSTANTS',
    'uiGridConstants', 'DncDataServices', '$rootScope', '$localStorage', '$filter', 'DNCDropDownService', '$uibModalStack', 'DNCServices',
function ($scope, $state, $uibModal, $uibModalInstance, DNC_CONSTANTS,
    uiGridConstants, DncDataServices, $rootScope, $localStorage, $filter, DNCDropDownService, $uibModalStack, DNCServices) {

    var groupName = null;

    $scope.dncUpload = {
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
        invalidInput: "** DNC End Effective Date cannot be less than the start effective date"
    }

    $scope.dncUpload.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.dncUpload.format = $scope.dncUpload.formats[0];

    $scope.dncUpload.openEndDate = function () {
        $scope.dncUpload.popup3.opened = true;
    };
    $scope.dncUpload.popup3 = {
        opened: false
    };

    $scope.dncUpload.openStartDate = function () {
        $scope.dncUpload.popup2.opened = true;
    };
    $scope.dncUpload.popup2 = {
        opened: false
    };

    $scope.dncUpload.openCreatedDate = function () {
        $scope.dncUpload.popup1.opened = true;
    };
    $scope.dncUpload.popup1 = {
        opened: false
    };

    DNCDropDownService.getDropDown(DNC_CONSTANTS.CHAPTERCODE).success(function (result) {
        $scope.dncUpload.chapterCodes = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    // is this really required?
    DNCDropDownService.getDropDown(DNC_CONSTANTS.GROUPCODE).success(function (result) {
        $scope.dncUpload.groupCodes = result;
        $scope.names = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    // is this really required?
    $scope.dncUpload.populateGroupCode = function (item) {
        $scope.dncUpload.groupCodes.forEach(function (index) {
            if (index.value == item) {
                $scope.dncUpload.groupCode = index.id;
            }
        });
    };

    $scope.dncUpload.compareAgainstStartDate = function (item) {
        var startDate = $filter('date')(new Date($scope.dncUpload.startDate), 'MM/dd/yyyy');
        var endDate = $filter('date')(new Date($scope.dncUpload.endDate), 'MM/dd/yyyy');

        if (endDate < startDate) {
            $scope.dncUpload.toggleInvalidInput = true;
        }
        else
            $scope.dncUpload.toggleInvalidInput = false;

        return $scope.dncUpload.toggleInvalidInput;
    };

    $scope.dncUpload.dncUserInputModal = function () {
        $uibModalInstance.dismiss('cancel');
        DNCServices.clearFileInput();
    };

    var loggedInUserName = DncDataServices.getUserName();

    $scope.dncUploadDateRegex = /^((\d{2})\/(\d{2})\/(\d{4}))$/;

    $scope.dncUpload.submit = function () {

        if ($scope.dncUserInputForm.$valid) {
            myApp.showPleaseWait();//$scope.pleaseWait = { "display": "block" };

            $scope.dncUploadParams = {
                "loggedInUserName": DncDataServices.getUserName(),
              /*  "chapterCode": $scope.dncUpload.chapterCode.id.split(' - ')[0],
                "groupCode": $scope.dncUpload.groupCode,
                "groupName": $scope.dncUpload.groupName,
                "createdBy": $scope.dncUpload.createdBy,
                "createdDate": $filter('date')(new Date($scope.dncUpload.createdDate), 'MM/dd/yyyy'),
                "endDate": $filter('date')(new Date($scope.dncUpload.endDate), 'MM/dd/yyyy'),
                "startDate": $filter('date')(new Date($scope.dncUpload.startDate), 'MM/dd/yyyy')*/
            }

            DncDataServices.getDNCUploadParams($scope.dncUploadParams).then(function (result) {
                //Close the modal first
               // $uibModalInstance.dismiss('cancel');
                //Fire the validate event
                //myApp.hidePleaseWait();
                //do it here only

                $uibModalInstance.close();
                //$rootScope.$emit('validateUploadedFilesEvent', {});
            }, function (error) { });

        }
    };

    $scope.dncUpload.cancel = function () {
        $uibModalInstance.dismiss('cancel');
        DNCServices.clearFileInput();
    };

}]);



angular.module('upload').controller('DNCValidationCtrl', ['results', '$scope', '$uibModal', '$uibModalInstance',
    '$uibModalStack', 'DncColumnDefHelper', 'DNCServices','DNC_CONSTANTS',
    function (results, $scope, $uibModal, $uibModalInstance, $uibModalStack, DncColumnDefHelper, DNCServices, DNC_CONSTANTS) {


        var initialize = function () {
            var columnDef = DncColumnDefHelper.getColumnDef(DNC_CONSTANTS.DNC_VALIDATION , results);
           
            $scope.dncUpload = {
                validationGrid: DNCServices.getGridOptions(columnDef, 1),
                totalItems : results.dncInvalidList.length
            };

            $scope.dncUpload.validationGrid.data = results.dncInvalidList;
            $scope.dncUpload.validationGrid.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.dncUpload.gridApi = gridApi;
            }
        }
        initialize();
       



        $scope.dncUpload.dncClose = function () {
            $uibModalInstance.dismiss('cancel');
            DNCServices.clearFileInput();
        };

        $scope.dncUpload.cancel = function () {
            $uibModalInstance.dismiss('cancel');
            DNCServices.clearFileInput();
        };

        $scope.dncUpload.pageChanged = function (page) {
            $scope.dncUpload.gridApi.pagination.seek(page);
        }

    }]);


angular.module('upload').controller("DNCUploadUserConfirmCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', '$uibModalStack',
    'DncDataServices','DNCServices',
function ($scope, $state, $uibModal, $uibModalInstance, $uibModalStack, DncDataServices, DNCServices) {

    $scope.dncUpload = {
        uploadedFileMessage: null
    }

    $scope.dncUpload.uploadedFileMessage = DncDataServices.getUploadedFileMessage();

    $scope.dncUpload.back = function () {
        $uibModalInstance.dismiss('cancel');
        DNCServices.clearFileInput();
    }

}]);