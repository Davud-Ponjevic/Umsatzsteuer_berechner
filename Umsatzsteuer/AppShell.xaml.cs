using Microsoft.Maui.Controls;

namespace Umsatzsteuer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ResultPage), typeof(ResultPage));
        }
    }
}
