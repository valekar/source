//Controller for the Enterprise Account Create functionality
enterpriseAccMod.controller('enterPriseAccountCreateController',
    ['$state', '$http', '$location', '$rootScope', '$scope', '$uibModal', 'uibDateParser', 'EnterpriseAccountService',
        'EnterpriseAccountDataService', 'uiGridConstants', 'uiGridTreeViewConstants',
    function ($state, $http, $location, $rootScope, $scope, $uibModal, dateParser, service, dataService, uiGridTreeViewConstants, uiGridConstants) {

        /************************* Constants *************************/
        var BasePath = $("base").first().attr("href");
        $scope.processingOverlaySource = BasePath + "Images/Loading.gif";
        var NAVIGATE_URL = "/enterpriseaccount/search/result";
        $rootScope.enterPriseAccountNAICSCodeCreateTemplate = BasePath + "App/EnterpriseAccount/Views/EnterpriseAccountSelectNaicsCodeCreate.html";
        //var NAVIGATE_URL = "/enterpriseaccount/search";
        /************************* Generic Methods *************************/
        //Method to hide the search message label
        $scope.clearSearchMessage = function () {
            $scope.showSearchMessage = false;
        }

        //Method to show the search message label
        $scope.showSearchMessage = function () {
            $scope.showSearchMessage = true;
        }

        //Method to hide the processing overlay
        $scope.hidePleaseWait = function () {
            $scope.processingOverlay = false;
        }

        //Method to show the processing overlay
        $scope.showPleaseWaitText = function () {
            $scope.processingOverlay = true;
        }

        /************************* DOM Element Data Initialization *************************/
        //Hiding the search message field
        $scope.clearSearchMessage();
        $scope.hidePleaseWait();

        /************************* Clear Input Fields *************************/
        $scope.EnterpriseAccountClearInput = function () {           
            if ($scope.CreateEnterpriseAccountForm.$valid) {
                //Frame the inputs                
                $scope.EnterpriseOrgName = "";
                $scope.SourceSystem = "";
                $scope.SourceSystemId = "";               
                $scope.EnterpriseOrgsDesc = "";
            }
        }

        //service.getSourceSystem()
        //   .success(function (result) {
        //       $rootScope.SourceSystemData = result;

        //   })
        //   .error(function (result) {
        //       errorPopups(result);
        //   });
        /************************* New Enterprise Account Creation*************************/      
        $scope.EnterpriseAccountCreate = function () {
            // $scope.showPleaseWaitText();
            $scope.pleaseWait = { "display": "block" };            
            if ($scope.CreateEnterpriseAccountForm.$valid) {
                //Frame the inputs
                $scope.postParams = {};
                $scope.postParams.ent_org_name = (angular.isUndefined($scope.EnterpriseOrgName) ? "" : $scope.EnterpriseOrgName);
                $scope.postParams.ent_org_src_cd = (angular.isUndefined($scope.SourceSystem) ? "" : $scope.SourceSystem);
                $scope.postParams.nk_ent_org_id = (angular.isUndefined($scope.SourceSystemId) ? "" : $scope.SourceSystemId);
                $scope.postParams.ent_org_dsc = (angular.isUndefined($scope.EnterpriseOrgsDesc) ? "" : $scope.EnterpriseOrgsDesc);

              //  postParams = dataService.getEnterpriseAccountCreateInput();
                callCreateEnterpriseAccountService($scope, service, dataService, $scope.postParams, $location, $rootScope);
            }
            else {
                $scope.ConstNoResults = ENT_ACC.PLEASE_PROVIDE_INPUT;
                $scope.pleaseWait = { "display": "none" };
            }
        }
        /*****************Create Enterprise Account Service call**************************/

        function callCreateEnterpriseAccountService($scope, service, dataService, postParams, $location, $rootScope) {
           
            service.submitEnterpriseAccountCreate(postParams).success(function (result) {
               
                if (result.length > 0) {
                    
                    if (result[0].o_outputMessage == "Success") {

                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_HEADER, ENT_ACC.ENT_ORG_MESSAGE + result[0].o_ent_org_id)
                        $scope.pleaseWait = { "display": "none" };
                        $scope.EnterpriseAccountClearInput();
                        $rootScope.gridApiNAICSAdd.grid.clearAllFilters();
                        $scope.pleaseWait = { "display": "none" };
                       // $location.url(NAVIGATE_URL);
                    }
                    else if (result[0].o_outputMessage == "Duplicate") {

                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_EXIST_HEADER, ENT_ACC.ENT_ORG_EXIST_MESSAGE)
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {                       
                        MessagePopup($rootScope, ENT_ACC.ENT_ORG_CREATION_HEADER, ENT_ACC.ENT_ORG_CREATION_MESSAGE)
                        $scope.pleaseWait = { "display": "none" };
                    }
                }
                else {
                    MessagePopup($rootScope, ENT_ACC.ENT_ORG_CREATION_HEADER, ENT_ACC.ENT_ORG_CREATION_MESSAGE)
                    $scope.pleaseWait = { "display": "none" };
                }
                $scope.pleaseWait = { "display": "none" };
            }).error(function (result) {
               // myApp.hidePleaseWait();
                // $scope.pleaseWait = { "display": "none" };
                MessagePopup($rootScope, ENT_ACC.ENT_ORG_CREATION_HEADER, ENT_ACC.ENT_ORG_CREATION_MESSAGE)
                $scope.pleaseWait = { "display": "none" };
                //if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                //    $scope.ConfNote = CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM;
                //    $scope.CaseNumText = CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE
                //}
                //else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                //    MessagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                //}
                //else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                //    MessagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                //}

            });
        }
        //Message Pop-up 
        function MessagePopup($rootScope, headerText, bodyText) {
            $rootScope.enterpriseAccountModalPopupHeaderText = headerText;
            $rootScope.enterpriseAccountModalPopupBodyText = bodyText;
            angular.element(enterpriseAccountMessageDialogBox).modal({ backdrop: "static" });
        }

        //Pop-up surfacing
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
           // debugger;
            angular.element(createNAICSCodePopup).modal('hide');
            //Refresh the grid with updated information
            angular.forEach($rootScope.gridApiNAICSAdd.selection.getSelectedRows(), function (value, key) {
                listSelectedCreateNAICSCodes.push(value.naics_cd);
            });
            $rootScope.NaicsCodeData = listSelectedCreateNAICSCodes.toString();
        }

    }]);