using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "Notes.db3"), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            await _database.CreateTableAsync<Note>();
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            await Init();
            return await _database.Table<Note>().ToListAsync();
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            await Init();
            if (note.Id != 0)
                return await _database.UpdateAsync(note);
            else
                return await _database.InsertAsync(note);
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            await Init();
            return await _database.DeleteAsync(note);
        }
    }
}
