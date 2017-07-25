
angular.module('case').controller('CaseCreateController', ['$scope', '$location', '$log', 'CaseServices',
    '$window', 'CaseDataServices', '$http', '$localStorage', 'CaseClearDataService', 'mainService', '$rootScope',
function ($scope, $location, $log, CaseServices, $window, CaseDataServices, $http, $localStorage, CaseClearDataService, mainService, $rootScope) {
    // console.log("hittin");
    //var root = $scipe.BaseURL;
    //alert($scipe.BaseURL);



    var initialize = function () {


        $scope.CaseDateFrom = null;
        $scope.CaseDateTo = null;
        $scope.inlineOptions = {
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };
        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];

        $scope.toggleMin();

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        $scope.popup1 = {
            opened: false
        };

        $scope.open2 = function () {
            $scope.popup2.opened = true;
        };
        $scope.popup2 = {
            opened: false
        };
        $scope.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        var postParams = {};
        //this is clearing localstorage contents(masterid/name)
        delete $localStorage.masterId;
        delete $localStorage.name;
        $scope.pleaseWait = { "display": "none" };
        //  $scope.searchHeader = "Search";

        //if (!angular.isUndefined($localStorage.quickSearchParams)) {
        //    postParams = $localStorage.quickSearchParams;
        //    // console.log(postParams);
        //    //  $scope.constituent_masterid = postParams.masterId;
        //    $localStorage.$reset();
        //    $scope.toggleHeader = false;
        //    callConstituentService($scope, CaseServices, CaseDataServices, postParams, CaseClearDataService, $location, NAVIGATE_URL);
        //}

    };
    initialize();


    //when clicked in search go to search results and do a post there
    $scope.CreateCase = function () {
        if ($scope.CreateCaseForm.$valid) {
            if ($scope.case_Type == "DNC") {
                if ($scope.case_firstName == null || $scope.case_lastName == null) {
                    alert("All DNC cases must containg a First Name and Last Name");
                    return;
                }
            }
            $scope.pleaseWait = { "display": "block" };
            $scope.uploadFiles();

        }
    }

    $scope.selConstType = "Individual";
    $scope.OnIndTypeSel = true;
    $scope.OnOrgTypeSel = false;
    $scope.ChangeConstType = function () {
        if ($scope.selConstType == "Organization") {
            $scope.OnOrgTypeSel = true;
            $scope.OnIndTypeSel = false;
        }
        else if ($scope.selConstType == "Individual") {
            $scope.OnOrgTypeSel = false;
            $scope.OnIndTypeSel = true;
        }
    }


    $scope.file = null;



    //UPLOAD FILE CODE
    var formdata;
    $scope.getTheFiles = function ($files) {
        formdata = new FormData();
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });
    };

    // NOW UPLOAD THE FILES.
    $scope.uploadFiles = function () {
        var request = {
            method: 'POST',
            url: BasePath + 'caseNative/UploadFiles/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        };

        // SEND THE FILES.
       // console.log(formdata);
        if (formdata != null || formdata != undefined) {
            $http(request)
                .success(function (response) {
                    if (response != "Failed!") {
                        postParams = CaseDataServices.getCreateCaseParams($scope, response);
                        callCreateCaseService($scope, CaseServices, CaseDataServices, postParams, CaseClearDataService, $location, $rootScope);
                    }
                    else {
                        alert("Upload Failed! Case creation process aborted.");
                    }
                })
                .error(function () {
                });
        }
        else {
            postParams = CaseDataServices.getCreateCaseParams($scope, null);
            callCreateCaseService($scope, CaseServices, CaseDataServices, postParams, CaseClearDataService, $location, $rootScope);
        }
    }
}]);

angular.module('case').directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}]);


//angular.module('case').directive('fileSelect', function () {
//    var uploadFileTemplate = ' <input type="file" name="files" />';
//    return function (scope, elem, attrs) {
//        var selector = $(uploadFileTemplate);
//        elem.append(selector);
//        selector.bind('change', function (event) {
//            scope.$apply(function () {
//                scope[attrs.fileSelect] = event.originalEvent.target.files;
//            });
//        });
//        scope.$watch(attrs.fileSelect, function (file) {
//            if(file == null)
//                selector.val(file);

//        });
//    };
//});
function callCreateCaseService($scope, CaseServices, CaseDataServices, postParams, CaseClearDataService, $location, $rootScope) {
    $scope.pleaseWait = { "display": "block" };
   // console.log("Into the callcaseService method");
    myApp.showPleaseWait();

    CaseServices.CreateCase(postParams).success(function (result) {
       // console.log("Result retrieved.");
        $("#CreateCaseConfirmation").modal({ backdrop: "static" });
        if (result.length > 0) {
           // console.log(result[0].o_outputMessage);
            $scope.pleaseWait = { "display": "none" };

            //if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
            //    debugger;
            //    $scope.ConfNote = CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM;
            //    $scope.CaseNumText = CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE
            //}
            //else
                if (result[0].o_outputMessage == "Success") {

                $scope.ConfNote = "Case created successfully!";
                $scope.CaseNumText = "Case number: " + result[0].o_case_seq;
                angular.forEach(
                        angular.element("input[type='file']"),
                        function (inputElem) {
                     angular.element(inputElem).val(null);
                         });
                CaseServices.clearCreateParams($scope);
            }
            else {
                $scope.ConfNote = "Case creation failed!";
                $scope; CaseNumText = "Possible error: " + result[0].o_outputMessage;
            }
        }
        else {
            //alert("no data");
            $scope.ConfNote = "Case creation failed!";
            $scope; CaseNumText = "Could not contact web service."
        }
        myApp.hidePleaseWait();
    }).error(function (result) {
        myApp.hidePleaseWait();
        $scope.pleaseWait = { "display": "none" };
        if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
            $scope.ConfNote = CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM;
            $scope.CaseNumText = CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE
        }
        else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }

    });
}