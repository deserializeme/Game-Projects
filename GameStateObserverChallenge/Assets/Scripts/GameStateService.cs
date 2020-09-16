public class GameStateService
{
    private static readonly GameStateService _instance = new GameStateService();

    public static GameStateService Get()
    {
        return _instance;
    }

    public GameState State { get; private set; }


    public void Init(int coins=0, int stars=0, bool isBoosted=false, float boost_timer=0)
    {
        State = new GameState()
        {
            Coins = coins,
            Stars = stars,
            IsBoosted = isBoosted,
            Boost_Timer = boost_timer
        };
    }
}