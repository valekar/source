//Search Results controller - New Accounts
topAccMod.controller("topAccountSearchResultController", ['$scope', '$http', '$rootScope', '$uibModal', '$location', '$timeout', 'uiGridConstants', '$state', 'TopAccountService', 'TopAccountDataService', 'TopAccountHelperService', 'uiGridTreeViewConstants', 'FileUploadService','$window', '$stateParams', 'constituentCRUDoperations', 'mainService', '$localStorage',

    function ($scope, $http, $rootScope, $uibModal, $location, $timeout, uiGridConstants, $state, service, dataService, helperService, uiGridTreeViewConstants, FileUploadService,$window, $stateParams, constituentCRUDoperations, mainService, $localStorage) {

        /************************* Constants *************************/
        var BasePath = $("base").first().attr("href");
        $scope.processingOverlaySource = BasePath + "Images/Loading.gif";
        $rootScope.addNAICSCodeTemplate = BasePath + "App/Shared/Views/Common/AddNAICSCode.tpl.html";
        $rootScope.editNAICSCodeTemplate = BasePath + "App/Shared/Views/Common/EditNAICSCode.tpl.html";
        $rootScope.potentialMergeTemplate = BasePath + "App/Shared/Views/Common/PotentialMergeResearch.tpl.html";
        $rootScope.potentialUnmergeTemplate = BasePath + "App/Shared/Views/Common/PotentialUnMergeAddress.tpl.html";
        $rootScope.downloadNAICSSuggestionTemplate = BasePath + "App/Shared/Views/Common/DownloadNAICSSuggestion.tpl.html";
        $rootScope.editAddressTemplate = BasePath + "App/Shared/Views/Common/EditAddress.tpl.html";
        $rootScope.editNameTemplate = BasePath + "App/Shared/Views/Common/EditName.tpl.html";
        $rootScope.editPhoneTemplate = BasePath + "App/Shared/Views/Common/EditPhone.tpl.html";
        $rootScope.uploadNAICSSuggestionTemplate = BasePath + "App/Shared/Views/Common/UploadNAICSSuggestions.tpl.html";       
        //Method to store the Tab level security details

        service.getUserPermissions().success(function (result) {
            if (result != null) {
                $rootScope.groupName = result.grp_nm;
            }
        }).error(function (result) {
            errorPopups(result);
            $scope.hidePleaseWait();
        });
        $scope.getImagePath = function (action) {
            if ($rootScope.groupName == 'Orgler') {
                if (action == 'edit') {
                    return BasePath + "Images/ResearchButton.png";
                }
                if (action == 'add') {
                    return BasePath + "Images/ResearchButton.png";
                }
                if (action == 'approve') {
                    return BasePath + "Images/ApproveButton.png";
                }
                if (action == 'reject') {
                    return BasePath + "Images/RejectButton.png";
                }
                if (action == 'confirm') {
                    return BasePath + "Images/ConfirmButton.png";
                }
                if (action == 'research') {
                    return BasePath + "Images/ResearchButton.png";
                }
                if (action == 'new') {
                    return BasePath + "Images/new.png";
                }
                if (action == 'view') {
                    return BasePath + "Images/ViewButton.png";
                }

            }
            else {
                if (action == 'edit') {
                    return BasePath + "Images/EditAdd.png";
                }
                if (action == 'add') {
                    return BasePath + "Images/ResearchButton.png";
                }
                if (action == 'approve') {
                    return BasePath + "Images/ApproveButton.png";
                }
                if (action == 'reject') {
                    return BasePath + "Images/RejectButton.png";
                }
                if (action == 'confirm') {
                    return BasePath + "Images/ConfirmButton.png";
                }
                if (action == 'research') {
                    return BasePath + "Images/ResearchButton.png";
                }
                if (action == 'new') {
                    return BasePath + "Images/new.png";
                }
                if (action == 'view') {
                    return BasePath + "Images/ViewButton.png";
                }
            }

        }
        $rootScope.addButtonPath = BasePath + "Images/AddButton.png";
        $rootScope.approveButtonPath = BasePath + "Images/ApproveButton.png";
        $rootScope.rejectButtonPath = BasePath + "Images/RejectButton.png";
        $rootScope.downloadButtonPath = BasePath + "Images/download.jpg";

        /************************* Generic Methods *************************/
        //Method to hide the processing overlay
        $scope.hidePleaseWait = function () {
            $scope.processingOverlay = false;
        }

        //Method to show the processing overlay
        $scope.showPleaseWaitText = function () {
            $scope.processingOverlay = true;
        }
        
        $scope.hidePleaseWait();

        /************************* Defining the Search Results Grid *************************/
        //Variables used for paginating the search results
        var searchInput = {};
        searchInput = dataService.getTopAccountSearchInput();
        var searchResults = {};
        searchResults = dataService.getTopAccountSearchResults();
        
        if ($window.localStorage['Main-TopAccountSearchResults'] != null) {
            // constituentDataServices.setSearchResutlsData($localStorage.QuickSearchResultsData);
            //$localStorage.QuickSearchResultsData = null;
            dataService.setTopAccountSearchResults(JSON.parse($window.localStorage['Main-TopAccountSearchResults']));
            $window.localStorage.removeItem('Main-TopAccountSearchResults');
        }
        if (searchResults != null) {
            if (!angular.isUndefined(searchResults.objSearchResults)) {
                $localStorage._previousResults = searchResults;
                $localStorage._previousInput = searchInput;
            }

        }
        if ($localStorage._previousResults != null) {

            searchResults = $localStorage._previousResults;
            searchInput = $localStorage._previousInput;
            //$scope.toggleShowBIOResults = true;
            //$scope.toggleShowFRResults = true;
            //$scope.toggleShowPHSSResults = true;
            //$scope.showIndLOSCounts = true;
            if (!angular.isUndefined(searchInput.los)) {
                if (searchInput.los == "All") {
                    $scope.toggleShowBIOResults = true;
                    $scope.toggleShowFRResults = true;
                    $scope.toggleShowPHSSResults = true;

                    $scope.showIndLOSCounts = true;
                }
                else {
                    if (searchInput.los == "BIO") {
                        $scope.toggleShowBIOResults = true;
                        $scope.toggleShowFRResults = false;
                        $scope.toggleShowPHSSResults = false;
                    }
                    if (searchInput.los == "FR") {
                        $scope.toggleShowBIOResults = false;
                        $scope.toggleShowFRResults = true;
                        $scope.toggleShowPHSSResults = false;
                    }
                    if (searchInput.los == "PHSS") {
                        $scope.toggleShowBIOResults = false;
                        $scope.toggleShowFRResults = false;
                        $scope.toggleShowPHSSResults = true;
                    }
                    $scope.showIndLOSCounts = false;
                    $scope.searchCriteria = $scope.searchCriteria + " under '" + searchInput.los + "' LOB ";
                }
            }
        }
        //Control the visibility of the LOS panels
       
        $scope.showIndLOSCounts = true;     
        if (!angular.isUndefined(searchInput)) { 
            if (!angular.isUndefined(searchInput.rfm_scr)) {
                $scope.searchCriteria = "";
                if (searchInput.rfm_scr != "") {
                    $scope.searchCriteria =" with RFM Score '" + searchInput.rfm_scr + "'";
                }
            }
            if (!angular.isUndefined(searchInput.listNaicsCodes)) {
                if (searchInput.listNaicsCodes != "") {
                    $scope.searchCriteria =  $scope.searchCriteria+ " under  NAICS Codes '" + searchInput.listNaicsCodes+"'";
                   
                }
            }
            if (!angular.isUndefined(searchInput.naicsStatus)) {
                if (searchInput.naicsStatus != "All") {
                    $scope.searchCriteria = $scope.searchCriteria + " with '" + searchInput.naicsStatus + "' status ";
                }
            }
            if (!angular.isUndefined(searchInput.enterpriseOrgAssociation)) {
                if (searchInput.enterpriseOrgAssociation == "Enterprise Org found")
                    $scope.searchCriteria = $scope.searchCriteria + " associated with enterprise organizations";
                if (searchInput.enterpriseOrgAssociation == "No Enterprise Org")
                    $scope.searchCriteria = $scope.searchCriteria + " not associated with enterprise organizations";
                if (searchInput.enterpriseOrgAssociation == "Premier Enterprise Org found")
                    $scope.searchCriteria = $scope.searchCriteria + " associated with premier enterprise organizations";
            }

        }

        if (!angular.isUndefined(searchInput))
        {
            if (!angular.isUndefined(searchInput.los)) {
                if (searchInput.los == "All") {
                    $scope.toggleShowBIOResults = true;
                    $scope.toggleShowFRResults = true;
                    $scope.toggleShowPHSSResults = true;

                    $scope.showIndLOSCounts = true;
                }
                else {
                    if (searchInput.los == "BIO") {
                        $scope.toggleShowBIOResults = true;
                        $scope.toggleShowFRResults = false;
                        $scope.toggleShowPHSSResults = false;
                    }
                    if (searchInput.los == "FR") {
                        $scope.toggleShowBIOResults = false;
                        $scope.toggleShowFRResults = true;
                        $scope.toggleShowPHSSResults = false;
                    }
                    if (searchInput.los == "PHSS") {
                        $scope.toggleShowBIOResults = false;
                        $scope.toggleShowFRResults = false;
                        $scope.toggleShowPHSSResults = true;
                    }
                    $scope.showIndLOSCounts = false;
                    $scope.searchCriteria = $scope.searchCriteria + " under '" + searchInput.los + "' LOB ";
                }
            }
        }

        //Edit Cell column Definition
        $scope.indColDef = [
            { field: 'strText', width: "*", cellTemplate: '<div class="innergridwordwrap">{{COL_FIELD}}</div>' },
            { field: 'strText', width: "*", cellTemplate: '<img src="../../../Images/Edit.jpg" style="width:20px; height:20px;" ng-click="grid.appScope.editNewAccount(row.entity, grid)" />' }
        ];
        
        /************************* Binding Grid data *************************/
        //Assigning Data to the BIO grid
        $scope.searchResultsBIOGridOptions = helperService.getGridOptions();
        $scope.searchResultsBIOGridOptions.columnDefs = helperService.getTopAccountSearchResultsColDef();
      //  $scope.searchResultsBIOGridOptions.columnDefs = helperService.getTopAccountSearchResultsMonetaryFormattedColDef();
        $scope.searchResultsBIOGridOptions.data = searchResults.objBIOSearchResults;
        $scope.searchResultsBIOGridOptions.totalItems = $scope.searchResultsBIOGridOptions.data.length;
        $scope.totalBIOAccountCount = $scope.searchResultsBIOGridOptions.data.length;

        //helperService.getDynamicGridLayout($scope.searchResultsBIOGridOptions, uiGridConstants, searchResults.objBIOSearchResults, 12);

        //Registering the method which will get executed once the grid gets loaded
        $scope.searchResultsBIOGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.gridApiBIO = gridApi;
        }

        //Method which is called to paginate the search results
        $scope.navigationPageChangeBIO = function (page) {
            $scope.gridApiBIO.pagination.seek(page);
        }

        //Assigning Data to the FR grid
        $scope.searchResultsFRGridOptions = helperService.getGridOptions();
        $scope.searchResultsFRGridOptions.columnDefs = helperService.getTopAccountSearchResultsMonetaryFormattedColDef();
        $scope.searchResultsFRGridOptions.data = searchResults.objFRSearchResults;
        $scope.searchResultsFRGridOptions.totalItems = $scope.searchResultsFRGridOptions.data.length;
        $scope.totalFRAccountCount = $scope.searchResultsFRGridOptions.data.length;

        //helperService.getDynamicGridLayout($scope.searchResultsFRGridOptions, uiGridConstants, searchResults.objFRSearchResults, 12);

        //Registering the method which will get executed once the grid gets loaded
        $scope.searchResultsFRGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.gridApiFR = gridApi;
        }

        //Method which is called to paginate the search results
        $scope.navigationPageChangeFR = function (page) {
            $scope.gridApiFR.pagination.seek(page);
        }

        //Assigning Data to the PHSS grid
        $scope.searchResultsPHSSGridOptions = helperService.getGridOptions();
        $scope.searchResultsPHSSGridOptions.columnDefs = helperService.getTopAccountSearchResultsMonetaryFormattedColDef();
        $scope.searchResultsPHSSGridOptions.data = searchResults.objPHSSSearchResults;
        $scope.searchResultsPHSSGridOptions.totalItems = $scope.searchResultsPHSSGridOptions.data.length;
        $scope.totalPHSSAccountCount = $scope.searchResultsPHSSGridOptions.data.length;

        //helperService.getDynamicGridLayout($scope.searchResultsPHSSGridOptions, uiGridConstants, searchResults.objPHSSSearchResults, 12);

        //Registering the method which will get executed once the grid gets loaded
        $scope.searchResultsPHSSGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.gridApiPHSS = gridApi;
        }

        //Method which is called to paginate the search results
        $scope.navigationPageChangePHSS = function (page) {
            $scope.gridApiPHSS.pagination.seek(page);
        }

        $scope.paginationPageSize = $scope.searchResultsBIOGridOptions.paginationPageSize;
        
        if (searchResults.objSearchResults.length >= 500) {
           // $scope.totalAccountCount = searchResults.objSearchResults.length + "+";
            $scope.totalAccountCount = $scope.searchResultsFRGridOptions.totalItems + $scope.searchResultsPHSSGridOptions.totalItems + $scope.searchResultsBIOGridOptions.totalItems + "+";
        }
        else
        {
            $scope.totalAccountCount = $scope.searchResultsFRGridOptions.totalItems + $scope.searchResultsPHSSGridOptions.totalItems + $scope.searchResultsBIOGridOptions.totalItems;
        }
        $scope.paginationBIOCurrentPage = 1;
        $scope.paginationFRCurrentPage = 1;
        $scope.paginationPHSSCurrentPage = 1;

        /************************* Search Results Behaviours - Add *************************/
        //Add methods
        $scope.addNameNewAccount = function(row, grid) {
            MessagePopup($rootScope, "Add New Name", "Currently, this functionality is in the development stage.");
        }
        $scope.addAddressNewAccount = function (row, grid) {
            MessagePopup($rootScope, "Add New Address", "Currently, this functionality is in the development stage.");
        }
        $scope.addPhoneNewAccount = function (row, grid) {
            MessagePopup($rootScope, "Add New Phone Number", "Currently, this functionality is in the development stage.");
        }
        $scope.addEntOrgIdNewAccount = function (row, grid) {
            MessagePopup($rootScope, "Add New Enterprise Org ID", "Currently, this functionality is in the development stage.");
        }

        /************************* Search Results Behaviours - Edit *************************/
        $rootScope.constAddressGridOptions = helperService.getDetailsPopupGridOptions();
        $rootScope.constAddressGridOptions.columnDefs = helperService.getAddressColDefs(uiGridConstants);

        $rootScope.constAddressPotentialUnmergeGridOptions = helperService.getDetailsPopupGridOptions();
        $rootScope.constAddressPotentialUnmergeGridOptions.columnDefs = helperService.getAddressPotentialUnmergeColDefs(uiGridConstants);

        $rootScope.constNameGridOptions = helperService.getDetailsPopupGridOptions();
        $rootScope.constNameGridOptions.columnDefs = helperService.getNameColDefs(uiGridConstants);

        $rootScope.constPhoneGridOptions = helperService.getDetailsPopupGridOptions();
        $rootScope.constPhoneGridOptions.columnDefs = helperService.getPhoneColDefs(uiGridConstants);

        //Edit methods - To open the pop-up
        $scope.editNameNewAccount = function (row, grid) {
            $scope.showPleaseWaitText();

            $scope.postData = {};
            $scope.postData.cnst_mstr_id = row.master_id;
            $scope.postData.cnst_srcsys_id = row.source_system_id;
            $scope.postData.arc_srcsys_cd = row.source_system_code;

            service.getStuartDetails($scope.postData, "name").success(function (result) {

                $rootScope.constNameGridOptions.data = result;
                $rootScope.constNameGridOptions.totalItems = $scope.constNameGridOptions.data.length;

                angular.element(editNamePopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }

        $scope.editAddressNewAccount = function (row, grid) {
            $scope.showPleaseWaitText();

            $scope.postData = {};
            $scope.postData.cnst_mstr_id = row.master_id;
            $scope.postData.cnst_srcsys_id = row.source_system_id;
            $scope.postData.arc_srcsys_cd = row.source_system_code;

            service.getStuartDetails($scope.postData, "address").success(function (result) {

                $rootScope.constAddressGridOptions.data = result;
                $rootScope.constAddressGridOptions.totalItems = $scope.constAddressGridOptions.data.length;

                angular.element(editAddressPopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }
        //Added for Potential Unmerges Address details
        $scope.potentialUnmergesAddressNewAccount = function (row, grid) {
            $scope.showPleaseWaitText();

            $scope.postData = {};
            $scope.postData.cnst_mstr_id = row.master_id;
            $scope.postData.cnst_srcsys_id = row.source_system_id;
            $scope.postData.arc_srcsys_cd = row.source_system_code;

            service.getStuartDetails($scope.postData, "address").success(function (result) {

                $rootScope.constAddressPotentialUnmergeGridOptions.data = result;
                $rootScope.constAddressPotentialUnmergeGridOptions.totalItems = $scope.constAddressPotentialUnmergeGridOptions.data.length;
                $rootScope.accountName = row.name;

                angular.element(potentialUnmergesPopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }
        $rootScope.addToCartPotUnmergeNewAccount = function () {
            MessagePopup($rootScope, "Add to Cart", "Currently, this functionality is in the development stage.");
        }
        $scope.editPhoneNewAccount = function (row, grid) {
            $scope.showPleaseWaitText();

            $scope.postData = {};
            $scope.postData.cnst_mstr_id = row.master_id;
            $scope.postData.cnst_srcsys_id = row.source_system_id;
            $scope.postData.arc_srcsys_cd = row.source_system_code;

            service.getStuartDetails($scope.postData, "phone").success(function (result) {

                $rootScope.constPhoneGridOptions.data = result;
                $rootScope.constPhoneGridOptions.totalItems = $scope.constPhoneGridOptions.data.length;

                angular.element(editPhonePopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }

        $scope.editEntOrgIdNewAccount = function (row, grid) {
            MessagePopup($rootScope, "Edit Enterprise Org ID", "Currently, this functionality is in the development stage.");
        }

        //Stewarding Actions for Name, Address, Email and Contacts
        $scope.editOrgNameGridRow = function (row, grid) {
            $stateParams = row.master_id;
            $rootScope.paramsacc = row.master_id;
            commonDetailGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_ORG_NAME);
        }

        $scope.editOrgAddressGridRow = function (row, grid) {
            $stateParams = row.master_id;
            $rootScope.paramsacc = row.master_id;
            commonDetailGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_ADDRESS);
        }

        $scope.editOrgPhoneGridRow = function (row, grid) {
            $stateParams = row.master_id;
            $rootScope.paramsacc = row.master_id;
            commonDetailGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_PHONE);
        }

        $scope.editOrgEmailGridRow = function (row, grid) {
            $stateParams = row.master_id;
            $rootScope.paramsacc = row.master_id;
            commonDetailGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_EMAIL);
        }
        $scope.editOrgPotentialMergeGridRow = function (row, grid) {
            $stateParams = row.master_id;
            $rootScope.paramsacc = row.master_id;
            $rootScope.pot_merge = {};
            $rootScope.pot_merge.name = !angular.isUndefined(row.name) ? row.name : "";
            $rootScope.pot_merge.addr = !angular.isUndefined(row.address) ? row.address : "";
            $rootScope.pot_merge.phone = !angular.isUndefined(row.phone) ? row.phone : "";
            $rootScope.pot_merge.email = !angular.isUndefined(row.email) ? row.email : "";
            $rootScope.pot_merge.lexis_nexis_id = !angular.isUndefined(row.lexis_nexis_id) ? row.lexis_nexis_id : "";
            $rootScope.pot_merge.msrid = !angular.isUndefined(row.master_id) ? row.master_id : "";
            commonDetailGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_POTENTIALMERGE);
        }
        $scope.editOrgPotentialUnMergeGridRow = function (row, grid) {
            $stateParams = row.master_id;
            $rootScope.paramsacc = row.master_id;
            $rootScope.pot_merge = {};
            $rootScope.pot_merge.name = !angular.isUndefined(row.name) ? row.name : "";
            $rootScope.pot_merge.addr = !angular.isUndefined(row.address) ? row.address : "";
            $rootScope.pot_merge.msrid = !angular.isUndefined(row.master_id) ? row.master_id : "";
            commonDetailGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_EXT_BRIDGE);
        }

        /************************* Search Results Behaviours - NAICS *************************/
        //Method to open the add pop-up
        $scope.addNaicsCdNewAccount = function (row, grid) {

            $scope.showPleaseWaitText();
           
            //Customize the tree on load
            $rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
            $rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children
            $rootScope.gridApiNAICSAdd.grid.clearAllFilters(); //Clear all the filters

            $rootScope.newNAICSSeletedConsMstrId = row.master_id;
            $rootScope.newNAICSSeletedSrcSysId = row.source_system_id;
            $rootScope.newNAICSSeletedSrcSysCd = row.source_system_code;

            //Open the pop-up
            angular.element(addNAICSCodePopup).modal();

            $scope.hidePleaseWait();
        }

        //Method to submit the add request
        $scope.addNaicsCdNewAccountSubmit = function () {

            $scope.showPleaseWaitText();

            //Hide the pop-up
            angular.element(addNAICSCodePopup).modal('hide');

            var listSelectedNAICSCodes = [];

            //Refresh the grid with updated information
            angular.forEach($rootScope.gridApiNAICSAdd.selection.getSelectedRows(), function (value, key) {
                listSelectedNAICSCodes.push(value.naics_cd);
            });

            var postData = {
                cnst_mstr_id: $rootScope.newNAICSSeletedConsMstrId,
                source_system_id: $rootScope.newNAICSSeletedSrcSysId,
                source_system_code: $rootScope.newNAICSSeletedSrcSysCd,
                new_naics_codes: listSelectedNAICSCodes
            };

            //Send the newly added NAICS codes to the client service
            service.submitNewNAICSCodes(postData).success(function (result) {

                    //Transaction status pop-up
                    if (result[0].o_message == "Success") {
                        //Refresh the search results section
                        //Preset the Search inputs on load
                        var SearchInput = {};
                        SearchInput = dataService.getTopAccountSearchInput();

                        //Invoke the service method to place the search
                        service.getTopAccountSearchResults(SearchInput).success(function (searchRes) {
                            //Refresh all the grids
                            $scope.searchResultsBIOGridOptions.data = searchRes.objBIOSearchResults;
                            $scope.searchResultsFRGridOptions.data = searchRes.objFRSearchResults;
                            $scope.searchResultsPHSSGridOptions.data = searchRes.objPHSSSearchResults;
                            $scope.totalPHSSAccountCount = $scope.searchResultsPHSSGridOptions.data.length;
                            $scope.totalFRAccountCount = $scope.searchResultsFRGridOptions.data.length;
                            $scope.totalBIOAccountCount = $scope.searchResultsBIOGridOptions.data.length;
                            $scope.totalAccountCount = searchRes.objSearchResults.length;

                            MessagePopup($rootScope, "Add New NAICS Code", ACTION_CONSTANTS.MESSAGE_TEXT.SUCCESS_MSG + result[0].trans_id);

                            $scope.hidePleaseWait();
                            
                        })
                        .error(function (result) {
                            errorPopups(result);
                            $scope.hidePleaseWait();
                        });
                    }
                    else
                    {
                        MessagePopup($rootScope, "Add New NAICS Code", ACTION_CONSTANTS.MESSAGE_TEXT.FAILURE_MSG);
                        $scope.showPleaseWaitText();
                    }
                })
                .error(function (result) {
                    errorPopups(result);
                    $scope.hidePleaseWait();
                });
        }

        //Method to open the edit pop-up
        $rootScope.naicsEditGridOption = helperService.getnaicsEditPopupGridOptions();
        $rootScope.naicsEditGridOption.columnDefs = helperService.getnaicsEditColDefs();

        //Registering the method which will get executed once the grid gets loaded
        $scope.naicsEditGridOption.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $rootScope.gridApiEditNAICS = gridApi;
        }

        $rootScope.naicsApprovedCodes = [];
        $rootScope.naicsRejectedCodes = [];
        $rootScope.naicsNewlyAddedCodes = [];

        $scope.editNaicsCdNewAccount = function (row, grid) {
            $scope.showPleaseWaitText();
            //Method to get the recent searches on load of the application
            service.getRecentNAICSUpdate().success(function (result) {
                $rootScope.NewAccountRecentNAICSUpdate = result;
            }).error(function (result) {
                errorPopups(result);
            });
            $rootScope.naicsApprovedCodes = [];
            $rootScope.naicsRejectedCodes = [];
            $rootScope.naicsNewlyAddedCodes = [];

            $rootScope.isNaicsSuggested = false;
            $rootScope.selectedAction = "NA";

            //Customize the tree on load
            $rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
            $rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children
            $rootScope.gridApiNAICSAdd.grid.clearAllFilters(); //Clear all the filters

            $rootScope.edit_naics_cd_input = {};
            $rootScope.edit_naics_cd_input.cnst_mstr_id = !angular.isUndefined(row.master_id) ? row.master_id : "";
            $rootScope.edit_naics_cd_input.source_system_id = !angular.isUndefined(row.source_system_id) ? row.source_system_id : "";
            $rootScope.edit_naics_cd_input.source_system_code = !angular.isUndefined(row.source_system_code) ? row.source_system_code : "";           
            $rootScope.edit_naics_cd_input.cnst_org_nm = !angular.isUndefined(row.name) ? row.name : "";
            $rootScope.edit_naics_cd_input.address = !angular.isUndefined(row.address) ? row.address : "";

            if (angular.isDefined(row.listNAICSDesc) && row.listNAICSDesc != null && row.listNAICSDesc.length > 0)
            {
                $rootScope.isNaicsSuggested = true;
                //Get the NAICS Master details from the server
                service.getMasterNAICSDetails($rootScope.edit_naics_cd_input)
                    .success(function (result) {
                        $rootScope.naicsEditGridOption.data = result;
                        $rootScope.naicsEditGridOption.totalItems = $scope.naicsEditGridOption.data.length;

                        $scope.hidePleaseWait();

                        //Open the Edit Modal pop-up
                        angular.element(editNAICSCodePopup).modal();
                    })
                    .error(function (result) {
                        errorPopups(result);
                        $scope.hidePleaseWait();
                    });
            }
            else
            {
                $rootScope.isNaicsSuggested = false;
                
                $scope.hidePleaseWait();

                //Open the Edit Modal pop-up
                angular.element(editNAICSCodePopup).modal();
            }
        }

        //Method called on click of the Submit button
        $scope.submitNaicsCodeUpdates = function () {
            $scope.showPleaseWaitText();

            //Hide the pop-up
            angular.element(editNAICSCodePopup).modal('hide');

            var listSelectedNAICSCodes = [];
            var listSelectedNAICSTitles = [];
            var listApprovedNAICSCodes = [];
            var listApprovedNAICSTitles = [];
            var listRejectedNAICSCodes = [];
            var listRejectedNAICSTitles = [];

            //Refresh the grid with updated information
            angular.forEach($rootScope.gridApiNAICSAdd.selection.getSelectedRows(), function (value, key) {
                listSelectedNAICSCodes.push(value.naics_cd);
                listSelectedNAICSTitles.push(value.naics_indus_title);
            });

            angular.forEach($rootScope.gridApiEditNAICS.grid.rows, function (value, key) {
                if (value.entity.manual_sts == "Approve") {
                    listApprovedNAICSCodes.push(value.entity.naics_cd);
                    listApprovedNAICSTitles.push(value.entity.naics_indus_title);
                }
                if (value.entity.manual_sts == "Reject") {
                    listRejectedNAICSCodes.push(value.entity.naics_cd);
                    listRejectedNAICSTitles.push(value.entity.naics_indus_title);
                }
            });

            var postData = {
                cnst_mstr_id: $rootScope.edit_naics_cd_input.cnst_mstr_id,
                source_system_id: $rootScope.edit_naics_cd_input.source_system_id,
                source_system_code: $rootScope.edit_naics_cd_input.source_system_code,
                added_naics_codes: listSelectedNAICSCodes,
                approved_naics_codes: listApprovedNAICSCodes,
                rejected_naics_codes: listRejectedNAICSCodes,
                cnst_org_nm: $rootScope.edit_naics_cd_input.cnst_org_nm
            };
            if (postData.added_naics_codes.length == 0 && postData.approved_naics_codes.length == 0 && postData.rejected_naics_codes.length == 0) {
                MessagePopup($rootScope, "NAICS Update", "Please select aleast one stewarding action ");
                $scope.hidePleaseWait();
            }
            else {           
            //Send the newly added NAICS codes to the client service
            service.submitNAICSCodeUpdates(postData).success(function (result) {

                //Transaction status pop-up
                if (result[0].o_message == "Success") {

                    var postDataNaics = {
                        added_naics_codes: listSelectedNAICSCodes,
                        added_naics_titles: listSelectedNAICSTitles,
                        approved_naics_codes: listApprovedNAICSCodes,
                        approved_naics_titles: listApprovedNAICSTitles,
                        rejected_naics_codes: listRejectedNAICSCodes,
                        rejected_naics_titles: listRejectedNAICSTitles
                    };
                    //log the NAICS Codes
                    //To track the recent searches
                    service.logRecentNAICSUpdate(postDataNaics).success(function (result) {
                        //To refresh the recent searches list
                        service.getRecentNAICSUpdate().success(function (result) {
                            $rootScope.NewAccountRecentNAICSUpdate = result;
                        });
                    });
                    //Refresh the search results section
                    //Preset the Search inputs on load
                    var SearchInput = {};
                    SearchInput = dataService.getTopAccountSearchInput();

                    //Invoke the service method to place the search
                    service.getTopAccountSearchResults(SearchInput).success(function (searchRes) {
                        //Refresh all the grids
                        $scope.searchResultsBIOGridOptions.data = searchRes.objBIOSearchResults;
                        $scope.searchResultsFRGridOptions.data = searchRes.objFRSearchResults;
                        $scope.searchResultsPHSSGridOptions.data = searchRes.objPHSSSearchResults;
                        $scope.totalPHSSAccountCount = $scope.searchResultsPHSSGridOptions.data.length;
                        $scope.totalFRAccountCount = $scope.searchResultsFRGridOptions.data.length;
                        $scope.totalBIOAccountCount = $scope.searchResultsBIOGridOptions.data.length;
                        $scope.totalAccountCount = searchRes.objSearchResults.length;

                        MessagePopup($rootScope, "Add New NAICS Code", ACTION_CONSTANTS.MESSAGE_TEXT.SUCCESS_MSG + result[0].trans_id);

                        $scope.hidePleaseWait();

                    })
                    .error(function (result) {
                        errorPopups(result);
                        $scope.hidePleaseWait();
                    });
                }
                else {
                    MessagePopup($rootScope, "Add New NAICS Code", ACTION_CONSTANTS.MESSAGE_TEXT.FAILURE_MSG);
                    $scope.hidePleaseWait();
                }
            })
                .error(function (result) {
                    errorPopups(result);
                    $scope.hidePleaseWait();
                });
            }
        }

        //Method which is called on click of the approve button
        $scope.approveNaicsCode = function (row, grid) {
            //Check if the approved record is present in the reject list, if so remove it off from the reject list
            angular.forEach($rootScope.naicsRejectedCodes, function (value, key) {
                if(value.naics_cd == row.naics_cd) 
                {
                    $rootScope.naicsRejectedCodes.splice(key, 1);
                }
            });

            //Add the row to the approve list
            $rootScope.naicsApprovedCodes.push(row);
        }

        //method to call on click of naics recent entry button
        $scope.naicsRecentSearch = function (obj) {
            var naicsCodeSelectedFromRecentSearch = obj.naics_codes;
            $scope.gridApiNAICSAdd.grid.modifyRows($rootScope.searchGridOptions.data);
            var i = 0;
            var j = 0;
            var parentNaicsCode = "0";
            var currentNodeLevel = 2;
            for (i = 0; i < $rootScope.searchGridOptions.data.length; i++) {
                if (naicsCodeSelectedFromRecentSearch == $rootScope.searchGridOptions.data[i].naics_cd) {
                    $scope.gridApiNAICSAdd.selection.selectRow($rootScope.searchGridOptions.data[i]);
                    if ($rootScope.searchGridOptions.data[i].naics_lvl != "2") {
                        parentNaicsCode = $rootScope.searchGridOptions.data[i].parent_naics_cd;
                        currentNodeLevel = $rootScope.searchGridOptions.data[i].naics_lvl;
                    }
                    break;
                }
            }
            if (parentNaicsCode != "0") {
                var arrayNAICSCodeExpansion = [];
                arrayNAICSCodeExpansion.push(parentNaicsCode);
                var ParentCodeLength = parentNaicsCode.length;
                var tempParentCode = parentNaicsCode;
                while (tempParentCode.length != 2) {
                    tempParentCode = tempParentCode.substr(0, tempParentCode.length - 1);
                    arrayNAICSCodeExpansion.push(tempParentCode);
                }
                for (j = arrayNAICSCodeExpansion.length; j > 0; j--) {
                    for (i = 0; i < $rootScope.searchGridOptions.data.length; i++) {
                        if (arrayNAICSCodeExpansion[j - 1] == $rootScope.searchGridOptions.data[i].naics_cd) {
                            $scope.gridApiNAICSAdd.grid.rows[i].treeNode.state = "expanded";
                        }
                    }
                }
            }
            angular.element(newAccountrecentNAICSUpdate).modal('hide');
        }

        //Method which is called on click of the reject button
        $scope.rejectNaicsCode = function (row, grid) {
            //Check if the rejected record is present in the approve list, if so remove it off from the approve list
            angular.forEach($rootScope.naicsApprovedCodes, function (value, key) {
                if (value.naics_cd == row.naics_cd) {
                    $rootScope.naicsApprovedCodes.splice(key, 1);
                }
            });

            //Add the row to the reject list
            $rootScope.naicsRejectedCodes.push(row);
        }

        //Method which is called on click of the reject button
        $scope.updateNaicsCode = function (row, grid, action) {
            //Check if the rejected record is present in the approve list, if so remove it off from the approve list
            angular.forEach($rootScope.naicsApprovedCodes, function (value, key) {
                if (value.naics_cd == row.naics_cd) {
                    $rootScope.naicsApprovedCodes.splice(key, 1);
                }
            });

            //Add the row to the reject list
            $rootScope.naicsRejectedCodes.push(row);
        }
           
           $scope.getGoogleUrl = function (row, grid) {
      
               var q = row.name + "+" + row.address;

               // window.open("http://www.google.com/search?q=" + row.name + "+" + row.address, '_blank');
               window.open("http://www.google.com/search?q=" + encodeURIComponent(q), '_blank');
         
     }

        //Method to submit the edit request (approve/reject)
        //Method to open the add pop-up on click of the Add button
        $scope.editNaicsCdNewAccountSubmit = function (actionType, edit_naics_cd) {

            $scope.showPleaseWaitText();
            //For add functionality open the Add pop-up
            if (actionType == "Add") {

                //Customize the tree on load
                $rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
                $rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children

                $rootScope.newNAICSSeletedConsMstrId = edit_naics_cd.cnst_mstr_id;
                $rootScope.newNAICSSeletedSrcSysId = edit_naics_cd.source_system_id;
                $rootScope.newNAICSSeletedSrcSysCd = edit_naics_cd.source_system_code;

                angular.element(editNAICSCodePopup).modal('hide');

                //Open the pop-up
                angular.element(addNAICSCodePopup).modal();

                $scope.hidePleaseWait();
            }
            //For approve/reject records, place the request to DB
            else {
                edit_naics_cd.status = actionType;

                //Hide the pop-up
                angular.element(editNAICSCodePopup).modal('hide');
    
                //Send the edited NAICS codes to the client service
                service.submitEditNAICSCodes(edit_naics_cd).success(function (result) {

                    //Transaction status pop-up
                    if (result[0].o_message == "Success") {
                        //Refresh the search results section
                        //Preset the Search inputs on load
                        var SearchInput = {};
                        SearchInput = dataService.getTopAccountSearchInput();

                        //Invoke the service method to place the search
                        service.getTopAccountSearchResults(SearchInput).success(function (searchRes) {
                            //Refresh all the grids
                            $scope.searchResultsBIOGridOptions.data = searchRes.objBIOSearchResults;
                            $scope.searchResultsFRGridOptions.data = searchRes.objFRSearchResults;
                            $scope.searchResultsPHSSGridOptions.data = searchRes.objPHSSSearchResults;
                            $scope.totalPHSSAccountCount = $scope.searchResultsPHSSGridOptions.data.length;
                            $scope.totalFRAccountCount = $scope.searchResultsFRGridOptions.data.length;
                            $scope.totalBIOAccountCount = $scope.searchResultsBIOGridOptions.data.length;
                            $scope.totalAccountCount = searchRes.objSearchResults.length;
                            MessagePopup($rootScope, "Approve/Reject NAICS Code", ACTION_CONSTANTS.MESSAGE_TEXT.SUCCESS_MSG + result[0].trans_id);

                            $scope.hidePleaseWait();

                        })
                        .error(function (result) {
                            errorPopups(result);
                            $scope.hidePleaseWait();
                        });
                    }
                    else {
                        MessagePopup($rootScope, "Approve/Reject NAICS Code", ACTION_CONSTANTS.MESSAGE_TEXT.FAILURE_MSG);
                        $scope.showPleaseWaitText();
                    }
                })
                    .error(function (result) {
                        errorPopups(result);
                        $scope.hidePleaseWait();
                    });
            }
        }

        /************************* Search Results Behaviours - Potential Merge/Unmerge *************************/

        $rootScope.potentialMergeGridOptions = helperService.getPopupGridOptions();
        $rootScope.potentialMergeGridOptions.columnDefs = helperService.getTopAccountPotentialMergeColDef();

        //Registering the method which will get executed once the grid gets loaded
        $scope.potentialMergeGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.potentialMergeGrid = gridApi;
        }

        $scope.getPotentialMergeDetails = function (row, grid) {
            $scope.showPleaseWaitText();

            $scope.postData = { master_id: row.master_id };

            service.getPotentialMergesDetails($scope.postData).success(function (result) {

                $scope.pot_merge = {};
                $scope.pot_merge.name = !angular.isUndefined(row.name) ? row.name : "";
                $scope.pot_merge.addr = !angular.isUndefined(row.address) ? row.address : "";

                $rootScope.potentialMergeGridOptions.data = result;
                $rootScope.potentialMergeGridOptions.totalItems = $scope.potentialMergeGridOptions.data.length;

                angular.element(potentialMergePopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }

        //$scope.getPotentialUnmergeDetails = function (row, grid) {
        //    $scope.showPleaseWaitText();

        //    $scope.pot_unmerge = {};
        //    $scope.pot_unmerge.name = !angular.isUndefined(row.name) ? row.name : "";
        //    $scope.pot_unmerge.srcsys_cd = !angular.isUndefined(row.source_system_code) ? row.source_system_code : "";
        //    $scope.pot_unmerge.srcsys_id = !angular.isUndefined(row.source_system_id) ? row.source_system_id : "";
        //    $scope.pot_unmerge.unmrg_rsn = !angular.isUndefined(row.pot_unmerge_rsn) ? row.pot_unmerge_rsn : "";

        //    angular.element(potentialUnmergePopup).modal();
        //    $scope.hidePleaseWait();
        //}
        //Added for Potential Unmerges Address details
        $scope.getPotentialUnmergeDetails = function (row, grid) {
            $scope.showPleaseWaitText();

            $scope.postData = {};
            $scope.postData.cnst_mstr_id = row.master_id;
            $scope.postData.cnst_srcsys_id = row.source_system_id;
            $scope.postData.arc_srcsys_cd = row.source_system_code;

            service.getStuartDetails($scope.postData, "address").success(function (result) {

                $rootScope.constAddressPotentialUnmergeGridOptions.data = result;
                $rootScope.constAddressPotentialUnmergeGridOptions.totalItems = $scope.constAddressPotentialUnmergeGridOptions.data.length;
                $rootScope.accountName = row.name;

                angular.element(potentialUnmergesPopup).modal();
                $scope.hidePleaseWait();
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }
        $rootScope.addToCartPotUnmergeNewAccount = function () {
            MessagePopup($rootScope, "Add to Cart", "This functionality is under construction.");
        }

        /************************* Search Results Behaviours - Confirm *************************/
        //Confirm methods
        $scope.confirmNewAccount = function (row, grid) {

            $scope.showPleaseWaitText();

            $scope.postData = { master_id: row.master_id };

            //Send the master which we need to confirm to the client service
            service.confirmAccount($scope.postData).success(function (result) {

                //Transaction status pop-up
                if (result[0].o_message == "Success") {
                    //Refresh the search results section
                    //Preset the Search inputs on load
                    var SearchInput = {};
                    SearchInput = dataService.getTopAccountSearchInput();

                    //Invoke the service method to place the search
                    service.getTopAccountSearchResults(SearchInput).success(function (searchRes) {
                        //Refresh all the grids
                        $scope.searchResultsBIOGridOptions.data = searchRes.objBIOSearchResults;
                        $scope.searchResultsFRGridOptions.data = searchRes.objFRSearchResults;
                        $scope.searchResultsPHSSGridOptions.data = searchRes.objPHSSSearchResults;
                        $scope.totalPHSSAccountCount = $scope.searchResultsPHSSGridOptions.data.length;
                        $scope.totalFRAccountCount = $scope.searchResultsFRGridOptions.data.length;
                        $scope.totalBIOAccountCount = $scope.searchResultsBIOGridOptions.data.length;
                        $scope.totalAccountCount = searchRes.objSearchResults.length;

                        MessagePopup($rootScope, "Confirm Account", "Account confirmed successfully. The transaction ID is "+ result[0].trans_id);

                        $scope.hidePleaseWait();

                    })
                    .error(function (result) {
                        errorPopups(result);
                        $scope.hidePleaseWait();
                    });
                }
                else {
                    MessagePopup($rootScope, "Confirm Account", ACTION_CONSTANTS.MESSAGE_TEXT.FAILURE_MSG);
                    $scope.showPleaseWaitText();
                }
            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }

        /************************* Generic Pop-up Definition *************************/
        function errorPopups(result) {
            if (result == GEN_CONSTANTS.ACCESS_DENIED) {
                MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE);
            }
            else if (result == GEN_CONSTANTS.DB_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.DB_ERROR_CONFIRM, GEN_CONSTANTS.DB_ERROR_MESSAGE);
            }
            else if (result == GEN_CONSTANTS.TIMEOUT_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.TIMEOUT_ERROR_CONFIRM, GEN_CONSTANTS.TIMEOUT_ERROR_MESSAGE);
            }
        }

        function MessagePopup($rootScope, headerText, bodyText) {
            $rootScope.newAccountModalPopupHeaderText = headerText;
            $rootScope.newAccountModalPopupBodyText = bodyText;
            angular.element(topAccountMessageDialogBox).modal({ backdrop: "static" });
        }

        /************************* Sample data for NAICS Code Add functionality *************************/
        var gridOption = {
            showTreeExpandNoChildren: false,
            enableSorting: true,
            enablePager: false,
            enableGridMenu: true,
            enableFiltering: true,
            enableVerticalScrollbar: 1,
            enableHorizontalScrollbar: 0,
            enableRowSelection: true,
            multiSelect: true,
            enableSelectAll: false,
            columnDefs: [
              { displayName: 'NAICS Title', field: 'naics_indus_title', width: '*', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000 },
              {
                  displayName: 'NAICS Code',filter: {
                      condition: uiGridConstants.filter.STARTS_WITH,
                      //  placeholder: 'starts with'
                  },sort: {direction: 'asc', priority: 0}, field: 'naics_cd', width: '*',
                 
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000
              }
            ]
        };

        $rootScope.searchGridOptions = gridOption;

        //Registering the method which will get executed once the grid gets loaded
        $rootScope.searchGridOptions.onRegisterApi = function (gridApi) {

            //Store the grid data against a variable so that it can be used for pagination
            $rootScope.gridApiNAICSAdd = gridApi;
        }

        var data = [];

        service.getNAICSCodes()
            .success(function(result)
            {   
                data = result;
                var writeoutNode = function (childArray, currentLevel, dataArray) {
                    childArray.forEach(function (childNode) {
                        childNode.$$treeLevel = currentLevel;
                        dataArray.push(childNode);
                        if (childNode.children.length > 0) {
                            writeoutNode(childNode.children, currentLevel + 1, dataArray);
                        }
                    });
                };

                $rootScope.searchGridOptions.data = [];
                writeoutNode(data, 0, $rootScope.searchGridOptions.data);
            })
            .error(function (result) {
                errorPopups(result);
            });   


        var writeoutNode = function (childArray, currentLevel, dataArray) {
            childArray.forEach(function (childNode) {
                childNode.$$treeLevel = currentLevel;
                dataArray.push(childNode);
                if (childNode.children.length > 0) {
                    writeoutNode(childNode.children, currentLevel + 1, dataArray);
                }
            });
        };


        //Startswith ui select
        $scope.startsWith_naics = function (actual, expected) {
            var lowerStr = (actual + "").toLowerCase();
            return lowerStr.indexOf(expected.toLowerCase()) === 0;
        }

        $rootScope.searchGridOptions.data = [];
        writeoutNode(data, 0, $rootScope.searchGridOptions.data);

        //Registering the method which will get executed once the grid gets loaded
        $rootScope.searchGridOptions.onRegisterApi = function (gridApi) {
            
            //Store the grid data against a variable so that it can be used for pagination
            $rootScope.gridApiNAICSAdd = gridApi;
        }
        //To open Download NAICS suggestions Popup  
        $scope.exportSearchResults = function (type)
        {          
            //Binding the keywords to the string Array
            //$rootScope.keyWordMatch = [" ","ale", "Alfalfa", "ambulance", "Ambulance service", "Ambulatory", "American Indian", "Ammonia", "Amusement", "Analytical", "Analytical instruments", "Animal feeds", "Animal Shelter", "animation", "Antennas", "antique auto", "Apothecaries", "apparel", "appliance", "Aquaculture", "architect", "areas", "arena", "army", "Aromatic", "art", "Art Goods", "artist", "arts", "Asphalt & concrete", "asset", "assistance", "assisted living", "assoc", "athletes", "attorney", "attractions", "Audio", "Auto Body Shop", "auto dealers", "auto supply", "automobile", "Automobile accessories", "automobile dealers", "automotive", "Automotive parts", "automotive wholesalers", "awarness", "baby furniture", "Babysitting services", "baked foods", "bakerie", "Bakery", "Bakery products", "Ballet", "Bands", "bank", "bankers", "banking", "banks", "Bar", "baseball", "baseketball", "bath", "bathroom", "batteries", "bay", "beauty", "Beer", "Beer wholesalers", "belt", "beverage", "binding", "bio mass", "biomass", "bird", "bkstr", "blade", "blades", "Blanket", "blood", "blood bank", "blueprint &", "blueprint service", "blueprinting", "Body shop suplies", "boilers", "book", "book store", "books", "botanical", "Botanical Gardens", "boxer", "boys cloth", "brandy", "Broadcast", "broker", "brokerage", "brokers", "build", "building", "Building Material", "Building Stone", "bulbs", "Bungalow", "Burger", "burial", "Bus merchant", "business management", "business service", "business to business", "butcher", "buttermilk", "cable", "cables", "Cafe", "Camera", "Camping trailer", "camps", "canned foods", "canning", "Capacitors", "capital", "caps", "Carbon Black", "Cardiologists", "care", "care services", "carnival", "carpet", "cartoon", "Casino", "Cast Iron", "Cast iron pipe", "Cattle", "caverns", "Cement", "Cemetery", "centers", "ceramic", "Ceramic materials", "Charitable", "charitable trust", "Charities", "Charity", "Chassis", "cheese", "chemicals", "chewing gum", "Child", "Chiropractor", "chocolate", "choir", "chromatography", "chronic", "church", "Cigarettes", "Cigars", "Cinema", "Circuit breaker", "Circuits", "circus", "civil", "civil engineer", "Civil service", "classical", "cleaning", "Climate", "clinic", "cloth", "clothes", "Club", "clubs", "coat", "Cocktails", "Coins &", "cold storage", "college", "cologne", "coloring", "Comfort", "Comic", "Commercial stationery", "Communicat", "community", "community foundations", "community services", "Companion services", "component", "Compounds", "compressors", "Computer", "computer suppl", "concert", "Concrete Reinforcing", "Confectionery", "congress", "Conservation", "conservator", "construction", "consultants", "consulting", "Contact lens", "contemporary", "Continuing Care", "contractors", "control", "control board", "convalescent", "convent", "cooking equipment", "coolers", "cooling", "cooling & heating", "Cooperation", "corn chips", "Corps", "Correctional", "cosmetic", "Cosmetics", "costume", "Counsel", "Counseling Services", "Counselling services", "Country Clubs", "county trooper", "court", "Courts", "covering", "coverings", "credit", "Credit Card Services", "crops", "crude", "crude oil", "Crude oil merchant", "Cruise", "Crushed stone", "cultivating", "cutlery", "dairy", "dairy products", "Dance", "Data", "Data Steward", "Daycare", "Decorative", "Dental", "Dentist", "Detergents", "Development", "diagnostic", "Dinners", "Directory", "Disable", "Discount", "dispos", "disposal system", "disposer", "distilled", "Distilleries", "donor", "Drafting", "dress", "Drug", "Drug store", "drum", "dry clean", "dry cleaner", "dry good", "Drywall supply", "Dwelling", "dyes", "dyestuffs", "earth", "economic", "Ecosystem", "edible", "edu", "education", "educational", "elderly", "Electric", "electric lighting", "Electric Motor", "Electrical", "electrical distribution", "Electronic", "electronic marketing", "electroplating", "elem", "elementary", "Embassies", "embroidery", "Emergency", "empire", "employment", "Enamels", "engine", "engine part", "engineers", "enterprise", "entertainer", "Envelopes", "equipment", "equit_", "erosion", "essential oils", "Estate", "estates", "ethnic", "Executive", "exhibits", "exploration", "explosives", "Export", "eye bank", "eyeglass", "fabric", "factory", "Fans", "farm", "Farm supplies", "farming", "farms", "fasteners", "federal", "Fertility", "fertiliz", "fertilizers", "festival", "fiberglass", "fibers", "Film", "finance", "financial", "Fine arts", "Fine paper", "Fire", "fire extinguisher", "Fire Protection", "Firearms", "Firefighting Equipment", "first aid", "Fish", "fishing", "Fishing Club", "Fishing clubs", "Fitness", "fittings", "Fixture/Fixtures", "Flat glass", "flight", "Floor Covering", "Floor maint equipment", "FloorCovering", "Flooring", "Florists", "Flour", "Flowers", "Flying club", "Foaster", "Folk", "Food", "food distribution", "Food service", "food stores", "foods store", "football", "Football clubs", "force", "Forensic", "Forest Products", "forestry", "fossil fuel", "foundation", "Foundry Products", "fountain", "Freezer", "fresh", "fresh fish", "fresh seafood", "frozen", "Frozen foods", "fruit", "Fruits", "Fuel oil", "Fun Center", "Fund", "fundrais", "furnace", "furnishing", "furniture", "gambling", "game", "Gaming", "garage", "Garbage", "Garbage disposal", "Garden supplies", "Gardens", "gas", "Gases", "gasoline", "Gasoline equipment", "gauze", "General", "General services", "Generator", "geo thermal", "geograph", "geothermal", "Gifts", "glues", "goats", "golf", "golf carts", "Golf clubs", "Golf Courses", "Gourmet", "government", "Graphics", "Gravel", "Greeting", "groceries", "grocery", "Gun clubs", "hair", "Hand tools", "Hardware", "Harness", "hat", "hats", "Hay", "hazardous", "health", "healthcare", "hearing aid", "heaters", "heating", "heating & cooling", "help", "Herbal", "heritage", "Historical", "HMO", "hobbie", "hockey", "hogs", "Hoists", "holding", "Home care", "home furnishing", "Homeless", "homes", "Honey", "hosiery", "hospital", "Hospital equipment", "Hospitals", "Hosting", "Hotel", "Hotel furntiure", "hotel management", "hotel supply", "household", "Housewares", "Housing", "housing Authority", "Human rights", "hunting", "Hunting clubs", "hydronics", "Hygienist", "Hypnotherapy", "Ice cream", "Import", "independent", "Indoor", "industrial chemicals", "industrial gases", "Infant", "ink Manufacturing", "ink mfg", "Inn", "inpatient", "Institute", "insulated", "Insulated wire", "insulation", "insurance", "Internet", "investigation", "investment", "Janitorial supplies", "Janitorial supply", "jazz", "jewelry", "jockey", "Journal", "Juices", "Justice", "Juvenile", "Kayak", "kennels", "Kitchen Cabinets", "knives", "label", "Laboratory equipment", "landfil", "landfill", "landscape", "Laser Surgery", "Laundry", "law", "law enforcement", "Lawn care", "lawyer", "lawyers", "league", "lease", "leasing", "Legislative", "licensing", "Lime", "Limestone", "lingerie", "liquified gas", "Liquified petroleum gas", "liquor", "Liquors", "literary", "Livestock", "lllp", "loan", "locker", "Locksmith", "Lodge", "logging", "logistics", "Longue", "lumber", "Macaroni", "Mach & Foundry", "machinary", "machine", "Magazine", "Magazines", "Maintenance", "mall", "Malt", "management", "management service", "manufactur", "manufacturing", "Maps", "marines", "marketing", "MART", "mastics", "meat", "meat products", "Meats", "mechanical", "med", "Medical", "medical assistance", "medical equipment", "medical instrument", "Medical supplies", "medical supply", "medicare", "memorial", "mens cloth", "mental", "merchants", "metallic gasket", "Meter", "mfg", "Midwives", "military", "mill", "millinery", "millwork", "mineral", "mining", "missionary", "missions", "mobile", "model", "Mortgage", "Motel", "mothers", "motor", "Motor control", "Motor home", "motor vehicle", "motorcycle parts", "Motors", "Movie", "Mulch", "municipal", "museums", "Music", "music store", "Music stores", "musical", "Musical Instrument", "mutual", "mutual funds", "National Security", "natural", "natural gas", "natural resource", "Natural science", "nature", "Naturopath", "naval", "navy", "needle", "new car", "new car dealers", "Newsletter", "Newspaper", "notion", "Novelties", "Novelty", "Nursering", "Nursery", "Nursing", "nursing care", "nursing facility", "Nutrition", "nuts", "Obstetrical", "Occupational safety", "offendres", "office", "office equipment", "office equipment &", "office furniture", "office supplies", "oil & gas", "oil&gas", "oils", "oncology", "Opera", "operating", "ophthalmic", "optometrists", "optometry", "Oral", "orchard", "orchestra", "Order", "ordnance", "organic", "organic chemical", "organiser", "organizations", "Ornamental ironwork", "Orthodontist", "Orthopaedic", "outdoor furniture", "outerwear", "outpatient", "p t a", "packaged", "packaged frozen", "packaging", "padding", "pageant", "Paint", "painting", "paints", "Paramedic", "park equipment", "Park police", "parks", "Parks & Recreation", "Pathologists", "patient", "patients", "paving", "payday", "perfume", "Periodicals", "personal watercraft", "pest control services", "pet food", "Petroleum", "pharmaceutical", "Pharmacies", "Pharmacist", "pharmacy", "Phone", "photo film", "photographic", "photographics", "physician", "physiology", "Picnic", "Picture", "Pies", "Pigments", "Pipe & Foundry", "Pipe tobacco", "Pizza", "Pizzas", "plant", "planting", "plasma", "Plaster", "plastic", "plastic material", "Plate glass", "plating", "playwright", "Plumber", "plumbers", "plumbers & pipe fitters", "plumbers & pipefitters", "Plumbers & steam fitters", "Plumbers & steamfitters", "plumbing", "plumbing supplies", "plywood", "poet", "police", "polishing", "political", "popcorn", "porter", "portfolio", "pottery", "poulry", "Poultry", "Power Generation", "practitioners", "Precious metals", "Precision", "pregnancy", "Prepared foods", "prescription", "Press", "Pretzels", "printer", "printing", "printing equipment", "probation", "Probation Office", "Processed meats", "produce", "product", "professional", "promoter", "propane", "properties", "property", "Prosthetic", "protection", "provincial", "Psychiatric", "psychology", "pta", "Public", "Public safety", "Publisher", "Pulp Wood", "puppet", "race track", "Racetrack", "racing", "Ractrack", "Radiators & Mufflers", "Radio", "Radio Station", "Radiologists", "ranching", "razor", "record", "Recorder", "Recording", "Recreational Vehicles", "refineries", "refining", "refrigeration", "refuse", "Regulator", "rehab", "Rehabilitation services", "Relays", "Relief services", "remodeling", "rental", "repair", "reproductive", "Rescue", "reserves", "reservoirs", "resins", "Restaurant equipment", "restaurant furniture", "Restaurent", "Retail", "retirement", "Riding", "rodeo", "roller", "Rollers", "roofing", "royalty", "rubber", "Saddlery", "safari", "Salad", "Salons", "saloon", "salt", "Sanitary service", "school", "School equipment", "School supply", "Scientific instruments", "scintefic", "scoccer", "script", "Seafoods", "seafoods market", "sealents", "securit", "securities", "Security system", "Seeds", "Senior citizens", "Septic", "Septic tanks", "service", "services", "settellite", "sewage", "sewage system", "Sewing", "Sewing machines", "shaving", "sheep", "Sheet metal roofing", "Shellac", "Shelters", "Ship chandler", "shop", "shows", "Signal systems", "singers", "sites", "skating", "sketch", "Ski resorts", "skier", "Skiing", "smoked meat", "snacks", "Snowmobile", "Snuff", "Soccer Club", "social", "social security", "Social service", "social services", "Software", "software service", "software solution", "soil", "solar", "song", "Sound production", "Soups", "Souvenir", "spa", "space", "Speciality Program", "Spikes", "spirits merchant", "spirits merchants", "Sponges", "sport", "Sport utility", "Sporting", "Sporting good", "Sporting Goods", "sportwear", "Spray painting", "Springs metal", "Sprinkler system", "stadium", "stage", "Stains Merchant", "state", "State Department", "State police", "state troopers", "steam provider", "stereo", "Storage bins", "store", "stoves", "student", "Substance Abuse", "Sugar", "suit", "super market", "supermarket", "Supervisor", "suppl", "Suppliers", "supply store", "supply stores", "support services", "Suppt", "Surgical", "Surgical supplies", "Surveying equipment", "swimwear", "swine", "symphony", "synthetic rubber", "Syrup", "tacks", "talent", "tax", "Tea", "teams", "technical", "technician", "technology", "Telecom", "telemarketing", "telephone", "Television", "temple", "tennis", "textile", "Theater", "theatrical", "Theme Park", "Therapists", "therapy", "tidal", "tile", "timber", "Tin Plate", "Tire Service", "Tire Shop", "tires", "tires &", "Tobacco", "Tobacco Merchant", "Tobacco products", "toilet", "Tool", "toothbrush", "Toupees", "tour", "tourist", "Toy store", "trading", "Trailer parts", "Training Center", "Trampoline", "Transformer", "transmission", "transmission system", "trash", "travel", "Travel trailer", "tree", "Trees", "troop", "Tropical fish", "troupe", "truck dealers", "Truck trailer", "trust", "trusts", "tubes", "tubs", "tv", "Typewriter", "Typewriter &", "Ultrasound", "umbrella", "uniform", "uniforms &", "United Nations", "Univ", "Upholsterers", "upholstery", "used", "used cars", "Used tires", "utilities", "utility", "utility trailer", "vaccine", "Vacuum", "valves", "Varnish", "Vegetable", "Vegetables", "vehicle dealers", "Video", "Video Game", "vineyard", "Vitamin", "Vocational", "Vocational Rehabilitation Services", "voltage", "volunteers", "WAL MART", "Wall coverings", "Wallpaper", "warehouse", "waste", "waste collection", "waste treatment", "watch repair", "Watches", "water conservation", "water coolers", "Water Park", "wax", "wear", "Weather stripping", "welding", "welding gas", "Welfare services", "Wheelchair", "wholesalers", "wildlife", "wind energy", "Window Cleaning", "wine", "wine merchants", "wine wholesalers", "Wire rope", "wireless", "Wires", "wiring", "wonder", "wood chemical", "Wood products", "wood treating", "work clothing", "workship", "writer", "wvu", "Yacht club", "yeast", "ymca", "yoga", "Youth", "Youth sports", "zoo", "zoological"];
            $rootScope.keyWordMatch1 = ["& assoc", "& associate", "& company", "& corporate", "4H", "Abuse Center", "academy", "account", "accountants", "Acupuncture", "Additives", "Adhesives", "admin", "Administration", "Adoption", "Adventist", "Adventure", "Advertising", "advisor", "Advisors", "Advisory", "Aeronautical equipment", "Aeronautics", "aerospace", "AFB", "agencies", "Agency", "agents", "agricultural", "agriculture", "Aid", "air conditioning", "Air Force", "air supply", "Airport", "Alarm", "alcoholic", "alcoholism", "ale", "Alfalfa", "ambulance", "Ambulance service", "Ambulatory", "American Indian", "Ammonia", "Amusement", "Analytical", "Analytical instruments", "Animal feeds", "Animal Shelter", "animation", "Antennas", "antique auto", "Apothecaries", "apparel", "appliance", "Appraisal", "Apts", "Aquaculture", "Aquatic", "Aquatics", "ARC", "architect", "Architects", "areas", "arena", "army", "Aromatic", "art", "Art Goods", "artist", "arts", "Asphalt & concrete", "Assembly", "asset", "Assistance", "Assisted", "assisted living", "Assn", "assoc", "athletes", "Athletic", "attorney", "attractions", "Attys", "Audio", "Auto", "Auto Body Shop", "auto dealers", "auto supply", "automobile", "Automobile accessories", "automobile dealers", "Automotive", "Automotive parts", "automotive wholesalers", "Auxiliary", "Aviation", "awarness", "baby furniture", "Babysitting services", "baked foods", "bakerie", "bakery", "Bakery products", "Ballet", "Bands", "bank", "bankers", "banking", "banks", "Bapt", "Baptist", "Bar", "baseball", "baseketball", "bath", "bathroom", "Batteries", "bay", "Bbq", "beauty", "Beer", "Beer wholesalers", "Behavioral", "belt", "Bethel", "beverage", "Bible", "binding", "bio mass", "biomass", "bird", "bkstr", "blade", "blades", "Blanket", "blood", "blood bank", "blueprint &", "blueprint service", "blueprinting", "Board", "Body shop suplies", "boilers", "book", "book store", "books", "botanical", "Botanical Gardens", "boxer", "boys cloth", "Brake", "brandy", "Broadcast", "broker", "brokerage", "brokers", "Brothers", "build", "Builders", "building", "Building Material", "Building Stone", "bulbs", "Bungalow", "Bureau", "Burger", "Burial", "Bus merchant", "business management", "business service", "business to business", "butcher", "buttermilk", "cable", "cables", "Cafe", "Camera", "Camp", "Campaign", "Camping trailer", "camps", "Campus", "canned foods", "canning", "Capacitors", "capital", "caps", "Carbon Black", "Cardiologists", "care", "care services", "carnival", "carpet", "Cartoon", "Casino", "Cast Iron", "Cast iron pipe", "Catering", "Catholic", "Cattle", "caverns", "Cement", "Cemetery", "centers", "ceramic", "Ceramic materials", "Chamber", "Chapel", "Chaplain", "Chaplains", "Charitable", "charitable trust", "Charities", "Charity", "Chartered", "Charters", "Chartiable", "Chassis", "Cheer", "Cheerleading", "cheese", "Chef", "Chefs", "Chem", "Chemical", "chemicals", "Chevrolet", "Chevron", "chewing gum", "Child", "Childcare", "Childhood", "Children", "Childrens", "Chinese", "Chiropractic", "Chiropractor", "Chiropratic", "chocolate", "Chocolates", "choir", "Christ", "Christian", "Christians", "chromatography", "chronic", "Chrysler", "church", "Churches", "Cigar", "Cigarettes", "Cigars", "Cinema", "Cinemas", "Circuit", "Circuit breaker", "Circuits", "circus", "Civic", "civil", "civil engineer", "Civil service", "Claim", "Claims", "Class", "classical", "Cleaners", "cleaning", "Climate", "clinic", "cloth", "clothes", "Club", "clubs", "coat", "Cocktails", "Coffee", "Coins &", "cold storage", "college", "cologne", "coloring", "Comfort", "Comic", "Commercial", "Commercial stationery", "Commission", "Committee", "Communicat", "Communications", "Community", "community foundations", "community services", "Companion services", "component", "Compounds", "compressors", "Computer", "computer suppl", "concert", "Concrete Reinforcing", "Conditioning", "Confectionery", "Congregation", "congress", "Conservation", "conservator", "construction", "consultants", "consulting", "Contact lens", "contemporary", "Continuing Care", "Contracting", "contractors", "control", "control board", "convalescent", "convent", "cooking equipment", "coolers", "cooling", "cooling & heating", "Cooperation", "corn chips", "Corps", "Correctional", "cosmetic", "Cosmetics", "costume", "Council", "Counsel", "Counseling", "Counseling Services", "Counselling services", "Country Clubs", "county trooper", "court", "Courts", "covering", "coverings", "CPA", "Cpr", "credit", "Credit Card Services", "crops", "crude", "crude oil", "Crude oil merchant", "Cruise", "Crushed stone", "CTR", "cultivating", "cutlery", "dairy", "dairy products", "Dance", "Data", "Data Steward", "Daycare", "Decorative", "Deli", "Dental", "Dentist", "Dentistry", "Department", "Dept", "Design", "Detergents", "Development", "diagnostic", "Diamond", "Dinners", "Directory", "Disable", "Discount", "dispos", "disposal system", "disposer", "Dist", "distilled", "Distilleries", "Distributing", "Distribution", "Distributors", "District", "Dj", "donor", "Donuts", "doughnut", "doughnuts", "Dr", "Drafting", "dress", "Drug", "Drug store", "drum", "dry clean", "dry cleaner", "dry good", "Drywall supply", "Dwelling", "dyes", "dyestuffs", "earth", "Economic", "Ecosystem", "edible", "edu", "Educ", "education", "educational", "elderly", "Electric", "electric lighting", "Electric Motor", "Electrical", "electrical distribution", "Electronic", "electronic marketing", "Electronics", "electroplating", "elem", "elementary", "Elks", "Embassies", "embroidery", "Emergency", "empire", "employment", "Enamels", "Endowment", "Energy", "engine", "engine part", "Engineering", "engineers", "enterprise", "entertainer", "Entertainment", "Envelopes", "Environmental", "Episcopal", "equipment", "equit_", "erosion", "essential oils", "Estate", "estates", "ethnic", "Evangelical", "Excavating", "Executive", "exhibits", "exploration", "explosives", "Export", "Eye", "eye bank", "Eyecare", "eyeglass", "Eyes", "fabric", "factory", "Fans", "farm", "Farm supplies", "Farmers", "farming", "farms", "fasteners", "federal", "Federation", "Fellowship", "Fertility", "fertiliz", "fertilizers", "festival", "fiberglass", "fibers", "Fighters", "Film", "finance", "financial", "Fine arts", "Fine paper", "Fire", "fire extinguisher", "Fire Protection", "Firearms", "Firefighting Equipment", "first aid", "Fish", "fishing", "Fishing Club", "Fishing clubs", "Fitness", "fittings", "Fixture/Fixtures", "Flat glass", "flight", "floor covering", "Floor maint equipment", "FloorCovering", "flooring", "Floors", "Florists", "Flour", "Flowers", "Flying club", "Foaster", "Folk", "Food", "food distribution", "Food service", "food stores", "Foods", "foods store", "football", "Football clubs", "force", "Ford", "Forensic", "Forest Products", "forestry", "fossil fuel", "foundation", "Foundry Products", "fountain", "Fraternal", "Fraternity", "Freezer", "fresh", "fresh fish", "fresh seafood", "Friendship", "frozen", "Frozen foods", "fruit", "Fruits", "Fuel oil", "Fun Center", "Fund", "fundrais", "Funeral", "furnace", "furnishing", "furniture", "Gallery", "gambling", "game", "Gaming", "Gamma", "garage", "Garbage", "Garbage disposal", "Garden supplies", "gardens", "gas", "Gases", "gasoline", "Gasoline equipment", "gauze", "General", "General services", "Generator", "geo thermal", "geograph", "geothermal", "Gifts", "Glass", "glues", "goats", "God", "golf", "Golf carts", "Golf clubs", "Golf Courses", "Gospel", "Gourmet", "government", "Govt", "Graphics", "Gravel", "Greeting", "Grill", "groceries", "grocery", "Guild", "Gun clubs", "Gym", "hair", "Hairstyling", "Hand tools", "Hardware", "Harness", "hat", "hats", "Hay", "hazardous", "health", "healthcare", "hearing aid", "heaters", "heating", "heating & cooling", "help", "herbal", "heritage", "Historical", "HMO", "hobbie", "hockey", "hogs", "Hoists", "holding", "Holdings", "Holy", "Home care", "home furnishing", "Homebuilders", "Homeless", "homes", "Honda", "Honey", "Hope", "hosiery", "Hospice", "hospital", "Hospital equipment", "Hospitality", "Hospitals", "Hosting", "Hotel", "Hotel furntiure", "hotel management", "hotel supply", "Hotels", "household", "Housewares", "Housing", "housing Authority", "Hs", "human rights", "hunting", "Hunting clubs", "hydronics", "Hygienist", "Hypnotherapy", "Ice", "ice cream", "Imaging", "Import", "independent", "Indoor", "Industrial", "industrial chemicals", "industrial gases", "Industries", "Infant", "ink Manufacturing", "ink mfg", "Inn", "Inns", "inpatient", "Ins", "Institute", "insulated", "Insulated wire", "insulation", "insurance", "Interior", "Interiors", "Internet", "investigation", "investment", "Investments", "Iron", "Islamic", "Janitorial supplies", "Janitorial supply", "jazz", "Jesus", "Jewelers", "jewelry", "Jewish", "jockey", "Journal", "Juices", "Justice", "Juvenile", "Kappa", "Kayak", "kennels", "Kids", "Kitchen Cabinets", "knives", "label", "Laboratories", "Laboratory equipment", "Ladies", "Land", "landfil", "landfill", "landscape", "Landscaping", "Laser Surgery", "Laundry", "law", "law enforcement", "Lawn", "Lawn care", "lawyer", "lawyers", "LDS", "league", "Learning", "lease", "leasing", "Legion", "Legislative", "Library", "licensing", "Life", "Lighting", "Lime", "Limestone", "lingerie", "liquified gas", "Liquified petroleum gas", "liquor", "Liquors", "literary", "Livestock", "lllp", "loan", "locker", "Locksmith", "Lodge", "Lodging", "logging", "logistics", "Longue", "lumber", "Luth", "Macaroni", "Mach", "Mach & Foundry", "machinary", "Machine", "Magazine", "Magazines", "Maintenance", "mall", "Malt", "management", "management service", "manufactur", "manufacturing", "Maps", "marines", "Market", "marketing", "MART", "Masonic", "Masonry", "Massage", "mastics", "Materials", "Md", "meat", "meat products", "Meats", "mechanical", "med", "Media", "Medical", "medical assistance", "medical equipment", "medical instrument", "Medical supplies", "medical supply", "medicare", "Medicine", "memorial", "mens cloth", "mental", "merchants", "Messiah", "Metal", "metallic gasket", "Metals", "Meter", "Meth", "Methodist", "mfg", "Midwives", "military", "mill", "millinery", "millwork", "mineral", "mining", "Ministries", "Ministry", "Mission", "missionary", "missions", "mobile", "model", "Montessori", "mortgage", "Motel", "mothers", "motor", "Motor control", "Motor home", "motor vehicle", "motorcycle parts", "Motors", "Mount", "Movie", "Mr", "Ms", "Mulch", "municipal", "Museum", "museums", "Music", "music store", "Music stores", "musical", "Musical Instrument", "Muslim", "mutual", "mutual funds", "Nails", "National Security", "Nativity", "natural", "natural gas", "natural resource", "Natural science", "nature", "Naturopath", "naval", "navy", "needle", "Network", "new car", "new car dealers", "Newsletter", "Newspaper", "notion", "Novelties", "Novelty", "Nursering", "Nursery", "Nursing", "nursing care", "nursing facility", "Nutrition", "nuts", "Ob", "Obstetrical", "Occupational safety", "offendres", "office", "office equipment", "office equipment &", "office furniture", "Office Supplies", "Oil", "oil & gas", "oil&gas", "oils", "oncology", "Opera", "operating", "ophthalmic", "optometrists", "optometry", "Oral", "orchard", "orchestra", "Order", "ordnance", "organic", "organic chemical", "organiser", "organizations", "Ornamental ironwork", "Orthodontist", "Orthopaedic", "outdoor furniture", "outerwear", "outpatient", "Outreach", "P T A", "packaged", "packaged frozen", "packaging", "padding", "pageant", "paint", "painting", "paints", "Paper", "Paramedic", "park equipment", "Park police", "parks", "Parks & Recreation", "Pathologists", "patient", "patients", "paving", "payday", "PC", "PD", "Peace", "Pentecostal", "perfume", "Periodicals", "personal watercraft", "Pest", "pest control services", "Pet", "pet food", "Petroleum", "pharmaceutical", "Pharmacies", "Pharmacist", "pharmacy", "Phi", "Phone", "photo film", "photographic", "photographics", "Photography", "physician", "Physicians", "physiology", "Picnic", "Picture", "Pies", "Pigments", "Pipe & Foundry", "Pipe tobacco", "Pizza", "Pizzas", "plant", "planting", "plasma", "Plaster", "plastic", "plastic material", "Plastics", "Plate glass", "plating", "playwright", "Plaza", "Plumber", "plumbers", "plumbers & pipe fitters", "plumbers & pipefitters", "Plumbers & steam fitters", "Plumbers & steamfitters", "plumbing", "plumbing supplies", "plywood", "poet", "police", "polishing", "political", "Pool", "popcorn", "porter", "portfolio", "Post", "Postal", "pottery", "poulry", "Poultry", "Power Generation", "practitioners", "Precious metals", "Precision", "pregnancy", "Prepared foods", "Presbyterian", "Preschool", "prescription", "Press", "Pretzels", "printer", "printing", "printing equipment", "probation", "Probation Office", "Processed meats", "produce", "product", "Productions", "Products", "professional", "Program", "promoter", "propane", "properties", "property", "Prosthetic", "protection", "provincial", "Psychiatric", "psychology", "PTA", "Pto", "Pub", "Public", "Public safety", "Publisher", "Publishing", "Pulp Wood", "puppet", "race track", "Racetrack", "racing", "Ractrack", "Radiators & Mufflers", "Radio", "Radio Station", "Radiologists", "Ranch", "ranching", "razor", "Real", "Realtor", "Realtors", "Realty", "record", "Recorder", "Recording", "Recovery", "Recreation", "Recreational Vehicles", "Recycling", "refineries", "refining", "refrigeration", "refuse", "Regulator", "rehab", "Rehabilitation", "Rehabilitation services", "Reinsurance", "Relays", "Relief services", "remodeling", "rental", "Rentals", "repair", "reproductive", "Rescue", "Research", "reserves", "reservoirs", "Residential", "resins", "Resort", "Restaurant", "Restaurant equipment", "restaurant furniture", "Restaurants", "Restaurent", "Restoration", "Retail", "retirement", "Revocable", "Riding", "rodeo", "roller", "Rollers", "roofing", "Rotary", "ROTC", "royalty", "rubber", "Rx", "Saddlery", "safari", "Safety", "Saints", "Salad", "Sales", "Salon", "Salons", "saloon", "salt", "Sanitary service", "Savings", "Sch", "school", "School equipment", "School supply", "Schools", "Scientific instruments", "scintefic", "scoccer", "Scout", "Scouts", "script", "Seafoods", "seafoods market", "sealents", "securit", "securities", "Security system", "Seeds", "Senior citizens", "Septic", "Septic tanks", "service", "services", "settellite", "sewage", "sewage system", "sewing", "Sewing machines", "shaving", "Shear", "sheep", "Sheet metal roofing", "Shellac", "Shelters", "Sheriffs", "Ship chandler", "shop", "shows", "Sigma", "Signal systems", "singers", "Sisters", "sites", "skating", "sketch", "Ski resorts", "skier", "Skiing", "smoked meat", "snacks", "Snowmobile", "Snuff", "Soccer Club", "social", "social security", "Social service", "social services", "Society", "software", "software service", "software solution", "soil", "solar", "song", "Sorority", "Sound production", "Soups", "Souvenir", "spa", "space", "Speciality Program", "Spikes", "Spirit", "spirits merchant", "spirits merchants", "Sponges", "sport", "Sport utility", "Sporting", "Sporting good", "Sporting Goods", "Sports", "sportwear", "Spray painting", "Springs metal", "Sprinkler system", "St.", "stadium", "Staffing", "stage", "Stains Merchant", "state", "State Department", "State police", "state troopers", "Station", "steam provider", "Steel", "stereo", "Storage", "storage bins", "store", "Stores", "stoves", "student", "Students", "Studio", "Substance Abuse", "Sugar", "suit", "Suites", "super market", "supermarket", "Supermarkets", "Supervisor", "suppl", "Suppliers", "Supply", "supply store", "supply stores", "support services", "Suppt", "Surgery", "Surgical", "Surgical supplies", "Surveying equipment", "Swim", "Swimming", "swimwear", "swine", "symphony", "synthetic rubber", "Syrup", "Tabernacle", "tacks", "talent", "tax", "Tea", "Teachers", "teams", "Tech", "technical", "technician", "Technologies", "technology", "Telecom", "telemarketing", "telephone", "Television", "temple", "tennis", "textile", "Theater", "Theatre", "theatrical", "Theme Park", "Therapists", "Therapy", "Theta", "tidal", "tile", "timber", "Tin Plate", "Tire", "Tire Service", "Tire Shop", "tires", "tires &", "Title", "Tobacco", "Tobacco Merchant", "Tobacco products", "toilet", "Tool", "toothbrush", "Toupees", "tour", "tourist", "Toy store", "Toyota", "Track", "trading", "Trailer parts", "Training", "Training Center", "Trampoline", "Transformer", "transmission", "transmission system", "Transport", "Transportation", "trash", "travel", "Travel trailer", "tree", "Trees", "Trinity", "troop", "Tropical fish", "troupe", "Truck", "truck dealers", "Truck trailer", "Trucking", "trust", "trusts", "tubes", "tubs", "tv", "Typewriter", "Typewriter &", "Ultrasound", "umbrella", "Underwriters", "uniform", "uniforms &", "United Nations", "Unity", "Univ", "Upholsterers", "upholstery", "used", "used cars", "Used tires", "utilities", "utility", "utility trailer", "vaccine", "Vacuum", "valves", "Varnish", "Vegetable", "Vegetables", "vehicle dealers", "Veterans", "Veterinary", "Victory", "Video", "Video Game", "vineyard", "Vision", "vitamin", "Vocational", "Vocational Rehabilitation Services", "voltage", "volunteers", "WAL MART", "Wall coverings", "Wallpaper", "warehouse", "waste", "waste collection", "waste treatment", "watch repair", "Watches", "Water", "water conservation", "water coolers", "Water Park", "wax", "Wealth", "wear", "Weather stripping", "welding", "welding gas", "Welfare services", "Wellness", "Wesleyan", "Wheelchair", "Wholesale", "wholesalers", "wildlife", "wind energy", "Window Cleaning", "wine", "wine merchants", "wine wholesalers", "Wire rope", "wireless", "Wires", "wiring", "Womans", "Women", "Womens", "wonder", "wood chemical", "Wood products", "wood treating", "work clothing", "workship", "writer", "wvu", "Yacht", "Yacht club", "yeast", "ymca", "yoga", "Youth", "Youth sports", "Zion", "zoo", "zoological"];
           // $rootScope.keyWordMatch1 = ["& assoc", "& associate", "& company", "& corporate", "4H", "Abuse Center", "academy", "account", "accountants", "Acupuncture", "Additives", "Adhesives", "admin", "Administration", "Adoption", "Adventist", "Adventure", "Advertising", "advisor", "Advisors", "Advisory", "Aeronautical equipment", "Aeronautics", "aerospace", "AFB", "agencies", "Agency", "agents", "agricultural", "agriculture", "Aid", "air conditioning", "Air Force", "air supply", "Airport", "Alarm", "alcoholic", "alcoholism", "ale", "Alfalfa", "ambulance", "Ambulance service", "Ambulatory", "American Indian", "Ammonia", "Amusement", "Analytical", "Analytical instruments", "Animal feeds", "Animal Shelter", "animation", "Antennas", "antique auto", "Apothecaries", "apparel", "appliance", "Appraisal", "Apts", "Aquaculture", "Aquatic", "Aquatics", "ARC", "architect", "Architects", "areas", "arena", "army", "Aromatic", "art", "Art Goods", "artist", "arts", "Asphalt & concrete", "Assembly", "asset", "Assistance", "Assisted", "assisted living", "Assn", "assoc", "athletes", "Athletic", "attorney", "attractions", "Attys", "Audio", "Auto", "Auto Body Shop", "auto dealers", "auto supply", "automobile", "Automobile accessories", "automobile dealers", "Automotive", "Automotive parts", "automotive wholesalers", "Auxiliary", "Aviation", "awarness", "baby furniture", "Babysitting services", "baked foods", "bakerie", "bakery", "Bakery products", "Ballet", "Bands", "bank", "bankers", "banking", "banks", "Bapt", "Baptist", "Bar", "baseball", "baseketball", "bath", "bathroom", "Batteries", "bay", "Bbq", "beauty", "Beer", "Beer wholesalers", "Behavioral", "belt", "Bethel", "beverage", "Bible", "binding", "bio mass", "biomass", "bird", "bkstr", "blade", "blades", "Blanket", "blood", "blood bank", "blueprint &", "blueprint service", "blueprinting", "Board", "Body shop suplies", "boilers", "book", "book store", "books", "botanical", "Botanical Gardens", "boxer", "boys cloth", "Brake", "brandy", "Broadcast", "broker", "brokerage", "brokers", "Brothers", "build", "Builders", "building", "Building Material", "Building Stone", "bulbs", "Bungalow", "Bureau", "Burger", "Burial", "Bus merchant", "business management", "business service", "business to business", "butcher", "buttermilk", "cable", "cables", "Cafe", "Camera", "Camp", "Campaign", "Camping trailer", "camps", "Campus", "canned foods", "canning", "Capacitors", "capital", "caps", "Carbon Black", "Cardiologists", "care", "care services", "carnival", "carpet", "Cartoon", "Casino", "Cast Iron", "Cast iron pipe"]
           $rootScope.searchInput = {};
            $rootScope.exportType = type;
            angular.element(downloadNAICSSuggestionsPopup).modal();
        }
        //To export/download the NAICS Suggestions details to the excel file
        $scope.exportNAICSsuggestions = function (listRuleKeyword) {
            angular.element(downloadNAICSSuggestionsPopup).modal('hide');
            MessagePopup($rootScope, "Download NAICS Suggestions", "Download has begun and will complete shortly.");
            var searchInput = {};
            searchInput = dataService.getTopAccountSearchInput();
            searchInput.los = $rootScope.exportType;                
            searchInput.listRuleKeyword =listRuleKeyword;
            service.getDownloadNAICSSuggestionsData(searchInput, $rootScope.exportType);
        }
        // To open Upload NAICS suggestions Popup 
        $scope.uploadNAICSSuggestions = function (type) {
            //var searchInput = {};
            //searchInput = dataService.getTopAccountSearchInput();
            $rootScope.exportType = type;
            angular.element(uploadNAICSSuggestionsPopup).modal();           
        }

       //To get the upload files
        var formdata = new FormData();
        $rootScope.fileName = null;
        $scope.getTheFiles = function ($files) {
            var result = document.getElementsByClassName("file-input-label");
            angular.element(result).text($files[0].name);
            $rootScope.fileName = angular.element(result).text($files[0].name);
            dataService.setUploadNaicsSuggetionsFormData(formdata);
            angular.forEach($files, function (value, key) {
                formdata.append(key, value);            
            });
        };      
        //To clear the seleted file
       
        $scope.clearTheFiles = function () {          
             var result = document.getElementsByClassName("file-input-label");
            angular.element(result).text("");
            $rootScope.exportType = "All";
        };
        // To upload the NAICS Suggestion file.
        $scope.uploadNaicsSuggestionsExcelFile = function () {
            angular.element(uploadNAICSSuggestionsPopup).modal('hide');
           
            if ($rootScope.fileName == null)
            {
                MessagePopup($rootScope, "NAICS Suggestions Upload", "No files have been selected for Upload");
            }
            else
                {
                MessagePopup($rootScope, "NAICS Suggestions Upload", "File is getting Uploaded...");
            var searchInput = {};
            searchInput = dataService.getTopAccountSearchInput();
            searchInput.los = $rootScope.exportType;           
            var request = {
                method: 'POST',
                url: BasePath + "TopAccountService/UploadNAICSSuggestions/",
                data: formdata,
                headers: {
                    'Content-Type': undefined
                }
            };
            // SEND THE FILE to the server for upload
            $http(request)
                .success(function (res) {
                    MessagePopup($rootScope, "Upload Successful", "File Uploaded successfully.");
                    //Invoke the service method to place the search
                    var SearchInput = {};
                    SearchInput = dataService.getTopAccountSearchInput();
                    searchInput.los = "All";
                    service.getTopAccountSearchResults(SearchInput).success(function (searchRes) {
                        //Refresh all the grids
                        $scope.searchResultsBIOGridOptions.data = searchRes.objBIOSearchResults;
                        $scope.searchResultsFRGridOptions.data = searchRes.objFRSearchResults;
                        $scope.searchResultsPHSSGridOptions.data = searchRes.objPHSSSearchResults;
                        $scope.totalPHSSAccountCount = $scope.searchResultsPHSSGridOptions.data.length;
                        $scope.totalFRAccountCount = $scope.searchResultsFRGridOptions.data.length;
                        $scope.totalBIOAccountCount = $scope.searchResultsBIOGridOptions.data.length;
                        $scope.totalAccountCount = searchRes.objSearchResults.length;
                    });
                    $scope.clearTheFiles();
                  
                })
                .error(function () {
                    MessagePopup($rootScope, "Upload Error", "Error occured during upload.");
                });  
            }
        }  
        

    }]);
