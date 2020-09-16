public class GameStateService
{
    private static readonly GameStateService _instance = new GameStateService();

    public static GameStateService Get()
    {
        return _instance;
    }

    public GameState State { get; private set; }

    public void Init(int coins, int stars)
    {
        State = new GameState()
        {
            Coins = coins,
            Stars = stars
        };
    }
}