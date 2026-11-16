using Microsoft.Maui.Controls;
using MauiAppEventos.ViewModels;

namespace MauiAppEventos.Views
{
    public partial class CadastroEventoView : ContentPage
    {
        public CadastroEventoView()
        {
            InitializeComponent();
            BindingContext = new CadastroEventoViewModel();
        }
    }
}
