using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdvientoXamarinDemo.Models;
using Stripe;
using Xamarin.Forms;

namespace AdvientoXamarinDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Declaraciones

        public DelegateCommand CobrarCommand { get; set; }
        private string _colorMensaje="Red";

        public string ColorMensaje
        {
            get { return _colorMensaje; }
            set { SetProperty(ref _colorMensaje, value); }
        }
        private string _mensaje;

        public string Mensaje
        {
            get { return _mensaje; }
            set { SetProperty(ref _mensaje, value); }
        }

        private double monto;

        public double Monto
        {
            get { return monto; }
            set { SetProperty(ref monto, value); }
        }
        #endregion
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Cobros con Stripe";
            CobrarCommand=new DelegateCommand(Cobrar);
        }

        private async void Cobrar()
        {
            try
            {
               Charge cargoGenerado= await new ClientHttp().Get<Charge>($"/api/Payment/{monto}");
               switch (cargoGenerado.Status.ToLower())
               {
                    case "succeeded":
                        Mensaje = $"Se cobraron {Monto:C}, gracias por su compra.";
                        ColorMensaje = "Green";
                        break;
                    case "pending":
                        Mensaje = $"El pago de {Monto:C} esta pendiente por autorizar de parte de su banco.";
                        ColorMensaje = "Orange";
                        break;
                    case "failed":
                        Mensaje = $"No pudimos cobrar {Monto:C}, por favor intente más tarde.";
                        ColorMensaje = "Red";
                        break;
                }
               Monto = 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ColorMensaje = "Red";
                Mensaje = $"Error al cobrar: {e.Message}";
            }
        }
    }
}
