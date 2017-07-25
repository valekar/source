angular.module('constituent').controller('ConstGrpMembrshipCtrl',
    ['$scope', '$stateParams', 'ConstCemServices', 'ConstCemDataServices', 'StoreData', '$rootScope',
        'GrpMembrshpModal', 'CEM_CONSTANTS', 'Message', 'HandleResults', 'ShowDetailsModal','uiGridConstants',
    function ($scope, $stateParams, ConstCemServices, ConstCemDataServices, StoreData, $rootScope,
    GrpMembrshpModal, CEM_CONSTANTS, Message, HandleResults, ShowDetailsModal,uiGridConstants) {
    var initialize = function () {
        $scope.grpMembrshp = {
            columnDef: ConstCemDataServices.getGrpMembrshpColumns(),
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
        $scope.grpMembrshp.gridOptions = ConstCemServices.getGridOptions($scope.grpMembrshp.columnDef);
        //console.log("INitialized");
        $scope.grpMembrshp.gridOptions.onRegisterApi = function (gridApi) {
            //set gridApi on scope
            $scope.grpMembrshp.gridApi = gridApi;
        }

        // get the dropdowns in advance for add/edit
        ConstCemDataServices.getLocatorDropDowns($scope.grpMembrshp.masterId, false).then(function (res) {
         //   console.log(res);
        });

        showGrpMembrshipDetails()
    }
    initialize();


    $scope.$on('grpMembrshp', function (event, args) {

        if (args.cem_grpMembrshp) {
            $scope.grpMembrshp.togglePleaseWait = true;
            showGrpMembrshipDetails();

        } else {
            $scope.grpMembrshp.togglePleaseWait = false;
            $scope.grpMembrshp.toggleDetails = false;
        }

    });

        // show details popup
    $scope.grpMembrshp.showDetails.allDetails = function () {
        $scope.grpMembrshp.togglePleaseWait = true;
        ConstCemDataServices.getAllGroupMembership($scope.grpMembrshp.masterId).then(function (results) {
            $scope.grpMembrshp.togglePleaseWait = false;
            var params = {
                results: results,
                type: CEM_CONSTANTS.GRP_MEM_DETAILS
            };
            ShowDetailsModal(params).then(function (response) { });

        }, function (error) {
            $scope.grpMembrshp.togglePleaseWait = false
            HandleResults(error);
        });
    }

        // show all records
    $scope.grpMembrshp.showAllrecords = function () {
        if ($scope.grpMembrshp.toggleRecords) {
            $scope.grpMembrshp.toggleRecords = !$scope.grpMembrshp.toggleRecords;
            $scope.grpMembrshp.showAllName = CEM_CONSTANTS.HIDE_RECORDS;
            $scope.grpMembrshp.gridOptions = ConstCemServices.refreshGridData($scope.grpMembrshp.gridOptions, StoreData.getGroupMembershipFullList());
            $scope.grpMembrshp.totalItems = StoreData.getGroupMembershipFullList().length;
            $scope.grpMembrshp.distinctRecordCount = StoreData.getGroupMembershipFullList()[0].distinct_records_count;
        }
        else {
            $scope.grpMembrshp.toggleRecords = !$scope.grpMembrshp.toggleRecords;
            $scope.grpMembrshp.showAllName = CEM_CONSTANTS.SHOW_ALL_RECORDS;
            $scope.grpMembrshp.gridOptions = ConstCemServices.refreshGridData($scope.grpMembrshp.gridOptions, StoreData.getGroupMembershipList());
            $scope.grpMembrshp.totalItems = StoreData.getGroupMembershipList().length;
            $scope.grpMembrshp.distinctRecordCount = StoreData.getGroupMembershipList()[0].distinct_records_count;
        }
    }

    $scope.grpMembrshp.pageChanged = function (page) {
        $scope.grpMembrshp.gridApi.pagination.seek(page);
    }

        //filter for search results
    $rootScope.toggleGrpMemSearchFilter = function () {
        $scope.grpMembrshp.gridOptions.enableFiltering = !$scope.grpMembrshp.gridOptions.enableFiltering;
        $scope.grpMembrshp.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }



        //add  grpMembrshp record
    $scope.grpMembrshp.addRecord = function () {
        var params = {
            masterId: $scope.grpMembrshp.masterId
        }
        GrpMembrshpModal.getModal(CEM_CONSTANTS.GROUP_MEMBERSHIP_ADD, params).then(function (result) {
            //console.log(result);
            var bool = HandleResults(result, CEM_CONSTANTS.ADD);
            if (bool) {
                $scope.grpMembrshp.togglePleaseWait = true;
                $scope.grpMembrshp.toggleDetails = false;
                refeshGrid();
            }
        });
    }

        //edit record
    $scope.editGridRow = function (row, grid) {
        //console.log(row);
        var params = {
            masterId: $scope.grpMembrshp.masterId,
            row: row
        }
        GrpMembrshpModal.getModal(CEM_CONSTANTS.GROUP_MEMBERSHIP_EDIT, params).then(function (result) {
            // console.log(result);
            var bool = HandleResults(result, CEM_CONSTANTS.EDIT);
            if (bool) {
                $scope.grpMembrshp.togglePleaseWait = true;
                $scope.grpMembrshp.toggleDetails = false;
                refeshGrid();
            }
        });
    }

        //Delete record
    $scope.deleteGridRow = function (row, grid) {
        //console.log(row);
        var params = {
            masterId: $scope.grpMembrshp.masterId,
            row: row
        }
        GrpMembrshpModal.getModal(CEM_CONSTANTS.GROUP_MEMBERSHIP_DELETE, params).then(function (result) {
            // console.log(result);
            var bool = HandleResults(result, CEM_CONSTANTS.DELETE);
            if (bool) {
                $scope.grpMembrshp.togglePleaseWait = true;
                $scope.grpMembrshp.toggleDetails = false;
                refeshGrid();
            }

        });
    }

        //referesh grid after add/edit/delete operations
    function refeshGrid() {
        ConstCemDataServices.getGroupMembership($scope.grpMembrshp.masterId, false)
            .then(function (result) {
                StoreData.addGroupMembershipList(result, filterConstituentData(result));
                $scope.grpMembrshp.gridOptions = ConstCemServices.refreshGridData($scope.grpMembrshp.gridOptions, StoreData.getGroupMembershipList());
                $scope.grpMembrshp.togglePleaseWait = false;
                $scope.grpMembrshp.toggleDetails = true;
                if (StoreData.getGroupMembershipList().length > 0) {
                    $scope.grpMembrshp.distinctRecordCount = StoreData.getGroupMembershipList()[0].distinct_records_count;
                }
                $scope.grpMembrshp.totalItems = StoreData.getGroupMembershipList().length;
                
            }, function (error) {
                $scope.grpMembrshp.togglePleaseWait = false
                HandleResults(error);
            });
    }
    function showGrpMembrshipDetails() {
        if (StoreData.getGroupMembershipList().length <= 0) {
            refeshGrid()
            }
        else {

            var res = StoreData.getGroupMembershipList();
            $scope.grpMembrshp.gridOptions = ConstCemServices.refreshGridData($scope.grpMembrshp.gridOptions, res);
            $scope.grpMembrshp.distinctRecordCount = res[0].distinct_records_count;
            $scope.grpMembrshp.togglePleaseWait = false;
            $scope.grpMembrshp.toggleDetails = true;
        }
    }


   

    }]);


angular.module('constituent').controller("AddGrpMembrshpCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices",
        "Custom_Date", "dropDownService","Message","CEM_CONSTANTS", "$filter",
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices,
        ConstCemDataServices, Custom_Date, dropDownService, Message, CEM_CONSTANTS,$filter) {

      //  console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };
            

            $scope.addGrpMembrshp = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    groupLOS: "",
                    //groupName: "",
                   // groupNameCode: ""
                },
                LOSs: getLOS(),
                groupNames: [],
                assignmentMethod: "Constituent Maintenance",
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: ConstUtilServices.getStartDate(),
                effStartDate: Custom_Date.current(),
                effEndDate: new Date(9999, 11, 31),
                rowCode: CRUD_CONSTANTS.ROW_CODE,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                dateOptions: {
                    formatYear: 'yy',
                    maxDate: new Date(9999, 11, 31),
                    minDate: new Date(),
                    startingDay: 1
                },
                toggleInvalidInput: false,
                invalidInput: "** Group Membership End Effective Date cannot be less than the Start Effective Date"

            }

            dropDownService.getDropDown(HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result)
            { $scope.addGrpMembrshp.groupNames = result; }).error(function (result) { });

        }
        initialize();


        // added by Srini
       /* $scope.addGrpMembrshp.GrpFilter = function (item) {
            //var matcher = item.value.match(/[^\_0-9](.?)+$/);
            var matcher = item.value.match(/([a-zA-Z])+\w/);
            return matcher;
        }
        $scope.addGrpMembrshp.GroupNameTxtbox = function () {
            $scope.addGrpMembrshp.groupNamedropdown = true;
        }

        $scope.addGrpMembrshp.getGroupName = function (SelectedDataid, SelectedDatavalue) {
            $scope.addGrpMembrshp.selected.groupName = SelectedDatavalue;
            $scope.addGrpMembrshp.groupNamedropdown = false;
            $scope.addGrpMembrshp.selected.groupNameCode = SelectedDataid;
        }*/
        //end of adding


       // $scope.addGrpMembrshp.effStartDate = $filter('custom_current_date');

        $scope.addGrpMembrshp.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.addGrpMembrshp.format = $scope.addGrpMembrshp.formats[0];

        $scope.addGrpMembrshp.openEffStartDate = function () {
            $scope.addGrpMembrshp.EffStartDatePopup.opened = true;
        };
        $scope.addGrpMembrshp.EffStartDatePopup = {
            opened: false
        };

        $scope.addGrpMembrshp.openEffEndDate = function () {
            $scope.addGrpMembrshp.EffEndDatePopup.opened = true;
        };
        $scope.addGrpMembrshp.EffEndDatePopup = {
            opened: false
        };

        $scope.addGrpMembrshp.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.addGrpMembrshp.compareAgainstStartDate = function (item) {
            var startDate = new Date($filter('date')(new Date($scope.addGrpMembrshp.effStartDate), 'MM/dd/yyyy'));
            var endDate = new Date($filter('date')(new Date($scope.addGrpMembrshp.effEndDate), 'MM/dd/yyyy'));

            if (endDate < startDate) {
                $scope.addGrpMembrshp.toggleInvalidInput = true;
            }
            else
                $scope.addGrpMembrshp.toggleInvalidInput = false;

            return $scope.addGrpMembrshp.toggleInvalidInput;
        };

        $scope.addGrpMembrshp.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id : $scope.addGrpMembrshp.masterId,
                    i_arc_srcsys_cd: $scope.addGrpMembrshp.sourceSys,
                    i_grp_mbrshp_eff_strt_dt: $scope.addGrpMembrshp.effStartDate,
                    i_grp_mbrshp_eff_end_dt: $scope.addGrpMembrshp.effEndDate,
                    i_cnst_typ: $scope.addGrpMembrshp.constType,
                    i_notes: $scope.addGrpMembrshp.selected.note,
                    i_case_seq_num: $scope.addGrpMembrshp.caseNo,
                    i_bk_group_key : "",
                    i_bk_assgnmnt_mthd : "",
                    i_bk_line_of_service_cd: "",
                    i_bk_arc_srcsys_cd: "",
                    i_new_group_key: $scope.addGrpMembrshp.grp.id,
                    i_new_assgnmnt_mthd: $scope.addGrpMembrshp.assignmentMethod,
                    i_new_line_of_service_cd: $scope.addGrpMembrshp.selected.groupLOS,
                    i_new_arc_srcsys_cd: $scope.addGrpMembrshp.sourceSys,
                }

                //console.log(postParams);

                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }
                ConstCemDataServices.postAddGroupMembership(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }


        function validation(postParams) {
            if (postParams.i_new_line_of_service_cd == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.i_new_group_key == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.GROUP_MEM_VALIDATION_GROUP_NAME);
                return false;
            }
           
            return true;
        }

    }]);





