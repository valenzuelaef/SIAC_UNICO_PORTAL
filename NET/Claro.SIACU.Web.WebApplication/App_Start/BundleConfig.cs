using System.Web.Optimization;

namespace Claro.SIACU.Web.WebApplication.App_Start
{
    static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
//#if DEBUG
            BundleTable.EnableOptimizations = true;
//#endif

            bundles.Add(new StyleBundle("~/bundles/bootstrap-addon-css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/dataTables.bootstrap.min.css",
                "~/Content/css/bootstrap-select.min.css",
                "~/Content/css/bootstrap-multiselect.css",
                "~/Content/css/jquery.dataTables.select.css",
                "~/Content/css/datepicker.css",
                "~/Content/css/jquery.smartmenus.bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery-addon-css")
                .Include("~/Content/css/jquery-ui.css",
                        "~/Content/css/jquery.bar.css"
                         ));

            bundles.Add(new StyleBundle("~/bundles/Site-css").Include(
               "~/Content/css/Site.css",
               "~/Content/css/SiteError.css",
               "~/Content/css/Header.css",
               "~/Content/css/Footer.css",
               "~/Content/css/MyContainer.css",
               "~/Content/css/TreeView.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/Lib/jquery-2.0.0.js",
                "~/Content/Lib/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/Lib/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/Lib/bootstrap.js",
                 "~/Content/Lib/bootstrap-select.min.js",
                "~/Content/Lib/bootstrap-multiselect.js",
                "~/Content/Lib/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-addon").Include(
                "~/Content/Lib/jquery.dataTables.min.js",
                "~/Content/Lib/jquery.dataTables.select.js",
                "~/Content/Lib/jquery.blockUI.js",
                "~/Content/Lib/jquery.smartmenus.js",
                "~/Content/Lib/jquery.smartmenus.bootstrap.js",
                "~/Content/Lib/jquery.numeric.js",
                 "~/Content/Lib/dataTables.bootstrap.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/moment-js")
                .Include("~/Content/Lib/moment.js",
                         "~/Content/Lib/moment-es.js"));

            bundles.Add(new ScriptBundle("~/bundles/Claro")
                .Include("~/Content/Scripts/ClaroSession.js",
                "~/Content/Scripts/ClaroRedirect.js",
                "~/Content/Scripts/plupload.full.min.js",
                "~/Content/Scripts/ClaroModalTemplate.js",
                
                "~/Content/Scripts/ClaroModalLoad.js",
                "~/Content/Scripts/ClaroCommon.js",
                "~/Content/Scripts/ClaroAppCommon.js",
                "~/Content/Scripts/ClaroUtils.js"));

            bundles.Add(new ScriptBundle("~/bundles/ClaroLayout")
             .Include("~/Content/Scripts/ClaroLayout.js"));

