/*Preference locator*/
angular.module("constituent").controller("ConstCemPrefLocCtrl",
    ["$scope", "ConstCemDataServices", "ConstCemServices", "$stateParams", "StoreData", "uiGridConstants", "$rootScope",
        "PreferenceLocatorModal", "CEM_CONSTANTS", "HandleResults","ShowDetailsModal",
    function ($scope, ConstCemDataServices, ConstCemServices, $stateParams, StoreData, uiGridConstants, $rootScope,
    PreferenceLocatorModal, CEM_CONSTANTS, HandleResults, ShowDetailsModal) {

        var initialize = function () {
            $scope.prefLoc = {
                columnDef: ConstCemDataServices.getPrefLocatorColumns(),
                //gridOptions: 
                toggleDetails: false,
                togglePleaseWait: true,
                totalItems: 0,
                currentPage: 1,
                masterId: $stateParams.constituentId,
                distinctRecordCount: 0,
                showAllName: CEM_CONSTANTS.SHOW_ALL_RECORDS,
                toggleRecords:true,
                showDetails: {}

            }
            $scope.prefLoc.gridOptions = ConstCemServices.getGridOptions($scope.prefLoc.columnDef);
            //console.log("INitialized");
            $scope.prefLoc.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.prefLoc.gridApi = gridApi;
            }

            // get the dropdowns in advance for add/edit
            ConstCemDataServices.getLocatorDropDowns($scope.prefLoc.masterId, false).then(function (res) {
               // console.log(res);
            });

            showLocaPreferenceDetails();

        }
        initialize();


        $scope.$on('preference-locator', function (event, args) {

            if (args.cem_pref_locator) {
                $scope.prefLoc.togglePleaseWait = true;
                showLocaPreferenceDetails();

            } else {
                $scope.prefLoc.togglePleaseWait = false;
                $scope.prefLoc.toggleDetails = false;
            }

        });

        // show details popup
        $scope.prefLoc.showDetails.allDetails = function () {
            $scope.prefLoc.togglePleaseWait = true;
            ConstCemDataServices.getAllPrefLocator($scope.prefLoc.masterId).then(function (results) {
                $scope.prefLoc.togglePleaseWait = false;
                var params = {
                    results: results,
                    type: CEM_CONSTANTS.PREF_LOC_DETAILS
                };
                ShowDetailsModal(params).then(function (response) { });

            }, function (error) {
                $scope.prefLoc.togglePleaseWait = false
                HandleResults(error);
            });
        }

        // show all records
        $scope.prefLoc.showAllrecords = function () {
            if ($scope.prefLoc.toggleRecords) {
                $scope.prefLoc.toggleRecords = !$scope.prefLoc.toggleRecords;
                $scope.prefLoc.showAllName = CEM_CONSTANTS.HIDE_RECORDS;
                $scope.prefLoc.gridOptions = ConstCemServices.refreshGridData($scope.prefLoc.gridOptions, StoreData.getPrefLocatorFullList());
                $scope.prefLoc.totalItems = StoreData.getPrefLocatorFullList().length;
                $scope.prefLoc.distinctRecordCount = StoreData.getPrefLocatorFullList()[0].distinct_records_count;
            }
            else {
                $scope.prefLoc.toggleRecords = !$scope.prefLoc.toggleRecords;
                $scope.prefLoc.showAllName = CEM_CONSTANTS.SHOW_ALL_RECORDS;
                $scope.prefLoc.gridOptions = ConstCemServices.refreshGridData($scope.prefLoc.gridOptions, StoreData.getPrefLocatorList());
                $scope.prefLoc.totalItems = StoreData.getPrefLocatorList().length;
                $scope.prefLoc.distinctRecordCount = StoreData.getPrefLocatorList()[0].distinct_records_count;
            }
        }

        $scope.prefLoc.pageChanged = function (page) {
            $scope.prefLoc.gridApi.pagination.seek(page);
        }

        //filter for search results
        $rootScope.togglePrefLocSearchFilter = function () {
            $scope.prefLoc.gridOptions.enableFiltering = !$scope.prefLoc.gridOptions.enableFiltering;
            $scope.prefLoc.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }

        //add locator preference record
        $scope.prefLoc.addRecord = function () {
            var params = {
                masterId: $scope.prefLoc.masterId
            }
            PreferenceLocatorModal.getModal(CEM_CONSTANTS.PREFERENCE_LOCATOR_ADD, params).then(function (result) {
                var bool = HandleResults(result, CEM_CONSTANTS.ADD);
                if (bool) {
                    $scope.prefLoc.togglePleaseWait = true;
                    $scope.prefLoc.toggleDetails = false;
                    refeshGrid();
                }                                   
            });
        }
        //edit record
        $scope.editGridRow = function (row, grid) {
            //console.log(row);
            var params = {
                masterId: $scope.prefLoc.masterId,
                row: row
            }
            PreferenceLocatorModal.getModal(CEM_CONSTANTS.PREFERENCE_LOCATOR_EDIT, params).then(function (result) {
                var bool = HandleResults(result, CEM_CONSTANTS.EDIT);
                if (bool) {
                    $scope.prefLoc.togglePleaseWait = true;
                    $scope.prefLoc.toggleDetails = false;
                    refeshGrid();
                }
            });
        }

        //Delete record
        $scope.deleteGridRow = function (row, grid) {
            //console.log(row);
            var params = {
                masterId: $scope.prefLoc.masterId,
                row: row
            }
            PreferenceLocatorModal.getModal(CEM_CONSTANTS.PREFERENCE_LOCATOR_DELETE, params).then(function (result) {
                var bool = HandleResults(result, CEM_CONSTANTS.DELETE);
                if (bool) {
                    $scope.prefLoc.togglePleaseWait = true;
                    $scope.prefLoc.toggleDetails = false;
                    refeshGrid();
                }
            });
        }


        //referesh grid after add/edit/delete operations
        function refeshGrid() {
            ConstCemDataServices.getPrefLocator($scope.prefLoc.masterId, false)
                .then(function (result) {
                    StoreData.addPrefLocatorList(result, filterConstituentData(result));
                    $scope.prefLoc.gridOptions = ConstCemServices.refreshGridData($scope.prefLoc.gridOptions, StoreData.getPrefLocatorList());
                    $scope.prefLoc.togglePleaseWait = false;
                    $scope.prefLoc.toggleDetails = true;
                    $scope.prefLoc.totalItems = StoreData.getPrefLocatorList().length;
                    if (StoreData.getPrefLocatorList().length > 0) {
                        $scope.prefLoc.distinctRecordCount = StoreData.getPrefLocatorList()[0].distinct_records_count;
                    }
                   
                }, function (error) {
                    $scope.prefLoc.togglePleaseWait = false;
                    HandleResults(error);
                });
        }
        //showing records 
        function showLocaPreferenceDetails() {
            if (StoreData.getPrefLocatorList().length <= 0) {
                refeshGrid();
            }
            else {
                //console.log(ConstCemDataServices.getStoredPrefLocator());
                var res = StoreData.getPrefLocatorList();
                $scope.prefLoc.gridOptions = ConstCemServices.refreshGridData($scope.prefLoc.gridOptions, res);
                $scope.prefLoc.distinctRecordCount = res[0].distinct_records_count;
                $scope.prefLoc.togglePleaseWait = false;
                $scope.prefLoc.toggleDetails = true;
            }
        }

    }]);


