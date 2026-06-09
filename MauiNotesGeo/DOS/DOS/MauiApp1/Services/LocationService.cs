namespace MauiApp1.Services
{
    public class LocationService
    {
        public async Task<Location?> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                return await Geolocation.Default.GetLocationAsync(request);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
