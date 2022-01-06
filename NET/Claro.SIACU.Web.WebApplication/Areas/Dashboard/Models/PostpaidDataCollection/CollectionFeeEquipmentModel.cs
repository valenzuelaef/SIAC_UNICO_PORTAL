using Claro.Helpers;
using System.Collections.Generic;
using FEEQUIPMENT_MODEL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionFeeEquipment;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class CollectionFeeEquipmentModel
    {
        [Header(Title = "ClientId")]
        public string ClientId { get; set; }
        [Header(Title = "NameClient")]
        public string NameClient { get; set; }
        [Header(Title = "NumberServices")]
        public string NumberServices { get; set; }

        [Header(Title = "ImportOrigin")]
        public string ImportOrigin { get; set; }
        [Header(Title = "ImportPending")]
        public string ImportPending { get; set; }


        [Header(Title = "objFeeEquipment")]
        public FEEQUIPMENT_MODEL.CollectionFeeEquipment objFeeEquipment { get; set; }

        [Header(Title = "listFeeEquipmentModel")]
        public List<FEEQUIPMENT_MODEL.CollectionFeeEquipment> listFeeEquipmentModel { get; set; }

        [Header(Title = "listFeeEquipmentModelMovil")]
        public List<FEEQUIPMENT_MODEL.CollectionFeeEquipmentMovil> listFeeEquipmentModelMovil { get; set; }
    }
}