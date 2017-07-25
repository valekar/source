/* Message Preference */
angular.module("constituent").controller("ConstCemMsgPrefCtrl",
    ["$scope", "ConstCemDataServices", "ConstCemServices", "StoreData", "$stateParams", "MessagePreferenceModal","uiGridConstants",
        "CEM_CONSTANTS", "HandleResults", "ShowDetailsModal", "$rootScope",
    function ($scope, ConstCemDataServices, ConstCemServices, StoreData, $stateParams, MessagePreferenceModal,uiGridConstants,
        CEM_CONSTANTS, HandleResults, ShowDetailsModal, $rootScope) {


        var initialize = function () {
            $scope.msgPref = {
                columnDef: ConstCemDataServices.getMsgPrefColumns(),
                gridOptions: {},
                toggleDetails: false,
                togglePleaseWait: true,
                totalItems: 0,
                currentPage: 1,
                masterId: $stateParams.constituentId,
                distinctRecordCount: 0,
                showAllName: CEM_CONSTANTS.SHOW_ALL_RECORDS,
                toggleRecords: true,
                showDetails: {}
            }

            $scope.msgPref.gridOptions = ConstCemServices.getGridOptions($scope.msgPref.columnDef);
            $scope.msgPref.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.msgPref.gridApi = gridApi;
            }

            showMessagePrefDetails();
            // get the dropdowns in advance for add/edit
            ConstCemDataServices.getMessagePrefDropDowns($scope.msgPref.masterId, false).then(function (res) {
                //console.log(res);
            });
        };
        initialize();

        
        $scope.$on('message-preference', function (event, args) {
            if (args.cem_msg_pref) {
                $scope.msgPref.togglePleaseWait = true;
                showMessagePrefDetails();
            } else {
                $scope.msgPref.togglePleaseWait = false;
                $scope.msgPref.toggleDetails = false;
            }

        });


        // show details popup
        $scope.msgPref.showDetails.allDetails = function () {
            $scope.msgPref.togglePleaseWait = true;
            ConstCemDataServices.getAllMessagePref($scope.msgPref.masterId).then(function (results) {
                $scope.msgPref.togglePleaseWait = false;
                var params = {
                    results: results,
                    type: CEM_CONSTANTS.MSG_PREF_DETAILS
                };
                ShowDetailsModal(params).then(function (response) { });

            }, function (error) {
                $scope.msgPref.togglePleaseWait = false
                HandleResults(error);
            });
        }

        // show all records
        $scope.msgPref.showAllrecords = function () {
            if ($scope.msgPref.toggleRecords) {
                $scope.msgPref.toggleRecords = !$scope.msgPref.toggleRecords;
                $scope.msgPref.showAllName = CEM_CONSTANTS.HIDE_RECORDS;
                $scope.msgPref.gridOptions = ConstCemServices.refreshGridData($scope.msgPref.gridOptions, StoreData.getMessagePrefFullList());
                $scope.msgPref.totalItems = StoreData.getMessagePrefFullList().length;
                $scope.msgPref.distinctRecordCount = StoreData.getMessagePrefFullList()[0].distinct_records_count;
            }
            else {
                $scope.msgPref.toggleRecords = !$scope.msgPref.toggleRecords;
                $scope.msgPref.showAllName = CEM_CONSTANTS.SHOW_ALL_RECORDS;
                $scope.msgPref.gridOptions = ConstCemServices.refreshGridData($scope.msgPref.gridOptions, StoreData.getMessagePrefList());
                $scope.msgPref.totalItems = StoreData.getMessagePrefList().length;
                $scope.msgPref.distinctRecordCount = StoreData.getMessagePrefList()[0].distinct_records_count;
            }
        }

        // add a record
        $scope.msgPref.addRecord = function () {
            var params = {
                masterId: $scope.msgPref.masterId
            }
            MessagePreferenceModal.getModal(CEM_CONSTANTS.MESSAGE_PREFERENCE_ADD, params).then(function (result) {
               // console.log(result);
                var bool = HandleResults(result, CEM_CONSTANTS.ADD);
                if (bool) {
                    $scope.msgPref.togglePleaseWait = true;
                    $scope.msgPref.toggleDetails = false;
                    refeshGrid();
                }
                
            });
        }

        //remove a reocrd
        //edit record
        $scope.editGridRow = function (row, grid) {
            //console.log(row);
            var params = {
                masterId: $scope.msgPref.masterId,
                row: row
            }
            MessagePreferenceModal.getModal(CEM_CONSTANTS.MESSAGE_PREFERENCE_EDIT, params).then(function (result) {
                // console.log(result);
                var bool = HandleResults(result, CEM_CONSTANTS.EDIT);
                if (bool) {
                    $scope.msgPref.togglePleaseWait = true;
                    $scope.msgPref.toggleDetails = false;
                    refeshGrid();
                }
            });
        }

        //Delete record
        $scope.deleteGridRow = function (row, grid) {
            //console.log(row);
            var params = {
                masterId: $scope.msgPref.masterId,
                row: row
            }
            MessagePreferenceModal.getModal(CEM_CONSTANTS.MESSAGE_PREFERENCE_DELETE, params).then(function (result) {
                // console.log(result);
                var bool = HandleResults(result, CEM_CONSTANTS.DELETE);
                if (bool) {
                    $scope.msgPref.togglePleaseWait = true;
                    $scope.msgPref.toggleDetails = false;
                    refeshGrid();
                }
            });
        }

        $scope.msgPref.pageChanged = function (page) {
            $scope.msgPref.gridApi.pagination.seek(page);
        }

        //filter for search results
        $rootScope.toggleMsgPrefSearchFilter = function () {
            //console.log("called msg Pref");
            $scope.msgPref.gridOptions.enableFiltering = !$scope.msgPref.gridOptions.enableFiltering;
            $scope.msgPref.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }


            //showing records 
        function showMessagePrefDetails() {
            if (StoreData.getMessagePrefList().length <= 0) {
                refeshGrid();
            }
            else {
            
                var res = StoreData.getMessagePrefList();
                $scope.msgPref.gridOptions = ConstCemServices.refreshGridData($scope.msgPref.gridOptions, res);
                $scope.msgPref.distinctRecordCount = res[0].distinct_records_count;
                $scope.msgPref.togglePleaseWait = false;
                $scope.msgPref.toggleDetails = true;
            }
        }

        //referesh grid after add/edit/delete operations
        function refeshGrid() {
            ConstCemDataServices.getMessagePref($scope.msgPref.masterId, false)
                .then(function (result) {
                   // console.log(result);
                    StoreData.addMessagePrefList(result, filterConstituentData(result));
                    $scope.msgPref.gridOptions = ConstCemServices.refreshGridData($scope.msgPref.gridOptions, StoreData.getMessagePrefList());
                    $scope.msgPref.togglePleaseWait = false;
                    $scope.msgPref.toggleDetails = true;
                    $scope.msgPref.totalItems = StoreData.getMessagePrefList().length;
                    //console.log(result[0].distinct_records_count);
                    if (StoreData.getMessagePrefList().length > 0) {
                        $scope.msgPref.distinctRecordCount = StoreData.getMessagePrefList()[0].distinct_records_count;
                    }
                    
                }, function (error) {
                    $scope.msgPref.togglePleaseWait = false;
                    HandleResults(error);
                });
        }

    
    }]);


