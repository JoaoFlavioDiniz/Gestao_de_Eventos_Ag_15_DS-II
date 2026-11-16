using System;
using System.Linq;
using Microsoft.Maui.Controls;
using MauiAppEventos.Models;
using MauiAppEventos.Services;

namespace MauiAppEventos.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            // Vincula a lista da tela à ObservableCollection estática em memória
            ListaEventosCollection.ItemsSource = EventoRepository.ListaEventos;
        }

        private async void OnIrParaCadastroClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroEventoView());
        }

        private async void OnEventoSelecionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Evento eventoSelecionado)
            {
                // Limpa a seleção visual e abre o resumo do evento clicado
                ListaEventosCollection.SelectedItem = null;
                await Navigation.PushAsync(new ResumoEventoView(eventoSelecionado));
            }
        }
    }
}
