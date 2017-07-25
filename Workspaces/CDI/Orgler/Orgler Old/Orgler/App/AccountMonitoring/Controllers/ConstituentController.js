﻿angular.module('constituent').controller('constituentSearchController', ['$scope', '$location', '$log', 'constituentServices', 'globalServices', '$rootScope',
    '$window', 'constituentDataServices', 'mainService', '$localStorage', '$sessionStorage', 'constClearDataService',
    'constRecordType', 'dropDownService', '$state', '$timeout','Stack',
function ($scope, $location, $log, constituentServices, globalServices, $rootScope, $window, constituentDataServices, mainService,
    $localStorage, $sessionStorage, constClearDataService, constRecordType, dropDownService, $state, $timeout, Stack) {
    // console.log("hittin$localS
    //var root = $scipe.BaseURL;
    //alert($scipe.BaseURL);


    var NAVIGATE_URL = "/account/search/results";


    var initialize = function () {
       
        //initiate the stack for storing indices
        myStack = new Stack();

        /*********************************************************************/
        //recent most search result code //Chiranjib

        if (mainService.getLastConstSearchResult())
            $scope.lastConstSearchResultPresent = true;
        else
            $scope.lastConstSearchResultPresent = false;

        /*********************************************************************/
        //actual code of search page starts//Srini
        $scope.constituent = {
            states: States(),
            chapters: [],
            sourceSystems: []
        };
       // $scope.constituent_type = "IN";
        //this is used for ng-repeat directive, it needs an array for iterating , you can call this as a dummy array
        $scope.constituent.searchForms = [{ id: 0, value: "0" }];

        //set the search params if they were stored previously // if present then preset the search params
        var savedPostParams = constituentDataServices.getSaveSearchParams();
            console
            if (!angular.isUndefined(savedPostParams["listAccountSearchInputModel"]) && savedPostParams["listAccountSearchInputModel"].length > 0) {
                var searchFormlength = savedPostParams["listAccountSearchInputModel"].length;
            $scope.constituent.searchForms = [];
            for (var i = 0; i < searchFormlength; i++) {
                var form = { id: i, value: i };
                $scope.constituent.searchForms.push(form);
            }
            
            for (var i = 0; i < $scope.constituent.searchForms.length; i++) {
                var savedParam = savedPostParams["listAccountSearchInputModel"][i];
                $scope.constituent['masterId_' + i] = savedParam["masterId"];               
                $scope.constituent['naicsCode_' + i] = savedParam["naicsCode"];               
                $scope.constituent['sourceSystem_' + i] = savedParam["sourceSystem"];
                $scope.constituent['sourceSystemId_' + i] = savedParam["sourceSystemId"];
                $scope.constituent['organizationName_' + i] = savedParam["organizationName"];
                //$scope.constituent['chapterSys_' + i] = savedParam["chapterSystem"];
                $scope.constituent['addressLine_' + i] = savedParam["addressLine"];
                $scope.constituent['city_' + i] = savedParam["city"];
                $scope.constituent['state_' + i] = savedParam["state"];
                $scope.constituent['zip_' + i] = savedParam["zip"];
                $scope.constituent['emailAddress_' + i] = savedParam["emailAddress"];
                $scope.constituent['phone_' + i] = savedParam["phone"];
                $scope.constituent['dateFrom_' + i] = savedParam["dateFrom"];
                $scope.constituent['dateTo_' + i] = savedParam["dateTo"];
                $scope.constituent['masteringType_' + i] = savedParam["masteringType"];
                $scope.constituent['naicsSuggestionPresentInd_' + i] = savedParam["naicsSuggestionPresentInd"];
                $scope.constituent['potentialMergeInd_' + i] = savedParam["potentialMergeInd"];
              
            }
            
        }
        
        /*********************************************************************/
        /*********************************************************************/
        //this is used for case tab to constitutent tab prepopulation
        var caseObj = mainService.getLocInfoResearchData(); // this value is being set when a user redirects from case tab to constituent tab
        //console.log(caseObj); // check if this is a case tab rediection
        if (!angular.isUndefined(caseObj)) {
            //set the value in the constituent global
            globalServices.setCaseInfoData(caseObj);
            mainService.clearLocInfoResearchData();
            globalServices.clearCaseTabCaseNo();
            if ('groupID' in caseObj) {// this means this is a merge conflict record
                var searchparams = caseObj.locInfoResearchData;
                //console.log(searchparams);
                bindFields(searchparams); // private method
                //this is used to set case no in case association section
                // globalServices.setCaseTabCaseNo(caseObj.caseKey);  ---> this is not required because we want to set the case number only for merge conflicts 
                // constRecordType.setRecordType("IN"); // we are setting this before submission of search i.e at $scope.constituent.constSearch
            }
            else {
                //this is used to set case no in case association section
                globalServices.setCaseTabCaseNo(caseObj.caseKey);
                var searchparams = caseObj.locInfoResearchData;
                bindFields(searchparams);// private method
                //constRecordType.setRecordType("IN");// we are setting this before submission of search i.e at $scope.constituent.constSearch
            }

            console.log(globalServices.getCaseInfoData());
            //store them into cookies
            if (globalServices.getCaseInfoData().locInfoResearchData.length > 0) {
                constituentServices.addMergeConflicts(globalServices.getCaseInfoData()).success(function (result) {
                    // console.log("adddedd");
                    //console.log(result);
                }).error(function (result) {

                });
            }
            //constRecordType.setRecordType("IN");
        }
        /*********************************************************************/
        /*********************************************************************/
        var postParams = {};
        //this is clearing localstorage contents(masterid/name)
        delete $sessionStorage.masterId;
        //delete $sessionStorage.name;
        //delete $sessionStorage.type;
        $scope.pleaseWait = { "display": "none" };
        // phone research// this is used for phone details section --> dropdowns/research
        if (constituentDataServices.getPhoneSearchParams().length > 0) {
            var postParams = constituentDataServices.getPhoneSearchParams();
            var newPostParams = { "listAccountSearchInputModel": [] };
            for (var i = 0 ; i < postParams.length; i++) {
                newPostParams["listAccountSearchInputModel"].push(postParams[i]);
            }
            $scope.toggleHeader = false;
            // console.log(newPostParams);
            constituentDataServices.clearPhoneSearchParams();
            constRecordType.setRecordType($scope.constituent_type);
            callConstituentService($scope, constituentServices, constituentDataServices, newPostParams, constClearDataService,mainService, $location, NAVIGATE_URL, $rootScope);
        }
            //quick search
        //else if (!angular.isUndefined($localStorage.quickSearchParams)) {
        //    postParams = $localStorage.quickSearchParams;
        //    var newPostParams = { "ConstituentInputSearchModel": [] };
        //    newPostParams["ConstituentInputSearchModel"].push(postParams);
        //    console.log(newPostParams);
        //    $localStorage.$reset();
        //    $scope.toggleHeader = false;
        //    callConstituentService($scope, constituentServices, constituentDataServices, newPostParams, constClearDataService, $location, NAVIGATE_URL,$rootScope);
        //    //this is used in merge record // for quick search we are only concentrating the "IN"
        //    constRecordType.setRecordType("IN");
        //}
        /*********************************************************************/
        /*********************************************************************/
        constituentServices.getConstRecentSearches().success(function (result) {
            $scope.ConstRecentSearches = result;
        }).error(function (result) {
            errorPopupMessages(result);
        });



        dropDownService.getDropDown(HOME_CONSTANTS.CHAPTER_SYSTEM).success(function (result) {
            $scope.constituent.chapters = result;
        }).error(function (result) {
            errorPopupMessages(result);
        });

        dropDownService.getDropDown(HOME_CONSTANTS.SOURCE_SYSTEM_TYPE).success(function (result) {
            $scope.constituent.sourceSystems = result;
        }).error(function (result) {
            errorPopupMessages(result);
        });
        /*********************************************************************/
    };
    initialize();

    //Get the recentmost search result data on clicking of the button from the mainService
    $scope.fetchLastConstSearchResult = function () {

        if (mainService.getLastConstSearchResult()) {
            //Get back the last search result
            constituentDataServices.setSearchResutlsData(mainService.getLastConstSearchResult());
            $state.go('constituent.search.results', {});
        }
        else {
            window.alert("LocalStorage seems just to be cleared out! Please proceed with the regular search");
        }
    }


    //dynamically add forms 
    $scope.constituent.addForm = function () {

        if (!myStack.isEmpty()) {
            var index = myStack.pop();            
            var addObject = { id: index, value: index };
            $scope.constituent.searchForms.push(addObject);
            clearSelectedForm(index);
        }
        else {
            //get the keys
            var keys = Object.keys($scope.constituent.searchForms);
            //get the last item
            var last = keys[keys.length - 1];
            //add only 5 forms
            if (last < 4) {
                var addObject = { id: +last + 1, value: +last + 1 };
                $scope.constituent.searchForms.push(addObject);
                // $scope.constituent.searchForms.unshift(addObject);
            }

        }

      
       

    };

    $scope.constituent.removeForm = function (index) {
        console.log(index);
        if ($scope.constituent.searchForms.length > 1) {
            angular.forEach($scope.constituent.searchForms, function (v, k) {
                if (v.id == index) {
                    $scope.constituent.searchForms.splice(k, 1);
                    //clearSelectedForm(k);
                }
            });
            myStack.push(index);
          //  myStack.getContents();
            //console.log(index);
          // console.log($scope.constituent.searchForms);            
        }
    }

    function clearSelectedForm(i) {
        $scope.constituent['masterId_' + i] = "";       
        $scope.constituent['naicsCode_' + i] = "";
        $scope.constituent['sourceSystem_' + i] = "";
        $scope.constituent['sourceSystemId_' + i] = "";
        $scope.constituent['organizationName_' + i] = "";
       // $scope.constituent['chapterSys_' + i] = "";
        $scope.constituent['addressLine_' + i] = "";
        $scope.constituent['city_' + i] = "";
        $scope.constituent['state_' + i] = "";
        $scope.constituent['zip_' + i] = "";
        $scope.constituent['emailAddress_' + i] = "";
        $scope.constituent['phone_' + i] = "";
        $scope.constituent['dateFrom_' + i] = "";
        $scope.constituent['dateTo_' + i] = "";
        $scope.constituent['masteringType_' + i] = "";
        $scope.constituent['naicsSuggestionPresentInd_' + i] = "";
        $scope.constituent['potentialMergeInd_' + i] = "";
    }


    $scope.constituent.constSearch = function () {
        $scope.ConstNoResults = "";
        //clear out any no result under quick search panel
        $rootScope.quickSearchConstNoResults = "";
        if ($scope.myForm.$valid) {
            var boolValidateFields = true;
            var postParams = { "listAccountSearchInputModel": [] };
            angular.forEach($scope.constituent.searchForms, function (v, k) {
                if (!($scope.constituent['masterId_' + v.id] ||                      
                        $scope.constituent['naicsCode_' + v.id] ||
                        $scope.constituent['sourceSystem_' + v.id] ||
                        $scope.constituent['sourceSystemId_' + v.id] ||
                    //$scope.constituent['chapterSys_' + v.id] ||
                        $scope.constituent['organizationName_' + v.id] ||
                        $scope.constituent['addressLine_' + v.id] ||
                        $scope.constituent['city_' + v.id] ||
                        $scope.constituent['state_' + v.id] ||
                        $scope.constituent['zip_' + v.id] ||
                        $scope.constituent['emailAddress_' + v.id] ||
                        $scope.constituent['phone_' + v.id] ||
                        $scope.constituent['dateFrom_' + v.id] ||
                        $scope.constituent['dateTo_' + v.id]||
                        $scope.constituent['masteringType_' + v.id] ||
                        $scope.constituent['naicsSuggestionPresentInd_' + v.id] ||
                        $scope.constituent['potentialMergeInd_' + v.id]
                    )) {
                    $scope.ConstNoResults = "Please provide at least one input criteria before search";
                    $scope.pleaseWait = { "display": "none" };
                    boolValidateFields = false;
                }
                //else if (($scope.constituent['srcSystem_' + v.id] && !$scope.constituent['srcSysemId_' + v.id]) ||
                //    ($scope.constituent['chapterSys_' + v.id] && !$scope.constituent['srcSysemId_' + v.id]) ||
                //    ($scope.constituent['srcSysemId_' + v.id] && (!$scope.constituent['srcSystem_' + v.id] && !$scope.constituent['chapterSys_' + v.id]))) {
                //    $scope.ConstNoResults = "Please provide both Source System and Source System Id before search";
                //    $scope.pleaseWait = { "display": "none" };
                //    boolValidateFields = false;
                //}
            });
            if (boolValidateFields == true) {
                angular.forEach($scope.constituent.searchForms, function (v, k) {

                    var searchParams = {
                        "masterId": $scope.constituent['masterId_' + v.id],                       
                        "naicsCode": $scope.constituent['naicsCode_' + v.id],
                        "sourceSystem": $scope.constituent['sourceSystem_' + v.id],
                        "sourceSystemId": $scope.constituent['sourceSystemId_' + v.id],
                        // "chapterSystem": $scope.constituent['chapterSystem_' + v.id],
                        "organizationName": $scope.constituent['"organizationName_' + v.id],
                        "city": $scope.constituent['city_' + v.id],
                        "state": $scope.constituent['state_' + v.id],
                        "zip": $scope.constituent['zip_' + v.id],
                        "addressLine": $scope.constituent['addressLine_' + v.id],
                        "emailAddress": $scope.constituent['emailAddress_' + v.id],
                        "phone": $scope.constituent['phone_' + v.id],
                        "masteringType": $scope.account_type,
                        "dateFrom": $scope.constituent['dateFrom_' + v.id],
                        "dateTo": $scope.constituent['dateTo_' + v.id],
                        "masteringType": $scope.constituent['masteringType_' + v.id],
                        "naicsSuggestionPresentInd": $scope.constituent['naicsSuggestionPresentInd_' + v.id],
                        "potentialMergeInd": $scope.constituent['potentialMergeInd_' + v.id]
                    };
                    postParams["listAccountSearchInputModel"].push(searchParams);

                });
                console.log(postParams);

                postParams["constituent_type"] = $scope.constituent_type;
                constituentDataServices.setSaveSearchParams(postParams);

                //please don't remove this
                constRecordType.setRecordType($scope.constituent_type);
                //mainService.setSearchResutlsData(null);
                //mainService.setAdvSearchParams(null);
                $localStorage.QuickSearchResultsData = null;
                $localStorage.quickSearchParams = null;
                callConstituentService($scope, constituentServices, constituentDataServices, postParams, constClearDataService, mainService, $location, NAVIGATE_URL, $rootScope);
            }
        }
        else {

            $scope.ConstNoResults = "Please enter only numbers for MasterId field"
        }
    }



    $scope.constituent.clearForm = function () {
        //this is used for ng-repeat directive, it needs an array for iterating , you can call this as a dummy array

        for (var i = 0; i < $scope.constituent.searchForms.length; i++) {
            $scope.constituent['masterId_' + i] = "";
            $scope.constituent['naicsCode_' + i] = "";
            $scope.constituent['sourceSystem_' + i] = "";
            $scope.constituent['sourceSystemId_' + i] = "";
            $scope.constituent['organizationName_' + i] = "";
            // $scope.constituent['chapterSys_' + i] = "";
            $scope.constituent['addressLine_' + i] = "";
            $scope.constituent['city_' + i] = "";
            $scope.constituent['state_' + i] = "";
            $scope.constituent['zip_' + i] = "";
            $scope.constituent['emailAddress_' + i] = "";
            $scope.constituent['phone_' + i] = "";
            $scope.constituent['dateFrom_' + i] = "";
            $scope.constituent['dateTo_' + i] = "";
            $scope.constituent['masteringType_' + i] = "";
            $scope.constituent['naicsSuggestionPresentInd_' + i] = "";
            $scope.constituent['potentialMergeInd_' + i] = "";

        }
        $scope.constituent.searchForms = [];
        $scope.constituent.searchForms = [{ id: 0, value: "0" }];
        $scope.ConstNoResults = "";
        return
    }



    $scope.constituentRecentSearch = function (qry) {
        $scope.pleaseWait = { "display": "block" };
        // console.log("Query");
         console.log(qry);
        var listPostParam = [];
        $('#recentConstSearchesModal').modal('hide');
        listPostParam.push(qry);
        postParams = listPostParam;
        //postParams = constituentDataServices.getSearchParams($scope);
        if (typeof qry.listAccountSearchInputModel != 'undefined') {
            if (typeof qry.listAccountSearchInputModel[0].type != 'undefined') {
                //please don't remove this
                constRecordType.setRecordType(qry.listAccountSearchInputModel[0].type);
            }
        }

        callConstituentService($scope, constituentServices, constituentDataServices, qry, constClearDataService,mainService, $location, NAVIGATE_URL, $rootScope);

    }

    function errorPopupMessages(result) {

        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
    }
    //added by srini to clear the search parameters upon changing the type
    $scope.constituent.typeChange = function () {
        $scope.constituent.clearForm();
    }
    //GET THE naicsAproveFormData

    $scope.naicsAproveFormData= function () {
        // getNameAndEmailUserUploadDataPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, NameAndEmailUploadDataServices);
        NameEmailUploadModalService.getModalPopup($scope, 'lg', NAME_AND_EMAIL_UPLOAD_CONSTANTS.DATA_POPUP);
    };

}]);


