angular.module('constituent').controller("AddNAICSCodesInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance','globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', 'ConstituentNAICSHelperService','ConstituentNAICSService','uiGridTreeViewConstants' ,function ($scope, $filter, $uibModalInstance, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope, helperService, service) {

        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }

        $scope.edit_naics_cd_input = {           
            masterId: params.masterId
           
        };


        /************************* Search Results Behaviours - NAICS *************************/
               
       
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

        $scope.editNaicsCdConstituent = function () {
           // $scope.showPleaseWaitText();           
            //Method to get the recent searches on load of the application
            service.getRecentNAICSUpdate().success(function (result) {
                $rootScope.ConstituentRecentNAICSUpdate = result;
            }).error(function (result) {
                errorPopups(result);
            });

            $rootScope.naicsApprovedCodes = [];
            $rootScope.naicsRejectedCodes = [];
            $rootScope.naicsNewlyAddedCodes = [];

            $rootScope.isNaicsSuggested = false;
            $rootScope.selectedAction = "NA";

            //Customize the tree on load
            //$rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections
            //$rootScope.gridApiNAICSAdd.treeBase.collapseAllRows(); //Collapse all the tree children
            //$rootScope.gridApiNAICSAdd.grid.clearAllFilters(); //Clear all the filters

            $rootScope.edit_naics_cd_input = {};
            $rootScope.edit_naics_cd_input.cnst_mstr_id = !angular.isUndefined(params.masterId) ? params.masterId : "";
            //$rootScope.edit_naics_cd_input.source_system_id = !angular.isUndefined(row.source_system_id) ? row.source_system_id : "";
            //$rootScope.edit_naics_cd_input.source_system_code = !angular.isUndefined(row.source_system_code) ? row.source_system_code : "";
            //$rootScope.edit_naics_cd_input.cnst_org_nm = !angular.isUndefined(row.name) ? row.name : "";
            //$rootScope.edit_naics_cd_input.address = !angular.isUndefined(row.address) ? row.address : "";

            //if (angular.isDefined(row.listNAICSDesc) && row.listNAICSDesc != null && row.listNAICSDesc.length > 0) {
                $rootScope.isNaicsSuggested = true;
                //Get the NAICS Master details from the server
            service.getMasterNAICSDetails($rootScope.edit_naics_cd_input)
                    .success(function (result) {
                        $rootScope.naicsEditGridOption.data = result;
                        $rootScope.naicsEditGridOption.totalItems = $scope.naicsEditGridOption.data.length;

                       // $scope.hidePleaseWait();

                        //Open the Edit Modal pop-up
                        //angular.element(editNAICSCodePopup).modal();
                    })
                    .error(function (result) {
                        errorPopups(result);
                        $scope.hidePleaseWait();
                    });
            //}
            //else {
            //    $rootScope.isNaicsSuggested = false;

            //    $scope.hidePleaseWait();

                //Open the Edit Modal pop-up
               // angular.element(editNAICSCodePopup).modal();
            //}
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
            angular.element(constituentRecentNAICSUpdate).modal('hide');
        }

       // Method called on click of the Submit button
        $scope.submitNaicsCodeUpdates = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

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

                //Send the newly added NAICS codes to the client service
                service.submitNAICSCodeUpdates(postData).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_message;
                    trans_key = result[0].trans_id;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }
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
                                $rootScope.ConstituentRecentNAICSUpdate = result;
                            });
                        });

                        constituentApiService.getApiDetails($scope.edit_naics_cd_input.masterId, HOME_CONSTANTS.NAICS_CODES).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.NAICS_CODES);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.NAICS_CODES);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            // emailData.push(PushParams);
                            $uibModalInstance.dismiss('cancel');
                            affErrorPopup($rootScope, result)
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    /* else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                         var output = {
                             finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                             ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                         }
                         myApp.hidePleaseWait();
                         $uibModalInstance.close(output);
                     }*/
                }).error(function (result) {
                    affErrorPopup($rootScope, result)
                });
            }
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
              {
                  displayName: 'NAICS Code', filter: {
                      condition: uiGridConstants.filter.STARTS_WITH,
                      //  placeholder: 'starts with'
                  }, type: 'number', sort: { direction: 'asc', priority: 0 }, field: 'naics_cd', width: '*', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000
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

        service.getNAICSCodesTreeGrid()
            .success(function (result) {
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


        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);


function affErrorPopup($rootScope, result) {
    if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
        messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, CONSTITUENT_MESSAGES.ACCESS_DENIED_HEADER);
    }
    else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
        messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, CONSTITUENT_MESSAGES.TIMED_OUT_HEADER);
    }
    else if (result == CRUD_CONSTANTS.DB_ERROR) {
        messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, CONSTITUENT_MESSAGES.TIMED_OUT_HEADER);

    }
}