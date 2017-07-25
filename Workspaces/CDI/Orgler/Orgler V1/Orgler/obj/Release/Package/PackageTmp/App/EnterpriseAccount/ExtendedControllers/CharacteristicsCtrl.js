angular.module('enterpriseAccountModule').controller('CharacteristicsCtrl',
    ['$scope', '$rootScope', 'HandleResults', 'EXTENDED_FUNC', 'ExtendedStoreData', 'ExtendedColumnServices',
        'ExtendedServices', '$stateParams', 'ExtendedHttpServices', 'ShowDetailsModal','CharacteristicsModal',
    function ($scope, $rootScope, HandleResults, EXTENDED_FUNC, StoreData, ExtendedColumnServices,
        ExtendedServices, $stateParams, ExtendedHttpServices, ShowDetailsModal, CharacteristicsModal) {
    var initialize = function () {
        $scope.characteristics = {
           // columnDef: ExtendedColumnServices.getCharacteristicsColumns(),
            //gridOptions: 
            toggleDetails: false,
            togglePleaseWait: true,
            totalItems: 0,
            currentPage: 1,
            masterId: $stateParams.ent_org_id,
            distinctRecordCount: 0,
            showAllRecords: {
                showAllName: EXTENDED_FUNC.SHOW_ALL_RECORDS,
                togglePleaseWait: false,
                toggleRecords: true
            },
            showDetails: {}
        }
        $scope.characteristics.gridOptions = ExtendedServices.getGridOptions(ExtendedColumnServices.getCharacteristicsColumns());
        //console.log("INitialized");
        $scope.characteristics.gridOptions.onRegisterApi = function (gridApi) {
            //set gridApi on scope
            $scope.characteristics.gridApi = gridApi;
        }

        //console.log($stateParams);
        //console.log(uiGridConstants);
        showCharacteristicsDetails()
    }
    initialize();


    $scope.$on('characteristics', function (event, args) {

        if (args.characteristics) {
            $scope.characteristics.togglePleaseWait = true;
            //$scope.characteristics.gridOptions = ExtendedServices.getGridOptions(ExtendedColumnServices.getCharacteristicsColumns());
            showCharacteristicsDetails();

        } else {
            $scope.characteristics.togglePleaseWait = false;
            $scope.characteristics.toggleDetails = false;
        }

    });


    // show all records
    $scope.characteristics.showAllRecords.showAllrecords = function () {
        if ($scope.characteristics.showAllRecords.toggleRecords) {
            $scope.characteristics.showAllRecords.toggleRecords = !$scope.characteristics.showAllRecords.toggleRecords;
            $scope.characteristics.showAllRecords.showAllName = EXTENDED_FUNC.HIDE_RECORDS;

            $scope.characteristics.gridOptions = ExtendedServices.refreshGridData($scope.characteristics.gridOptions, StoreData.getCharacteristicsFullList());
            $scope.characteristics.totalItems = StoreData.getCharacteristicsFullList().length;
            $scope.characteristics.distinctRecordCount = StoreData.getCharacteristicsFullList()[0].distinct_records_count;
        }
        else {
            $scope.characteristics.showAllRecords.toggleRecords = !$scope.characteristics.showAllRecords.toggleRecords;
            $scope.characteristics.showAllRecords.showAllName = EXTENDED_FUNC.SHOW_ALL_RECORDS;

            $scope.characteristics.gridOptions = ExtendedServices.refreshGridData($scope.characteristics.gridOptions, StoreData.getCharacteristicsList());
            $scope.characteristics.totalItems = StoreData.getCharacteristicsList().length;
            $scope.characteristics.distinctRecordCount = StoreData.getCharacteristicsList()[0].distinct_records_count;
        }

    }



    // show details popup
   /* $scope.characteristics.showDetails.allDetails = function () {
        $scope.characteristics.togglePleaseWait = true;
        ExtendedHttpServices.getAllCharacteristics($scope.characteristics.masterId).then(function (results) {
            $scope.characteristics.togglePleaseWait = false;
            var params = {
                results: results,
                type: EXTENDED_FUNC.CHARACTERISTICS
            };
            ShowDetailsModal(params).then(function (response) { });

        }, function (error) {
            $scope.characteristics.togglePleaseWait = false
            HandleResults(error);
        });
    }*/


    $scope.characteristics.pageChanged = function (page) {
        $scope.characteristics.gridApi.pagination.seek(page);
    }

    //filter for search results
    $rootScope.toggleCharacteristicsSearchFilter = function () {
        $scope.characteristics.gridOptions.enableFiltering = !$scope.characteristics.gridOptions.enableFiltering;
        $scope.characteristics.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }


        //add  dnc record
    $scope.characteristics.addRecord = function () {
        var params = {
            masterId: $scope.characteristics.masterId
        }
        CharacteristicsModal.getModal(EXTENDED_FUNC.ADD_CHARACTERISTICS, params).then(function (result) {
            //console.log(result);
            var bool = HandleResults(result, EXTENDED_FUNC.ADD);
            if (bool) {
                $scope.characteristics.togglePleaseWait = true;
                $scope.characteristics.toggleDetails = false;
                refreshGrid();
            }
        });
    }


        //edit record
    $scope.extendedEditGridRow = function (row, grid) {
        //console.log(row);
        var params = {
            masterId: $scope.characteristics.masterId,
            row: row
        }
        CharacteristicsModal.getModal(EXTENDED_FUNC.EDIT_CHARACTERISTICS, params).then(function (result) {
            // console.log(result);
            var bool = HandleResults(result, EXTENDED_FUNC.EDIT);
            if (bool) {
                $scope.characteristics.togglePleaseWait = true;
                $scope.characteristics.toggleDetails = false;
                refreshGrid();
            }
        });
    }

        //Delete record
    $scope.extendedDeleteGridRow = function (row, grid) {
        //console.log(row);
        var params = {
            masterId: $scope.characteristics.masterId,
            row: row
        }
        CharacteristicsModal.getModal(EXTENDED_FUNC.DELETE_CHARACTERISTICS, params).then(function (result) {
            var bool = HandleResults(result, EXTENDED_FUNC.DELETE);
            if (bool) {
                $scope.characteristics.togglePleaseWait = true;
                $scope.characteristics.toggleDetails = false;
                refreshGrid();
            }
        });
    }


    //showing records 
    function showCharacteristicsDetails() {
        if (StoreData.getCharacteristicsList().length <= 0) {
            refreshGrid();
        }
        else {
            var res = StoreData.getCharacteristicsList();
            $scope.characteristics.gridOptions = ExtendedServices.refreshGridData($scope.characteristics.gridOptions, res);
            $scope.characteristics.distinctRecordCount = res[0].distinct_records_count;
            $scope.characteristics.togglePleaseWait = false;
            $scope.characteristics.toggleDetails = true;
        }
    }

    //referesh grid after add/edit/delete operations
    function refreshGrid() {
        ExtendedHttpServices.getCharacteristics($scope.characteristics.masterId, false)
        .then(function (result) {
            //console.log(result);
            StoreData.addCharacteristicsList(result, filterEntOrgData(result));
            $scope.characteristics.gridOptions = ExtendedServices.refreshGridData($scope.characteristics.gridOptions, StoreData.getCharacteristicsList());
            $scope.characteristics.togglePleaseWait = false;
            $scope.characteristics.toggleDetails = true;
            $scope.characteristics.totalItems = StoreData.getCharacteristicsList().length;
            if (StoreData.getCharacteristicsList().length > 0) {
                $scope.characteristics.distinctRecordCount = StoreData.getCharacteristicsList()[0].distinct_records_count;
            }
        },
        function (error) {
            $scope.characteristics.togglePleaseWait = false;
            HandleResults(error);
        });
    }

    }]);



