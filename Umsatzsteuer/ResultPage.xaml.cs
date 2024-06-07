using Microsoft.Maui.Controls;

namespace Umsatzsteuer
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage(decimal amount, decimal taxRate, decimal taxAmount, bool isGross)
        {
            InitializeComponent();

            InputAmountLabel.Text = $"Eingegebener Betrag: {amount:C}";
            TaxRateLabel.Text = $"Umsatzsteuer: {taxRate}%";

            if (isGross)
            {
                ResultLabel.Text = $"Brutto Betrag: {amount:C}\nUmsatzsteuer Betrag: {taxAmount:C}\nNetto Betrag: {(amount - taxAmount):C}";
            }
            else
            {
                ResultLabel.Text = $"Netto Betrag: {amount:C}\nUmsatzsteuer Betrag: {taxAmount:C}\nBrutto Betrag: {(amount + taxAmount):C}";
            }
        }

        private async void OnBackToHomeClicked(object sender, EventArgs e)
        {
            // Navigieren zur Hauptseite (MainPage)
            await Navigation.PopToRootAsync();
        }
    }
}
