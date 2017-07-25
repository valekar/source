//Controller for the Enterprise Account Search functionality
enterpriseAccMod.controller('enterPriseAccountSearchController', ['$state', '$http', '$location', '$rootScope', '$scope',
    '$uibModal', '$localStorage', 'uibDateParser', 'EnterpriseAccountService', 'EnterpriseAccountDataService', 'uiGridConstants',
    'uiGridTreeViewConstants', 'mainService','StoreData',
    function ($state, $http, $location, $rootScope, $scope, $uibModal, $localStorage, dateParser, service, dataService, uiGridConstants, uiGridTreeViewConstants
        , mainService, StoreData) {


        /************************* Constants *************************/
        var BasePath = $("base").first().attr("href");
        //$scope.processingOverlaySource = BasePath + "Images/Loading.gif";
        var NAVIGATE_URL = "/enterpriseaccount/search/result";
        $rootScope.enterPriseAccountNAICSCodeTemplate = BasePath + "App/EnterpriseAccount/Views/Shared/EnterpriseAccountSelectNaicsCodeSearch.html";
        //$rootScope.sourceSystemAddTemplate = BasePath + "App/EnterpriseAccount/Views/Shared/EnterpriseSearchSourceSystemAdd.html";
        //$rootScope.chapterSystemAddTemplate = BasePath + "App/EnterpriseAccount/Views/Shared/EnterpriseSearchChapterSystemAdd.html";
        var username = "";

        /************************* Generic Methods *************************/
        //Method to hide the search message label
        $scope.clearSearchMessage = function () {
            $scope.showSearchMessage = false;
        }

        //Method to show the search message label
        $scope.showSearchMessage = function () {
            $scope.showSearchMessage = true;
        }   
       
        $scope.clear = function () {
            $scope.dt = null;
        };

        $scope.open = function ($event) {
            $scope.status.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        $scope.dateOptions = {
            formatYear: 'yyyy',
            startingDay: 1,
            minMode: 'year'
        };

        $scope.formats = ['yyyy'];
        $scope.format = $scope.formats[0];

        $scope.status = {
            opened: false
        };


        if (dataService.getEnterpriseAccountlastSearchResults())
            $scope.lastEnterpriseSearchResultPresent = true;
        else
            $scope.lastEnterpriseSearchResultPresent = false;
        /************************* DOM Element Data Initialization *************************/
        //Hiding the search message field
        $scope.clearSearchMessage();
        //$scope.pleaseWait = { "display": "none" };

        var SearchPanelsCount = 5; // Define the maximum number of search panels
        var defaultPanelsCount = 1; // Set the number of search panels to be opened by default

        var appendString = 'ID'; // Specify the string that needs to be appended to distinguish between each elements
        $rootScope.NaicsCode = "";
        var rootModel = null;
        //Initialize        
        var initialize = function () {

           // console.log("Entered");
            var username = "";
            $scope.pleaseWait = { "display": "none" };

            //if (mainService.getLastCaseSearchResult())
            //    $scope.lastCaseSearchResultPresent = true;
            //else
            //    $scope.lastCaseSearchResultPresent = false;
           var SearchParams = dataService.getSavedSearchParams(); //This variable holds the search params so that they can be bound when the users returns back to this page via breadcrumbs
            $scope.EnterpriseAccountSearchFormElements = [];
            $scope.SearchRows = defaultPanelsCount;
            for (i = 1; i <= SearchPanelsCount; i++) {
                $scope.EnterpriseAccountSearchFormElements.push(appendString + i);
            }

            //If you are adding an element to the HTML, then add the model names here as well
            $scope.InputParams = {
                SearchPanel: [],
                PanelSeparator: [],
                showCloseButton: [],
                EnterpriseOrgId: [],
                EnterpriseOrgName: [],
                RankProviderInput: [],
                RankTo: [],
                RankFrom: [],                       
                ListNaicsCodes: [],
                Tags: [],
                IncludeSuperiorOrSubordinate: [],
                IncludeSuperior:[],
                IncludeSubordinate: [],               
                ExcludeTransformations: [],
                Username: [],
                SearchMeChkbx: [],
                RecentChanges: [],
                ListNaicsCode :[]
                
            };

            angular.forEach($scope.InputParams, function (paramskey, paramsvalue) {

                angular.forEach($scope.EnterpriseAccountSearchFormElements, function (key) {
                    $scope.InputParams[paramsvalue].push(key);
                });
            });

            for (i = 1; i <= $scope.SearchRows; i++) {

                $scope.InputParams.SearchPanel[appendString + i] = true;
                $scope.InputParams.PanelSeparator[appendString + i] = true;
                $scope.InputParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.InputParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }               
           
           // Set the previously searched search params if navigating from breadcrumbs
            if (SearchParams) {
                var rootModel = SearchParams["EnterpriseOrgInputSearchModel"];
                if (rootModel.length > 1) {
                    $scope.SearchRows = rootModel.length;
                }
                else {
                    $scope.SearchRows = defaultPanelsCount;
                }
                for (i = 1; i <= rootModel.length; i++) {
                    $scope.InputParams.SearchPanel[appendString + i] = true;
                    $scope.InputParams.PanelSeparator[appendString + i] = true;
                    $scope.InputParams.showCloseButton[appendString + i] = true;
                    if (i == 1) {
                        $scope.InputParams.showCloseButton[appendString + i] = false;
                    }
                    $scope.InputParams.EnterpriseOrgId[appendString + i] = rootModel[i - 1]["EnterpriseOrgID"];
                    $scope.InputParams.EnterpriseOrgName[appendString + i] = rootModel[i - 1]["EnterpriseOrgName"];
                    $scope.InputParams.RankProviderInput[appendString + i] = rootModel[i - 1]["RankProviderInput"];
                    $scope.InputParams.RankTo[appendString + i] = rootModel[i - 1]["RankTo"];
                    $scope.InputParams.RankFrom[appendString + i] = rootModel[i - 1]["RankFrom"];                   
                    $scope.InputParams.ListNaicsCodes[appendString + i] = rootModel[i - 1]["listNaicsCodes"];
                    $scope.InputParams.Tags[appendString + i] = rootModel[i - 1]["Tags"];
                    $scope.InputParams.IncludeSuperiorOrSubordinate[appendString + i] = rootModel[i - 1]["IncludeSuperiorIncludeSubordinate"];
                    $scope.InputParams.IncludeSuperior[appendString + i] = rootModel[i - 1]["IncludeSuperior"];
                    $scope.InputParams.IncludeSubordinate[appendString + i] = rootModel[i - 1]["IncludeSubordinate"];                   
                    $scope.InputParams.ExcludeTransformations[appendString + i] = rootModel[i - 1]["ExcludeTransformations"];                  
                    $scope.InputParams.Username[appendString + i] = rootModel[i - 1]["Username"];                  
                    $scope.InputParams.SearchMeChkbx[appendString + i] = rootModel[i - 1]["RecentChanges"];
                    
                }
            }
            //Method to get the recent searches on load of the application
            service.getEnterpriseAccountRecentSearch().success(function (result) {
                $rootScope.EnterpriseAccountRecentSearches = result;
            }).error(function (result) {
                errorPopups(result);
            });
        };
        initialize();
        $scope.AddNewSearchRow = function (RowCount) {
            if (RowCount < SearchPanelsCount) {
                RowCount++;
                $scope.SearchRows = RowCount; // Change the default search row count to the number of counts open                
                for (i = 1; i <= RowCount; i++) {
                    $scope.InputParams.SearchPanel[appendString + i] = true;
                    $scope.InputParams.PanelSeparator[appendString + i] = true;
                    $scope.InputParams.showCloseButton[appendString + i] = true;
                    if (i == 1) {
                        $scope.InputParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                    }
                }
            }
            else {
                alert(ENT_ACC.ONLY_FIVE);
            }
        }
            $scope.RemoveRow = function (panelID) {
            $scope.InputParams.SearchPanel[panelID] = false;
            $scope.InputParams.PanelSeparator[panelID] = false;
            $scope.InputParams.showCloseButton[panelID] = false;
            if ($scope.SearchRows != 1)
                $scope.SearchRows--           
        }

        //When clicked in search go to search results and do a post there         
            $scope.EnterpriseAccountSearch = function (myForm) {
                if (myForm.$valid) {
                    $scope.pleaseWait = { "display": "block" };
                    var postParams = dataService.getSearchParams($scope, $scope.SearchRows, appendString, $rootScope);
                    if (postParams.EnterpriseOrgInputSearchModel[0].EnterpriseOrgID == "")
                    {
                        postParams.EnterpriseOrgInputSearchModel[0].EnterpriseOrgID = undefined;
                    }
                    if (postParams.EnterpriseOrgInputSearchModel[0].EnterpriseOrgID == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].EnterpriseOrgName == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].RankProviderInput == null &&
                       postParams.EnterpriseOrgInputSearchModel[0].RankTo == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].RankFrom == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].listNaicsCodes == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].Tags == null &&
                       postParams.EnterpriseOrgInputSearchModel[0].IncludeSuperiorIncludeSubordinate == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].ExcludeTransformations == undefined &&
                       postParams.EnterpriseOrgInputSearchModel[0].Username == undefined
                       ) {
                        $scope.EntOrgsNoResults = ENT_ACC.ATLEAST_ONE_CRITERIA;
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else if (postParams.EnterpriseOrgInputSearchModel[0].RankTo != undefined ||
                    postParams.EnterpriseOrgInputSearchModel[0].RankFrom != undefined) {

                        if (postParams.EnterpriseOrgInputSearchModel[0].RankProviderInput != undefined) {
                            if (postParams.EnterpriseOrgInputSearchModel[0].RankProviderInput.length == 0) {
                                $scope.EntOrgsNoResults = "Please select Rank Provider before proceed";
                                $scope.pleaseWait = { "display": "none" };
                            }
                            else {
                                $scope.pleaseWait = { "display": "block" };
                                $scope.EntOrgsNoResults = "";
                                callEnterpriseAccountService($scope, service, dataService, postParams, $location, NAVIGATE_URL, $rootScope);
                            }
                        }
                        else if (postParams.EnterpriseOrgInputSearchModel[0].RankTo != undefined ||
                                           postParams.EnterpriseOrgInputSearchModel[0].RankFrom != undefined) {


                            if (postParams.EnterpriseOrgInputSearchModel[0].RankProviderInput == undefined) {
                                $scope.EntOrgsNoResults = "Please select Rank Provider before proceed";
                                $scope.pleaseWait = { "display": "none" };
                            }
                            else {
                                $scope.pleaseWait = { "display": "block" };
                                $scope.EntOrgsNoResults = "";
                                callEnterpriseAccountService($scope, service, dataService, postParams, $location, NAVIGATE_URL, $rootScope);
                            }


                        }


                    }
                    else if (postParams.EnterpriseOrgInputSearchModel[0].IncludeSuperiorIncludeSubordinate != undefined && postParams.EnterpriseOrgInputSearchModel[0].EnterpriseOrgName == undefined) {
                        $scope.EntOrgsNoResults = "Please enter EnterpriseOrg Name";
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else if (postParams.EnterpriseOrgInputSearchModel[0].ExcludeTransformations != undefined && postParams.EnterpriseOrgInputSearchModel[0].ExcludeTransformations != false && postParams.EnterpriseOrgInputSearchModel[0].EnterpriseOrgID == undefined) {
                        $scope.EntOrgsNoResults = "Please enter Enterprise OrgID";
                        $scope.pleaseWait = { "display": "none" };
                    }
                    else {
                        $scope.pleaseWait = { "display": "block" };
                        $scope.EntOrgsNoResults = "";
                        callEnterpriseAccountService($scope, service, dataService, postParams, $location, NAVIGATE_URL, $rootScope);

                    }                   
                }
                else { $scope.EntOrgsNoResults = "Please enter only numbers" }
            }
        //Method to perform the search        
            
            $scope.EnterpriseOrgsRecentSearch = function (qry) {
                $scope.pleaseWait = { "display": "block" };
                angular.element(enterPriseAccountrecentSearches).modal('hide');
                var listPostParam = [];
                listPostParam.push(qry);
                postParams = listPostParam;   
                callEnterpriseAccountService($scope, service, dataService, qry, $location, NAVIGATE_URL, $rootScope);
            }

        //Get the recentmost search result data on clicking of the button 
            $scope.fetchLastEnterpriseSearchResult = function () {

                if (dataService.getEnterpriseAccountlastSearchResults()) {
                    //Get back the last search result
                    dataService.setEnterpriseAccountSearchResults(dataService.getEnterpriseAccountlastSearchResults());
                    $state.go('enterpriseaccount.search.result', {});
                }
                else {
                    window.alert(ENT_ACC.LOCAL_STORAGE_CLEARED);
                }
            }


            function callEnterpriseAccountService($scope, service, dataService, postParams, $location, NAVIGATE_URL, $rootScope) {
                $scope.pleaseWait = { "display": "block" };
                // console.log("Into the callcaseService method");
                dataService.setSearchParams(postParams);
                service.getEnterpriseAccountSearchResults(postParams).success(function (result) {
                    //clear all multi tab data , need to do for normal tab too
                    //CaseClearDataService.clearMultiData(); // TEMPORARILY COMMENTED !!
                    // console.log("retreived adv search results");
                    dataService.setEnterpriseAccountSearchResults(result);
                    //track the last search result
                    dataService.setEnterpriseAccountlastSearchResults(result);
                    dataService.setEnterpriseAccountlastSearchResults(result);
                    dataService.setEnterpriseAccountlastSearchInput(postParams);
                    //To track the recent searches
                    service.logEnterpriseAccountRecentSearch(postParams).success(function (result) {
                        //To refresh the recent searches list
                        service.getEnterpriseAccountRecentSearch().success(function (result) {
                            $rootScope.EnterpriseOrgsRecentSearches = result;
                        });
                    });

                    var _result = dataService.getEnterpriseAccountSearchResults();
                    if (_result != null) {                        
                        dataService.getEnterpriseAccountlastSearchResults();                       
                        if (_result.objSearchResults.length==0) {
                            $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
                            $scope.pleaseWait = { "display": "none" };                            
                        }
                        else{
                            $scope.pleaseWait = { "display": "none" };
                            dataService.setEnterpriseAccountSearchResults(dataService.getEnterpriseAccountSearchResults());
                            $localStorage.previousParams = dataService.setSearchParams(postParams);
                        $location.url(NAVIGATE_URL);
                       }
                    }
                    else {
                        //alert("no data");
                        $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
                        $scope.pleaseWait = { "display": "none" };
                    }
                }).error(function (result) {
                    errorPopups(result);
                    $scope.pleaseWait = { "display": "none" };

                });
            }
           
        //Clear the Search Criteria           
            $scope.EnterpriseAccountClear = function () {
                $scope.EntOrgsNoResults = "";
                dataService.clearSearchParams($scope, SearchPanelsCount, defaultPanelsCount, appendString, $rootScope);
            }
       
        /************************* NAICS Code Add functionality *************************/
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
              { displayName: 'NAICS Code', field: 'naics_cd', width: '*', type: 'number', cellFilter: 'number', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', minWidth: 50, maxWidth: 9000 }
            ]
        };

        $rootScope.searchGridOptionsSearch = gridOption;

        //Registering the method which will get executed once the grid gets loaded
        $rootScope.searchGridOptionsSearch.onRegisterApi = function (gridApi) {

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

                $rootScope.searchGridOptionsSearch.data = [];
                writeoutNode(dataLevel2and3, 0, $rootScope.searchGridOptionsSearch.data);
            })
            .error(function (result) {
                errorPopups(result);
            });
        //Method to open the add pop-up on click of the Add button
        var listSelectedSearchNAICSCodes = [];
        $scope.selectNaicsSegmentEnterpriseAccount = function (counter) {          
            $rootScope.gridApiNAICSAdd.selection.clearSelectedRows(); //Clear all the selections            
            StoreData.clearNaicsCounter();
            StoreData.setNaicsCounter(counter);
            //Open the pop-up
            angular.element(searchNAICSCodePopup).modal();
            $scope.pleaseWait = { "display": "none" };


        }
        //Method called on click of the Submit button
       
        $scope.submitNaicsSegment = function () {
           // $scope.pleaseWait = { "display": "block" };
            //Hide the pop-up
            angular.element(searchNAICSCodePopup).modal('hide');
            //Refresh the grid with updated information
            angular.forEach($rootScope.gridApiNAICSAdd.selection.getSelectedRows(), function (value, key) {
                listSelectedSearchNAICSCodes.push(value.naics_cd);
            });
            $rootScope.ListNaicsCodes = listSelectedSearchNAICSCodes.toString();
            //console.log(StoreData.getNaicsCounter());
            $scope.InputParams.ListNaicsCode[StoreData.getNaicsCounter()] = $rootScope.ListNaicsCodes;
        }
        $scope.closeNaicsSegment = function () {
            $scope.pleaseWait = { "display": "none" };
            //Hide the pop-up
            angular.element(searchNAICSCodePopup).modal('hide');
           
        }
        $rootScope.IncludeSuperiorOrSubordinate = ["Include Superior", "Include Subordinate"];   
      
        //Get Tags data from Json file
        service.getTags()
           .success(function (result) {
               $rootScope.Tags = result;

           })
           .error(function (result) {
               errorPopups(result);
           });
        //Get Fourtune Rank Data from Json file
        service.getFourtuneRank()
           .success(function (result) {
               $rootScope.RankProvider = result;

           })
           .error(function (result) {
               errorPopups(result);
           });
        //Get SourceSystem Data from Json file
        service.getSourceSystem()
           .success(function (result) {
               $rootScope.SourceSystemData = result;

           })
           .error(function (result) {
               errorPopups(result);
           });
        //Get ChapterSystem Data from Json file
        service.getChapterSystem()
           .success(function (result) {
               $rootScope.ChapterSystem = result;

           })
           .error(function (result) {
               errorPopups(result);
           });
        //Get User Name
        mainService.getUsername().then(function (result) {
            username = result.data;
        });

        $scope.SearchMeChkbxChange = function (MeChkbxID) {           
            if ($scope.InputParams.SearchMeChkbx[MeChkbxID]) {
                $scope.InputParams.Username[MeChkbxID] = username;
                $scope.InputParams.SearchMeChkbx[MeChkbxID] = true;
               // $localStorage.entOrgUserNameState = $scope.InputParams.entOrgUsernameState;//To retain the me checkbox state
            }
            else {
                $scope.InputParams.Username[MeChkbxID] = "";
                $scope.InputParams.SearchMeChkbx[MeChkbxID] = false;
               // $localStorage.entOrgUserNameState = $scope.InputParams.entOrgUsernameState;
            }
        }   

        //Registering the method which will get executed once the grid gets loaded
        $rootScope.searchGridOptionsSearch.onRegisterApi = function (gridApi) {

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

                $rootScope.searchGridOptionsSearch.data = [];
                writeoutNode(dataLevel2and3, 0, $rootScope.searchGridOptionsSearch.data);
            })
            .error(function (result) {
                errorPopups(result);
            });


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
        //Message Pop-up 
        function MessagePopup($rootScope, headerText, bodyText) {
            $rootScope.enterpriseAccountModalPopupHeaderText = headerText;
            $rootScope.enterpriseAccountModalPopupBodyText = bodyText;
            angular.element(enterpriseAccountMessageDialogBox).modal({ backdrop: "static" });
        }

    }]);

