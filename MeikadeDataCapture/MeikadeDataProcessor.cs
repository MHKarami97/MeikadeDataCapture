using MeikadeDataCapture.Models;

namespace MeikadeDataCapture;

public class MeikadeDataProcessor
{
    private const string ResultFile = "resultFile";
    private const string ResultFolder = "resultFolder";

    public List<FavoritePoem> GetFavoriteLists(List<SyncData> input)
    {
        var data = input.Where(a => a.Type == 1 && a.VerseId > 0).ToList();

        var result = data.Select(item => new FavoritePoem
        {
            PoemId = item.PoemId,
            PoetId = item.PoetId,
            VerseId = item.VerseId
        }).ToList();

        return result;
    }

    public List<SyncData> GetSpecificPoet(List<SyncData> input, int poetId)
    {
        var data = input.Where(a => a.PoetId == poetId).ToList();

        return data;
    }

    public List<SyncData> GetSpecificNotPoet(List<SyncData> input, int poetId)
    {
        var data = input.Where(a => a.PoetId != poetId).ToList();

        return data;
    }

    public string GetSinglePoem(List<Verse> verses, int verseId)
    {
        var data1 = verses.FirstOrDefault(a => a.Vorder.Equals(verseId));
        var data2 = verses.FirstOrDefault(a => a.Vorder.Equals(verseId + 1));

        var result = string.Empty;

        if (data1 != null)
        {
            result = data1.Text;

            if (data2 != null)
            {
                result += " / " + data2.Text;
            }
        }
        else if (data2 != null)
        {
            result = data2.Text;
        }

        return result;
    }

    public async Task WriteToFile(List<string> input, int poetId)
    {
        var file = ResultFolder + "_" + poetId;

        Directory.CreateDirectory(ResultFolder);

        await File.WriteAllLinesAsync($"{ResultFolder}/{file}.txt", input);
    }
}