angular.module('constituent').controller("EditGrpMembrshpCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices",
        "Custom_Date", "dropDownService", "Message","$filter",
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices, Custom_Date, dropDownService, Message, $filter) {

      //  console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };


            $scope.editGrpMembrshp = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    groupLOS: params.row.line_of_service_cd,
                  //  groupName: "",
                   // groupNameCode: ""
                },
                LOSs: getLOS(),
                groupNames: [],
                assignmentMethod: params.row.assgnmnt_mthd,
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: params.row.grp_strt_ts,
                effStartDate: new Date(params.row.grp_mbrshp_eff_strt_dt),
                effEndDate: new Date(params.row.grp_mbrshp_eff_end_dt),
                rowCode: params.row.strx_row_stat_cd,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES,
                dateOptions: {
                    formatYear: 'yy',
                    maxDate: new Date(9999, 11, 31),
                    minDate: new Date(params.row.grp_mbrshp_eff_strt_dt),
                    startingDay: 1
                },
                toggleInvalidInput: false,
                invalidInput: "** Group Membership End Effective Date cannot be less than the Start Effective Date"
            }

            dropDownService.getDropDown(HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result)
            {
                $scope.editGrpMembrshp.groupNames = result;

                angular.forEach(result, function (v, k) {
                    if (v.id == params.row.grp_key) {
                       // $scope.editGrpMembrshp.selected.groupNameCode = v.id;
                        // $scope.editGrpMembrshp.selected.groupName = v.value;

                        $scope.editGrpMembrshp.grp = v;
                    }
                });
                

            }).error(function (result) { });

        }
        initialize();
    

        // added by Srini
       /* $scope.editGrpMembrshp.GrpFilter = function (item) {
            //var matcher = item.value.match(/[^\_0-9](.?)+$/);
            var matcher = item.value.match(/([a-zA-Z])+\w/);
            return matcher;
        }
        $scope.editGrpMembrshp.GroupNameTxtbox = function () {
            $scope.editGrpMembrshp.groupNamedropdown = true;
        }

        $scope.editGrpMembrshp.getGroupName = function (SelectedDataid, SelectedDatavalue) {
            $scope.editGrpMembrshp.selected.groupName = SelectedDatavalue;
            $scope.editGrpMembrshp.groupNamedropdown = false;
            $scope.editGrpMembrshp.selected.groupNameCode = SelectedDataid;
        }*/
        //end of adding


        $scope.editGrpMembrshp.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.editGrpMembrshp.format = $scope.editGrpMembrshp.formats[0];

        $scope.editGrpMembrshp.openEffStartDate = function () {
            $scope.editGrpMembrshp.EffStartDatePopup.opened = true;
        };
        $scope.editGrpMembrshp.EffStartDatePopup = {
            opened: false
        };

        $scope.editGrpMembrshp.openEffEndDate = function () {
            $scope.editGrpMembrshp.EffEndDatePopup.opened = true;
        };
        $scope.editGrpMembrshp.EffEndDatePopup = {
            opened: false
        };

        $scope.editGrpMembrshp.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.editGrpMembrshp.compareAgainstStartDate = function (item) {
            var startDate = new Date($filter('date')(new Date($scope.editGrpMembrshp.effStartDate), 'MM/dd/yyyy'));
            var endDate = new Date($filter('date')(new Date($scope.editGrpMembrshp.effEndDate), 'MM/dd/yyyy'));

            if (endDate < startDate) {
                $scope.editGrpMembrshp.toggleInvalidInput = true;
            }
            else
                $scope.editGrpMembrshp.toggleInvalidInput = false;

            return $scope.editGrpMembrshp.toggleInvalidInput;
        };


        $scope.editGrpMembrshp.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.editGrpMembrshp.masterId,
                    i_grp_mbrshp_eff_strt_dt: $scope.editGrpMembrshp.effStartDate,
                    i_grp_mbrshp_eff_end_dt: $scope.editGrpMembrshp.effEndDate,
                    i_cnst_typ: $scope.editGrpMembrshp.constType,
                    i_notes: $scope.editGrpMembrshp.selected.note,
                    i_case_seq_num: $scope.editGrpMembrshp.caseNo,
                    i_bk_group_key: params.row.grp_key,
                    i_bk_assgnmnt_mthd: params.row.assgnmnt_mthd,
                    i_bk_line_of_service_cd: params.row.line_of_service_cd,
                    i_bk_arc_srcsys_cd: params.row.arc_srcsys_cd,
                    i_new_group_key: $scope.editGrpMembrshp.grp.id,
                    i_new_assgnmnt_mthd: $scope.editGrpMembrshp.assignmentMethod,
                    i_new_line_of_service_cd: $scope.editGrpMembrshp.selected.groupLOS,
                    i_new_arc_srcsys_cd: $scope.editGrpMembrshp.sourceSys,
                }

               // console.log(postParams);
                if (!validation(postParams)) {
                    myApp.hidePleaseWait();
                    return false;
                }
                ConstCemDataServices.postEditGroupMembership(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }
        function validation(postParams) {
            if (postParams.i_new_line_of_service_cd == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.MESSAGE_PREF_VALIDAION_LOS);
                return false;
            }
            if (postParams.i_new_group_key == "") {
                Message.open(CEM_CONSTANTS.ERROR_HEADER, CEM_CONSTANTS.GROUP_MEM_VALIDATION_GROUP_NAME);
                return false;
            }

            return true;
        }
    }]);


