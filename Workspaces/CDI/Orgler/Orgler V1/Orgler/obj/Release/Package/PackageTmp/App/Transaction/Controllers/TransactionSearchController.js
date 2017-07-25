angular.module('transaction').controller('TransactionSearchController', ['$scope', '$location', '$log', 'TransactionServices',
    '$window', 'TransactionDataServices', '$localStorage', 'TransactionClearDataService', 'mainService','$state','$rootScope',
function ($scope, $location, $log, TransactionServices, $window, TransactionDataServices, $localStorage, TransactionClearDataService, mainService, $state, $rootScope) {

    var NAVIGATE_URL = "/transaction/search/results";
    var username = "";

    var SearchPanelsCount = 5; // Define the maximum number of search panels
    var defaultPanelsCount = 1; // Set the number of search panels to be opened by default

    var appendString = 'ID'; // Specify the string that needs to be appended to distinguish between each elements

    var initialize = function () {

        var SearchParams = TransactionDataServices.getTransSavedSearchParams();

        $scope.TransSearchFormElements = []; //Initiate the variable

        if ($localStorage.lastTransSearchResultData)
            $scope.lastTransSearchResultPresent = true;
        else
            $scope.lastTransSearchResultPresent = false;

        $scope.SearchRows = defaultPanelsCount; // Set the Search Rows count to 0

        for (i = 1; i <= SearchPanelsCount; i++) { //All the IDs are now pushed into TransSearchFormElements
            $scope.TransSearchFormElements.push(appendString+i);
        }

        $scope.TransParams = { //Initialize the arrays of the form elements to empty
            SearchPanel:[],
            transactionNumber: [],
            transactionType: [],
            transactionStatus: [],
            transactionMasterId: [],
            transactionDateFrom: [],
            transDateFromPopup:[],
            transactionDateTo: [],
            transDateToPopup: [],
            transactionUserName: [],
            transUsernameState: [],
            SearchMeChkbx: [],
            PanelSeparator: [],
            showCloseButton: []
        }

        angular.forEach($scope.TransParams, function (paramskey, paramsvalue) {

          //  console.log("In ForEach");
           // console.log(paramskey);
           // console.log(paramsvalue);
            
            angular.forEach($scope.TransSearchFormElements, function (key) {
                $scope.TransParams[paramsvalue].push(key);
            });
        });

        for (i = 1; i <= $scope.SearchRows; i++) { //Show the Panels
            $scope.TransParams.SearchPanel[appendString + i] = true;
            $scope.TransParams.PanelSeparator[appendString + i] = true;
            $scope.TransParams.showCloseButton[appendString + i] = true;

            if(i==1)
                $scope.TransParams.showCloseButton[appendString + i] = false; //Hide the Close button for the first Search Panel
        }



        mainService.getUsername().then(function (result) {
            username = result.data;
        });

        $scope.inlineOptions = {
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1

        };
        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        angular.forEach($scope.TransParams.transactionDateFrom, function (key, value) {
            $scope.TransParams.transactionDateFrom[key] = null;
            $scope.TransParams.transDateFromPopup[key] = {
                opened:false
            }
        });

        angular.forEach($scope.TransParams.transactionDateTo, function (key, value) {
            $scope.TransParams.transactionDateTo[key] = null;
            $scope.TransParams.transDateToPopup[key] = {
                opened: false
            }
        });

        $scope.TransParams.openTransDateFrom = function (counter) {
            $scope.TransParams.transDateFromPopup[counter].opened = true;
        }

        $scope.TransParams.openTransDateTo = function (counter) {
            $scope.TransParams.transDateToPopup[counter].opened = true;
        }

        var postParams = {};

        $scope.pleaseWait = { "display": "none" };

        $scope.TransactionNumberRegex = /^[0-9]+$/;
        $scope.MasterIdRegex = /^[0-9]+$/;

        //Retain the search Params while navigating back and forth via breadcrums
        if (SearchParams) {
            var rootModel = SearchParams["TransactionSearchInputModel"];
          //  console.log("In Root Model");
          //  console.log(rootModel);
            if (rootModel.length > 1) {
                $scope.SearchRows = rootModel.length;
            }
            else {
                $scope.SearchRows = defaultPanelsCount;
            }
            for (i = 1; i <= rootModel.length; i++) {
                $scope.TransParams.SearchPanel[appendString + i] = true;
                $scope.TransParams.transactionNumber[appendString + i] = rootModel[i - 1]["TransactionKey"];
                $scope.TransParams.transactionType[appendString + i] = rootModel[i - 1]["TransactionType"] + "-" + rootModel[i - 1]["SubTransactionType"];
                $scope.TransParams.transactionStatus[appendString + i] = rootModel[i - 1]["Status"];
                $scope.TransParams.transactionMasterId[appendString + i] = rootModel[i - 1]["MasterId"];
                $scope.TransParams.transactionDateFrom[appendString + i] = rootModel[i - 1]["FromDate"];
                $scope.TransParams.transactionDateTo[appendString + i] = rootModel[i - 1]["ToDate"];
                $scope.TransParams.transactionUserName[appendString + i] = rootModel[i - 1]["UserName"];
                //$scope.TransParams.SearchMeChkbx[appendString + i] = $localStorage.transUserNameState[appendString + i];
                $scope.TransParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.TransParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }
        }

        TransactionServices.getTransactionRecentSearches().success(function (result) {

            $scope.TransactionRecentSearches = result;
           // console.log("Transaction recent search data");
           // console.log(result.data);

        }).error(function (result) {
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }

        });

    };

    initialize(); //Call the initialize() function

    $scope.SearchMeChkbxChange = function (SearchMeChkbxID) {
        if ($scope.TransParams.SearchMeChkbx[SearchMeChkbxID]) {
            $scope.TransParams.transactionUserName[SearchMeChkbxID] = username;
            $scope.TransParams.transUsernameState[SearchMeChkbxID] = true;
            $localStorage.transUserNameState = $scope.TransParams.transUsernameState;//To retain the me checkbox state
        }
        else {
            $scope.TransParams.transactionUserName[SearchMeChkbxID] = "";
            $scope.TransParams.transUsernameState[SearchMeChkbxID] = false;
            $localStorage.transUserNameState = $scope.TransParams.transUsernameState;
        }
    }

    //Get the recentmost search result data on clicking of the button from the mainService
    $scope.fetchLastTransSearchResult = function () {

        if ($localStorage.lastTransSearchResultData) {
            //Get back the last search result
            TransactionDataServices.setTransSearchResultsData($localStorage.lastTransSearchResultData);
            $state.go('transaction.search.results', {});
        }
        else
        {
            window.alert("LocalStorage seems just to be cleared out! Please proceed with the regular search");
        }
    }

    //when clicked in search go to search results and do a post there
    $scope.transactionSearch = function () {

        $scope.ConstNoResults = "";

        var searchFlag = true;

        var searchFlag

        for (i = 1; i <= $scope.SearchRows; i++) {
            if (($scope.TransParams.transactionNumber[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionNumber[appendString + i]))
                && ($scope.TransParams.transactionType[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionType[appendString + i]))
                && ($scope.TransParams.transactionStatus[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionStatus[appendString + i]))
                && ($scope.TransParams.transactionMasterId[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionMasterId[appendString + i]))
                && ($scope.TransParams.transactionDateFrom[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionDateFrom[appendString + i]) || $scope.TransParams.transactionDateFrom[appendString + i] == null)
                && ($scope.TransParams.transactionDateTo[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionDateTo[appendString + i]) || $scope.TransParams.transactionDateTo[appendString + i] == null)
                && ($scope.TransParams.transactionUserName[appendString + i] == "" || angular.isUndefined($scope.TransParams.transactionUserName[appendString + i])))
            {
               // console.log($scope.TransParams.transactionDateFrom[appendString + i]);
                searchFlag = false;
            }
            else
            {
                //If any of the opened panels has any input proceed with a search 
                searchFlag = true;
                break;
            }
        }

        if (searchFlag) {
            $scope.pleaseWait = { "display": "block" };
            postParams = TransactionDataServices.getTransactionSearchParams($scope, $scope.SearchRows, appendString);
         //   console.log("Before Firing to Adv Search");
          //  console.log(postParams);
            callTransactionService($scope, TransactionServices, TransactionDataServices, postParams, TransactionClearDataService,mainService,$rootScope,$localStorage, $location, NAVIGATE_URL);
        }
        else
        {
            $scope.ConstNoResults = "Please provide a search criteria input!";
        }
    }


    //When clicked clear the search page
    $scope.transactionClear = function () {
        TransactionServices.clearSearchParams($scope, SearchPanelsCount, defaultPanelsCount, appendString);
    }


    $scope.transactionRecentSearch = function (qry) {
        $scope.pleaseWait = { "display": "block" };
      //  console.log("Each transaction click request");
       // console.log("Query");
       // console.log(qry);
        var listPostParam = [];
        $('#recentSearchesModal').modal('hide');
        listPostParam.push(qry);
        postParams = listPostParam;

        callTransactionService($scope, TransactionServices, TransactionDataServices, qry, TransactionClearDataService,mainService,$rootScope,$localStorage, $location, NAVIGATE_URL);
    }

    $scope.AddNewSearchRow = function (RowCount) {
        if (RowCount < SearchPanelsCount) {
            RowCount++;
            $scope.SearchRows = RowCount; // Change the default search row count to the number of counts open 

            for (i = 1; i <= RowCount; i++) {
                $scope.TransParams.SearchPanel[appendString + i] = true;
                $scope.TransParams.PanelSeparator[appendString + i] = true;
                $scope.TransParams.showCloseButton[appendString + i] = true;
                if (i == 1) {
                    $scope.TransParams.showCloseButton[appendString + i] = false; // hide the close button for the fist search panel
                }
            }
        }
        else {
            alert("A maximum of only 5 search rows can be added.");
        }
    }

    $scope.RemoveRow = function (panelID) {

        //When you close a panel remove the corresponding search params also
        $scope.TransParams.transactionNumber[panelID] = "";
        $scope.TransParams.transactionType[panelID] = "";
        $scope.TransParams.transactionStatus[panelID] = "";
        $scope.TransParams.transactionMasterId[panelID] = "";
        $scope.TransParams.transactionDateFrom[panelID] = null;
        $scope.TransParams.transactionDateTo[panelID] = null;
        $scope.TransParams.transactionUserName[panelID] = "";

        $scope.TransParams.SearchPanel[panelID] = false;
        $scope.TransParams.PanelSeparator[panelID] = false;
        $scope.TransParams.showCloseButton[panelID] = false;

        if ($scope.SearchRows != 1)
            $scope.SearchRows--;
    }

}]);

