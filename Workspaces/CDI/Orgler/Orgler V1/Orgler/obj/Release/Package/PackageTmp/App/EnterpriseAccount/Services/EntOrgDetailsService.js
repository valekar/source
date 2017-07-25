var BasePath = $("base").first().attr("href");

//Factory services to communicate with the client server
enterpriseAccMod.factory("enterpriseDetailsService", ['$http',
    function ($http) {
        return {
            SaveMenuPreference: function (pref) {
                return $http.post(BasePath + 'EnterpriseAccountService/SaveMenuJSON', pref, {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetMenuPreference: function () {
                return $http.get(BasePath + 'EnterpriseAccountService/GetMenuJson', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetEntOrgDetailDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetEntOrgDetailDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetTransformationDetailDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetTransformationDetailDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetTagsDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetTagsDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetEntOrgHierarchyDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetEntOrgHierarchyDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetMasterBridgeDetails: function (input) {
                return $http.post(BasePath + 'EnterpriseAccountService/GetMasterBridgeDetails', input, {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetNaicsCodeStewDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetNaicsCodeStewDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetRankingStewDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetRankingStewDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetTransactionHistoryDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetTransactionHistoryDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            GetRFMDetails: function (entOrgId) {
                return $http.get(BasePath + 'EnterpriseAccountService/GetRFMDetails', {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    },
                    params: {
                        ent_org_id: entOrgId
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            //Service methods corresponding to the actions performed from the EO Details
            getTagDDList: function () {
                return $http.get(BasePath + 'EnterpriseAccountService/GetTagDDList', {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            UpdateTags: function (input) {
                return $http.post(BasePath + 'EnterpriseAccountService/UpdateTags', input, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            UpdateTransformations: function (input) {
                return $http.post(BasePath + 'EnterpriseAccountService/UpdateTransformations', input, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            SmokeTestingCount: function (input) {
                return $http.post(BasePath + 'EnterpriseAccountService/SmokeTestingCount', input, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            UpdateAffiliations: function (input) {
                return $http.post(BasePath + 'EnterpriseAccountService/UpdateAffiliations', input, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            },
            UpdateHierarchy: function (input) {
                return $http.post(BasePath + 'EnterpriseAccountService/UpdateHierarchy', input, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                });
            }
        }

    }]);