namespace MauiApp1.Pages
{
    [QueryProperty(nameof(Lat), "lat")]
    [QueryProperty(nameof(Lon), "lon")]
    public partial class MapPage : ContentPage
    {
        public string Lat { get; set; }
        public string Lon { get; set; }

        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (double.TryParse(Lat.Replace('.', ','), out double latitude) && 
                double.TryParse(Lon.Replace('.', ','), out double longitude))
            {
                LoadMap(latitude, longitude);
            }
        }

        private void LoadMap(double lat, double lon)
        {
            string htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css' />
                <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
                <style>
                    #map {{ height: 100vh; width: 100vw; margin: 0; }}
                </style>
            </head>
            <body>
                <div id='map'></div>
                <script>
                    var map = L.map('map').setView([{lat.ToString().Replace(',', '.')}, {lon.ToString().Replace(',', '.')}], 15);
                    L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
                        attribution: '© OpenStreetMap contributors'
                    }}).addTo(map);
                    L.marker([{lat.ToString().Replace(',', '.')}, {lon.ToString().Replace(',', '.')}]).addTo(map);
                </script>
            </body>
            </html>";

            MapView.Source = new HtmlWebViewSource { Html = htmlContent };
        }
    }
}