angular.module('enterpriseAccountModule').controller("AddCharacteristicsCtrl",
    ["$scope", "params", "$uibModalInstance", "$uibModal",  'Message', 'EXTENDED_FUNC','dropDownService','ConstUtilServices','ExtendedHttpServices',
    function ($scope, params, $uibModalInstance, $uibModal, Message, EXTENDED_FUNC, dropDownService, ConstUtilServices, ExtendedHttpServices) {

        // console.log(params);
        var initialize = function () {
            $scope.characteristics = {
                selected: {
                    note: EXTENDED_FUNC.DEFAULT_NOTE,
                    characteristicsType: "Sales for Company",
                    characteristicsTypeCode: "SLS"
                },
                characterTypes: [],
                notes: EXTENDED_FUNC.NOTES,
                masterId: params.masterId,
                startDate: ConstUtilServices.getStartDate(),
                rowCode: EXTENDED_FUNC.ROW_CODE,
                constType: 'OR'
            };

            dropDownService.getDropDown(EXTENDED_FUNC.CHARACTERISTICS).then(
                function (result)
                {
                    $scope.characteristics.characterTypes = result; //console.log(result);
                }, function (error) { HandleResults(error); });

            //console.log("COnstituent Id " + $scope.characteristics.masterId);

        }
        initialize();

        $scope.characteristics.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.characteristics.submit = function () {
           // console.log("IN");
            if ($scope.myForm.$valid) {
               // console.log("valid");
                myApp.showPleaseWait();
                var postParams = {
                    "EntOrgID": $scope.characteristics.masterId,
                    "CharacteristicValue": $scope.characteristics.characterValue,
                    "CharacteristicTypeCode": $scope.characteristics.selected.characteristicsTypeCode,
                    "Notes": $scope.characteristics.selected.note,
                    "ConstType": $scope.characteristics.constType
                };

                ExtendedHttpServices.postAddChracteristics(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });
            }
        }
       

    }]);

