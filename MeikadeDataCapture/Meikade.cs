using MeikadeDataCapture.Models;
using MeikadeDataCapture.Request;

namespace MeikadeDataCapture;

public class Meikade
{
    private static string _token;
    private readonly string _baseUrl;
    private readonly string _passwordSalt;
    private readonly BaseHttpRequest _baseHttpRequest;
    private static Dictionary<string, string> _tokenHeader;

    public Meikade()
    {
        _token = "";
        _baseHttpRequest = new BaseHttpRequest();
        _baseUrl = "https://api.meikade.com/api";
        _passwordSalt = "65ae7da2-acce-470e-9243-73fccd363ddc";
    }

    public void AddToken(string token)
    {
        _token = token;
        _tokenHeader = new Dictionary<string, string>
        {
            { "User-token", _token }
        };
    }

    public async Task<string> Login(string username, string password)
    {
        var pass = Util.HashSha256Password(_passwordSalt, password);

        var data = await _baseHttpRequest.Post<Base<LoginDto>>($"{_baseUrl}/auth/login", new
        {
            username = username,
            password = pass
        });

        return data.Result.Token;
    }

    public async Task<List<SyncData>> GetSyncData()
    {
        var data = await _baseHttpRequest.Get<Base<List<SyncData>>>(
            $"{_baseUrl}/user/sync",
            GetTokenHeader(),
            new
            {
                date = "2000-01-01 01:01:01"
            });

        return data.Result;
    }

    public async Task<List<Verse>> GetPoems(int poemId)
    {
        var data = await _baseHttpRequest.Get<Base<PoemData>>(
            $"{_baseUrl}/main/poem",
            GetTokenHeader(),
            new
            {
                poem_id = poemId,
                verse_limit = 1000,
                verse_offset = 0
            });

        return data.Result.Verses;
    }

    #region private

    private Dictionary<string, string> GetTokenHeader()
    {
        return _tokenHeader;
    }

    #endregion
}