angular.module('constituent').controller("DeleteGrpMembrshpCtrl",
    ["$scope", "params", "$uibModalInstance", "globalServices", "$sessionStorage", "$uibModal", "ConstUtilServices", "ConstCemDataServices",
        "Custom_Date", "dropDownService", "Message",
    function ($scope, params, $uibModalInstance, globalServices, $sessionStorage, $uibModal, ConstUtilServices, ConstCemDataServices, Custom_Date, dropDownService) {

      //  console.log(params);
        var initialize = function () {
            if (globalServices.getCaseTabCaseNo() != null) {
                var caseNo = globalServices.getCaseTabCaseNo();
            };


            $scope.deleteGrpMembrshp = {
                caseNo: caseNo,
                selected: {
                    note: CRUD_CONSTANTS.DEFAULT_NOTE,
                    groupLOS: params.row.line_of_service_cd,
                    groupName: "",
                    groupNameCode: ""
                },
                LOSs: getLOS(),
                groupNames: [],
                assignmentMethod: params.row.assgnmnt_mthd,
                masterId: params.masterId,
                sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
                startDate: params.row.grp_strt_ts,
                effStartDate: params.row.grp_mbrshp_eff_strt_dt,
                effEndDate: params.row.grp_mbrshp_eff_end_dt,
                rowCode: params.row.strx_row_stat_cd,
                constType: $sessionStorage.type,
                notes: CRUD_CONSTANTS.NOTES
            }

            dropDownService.getDropDown(HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) {
                $scope.deleteGrpMembrshp.groupNames = result;

                angular.forEach(result, function (v, k) {
                    if (v.id == params.row.grp_key) {
                        $scope.deleteGrpMembrshp.selected.groupNameCode = v.id;
                        $scope.deleteGrpMembrshp.selected.groupName = v.value;
                    }
                });


            }).error(function (result) { });

        }
        initialize();

        $scope.deleteGrpMembrshp.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.deleteGrpMembrshp.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {
                var postParams = {
                    i_mstr_id: $scope.deleteGrpMembrshp.masterId,
                    i_grp_mbrshp_eff_strt_dt: $scope.deleteGrpMembrshp.effStartDate,
                    i_grp_mbrshp_eff_end_dt: $scope.deleteGrpMembrshp.effEndDate,
                    i_cnst_typ: $scope.deleteGrpMembrshp.constType,
                    i_notes: $scope.deleteGrpMembrshp.selected.note,
                    i_case_seq_num: $scope.deleteGrpMembrshp.caseNo,
                    i_bk_group_key: params.row.grp_key,
                    i_bk_assgnmnt_mthd: params.row.assgnmnt_mthd,
                    i_bk_line_of_service_cd: params.row.line_of_service_cd,
                    i_bk_arc_srcsys_cd: params.row.arc_srcsys_cd,
                    i_new_group_key: "",
                    i_new_assgnmnt_mthd: "",
                    i_new_line_of_service_cd: "",
                    i_new_arc_srcsys_cd: $scope.deleteGrpMembrshp.sourceSys
                }

               // console.log(postParams);
                ConstCemDataServices.postDeleteGroupMembership(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });

            }
        }

    }]);