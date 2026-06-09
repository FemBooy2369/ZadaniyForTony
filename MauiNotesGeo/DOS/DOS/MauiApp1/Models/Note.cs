using SQLite;

namespace MauiApp1.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool HasLocation => Latitude != 0 || Longitude != 0;
        public DateTime CreatedAt { get; set; }
    }
}
