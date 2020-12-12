using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiServicioStripe.Dtos;
using MiServicioStripe.Models;
using Newtonsoft.Json;
using Stripe;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiServicioStripe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DtoStripeSecrets _stripeSecrets;
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(IOptions<DtoStripeSecrets> stripeSecrets, ILogger<PaymentController> logger)
        {
            _stripeSecrets = stripeSecrets.Value;
            _logger = logger;
        }
        // GET: api/<Payment>
        [HttpGet]
        public string Get()
        {
            return $"v{ GetType().Assembly.GetName().Version}";
        }


        [HttpGet("{montoPagado}")]
        public IActionResult CreatePayment(double montoPagado)
        {
            try
            {
                StripePayment stripePayment = new StripePayment(new DtoCreditDebitCard
                {
                    Name = "Armando Cárdenas",
                    Correo = "unsimpledeveloper@gmail.com",
                    AddressLine1 = "Avenida Siempreviva 742",
                    AddressCity = "Springfield",
                    AddressState = "Springfield",
                    AddressZip = "939",
                    Descripcion = $"Un pago del 2do Adviento Xamarin {DateTime.Now:d}",
                    DescripcionEstadoCuenta = $"Pago Stripe {DateTime.Now:d}",
                    MontoPagado = Convert.ToInt64(montoPagado * 100),
                    Currency = "MXN",
                    Number = "4242 4242 4242 4242",
                    ExpMonth = 8,
                    ExpYear = 2025,
                    Cvc = "123"//Debe viajar encriptado y desencriptarse en api core
                },_stripeSecrets);
                Charge charge = stripePayment.ProccessPayment();
                if (charge != null)
                {
                    //Retornamos el cargo (puede haberse hecho con éxito, con error o pendiente eso lo validará la app)
                    return Ok(charge);
                }

                return BadRequest();
            }
            catch (StripeException stripeException)
            {
                _logger.LogError(_stripeSecrets.SecretKey);
                throw;
            }
            catch (Exception generalEx)
            {
                _logger.LogError(generalEx.Message);
                throw;
            }
        }
    }
}
