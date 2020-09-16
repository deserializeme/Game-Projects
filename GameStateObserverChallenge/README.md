



- switching to a delegate listening so we can have multiple objects/scripts can subscribe as requested in the original directions 
 
  example:
 
 ```
    //heres where we keep track of the number of coins the player has
    
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
```

then in the script we want to be able to track our delegate we set up a listener like this:

```
            // here we set up a simple system where a delagate listner stores the new value 
            // each time the method it subscribes to changes
            int _coins = 0;
            int coins_change(int coins)
            {
                _coins = coins;
                return _coins;
            }
```

once we have this in place in another script we can subscribe to it using
  ```
         "my_gameStateService.State.Coins_changed += coins_change;"
  ```
we also unsubscribe with
 ```
         "my_gameStateService.State.Coins_changed -= coins_change;"
```



  this way, any time our setter property changes, and we fire the coins_changed(value) delgate event, 
  we will automatically invoke the subscriber method ie: coins_change(int coins) in ALL scripts that are subscribed to the delegate
  using this method, we can create logic that facilitates the desired behavior of observers that register for multiple properties but are only 
  notified once even when both are changed. 
  
  Addeded two values that can be subscribed to to track if the play has a "Boost" of some variety
  and how long that boost will last as an example of how this can be extended.
  Ideally you would use these values in your UI scripts etc to show the values as they update

 Good luck and have fun! -eMax


 
