using Microsoft.Maui.Controls;

namespace Umsatzsteuer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(AmountEntry.Text);
            bool isGross = TypePicker.SelectedIndex == 0; // 0: Brutto, 1: Netto
            decimal taxRate = decimal.Parse(TaxRateEntry.Text);

            var taxCalc = new TaxCalc();
            decimal taxAmount = taxCalc.CalculateTax(amount, taxRate, isGross);

            // Ergebnisanzeige aktualisieren
            InputAmountLabel.Text = $"Eingegebener Betrag: {amount:C}";
            TaxRateLabel.Text = $"Umsatzsteuer: {taxRate}%";
            ResultLabel.Text = isGross
                ? $"Netto Betrag: {amount - taxAmount:C}\nUmsatzsteuer Betrag: {taxAmount:C}"
                : $"Brutto Betrag: {amount + taxAmount:C}\nUmsatzsteuer Betrag: {taxAmount:C}";

            ResultStack.IsVisible = true;
        }
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
