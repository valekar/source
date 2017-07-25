HOME_TRANS_CONSTANTS = {
    BEST_SMRY: 'BestSmry',
    TRANS_PERSON_NAME: 'PersonName',
    TRANS_ORG_NAME: 'OrgName',
    TRANS_ORG_TRANSFORM: 'TransOrgTransform',
    TRANS_ADDRESS: 'TransAddress',
    TRANS_AFFILIATOR_HIERARCHY: 'TransAffiliatorHierarchy',
    TRANS_AFFILIATOR_TAGS:'TransAffiliatorTags',
    TRANS_EMAIL: 'TransEmail',
    TRANS_EMAIL_UPLOAD: 'TransEmailUpload',
    TRANS_PHONE: 'TransPhone',
    CONST_EXT_BRIDGE: 'ConstExtBridge',
    TRANS_BIRTH: 'TransBirth',
    TRANS_DEATH: 'TransDeath',
    CONST_DEATH: 'ConstDeath',
    TRANS_CONTACT_PREF: 'TransContactPref',
    TRANS_CHARACTERISTICS: 'TransCharacteristics',
    TRANS_GROUP_UPLOAD: 'TransGroupMembershipUpload',
    GRP_MEMBERSHIP: 'GrpMembership',
    TRANS_HISTORY: "TransHistory",
    ANON_INFO: 'AnonInfo',
    MASTER_ATTEMPT: 'MasterAttempt',
    INTERNAL_BRIDGE: 'InternalBridge',
    MERGE_HISTORY: 'MergeHistory',
    CONST_ORG_NAME: "ConstOrgName",
    TRANS_AFFILIATOR: "TransAffiliator",
    SUMMARY_VIEW: "SummaryView",
    MASTER_DETAIL: "MasterDetail",
    ADV_CASE_SEARCH: "AdvCaseSearch",
    QUICK_CASE_SEARCH: "QuickCaseSearch",
    TRANS_MERGE: "TransMerge",

    TRANS_NAICS: "TransNAICS",
    TRANS_NAICS_UPLOAD: "TransNAICSUpload",
    TRANS_ORG_CONFIRMATION: "TransOrgConfirmation",
    TRANS_ORG_EMAIL_DOMAIN: "TransOrgEmailDomain",

    TRANS_UNMERGE_REQUEST_LOG: "UnmergeRequestLog",
    TRANS_UNMERGE_PROCESS_LOG: "UnmergeProcessLog",
    EO_AFFILIATION_UPLOAD: 'EOAffiliationUploads',
    EO_SITE_UPLOAD: 'EOSiteUploads',
    EO_UPLOAD: 'EOUploads'
};

HOME_REDIRECT_URL = BasePath + "transaction/search";

angular.module('transaction').controller('TransactionDetailsBirthController', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$localStorage', 'TransactionDataServices', '$state', '$rootScope', 'TransactionMultiGridServices', 'TransactionApiServices','TransactionMultiDataServices',
function ($scope, uiGridConstants, $uibModal, $stateParams, $localStorage, TransactionDataServices,
    $state, $rootScope, TransactionMultiGridServices, TransactionApiServices, TransactionMultiDataServices) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.BaseURL = BasePath;

        var options = TransactionMultiGridServices.getGridOptions(uiGridConstants, HOME_TRANS_CONSTANTS.TRANS_BIRTH);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

   // console.log("In Birth Details Controller .............");

    var savedDetailsParams = TransactionDataServices.getSavedTransMultiSearchParams();

    $scope.trans_typ_dsc = savedDetailsParams.trans_typ_dsc;
    $scope.sub_trans_typ_dsc = savedDetailsParams.sub_trans_typ_dsc;

    $scope.trans_key = savedDetailsParams.trans_key;

    $scope.backToDetailsPage = function () {
        $state.go('transaction.search.results', {});
    };

    //get the consId from URL;
    var params = {
        "trans_key": $scope.trans_key,
        "trans_typ_dsc": $scope.trans_typ_dsc,
        "sub_trans_typ_dsc": $scope.sub_trans_typ_dsc
    };

    if (params) {
        $scope.pleaseWait = { "display": "block" };


        TransactionApiServices.getApiDetails(params).success(function (results) {
            $scope.gridOptions.data = "";
            $scope.gridOptions.data.length = 0;
            $scope.gridOptions.data = results;
            //added by srini for pagination
            $scope.totalItems = $scope.gridOptions.data.length;
           // console.log(results);
            $scope.toggleDetails = { "display": "block" };
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };
        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    }

    //added by srini for pagination
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

}]);

