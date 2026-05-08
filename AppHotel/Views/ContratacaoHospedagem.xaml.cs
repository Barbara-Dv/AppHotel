using AppHotel.Models;
using System;
using System.Collections.Generic;

namespace AppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    public ContratacaoHospedagem()
    {
        InitializeComponent();

        List<Quarto> lista_quartos = new List<Quarto>()
        {
            new Quarto()
            {
                Descricao = "Suíte Super Luxo",
                ValorDiariaAdulto = 110,
                ValorDiariaCrianca = 55
            },

            new Quarto()
            {
                Descricao = "Suíte Luxo",
                ValorDiariaAdulto = 80,
                ValorDiariaCrianca = 40
            },

            new Quarto()
            {
                Descricao = "Suíte Simples",
                ValorDiariaAdulto = 50,
                ValorDiariaCrianca = 25
            }
        };

        pck_quarto.ItemsSource = lista_quartos;
        pck_quarto.ItemDisplayBinding = new Binding("Descricao");

        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkout.MinimumDate = DateTime.Today.AddDays(1);
    }

    private async void BtnAvancar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (pck_quarto.SelectedItem == null)
            {
                await DisplayAlert(
                    "Erro",
                    "Selecione uma suíte.",
                    "OK");

                return;
            }

            Quarto quartoSelecionado =
                (Quarto)pck_quarto.SelectedItem;

            int adultos = Convert.ToInt32(stp_adultos.Value);

            int criancas = Convert.ToInt32(stp_criancas.Value);

            DateTime checkin = dtpck_checkin.Date.Value;

            DateTime checkout = dtpck_checkout.Date.Value;

            TimeSpan periodo = checkout.Subtract(checkin);

            int dias = periodo.Days;

            if (dias <= 0)
            {
                await DisplayAlert(
                    "Erro",
                    "O check-out deve ser após o check-in.",
                    "OK");

                return;
            }

            double totalAdultos =
                adultos * quartoSelecionado.ValorDiariaAdulto;

            double totalCriancas =
                criancas * quartoSelecionado.ValorDiariaCrianca;

            double total =
                (totalAdultos + totalCriancas) * dias;

            string mensagem =
                $"Suíte: {quartoSelecionado.Descricao}\n" +
                $"Adultos: {adultos}\n" +
                $"Crianças: {criancas}\n" +
                $"Diárias: {dias}\n" +
                $"Total: {total:C}";

            await DisplayAlert(
                "Resumo da Reserva",
                mensagem,
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Erro",
                ex.Message,
                "Fechar");
        }
    }
}