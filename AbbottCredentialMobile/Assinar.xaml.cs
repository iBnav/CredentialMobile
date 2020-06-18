using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbbottCredentialMobile.Classes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using TouchTracking;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbbottCredentialMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Assinar : ContentPage
    {
        private long id;
        public Assinar(long i)
        {
            InitializeComponent();
            id = i;
        }

        bool Save = false;
        bool Clear = false;
        Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
        List<SKPath> completedPaths = new List<SKPath>();
        SKPaint blackFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Blue,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            if (Clear == true)
            {
                Clear = false;
                inProgressPaths.Clear();
                completedPaths.Clear();
                return;
            }
            foreach (SKPath path in completedPaths)
            {
                canvas.DrawPath(path, blackFillPaint);
            }

            foreach (SKPath path in inProgressPaths.Values)
            {
                canvas.DrawPath(path, blackFillPaint);
            }

            canvas.Flush();

            if (Save)
            {
                Save = false;
                var snap = e.Surface.Snapshot();
                var pngImage = snap.Encode();
                using (MemoryStream ms = new MemoryStream())
                {
                    pngImage.SaveTo(ms);
                    byte[] imageBytes = ms.ToArray();
                    string ib64 = Convert.ToBase64String(imageBytes);
                    try
                    {
                        //SaveOnDB(ib64);
                        CallNextPage();
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Erro", "Erro ao salvar assinatura e efetuar o checkin:" + ex.Message, "Fechar");
                        Console.WriteLine(ex.Message);
                    }
                    
                }
                
            }
        }

        private void SaveOnDB(string image)
        {
            try
            {
                DataBase.SalvarImagem(image, this.id);
                DataBase.Checkin(this.id);
            }
            catch (Exception ex)
            {
                DataBase.FecharConexao();
                DisplayAlert("Erro", "Erro ao salvar assinatura e efetuar o checkin:" + ex.Message, "Fechar");
            }
            
        }

        private async void CallNextPage()
        {
            await Navigation.PushAsync(new PaginaFinal());
        }
        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = new SKPath();
                        path.MoveTo(ConvertToPixel(args.Location));
                        inProgressPaths.Add(args.Id, path);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Moved:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = inProgressPaths[args.Id];
                        path.LineTo(ConvertToPixel(args.Location));
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        completedPaths.Add(inProgressPaths[args.Id]);
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }
        SKPoint ConvertToPixel(TouchTrackingPoint pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * (pt.Y) / canvasView.Height));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Clear = true;
            canvasView.InvalidateSurface();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (!completedPaths.Any())
            {
                callError();
            }
            else
            {
                Save = true;
                canvasView.InvalidateSurface();
            }
            
        }

        private async void callError()
        {
            await DisplayAlert("Assinatura em branco", "Por favor assine no retângulo branco", "Fechar");
        }

    }
}