angular.module('enterpriseAccountModule').controller("EditCharacteristicsCtrl",
    ["$scope", "params", "$uibModalInstance", "$uibModal", 'Message', 'EXTENDED_FUNC', 'dropDownService', 'ConstUtilServices', 'ExtendedHttpServices',
    function ($scope, params, $uibModalInstance, $uibModal, Message, EXTENDED_FUNC, dropDownService, ConstUtilServices, ExtendedHttpServices) {
        var row = params.row;
        // console.log(params);
        var initialize = function () {
            $scope.characteristics = {
                selected: {
                    note: EXTENDED_FUNC.DEFAULT_NOTE,
                    characteristicsType: "",
                    characteristicsTypeCode: row.cnst_chrctrstc_typ_cd
                },
                characterValue: row.cnst_chrctrstc_val,
                characterTypes: [],
                notes: EXTENDED_FUNC.NOTES,
                masterId: params.masterId,
                startDate: row.cnst_chrctrstc_strt_dt,
                rowCode: row.row_stat_cd,
                endDate: row.cnst_chrctrstc_end_dt,
                DWTimestamp: row.dw_srcsys_trans_ts,
                loadId: row.load_id,
            };

            dropDownService.getDropDown(EXTENDED_FUNC.CHARACTERISTICS).then(
                function (result) {
                    $scope.characteristics.characterTypes = result; //console.log(result);
                    angular.forEach($scope.characteristics.characterTypes, function (v, k) {
                        if (v.id == $scope.characteristics.selected.characteristicsTypeCode) {
                            $scope.characteristics.selected.characteristicsType = v.value;
                        }
                    });
                }, function (error) { HandleResults(error); });

            //console.log("COnstituent Id " + $scope.characteristics.masterId);

        }
        initialize();

        $scope.characteristics.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.characteristics.submit = function () {
            // console.log("IN");
            if ($scope.myForm.$valid) {
                // console.log("valid");
                myApp.showPleaseWait();
                var postParams = {
                    "EntOrgID": $scope.characteristics.masterId,
                    "CharacteristicValue": $scope.characteristics.characterValue,
                    "CharacteristicTypeCode": $scope.characteristics.selected.characteristicsTypeCode,
                    "Notes": $scope.characteristics.selected.note,
                    "OldCharacteristicValue": row.cnst_chrctrstc_val,
                    "OldCharacteristicTypeCode": row.cnst_chrctrstc_typ_cd,

                };

                ExtendedHttpServices.postEditChracteristics(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });
            }
        }


    }]);



