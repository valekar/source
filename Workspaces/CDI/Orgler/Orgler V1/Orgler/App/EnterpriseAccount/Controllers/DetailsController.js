BasePath = $("base").first().attr("href");

//Details Controller
enterpriseAccMod.controller("enterpriseOrgDetailsCtrl",
    ['$scope', '$rootScope', '$stateParams', 'enterpriseDetailsService', 'exportService', 'uiGridConstants',
        'EnterpriseAccountDataService', '$filter', 'EnterpriseAccountService', 'ENT_DETAIL_CRUD', 'EXTENDED_FUNC', 'ExtendedStoreData', 'ExtendedServices', '$templateCache',
    function ($scope, $rootScope, $stateParams, enterpriseDetailsService, exportService, uiGridConstants,
        EnterpriseAccountDataService, $filter, service, ENT_DETAIL_CRUD, EXTENDED_FUNC, ExtendedStoreData, ExtendedServices, $templateCache) {

        //Get the selected enterprise org id from the url
        $scope.selectedEntOrg = $stateParams.ent_org_id;
        $scope.selectedEntOrgNm = sessionStorage.ent_org_name;

        //User controls location
        $scope.entOrgDetailHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/EnterpriseOrgDetails.tpl.html";
        $scope.rFMDetailsHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/RFMDetails.tpl.html";
        $scope.transformationDetailHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/TransformationDetails.tpl.html";
        $scope.tagsHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/TagsDetails.tpl.html";
        $scope.entOrgHierarchyHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/EntOrgHierarchyDetails.tpl.html";
        $scope.masterBridgeLocationHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/MasterBridgeLocationDetails.tpl.html";
        $scope.masterBridgeMasterHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/MasterBridgeMasterDetails.tpl.html";
        $scope.naicsCodeStewHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/NaicsCodeStewDetails.tpl.html";
        $scope.rankingStewHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/RankingStewDetails.tpl.html";
        $scope.transactionHistoryHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/TransactionHistoryDetails.tpl.html";
        $scope.showDetailsPopupHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/ShowDetailsPopUp.tpl.html";

        $scope.editEntOrgsPopupHtml = BasePath + "App/Shared/Views/EnterpriseAccount/EnterpriseOrgsEdit.html";
        $scope.tagsAddModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Tags/Add.html";
        $scope.tagsDeleteModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Tags/Delete.html";
        $scope.transformationsDeleteModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Transformations/Delete.html";
        $scope.transformationsEditModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Transformations/Edit.html";
        $scope.transformationsAddModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Transformations/Add.html";
        $scope.transformationPatternString = "^[a-zA-Z0-9?=.*!@#$%^+&*()\"\';\.\/\{\}\\[\\]~`\\\:<> _\\-\s]{2,}$";
        $scope.affiliationsDeleteModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Affiliations/Delete.html";
        $scope.affiliationsAddModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Affiliations/Add.html";
        $scope.hierarchyDeleteModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Hierarchy/Delete.html";
        $scope.hierarchyAddModalHtml = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Hierarchy/Add.html";
        
        /* *********************** Side Menu bar on load settings *********************** */
        $scope.showSideMenuBar = false;
        $scope.showHomeSideBar = false;
        $scope.showMultiSideBar = true;
        $scope.savePref = {};
        $scope.divMenuSuccessMsg = false;
        $scope.divMenuErrorMsg = false;
        $scope.alerts = [];

        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        }

        $scope.sideMenuSavePopup = {
            template: BasePath + "App/EnterpriseAccount/Views/DetailSectionTemplates/MenuPrefPopup.html",
            title: "Menu Settings",
            showPref: false,
        }

        $scope.closePreferences = function () {
            $scope.sideMenuSavePopup.showPref = false;
            $scope.alerts = [];
        };

        //Method to save the preferences
        $scope.savePreferences = function () {
            $scope.closeAlert(0);
            var preferences = [];
            var JSONObj = {
                "Preferences": preferences
            };
            if ($scope.savePref.EntOrgDetail)
                preferences.push("EntOrgDetail");
            if ($scope.savePref.RFMDetails)
                preferences.push("RFMDetails");
            if ($scope.savePref.TransformationDetail)
                preferences.push("TransformationDetail");
            if ($scope.savePref.Tags)
                preferences.push("Tags");
            if ($scope.savePref.EntOrgHierarchy)
                preferences.push("EntOrgHierarchy");
            if ($scope.savePref.MasterBridgeLocation)
                preferences.push("MasterBridgeLocation");
            if ($scope.savePref.MasterBridgeMaster)
                preferences.push("MasterBridgeMaster");
            if ($scope.savePref.NaicsCodeStew)
                preferences.push("NaicsCodeStew");
            if ($scope.savePref.RankingStew)
                preferences.push("RankingStew");
            if ($scope.savePref.TransactionHistory)
                preferences.push("TransactionHistory");
            if ($scope.savePref.Characteristics)
                preferences.push("Characteristics")

            //Call the service method to save the preferences
            enterpriseDetailsService.SaveMenuPreference(JSONObj)
            .success(function (result) {
                if (result == "SUCCESS") {
                    $scope.alerts.push({});
                }
                else {
                    $scope.divMenuErrorMsg = true;
                }
            }).error(function (result) {
                if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                    MessagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, ENT_ACC.ACCESS_DENIED_HEADER);
                }
                else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    MessagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, ENT_ACC.TIMED_OUT_HEADER);
                }
                else if (result == CRUD_CONSTANTS.DB_ERROR) {
                    MessagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, ENT_ACC.TIMED_OUT_HEADER);

                }
            });;
        }

        /************************* Generic Methods *************************/

        function getLavelText(sectionName) {
            var headerText = '';
            switch (sectionName) {
                case "EntOrgDetail":
                    headerText = "Enterprise Organization Details";
                    break;
                case "RFMDetails":
                    headerText = "RFM Details";
                    break;
                case "TransformationDetail":
                    headerText = "Transformation Details";
                    break;
                case "Tags":
                    headerText = "Tags";
                    break;
                case "EntOrgHierarchy":
                    headerText = "Enterprise Organization Hierarchy";
                    break;
                case "MasterBridgeLocation":
                    headerText = "Affiliated Master Bridge (Location)";
                    break;
                case "MasterBridgeMaster":
                    headerText = "Affiliated Master Bridge (Master)";
                    break;
                case "NaicsCodeStew":
                    headerText = "NAICS Code Stewarding";
                    break;
                case "RankingStew":
                    headerText = "Ranking Stewarding";
                    break;
                case "TransactionHistory":
                    headerText = "Transaction History";
                    break;
            }
            return headerText;
        }

        //Hide the processing overlay at individual sections
        $scope.hideProcessingOverlay = function (sectionName) {
            switch (sectionName) {
                case "OverallDetail":
                    $scope.toggleDetailsProcessOverlay = false;
                    break;
                case "EntOrgDetail":
                    $scope.toggleEntOrgDetailProcessOverlay = false;
                    break;
                case "RFMDetails":
                    $scope.toggleRFMDetailsProcessOverlay = false;
                    break;
                case "TransformationDetail":
                    $scope.toggleTransformationDetailProcessOverlay = false;
                    break;
                case "Tags":
                    $scope.toggleTagsProcessOverlay = false;
                    break;
                case "EntOrgHierarchy":
                    $scope.toggleEntOrgHierarchyProcessOverlay = false;
                    break;
                case "MasterBridgeLocation":
                    $scope.toggleMasterBridgeLocationProcessOverlay = false;
                    break;
                case "MasterBridgeMaster":
                    $scope.toggleMasterBridgeMasterProcessOverlay = false;
                    break;
                case "NaicsCodeStew":
                    $scope.toggleNaicsCodeStewProcessOverlay = false;
                    break;
                case "RankingStew":
                    $scope.toggleRankingStewProcessOverlay = false;
                    break;
                case "TransactionHistory":
                    $scope.toggleTransactionHistoryProcessOverlay = false;
                    break;
            }
        }

        //Show the processing overlay at individual sections
        $scope.showProcessingOverlay = function (sectionName) {
            switch (sectionName) {
                case "OverallDetail":
                    $scope.toggleDetailsProcessOverlay = true;
                    break;
                case "EntOrgDetail":
                    $scope.toggleEntOrgDetailProcessOverlay = true;
                    break;
                case "RFMDetails":
                    $scope.toggleRFMDetailsProcessOverlay = true;
                    break;
                case "TransformationDetail":
                    $scope.toggleTransformationDetailProcessOverlay = true;
                    break;
                case "Tags":
                    $scope.toggleTagsProcessOverlay = true;
                    break;
                case "EntOrgHierarchy":
                    $scope.toggleEntOrgHierarchyProcessOverlay = true;
                    break;
                case "MasterBridgeLocation":
                    $scope.toggleMasterBridgeLocationProcessOverlay = true;
                    break;
                case "MasterBridgeMaster":
                    $scope.toggleMasterBridgeMasterProcessOverlay = true;
                    break;
                case "NaicsCodeStew":
                    $scope.toggleNaicsCodeStewProcessOverlay = true;
                    break;
                case "RankingStew":
                    $scope.toggleRankingStewProcessOverlay = true;
                    break;
                case "TransactionHistory":
                    $scope.toggleTransactionHistoryProcessOverlay = true;
                    break;
            }
        }

        //Toggle Sub Menu tab in the Menu Preferences
        $scope.toggleSideMenuSection = function (tabName) {
            //Home tab
            if (tabName == "Home") {
                $scope.showHomeSideBar = true;
                $scope.showMultiSideBar = false;
                clearDetailsForHomeSelection();
                setSectionalSettingsHome('EntOrgDetail');
                //Load the selected details
                processSectionalDetails('EntOrgDetail');
            }
            else if (tabName == "Multi") {
                $scope.showHomeSideBar = false;
                $scope.showMultiSideBar = true;
                clearDetailsForMultiSelection();
                setSectionalSettingMulti();
            }
        }

        //Method to revert back the section display based on Home Side Menu selection
        function clearDetailsForHomeSelection() {
            //Clear the class assignment
            $scope.EntOrgDetailHomeSelectedClass = "";
            $scope.RFMDetailsHomeSelectedClass = "";
            $scope.TransformationDetailHomeSelectedClass = "";
            $scope.TagsHomeSelectedClass = "";
            $scope.EntOrgHierarchyHomeSelectedClass = "";
            $scope.MasterBridgeLocationHomeSelectedClass = "";
            $scope.MasterBridgeMasterHomeSelectedClass = "";
            $scope.NaicsCodeStewHomeSelectedClass = "";
            $scope.RankingStewHomeSelectedClass = "";
            $scope.TransactionHistorySelectedClass = "";
            // added for home section
            $scope.CharacteristicsSelectedClass = "";
           
            //Hide all the sections
            $scope.toggleEntOrgDetailSection = false;
            $scope.toggleRFMDetailsSection = false;
            $scope.toggleTransformationDetailSection = false;
            $scope.toggleTagsSection = false;
            $scope.toggleEntOrgHierarchySection = false;
            $scope.toggleMasterBridgeLocationSection = false;
            $scope.toggleMasterBridgeMasterSection = false;
            $scope.toggleNaicsCodeStewSection = false;
            $scope.toggleRankingStewSection = false;
            $scope.toggleTransactionHistorySection = false;
           
            //characterisitics added by srini
            $scope.multi.toggleCharacteristics = false;
            $scope.multi.toggleSection(EXTENDED_FUNC.CHARACTERISTICS);


        }

        //Method to revert back the section display based on Multi Side Menu selection
        function clearDetailsForMultiSelection() {
            //Hide all the sections
            $scope.toggleEntOrgDetailSection = false;
            $scope.toggleRFMDetailsSection = false;
            $scope.toggleTransformationDetailSection = false;
            $scope.toggleTagsSection = false;
            $scope.toggleEntOrgHierarchySection = false;
            $scope.toggleMasterBridgeLocationSection = false;
            $scope.toggleMasterBridgeMasterSection = false;
            $scope.toggleNaicsCodeStewSection = false;
            $scope.toggleRankingStewSection = false;
            $scope.toggleTransactionHistorySection = false;

            //Uncheck the Checkbox in the Multi menu preference section
            $scope.entOrgDetailCbModel = false;
            $scope.rFMDetailsCbModel = false;
            $scope.transformationDetailCbModel = false;
            $scope.tagsCbModel = false;
            $scope.entOrgHierarchyCbModel = false;
            $scope.masterBridgeLocationCbModel = false;
            $scope.masterBridgeMasterCbModel = false;
            $scope.naicsCodeStewCbModel = false;
            $scope.rankingStewCbModel = false;
            $scope.transactionHistoryCbModel = false;

            //Uncheck the checkbox in the Save menu preference pop-up
            $scope.savePref.EntOrgDetail = false;
            $scope.savePref.RFMDetails = false;
            $scope.savePref.TransformationDetail = false;
            $scope.savePref.Tags = false;
            $scope.savePref.EntOrgHierarchy = false;
            $scope.savePref.MasterBridgeLocation = false;
            $scope.savePref.MasterBridgeMaster = false;
            $scope.savePref.NaicsCodeStew = false;
            $scope.savePref.RankingStew = false;
            $scope.savePref.TransactionHistory = false;

            $scope.home.toggleCharacteristics = false;
            $scope.home.toggleSection(EXTENDED_FUNC.CHARACTERISTICS);
        }

        //Control the sectional updates as per users input in the menu preference tab
        //* Class assignment to the respective menu preference label to highlight it
        //* Show the selected section alone
        function setSectionalSettingsHome(sectionName) {
            switch (sectionName) {
                case "EntOrgDetail":
                    $scope.EntOrgDetailHomeSelectedClass = "ChkSelected";
                    $scope.toggleEntOrgDetailSection = true;
                    break;
                case "RFMDetails":
                    $scope.RFMDetailsHomeSelectedClass = "ChkSelected";
                    $scope.toggleRFMDetailsSection = true;
                    break;
                case "TransformationDetail":
                    $scope.TransformationDetailHomeSelectedClass = "ChkSelected";
                    $scope.toggleTransformationDetailSection = true;
                    break;
                case "Tags":
                    $scope.TagsHomeSelectedClass = "ChkSelected";
                    $scope.toggleTagsSection = true;
                    break;
                case "EntOrgHierarchy":
                    $scope.EntOrgHierarchyHomeSelectedClass = "ChkSelected";
                    $scope.toggleEntOrgHierarchySection = true;
                    break;
                case "MasterBridgeLocation":
                    $scope.MasterBridgeLocationHomeSelectedClass = "ChkSelected";
                    $scope.toggleMasterBridgeLocationSection = true;
                    break;
                case "MasterBridgeMaster":
                    $scope.MasterBridgeMasterHomeSelectedClass = "ChkSelected";
                    $scope.toggleMasterBridgeMasterSection = true;
                    break;
                case "NaicsCodeStew":
                    $scope.NaicsCodeStewHomeSelectedClass = "ChkSelected";
                    $scope.toggleNaicsCodeStewSection = true;
                    break;
                case "RankingStew":
                    $scope.RankingStewHomeSelectedClass = "ChkSelected";
                    $scope.toggleRankingStewSection = true;
                    break;
                case "TransactionHistory":
                    $scope.TransactionHistorySelectedClass = "ChkSelected";
                    $scope.toggleTransactionHistorySection = true;
                    break;
                case "Characteristics":
                    $scope.CharacteristicsSelectedClass = "ChkSelected";
                    //characterisitics added by srini
                    $scope.home.toggleCharacteristics = true;
                    $scope.home.toggleSection(EXTENDED_FUNC.CHARACTERISTICS);
                    break;
                    
            }
        }

        //Toggle the sections as per home sub menu selection
        $scope.homeToggleSection = function (sectionName) {
            //Revert back dtails settings
            clearDetailsForHomeSelection();
            //Highlight the menu list item for the selected section label
            setSectionalSettingsHome(sectionName);
            //Load the selected details
            processSectionalDetails(sectionName);
        }

        function setSectionalDefaultSettingMulti() {
            //Setting the section visibility on load
            $scope.toggleEntOrgDetailSection = true;
            $scope.entOrgDetailCbModel = true;
            $scope.savePref.EntOrgDetail = true;
            processSectionalDetails("EntOrgDetail");
            $scope.toggleTransformationDetailSection = true;
            $scope.transformationDetailCbModel = true;
            $scope.savePref.TransformationDetail = true;
            processSectionalDetails("TransformationDetail");
            $scope.toggleTagsSection = true;
            $scope.tagsCbModel = true;
            $scope.savePref.Tags = true;
            processSectionalDetails("Tags");
            $scope.toggleNaicsCodeStewSection = true;
            $scope.naicsCodeStewCbModel = true;
            $scope.savePref.NaicsCodeStew = true;
            processSectionalDetails("NaicsCodeStew");
            $scope.toggleTransactionHistorySection = true;
            $scope.transactionHistoryCbModel = true;
            $scope.savePref.TransactionHistory = true;
            processSectionalDetails("TransactionHistory");
        }

        function setSectionalSettingMulti() {
            var flagBridgeDetailsFetched = false;
            enterpriseDetailsService.GetMenuPreference()
            .success(function (result) {
                if (result != null && result != '') {
                    if (result.Preferences != null)
                    {
                        angular.forEach(result.Preferences, function (v, k) {
                            switch(v)
                            {
                                case "EntOrgDetail":
                                    $scope.toggleEntOrgDetailSection = true;
                                    $scope.entOrgDetailCbModel = true;
                                    $scope.savePref.EntOrgDetail = true;
                                    break;
                                case "RFMDetails":
                                    $scope.toggleRFMDetailsSection = true;
                                    $scope.rFMDetailsCbModel = true;
                                    $scope.savePref.RFMDetails = true;
                                    break;
                                case "TransformationDetail":
                                    $scope.toggleTransformationDetailSection = true;
                                    $scope.transformationDetailCbModel = true;
                                    $scope.savePref.TransformationDetail = true;
                                    break;
                                case "Tags":
                                    $scope.toggleTagsSection = true;
                                    $scope.tagsCbModel = true;
                                    $scope.savePref.Tags = true;
                                    break;
                                case "EntOrgHierarchy":
                                    $scope.toggleEntOrgHierarchySection = true;
                                    $scope.entOrgHierarchyCbModel = true;
                                    $scope.savePref.EntOrgHierarchy = true;
                                    break;
                                case "MasterBridgeLocation":
                                    $scope.toggleMasterBridgeLocationSection = true;
                                    $scope.masterBridgeLocationCbModel = true;
                                    $scope.savePref.MasterBridgeLocation = true;
                                    break;
                                case "MasterBridgeMaster":
                                    $scope.toggleMasterBridgeMasterSection = true;
                                    $scope.masterBridgeMasterCbModel = true;
                                    $scope.savePref.MasterBridgeMaster = true;
                                    break;
                                case "NaicsCodeStew":
                                    $scope.toggleNaicsCodeStewSection = true;
                                    $scope.naicsCodeStewCbModel = true;
                                    $scope.savePref.NaicsCodeStew = true;
                                    break;
                                case "RankingStew":
                                    $scope.toggleRankingStewSection = true;
                                    $scope.rankingStewCbModel = true;
                                    $scope.savePref.RankingStew = true;
                                    break;
                                case "TransactionHistory":
                                    $scope.toggleTransactionHistorySection = true;
                                    $scope.transactionHistoryCbModel = true;
                                    $scope.savePref.TransactionHistory = true;
                                    break;

                                case "Characteristics":
                                    //characterisitics added by srini
                                    $scope.multi.toggleCharacteristics = true;
                                    $scope.multi.toggleSection(EXTENDED_FUNC.CHARACTERISTICS);
                                    $scope.savePref.Characteristics = true;
                                    break;

                            }
                            processSectionalDetails(v);
                        });
                    } else {
                        setSectionalDefaultSettingMulti();
                    }
                }
                else {
                    setSectionalDefaultSettingMulti();
                }
            })
            .error(function (result) {
                setSectionalDefaultSettingMulti();
            })
        }

        setSectionalSettingMulti();

        //Multi Tab method to toggle the detail sections based on the selection
        $scope.toggleDetailSections = function (sectionName, cbValue) {
            switch (sectionName) {
                case "EntOrgDetail":
                    if (cbValue == 1) {
                        $scope.toggleEntOrgDetailSection = true;
                        $scope.savePref.EntOrgDetail = true;
                    }
                    else {
                        $scope.toggleEntOrgDetailSection = false;
                        $scope.savePref.EntOrgDetail = false;
                    }
                    break;
                case "RFMDetails":
                    if (cbValue == 1) {
                        $scope.toggleRFMDetailsSection = true;
                        $scope.savePref.RFMDetails = true;
                    }
                    else {
                        $scope.toggleRFMDetailsSection = false;
                        $scope.savePref.RFMDetails = false;
                    }
                    break;
                case "TransformationDetail":
                    if (cbValue == 1) {
                        $scope.toggleTransformationDetailSection = true;
                        $scope.savePref.TransformationDetail = true;
                    }
                    else {
                        $scope.toggleTransformationDetailSection = false;
                        $scope.savePref.TransformationDetail = false;
                    }
                    break;
                case "Tags":
                    if (cbValue == 1) {
                        $scope.toggleTagsSection = true;
                        $scope.savePref.Tags = true;
                    }
                    else {
                        $scope.toggleTagsSection = false;
                        $scope.savePref.Tags = false;
                    }
                    break;
                case "EntOrgHierarchy":
                    if (cbValue == 1) {
                        $scope.toggleEntOrgHierarchySection = true;
                        $scope.savePref.EntOrgHierarchy = true;
                    }
                    else {
                        $scope.toggleEntOrgHierarchySection = false;
                        $scope.savePref.EntOrgHierarchy = false;
                    }
                    break;
                case "MasterBridgeLocation":
                    if (cbValue == 1) {
                        $scope.toggleMasterBridgeLocationSection = true;
                        $scope.savePref.MasterBridgeLocation = true;
                    }
                    else {
                        $scope.toggleMasterBridgeLocationSection = false;
                        $scope.savePref.MasterBridgeLocation = false;
                    }
                    break;
                case "MasterBridgeMaster":
                    if (cbValue == 1) {
                        $scope.toggleMasterBridgeMasterSection = true;
                        $scope.savePref.MasterBridgeMaster = true;
                    }
                    else {
                        $scope.toggleMasterBridgeMasterSection = false;
                        $scope.savePref.MasterBridgeMaster = false;
                    }
                    break;
                case "NaicsCodeStew":
                    if (cbValue == 1) {
                        $scope.toggleNaicsCodeStewSection = true;
                        $scope.savePref.NaicsCodeStew = true;
                    }
                    else {
                        $scope.toggleNaicsCodeStewSection = false;
                        $scope.savePref.NaicsCodeStew = false;
                    }
                    break;
                case "RankingStew":
                    if (cbValue == 1) {
                        $scope.toggleRankingStewSection = true;
                        $scope.savePref.RankingStew = true;
                    }
                    else {
                        $scope.toggleRankingStewSection = false;
                        $scope.savePref.RankingStew = false;
                    }
                    break;
                case "TransactionHistory":
                    if (cbValue == 1) {
                        $scope.toggleTransactionHistorySection = true;
                        $scope.savePref.TransactionHistory = true;
                    }
                    else {
                        $scope.toggleTransactionHistorySection = false;
                        $scope.savePref.TransactionHistory = false;
                    }
                    break;
            }
            if (cbValue == 1)
                processSectionalDetails(sectionName);
        };

        //Grid Option
        $scope.entOrgDetailGridOptions = EnterpriseAccountDataService.getDetailsGridOption('', '', false);
        $scope.rFMDetailsGridOptions = EnterpriseAccountDataService.getDetailsGridOption('row_stat_cd', 'L', false);
        $scope.transformationDetailGridOptions = EnterpriseAccountDataService.getDetailsGridOption('act_ind', '0', true);
        $scope.transformationDetailGridOptions.enableRowSelection = false;
        $scope.transformationDetailGridOptions.enableGroupHeaderSelection = false;
        //$scope.transformationDetailGridOptions.enableHorizontalScrollbar = 0;
        $scope.tagsGridOptions = EnterpriseAccountDataService.getDetailsGridOption('row_stat_cd', 'L', false);
        $scope.entOrgHierarchyGridOptions = EnterpriseAccountDataService.getDetailsGridOption('', '', false);
        //$scope.entOrgHierarchyGridOptions.appScopeProvider = {
        //    showRow: function (row) {
        //        if (row.groupHeader && row.treeLevel == 0)
        //            return (row.treeNode.aggregations[0].groupVal != "" && row.treeNode.aggregations[0].groupVal != null && row.treeNode.aggregations[0].groupVal != 'Null');
        //        else if (row.groupHeader && row.treeLevel == 1)
        //            return (row.treeNode.aggregations[1].groupVal != "" && row.treeNode.aggregations[1].groupVal != null && row.treeNode.aggregations[1].groupVal != 'Null');
        //        else if (row.groupHeader && row.treeLevel == 2)
        //            return (row.treeNode.aggregations[2].groupVal != "" && row.treeNode.aggregations[2].groupVal != null && row.treeNode.aggregations[2].groupVal != 'Null');
        //        else if (row.groupHeader && row.treeLevel == 3)
        //            return (row.treeNode.aggregations[3].groupVal != "" && row.treeNode.aggregations[3].groupVal != null && row.treeNode.aggregations[3].groupVal != 'Null');
        //        else if (!row.groupHeader)
        //            return (row.entity.lvl4_ent_org_id != "" && row.entity.lvl4_ent_org_id != null && row.entity.lvl4_ent_org_id != 'Null');
        //        else
        //            return true;
        //    },
        //    cellTemplateFunc: function(row) {
        //        console.log(row)
        //    }
        //};
        //$scope.entOrgHierarchyGridOptions.rowTemplate = '<div ng-if="row.groupHeader"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>';
        //$scope.entOrgHierarchyGridOptions.rowTemplate = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 3 && row.treeNode.aggregations[3].groupVal == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
        //$scope.entOrgHierarchyGridOptions.rowTemplate = '<div ng-if="row.groupHeader && row.treeLevel == 0 && (row.treeNode.aggregations[0].groupVal != null && row.treeNode.aggregations[0].groupVal != \'\')"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>'
        //                                                + '<div ng-if="row.groupHeader && row.treeLevel == 1 && (row.treeNode.aggregations[1].groupVal != null && row.treeNode.aggregations[1].groupVal != \'\')"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>'
        //                                                + '<div ng-if="row.groupHeader && row.treeLevel == 2 && (row.treeNode.aggregations[2].groupVal != null && row.treeNode.aggregations[2].groupVal != \'\')"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>'
        //                                                + '<div ng-if="row.groupHeader && row.treeLevel == 3 && (row.treeNode.aggregations[3].groupVal != null && row.treeNode.aggregations[3].groupVal != \'\')"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>'
        //                                                + '<div ng-show="!row.groupHeader && (row.entity.lvl5_ent_org_id != null && row.entity.lvl5_ent_org_id != \'\')"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>';
        //$scope.entOrgHierarchyGridOptions.rowTemplate = '<div grid="grid" class="ui-grid-draggable-row" draggable="true"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>';
        $scope.entOrgHierarchyGridOptions.enableRowSelection = false;
        $scope.entOrgHierarchyGridOptions.enableGroupHeaderSelection = false;
        $scope.entOrgHierarchyGridOptions.rowHeight = 70;
        $scope.entOrgHierarchyGridOptions.groupingShowCounts = false;
        $scope.masterBridgeLocationGridOptions = EnterpriseAccountDataService.getDetailsGridOption('', '', false);
        $scope.masterBridgeLocationGridOptions.rowHeight = 45;
        $scope.masterBridgeLocationGridOptions.enableRowSelection = false;
        $scope.masterBridgeLocationGridOptions.enableGroupHeaderSelection = false;
        $scope.masterBridgeMasterGridOptions = EnterpriseAccountDataService.getDetailsGridOption('', '', false);
        $scope.masterBridgeMasterGridOptions.rowHeight = 85;
        $scope.masterBridgeMasterGridOptions.enableRowSelection = false;
        $scope.masterBridgeMasterGridOptions.enableGroupHeaderSelection = false;
        $scope.naicsCodeStewGridOptions = EnterpriseAccountDataService.getDetailsGridOption('row_stat_cd', 'L', false);
        $scope.rankingStewGridOptions = EnterpriseAccountDataService.getDetailsGridOption('row_stat_cd', 'L', false);
        $scope.transactionHistoryGridOptions = EnterpriseAccountDataService.getDetailsGridOption('', '', false);

  //      $templateCache.put('ui-grid/uiGridViewport',
  //  "<div class=\"ui-grid-viewport\" ng-style=\"colContainer.getViewportStyle()\"><div class=\"ui-grid-canvas\"><div ng-repeat=\"(rowRenderIndex, row) in rowContainer.renderedRows track by $index\" ng-if=\"grid.appScope.showRow(row)\" class=\"ui-grid-row\" ng-style=\"Viewport.rowStyle(rowRenderIndex)\"><div ui-grid-row=\"row\" row-render-index=\"rowRenderIndex\"></div></div></div></div>"
  //);

        //Registering the method which will get executed once the grid gets loaded
        $scope.entOrgDetailGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.entOrgDetailGridApi = gridApi;
        }
        $scope.rFMDetailsGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.rFMDetailsGridApi = gridApi;
        }
        $scope.transformationDetailGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.transformationDetailGridApi = gridApi;
            $scope.transformationDetailGridApi.grid.registerDataChangeCallback(function () {
                $scope.transformationDetailGridApi.treeBase.expandAllRows();
            });
        }
        $scope.tagsGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.tagsGridApi = gridApi;
        }
        $scope.entOrgHierarchyGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.entOrgHierarchyGridApi = gridApi;
            $scope.entOrgHierarchyGridApi.grid.registerDataChangeCallback(function () {
                $scope.entOrgHierarchyGridApi.treeBase.expandAllRows();
            });
            //gridApi.draggableRows.on.rowDropped($scope, function (info, dropTarget) {
            //    console.log("Dropped", info);
            //});
        }
        $scope.masterBridgeLocationGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.masterBridgeLocationGridApi = gridApi;
        }
        $scope.masterBridgeMasterGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.masterBridgeMasterGridApi = gridApi;
        }
        $scope.naicsCodeStewGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.naicsCodeStewGridApi = gridApi;
        }
        $scope.rankingStewGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.rankingStewGridApi = gridApi;
        }
        $scope.transactionHistoryGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.transactionHistoryGridApi = gridApi;
        }

        //Method which is called to paginate the search results
        $scope.entOrgDetailNavigationPageChange = function (page) {
            $scope.entOrgDetailGridApi.pagination.seek(page);
        }
        $scope.rFMDetailsNavigationPageChange = function (page) {
            $scope.rFMDetailsGridApi.pagination.seek(page);
        }
        $scope.transformationDetailNavigationPageChange = function (page) {
            $scope.transformationDetailGridApi.pagination.seek(page);
        }
        $scope.tagsNavigationPageChange = function (page) {
            $scope.tagsGridApi.pagination.seek(page);
        }
        $scope.entOrgHierarchyNavigationPageChange = function (page) {
            $scope.entOrgHierarchyGridApi.pagination.seek(page);
        }
        $scope.masterBridgeLocationNavigationPageChange = function (page) {
            $scope.masterBridgeLocationGridApi.pagination.seek(page);
        }
        $scope.masterBridgeMasterNavigationPageChange = function (page) {
            $scope.masterBridgeMasterGridApi.pagination.seek(page);
        }
        $scope.naicsCodeStewNavigationPageChange = function (page) {
            $scope.naicsCodeStewGridApi.pagination.seek(page);
        }
        $scope.rankingStewNavigationPageChange = function (page) {
            $scope.rankingStewGridApi.pagination.seek(page);
        }
        $scope.transactionHistoryNavigationPageChange = function (page) {
            $scope.transactionHistoryGridApi.pagination.seek(page);
        }

        //Fetch data from server on the first time load
        //Use the data fetched already after the first time load
        function processSectionalDetails(sectionName) {
            //Show the processing overlay at the respective section
            $scope.showProcessingOverlay(sectionName);
            var dataFlag = EnterpriseAccountDataService.getDetailsDataFetchedFlag(sectionName);

            ///************************* Details - Enterprise Org Details *************************/
            if (sectionName == "EntOrgDetail") {
                $scope.entOrgDetailGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsEntOrgDetailGridDef($scope); //Column Definition

                //Custom config
                $scope.entOrgDetailGridOptions.exporterCsvFilename = 'Enterprise_Details_EntOrgDetail.csv',
                $scope.entOrgDetailGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetEntOrgDetailDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.entOrgDetailGridOptions.data = result;
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.entOrgDetailGridOptions.totalItems = $scope.entOrgDetailGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalEntOrgDetailItems = $scope.entOrgDetailGridOptions.totalItems;
                        $scope.entOrgDetailPaginationCurrentPage = $scope.entOrgDetailGridOptions.paginationCurrentPage;
                        $scope.toggleEntOrgDetailResults = true;
                        $scope.toggleEntOrgDetailDBError = false;
                        $scope.isEntOrgDetailInactiveHidden = false;
                        $scope.isEntOrgDetailFilterFlag = false;
                        $scope.isEntOrgDetailShowDetailsFlag = false;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleEntOrgDetailDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.entOrgDetailGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.entOrgDetailGridOptions.totalItems = $scope.entOrgDetailGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalEntOrgDetailItems = $scope.entOrgDetailGridOptions.totalItems;
                    $scope.entOrgDetailPaginationCurrentPage = $scope.entOrgDetailGridOptions.paginationCurrentPage;
                    $scope.toggleEntOrgDetailResults = true;
                    $scope.toggleEntOrgDetailDBError = false;
                    $scope.isEntOrgDetailInactiveHidden = false;
                    $scope.isEntOrgDetailFilterFlag = false;
                    $scope.isEntOrgDetailShowDetailsFlag = false;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - RFM Details *************************/
            if (sectionName == "RFMDetails") {
                $scope.rFMDetailsGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsRFMDetailsGridDef($scope); //Column Definition

                //Custom config
                $scope.rFMDetailsGridOptions.exporterCsvFilename = 'Enterprise_Details_RFM_Details.csv',
                $scope.rFMDetailsGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetRFMDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.rFMDetailsGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result, "row_stat_cd");
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.rFMDetailsGridOptions.totalItems = $scope.rFMDetailsGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalRFMDetailsItems = $scope.rFMDetailsGridOptions.totalItems;
                        $scope.rFMDetailsPaginationCurrentPage = $scope.rFMDetailsGridOptions.paginationCurrentPage;
                        $scope.toggleRFMDetailsResults = true;
                        $scope.toggleRFMDetailsDBError = false;
                        $scope.isRFMDetailsInactiveHidden = true;
                        $scope.isRFMDetailsFilterFlag = true;
                        $scope.isRFMDetailsShowDetailsFlag = false;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleRFMDetailsDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.rFMDetailsGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.rFMDetailsGridOptions.totalItems = $scope.rFMDetailsGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalTagsItems = $scope.rFMDetailsGridOptions.totalItems;
                    $scope.tagsPaginationCurrentPage = $scope.rFMDetailsGridOptions.paginationCurrentPage;
                    $scope.toggleRFMDetailsResults = true;
                    $scope.toggleRFMDetailsDBError = false;
                    $scope.isRFMDetailsInactiveHidden = true;
                    $scope.isRFMDetailsFilterFlag = true;
                    $scope.isRFMDetailsShowDetailsFlag = false;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Transformation Details *************************/
            if (sectionName == "TransformationDetail") {
                $scope.transformationDetailGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsTransformationDetailGridDef($scope); //Column Definition

                //Custom config
                $scope.transformationDetailGridOptions.exporterCsvFilename = 'Enterprise_Details_TransformationDetail.csv',
                $scope.transformationDetailGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetTransformationDetailDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.transformationDetailGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result.output, "act_ind");
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.transformationDetailGridOptions.totalItems = $scope.transformationDetailGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalTransformationDetailItems = $scope.transformationDetailGridOptions.totalItems;
                        $scope.transformationDetailPaginationCurrentPage = $scope.transformationDetailGridOptions.paginationCurrentPage;
                        $scope.toggleTransformationDetailResults = true;
                        $scope.toggleTransformationDetailDBError = false;
                        $scope.transformationDetailGridOptions.paginationPageSize = 10000;
                        $scope.isTransformationDetailInactiveHidden = true;
                        $scope.isTransformationDetailFilterFlag = true;
                        $scope.isTransformationDetailShowDetailsFlag = false;
                        $scope.manuallyAffiliatedMasterCount = 0;
                        $scope.manuallyAffiliatedMasterCount = result.manual_affil_cnt;

                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleTransformationDetailDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    var tempRes = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.transformationDetailGridOptions.data = tempRes.output;
                    $scope.transformationDetailGridOptions.totalItems = $scope.transformationDetailGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalTransformationDetailItems = $scope.transformationDetailGridOptions.totalItems;
                    $scope.transformationDetailPaginationCurrentPage = $scope.transformationDetailGridOptions.paginationCurrentPage;
                    $scope.toggleTransformationDetailResults = true;
                    $scope.toggleTransformationDetailDBError = false;
                    $scope.transformationDetailGridOptions.paginationPageSize = 10000;
                    $scope.isTransformationDetailInactiveHidden = true;
                    $scope.isTransformationDetailFilterFlag = true;
                    $scope.isTransformationDetailShowDetailsFlag = false;
                    $scope.manuallyAffiliatedMasterCount = 0;
                    $scope.manuallyAffiliatedMasterCount = tempRes.manual_affil_cnt;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Tags *************************/
            if (sectionName == "Tags") {
                $scope.showProcessingOverlay(sectionName);
                $scope.tagsGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsTagsGridDef($scope); //Column Definition
                
                //Custom config
                $scope.tagsGridOptions.exporterCsvFilename = 'Enterprise_Details_Tags.csv',
                $scope.tagsGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetTagsDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.tagsGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result, "row_stat_cd");
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.tagsGridOptions.totalItems = $scope.tagsGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalTagsItems = $scope.tagsGridOptions.totalItems;
                        $scope.tagsPaginationCurrentPage = $scope.tagsGridOptions.paginationCurrentPage;
                        $scope.toggleTagsResults = true;
                        $scope.toggleTagsDBError = false;
                        $scope.isTagsInactiveHidden = true;
                        $scope.isTagsFilterFlag = true;
                        $scope.isTagsShowDetailsFlag = false;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleTagsDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.tagsGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.tagsGridOptions.totalItems = $scope.tagsGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalTagsItems = $scope.tagsGridOptions.totalItems;
                    $scope.tagsPaginationCurrentPage = $scope.tagsGridOptions.paginationCurrentPage;
                    $scope.toggleTagsResults = true;
                    $scope.toggleTagsDBError = false;
                    $scope.isTagsInactiveHidden = true;
                    $scope.isTagsFilterFlag = true;
                    $scope.isTagsShowDetailsFlag = false;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Enterprise Hierarchy *************************/
            if (sectionName == "EntOrgHierarchy") {
                $scope.showProcessingOverlay(sectionName);
                
                //Custom config
                $scope.entOrgHierarchyGridOptions.exporterCsvFilename = 'Enterprise_Details_EntOrgHierarchy.csv',
                $scope.entOrgHierarchyGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetEntOrgHierarchyDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.entOrgHierarchyGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result, "row_stat_cd");
                        var level = 0;
                        var rowTemplateHier = '';
                        if (!angular.isUndefined($scope.entOrgHierarchyGridOptions.data) && $scope.entOrgHierarchyGridOptions.data.length > 0) {
                            angular.forEach($scope.entOrgHierarchyGridOptions.data, function (agg, key) {
                                if (level < 5 && !angular.isUndefined(agg.lvl5_ent_org_id) && agg.lvl5_ent_org_id != null && agg.lvl5_ent_org_id != '') {
                                    level = 5;
                                    rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 3 && row.treeNode.aggregations[3].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl5_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                }
                                else if (level < 4 && !angular.isUndefined(agg.lvl4_ent_org_id) && agg.lvl4_ent_org_id != null && agg.lvl4_ent_org_id != '') {
                                    level = 4;
                                    rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl4_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                }
                                else if (level < 3 && !angular.isUndefined(agg.lvl3_ent_org_id) && agg.lvl3_ent_org_id != null && agg.lvl3_ent_org_id != '') {
                                    level = 3;
                                    rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl3_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                }
                                else if (level < 2 && !angular.isUndefined(agg.lvl2_ent_org_id) && agg.lvl2_ent_org_id != null && agg.lvl2_ent_org_id != '') {
                                    level = 2;
                                    rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl2_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                }
                                else if (level < 1 && !angular.isUndefined(agg.lvl1_ent_org_id) && agg.lvl1_ent_org_id != null && agg.lvl1_ent_org_id != '') {
                                    level = 1;
                                    rowTemplateHier = '<div ng-class="{\'pinkClass\': (!row.groupHeader && row.entity.lvl1_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                }

                            });
                        }

                        $scope.entOrgHierarchyGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsEntOrgHierarchyGridDef($scope, level); //Column Definition
                        //$scope.entOrgHierarchyGridOptions.rowTemplate = rowTemplateHier;
                        
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.entOrgHierarchyGridOptions.totalItems = $scope.entOrgHierarchyGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalEntOrgHierarchyItems = $scope.entOrgHierarchyGridOptions.totalItems;
                        $scope.entOrgHierarchyPaginationCurrentPage = $scope.entOrgHierarchyGridOptions.paginationCurrentPage;
                        $scope.toggleEntOrgHierarchyResults = true;
                        $scope.toggleEntOrgHierarchyDBError = false;
                        $scope.isEntOrgHierarchyInactiveHidden = false;
                        $scope.isEntOrgHierarchyFilterFlag = false;
                        $scope.isEntOrgHierarchyShowDetailsFlag = false;
                        $scope.entOrgHierarchyGridOptions.paginationPageSize = 10000;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleEntOrgHierarchyDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.entOrgHierarchyGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    var level = 0;
                    var rowTemplateHier = '';
                    if (!angular.isUndefined($scope.entOrgHierarchyGridOptions.data) && $scope.entOrgHierarchyGridOptions.data.length > 0) {
                        angular.forEach($scope.entOrgHierarchyGridOptions.data, function (agg, key) {
                            if (level < 5 && !angular.isUndefined(agg.lvl5_ent_org_id) && agg.lvl5_ent_org_id != null && agg.lvl5_ent_org_id != '') {
                                level = 5;
                                rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 3 && row.treeNode.aggregations[3].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl5_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                            }
                            else if (level < 4 && !angular.isUndefined(agg.lvl4_ent_org_id) && agg.lvl4_ent_org_id != null && agg.lvl4_ent_org_id != '') {
                                level = 4;
                                rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl4_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                            }
                            else if (level < 3 && !angular.isUndefined(agg.lvl3_ent_org_id) && agg.lvl3_ent_org_id != null && agg.lvl3_ent_org_id != '') {
                                level = 3;
                                rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl3_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                            }
                            else if (level < 2 && !angular.isUndefined(agg.lvl2_ent_org_id) && agg.lvl2_ent_org_id != null && agg.lvl2_ent_org_id != '') {
                                level = 2;
                                rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl2_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                            }
                            else if (level < 1 && !angular.isUndefined(agg.lvl1_ent_org_id) && agg.lvl1_ent_org_id != null && agg.lvl1_ent_org_id != '') {
                                level = 1;
                                rowTemplateHier = '<div ng-class="{\'pinkClass\': (!row.groupHeader && row.entity.lvl1_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                            }

                        });
                    }
                    $scope.entOrgHierarchyGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsEntOrgHierarchyGridDef($scope, level); //Column Definition
                    //$scope.entOrgHierarchyGridOptions.rowTemplate = rowTemplateHier;
                    $scope.entOrgHierarchyGridOptions.totalItems = $scope.entOrgHierarchyGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalEntOrgHierarchyItems = $scope.entOrgHierarchyGridOptions.totalItems;
                    $scope.entOrgHierarchyPaginationCurrentPage = $scope.entOrgHierarchyGridOptions.paginationCurrentPage;
                    $scope.toggleEntOrgHierarchyResults = true;
                    $scope.toggleEntOrgHierarchyDBError = false;
                    $scope.isEntOrgHierarchyInactiveHidden = false;
                    $scope.isEntOrgHierarchyFilterFlag = false;
                    $scope.isEntOrgHierarchyShowDetailsFlag = false;
                    $scope.entOrgHierarchyGridOptions.paginationPageSize = 10000;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Affiliated Master Details *************************/
            if (sectionName == "MasterBridgeLocation") {
                //$scope.showProcessingOverlay(sectionName);
                $scope.masterBridgeLocationGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsMasterBridgeLocationGridDef($scope); //Column Definition

                //Custom config
                $scope.masterBridgeLocationGridOptions.exporterCsvFilename = 'Enterprise_Details_MasterBridgeLocation.csv',
                $scope.masterBridgeLocationGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                var serviceInput = {};
                serviceInput.ent_org_id = $scope.selectedEntOrg;
                serviceInput.AffiliationLimit = "0";
                serviceInput.strLoadType = "initial";

                if (!dataFlag) {
                    enterpriseDetailsService.GetMasterBridgeDetails(serviceInput)
                    .success(function (result) {
                        $scope.masterBridgeLocationGridOptions.data = result.lt_affil_res;
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.masterBridgeLocationGridOptions.totalItems = $scope.masterBridgeLocationGridOptions.data.length;
                        $scope.masterBridgeLocationGridOptions.paginationPageSize = 1000000;
                        //Variables used for paginating the search results
                        $scope.toggleMasterBridgeLocationResults = true;
                        $scope.toggleMasterBridgeLocationDBError = false;
                        $scope.isMasterBridgeLocationInactiveHidden = false;
                        $scope.isMasterBridgeLocationFilterFlag = false;
                        $scope.isMasterBridgeLocationShowDetailsFlag = false;

                        //Aggregates
                        $scope.total_rec_cnt = 0;
                        $scope.diffOrgTypes = '';

                        var smry_cnt = result.summary_info;
                        $scope.total_rec_cnt = smry_cnt.total_brid_cnt;
                        $scope.diffOrgTypes = smry_cnt.str_concat_org_typ_cnt;

                        //var tempArr = [];
                        //var tempFinalArr = [];
                        //$scope.diffOrgTypes = '';
                        //angular.forEach($scope.masterBridgeLocationGridOptions.data, function (v, k) {
                        //    if (v.eosi_org_typ != null) {
                        //        var boolDupFlag = false;
                        //        angular.forEach(tempArr, function (v1, k1) {
                        //            if (v.eosi_org_typ == v1.eosi_org_typ && v.cnst_mstr_id == v1.cnst_mstr_id) {
                        //                boolDupFlag = true;
                        //            }
                        //        });
                        //        if (!boolDupFlag) {
                        //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                        //            tempArr.push({ eosi_org_typ: v.eosi_org_typ, cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                        //        }
                        //    }
                        //    else {
                        //        var boolDupFlag = false;
                        //        angular.forEach(tempArr, function (v1, k1) {
                        //            if (v1.eosi_org_typ == "Unknown" && v.cnst_mstr_id == v1.cnst_mstr_id) {
                        //                boolDupFlag = true;
                        //            }
                        //        });
                        //        if (!boolDupFlag) {
                        //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                        //            tempArr.push({ eosi_org_typ: "Unknown", cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                        //        }
                        //    }
                        //});

                        //angular.forEach(tempArr, function (v, k) {
                        //    var boolDupFlag = false;
                        //    angular.forEach(tempFinalArr, function (v1, k1) {
                        //        if (v.eosi_org_typ == v1.eosi_org_typ) {
                        //            boolDupFlag = true;
                        //            v1.cnt += 1;
                        //        }
                        //    });
                        //    if (!boolDupFlag) {
                        //        tempFinalArr.push({ eosi_org_typ: v.eosi_org_typ, cnt: 1 });
                        //    }
                        //});

                        //angular.forEach(tempFinalArr, function (v1, k1) {
                        //    $scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v1.eosi_org_typ + '(' + v1.cnt + ')';
                        //});

                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleMasterBridgeLocationDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    var tempDetailResults = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.masterBridgeLocationGridOptions.data = tempDetailResults.lt_affil_res;
                    $scope.masterBridgeLocationGridOptions.totalItems = $scope.masterBridgeLocationGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalMasterBridgeLocationItems = $scope.masterBridgeLocationGridOptions.totalItems;
                    $scope.masterBridgeLocationPaginationCurrentPage = $scope.masterBridgeLocationGridOptions.paginationCurrentPage;
                    $scope.masterBridgeLocationGridOptions.paginationPageSize = 1000000;
                    $scope.toggleMasterBridgeLocationResults = true;
                    $scope.toggleMasterBridgeLocationDBError = false;
                    $scope.isMasterBridgeLocationInactiveHidden = false;
                    $scope.isMasterBridgeLocationFilterFlag = false;
                    $scope.isMasterBridgeLocationShowDetailsFlag = false;

                    //Aggregates
                    $scope.total_rec_cnt = 0;
                    $scope.diffOrgTypes = '';

                    var smry_cnt = tempDetailResults.summary_info;
                    $scope.total_rec_cnt = smry_cnt.total_brid_cnt;
                    $scope.diffOrgTypes = smry_cnt.str_concat_org_typ_cnt;

                    //var tempArr = [];
                    //var tempFinalArr = [];
                    //$scope.diffOrgTypes = '';
                    //angular.forEach($scope.masterBridgeLocationGridOptions.data, function (v, k) {
                    //    if (v.eosi_org_typ != null) {
                    //        var boolDupFlag = false;
                    //        angular.forEach(tempArr, function (v1, k1) {
                    //            if (v.eosi_org_typ == v1.eosi_org_typ && v.cnst_mstr_id == v1.cnst_mstr_id) {
                    //                boolDupFlag = true;
                    //            }
                    //        });
                    //        if (!boolDupFlag) {
                    //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                    //            tempArr.push({ eosi_org_typ: v.eosi_org_typ, cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                    //        }
                    //    }
                    //    else {
                    //        var boolDupFlag = false;
                    //        angular.forEach(tempArr, function (v1, k1) {
                    //            if (v1.eosi_org_typ == "Unknown" && v.cnst_mstr_id == v1.cnst_mstr_id) {
                    //                boolDupFlag = true;
                    //            }
                    //        });
                    //        if (!boolDupFlag) {
                    //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                    //            tempArr.push({ eosi_org_typ: "Unknown", cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                    //        }
                    //    }
                    //});

                    //angular.forEach(tempArr, function (v, k) {
                    //    var boolDupFlag = false;
                    //    angular.forEach(tempFinalArr, function (v1, k1) {
                    //        if (v.eosi_org_typ == v1.eosi_org_typ) {
                    //            boolDupFlag = true;
                    //            v1.cnt += 1;
                    //        }
                    //    });
                    //    if (!boolDupFlag) {
                    //        tempFinalArr.push({ eosi_org_typ: v.eosi_org_typ, cnt: 1 });
                    //    }
                    //});

                    //angular.forEach(tempFinalArr, function (v1, k1) {
                    //    $scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v1.eosi_org_typ + '(' + v1.cnt + ')';
                    //});

                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Bridge Records *************************/
            if (sectionName == "MasterBridgeMaster") {
                //$scope.showProcessingOverlay(sectionName);
                $scope.masterBridgeMasterGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsMasterBridgeMasterGridDef($scope); //Column Definition

                //Custom config
                $scope.masterBridgeMasterGridOptions.exporterCsvFilename = 'Enterprise_Details_MasterBridgeMaster.csv',
                $scope.masterBridgeMasterGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                var serviceInput = {};
                serviceInput.ent_org_id = $scope.selectedEntOrg;
                serviceInput.AffiliationLimit = "0";
                serviceInput.strLoadType = "initial";

                if (!dataFlag) {
                    enterpriseDetailsService.GetMasterBridgeDetails(serviceInput)
                    .success(function (result) {
                        $scope.masterBridgeMasterGridOptions.data = result.lt_affil_res;
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.masterBridgeMasterGridOptions.totalItems = $scope.masterBridgeMasterGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalMasterBridgeMasterItems = $scope.masterBridgeMasterGridOptions.totalItems;
                        $scope.masterBridgeMasterPaginationCurrentPage = $scope.masterBridgeMasterGridOptions.paginationCurrentPage;
                        $scope.masterBridgeMasterGridOptions.paginationPageSize = 1000000;
                        $scope.toggleMasterBridgeMasterResults = true;
                        $scope.toggleMasterBridgeMasterDBError = false;
                        $scope.isMasterBridgeMasterInactiveHidden = false;
                        $scope.isMasterBridgeMasterFilterFlag = false;
                        $scope.isMasterBridgeMasterShowDetailsFlag = false;

                        //Aggregates
                        $scope.total_rec_cnt = 0;
                        $scope.prospect_cnt = 0;
                        $scope.act_valid_cnt = 0;
                        $scope.inact_valid_cnt = 0;
                        $scope.act_invalid_cnt = 0;
                        $scope.inact_invalid_cnt = 0;
                        $scope.total_mstr_cnt = 0;
                        $scope.diffOrgTypes = '';

                        var smry_cnt = result.summary_info;
                        $scope.total_rec_cnt = smry_cnt.total_brid_cnt;
                        $scope.prospect_cnt = smry_cnt.pros_ind;
                        $scope.act_valid_cnt = smry_cnt.act_val_ind;
                        $scope.inact_valid_cnt = smry_cnt.inact_val_ind;
                        $scope.act_invalid_cnt = smry_cnt.act_unval_ind;
                        $scope.inact_invalid_cnt = smry_cnt.inact_unval_ind;
                        $scope.total_mstr_cnt = smry_cnt.total_mstr_cnt;
                        $scope.diffOrgTypes = smry_cnt.str_concat_org_typ_cnt;

                        //var distinctMaster = [];
                        //angular.forEach($scope.masterBridgeMasterGridOptions.data, function (v, k) {
                        //    var mstrExist = false;
                        //    angular.forEach(distinctMaster, function (v1, k1) {
                        //        if (v.cnst_mstr_id == v1.cnst_mstr_id)
                        //            mstrExist = true;
                        //    });
                        //    if (!mstrExist) {
                        //        distinctMaster.push({ cnst_mstr_id: v.cnst_mstr_id });
                        //        $scope.prospect_cnt += v.prospect_cnt;
                        //        $scope.act_valid_cnt += v.act_valid_cnt;
                        //        $scope.inact_valid_cnt += v.inact_valid_cnt;
                        //        $scope.act_invalid_cnt += v.act_invalid_cnt;
                        //        $scope.inact_invalid_cnt += v.inact_invalid_cnt;
                        //    }
                        //});

                        //var tempArr = [];
                        //var tempFinalArr = [];
                        //$scope.diffOrgTypes = '';
                        //angular.forEach($scope.masterBridgeMasterGridOptions.data, function (v, k) {
                        //    if (v.eosi_org_typ != null) {
                        //        var boolDupFlag = false;
                        //        angular.forEach(tempArr, function (v1, k1) {
                        //            if (v.eosi_org_typ == v1.eosi_org_typ && v.cnst_mstr_id == v1.cnst_mstr_id) {
                        //                boolDupFlag = true;
                        //            }
                        //        });
                        //        if (!boolDupFlag) {
                        //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                        //            tempArr.push({ eosi_org_typ: v.eosi_org_typ, cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                        //        }
                        //    }
                        //    else {
                        //        var boolDupFlag = false;
                        //        angular.forEach(tempArr, function (v1, k1) {
                        //            if (v1.eosi_org_typ == "Unknown" && v.cnst_mstr_id == v1.cnst_mstr_id) {
                        //                boolDupFlag = true;
                        //            }
                        //        });
                        //        if (!boolDupFlag) {
                        //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                        //            tempArr.push({ eosi_org_typ: "Unknown", cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                        //        }
                        //    }
                        //});

                        //angular.forEach(tempArr, function (v, k) {
                        //    var boolDupFlag = false;
                        //    angular.forEach(tempFinalArr, function (v1, k1) {
                        //        if (v.eosi_org_typ == v1.eosi_org_typ) {
                        //            boolDupFlag = true;
                        //            v1.cnt += 1;
                        //        }
                        //    });
                        //    if (!boolDupFlag) {
                        //        tempFinalArr.push({ eosi_org_typ: v.eosi_org_typ, cnt: 1 });
                        //    }
                        //});

                        //angular.forEach(tempFinalArr, function (v1, k1) {
                        //    $scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v1.eosi_org_typ + '(' + v1.cnt + ')';
                        //});

                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleMasterBridgeMasterDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    var tempDetailResults = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.masterBridgeMasterGridOptions.data = tempDetailResults.lt_affil_res;
                    $scope.masterBridgeMasterGridOptions.totalItems = $scope.masterBridgeMasterGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalMasterBridgeMasterItems = $scope.masterBridgeMasterGridOptions.totalItems;
                    $scope.masterBridgeMasterPaginationCurrentPage = $scope.masterBridgeMasterGridOptions.paginationCurrentPage;
                    $scope.masterBridgeMasterGridOptions.paginationPageSize = 1000000;
                    $scope.toggleMasterBridgeMasterResults = true;
                    $scope.toggleMasterBridgeMasterDBError = false;
                    $scope.isMasterBridgeMasterInactiveHidden = false;
                    $scope.isMasterBridgeMasterFilterFlag = false;
                    $scope.isMasterBridgeMasterShowDetailsFlag = false;

                    //Aggregates
                    $scope.total_rec_cnt = 0;
                    $scope.prospect_cnt = 0;
                    $scope.act_valid_cnt = 0;
                    $scope.inact_valid_cnt = 0;
                    $scope.act_invalid_cnt = 0;
                    $scope.inact_invalid_cnt = 0;
                    $scope.total_mstr_cnt = 0;
                    $scope.diffOrgTypes = '';

                    var smry_cnt = tempDetailResults.summary_info;
                    $scope.total_rec_cnt = smry_cnt.total_brid_cnt;
                    $scope.prospect_cnt = smry_cnt.pros_ind;
                    $scope.act_valid_cnt = smry_cnt.act_val_ind;
                    $scope.inact_valid_cnt = smry_cnt.inact_val_ind;
                    $scope.act_invalid_cnt = smry_cnt.act_unval_ind;
                    $scope.inact_invalid_cnt = smry_cnt.inact_unval_ind;
                    $scope.total_mstr_cnt = smry_cnt.total_mstr_cnt;
                    $scope.diffOrgTypes = smry_cnt.str_concat_org_typ_cnt;

                    //var distinctMaster = [];
                    //angular.forEach($scope.masterBridgeMasterGridOptions.data, function (v, k) {
                    //    var mstrExist = false;
                    //    angular.forEach(distinctMaster, function (v1, k1) {
                    //        if (v.cnst_mstr_id == v1.cnst_mstr_id)
                    //            mstrExist = true;
                    //    });
                    //    if (!mstrExist) {
                    //        distinctMaster.push({ cnst_mstr_id: v.cnst_mstr_id });
                    //        $scope.prospect_cnt += v.prospect_cnt;
                    //        $scope.act_valid_cnt += v.act_valid_cnt;
                    //        $scope.inact_valid_cnt += v.inact_valid_cnt;
                    //        $scope.act_invalid_cnt += v.act_invalid_cnt;
                    //        $scope.inact_invalid_cnt += v.inact_invalid_cnt;
                    //    }
                    //});

                    //var tempArr = [];
                    //var tempFinalArr = [];
                    //$scope.diffOrgTypes = '';
                    //angular.forEach($scope.masterBridgeMasterGridOptions.data, function (v, k) {
                    //    if (v.eosi_org_typ != null) {
                    //        var boolDupFlag = false;
                    //        angular.forEach(tempArr, function (v1, k1) {
                    //            if (v.eosi_org_typ == v1.eosi_org_typ && v.cnst_mstr_id == v1.cnst_mstr_id) {
                    //                boolDupFlag = true;
                    //            }
                    //        });
                    //        if (!boolDupFlag) {
                    //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                    //            tempArr.push({ eosi_org_typ: v.eosi_org_typ, cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var boolDupFlag = false;
                    //        angular.forEach(tempArr, function (v1, k1) {
                    //            if (v1.eosi_org_typ == "Unknown" && v.cnst_mstr_id == v1.cnst_mstr_id) {
                    //                boolDupFlag = true;
                    //            }
                    //        });
                    //        if (!boolDupFlag) {
                    //            //$scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v.eosi_org_typ;
                    //            tempArr.push({ eosi_org_typ: "Unknown", cnst_mstr_id: v.cnst_mstr_id, cnt: 1 });
                    //        }
                    //    }
                    //});

                    //angular.forEach(tempArr, function (v, k) {
                    //    var boolDupFlag = false;
                    //    angular.forEach(tempFinalArr, function (v1, k1) {
                    //        if (v.eosi_org_typ == v1.eosi_org_typ) {
                    //            boolDupFlag = true;
                    //            v1.cnt += 1;
                    //        }
                    //    });
                    //    if (!boolDupFlag) {
                    //        tempFinalArr.push({ eosi_org_typ: v.eosi_org_typ, cnt: 1 });
                    //    }
                    //});

                    //angular.forEach(tempFinalArr, function (v1, k1) {
                    //    $scope.diffOrgTypes = $scope.diffOrgTypes + ' - ' + v1.eosi_org_typ + '(' + v1.cnt + ')';
                    //});
                    
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - NAICS Code Stewarding *************************/
            if (sectionName == "NaicsCodeStew") {
                $scope.showProcessingOverlay(sectionName);
                $scope.naicsCodeStewGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsNaicsCodeStewGridDef($scope); //Column Definition

                //Custom config
                $scope.naicsCodeStewGridOptions.exporterCsvFilename = 'Enterprise_Details_NaicsCodeStew.csv',
                $scope.naicsCodeStewGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetNaicsCodeStewDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.naicsCodeStewGridOptions.data = result;
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.naicsCodeStewGridOptions.totalItems = $scope.naicsCodeStewGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalNaicsCodeStewItems = $scope.naicsCodeStewGridOptions.totalItems;
                        $scope.naicsCodeStewPaginationCurrentPage = $scope.naicsCodeStewGridOptions.paginationCurrentPage;
                        $scope.toggleNaicsCodeStewResults = true;
                        $scope.toggleNaicsCodeStewDBError = false;
                        $scope.isNaicsCodeStewInactiveHidden = false;
                        $scope.isNaicsCodeStewFilterFlag = false;
                        $scope.isNaicsCodeStewShowDetailsFlag = false;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleNaicsCodeStewDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.naicsCodeStewGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.naicsCodeStewGridOptions.totalItems = $scope.naicsCodeStewGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalNaicsCodeStewItems = $scope.naicsCodeStewGridOptions.totalItems;
                    $scope.naicsCodeStewPaginationCurrentPage = $scope.naicsCodeStewGridOptions.paginationCurrentPage;
                    $scope.toggleNaicsCodeStewResults = true;
                    $scope.toggleNaicsCodeStewDBError = false;
                    $scope.isNaicsCodeStewInactiveHidden = false;
                    $scope.isNaicsCodeStewFilterFlag = false;
                    $scope.isNaicsCodeStewShowDetailsFlag = false;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Ranking Stewarding *************************/
            if (sectionName == "RankingStew") {
                $scope.showProcessingOverlay(sectionName);
                $scope.rankingStewGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsRankingStewGridDef($scope); //Column Definition

                //Custom config
                $scope.rankingStewGridOptions.exporterCsvFilename = 'Enterprise_Details_RankingStew.csv',
                $scope.rankingStewGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetRankingStewDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.rankingStewGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result, "row_stat_cd");
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.rankingStewGridOptions.totalItems = $scope.rankingStewGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalRankingStewItems = $scope.rankingStewGridOptions.totalItems;
                        $scope.rankingStewPaginationCurrentPage = $scope.rankingStewGridOptions.paginationCurrentPage;
                        $scope.toggleRankingStewResults = true;
                        $scope.toggleRankingStewDBError = false;
                        $scope.isRankingStewInactiveHidden = true;
                        $scope.isRankingStewFilterFlag = true;
                        $scope.isRankingStewShowDetailsFlag = false;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleRankingStewDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.rankingStewGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.rankingStewGridOptions.totalItems = $scope.rankingStewGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalRankingStewItems = $scope.rankingStewGridOptions.totalItems;
                    $scope.rankingStewPaginationCurrentPage = $scope.rankingStewGridOptions.paginationCurrentPage;
                    $scope.toggleRankingStewResults = true;
                    $scope.toggleRankingStewDBError = false;
                    $scope.isRankingStewInactiveHidden = true;
                    $scope.isRankingStewFilterFlag = true;
                    $scope.isRankingStewShowDetailsFlag = false;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }

            /************************* Details - Transaction History *************************/
            if (sectionName == "TransactionHistory") {
            $scope.showProcessingOverlay(sectionName);
                    $scope.transactionHistoryGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsTransactionHistoryGridDef($scope); //Column Definition

            //Custom config
            $scope.transactionHistoryGridOptions.exporterCsvFilename = 'Enterprise_Details_TransactionHistory.csv',
            $scope.transactionHistoryGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                if (!dataFlag) {
                    enterpriseDetailsService.GetTransactionHistoryDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.transactionHistoryGridOptions.data = result;
                        EnterpriseAccountDataService.setDetails(result, sectionName);
                        $scope.transactionHistoryGridOptions.totalItems = $scope.transactionHistoryGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalTransactionHistoryItems = $scope.transactionHistoryGridOptions.totalItems;
                        $scope.transactionHistoryPaginationCurrentPage = $scope.transactionHistoryGridOptions.paginationCurrentPage;
                        $scope.toggleTransactionHistoryResults = true;
                        $scope.toggleTransactionHistoryDBError = false;
                        $scope.isTransactionHistoryInactiveHidden = false;
                        $scope.isTransactionHistoryFilterFlag = false;
                        $scope.isTransactionHistoryShowDetailsFlag = false;
                        $scope.hideProcessingOverlay(sectionName);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleTransactionHistoryDBError = true;
                        $scope.hideProcessingOverlay(sectionName);
                    });
                }
                else {
                    $scope.transactionHistoryGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    $scope.transactionHistoryGridOptions.totalItems = $scope.transactionHistoryGridOptions.data.length;
                    //Variables used for paginating the search results
                    $scope.totalTransactionHistoryItems = $scope.transactionHistoryGridOptions.totalItems;
                    $scope.transactionHistoryPaginationCurrentPage = $scope.transactionHistoryGridOptions.paginationCurrentPage;
                    $scope.toggleTransactionHistoryResults = true;
                    $scope.toggleTransactionHistoryDBError = false;
                    $scope.isTransactionHistoryInactiveHidden = false;
                    $scope.isTransactionHistoryFilterFlag = false;
                    $scope.isTransactionHistoryShowDetailsFlag = false;
                    $scope.hideProcessingOverlay(sectionName);
                }
            }
        }

        /************************* Drop down Template Generic Methods *************************/
        //Control the visibility of the dropdown based on the section
        $scope.checkAction = function(action, sectionName) {
            switch(sectionName)
            {
                case "EntOrgDetail":
                    if(action == "Edit")
                        return true;
                    if(action == "Inactivate")
                        return false;
                    if (action == "Add")
                        return false;
                    if (action == "Review")
                        return true;
                    break;
                case "RFMDetails":
                    if (action == "Edit")
                        return false;
                    if (action == "Inactivate")
                        return false;
                    if (action == "Add")
                        return false;
                    break;
                case "TransformationDetail":
                    if(action == "Edit")
                        return true;
                    if(action == "Inactivate")
                        return true;
                    if (action == "Add")
                        return true;
                    break;
                case "Tags":
                    if(action == "Edit")
                        return false;
                    if(action == "Inactivate")
                        return true;
                    if (action == "Add")
                        return true;
                    break;
                case "EntOrgHierarchy":
                    if(action == "Edit")
                        return false;
                    if(action == "Inactivate")
                        return true;
                    if (action == "Add")
                        return false;
                    break;
                case "MasterBridgeLocation":
                    if(action == "Edit")
                        return false;
                    if(action == "Inactivate")
                        return true;
                    if (action == "Add")
                        return true;
                    break;
                case "MasterBridgeMaster":
                    if(action == "Edit")
                        return false;
                    if(action == "Inactivate")
                        return true;
                    if (action == "Add")
                        return true;
                    break;
                case "NaicsCodeStew":
                    return false;
                    break;
                case "RankingStew":
                    if(action == "Edit")
                        return true;
                    if(action == "Inactivate")
                        return true;
                    if (action == "Add")
                        return true;
                    break;
                case "TransactionHistory":
                    if(action == "Edit")
                        return false;
                    if(action == "Inactivate")
                        return false;
                    if (action == "Add")
                        return false;
                    break;
            }   
            return false;
        }

        /************************* Features availability in the individual sections *************************/
        //Sectional features
        $scope.isEntOrgDetailInactiveHidden = false;
        $scope.isEntOrgDetailFilterFlag = false;
        $scope.isEntOrgDetailShowDetailsFlag = false;
        $scope.isRFMDetailsInactiveHidden = true;
        $scope.isRFMDetailsFilterFlag = true;
        $scope.isRFMDetailsShowDetailsFlag = false;
        $scope.isTransformationDetailInactiveHidden = true;
        $scope.isTransformationDetailFilterFlag = true;
        $scope.isTransformationDetailShowDetailsFlag = false;
        $scope.isTagsInactiveHidden = true;
        $scope.isTagsFilterFlag = true;
        $scope.isTagsShowDetailsFlag = false;
        $scope.isEntOrgHierarchyInactiveHidden = false;
        $scope.isEntOrgHierarchyFilterFlag = false;
        $scope.isEntOrgHierarchyShowDetailsFlag = false;
        $scope.isNaicsCodeStewInactiveHidden = false;
        $scope.isNaicsCodeStewFilterFlag = false;
        $scope.isNaicsCodeStewShowDetailsFlag = false;
        $scope.isRankingStewInactiveHidden = true;
        $scope.isRankingStewFilterFlag = true;
        $scope.isRankingStewShowDetailsFlag = false;
        $scope.isTransactionHistoryInactiveHidden = false;
        $scope.isTransactionHistoryFilterFlag = false;
        $scope.isTransactionHistoryShowDetailsFlag = false;
        $scope.isMasterBridgeMasterInactiveHidden = false;
        $scope.isMasterBridgeMasterFilterFlag = false;
        $scope.isMasterBridgeMasterShowDetailsFlag = false;
        $scope.isMasterBridgeLocationInactiveHidden = false;
        $scope.isMasterBridgeLocationFilterFlag = false;
        $scope.isMasterBridgeLocationShowDetailsFlag = false;

        //Show/Hide Inactive records method
        $scope.filterShowInactive = function (sectionName) {
            $scope.showProcessingOverlay(sectionName);
            switch (sectionName) {
                case "TransformationDetail":
                    $scope.isTransformationDetailInactiveHidden = !$scope.isTransformationDetailInactiveHidden;
                    if ($scope.isTransformationDetailInactiveHidden) {
                        var tempRes = EnterpriseAccountDataService.getActiveDetails(sectionName);
                        $scope.transformationDetailGridOptions.data = tempRes.output;
                    }
                    else {
                        var tempRes = EnterpriseAccountDataService.getDetails(sectionName);
                        $scope.transformationDetailGridOptions.data = tempRes.output;
                    }
                    $scope.transformationDetailGridOptions.totalItems = $scope.transformationDetailGridOptions.data.length;
                    $scope.totalTransformationDetailItems = $scope.transformationDetailGridOptions.totalItems;
                    $scope.transformationDetailPaginationCurrentPage = $scope.transformationDetailGridOptions.paginationCurrentPage;
                    break;
                case "RFMDetails":
                    $scope.isRFMDetailsInactiveHidden = !$scope.isRFMDetailsInactiveHidden;
                    if ($scope.isRFMDetailsInactiveHidden)
                        $scope.rFMDetailsGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    else
                        $scope.rFMDetailsGridOptions.data = EnterpriseAccountDataService.getDetails(sectionName);
                    $scope.rFMDetailsGridOptions.totalItems = $scope.rFMDetailsGridOptions.data.length;
                    $scope.totalRFMDetailsItems = $scope.rFMDetailsGridOptions.totalItems;
                    $scope.rFMDetailsPaginationCurrentPage = $scope.rFMDetailsGridOptions.paginationCurrentPage;
                    break;
                case "Tags":
                    $scope.isTagsInactiveHidden = !$scope.isTagsInactiveHidden;
                    if ($scope.isTagsInactiveHidden)
                        $scope.tagsGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    else
                        $scope.tagsGridOptions.data = EnterpriseAccountDataService.getDetails(sectionName);
                    $scope.tagsGridOptions.totalItems = $scope.tagsGridOptions.data.length;
                    $scope.totalTagsItems = $scope.tagsGridOptions.totalItems;
                    $scope.tagsPaginationCurrentPage = $scope.tagsGridOptions.paginationCurrentPage;
                    break;
                case "EntOrgHierarchy":
                    $scope.isEntOrgHierarchyInactiveHidden = !$scope.isEntOrgHierarchyInactiveHidden;
                    if ($scope.isEntOrgHierarchyInactiveHidden)
                        $scope.entOrgHierarchyGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    else
                        $scope.entOrgHierarchyGridOptions.data = EnterpriseAccountDataService.getDetails(sectionName);
                    $scope.entOrgHierarchyGridOptions.totalItems = $scope.entOrgHierarchyGridOptions.data.length;
                    $scope.totalEntOrgHierarchyItems = $scope.entOrgHierarchyGridOptions.totalItems;
                    $scope.entOrgHierarchyPaginationCurrentPage = $scope.entOrgHierarchyGridOptions.paginationCurrentPage;
                    break;
                case "MasterBridgeLocation":
                    $scope.toggleMasterBridgeLocationProcessOverlay = false;
                    break;
                case "MasterBridgeMaster":
                    $scope.toggleMasterBridgeMasterProcessOverlay = false;
                    break;
                case "NaicsCodeStew":
                    $scope.toggleNaicsCodeStewProcessOverlay = false;
                    break;
                case "RankingStew":
                    $scope.isRankingStewInactiveHidden = !$scope.isRankingStewInactiveHidden;
                    if ($scope.isRankingStewInactiveHidden)
                        $scope.rankingStewGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                    else
                        $scope.rankingStewGridOptions.data = EnterpriseAccountDataService.getDetails(sectionName);
                    $scope.rankingStewGridOptions.totalItems = $scope.rankingStewGridOptions.data.length;
                    $scope.totalRankingStewItems = $scope.rankingStewGridOptions.totalItems;
                    $scope.rankingStewPaginationCurrentPage = $scope.rankingStewGridOptions.paginationCurrentPage;
                    break;
                    break;
                case "TransactionHistory":
                    $scope.toggleTransactionHistoryProcessOverlay = false;
                    break;
            }
            $scope.hideProcessingOverlay(sectionName);
        }

        //Method to show the details pop-up
        $rootScope.popupGridOptions = {};

        $rootScope.popupGridOptions.onRegisterApi = function (data) {
            $rootScope.popupGridApi = data;
        }

        //Method which is called to paginate the search results
        $rootScope.popupGridNavigationPageChange = function (page) {
            $scope.popupGridApi.pagination.seek(page);
        }

        $scope.showDetailsPopup = function (sectionName) {
            $scope.showProcessingOverlay(sectionName);
            $rootScope.popupGridOptions = {};
            $rootScope.popupGridOptions = EnterpriseAccountDataService.getDetailsGridOption('', '', false);
            $rootScope.popupGridOptions.enableVerticalScrollbar = 1;
            $rootScope.popupGridOptions.enableHorizontalScrollbar = 1;
            $rootScope.popupGridOptions.rowTemplate = '';

            //Grid configuration in the details pop-up
            $rootScope.popupGridOptions.data = EnterpriseAccountDataService.getDetails(sectionName);
            $rootScope.popupGridOptions.totalItems = $rootScope.popupGridOptions.data.length;
            $rootScope.popupGridPaginationCurrentPage = $rootScope.popupGridOptions.paginationCurrentPage;

            //Pop-up customization
            $rootScope.showDetailsPopupHeader = getLavelText(sectionName);
            angular.element(showDetailsPopup).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay(sectionName);
        }

        /* ************************** Generic Action methods ************************** */
        //Method to perform edit record
        $scope.editRow = function(row)
        {
            var hdrlabel = 'Edit - ' + getLavelText(row.entity.section_nm);
            $scope.showProcessingOverlay(row.entity.section_nm);

            //Generic method to call the respective action method
            if (row.entity.section_nm == "EntOrgDetail") {
                editEnterpriseOrgs(row);
            }
            else {
                MessagePopup($rootScope, hdrlabel, 'This feature is currently under construction');
            }
        }

        //Method to perform review record
        $scope.reviewRow = function (row) {
            var hdrlabel = 'Review - ' + getLavelText(row.entity.section_nm);
            MessagePopup($rootScope, hdrlabel, 'This feature is currently under construction');
        }

        //Method to perform inactivate record
        $scope.deleteRow = function (row) {
            var hdrlabel = 'Delete - ' + getLavelText(row.entity.section_nm);
            $scope.showProcessingOverlay(row.entity.section_nm);

            if (row.entity.section_nm == "Tags") {
                deleteTags(row);
            }
            else {
                MessagePopup($rootScope, hdrlabel, 'This feature is currently under construction');
                $scope.hideProcessingOverlay(row.entity.section_nm);
            }
        }

        //Method to perform add record
        $scope.addDetails = function (sectionName) {
            var hdrlabel = 'Add - ' + getLavelText(sectionName);
            $scope.showProcessingOverlay(sectionName);

            //Tags
            if (sectionName == "Tags") {
                addTags(sectionName);
            }
            //Transformation
            else if (sectionName == "TransformationDetail") {
                addTransformations(sectionName);
            }
            //Affiliations
            else if (sectionName == "MasterBridgeLocation" || sectionName == "MasterBridgeMaster") {
                addAffiliations(sectionName);
            }
            else {
                MessagePopup($rootScope, hdrlabel, ENT_ACC.ENT_ORG_CURRENTLY_CONSTRUCTION);
                $scope.hideProcessingOverlay(sectionName);
            }
        }

        /* ************************** Enterprise Organizations ************************** */
        //Edit EO methods       
        var editEnterpriseOrgs = function (row) {
            $scope.pleaseWait = { "display": "block" };
            $scope.InputData = {};
            $scope.InputData.enterpriseOrgId = row.entity.ent_org_id;

            $scope.InputData.EnterpriseOrgId = row.entity.ent_org_id;
            $scope.InputData.EnterpriseOrgName = row.entity.ent_org_name;
            $scope.InputData.SourceSystem = row.entity.ent_org_src_cd;
            $scope.InputData.EnterpriseOrgsDesc = row.entity.ent_org_dsc;
            $scope.InputData.SourceSystemId = row.entity.nk_ent_org_id;
            angular.element(editEnterpriseOrgsPopup).modal();
        }

        /* ************************** Specific methods to submit the updates to the server ************************** */
        //Edit EO methods       
        $scope.editEnterpriseOrgsSubmit = function () {        
            //Frame the inputs
            $scope.postData = {};
            $scope.postData.ent_org_id = (angular.isUndefined($scope.InputData.EnterpriseOrgId) ? "" : $scope.InputData.EnterpriseOrgId);
            $scope.postData.ent_org_name = (angular.isUndefined($scope.InputData.EnterpriseOrgName) ? "" : $scope.InputData.EnterpriseOrgName);
            $scope.postData.ent_org_src_cd = (angular.isUndefined($scope.InputData.SourceSystem) ? "" : $scope.InputData.SourceSystem);
            $scope.postData.nk_ent_org_id = (angular.isUndefined($scope.InputData.SourceSystemId) ? "" : $scope.InputData.SourceSystemId);
            $scope.postData.ent_org_dsc = (angular.isUndefined($scope.InputData.EnterpriseOrgsDesc) ? "" : $scope.InputData.EnterpriseOrgsDesc);

            service.submitEnterpriseAccountEdit($scope.postData).success(function (result) {
                if (result.length > 0) {

                    if (result[0].o_outputMessage == "Success") {

                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_EDIT_HEADER, ENT_ACC.ENT_ORG_EDITION_MESSAGE + result[0].o_transaction_key)
                        $scope.pleaseWait = { "display": "none" };
                        angular.element(editEnterpriseOrgsPopup).modal('hide');
                        var sectionName = "EntOrgDetail";
                        if (sectionName == "EntOrgDetail") {
                            $scope.entOrgDetailGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsEntOrgDetailGridDef($scope); //Column Definition

                            //Custom config
                            $scope.entOrgDetailGridOptions.exporterCsvFilename = 'Enterprise_Details_EntOrgDetail.csv',
                            $scope.entOrgDetailGridOptions.exporterCsvLinkElement = angular.element(document.querySelectorAll(".custom-csv-link-location"))

                            //if (!dataFlag) {
                            $scope.selectedEntOrg = $scope.postData.ent_org_id;
                                enterpriseDetailsService.GetEntOrgDetailDetails($scope.selectedEntOrg)
                                .success(function (result) {
                                    $scope.entOrgDetailGridOptions.data = result;
                                    EnterpriseAccountDataService.setDetails(result, sectionName);
                                    $scope.entOrgDetailGridOptions.totalItems = $scope.entOrgDetailGridOptions.data.length;
                                    //Variables used for paginating the search results
                                    $scope.totalEntOrgDetailItems = $scope.entOrgDetailGridOptions.totalItems;
                                    $scope.entOrgDetailPaginationCurrentPage = $scope.entOrgDetailGridOptions.paginationCurrentPage;
                                    $scope.toggleEntOrgDetailResults = true;
                                    $scope.toggleEntOrgDetailDBError = false;
                                    $scope.isEntOrgDetailInactiveHidden = false;
                                    $scope.isEntOrgDetailFilterFlag = false;
                                    $scope.isEntOrgDetailShowDetailsFlag = false;
                                    $scope.hideProcessingOverlay(sectionName);
                                     $scope.pleaseWait = { "display": "none" };
                                })
                                .error(function (res) {
                                    $scope.toggleEntOrgDetailDBError = true;
                                    $scope.hideProcessingOverlay(sectionName);
                                    $scope.pleaseWait = { "display": "none" };
                                });
                            //}
                            //else {
                            //    $scope.entOrgDetailGridOptions.data = EnterpriseAccountDataService.getActiveDetails(sectionName);
                            //    $scope.entOrgDetailGridOptions.totalItems = $scope.entOrgDetailGridOptions.data.length;
                            //    //Variables used for paginating the search results
                            //    $scope.totalEntOrgDetailItems = $scope.entOrgDetailGridOptions.totalItems;
                            //    $scope.entOrgDetailPaginationCurrentPage = $scope.entOrgDetailGridOptions.paginationCurrentPage;
                            //    $scope.toggleEntOrgDetailResults = true;
                            //    $scope.toggleEntOrgDetailDBError = false;
                            //    $scope.isEntOrgDetailInactiveHidden = false;
                            //    $scope.isEntOrgDetailFilterFlag = false;
                            //    $scope.isEntOrgDetailShowDetailsFlag = false;
                            //    $scope.hideProcessingOverlay(sectionName);
                            //}
                        }
                    }
                    else {
                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_EDIT_HEADER, ENT_ACC.ENT_ORG_EDIT_FAILEDMESSAGE)
                        $scope.pleaseWait = { "display": "none" };
                        $scope.hideProcessingOverlay(sectionName);
                    }
                }
                else {
                    MessagePopup($rootScope, ENT_ACC.ENT_ORG_EDIT_HEADER, ENT_ACC.ENT_ORG_EDIT_FAILEDMESSAGE)
                    $scope.pleaseWait = { "display": "none" };
                    $scope.hideProcessingOverlay(sectionName);
                }
                $scope.pleaseWait = { "display": "none" };

            })
            .error(function (result) {
                errorPopups(result);
                $scope.pleaseWait = { "display": "none" };
            });
        }

        /* ************* Tag Action Methods ************* */
        //Tag enterprise
        var addTags = function (sectionName) {
            $scope.addRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                tagsDDList: [],
                selectedTags: '',
                startDate: EnterpriseAccountDataService.getCurrentDate(),
                endDate: EnterpriseAccountDataService.getEndDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate()
            };

            //Get the tags list and populate the dropdown, if not show an error pop-up instead of add
            enterpriseDetailsService.getTagDDList()
            .success(function (res) {
                var formatResultsForDD = [];
                angular.forEach(res, function (val, key) {
                    formatResultsForDD.push({ code: val.tag, value: val.tag });
                });
                $scope.addRecord.tagsDDList = formatResultsForDD;
                $scope.addRecord.selectedTags = $scope.addRecord.tagsDDList[0].value;
                //Open the pop-up
                angular.element(tagsAddModal).modal({ backdrop: "static" });
                $scope.hideProcessingOverlay(sectionName);
            })
            .error(function (res) {
                if (res == GEN_CONSTANTS.ACCESS_DENIED) {
                    MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE);
                }
                else if (res == GEN_CONSTANTS.DB_ERROR) {
                    MessagePopup($rootScope, ENT_DETAIL_CRUD.TAG_ADD_HDR, ENT_DETAIL_CRUD.TAG_DDL_GET_ERR_MSG);
                }
                else if (res == GEN_CONSTANTS.TIMEOUT_ERROR) {
                    MessagePopup($rootScope, GEN_CONSTANTS.TIMEOUT_ERROR_CONFIRM, GEN_CONSTANTS.TIMEOUT_ERROR_MESSAGE);
                }
                $scope.hideProcessingOverlay(sectionName);
            });
        }

        //Remove tags from enterprise
        var deleteTags = function (row) {
            var localDate = new Date(row.entity.start_dt);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;
            //startDate: $filter('date')(new Date(localTime + localOffset), 'MM/dd/yyyy'),
            $scope.deleteRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                tagsDDList: [],
                selectedTags: row.entity.tag,
                startDate: $filter('date')(localDate, 'MM/dd/yyyy'),
                endDate: EnterpriseAccountDataService.getCurrentDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate()
            };

            //Open the pop-up
            angular.element(tagsDeleteModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay(row.entity.section_nm);
        }

        //submit the tag updates on the enterprise
        $scope.submitTagUpdates = function (actionType) {
            $scope.showProcessingOverlay("Tags");

            //Frame input to the client server
            var updateInput = {};
            updateInput.ent_org_id = $scope.selectedEntOrg;
            if (actionType == 'Insert') {
                updateInput.tag = $scope.addRecord.selectedTags;
                angular.element(tagsAddModal).modal('hide');
            }
            else if (actionType == 'Delete') {
                updateInput.tag = $scope.deleteRecord.selectedTags;
                angular.element(tagsDeleteModal).modal('hide');
            }
            updateInput.action_type = actionType;

            //place the server call and adjust the message on screen based on the response
            enterpriseDetailsService.UpdateTags(updateInput)
            .success(function (result) {
                var popStatusMessage = '';
                var popUpHeaderMessage = '';
                if (actionType == 'Insert')
                    popUpHeaderMessage = ENT_DETAIL_CRUD.TAG_ADD_HDR;
                else if (actionType == 'Delete')
                    popUpHeaderMessage = ENT_DETAIL_CRUD.TAG_DELETE_HDR;

                if (result[0].o_outputMessage == ENT_DETAIL_CRUD.TAG_ADD_VERIFICATION_MSG) {
                    if (actionType == 'Insert')
                        popStatusMessage = ENT_DETAIL_CRUD.TAG_ADD_SUCCESS_MSG + $scope.addRecord.selectedTags;
                    else if (actionType == 'Delete')
                        popStatusMessage = ENT_DETAIL_CRUD.TAG_DELETE_SUCCESS_MSG + $scope.deleteRecord.selectedTags;

                    popStatusMessage += '\' as part of the transaction with ID: ' + result[0].o_outputTrans + '.';
                    enterpriseDetailsService.GetTagsDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.tagsGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result, "row_stat_cd");
                        EnterpriseAccountDataService.setDetails(result, "Tags");
                        $scope.tagsGridOptions.totalItems = $scope.tagsGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalTagsItems = $scope.tagsGridOptions.totalItems;
                        $scope.tagsPaginationCurrentPage = $scope.tagsGridOptions.paginationCurrentPage;
                        $scope.toggleTagsResults = true;
                        $scope.toggleTagsDBError = false;
                        $scope.isTagsInactiveHidden = true;
                        $scope.isTagsFilterFlag = true;
                        $scope.isTagsShowDetailsFlag = false;
                        $scope.hideProcessingOverlay("Tags");
                        MessagePopup($rootScope, popUpHeaderMessage, popStatusMessage);
                    })
                    .error(function (res) {
                        $scope.toggleTagsDBError = true;
                        $scope.hideProcessingOverlay("Tags");
                    });

                    //Refresh the transaction history section
                    $scope.historyRefresh();
                }
                else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.TAG_KEY_MISSING_VERIFICATION_MSG)
                {
                    MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.TAG_KEY_MISSING_ERROR_MSG);
                }
                else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.TAG_DUP_VERIFICATION_MSG) {
                    MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.TAG_DUP_ERROR_MSG + $scope.addRecord.selectedTags + "\'");
                }
                else {
                    MessagePopup($rootScope, ENT_DETAIL_CRUD.DB_ERROR_CONFIRM, ENT_DETAIL_CRUD.DB_ERROR_MESSAGE);
                }
                $scope.hideProcessingOverlay("Tags");
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hideProcessingOverlay("Tags");
            });
        }

        /* ************* Transformation Action Methods ************* */
        $scope.addNewCondition = function (action) {
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showDupTransformationWarning = false;
            $scope.showPotentialAffiliation = false;
            if(action == 'Add')
                $scope.addRecord.listTransformationConditions.push({ condition: "Contains", string: "", dup: '' });
            else
                $scope.editRecord.listTransformationConditions.push({ condition: "Contains", string: "", dup: '' });
        }

        $scope.removeCondition = function (rowIndex, action) {
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showDupTransformationWarning = false;
            $scope.showPotentialAffiliation = false;
            if (action == 'Add') {
                angular.forEach($scope.addRecord.listTransformationConditions, function (v, k) {
                    if (k == rowIndex) {
                        $scope.addRecord.listTransformationConditions.splice(rowIndex, 1);
                    }
                });
            }
            else
            {
                angular.forEach($scope.editRecord.listTransformationConditions, function (v, k) {
                    if (k == rowIndex) {
                        $scope.editRecord.listTransformationConditions.splice(rowIndex, 1);
                    }
                });
            }
        }

        $scope.editTransformations = function (row) {
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showDupTransformationWarning = false;
            $scope.showPotentialAffiliation = false;
            var localDate = new Date(row.treeNode.children[0].row.entity.org_nm_transform_strt_dt);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;
            var commentString = row.treeNode.children[0].row.entity.ent_org_branch;
            if (commentString == null || angular.isUndefined(commentString))
                commentString = '';
            $scope.editRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                comment: commentString,
                startDate: $filter('date')(new Date(localDate), 'MM/dd/yyyy'),
                endDate: EnterpriseAccountDataService.getEndDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate(),
                cdimTransformId: row.treeNode.children[0].row.entity.cdim_transform_id,
                listTransformationConditions: [],
                listCondition: [
                    { key: "Contains", val: "Contains" },
                    { key: "Does not Contain", val: "Does not Contain" },
                    { key: "Starts With", val: "Starts With" },
                    { key: "Exact Match", val: "Exact Match" }
                ]
            };

            angular.forEach(row.treeNode.children, function (val, key) {
                var strConcatenatedValue = val.row.entity.conditional;
                var strCondition = "";
                var strString = "";
                strCondition = angular.copy(strConcatenatedValue.split('"')[0].trim());
                strCondition = strCondition.replace("AND ", "");
                strString = strConcatenatedValue.split('"')[1].trim();
                $scope.editRecord.listTransformationConditions.push({ condition: strCondition, string: strString, dup: '' });
            });

            $scope.editRecordsBK = angular.copy($scope.editRecord.listTransformationConditions);
            $scope.editRecordCommentBK = row.treeNode.children[0].row.entity.ent_org_branch;
            if ($scope.editRecordCommentBK == null || angular.isUndefined($scope.editRecordCommentBK))
                $scope.editRecordCommentBK = '';

            //Open the pop-up
            angular.element(transformationsEditModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay("TransformationDetail");
        }

        var addTransformations = function (sectionName) {
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showDupTransformationWarning = false;
            $scope.showPotentialAffiliation = false;
            $scope.addRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                listTransformationConditions: [
                    { condition: "Contains", string: "", dup: '' }
                ],
                listCondition: [
                    { key: "Contains", val: "Contains" },
                    { key: "Does not Contain", val: "Does not Contain" },
                    { key: "Starts With", val: "Starts With" },
                    { key: "Exact Match", val: "Exact Match" }
                ],
                comment: '',
                startDate: EnterpriseAccountDataService.getCurrentDate(),
                endDate: EnterpriseAccountDataService.getEndDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate()
            };

            //Open the pop-up
            angular.element(transformationsAddModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay(sectionName);
        }

        //Delete transformations
        $scope.deleteTransformations = function (row) {
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showDupTransformationWarning = false;
            $scope.showPotentialAffiliation = false;
            var localDate = new Date(row.treeNode.children[0].row.entity.org_nm_transform_strt_dt);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;

            var patternString = "";
            angular.forEach(row.treeNode.children, function (val, key) {
                if (patternString == "")
                    patternString = val.row.entity.conditional;
                else
                    patternString = patternString + val.row.entity.conditional;
            });

            $scope.deleteRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                comment: row.treeNode.children[0].row.entity.ent_org_branch,
                condition: patternString,
                startDate: $filter('date')(new Date(localDate), 'MM/dd/yyyy'),
                endDate: EnterpriseAccountDataService.getCurrentDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate(),
                cdimTransformId: row.treeNode.children[0].row.entity.cdim_transform_id
            };

            //Open the pop-up
            angular.element(transformationsDeleteModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay("TransformationDetail");
        }

        //submit the transformation updates on the enterprise
        $scope.submitTransformationUpdates = function (actionType) {
            $scope.showProcessingOverlay("TransformationDetail");
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showDupTransformationWarning = false;
            $scope.showPotentialAffiliation = false;

            //Frame input to the client server
            var updateInput = {};
            updateInput.ent_org_id = $scope.selectedEntOrg;
            updateInput.req_typ = actionType;
            updateInput.ltCondition = [];

            var tempList = [];
            var AnotherArray = [];
            var DuplicateList = [];

            //Validate the input before submission
            var boolValid = true;
            var boolNoChange = false;
            var boolDup = false;
            var boolDupTransformation = false;

            var tempRes = EnterpriseAccountDataService.getActiveDetails("TransformationDetail");
            var existingRules = tempRes.output; //variable to store the existing transformations
            var formattedExistingRules = {};
            var configuredRules = [];
            //Format the transformation rules to get grouped by
            angular.forEach(existingRules, function (v1, k1) {
                //check for the existance of the object
                if (!angular.isUndefined(formattedExistingRules[v1.cdim_transform_id]))
                    formattedExistingRules[v1.cdim_transform_id].push(v1);
                else {
                    formattedExistingRules[v1.cdim_transform_id] = [];
                    formattedExistingRules[v1.cdim_transform_id].push(v1);
                }
            });

            if (actionType == 'Add') {
                angular.forEach($scope.addRecord.listTransformationConditions, function (v, k) {
                    //Validate for the Does not contain check
                    var boolDistinct = true;
                    angular.forEach(tempList, function (v1, k1) {
                        if (v1 == v.condition)
                            boolDistinct = false;
                    });

                    if (boolDistinct)
                        tempList.push(v.condition);

                    //Validate for the duplicates
                    if (AnotherArray.length > 0) {
                        angular.forEach(AnotherArray, function (v1, k1) {
                            if (v1.condition == v.condition && EnterpriseAccountDataService.getLower(v1.string) == EnterpriseAccountDataService.getLower(v.string))
                                DuplicateList.push(angular.copy(v));
                            else
                                AnotherArray.push(angular.copy(v));
                        });
                    }
                    else {
                        AnotherArray.push(angular.copy(v));
                    }
                });

                //Check the existance of condition other than 'Does not contain'
                if (tempList.length == 1 && tempList[0] == "Does not Contain")
                    boolValid = false;

                angular.forEach($scope.addRecord.listTransformationConditions, function (v, k) {
                    v.dup = '#ffffff';
                    angular.forEach(DuplicateList, function (v1, k1) {
                        if (v1.condition == v.condition && EnterpriseAccountDataService.getLower(v1.string) == EnterpriseAccountDataService.getLower(v.string)) {
                            v.dup = '#f9e5e5';
                            boolDup = true;
                        }
                    });
                });

                if (!boolDup) {
                    //Check for Duplicate transformation
                    angular.forEach(formattedExistingRules, function (v1, k1) {
                        configuredRules = [];
                        //For each of the transformation check if the rules match with the configured ones
                        if (!angular.isUndefined(v1)) {
                            angular.forEach(v1, function (v2, k2) {
                                var strConcatenatedValue = v2.conditional;
                                var strCondition = "";
                                var strString = "";
                                strCondition = angular.copy(strConcatenatedValue.split('"')[0].trim());
                                strCondition = strCondition.replace("AND ", "");
                                strString = strConcatenatedValue.split('"')[1].trim();

                                angular.forEach($scope.addRecord.listTransformationConditions, function (v3, k3) {
                                    if (strCondition == v3.condition && strString == v3.string)
                                        configuredRules.push(v2);
                                });
                            });
                        }

                        if (v1.length == configuredRules.length)
                            boolDupTransformation = true;
                    });
                }
            }
            else if (actionType == 'Edit') {
                var tempList = [];
                var ruleMatchingList = [];
                //Validate for the Does not contain check
                angular.forEach($scope.editRecord.listTransformationConditions, function (v, k) {
                    var boolDistinct = true;
                    var boolRuleExists = false;
                    angular.forEach(tempList, function (v1, k1) {
                        if (v1 == v.condition)
                            boolDistinct = false;
                    });

                    if (boolDistinct)
                        tempList.push(v.condition);

                    //Validate for the duplicates
                    if (AnotherArray.length > 0) {
                        angular.forEach(AnotherArray, function (v1, k1) {
                            if (v1.condition == v.condition && EnterpriseAccountDataService.getLower(v1.string) == EnterpriseAccountDataService.getLower(v.string))
                                DuplicateList.push(angular.copy(v));
                            else
                                AnotherArray.push(angular.copy(v));
                        });
                    }
                    else {
                        AnotherArray.push(angular.copy(v));
                    }

                    //Validate for changes
                    angular.forEach($scope.editRecordsBK, function (v1, k1) {
                        if (v.condition == v1.condition && EnterpriseAccountDataService.getLower(v.string) == EnterpriseAccountDataService.getLower(v1.string))
                            ruleMatchingList.push(angular.copy(v));
                    });
                });

                if (tempList.length == 1 && tempList[0] == "Does not Contain")
                    boolValid = false;

                angular.forEach($scope.editRecord.listTransformationConditions, function (v, k) {
                    v.dup = '#ffffff';
                    angular.forEach(DuplicateList, function (v1, k1) {
                        if (v1.condition == v.condition && EnterpriseAccountDataService.getLower(v1.string) == EnterpriseAccountDataService.getLower(v.string)) {
                            v.dup = '#f9e5e5';
                            boolDup = true;
                        }
                    });
                });

                if (ruleMatchingList.length == $scope.editRecordsBK.length && $scope.editRecordsBK.length == $scope.editRecord.listTransformationConditions.length && $scope.editRecordCommentBK.trim() == $scope.editRecord.comment.trim())
                    boolNoChange = true;

                if (!boolDup && !boolNoChange) {
                    //Check for Duplicate transformation
                    angular.forEach(formattedExistingRules, function (v1, k1) {
                        configuredRules = [];
                        //For each of the transformation check if the rules match with the configured ones
                        if (!angular.isUndefined(v1) && $scope.editRecord.cdimTransformId != k1) {
                            angular.forEach(v1, function (v2, k2) {
                                var strConcatenatedValue = v2.conditional;
                                var strCondition = "";
                                var strString = "";
                                strCondition = angular.copy(strConcatenatedValue.split('"')[0].trim());
                                strCondition = strCondition.replace("AND ", "");
                                strString = strConcatenatedValue.split('"')[1].trim();

                                angular.forEach($scope.editRecord.listTransformationConditions, function (v3, k3) {
                                    if (strCondition == v3.condition && strString == v3.string)
                                        configuredRules.push(v2);
                                });
                            });
                        }

                        if (v1.length == configuredRules.length)
                            boolDupTransformation = true;
                    });
                }
            }

            if (boolNoChange) {
                $scope.showNoChangeWarning = true;
                $scope.hideProcessingOverlay("TransformationDetail");
            }
            else if (boolDup) {
                $scope.showDupWarning = true;
                $scope.hideProcessingOverlay("TransformationDetail");
            }
            else if (boolDupTransformation) {
                $scope.showDupTransformationWarning = true;
                $scope.hideProcessingOverlay("TransformationDetail");
            }
            else if (boolValid) {
                if (actionType == 'Add') {
                    updateInput.cdim_transform_id = $scope.addRecord.cdimTransformId;
                    updateInput.ent_org_branch = $scope.addRecord.comment;
                    updateInput.active_ind = "1";
                    angular.forEach($scope.addRecord.listTransformationConditions, function (v, k) {
                        updateInput.ltCondition.push({ condition_typ: v.condition.trim(), pattern_string: v.string.trim() });
                    });

                    angular.element(transformationsAddModal).modal('hide');
                }
                else if (actionType == 'Edit') {
                    updateInput.cdim_transform_id = $scope.editRecord.cdimTransformId;
                    updateInput.ent_org_branch = $scope.editRecord.comment;
                    updateInput.active_ind = "1";
                    angular.forEach($scope.editRecord.listTransformationConditions, function (v, k) {
                        updateInput.ltCondition.push({ condition_typ: v.condition.trim(), pattern_string: v.string.trim() });
                    });

                    angular.element(transformationsEditModal).modal('hide');
                }
                else if (actionType == 'Delete') {
                    updateInput.cdim_transform_id = $scope.deleteRecord.cdimTransformId;
                    updateInput.ent_org_branch = $scope.deleteRecord.comment;
                    updateInput.active_ind = "0";
                    angular.element(transformationsDeleteModal).modal('hide');
                }

                //place the server call and adjust the message on screen based on the response
                enterpriseDetailsService.UpdateTransformations(updateInput)
                .success(function (result) {
                    var popStatusMessage = '';
                    var popUpHeaderMessage = '';
                    if (actionType == 'Add')
                        popUpHeaderMessage = ENT_DETAIL_CRUD.TRANSFORMATION_ADD_HDR;
                    else if (actionType == 'Edit')
                        popUpHeaderMessage = ENT_DETAIL_CRUD.TRANSFORMATION_EDIT_HDR;
                    else if (actionType == 'Delete')
                        popUpHeaderMessage = ENT_DETAIL_CRUD.TRANSFORMATION_DEL_HDR;

                    if (result[0].o_outputMessage == ENT_DETAIL_CRUD.TRANSFORMATION_ADD_VERIFICATION_MSG) {
                        popStatusMessage = ENT_DETAIL_CRUD.TRANSFORMATION_ADD_SUCCESS_MSG + result[0].o_transaction_key + '.';

                        //Refresh the transformation details section
                        enterpriseDetailsService.GetTransformationDetailDetails($scope.selectedEntOrg)
                        .success(function (result) {
                            $scope.transformationDetailGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result.output, "act_ind");
                            EnterpriseAccountDataService.setDetails(result, "TransformationDetail");
                            $scope.transformationDetailGridOptions.totalItems = $scope.transformationDetailGridOptions.data.length;
                            //Variables used for paginating the search results
                            $scope.totalTransformationDetailItems = $scope.transformationDetailGridOptions.totalItems;
                            $scope.transformationDetailPaginationCurrentPage = $scope.transformationDetailGridOptions.paginationCurrentPage;
                            $scope.toggleTransformationDetailResults = true;
                            $scope.toggleTransformationDetailDBError = false;
                            $scope.transformationDetailGridOptions.paginationPageSize = 10000;
                            $scope.isTransformationDetailInactiveHidden = true;
                            $scope.isTransformationDetailFilterFlag = true;
                            $scope.isTransformationDetailShowDetailsFlag = false;
                            $scope.manuallyAffiliatedMasterCount = 0;
                            $scope.manuallyAffiliatedMasterCount = result.manual_affil_cnt;

                            $scope.hideProcessingOverlay("TransformationDetail");
                            MessagePopup($rootScope, popUpHeaderMessage, popStatusMessage);
                        })
                        .error(function (res) {
                            errorPopups(res);
                            //$scope.toggleTransformationDetailDBError = true;
                            $scope.hideProcessingOverlay("TransformationDetail");
                        });

                        //Refresh the transaction history section
                        $scope.historyRefresh();
                    }
                    else {
                        MessagePopup($rootScope, ENT_DETAIL_CRUD.DB_ERROR_CONFIRM, ENT_DETAIL_CRUD.DB_ERROR_MESSAGE);
                        $scope.hideProcessingOverlay("TransformationDetail");
                    }
                })
                .error(function (result) {
                    errorPopups(result);
                    $scope.hideProcessingOverlay("TransformationDetail");
                });
            }
            else
            {
                $scope.showDoesNotContainWarning = true;
                $scope.hideProcessingOverlay("TransformationDetail");
            }
        }

        $scope.smokeTestingCount = function (actionType) {
            $scope.toggleSmokeTestProcessOverlay = true;
            $scope.showDoesNotContainWarning = false;
            $scope.showNoChangeWarning = false;
            $scope.showDupWarning = false;
            $scope.showPotentialAffiliation = false;

            //Frame input to the client server
            var updateInput = {};
            updateInput.ent_org_id = $scope.selectedEntOrg;
            updateInput.req_typ = actionType;
            updateInput.ltCondition = [];

            if (actionType == 'Add') {
                updateInput.cdim_transform_id = $scope.addRecord.cdimTransformId;
                updateInput.ent_org_branch = $scope.addRecord.comment;
                updateInput.active_ind = "1";
                angular.forEach($scope.addRecord.listTransformationConditions, function (v, k) {
                    updateInput.ltCondition.push({ condition_typ: v.condition.trim(), pattern_string: v.string.trim() });
                });
            }
            else if (actionType == 'Edit') {
                updateInput.cdim_transform_id = $scope.editRecord.cdimTransformId;
                updateInput.ent_org_branch = $scope.editRecord.comment;
                updateInput.active_ind = "1";
                angular.forEach($scope.editRecord.listTransformationConditions, function (v, k) {
                    updateInput.ltCondition.push({ condition_typ: v.condition.trim(), pattern_string: v.string.trim() });
                });
            }

            //place the server call and adjust the message on screen based on the response
            enterpriseDetailsService.SmokeTestingCount(updateInput)
            .success(function (result) {
                $scope.showPotentialAffiliation = true;
                $scope.PotentialAffiliationCount = 0;
                $scope.DeltaAffiliationCount = 0;

                if (!angular.isUndefined(result[0])) {
                    $scope.PotentialAffiliationCount = result[0].total_affil_cnt;
                    $scope.DeltaAffiliationCount = result[0].delta_affil_cnt;
                }
                $scope.toggleSmokeTestProcessOverlay = false;
            })
            .error(function (result) {
                //Close the action pop-up
                if (actionType == 'Add') {
                    angular.element(transformationsAddModal).modal('hide');
                }
                else if (actionType == 'Edit') {
                    angular.element(transformationsEditModal).modal('hide');
                }

                errorPopups(result);
                $scope.toggleSmokeTestProcessOverlay = false;
            });
        }

        $scope.exportPotentialMasters = function (actionType) {
            $scope.toggleSmokeTestProcessOverlay = true;

            //Frame input to the client server
            var updateInput = {};
            updateInput.ent_org_id = $scope.selectedEntOrg;
            updateInput.req_typ = actionType;
            updateInput.ltCondition = [];

            if (actionType == 'Add') {
                updateInput.cdim_transform_id = $scope.addRecord.cdimTransformId;
                updateInput.ent_org_branch = $scope.addRecord.comment;
                updateInput.active_ind = "1";
                angular.forEach($scope.addRecord.listTransformationConditions, function (v, k) {
                    updateInput.ltCondition.push({ condition_typ: v.condition.trim(), pattern_string: v.string.trim() });
                });
            }
            else if (actionType == 'Edit') {
                updateInput.cdim_transform_id = $scope.editRecord.cdimTransformId;
                updateInput.ent_org_branch = $scope.editRecord.comment;
                updateInput.active_ind = "1";
                angular.forEach($scope.editRecord.listTransformationConditions, function (v, k) {
                    updateInput.ltCondition.push({ condition_typ: v.condition.trim(), pattern_string: v.string.trim() });
                });
            }

            exportService.SmokeTestingExportTransformationOrgDetails(updateInput)
            .success(function (result) {
                $scope.toggleSmokeTestProcessOverlay = false;
            })
            .error(function (result) {
                //Close the action pop-up
                if (actionType == 'Add') {
                    angular.element(transformationsAddModal).modal('hide');
                }
                else if (actionType == 'Edit') {
                    angular.element(transformationsEditModal).modal('hide');
                }
                errorPopups(result);
                $scope.toggleSmokeTestProcessOverlay = false;
            });
        }

        /* ************* Affiliations Action Methods ************* */
        $scope.deleteAffiliation = function (row, sectionName) {
            $scope.showProcessingOverlay(sectionName);
            var localDate = new Date(row.treeNode.children[0].row.entity.cnst_affil_strt_ts);

            $scope.deleteRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                mstrId: row.treeNode.children[0].row.entity.cnst_mstr_id,
                mstrName: row.treeNode.children[0].row.entity.mstr_name,
                startDate: $filter('date')(new Date(localDate), 'MM/dd/yyyy'),
                endDate: EnterpriseAccountDataService.getCurrentDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate(),
                sectionName: sectionName
            };

            //Open the pop-up
            angular.element(affiliationsDeleteModal).modal();
            $scope.hideProcessingOverlay(sectionName);
        }

        var addAffiliations = function (sectionName) {
            $scope.addRecord = {
                entOrgId: $scope.selectedEntOrg,
                entOrgName: $scope.selectedEntOrgNm,
                mstrId: '',
                startDate: EnterpriseAccountDataService.getCurrentDate(),
                endDate: EnterpriseAccountDataService.getEndDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate(),
                sectionName: sectionName
            };

            //Open the pop-up
            angular.element(affiliationsAddModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay(sectionName);
        }

        $scope.submitAffiliationUpdates = function (actionType, sectionName) {
            $scope.showProcessingOverlay(sectionName);

            //Frame input to the client server
            var updateInput = {};

            if (actionType == 'Add') {
                updateInput.new_ent_org_id = $scope.selectedEntOrg;
                updateInput.mstr_id = $scope.addRecord.mstrId;
                angular.element(affiliationsAddModal).modal('hide');
            }
            else if (actionType == 'Delete') {
                updateInput.bk_ent_org_id = $scope.selectedEntOrg;
                updateInput.mstr_id = $scope.deleteRecord.mstrId;
                angular.element(affiliationsDeleteModal).modal('hide');
            }
            updateInput.req_typ = actionType;
            updateInput.cnst_typ = 'OR';

            //place the server call and adjust the message on screen based on the response
            enterpriseDetailsService.UpdateAffiliations(updateInput)
            .success(function (result) {
                var popStatusMessage = '';
                var popUpHeaderMessage = '';
                if (actionType == 'Add')
                    popUpHeaderMessage = ENT_DETAIL_CRUD.AFFILIATION_ADD_HDR;
                else if (actionType == 'Delete')
                    popUpHeaderMessage = ENT_DETAIL_CRUD.AFFILIATION_DELETE_HDR;

                if (result[0].o_outputMessage == ENT_DETAIL_CRUD.AFFILIATION_ADD_VERIFICATION_MSG) {
                    popStatusMessage = ENT_DETAIL_CRUD.AFFILIATION_SUCCESS_MSG + result[0].o_transaction_key + '\'.';

                    var serviceInput = {};
                    serviceInput.ent_org_id = $scope.selectedEntOrg;
                    serviceInput.AffiliationLimit = "0";
                    serviceInput.strLoadType = "initial";

                    enterpriseDetailsService.GetMasterBridgeDetails(serviceInput)
                    .success(function (result) {

                        EnterpriseAccountDataService.setDetails(result, sectionName);

                        //Refresh tree grouped by address
                        $scope.masterBridgeLocationGridOptions.data = result.lt_affil_res;
                        $scope.masterBridgeLocationGridOptions.totalItems = $scope.masterBridgeLocationGridOptions.data.length;
                        $scope.masterBridgeLocationGridOptions.paginationPageSize = 1000000;
                        //Variables used for paginating the search results
                        $scope.toggleMasterBridgeLocationResults = true;
                        $scope.toggleMasterBridgeLocationDBError = false;
                        $scope.isMasterBridgeLocationInactiveHidden = false;
                        $scope.isMasterBridgeLocationFilterFlag = false;
                        $scope.isMasterBridgeLocationShowDetailsFlag = false;

                        //Refresh tree grouped by master id
                        $scope.masterBridgeMasterGridOptions.data = result.lt_affil_res;
                        $scope.masterBridgeMasterGridOptions.totalItems = $scope.masterBridgeMasterGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalMasterBridgeMasterItems = $scope.masterBridgeMasterGridOptions.totalItems;
                        $scope.masterBridgeMasterPaginationCurrentPage = $scope.masterBridgeMasterGridOptions.paginationCurrentPage;
                        $scope.masterBridgeMasterGridOptions.paginationPageSize = 1000000;
                        $scope.toggleMasterBridgeMasterResults = true;
                        $scope.toggleMasterBridgeMasterDBError = false;
                        $scope.isMasterBridgeMasterInactiveHidden = false;
                        $scope.isMasterBridgeMasterFilterFlag = false;
                        $scope.isMasterBridgeMasterShowDetailsFlag = false;

                        //Aggregates
                        $scope.total_rec_cnt = 0;
                        $scope.prospect_cnt = 0;
                        $scope.act_valid_cnt = 0;
                        $scope.inact_valid_cnt = 0;
                        $scope.act_invalid_cnt = 0;
                        $scope.inact_invalid_cnt = 0;
                        $scope.total_mstr_cnt = 0;
                        $scope.diffOrgTypes = '';

                        var smry_cnt = result.summary_info;
                        $scope.total_rec_cnt = smry_cnt.total_brid_cnt;
                        $scope.prospect_cnt = smry_cnt.pros_ind;
                        $scope.act_valid_cnt = smry_cnt.act_val_ind;
                        $scope.inact_valid_cnt = smry_cnt.inact_val_ind;
                        $scope.act_invalid_cnt = smry_cnt.act_unval_ind;
                        $scope.inact_invalid_cnt = smry_cnt.inact_unval_ind;
                        $scope.total_mstr_cnt = smry_cnt.total_mstr_cnt;
                        $scope.diffOrgTypes = smry_cnt.str_concat_org_typ_cnt;

                        $scope.hideProcessingOverlay(sectionName);
                        MessagePopup($rootScope, popUpHeaderMessage, popStatusMessage);
                    })
                    .error(function (res) {
                        errorPopups(res);
                        $scope.hideProcessingOverlay(sectionName);
                    });

                    //Refresh the transaction history section
                    $scope.historyRefresh();
                }
                else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.AFFILIATION_DUP_VERIFICATION_MSG && actionType == 'Add') {
                    $scope.hideProcessingOverlay(sectionName);
                    MessagePopup($rootScope, popUpHeaderMessage, "Noticed duplicate insertion, since organization (" + $scope.addRecord.mstrId + ") is already affiliated to the enterprise (" + $scope.addRecord.entOrgId + ").");
                }
                else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.AFFILIATION_MISSING_ORG_VERIFICATION_MSG && actionType == 'Delete') {
                    $scope.hideProcessingOverlay(sectionName);
                    MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.AFFILIATION_MISSING_ORG_MSG);
                }
                else {
                    $scope.hideProcessingOverlay(sectionName);
                    MessagePopup($rootScope, ENT_DETAIL_CRUD.DB_ERROR_CONFIRM, ENT_DETAIL_CRUD.DB_ERROR_MESSAGE);
                }
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hideProcessingOverlay(sectionName);
            });
        }

        /* ************* Hierarchy Action Methods ************* */
        //Hierarchy enterprise
        $scope.addHierarchy = function (row, grid, action) {
            $scope.addRecord = {};
            var parentLevel = '';
            var childLevel = '';

            if (action == 'addchildren') {
                var selParentEntOrgID = '';
                var selParentEntOrgName = '';
                if (row.groupHeader) {
                    switch(row.treeLevel)
                    {
                        case 0: selParentEntOrgID = row.grid.rows[0].entity.lvl1_ent_org_id;
                                selParentEntOrgName = row.grid.rows[0].entity.lvl1_ent_org_name;
                                parentLevel = 'L1';
                                childLevel = 'L2';
                                break;
                        case 1: angular.forEach(row.treeNode.aggregations, function (agg, key) {
                                    if (agg.col.field == "lvl2_ent_org_id")
                                        selParentEntOrgID = agg.groupVal;
                                    if (agg.col.field == "lvl2_ent_org_name")
                                        selParentEntOrgName = agg.value;
                                });
                                parentLevel = 'L2';
                                childLevel = 'L3';
                                break;
                        case 2: angular.forEach(row.treeNode.aggregations, function (agg, key) {
                                    if (agg.col.field == "lvl3_ent_org_id")
                                        selParentEntOrgID = agg.groupVal;
                                    if (agg.col.field == "lvl3_ent_org_name")
                                        selParentEntOrgName = agg.value;
                                });
                                parentLevel = 'L3';
                                childLevel = 'L4';
                                break;
                        case 3: angular.forEach(row.treeNode.aggregations, function (agg, key) {
                                    if (agg.col.field == "lvl4_ent_org_id")
                                        selParentEntOrgID = agg.groupVal;
                                    if (agg.col.field == "lvl4_ent_org_name")
                                        selParentEntOrgName = agg.value;
                                });
                                parentLevel = 'L4';
                                childLevel = 'L5';
                                break;
                    }
                }
                else {
                    if (row.treeNode.parentRow != null) {
                        switch (row.treeNode.parentRow.treeLevel) {
                            case 0: selParentEntOrgID = row.entity.lvl2_ent_org_id;
                                selParentEntOrgName = row.entity.lvl2_ent_org_name;
                                parentLevel = 'L2';
                                childLevel = 'L3';
                                break;
                            case 1: selParentEntOrgID = row.entity.lvl3_ent_org_id;
                                selParentEntOrgName = row.entity.lvl3_ent_org_name;
                                parentLevel = 'L3';
                                childLevel = 'L4';
                                break;
                            case 2: selParentEntOrgID = row.entity.lvl4_ent_org_id;
                                selParentEntOrgName = row.entity.lvl4_ent_org_name;
                                parentLevel = 'L4';
                                childLevel = 'L5';
                                break;
                        }
                    }
                    else {
                        selParentEntOrgID = row.entity.lvl1_ent_org_id;
                        selParentEntOrgName = row.entity.lvl1_ent_org_name;
                        parentLevel = 'L1';
                        childLevel = 'L2';
                    }
                }

                $scope.addRecord = {
                    parentEntOrgId: selParentEntOrgID,
                    parentEntOrgName: selParentEntOrgName,
                    childEntOrgId: '',
                    childEntOrgName: '',
                    startDate: EnterpriseAccountDataService.getCurrentDate(),
                    endDate: EnterpriseAccountDataService.getEndDate(),
                    dwDate: EnterpriseAccountDataService.getCurrentDate(),
                    IsParentAddition: false,
                    parentLevel: parentLevel,
                    childLevel: childLevel
                };
            }
            else if (action == 'addparent') {
                //Check for the level
                var selChildEntOrgID = '';
                var selChildEntOrgName = '';
                if (row.groupHeader)
                {
                    selChildEntOrgID = row.grid.rows[0].entity.lvl1_ent_org_id;
                    selChildEntOrgName = row.grid.rows[0].entity.lvl1_ent_org_name;
                }
                else
                {
                    selChildEntOrgID = row.entity.lvl1_ent_org_id;
                    selChildEntOrgName = row.entity.lvl1_ent_org_name;
                }

                $scope.addRecord = {
                    parentEntOrgId: '',
                    parentEntOrgName: '',
                    childEntOrgId: selChildEntOrgID,
                    childEntOrgName: selChildEntOrgName,
                    startDate: EnterpriseAccountDataService.getCurrentDate(),
                    endDate: EnterpriseAccountDataService.getEndDate(),
                    dwDate: EnterpriseAccountDataService.getCurrentDate(),
                    IsParentAddition: true,
                    parentLevel: 'L1',
                    childLevel: 'L2'
                };
            }

            //Open the pop-up
            angular.element(hierarchyAddModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay("EntOrgHierarchy");
        }

        //Remove Hierarchy from enterprise
        $scope.deleteHierarchy = function (row) {
            var selParentEntOrgID = '';
            var selParentEntOrgName = '';
            var selChildEntOrgID = '';
            var selChildEntOrgName = '';
            var startDate;
            var parentLevel = '';
            var childLevel = '';
            if (row.groupHeader) {
                switch (row.treeLevel) {
                    case 1:
                        angular.forEach(row.treeNode.aggregations, function (agg, key) {
                            if (agg.col.field == "lvl1_ent_org_id")
                                selParentEntOrgID = row.treeNode.parentRow.treeNode.aggregations[key].groupVal;
                            if (agg.col.field == "lvl1_ent_org_name")
                                selParentEntOrgName = agg.value;
                            if (agg.col.field == "lvl2_ent_org_id")
                                selChildEntOrgID = agg.groupVal;
                            if (agg.col.field == "lvl2_ent_org_name")
                                selChildEntOrgName = agg.value;
                            if (agg.col.field == "lvl2_start_dt")
                                startDate = agg.value;
                        });
                        parentLevel = 'L1';
                        childLevel = 'L2';
                        break;
                    case 2:
                        angular.forEach(row.treeNode.aggregations, function (agg, key) {
                            if (agg.col.field == "lvl2_ent_org_id")
                                selParentEntOrgID = row.treeNode.parentRow.treeNode.aggregations[key].groupVal;
                            if (agg.col.field == "lvl2_ent_org_name")
                                selParentEntOrgName = agg.value;
                            if (agg.col.field == "lvl3_ent_org_id")
                                selChildEntOrgID = agg.groupVal;
                            if (agg.col.field == "lvl3_ent_org_name")
                                selChildEntOrgName = agg.value;
                            if (agg.col.field == "lvl3_start_dt")
                                startDate = agg.value;
                        });
                        parentLevel = 'L2';
                        childLevel = 'L3';
                        break;
                    case 3:
                        angular.forEach(row.treeNode.aggregations, function (agg, key) {
                            if (agg.col.field == "lvl3_ent_org_id")
                                selParentEntOrgID = row.treeNode.parentRow.treeNode.aggregations[key].groupVal;
                            if (agg.col.field == "lvl3_ent_org_name")
                                selParentEntOrgName = agg.value;
                            if (agg.col.field == "lvl4_ent_org_id")
                                selChildEntOrgID = agg.groupVal;
                            if (agg.col.field == "lvl4_ent_org_name")
                                selChildEntOrgName = agg.value;
                            if (agg.col.field == "lvl4_start_dt")
                                startDate = agg.value;
                        });
                        parentLevel = 'L3';
                        childLevel = 'L4';
                        break;
                }
            }
            else {
                if (row.treeNode.parentRow != null) {
                    switch (row.treeNode.parentRow.treeLevel) {
                        case 0: selParentEntOrgID = row.entity.lvl1_ent_org_id;
                            selParentEntOrgName = row.entity.lvl1_ent_org_name;
                            selChildEntOrgID = row.entity.lvl2_ent_org_id;
                            selChildEntOrgName = row.entity.lvl2_ent_org_name;
                            startDate = row.entity.lvl2_start_dt;
                            parentLevel = 'L1';
                            childLevel = 'L2';
                            break;
                        case 1: selParentEntOrgID = row.entity.lvl2_ent_org_id;
                            selParentEntOrgName = row.entity.lvl2_ent_org_name;
                            selChildEntOrgID = row.entity.lvl3_ent_org_id;
                            selChildEntOrgName = row.entity.lvl3_ent_org_name;
                            startDate = row.entity.lvl3_start_dt;
                            parentLevel = 'L2';
                            childLevel = 'L3';
                            break;
                        case 2: selParentEntOrgID = row.entity.lvl3_ent_org_id;
                            selParentEntOrgName = row.entity.lvl3_ent_org_name;
                            selChildEntOrgID = row.entity.lvl4_ent_org_id;
                            selChildEntOrgName = row.entity.lvl4_ent_org_name;
                            startDate = row.entity.lvl4_start_dt;
                            parentLevel = 'L3';
                            childLevel = 'L4';
                            break;
                        case 3: selParentEntOrgID = row.entity.lvl4_ent_org_id;
                            selParentEntOrgName = row.entity.lvl4_ent_org_name;
                            selChildEntOrgID = row.entity.lvl5_ent_org_id;
                            selChildEntOrgName = row.entity.lvl5_ent_org_name;
                            startDate = row.entity.lvl5_start_dt;
                            parentLevel = 'L4';
                            childLevel = 'L5';
                            break;
                    }
                }
            }

            //Number of children
            var hierDeleteChildCount = 0;
            if (!angular.isUndefined(row.treeNode.children) && row.treeNode.children.length > 0) {
                hierDeleteChildCount = findChildCount(row.treeNode.children);
            }

            var localDate = new Date(startDate);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;
            //startDate: $filter('date')(new Date(localTime + localOffset), 'MM/dd/yyyy'),
            $scope.deleteRecord = {
                parentEntOrgId: selParentEntOrgID,
                parentEntOrgName: selParentEntOrgName,
                childEntOrgId: selChildEntOrgID,
                childEntOrgName: selChildEntOrgName,
                startDate: $filter('date')(localDate, 'MM/dd/yyyy'),
                endDate: EnterpriseAccountDataService.getCurrentDate(),
                dwDate: EnterpriseAccountDataService.getCurrentDate(),
                hierDeleteChildCount: hierDeleteChildCount,
                parentLevel: parentLevel,
                childLevel: childLevel
            };

            //Open the pop-up
            angular.element(hierarchyDeleteModal).modal({ backdrop: "static" });
            $scope.hideProcessingOverlay("EntOrgHierarchy");
        }

        var findChildCount = function (chilList) {
            var tempCount = 0;
            angular.forEach(chilList, function (v1, k1) {
                tempCount += 1;
                if (!angular.isUndefined(v1.treeNode)) {
                    if (!angular.isUndefined(v1.treeNode.children) && v1.treeNode.children.length > 0)
                    {
                        tempCount += findChildCount(v1);
                    }
                }
            });
            return tempCount;
        }

        //submit the hierarchy updates on the enterprise
        $scope.submitHierarchyUpdates = function (actionType) {
            $scope.showProcessingOverlay("EntOrgHierarchy");
            var boolSameID = false;

            //Frame input to the client server
            var updateInput = {};
            if (actionType == 'Add') {
                updateInput.superior_ent_org_key = $scope.addRecord.parentEntOrgId.trim();
                updateInput.subodinate_ent_org_key = $scope.addRecord.childEntOrgId.trim();
                updateInput.rlshp_cd = 'ent_org_hierarchy';
                updateInput.rlshp_desc = 'Enterprise Organization Hierarchy';
                angular.element(hierarchyAddModal).modal('hide');

                if (updateInput.superior_ent_org_key == updateInput.subodinate_ent_org_key)
                    boolSameID = true;
            }
            else if (actionType == 'Delete') {
                updateInput.superior_ent_org_key = $scope.deleteRecord.parentEntOrgId.trim();
                updateInput.subodinate_ent_org_key = $scope.deleteRecord.childEntOrgId.trim();
                updateInput.rlshp_cd = 'ent_org_hierarchy';
                updateInput.rlshp_desc = 'Enterprise Organization Hierarchy';
                angular.element(hierarchyDeleteModal).modal('hide');
            }
            updateInput.action_type = actionType;

            var popUpHeaderMessage = '';
            if (actionType == 'Add')
                popUpHeaderMessage = ENT_DETAIL_CRUD.HIERARCHY_ADD_HDR;
            else if (actionType == 'Delete')
                popUpHeaderMessage = ENT_DETAIL_CRUD.HIERARCHY_DEL_HDR;

            if (boolSameID) {
                $scope.hideProcessingOverlay("EntOrgHierarchy");
                MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.HIERARCHY_SAME_EO_MSG);
            }
            else {
                //place the server call and adjust the message on screen based on the response
                enterpriseDetailsService.UpdateHierarchy(updateInput)
                .success(function (result) {
                    var popStatusMessage = '';

                    if (result[0].o_outputMessage == ENT_DETAIL_CRUD.HIERARCHY_ADD_VERIFICATION_MSG) {
                        if (actionType == 'Insert')
                            popStatusMessage = ENT_DETAIL_CRUD.HIERARCHY_ADD_SUCCESS_MSG + result[0].o_transaction_key + '.';
                        else if (actionType == 'Delete')
                            popStatusMessage = ENT_DETAIL_CRUD.HIERARCHY_DEL_SUCCESS_MSG + result[0].o_transaction_key + '.';

                        enterpriseDetailsService.GetEntOrgHierarchyDetails($scope.selectedEntOrg)
                        .success(function (result) {
                            $scope.entOrgHierarchyGridOptions.data = EnterpriseAccountDataService.filterInactiveRecords(result, "row_stat_cd");
                            var level = 0;
                            var rowTemplateHier = '';
                            if (!angular.isUndefined($scope.entOrgHierarchyGridOptions.data) && $scope.entOrgHierarchyGridOptions.data.length > 0) {
                                angular.forEach($scope.entOrgHierarchyGridOptions.data, function (agg, key) {
                                    if (level < 5 && !angular.isUndefined(agg.lvl5_ent_org_id) && agg.lvl5_ent_org_id != null && agg.lvl5_ent_org_id != '') {
                                        level = 5;
                                        rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 3 && row.treeNode.aggregations[3].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl5_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                    }
                                    else if (level < 4 && !angular.isUndefined(agg.lvl4_ent_org_id) && agg.lvl4_ent_org_id != null && agg.lvl4_ent_org_id != '') {
                                        level = 4;
                                        rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 2 && row.treeNode.aggregations[2].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl4_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                    }
                                    else if (level < 3 && !angular.isUndefined(agg.lvl3_ent_org_id) && agg.lvl3_ent_org_id != null && agg.lvl3_ent_org_id != '') {
                                        level = 3;
                                        rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (row.treeLevel == 1 && row.treeNode.aggregations[1].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl3_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                    }
                                    else if (level < 2 && !angular.isUndefined(agg.lvl2_ent_org_id) && agg.lvl2_ent_org_id != null && agg.lvl2_ent_org_id != '') {
                                        level = 2;
                                        rowTemplateHier = '<div ng-class="{\'pinkClass\': (row.treeLevel == 0 && row.treeNode.aggregations[0].groupVal == \'' + $stateParams.ent_org_id + '\') || (!row.groupHeader && row.entity.lvl2_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                    }
                                    else if (level < 1 && !angular.isUndefined(agg.lvl1_ent_org_id) && agg.lvl1_ent_org_id != null && agg.lvl1_ent_org_id != '') {
                                        level = 1;
                                        rowTemplateHier = '<div ng-class="{\'pinkClass\': (!row.groupHeader && row.entity.lvl1_ent_org_id == \'' + $stateParams.ent_org_id + '\')}"> <div ng-repeat="col in colContainer.renderedColumns track by col.colDef.name"  class="ui-grid-cell" ui-grid-cell></div></div>';
                                    }

                                });
                            }
                            $scope.entOrgHierarchyGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsEntOrgHierarchyGridDef($scope, level); //Column Definition
                            //$scope.entOrgHierarchyGridOptions.rowTemplate = rowTemplateHier;

                            EnterpriseAccountDataService.setDetails(result, "EntOrgHierarchy");
                            $scope.entOrgHierarchyGridOptions.totalItems = $scope.entOrgHierarchyGridOptions.data.length;
                            //Variables used for paginating the search results
                            $scope.totalEntOrgHierarchyItems = $scope.entOrgHierarchyGridOptions.totalItems;
                            $scope.entOrgHierarchyPaginationCurrentPage = $scope.entOrgHierarchyGridOptions.paginationCurrentPage;
                            $scope.toggleEntOrgHierarchyResults = true;
                            $scope.toggleEntOrgHierarchyDBError = false;
                            $scope.isEntOrgHierarchyInactiveHidden = false;
                            $scope.isEntOrgHierarchyFilterFlag = false;
                            $scope.isEntOrgHierarchyShowDetailsFlag = false;
                            $scope.entOrgHierarchyGridOptions.paginationPageSize = 10000;
                            $scope.hideProcessingOverlay('EntOrgHierarchy');
                        })
                        .error(function (res) {
                            errorPopups(res);
                            $scope.hideProcessingOverlay('EntOrgHierarchy');
                        });

                        //Refresh the transaction history section
                        $scope.historyRefresh();
                    }
                    else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.HIERARCHY_INVALID_SUP_VERIFICATION_MSG) {
                        $scope.hideProcessingOverlay("EntOrgHierarchy");
                        MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.HIERARCHY_INVALID_SUP_MSG);
                    }
                    else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.HIERARCHY_INVALID_SUB_VERIFICATION_MSG) {
                        $scope.hideProcessingOverlay("EntOrgHierarchy");
                        MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.HIERARCHY_INVALID_SUB_MSG);
                    }
                    else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.HIERARCHY_DUP_VERIFICATION_MSG) {
                        $scope.hideProcessingOverlay("EntOrgHierarchy");
                        MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.HIERARCHY_DUP_MSG);
                    }
                    else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.HIERARCHY_MUL_PARENT_VERIFICATION_MSG) {
                        $scope.hideProcessingOverlay("EntOrgHierarchy");
                        MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.HIERARCHY_MUL_PARENT_MSG);
                    }
                    else if (result[0].o_outputMessage == ENT_DETAIL_CRUD.HIERARCHY_LVL_VERIFICATION_MSG) {
                        $scope.hideProcessingOverlay("EntOrgHierarchy");
                        MessagePopup($rootScope, popUpHeaderMessage, ENT_DETAIL_CRUD.HIERARCHY_LVL_MSG);
                    }
                    else {
                        $scope.hideProcessingOverlay("EntOrgHierarchy");
                        MessagePopup($rootScope, ENT_DETAIL_CRUD.DB_ERROR_CONFIRM, ENT_DETAIL_CRUD.DB_ERROR_MESSAGE);
                    }
                })
                .error(function (result) {
                    errorPopups(result);
                    $scope.hideProcessingOverlay("EntOrgHierarchy");
                });
            }
        }

        /* ************* Transaction History section refresh ************* */
        $scope.historyRefresh = function () {
            enterpriseDetailsService.GetTransactionHistoryDetails($scope.selectedEntOrg)
                    .success(function (result) {
                        $scope.transactionHistoryGridOptions.data = result;
                        EnterpriseAccountDataService.setDetails(result, "TransactionHistory");
                        $scope.transactionHistoryGridOptions.totalItems = $scope.transactionHistoryGridOptions.data.length;
                        //Variables used for paginating the search results
                        $scope.totalTransactionHistoryItems = $scope.transactionHistoryGridOptions.totalItems;
                        $scope.transactionHistoryPaginationCurrentPage = $scope.transactionHistoryGridOptions.paginationCurrentPage;
                        $scope.toggleTransactionHistoryResults = true;
                        $scope.toggleTransactionHistoryDBError = false;
                        $scope.isTransactionHistoryInactiveHidden = false;
                        $scope.isTransactionHistoryFilterFlag = false;
                        $scope.isTransactionHistoryShowDetailsFlag = false;
                        $scope.hideProcessingOverlay("TransactionHistory");
                    })
                    .error(function (res) {
                        errorPopups(res);
                        //$scope.toggleTransactionHistoryDBError = true;
                        $scope.hideProcessingOverlay("TransactionHistory");
                    });
        }

        $scope.cancel = function () {
            angular.element(editEnterpriseOrgsPopup).modal('hide');
            // $uibModalInstance.dismiss('cancel');
        };

        $scope.editNaicsCdNewAccount = function (sectionName) {
            var hdrlabel = 'Approve/Reject/Add - ' + getLavelText(sectionName);
            MessagePopup($rootScope, hdrlabel, ENT_ACC.ENT_ORG_CURRENTLY_CONSTRUCTION);
            //$scope.showProcessingOverlay('NaicsCodeStew');
            ////Method to get the recent searches on load of the application
            //service.getRecentNAICSUpdate().success(function (result) {
            //    $rootScope.NewAccountRecentNAICSUpdate = result;
            //}).error(function (result) {
            //    errorPopups(result);
            //});
            //$rootScope.naicsApprovedCodes = [];
            //$rootScope.naicsRejectedCodes = [];
            //$rootScope.naicsNewlyAddedCodes = [];

            //$rootScope.isNaicsSuggested = false;
            //$rootScope.selectedAction = "NA";

            ////Customize the tree on load
            //$rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
            //$rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children
            //$rootScope.gridApiNAICSAdd.grid.clearAllFilters(); //Clear all the filters

            //$rootScope.edit_naics_cd_input = {};
            //$rootScope.edit_naics_cd_input.cnst_mstr_id = !angular.isUndefined(row.master_id) ? row.master_id : "";
            //$rootScope.edit_naics_cd_input.source_system_id = !angular.isUndefined(row.source_system_id) ? row.source_system_id : "";
            //$rootScope.edit_naics_cd_input.source_system_code = !angular.isUndefined(row.source_system_code) ? row.source_system_code : "";
            //$rootScope.edit_naics_cd_input.cnst_org_nm = !angular.isUndefined(row.name) ? row.name : "";
            //$rootScope.edit_naics_cd_input.address = !angular.isUndefined(row.address) ? row.address : "";

            //if (angular.isDefined(row.listNAICSDesc) && row.listNAICSDesc != null && row.listNAICSDesc.length > 0) {
            //    $rootScope.isNaicsSuggested = true;
            //    //Get the NAICS Master details from the server
            //    service.getMasterNAICSDetails($rootScope.edit_naics_cd_input)
            //        .success(function (result) {
            //            $rootScope.naicsEditGridOption.data = result;
            //            $rootScope.naicsEditGridOption.totalItems = $scope.naicsEditGridOption.data.length;

            //            $scope.hideProcessingOverlay('NaicsCodeStew');

            //            //Open the Edit Modal pop-up
            //            angular.element(editNAICSCodePopup).modal();
            //        })
            //        .error(function (result) {
            //            errorPopups(result);
            //            $scope.hideProcessingOverlay('NaicsCodeStew');
            //        });
            //}
            //else {
            //    $rootScope.isNaicsSuggested = false;
            //    $scope.hideProcessingOverlay('NaicsCodeStew');

            //    //Open the Edit Modal pop-up
            //    angular.element(editNAICSCodePopup).modal();
            //}
        }

        $scope.exportDetails = function()
        {
            var hdrlabel = 'Export Details';
            MessagePopup($rootScope, hdrlabel, ENT_ACC.ENT_ORG_CURRENTLY_CONSTRUCTION);
        }

        $scope.loadAllDetails = function (sectionName) {
            /************************* Details - Affiliated Master Details *************************/
            //clearDetailsForMultiSelection();
            if (sectionName == "MasterBridgeLocation") {
                $scope.showProcessingOverlay(sectionName);
                $scope.toggleMasterBridgeLocationResults = false;
                $scope.toggleMasterBridgeLocationDBError = false;
                $scope.masterBridgeLocationGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsMasterBridgeLocationGridDef($scope); //Column Definition

                var serviceInput = {};
                serviceInput.ent_org_id = $scope.selectedEntOrg;
                serviceInput.AffiliationLimit = "0";
                serviceInput.strLoadType = "all";

                enterpriseDetailsService.GetMasterBridgeDetails(serviceInput)
				.success(function (result) {
				    $scope.masterBridgeLocationGridOptions.data = result.lt_affil_res;
				    $scope.masterBridgeLocationGridOptions.totalItems = $scope.masterBridgeLocationGridOptions.data.length;
				    $scope.masterBridgeLocationGridOptions.paginationPageSize = 40000;
				    //Variables used for paginating the search results
				    $scope.toggleMasterBridgeLocationResults = true;
				    $scope.toggleMasterBridgeLocationDBError = false;
				    $scope.isMasterBridgeLocationInactiveHidden = false;
				    $scope.isMasterBridgeLocationFilterFlag = false;
				    $scope.isMasterBridgeLocationShowDetailsFlag = false;

				    $scope.hideProcessingOverlay(sectionName);
				})
				.error(function (res) {
				    $scope.toggleMasterBridgeLocationDBError = true;
				    $scope.hideProcessingOverlay(sectionName);
				});
            }

            /************************* Details - Bridge Records *************************/
            if (sectionName == "MasterBridgeMaster") {
                $scope.showProcessingOverlay(sectionName);
                $scope.toggleMasterBridgeMasterResults = false;
                $scope.toggleMasterBridgeMasterDBError = false;
                $scope.masterBridgeMasterGridOptions.columnDefs = EnterpriseAccountDataService.getDetailsMasterBridgeMasterGridDef($scope); //Column Definition

                var serviceInput = {};
                serviceInput.ent_org_id = $scope.selectedEntOrg;
                serviceInput.AffiliationLimit = "0";
                serviceInput.strLoadType = "all";

                enterpriseDetailsService.GetMasterBridgeDetails(serviceInput)
				.success(function (result) {
				    $scope.masterBridgeMasterGridOptions.data = result.lt_affil_res;
				    $scope.masterBridgeMasterGridOptions.totalItems = $scope.masterBridgeMasterGridOptions.data.length;
				    //Variables used for paginating the search results
				    $scope.totalMasterBridgeMasterItems = $scope.masterBridgeMasterGridOptions.totalItems;
				    $scope.masterBridgeMasterPaginationCurrentPage = $scope.masterBridgeMasterGridOptions.paginationCurrentPage;
				    $scope.masterBridgeMasterGridOptions.paginationPageSize = 40000;
				    $scope.toggleMasterBridgeMasterResults = true;
				    $scope.toggleMasterBridgeMasterDBError = false;
				    $scope.isMasterBridgeMasterInactiveHidden = false;
				    $scope.isMasterBridgeMasterFilterFlag = false;
				    $scope.isMasterBridgeMasterShowDetailsFlag = false;

				    $scope.hideProcessingOverlay(sectionName);
				})
				.error(function (res) {
				    $scope.toggleMasterBridgeMasterDBError = true;
				    $scope.hideProcessingOverlay(sectionName);
				});
            }
        }
        /************************* Details Export *************************/
        $scope.exportDetailsData = function (sectionName) {
            MessagePopup($rootScope, "Export " + getLavelText(sectionName) + " Results", " Download has begun and will complete shortly ...");
            exportService.getDetailsExportData({ strEntOrgId: $scope.selectedEntOrg, strSectionName: sectionName });
        }

        /************************* Pop-ups *************************/
        function errorPopups(result) {
            if (result == GEN_CONSTANTS.ACCESS_DENIED) {
                MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE);
                angular.element(editEnterpriseOrgsPopup).modal('hide');
            }
            else if (result == GEN_CONSTANTS.DB_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.DB_ERROR_CONFIRM, GEN_CONSTANTS.DB_ERROR_MESSAGE);
            }
            else if (result == GEN_CONSTANTS.TIMEOUT_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.TIMEOUT_ERROR_CONFIRM, GEN_CONSTANTS.TIMEOUT_ERROR_MESSAGE);
            }
        }

        function MessagePopup($rootScope, headerText, bodyText) {
            $rootScope.enterpriseAccountModalPopupHeaderText = headerText;
            $rootScope.enterpriseAccountModalPopupBodyText = bodyText;
            angular.element(enterpriseAccountMessageDialogBox).modal({ backdrop: "static" });
        }

        $scope.look = function(row)
        {
            debugger;
        }

        $scope.validatedSelectedEO = function (compared_eo_id) {
            if ($stateParams.ent_org_id == compared_eo_id)
                return true;
        }

    // from now onwards please add new functionalities in the extended section
        var extendedMultiFuncInitialize = function () {
            //extended functionalities
            $scope.multi = {
                EXTENDED_BASE_URL: BasePath + 'App/EnterpriseAccount/Views/Extended',
                CONSTANTS: {
                    CHARACTERISTICS: EXTENDED_FUNC.CHARACTERISTICS
                },
                toggleCharacteristics: false,
                toggleExtendedAddButton: true
            }

            //clear the store data before initializing 
            ExtendedStoreData.cleanData();
            if (ExtendedServices.getTabDenyPermission()) {
                toggleExtendedAddButton: false;
            }
        }
        extendedMultiFuncInitialize();

        $scope.multi.toggleSection = function (type) {
            //console.log(type);
            switch (type) {
                case EXTENDED_FUNC.CHARACTERISTICS: {
                    $scope.multi.characteristicsTemplate = $scope.multi.EXTENDED_BASE_URL + '/Characteristics.tpl.html';
                    $scope.$broadcast('characteristics', { 'characteristics': $scope.multi.toggleCharacteristics, 'page_size':5 });
                     //console.log("Firing" + type);
                    break;
                }
            }
        }



        // this section is for home section
        // from now onwards please add new functionalities in the extended section
        var extendedHomeFuncInitialize = function () {
            //extended functionalities
            $scope.home = {
                EXTENDED_BASE_URL: BasePath + 'App/EnterpriseAccount/Views/Extended',
                CONSTANTS: {
                    CHARACTERISTICS: EXTENDED_FUNC.CHARACTERISTICS
                },
                toggleCharacteristics: false,
                toggleExtendedAddButton: true
            }

            //clear the store data before initializing 
            ExtendedStoreData.cleanData();
            if (ExtendedServices.getTabDenyPermission()) {
                toggleExtendedAddButton: false;
            }
        }
        extendedHomeFuncInitialize();

        $scope.home.toggleSection = function (type) {
            //console.log(type);
            switch (type) {
                case EXTENDED_FUNC.CHARACTERISTICS: {
                    $scope.home.characteristicsTemplate = $scope.home.EXTENDED_BASE_URL + '/Characteristics.tpl.html';
                    $scope.$broadcast('characteristics', { 'characteristics': $scope.home.toggleCharacteristics, 'page_size':10 });
                    //console.log("Firing" + type);
                    break;
                }
            }
        }
      
    }
]);
