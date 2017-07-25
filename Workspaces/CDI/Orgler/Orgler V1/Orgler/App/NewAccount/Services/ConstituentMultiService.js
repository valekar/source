newAccMod.constant('CONSTANTS', {
    BEST_SMRY: 'BestSmry',
    CONST_NAME: 'ConstName',
    CONST_ADDRESS: 'ConstAddress',
    CONST_PHONE: 'ConstPhone',
    CONST_EMAIL: 'ConstEmail',
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
    CONST_ORG_NAME: "ConstOrgName",
    AFFILIATOR: "Affiliator",
    SUMMARY_VIEW:"SummaryView",
    MASTER_DETAIL: "MasterDetail",
    ADV_CASE_SEARCH: "AdvCaseSearch",
    QUICK_CASE_SEARCH: "QuickCaseSearch",
    SHOW_DETAIL: "ShowDetails",
    EMAIL_DOMAINS: "EmailDomains",
    NAICS_CODES: "NAICSCodes",
    CONTACTS_DETAILS: "ContactsDetails",
    ALTERNATEIDS_DETAILS: "AlternateIds",
    CONST_POTENTIALMERGE: 'PotentialMerge',
    CONST_POTENTIALUNMERGE: 'PotentialUnMerge'

});

newAccMod.factory('constMultiDataService', ['CONSTANTS', function (CONSTANTS) {
    // var bestSmryData = [];

    /*var CONSTANTS = {
        BEST_SMRY: 'BestSmry',
        CONST_NAME: 'ConstName',
        CONST_ADDRESS:'ConstAddress'
    };*/

    var allConstDatas = {
        bestSmryData: [],
        constNameData: [],
        constOrgNameData: [],
        constAddressData: [],
        constPhoneData: [],
        constEmailData: [],
        constExtBridgeData: [],
        contBirthData: [],
        constDeathData: [],
        constPrefData: [],
        characteristicsData: [],
        grpMemberData: [],
        transHistoryData: [],
        anonInfoData: [],
        masterAttemptData: [],
        internalBridgeData: [],
        mergeHistoryData: [],
        affiliatorData: [],
        summaryViewData:[],
        masterDetailData: [],
        quickCaseSearchData: [],
        emailDomains: [],
        naicsCodes: [],
        contactsDetails: [],
        alternateIdsDetails: [],
        constPotentialMergeData: [],
        constPotentialUnMergeData: []
    };

    var allConstFullDatas = {
        bestSmryData: [],
        constNameData: [],
        constOrgNameData: [],
        constAddressData: [],
        constPhoneData: [],
        constEmailData: [],
        constExtBridgeData: [],
        contBirthData: [],
        constDeathData: [],
        constPrefData: [],
        characteristicsData: [],
        grpMemberData: [],
        transHistoryData: [],
        anonInfoData: [],
        masterAttemptData: [],
        internalBridgeData: [],
        mergeHistoryData: [],
        affiliatorData: [],
        summaryViewData: [],
        masterDetailData: [],
        quickCaseSearchData: [],
        emailDomains: [],
        naicsCodes: [],
        contactsDetails: [],
        alternateIdsDetails: [],
        constPotentialMergeData: [],
        constPotentialUnMergeData: []
    };


    return {

        getData: function (type) {
            switch (type) {
                case CONSTANTS.BEST_SMRY: { return allConstDatas.bestSmryData; break; };
                case CONSTANTS.CONST_NAME: { return allConstDatas.constNameData; break; };
                    //same as person name
                case CONSTANTS.CONST_ORG_NAME: { return allConstDatas.constOrgNameData; break; };
                case CONSTANTS.CONST_ADDRESS: { return allConstDatas.constAddressData; break; };
                case CONSTANTS.CONST_PHONE: { return allConstDatas.constPhoneData; break; };
                case CONSTANTS.CONST_EMAIL: { return allConstDatas.constEmailData; break; };
                case CONSTANTS.CONST_EXT_BRIDGE: { return allConstDatas.constExtBridgeData; break; }
                case CONSTANTS.CONST_BIRTH: { return allConstDatas.contBirthData; break; };
                case CONSTANTS.CONST_DEATH: { return allConstDatas.constDeathData; break; };
                case CONSTANTS.CONST_PREF: { return allConstDatas.constPrefData; break; };
                case CONSTANTS.CHARACTERISTICS: { return allConstDatas.characteristicsData; break; };
                case CONSTANTS.GRP_MEMBERSHIP: { return allConstDatas.grpMemberData; break; };
                case CONSTANTS.TRANS_HISTORY: { return allConstDatas.transHistoryData; break; };
                case CONSTANTS.ANON_INFO: { return allConstDatas.anonInfoData; break; };
                case CONSTANTS.MASTER_ATTEMPT: { return allConstDatas.masterAttemptData; break; };
                case CONSTANTS.INTERNAL_BRIDGE: { return allConstDatas.internalBridgeData; break; };
                case CONSTANTS.MERGE_HISTORY: { return allConstDatas.mergeHistoryData; break; };
                case CONSTANTS.AFFILIATOR: { return allConstDatas.affiliatorData; break; };
                case CONSTANTS.SUMMARY_VIEW: { return allConstDatas.summaryViewData; break; };
                case CONSTANTS.MASTER_DETAIL: { return allConstDatas.masterDetailData; break; };
                case CONSTANTS.QUICK_CASE_SEARCH: { return allConstDatas.quickCaseSearchData; break; };
                case CONSTANTS.EMAIL_DOMAINS: { return allConstDatas.emailDomains; break; };
                case CONSTANTS.NAICS_CODES: { return allConstDatas.naicsCodes; break; };
                case CONSTANTS.CONTACTS_DETAILS: { return allConstDatas.contactsDetails; break; };
                case CONSTANTS.ALTERNATEIDS_DETAILS: { return allConstDatas.alternateIdsDetails; break; };
                case CONSTANTS.CONST_POTENTIALMERGE: { return allConstDatas.constPotentialMergeData; break; };
                case CONSTANTS.CONST_POTENTIALUNMERGE: { return allConstDatas.constPotentialUnMergeData; break; };
            }
        },

        setData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.BEST_SMRY: { allConstDatas.bestSmryData = resultData; break; };
                case CONSTANTS.CONST_NAME: { allConstDatas.constNameData = resultData; break; };
                    //same as person name
                case CONSTANTS.CONST_ORG_NAME: { allConstDatas.constOrgNameData = resultData; break; };
                case CONSTANTS.CONST_ADDRESS: { allConstDatas.constAddressData = resultData; break; };
                case CONSTANTS.CONST_PHONE: { allConstDatas.constPhoneData = resultData; break; };
                case CONSTANTS.CONST_EMAIL: { allConstDatas.constEmailData = resultData; break; };
                case CONSTANTS.CONST_EXT_BRIDGE: { allConstDatas.constExtBridgeData = resultData; break; };
                case CONSTANTS.CONST_BIRTH: { allConstDatas.contBirthData = resultData; break; };
                case CONSTANTS.CONST_DEATH: { allConstDatas.constDeathData = resultData; break; };
                case CONSTANTS.CONST_PREF: { allConstDatas.constPrefData = resultData; break; };
                case CONSTANTS.CHARACTERISTICS: { allConstDatas.characteristicsData = resultData; break; };
                case CONSTANTS.GRP_MEMBERSHIP: { allConstDatas.grpMemberData = resultData; break; };
                case CONSTANTS.TRANS_HISTORY: { allConstDatas.transHistoryData = resultData; break; };
                case CONSTANTS.ANON_INFO: { allConstDatas.anonInfoData = resultData; break; };
                case CONSTANTS.MASTER_ATTEMPT: { allConstDatas.masterAttemptData = resultData; break; };
                case CONSTANTS.INTERNAL_BRIDGE: { allConstDatas.internalBridgeData = resultData; break; };
                case CONSTANTS.MERGE_HISTORY: { allConstDatas.mergeHistoryData = resultData; break; };
                case CONSTANTS.AFFILIATOR: { allConstDatas.affiliatorData = resultData; break; };
                case CONSTANTS.SUMMARY_VIEW: { allConstDatas.summaryViewData = resultData; break; };
                case CONSTANTS.MASTER_DETAIL: { allConstDatas.masterDetailData = resultData; break; };
                case CONSTANTS.QUICK_CASE_SEARCH: { allConstDatas.quickCaseSearchData = resultData; break; };
                case CONSTANTS.EMAIL_DOMAINS: {allConstDatas.emailDomains = resultData; break; };
                case CONSTANTS.NAICS_CODES: {allConstDatas.naicsCodes = resultData; break; };
                case CONSTANTS.CONTACTS_DETAILS: {allConstDatas.contactsDetails = resultData; break; };
                case CONSTANTS.ALTERNATEIDS_DETAILS: { allConstDatas.alternateIdsDetails = resultData; break; };
                case CONSTANTS.CONST_POTENTIALMERGE: { allConstDatas.constPotentialMergeData = resultData; break; };
                case CONSTANTS.ALTERNATEIDS_DETAILS: { allConstDatas.constPotentialUnMergeData = resultData; break; };

            }
        },
        setFullData: function (resultData, type) {
            switch (type) {
                case CONSTANTS.BEST_SMRY: { allConstFullDatas.bestSmryData = resultData; break; };
                case CONSTANTS.CONST_NAME: { allConstFullDatas.constNameData = resultData; break; };
                    //same as person name
                case CONSTANTS.CONST_ORG_NAME: { allConstFullDatas.constOrgNameData = resultData; break; };
                case CONSTANTS.CONST_ADDRESS: { allConstFullDatas.constAddressData = resultData; break; };
                case CONSTANTS.CONST_PHONE: { allConstFullDatas.constPhoneData = resultData; break; };
                case CONSTANTS.CONST_EMAIL: { allConstFullDatas.constEmailData = resultData; break; };
                case CONSTANTS.CONST_EXT_BRIDGE: { allConstFullDatas.constExtBridgeData = resultData; break; };
                case CONSTANTS.CONST_BIRTH: { allConstFullDatas.contBirthData = resultData; break; };
                case CONSTANTS.CONST_DEATH: { allConstFullDatas.constDeathData = resultData; break; };
                case CONSTANTS.CONST_PREF: { allConstFullDatas.constPrefData = resultData; break; };
                case CONSTANTS.CHARACTERISTICS: { allConstFullDatas.characteristicsData = resultData; break; };
                case CONSTANTS.GRP_MEMBERSHIP: { allConstFullDatas.grpMemberData = resultData; break; };
                case CONSTANTS.TRANS_HISTORY: { allConstFullDatas.transHistoryData = resultData; break; };
                case CONSTANTS.ANON_INFO: { allConstFullDatas.anonInfoData = resultData; break; };
                case CONSTANTS.MASTER_ATTEMPT: { allConstFullDatas.masterAttemptData = resultData; break; };
                case CONSTANTS.INTERNAL_BRIDGE: { allConstFullDatas.internalBridgeData = resultData; break; };
                case CONSTANTS.MERGE_HISTORY: { allConstFullDatas.mergeHistoryData = resultData; break; };
                case CONSTANTS.AFFILIATOR: { allConstFullDatas.affiliatorData = resultData; break; };
                case CONSTANTS.SUMMARY_VIEW: { allConstFullDatas.summaryViewData = resultData; break; };
                case CONSTANTS.MASTER_DETAIL: { allConstFullDatas.masterDetailData = resultData; break; };
                case CONSTANTS.QUICK_CASE_SEARCH: { allConstFullDatas.quickCaseSearchData = resultData; break; };
                case CONSTANTS.EMAIL_DOMAINS: {allConstFullDatas.emailDomains = resultData; break; };
                case CONSTANTS.NAICS_CODES: {allConstFullDatas.naicsCodes = resultData; break; };
                case CONSTANTS.CONTACTS_DETAILS: {allConstFullDatas.contactsDetails=resultData; break; };
                case CONSTANTS.ALTERNATEIDS_DETAILS: {allConstFullDatas.alternateIdsDetails = resultData; break;};
                case CONSTANTS.CONST_POTENTIALMERGE: { allConstFullDatas.constPotentialMergeData = resultData; break; };
                case CONSTANTS.CONST_POTENTIALUNMERGE: { allConstFullDatas.constPotentialUnMergeData = resultData; break; };
            }
        },
        getFullData: function (type) {
            switch (type) {
                case CONSTANTS.BEST_SMRY: { return allConstFullDatas.bestSmryData; break; };
                case CONSTANTS.CONST_NAME: { return allConstFullDatas.constNameData; break; };
                    //same as person name
                case CONSTANTS.CONST_ORG_NAME: { return allConstFullDatas.constOrgNameData; break; };
                case CONSTANTS.CONST_ADDRESS: { return allConstFullDatas.constAddressData; break; };
                case CONSTANTS.CONST_PHONE: { return allConstFullDatas.constPhoneData; break; };
                case CONSTANTS.CONST_EMAIL: { return allConstFullDatas.constEmailData; break; };
                case CONSTANTS.CONST_EXT_BRIDGE: { return allConstFullDatas.constExtBridgeData; break; }
                case CONSTANTS.CONST_BIRTH: { return allConstFullDatas.contBirthData; break; };
                case CONSTANTS.CONST_DEATH: { return allConstFullDatas.constDeathData; break; };
                case CONSTANTS.CONST_PREF: { return allConstFullDatas.constPrefData; break; };
                case CONSTANTS.CHARACTERISTICS: { return allConstFullDatas.characteristicsData; break; };
                case CONSTANTS.GRP_MEMBERSHIP: { return allConstFullDatas.grpMemberData; break; };
                case CONSTANTS.TRANS_HISTORY: { return allConstFullDatas.transHistoryData; break; };
                case CONSTANTS.ANON_INFO: { return allConstFullDatas.anonInfoData; break; };
                case CONSTANTS.MASTER_ATTEMPT: { return allConstFullDatas.masterAttemptData; break; };
                case CONSTANTS.INTERNAL_BRIDGE: { return allConstFullDatas.internalBridgeData; break; };
                case CONSTANTS.MERGE_HISTORY: { return allConstFullDatas.mergeHistoryData; break; };
                case CONSTANTS.AFFILIATOR: { return allConstFullDatas.affiliatorData; break; };
                case CONSTANTS.SUMMARY_VIEW: { return allConstFullDatas.summaryViewData; break; };
                case CONSTANTS.MASTER_DETAIL: { return allConstFullDatas.masterDetailData; break; };
                case CONSTANTS.QUICK_CASE_SEARCH: { return allConstFullDatas.quickCaseSearchData; break; };
                case CONSTANTS.EMAIL_DOMAINS: { return allConstFullDatas.emailDomains; break; };
                case CONSTANTS.NAICS_CODES: { return allConstFullDatas.naicsCodes; break; };
                case CONSTANTS.CONTACTS_DETAILS: { return allConstFullDatas.contactsDetails; break; };
                case CONSTANTS.ALTERNATEIDS_DETAILS: { return allConstFullDatas.alternateIdsDetails; break; };
                case CONSTANTS.CONST_POTENTIALMERGE: { return allConstFullDatas.constPotentialMergeData; break; };
                case CONSTANTS.CONST_POTENTIALUNMERGE: { return allConstFullDatas.constPotentialUnMergeData; break; };
            }
        },

        clearData: function () {
            allConstDatas = {
                bestSmryData: [],
                constNameData: [],
                constOrgNameData: [],
                constAddressData: [],
                constPhoneData: [],
                constEmailData: [],
                constExtBridgeData: [],
                contBirthData: [],
                constDeathData: [],
                constPrefData: [],
                characteristicsData: [],
                grpMemberData: [],
                transHistoryData: [],
                anonInfoData: [],
                masterAttemptData: [],
                internalBridgeData: [],
                mergeHistoryData: [],
                affiliatorData: [],
                summaryViewData:[],
                masterDetailData: [],
                quickCaseSearchData: [],
                emailDomains: [],
                naicsCodes: [],
                contactsDetails: [],
                alternateIdsDetails: [],
                constPotentialMergeData: [],
                constPotentialUnMergeData: []
            };
            allConstFullDatas = {
                bestSmryData: [],
                constNameData: [],
                constOrgNameData: [],
                constAddressData: [],
                constPhoneData: [],
                constEmailData: [],
                constExtBridgeData: [],
                contBirthData: [],
                constDeathData: [],
                constPrefData: [],
                characteristicsData: [],
                grpMemberData: [],
                transHistoryData: [],
                anonInfoData: [],
                masterAttemptData: [],
                internalBridgeData: [],
                mergeHistoryData: [],
                affiliatorData: [],
                summaryViewData: [],
                masterDetailData: [],
                quickCaseSearchData: [],
                emailDomains: [],
                naicsCodes: [],
                contactsDetails: [],
                alternateIdsDetails: [],
                constPotentialMergeData: [],
                constPotentialUnMergeData: []
            };
        },

        pushAData: function (type, aData) {
            switch (type) {

                case CONSTANTS.CONST_NAME: { allConstDatas.constNameData.push(aData); break; };


            }
        }


    }
}]);