angular.module('transaction').filter('placeholderfunc', function () {
    return function (input) {

        var TransResultsBindingData = "";
        angular.forEach(input.TransactionSearchInputModel, function (value, key) {
            if (value.MasterId != null)
                TransResultsBindingData += "Master ID: " + value.MasterId + ", ";
            if (value.TransactionKey != null)
                TransResultsBindingData += "Transaction Key: " + value.TransactionKey + ", ";
            if (value.TransactionType != null)
                TransResultsBindingData += "Transaction Type: " + value.TransactionType + ", ";
            if (value.SubTransactionType != null)
                TransResultsBindingData += "Sub Transaction Type: " + value.SubTransactionType + ", ";
            if (value.Status != null)
                TransResultsBindingData += "Status: " + value.Status + ", "
            if (value.FromDate != null)
                TransResultsBindingData += "From Date: " + value.FromDate + ", ";
            if (value.ToDate != null)
                TransResultsBindingData += "To Date: " + value.ToDate + ", ";
            if (value.UserName != null)
                TransResultsBindingData += "User Name: " + value.UserName + ", ";
            TransResultsBindingData += ";"
        });

        if (TransResultsBindingData != "")
            return TransResultsBindingData.slice(0, -3);
        else
            return TransResultsBindingData;

    }
});

