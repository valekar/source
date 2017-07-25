angular.module('upload').controller("MsgPrefUploadCtrl", ['$scope', 'MSG_PREF_CONSTANTS', 'MsgPrefServices', 'MsgPrefDataServices','mainService','$timeout','$uibModalStack',
    function ($scope, MSG_PREF_CONSTANTS, MsgPrefServices, MsgPrefDataServices, mainService, $timeout, $uibModalStack) {


    var initialize = function () {
        $scope.msgPref = {
            togglePleaseWait: false,
            msgPrefTemplateDownload: MSG_PREF_CONSTANTS.MSG_PREF_TEMPLATE_PATH,
        }
        mainService.getUsername().then(function (result) {
            username = result.data;
            MsgPrefDataServices.setUserName(username);
        });
    }

    initialize();



    $scope.msgPref.getMsgPrefFiles = function ($files) {
        var result = document.getElementsByClassName("file-input-label");
        angular.element(result).text($files[0].name);

        formdata = new FormData();
        MsgPrefDataServices.setMsgPrefUploadFormData(formdata);
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });

        $scope.msgPref.getUserUploadData();
    }

    //GET THE USER UPLOAD DATA

    $scope.msgPref.getUserUploadData = function () {
        MsgPrefServices.getMsgPrefPopup(MSG_PREF_CONSTANTS.DATA_POPUP).then(function () {

            console.log('after promise');
            $scope.validateRecords();
        });
    }


    $scope.validateRecords = function () {

        console.log("validation");

        MsgPrefDataServices.validateMsgPrefUpload(formdata).then(function (result) {
            console.log(result)
            MsgPrefServices.clearFileInput();
            myApp.hidePleaseWait();
            if (result != "Failed!" && result.data.message != "FileAlreadyUploaded" && result.data.message != "IncorrectTemplate" &&
                result.data.message != "FileLimitExceeded" && !angular.isUndefined(result.data.msgPrefValidList) && !angular.isUndefined(result.data.msgPrefInvalidList) &&
                    result.data.msgPrefInvalidList.length == 0 && result.data.msgPrefValidList.length > 0) {

                if (angular.isUndefined(result.data.message)) {
                    MsgPrefDataServices.setUploadResult(result.data)

                    angular.element("#msgPrefFileUploadSuccessModal").modal();
                }
                // return;
            }
            else if (result.data.message == "IncorrectTemplate") {
                angular.element("#incorrectMsgPrefTemplateUploadModal").modal();
                return;
            }
            else if (result.data.message == "EmptyFileUploaded") {
                // myApp.hidePleaseWait();
                angular.element("#emptyUploadModal").modal();
                return;
            }
            else if (result.data.message == "FileAlreadyUploaded") {
                angular.element("#fileAlreadyUploadedModal").modal();
            }
            else if (result.data.message == "FileLimitExceeded") {
              
                angular.element("#fileLimitExceedModal").modal();

            }

            else if (!angular.isUndefined(result.data.msgPrefInvalidList) && result.data.msgPrefInvalidList.length > 0) {
                // myApp.hidePleaseWait();
                console.log(result.data);
                MsgPrefServices.getMsgPrefPopup(MSG_PREF_CONSTANTS.VALIDATION_POPUP, result.data);
                //return;
            }


            myApp.hidePleaseWait();
        },
        function (error) {
            myApp.hidePleaseWait(); //$scope.pleaseWait = { "display": "none" };

            //Close all the modals
            // $uibModalStack.dismissAll();

            //Clear the file input control
            MsgPrefServices.clearFileInput();

            if (error.data == UPLOAD_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (error.data == UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                // MsgPrefServices.messageUploadPopup(UPLOAD_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (error.data == UPLOAD_CRUD_CONSTANTS.DB_ERROR) {
                 //MsgPrefServices.messageUploadPopup(UPLOAD_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    }

        //upload the results to service 
    $scope.msgPref.fileUpload = function () {

        function pleaseWaitTimeoutPromise() {
            return $timeout(function () {
                return $uibModalStack.dismissAll();
            }, 1000);
        }

        $scope.msgPref.togglePleaseWait = true;

        var msgPrefUploadedFormData = MsgPrefDataServices.getUploadResult();
        var str1 = "The file ";
        var uploadedFileMessage = str1.concat(msgPrefUploadedFormData.msgPrefUploadedFileOutputInfo.fileName, " has been successfully uploaded and is being processed. An email confirmation will be sent once the processing is complete.");

        pleaseWaitTimeoutPromise().then(function (result) {
            $scope.msgPref.togglePleaseWait = false;
            MsgPrefDataServices.setUploadedFileMessage(uploadedFileMessage);
            //getUploadConfirmationPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, UploadDataServices);
            MsgPrefServices.getMsgPrefPopup(MSG_PREF_CONSTANTS.CONFIRMATION_POPUP);
        });

        MsgPrefServices.clearFileInput();

        MsgPrefDataServices.postMsgPrefUploadData(msgPrefUploadedFormData, username)
            .then(function (result) {
                console.log("File Data sent to Server");
                console.log(result);
            });
    };

    $scope.msgPref.fileUploadModalClose = function () {
        MsgPrefServices.clearFileInput();
    }


}]);


angular.module('upload').controller("MsgPrefUploadUserDataCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'MSG_PREF_CONSTANTS',
    'uiGridConstants', 'MsgPrefDataServices', '$filter', '$uibModalStack', 'MsgPrefServices',
function ($scope, $state, $uibModal, $uibModalInstance, MSG_PREF_CONSTANTS,
    uiGridConstants, MsgPrefDataServices, $filter, $uibModalStack, MsgPrefServices) {

    var initialize = function(){
        $scope.msgPref = {
            showCloseButton: true,
            toggleInvalidInput: false,
            expDate: new Date(9999, 11, 31),
            dateOptions: {
                formatYear: 'yy',
                maxDate: new Date(9999, 11, 31),
                minDate: new Date(2000, 0, 1),
                minDate: new Date(),
                startingDay: 1
            },
        }

        $scope.msgPref.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.msgPref.format = $scope.msgPref.formats[0];
    }
    initialize();

    $scope.msgPref.openExpirationDate = function () {
        $scope.msgPref.expirationPopup.opened = true;
    };
    $scope.msgPref.expirationPopup = {
        opened: false
    };


    $scope.msgPref.msgPrefUserInputModal = function () {
        $uibModalInstance.dismiss('cancel');
        MsgPrefServices.clearFileInput();
    };

    var loggedInUserName = MsgPrefDataServices.getUserName();


    $scope.msgPref.submit = function () {

        if ($scope.msgPrefUserInputForm.$valid) {
            myApp.showPleaseWait();//$scope.pleaseWait = { "display": "block" };

            $scope.msgPrefUploadParams = {
                "loggedInUserName": MsgPrefDataServices.getUserName(),
                "expirationDate": $filter('date')(new Date($scope.msgPref.expDate), 'MM/dd/yyyy'),
            }

            MsgPrefDataServices.getMsgPrefUploadParams($scope.msgPrefUploadParams).then(function (result) {
                $uibModalInstance.close();
            }, function (error) { });

        }
    };

    $scope.msgPref.cancel = function () {
        $uibModalInstance.dismiss('cancel');
        MsgPrefServices.clearFileInput();
    };

}]);


angular.module('upload').controller('MsgPrefValidationCtrl', ['results', '$scope', '$uibModal', '$uibModalInstance',
    '$uibModalStack', 'MsgPrefColumnDefHelper', 'MsgPrefServices',
    function (results, $scope, $uibModal, $uibModalInstance, $uibModalStack, MsgPrefColumnDefHelper, MsgPrefServices) {


        var initialize = function () {
            var columnDef = MsgPrefColumnDefHelper.getColumnDef(results);

            $scope.msgPrefUpload = {
                validationGrid: MsgPrefServices.getGridOptions(columnDef, 1),
                totalItems : results.msgPrefInvalidList.length
            };

            $scope.msgPrefUpload.validationGrid.data = results.msgPrefInvalidList;

            $scope.msgPrefUpload.validationGrid.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.msgPrefUpload.gridApi = gridApi;
            }


        }
        initialize();
        $scope.msgPrefUpload.close = function () {
            $uibModalInstance.dismiss('cancel');
            MsgPrefServices.clearFileInput();
        };

        $scope.msgPrefUpload.cancel = function () {
            $uibModalInstance.dismiss('cancel');
            MsgPrefServices.clearFileInput();
        };

        $scope.msgPrefUpload.pageChanged = function (page) {
            $scope.msgPrefUpload.gridApi.pagination.seek(page);
        }

    }]);


angular.module('upload').controller("MsgPrefUploadUserConfirmCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', '$uibModalStack',
    'MsgPrefDataServices', 'MsgPrefServices',
function ($scope, $state, $uibModal, $uibModalInstance, $uibModalStack, MsgPrefDataServices, MsgPrefServices) {

    $scope.msgPrefUpload = {
        uploadedFileMessage: null
    }

    $scope.msgPrefUpload.uploadedFileMessage = MsgPrefDataServices.getUploadedFileMessage();

    $scope.msgPrefUpload.back = function () {
        $uibModalInstance.dismiss('cancel');
        MsgPrefServices.clearFileInput();
    }

}]);