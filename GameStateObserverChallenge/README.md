switching to a delegate listening so we can have multiple objects/scripts/players subscribe 

 1. create the delegate type - in this case an int, but can be a void, bool, string etc..

 2. create and event of the delagate type that other objects can subscribe to and be notified of once it changes

 3. change our getters and setters to use the new delegate event

 4. now we need to create the method that will subscrive to these events in another script
 
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

once we have this in place in another script we can subscribe to it using
  ```
         "my_gameStateService.State.coins_changed += coins_change;"
  ```
we also unsubscribe with
 ```
         "my_gameStateService.State.coins_changed -= coins_change;"
```
  this way, anytime out setter property changes, and we fire the coins_changed(value) delgate event, 
  we will automatically invoke the sobscriber method ie: coins_change(int coins) in ALL scripts that are subsscribed to the delegate
  using this method, we can create logic that facilitates the desired behavior of observers that register for multiple properties but are only 
  notified once even when both are changed. 
  
  Addeded two values that can be subscribed to to track if the play has a "Boost" of some variety
  and how long that boost will last as an example of how this can be extended.
  Ideally you would use these values in your UI scripts etc to show the values as they update

 Good luck and have fun! -eMax


 
