angular.module('admin').controller("TabSecurityDeleteCtrl", ['$scope', 'params', '$uibModalInstance', 'TabSecurityApiServices','ErrorService',
function ($scope, params, $uibModalInstance, TabSecurityApiServices, ErrorService) {
    var row = {};
        console.log(params);
        var initialize = function () {
            row = params.row;
            $scope.tabSecurityDelete = {

                usr_nm: row.usr_nm,
                grp_nm: row.grp_nm,
                email_address: row.email_address,


                admin_tb_access_id: row.admin_tb_access,
                admin_tb_access_value: accessValue(row.admin_tb_access),

                case_tb_access_id: row.case_tb_access,
                case_tb_access_value: accessValue(row.case_tb_access),

                constituent_tb_access_id: row.constituent_tb_access,
                constituent_tb_access_value: accessValue(row.constituent_tb_access),
                //domn_corctn_access_id: row.domn_corctn_access,
                has_merge_unmerge_access_id: row.has_merge_unmerge_access,
                is_approver_id: row.is_approver,

                //locator_tab_access_id:row.locator_tab_access,
                transaction_tb_access_id: row.transaction_tb_access,
                transaction_tb_access_value: accessValue(row.transaction_tb_access),

                upload_tb_access_id: row.upload_tb_access,
                upload_tb_access_value: accessValue(row.upload_tb_access),


            }
        }
        initialize();

        $scope.tabSecurityDelete.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }


        $scope.tabSecurityDelete.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {

                var postParams = {
                    usr_nm: $scope.tabSecurityDelete.usr_nm,
                    grp_nm: $scope.tabSecurityDelete.grp_nm,
                    email_address: $scope.tabSecurityDelete.email_address,
                    telephone_number: $scope.tabSecurityDelete.telephone_number,
                    constituent_tb_access: $scope.tabSecurityDelete.constituent_tb_access_id,
                    transaction_tb_access: $scope.tabSecurityDelete.transaction_tb_access_id,
                    case_tb_access: $scope.tabSecurityDelete.case_tb_access_id,
                    admin_tb_access: $scope.tabSecurityDelete.admin_tb_access_id,

                    upload_tb_access: $scope.tabSecurityDelete.upload_tb_access_id,
                    locator_tab_access: row.locator_tab_access,

                    domn_corctn_access: row.domn_corctn_access,
                    has_merge_unmerge_access: $scope.tabSecurityDelete.has_merge_unmerge_access_id,
                    is_approver: $scope.tabSecurityDelete.is_approver_id,

                    account_tb_access: row.account_tb_access,
                    enterprise_orgs_tb_access: row.enterprise_orgs_tb_access,
                    reference_data_tb_access: row.reference_data_tb_access,
                    report_tb_access: row.report_tb_access,
                    utitlity_tb_access: row.utitlity_tb_access,
                    help_tb_access: row.help_tb_access
                }

                TabSecurityApiServices.deleteTabSecurity(postParams).then(function (result) {
                    myApp.hidePleaseWait();
                    //console.log(result);
                    var result = {
                        message: "Request has been processed successfully",
                        header: result.o_transOutput
                    }
                    $uibModalInstance.close(result);

                },
                function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    var result = {
                        header: error.data.o_transOutput,
                        message: "Unable to process the request"
                    }
                    ErrorService.messagePopup(result);
                });

            }
        }



        function accessValue(tabAccess) {
            if (tabAccess == 'N') {
                return 'No Access';
            }
            else if (tabAccess == 'R') {
                return 'Read';
            }
            else if (tabAccess == 'RW') {
                return 'Read Write';
            }
        }

    }]);