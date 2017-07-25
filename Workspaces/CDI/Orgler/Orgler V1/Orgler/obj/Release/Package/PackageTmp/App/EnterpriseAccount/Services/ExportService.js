var BasePath = $("base").first().attr("href");

//Factory services to communicate with the client server
enterpriseAccMod.factory("exportService", ['$http',
    function ($http) {
        return {
            getDetailsExportData: function (detailsInput) {
                return $http.post(BasePath + "EnterpriseAccountService/ExportDetails/", detailsInput, {
                    headers: {
                        "Content-type": 'application/json'
                    },
                    "responseType": "arraybuffer"
                })
                //On success response, save the file to the local server
                .success(function (data) {
                    if (data.byteLength > 0) {
                        var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                        //  var blob = new Blob([data], { type: type });
                        saveAs(blob, detailsInput.strSectionName + " Detail.xlsx");
                    }
                });
            },
            SmokeTestingExportTransformationOrgDetails: function (input) {
                return $http.post(BasePath + "EnterpriseAccountService/SmokeTestingExportTransformationOrgDetails/", input, {
                    headers: {
                        "Content-type": 'application/json'
                    },
                    "responseType": "arraybuffer"
                })
                //On success response, save the file to the local server
                .success(function (data) {
                    if (data.byteLength > 0) {
                        var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                        //  var blob = new Blob([data], { type: type });
                        saveAs(blob, "Affiliation Matching Rule - Sample Organization Detail.xlsx");
                    }
                });
            }
        }

    }]);