using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MauiAppEventos.Views;

namespace MauiAppEventos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new HomeView()))
            ///<summary>
            /// define o tamanho da tela do aplicativo
            ///</summary>
            {
                Title = "Gestão e Cadastro de Eventos",
                Width = 450,
                Height = 820
            };
        }
    }
}
