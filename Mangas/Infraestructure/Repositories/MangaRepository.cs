using System.Text.Json;
using mangas.Domain.Entities;

namespace mangas.Infraestructure.Repositories;

public class MangaRepository
{
    private List<Manga> _mangas;
    private string _filePath;

    public MangaRepository(IConfiguration configuration)
    {
        _filePath = configuration.GetValue<string>("dataBank") ?? string.Empty;
        _mangas = LoadData(); // Cargar los datos inicialmente
    }

    private string GetCurrentFilePath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        return Path.Combine(currentDirectory, _filePath);
    }

    private List<Manga> LoadData()
    {
        var currentFilePath = GetCurrentFilePath();

        if (File.Exists(currentFilePath))
        {
            var jsonData = File.ReadAllText(currentFilePath);
            return JsonSerializer.Deserialize<List<Manga>>(jsonData) ?? new List<Manga>();
        }

        return new List<Manga>();
    }

    private void SaveData()
    {
        var currentFilePath = GetCurrentFilePath();
        File.WriteAllText(currentFilePath, JsonSerializer.Serialize(_mangas, new JsonSerializerOptions { WriteIndented = true }));
    }

    public IEnumerable<Manga> GetAll()
    {
        return _mangas;
    }

    public Manga GetById(int id)
    {
        return _mangas.FirstOrDefault(manga => manga.Id == id) 
               ?? new Manga { Title = string.Empty, Author = string.Empty };
    }

    public void Add(Manga manga)
    {
        _mangas.Add(manga);
        SaveData(); // Guardar los datos después de agregar
    }

    public void Update(Manga updateManga)
    {
        var index = _mangas.FindIndex(m => m.Id == updateManga.Id);

        if (index != -1)
        {
            _mangas[index] = updateManga;
            SaveData(); // Guardar los datos después de actualizar
        }
    }

    public void Delete(int id)
    {
        _mangas.RemoveAll(m => m.Id == id);
        SaveData(); // Guardar los datos después de eliminar
    }
}