function getLOS() {
    return [
        { id: "All", value: "All" },
        { id: "Bio", value: "Bio" },
        { id: "PHSS", value: "PHSS" },
        { id: "FR", value: "FR" }
    ]
}

function getLocatorTypes() {
    return [
        { id: "All", value: "All" },
        { id: "Email", value: "Email" },
        { id: "Phone", value: "Phone" },
        { id: "Mail", value: "Mail" },
        { id: "Phone", value: "Text" }
    ]
}

angular.module('constituent').controller("AddPreferenceLocatorCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices", 'Message', 'CEM_CONSTANTS',
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices,Message, CEM_CONSTANTS) {

        console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };


            $scope.addPrefLocator = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    prefLocatorLOS: "",
                    prefLocatorTypeCode: "",
                    prefLocatorTypeValue: "",
                    prefLocatorIdCode: "",
                    prefLocatorIdValue: ""
                },
                prefLocatorTypes: getLocatorTypes(),
                prefLOSs: getLOS(),
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: ConstUtilServices.getStartDate(),
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                prefLocatorIds: [],
               
            }

            ConstCemDataServices.getLocatorDropDowns($scope.addPrefLocator.masterId, false).then(function (res) {
                $scope.addPrefLocator.prefLocatorIds = res;


            }, function (error) {
                //handle the error in service itself 
            });

        }
        initialize();


        $scope.addPrefLocator.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.addPrefLocator.setLocatorType = function (type) {
            $scope.addPrefLocator.selected.prefLocatorTypeValue = type.value;
            $scope.addPrefLocator.selected.prefLocatorTypeCode = type.id;
            $scope.addPrefLocator.selected.prefLocatorIdValue = '';
            $scope.addPrefLocator.selected.prefLocatorIdCode = '';
            
        }


        $scope.addPrefLocator.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                if ($scope.addPrefLocator.selected.prefLocatorTypeCode.toLowerCase() == 'all') {
                    $scope.addPrefLocator.selected.prefLocatorIdCode = "0";
                }
                if ($scope.addPrefLocator.selected.prefLocatorTypeValue.toLowerCase() == "text") {
                    $scope.addPrefLocator.selected.prefLocatorTypeCode = "Text";
                }

                var postParams = {
                    masterId: $scope.addPrefLocator.masterId,
                    constituentType: $scope.addPrefLocator.constType,
                    lineOfService: $scope.addPrefLocator.selected.prefLocatorLOS,
                    notes: $scope.addPrefLocator.notes,
                    //createdBy:"",
                    //userId:"",
                    oldSourceSystemCode: "",
                    oldPrefLocType: "",
                    oldPrefLocId: "",
                    newSourceSystemCode: $scope.addPrefLocator.sourceSys,
                    newPrefLocType: $scope.addPrefLocator.selected.prefLocatorTypeCode,
                    newPrefLocId: $scope.addPrefLocator.selected.prefLocatorIdCode,
                    caseNo: $scope.addPrefLocator.caseNo
                }
                // this is for validation
                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }
                ConstCemDataServices.postAddPrefLocator(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }

        function validation(postParams) {
            if (postParams.lineOfService == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.newPrefLocType == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.PREFERRED_LOC_VALIDATION_LOC_TYPE);
                return false;
            }
            if (postParams.newPrefLocId == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.PREFERRED_LOC_VALIDATION_LOC);
                return false;
            }
           
            return true;
        }

    }]);

