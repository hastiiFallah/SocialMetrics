

public sealed class SocialDataFetcherFacade(IReadOnlyList<ISocialDataFetcher> _fetcher)
{
    public async Task<IReadOnlyList<SocialStatus>> FetchSocialStatusAsync(CancellationToken token)
    {
        var result = new List<SocialStatus>();
        foreach (var plugin in _fetcher)
        {
            var status = await plugin.FetchSocialStatusAsync(token);
            result.Add(status);
        }
        return result;
    }
}