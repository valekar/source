﻿

angular.module('constituent').controller("constMultiDetialsController", ['$scope', '$parse', '$http', '$localStorage', '$sessionStorage', '$stateParams', 'uiGridConstants', '$uibModal', '$stateParams', 'constMultiGridService',
    'constituentServices', 'constMultiDataService', 'constituentApiService', '$timeout', '$rootScope', 'constituentDataServices', 'mainService', 'constituentCRUDoperations',
    'constituentCRUDapiService', '$q','showDetailsPostApiService','exportService','$location', 'StoreData','CEM_CONSTANTS','EXTENDED_CONSTANTS',
function ($scope, $parse, $http, $localStorage, $sessionStorage, $stateParams, uiGridConstants, $uibModal, $stateParams, constMultiGridService, constituentServices,
    constMultiDataService, constituentApiService, $timeout, $rootScope, constituentDataServices, mainService, constituentCRUDoperations, constituentCRUDapiService,
    $q, showDetailsPostApiService, exportService, $location, StoreData, CEM_CONSTANTS, EXTENDED_CONSTANTS) {

    var COMMON_URL = BasePath + 'App/Constituent/Views/multi/';

    var TEMPLATES = {
        BEST_SMRY: 'ConstMultiBestSmry.tpl.html',
        CONST_NAME: 'ConstMultiName.tpl.html',
        CONST_ADDRESS: 'ConstMultiAddress.tpl.html',
        CONST_EMAIL: 'ConstMultiEmail.tpl.html',
        CONST_PHONE: 'ConstMultiPhone.tpl.html',
        CONST_EXT_BRIDGE: 'ConstMultiExtBridge.tpl.html',
        CONST_BIRTH: 'ConstMultiBirth.tpl.html',
        CONST_DEATH: 'ConstMultiDeath.tpl.html',
        CONST_PREF: 'ConstMultiPref.tpl.html',
        CHARACTERISTICS: 'MultiCharacter.tpl.html',
        GRP_MEMBERSHIP: 'MultiGrpMembership.tpl.html',
        TRANS_HISTORY: "MultiTransHistory.tpl.html",
        ANON_INFO: 'MultiAnonInfo.tpl.html',
        MASTER_ATTEMPT: 'MultiMasterAttempt.tpl.html',
        INTERNAL_BRIDGE: 'MultiInternalBridge.tpl.html',
        MERGE_HISTORY: 'MultiMergeHistory.tpl.html',
        CONST_MULTI_DETAILS_SIDE_PANEL: "ConstMultiDetailsSidePanel.html",
        CONST_ORG_NAME: 'ConstOrgName.tpl.html',
        AFFILIATOR: 'Affiliator.tpl.html',
        SUMMARY_VIEW: 'SummaryView.tpl.html',
        MASTER_DETAIL:'MasterDetail.tpl.html'
    }

    CONSTANTS = {
        BEST_SMRY: 'BestSmry',
        CONST_NAME: 'ConstName',
        CONST_ADDRESS: 'ConstAddress',
        CONST_EMAIL: 'ConstEmail',
        CONST_PHONE: 'ConstPhone',
        CONST_EXT_BRIDGE: 'ConstExtBridge',
        CONST_BIRTH: 'ConstBirth',
        CONST_DEATH: 'ConstDeath',
        CONST_PREF: 'ConstPref',
        CHARACTERISTICS: 'Character',
       
        TRANS_HISTORY: "TransHistory",
        ANON_INFO: 'AnonInfo',
        MASTER_ATTEMPT: 'MasterAttempt',
        INTERNAL_BRIDGE: 'InternalBridge',
        MERGE_HISTORY: 'MergeHistory',
        MENU_PREF: "MenuPref",
        CONST_ORG_NAME: "ConstOrgName",
        AFFILIATOR: "Affiliator",
        SUMMARY_VIEW: "SummaryView",
        MASTER_DETAIL: "MasterDetail",
        ADV_CASE_SEARCH: "AdvCaseSearch",
        QUICK_CASE_SEARCH: "QuickCaseSearch",

        //CEM functionality menu pref
        CEM_GRP_MEMBERSHIP: CEM_CONSTANTS.GRP_MEMBRSHP,
        CEM_DNC :CEM_CONSTANTS.DNC_SURFACE,
        CEM_MESG_PREF:CEM_CONSTANTS.MSG_PREF,
        CEM_PREF_LOC: CEM_CONSTANTS.PREF_LOCATION
        //Alternate Ids
       

    };

    //used in templates
    $scope.CONSTANTS = {
        BEST_SMRY: 'BestSmry', CONST_NAME: 'ConstName', CONST_ADDRESS: 'ConstAddress', CONST_EMAIL: 'ConstEmail', CONST_PHONE: 'ConstPhone', CONST_EXT_BRIDGE: 'ConstExtBridge',
        CONST_BIRTH: 'ConstBirth', CONST_DEATH: 'ConstDeath', CONST_PREF: 'ConstPref', CHARACTERISTICS: 'Character', GRP_MEMBERSHIP: 'GrpMembership', TRANS_HISTORY: "TransHistory",
        ANON_INFO: 'AnonInfo', MASTER_ATTEMPT: 'MasterAttempt', INTERNAL_BRIDGE: 'InternalBridge', MERGE_HISTORY: 'MergeHistory', MENU_PREF: "MenuPref", CONST_ORG_NAME: "ConstOrgName",
        AFFILIATOR: "Affiliator", SUMMARY_VIEW: "SummaryView", MASTER_DETAIL: "MasterDetail"
    };

    $scope.exportDetails = function () {
        //alert("export");
        messagePopup($rootScope, "File will be exported shortly.", "Exporting...");
      //  $timeout(function () {
            exportService.getConstDetailsExportData($scope.masterId,$rootScope).success(function (result) {
                messagePopupClose();
            }).error(function (result) {
                if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                    messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
                }
                else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == CRUD_CONSTANTS.DB_ERROR) {
                    messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });
       // }, 500);
        
    };


    var initailize = function () {


        mainService.getUsername().then(function (result) {
            $scope.UserName= result.data;
        });

        $scope.masterId = $sessionStorage.masterId;
        var params = $stateParams.constituentId;
        if (params != null || params != "" || !angular.isUndefined(params)) {
            $scope.param = params;
            $scope.constituent_headerName = $sessionStorage.name;
            $scope.constituent_masterId = $sessionStorage.masterId;
            //getting value in constituent helper
            $scope.constituent_type = $sessionStorage.type;
            $rootScope.constituent_type = $scope.constituent_type;
        } else {
            $scope.param = null;
        }
        var url = COMMON_URL + TEMPLATES.CONST_MULTI_DETAILS_SIDE_PANEL;
        $scope.ConstMultiDetailsSidePanel = url;

        constituentApiService.getApiDetails('', CONSTANTS.MENU_PREF).success(function (result) {
            //console.log(result[0].Preferences);
            //$scope.constName = true;
            // $scope.getConstName(false);
            // used in MenuPrefPopup.html for checking the values of checkboxes
            $scope.pref_ARCBestSmry = false;
            $scope.pref_PrsnName = false;
            $scope.pref_AnonInfo = false;
            $scope.pref_Address = false;
            $scope.pref_Email = false;
            $scope.pref_Phones = false;
            $scope.pref_MergeHist = false;
            $scope.pref_IntBridge = false;
            $scope.pref_MasterAttmpt = false;
            $scope.pref_TransHistory = false;
            $scope.pref_grpMemb = false;
            $scope.pref_Characteristics = false;
            $scope.pref_CntctPref = false;
            $scope.pref_Death = false;
            $scope.pref_Birth = false;
            $scope.pref_ExternalBrdge = false;
            $scope.pref_MasterDetails = false;
            $scope.pref_Smryview = false;
            $scope.pref_OrgName = false;
            $scope.pref_affiliator = false;
            // console.log(result.Preferences);

            //console.log("+++++++++");
            //console.log(angular.isUndefined(result.PreferencesOR));
            if (($scope.constituent_type == 'IN' && (angular.isUndefined(result.PreferencesIN) || result.PreferencesIN == null)) ||
                    ($scope.constituent_type == 'OR' && (angular.isUndefined(result.PreferencesOR) || result.PreferencesOR == null))) {
                $scope.masterDetail = true;
                $scope.constExtBridge = true;
                $scope.bestSmry = true;

                $scope.getMasterDetail(false);
                $scope.getSummary(false);
                $scope.getConstExtBridge(false);

                $scope.pref_ARCBestSmry = true;
                $scope.pref_ExternalBrdge = true;
                $scope.pref_MasterDetails = true;
            }
            else {
                if($scope.constituent_type == 'IN'){
                    angular.forEach(result.PreferencesIN, function (v, k) {
                        showPreferences(v);
                    });
                }
                if ($scope.constituent_type == 'OR') {
                    angular.forEach(result.PreferencesOR, function (v, k) {
                        showPreferences(v);
                    });
                }

        }
        }).error(function (result) {
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        });

        function showPreferences(v) {
            switch (v) {
                case CONSTANTS.MASTER_DETAIL: {
                    //check the checkbox
                    $scope.masterDetail = true;
                    $scope.getMasterDetail(false);
                    $scope.pref_MasterDetails = true;
                    break;
                }
                case CONSTANTS.SUMMARY_VIEW: {
                    if ($scope.constituent_type == 'IN') {
                        $scope.smryView = true;
                        $scope.getSummaryView(false);
                        $scope.pref_Smryview = true;
                    }
                    break;
                }
                case CONSTANTS.BEST_SMRY: {
                    //this can be used for preferences
                    $scope.bestSmry = true;
                    $scope.getSummary(false);
                    $scope.pref_ARCBestSmry = true;
                    break;
                }
                case CONSTANTS.CONST_NAME: {
                    if ($scope.constituent_type == 'IN') {
                        $scope.constName = true;
                        $scope.getConstName(false);
                        $scope.pref_PrsnName = true;
                    }
                    break;
                }
                case CONSTANTS.CONST_ORG_NAME: {
                    if ($scope.constituent_type == 'OR') {
                        $scope.constOrgName = true;
                        $scope.getConstOrgName(false);
                        $scope.pref_OrgName = true;
                    }
                    break;
                }

                case CONSTANTS.ANON_INFO: {
                    $scope.anonInfo = true;
                    $scope.getAnonInfo(false);
                    $scope.pref_AnonInfo = true;
                    break;
                }

                case CONSTANTS.CONST_ADDRESS: {
                    $scope.constAddress = true;
                    $scope.getConstAddress(false);
                    $scope.pref_Address = true;
                    break;
                }
                case CONSTANTS.CONST_EMAIL: {
                    $scope.constEmail = true;
                    $scope.getConstEmail(false);
                    $scope.pref_Email = true;
                    break;
                }
                case CONSTANTS.CONST_PHONE: {
                    $scope.constPhone = true;
                    $scope.getConstPhone(false);
                    $scope.pref_Phones = true;
                    break;
                }
                case CONSTANTS.CONST_EXT_BRIDGE: {
                    $scope.constExtBridge = true;
                    $scope.getConstExtBridge(false);
                    $scope.pref_ExternalBrdge = true;
                    break;
                }
                case CONSTANTS.CONST_BIRTH: {
                    if ($scope.constituent_type == 'IN') {
                        $scope.constBirth = true;
                        $scope.getConstBirth(false);
                        $scope.pref_Birth = true;
                    }
                    break;
                }
                case CONSTANTS.CONST_DEATH: {
                    if ($scope.constituent_type == 'IN') {
                        $scope.constDeath = true;
                        $scope.getConstDeath(false);
                        $scope.pref_Death = true;
                    }
                    break;
                }
                case CONSTANTS.CHARACTERISTICS: {
                    $scope.characteristics = true;
                    $scope.getCharacteristics(false);
                    $scope.pref_Characteristics = true;
                    break;
                }
                case CONSTANTS.TRANS_HISTORY: {
                    $scope.transHistory = true;
                    $scope.getTransHistory(false);
                    $scope.pref_TransHistory = true;
                    break;
                }
                case CONSTANTS.MASTER_ATTEMPT: {
                    $scope.masterAttempt = true;
                    $scope.getMasterAttempts(false);
                    $scope.pref_MasterAttmpt = true;
                    break;
                }
                case CONSTANTS.INTERNAL_BRIDGE: {
                    $scope.internalBridge = true;
                    $scope.getInternalBridge(false);
                    $scope.pref_IntBridge = true;
                    break;
                }
                case CONSTANTS.MERGE_HISTORY: {
                    $scope.mergeHistory = true;
                    $scope.getMergeHistory(false);
                    $scope.pref_MergeHist = true;
                    break;
                }
                case CONSTANTS.AFFILIATOR: {
                    if ($scope.constituent_type == 'OR') {
                        $scope.affiliator = true;
                        $scope.getAffiliator(false);
                        $scope.pref_affiliator = true;
                    }
                    break;
                }
                    // this is for CEM
                case CONSTANTS.CEM_GRP_MEMBERSHIP: {
                    $scope.cem.redirectTo(CONSTANTS.CEM_GRP_MEMBERSHIP);
                    $scope.pref_cem_grpMembershp = true;
                    $scope.cem.toggleGrpMembrshp = true;
                    break;
                }
                case CONSTANTS.CEM_DNC: {
                    $scope.cem.redirectTo(CONSTANTS.CEM_DNC);
                    $scope.cem.toggleDnc = true;
                    $scope.pref_cem_dnc = true;
                    break;
                }
                case CONSTANTS.CEM_MESG_PREF: {
                    $scope.cem.redirectTo(CONSTANTS.CEM_MESG_PREF);
                    $scope.pref_cem_msg_pref = true;
                    $scope.cem.toggleMsgPref = true;
                    break;
                }
                case CONSTANTS.CEM_PREF_LOC: {
                    $scope.cem.redirectTo(CONSTANTS.CEM_PREF_LOC);
                    $scope.pref_cem_pref_loc = true;
                    $scope.cem.togglePrefLoc = true;
                    break;
                }

                default: {
                    $scope.masterDetail = true;
                    $scope.getMasterDetail(false);
                    $scope.pref_MasterDetails = true;
                }
            }
        }

        $scope.divMenuSuccessMsg = false;
        $scope.divMenuErrorMsg = false;
        $scope.alerts = [];
        $scope.popver = {
            templateUrl: BasePath + "App/Constituent/Views/multi/MenuPrefPopup.html",
            title: "Menu Settings",
            showPref: false,
        }
       

        $scope.closePreferences = function () {
            $scope.popver.showPref = false;
            $scope.alerts = [];
        };


        $scope.toggleCheck = function (scopeName, scopeValue) {
            $parse(scopeName).assign($scope, scopeValue);
        }


        function pushPreferences() {
            var preferences = [];
            if ($scope.pref_MasterDetails) {
                preferences.push(CONSTANTS.MASTER_DETAIL);
            }
            if ($scope.pref_Smryview) {
                preferences.push(CONSTANTS.SUMMARY_VIEW);
            }
            if ($scope.pref_ARCBestSmry) {
                preferences.push(CONSTANTS.BEST_SMRY);
            }
            if ($scope.pref_PrsnName) {
                preferences.push(CONSTANTS.CONST_NAME);
            }
            if ($scope.pref_OrgName) {
                preferences.push(CONSTANTS.CONST_ORG_NAME);
            }
            if ($scope.pref_Address) {
                preferences.push(CONSTANTS.CONST_ADDRESS);
            }
            if ($scope.pref_Phones) {
                preferences.push(CONSTANTS.CONST_PHONE);
            }
            if ($scope.pref_Email) {
                preferences.push(CONSTANTS.CONST_EMAIL);
            }
            if ($scope.pref_ExternalBrdge) {
                preferences.push(CONSTANTS.CONST_EXT_BRIDGE);
            }
            if ($scope.pref_Birth) {
                preferences.push(CONSTANTS.CONST_BIRTH);
            }
            if ($scope.pref_Death) {
                preferences.push(CONSTANTS.CONST_DEATH);
            }
            if ($scope.pref_Characteristics) {
                preferences.push(CONSTANTS.CHARACTERISTICS);
            }
            if ($scope.pref_TransHistory) {
                preferences.push(CONSTANTS.TRANS_HISTORY);
            }
            if ($scope.pref_AnonInfo) {
                preferences.push(CONSTANTS.ANON_INFO);
            }
            if ($scope.pref_MasterAttmpt) {
                preferences.push(CONSTANTS.MASTER_ATTEMPT);
            }
            if ($scope.pref_IntBridge) {
                preferences.push(CONSTANTS.INTERNAL_BRIDGE);
            }
            if ($scope.pref_MergeHist) {
                preferences.push(CONSTANTS.MERGE_HISTORY);
            }
            if ($scope.pref_affiliator) {
                preferences.push(CONSTANTS.AFFILIATOR);
            }
            if ($scope.pref_cem_grpMembershp) {
                preferences.push(CONSTANTS.CEM_GRP_MEMBERSHIP);
            }
            if ($scope.pref_cem_dnc) {
                preferences.push(CONSTANTS.CEM_DNC);
            }
            if ($scope.pref_cem_msg_pref) {
                preferences.push(CONSTANTS.CEM_MESG_PREF);
            }
            if ($scope.pref_cem_pref_loc) {
                preferences.push(CONSTANTS.CEM_PREF_LOC);
            }

            return preferences;
        }
        // save the preferences
        $scope.savePreferences = function () {
            var JSONObjMain = [];
            //var preferences = [];
            var JSONObj = {};
            
            if ($scope.constituent_type == 'IN') {
                JSONObj = {
                    "UserName": $scope.UserName,
                    "PreferencesIN": pushPreferences(),
                    "PreferencesOR": []
                }
            }
            if ($scope.constituent_type == 'OR') {
                JSONObj = {
                    "UserName": $scope.UserName,
                    "PreferencesIN": [],
                    "PreferencesOR": pushPreferences()
                };
            }

            var SavePrefResult = constituentCRUDapiService.getCRUDOperationResult(JSONObj, "WriteMenuPref");
            
            SavePrefResult.success(function (result) {
            
                console.log(result);
                if (result == "SUCCESS") {
                    $scope.alerts.push({});
                }
                else {
                    $scope.divMenuErrorMsg = true;
                }
            }).error(function (result) {
                if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                    //angular.element("#accessDeniedModal").modal();
                    messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
                }
                else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == CRUD_CONSTANTS.DB_ERROR) {
                    messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });
        };

        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        }

        /****************************************BEST SUMMARY SETTINGS******************************/
        $scope.bestSmry = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.BEST_SMRY);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.BEST_SMRY);
        $scope.bestSmryGridOptions = options;
        // $scope.bestSmryGridOptions.enableFiltering = false;
        $scope.bestSmryGridOptions.onRegisterApi = function (grid) {
            $scope.bestSmryGridApi = grid;
        }

        /*****end of set grid options*****/
        /****************************************END OF BEST SUMMARY SETTINGS******************************/


        /*************************************CONST NAME SETTINGS***********************************/
        $scope.constName = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_NAME);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_NAME);
        $scope.constNameGridOptions = options;
        //  $scope.constNameGridOptions.enableFiltering = false;
        $scope.constNameGridOptions.onRegisterApi = function (grid) {
            $scope.constNameGridApi = grid;
            $scope.nameGrid = {
                changedColumns: 0
            };
            //console.log( grid);

            grid.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                //console.log(changedColumn);
                if (changedColumn.visible) {
                    $scope.nameGrid.changedColumns++;
                }
                else {
                    //  _element.css('width', _element.width() - _element.width() * .1);
                    if ($scope.nameGrid.changedColumns > 0) {
                        $scope.nameGrid.changedColumns--;
                    }
                }
            });
        }


        $scope.expandNameGrid = function () {
            var _element = angular.element('#ConstName');
            var initialWidth = _element.width();
            if ($scope.nameGrid.changedColumns > 0) {
                _element.css('width', initialWidth + _element.width() * .1 * $scope.nameGrid.changedColumns);

            }
            else {
                _element.css('width', initialWidth);
            }
            // $scope.constNameGridApi.core.refresh();
        }


        $scope.contractNameGrid = function () {
            var _element = angular.element('#ConstName');
            _element.css('width', '100%');
        }

     
        /*************************************END OF CONST NAME SETTINGS***********************************/



        /*************************************CONST ORG NAME SETTINGS***********************************/
        $scope.constOrgName = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_ORG_NAME);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_ORG_NAME);
        $scope.constOrgNameGridOptions = options;
        $scope.constOrgNameGridOptions.onRegisterApi = function (grid) {
            $scope.constOrgNameGridApi = grid;
        }

        /*************************************END OF CONST ORG NAME SETTINGS***********************************/



        /*************************************CONST Address SETTINGS***********************************/
        $scope.constAddress = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_ADDRESS);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_ADDRESS);
        $scope.constAddressGridOptions = options;
        // $scope.constAddressGridOptions.enableFiltering = false;
        $scope.constAddressGridOptions.onRegisterApi = function (grid) {
            $scope.constAddressGridApi = grid;
        }
        /*************************************END OF CONST Address SETTINGS***********************************/


        /*************************************CONST Phone SETTINGS***********************************/
        $scope.constPhone = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_PHONE);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_PHONE);
        $scope.constPhoneGridOptions = options;
        //  $scope.constPhoneGridOptions.enableFiltering = false;
        $scope.constPhoneGridOptions.onRegisterApi = function (grid) {
            $scope.constPhoneGridApi = grid;
        }

        /*************************************END OF CONST Phone SETTINGS***********************************/


        /*************************************CONST Email SETTINGS***********************************/
        $scope.constEmail = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_EMAIL);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_EMAIL);
        $scope.constEmailGridOptions = options;
        //  $scope.constEmailGridOptions.enableFiltering = false;
        $scope.constEmailGridOptions.onRegisterApi = function (grid) {
            $scope.constEmailGridApi = grid;
        }
        /*************************************END OF CONST Email SETTINGS***********************************/


        /*************************************CONST Ext Bridge SETTINGS***********************************/
        $scope.constExtBridge = false;
        $scope.unmerge = {
            rows:[]
        };

        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_EXT_BRIDGE);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_EXT_BRIDGE);
        $scope.constExtBridgeGridOptions = options;
        // $scope.constExtBridgeGridOptions.enableFiltering = false;
        $scope.constExtBridgeGridOptions.onRegisterApi = function (grid) {
            $scope.constExtBridgeGridApi = grid;

           

            // add to unmerge cart
            $scope.constExtBridgeGridApi.selection.on.rowSelectionChanged($scope, function (row) {
               // var msg = 'row selected ' + row.isSelected;

               
              //  console.log(row);
            });
        }
        /*************************************END OF CONST Ext Bridge SETTINGS***********************************/


        /*************************************CONST Birth SETTINGS***********************************/
        $scope.constBirth = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_BIRTH);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_BIRTH);
        $scope.constBirthGridOptions = options;
        // $scope.constBirthGridOptions.enableFiltering = false;
        $scope.constBirthGridOptions.onRegisterApi = function (grid) {
            $scope.constBirthGridApi = grid;
        }
        /*************************************END OF CONST Birth SETTINGS***********************************/


        /*************************************CONST Death SETTINGS***********************************/
        $scope.constDeath = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_DEATH);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_DEATH);
        $scope.constDeathGridOptions = options;
        // $scope.constDeathGridOptions.enableFiltering = false;
        $scope.constDeathGridOptions.onRegisterApi = function (grid) {
            $scope.constDeathGridApi = grid;
        }
        /*************************************END OF CONST Death SETTINGS***********************************/


        /*************************************CON PREF SETTINGS***********************************/
        //$scope.contactPref = false;
        ////toggle grid results
        //constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_PREF);
        ///*****set the grid options*****/
        //var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CONST_PREF);
        //$scope.constPrefGridOptions = options;
        //// $scope.constPrefGridOptions.enableFiltering = false;
        //$scope.constPrefGridOptions.onRegisterApi = function (grid) {
        //    $scope.constPrefGridApi = grid;
        //}
        /*************************************END OF CON PREF  SETTINGS***********************************/


        /*************************************CHARACTERISTICS SETTINGS***********************************/
        $scope.characteristics = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CHARACTERISTICS);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.CHARACTERISTICS);
        $scope.characteristicsGridOptions = options;
        // $scope.characteristicsGridOptions.enableFiltering = false;
        $scope.characteristicsGridOptions.onRegisterApi = function (grid) {
            $scope.characteristicsGridApi = grid;
        }
        /*************************************END OF CHARACTERISTICS  SETTINGS***********************************/


        /*************************************GROUP MEMBERSHIP SETTINGS***********************************/
        //$scope.grpMembership = false;
        ////toggle grid results
        //constMultiGridService.getToggleDetails($scope, false, CONSTANTS.GRP_MEMBERSHIP);
        ///*****set the grid options*****/
        //var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.GRP_MEMBERSHIP);
        //$scope.grpMembershipGridOptions = options;
        //// $scope.grpMembershipGridOptions.enableFiltering = false;
        //$scope.grpMembershipGridOptions.onRegisterApi = function (grid) {
        //    $scope.grpMembershipGridApi = grid;
        //}
        /*************************************END OF GROUP MEMBERSHIP  SETTINGS***********************************/


        /*************************************TRANSACTION HISTORY SETTINGS***********************************/
        $scope.transHistory = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.TRANS_HISTORY);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.TRANS_HISTORY);
        $scope.transHistoryGridOptions = options;
        //  $scope.transHistoryGridOptions.enableFiltering = false;
        $scope.transHistoryGridOptions.onRegisterApi = function (grid) {
            $scope.transHistoryGridApi = grid;
        }
        /*************************************END OF TRANSACTION HISTORY  SETTINGS***********************************/


        /*************************************ANONYMOUS INFO SETTINGS***********************************/
        $scope.anonInfo = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.ANON_INFO);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.ANON_INFO);
        $scope.anonInfoGridOptions = options;
        //$scope.anonInfoGridOptions.enableFiltering = false;
        $scope.anonInfoGridOptions.onRegisterApi = function (grid) {
            $scope.anonInfoGridApi = grid;
        }
        /*************************************END OF ANONYMOUS INFO  SETTINGS***********************************/


        /*************************************MASTER ATTEMPT SETTINGS***********************************/
        $scope.masterAttempt = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.MASTER_ATTEMPT);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.MASTER_ATTEMPT);
        $scope.masterAttemptGridOptions = options;
        //  $scope.masterAttemptGridOptions.enableFiltering = false;
        $scope.masterAttemptGridOptions.onRegisterApi = function (grid) {
            $scope.masterAttemptGridApi = grid;
        }
        /*************************************END OF MASTER ATTEMPT  SETTINGS***********************************/


        /*************************************INTERNAL BRIDGE SETTINGS***********************************/
        $scope.internalBridge = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.INTERNAL_BRIDGE);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.INTERNAL_BRIDGE);
        $scope.internalBridgeGridOptions = options;
        //$scope.internalBridgeGridOptions.enableFiltering = false;
        $scope.internalBridgeGridOptions.onRegisterApi = function (grid) {
            $scope.internalBridgeGridApi = grid;
        }
        /*************************************END OF INTERNAL BRIDGE  SETTINGS***********************************/


        /*************************************MERGE HISTORY SETTINGS***********************************/
        $scope.mergeHistory = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.MERGE_HISTORY);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.MERGE_HISTORY);
        $scope.mergeHistoryGridOptions = options;
        // $scope.mergeHistoryGridOptions.enableFiltering = false;
        $scope.mergeHistoryGridOptions.onRegisterApi = function (grid) {
            $scope.mergeHistoryGridApi = grid;
        }
        /*************************************END OF MERGE HISTORY  SETTINGS***********************************/

        /*************************************AFFILIATOR SETTINGS***********************************/
        $scope.affiliator = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.AFFILIATOR);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.AFFILIATOR);
        $scope.affiliatorGridOptions = options;
        // $scope.mergeHistoryGridOptions.enableFiltering = false;
        $scope.affiliatorGridOptions.onRegisterApi = function (grid) {
            $scope.affiliatorGridApi = grid;
        }
        /*************************************AFFILIATOR  SETTINGS***********************************/

        /*************************************SUMMARY VIEW SETTINGS***********************************/
        $scope.smryView = false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.SUMMARY_VIEW);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.SUMMARY_VIEW);
        $scope.summaryViewGridOptions = options;
        // $scope.mergeHistoryGridOptions.enableFiltering = false;
        $scope.summaryViewGridOptions.onRegisterApi = function (grid) {
            $scope.summaryViewGridApi = grid;
        }
        /*************************************SUMMARY VIEW   SETTINGS***********************************/

        /*************************************CONST Phone SETTINGS***********************************/
        $scope.masterDetail =  false;
        //toggle grid results
        constMultiGridService.getToggleDetails($scope, false, CONSTANTS.MASTER_DETAIL);
        /*****set the grid options*****/
        var options = constMultiGridService.getGridOptions(uiGridConstants, CONSTANTS.MASTER_DETAIL);
        $scope.masterDetailGridOptions = options;
        //  $scope.constPhoneGridOptions.enableFiltering = false;
        $scope.masterDetailGridOptions.onRegisterApi = function (grid) {
            $scope.masterDetailGridApi = grid;
        }

        /*************************************END OF CONST Phone SETTINGS***********************************/



        /***********************This is used to show/hide add buttons in all section(wherever the add button is) this is implemented for tab level security *****/
        $scope.commonShowAddGridRow = true;
        $scope.showMergeUnmerge = true;
        var userTabPermission = constMultiGridService.getTabDenyPermission();
        if (userTabPermission) {
            //this is a common varaible for all sections
            $scope.commonShowAddGridRow = false;
        }

        var mergeUnmergePermission = constMultiGridService.getMergeUnmergePermission();
        if (!mergeUnmergePermission) {
            //this is a common varaible for all sections
            $scope.showMergeUnmerge = false;
        }



        /************************End of tab level security**********************************************/
    }
    initailize();

    //checkbox for summary 
    $scope.getSummary = function (bestSmry) {
        var url = COMMON_URL + TEMPLATES.BEST_SMRY;
        $scope.bestSmryTemplate = url;
        $scope.BaseURL = BasePath;
        if (!bestSmry) {
            $scope.toggleBestSmryLoader = { "display": "block" };

            //show and set the grid data 

            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.BEST_SMRY, $scope.bestSmryGridOptions,$rootScope);
            $scope.bestSmryGridOptions = finalResults.gridOp;

            //  $scope.totalbestSmryItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.bestSmryGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalbestSmryItems = $scope.bestSmryGridOptions.data.length;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentbestSmryPage = 1;
        } else {
            //hide the grid
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.BEST_SMRY);
        }
    }

    // checkbox for name
    $scope.getConstName = function (constName) {
        
       // if ($rootScope.constituent_type == 'OR') {
           // var url = COMMON_URL + TEMPLATES.CONST_ORG_NAME;
      //  }
        //else if ($rootScope.constituent_type == 'IN') {
            var url = COMMON_URL + TEMPLATES.CONST_NAME;
       // }


        $scope.constNameTemplate = url;
        if (!constName) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showName = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstNameLoader = { "display": "block" };
            
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_NAME, $scope.constNameGridOptions, $rootScope);
            console.log(finalResults.gridOp);
            constituentDataServices.setConstNameGrid(finalResults.gridOp);
            $scope.constNameGridOptions = finalResults.gridOp;
            $scope.currentConstNamePage = 1;

            // $scope.totalConstNameItems = $scope.constNameGridOptions.totalItems;
            // console.log($scope.constNameGridOptions.data.length);

            var listener = $scope.$watch(function () {
                return $scope.constNameGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstNameItems = $scope.constNameGridOptions.data.length;
                    $scope.constNameCount = $scope.constNameGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });


        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_NAME);
        }
    }

    // checkbox for Org name
    $scope.getConstOrgName = function (constOrgName) {

        //if ($rootScope.constituent_type == 'OR') {
            var url = COMMON_URL + TEMPLATES.CONST_ORG_NAME;
        
      

        $scope.constOrgNameTemplate = url;
        if (!constOrgName) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showOrgName = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstOrgNameLoader = { "display": "block" };

            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_ORG_NAME, $scope.constOrgNameGridOptions, $rootScope);
           // console.log(finalResults.gridOp);
            //constituentDataServices.setconstOrgNameGrid(finalResults.gridOp);
            $scope.constOrgNameGridOptions = finalResults.gridOp;
            $scope.currentconstOrgNamePage = 1;

            var listener = $scope.$watch(function () {
                return $scope.constOrgNameGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstOrgNameItems = $scope.constOrgNameGridOptions.data.length;
                    $scope.constOrgNameCount = $scope.constOrgNameGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });


        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_ORG_NAME);
        }
    }


    

    //checkbox for address
    $scope.getConstAddress = function (constAddress) {
        var url = COMMON_URL + TEMPLATES.CONST_ADDRESS;
        $scope.constAddressTemplate = url;
        if (!constAddress) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showAddress = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstAddressLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_ADDRESS, $scope.constAddressGridOptions, $rootScope);
            $scope.constAddressGridOptions = finalResults.gridOp;


            // $scope.totalcontAddressItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.constAddressGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalcontAddressItems = $scope.constAddressGridOptions.data.length;
                    $scope.constAddressCount = $scope.constAddressGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstAddress = 1;
        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_ADDRESS);
        }
    }

    //expand address grid
    /*  $scope.expandNameGrid = function () {
          $scope.constNameGridOptions = constMultiGridService.getMultiExpandedGrid(CONSTANTS.CONST_NAME,
              $scope.constAddressGridOptions,uiGridConstants);
          $scope.constNameGridApi.core.refresh();
      }*/

    //checkbox for Phone
    $scope.getConstPhone = function (constPhone) {
        var url = COMMON_URL + TEMPLATES.CONST_PHONE;
        $scope.constPhoneTemplate = url;

        if (!constPhone) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showPhone = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstPhoneLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_PHONE, $scope.constPhoneGridOptions, $rootScope);
            $scope.constPhoneGridOptions = finalResults.gridOp;

            //$scope.totalConstPhoneItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.constPhoneGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstPhoneItems = $scope.constPhoneGridOptions.data.length;
                    $scope.constPhoneCount = $scope.constPhoneGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstPhonePage = 1;
        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_PHONE);
        }
    }

    //checkbox for email
    $scope.getConstEmail = function (constEmail) {
        var url = COMMON_URL + TEMPLATES.CONST_EMAIL;
        $scope.constEmailTemplate = url;

        if (!constEmail) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showEmail = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstEmailLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_EMAIL, $scope.constEmailGridOptions, $rootScope);
            $scope.constEmailGridOptions = finalResults.gridOp;

            // $scope.totalConstEmailItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.constEmailGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstEmailItems = $scope.constEmailGridOptions.data.length;
                    $scope.constEmailCount = $scope.constEmailGridOptions.data[0].distinct_count;
                    //console.log("+++++");
                   // console.log($scope.constEmailGridOptions.data);
                   // console.log("++++");
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstEmailPage = 1;


        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_EMAIL);
        }
    };

    //checkbox for external bridge
    $scope.getConstExtBridge = function (constExtBridge) {
        var url = COMMON_URL + TEMPLATES.CONST_EXT_BRIDGE;
        $scope.constExtBridgeTemplate = url;

        if (!constExtBridge) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showExternal = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstExtBridgeLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_EXT_BRIDGE, $scope.constExtBridgeGridOptions, $rootScope);
            $scope.constExtBridgeGridOptions = finalResults.gridOp;

            $scope.totalConstExtBridgeItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.constExtBridgeGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstExtBridgeItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstExtBridgePage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_EXT_BRIDGE);
        }
    };

    //checkbox for Birth
    $scope.getConstBirth = function (constBirth) {
        var url = COMMON_URL + TEMPLATES.CONST_BIRTH;
        $scope.constBirthTemplate = url;

        if (!constBirth) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showBirth = {
                showAllButtonName: CONST_SHOW
            }
            $scope.toggleConstBirthLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
               constMultiGridService, CONSTANTS.CONST_BIRTH, $scope.constBirthGridOptions, $rootScope);
            $scope.constBirthGridOptions = finalResults.gridOp;

            //  $scope.totalConstBirthItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.constBirthGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstBirthItems = $scope.constBirthGridOptions.data.length;
                    $scope.constBirthCount = $scope.constBirthGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstBirthPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_BIRTH);
        }
    };

    //checkbox for Death
    $scope.getConstDeath = function (constDeath) {
        var url = COMMON_URL + TEMPLATES.CONST_DEATH;
        $scope.constDeathTemplate = url;

        if (!constDeath) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showDeath = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleConstDeathLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_DEATH, $scope.constDeathGridOptions, $rootScope);
            $scope.constDeathGridOptions = finalResults.gridOp;


            var listener = $scope.$watch(function () {
                return $scope.constDeathGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstDeathItems = $scope.constDeathGridOptions.data.length;
                    $scope.constDeathCount = $scope.constDeathGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstDeathPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_DEATH);
        }
    };

    //checkbox for Contact Preference
    $scope.getContactPref = function (contactPref) {
        var url = COMMON_URL + TEMPLATES.CONST_PREF;
        $scope.constPrefTemplate = url;
       // console.log(contactPref);

        if (!contactPref) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showContactPref = {
                showAllButtonName: CONST_SHOW
                //toggleContactPref: false
            };
            $scope.toggleConstPrefLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CONST_PREF, $scope.constPrefGridOptions, $rootScope);
            $scope.constPrefGridOptions = finalResults.gridOp;

            //  $scope.totalConstPrefItems = finalResults.itemCount;

            var listener = $scope.$watch(function () {
                return $scope.constPrefGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalConstPrefItems = newLength;
                    $scope.constPrefCount = $scope.constPrefGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentConstPrefPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CONST_PREF);
        }
    };



    //checkbox for Characteristics
    $scope.getCharacteristics = function (characteristics) {
        var url = COMMON_URL + TEMPLATES.CHARACTERISTICS;
        $scope.characterTemplate = url;

        if (!characteristics) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showCharacteristics = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleCharacteristicsLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.CHARACTERISTICS, $scope.characteristicsGridOptions, $rootScope);
            $scope.characteristicsGridOptions = finalResults.gridOp;

            // $scope.totalCharacteristicsItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.characteristicsGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalCharacteristicsItems = newLength;
                    $scope.constCharacterCount = $scope.characteristicsGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentCharacteristicsPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.CHARACTERISTICS);
        }
    };


    //checkbox for Group Membership
    $scope.getGrpMembership = function (grpMembership) {
        var url = COMMON_URL + TEMPLATES.GRP_MEMBERSHIP;
        $scope.grpMembershipTemplate = url;

        if (!grpMembership) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showGrpMembership = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleGrpMembershipLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.GRP_MEMBERSHIP, $scope.grpMembershipGridOptions, $rootScope);
            $scope.grpMembershipGridOptions = finalResults.gridOp;

            // $scope.totalGrpMembershipItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.grpMembershipGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalGrpMembershipItems = newLength;
                    $scope.constGrpMembershipCount = $scope.grpMembershipGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentgrpMembershipsPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.GRP_MEMBERSHIP);
        }
    };

    //checkbox for Transaction History
    $scope.getTransHistory = function (transHistory) {
        var url = COMMON_URL + TEMPLATES.TRANS_HISTORY;
        $scope.transHistoryTemplate = url;

        if (!transHistory) {
            // $scope.constName =true;
            $scope.toggleTransHistoryLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.TRANS_HISTORY, $scope.transHistoryGridOptions, $rootScope);
            $scope.transHistoryGridOptions = finalResults.gridOp;

            //  $scope.totalTransHistoryItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.transHistoryGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalTransHistoryItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentTransHistoryPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.TRANS_HISTORY);
        }
    };

    //checkbox for Anonymous Information
    $scope.getAnonInfo = function (anonInfo) {
        var url = COMMON_URL + TEMPLATES.ANON_INFO;
        $scope.anonInfoTemplate = url;

        if (!anonInfo) {
            $scope.toggleAnonInfoLoader = { "display": "block" };
            var finalResutls = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.ANON_INFO, $scope.anonInfoGridOptions, $rootScope);
            $scope.anonInfoGridOptions = finalResutls.gridOp;

            //  $scope.totalAnonInfoItems = finalResutls.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.anonInfoGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalAnonInfoItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentAnonInfoPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.ANON_INFO);
        }
    };


    //checkbox for Mastering Attempts
    $scope.getMasterAttempts = function (masterAttempt) {
        var url = COMMON_URL + TEMPLATES.MASTER_ATTEMPT;
        $scope.masterAttemptTemplate = url;

        if (!masterAttempt) {
            // $scope.constName =true;
            $scope.toggleMasterAttemptLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.MASTER_ATTEMPT, $scope.masterAttemptGridOptions, $rootScope);
            $scope.masterAttemptGridOptions = finalResults.gridOp;

            $scope.totalMasterAttemptItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.masterAttemptGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalMasterAttemptItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentMasterAttemptPage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.MASTER_ATTEMPT);
        }
    };

    //checkbox for Internal Bridge
    $scope.getInternalBridge = function (internalBridge) {
        var url = COMMON_URL + TEMPLATES.INTERNAL_BRIDGE;
        $scope.internalBridgeTemplate = url;

        if (!internalBridge) {
            //added this because the user wants to see show All Records link at the right corner
            $scope.showInternalBridge = {
                showAllButtonName: CONST_SHOW
            };
            $scope.toggleInternalBridgeLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.INTERNAL_BRIDGE, $scope.internalBridgeGridOptions, $rootScope);
            $scope.internalBridgeGridOptions = finalResults.gridOp;

            // $scope.totalInternalBridgeItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.internalBridgeGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalInternalBridgeItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentInternalBridgePage = 1;

        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.INTERNAL_BRIDGE);
        }
    };

    //checkbox for Merge History
    $scope.getMergeHistory = function (mergeHistory) {
        var url = COMMON_URL + TEMPLATES.MERGE_HISTORY;
        $scope.mergeHistoryTemplate = url;

        if (!mergeHistory) {

            $scope.toggleMergeHistoryLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.MERGE_HISTORY, $scope.mergeHistoryGridOptions, $rootScope);
            $scope.mergeHistoryGridOptions = finalResults.gridOp;
            // $scope.totalMergeHistoryItems = finalResults.itemCount;
            var listener = $scope.$watch(function () {
                return $scope.mergeHistoryGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalMergeHistoryItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentMergeHistoryPage = 1;
        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.MERGE_HISTORY);
        }
    };

    
    //checkbox for affiliators
    $scope.getAffiliator = function (affiliator) {
        var url = COMMON_URL + TEMPLATES.AFFILIATOR;
        $scope.affiliatorTemplate = url;

        if (!affiliator) {

            $scope.toggleAffiliatorLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.AFFILIATOR, $scope.affiliatorGridOptions, $rootScope);
            $scope.affiliatorGridOptions = finalResults.gridOp;

            var listener = $scope.$watch(function () {
                return $scope.affiliatorGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalAffiliatorItems = newLength;
                    $scope.affiliatorCount = $scope.affiliatorGridOptions.data[0].distinct_count;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentAffiliatorPage = 1;
        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.AFFILIATOR);
        }
    };

    //checkbox for affiliators
    $scope.getSummaryView = function (smryView) {
        var url = COMMON_URL + TEMPLATES.SUMMARY_VIEW;
        //used in constmultidetail section ng-include
        $scope.summaryViewTemplate = url;

        if (!smryView) {

            $scope.toggleSummaryViewLoader = { "display": "block" };
            var finalResults = callConstSetup($scope, constituentApiService, $scope.param, uiGridConstants, constMultiDataService,
                constMultiGridService, CONSTANTS.SUMMARY_VIEW, $scope.summaryViewGridOptions, $rootScope);
            $scope.summaryViewGridOptions = finalResults.gridOp;

            var listener = $scope.$watch(function () {
                return $scope.summaryViewGridOptions.data.length;
            }, function (newLength, oldLength) {
                if (newLength > 0) {
                    $scope.totalSummaryViewItems = newLength;
                    //unbind watch after use
                    listener();
                }
            });
            $scope.currentSummaryViewPage = 1;
        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.SUMMARY_VIEW);
        }
    };

    $scope.toggleSummary = false;
    //checkbox for affiliators
    $scope.getMasterDetail = function (masterDetail) {
        var url = COMMON_URL + TEMPLATES.MASTER_DETAIL;
        //used in constmultidetail section ng-include
        $scope.masterDetailTemplate = url;
        $scope.toggleMasterDetailLoader = { "display": "none" };
        if (!masterDetail) {
            $scope.toggleMasterDetailHeader = { "display": "block" };
            $scope.liMstrDetail = "";
        } else {
            constMultiGridService.getToggleDetails($scope, false, CONSTANTS.MASTER_DETAIL);
            $scope.liMstrDetail = "";
        }
    };

    $scope.pageChanged = function (type, page) {
        switch (type) {
            case CONSTANTS.BEST_SMRY: {
                $scope.bestSmryGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_NAME: {
                $scope.constNameGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_ORG_NAME: {
                $scope.constOrgNameGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_ADDRESS: {
                $scope.constAddressGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_BIRTH: {
                $scope.constBirthGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_DEATH: {
                $scope.constDeathGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_EMAIL: {
                $scope.constEmailGridApi.pagination.seek(page);
                break;
            }

            case CONSTANTS.CONST_EXT_BRIDGE: {
                $scope.constExtBridgeGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CHARACTERISTICS: {
                $scope.characteristicsGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.ANON_INFO: {
                $scope.anonInfoGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_PHONE: {
                $scope.constPhoneGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.CONST_PREF: {
                $scope.constPrefGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.GRP_MEMBERSHIP: {
                $scope.grpMembershipGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.INTERNAL_BRIDGE: {
                $scope.internalBridgeGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.MASTER_ATTEMPT: {
                $scope.masterAttemptGridApi.pagination.seek(page);
                break;
            }
            case $scope.CONSTANTS.TRANS_HISTORY: {
                $scope.transHistoryGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.MERGE_HISTORY: {
                $scope.mergeHistoryGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.AFFILIATOR: {
                $scope.affiliatorGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.SUMMARY_VIEW: {
                $scope.summaryViewGridApi.pagination.seek(page);
                break;
            }
            case CONSTANTS.MASTER_DETAIL: {
                $scope.masterDetailGridApi.pagination.seek(page);
                break;
            }
        }

    };



    $rootScope.toggleFiltering = function (type) {
        switch (type) {
            case CONSTANTS.BEST_SMRY: {
                $scope.bestSmryGridOptions.enableFiltering = !$scope.bestSmryGridOptions.enableFiltering;
                $scope.bestSmryGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_NAME: {
                $scope.constNameGridOptions.enableFiltering = !$scope.constNameGridOptions.enableFiltering;
                $scope.constNameGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_ORG_NAME: {
                $scope.constOrgNameGridOptions.enableFiltering = !$scope.constOrgNameGridOptions.enableFiltering;
                $scope.constOrgNameGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_ADDRESS: {
                $scope.constAddressGridOptions.enableFiltering = !$scope.constAddressGridOptions.enableFiltering;
                $scope.constAddressGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_BIRTH: {
                $scope.constBirthGridOptions.enableFiltering = !$scope.constBirthGridOptions.enableFiltering;
                $scope.constBirthGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_DEATH: {
                $scope.constDeathGridOptions.enableFiltering = !$scope.constDeathGridOptions.enableFiltering;
                $scope.constDeathGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_EMAIL: {
                $scope.constEmailGridOptions.enableFiltering = !$scope.constEmailGridOptions.enableFiltering;
                $scope.constEmailGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }

            case CONSTANTS.CONST_EXT_BRIDGE: {
                $scope.constExtBridgeGridOptions.enableFiltering = !$scope.constExtBridgeGridOptions.enableFiltering;
                $scope.constExtBridgeGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CHARACTERISTICS: {
                $scope.characteristicsGridOptions.enableFiltering = !$scope.characteristicsGridOptions.enableFiltering;
                $scope.characteristicsGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.ANON_INFO: {
                $scope.anonInfoGridOptions.enableFiltering = !$scope.anonInfoGridOptions.enableFiltering;
                $scope.anonInfoGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_PHONE: {
                $scope.constPhoneGridOptions.enableFiltering = !$scope.constPhoneGridOptions.enableFiltering;
                $scope.constPhoneGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.CONST_PREF: {
                $scope.constPrefGridOptions.enableFiltering = !$scope.constPrefGridOptions.enableFiltering;
                $scope.constPrefGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.GRP_MEMBERSHIP: {
                $scope.grpMembershipGridOptions.enableFiltering = !$scope.grpMembershipGridOptions.enableFiltering;
                $scope.grpMembershipGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.INTERNAL_BRIDGE: {
                $scope.internalBridgeGridOptions.enableFiltering = !$scope.internalBridgeGridOptions.enableFiltering;
                $scope.internalBridgeGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.MASTER_ATTEMPT: {
                $scope.masterAttemptGridOptions.enableFiltering = !$scope.masterAttemptGridOptions.enableFiltering;
                $scope.masterAttemptGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.TRANS_HISTORY: {
                $scope.transHistoryGridOptions.enableFiltering = !$scope.transHistoryGridOptions.enableFiltering;
                $scope.transHistoryGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.MERGE_HISTORY: {
                $scope.mergeHistoryGridOptions.enableFiltering = !$scope.mergeHistoryGridOptions.enableFiltering;
                $scope.mergeHistoryGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.AFFILIATOR: {
                $scope.affiliatorGridOptions.enableFiltering = !$scope.affiliatorGridOptions.enableFiltering;
                $scope.affiliatorGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.SUMMARY_VIEW: {
                $scope.summaryViewGridOptions.enableFiltering = !$scope.summaryViewGridOptions.enableFiltering;
                $scope.summaryViewGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
            case CONSTANTS.MASTER_DETAIL: {
                $scope.masterDetailGridOptions.enableFiltering = !$scope.masterDetailGridOptions.enableFiltering;
                $scope.masterDetailGridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
                break;
            }
                //this is used in multi coz we donnot get constants instead we get the grid, this is used in the extra X mark that comes at the end
            default: {
                type.options.enableFiltering = !type.options.enableFiltering;
                type.api.core.notifyDataChange(uiGridConstants.dataChange.ALL);
            }
        }

    }

    $scope.toggleMenuPreferences = function () {

    }


   

    $scope.commonDeleteGridRow = function (row, grid) {
        var className = grid.element[0].className.match(/([^\s]+)/)[0];
        switch (className) {
            case HOME_CONSTANTS.CONST_PHONE: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constPhoneGridOptions, HOME_CONSTANTS.CONST_PHONE);
              
                break;
            }
            case HOME_CONSTANTS.CONST_EMAIL: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constEmailGridOptions, HOME_CONSTANTS.CONST_EMAIL);
                
                break;
            }
            case HOME_CONSTANTS.CONST_DEATH: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constDeathGridOptions, HOME_CONSTANTS.CONST_DEATH);
               
                break;
            }
            case HOME_CONSTANTS.CONST_BIRTH: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constBirthGridOptions, HOME_CONSTANTS.CONST_BIRTH);
               
                break;
            }
            case HOME_CONSTANTS.CONST_ADDRESS: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constAddressGridOptions, HOME_CONSTANTS.CONST_ADDRESS);
              
                break;
            }
            case HOME_CONSTANTS.CONST_PREF: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constPrefGridOptions, HOME_CONSTANTS.CONST_PREF);
              
                break;
            }
            case HOME_CONSTANTS.GRP_MEMBERSHIP: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.grpMembershipGridOptions, HOME_CONSTANTS.GRP_MEMBERSHIP);
               
                break;
            }
            case HOME_CONSTANTS.CHARACTERISTICS: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.characteristicsGridOptions, HOME_CONSTANTS.CHARACTERISTICS);
               
                break;
            }
            case HOME_CONSTANTS.CONST_NAME: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constNameGridOptions, HOME_CONSTANTS.CONST_NAME);
               
                break;
            }
            case HOME_CONSTANTS.CONST_ORG_NAME: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constOrgNameGridOptions, HOME_CONSTANTS.CONST_ORG_NAME);
                break;
            }
            case HOME_CONSTANTS.AFFILIATOR: {
                commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.affiliatorGridOptions, HOME_CONSTANTS.AFFILIATOR);
                break;
            }

        }

    }

    $scope.commonAddGridRow = function (type) {


        switch (type) {
            case HOME_CONSTANTS.CONST_PHONE: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constPhoneGridOptions);
                break;
            }
            case HOME_CONSTANTS.CONST_EMAIL: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constEmailGridOptions);

                break;
            }
            case HOME_CONSTANTS.CONST_DEATH: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constDeathGridOptions);

                break;
            }
            case HOME_CONSTANTS.CONST_BIRTH: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constBirthGridOptions);

                break;
            }
            case HOME_CONSTANTS.CONST_ADDRESS: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constAddressGridOptions);

                break;
            }
            case HOME_CONSTANTS.CONST_PREF: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constPrefGridOptions);

                break;
            }
            case HOME_CONSTANTS.GRP_MEMBERSHIP: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.grpMembershipGridOptions);

                break;
            }
            case HOME_CONSTANTS.CHARACTERISTICS: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.characteristicsGridOptions);

                break;
            }

            case HOME_CONSTANTS.CONST_NAME: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constNameGridOptions);
                break;
            }
            case HOME_CONSTANTS.CONST_ORG_NAME: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.constOrgNameGridOptions);
                break;
            }
            case HOME_CONSTANTS.AFFILIATOR: {
                commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.affiliatorGridOptions);
                break;
            }

        }
    }

    $scope.commonEditGridRow = function (row, grid) {
        var className = grid.element[0].className.match(/([^\s]+)/)[0];
        switch (className) {
            case HOME_CONSTANTS.CONST_PHONE: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams,$scope.constPhoneGridOptions,HOME_CONSTANTS.CONST_PHONE);
                break;
            }
            case HOME_CONSTANTS.CONST_EMAIL: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constEmailGridOptions, HOME_CONSTANTS.CONST_EMAIL);

                break;
            }
            case HOME_CONSTANTS.CONST_DEATH: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constDeathGridOptions, HOME_CONSTANTS.CONST_DEATH);

                break;
            }
            case HOME_CONSTANTS.CONST_BIRTH: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constBirthGridOptions, HOME_CONSTANTS.CONST_BIRTH);

                break;
            }
            case HOME_CONSTANTS.CONST_ADDRESS: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constAddressGridOptions, HOME_CONSTANTS.CONST_ADDRESS);

                break;
            }
            case HOME_CONSTANTS.CONST_PREF: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constPrefGridOptions, HOME_CONSTANTS.CONST_PREF);

                break;
            }
            case HOME_CONSTANTS.GRP_MEMBERSHIP: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.grpMembershipGridOptions, HOME_CONSTANTS.GRP_MEMBERSHIP);

                break;
            }
            case HOME_CONSTANTS.CHARACTERISTICS: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.characteristicsGridOptions, HOME_CONSTANTS.CHARACTERISTICS);

                break;
            }
            case HOME_CONSTANTS.CONST_NAME: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constNameGridOptions, HOME_CONSTANTS.CONST_NAME);
                break;
            }
            case HOME_CONSTANTS.CONST_ORG_NAME: {
                commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.constOrgNameGridOptions, HOME_CONSTANTS.CONST_ORG_NAME);
                break;
            }

        }
    }
    //add to unmerge cart!!
    $scope.unmerge.addTocart = function () {
        $scope.unmerge.rows = $scope.constExtBridgeGridApi.selection.getSelectedRows();

        angular.forEach($scope.unmerge.rows, function (k, v) {
            {
                if (k.$$hashKey != null) {
                    k.IndexString = k.$$hashKey;
                    delete k.$$hashKey;
                }
                //added new column for type of the record
                k["constituent_type"] = $scope.constituent_type;
            }
        });

        //console.log($scope.unmerge.rows);
        if ($scope.unmerge.rows.length > 0) {
            constituentServices.addUnmergeToCart(JSON.stringify($scope.unmerge.rows)).success(function (result) {
                $rootScope.ConfirmationMessage = CART.CONFIRMATION_MESSAGE;
                $rootScope.FinalMessage = '';
                $rootScope.ReasonOrTransKey = CART.REASON;
                angular.element("#iConfirmationModal").modal();
                constituentServices.getCartResults().success(function (result) {
                    var carItemsNumber = result.length;
                    $rootScope.CartItemsNumber = carItemsNumber;

                    constituentServices.getUnmergeCartResults().success(function (result) {
                        constituentDataServices.setUnmergeCartData(result);
                        $rootScope.CartItemsNumber = $rootScope.CartItemsNumber + result.length;
                        if ($rootScope.CartItemsNumber > 0) {

                            $rootScope.CartVisibility = true;
                        }
                        else {
                            $rootScope.CartVisibility = false;
                        }
                    }).error(function (result) {
                        unmergeErrorPopup(result);
                    });

                }).error(function (result) {
                    unmergeErrorPopup(result);
                });

               
            }).error(function (result) {
                unmergeErrorPopup(result);
            });
        }
        else {
            messagePopup($rootScope, CART.SELECT_MESSAGE, "Alert");
        }
    }

    function unmergeErrorPopup(result) {
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


    var CONST_SHOW = "Show All Records";
    var CONST_HIDE = "Hide Inactive";

    $scope.showAddress = {
        showAllButtonName: CONST_SHOW
    };
    $scope.showName = {
        showAllButtonName: CONST_SHOW
    };
    $scope.showOrgName = {
        showAllButtonName: CONST_SHOW
    };
    $scope.showPhone = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showBirth = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showDeath = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showEmail = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showExternal = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showContactPref = {
        showAllButtonName: CONST_SHOW
        //toggleContactPref: false
    }
    $scope.showCharacteristics = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showGrpMembership = {
        showAllButtonName: CONST_SHOW
    }
    $scope.showInternalBridge = {
        showAllButtonName: CONST_SHOW
    }



    // this is used for show all records event
    $scope.showAllrecords = function (type) {

        if (type == HOME_CONSTANTS.CONST_ADDRESS) {
            switch ($scope.showAddress.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showAddress.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_ADDRESS, $scope.constAddressGridOptions);
                    //console.log(result);
                    $scope.constAddressGridOptions = result.gridOp;
                    $scope.totalcontAddressItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showAddress.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_ADDRESS, $scope.constAddressGridOptions);
                    //console.log(result);
                    $scope.constAddressGridOptions = result.gridOp;
                    $scope.totalcontAddressItems = result.itemCount;
                    break;
                }
            }
        }
        
        if (type == HOME_CONSTANTS.CONST_NAME) {
            switch ($scope.showName.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showName.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_NAME, $scope.constNameGridOptions);
                    //console.log(result);
                    $scope.constNameGridOptions = result.gridOp;
                    $scope.totalcontAddressItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showName.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_NAME, $scope.constNameGridOptions);
                    //console.log(result);
                    $scope.constNameGridOptions = result.gridOp;
                    $scope.totalConstNameItems = result.itemCount;
                    break;
                }
            }
        }
        if (type == HOME_CONSTANTS.CONST_ORG_NAME) {
            switch ($scope.showOrgName.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showOrgName.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_ORG_NAME, $scope.constOrgNameGridOptions);
                    //console.log(result);
                    $scope.constOrgNameGridOptions = result.gridOp;
                    $scope.totalConstOrgNameItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showOrgName.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_ORG_NAME, $scope.constOrgNameGridOptions);
                    //console.log(result);
                    $scope.constOrgNameGridOptions = result.gridOp;
                    $scope.totalConstOrgNameItems = result.itemCount;
                    break;
                }
            }
        }
        
        if (type == HOME_CONSTANTS.CONST_PHONE) {
            switch ($scope.showPhone.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showPhone.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_PHONE, $scope.constPhoneGridOptions);
                    //console.log(result);
                    $scope.constPhoneGridOptions = result.gridOp;
                    $scope.totalConstPhoneItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showPhone.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_PHONE, $scope.constPhoneGridOptions);
                    //console.log(result);
                    $scope.constPhoneGridOptions = result.gridOp;
                    $scope.totalConstPhoneItems = result.itemCount;
                    break;
                }
            }
        }
       
        if (type == HOME_CONSTANTS.CONST_BIRTH) {
            switch ($scope.showBirth.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showBirth.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_BIRTH, $scope.constBirthGridOptions);
                    //console.log(result);
                    $scope.constBirthGridOptions = result.gridOp;
                    $scope.totalConstBirthItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showBirth.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_BIRTH, $scope.constBirthGridOptions);
                    //console.log(result);
                    $scope.constBirthGridOptions = result.gridOp;
                    $scope.totalConstBirthItems = result.itemCount;
                    break;
                }
            }
        }
        if (type == HOME_CONSTANTS.CONST_DEATH) {
            switch ($scope.showDeath.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showDeath.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_DEATH, $scope.constDeathGridOptions);
                    //console.log(result);
                    $scope.constDeathGridOptions = result.gridOp;
                    $scope.totalConstDeathItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showDeath.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_DEATH, $scope.constDeathGridOptions);
                    //console.log(result);
                    $scope.constDeathGridOptions = result.gridOp;
                    $scope.totalConstDeathItems = result.itemCount;
                    break;
                }
            }
        }
        
        if (type == HOME_CONSTANTS.CONST_EMAIL) {
            switch ($scope.showEmail.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showEmail.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_EMAIL, $scope.constEmailGridOptions);
                    //console.log(result);
                    $scope.constEmailGridOptions = result.gridOp;
                    $scope.totalConstEmailItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showEmail.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_EMAIL, $scope.constEmailGridOptions);
                    //console.log(result);
                    $scope.constEmailGridOptions = result.gridOp;
                    $scope.totalConstEmailItems = result.itemCount;
                    break;
                }
            }
        }
        

        if (type == HOME_CONSTANTS.CONST_EXT_BRIDGE) {
            switch ($scope.showExternal.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showExternal.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_EXT_BRIDGE, $scope.constExtBridgeGridOptions);
                    //console.log(result);
                    $scope.constExtBridgeGridOptions = result.gridOp;
                    $scope.totalConstExtBridgeItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showExternal.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_EXT_BRIDGE, $scope.constExtBridgeGridOptions);
                    //console.log(result);
                    $scope.constExtBridgeGridOptions = result.gridOp;
                    $scope.totalConstExtBridgeItems = result.itemCount;
                    break;
                }
            }
        }
        if (type == HOME_CONSTANTS.CONST_PREF) {
            switch ($scope.showContactPref.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showContactPref.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_PREF, $scope.constPrefGridOptions);
                    //console.log(result);
                    $scope.constPrefGridOptions = result.gridOp;
                    $scope.totalConstPrefItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showContactPref.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CONST_PREF, $scope.constPrefGridOptions);
                    //console.log(result);
                    $scope.constPrefGridOptions = result.gridOp;
                    $scope.totalConstPrefItems = result.itemCount;
                    break;
                }
            }
        }
        
        if (type == HOME_CONSTANTS.CHARACTERISTICS) {
            switch ($scope.showCharacteristics.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showCharacteristics.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CHARACTERISTICS, $scope.characteristicsGridOptions);
                    //console.log(result);
                    $scope.characteristicsGridOptions = result.gridOp;
                    $scope.totalCharacteristicsItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showCharacteristics.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.CHARACTERISTICS, $scope.characteristicsGridOptions);
                    //console.log(result);
                    $scope.characteristicsGridOptions = result.gridOp;
                    $scope.totalCharacteristicsItems = result.itemCount;
                    break;
                }
            }
        }
        
        if (type == HOME_CONSTANTS.GRP_MEMBERSHIP) {
            switch ($scope.showGrpMembership.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showGrpMembership.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.GRP_MEMBERSHIP, $scope.grpMembershipGridOptions);
                    //console.log(result);
                    $scope.grpMembershipGridOptions = result.gridOp;
                    $scope.totalGrpMembershipItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showGrpMembership.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.GRP_MEMBERSHIP, $scope.grpMembershipGridOptions);
                    //console.log(result);
                    $scope.grpMembershipGridOptions = result.gridOp;
                    $scope.totalGrpMembershipItems = result.itemCount;
                    break;
                }
            }
        }
       
        if (type == HOME_CONSTANTS.INTERNAL_BRIDGE) {
            switch ($scope.showInternalBridge.showAllButtonName) {
                case CONST_HIDE: {
                    $scope.showInternalBridge.showAllButtonName = CONST_SHOW;
                    var result = setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.INTERNAL_BRIDGE, $scope.internalBridgeGridOptions);
                    //console.log(result);
                    $scope.internalBridgeGridOptions = result.gridOp;
                    $scope.totalInternalBridgeItems = result.itemCount;
                    break;
                }
                case CONST_SHOW: {
                    $scope.showInternalBridge.showAllButtonName = CONST_HIDE;
                    var result = setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
                                  constMultiGridService, HOME_CONSTANTS.INTERNAL_BRIDGE, $scope.internalBridgeGridOptions);
                    //console.log(result);
                    $scope.internalBridgeGridOptions = result.gridOp;
                    $scope.totalInternalBridgeItems = result.itemCount;
                    break;
                }
            }
        }
        
       
       
    }



    $scope.phoneResearchOneRecord = function (row,grid) {
        console.log(row);
        var NAVIGATE_URL = BasePath + "App/constituent/search"
        var postParams = {
            "phone": row.cnst_phn_num,
            "type": $sessionStorage.type
        }
        constituentDataServices.setPhoneSearchParams(postParams);
        $location.url(NAVIGATE_URL);
    }

    $scope.phoneResearchAllRecords = function () {
        var NAVIGATE_URL = BasePath + "App/constituent/search"
        var results = constMultiDataService.getData(HOME_CONSTANTS.CONST_PHONE);

        if (results.length > 0) {
            for (var i = 0; i < results.length; i++) {
                var postParams = {
                    "phone": results[i].cnst_phn_num,
                    "type": $sessionStorage.type
                }
                constituentDataServices.setPhoneSearchParams(postParams);
            }
            $location.url(NAVIGATE_URL);
        }
        else {
            messagePopup($rootScope,"No Records found to search","Alert");
        }
       
    }   
    // for hiding side panel automatically
    $timeout(function () {
        //$scope.toggleMultiSidePanel = !$scope.toggleMultiSidePanel;
        console.log($sessionStorage.hideSideMenu);
        if (angular.isUndefined($sessionStorage.hideSideMenu)) {
            $scope.hideMultiSidePanel = true;
        }
        else if ($sessionStorage.hideSideMenu) {
            $scope.hideMultiSidePanel = true;
        }
        else {
            $scope.hideMultiSidePanel = false;
        }
        
    }, 10);


    //for hiding/showing side panel 
    $scope.hideSideMenu = function () {
        $sessionStorage.hideSideMenu = true;
        $scope.hideMultiSidePanel = $sessionStorage.hideSideMenu;
    }

    $scope.showSideMenu = function () {
        $sessionStorage.hideSideMenu = false;
        $scope.hideMultiSidePanel = $sessionStorage.hideSideMenu;
        
    }


    // added for CEM functionality
    var cemInitialize = function () {
        $scope.cem = {
            masterId: $stateParams.constituentId,
            BASE_URL: BasePath + 'App/Constituent/Views/cem',
          
            CONSTANTS: {
                MSG_PREF: CEM_CONSTANTS.MSG_PREF,
                PREF_LOCATION: CEM_CONSTANTS.PREF_LOCATION,
                DNC_SURFACE : CEM_CONSTANTS.DNC_SURFACE,
                GRP_MEMBRSHP : CEM_CONSTANTS.GRP_MEMBRSHP,
               
            },
            toggleMsgPref: false,
            togglePrefLoc: false,
            toggleDnc: false,
            toggleGrpMembrshp: false,           
            toggleCemAddButton: true
        }

      

        StoreData.cleanData();
        var userTabPermission = constMultiGridService.getTabDenyPermission();
        if (userTabPermission) {
            //this is a common varaible for all sections
            $scope.cem.toggleCemAddButton = false;
        }

    }

    cemInitialize();


    $scope.cem.redirectTo = function (type) {

        switch (type) {
            case $scope.cem.CONSTANTS.MSG_PREF: {
                $scope.cem.msgPrefTemplate = $scope.cem.BASE_URL + '/ConstCemMsgPref.tpl.html';
                $scope.$broadcast('message-preference', { 'cem_msg_pref': $scope.cem.toggleMsgPref });
                //console.log(" chan");
                break;
            }
            case $scope.cem.CONSTANTS.PREF_LOCATION: {
                $scope.cem.prefLocTemplate = $scope.cem.BASE_URL + '/ConstCemPrefLocator.tpl.html';
                $scope.$broadcast('preference-locator', { 'cem_pref_locator': $scope.cem.togglePrefLoc });
                //console.log(" loc");
                break;
            }
            case $scope.cem.CONSTANTS.DNC_SURFACE: {
                $scope.cem.dncTemplate = $scope.cem.BASE_URL + '/ConstCemDnc.tpl.html';
                $scope.$broadcast('dnc', { 'cem_dnc': $scope.cem.toggleDnc });
                //console.log(" dnc");
                break;
            }

            case $scope.cem.CONSTANTS.GRP_MEMBRSHP: {
                $scope.cem.grpMembrshpTemplate = $scope.cem.BASE_URL + '/ConstCemGrpMembrshp.tpl.html';
                $scope.$broadcast('grpMembrshp', { 'cem_grpMembrshp': $scope.cem.toggleGrpMembrshp });
                //console.log(" grp membership");
                break;
            }

           
        }
    }


    var extendedFuncInitialize = function () {
        // NON CEM functionality
        $scope.multi = {
            EXTENDED_BASE_URL: BasePath + 'App/Constituent/Views/multi',
            CONSTANTS: {
                ALTERNATE_IDS: EXTENDED_CONSTANTS.ALTERNATE_IDS
            },
            toggleAlternateIds: false,
        }
    }
    extendedFuncInitialize();
    
    $scope.multi.redirectTo = function (type) {
        
        switch (type) {
            case $scope.multi.CONSTANTS.ALTERNATE_IDS: {
                $scope.multi.alternateIdsTemplate = $scope.multi.EXTENDED_BASE_URL + '/ConstAlternateIds.tpl.html';
                $scope.$broadcast('alternate_ids', { 'alternate_ids': $scope.multi.toggleAlternateIds });
               // console.log("Firing" + type);
                break;
            }
        }
    }

}]);
/******************************** End of MultiController*****************************************/


