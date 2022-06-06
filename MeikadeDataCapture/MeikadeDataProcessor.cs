using MeikadeDataCapture.Models;

namespace MeikadeDataCapture;

public class MeikadeDataProcessor
{
    private const string ResultFile = "resultFile";
    private const string ResultFolder = "resultFolder";

    public List<FavoritePoem> GetFavoriteLists(List<SyncData> input)
    {
        var data = input.Where(a => a.Type == 1).ToList();

        var result = data.Select(item => new FavoritePoem
        {
            PoemId = item.PoemId,
            PoetId = item.PoetId,
            VerseId = item.VerseId
        }).ToList();

        return result;
    }

    public string GetSinglePoem(List<Verse> verses, int verseId)
    {
        var data1 = verses.First(a => a.Vorder.Equals(verseId));
        var data2 = verses.First(a => a.Vorder.Equals(verseId + 1));

        var result = data1 + "/" + data2;

        return result;
    }

    public async Task WriteToFile(List<string> input)
    {
        Directory.CreateDirectory(ResultFolder);
        await using var sw = new StreamWriter($"{ResultFolder}/{ResultFile}.txt");

        foreach (var item in input)
        {
            await sw.WriteLineAsync(item);
        }
    }
}