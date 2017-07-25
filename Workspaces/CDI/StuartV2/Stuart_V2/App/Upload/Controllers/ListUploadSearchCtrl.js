/// <reference path="ListUploadSearchCtrl.js" />
angular.module('upload').controller('ListUploadSearchCtrl', ['$scope', 'ListUploadServices', '$location', 'ErrorService', 'Message', "UPLOAD_CONSTANTS",
    function ($scope, ListUploadServices, $location, ErrorService, Message, UPLOAD_CONSTANTS) {
        var NAVIGATE_URL = "/upload/listUpload/results";
        var initialize = function () {
            $scope.listUpload = {
                pleaseWait: false,
                searchForms: [{ id: "0", value: "0" }],
                groups: [],
                noResults: ""
            };

        }
        initialize();

        ListUploadServices.getDropDownValues().then(function (result) {
            //console.log(result);
            $scope.listUpload.groups = result.data;

        }, function (error) { }),


        $scope.listUpload.UploadFilter = function (item) {
            //var matcher = item.value.match(/[^\_0-9](.?)+$/);
            var matcher = item.value.match(/([a-zA-Z])+\w/);
            return matcher;
        }

        //Added by vinoth
      /*  $scope.listUpload.groupmembership = function (formid) {

            var dropdown_ngshow = 'groupNamedropdown_' + formid;
            //$scope[dropdown_ngshow] = !$scope[dropdown_ngshow]
            $scope[dropdown_ngshow] = true;
        }*/
       /* $scope.listUpload.Getgroupname = function (SelectedDataid, SelectedDatavalue, formid) {
            // alert("selected");
            $scope.listUpload['grpValue_' + formid] = SelectedDatavalue;
            $scope.listUpload['grpCd_' + formid] = SelectedDataid;
            var dropdown_ngshow = 'groupNamedropdown_' + formid;
            $scope[dropdown_ngshow] = false;

        }*/



        $scope.listUpload.clearForm = function () {
            for (var i = 0; i < $scope.listUpload.searchForms.length; i++) {
                //console.log($scope.listUpload.searchForms);
                $scope.listUpload['grp_' + i] = "";
            }
            $scope.listUpload.searchForms = [];
            $scope.listUpload.searchForms = [{ id: 0, value: "0" }];
            $scope.listUpload.noResults = "";
        }

        $scope.listUpload.search = function () {
            $scope.listUpload.pleaseWait = true;
            if ($scope.myForm.$valid) {
                var postParams = { "ListUploadSearchInput": [] };

                angular.forEach($scope.listUpload.searchForms, function (v, k) {

                    var searchParams = {
                        "grp_cd": $scope.listUpload['grp_' + v.id].id
                    };
                    postParams["ListUploadSearchInput"].push(searchParams);
                })
            }

            callListUploadSearch(postParams);

        }

        $scope.listUpload.removeForm = function (index) {
            if ($scope.listUpload.searchForms.length > 1) {
                $scope.listUpload.searchForms.splice(index, 1);
            }
        }


        $scope.listUpload.addForm = function () {
            //get the keys
            var keys = Object.keys($scope.listUpload.searchForms);
            //get the last item
            var last = keys[keys.length - 1];
            //add only 5 forms
            if (last < 4) {
                var addObject = { id: +last + 1, value: +last + 1 };
                $scope.listUpload.searchForms.push(addObject);
                // $scope.constituent.searchForms.unshift(addObject);
            }
        }


        function callListUploadSearch(postParams) {
            console.log(postParams);
            ListUploadServices.getListUploadSearchResults(postParams).then(function (result) {
                ListUploadServices.setListUploadSearchData(result.data);
                // used for exporting
                ListUploadServices.setListUploadSearchParams(postParams);
                var _results = ListUploadServices.getListUploadSearchData();
                if (_results.length > 0) {
                    console.log(_results);
                    $scope.listUpload.pleaseWait = false;
                    $location.url(NAVIGATE_URL);
                }
                else if (_results.length <= 0) {

                    $scope.listUpload.pleaseWait = false;
                    Message.open(UPLOAD_CONSTANTS.EMPTY_RESULTS_HEADER, UPLOAD_CONSTANTS.EMPTY_RESUTLS_MESSAGE);
                }

            }, function (error) {
                // console.log(error);
                ErrorService.messagePopup(error);
                $scope.listUpload.pleaseWait = false;
            });
        }

    }]);