function callTransactionService($scope, TransactionServices, TransactionDataServices, postParams, TransactionClearDataService,mainService,$rootScope,$localStorage, $location, NAVIGATE_URL) {

   // console.log("Into the calltransactionService method");
    TransactionDataServices.setTransSearchParams(postParams);
    TransactionServices.getTransactionAdvSearchResults(postParams).success(function (result) {

       // console.log("retreived adv search results");
        TransactionDataServices.setTransSearchResultsData(result);
        var _result = TransactionDataServices.getTransSearchResultsData();

       // console.log("Results returned");

       // console.log(_result);

        //Setting the last search result's data in the mainService

        $localStorage.lastTransSearchResultData = _result;

        //Setting the search params in local storage to use if user navigates from last search result

        if (!angular.isUndefined(postParams)) {
            $localStorage.transAdvSearchParams = postParams;
        }

        if (_result.length > 0) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else {
                $location.url(NAVIGATE_URL);
            }
        }
        else {
            //alert("no data");
            $scope.ConstNoResults = "No Results found!";
            $scope.pleaseWait = { "display": "none" };
        }
    }).error(function (result) {

        $scope.pleaseWait = { "display": "none" };

        if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
            messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
        }

    });


}


// this is used for storing name and master id and then show the same in the header 
angular.module('transaction').controller('TransactionheaderCtrl', ['$localStorage', '$scope', '$rootScope', '$window',
    function ($localStorage, $scope, $rootScope, $window) {
        $scope.pleaseWait = false;

        $rootScope.$on('$stateChangeStart',
            function (event, toState, toParams, fromState, fromParams) {
                $scope.pleaseWait = true;
            });


        $rootScope.$on('$viewContentLoaded',
        function (event, toState, toParams, fromState, fromParams) {

            $scope.pleaseWait = false;
        })

        $scope.display = 'none';

    }]);

