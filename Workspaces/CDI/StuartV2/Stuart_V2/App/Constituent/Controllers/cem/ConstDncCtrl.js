angular.module('constituent').controller('ConstCemDncCtrl',
    ["$scope", "ConstCemDataServices", "ConstCemServices", "$stateParams", "StoreData", "uiGridConstants", "$rootScope", "DncModal", "CEM_CONSTANTS", "HandleResults", "ShowDetailsModal",
    function ($scope, ConstCemDataServices, ConstCemServices, $stateParams, StoreData, uiGridConstants, $rootScope, DncModal, CEM_CONSTANTS, HandleResults, ShowDetailsModal) {

    var initialize = function () {
        $scope.dnc = {
            columnDef: ConstCemDataServices.getDncColumns(),
            //gridOptions: 
            toggleDetails: false,
            togglePleaseWait: true,
            totalItems: 0,
            currentPage: 1,
            masterId: $stateParams.constituentId,
            distinctRecordCount: 0,           
            showAllRecords:{
                showAllName: CEM_CONSTANTS.SHOW_ALL_RECORDS,
                togglePleaseWait:false,
                toggleRecords: true
            },
            showDetails: {}
        }
        $scope.dnc.gridOptions = ConstCemServices.getGridOptions($scope.dnc.columnDef);
        //console.log("INitialized");
        $scope.dnc.gridOptions.onRegisterApi = function (gridApi) {
            //set gridApi on scope
            $scope.dnc.gridApi = gridApi;
        }

        // get the dropdowns in advance for add/edit
        ConstCemDataServices.getLocatorDropDowns($scope.dnc.masterId, false).then(function (res) {
           // console.log(res);
        });

        showDncDetails()
    }
    initialize();


    $scope.$on('dnc', function (event, args) {

        if (args.cem_dnc) {
            $scope.dnc.togglePleaseWait = true;
            showDncDetails();

        } else {
            $scope.dnc.togglePleaseWait = false;
            $scope.dnc.toggleDetails = false;
        }

    });

        // show details popup
    $scope.dnc.showDetails.allDetails = function () {
        $scope.dnc.togglePleaseWait = true;
        ConstCemDataServices.getAllDnc($scope.dnc.masterId).then(function (results) {
            $scope.dnc.togglePleaseWait = false;
            var params = {
                results: results,
                type: CEM_CONSTANTS.DNC_DETAILS
            };
            ShowDetailsModal(params).then(function (response) { });

        }, function (error) {
            $scope.dnc.togglePleaseWait = false
            HandleResults(error);
        });
    }

    // show all records
    $scope.dnc.showAllRecords.showAllrecords = function () {
        if ($scope.dnc.showAllRecords.toggleRecords) {
            $scope.dnc.showAllRecords.toggleRecords = !$scope.dnc.showAllRecords.toggleRecords;
            $scope.dnc.showAllRecords.showAllName = CEM_CONSTANTS.HIDE_RECORDS;

            $scope.dnc.gridOptions = ConstCemServices.refreshGridData($scope.dnc.gridOptions, StoreData.getDncFullList());
            $scope.dnc.totalItems = StoreData.getDncFullList().length;
            $scope.dnc.distinctRecordCount = StoreData.getDncFullList()[0].distinct_records_count;
        }
        else {
            $scope.dnc.showAllRecords.toggleRecords = !$scope.dnc.showAllRecords.toggleRecords;
            $scope.dnc.showAllRecords.showAllName = CEM_CONSTANTS.SHOW_ALL_RECORDS;

            $scope.dnc.gridOptions = ConstCemServices.refreshGridData($scope.dnc.gridOptions, StoreData.getDncList());
            $scope.dnc.totalItems = StoreData.getDncList().length;
            $scope.dnc.distinctRecordCount = StoreData.getDncList()[0].distinct_records_count;
        }

    }
      


    $scope.dnc.pageChanged = function (page) {
        $scope.dnc.gridApi.pagination.seek(page);
    }

        //filter for search results
    $rootScope.toggleDncSearchFilter = function () {
        //console.log("called DNC");
        $scope.dnc.gridOptions.enableFiltering = !$scope.dnc.gridOptions.enableFiltering;
        $scope.dnc.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }


        //add  dnc record
    $scope.dnc.addRecord = function () {
        var params = {
            masterId: $scope.dnc.masterId
        }
        DncModal.getModal(CEM_CONSTANTS.DNC_ADD, params).then(function (result) {
            //console.log(result);
            var bool = HandleResults(result, CEM_CONSTANTS.ADD);
            if (bool) {
                $scope.dnc.togglePleaseWait = true;
                $scope.dnc.toggleDetails = false;
                refreshGrid();
            }
        });
    }

        //edit record
    $scope.editGridRow = function (row, grid) {
        //console.log(row);
        var params = {
            masterId: $scope.dnc.masterId,
            row: row
        }
        DncModal.getModal(CEM_CONSTANTS.DNC_EDIT, params).then(function (result) {
            // console.log(result);
            var bool = HandleResults(result, CEM_CONSTANTS.EDIT);
            if (bool) {
                $scope.dnc.togglePleaseWait = true;
                $scope.dnc.toggleDetails = false;
                refreshGrid();
            }
        });
    }

        //Delete record
    $scope.deleteGridRow = function (row, grid) {
        //console.log(row);
        var params = {
            masterId: $scope.dnc.masterId,
            row: row
        }
        DncModal.getModal(CEM_CONSTANTS.DNC_DELETE, params).then(function (result) {
            var bool = HandleResults(result, CEM_CONSTANTS.DELETE);
            if (bool) {
                $scope.dnc.togglePleaseWait = true;
                $scope.dnc.toggleDetails = false;
                refreshGrid();
            }
        });
    }

       
        //showing records 
    function showDncDetails() {
        if (StoreData.getDncList().length <= 0) {
            refreshGrid();
        }
        else {
            var res = StoreData.getDncList();
            $scope.dnc.gridOptions = ConstCemServices.refreshGridData($scope.dnc.gridOptions, res);
            $scope.dnc.distinctRecordCount = res[0].distinct_records_count;
            $scope.dnc.togglePleaseWait = false;
            $scope.dnc.toggleDetails = true;
        }
    }

        //referesh grid after add/edit/delete operations
    function refreshGrid() {
        ConstCemDataServices.getDnc($scope.dnc.masterId, false)
        .then(function (result) {
            //console.log(result);
            StoreData.addDncList(result,filterConstituentData(result));
            $scope.dnc.gridOptions = ConstCemServices.refreshGridData($scope.dnc.gridOptions, StoreData.getDncList());
            $scope.dnc.togglePleaseWait = false;
            $scope.dnc.toggleDetails = true;
            $scope.dnc.totalItems = StoreData.getDncList().length;
            if (StoreData.getDncList().length > 0) {
                $scope.dnc.distinctRecordCount = StoreData.getDncList()[0].distinct_records_count;
            }
        },
        function (error) {
            $scope.dnc.togglePleaseWait = false;
            HandleResults(error);
        });
    }

    }]);


