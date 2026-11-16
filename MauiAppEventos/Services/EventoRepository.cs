using System.Collections.ObjectModel;
using MauiAppEventos.Models;

namespace MauiAppEventos.Services
{
    /// <summary>
    /// Repositório em memória utilizando ObservableCollection para atualizar a interface automaticamente.
    /// </summary>
    public static class EventoRepository
    {
        public static ObservableCollection<Evento> ListaEventos { get; } = new ObservableCollection<Evento>();

        public static void Adicionar(Evento evento)
        {
            ListaEventos.Add(evento);
        }
    }
}
