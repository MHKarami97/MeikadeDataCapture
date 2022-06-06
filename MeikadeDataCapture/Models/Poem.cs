using Newtonsoft.Json;

namespace MeikadeDataCapture.Models;

public class PoemData
{
    public List<Category> Categories { get; set; }
    public Poem Poem { get; set; }
    public Poet Poet { get; set; }
    public List<Verse> Verses { get; set; }
}

public class Verse
{
    public int Vorder { get; set; }
    public int Position { get; set; }
    public string Text { get; set; }
}

public class Poet
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Wikipedia { get; set; }
    public string Color { get; set; }
    public int Views { get; set; }
}

public class Poem
{
    public int Id { get; set; }
    public int PoetId { get; set; }

    [JsonProperty("category_id")]
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Phrase { get; set; }
    public int Views { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
}