/*Add message preference*/
angular.module('constituent').controller('AddMessagePreferenceCtrl',
    ['$scope', 'params', 'globalServices', 'ConstCemDataServices', '$sessionStorage', '$uibModalInstance','CEM_CONSTANTS','Message',
    function ($scope, params, globalServices, ConstCemDataServices, $sessionStorage, $uibModalInstance, CEM_CONSTANTS,Message) {
       // console.log("Add Message");
       // console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };
            $scope.addMessagePref = {
                masterId: params.masterId,
                selected:{
                    msgPrefLOS: '',
                    msgPrefCommChannel: '' ,
                    msgPrefCommType: '',
                    msgPrefType: '',
                    msgPrefValue: ''
                },
                caseNo: caseNo,
                notes: CRUD_CONSTANTS.NOTES,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                dropDowns : [],
                dateOptions: {
                    formatYear: 'yy',
                    maxDate: new Date(9999, 11, 31),
                    minDate: new Date(),
                    startingDay: 1
                },
                expirationDate: new Date(9999,11,31)
            }

            ConstCemDataServices.getMessagePrefDropDowns($scope.addMessagePref.masterId, false).then(function (res) {
               // console.log(res);
                $scope.addMessagePref.dropDowns = res;
            }, function (error) { });

        }
        initialize();

        $scope.addMessagePref.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        //handling LOS changes
        $scope.addMessagePref.changeInLOS = function ($event, option) {
            $event.preventDefault();
            $scope.addMessagePref.selected.msgPrefLOS = option.line_of_service_cd;
            $scope.addMessagePref.selected.msgPrefCommChannel = '';
            $scope.addMessagePref.selected.msgPrefCommType = '';
            $scope.addMessagePref.selected.msgPrefType = '';
            $scope.addMessagePref.selected.msgPrefValue = '';
        }
        //andling change in communication channel
        $scope.addMessagePref.changeInCommChannel = function ($event, option) {
            $event.preventDefault();
            $scope.addMessagePref.selected.msgPrefCommChannel = option.comm_chan;
           
            $scope.addMessagePref.selected.msgPrefType = '';
            $scope.addMessagePref.selected.msgPrefValue = '';
            $scope.addMessagePref.selected.msgPrefCommType = '';
        }

      
        //handling in change In Message Pref Type
        $scope.addMessagePref.changeInMessagePrefType = function ($event, option) {
            $event.preventDefault();
            $scope.addMessagePref.selected.msgPrefType = option.msg_prefc_typ;
            //console.log($scope.addMessagePref.selected.msgPrefType);
            $scope.addMessagePref.selected.msgPrefValue = '';
            $scope.addMessagePref.selected.msgPrefCommType = '';
        }

        //handling change in Message pref value
        $scope.addMessagePref.changeInMessagePrefValue = function ($event, option) {
            $event.preventDefault();

            //console.log($scope.addMessagePref.selected.msgPrefValue)

            $scope.addMessagePref.selected.msgPrefValue = option.msg_prefc_val;
            $scope.addMessagePref.selected.msgPrefCommType = '';
        }
        ////handling in change In CommType
        //$scope.addMessagePref.changeInCommType = function ($event, option) {
        //    $event.preventDefault();
        //    $scope.addMessagePref.selected.msgPrefCommType = option.comm_typ;
        //    // $scope.addMessagePref.selected.msgPrefType = '';
        //    //  $scope.addMessagePref.selected.msgPrefValue = '';
        //}

        $scope.addMessagePref.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.addMessagePref.format = $scope.addMessagePref.formats[0];

        $scope.addMessagePref.openExpirationDate = function () {
            $scope.addMessagePref.popup.opened = true;
        };
        $scope.addMessagePref.popup = {
            opened: false
        };


        $scope.addMessagePref.submit = function () {
            myApp.showPleaseWait();

            angular.forEach($scope.addMessagePref.dropDowns, function (v, k) {

                if(v.line_of_service_cd == $scope.addMessagePref.selected.msgPrefLOS && v.comm_chan == $scope.addMessagePref.selected.msgPrefCommChannel &&
                    v.msg_prefc_typ == $scope.addMessagePref.selected.msgPrefType && v.msg_prefc_val == $scope.addMessagePref.selected.msgPrefValue) {
                    $scope.addMessagePref.selected.msgPrefCommType = v.comm_typ;
                }

            });


            //console.log($scope.myForm);
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id : $scope.addMessagePref.masterId,
                    i_cnst_typ :  $sessionStorage.type,
                    i_case_num_seq : $scope.addMessagePref.caseNo,
                    i_notes : $scope.addMessagePref.selected.note,
                    //i_created_by : 
                    //i_user_id :
                    i_msg_pref_exp_dt : $scope.addMessagePref.expirationDate,
                    i_bk_arc_srcsys_cd : "",
                    i_bk_msg_pref_typ: "",
                    i_bk_msg_pref_val : "",
                    i_bk_msg_pref_comm_chan :"",
                    i_bk_msg_pref_comm_chan_typ : "",
                    i_bk_line_of_service : "",
                    i_new_arc_srcsys_cd : $scope.addMessagePref.sourceSys,
                    i_new_msg_pref_typ : $scope.addMessagePref.selected.msgPrefType,
                    i_new_msg_pref_val : $scope.addMessagePref.selected.msgPrefValue,
                    i_new_msg_pref_comm_chan :  $scope.addMessagePref.selected.msgPrefCommChannel,
                    i_new_msg_pref_comm_chan_typ : $scope.addMessagePref.selected.msgPrefCommType,
                    i_new_line_of_service: $scope.addMessagePref.selected.msgPrefLOS
                }
                

                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }



                ConstCemDataServices.postAddMessagePref(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });
            }
        }


        function validation(postParams) {
            if (postParams.i_new_line_of_service == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.i_new_msg_pref_comm_chan == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDATION_COMM_CHAN);
                return false;
            }
            if (postParams.i_new_msg_pref_typ == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDATION_TYPE);
                return false;
            }
            if (postParams.i_new_msg_pref_val == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_VALUE);
                return false;
            }

      
            if (postParams.i_new_msg_pref_comm_chan_typ == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.SELECT_CORRECT_COMBO);
                return false;
            }
            return true;
        }

}]);



