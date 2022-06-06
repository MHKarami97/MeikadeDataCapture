using MeikadeDataCapture;

Console.WriteLine("Start");

try
{
    var meikade = new Meikade();
    var meikadeDataProcessor = new MeikadeDataProcessor();

    const string password = "";
    const string username = "";
    var result = new List<string>();

    var token = await meikade.Login(username, password);

    meikade.AddToken(token);

    var syncData = await meikade.GetSyncData();

    var favorites = meikadeDataProcessor.GetFavoriteLists(syncData);

    foreach (var favorite in favorites)
    {
        var poems = await meikade.GetPoems(favorite.PoemId);

        var poem = meikadeDataProcessor.GetSinglePoem(poems, favorite.VerseId);

        result.Add(poem);
    }

    await meikadeDataProcessor.WriteToFile(result);

    Console.WriteLine("Finish");
}
catch (Exception e)
{
    Console.WriteLine(e);
}