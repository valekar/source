angular.module('admin').controller("TabSecurityInsertCtrl", ['$scope', '$uibModalInstance', 'TabSecurityApiServices', 'ErrorService',
    function ($scope, $uibModalInstance, TabSecurityApiServices, ErrorService) {
       // console.log(params);
        //var row = {};
        var initialize = function () {
          //  row = params.row;
            $scope.tabSecurityInsert = {

                usr_nm: '',
               
                email_address: '',

                telephone_number:'',

                grp_nm_id: grpName(),
                grp_nm_value: grpName(),

                admin_tb_access_id: accessId(),
                admin_tb_access_value: accessValue(),

                case_tb_access_id: accessId(),
                case_tb_access_value: accessValue(),

                constituent_tb_access_id: accessId(),
                constituent_tb_access_value: accessValue(),

                domn_corctn_access_id: accessId(),
                domn_corctn_access_value: accessValue(),

                has_merge_unmerge_access_id: '0',
                is_approver_id: '0',

                locator_tab_access_id:accessId(),
                locator_tab_access_value:accessValue(),

                transaction_tb_access_id: accessId(),
                transaction_tb_access_value: accessValue(),

                upload_tb_access_id: accessId(),
                upload_tb_access_value: accessValue(),


            
                accessDropDown: TabSecurityApiServices.getWriteAccessValues(),
                groupNames : TabSecurityApiServices.getGroupNames()

            }

        }

        initialize();


        $scope.tabSecurityInsert.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }


        $scope.tabSecurityInsert.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {

                var postParams = {
                    usr_nm: $scope.tabSecurityInsert.usr_nm,
                   // grp_nm: $scope.tabSecurityInsert.grp_nm_id,
                   // email_address: $scope.tabSecurityInsert.email_address,
                   // telephone_number: $scope.tabSecurityInsert.telephone_number,

                    grp_nm: '',
                    email_address: '',
                    telephone_number: '',


                    constituent_tb_access: $scope.tabSecurityInsert.constituent_tb_access_id,
                    transaction_tb_access: $scope.tabSecurityInsert.transaction_tb_access_id,
                    case_tb_access: $scope.tabSecurityInsert.case_tb_access_id,
                    admin_tb_access: $scope.tabSecurityInsert.admin_tb_access_id,

                    upload_tb_access: $scope.tabSecurityInsert.upload_tb_access_id,
                    locator_tab_access: $scope.tabSecurityInsert.locator_tab_access_id,

                    domn_corctn_access: $scope.tabSecurityInsert.domn_corctn_access_id,
                    has_merge_unmerge_access: $scope.tabSecurityInsert.has_merge_unmerge_access_id,
                    is_approver: $scope.tabSecurityInsert.is_approver_id,

                    account_tb_access: accessId(),
                    enterprise_orgs_tb_access: accessId(),
                    reference_data_tb_access: accessId(),
                    report_tb_access: accessId(),
                    utitlity_tb_access: accessId(),
                    help_tb_access: accessId()
                }


                console.log(postParams);

                TabSecurityApiServices.insertTabSecurity(postParams).then(function (result) {
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



        function accessValue() {
            return 'No Access';
        }

        function accessId() {
            return 'N';
        }

        function grpName(){
            return '';
        }


    }]);