enterpriseAccMod.filter('placeholderfunc', ['EnterpriseAccountDataService', function (EnterpriseAccountDataService) {
    return function (input) {
        var EnterpriseAccountResultsBindingData = "";      
        angular.forEach(input.EnterpriseOrgInputSearchModel, function (value, key) {
            if (value.EnterpriseOrgID != null)
                EnterpriseAccountResultsBindingData += "Enterprise Org Id: " + value.EnterpriseOrgID + ", ";
            if (value.EnterpriseOrgName != null)
                EnterpriseAccountResultsBindingData += "Enterprise OrgName: " + value.EnterpriseOrgName + ", ";
            if (value.RankProviderInput != null)
                EnterpriseAccountResultsBindingData += "Rank Provider: " + value.RankProviderInput + ", ";
            if (value.RankTo != null)
                EnterpriseAccountResultsBindingData += "Rank To: " + value.RankTo + ", ";
            if (value.RankFrom != null)
                EnterpriseAccountResultsBindingData += "Rank From: " + value.RankFrom + ", ";
            if (value.listNaicsCodes != null)
                EnterpriseAccountResultsBindingData += "Naics Code: " + value.listNaicsCodes + ", "
            if (value.Tags != null)
                EnterpriseAccountResultsBindingData += "Tags: " + value.Tags + ", ";           
            if (value.IncludeSuperiorIncludeSubordinate != null)
                EnterpriseAccountResultsBindingData += "IncludeSuperior/IncludeSubordinate: " + value.IncludeSuperiorIncludeSubordinate + ", "
            if (value.ExcludeTransformations != null)
                EnterpriseAccountResultsBindingData += "Exclude Transformations: " + value.ExcludeTransformations + ", ";          
            //if (value.Username != null)
            //    EnterpriseAccountResultsBindingData += "Created By: " + value.Username + ", ";
            EnterpriseAccountResultsBindingData += ";";
        });
        //console.log(CaseTypesDropDown);

        if (EnterpriseAccountResultsBindingData != "")
            return EnterpriseAccountResultsBindingData.slice(0, -3);
        else
            return EnterpriseAccountResultsBindingData;
    }
}]);
