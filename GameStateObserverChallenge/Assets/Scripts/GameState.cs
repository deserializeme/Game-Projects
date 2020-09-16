using System;

public class GameState
{
    private int _coins;

    public event Action CoinsChanged = () => { };
    public int Coins
    {
        get => _coins;
        set
        {
            var coins = _coins;
            _coins = value;

            if (value != coins)
            {
                CoinsChanged();
            }
        }
    }

    private int _stars;
    
    public event Action StarsChanged = () => { };
    public int Stars
    {
        get => _stars;
        set
        {
            var stars = _stars;
            _stars = value;

            if (value != stars)
            {
                StarsChanged();
            }
        }
    }
}