angular.module('constituent').controller("EditPreferenceLocatorCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices",'Message','CEM_CONSTANTS',
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices, Message, CEM_CONSTANTS) {
       // console.log("Edit");
       // console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };
            $scope.editPrefLocator = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    prefLocatorLOS: params.row.line_of_service_cd,
                    prefLocatorTypeCode: "",
                    prefLocatorTypeValue: "",
                    prefLocatorIdCode: "",
                    prefLocatorIdValue: ""
                },
                prefLocatorTypes: getLocatorTypes(),
                prefLOSs: getLOS(),
                masterId: params.masterId,
                sourceSys: params.row.arc_srcsys_cd,
                startDate: params.row.cnst_pref_loc_strt_ts,
                endDate: params.row.cnst_pref_loc_end_ts,
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                prefLocatorIds: [],
               
            }

            ConstCemDataServices.getLocatorDropDowns($scope.editPrefLocator.masterId, false).then(function (res) {
                $scope.editPrefLocator.prefLocatorIds = res;
                angular.forEach($scope.editPrefLocator.prefLocatorIds, function (val, key) {
                    if (val.loc_id == params.row.pref_loc_id) {
                        $scope.editPrefLocator.selected.prefLocatorIdCode = params.row.pref_loc_id;
                        $scope.editPrefLocator.selected.prefLocatorIdValue = val.loc_value;
                        $scope.editPrefLocator.selected.prefLocatorTypeValue = params.row.pref_loc_typ;

                    }

                });

                angular.forEach($scope.editPrefLocator.prefLocatorTypes, function (val, key) {
                    if ($scope.editPrefLocator.selected.prefLocatorTypeValue == val.value) {
                        $scope.editPrefLocator.selected.prefLocatorTypeCode = val.id;
                        //console.log("TYTT");
                       // console.log($scope.editPrefLocator.selected.prefLocatorTypeCode);
                    }

                });

            }, function (error) {//handle the error in service itself 
            });

            // console.log($scope.editPrefLocator);
        }
        initialize();


        $scope.editPrefLocator.setLocatorType = function (type) {
            $scope.editPrefLocator.selected.prefLocatorTypeValue = type.value;
            $scope.editPrefLocator.selected.prefLocatorTypeCode = type.id;
            $scope.editPrefLocator.selected.prefLocatorIdValue = '';
            $scope.editPrefLocator.selected.prefLocatorIdCode = '';
           
        }

        $scope.editPrefLocator.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.editPrefLocator.submit = function () {
            myApp.showPleaseWait();
            if ($scope.editPrefLocator.selected.prefLocatorTypeCode.toLowerCase() == 'all') {
                $scope.editPrefLocator.selected.prefLocatorIdCode = "0";
            }
            if ($scope.editPrefLocator.selected.prefLocatorTypeValue.toLowerCase() == "text") {
                $scope.editPrefLocator.selected.prefLocatorTypeCode = "Text";
            }
            if ($scope.myForm.$valid) {
                var postParams = {
                    masterId: $scope.editPrefLocator.masterId,
                    constituentType: $scope.editPrefLocator.constType,
                    lineOfService: $scope.editPrefLocator.selected.prefLocatorLOS,
                    notes: $scope.editPrefLocator.notes,
                    //createdBy:"",
                    //userId:"",
                    oldSourceSystemCode: params.row.arc_srcsys_cd,
                    oldPrefLocType: params.row.pref_loc_typ,
                    oldPrefLocId: params.row.pref_loc_id,
                    newSourceSystemCode: $scope.editPrefLocator.sourceSys,
                    newPrefLocType: $scope.editPrefLocator.selected.prefLocatorTypeCode,
                    newPrefLocId: $scope.editPrefLocator.selected.prefLocatorIdCode,
                    caseNo: $scope.editPrefLocator.caseNo
                }
               // console.log(postParams);
                // this is for validation
                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }
                ConstCemDataServices.postEditPrefLocator(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }
        function validation(postParams) {
            if (postParams.lineOfService == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.newPrefLocType == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.PREFERRED_LOC_VALIDATION_LOC_TYPE);
                return false;
            }
            if (postParams.newPrefLocId == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.PREFERRED_LOC_VALIDATION_LOC);
                return false;
            }

            return true;
        }

    }]);


