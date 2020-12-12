using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MiServicioStripe.Dtos
{
    public class DtoCreditDebitCard
    {
        [JsonProperty("exp_month")]
        public long? ExpMonth { get; set; }
        [JsonProperty("exp_year")]
        public long? ExpYear { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("address_city")]
        public string AddressCity { get; set; }
        [JsonProperty("address_country")]
        public string AddressCountry { get; set; }
        [JsonProperty("address_line1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("address_line2")]
        public string AddressLine2 { get; set; }
        [JsonProperty("address_state")]
        public string AddressState { get; set; }
        [JsonProperty("address_zip")]
        public string AddressZip { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("cvc")]
        public string Cvc { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("metadata")]
        public object MetaData { get; set; }
        [JsonProperty("issuing_card")]
        public string IssuingCardId { get; set; }
        #region Extras
        public string Correo { get; set; }
        /// <summary>
        /// Descripción que se muestra en el recibo de pago
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Descripción que se muestra en el recibo web de Stripe tiene un máximo de 22 caracteres
        /// </summary>
        public string DescripcionEstadoCuenta { get; set; }
        /// <summary>
        /// El monto mínimo es 0.50 USD o su conversión a la moneda del cargo establecida, es necesario hacer la conversión a centavos de la moneda establecida. Ej. 100 para $1.00 MXN
        /// </summary>
        public long MontoPagado { get; set; }
        #endregion
    }
}
