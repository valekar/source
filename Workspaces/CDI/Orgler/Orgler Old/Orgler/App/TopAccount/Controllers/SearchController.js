//Search Controller
topAccModule.controller("topAccountController", ['$scope', '$http', '$location', '$rootScope', '$state',
    function ($scope, $http, $location, $rootScope, $state) {

        /************************* Constants *************************/
        var BasePath = $("base").first().attr("href");
        alert("Controller loaded");
        var NAVIGATE_URL = $state.current.data.searchResultsNavURL;

        //Declaring the search input object
        $scope.text = "Test";
        $scope.searchInput = {};
        $scope.popup1 = {
            opened: false
        };
        $scope.showPleaseWait = false;

        //Date Popup options
        $scope.inlineOptions = {
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2040, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };
        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();
        $scope.searchInput.accountCreatedDate = null;
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        $scope.setDate = function (year, month, day) {
            $scope.searchInput.accountCreatedDate = new Date(year, month, day);
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        $scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];

        //$scope.fileUploadMessage = "No file chosen";

        ////Number of editable columns in the reference data type
        //$rootScope.refDataColCount = 0;
        //$rootScope.refDataColCount = $state.current.data.refColumnMapping.length;

        ///************************* Generic Methods *************************/
        ////Method to hide the search message label
        //$scope.clearSearchMessage = function () {
        //    $scope.showSearchMessage = false;
        //}

        ////Method to show the search message label
        //$scope.showSearchMessage = function () {
        //    $scope.showSearchMessage = true;
        //}

        ////Method to hide the upload message label
        //$scope.clearUploadMessage = function () {
        //    $scope.showUploadMessage = false;
        //}

        ////Method to show the upload message label
        //$scope.showUploadMessageFunc = function () {
        //    $scope.showUploadMessage = true;
        //}

        ////Method to hide the processing overlay
        //$scope.hideProcessingOverlay = function () {
        //    $scope.processingOverlay = false;
        //}

        ////Method to show the processing overlay
        //$scope.showProcessingOverlay = function () {
        //    $scope.processingOverlay = true;
        //}

        ////Assign the uploaded files to the form data
        //var formdata;
        //$scope.getUploadedFiles = function ($files) {
        //    formdata = new FormData();
        //    angular.forEach($files, function (value, key) {
        //        formdata.append(key, value);
        //    });

        //};

        ///* ******************************** Binding inputs to the search if not null 
        //                                        and checking if the operation is pertaining to the same reference data set******************************** */
        ////Clearing the Search section
        ////Hiding the search message field
        //$scope.clearSearchMessage();
        //$scope.hideProcessingOverlay();

        //var SearchInput = {};
        //var prevSearchRefDataType = '';
        //SearchInput = DataService.getSearchInput();
        //prevSearchRefDataType = DataService.getPrevSearchRefDataType();

        //if (!angular.isUndefined(SearchInput) && prevSearchRefDataType == $rootScope.lobRefTypeConcatenatedVal) {
        //    //Get the number of columns and assign the respective search input values to the scope variable (bound to view)
        //    switch ($rootScope.refDataColCount) {
        //        case 20: $scope.searchInput.field_20 = angular.isUndefined(SearchInput.field_20) ? '' : SearchInput.field_20;
        //        case 19: $scope.searchInput.field_19 = angular.isUndefined(SearchInput.field_19) ? '' : SearchInput.field_19;
        //        case 18: $scope.searchInput.field_18 = angular.isUndefined(SearchInput.field_18) ? '' : SearchInput.field_18;
        //        case 17: $scope.searchInput.field_17 = angular.isUndefined(SearchInput.field_17) ? '' : SearchInput.field_17;
        //        case 16: $scope.searchInput.field_16 = angular.isUndefined(SearchInput.field_16) ? '' : SearchInput.field_16;
        //        case 15: $scope.searchInput.field_15 = angular.isUndefined(SearchInput.field_15) ? '' : SearchInput.field_15;
        //        case 14: $scope.searchInput.field_14 = angular.isUndefined(SearchInput.field_14) ? '' : SearchInput.field_14;
        //        case 13: $scope.searchInput.field_13 = angular.isUndefined(SearchInput.field_13) ? '' : SearchInput.field_13;
        //        case 12: $scope.searchInput.field_12 = angular.isUndefined(SearchInput.field_12) ? '' : SearchInput.field_12;
        //        case 11: $scope.searchInput.field_11 = angular.isUndefined(SearchInput.field_11) ? '' : SearchInput.field_11;
        //        case 10: $scope.searchInput.field_10 = angular.isUndefined(SearchInput.field_10) ? '' : SearchInput.field_10;
        //        case 9: $scope.searchInput.field_9 = angular.isUndefined(SearchInput.field_9) ? '' : SearchInput.field_9;
        //        case 8: $scope.searchInput.field_8 = angular.isUndefined(SearchInput.field_8) ? '' : SearchInput.field_8;
        //        case 7: $scope.searchInput.field_7 = angular.isUndefined(SearchInput.field_7) ? '' : SearchInput.field_7;
        //        case 6: $scope.searchInput.field_6 = angular.isUndefined(SearchInput.field_6) ? '' : SearchInput.field_6;
        //        case 5: $scope.searchInput.field_5 = angular.isUndefined(SearchInput.field_5) ? '' : SearchInput.field_5;
        //        case 4: $scope.searchInput.field_4 = angular.isUndefined(SearchInput.field_4) ? '' : SearchInput.field_4;
        //        case 3: $scope.searchInput.field_3 = angular.isUndefined(SearchInput.field_3) ? '' : SearchInput.field_3;
        //        case 2: $scope.searchInput.field_2 = angular.isUndefined(SearchInput.field_2) ? '' : SearchInput.field_2;
        //        case 1: $scope.searchInput.field_1 = angular.isUndefined(SearchInput.field_1) ? '' : SearchInput.field_1;
        //    }
        //    //Transaction id column
        //    if (!angular.isUndefined(SearchInput.trans_id)) {
        //        $scope.searchInput.transactionId = SearchInput.trans_id;
        //    }
        //}

        //refDataService.getRecentSearches($rootScope.controllerName, $rootScope.lobName + ' ' + $rootScope.referenceDataType).success(function (result) {
        //    $rootScope.AddressTypeRecentSearches = result;
        //}).error(function (result) {
        //    errorPopups(result);
        //});

        //// When upload button is clicked, form the http request and post the formdata to the server
        //$scope.uploadFiles = function () {
        //    var request = {
        //        method: 'POST',
        //        url: BasePath + $rootScope.controllerName + '/UploadFiles/',
        //        data: formdata,
        //        headers: {
        //            'Content-Type': undefined,
        //            "Accept": "application/json"
        //        },
        //        params: { strUploadType: $rootScope.lobName + ' ' + $rootScope.referenceDataType }
        //    };

        //    // Send the files to the server
        //    if (!angular.isUndefined(formdata)) {
        //        if (formdata != null) {
        //            $scope.clearUploadMessage();
        //            CRUDService.uploadReferenceDataChanges(request).success(function (result) {
        //                if (result.strUploadResult == 'No file') {
        //                    $scope.showUploadMessageFunc();
        //                    $scope.UploadMessage = UPLOAD_CONSTANTS.MESSAGE_TEXT.NO_FILE;
        //                    $scope.UploadMessageColour = UPLOAD_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_FILE;
        //                }
        //                else if (result.strUploadResult == 'Invalid format') {
        //                    $scope.showUploadMessageFunc();
        //                    $scope.UploadMessage = UPLOAD_CONSTANTS.MESSAGE_TEXT.INVALID_FORMAT;
        //                    $scope.UploadMessageColour = UPLOAD_CONSTANTS.MESSAGE_TEXT_COLOUR.INVALID_FORMAT;
        //                }
        //                else if (result.strUploadResult == 'Not even one valid') {
        //                    $scope.showUploadMessageFunc();
        //                    $scope.UploadMessage = UPLOAD_CONSTANTS.MESSAGE_TEXT.NO_INPUT;
        //                    $scope.UploadMessageColour = UPLOAD_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_INPUT;
        //                }
        //                else if (result.strUploadResult == 'Exceeds Limit') {
        //                    $scope.showUploadMessageFunc();
        //                    $scope.UploadMessage = UPLOAD_CONSTANTS.MESSAGE_TEXT.EXCEED_LIMIT;
        //                    $scope.UploadMessageColour = UPLOAD_CONSTANTS.MESSAGE_TEXT_COLOUR.EXCEED_LIMIT;
        //                }
        //                else if (result.boolSuccess) {
        //                    //Hide the upload popup
        //                    angular.element(uploadModalPopup).modal('hide');
        //                    //Show the upload success popup
        //                    MessagePopup($rootScope, "Upload " + $rootScope.referenceDataType, (UPLOAD_CONSTANTS.MESSAGE_TEXT.SUCCESS_MSG + " " + result.strUploadId + "."));
        //                }
        //                $scope.hideProcessingOverlay();
        //            })
        //            .error(function (result) {
        //                errorPopups(result);
        //                $scope.hideProcessingOverlay();
        //            });
        //        }
        //        else {
        //            $scope.hideProcessingOverlay();
        //            $scope.showUploadMessageFunc();
        //            $scope.UploadMessage = UPLOAD_CONSTANTS.MESSAGE_TEXT.NO_FILE;
        //            $scope.UploadMessageColour = UPLOAD_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_FILE;
        //        }
        //    }
        //    else {
        //        $scope.hideProcessingOverlay();
        //        $scope.showUploadMessageFunc();
        //        $scope.UploadMessage = UPLOAD_CONSTANTS.MESSAGE_TEXT.NO_FILE;
        //        $scope.UploadMessageColour = UPLOAD_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_FILE;
        //    }
        //};

        ////Method to clear the file input and the error message whenever the Upload button is clicked
        //$scope.btnBulkUpload = function () {
        //    angular.element(fileUploadedFile).val('');
        //    $scope.clearUploadMessage();
        //};

        ///************************* Function declarations *************************/
        ////Perform Search when 'Search' button is pressed in the Address Type Search section
        //$scope.SearchFormMethod = function () {
        //    if ($scope.searchFormName.$valid) {
        //        $scope.showProcessingOverlay();

        //        var SearchParameters = {};

        //        //Get the number of columns and assign the respective search input values to the variable (passed to server)
        //        switch ($scope.refDataColCount) {
        //            case 20: SearchParameters.field_20 = angular.isUndefined($scope.searchInput.field_20) ? '' : $scope.searchInput.field_20;
        //            case 19: SearchParameters.field_19 = angular.isUndefined($scope.searchInput.field_19) ? '' : $scope.searchInput.field_19;
        //            case 18: SearchParameters.field_18 = angular.isUndefined($scope.searchInput.field_18) ? '' : $scope.searchInput.field_18;
        //            case 17: SearchParameters.field_17 = angular.isUndefined($scope.searchInput.field_17) ? '' : $scope.searchInput.field_17;
        //            case 16: SearchParameters.field_16 = angular.isUndefined($scope.searchInput.field_16) ? '' : $scope.searchInput.field_16;
        //            case 15: SearchParameters.field_15 = angular.isUndefined($scope.searchInput.field_15) ? '' : $scope.searchInput.field_15;
        //            case 14: SearchParameters.field_14 = angular.isUndefined($scope.searchInput.field_14) ? '' : $scope.searchInput.field_14;
        //            case 13: SearchParameters.field_13 = angular.isUndefined($scope.searchInput.field_13) ? '' : $scope.searchInput.field_13;
        //            case 12: SearchParameters.field_12 = angular.isUndefined($scope.searchInput.field_12) ? '' : $scope.searchInput.field_12;
        //            case 11: SearchParameters.field_11 = angular.isUndefined($scope.searchInput.field_11) ? '' : $scope.searchInput.field_11;
        //            case 10: SearchParameters.field_10 = angular.isUndefined($scope.searchInput.field_10) ? '' : $scope.searchInput.field_10;
        //            case 9: SearchParameters.field_9 = angular.isUndefined($scope.searchInput.field_9) ? '' : $scope.searchInput.field_9;
        //            case 8: SearchParameters.field_8 = angular.isUndefined($scope.searchInput.field_8) ? '' : $scope.searchInput.field_8;
        //            case 7: SearchParameters.field_7 = angular.isUndefined($scope.searchInput.field_7) ? '' : $scope.searchInput.field_7;
        //            case 6: SearchParameters.field_6 = angular.isUndefined($scope.searchInput.field_6) ? '' : $scope.searchInput.field_6;
        //            case 5: SearchParameters.field_5 = angular.isUndefined($scope.searchInput.field_5) ? '' : $scope.searchInput.field_5;
        //            case 4: SearchParameters.field_4 = angular.isUndefined($scope.searchInput.field_4) ? '' : $scope.searchInput.field_4;
        //            case 3: SearchParameters.field_3 = angular.isUndefined($scope.searchInput.field_3) ? '' : $scope.searchInput.field_3;
        //            case 2: SearchParameters.field_2 = angular.isUndefined($scope.searchInput.field_2) ? '' : $scope.searchInput.field_2;
        //            case 1: SearchParameters.field_1 = angular.isUndefined($scope.searchInput.field_1) ? '' : $scope.searchInput.field_1;
        //        }

        //        //Transaction id and upload type
        //        SearchParameters.strUploadType = $rootScope.lobName + ' ' + $rootScope.referenceDataType;
        //        SearchParameters.trans_id = angular.isUndefined($scope.searchInput.transactionId) ? '' : $scope.searchInput.transactionId;

        //        //Store the search against a variable for future use
        //        DataService.setSearchInput(SearchParameters);
        //        //Set the current reference type
        //        DataService.setPrevSearchRefDataType($rootScope.lobRefTypeConcatenatedVal);

        //        var searchType = (angular.isUndefined($scope.searchInput.transactionId) || $scope.searchInput.transactionId == '' || $scope.searchInput.transactionId == null) ? 'ReferenceDataSearch' : 'TransactionSearch';

        //        //Perform search and get the results by calling the service method which send and receives responses
        //        refDataService.getReferenceDataSearch($rootScope.controllerName, SearchParameters).success(function (searchRes) {
        //            //To track the recent searches
        //            refDataService.logRecentSearch($rootScope.controllerName, SearchParameters).success(function (result) {
        //                //To refresh the recent searches list
        //                refDataService.getRecentSearches($rootScope.controllerName, $rootScope.lobName + ' ' + $rootScope.referenceDataType).success(function (result) {
        //                    $rootScope.addresstypeRecentSearches = result;
        //                });
        //            });
        //            var searchResultJson = searchRes;
        //            $scope.hideProcessingOverlay();
        //            $scope.showSearchMessage = false;
        //            //Invalid Search
        //            if (!searchResultJson.boolValidSearch) {
        //                $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.INVALID_SEARCH;
        //                $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.INVALID_SEARCH;
        //                $scope.showSearchMessage = true;
        //            }
        //                //No Records Found
        //            else if (searchResultJson.boolNoRecord) {
        //                $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
        //                $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_SEARCH_RESULTS;
        //                $scope.showSearchMessage = true;
        //            }
        //                //Show the search results
        //            else {
        //                DataService.setSearchResults(searchResultJson.objSearchResults);
        //                DataService.setSearchType(searchType);
        //                $location.url(NAVIGATE_URL);
        //            }
        //        })
        //        .error(function (result) {
        //            errorPopups(result);
        //            $scope.hideProcessingOverlay();
        //        });
        //    }
        //};

        ////Clearing the search input fields when 'Clear' button is pressed in the search
        //$scope.ClearForm = function () {
        //    $scope.showProcessingOverlay();

        //    //Get the number of columns and Clear the search input fields
        //    switch ($scope.refDataColCount) {
        //        case 20: $scope.searchInput.field_20 = "";
        //        case 19: $scope.searchInput.field_19 = "";
        //        case 18: $scope.searchInput.field_18 = "";
        //        case 17: $scope.searchInput.field_17 = "";
        //        case 16: $scope.searchInput.field_16 = "";
        //        case 15: $scope.searchInput.field_15 = "";
        //        case 14: $scope.searchInput.field_14 = "";
        //        case 13: $scope.searchInput.field_13 = "";
        //        case 12: $scope.searchInput.field_12 = "";
        //        case 11: $scope.searchInput.field_11 = "";
        //        case 10: $scope.searchInput.field_10 = "";
        //        case 9: $scope.searchInput.field_9 = "";
        //        case 8: $scope.searchInput.field_8 = "";
        //        case 7: $scope.searchInput.field_7 = "";
        //        case 6: $scope.searchInput.field_6 = "";
        //        case 5: $scope.searchInput.field_5 = "";
        //        case 4: $scope.searchInput.field_4 = "";
        //        case 3: $scope.searchInput.field_3 = "";
        //        case 2: $scope.searchInput.field_2 = "";
        //        case 1: $scope.searchInput.field_1 = "";
        //    }

        //    //Transaction id
        //    $scope.searchInput.transactionId = "";

        //    //Hiding the search message field
        //    $scope.clearSearchMessage();
        //    $scope.hideProcessingOverlay();
        //    $scope.clearUploadMessage();
        //}

        ////Method to post the uploaded file to the server on click of the 'Upload' button
        //$scope.referenceDataUpload = function () {
        //    $scope.showProcessingOverlay();
        //    $scope.uploadFiles();
        //}

        ////Method to populate the 
        //$scope.recentSearch = function (obj) {
        //    $scope.showProcessingOverlay();
        //    var SearchInputViewModel = {};
        //    angular.element(recentSearches).modal('hide');

        //    SearchInputViewModel = {
        //        "SearchInput": obj
        //    };

        //    //Store the search against a variable for future use
        //    DataService.setSearchInput(SearchInputViewModel);
        //    //Set the current reference type
        //    DataService.setPrevSearchRefDataType($rootScope.lobRefTypeConcatenatedVal);

        //    //Set the search input values
        //    if (!angular.isUndefined(SearchInputViewModel.SearchInput)) {
        //        //Get the number of columns and assign the respective search input values to the variable (passed to server)
        //        switch ($scope.refDataColCount) {
        //            case 20: $scope.searchInput.field_20 = angular.isUndefined(SearchInputViewModel.SearchInput.field_20) ? '' : SearchInputViewModel.SearchInput.field_20;
        //            case 19: $scope.searchInput.field_19 = angular.isUndefined(SearchInputViewModel.SearchInput.field_19) ? '' : SearchInputViewModel.SearchInput.field_19;
        //            case 18: $scope.searchInput.field_18 = angular.isUndefined(SearchInputViewModel.SearchInput.field_18) ? '' : SearchInputViewModel.SearchInput.field_18;
        //            case 17: $scope.searchInput.field_17 = angular.isUndefined(SearchInputViewModel.SearchInput.field_17) ? '' : SearchInputViewModel.SearchInput.field_17;
        //            case 16: $scope.searchInput.field_16 = angular.isUndefined(SearchInputViewModel.SearchInput.field_16) ? '' : SearchInputViewModel.SearchInput.field_16;
        //            case 15: $scope.searchInput.field_15 = angular.isUndefined(SearchInputViewModel.SearchInput.field_15) ? '' : SearchInputViewModel.SearchInput.field_15;
        //            case 14: $scope.searchInput.field_14 = angular.isUndefined(SearchInputViewModel.SearchInput.field_14) ? '' : SearchInputViewModel.SearchInput.field_14;
        //            case 13: $scope.searchInput.field_13 = angular.isUndefined(SearchInputViewModel.SearchInput.field_13) ? '' : SearchInputViewModel.SearchInput.field_13;
        //            case 12: $scope.searchInput.field_12 = angular.isUndefined(SearchInputViewModel.SearchInput.field_12) ? '' : SearchInputViewModel.SearchInput.field_12;
        //            case 11: $scope.searchInput.field_11 = angular.isUndefined(SearchInputViewModel.SearchInput.field_11) ? '' : SearchInputViewModel.SearchInput.field_11;
        //            case 10: $scope.searchInput.field_10 = angular.isUndefined(SearchInputViewModel.SearchInput.field_10) ? '' : SearchInputViewModel.SearchInput.field_10;
        //            case 9: $scope.searchInput.field_9 = angular.isUndefined(SearchInputViewModel.SearchInput.field_9) ? '' : SearchInputViewModel.SearchInput.field_9;
        //            case 8: $scope.searchInput.field_8 = angular.isUndefined(SearchInputViewModel.SearchInput.field_8) ? '' : SearchInputViewModel.SearchInput.field_8;
        //            case 7: $scope.searchInput.field_7 = angular.isUndefined(SearchInputViewModel.SearchInput.field_7) ? '' : SearchInputViewModel.SearchInput.field_7;
        //            case 6: $scope.searchInput.field_6 = angular.isUndefined(SearchInputViewModel.SearchInput.field_6) ? '' : SearchInputViewModel.SearchInput.field_6;
        //            case 5: $scope.searchInput.field_5 = angular.isUndefined(SearchInputViewModel.SearchInput.field_5) ? '' : SearchInputViewModel.SearchInput.field_5;
        //            case 4: $scope.searchInput.field_4 = angular.isUndefined(SearchInputViewModel.SearchInput.field_4) ? '' : SearchInputViewModel.SearchInput.field_4;
        //            case 3: $scope.searchInput.field_3 = angular.isUndefined(SearchInputViewModel.SearchInput.field_3) ? '' : SearchInputViewModel.SearchInput.field_3;
        //            case 2: $scope.searchInput.field_2 = angular.isUndefined(SearchInputViewModel.SearchInput.field_2) ? '' : SearchInputViewModel.SearchInput.field_2;
        //            case 1: $scope.searchInput.field_1 = angular.isUndefined(SearchInputViewModel.SearchInput.field_1) ? '' : SearchInputViewModel.SearchInput.field_1;
        //        }
        //        //Transaction id
        //        if (!angular.isUndefined(SearchInputViewModel.SearchInput.trans_id)) {
        //            $scope.searchInput.transactionId = SearchInputViewModel.SearchInput.trans_id;
        //        }
        //    }

        //    var searchType = (angular.isUndefined(obj.trans_id) || obj.trans_id == '' || obj.trans_id == null) ? 'ReferenceDataSearch' : 'TransactionSearch';

        //    //Perform search and get the results by calling the service method which send and receives responses
        //    refDataService.getReferenceDataSearch($rootScope.controllerName, SearchInputViewModel.SearchInput).success(function (searchRes) {
        //        var searchResultJson = searchRes;
        //        $scope.hideProcessingOverlay();
        //        $scope.showSearchMessage = false;
        //        //Invalid Search
        //        if (!searchResultJson.boolValidSearch) {
        //            $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.INVALID_SEARCH;
        //            $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.INVALID_SEARCH;
        //            $scope.showSearchMessage = true;
        //        }
        //            //No Records Found
        //        else if (searchResultJson.boolNoRecord) {
        //            $scope.SearchMessage = SEARCH_CONSTANTS.MESSAGE_TEXT.NO_SEARCH_RESULTS;
        //            $scope.SearchMessageColour = SEARCH_CONSTANTS.MESSAGE_TEXT_COLOUR.NO_SEARCH_RESULTS;
        //            $scope.showSearchMessage = true;
        //        }
        //            //Show the search results
        //        else {
        //            DataService.setSearchResults(searchResultJson.objSearchResults);
        //            DataService.setSearchType(searchType);
        //            $location.url(NAVIGATE_URL);
        //        }
        //    })
        //    .error(function (result) {
        //        errorPopups(result);
        //        $scope.hideProcessingOverlay();
        //    });

        //}

        //function errorPopups(result) {
        //    if (result == GEN_CONSTANTS.ACCESS_DENIED) {
        //        MessagePopup($rootScope, GEN_CONSTANTS.ACCESS_DENIED_CONFIRM, GEN_CONSTANTS.ACCESS_DENIED_MESSAGE);
        //    }
        //    else if (result == GEN_CONSTANTS.DB_ERROR) {
        //        MessagePopup($rootScope, GEN_CONSTANTS.DB_ERROR_CONFIRM, GEN_CONSTANTS.DB_ERROR_MESSAGE);
        //    }
        //}

        //function MessagePopup($rootScope, headerText, bodyText) {
        //    $rootScope.ModalPopupHeaderText = headerText;
        //    $rootScope.ModalPopupBodyText = bodyText;
        //    angular.element(MessageDialogBox).modal({ backdrop: "static" });
        //}

    }]);