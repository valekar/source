//Search Results controller - Enterprise Account
enterpriseAccMod.controller("enterPriseAccountSearchResultController", ['$scope', '$http', '$rootScope', '$uibModal', '$location', '$timeout', 'uiGridConstants', '$window','$localStorage', '$state', 'EnterpriseAccountService', 'EnterpriseAccountDataService', 'EnterpriseAccountHelperService',

    function ($scope, $http, $rootScope, $uibModal, $location, $timeout, uiGridConstants,$window,$localStorage, $state, service, dataService, helperService) {

        /************************* Constants *************************/
        $rootScope.editEnterpriseAccountTemplate = BasePath + "App/Shared/Views/EnterpriseAccount/EnterpriseAccountEdit.html";
        $rootScope.deleteEnterpriseOrgsTemplate = BasePath + "App/Shared/Views/EnterpriseAccount/EnterpriseOrgsDelete.html";
        //$rootScope.enterPriseAccountNAICSCodeCreateTemplate = BasePath + "App/EnterpriseAccount/Views/EnterpriseAccountSelectNaicsCodeCreate.html";
        var NAVIGATION_URL = "/enterpriseaccount/search/result/multi/";

        /***************************Get Image path************************************/
      
        $scope.getImagePath = function (action) {
            if (action == 'edit') {
                return BasePath + "Images/Edit.jpg";
            }
            if (action == 'addedit') {
                return BasePath + "Images/EditAdd.png";
            }
            if (action == 'delete') {
                return BasePath + "Images/EditAdd.png";
            }
        }
       

        /************************* Generic Methods *************************/
        //Method to hide the processing overlay
        $scope.hidePleaseWait = function () {
            $scope.processingOverlay = false;
        }

        //Method to show the processing overlay
        $scope.showPleaseWaitText = function () {
            $scope.processingOverlay = true;
        }

       // $scope.hidePleaseWait();

        /************************* Defining the Search Results Grid *************************/
      //  Variables used for paginating the search results
        var searchInput = {};
        searchInput = dataService.getSearchParams();
        var searchResults = {};
        searchResults = dataService.getEnterpriseAccountSearchResults();       
        if (searchResults != null) {
           // if (angular.isUndefined(searchResults.objSearchResults)) {
                $localStorage.EnterpriseOrgsResults = dataService.getEnterpriseAccountSearchResults();
                $localStorage._previousInput = dataService.getSearchParams();
                //console.log(searchResults);
            //}

        }
        if ($localStorage.EnterpriseAccountlastSearchResults.objSearchResults.length > 0)
        {
            searchResults = $localStorage.EnterpriseAccountlastSearchResults;
            searchInput = $localStorage.EnterpriseAccountlastSearchInput;
        }

        /************************* Binding Grid data *************************/       
        $scope.searchResultsGridOptions = helperService.getEnterAccGridOptions();
        $scope.searchResultsGridOptions.columnDefs = helperService.getEnterPriseAccountSearchResultsColDef($scope);
        $scope.searchResultsGridOptions.data = searchResults.objSearchResults;
        $scope.searchResultsGridOptions.totalItems = $scope.searchResultsGridOptions.data.length;    
       

        //Registering the method which will get executed once the grid gets loaded
        $scope.searchResultsGridOptions.onRegisterApi = function (gridApi) {
            //Store the grid data against a variable so that it can be used for pagination
            $scope.searchResultsGridEnterpriseOrgs = gridApi;
        }

        //Method which is called to paginate the search results
        $scope.navigationPageChange = function (page) {
            $scope.searchResultsGridEnterpriseOrgs.pagination.seek(page);
        }       

        $scope.paginationPageSize = $scope.searchResultsGridOptions.paginationPageSize;
      
            $scope.totalAccountCount = searchResults.objSearchResults.length ;      
            $scope.paginationCurrentPage = 1;
          //  console.log($scope.totalAccountCount);

        /************************* Search Results Behaviours - Edit *************************/       
        //Edit methods       
        $scope.editEnterpriseAccount = function (row, grid) {
            $scope.pleaseWait = { "display": "block" };

            $scope.InputData = {};
            $scope.InputData.enterpriseOrgId = row.ent_org_id;
            service.getEnterpriseAccountBasicDetails($scope.InputData).success(function (result) {
                if (result != null) {                   
                $scope.InputData.EnterpriseOrgId = result[0].ent_org_id;
                $scope.InputData.EnterpriseOrgName = result[0].ent_org_name;
                $scope.InputData.EnterpriseOrgsDesc = result[0].ent_org_dsc;                
                $scope.InputData.SourceSystemId = result[0].nk_ent_org_id;           
            }
                angular.element(deleteEnterpriseOrgsTemplate).modal();
                $scope.pleaseWait = { "display": "none" };
               
            })
            .error(function (result) {
                errorPopups(result);
                $scope.pleaseWait = { "display": "none" };
            });
        }
        //Edit methods       
        $scope.editEnterpriseAccountSubmit = function () {
            $scope.showPleaseWaitText();
            //Frame the inputs
            $scope.postData = {};
            $scope.postData.ent_org_id = (angular.isUndefined($scope.InputData.EnterpriseOrgId) ? "" : $scope.InputData.EnterpriseOrgId);
            $scope.postData.ent_org_name = (angular.isUndefined($scope.InputData.EnterpriseOrgName) ? "" : $scope.InputData.EnterpriseOrgName);
            $scope.postData.naics_cd = (angular.isUndefined($scope.InputData.NaicsCodeData) ? "" : $scope.InputData.NaicsCodeData);
            $scope.postData.ent_org_typ = (angular.isUndefined($scope.InputData.EnterpriseOrgType) ? "" : $scope.InputData.EnterpriseOrgType);
            $scope.postData.affili_vsblty = (angular.isUndefined($scope.InputData.Affil_vsblty) ? 0 : $scope.InputData.Affil_vsblty);
            $scope.postData.key_acct_fr = (angular.isUndefined($scope.InputData.IsPremierFR) ? 0 : $scope.InputData.IsPremierFR);
            $scope.postData.key_acct_hs = (angular.isUndefined($scope.InputData.IsPremierHS) ? 0 : $scope.InputData.IsPremierHS);
            $scope.postData.key_acct_bio = (angular.isUndefined($scope.InputData.IsPremierBIO) ? 0 : $scope.InputData.IsPremierBIO);
            $scope.postData.key_acct_ent = (angular.isUndefined($scope.InputData.IsPremierENT) ? 0 : $scope.InputData.IsPremierENT);
            service.submitEnterpriseAccountEdit($scope.postData).success(function (result) {
                if (result.length > 0) {

                    if (result[0].o_outputMessage == "Success") {

                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_EDIT_HEADER, ENT_ACC.ENT_ORG_EDIT_MESSAGE)
                        $scope.hidePleaseWait();
                        //$scope.EnterpriseAccountClearInput();
                        //$rootScope.gridApiNAICSAdd.grid.clearAllFilters();
                        //$scope.pleaseWait = { "display": "none" };
                        //$location.url(NAVIGATE_URL);
                        angular.element(editEnterpriseAccountPopup).modal('hide');
                    }
                    else {
                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_EDIT_HEADER, ENT_ACC.ENT_ORG_EDIT_FAILEDMESSAGE)
                        $scope.hidePleaseWait();
                    }
                }
                else {
                    MessagePopup($rootScope, ENT_ACC.ENT_ORG_EDIT_HEADER, ENT_ACC.ENT_ORG_CREATION_FAILEDMESSAGE)
                    $scope.hidePleaseWait();
                }
                $scope.hidePleaseWait();

            })
            .error(function (result) {
                errorPopups(result);
                $scope.hidePleaseWait();
            });
        }
        /************************* Search Results Behaviours - Delete *************************/
        //Delete methods       
        $scope.deleteEnterpriseOrgs = function (row, grid) {
            $scope.pleaseWait = { "display": "block" };

            $scope.InputData = {};
            $scope.InputData.enterpriseOrgId = row.ent_org_id;

            //service.getEnterpriseAccountBasicDetails($scope.InputData).success(function (result) {
            //    if (result != null) {
                    $scope.InputData.EnterpriseOrgId = row.ent_org_id;
                    $scope.InputData.EnterpriseOrgName = row.ent_org_name;
                    $scope.InputData.SourceSystem = row.ent_org_src_cd;
                    $scope.InputData.EnterpriseOrgsDesc = row.ent_org_dsc;
                    $scope.InputData.SourceSystemId = row.nk_ent_org_id;
               // }
                angular.element(deleteEnterpriseOrgsPopup).modal();
                $scope.pleaseWait = { "display": "none" };

            //})
            //.error(function (result) {
            //    errorPopups(result);
            //    $scope.pleaseWait = { "display": "none" };
            //});
        }
        //Delete methods       
        $scope.deleteEnterpriseOrgsSubmit = function () {
            $scope.pleaseWait = { "display": "block" };
            //Frame the inputs
            $scope.postData = {};
            $scope.postData.ent_org_id = (angular.isUndefined($scope.InputData.EnterpriseOrgId) ? "" : $scope.InputData.EnterpriseOrgId);           
           
            service.submitEnterpriseOrgsDelete($scope.postData).success(function (result) {
                if (result.length > 0) {

                    if (result[0].o_outputMessage == "Success") {

                        //Refresh all the grids
                        service.getEnterpriseAccountSearchResults(searchInput).success(function (result) {
                            $scope.searchResultsGridOptions.data = result.objSearchResults;
                        });
                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_DELETE_HEADER, ENT_ACC.ENT_ORG_DELETE_MESSAGE + result[0].o_transaction_key)
                        $scope.pleaseWait = { "display": "none" };
                        angular.element(deleteEnterpriseOrgsPopup).modal('hide');
                    }
                    else {
                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_DELETE_HEADER, ENT_ACC.ENT_ORG_DELETION_MESSAGE)
                        $scope.pleaseWait = { "display": "none" };
                    }
                }
                else {
                    MessagePopup($rootScope, ENT_ACC.ENT_ORG_DELETE_HEADER, ENT_ACC.ENT_ORG_DELETION_MESSAGE)
                    $scope.pleaseWait = { "display": "none" };
                }
                $scope.pleaseWait = { "display": "none" };

            })
            .error(function (result) {
                errorPopups(result);
                $scope.pleaseWait = { "display": "none" };
            });
        }     

        $scope.cancel = function () {
            angular.element(deleteEnterpriseOrgsPopup).modal('hide');
           // $uibModalInstance.dismiss('cancel');
        };

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
              { displayName: 'NAICS Code', field: 'naics_cd', width: '*', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000 }
            ]
        };

        $rootScope.searchGridOptions = gridOption;

        //Registering the method which will get executed once the grid gets loaded
        $rootScope.searchGridOptions.onRegisterApi = function (gridApi) {

            //Store the grid data against a variable so that it can be used for pagination
            $rootScope.gridApiNAICSAdd = gridApi;
        }

        //Method to bind NAICS Codes Level2 and 3
        var dataLevel2and3 = [];
        service.getNAICSCodesLevel2and3()
            .success(function (result) {
                dataLevel2and3 = result;
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
                writeoutNode(dataLevel2and3, 0, $rootScope.searchGridOptions.data);
            })
            .error(function (result) {
                // errorPopups(result);
            });
        //Method to open the add pop-up on click of the Add button
        $scope.selectNaicsCodesEnterpriseAccountCreate = function () {

            //$scope.showPleaseWaitText();
            //$rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
            //$rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children
            // $rootScope.gridApiNAICSAdd.grid.clearAllFilters(); //Clear all the filters
            //listSelectedCreateNAICSCodes = [];
            $rootScope.NaicsCodeData = "";
            //Open the pop-up
            angular.element(createNAICSCodePopup).modal();
            // $scope.hidePleaseWait();


        }
        //Method called on click of the Submit button
        var listSelectedCreateNAICSCodes = [];
        $scope.submitNaicsCodesEnterpriseAccountCreate = function () {
            // $scope.showPleaseWaitText();
            //Hide the pop-up
            debugger;
            angular.element(createNAICSCodePopup).modal('hide');
            //Refresh the grid with updated information
            angular.forEach($rootScope.gridApiNAICSAdd.selection.getSelectedRows(), function (value, key) {
                listSelectedCreateNAICSCodes.push(value.naics_cd);
            });
            $rootScope.NaicsCodeData = listSelectedCreateNAICSCodes.toString();
        }
        //Add methods
        $scope.addNameNewAccount = function (row, grid) {
            MessagePopup($rootScope, ENT_ACC.ENT_ORG_NEWNAME_HEADER, ENT_ACC.ENT_ORG_NEWNAME_MESSAGE);
        }
        $scope.addAddressNewAccount = function (row, grid) {
            MessagePopup($rootScope, ENT_ACC.ENT_ORG_NEWADDRESS_HEADER, ENT_ACC.ENT_ORG_NEWADDRESS_MESSAGE);
        }
        $scope.addPhoneNewAccount = function (row, grid) {
            MessagePopup($rootScope, ENT_ACC.ENT_ORG_NEWNOHEADER, ENT_ACC.ENT_ORG_NEWNOMESSAGE);
        }
        $scope.addEntOrgIdNewAccount = function (row, grid) {
            MessagePopup($rootScope, ENT_ACC.ENT_ORG_NEWIDHEADER, ENT_ACC.ENT_ORG_NEWIDMESSAGE);
        }      
        //Pop-up surfacing
        function errorPopups(result) {
            if (result == GEN_CONSTANTS.ACCESS_DENIED) {
                MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE);
                $scope.pleaseWait = { "display": "none" };
                angular.element(deleteEnterpriseOrgsPopup).modal('hide');
            }
            else if (result == GEN_CONSTANTS.DB_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.DB_ERROR_CONFIRM, GEN_CONSTANTS.DB_ERROR_MESSAGE);
            }
            else if (result == GEN_CONSTANTS.TIMEOUT_ERROR) {
                MessagePopup($rootScope, GEN_CONSTANTS.TIMEOUT_ERROR_CONFIRM, GEN_CONSTANTS.TIMEOUT_ERROR_MESSAGE);
            }
        }
        //Message Pop-up 
        function MessagePopup($rootScope, headerText, bodyText) {
            $rootScope.enterpriseAccountModalPopupHeaderText = headerText;
            $rootScope.enterpriseAccountModalPopupBodyText = bodyText;
            angular.element(enterpriseAccountMessageDialogBox).modal({ backdrop: "static" });
        }  


        $scope.openDetails = function (row) {
            var entOrgId = row.entity.ent_org_id;
            dataService.clearDetails();
            sessionStorage.ent_org_name = row.entity.ent_org_name;
            $location.url(NAVIGATION_URL + entOrgId);
        }
        
    }]);
