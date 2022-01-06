using Claro.Helpers;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionDueDocumentNumber;
using System.Collections.Generic;
using DUEDOCUMENTNUMBER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionDueDocumentNumberModel;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class CollectionDueDocumentNumberModel : IExcel
    {
        [Header(Title = "ObjCollectionDueDocumentNumberModel")]
        public DUEDOCUMENTNUMBER.CollectionDueDocumentNumberModel ObjCollectionDueDocumentNumberModel { get; set; }

        [Header(Title = "ListCollectionDueDocumentNumberModelFija")]
        public List<DUEDOCUMENTNUMBER.CollectionDueDocumentNumberModel> ListCollectionDueDocumentNumberModelFija { get; set; }

        [Header(Title = "ListCollectionDueDocumentNumberModelMovil")]
        public List<DUEDOCUMENTNUMBER.CollectionDueDocumentNumberModel> ListCollectionDueDocumentNumberModelMovil { get; set; }

        public List<DocumentType> listDocumentType { get; set; }

    }
}