using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbbottCredentialMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeituraQRCode : ContentPage
    {
        public LeituraQRCode()
        {
            InitializeComponent();
            Form_Load();
        }

        private async void Form_Load()
        {
            scanner.Options.UseFrontCameraIfAvailable = true;
            await imgArrow.TranslateTo(100, -60, 1000);
            await imgArrow.TranslateTo(-100, -60, 1000);
            await imgArrow.TranslateTo(100, -60, 1000);
            await imgArrow.TranslateTo(-100, -60, 1000);
            await imgArrow.TranslateTo(100, -60, 1000);
            await imgArrow.TranslateTo(-100, -60, 1000);
        }

        private void scanner_OnScanResult(ZXing.Result result)
        {
            scanner.IsScanning = false;
            scanner.IsAnalyzing = false;
            Console.WriteLine(result.Text);
            Device.BeginInvokeOnMainThread(() =>
            {
                NextPage();
            });
        }
        
        async void NextPage()
        {
            
        }
    }
}