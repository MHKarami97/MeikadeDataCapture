using MeikadeDataCapture;

Console.WriteLine("Start");

try
{
    var meikade = new Meikade();
    var meikadeDataProcessor = new MeikadeDataProcessor();

    const string username = "";
    const string password = "";
    var result = new List<string>();

    var token = await meikade.Login(username, password);

    meikade.AddToken(token);

    var syncData = await meikade.GetSyncData();

    var poets = syncData.Select(a => a.PoetId).Distinct().ToList();
    poets.Remove(2); // hafez
    poets.Remove(7); // sadi

    foreach (var poet in poets)
    {
        var justSadi = meikadeDataProcessor.GetSpecificPoet(syncData, poet);

        var favorites = meikadeDataProcessor.GetFavoriteLists(justSadi);

        foreach (var favorite in favorites)
        {
            var poems = await meikade.GetPoems(favorite.PoemId);

            var poem = meikadeDataProcessor.GetSinglePoem(poems, favorite.VerseId);

            result.Add(poem);
        }

        if (result.Any())
        {
            await meikadeDataProcessor.WriteToFile(result, poet);
        }

        result.Clear();
    }

    Console.WriteLine("Finish");
}
catch (Exception e)
{
    Console.WriteLine(e);
}