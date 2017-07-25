﻿angular.module('transaction').controller('TransactionMultiDetailsController', ['$scope', '$stateParams', 'TransactionServices', 'TransactionDataServices', 'TransactionApiServices', '$q', '$rootScope', '$state', function ($scope, $stateParams, TransactionServices, TransactionDataServices, TransactionApiServices, $q, $rootScope, $state) {
    var params = $stateParams.trans_key;

    var savedDetailsParams;

    $scope.pleaseWait = { "display": "block" };

    var postParams = { "TransactionSearchInputModel": [] };

    var searchParams = {
        "TransactionKey": params,
        "TransactionType": null,
        "Status": null,
        "MasterId": null,
        "FromDate": null,
        "ToDate": null,
        "UserName": null,
        "SubTransactionType": null
    };

    postParams["TransactionSearchInputModel"].push(searchParams);

    var multiPromise = TransactionServices.getTransactionAdvSearchResults(postParams).success(function (result) {
       // console.log("Trans Search Results");
       // console.log(result);
   
        $scope.trans_typ_dsc = result[0].trans_typ_dsc;
        $scope.sub_trans_typ_dsc = result[0].sub_trans_typ_dsc;
        $scope.trans_key = result[0].trans_key;
        $scope.trans_stat = result[0].trans_stat;

        var savedTransMultiParams = {
            "trans_key": $scope.trans_key,
            "trans_typ_dsc": $scope.trans_typ_dsc,
            "sub_trans_typ_dsc": $scope.sub_trans_typ_dsc,
            "trans_stat": $scope.trans_stat
        };

        TransactionDataServices.setSavedTransMultiSearchParams(savedTransMultiParams);

        savedDetailsParams = savedTransMultiParams; // Set for exporting details

    }).error(function (result) {
        $scope.pleaseWait = { "display": "none" };
        if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            $state.go('transaction.search.results', {});
            messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
            $state.go('transaction.search.results', {});
            messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
    });

    $q.all([multiPromise]).then(function () {
        console.log('multi');
        $scope.pleaseWait = { "display": "none" };

        //After we get back the results proceed with the rest template uploading for respective trans keys

       // console.log("In Multi Details Controller ...");

       // console.log($scope.trans_key);
       // console.log($scope.trans_typ_desc);
       // console.log($scope.sub_trans_typ_dsc);

        var BASE_URL = BasePath + 'App/Transaction/Views/Multi';

        $scope.TRANS_CONSTANT_TEMPLATES = {
            BIRTH: '/TransDetailsBirthTemplate.html',
            UNMERGE: '/TransDetailsUnmergeTemplate.html',
            MERGE: '/TransDetailsMergeTemplate.html',
            AFFILIATOR: '/TransDetailsAffiliatorTemplate.html',
            DEATH: '/TransDetailsDeathTemplate.html',
            PERSON_NAME: '/TransDetailsPersonNameTemplate.html',
            ORG_NAME: '/TransDetailsOrgNameTemplate.html',
            ADDRESS: '/TransDetailsAddressTemplate.html',
            AFFILIATOR_HIERARCHY: '/TransDetailsAffiliatorHierarchyTemplate.html',
            AFFILIATOR_TAGS: '/TransDetailsAffiliatorTagsTemplate.html',
            CHARACTERISTICS: '/TransDetailsCharacteristicsTemplate.html',
            CONTACT_PREF: '/TransDetailsContactPreferenceTemplate.html',
            EMAIL: '/TransDetailsEmailTemplate.html',
            EMAIL_UPLOAD: '/TransDetailsEmailOnlyUploadTemplate.html',
            GROUP_MEMBERSHIP_UPLOAD: '/TransDetailsGroupMembershipUploadTemplate.html',
            ORG_TRANSFORM: '/TransDetailsOrgTransformsTemplate.html',
            PHONE: '/TransDetailsPhoneTemplate.html',
            NAICS: '/TransDetailsNAICSTemplate.html',
            NAICS_UPLOAD: '/TransDetailsNAICSUploadTemplate.html',
            ORG_EMAIL_DOMAIN: '/TransDetailsOrgEmailDomainTemplate.html',
            ORG_CONFIRMATION: '/TransDetailsOrgConfirmationTemplate.html',
            EO_AFFILIATION_UPLOAD: '/TransDetailsEOAffiliationUploadTemplate.html',
            EO_SITE_UPLOAD: '/TransDetailsEOSiteUploadTemplate.html',
            EO_UPLOAD: '/TransDetailsEOUploadTemplate.html',
            EO_CHARACTERISTICS:'/TransDetailsEOCharacteristics.tpl.html'
        };

        if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "DATE OF BIRTH") {
           // console.log("In Birth View Port");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.BIRTH;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "UNMERGE") {
           // console.log("In Unmerge ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.UNMERGE;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ORGANIZATION AFFILIATOR") {
           // console.log("In Org Affiliator ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.AFFILIATOR;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "DATE OF DEATH") {
           // console.log("In Death ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.DEATH;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "PERSON NAME") {
           // console.log("In Person Name ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.PERSON_NAME;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ORGANIZATION NAME") {
          //  console.log("In Org Name ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.ORG_NAME;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ADDRESS") {
           // console.log("In Address ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.ADDRESS;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "AFFILIATOR HIERARCHY") {
          //  console.log("In Affiliator Hierarchy ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.AFFILIATOR_HIERARCHY;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && ($scope.sub_trans_typ_dsc.toUpperCase() == "AFFILIATOR TAGS" || $scope.sub_trans_typ_dsc.toUpperCase() == "AFFILIATOR UPLOAD")) {
           // console.log("In Affiliator Tags ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.AFFILIATOR_TAGS;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "CHARACTERISTICS") {
           // console.log("In Characteristics ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.CHARACTERISTICS;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "CONTACT PREFERENCE") {
           // console.log("In Contact Preference ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.CONTACT_PREF;
        }

        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "EMAIL") {
          //  console.log("In Email ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.EMAIL;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "NAICS CODE") {
            //  console.log("In NAICS ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.NAICS;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "NAICS UPLOAD") {
            //  console.log("In NAICS Upload ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.NAICS_UPLOAD;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ORG CONFIRM INDICATOR") {
            //  console.log("In Org Confirm Indicator ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.ORG_CONFIRMATION;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ORG EMAIL DOMAIN") {
            //  console.log("In Org Email Domain ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.ORG_EMAIL_DOMAIN;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ENTERPRISE ORGANIZATION AFFILIATION UPLOAD") {
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.EO_AFFILIATION_UPLOAD;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ENTERPRISE ORGANIZATION SITE UPLOAD") {
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.EO_SITE_UPLOAD;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ENTERPRISE ORGANIZATION UPLOAD") {
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.EO_UPLOAD;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase().indexOf("UPLOAD") != -1) {
           // console.log("In Email Only Upload ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.EMAIL_UPLOAD;
        }

        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "GROUP MEMBERSHIP") {
           // console.log("In Group Membership ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.GROUP_MEMBERSHIP_UPLOAD;
        }
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ORGANIZATION TRANSFORMATIONS") {
          //  console.log("In Organization Transform ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.ORG_TRANSFORM;
        }

        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "MERGE") {
          //  console.log("In Merge ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.MERGE;
        }

        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "PHONE") {
          //  console.log("In Phone ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.PHONE;
        }
        
        else if (($scope.trans_key != null || $scope.trans_key != undefined) && $scope.sub_trans_typ_dsc.toUpperCase() == "ENTERPRISE ORGANIZATION CHARACTERISTICS") {
            //  console.log("In Phone ViewPort");
            $scope.TransactionInfoTemplate = BASE_URL + $scope.TRANS_CONSTANT_TEMPLATES.EO_CHARACTERISTICS;
        }

        $scope.exportDetails = function () {
            TransactionApiServices.getApiExportDetails(savedDetailsParams).then(function (results) {
              //  console.log("Got back the results for export");
              //  console.log(results);
            });
        };

    });
}])