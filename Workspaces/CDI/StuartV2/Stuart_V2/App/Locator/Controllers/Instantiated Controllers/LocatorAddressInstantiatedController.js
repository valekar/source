
/********************* Edit Name Modal Instance ***************/
angular.module('locator').controller('EditLocatorAddressInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'pageName', 'LocatorAddressMultiDataService',  'uiGridConstants', 'LocatorAddressCRUDapiService', '$rootScope',
    'uibDateParser', 'LocatorAddressCRUDoperations', 'LocatorAddressServices', 'LocatorAddressDataServices', 'LocatorAddressDropDownService', '$state',
    function ($scope, $filter, $uibModalInstance, $uibModal,
    params, pageName, LocatorAddressMultiDataService, uiGridConstants, LocatorAddressCRUDapiService, $rootScope, uibDateParser, LocatorAddressCRUDoperations, LocatorAddressServices, LocatorAddressDataServices, LocatorAddressDropDownService,$state) {
        //console.log(params.grid)
       
        var row = params.row;
        var BASE_URL = BasePath + 'App/Locator/Views/Multi';
         //console.log(row);
        // editRow.row_stat_cd = "L";
        $scope.inlineOptions = {
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2050, 1, 1),
            minDate: new Date(2000, 1, 1),
            startingDay: 1
        };

        $scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
       
        $scope.LocatorAddress = {         
                       
            locator_addr_key: row.locator_addr_key,
            line1_addr: row.line1_addr,
            line2_addr: row.line2_addr,
            city: row.city,
            state: row.state,
            zip_5: row.zip_5,
            code_category: row.code_category,
            LocatorReportDate: row.report_dt ? new Date(row.report_dt) : null,
            userName: row.crtd_by_usr_id
        };
   

        //LocatorAddressDropDownService.getDropDown1(LOCATOR_CONSTANTS.LOCATOR_MANUALLASSESMENT_CODE).success(function (result) {

        //   console.log(angular.toJson(result));
        //   $scope.LocatorAddress.Finalassesmentcodes = result;
        //    //return result;
        //}).error(function (result) {
        //    alert("Unable to retrieve dropdown values");
        //});


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var LocatorReportDate = $scope.LocatorAddress.LocatorReportDate ? $filter('date')(new Date($scope.LocatorAddress.LocatorReportDate), 'MM/dd/yyyy') : null;

                var postParams = {
                    "LocAddrKey": $scope.LocatorAddress.locator_addr_key,
                    "LocAddressLine": $scope.LocatorAddress.line1_addr,
                    "LocAddressLine2": $scope.LocatorAddress.line2_addr,
                    "LocCity": $scope.LocatorAddress.city,
                    "LocState": $scope.LocatorAddress.state,
                    "LocZip": $scope.LocatorAddress.zip_5,
                    "LocZip4": $scope.LocatorAddress.zip_4,
                    "LocAssessCode": $scope.LocatorAddress.code_category,
                    "Date": LocatorReportDate,
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
              
                LocatorAddressCRUDapiService.getCRUDOperationResult(postParams,LOCATOR_CRUD_CONSTANTS.LOCATORADDRESSINFO).success(function (result) {
                    // console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;
                    //console.log(output_msg);
                    if (output_msg == LOCATOR_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: LOCATOR_CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }
                        var searchparam =
                            {
                                        "LocAddrKey": $scope.LocatorAddress.locator_addr_key
                                    
                            }

                        
                        // console.log(params);
                      
                        var SearchResultsParam = LocatorAddressDataServices.getSavedSearchParams(); //This gets the search params for the search results and refreshes the search results section
                       // console.log("getSavedSearchParams");
                      //  console.log(SearchResultsParam);
                        // if (params.grid.data.length > 1) {
                        if(pageName == "SearchResults"){
                           
                            LocatorAddressServices.getLocatorAddressAdvSearchResultsByID(searchparam).success(function (result) {
                               
                                myApp.hidePleaseWait();
                                params.grid.data = '';
                                params.grid.data.length = 0;
                                params.grid.data = result;

                               // LocatorAddressDataServices.setSearchResutlsDataDetail(result);
                                //LocatorAddressDataServices.getSearchResultsDataDetail();                             
                              
                                LocatorAddressDataServices.setSearchResutlsDataDetail(result);
                                LocatorAddressDataServices.getSearchResultsDataDetail();

                                
                               

                                var key = $scope.LocatorAddress.locator_addr_key;                               
                                $uibModalInstance.close(output);
                                messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SUCCESS_MESSAGE, LOCATOR_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                                $state.go('locator.address.results.multi', { obj: key }, { reload: true });

                                $scope.Address_constituent = $rootScope.AddressConstchk_value;
                                $scope.Address_Assessment = $rootScope.AddressAsseschk_value;

                                if ($scope.Address_constituent == false) {                                    
                                    $rootScope.LocatorAddressConstituents = BASE_URL + '/LocatorAddressConstituentsInfo.tpl.html';
                                    $rootScope.addressConstituent = true;
                                    $rootScope.disableConstView = { "display": "block" };

                                } else {
                                    $rootScope.addressConstituent = false;
                                    $rootScope.disableConstView = { "display": "none" };
                                }

                                if ($scope.Address_Assessment == false) {                                    
                                      $rootScope.LocatorAddressAssessment = BASE_URL + '/LocatorAddressAssessmentsInfo.tpl.html';
                                      $rootScope.addressAssessments = true;
                                    $rootScope.disableAssesView = { "display": "block" };

                                } else {
                                    $rootScope.addressAssessments = false;
                                    $rootScope.disableAssesView = { "display": "none" };
                                }


                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }
                                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }

                            });
                           

                        }
                        //else
                        //{
                      
                        //    LocatorAddressServices.getLocatorAddressAdvSearchResults(SearchResultsParam).success(function (result) {
                        //        LocatorAddressDataServices.setSearchResutlsData(result);
                        //    }).error(function (result) {
                        //        myApp.hidePleaseWait();
                        //        $uibModalInstance.close(output);
                        //        if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        //            messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                        //            myApp.hidePleaseWait();
                        //            $uibModalInstance.close(output);
                        //        }
                        //        else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        //            messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                        //            myApp.hidePleaseWait();
                        //            $uibModalInstance.close(output);
                        //        }

                        //    });
                        //    LocatorAddressServices.getLocatorAddressAdvSearchResults(searchparam).success(function (result) {
                        //        myApp.hidePleaseWait();
                        //       // console.log("case key");
                        //      //  console.log($scope.caseInfo.caseKey);
                        //        params.grid.data = '';
                        //        params.grid.data.length = 0;
                        //        params.grid.data = result;
                        //        LocatorAddressDataServices.setCaseDetails(result[0]["case_desc"]);
                        //       // console.log("Case Details");
                        //      //  console.log(CaseDataServices.getCaseDetails());

                        //        // emailData.push(PushParams);

                        //        CaseMultiDataService.setData(result, CASE_CONSTANTS.CASE_INFO);
                        //        $uibModalInstance.close(output);
                        //        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SUCCESS_MESSAGE, LOCATOR_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                        //    }).error(function (result) {
                        //        myApp.hidePleaseWait();
                        //        $uibModalInstance.close(output);
                        //        if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        //            myApp.hidePleaseWait();
                        //            $uibModalInstance.close(output);
                        //            messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                        //        }
                        //        else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        //            myApp.hidePleaseWait();
                        //            $uibModalInstance.close(output);
                        //            messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                        //        }
                        //    });
                        //}
                      
                    }
                    else if (output_msg == LOCATOR_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == LOCATOR_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: LOCATOR_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,

                            ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE, LOCATOR_CRUD_CONSTANTS.FAILURE_CONFIRM);
                    }
                    //else if (result.data == LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    debugger;
                    //    var output = {
                    //        finalMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output);
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, LOCATOR_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM);
                    }
                    else if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
        
    }]);

function modalMessage($rootScope, result) {   
    $rootScope.FinalMessage = result.finalMessage;   
    //$rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}



function messagePopup($rootScope, message, header) {
    $rootScope.FinalMessage = message;
    $rootScope.ConfirmationMessage = header;
    $rootScope.ReasonOrTransKey = '';
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}

