using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiAppEventos.Models;
using MauiAppEventos.Services;
using MauiAppEventos.Views;
using Microsoft.Maui.Controls;

namespace MauiAppEventos.ViewModels
{
    public class CadastroEventoViewModel : INotifyPropertyChanged
    {
        private readonly Evento _evento = new Evento();

        private string _numeroParticipantesTexto = string.Empty;
        private string _custoPorParticipanteTexto = string.Empty;
        private string _mensagemErro = string.Empty;

        public string Nome
        {
            get => _evento.Nome;
            set
            {
                if (_evento.Nome != value)
                {
                    _evento.Nome = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DataInicio
        {
            get => _evento.DataInicio;
            set
            {
                if (_evento.DataInicio != value)
                {
                    _evento.DataInicio = value;
                    OnPropertyChanged();
                    if (_evento.DataTermino < _evento.DataInicio)
                    {
                        DataTermino = _evento.DataInicio;
                    }
                    AtualizarPreviaCalculos();
                }
            }
        }

        public DateTime DataTermino
        {
            get => _evento.DataTermino;
            set
            {
                if (_evento.DataTermino != value)
                {
                    _evento.DataTermino = value;
                    OnPropertyChanged();
                    AtualizarPreviaCalculos();
                }
            }
        }

        public string Local
        {
            get => _evento.Local;
            set
            {
                if (_evento.Local != value)
                {
                    _evento.Local = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NumeroParticipantesTexto
        {
            get => _numeroParticipantesTexto;
            set
            {
                if (_numeroParticipantesTexto != value)
                {
                    _numeroParticipantesTexto = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CustoPorParticipanteTexto
        {
            get => _custoPorParticipanteTexto;
            set
            {
                if (_custoPorParticipanteTexto != value)
                {
                    _custoPorParticipanteTexto = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MensagemErro
        {
            get => _mensagemErro;
            set
            {
                if (_mensagemErro != value)
                {
                    _mensagemErro = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TemErro));
                }
            }
        }

        public bool TemErro => !string.IsNullOrEmpty(MensagemErro);

        public string PreviaDuracao => $"Diferença (TimeSpan): {_evento.DiferencaDatas.Days} dia(s)";

        public ICommand ConcluirCadastroCommand { get; }
        public ICommand VoltarCommand { get; }

        public CadastroEventoViewModel()
        {
            ConcluirCadastroCommand = new Command(async () => await ConcluirCadastroAsync());
            VoltarCommand = new Command(async () => await VoltarParaInicioAsync());
        }

        private void AtualizarPreviaCalculos()
        {
            OnPropertyChanged(nameof(PreviaDuracao));
        }

        private async System.Threading.Tasks.Task VoltarParaInicioAsync()
        {
            var paginaAtual = Application.Current?.Windows[0].Page;
            if (paginaAtual != null)
            {
                await paginaAtual.Navigation.PopAsync();
            }
        }

        private async System.Threading.Tasks.Task ConcluirCadastroAsync()
        {
            MensagemErro = string.Empty;

            if (string.IsNullOrWhiteSpace(Nome))
            {
                MensagemErro = "Por favor, informe o nome do evento.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Local))
            {
                MensagemErro = "Por favor, informe o local do evento.";
                return;
            }

            if (DataTermino.Date < DataInicio.Date)
            {
                MensagemErro = "A data de término não pode ser anterior à data de início.";
                return;
            }

            if (!int.TryParse(NumeroParticipantesTexto, out int participantes) || participantes <= 0)
            {
                MensagemErro = "Informe um número de participantes válido (inteiro maior que zero).";
                return;
            }

            string custoNormalizado = CustoPorParticipanteTexto.Replace(",", ".");
            if (!decimal.TryParse(custoNormalizado, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal custo) || custo < 0)
            {
                MensagemErro = "Informe um custo por participante válido (valor igual ou maior que zero).";
                return;
            }

            _evento.NumeroParticipantes = participantes;
            _evento.CustoPorParticipante = custo;

            // Salva o evento em memória na ObservableCollection (Opção 1)
            EventoRepository.Adicionar(_evento);

            var paginaAtual = Application.Current?.Windows[0].Page;
            if (paginaAtual != null)
            {
                await paginaAtual.Navigation.PushAsync(new ResumoEventoView(_evento));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