angular.module('constituent').controller("AddDncCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices", 'Message', 'CEM_CONSTANTS',
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices, Message, CEM_CONSTANTS) {

       // console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };


            $scope.addDnc = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    locatorLOS: "",
                    communicationCode: "",
                    communicationValue: "",
                    locatorIdCode: "",
                    locatorIdValue: ""
                },
                communicationTypes: getCommunicationTypes(),
                LOSs: getLOS(),
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: ConstUtilServices.getStartDate(),
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                locatorIds: [],
                toggleLocatorId: true,
                dateOptions: {
                    formatYear: 'yy',
                    maxDate: new Date(9999, 11, 31),
                    minDate: new Date(),
                    startingDay: 1
                },
                expirationDate : '12/31/9999'
            }

            ConstCemDataServices.getLocatorDropDowns($scope.addDnc.masterId, false).then(function (res) {
                $scope.addDnc.locatorIds = res;


            }, function (error) {
                //handle the error in service itself 
            });


        }
        initialize();



        $scope.addDnc.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.addDnc.format = $scope.addDnc.formats[0];

        $scope.addDnc.openExpirationDate = function () {
            $scope.addDnc.popup.opened = true;
        };
        $scope.addDnc.popup = {
            opened: false
        };

        $scope.addDnc.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.addDnc.setCommunicationType = function (type) {
            $scope.addDnc.selected.communicationValue = type.value;
            $scope.addDnc.selected.communicationCode = type.id;
            $scope.addDnc.selected.locatorIdValue = '';
            $scope.addDnc.selected.locatorIdCode = '';            
           // console.log($scope.addDnc.selected.communicationCode);
        }


        $scope.addDnc.submit = function () {

            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.addDnc.masterId,
                    i_cnst_typ: $scope.addDnc.constType,
                    i_notes: $scope.addDnc.selected.note,
                    //i_cnst_dnc_exp_ts: $scope.addDnc.expirationDate,
                    i_bk_cnst_dnc_line_of_service_cd: "",
                    i_bk_cnst_dnc_comm_chan: "",
                    i_bk_cnst_loc_id: "",
                    i_new_cnst_dnc_line_of_service_cd: $scope.addDnc.selected.locatorLOS,
                    i_new_cnst_dnc_comm_chan: $scope.addDnc.selected.communicationValue,
                    i_new_cnst_dnc_loc_id: $scope.addDnc.selected.locatorIdCode,
                    i_case_num_seq: $scope.addDnc.caseNo
                }

              //  console.log(postParams);
                // this is for validation
                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }

                ConstCemDataServices.postAddDnc(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }


        function validation(postParams) {
            if (postParams.i_new_cnst_dnc_line_of_service_cd == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.i_new_cnst_dnc_comm_chan == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDATION_COMM_CHAN);
                return false;
            }
          
            return true;
        }

    }]);