var allGrids = {
    bestSmryGrid: null,
    nameGrid: null,
    orgNameGrid:null,
    addressGrid: null,
    phoneGrid: null,
    emailGrid: null,
    extBridgeGrid: null,
    birthGrid: null,
    deathGrid: null,
    prefGrid: null,
    characteristicsGrid: null,
    grpMembershipGrid: null,
    transHistoryGrid: null,
    anonInfoGrid: null,
    masterAttemptGrid: null,
    internalBridgeGrid: null,
    mergeHistoryGrid: null,
    affiliatorGrid: null,
    summaryViewGrid:null,
    masterDetailGrid: null,
    advCaseSearchGrid: null,
    showDetailGrid: null,
    emailDomainsGrid: null,
    naicsCodesGrid: null,
    contactsDetailsGrid: null,
    alternateIdsDetailsGrid: null,
    potentialMergeGrid: null,
    potentialUnmergeGrid:null
}

newAccMod.factory('constMultiGridService', ['uiGridConstants', 'CONSTANTS', '$rootScope', '$window', function (uiGridConstants, CONSTANTS, $rootScope, $window) {

    var gridColumns = new GridColumns();
    var uiScrollNever = uiGridConstants.scrollbars.NEVER;
    var uiScrollAlways = uiGridConstants.scrollbars.ALWAYS;
    //passing rootscope for toggling filters
    var columnDefs = gridColumns.getGridColumns(uiGridConstants, $rootScope);
    /*  var CONSTANTS = {
          BEST_SMRY: 'BestSmry',
          CONST_NAME: 'ConstName',
          CONST_ADDRESS:'ConstAddress'
      };*/


    var gridOptions = {
        bestSmryGridOption: null,
        nameGridOption: null,
        orgNameGridOption:null,
        addressGridOption: null,
        phoneGridOption: null,
        emailGridOption: null,
        extBridgeGridOption: null,
        birthGridOption: null,
        deathGridOption: null,
        prefGridOption: null,
        characteristicsGridOption: null,
        grpMembershipGridOption: null,
        transHistoryGridOption: null,
        anonInfoGridOption: null,
        masterAttemptGridOption: null,
        internalBridgeGridOption: null,
        mergeHistoryGridOption: null,
        affiliatorGridOption: null,
        summaryViewGridOption:null,
        masterDetailGridOption: null,
        advCaseSearchOption: null,
        showDetailGridOption: null,
        emailDomainsGrid: null,
        naicsCodesGrid: null,
        contactsDetailsGrid: null,
        alternateIdsDetailsGrid: null,
        potentialMergeGrid: null,
        potentialUnmergeGrid:null

    };


    return {

        getGridOptions: function (uiGridConstants, type,pages) {

            if (angular.isUndefined(pages) || typeof pages == 'undefined' || pages == null) {
                pages = 5;
            }

            switch (type) {
                case CONSTANTS.BEST_SMRY: {
                    allGrids.bestSmryGrid = typeof null ? new Grid(columnDefs.bestSmryColDef) : allGrids.bestSmryGrid;
                    gridOptions.bestSmryGridOption = allGrids.bestSmryGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.bestSmryGridOption;
                    break;
                };
                case CONSTANTS.CONST_NAME: {
                    allGrids.nameGrid = typeof null ? new Grid(columnDefs.constNameColDef) : allGrids.nameGrid;
                    gridOptions.nameGridOption = allGrids.nameGrid.getGridOption(uiScrollNever, "best_prsn_nm_ind", true, true, pages, true, false);
                    gridOptions.nameGridOption.columnDefs = this.removeUserAction(gridOptions.nameGridOption.columnDefs);
                    return gridOptions.nameGridOption; break;
                };
                case CONSTANTS.CONST_ORG_NAME: {
                    allGrids.orgNameGrid = typeof null ? new Grid(columnDefs.constOrgNameColDef) : allGrids.orgNameGrid;
                    gridOptions.orgNameGridOption = allGrids.orgNameGrid.getGridOption(uiScrollNever, "best_org_nm_ind", true, true, pages, true, false);
                    gridOptions.orgNameGridOption.columnDefs = this.removeUserAction(gridOptions.orgNameGridOption.columnDefs);
                    return gridOptions.orgNameGridOption; break;
                };
                case CONSTANTS.CONST_ADDRESS: {
                    allGrids.addressGrid = typeof null ? new Grid(columnDefs.constAddressColDef) : allGrids.addressGrid;
                    gridOptions.addressGridOption = allGrids.addressGrid.getGridOption(uiScrollNever, "best_addr_ind", true, true, pages, true, false);
                    gridOptions.addressGridOption.columnDefs = this.removeUserAction(gridOptions.addressGridOption.columnDefs);
                    return gridOptions.addressGridOption; break;
                };
                case CONSTANTS.CONST_PHONE: {
                    allGrids.phoneGrid = typeof null ? new Grid(columnDefs.constPhoneColDef) : allGrids.phoneGrid;
                    gridOptions.phoneGridOption = allGrids.phoneGrid.getGridOption(uiScrollNever, "best_phn_ind", true, true, pages, true, false);
                    gridOptions.phoneGridOption.columnDefs = this.removeUserAction(gridOptions.phoneGridOption.columnDefs);
                    return gridOptions.phoneGridOption; break;
                };
                case CONSTANTS.CONST_EMAIL: {
                    allGrids.emailGrid = typeof null ? new Grid(columnDefs.constEmailColDef) : allGrids.emailGrid;
                    gridOptions.emailGridOption = allGrids.emailGrid.getGridOption(uiScrollNever, "best_email_ind", true, true, pages, true, false);
                    gridOptions.emailGridOption.columnDefs = this.removeUserAction(gridOptions.emailGridOption.columnDefs);
                    return gridOptions.emailGridOption; break;

                };
                case CONSTANTS.CONST_EXT_BRIDGE: {
                    allGrids.extBridgeGrid = typeof null ? new Grid(columnDefs.constExtBridgeColDef) : allGrids.extBridgeGrid;
                    // added by srini for tab security
                    var mergeUnmergePermission = this.getMergeUnmergePermission();
                    var enableSelection = true;
                    if (!mergeUnmergePermission) {
                        enableSelection = false;
                    }

                    gridOptions.extBridgeGridOption = allGrids.extBridgeGrid.getGridOption(uiScrollNever, null, true, true, pages, false, enableSelection);
                    return gridOptions.extBridgeGridOption; break;
                };
                case CONSTANTS.CONST_BIRTH: {
                    allGrids.birthGrid = typeof null ? new Grid(columnDefs.constBirthColDef) : allGrids.birthGrid;
                    gridOptions.birthGridOption = allGrids.birthGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    gridOptions.birthGridOption.columnDefs = this.removeUserAction(gridOptions.birthGridOption.columnDefs);
                    return gridOptions.birthGridOption; break;
                };
                case CONSTANTS.CONST_DEATH: {
                    allGrids.deathGrid = typeof null ? new Grid(columnDefs.constDeathColDef) : allGrids.deathGrid;
                    gridOptions.deathGridOption = allGrids.deathGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    gridOptions.deathGridOption.columnDefs = this.removeUserAction(gridOptions.deathGridOption.columnDefs);
                    return gridOptions.deathGridOption; break;
                };
                case CONSTANTS.CONST_PREF: {
                    allGrids.prefGrid = typeof null ? new Grid(columnDefs.constPrefColDef) : allGrids.prefGrid;
                    gridOptions.prefGridOption = allGrids.prefGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    gridOptions.prefGridOption.columnDefs = this.removeUserAction(gridOptions.prefGridOption.columnDefs);
                    return gridOptions.prefGridOption; break;
                };
                case CONSTANTS.CHARACTERISTICS: {
                    allGrids.characteristicsGrid = typeof null ? new Grid(columnDefs.characteristicsColDef) : allGrids.characteristicsGrid;
                    gridOptions.characteristicsGridOption = allGrids.characteristicsGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    gridOptions.characteristicsGridOption.columnDefs = this.removeUserAction(gridOptions.characteristicsGridOption.columnDefs);
                    return gridOptions.characteristicsGridOption; break;
                };
                case CONSTANTS.GRP_MEMBERSHIP: {
                    allGrids.grpMembershipGrid = typeof null ? new Grid(columnDefs.grpMembershipColDef) : allGrids.grpMembershipGrid;
                    gridOptions.grpMembershipGridOption = allGrids.grpMembershipGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    gridOptions.grpMembershipGridOption.columnDefs = this.removeUserAction(gridOptions.grpMembershipGridOption.columnDefs);
                    return gridOptions.grpMembershipGridOption; break;
                };
                case CONSTANTS.TRANS_HISTORY: {
                    allGrids.transHistoryGrid = typeof null ? new Grid(columnDefs.transHistoryColDef) : allGrids.transHistoryGrid;
                    gridOptions.transHistoryGridOption = allGrids.transHistoryGrid.getGridOption(uiScrollAlways, null, true, true, pages, true, false);
                    return gridOptions.transHistoryGridOption; break;
                };
                case CONSTANTS.ANON_INFO: {
                    allGrids.anonInfoGrid = typeof null ? new Grid(columnDefs.anonInfoColDef) : allGrids.anonInfoGrid;
                    gridOptions.anonInfoGridOption = allGrids.anonInfoGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.anonInfoGridOption; break;
                };
                case CONSTANTS.MASTER_ATTEMPT: {
                    allGrids.masterAttemptGrid = typeof null ? new Grid(columnDefs.masterAttemptColDef) : allGrids.masterAttemptGrid;
                    gridOptions.masterAttemptGridOption = allGrids.masterAttemptGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.masterAttemptGridOption; break;
                };
                case CONSTANTS.INTERNAL_BRIDGE: {
                    allGrids.internalBridgeGrid = typeof null ? new Grid(columnDefs.intBridgeColDef) : allGrids.internalBridgeGrid;
                    gridOptions.internalBridgeGridOption = allGrids.internalBridgeGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.internalBridgeGridOption; break;
                };
                case CONSTANTS.MERGE_HISTORY: {
                    allGrids.mergeHistoryGrid = typeof null ? new Grid(columnDefs.mergeHistoryColDef) : allGrids.mergeHistoryGrid;
                    gridOptions.mergeHistoryGridOption = allGrids.mergeHistoryGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.mergeHistoryGridOption; break;
                };
                case CONSTANTS.AFFILIATOR: {
                    allGrids.affiliatorGrid = typeof null ? new Grid(columnDefs.affiliatorColDef) : allGrids.affiliatorGrid;
                    gridOptions.affiliatorGridOption = allGrids.affiliatorGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.affiliatorGridOption; break;
                };
                case CONSTANTS.SUMMARY_VIEW: {
                    allGrids.summaryViewGrid = typeof null ? new Grid(columnDefs.summaryViewColDef) : allGrids.summaryViewGrid;
                    gridOptions.summaryViewGridOption = allGrids.summaryViewGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.summaryViewGridOption; break;
                };
                case CONSTANTS.MASTER_DETAIL: {
                    allGrids.masterDetailGrid = typeof null ? new Grid(columnDefs.masterDetailColDef) : allGrids.masterDetailGrid;
                    gridOptions.masterDetailGridOption = allGrids.masterDetailGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.masterDetailGridOption; break;
                };
                case CONSTANTS.ADV_CASE_SEARCH: {
                    allGrids.advCaseSearchGrid = typeof null ? new Grid(columnDefs.advCaseSearchColDef) : allGrids.advCaseSearchGrid;
                    gridOptions.advCaseSearchOption = allGrids.advCaseSearchGrid.getGridOption(uiScrollNever, null, true, false,pages);
                    return gridOptions.advCaseSearchOption; break;
                };
                case CONSTANTS.SHOW_DETAIL: {
                    allGrids.showDetailGrid = typeof null ? new Grid(columnDefs.constBirthColDef) : allGrids.showDetailGrid;
                    gridOptions.showDetailGridOption = allGrids.showDetailGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.showDetailGridOption; break;
                };
                case CONSTANTS.EMAIL_DOMAINS: {
                    allGrids.emailDomainsGrid = typeof null ? new Grid(columnDefs.constEmailDomainColDef) : allGrids.emailDomainsGrid;
                    gridOptions.emailDomainsGridOption = allGrids.emailDomainsGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    gridOptions.emailDomainsGridOption.columnDefs = this.removeUserAction(gridOptions.emailDomainsGridOption.columnDefs);
                    return gridOptions.emailDomainsGridOption; break;
                };                
                case CONSTANTS.NAICS_CODES: {
                    allGrids.naicsCodesGrid = typeof null ? new Grid(columnDefs.showNAICSCodesColdef) : allGrids.naicsCodesGrid;
                    gridOptions.naicsCodesGridOption = allGrids.naicsCodesGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.naicsCodesGridOption; break;
                };
                case CONSTANTS.CONTACTS_DETAILS: {
                    allGrids.contactsDetailsGrid = typeof null ? new Grid(columnDefs.showContactsDetailsColdef) : allGrids.contactsDetailsGrid;
                    gridOptions.contactsDetailsGridOption = allGrids.contactsDetailsGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.contactsDetailsGridOption; break;
                };
                case CONSTANTS.ALTERNATEIDS_DETAILS: {
                    allGrids.alternateIdsDetailsGrid = typeof null ? new Grid(columnDefs.showAlternateIdsDetailsColdef) : allGrids.alternateIdsDetailsGrid;
                    gridOptions.alternateIdsDetailsGridOption = allGrids.alternateIdsDetailsGrid.getGridOption(uiScrollNever, null, true, true, pages, true, false);
                    return gridOptions.alternateIdsDetailsGridOption; break;
                };
                case CONSTANTS.CONST_POTENTIALMERGE: {
                    allGrids.potentialMergeGrid = typeof null ? new Grid(columnDefs.constPotentialMergeColDef) : allGrids.potentialMergeGrid;
                    // added by srini for tab security
                    //var mergeUnmergePermission = this.getMergeUnmergePermission();
                    var enableSelection = true;
                    //if (!mergeUnmergePermission) {
                    //    enableSelection = false;
                    //}

                    gridOptions.potentialMergeGridOption = allGrids.potentialMergeGrid.getGridOption(uiScrollNever, null, true, true, pages, false, enableSelection);
                    return gridOptions.potentialMergeGridOption; break;
                };
                case CONSTANTS.CONST_POTENTIALUNMERGE: {
                    allGrids.potentialUnmergeGrid = typeof null ? new Grid(columnDefs.constPotentialUnmergeColDef) : allGrids.potentialUnmergeGrid;
                    // added by srini for tab security
                    //var mergeUnmergePermission = this.getMergeUnmergePermission();
                    var enableSelection = true;
                    //if (!mergeUnmergePermission) {
                    //    enableSelection = false;
                    //}

                    gridOptions.potentialMergeGridOption = allGrids.potentialUnmergeGrid.getGridOption(uiScrollNever, null, true, true, pages, false, enableSelection);
                    return gridOptions.potentialMergeGridOption; break;
                };
            }
        },

        getMultiGridLayout: function (gridOptions, uiGridConstants, result, type) {
            switch (type) {
                case CONSTANTS.BEST_SMRY: {
                    return allGrids.bestSmryGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_NAME: {
                    return allGrids.nameGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_ORG_NAME: {
                    return allGrids.orgNameGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_ADDRESS: {
                    return allGrids.addressGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_PHONE: {
                    return allGrids.phoneGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_EMAIL: {
                    return allGrids.emailGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_EXT_BRIDGE: {
                    return allGrids.extBridgeGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_BIRTH: {
                    return allGrids.birthGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_DEATH: {
                    return allGrids.deathGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_PREF: {
                    return allGrids.prefGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CHARACTERISTICS: {
                    return allGrids.characteristicsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.GRP_MEMBERSHIP: {
                    return allGrids.grpMembershipGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.TRANS_HISTORY: {
                    return allGrids.transHistoryGrid.getGridLayout(gridOptions, result, type, 1); break;
                };
                case CONSTANTS.ANON_INFO: {
                    return allGrids.anonInfoGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.MASTER_ATTEMPT: {
                    return allGrids.masterAttemptGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.INTERNAL_BRIDGE: {
                    return allGrids.internalBridgeGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.MERGE_HISTORY: {
                    return allGrids.mergeHistoryGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.AFFILIATOR: {
                    return allGrids.affiliatorGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.SUMMARY_VIEW: {
                    return allGrids.summaryViewGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.MASTER_DETAIL: {
                    return allGrids.masterDetailGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.ADV_CASE_SEARCH: {
                    return allGrids.advCaseSearchGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.SHOW_DETAIL: {
                    return allGrids.showDetailGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.EMAIL_DOMAINS: {
                    return allGrids.emailDomainsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.NAICS_CODES: {
                    return allGrids.naicsCodesGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONTACTS_DETAILS: {
                    return allGrids.contactsDetailsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.ALTERNATEIDS_DETAILS: {
                    return allGrids.alternateIdsDetailsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CONST_POTENTIALMERGE: {
                    return allGrids.potentialMergeGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };


            }

        },

        getToggleDetails: function ($scope, visible, type) {


            var toggleContents = {
                BestSmry: ['toggleBestSmryDetails', 'toggleBestSmryHeader', 'toggleBestSmryLoader', 'liBestSmry'],
                ConstName: ['toggleConstNameDetails', 'toggleConstNameHeader', 'toggleConstNameLoader', 'liName'],
                ConstOrgName: ['toggleConstOrgNameDetails', 'toggleConstOrgNameHeader', 'toggleConstOrgNameLoader', 'liOrgName'],
                ConstAddress: ['toggleConstAddressDetails', 'toggleConstAddressHeader', 'toggleConstAddressLoader', 'liAddress'],
                ConstPhone: ['toggleConstPhoneDetails', 'toggleConstPhoneHeader', 'toggleConstPhoneLoader', 'liPhone'],
                ConstEmail: ['toggleConstEmailDetails', 'toggleConstEmailHeader', 'toggleConstEmailLoader', 'liEmail'],
                ConstExtBrige: ['toggleConstExtBridgeDetails', 'toggleConstExtBridgeHeader', 'toggleConstExtBridgeLoader', 'liExtBridge'],
                ConstBirth: ['toggleConstBirthDetails', 'toggleConstBirthHeader', 'toggleConstBirthLoader', 'liBirth'],
                ConstDeath: ['toggleConstDeathDetails', 'toggleConstDeathHeader', 'toggleConstDeathLoader', 'liDeath'],
                ConstPref: ['toggleConstPrefDetails', 'toggleConstPrefHeader', 'toggleConstPrefLoader', 'liContactPref'],
                //ConstPref: ['toggleConstPrefDetails', 'toggleConstPrefHeader', 'toggleConstPrefLoader'],
                Character: ['toggleCharacteristicsDetails', 'toggleCharacteristicsHeader', 'toggleCharacteristicsLoader', 'liCharacteristics'],
                GrpMembership: ['toggleGrpMembershipDetails', 'toggleGrpMembershipHeader', 'toggleGrpMembershipLoader', 'liMembership'],
                TransHistory: ['toggleTransHistoryDetails', 'toggleTransHistoryHeader', 'toggleTransHistoryLoader', 'liTransHistory'],
                AnonInfo: ['toggleAnonInfoDetails', 'toggleAnonInfoHeader', 'toggleAnonInfoLoader', 'liAnonyInfo'],
                MasterAttempt: ['toggleMasterAttemptDetails', 'toggleMasterAttemptHeader', 'toggleMasterAttemptLoader', 'liMasterAttempt'],
                InternalBridge: ['toggleInternalBridgeDetails', 'toggleInternalBridgeHeader', 'toggleInternalBridgeLoader', 'liIntBridge'],
                MergeHistory: ['toggleMergeHistoryDetails', 'toggleMergeHistoryHeader', 'toggleMergeHistoryLoader', 'liMergeHistory'],
                Affiliator: ['toggleAffiliatorDetails', 'toggleAffiliatorHeader', 'toggleAffiliatorLoader', 'liAffiliator'],
                SummaryView: ['toggleSummaryViewDetails', 'toggleSummaryViewHeader', 'toggleSummaryViewLoader', 'liSmryView'],
                MasterDetail: ['toggleMasterDetailDetails', 'toggleMasterDetailHeader', 'toggleMasterDetailLoader', 'liMstrDetail'],
                EmailDomain: ['toggleEmailDomainDetails', 'toggleEmailDomainHeader', 'toggleConstEmailDomainLoader', 'liEmailDomainDetails'],
                NaicsCodes: ['toggleNaicsCodesDetails', 'toggleNaicsCodesHeader', 'toggleConstNaicsCodeLoader', 'liNAICSCodes'],
                Contacts: ['toggleContactsDetails', 'toggleContactsHeader', 'toggleConstContactsLoader', 'liContactsDetails'],
                AlternateIds: ['toggleAlternateIdsDetails', 'togglAlternateIdsHeader', 'toggleConstAlternateIdsLoader', 'liAlternateIdsDetails']
            };

            var toggle = {
                getToggleType: function (toggleType) {
                    if (visible) {
                        $scope[toggleType[0]] = { "display": "block" };
                        $scope[toggleType[1]] = { "display": "block" };
                        $scope[toggleType[2]] = { "display": "none" };
                        $scope[toggleType[3]] = "";
                    }
                    else {
                        $scope[toggleType[0]] = { "display": "none" };
                        $scope[toggleType[1]] = { "display": "none" };
                        $scope[toggleType[2]] = { "display": "none" };
                        $scope[toggleType[3]] = "";
                    }
                }
            };

            switch (type) {
                case CONSTANTS.BEST_SMRY: { return toggle.getToggleType(toggleContents.BestSmry); break; };
                case CONSTANTS.CONST_NAME: { return toggle.getToggleType(toggleContents.ConstName); break; };
                case CONSTANTS.CONST_ORG_NAME: { return toggle.getToggleType(toggleContents.ConstOrgName); break; };
                case CONSTANTS.CONST_ADDRESS: { return toggle.getToggleType(toggleContents.ConstAddress); break; };
                case CONSTANTS.CONST_PHONE: { return toggle.getToggleType(toggleContents.ConstPhone); break; };
                case CONSTANTS.CONST_EMAIL: { return toggle.getToggleType(toggleContents.ConstEmail); break; };
                case CONSTANTS.CONST_EXT_BRIDGE: { return toggle.getToggleType(toggleContents.ConstExtBrige); break; };
                case CONSTANTS.CONST_BIRTH: { return toggle.getToggleType(toggleContents.ConstBirth); break; };
                case CONSTANTS.CONST_DEATH: { return toggle.getToggleType(toggleContents.ConstDeath); break; };
                case CONSTANTS.CONST_PREF: { return toggle.getToggleType(toggleContents.ConstPref); break; };
                case CONSTANTS.CHARACTERISTICS: { return toggle.getToggleType(toggleContents.Character); break; };
                case CONSTANTS.GRP_MEMBERSHIP: { return toggle.getToggleType(toggleContents.GrpMembership); break; };
                case CONSTANTS.TRANS_HISTORY: { return toggle.getToggleType(toggleContents.TransHistory); break; };
                case CONSTANTS.ANON_INFO: { return toggle.getToggleType(toggleContents.AnonInfo); break; };
                case CONSTANTS.MASTER_ATTEMPT: { return toggle.getToggleType(toggleContents.MasterAttempt); break; };
                case CONSTANTS.INTERNAL_BRIDGE: { return toggle.getToggleType(toggleContents.InternalBridge); break; };
                case CONSTANTS.MERGE_HISTORY: { return toggle.getToggleType(toggleContents.MergeHistory); break; };
                case CONSTANTS.AFFILIATOR: { return toggle.getToggleType(toggleContents.Affiliator); break; };
                case CONSTANTS.SUMMARY_VIEW: { return toggle.getToggleType(toggleContents.SummaryView); break; };
                case CONSTANTS.MASTER_DETAIL: { return toggle.getToggleType(toggleContents.MasterDetail); break; };
                case CONSTANTS.EMAIL_DOMAINS: { return toggle.getToggleType(toggleContents.EmailDomain); break; };
                case CONSTANTS.NAICS_CODES: { return toggle.getToggleType(toggleContents.NaicsCodes); break; };
                case CONSTANTS.CONTACTS_DETAILS: { return toggle.getToggleType(toggleContents.Contacts); break; };
                case CONSTANTS.ALTERNATEIDS_DETAILS: { return toggle.getToggleType(toggleContents.AlternateIds); break; };
            }

        },
        //used in this service only to remove userAction column
        removeUserAction: function (columnDefs) {          
            if (this.getTabDenyPermission()) {
                    columnDefs.splice(columnDefs.length-1, 1);              
            }

            return columnDefs;
        },
        getUserAllPermission: function () {
            if (!angular.isUndefined($window.localStorage['Main-userPermissions'])) {
                if (typeof $window.localStorage['Main-userPermissions'] == 'string')
                    return JSON.parse($window.localStorage['Main-userPermissions']);
                else
                    return JSON.parse($window.localStorage['Main-userPermissions']);
                //console.log(permissions);               

            }
            return null;
        },

        getTabDenyPermission: function () {
            var permissions = this.getUserAllPermission();
            if (permissions != null) {
                if (permissions.constituent_tb_access == "R") {
                    return true;
                }
            }
            return false;
        },
        getMergeUnmergePermission: function () {
            var permissions = this.getUserAllPermission();
            if (permissions != null) {
                if (permissions.has_merge_unmerge_access == "1") {
                    return true;
                }
            }
            return false;
        }

    }
}]);


//Service to Get Details only
newAccMod.factory('constituentApiService', ['$http', 'CONSTANTS', function ($http, CONSTANTS) {
    var urls = {};
    urls[CONSTANTS.CONST_ADDRESS] = BasePath + 'Test/GetConstituentAddress/';
    urls[HOME_CONSTANTS.OLD_MASTER_IDS] = BasePath + 'Test/getconstituentoldmasters/';
    urls[CONSTANTS.CONST_NAME] = BasePath + 'Test/GetConstituentPersonName/';
    urls[CONSTANTS.MASTER_DETAIL] = BasePath + 'Test/GetConstituentMasterDetails/';
    urls[HOME_CONSTANTS.PRIVATE] = BasePath + 'Test/getconstituentprivateinformation/';
    urls[CONSTANTS.CONST_ORG_NAME] = BasePath + 'Test/GetConstituentOrgName/';
    urls[CONSTANTS.BEST_SMRY] = BasePath + 'Test/ConstituentARCBest/';
    urls[CONSTANTS.CONST_PHONE] = BasePath + 'Test/GetConstituentPhone/';
    urls[CONSTANTS.CONST_EMAIL] = BasePath + 'Test/GetConstituentEmail/';
    urls[CONSTANTS.CONST_EXT_BRIDGE] = BasePath + 'Test/GetConstituentExternalBridge/';
    urls[CONSTANTS.CONST_BIRTH] = BasePath + 'Test/GetConstituentBirth/';
    urls[CONSTANTS.CONST_DEATH] = BasePath + 'Test/GetConstituentDeath/';
    urls[CONSTANTS.CONST_PREF] = BasePath + 'Test/GetConstituentContactPreference/';
    urls[CONSTANTS.CHARACTERISTICS] = BasePath + 'Test/GetConstituentCharacteristics/';
    urls[CONSTANTS.GRP_MEMBERSHIP] = BasePath + 'Test/GetConstituentGroupMembership/';
    urls[CONSTANTS.TRANS_HISTORY] = BasePath + 'Test/GetConstituentTransactionHistory/';
    urls[CONSTANTS.ANON_INFO] = BasePath + 'Test/getconstituentanonymousinformation/';
    urls[CONSTANTS.MASTER_ATTEMPT] = BasePath + 'Test/GetConstituentMasteringAttempts/';
    urls[CONSTANTS.INTERNAL_BRIDGE] = BasePath + 'Test/GetConstituentInternalBridge/';
    urls[CONSTANTS.MERGE_HISTORY] = BasePath + 'Test/getconstituentmergehistory/';
    urls[CONSTANTS.AFFILIATOR] = BasePath + 'Test/getconstituentorgaffiliators/';
    urls[CONSTANTS.SUMMARY_VIEW] = BasePath + 'Test/getconstituentsummary/';
    urls[CONSTANTS.MENU_PREF] = BasePath + 'home/GetMenuJson';
    urls[CART.CLEAR] = BasePath + "home/ClearCart";
    urls[HOME_CONSTANTS.EXPORT_SEARCH] = BasePath + "Test/exportExcel/";
    //Added for the new sections
    urls[CONSTANTS.EMAIL_DOMAINS] = BasePath + 'Test/GetEmailDomains/';
    urls[CONSTANTS.NAICS_CODES] = BasePath + 'Test/GetNaicsCodes/';
    urls[CONSTANTS.CONTACTS_DETAILS] = BasePath + 'Test/GetContacts/';
    urls[CONSTANTS.ALTERNATEIDS_DETAILS] = BasePath + 'Test/GetAlternateIds/';
    urls[CONSTANTS.CONST_POTENTIALMERGE] = BasePath + 'Test/getPotentialMergesAccountDetails/';
    urls[CONSTANTS.CONST_POTENTIALUNMERGE] = BasePath + 'Test/GetConstituentExternalBridge/';
    var getApiData = function (param, type) {
        var url = urls[type];
        return $http.get(url + param + "", {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            console.log("Results");
            console.log(result);
            return result;
        }).error(function (result) {
            console.log(result);
            return result;
        });
    };


    return {
        getApiDetails: function (param, type) {
            return getApiData(param, type);
        }
    }
}]);




var CRUD_CONSTANTS = {
    EDIT_SUCCESS_MESSAGE: 'The record was successfully edited.',
    DELETE_SUCCESS_MESSAGE:'The Record was successfully deleted.',
    SUCCESS_MESSAGE: 'The record was successfully inserted!',
    MERGE_SUCCESS_MESSAGE: 'Records were successfully merged',
    UNMERGE_SUCCESS_MESSAGE :'Records were successfully unmerged',
    MERGE_FAILURE_MESSAGE: 'Failed to merge records',
    UNMERGE_FAILURE_MESSAGE: 'Failed to unmerge records',
    FAILURE_MESSAGE: 'The record was not inserted.',
    EDIT_FAILURE_MESSAGE: 'The record was not edited',
    DELETE_FAILURE_MESSAGE:'The record was not deleted',
    SUCCESS_REASON: ' Transaction Key: ',
    FAIULRE_REASON: 'It looks like there is a similar record in the database. Please review.',
    SUCCESS_CONFIRM: 'Success!',
    FAILURE_CONFIRM: 'Failed!',
    SOURCE_SYS: "STRX",
    ROW_CODE: 'I',
    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',
    SERVICE_TIMEOUT: 'Timed out',
    SERVICE_TIMEOUT_CONFIRM: 'Error: Timed Out',
    SERVICE_TIMEOUT_MESSAGE: 'The service/database timed out. Please try again after some time.',
    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM:"Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    CASE_MESSAGE:"The existing case number with which the transaction affiliated : ",
    DEFAULT_NOTE: 'Request from Constituent - Data Correction',
    PROCEDURE: {
        SUCCESS: 'Success',
        DUPLICATE: 'duplicate',
        NOT_PRESENT: 'The original record is not present.'
    },
    NOTES: [
        "Request from Constituent - Data Correction", "Request from Constituent - Change of Information", "Request from Chapter - Data Correction",
        "Request from Chapter - Change of Information", "Assessment for master constituent emails"
    ],
    PHONE: {
        ADD: "AddPhone",
        EDIT: "EditPhone",
        DELETE: "DeletePhone"
    },
    EMAIL: {
        ADD: "AddEmail",
        EDIT: "EditEmail",
        DELETE:"DeleteEmail"
    },
    DEATH: {
        ADD: "AddDeath",
        EDIT: "EditDeath",
        DELETE: "DeleteDeath"
    },
    BIRTH:{
        ADD: "AddBirth",
        EDIT: "EditBirth",
        DELETE:"DeleteBirth"
    },
    ADDRESS: {
        ADD: "AddAddress",
        EDIT: "EditAddress",
        DELETE:"DeleteAddress"
    },
    CONTACT_PREF: {
        ADD: "AddContactPref",
        EDIT: "EditContactPref",
        DELETE: "DeleteContactPref"
    },
    GRP_MEMBERSHIP: {
        ADD: "AddGrpMembership",
        EDIT: "EditGrpMembership",
        DELETE: "DeleteGrpMembership"
    },

    CHARACTERISITICS: {
        ADD: "AddCharacter",
        EDIT: "EditCharacter",
        DELETE: "DeleteCharacter"
    },
    NAME :{
        ADD:"AddName",
        EDIT:"EditName",
        DELETE:"DeleteName"
    },
    ORG_NAME:{
        ADD: "AddOrgName",
        EDIT: "EditOrgName",
        DELETE: "DeleteOrgName"
    },
    AFFILIATOR:{
        ADD: "AddAffiliator",
        DELETE: "DeleteAffiliator"
    },
    EMAIL_DOMAINS: {
        ADD: "AddEmailDomain",
        DELETE: "DeleteEmailDomain"
    },
    NAICS_CODES: {
        ADD: "AddNaicsCodes"
    },
    MERGE_RECORD: "MergeRecord",
    MERGE_COMPARE: "MergeCompare",
    MERGE_CONFLICT_RECORD:"MergeConflictRecord",
    UNMERGE_RECORD: "UnmergeRecord",
    UNMERGE_COMPARE: "UnmergeCompare"


};

//var CART_MERGE = "CartMerge";
var CART = {
    CLEAR: "ClearCart",
    CONFIRMATION_MESSAGE: "Success!",
    REASON: "Selected Items were added to the cart successfully.Click on view cart to see the records in the cart",
    NEW_REASON :  "Selected Items were added to the cart successfully.Click on view cart to see the records in the cart",
    MINIMUM_RECORDS_ERROR: "Please select at least 2 records.",
    MINIMUM_RECORDS_ALERT: "ALERT!",
    CLEARED: "Cleared!",
    CLEAR_REASON: "The cart has been cleared.",
    SELECT_MESSAGE:"Please select at least one Record"
}






//Service for CRUD Operations only
newAccMod.factory('constituentCRUDapiService', ['$http', function ($http) {

    var CRUDapiUrls = {};
    CRUDapiUrls["WriteMenuPref"] = BasePath + "home/SaveMenuJSON"; //  Saving the menu preferences in the constituent details (multi selection)

    //ADD New API URLS for CRUD operations below
    CRUDapiUrls[CRUD_CONSTANTS.NAME.ADD] = BasePath + "Test/addconstituentpersonname";
    CRUDapiUrls[CRUD_CONSTANTS.NAME.EDIT] = BasePath + "Test/updateconstituentpersonname";
    CRUDapiUrls[CRUD_CONSTANTS.NAME.DELETE] = BasePath + "Test/deleteconstituentpersonname";

    CRUDapiUrls[CRUD_CONSTANTS.ORG_NAME.ADD] = BasePath + "Test/addconstituentorgname";
    CRUDapiUrls[CRUD_CONSTANTS.ORG_NAME.EDIT] = BasePath + "Test/updateconstituentorgname";
    CRUDapiUrls[CRUD_CONSTANTS.ORG_NAME.DELETE] = BasePath + "Test/deleteconstituentorgname";

    CRUDapiUrls[CRUD_CONSTANTS.PHONE.ADD] = BasePath + "Test/addconstituentphone";
    CRUDapiUrls[CRUD_CONSTANTS.PHONE.EDIT] = BasePath + "Test/updateconstituentphone";
    CRUDapiUrls[CRUD_CONSTANTS.PHONE.DELETE] = BasePath + "Test/deleteconstituentphone";

    CRUDapiUrls[CRUD_CONSTANTS.EMAIL.ADD] = BasePath + "Test/addconstituentemail";
    CRUDapiUrls[CRUD_CONSTANTS.EMAIL.EDIT] = BasePath + "Test/updateconstituentemail";
    CRUDapiUrls[CRUD_CONSTANTS.EMAIL.DELETE] = BasePath + "Test/deleteconstituentemail";

    CRUDapiUrls[CRUD_CONSTANTS.DEATH.ADD] = BasePath + "Test/addconstituentdeath";
    CRUDapiUrls[CRUD_CONSTANTS.DEATH.EDIT] = BasePath + "Test/updateconstituentdeath";
    CRUDapiUrls[CRUD_CONSTANTS.DEATH.DELETE] = BasePath + "Test/deleteconstituentdeath";

    CRUDapiUrls[CRUD_CONSTANTS.BIRTH.ADD] = BasePath + "Test/addconstituentbirth";
    CRUDapiUrls[CRUD_CONSTANTS.BIRTH.EDIT] = BasePath + "Test/updateconstituentbirth";
    CRUDapiUrls[CRUD_CONSTANTS.BIRTH.DELETE] = BasePath + "Test/deleteconstituentbirth";

    CRUDapiUrls[CRUD_CONSTANTS.ADDRESS.ADD] = BasePath + "Test/addconstituentaddress";
    CRUDapiUrls[CRUD_CONSTANTS.ADDRESS.EDIT] = BasePath + "Test/updateconstituentaddress";
    CRUDapiUrls[CRUD_CONSTANTS.ADDRESS.DELETE] = BasePath + "Test/deleteconstituentaddress";

    CRUDapiUrls[CRUD_CONSTANTS.CONTACT_PREF.ADD] = BasePath + "Test/addconstituentcontactprefc";
    CRUDapiUrls[CRUD_CONSTANTS.CONTACT_PREF.EDIT] = BasePath + "Test/updateconstituentcontactprefc";
    CRUDapiUrls[CRUD_CONSTANTS.CONTACT_PREF.DELETE] = BasePath + "Test/deleteconstituentcontactprefc";

    CRUDapiUrls[CRUD_CONSTANTS.GRP_MEMBERSHIP.ADD] = BasePath + "Test/addconstituentchaptergrp";
    CRUDapiUrls[CRUD_CONSTANTS.GRP_MEMBERSHIP.EDIT] = BasePath + "Test/updateconstituentchaptergrp";
    CRUDapiUrls[CRUD_CONSTANTS.GRP_MEMBERSHIP.DELETE] = BasePath + "Test/deleteconstituentchaptergrp";

    CRUDapiUrls[CRUD_CONSTANTS.CHARACTERISITICS.ADD] = BasePath + "Test/addconstituentcharacteristics";
    CRUDapiUrls[CRUD_CONSTANTS.CHARACTERISITICS.EDIT] = BasePath + "Test/updateconstituentcharacteristics";
    CRUDapiUrls[CRUD_CONSTANTS.CHARACTERISITICS.DELETE] = BasePath + "Test/deleteconstituentcharacteristics";

    CRUDapiUrls[CRUD_CONSTANTS.AFFILIATOR.ADD] = BasePath + "Test/addorgaffiliators";
    CRUDapiUrls[CRUD_CONSTANTS.AFFILIATOR.DELETE] = BasePath + "Test/deleteorgaffiliators";
    //Added for new sections
    CRUDapiUrls[CRUD_CONSTANTS.EMAIL_DOMAINS.ADD] = BasePath + "Test/addemaildomain";
    CRUDapiUrls[CRUD_CONSTANTS.EMAIL_DOMAINS.DELETE] = BasePath + "Test/deleteemaildomain";

    CRUDapiUrls[CRUD_CONSTANTS.NAICS_CODES.ADD] = BasePath + "Test/addnaicscode";

    //get the cart result for merge
    CRUDapiUrls[CRUD_CONSTANTS.MERGE_COMPARE] = BasePath + "Test/getconstituentcomparedata";
    CRUDapiUrls[CRUD_CONSTANTS.MERGE_RECORD] = BasePath + "Test/postmergedata";
    CRUDapiUrls[CRUD_CONSTANTS.MERGE_CONFLICT_RECORD] = BasePath + "Test/postmergeconflictdata";

    //get the cart result for merge
    CRUDapiUrls[CRUD_CONSTANTS.UNMERGE_COMPARE] = BasePath + "home/compareUnmerge";
    CRUDapiUrls[CRUD_CONSTANTS.UNMERGE_RECORD] = BasePath + "Test/postunmergedata";

    //CRUDapiUrls[HOME_CONSTANTS.CONST_BIRTH] = BasePath + "Test/showbirthdetails";
    



    var getCRUDOperationData = function (postParams, requestType) {
        return $http.post(CRUDapiUrls[requestType], JSON.stringify(postParams), {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            return result;
        }).error(function (result) {
            return result;
        });
    }

    return {
        getCRUDOperationResult: function (PostParams, requestType) {
            return getCRUDOperationData(PostParams, requestType);
        }
    }
}]);

newAccMod.factory('constClearDataService', ['constMultiDataService', 'constituentDataServices', function (constMultiDataService, constituentDataServices) {

    return {
        clearMultiData: function () {
            constMultiDataService.clearData();
            return;
        },
        clearSearchData: function () {
            constituentDataServices.clearSearchData();
        }
    }

}]);


newAccMod.factory('constRecordType', function () {
    var type = null;

    return {
        setRecordType: function (t) { type = t;  },
        getRecordType: function () { return type; },
        clearRecordTyp: function () { type = null; },
       
    }
   

});

newAccMod.factory('globalServices', function () {
    var caseTabCaseNo = null;
    caseInfoData = {};
    return {
        getCaseTabCaseNo: function () {

            return caseTabCaseNo;
        },
        setCaseTabCaseNo: function (caseNo) {
            caseTabCaseNo = caseNo;
        },
        clearCaseTabCaseNo: function () {
            caseTabCaseNo = null;
        },
        setCaseInfoData: function (obj) {
            caseInfoData = obj;
        },

        getCaseInfoData: function () {
            return caseInfoData;
        }
    }
    

});
newAccMod.factory("ConstituentEmailDomainService", ['$http',
    function ($http) {
        //base Path
        var BasePath = $("base").first().attr("href");

        return {
            //Service method to perform search

            getEmailDomainDistinct: function () {
                return $http.get(BasePath + "Test/GetEmailDomainList",
                    {
                        headers: {
                            "Content-Type": "Application/JSON",
                            "Accept": "Application/JSON"
                        }
                    }).success(function (result) {
                        return result;

                    }).error(function (result) {
                        return result;
                    });
            }
        }

    }
]);
