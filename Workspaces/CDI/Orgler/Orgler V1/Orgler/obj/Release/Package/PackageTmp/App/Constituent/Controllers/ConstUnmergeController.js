
function masterGroups() {
    return [
        { id: "1", value: "1" },
        { id: "2", value: "2" },
        { id: "3", value: "3" },
        { id: "4", value: "4" },
        { id: "5", value: "5" }
    ]
}

angular.module('constituent').controller('UnmergeCartCtrl', ['$scope', '$uibModal', '$filter', '$uibModalInstance', '$parse', '$rootScope', 'globalServices',
    'constituentCRUDapiService', 'params', 'constituentCRUDoperations', 'constituentApiService', 'constituentDataServices', 'constituentServices', 'uiGridConstants',
    function ($scope, $uibModal, $filter, $uibModalInstance, $parse, $rootScope, globalServices, constituentCRUDapiService, params, constituentCRUDoperations,
        constituentApiService, constituentDataServices, constituentServices, uiGridConstants) {

        //console.log(params.row.data);
        //console.log(params.row.rows);
        var mygrid = params.grid;
       

        $scope.unmerge = {
            compareData: params.row.data,
            selected: {
                // masterGroupId: "1",
                record1: null,
                record2: null,
                record3: null,
                record4: null,
                record0: null
            },
            masterGroups: masterGroups(),
            toggleUnmergeSection: true,
            toggleConfirmSection: false,
            confirmData: {
                MasterGroup1: { MasterGroupName: "Master 1", MasterGroupValue: [], persistance: true },
                MasterGroup2: { MasterGroupName: "Master 2", MasterGroupValue: [], persistance: true },
                MasterGroup3: { MasterGroupName: "Master 3", MasterGroupValue: [], persistance: true },
                MasterGroup4: { MasterGroupName: "Master 4", MasterGroupValue: [], persistance: true },
                MasterGroup5: { MasterGroupName: "Master 5", MasterGroupValue: [], persistance: true }
            },
            caseNo: ""

        }


        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            $scope.unmerge.caseNo = globalServices.getCaseTabCaseNo();
        }
        else {
            //get merge conflicts records from cookies -- > 
            //added this because of the issue raised may have to remove this, as I guess this is not the intended functionality
            constituentServices.getMergeConflicts().success(function (result) {
                $scope.unmerge.caseNo = result.caseKey;
            }).error(function (result) {
                errorPopup(result);
            });
        }

        $scope.unmerge.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.unmerge.changeSelected = function ($index, $event, value) {
            $event.preventDefault();
            $scope.unmerge.selected['record' + $index] = value;
        }

        // we are assuming that the records are inorder and have not changed their places so we are directly passing the rows index ---> ex params.row.rows[0]
        $scope.unmerge.confirm = function () {
            var showConfirmationSection = false;
            if ($scope.unmerge.selected.record0 !== null) {
                $scope.unmerge.confirmData["MasterGroup" + $scope.unmerge.selected.record0].MasterGroupValue.push(params.row.rows[0]);
                showConfirmationSection = true;
            }
            if ($scope.unmerge.selected.record1 !== null) {
                $scope.unmerge.confirmData["MasterGroup" + $scope.unmerge.selected.record1].MasterGroupValue.push(params.row.rows[1]);
                showConfirmationSection = true;
            }
            if ($scope.unmerge.selected.record2 !== null) {
                $scope.unmerge.confirmData["MasterGroup" + $scope.unmerge.selected.record2].MasterGroupValue.push(params.row.rows[2]);
                showConfirmationSection = true;
            }
            if ($scope.unmerge.selected.record3 !== null) {
                $scope.unmerge.confirmData["MasterGroup" + $scope.unmerge.selected.record3].MasterGroupValue.push(params.row.rows[3]);
                showConfirmationSection = true;
            }
            if ($scope.unmerge.selected.record4 !== null) {
                $scope.unmerge.confirmData["MasterGroup" + $scope.unmerge.selected.record4].MasterGroupValue.push(params.row.rows[4]);
                showConfirmationSection = true;
            }


            if (showConfirmationSection) {
                $scope.unmerge.toggleUnmergeSection = false;
                $scope.unmerge.toggleConfirmSection = true;
            }
            else {

              //  messagePopup($rootScope, "Set MasterGroup for at least one record", "Error");
                openErrorPopup(CONSTITUENT_MESSAGES.SET_MSTR_GRP, CONSTITUENT_MESSAGES.ERROR, $uibModal);
                //  alert();
            }
            //console.log($scope.unmerge.confirmData);
        };



        $scope.unmerge.submit = function () {
            myApp.showPleaseWait();
            var postParams = {
                UnmergeRequestDetails: [],
                UserName: "",
                Notes: "",
                CaseNumber: $scope.unmerge.caseNo
            };

            var removeRecordsFromUnmerge = [];

            if ($scope.unmerge.confirmData.MasterGroup1.MasterGroupValue.length > 0) {
                var persistance = 0;
                if ($scope.unmerge.confirmData.MasterGroup1.persistance) {
                    persistance = 1;
                }
                else {
                    persistance = 0;
                }
                angular.forEach($scope.unmerge.confirmData.MasterGroup1.MasterGroupValue, function (v, k) {

                    var unmasterRequest = new UnMasterRequest(v.cnst_mstr_id, v.constituent_type, v.source_system_id, v.source_system_cd, "1", persistance).getUnMasterRequest();
                    postParams.UnmergeRequestDetails.push(unmasterRequest);
                    removeRecordsFromUnmerge.push(v.IndexString);
                });
            }

            if ($scope.unmerge.confirmData.MasterGroup2.MasterGroupValue.length > 0) {
                var persistance = 0;
                if ($scope.unmerge.confirmData.MasterGroup2.persistance) {
                    persistance = 1;
                }
                else {
                    persistance = 0;
                }
                angular.forEach($scope.unmerge.confirmData.MasterGroup2.MasterGroupValue, function (v, k) {
                    var unmasterRequest = new UnMasterRequest(v.cnst_mstr_id, v.constituent_type, v.source_system_id, v.source_system_cd, "2", persistance).getUnMasterRequest();
                    postParams.UnmergeRequestDetails.push(unmasterRequest);
                    removeRecordsFromUnmerge.push(v.IndexString);
                });
            }

            if ($scope.unmerge.confirmData.MasterGroup3.MasterGroupValue.length > 0) {
                var persistance = 0;
                if ($scope.unmerge.confirmData.MasterGroup3.persistance) {
                    persistance = 1;
                }
                else {
                    persistance = 0;
                }
                angular.forEach($scope.unmerge.confirmData.MasterGroup3.MasterGroupValue, function (v, k) {
                    var unmasterRequest = new UnMasterRequest(v.cnst_mstr_id, v.constituent_type, v.source_system_id, v.source_system_cd, "3", persistance).getUnMasterRequest();
                    postParams.UnmergeRequestDetails.push(unmasterRequest);
                    removeRecordsFromUnmerge.push(v.IndexString);
                });
            }

            if ($scope.unmerge.confirmData.MasterGroup4.MasterGroupValue.length > 0) {
                if ($scope.unmerge.confirmData.MasterGroup4.persistance) {
                    persistance = 1;
                }
                else {
                    persistance = 0;
                }
                angular.forEach($scope.unmerge.confirmData.MasterGroup4.MasterGroupValue, function (v, k) {
                    var unmasterRequest = new UnMasterRequest(v.cnst_mstr_id, v.constituent_type, v.source_system_id, v.source_system_cd, "4", persistance).getUnMasterRequest();
                    postParams.UnmergeRequestDetails.push(unmasterRequest);
                    removeRecordsFromUnmerge.push(v.IndexString);
                });
            }
            if ($scope.unmerge.confirmData.MasterGroup5.MasterGroupValue.length > 0) {
                if ($scope.unmerge.confirmData.MasterGroup5.persistance) {
                    persistance = 1;
                }
                else {
                    persistance = 0;
                }
                angular.forEach($scope.unmerge.confirmData.MasterGroup5.MasterGroupValue, function (v, k) {
                    var unmasterRequest = new UnMasterRequest(v.cnst_mstr_id, v.constituent_type, v.source_system_id, v.source_system_cd, "5", persistance).getUnMasterRequest();
                    postParams.UnmergeRequestDetails.push(unmasterRequest);
                    removeRecordsFromUnmerge.push(v.IndexString);
                });
            }


            constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.UNMERGE_RECORD).success(function (result) {
                output_msg = result[0].o_outputMessage;
                trans_key = result[0].o_transaction_key;

                if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                    var newRemoveRecordsFromUnmerge = [];
                    var unmergeCartItems = constituentDataServices.getUnmergeCartData();
                    for (var i = 0; i < removeRecordsFromUnmerge.length; i++) {
                        for (var j = 0; j < unmergeCartItems.length; j++) {
                            if (unmergeCartItems[j].IndexString == removeRecordsFromUnmerge[i]) {
                                newRemoveRecordsFromUnmerge.push(unmergeCartItems[i]);
                                break;
                            }
                        }
                    }

                    constituentServices.removeUnmergeFromCart(newRemoveRecordsFromUnmerge).success(function (result) {
                        if (result == "success") {
                            constituentServices.getUnmergeCartResults().success(function (innerResult) {

                                var columnDefs = constituentServices.getUnmergeColumnDef(uiGridConstants);
                                // $scope.cartGridOptions.data = [];
                                constituentDataServices.setUnmergeCartData(innerResult);
                                var _previousUnmergeResults = constituentDataServices.getUnmergeCartData();
                                $scope.totalCartItems = _previousUnmergeResults.length;
                                $scope.currentPage = 1;

                                mygrid.data = '';
                                mygrid.data.length = 0;
                                mygrid.data = _previousUnmergeResults;

                                var caseNo = null;
                                if ($scope.unmerge.caseNo != null) {
                                    caseNo = CRUD_CONSTANTS.CASE_MESSAGE + $scope.unmerge.caseNo;
                                };

                                var output = {
                                    finalMessage: CRUD_CONSTANTS.UNMERGE_SUCCESS_MESSAGE,
                                    ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                                    ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM,
                                    
                                    CaseNo: caseNo
                                }
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.dismiss('cancel');
                                errorPopup(result);
                            });
                        }
                    }).error(function (result) {
                        myApp.hidePleaseWait();
                        $uibModalInstance.dismiss('cancel');
                        errorPopup(result);
                    });

                    //});

                }
                else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.UNMERGE_FAILURE_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                        ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                    }
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                }
               
            }).error(function (result) {
                myApp.hidePleaseWait();
                $uibModalInstance.dismiss('cancel');
                errorPopup(result);
            });;
        }

        function errorPopup(result) {
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


    }]);

function UnMasterRequest(MasterId, ConstituentType, SourceSystemId, SourceSystemCode, MasterGroup, intPersistence) {

    this.MasterId = MasterId;
    this.ConstituentType = ConstituentType;
    this.SourceSystemId = SourceSystemId;
    this.SourceSystemCode = SourceSystemCode;
    this.MasterGroup = MasterGroup;
    this.intPersistence = intPersistence;

    this.getUnMasterRequest = function () {
        return {
            MasterId: this.MasterId,
            ConstituentType: this.ConstituentType,
            SourceSystemId: this.SourceSystemId,
            SourceSystemCode: this.SourceSystemCode,
            MasterGroup: this.MasterGroup,
            intPersistence: this.intPersistence
        }
    };
}