function getCommunicationTypes() {
    return [
        { id: "All", value: "All" },
        { id: "Email", value: "Email" },
        { id: "Phone", value: "Phone" },
        { id: "Mail", value: "Mail" },
        { id: "Phone", value: "Text" }
    ]
}

function titleCase(string) { return string.charAt(0).toUpperCase() + string.slice(1); } 


angular.module('constituent').controller("EditDncCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices",
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices) {

       // console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };

            $scope.editDnc = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    locatorLOS: params.row.line_of_service_cd,
                    communicationCode: "",
                    communicationValue: titleCase(params.row.comm_chan.toLowerCase()),
                    locatorIdCode: "",
                    locatorIdValue: ""
                },
                communicationTypes: getCommunicationTypes(), //email/text/mail/all
                LOSs: getLOS(),
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: params.row.cnst_dnc_strt_ts,
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                locatorIds: [],
                toggleLocatorId: true,
                dateOptions: {
                    formatYear: 'yy',
                    maxDate: new Date(9999, 11, 31),
                    minDate: new Date(),
                    startingDay: 1
                },
                
                expirationDate: '12/31/9999'
            }
           
            ConstCemDataServices.getLocatorDropDowns($scope.editDnc.masterId, false).then(function (res) {
                $scope.editDnc.locatorIds = res;

                angular.forEach($scope.editDnc.locatorIds, function (val, key) {
                    if (val.loc_id == params.row.locator_id) {
                        $scope.editDnc.selected.locatorIdCode = params.row.locator_id;
                        $scope.editDnc.selected.locatorIdValue = val.loc_value;
                        $scope.editDnc.selected.communicationValue = params.row.comm_chan;

                    }

                });

                //set the locator drop down according to the channel level 
                angular.forEach($scope.editDnc.communicationTypes, function (val, key) {
                    if ($scope.editDnc.selected.communicationValue == val.value) {
                        $scope.editDnc.selected.communicationCode = val.id;

                    }
                });

             
            }, function (error) {
                //handle the error in service itself 
            });

          

        }
        initialize();

        $scope.editDnc.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.editDnc.format = $scope.editDnc.formats[0];

        $scope.editDnc.openExpirationDate = function () {
            $scope.editDnc.popup.opened = true;
        };
        $scope.editDnc.popup = {
            opened: false
        };

        $scope.editDnc.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.editDnc.setCommunicationType = function (type) {
            //console.log(type);
            $scope.editDnc.selected.communicationValue = type.value;
            $scope.editDnc.selected.communicationCode = type.id;
            $scope.editDnc.selected.locatorIdValue = '';
            $scope.editDnc.selected.locatorIdCode = '';

        }

        $scope.editDnc.submit = function () {

            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.editDnc.masterId,
                    i_cnst_typ: $scope.editDnc.constType,
                    i_notes: $scope.editDnc.selected.note,
                    //i_cnst_dnc_exp_ts: $scope.editDnc.expirationDate,
                    i_bk_cnst_dnc_line_of_service_cd: params.row.line_of_service_cd,
                    i_bk_cnst_dnc_comm_chan: params.row.comm_chan,
                    i_bk_cnst_loc_id: params.row.locator_id,
                    i_new_cnst_dnc_line_of_service_cd: $scope.editDnc.selected.locatorLOS,
                    i_new_cnst_dnc_comm_chan: $scope.editDnc.selected.communicationValue,
                    i_new_cnst_dnc_loc_id: $scope.editDnc.selected.locatorIdCode,
                    i_case_num_seq: $scope.editDnc.caseNo
                }

               // console.log(postParams);
                // this is for validation
                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }
                ConstCemDataServices.postEditDnc(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }

        function validation(postParams) {
            if (postParams.i_new_cnst_dnc_line_of_service_cd == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.i_new_cnst_dnc_comm_chan == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDATION_COMM_CHAN);
                return false;
            }

            return true;
        }

    }]);


