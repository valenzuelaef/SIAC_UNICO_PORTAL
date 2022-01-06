﻿
using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag
{
    public class AccountSharedBag : IExcel
    {

        public string Account { get; set; }
        [Header(Title = "Grupo de la Bolsa", Order = Claro.Constants.NumberZero)]
        public string GroupBag { get; set; }
        [Header(Title = "Nombre Comercial de la Bolsa", Order = Claro.Constants.NumberOne)]
        public string Bag { get; set; }
        public string Line { get; set; }
        [Header(Title = "Tipo de Unidad", Order = Claro.Constants.NumberTwo)]
        public string Unit { get; set; }
        [Header(Title = "Unidades Asignadas", Order = Claro.Constants.NumberThree)]
        public string Stopper { get; set; }
        [Header(Title = "Consumo", Order = Claro.Constants.NumberFour)]
        public string Consumption { get; set; }
        [Header(Title = "Saldo Disponible", Order = Claro.Constants.NumberFive)]
        public string Balance { get; set; }
        [Header(Title = "Fecha de Expiración", Order = Claro.Constants.NumberSix)]
        public string DateValidity { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public int CountBag { get; set; }
        public int CountBagLine { get; set; }
    }
}