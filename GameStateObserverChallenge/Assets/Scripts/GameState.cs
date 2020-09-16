using System;

public class GameState
{

    //switching to a delegate listening so we can have multiple objects/scripts/players subscribe 
    //
    // 1. create the delegate type - in this case an int, but can be a void, bool, string etc..
    //
    // 2. create and event of the delagate type that other objects can subscribe to and be notified of once it changes
    //
    // 3. change our getters and setters to use the new delegate event
    //
    // 4. now we need to create the method that will subscrive to these events in another script
    // 
    //  example:
    //  
    //  public int coins_change(int coins)
    //  {
    //     return coins;
    //   }
    //
    //  - once we have this in place in another script we can subscribe to it using
    //         "my_gameStateService.State.coins_changed += coins_change;"
    //  
    // - we also unsubscribe with
    //         "my_gameStateService.State.coins_changed -= coins_change;"
    //
    //  this way, anytime out setter property changes, and we fire the coins_changed(value) delgate event, 
    //  we will automatically invoke the sobscriber method ie: coins_change(int coins) in ALL scripts that are subsscribed to the delegate
    //  using this method, we can create logic that facilitates the desired behavior of observers that register for multiple properties but are only 
    //  notified once even when both are changed. 
    //  
    //  Addeded two values that can be subscribed to to track if the play has a "Boost" of some variety
    //  and how long that boost will last as an example of how this can be extended.
    //  Ideally you would use these values in your UI scripts etc to show the values as they update
    //
    // Good luck and have fun! -eMax




    //heres where we keep track of the number of coins the player has
    #region Coins
    public delegate int CoinDelegate(int coins);
    public event CoinDelegate Coins_changed;

    private int _coins;
    public int Coins
    {
        get 
        {
            return _coins;
        }
        set
        {
            var coins = _coins;
            _coins = value;

            if (value != coins)
            {
                Coins_changed(value);
            }
        }
    }

    #endregion


    //heres where we keep track of the number of stars the player has
    #region stars
    public delegate int StarDelegate(int stars);
    public event StarDelegate Stars_changed;

    private int _stars;
    public int Stars
    {
        get
        {
            return _stars;
        }
        set
        {
            var stars = _stars;
            _stars = value;

            if (value != stars)
            {
                Stars_changed(value);
            }
        }
    }
    #endregion


    //This is for the "Boost" bool, it tells us if we are under the effects of a boost
    #region boost boolean
    // a "boost" boolean that would can use as an example for multiple delegate subscriptions
    // we can say that buying a star will grant "boost" status for X duration
    public delegate bool BoostDelegate(bool isBoosted);
    public event BoostDelegate Boost_changed;

    private bool _isBoosted;
    public bool IsBoosted
    {
        get
        {
            return _isBoosted;
        }
        set
        {
            var boost = _isBoosted;
            _isBoosted = value;

            if (value != boost)
            {
                Boost_changed(value);
            }
        }
    }
    #endregion


    //this is for the float duration value of the boost
    #region boost float duration
    //boost duration value
    public delegate float BoostTimeDelegate(float boost_time_remaining);
    public event BoostTimeDelegate Boost_time_changed;

    private float _boost_timer;
    public float Boost_Timer
    {
        get
        {
            return _boost_timer;
        }
        set
        {
            float btr = _boost_timer;
            _boost_timer = value;

            if (value != btr)
            {
                Boost_time_changed(value);
            }

            //when timer expires, disable the boost state
            if (value <= 0)
            {
                IsBoosted = false;
            }
            else
            {
                IsBoosted = true;
            }
        }
    }
    #endregion



}