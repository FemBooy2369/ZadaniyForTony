using MauiApp1.Services;
using MauiApp1.Pages;
using MauiApp1.Models;
using System.Linq;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly LocationService _locationService;

        public MainPage(DatabaseService databaseService, LocationService locationService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _locationService = locationService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadNotesAsync();
        }

        private async Task LoadNotesAsync()
        {
            try
            {
                var notes = await _databaseService.GetNotesAsync();

                NotesCollection.ItemsSource = notes?.OrderByDescending(n => n.CreatedAt).ToList()
                                           ?? new List<Note>();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить заметки:\n{ex.Message}", "OK");
            }
        }

        private async void OnAddNoteClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddNotePage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Note note && note.HasLocation)
            {
                var parameters = new Dictionary<string, object>
                {
                    { "lat", note.Latitude },
                    { "lon", note.Longitude }
                };

                await Shell.Current.GoToAsync($"{nameof(MapPage)}", parameters);
            }

            if (sender is CollectionView cv)
                cv.SelectedItem = null;
        }
    }
}