//Edit Message Preference
angular.module('constituent').controller('EditMessagePreferenceCtrl',
    ['$scope', 'params', 'globalServices', 'ConstCemDataServices', '$sessionStorage', '$uibModalInstance','Message','CEM_CONSTANTS',
    function ($scope, params, globalServices, ConstCemDataServices, $sessionStorage, $uibModalInstance, Message, CEM_CONSTANTS) {
      //  console.log("edit Message");
       // console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };
            $scope.editMessagePref = {
                masterId: params.masterId,
                selected: {
                    msgPrefLOS: params.row.line_of_service_cd,
                    msgPrefCommChannel: params.row.comm_chan,
                    msgPrefCommType: params.row.comm_typ,
                    msgPrefType: params.row.msg_pref_typ,
                    msgPrefValue: params.row.msg_pref_val,
                    note: params.row.note
                },
                caseNo: caseNo,
                notes: CRUD_CONSTANTS.NOTES,
                sourceSys: params.row.arc_srcsys_cd,
                rowCode: params.row.row_stat_cd,
                constType: $sessionStorage.type,
                dropDowns: [],
                dateOptions: {
                    formatYear: 'yy',
                    maxDate: new Date(9999, 11, 31),
                    minDate: new Date(),
                    startingDay: 1
                },
                expirationDate: new Date(params.row.msg_pref_exp_dt === null ? "" : params.row.msg_pref_exp_dt)
            }

 
            ConstCemDataServices.getMessagePrefDropDowns($scope.editMessagePref.masterId, false).then(function (res) {
                // console.log(res);
                $scope.editMessagePref.dropDowns = res;
            }, function (error) { });

        }
        initialize();

        $scope.editMessagePref.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        //handling LOS changes
        $scope.editMessagePref.changeInLOS = function ($event, option) {
            $event.preventDefault();
            $scope.editMessagePref.selected.msgPrefLOS = option.line_of_service_cd;
            $scope.editMessagePref.selected.msgPrefCommChannel = '';
            $scope.editMessagePref.selected.msgPrefCommType = '';
            $scope.editMessagePref.selected.msgPrefType = '';
            $scope.editMessagePref.selected.msgPrefValue = '';
        }
        //andling change in communication channel
        $scope.editMessagePref.changeInCommChannel = function ($event, option) {
            $event.preventDefault();
            $scope.editMessagePref.selected.msgPrefCommChannel = option.comm_chan;
            $scope.editMessagePref.selected.msgPrefCommType = '';
            $scope.editMessagePref.selected.msgPrefType = '';
            $scope.editMessagePref.selected.msgPrefValue = '';
        }   

        //handling in change In Message Pref Type
        $scope.editMessagePref.changeInMessagePrefType = function ($event, option) {
            $event.preventDefault();
            $scope.editMessagePref.selected.msgPrefType = option.msg_prefc_typ;
            $scope.editMessagePref.selected.msgPrefValue = '';
            $scope.editMessagePref.selected.msgPrefCommType = '';
        }

        //handling change in Message pref value
        $scope.editMessagePref.changeInMessagePrefValue = function ($event, option) {
            $event.preventDefault();
            $scope.editMessagePref.selected.msgPrefValue = option.msg_prefc_val;
            $scope.editMessagePref.selected.msgPrefCommType = '';
        }

        $scope.editMessagePref.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.editMessagePref.format = $scope.editMessagePref.formats[0];

        $scope.editMessagePref.openExpirationDate = function () {
            $scope.editMessagePref.popup.opened = true;
        };
        $scope.editMessagePref.popup = {
            opened: false
        };


        $scope.editMessagePref.submit = function () {
            myApp.showPleaseWait();
            angular.forEach($scope.editMessagePref.dropDowns, function (v, k) {

                if (v.line_of_service_cd == $scope.editMessagePref.selected.msgPrefLOS && v.comm_chan == $scope.editMessagePref.selected.msgPrefCommChannel &&
                    v.msg_prefc_typ == $scope.editMessagePref.selected.msgPrefType && v.msg_prefc_val == $scope.editMessagePref.selected.msgPrefValue) {
                    $scope.editMessagePref.selected.msgPrefCommType = v.comm_typ;
                }

            });
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.editMessagePref.masterId,
                    i_cnst_typ: $sessionStorage.type,
                    i_case_num_seq: $scope.editMessagePref.caseNo,
                    i_notes: $scope.editMessagePref.selected.note,
                    //i_created_by : 
                    //i_user_id :
                    i_msg_pref_exp_dt: $scope.editMessagePref.expirationDate,
                    i_bk_arc_srcsys_cd: params.row.arc_srcsys_cd,
                    i_bk_msg_pref_typ: params.row.msg_pref_typ,
                    i_bk_msg_pref_val: params.row.msg_pref_val,
                    i_bk_msg_pref_comm_chan: params.row.comm_chan,
                    i_bk_msg_pref_comm_chan_typ: params.row.comm_typ,
                    i_bk_line_of_service: params.row.line_of_service_cd,
                    i_new_arc_srcsys_cd: $scope.editMessagePref.sourceSys,
                    i_new_msg_pref_typ: $scope.editMessagePref.selected.msgPrefType,
                    i_new_msg_pref_val: $scope.editMessagePref.selected.msgPrefValue,
                    i_new_msg_pref_comm_chan: $scope.editMessagePref.selected.msgPrefCommChannel,
                    i_new_msg_pref_comm_chan_typ: $scope.editMessagePref.selected.msgPrefCommType,
                    i_new_line_of_service: $scope.editMessagePref.selected.msgPrefLOS
                }
                // this is for validation
                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }



                ConstCemDataServices.postEditMessagePref(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });
            }
        }

        function validation(postParams) {
            if (postParams.i_new_line_of_service == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.i_new_msg_pref_comm_chan == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDATION_COMM_CHAN);
                return false;
            }        
            if (postParams.i_new_msg_pref_typ == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDATION_TYPE);
                return false;
            }
            if (postParams.i_new_msg_pref_val == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_VALUE);
                return false;
            }

            if (postParams.i_new_msg_pref_comm_chan_typ == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.SELECT_CORRECT_COMBO);
                return false;
            }
            return true;
        }

    }]);



