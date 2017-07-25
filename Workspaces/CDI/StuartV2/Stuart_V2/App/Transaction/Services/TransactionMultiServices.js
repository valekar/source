var allTransGrids = {
    bestSmryGrid: null,
    transPersonNameGrid: null,
    transOrgNameGrid: null,
    transAddressGrid: null,
    transPhoneGrid: null,
    transEmailGrid: null,
    transEmailUploadGrid: null,
    extBridgeGrid: null,
    transBirthGrid: null,
    transDeathGrid: null,
    transContactPrefGrid: null,
    transCharacteristicsGrid: null,
    transGroupMembershipUploadGrid: null,
    transHistoryGrid: null,
    anonInfoGrid: null,
    masterAttemptGrid: null,
    transMergeGrid: null,
    internalBridgeGrid: null,
    mergeHistoryGrid: null,
    transOrgTransformGrid: null,
    transAffiliatorGrid: null,
    transAffiliatorHierarchyGrid: null,
    transAffiliatorTagsGrid: null,
    summaryViewGrid: null,
    masterDetailGrid: null,
    advCaseSearchGrid: null,

    unmergeRequestLogGrid: null,
    unmergeProcessLogGrid: null,

    //added by srini for cem surfacing functionalities
    transGroupMembershipUploadGrid: null,
    transCemDncGrid: null,
    transCemMsgPrefGrid: null,
    transCemPrefLocGrid: null,
    transCemDncUpldGrid: null,
    transCemMsgPrefUpldGrid:null
    //adding ends here
}


angular.module('transaction').constant('TRANS_CONSTANTS', {
    BEST_SMRY: 'BestSmry',
    TRANS_PERSON_NAME: 'PersonName',
    TRANS_ORG_NAME: 'OrgName',
    TRANS_ADDRESS: 'TransAddress',
    TRANS_PHONE: 'TransPhone',
    TRANS_EMAIL: 'TransEmail',
    TRANS_EMAIL_UPLOAD: 'TransEmailUpload',
    CONST_EXT_BRIDGE: 'ConstExtBridge',
    TRANS_BIRTH: 'TransBirth',
    TRANS_DEATH: 'TransDeath',
    TRANS_CONTACT_PREF: 'TransContactPref',
    TRANS_CHARACTERISTICS: 'TransCharacteristics',
    
    GRP_MEMBERSHIP: 'GrpMembership',
    CONST_HISTORY: "TransHistory",
    ANON_INFO: 'AnonInfo',
    MASTER_ATTEMPT: 'MasterAttempt',
    INTERNAL_BRIDGE: 'InternalBridge',
    TRANS_MERGE: 'TransMerge',
    MERGE_HISTORY: 'MergeHistory',
    MENU_PREF: "MenuPref",
    CONST_ORG_NAME: "ConstOrgName",
    TRANS_ORG_TRANSFORM: "TransOrgTransform",
    TRANS_AFFILIATOR: "TransAffiliator",
    TRANS_AFFILIATOR_HIERARCHY: "TransAffiliatorHierarchy",
    TRANS_AFFILIATOR_TAGS: "TransAffiliatorTags",
    SUMMARY_VIEW: "SummaryView",
    MASTER_DETAIL: "MasterDetail",
    ADV_CASE_SEARCH: "AdvCaseSearch",
    QUICK_CASE_SEARCH: "QuickCaseSearch",

    TRANS_UNMERGE_REQUEST_LOG: "UnmergeRequestLog",
    TRANS_UNMERGE_PROCESS_LOG: "UnmergeProcessLog",

    // added by srini for cem surfacing functionalities
    TRANS_GROUP_UPLOAD: 'TransGroupMembershipUpload',
    TRANS_CEM_DNC_DETAILS: 'TransCemDncDetails',
    TRANS_CEM_MSG_PREF_DETAILS: 'TransCemMsgPrefDetails',
    TRANS_CEM_PREF_LOC_DETAILS: 'TransPrefLocDetails',
    TRANS_CEM_DNC_UPLD_DETAILS: 'TransCemDncUploadDetails',
    TRANS_CEM_MSG_PREF_UPLD_DETAILS: 'TransCemMsgPrefUploadDetails'
    //adding ends here

});


