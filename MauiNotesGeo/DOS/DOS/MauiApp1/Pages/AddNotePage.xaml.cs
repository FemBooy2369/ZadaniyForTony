using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Pages
{
    public partial class AddNotePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly LocationService _locationService;
        private string _photoPath = string.Empty;
        private double _latitude;
        private double _longitude;

        public AddNotePage(DatabaseService databaseService, LocationService locationService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _locationService = locationService;
        }

        private async void OnCameraClicked(object sender, EventArgs e)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    await SaveImage(photo);
                }
            }
        }

        private async void OnGalleryClicked(object sender, EventArgs e)
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo != null)
            {
                await SaveImage(photo);
            }
        }

        private async Task SaveImage(FileResult photo)
        {
            string localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);
            await sourceStream.CopyToAsync(localFileStream);

            _photoPath = localFilePath;
            PreviewImage.Source = ImageSource.FromFile(_photoPath);
        }

        private async void OnGeoClicked(object sender, EventArgs e)
        {
            var location = await _locationService.GetCurrentLocation();
            if (location != null)
            {
                _latitude = location.Latitude;
                _longitude = location.Longitude;
                LocationLabel.Text = $"Широта: {_latitude:F4}, Долгота: {_longitude:F4}";
            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось получить местоположение", "OK");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                await DisplayAlert("Ошибка", "Введите заголовок", "OK");
                return;
            }

            var note = new Note
            {
                Title = TitleEntry.Text,
                PhotoPath = _photoPath,
                Latitude = _latitude,
                Longitude = _longitude,
                CreatedAt = DateTime.Now
            };

            await _databaseService.SaveNoteAsync(note);
            await DisplayAlert("Успех", "Заметка сохранена", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}
