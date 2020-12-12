using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiServicioStripe.Dtos
{
    public class DtoStripeSecrets
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
