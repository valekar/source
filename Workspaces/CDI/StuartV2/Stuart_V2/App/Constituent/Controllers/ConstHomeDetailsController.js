angular.module('constituent').controller('constituentHomeDetailsController', ['$scope', '$stateParams', '$localStorage', function ($scope, $stateParams, $localStorage) {
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
        MASTER_DETAIL: "MasterDetail"
    };
    if ($scope.consituent_type == 'IN') {
        $scope.liSmryView = "ChkSelected";
        $scope.currentTlp = BASE_URL + "/ConstDetailsSummaryView.tpl.html";
    }
    else {
        $scope.currentTlp = BASE_URL + "/ConstDetailsBestSmryTemplate.html";
        $scope.liARCBest = "ChkSelected";
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
        switch (type) {
            case $scope.CONSTANTS.SUMMARY_VIEW: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsSummaryView.tpl.html";
                $scope.liSmryView = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.BEST_SMRY: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsBestSmryTemplate.html";
                $scope.liARCBest = "ChkSelected";
                break;
            }
           
            case $scope.CONSTANTS.CONST_NAME: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsNameTemplate.html";
                $scope.liName = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_ORG_NAME: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsOrgName.tpl.html";
                $scope.liOrgName = "ChkSelected";
                break;
            }

            case $scope.CONSTANTS.ANON_INFO: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAnonymousInformationTemplate.html";
                $scope.liAnonymousInfo = "ChkSelected";
                break;
            }

            case $scope.CONSTANTS.CONST_ADDRESS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAddressTemplate.html";
                $scope.liAddress = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_EMAIL: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsEmailTemplate.html";
                $scope.liEmail = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_PHONE: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsPhoneTemplate.html";
                $scope.liPhone = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_EXT_BRIDGE: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsExternalBridgeTemplate.html";
                $scope.liExternalBridge = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_BIRTH: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsBirthTemplate.html";
                $scope.liBirth = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_DEATH: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsDeathTemplate.html";
                $scope.liDeath = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CONST_PREF: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsContactPreferenceTemplate.html";
                $scope.liConactPreference = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.CHARACTERISTICS: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsCharacteristicsTemplate.html";
                $scope.liCharacteristics = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.GRP_MEMBERSHIP: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsGroupMembershipsTemplate.html";
                $scope.liGroupMemberships = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.TRANS_HISTORY: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsTransactionHistoryTemplate.html";
                $scope.liTransactionHistory = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.MASTER_ATTEMPT: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsMasteringAttemptsTemplate.html";
                $scope.liMasteringAttempts = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.INTERNAL_BRIDGE: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsInternalBridgeTemplate.html";
                $scope.liInternalBridge = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.AFFILIATOR: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsAffiliator.tpl.html";
                $scope.liAffiliator = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.MERGE_HISTORY: {
                $scope.currentTlp = BASE_URL + "/ConstDetailsMergeHistoryTemplate.html";
                $scope.liMergeHistory = "ChkSelected";
                break;
            }
            case $scope.CONSTANTS.MASTER_DETAIL: {
                $scope.currentTlp = BASE_URL + "/MasterDetail.tpl.html";
                $scope.liMasterDetail = "ChkSelected";
                break;
            }
        }
    };
}]);

    