angular.module('enterpriseAccountModule').controller("DeleteCharacteristicsCtrl",
    ["$scope", "params", "$uibModalInstance", "$uibModal", 'Message', 'EXTENDED_FUNC', 'dropDownService', 'ConstUtilServices', 'ExtendedHttpServices',
    function ($scope, params, $uibModalInstance, $uibModal, Message, EXTENDED_FUNC, dropDownService, ConstUtilServices, ExtendedHttpServices) {
        var row = params.row;
        // console.log(params);
        var initialize = function () {
            $scope.characteristics = {
                selected: {
                    note: EXTENDED_FUNC.DEFAULT_NOTE,
                    characteristicsType: "",
                    characteristicsTypeCode: row.cnst_chrctrstc_typ_cd
                },
                characterValue: row.cnst_chrctrstc_val,
                characterTypes: [],
                notes: EXTENDED_FUNC.NOTES,
                masterId: params.masterId,
                startDate: row.cnst_chrctrstc_strt_dt,
                rowCode: row.row_stat_cd,
                endDate: row.cnst_chrctrstc_end_dt,
                DWTimestamp: row.dw_srcsys_trans_ts,
                loadId: row.load_id,
            };

            dropDownService.getDropDown(EXTENDED_FUNC.CHARACTERISTICS).then(
                function (result) {
                    $scope.characteristics.characterTypes = result; //console.log(result);
                    angular.forEach($scope.characteristics.characterTypes, function (v, k) {
                        if (v.id == $scope.characteristics.selected.characteristicsTypeCode) {
                            $scope.characteristics.selected.characteristicsType = v.value;
                        }
                    });
                }, function (error) { HandleResults(error); });

            //console.log("COnstituent Id " + $scope.characteristics.masterId);

        }
        initialize();

        $scope.characteristics.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.characteristics.submit = function () {
            // console.log("IN");
            if ($scope.myForm.$valid) {
                 //console.log("valid");
                myApp.showPleaseWait();
                var postParams = {
                    "EntOrgID": $scope.characteristics.masterId,
                    "Notes": $scope.characteristics.selected.note,
                    "OldCharacteristicValue": row.cnst_chrctrstc_val,
                    "OldCharacteristicTypeCode": row.cnst_chrctrstc_typ_cd,

                };

                ExtendedHttpServices.postDeleteChracteristics(postParams, false).then(function (res) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(res);
                }, function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(error);
                });
            }
        }


    }]);

function filterEntOrgData(result) {
    var newResult = [];
    angular.forEach(result, function (v, k) {
        //console.log(v);
        if ("row_stat_cd" in v) {
            if (v.row_stat_cd == "L") {

            }
            else if ("transNotes" in v) {
                if (v.transNotes.toLowerCase().indexOf("deleted") <= 0) {
                    newResult.push(v);

                }
            }
            else if (v.row_stat_cd == "U") {
                var allKeys = Object.keys(v);
                var endDateKey = allKeys.find(EndDate);
                //active records
                if (v[endDateKey].indexOf("9999") > 0) {
                    newResult.push(v);
                }
            }
            else {
                newResult.push(v);
            }
        }
        else if ("transNotes" in v) {
            if (v.transNotes.toLowerCase().indexOf("deleted") <= 0) {
                newResult.push(v);
            }
        }
        else {
            newResult.push(v);
        }

    });

    return newResult;
}