angular.module('constituent').filter('placeholderfunc', function () {
    return function (input) {
        // console.log("Recent Searches input.ConstituentInputSearchModel[0]");
        //console.log(angular.toJson(input.ConstituentInputSearchModel[0]));
        var ConstResultsBindingData = "";
        angular.forEach(input.listAccountSearchInputModel, function (value, key) {
            if (value.sourceSystem != null)
                ConstResultsBindingData += "Source System: " + value.sourceSystem + ", ";
            if (value.sourceSystemId != null)
                ConstResultsBindingData += "Source SystemId: " + value.sourceSystemId + ", ";
            if (value.masterId != null)
                ConstResultsBindingData += "Master Id: " + value.masterId + ", ";
            if (value.accountName != null)
                ConstResultsBindingData += "Account Name: " + value.accountName + ", ";
            if (value.addressLine != null)
                ConstResultsBindingData += "Address Line: " + value.addressLine + ", ";
            if (value.city != null)
                ConstResultsBindingData += "City: " + value.city + ", ";
            if (value.state != null)
                ConstResultsBindingData += "State: " + value.state + ", "
            if (value.zip != null)
                ConstResultsBindingData += "Zip: " + value.zip + ", ";
            if (value.naicsCode1 != null)
                ConstResultsBindingData += "naicsCode1: " + value.naicsCode1 + ", ";
            if (value.naicsCode2 != null)
                ConstResultsBindingData += "naicsCode2: " + value.naicsCode2 + ", ";
            if (value.naicsCode3 != null)
                ConstResultsBindingData += "naicsCode3: " + value.naicsCode3 + ", ";
            if (value.emailAddress != null)
                ConstResultsBindingData += "Email Address: " + value.emailAddress + ", ";
            if (value.rfm != null)
                ConstResultsBindingData += "rfm: " + value.rfm + ", ";
            if (value.identificationType != null)
                ConstResultsBindingData += "identificationType: " + value.identificationType + ", ";
            if (value.naics_sggstn_prsnt_ind != null)
                ConstResultsBindingData += "naics_sggstn_prsnt_ind: " + 'NAICS Suggestions' + "  ";
            if (value.pot_merge_ind != null)
                ConstResultsBindingData += "pot_merge_ind: " + 'Potential Merges' + "  ";
            ConstResultsBindingData += ";"
           
           
        });
        // console.log("ConstResultsBindingData");
        //  console.log(ConstResultsBindingData);
        if (ConstResultsBindingData != "")
            return ConstResultsBindingData.slice(0, -3);
        else
            return ConstResultsBindingData;

    }
});


function callConstituentService($scope, constituentServices, constituentDataServices, postParams, constClearDataService,mainService, $location, NAVIGATE_URL, $rootScope) {
    $scope.pleaseWait = { "display": "block" };

    constituentServices.getConstituentAdvSearchResults(postParams).success(function (result) {
        //cleat all multi tab data , need to do for normal tab too
        constClearDataService.clearMultiData();
        constituentDataServices.setSearchResutlsData(result);
        // this is for export 
        constituentDataServices.setAdvSearchParams(postParams);
        var _result = constituentDataServices.getSearchResultsData();
        //  console.log(_result);

        //Setting the last search result's data in the mainService

        mainService.setLastConstSearchResult(_result);

        if (_result.length > 0) {
            $scope.pleaseWait = { "display": "none" };            
            $location.url(NAVIGATE_URL);
        }
        else {
            $scope.ConstNoResults = "No Results found!";
            $scope.pleaseWait = { "display": "none" };
        }


    }).error(function (result) {
        $scope.pleaseWait = { "display": "none" };
        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            angular.element("#accessDeniedModal").modal();
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, "Internal Server Error", "Error: Timed Out");

        }
       
    });


}