angular.module('transaction').factory('TransactionMultiDataServices', ['TRANS_CONSTANTS', function (TRANS_CONSTANTS) {

    var allTransDatas = {
        bestSmryData: [],
        transPersonNameData: [],
        transOrgNameData: [],
        transAddressData: [],
        transPhoneData: [],
        transEmailData: [],
        transEmailUploadData: [],
        constExtBridgeData: [],
        transBirthData: [],
        transDeathData: [],
        transContactPrefData: [],
        transCharacteristicsData: [],
        transGroupMembershipUploadData: [],
        transHistoryData: [],
        anonInfoData: [],
        masterAttemptData: [],
        internalBridgeData: [],
        transMergeData: [],
        mergeHistoryData: [],
        transOrgTransformData: [],
        transAffiliatorHierarchyData: [],
        transAffiliatorTagsData: [],
        transAffiliatorData: [],
        summaryViewData: [],
        masterDetailData: [],
        quickCaseSearchData: [],

        unmergeProcessLogData: [],
        unmergeRequestLogData: []
    };

    return {

        getData: function (type) {
            switch (type) {

                case TRANS_CONSTANTS.TRANS_PHONE: { return allTransDatas.transPhoneData; break; };
                case TRANS_CONSTANTS.TRANS_PERSON_NAME: { return allTransDatas.transPersonNameData; break; };
                case TRANS_CONSTANTS.TRANS_BIRTH: { return allTransDatas.transBirthData; break; };
                case TRANS_CONSTANTS.TRANS_DEATH: { return allTransDatas.transDeathData; break; };
                case TRANS_CONSTANTS.TRANS_ORG_NAME: { return allTransDatas.transOrgNameData; break; };
                case TRANS_CONSTANTS.TRANS_ADDRESS: { return allTransDatas.transAddressData; break; };
                case TRANS_CONSTANTS.TRANS_CHARACTERISTICS: { return allTransDatas.transCharacteristicsData; break; };
                case TRANS_CONSTANTS.TRANS_EMAIL: { return allTransDatas.transEmailData; break; };
                case TRANS_CONSTANTS.TRANS_EMAIL_UPLOAD: { return allTransDatas.transEmailUploadData; break; };

                case TRANS_CONSTANTS.TRANS_GROUP_UPLOAD: { return allTransDatas.transGroupMembershipUploadData; break; };


                case TRANS_CONSTANTS.TRANS_MERGE: { return allTransDatas.transOrgTransformData; break; };
                case TRANS_CONSTANTS.TRANS_MERGE: { return allTransDatas.transMergeData; break; };
                case TRANS_CONSTANTS.TRANS_CONTACT_PREF: { return allTransDatas.transContactPrefData; break; };
                case TRANS_CONSTANTS.TRANS_AFFILIATOR_TAGS: { return allTransDatas.transAffiliatorTagsData; break; };
                case TRANS_CONSTANTS.TRANS_AFFILIATOR_HIERARCHY: { return allTransDatas.transAffiliatorHierarchyData; break; };
                case TRANS_CONSTANTS.TRANS_AFFILIATOR: { return allTransDatas.transAffiliatorData; break; };
                case TRANS_CONSTANTS.TRANS_UNMERGE_REQUEST_LOG: { return allTransDatas.unmergeRequestLogData; break; }
                case TRANS_CONSTANTS.TRANS_UNMERGE_PROCESS_LOG: { return allTransDatas.unmergeProcessLogData; break; }
            }
        },

        setData: function (resultData, type) {
            switch (type) {

                case TRANS_CONSTANTS.TRANS_PHONE: { allConstDatas.transPhoneData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_PERSON_NAME: { allTransDatas.transPersonNameData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_BIRTH: { allTransDatas.transBirthData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_DEATH: { allTransDatas.transDeathData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_ORG_NAME: { allTransDatas.transOrgNameData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_ADDRESS: { allTransDatas.transAddressData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_EMAIL: { allTransDatas.transEmailData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_EMAIL_UPLOAD: { allTransDatas.transEmailUploadData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_CONTACT_PREF: { allTransDatas.transContactPrefData = resultData; break; };


                case TRANS_CONSTANTS.TRANS_GROUP_UPLOAD: { allTransDatas.transOrgTransformData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_GROUP_UPLOAD: { allTransDatas.transGroupMembershipUploadData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_MERGE: { allTransDatas.transMergeData = resultData; break; };

                case TRANS_CONSTANTS.TRANS_CHARACTERISTICS: { allTransDatas.transCharacteristicsData = resultData; break; };

                case TRANS_CONSTANTS.TRANS_AFFILIATOR_TAGS: { allTransDatas.transAffiliatorTagsData = resultData; break; };

                case TRANS_CONSTANTS.TRANS_AFFILIATOR_HIERARCHY: { allTransDatas.transAffiliatorHierarchyData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_AFFILIATOR: { allTransDatas.transAffiliatorData = resultData; break; };
                case TRANS_CONSTANTS.TRANS_UNMERGE_REQUEST_LOG: { allTransDatas.unmergeRequestLogData = resultData; break; }
                case TRANS_CONSTANTS.TRANS_UNMERGE_PROCESS_LOG: { allTransDatas.unmergeProcessLogData = resultData; break; }
            }
        },

        clearData: function () {
            allTransDatas = {
                bestSmryData: [],
                transPersonNameData: [],
                transOrgNameData: [],
                transAddressData: [],
                transPhoneData: [],
                transEmailData: [],
                transEmailUploadData: [],
                constExtBridgeData: [],
                transBirthData: [],
                transDeathData: [],
                transContactPrefData: [],
                transCharacteristicsData: [],
                transGroupMembershipUploadData: [],
                transHistoryData: [],
                anonInfoData: [],
                masterAttemptData: [],
                internalBridgeData: [],
                transMergeData: [],
                mergeHistoryData: [],
                transOrgTransformData: [],
                transAffiliatorTagsData: [],
                transAffiliatorHierarchyData: [],
                transAffiliatorData: [],
                summaryViewData: [],
                masterDetailData: [],
                quickCaseSearchData: [],

                unmergeRequestLogData: [],
                unmergeProcessLogData: []
            };
        },

        pushAData: function (type, aData) {
            switch (type) {
                case TRANS_CONSTANTS.TRANS_PERSON_NAME: { allTransDatas.transPersonNameData.push(aData); break; };
            }
        }
    }

}]);


angular.module('transaction').factory('TransactionMultiGridServices', ['uiGridConstants', 'TRANS_CONSTANTS', '$rootScope', function (uiGridConstants, TRANS_CONSTANTS, $rootScope) {

    var transGridColumns = new TransGridColumns();
    var uiScrollNever = uiGridConstants.scrollbars.NEVER;
    var uiScrollAlways = uiGridConstants.scrollbars.ALWAYS;

    //passing rootscope for toggling filters
    var columnDefs = transGridColumns.getGridColumns(uiGridConstants, $rootScope);

    var gridOptions = {
        bestSmryGridOption: null,
        transPersonGridOption: null,
        transNameGridOption: null,
        transAddressGridOption: null,
        transPhoneGridOption: null,
        transEmailGridOption: null,
        transEmailUploadGridOption: null,
        extBridgeGridOption: null,
        transBirthGridOption: null,
        transDeathGridOption: null,
        transContactPrefGridOption: null,
        transCharacteristicsGridOption: null,
        
        transHistoryGridOption: null,
        anonInfoGridOption: null,
        masterAttemptGridOption: null,
        internalBridgeGridOption: null,
        transMergeGridOption: null,
        mergeHistoryGridOption: null,
        transOrgTransformGridOption: null,
        transAffiliatorHierarchyGridOption: null,
        transAffiliatorTagsGridOption: null,
        transAffiliatorGridOption: null,
        summaryViewGridOption: null,
        masterDetailGridOption: null,
        advCaseSearchOption: null,

        unmergeRequestLogGridOption: null,
        unmergeProcessLogGridOption: null,
        //added by srini for cem surfacing functionalities
        transGroupMembershipUploadGridOption: null,
        transCemDncGridOption: null,
        transCemMsgPrefGridOption: null,
        transCemPrefLocGridOption: null,
        transCemDncUpldGridOption: null,
        transCemMsgPrefUpldGridOption:null
        //adding ends here
    };


    return {

        getGridOptions: function (uiGridConstants, type) {

            switch (type) {
                case TRANS_CONSTANTS.BEST_SMRY: {
                    allTransGrids.bestSmryGrid = typeof null ? new Grid(columnDefs.bestSmryColDef) : allTransGrids.bestSmryGrid;
                    gridOptions.bestSmryGridOption = allTransGrids.bestSmryGrid.getGridOption(uiScrollNever);
                    return gridOptions.bestSmryGridOption;
                    break;
                };
                case TRANS_CONSTANTS.TRANS_PERSON_NAME: {
                    allTransGrids.transPersonNameGrid = typeof null ? new TransGrid(columnDefs.transPersonNameColDef) : allTransGrids.transPersonNameGrid;
                    //gridOptions.transPersonNameGridOption = allTransGrids.transPersonNameGrid.getGridOption(uiScrollNever, "best_prsn_nm_ind");
                    gridOptions.transPersonNameGridOption = allTransGrids.transPersonNameGrid.getGridOption();
                    return gridOptions.transPersonNameGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_ORG_NAME: {
                    allTransGrids.transOrgNameGrid = typeof null ? new TransGrid(columnDefs.transOrgNameColDef) : allTransGrids.transOrgNameGrid;
                    //gridOptions.transOrgGridOption = allTransGrids.transOrgNameGrid.getGridOption(uiScrollNever, "best_org_nm_ind");
                    gridOptions.transOrgGridOption = allTransGrids.transOrgNameGrid.getGridOption();
                    return gridOptions.transOrgGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_ADDRESS: {
                    allTransGrids.transAddressGrid = typeof null ? new TransGrid(columnDefs.transAddressColDef) : allTransGrids.transAddressGrid;
                    //gridOptions.transAddressGridOption = allTransGrids.transAddressGrid.getGridOption(uiScrollNever, "best_addr_ind")
                    gridOptions.transAddressGridOption = allTransGrids.transAddressGrid.getGridOption()
                    return gridOptions.transAddressGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_PHONE: {
                    allTransGrids.transPhoneGrid = typeof null ? new TransGrid(columnDefs.transPhoneColDef) : allTransGrids.transPhoneGrid;
                    //gridOptions.transPhoneGridOption = allTransGrids.transPhoneGrid.getGridOption(uiScrollNever, "best_phn_ind");
                    gridOptions.transPhoneGridOption = allTransGrids.transPhoneGrid.getGridOption();
                    return gridOptions.transPhoneGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_EMAIL: {
                    allTransGrids.transEmailGrid = typeof null ? new TransGrid(columnDefs.transEmailColDef) : allTransGrids.transEmailGrid;
                    //gridOptions.transEmailGridOption = allTransGrids.transEmailGrid.getGridOption(uiScrollNever, "best_email_ind")
                    gridOptions.transEmailGridOption = allTransGrids.transEmailGrid.getGridOption()
                    return gridOptions.transEmailGridOption; break;

                };

                case TRANS_CONSTANTS.TRANS_EMAIL_UPLOAD: {
                    allTransGrids.transEmailUploadGrid = typeof null ? new TransGrid(columnDefs.transEmailUploadColDef) : allTransGrids.transEmailUploadGrid;
                    //gridOptions.transEmailUploadGridOption = allTransGrids.transEmailUploadGrid.getGridOption(uiScrollNever, "best_email_ind")
                    gridOptions.transEmailUploadGridOption = allTransGrids.transEmailUploadGrid.getGridOption()
                    return gridOptions.transEmailUploadGridOption; break;

                };


                case TRANS_CONSTANTS.CONST_EXT_BRIDGE: {
                    allTransGrids.extBridgeGrid = typeof null ? new Grid(columnDefs.constExtBridgeColDef) : allTransGrids.extBridgeGrid;
                    //gridOptions.extBridgeGridOption = allTransGrids.extBridgeGrid.getGridOption(uiScrollNever, null, true, true)
                    gridOptions.extBridgeGridOption = allTransGrids.extBridgeGrid.getGridOption()

                    return gridOptions.extBridgeGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_BIRTH: {
                    allTransGrids.transBirthGrid = typeof null ? new TransGrid(columnDefs.transBirthColDef) : allTransGrids.transBirthGrid;
                    //gridOptions.transBirthGridOption = allTransGrids.transBirthGrid.getGridOption(uiScrollNever)
                    gridOptions.transBirthGridOption = allTransGrids.transBirthGrid.getGridOption()
                    return gridOptions.transBirthGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_DEATH: {
                    allTransGrids.transDeathGrid = typeof null ? new TransGrid(columnDefs.transDeathColDef) : allTransGrids.transDeathGrid;
                    //gridOptions.transDeathGridOption = allTransGrids.transDeathGrid.getGridOption(uiScrollNever);
                    gridOptions.transDeathGridOption = allTransGrids.transDeathGrid.getGridOption();
                    return gridOptions.transDeathGridOption; break;
                };


                case TRANS_CONSTANTS.TRANS_MERGE: {
                    allTransGrids.transMergeGrid = typeof null ? new TransGrid(columnDefs.transMergeColDef) : allTransGrids.transMergeGrid;
                    //gridOptions.transMergeGridOption = allTransGrids.transMergeGrid.getGridOption(uiScrollNever);
                    gridOptions.transMergeGridOption = allTransGrids.transMergeGrid.getGridOption();
                    return gridOptions.transMergeGridOption; break;
                };

                case TRANS_CONSTANTS.TRANS_CONTACT_PREF: {
                    allTransGrids.transContactPrefGrid = typeof null ? new TransGrid(columnDefs.transContactPreferenceColDef) : allTransGrids.transContactPrefGrid;
                    //gridOptions.transContactPrefGridOption = allTransGrids.transContactPrefGrid.getGridOption(uiScrollNever);
                    gridOptions.transContactPrefGridOption = allTransGrids.transContactPrefGrid.getGridOption();

                    return gridOptions.transContactPrefGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_CHARACTERISTICS: {
                    allTransGrids.transCharacteristicsGrid = typeof null ? new TransGrid(columnDefs.transCharacteristicsColDef) : allTransGrids.transCharacteristicsGrid;
                    //gridOptions.transCharacteristicsGridOption = allTransGrids.transCharacteristicsGrid.getGridOption(uiScrollNever);
                    gridOptions.transCharacteristicsGridOption = allTransGrids.transCharacteristicsGrid.getGridOption();
                    return gridOptions.transCharacteristicsGridOption; break;
                };
               
                case TRANS_CONSTANTS.TRANS_ORG_TRANSFORM: {
                    allTransGrids.transOrgTransformGrid = typeof null ? new TransGrid(columnDefs.transOrgTransformColDef) : allTransGrids.transOrgTransformGrid;
                    //gridOptions.transOrgTransformGridOption = allTransGrids.transOrgTransformGrid.getGridOption(uiScrollNever)
                    gridOptions.transOrgTransformGridOption = allTransGrids.transOrgTransformGrid.getGridOption()
                    return gridOptions.transOrgTransformGridOption; break;
                };

                case TRANS_CONSTANTS.TRANS_AFFILIATOR_TAGS: {
                    allTransGrids.transAffiliatorTagsGrid = typeof null ? new TransGrid(columnDefs.transAffiliatorTagsColDef) : allTransGrids.transAffiliatorTagsGrid;
                    //gridOptions.transAffiliatorTagsGridOption = allTransGrids.transAffiliatorTagsGrid.getGridOption(uiScrollNever)
                    gridOptions.transAffiliatorTagsGridOption = allTransGrids.transAffiliatorTagsGrid.getGridOption()
                    return gridOptions.transAffiliatorTagsGridOption; break;
                };


                case TRANS_CONSTANTS.TRANS_AFFILIATOR_HIERARCHY: {
                    allTransGrids.transAffiliatorHierarchyGrid = typeof null ? new TransGrid(columnDefs.transAffiliatorHierarchyColDef) : allTransGrids.transAffiliatorHierarchyGrid;
                    //gridOptions.transAffiliatorHierarchyGridOption = allTransGrids.transAffiliatorHierarchyGrid.getGridOption(uiScrollNever)
                    gridOptions.transAffiliatorHierarchyGridOption = allTransGrids.transAffiliatorHierarchyGrid.getGridOption()
                    return gridOptions.transAffiliatorHierarchyGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_AFFILIATOR: {
                    allTransGrids.transAffiliatorGrid = typeof null ? new TransGrid(columnDefs.transAffiliatorColDef) : allTransGrids.transAffiliatorGrid;
                    //gridOptions.transAffiliatorGridOption = allTransGrids.transAffiliatorGrid.getGridOption(uiScrollNever)
                    gridOptions.transAffiliatorGridOption = allTransGrids.transAffiliatorGrid.getGridOption()
                    return gridOptions.transAffiliatorGridOption; break;
                };

                case TRANS_CONSTANTS.TRANS_UNMERGE_REQUEST_LOG: {
                    allTransGrids.unmergeRequestLogGrid = typeof null ? new TransGrid(columnDefs.transUnmergeRequestColDef) : allTransGrids.unmergeRequestLogGrid;
                    gridOptions.unmergeRequestLogGridOption = allTransGrids.unmergeRequestLogGrid.getGridOption()
                    return gridOptions.unmergeRequestLogGridOption; break;
                };

                case TRANS_CONSTANTS.TRANS_UNMERGE_PROCESS_LOG: {
                    allTransGrids.unmergeProcessLogGrid = typeof null ? new TransGrid(columnDefs.transUnmergeProcessColDef) : allTransGrids.unmergeProcessLogGrid;
                    gridOptions.unmergeProcessLogGridOption = allTransGrids.unmergeProcessLogGrid.getGridOption()
                    return gridOptions.unmergeProcessLogGridOption; break;
                };


                 //added by srini for cem surfacing functionalities
                case TRANS_CONSTANTS.TRANS_GROUP_UPLOAD: {
                    allTransGrids.transGroupMembershipUploadGrid = typeof null ? new TransGrid(columnDefs.transGroupMembershipUploadColDef) : allTransGrids.transGroupMembershipUploadGrid;
                    //gridOptions.transGroupMembershipUploadGridOption = allTransGrids.transGroupMembershipUploadGrid.getGridOption(uiScrollNever);
                    gridOptions.transGroupMembershipUploadGridOption = allTransGrids.transGroupMembershipUploadGrid.getGridOption();
                    return gridOptions.transGroupMembershipUploadGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_CEM_DNC_DETAILS: {
                    allTransGrids.transCemDncGrid = typeof null ? new TransGrid(columnDefs.transCemDncColDef) : allTransGrids.transCemDncGridOption;
                    gridOptions.transCemDncGridOption = allTransGrids.transCemDncGrid.getGridOption();
                    return gridOptions.transCemDncGridOption; break;
                };
                    
                case TRANS_CONSTANTS.TRANS_CEM_MSG_PREF_DETAILS: {
                    allTransGrids.transCemMsgPrefGrid = typeof null ? new TransGrid(columnDefs.transCemMsgPrefColDef) : allTransGrids.transCemMsgPrefGrid;
                    gridOptions.transCemMsgPrefGridOption = allTransGrids.transCemMsgPrefGrid.getGridOption();
                    return gridOptions.transCemMsgPrefGridOption; break;
                };

                case TRANS_CONSTANTS.TRANS_CEM_PREF_LOC_DETAILS: {
                    allTransGrids.transCemPrefLocGrid = typeof null ? new TransGrid(columnDefs.transCemPrefLocColDef) : allTransGrids.transCemPrefLocGrid;
                    gridOptions.transCemPrefLocGridOption = allTransGrids.transCemPrefLocGrid.getGridOption();
                    return gridOptions.transCemPrefLocGridOption; break;
                };
                case TRANS_CONSTANTS.TRANS_CEM_DNC_UPLD_DETAILS: {
                    allTransGrids.transCemDncUpldGrid = typeof null ? new TransGrid(columnDefs.transCemDncUploadColDef) : allTransGrids.transCemDncUpldGrid;
                    gridOptions.transCemDncUpldGridOption = allTransGrids.transCemDncUpldGrid.getGridOption();
                    return gridOptions.transCemDncUpldGridOption; break;
                };

                case TRANS_CONSTANTS.TRANS_CEM_MSG_PREF_UPLD_DETAILS: {
                    allTransGrids.transCemMsgPrefUpldGrid = typeof null ? new TransGrid(columnDefs.transCemMsgPrefUploadColDef) : allTransGrids.transCemMsgPrefUpldGrid;
                    gridOptions.transCemMsgPrefUpldGridOption = allTransGrids.transCemMsgPrefUpldGrid.getGridOption();
                    return gridOptions.transCemMsgPrefUpldGridOption; break;
                };
                // adding ends here

            
            }
        },

        getMultiGridLayout: function (gridOptions, uiGridConstants, result, type) {
            switch (type) {
                case TRANS_CONSTANTS.BEST_SMRY: {
                    return allTransGrids.bestSmryGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_PERSON_NAME: {
                    return allTransGrids.transPersonNameGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_ORG_NAME: {
                    return allTransGrids.transOrgNameGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_ADDRESS: {
                    return allTransGrids.transAddressGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_PHONE: {
                    return allTransGrids.transPhoneGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_EMAIL: {
                    return allTransGrids.transEmailGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_EMAIL_UPLOAD: {
                    return allTransGrids.transEmailUploadGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.CONST_EXT_BRIDGE: {
                    return allTransGrids.extBridgeGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_BIRTH: {
                    return allTransGrids.transBirthGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_DEATH: {
                    return allTransGrids.transDeathGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };

                case TRANS_CONSTANTS.TRANS_MERGE: {
                    return allTransGrids.transMergeGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };

                case TRANS_CONSTANTS.TRANS_CONTACT_PREF: {
                    return allTransGrids.transContactPrefGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_CHARACTERISTICS: {
                    return allTransGrids.transCharacteristicsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_GROUP_UPLOAD: {
                    return allTransGrids.transGroupMembershipUploadGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_HISTORY: {
                    return allTransGrids.transHistoryGrid.getGridLayout(gridOptions, result, type, 1); break;
                };
                case TRANS_CONSTANTS.ANON_INFO: {
                    return allTransGrids.anonInfoGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.MASTER_ATTEMPT: {
                    return allTransGrids.masterAttemptGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.INTERNAL_BRIDGE: {
                    return allTransGrids.internalBridgeGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.MERGE_HISTORY: {
                    return allTransGrids.mergeHistoryGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };


                case TRANS_CONSTANTS.TRANS_AFFILIATOR_TAGS: {
                    return allTransGrids.transOrgTransformGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };

                case TRANS_CONSTANTS.TRANS_AFFILIATOR_TAGS: {
                    return allTransGrids.transAffiliatorTagsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.TRANS_AFFILIATOR_HIERARCHY: {
                    return allTransGrids.transAffiliatorHierarchyGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };

                case TRANS_CONSTANTS.TRANS_AFFILIATOR: {
                    return allTransGrids.transAffiliatorGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.SUMMARY_VIEW: {
                    return allTransGrids.summaryViewGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.MASTER_DETAIL: {
                    return allTransGrids.masterDetailGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case TRANS_CONSTANTS.ADV_CASE_SEARCH: {
                    return allTransGrids.advCaseSearchGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };

                case TRANS_CONSTANTS.TRANS_UNMERGE_REQUEST_LOG: {
                    return allTransGrids.unmergeRequestLogGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };


                case TRANS_CONSTANTS.TRANS_UNMERGE_PROCESS_LOG: {
                    return allTransGrids.unmergeProcessLogGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };


            }
        }
    }
}]);


//Service to Get Details only
angular.module('transaction').factory('TransactionApiServices', ['$http', 'TRANS_CONSTANTS', '$rootScope', function ($http, TRANS_CONSTANTS, $rootScope) {
    var urls = {};


    var getApiData = function (param) {
       // console.log("Type before sending");
        var url = BasePath + 'TransactionNative/getconstituentbirth/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            //console.log(result.data);
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiOrgAffiliatorData = function (param) {
        //console.log("Type before sending");
        var url = BasePath + 'TransactionNative/gettransorgaffiliator/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Results");
            //console.log(result.data);
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiUnmergeRequestData = function (param) {
       // console.log("Type before sending");
        var url = BasePath + 'TransactionNative/gettransunmergerequest/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiUnmergeProcessData = function (param) {
       // console.log("Type before sending");
        var url = BasePath + 'TransactionNative/gettransunmergeprocess/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiDeathData = function (param) {
       // console.log("Type before sending");
      //  console.log(param);
        var url = BasePath + 'TransactionNative/gettransdeath/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiPersonNameData = function (param) {
       // console.log("Type before sending");
      //  console.log(param);
        var url = BasePath + 'TransactionNative/gettranspersonname/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
         //   console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiOrgNameData = function (param) {
       // console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransorgname/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiAddressData = function (param) {
       // console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransaddress/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiAffiliatorHierarchyData = function (param) {
      //  console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransaffiliatorhierarchy/' + param["trans_key"];
      //  console.log("Sending data to controller from trans multi service");
      //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiAffiliatorTagsData = function (param) {
       // console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransaffiliatortags/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiCharacteristicsData = function (param) {
       // console.log("Type before sending");
      //  console.log(param);
        var url = BasePath + 'TransactionNative/gettranscharacteristics/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiContactPreferenceData = function (param) {
       // console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettranscontactpref/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiEmailData = function (param) {
        //console.log("Type before sending");
        //console.log(param);
        var url = BasePath + 'TransactionNative/gettransemail/' + param["trans_key"];
        //console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiEmailUploadData = function (param) {
       // console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransemailupload/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiMergeData = function (param) {
       // console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransmerge/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiOrgTransformData = function (param) {
      //  console.log("Type before sending");
      //  console.log(param);
        var url = BasePath + 'TransactionNative/gettransorgtransform/' + param["trans_key"];
     //   console.log("Sending data to controller from trans multi service");
     //   console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
         //   console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiPhoneData = function (param) {
      //  console.log("Type before sending");
       // console.log(param);
        var url = BasePath + 'TransactionNative/gettransphone/' + param["trans_key"];
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var postUpdateTransactionStatusData = function (param) {
      //  console.log("Approver Params before sending to native controller");

        var url = BasePath + 'TransactionNative/postUpdateTransactionStatus';
       // console.log("Sending data to controller from trans multi service");
       // console.log(param);
        return $http.post(url, param, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results From Post Transaction Update Status");
          //  console.log(result);
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var postUnmergeTransactionResearchData = function (param) {
        var url = BasePath + 'TransactionNative/postUnmergeTransactionResearch';
        return $http.post(url, param, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
          //  console.log("Results From Post Transaction Unmerge Research");
          //  console.log(result);
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getMergeResearchResultData = function (param) {

      //  console.log("Merge Research Params before sending to native controller");

        var url = BasePath + 'TransactionNative/getMergeResearchResultData';
       // console.log("Sending data to controller from trans multi service");
      //  console.log(param);

        return $http.post(url, JSON.stringify(param), {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });

    };

    var getApiExportData = function (param) {
       // console.log("Export params are");
       // console.log(param);

        return $http.post(BasePath + 'TransactionNative/transactionDetailsExport', param,{
            headers: {
                "Content-type": 'application/json'

            },
            "responseType": "arraybuffer",
            }).success(function (data) {
            if (data.byteLength > 0) {
                var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                //  var blob = new Blob([data], { type: type });
                saveAs(blob, "TransactionDetails.xlsx");
            }
            else {
                angular.element("#iErrorModal").modal('hide');
                angular.element("#accessDeniedModal").modal();
            }
        });
    };


    //added by srini for CEM surfacing functionalities
    var getApiGroupMembershipUploadData = function (param) {
        // console.log("Type before sending");
        //  console.log(param);
        var url = BasePath + 'TransactionNative/cem/group/membership/details/' + param["trans_key"];
        //  console.log("Sending data to controller from trans multi service");
        //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiCemDncTransDetailsData = function (param) {
        // console.log("Type before sending");
        //  console.log(param);
        var url = BasePath + 'TransactionNative/cem/dnc/details/' + param["trans_key"];
        //  console.log("Sending data to controller from trans multi service");
        //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiCemMsgPrefTransDetailsData = function (param) {
        // console.log("Type before sending");
        //  console.log(param);
        var url = BasePath + 'TransactionNative/cem/message/preference/details/' + param["trans_key"];
        //  console.log("Sending data to controller from trans multi service");
        //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiCemPrefLocTransDetailsData = function (param) {
        // console.log("Type before sending");
        //  console.log(param);
        var url = BasePath + 'TransactionNative/cem/preferred/locator/details/' + param["trans_key"];
        //  console.log("Sending data to controller from trans multi service");
        //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiCemDncUploadTransDetailsData = function (param) {
        // console.log("Type before sending");
        //  console.log(param);
        var url = BasePath + 'TransactionNative/cem/dnc/upload/' + param["trans_key"];
        //  console.log("Sending data to controller from trans multi service");
        //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    var getApiCemMsgPrefUploadTransDetailsData = function (param) {
        // console.log("Type before sending");
        //  console.log(param);
        var url = BasePath + 'TransactionNative/cem/message/preference/upload/' + param["trans_key"];
        //  console.log("Sending data to controller from trans multi service");
        //  console.log(param);
        return $http.get(url, {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            // console.log("Results");
            return result;
        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    };

    //adding ends here

    //Call Appropriate APIs
    return {
        getApiDetails: function (param) {
            return getApiData(param);
        },

        getApiOrgAffiliatorDetails: function (param) {
            return getApiOrgAffiliatorData(param);
        },

        getApiUnmergeRequestDetails: function (param) {
            return getApiUnmergeRequestData(param);
        },

        getApiUnmergeProcessDetails: function (param) {
            return getApiUnmergeProcessData(param);
        },

        getApiDeathDetails: function (param) {
            return getApiDeathData(param);
        },

        getApiPersonNameDetails: function (param) {
            return getApiPersonNameData(param);
        },

        getApiOrgNameDetails: function (param) {
            return getApiOrgNameData(param);
        },

        getApiAddressDetails: function (param) {
            return getApiAddressData(param);
        },

        getApiAffiliatorHierarchyDetails: function (param) {
            return getApiAffiliatorHierarchyData(param);
        },


        getApiAffiliatorTagsDetails: function (param) {
            return getApiAffiliatorTagsData(param);
        },

        getApiCharacteristicsDetails: function (param) {
            return getApiCharacteristicsData(param);
        },

        getApiContactPreferenceDetails: function (param) {
            return getApiContactPreferenceData(param);
        },

        getApiEmailDetails: function (param) {
            return getApiEmailData(param);
        },

        getApiEmailOnlyUploadDetails: function (param) {
            return getApiEmailUploadData(param);
        },

        getApiMergeDetails: function (param) {
            return getApiMergeData(param);
        },

       
        getApiOrgTransformDetails: function (param) {
            return getApiOrgTransformData(param);
        },

        getApiPhoneDetails: function (param) {
            return getApiPhoneData(param);
        },

        postUpdateTransactionStatusDetails: function (param) {
            return postUpdateTransactionStatusData(param);
        },

        postUnmergeTransactionResearchDetails: function(param) {
            return postUnmergeTransactionResearchData(param);
        },

        getMergeResearchResultDetails: function (param) {
            return getMergeResearchResultData(param);
        },

        getApiExportDetails: function (param) {
            return getApiExportData(param);
        },
        // added by srini for CEM surfacing 
        getApiCemDncDetails: function(param){
            return getApiCemDncTransDetailsData(param);
        },

        getApiCemMsgPrefDetails : function(param){
            return getApiCemMsgPrefTransDetailsData(param);
        },
        getApiCemPrefLocDetails : function(param){
            return getApiCemPrefLocTransDetailsData(param);
        },
        getApiGroupMembershipUploadDetails: function (param) {
            return getApiGroupMembershipUploadData(param);
        }
        ,
        getApiCemDncUploadDetails: function (param) {
            return getApiCemDncUploadTransDetailsData(param);
        },
         getApiCemMsgPrefUploadDetails: function (param) {
             return getApiCemMsgPrefUploadTransDetailsData(param);
        }
        
        
        //adding  ends here

    }

}]);


