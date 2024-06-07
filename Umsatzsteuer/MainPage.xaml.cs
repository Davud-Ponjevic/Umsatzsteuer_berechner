using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;

namespace Umsatzsteuer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Event-Handler für die Textänderung im Entry-Steuerelement
            AmountEntry.TextChanged += OnAmountEntryTextChanged;
        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            // Holen der eingegebenen Werte
            decimal amount = 0;
            decimal taxRate = 0;

            if (!string.IsNullOrEmpty(AmountEntry.Text))
                amount = decimal.Parse(AmountEntry.Text);

            if (TaxRatePicker.SelectedItem != null)
                taxRate = decimal.Parse(TaxRatePicker.SelectedItem.ToString());

            bool isGross = ModePicker.SelectedIndex == 0; // 0 für Brutto, 1 für Netto

            // Berechnung der Umsatzsteuer
            var taxCalc = new TaxCalc();
            decimal taxAmount = taxCalc.CalculateTax(amount, taxRate, isGross);

            // Navigieren zur ResultPage und das Ergebnis anzeigen
            await Navigation.PushAsync(new ResultPage(amount, taxRate, taxAmount, isGross));
        }

        private async void OnAmountEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Überprüfen, ob die eingegebene Zeichenfolge numerisch ist
            if (!IsNumeric(e.NewTextValue))
            {
                // Wenn die Eingabe nicht numerisch ist, die letzte Eingabe löschen und eine Fehlermeldung anzeigen
                AmountEntry.Text = e.OldTextValue ?? string.Empty;
                await DisplayAlert("Fehler", "Bitte geben Sie nur numerische Werte ein.", "OK");
            }
        }

        // Hilfsmethode zur Überprüfung, ob eine Zeichenfolge numerisch ist
        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]*\.?[0-9]*$");
        }

        public class TaxCalc
        {
            public decimal CalculateTax(decimal amount, decimal taxRate, bool isGross)
            {
                if (isGross)
                {
                    return amount - (amount / (1 + taxRate / 100));
                }
                else
                {
                    return amount * (taxRate / 100);
                }
            }

            public decimal CalculateNetAmount(decimal grossAmount, decimal taxRate)
            {
                return grossAmount / (1 + taxRate / 100);
            }

            public decimal CalculateGrossAmount(decimal netAmount, decimal taxRate)
            {
                return netAmount * (1 + taxRate / 100);
            }
        }
    }
}
