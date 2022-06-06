using Newtonsoft.Json;

namespace MeikadeDataCapture.Models;

public class SyncData
{
    [JsonProperty("poet_id")]
    public int PoetId { get; set; }

    [JsonProperty("category_id")]
    public int CategoryId { get; set; }

    [JsonProperty("poem_id")]
    public int PoemId { get; set; }

    [JsonProperty("verse_id")]
    public int VerseId { get; set; }

    public int Type { get; set; }

    public string Value { get; set; }

    public string Extra { get; set; }

    public int Declined { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("inserted_at")]
    public DateTime InsertedAt { get; set; }
}