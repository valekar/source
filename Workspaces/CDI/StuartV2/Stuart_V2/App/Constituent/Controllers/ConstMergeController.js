//Invoked wwhen clicked on compare in cart section
angular.module('constituent').controller('MergeCartCtrl', ['$scope', '$uibModal', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices', '$location',
    'constituentCRUDapiService', '$q', 'params', 'constituentCRUDoperations',
    'constituentApiService', '$rootScope', 'constRecordType', 'constituentServices', 'constClearDataService','uiGridConstants','constituentDataServices',
    function ($scope, $uibModal, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices, $location, constituentCRUDapiService, $q,
        params, constituentCRUDoperations, constituentApiService, $rootScope, constRecordType, constituentServices, constClearDataService, uiGridConstants, constituentDataServices) {

        var REDIRECT_URL = "/constituent/search/results/multi/";
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var mygrid = params.grid;
        var row = params.row;
        var deferLNs = $q.defer();

        $scope.mergeCart = {
            dsp: {},
            bridgePresence: {},
            name: {},
            address: {},
            phone: {},
            email: {},
            birth: {},
            death: {},
            toggleCartCompare: false,
            pleaseWait: true,
            toggleMergeCartSection: true,
            constituent_type: constRecordType.getRecordType() == null ? row.rows[0].constituent_type : constRecordType.getRecordType()

        };
        //console.log("Here I am ");
        //console.log(params.row.rows[0]);

        //this is how i get the result from the backend so had to process like this
        angular.forEach(params.row.data, function (v, k) {
            if (v.header == "Master Id") {
                $scope.mergeCart.masterId1 = v.MasterId1;
                $scope.mergeCart.masterId2 = v.MasterId2;
                $scope.mergeCart.masterId3 = v.MasterId3;
                $scope.mergeCart.masterId4 = v.MasterId4;
                $scope.mergeCart.masterId5 = v.MasterId5;
            }
            else if (v.header == "Dsp Id") {
                $scope.mergeCart.dsp.detail1 = v.Detail1;
                $scope.mergeCart.dsp.detail2 = v.Detail2;
                $scope.mergeCart.dsp.detail3 = v.Detail3;
                $scope.mergeCart.dsp.detail4 = v.Detail4;
                $scope.mergeCart.dsp.detail5 = v.Detail5;
            }
            else if (v.header == "Bridge Presence") {
                $scope.mergeCart.bridgePresence.detail1 = v.Detail1;
                $scope.mergeCart.bridgePresence.detail2 = v.Detail2;
                $scope.mergeCart.bridgePresence.detail3 = v.Detail3;
                $scope.mergeCart.bridgePresence.detail4 = v.Detail4;
                $scope.mergeCart.bridgePresence.detail5 = v.Detail5;
            }
            else if (v.header == "Name") {
                $scope.mergeCart.name.detail1 = v.Detail1;
                $scope.mergeCart.name.detail2 = v.Detail2;
                $scope.mergeCart.name.detail3 = v.Detail3;
                $scope.mergeCart.name.detail4 = v.Detail4;
                $scope.mergeCart.name.detail5 = v.Detail5;
            }
            else if (v.header == "Address") {
                $scope.mergeCart.address.detail1 = v.Detail1;
                $scope.mergeCart.address.detail2 = v.Detail2;
                $scope.mergeCart.address.detail3 = v.Detail3;
                $scope.mergeCart.address.detail4 = v.Detail4;
                $scope.mergeCart.address.detail5 = v.Detail5;
            }
            else if (v.header == "Email") {
                $scope.mergeCart.email.detail1 = v.Detail1;
                $scope.mergeCart.email.detail2 = v.Detail2;
                $scope.mergeCart.email.detail3 = v.Detail3;
                $scope.mergeCart.email.detail4 = v.Detail4;
                $scope.mergeCart.email.detail5 = v.Detail5;
            }
            else if (v.header == "Phone") {
                $scope.mergeCart.phone.detail1 = v.Detail1;
                $scope.mergeCart.phone.detail2 = v.Detail2;
                $scope.mergeCart.phone.detail3 = v.Detail3;
                $scope.mergeCart.phone.detail4 = v.Detail4;
                $scope.mergeCart.phone.detail5 = v.Detail5;
            }
            else if (v.header == "Death") {
                $scope.mergeCart.death.detail1 = v.Detail1;
                $scope.mergeCart.death.detail2 = v.Detail2;
                $scope.mergeCart.death.detail3 = v.Detail3;
                $scope.mergeCart.death.detail4 = v.Detail4;
                $scope.mergeCart.death.detail5 = v.Detail5;
            }
            else if (v.header == "Birth") {
                $scope.mergeCart.birth.detail1 = v.Detail1;
                $scope.mergeCart.birth.detail2 = v.Detail2;
                $scope.mergeCart.birth.detail3 = v.Detail3;
                $scope.mergeCart.birth.detail4 = v.Detail4;
                $scope.mergeCart.birth.detail5 = v.Detail5;
            }
        });

        $scope.mergeCart.toggleCartCompare = true;

        $scope.mergeCart.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }


        $scope.mergeCart.multiDetail = function(masterId,name){
            console.log(masterId);
            delete $sessionStorage.masterId;
            delete $sessionStorage.name;
            delete $sessionStorage.type;
            $sessionStorage.masterId = masterId;
            $sessionStorage.name = name;
            $sessionStorage.type = constRecordType.getRecordType() == null ? row.rows[0].constituent_type : constRecordType.getRecordType();
            constClearDataService.clearMultiData();
            $uibModalInstance.dismiss('cancel');
            $location.url(REDIRECT_URL + masterId);
        }

        $scope.mergeCart.getMaster = function (masterId) {

        }
        $scope.mergeCart.confirm = function () {
            var rowsTobeProcessed = [];
            angular.forEach(params.row.rows, function (v, k) {
                if ($scope.mergeCart.checkbox1) {
                    if ($scope.mergeCart.masterId1 == v.constituent_id) {
                        rowsTobeProcessed.push(v);
                    }
                }
                if ($scope.mergeCart.checkbox2) {
                    if ($scope.mergeCart.masterId2 == v.constituent_id) {
                        rowsTobeProcessed.push(v);
                    }
                }
                if ($scope.mergeCart.checkbox3) {
                    if ($scope.mergeCart.masterId3 == v.constituent_id) {
                        rowsTobeProcessed.push(v);
                    }
                }
                if ($scope.mergeCart.checkbox4) {
                    if ($scope.mergeCart.masterId4 == v.constituent_id) {
                        rowsTobeProcessed.push(v);
                    }
                }
                if ($scope.mergeCart.checkbox5) {
                    if ($scope.mergeCart.masterId5 == v.constituent_id) {
                        rowsTobeProcessed.push(v);
                    }
                }
            });


            console.log(rowsTobeProcessed);
            if (rowsTobeProcessed.length <= 1) {
               // messagePopup($rootScope, "Please select at least two records to merge", "Error!");
                openErrorPopup("Please select at least two records to merge", "Error!", $uibModal);
                //alert();
            }
            else {

                //check if the constituent type of the records are same
                var first_const_type = rowsTobeProcessed[0].constituent_type;
                for (var i = 0; i < rowsTobeProcessed.length; i++) {
                    if (first_const_type != rowsTobeProcessed[i].constituent_type) {
                       // messagePopup($rootScope, "Please select either Individual or Organization records for merging", "Error!");
                        openErrorPopup("Please select either Individual or Organization records for merging", "Error!", $uibModal);
                        return;
                    }
                }

                //get merge conflicts records from cookies
                constituentServices.getMergeConflicts().success(function (result) {
                    //checkboxed masters

                    var proccessingMstrs = [];
                    for (var i = 0; i < rowsTobeProcessed.length; i++) {
                        proccessingMstrs.push(+rowsTobeProcessed[i].constituent_id);
                    }
                    //masters from stored merge conflicts
                    var mergeMstrsFromCase = [];
                    for (var i = 0; i < result.locInfoResearchData.length; i++) {
                        mergeMstrsFromCase.push(+result.locInfoResearchData[i].cnst_mstr_id);
                    }
                    //check if they are same
                    $scope.confirmCart.mergeConflict = angular.equals(proccessingMstrs.sort(), mergeMstrsFromCase.sort());

                    // if yes then its merge conflict records
                    if ($scope.confirmCart.mergeConflict) {
                        $scope.confirmCart.toggleTrustedSysRadio = true;
                        $scope.confirmCart.mergeConflictResults = result;
                        $scope.confirmCart.caseNo = $scope.confirmCart.mergeConflictResults.caseKey;
                    }
                        //no then normal merge records
                    else {
                        $scope.confirmCart.toggleTrustedSysRadio = false;
                        $scope.confirmCart.mergeConflictResults = {};
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

                // $scope.confirmCart.mergeConflict = ?;

                $scope.mergeCart.toggleMergeCartSection = false;
                $scope.confirmCart.toggleMergeConfirmSection = true;
                //used in confirm section
                $scope.confirmCart.mergeRows = rowsTobeProcessed;
                // console.log($scope.confirmCart.mergeRows);

                deferLNs.resolve();

            }

        }

        /**************************confirm section logic***************************/
        //confirm section

        
        deferLNs.promise.then(function () {
            console.log("Resolving the LNs");
            var LNs = [];
            var masterIds = [];
            angular.forEach($scope.confirmCart.mergeRows, function (v, k) {
                if (v.cnst_dsp_id != null && !angular.isUndefined(v.cnst_dsp_id)) {
                    //changed by srini, because we have to pass masterid, not the dsp id
                    LNs.push({ "id": v.constituent_id, "value": v.cnst_dsp_id })

                }

                masterIds.push(v.constituent_id);
            });
            $scope.confirmCart.masterIds = masterIds;
            $scope.confirmCart.selected.LNValue = LNs[0].value;
            $scope.confirmCart.selected.LNId = LNs[0].id;
            $scope.confirmCart.LNs = LNs;

            //return LNs;
        });


        $scope.confirmCart = {
            toggleMergeConfirmSection: false,
            selected: {
                LNValue: "",
                LNId:"",
                note: ""
            },
            mergeConflict: false,
            sourceSysTypes: ['LN', 'SRCSYS'],
            mergeConflictResults: {},
            //record_type : $sessionStorage.type,
            LNs: [],
            notes: CRUD_CONSTANTS.NOTES,
            caseNo: caseNo,
            toggleTrustedSysRadio: false,
            trustedSourceSystem: 'SRCSYS'

        }
       
        
        $scope.confirmCart.back = function () {
            $scope.mergeCart.toggleMergeCartSection = true;
            $scope.confirmCart.toggleMergeConfirmSection = false;
        }

        // cancel
        $scope.confirmCart.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }

        //create new case 
        $scope.confirmCart.createCase = function () {
            constituentCRUDoperations.getCasePopup($scope, $uibModal);
        }

        //submit for merge
        $scope.confirmCart.submit = function (myForm) {

           // console.log(constRecordType.getRecordType());
           // console.log(row.rows[0].constituent_type);

            var cnstType = constRecordType.getRecordType() == null ?  row.rows[0].constituent_type : constRecordType.getRecordType() ;

            if (myForm.$valid) {
                myApp.showPleaseWait();
                //normal merge
                if (!$scope.confirmCart.mergeConflict) {
                    var postParams = {
                        "MasterIds": $scope.confirmCart.masterIds,
                        "ConstituentType": cnstType,
                        "Notes": $scope.confirmCart.selected.note,
                        "CaseNumber": $scope.confirmCart.caseNo,
                        "PreferredMasterIdForLn": $scope.confirmCart.selected.LNId
                    }

                    var merge_constant = CRUD_CONSTANTS.MERGE_RECORD;
                }
                    //merge conflict
                else {
                    var postParams = {
                        "MasterIds": $scope.confirmCart.masterIds,
                        "ConstituentType": cnstType,
                        "Notes": $scope.confirmCart.selected.note,
                        "CaseNumber": $scope.confirmCart.mergeConflictResults.caseKey,
                        "PreferredMasterIdForLn": $scope.confirmCart.selected.LNId,
                        "TrustedSource": $scope.confirmCart.trustedSourceSystem,
                        "InternalSourceSystemGroupId": $scope.confirmCart.mergeConflictResults.groupID

                    }
                    var merge_constant = CRUD_CONSTANTS.MERGE_CONFLICT_RECORD;
                }

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, merge_constant).success(function (result) {
                    //console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;
                    console.log($scope.confirmCart.mergeRows);
                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {

                       
                    constituentServices.removeMergeGroupFromCart($scope.confirmCart.mergeRows).success(function () {

                        constituentServices.getCartResults().success(function (innerResult) {
                            var columnDefs = constituentServices.getCartColumnDef(uiGridConstants);
                            // $scope.cartGridOptions.data = [];
                            constituentDataServices.setCartData(innerResult);
                            var _previousUnmergeResults = constituentDataServices.getCartData();
                            $scope.totalCartItems = _previousUnmergeResults.length;
                            $scope.currentPage = 1;

                            mygrid.data = '';
                            mygrid.data.length = 0;
                            mygrid.data = _previousUnmergeResults;


                            var output = {
                                finalMessage: CRUD_CONSTANTS.MERGE_SUCCESS_MESSAGE,
                                ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                                ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                            }
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);
                        }).error(function (result) {
                          myApp.hidePleaseWait();
                          $uibModalInstance.dismiss('cancel');
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
                     });                                                                  

                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.MERGE_FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                   
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
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


            }
        }


    }]);