angular.module('constituent').controller("DeletePreferenceLocatorCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices" ,
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices) {
       // console.log("delete");
        //console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };
            $scope.deletePrefLocator = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    prefLocatorLOS: params.row.line_of_service_cd,
                    prefLocatorTypeCode: "",
                    prefLocatorTypeValue: "",
                    prefLocatorIdCode: "",
                    prefLocatorIdValue: ""
                },
                prefLocatorTypes: getLocatorTypes(),
                prefLOSs: getLOS(),
                masterId: params.masterId,
                sourceSys: params.row.arc_srcsys_cd,
                startDate: params.row.cnst_pref_loc_strt_ts,
                endDate: params.row.cnst_pref_loc_end_ts,
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                prefLocatorIds: [],
               
            }

            ConstCemDataServices.getLocatorDropDowns($scope.deletePrefLocator.masterId, false).then(function (res) {
                $scope.deletePrefLocator.prefLocatorIds = res;
                angular.forEach($scope.deletePrefLocator.prefLocatorIds, function (val, key) {
                    if (val.loc_id == params.row.pref_loc_id) {
                        $scope.deletePrefLocator.selected.prefLocatorIdCode = params.row.pref_loc_id;
                        $scope.deletePrefLocator.selected.prefLocatorIdValue = val.loc_value;
                        $scope.deletePrefLocator.selected.prefLocatorTypeValue = val.loc_type;

                    }

                });

                angular.forEach($scope.deletePrefLocator.prefLocatorTypes, function (val, key) {
                    if ($scope.deletePrefLocator.selected.prefLocatorTypeValue == val.value) {
                        $scope.deletePrefLocator.selected.prefLocatorIdCode = val.id;
                    }

                });

            }, function (error) {//handle the error in service itself 
            });

            // console.log($scope.deletePrefLocator);
        }
        initialize();


        $scope.deletePrefLocator.setLocatorType = function (type) {
            $scope.deletePrefLocator.selected.prefLocatorTypeValue = type.value;
            $scope.deletePrefLocator.selected.prefLocatorTypeCode = type.id;
            $scope.deletePrefLocator.selected.prefLocatorIdValue = '';
            $scope.deletePrefLocator.selected.prefLocatorIdCode = '';
            
        }

        $scope.deletePrefLocator.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.deletePrefLocator.submit = function () {
            myApp.showPleaseWait();
            if ($scope.deletePrefLocator.selected.prefLocatorTypeCode == 'all') {
                $scope.deletePrefLocator.selected.prefLocatorIdCode = "0"
            }
            if ($scope.myForm.$valid) {
                var postParams = {
                    masterId: $scope.deletePrefLocator.masterId,
                    constituentType: $scope.deletePrefLocator.constType,
                    lineOfService: $scope.deletePrefLocator.selected.prefLocatorLOS,
                    notes: $scope.deletePrefLocator.notes,
                    //createdBy:"",
                    //userId:"",
                    oldSourceSystemCode: params.row.arc_srcsys_cd,
                    oldPrefLocType: params.row.pref_loc_typ,
                    oldPrefLocId: params.row.pref_loc_id,
                    newSourceSystemCode: $scope.deletePrefLocator.sourceSys,
                    newPrefLocType: $scope.deletePrefLocator.selected.prefLocatorTypeCode,
                    newPrefLocId: $scope.deletePrefLocator.selected.prefLocatorIdCode,
                    caseNo: $scope.deletePrefLocator.caseNo
                }
               // console.log(postParams);
                ConstCemDataServices.postDeletePrefLocator(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }


    }]);