angular.module('constituent').controller("DeleteDncCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices",
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices) {

        //console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };

            $scope.deleteDnc = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    locatorLOS: params.row.line_of_service_cd,
                    communicationCode: "",
                    communicationValue: titleCase(params.row.comm_chan.toLowerCase()),
                    locatorIdCode: "",
                    locatorIdValue: ""
                },
                communicationTypes: getCommunicationTypes(), //email/text/mail/all
                LOSs: getLOS(),
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: params.row.cnst_dnc_strt_ts,
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                locatorIds: [],
                toggleLocatorId: true,
                expirationDate: '12/31/9999'
            }

            ConstCemDataServices.getLocatorDropDowns($scope.deleteDnc.masterId, false).then(function (res) {
                $scope.deleteDnc.locatorIds = res;

                angular.forEach($scope.deleteDnc.locatorIds, function (val, key) {
                    if (val.loc_id == params.row.locator_id) {
                        $scope.deleteDnc.selected.locatorIdCode = params.row.locator_id;
                        $scope.deleteDnc.selected.locatorIdValue = val.loc_value;
                        $scope.deleteDnc.selected.communicationValue = val.loc_type;
                        $scope.deleteDnc.selected.communicationCode = val.loc_type;
                    }

                });
            }, function (error) {
                //handle the error in service itself 
            });

            //set the locator drop down according to the channel level 
            angular.forEach($scope.deleteDnc.communicationTypes, function (val, key) {
                if ($scope.deleteDnc.selected.communicationValue == val.value) {
                    $scope.deleteDnc.selected.communicationCode = val.id;

                }
            });
        }
        initialize();

        $scope.deleteDnc.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.deleteDnc.submit = function () {

            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.deleteDnc.masterId,
                    i_cnst_typ: $scope.deleteDnc.constType,
                    i_notes: $scope.deleteDnc.selected.note,
                    i_bk_cnst_dnc_line_of_service_cd: params.row.line_of_service_cd,
                    i_bk_cnst_dnc_comm_chan: params.row.comm_chan,
                    i_bk_cnst_loc_id: params.row.locator_id,
                    i_new_cnst_dnc_line_of_service_cd: params.row.line_of_service_cd,
                    i_new_cnst_dnc_comm_chan: params.row.comm_chan,
                    i_new_cnst_dnc_loc_id: params.row.locator_id,
                    i_case_num_seq: $scope.deleteDnc.caseNo
                }

              //  console.log(postParams);

                ConstCemDataServices.postDeleteDnc(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }

    }]);


angular.module('constituent').controller("ShowCemCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal","ConstCemServices","CEM_CONSTANTS","CemGridColumn",
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstCemServices, CEM_CONSTANTS,CemGridColumn) {

       // console.log(params);
        var initialize = function () {
            $scope.showDetails = {
                toggleDetails: true,
                title: '',
                columnDef:''
            }

            if (params.type == CEM_CONSTANTS.DNC_DETAILS) {
                $scope.showDetails.title = "DNC";
                $scope.showDetails.columnDef = CemGridColumn.getDncAllColumns();
            }
            else if (params.type == CEM_CONSTANTS.MSG_PREF_DETAILS) {
                $scope.showDetails.title = "Message Preference";
                $scope.showDetails.columnDef = CemGridColumn.getMsgPrefAllColumns();
            }
            else if (params.type == CEM_CONSTANTS.PREF_LOC_DETAILS) {
                $scope.showDetails.title = "Preferred Locator";
                $scope.showDetails.columnDef = CemGridColumn.getPrefLocAllColumns();
            }
            else if (params.type == CEM_CONSTANTS.GRP_MEM_DETAILS) {
                $scope.showDetails.title = "Group Membership";
                $scope.showDetails.columnDef = CemGridColumn.getGrpMemAllColumns();
            }

            $scope.showDetails.gridOptions = ConstCemServices.getGridOptions($scope.showDetails.columnDef);
            //console.log("INitialized");
            $scope.showDetails.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.showDetails.gridApi = gridApi;
            }
            $scope.showDetails.gridOptions = ConstCemServices.refreshGridData($scope.showDetails.gridOptions, params.results);

        }
        initialize();


        $scope.showDetails.back = function () {
            $uibModalInstance.dismiss('cancel');
        }

    }]);