            bundles.Add(new ScriptBundle("~/bundles/ClaroLayoutPopup")
                .Include("~/Content/Scripts/ClaroLayoutPopup.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/CustomerAlert").Include("~/Areas/Dashboard/Scripts/Home/CustomerAlert.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/CustomersBusinessNames").Include("~/Areas/Dashboard/Scripts/Home/CustomersBusinessNames.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/CustomersDocument").Include("~/Areas/Dashboard/Scripts/Home/CustomersDocument.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/CustomersNames").Include("~/Areas/Dashboard/Scripts/Home/CustomersNames.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/Instants").Include("~/Areas/Dashboard/Scripts/Home/Instants.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/ProductDetails").Include("~/Areas/Dashboard/Scripts/Home/ProductDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Home/CustomerProducts").Include("~/Areas/Dashboard/Scripts/Home/CustomerProducts.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Postpaid/PostpaidDataAccount").Include("~/Areas/Dashboard/Scripts/Postpaid/PostpaidDataAccount.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Postpaid/PostpaidDataBilling").Include("~/Areas/Dashboard/Scripts/Postpaid/PostpaidDataBilling.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Postpaid/PostpaidDataCustomer").Include("~/Areas/Dashboard/Scripts/Postpaid/PostpaidDataCustomer.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Postpaid/PostpaidDataCollection").Include("~/Areas/Dashboard/Scripts/Postpaid/PostpaidDataCollection.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Postpaid/PostpaidDataService").Include("~/Areas/Dashboard/Scripts/Postpaid/PostpaidDataService.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Postpaid/Postpaid").Include("~/Areas/Dashboard/Scripts/Postpaid/Postpaid.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountAnnotation").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountAnnotation.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountAnnotationList").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountAnnotationList.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountCreditLimit").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountCreditLimit.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountPinPuk").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountPinPuk.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountRelationPlan").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountRelationPlan.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountRelationPlanHFCLTE").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountRelationPlanHFCLTE.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidSubAccount").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidSubAccount.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountContract").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountContract.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountContractList").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountContractList.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountWarranty").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountWarranty.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/AdditionalLocalTrafficDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/AdditionalLocalTrafficDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/ConsumeLocalTrafficDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/ConsumeLocalTrafficDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/DebtDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/DebtDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/DetailLongDistance").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/DetailLongDistance.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/FixedCharge").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/FixedCharge.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/FixedChargeBagDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/FixedChargeBagDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/FixedChargeBagDetailOne").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/FixedChargeBagDetailOne.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/FixedChargeBagDetailThree").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/FixedChargeBagDetailThree.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/FixedChargeBagDetailTwo").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/FixedChargeBagDetailTwo.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/HistoricDelivery").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/HistoricDelivery.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/HistoryHR").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/HistoryHR.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/HistoryInvoice").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/HistoryInvoice.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/InternationalRoamingDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/InternationalRoamingDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/OtherCharges").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/OtherCharges.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/PrintTimServiceDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/PrintTimServiceDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/SMSDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/SMSDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/LocalTrafficDetailTM").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/LocalTrafficDetailTM.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/ConsumeLocalTrafficDetailTM").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/ConsumeLocalTrafficDetailTM.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/LocalTrafficDetailTP").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/LocalTrafficDetailTP.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataBilling/ConsumeLocalTrafficDetailTP").Include("~/Areas/Dashboard/Scripts/PostpaidDataBilling/ConsumeLocalTrafficDetailTP.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/MonitoringCases").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/MonitoringCases.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/DetailMonitoringCases").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/DetailMonitoringCases.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/PostpaidDueNumberDocument").Include("~/Areas/Dashboard/Scripts/Postpaid/PostpaidDueNumberDocument.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/PaymentCommitment").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/PaymentCommitment.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/StatusAccount").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/StatusAccount.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/StatusAccountHR").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/StatusAccountHR.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/StatusAccountLDI").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/StatusAccountLDI.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/AssociatedClaims").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/AssociatedClaims.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/DeferredCollections").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/DeferredCollections.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/UnpaidDebt").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/UnpaidDebt.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostPaidDataCollection/ExpiredDebt").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/ExpiredDebt.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCustomer/AddressOffice").Include("~/Areas/Dashboard/Scripts/PostpaidDataCustomer/AddressOffice.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCustomer/CustomerData").Include("~/Areas/Dashboard/Scripts/PostpaidDataCustomer/CustomerData.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCustomer/CustomerSegment").Include("~/Areas/Dashboard/Scripts/PostpaidDataCustomer/CustomerSegment.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Prepaid/DataCall").Include("~/Areas/Dashboard/Scripts/PrepaidDataCall/DataCall.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Prepaid/ClaroValidate").Include("~/Content/Scripts/ClaroValidate.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Prepaid/DataCustomer").Include("~/Areas/Dashboard/Scripts/PrepaidDataCustomer/DataCustomer.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Prepaid/DataReload").Include("~/Areas/Dashboard/Scripts/PrepaidDataReload/DataReload.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Prepaid/DataService").Include("~/Areas/Dashboard/Scripts/PrepaidDataService/DataService.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/Prepaid/Prepaid").Include("~/Areas/Dashboard/Scripts/Prepaid/Prepaid.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCall/DetailVisualizeCall").Include("~/Areas/Dashboard/Scripts/PrepaidDataCall/DetailVisualizeCall.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCall/ExportExcelCall").Include("~/Areas/Dashboard/Scripts/PrepaidDataCall/ExportExcelCall.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCall/VisualizeCall").Include("~/Areas/Dashboard/Scripts/PrepaidDataCall/VisualizeCall.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCustomer/CreateNotUser").Include("~/Areas/Dashboard/Scripts/PrepaidDataCustomer/CreateNotUser.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCustomer/CustomerPast").Include("~/Areas/Dashboard/Scripts/PrepaidDataCustomer/CustomerPast.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCustomer/CustomerQuery").Include("~/Areas/Dashboard/Scripts/PrepaidDataCustomer/CustomerQuery.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCustomer/CustomerSegment").Include("~/Areas/Dashboard/Scripts/PrepaidDataCustomer/CustomerSegment.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker")
                .Include("~/Content/Lib/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/ContactoGrilla")
                .Include("~/Content/Scripts/ContactoGrilla.js"));


            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidOrderConsumption").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidOrderConsumption.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidHistoricalPackage").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidHistoricalPackage.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/ConsumptionHistoricalRecharge").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/ConsumptionHistoricalRecharge.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidHistoricalRecharge").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidHistoricalRecharge.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidBalanceShared").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidBalanceShared.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidComputerQuery").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidComputerQuery.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidConsumptionBalance").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidConsumptionBalance.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidConsumptionBalanceHFCLTE").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidConsumptionBalanceHFCLTE.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/Equipments").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/Equipments.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidHistoricalStriations").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidHistoricalStriations.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/HistoricoSIM").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/HistoricoSIM.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidHistoryActions").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidHistoryActions.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/LineHistory").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/LineHistory.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidLoanRental").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidLoanRental.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidPetitions").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidPetitions.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidPhoneList").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidPhoneList.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidPhoneListAlt").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidPhoneListAlt.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidPhoneListAltHistory").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidPhoneListAltHistory.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidPinPuk").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidPinPuk.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PlanHistory").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PlanHistory.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidScheduledTasks").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidScheduledTasks.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidServices").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidServices.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidServicesBSCS").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidServicesBSCS.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidStriations").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidStriations.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidBalanceCBIOS").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidBalanceCBIOS.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataReload/DetailVisualizeReload").Include("~/Areas/Dashboard/Scripts/PrepaidDataReload/DetailVisualizeReload.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataReload/VisualizeReload").Include("~/Areas/Dashboard/Scripts/PrepaidDataReload/VisualizeReload.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataService/HistoricalBonds").Include("~/Areas/Dashboard/Scripts/PrepaidHistoricalBonds/HistoricalBonds.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataService/HistoricalStriations").Include("~/Areas/Dashboard/Scripts/PrepaidHistoricalStriations/HistoricalStriations.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataService/PinPuk").Include("~/Areas/Dashboard/Scripts/PrepaidDataService/PinPuk.js"));

            bundles.Add(new ScriptBundle("~/bundles/ClaroOptionsDefault").Include("~/Content/Scripts/ClaroOptionsDefault.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidExamineStock").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidExamineStock.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCall/PrintCall").Include("~/Areas/Dashboard/Scripts/PrepaidDataCall/PrintCall.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/PostpaidFeeEquipment").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/PostpaidFeeEquipment.js"));


            //news bundles 21-05-218 INI

            //SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/Cases/WipBin/WipBin").Include("~/Areas/Cases/Scripts/WipBin/WipBin.js"));
            bundles.Add(new ScriptBundle("~/bundles/Cases/Search/Search").Include("~/Areas/Cases/Scripts/Search/Search.js"));
            bundles.Add(new ScriptBundle("~/bundles/Cases/Queue/Queue").Include("~/Areas/Cases/Scripts/Queue/Queue.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/ManagementInstant/ManagementInstant").Include("~/Areas/Management/Scripts/ManagementInstant.js",
                                                                                                             "~/Areas/Management/Scripts/InstantManagement.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/InstantManagement/InstantManagement").Include("~/Areas/Management/Scripts/InstantManagement.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/VisualizeInstant/VisualizeInstant").Include("~/Areas/Management/Scripts/VisualizeInstant.js",
                                                                                                           "~/Areas/Management/Scripts/InstantManagement.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/DetailVisualizeInstant/DetailVisualizeInstant").Include("~/Areas/Management/Scripts/DetailVisualizeInstant.js",
                                                                                                                       "~/Areas/Management/Scripts/VisualizeInstant.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/MasiveInstantFail/MasiveInstantFail").Include("~/Areas/Management/Scripts/MasiveInstantFail.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/MassiveInstant/MassiveInstant").Include("~/Areas/Management/Scripts/MassiveInstant.js",
                                                                                                       "~/Areas/Management/Scripts/InstantManagement.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/Banner/Modify").Include("~/Areas/Management/Scripts/Banner/Modify.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/Banner/Deactivate").Include("~/Areas/Management/Scripts/Banner/Deactivate.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/Banner/Create").Include("~/Areas/Management/Scripts/Banner/Create.js"));
            bundles.Add(new ScriptBundle("~/bundles/Management/Banner/BannerList").Include("~/Areas/Management/Scripts/Banner/BannerList.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataService/Salesdues").Include("~/Areas/Dashboard/Scripts/PrepaidDataService/Salesdues.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataService/PinkpukSearch").Include("~/Areas/Dashboard/Scripts/PrepaidDataService/PinkpukSearch.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidHistoricalBalance/HistoricalBalance").Include("~/Areas/Dashboard/Scripts/PrepaidHistoricalBalance/HistoricalBalance.js"));   
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataReload/OnlineReload").Include("~/Areas/Dashboard/Scripts/PrepaidDataReload/OnlineReload.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataReload/ConsumptionDataPacket").Include("~/Areas/Dashboard/Scripts/PrepaidDataReload/ConsumptionDataPacket.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PrepaidDataCall/InLineCall").Include("~/Areas/Dashboard/Scripts/PrepaidDataCall/InLineCall.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidHistoryFrequent").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidHistoryFrequent.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataService/PostpaidFrequent").Include("~/Areas/Dashboard/Scripts/PostpaidDataService/PostpaidFrequent.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCustomer/Contact").Include("~/Areas/Dashboard/Scripts/PostpaidDataCustomer/Contact.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/TrackingDetail").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/TrackingDetail.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/ClosuresAndReclosures").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/ClosuresAndReclosures.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataAccount/PostpaidAccountSharedBag").Include("~/Areas/Dashboard/Scripts/PostpaidDataAccount/PostpaidAccountSharedBag.js"));

            //CSS
            bundles.Add(new StyleBundle("~/bundles/Dashboard/Css/FormatCss-css").Include(
              "~/Areas/Dashboard/Css/FormatCss.css"
              ));
            //news bundles 21-05-218 FIN

            //news bundles PROY-140073 Coliving
            //CSS
            bundles.Add(new StyleBundle("~/bundles/Coliving/Content/StyleColiving").Include("~/Areas/Coliving/Content/css/StyleColiving.css"));

            //JS
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_TypeSearchCustomer").Include("~/Areas/Coliving/Scripts/SearchCustomer/TypeSearchCustomer.js"));
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_SearchCustomerAccount").Include("~/Areas/Coliving/Scripts/SearchCustomer/SearchCustomerAccount.js"));
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_BusquedaClienteLinea").Include("~/Areas/Coliving/Scripts/SearchCustomer/SearchCustomerLine.js"));
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_CriteriaSale").Include("~/Areas/Coliving/Scripts/SearchCustomer/CriteriaSale.js"));
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_CustomerDocument").Include("~/Areas/Coliving/Scripts/SearchCustomer/SearchCustomerDocument.js"));
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_ListService").Include("~/Areas/Coliving/Scripts/SearchCustomer/ListService.js"));
            bundles.Add(new ScriptBundle("~/bundles/Coliving/SearchCustomer/SIACU_ListTechnicalServiceOST").Include("~/Areas/Coliving/Scripts/SearchCustomer/ListTechnicalServiceOST.js"));

            //PROY-140464 Ajustes - INI
            bundles.Add(new ScriptBundle("~/bundles/Dashboard/PostpaidDataCollection/CancelInvoice").Include("~/Areas/Dashboard/Scripts/PostpaidDataCollection/CancelInvoice.js"));

            //news bundles Fin Coliving 
            bundles.Add(new ScriptBundle("~/bundles/Content/Lib/BloqueoF12").Include("~/Content/Lib/BloqueoF12.js"));
        }
    }
}