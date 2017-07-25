using System.Web;
using System.Web.Optimization;

namespace Orgler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Scripts/bootstrap-daterangepicker-master/daterangepicker.css",
                      "~/Content/multiple-select.min.css",
                      "~/Content/select.min.css","~/Content/menustyles.css", "~/Content/Tab.css"));

            bundles.Add(new StyleBundle("~/Content/customcss").Include(
                      "~/StyleSheets/Custom.css"
                      //"~/StyleSheets/Tab.css",
                      //"~/StyleSheets/myCSS.css"
                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",                      
                      "~/Scripts/angular-route.js",
                       "~/Scripts/angular-touch.js",
                      "~/Scripts/ui-grid.min.js",
                      "~/Scripts/angular-resource.js",
                       "~/Scritps/angular-aria.js",
                      "~/Scritps/angular-animate.js",                                         
                      "~/Scripts/ui-bootstrap-tpls-new.js",
                      "~/Scripts/angular-ui-router.js",
                      "~/Scripts/ngStorage.min.js",
                      "~/Scripts/angular-backtop.js",
                      "~/Scripts/FileSaver.min.js",
                      "~/Scripts/bootstrap-daterangepicker-master/moment.js",
                      "~/Scripts/bootstrap-daterangepicker-master/daterangepicker.js",
                      "~/Scripts/angular-daterangepicker-master/js/angular-daterangepicker.js",
                      "~/Scripts/multiple-select.min.js",
                      "~/Scripts/select.min.js",
                      "~/Scripts/angular-sanitize.min.js",
                      "~/Scripts/draggable-rows.js"));

            bundles.Add(new ScriptBundle("~/App/constituent.js").Include(
                     //"~/App/App.js",

                     "~/App/Constituent/Directives/ConstituentDirectives.js",
                     "~/App/Constituent/Services/ConstituentServices.js",
                    "~/App/Constituent/Services/ConstituentMultiService.js",
                     "~/App/Constituent/Services/ConstituentHelper.js",
                     "~/App/Constituent/Services/CRUDService.js",
                     "~/App/Constituent/Services/ExtendedServices.js",

                     "~/App/Constituent/Controllers/ConstituentController.js",
                     "~/App/Constituent/Controllers/ConstSearchResultsController.js",
                     "~/App/Constituent/Controllers/ConstituentDetailsController.js",
                     "~/App/Constituent/Controllers/ConstMultiDetailsController.js",
                     "~/App/Constituent/Controllers/ConstHomeDetailsController.js",

                     "~/App/Constituent/Controllers/ConstCartController.js",
                     "~/App/Constituent/Controllers/ConstMergeController.js",
                     "~/App/Constituent/Controllers/ConstUnmergeController.js",

                     "~/App/Constituent/Controllers/InstantiatedControllers/ErrorInstantiateCtrl.js",

                     "~/App/Constituent/Controllers/InstantiatedControllers/NameInstantiateControllers.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/PhoneInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/EmailInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/DeathInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/BirthInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/AddressInstantiateControllers.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/ContactPrefInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/GroupInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/CharacterInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/CaseCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/AffiliatorCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/EmailDomainInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/InstantiatedControllers/NAICSCodesInstantiateCtrl.js",
                     "~/App/Constituent/Controllers/ExtendedControllers/RelationshipCtrl.js"
                     
                    ));

            bundles.Add(new ScriptBundle("~/App/transaction.js").Include(

                    "~/App/Transaction/Directives/TransactionDirectives.js",

                    "~/App/Transaction/Services/TransactionServices.js",
                    "~/App/Transaction/Services/TransactionMultiServices.js",
                    "~/App/Transaction/Services/CaseService/CaseServices.js",
                    "~/App/Transaction/Services/TransactionHelper.js",

                    "~/App/Transaction/Controllers/TransactionSearchController.js",
                    "~/App/Transaction/Controllers/TransactionSearchController.js",
                    "~/App/Transaction/Controllers/TransactionSearchResultsController.js",

                    "~/App/Transaction/Controllers/TransactionMultiDetailsController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsBirthController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsPersonNameController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsOrgNameController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAddressController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAffiliatorHierarchyController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAffiliatorTagsController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAffiliatorController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsCharacteristicsController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsContactPreferenceController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEmailController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEmailOnlyUploadController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsMergeController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsGroupMembershipUploadController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsOrgTransformsController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsPhoneController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsDeathController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsUnmergeController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/AssociateCaseCtrl.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEOAffiliationUploadController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEOSiteUploadController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEOUploadController.js"
                    ));

            bundles.Add(new ScriptBundle("~/App/newacount.js").Include(
                     "~/App/App.js",
                     "~/App/NewAccount/Directives/Directives.js",
                     "~/App/NewAccount/Services/HelperService.js",
                    "~/App/NewAccount/Services/DataService.js",
                     "~/App/NewAccount/Services/Service.js",
                      "~/App/NewAccount/Services/ConstituentServices.js",
                       "~/App/NewAccount/Services/ConstituentHelper.js",
                         "~/App/NewAccount/Services/CRUDService.js",
                         "~/App/NewAccount/Services/ConstituentMultiService.js",

                     "~/App/NewAccount/Controllers/SearchResultsController.js",
                     "~/App/NewAccount/Controllers/SearchController.js",
                      "~/App/NewAccount/Controllers/ConstituentDetailsController.js",
                     "~/App/NewAccount/Controllers/ConstMultiDetailsController.js",
                      "~/App/NewAccount/Controllers/ConstCartController.js",
                      "~/App/NewAccount/Controllers/ConstMergeController.js",
                     "~/App/NewAccount/Controllers/ConstUnmergeController.js",

                      "~/App/NewAccount/Controllers/InstantiatedControllers/NameInstantiateControllers.js",
                       "~/App/NewAccount/Controllers/InstantiatedControllers/AddressInstantiateControllers.js",
                        "~/App/NewAccount/Controllers/InstantiatedControllers/PhoneInstantiateCtrl.js",
                         "~/App/NewAccount/Controllers/InstantiatedControllers/EmailInstantiateCtrl.js"
                    ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
