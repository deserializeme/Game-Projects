public class ShopService
{
    private static readonly ShopService _instance = new ShopService();

    public static ShopService Get()
    {
        return _instance;
    }

    public void BuyStars(int stars, int forCoins, float boost_time)
    {
        UseCoins(forCoins);
        GetStars(stars);
        BoostPlayer(boost_time);
    }

    public void UseCoins(int coins)
    {
        GameStateService.Get().State.Coins -= coins;
    }

    public void GetStars(int stars)
    {
        GameStateService.Get().State.Stars += stars;
    }

    public void BoostPlayer(float duration)
    {

       GameStateService.Get().State.Boost_Timer += duration;
    }
}