//Delete Message Preference
angular.module('constituent').controller('DeleteMessagePreferenceCtrl',
    ['$scope', 'params', 'globalServices', 'ConstCemDataServices', '$sessionStorage', '$uibModalInstance',
    function ($scope, params, globalServices, ConstCemDataServices, $sessionStorage, $uibModalInstance) {
       // console.log("Delete Message");
        //console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };
            $scope.deleteMessagePref = {
                masterId: params.masterId,
                selected: {
                    msgPrefLOS: params.row.line_of_service_cd,
                    msgPrefCommChannel: params.row.comm_chan,
                    msgPrefCommType: params.row.comm_typ,
                    msgPrefType: params.row.msg_pref_typ,
                    msgPrefValue: params.row.msg_pref_val,
                    note: params.row.note
                },
                
                notes: CRUD_CONSTANTS.NOTES,
                sourceSys: params.row.arc_srcsys_cd,
                rowCode: params.row.row_stat_cd,
                constType: $sessionStorage.type,
                expirationDate: params.row.msg_pref_exp_dt
            }

        }
        initialize();

        $scope.deleteMessagePref.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

       
        $scope.deleteMessagePref.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.deleteMessagePref.masterId,
                    i_cnst_typ: $sessionStorage.type,
                    i_case_num_seq: "0",
                    i_notes: $scope.deleteMessagePref.selected.note,
                    //i_created_by : 
                    //i_user_id :
                    i_msg_pref_exp_dt: $scope.deleteMessagePref.expirationDate,
                    i_bk_arc_srcsys_cd: params.row.arc_srcsys_cd,
                    i_bk_msg_pref_typ: params.row.msg_pref_typ,
                    i_bk_msg_pref_val: params.row.msg_pref_val,
                    i_bk_msg_pref_comm_chan: params.row.comm_chan,
                    i_bk_msg_pref_comm_chan_typ: params.row.comm_typ,
                    i_bk_line_of_service: params.row.line_of_service_cd,
                    i_new_arc_srcsys_cd: params.row.arc_srcsys_cd,
                    i_new_msg_pref_typ: params.row.msg_pref_typ,
                    i_new_msg_pref_val: params.row.msg_pref_val,
                    i_new_msg_pref_comm_chan: params.row.comm_chan,
                    i_new_msg_pref_comm_chan_typ: params.row.comm_typ,
                    i_new_line_of_service: params.row.line_of_service_cd
                }

                ConstCemDataServices.postDeleteMessagePref(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });
            }
        }

    }]);









