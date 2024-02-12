

public sealed class TwitterDataFetcher(
    TwitterConfig _twitterconfig) : ISocialDataFetcher
{

    public async Task<SocialStatus> FetchSocialStatusAsync(CancellationToken token)
    {
        var twitterClinet = new Tweetinvi.TwitterClient(_twitterconfig.ConsumerKey, _twitterconfig.ConsumerSecret);
        var user = await twitterClinet.UsersV2.GetUserByNameAsync(_twitterconfig.UserName);
        var follower = user.User.PublicMetrics.FollowersCount;
        var following = user.User.PublicMetrics.FollowingCount;

        SocialStatus status = new SocialStatus(follower, following);
        return status;
    }


}