angular.module('constituent').controller('constituentHomeDetailsController', ['$scope', '$stateParams', '$localStorage', 
    function ($scope, $stateParams, $localStorage) {
    var params = $stateParams.constituentId;
    $scope.consituent_type = $localStorage.type;
    $scope.masterId = params;
    var BASE_URL = BasePath + 'App/Constituent/Views/home';
    $scope.CONSTANTS = {
        BEST_SMRY: 'BestSmry',
        CONST_NAME: 'ConstName',
        CONST_ORG_NAME:'ConstOrgName',
        CONST_ADDRESS: 'ConstAddress',
        CONST_EMAIL: 'ConstEmail',
        CONST_PHONE: 'ConstPhone',
        CONST_EXT_BRIDGE: 'ConstExtBridge',
        CONST_BIRTH: 'ConstBirth',
        CONST_DEATH: 'ConstDeath',
        CONST_PREF: 'ConstPref',
        CHARACTERISTICS: 'Character',
        GRP_MEMBERSHIP: 'GrpMembership',
        TRANS_HISTORY: "TransHistory",
        ANON_INFO: 'AnonInfo',
        MASTER_ATTEMPT: 'MasterAttempt',
        INTERNAL_BRIDGE: 'InternalBridge',
        MERGE_HISTORY: 'MergeHistory',
        MENU_PREF: "MenuPref",
        SUMMARY_VIEW: "SummaryView",
        AFFILIATOR: "Affiliator",
        MASTER_DETAIL: "MasterDetail",
        EMAIL_DOMAINS: "EmailDomains",
        NAICS_CODES: "NAICSCodes",
        CONTACTS_DETAILS: "ContactsDetails",
        ALTERNATEIDS_DETAILS: "AlternateIds",
        RFMVALUES_DETAILS: "RFMValues"
    };
    if ($scope.consituent_type == 'IN') {
        $scope.liSmryView = CONSTITUENT_MESSAGES.CHECK_SELECTED;
        $scope.currentTlp = BASE_URL + "/ConstDetailsSummaryView.tpl.html";
    }
    else {
        $scope.currentTlp = BASE_URL + "/ConstDetailsBestSmryTemplate.html";
        $scope.liARCBest = CONSTITUENT_MESSAGES.CHECK_SELECTED;
    }
   
    $scope.redirectTo = function (type) {
        $scope.liARCBest = "";
        $scope.liSmryView = "";
        $scope.liName = "";
        $scope.liOrgName = "";
        $scope.liAnonymousInfo = "";
        $scope.liAddress = "";
        $scope.liEmail = "";
        $scope.liPhone = "";
        $scope.liExternalBridge = "";
        $scope.liBirth = "";
        $scope.liDeath = "";
        $scope.liConactPreference = "";
        $scope.liCharacteristics = "";
        $scope.liGroupMemberships = "";
        $scope.liTransactionHistory = "";
        $scope.liMasteringAttempts = "";
        $scope.liInternalBridge = "";
        $scope.liMergeHistory = "";
        $scope.liAffiliator = "";
        $scope.liMasterDetail = "";
        //new section variables
        $scope.liEmailDomainDetails = "";
        $scope.liNAICSCodes = "";
        $scope.liContactsDetails = "";
        $scope.liAlternateIds = "";
        switch (type) {
            case $scope.CONSTANTS.SUMMARY_VIEW: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsSummaryView.tpl.html";
                $scope.liSmryView = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.BEST_SMRY: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsBestSmryTemplate.html";
                $scope.liARCBest = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
           
            case $scope.CONSTANTS.CONST_NAME: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsNameTemplate.html";
                $scope.liName = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_ORG_NAME: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsOrgName.tpl.html";
                $scope.liOrgName = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }

            case $scope.CONSTANTS.ANON_INFO: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAnonymousInformationTemplate.html";
                $scope.liAnonymousInfo = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }

            case $scope.CONSTANTS.CONST_ADDRESS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAddressTemplate.html";
                $scope.liAddress = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_EMAIL: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsEmailTemplate.html";
                $scope.liEmail = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_PHONE: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsPhoneTemplate.html";
                $scope.liPhone = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_EXT_BRIDGE: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsExternalBridgeTemplate.html";
                $scope.liExternalBridge = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_BIRTH: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsBirthTemplate.html";
                $scope.liBirth = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_DEATH: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsDeathTemplate.html";
                $scope.liDeath = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONST_PREF: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsContactPreferenceTemplate.html";
                $scope.liConactPreference = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CHARACTERISTICS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsCharacteristicsTemplate.html";
                $scope.liCharacteristics = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.GRP_MEMBERSHIP: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsGroupMembershipsTemplate.html";
                $scope.liGroupMemberships = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.TRANS_HISTORY: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsTransactionHistoryTemplate.html";
                $scope.liTransactionHistory = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.MASTER_ATTEMPT: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsMasteringAttemptsTemplate.html";
                $scope.liMasteringAttempts = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.INTERNAL_BRIDGE: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsInternalBridgeTemplate.html";
                $scope.liInternalBridge = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.AFFILIATOR: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAffiliator.tpl.html";
                $scope.liAffiliator = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.MERGE_HISTORY: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsMergeHistoryTemplate.html";
                $scope.liMergeHistory = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.MASTER_DETAIL: {
                $scope.currentTlp = BASE_URL + "/MasterDetail.tpl.html";
                $scope.liMasterDetail = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.EMAIL_DOMAINS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsEmailDomain.tpl.html";
                $scope.liEmailDomainDetails = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.NAICS_CODES: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsNaicsCodes.tpl.html";
                $scope.liNAICSCodes = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.CONTACTS_DETAILS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsContacts.tpl.html";
                $scope.liContactsDetails = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.ALTERNATEIDS_DETAILS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAlternateIds.tpl.html";
                $scope.liAlternateIds = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
            case $scope.CONSTANTS.RFMVALUES_DETAILS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsRFMValues.tpl.html";
                $scope.liAlternateIds = CONSTITUENT_MESSAGES.CHECK_SELECTED;
                break;
            }
        }
    };
}]);

    