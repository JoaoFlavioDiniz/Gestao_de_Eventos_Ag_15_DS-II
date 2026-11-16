using System;
using Microsoft.Maui.Controls;
using MauiAppEventos.Models;

namespace MauiAppEventos.Views
{
    public partial class ResumoEventoView : ContentPage
    {
        public ResumoEventoView(Evento evento)
        {
            InitializeComponent();
            BindingContext = evento;
        }

        private async void OnVoltarCadastroClicked(object sender, EventArgs e)
        {
            // Retorna direto para a pagina cadastro de eventos
            await Navigation.PopAsync();
        }

        private async void OnIrParaInicioClicked(object sender, EventArgs e)
        {
            // Retorna direto para a pagina inicial
            await Navigation.PopToRootAsync();
        }
    }
}
