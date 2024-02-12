

public interface ISocialDataFetcher
{
    Task<SocialStatus> FetchSocialStatusAsync(CancellationToken token);
}