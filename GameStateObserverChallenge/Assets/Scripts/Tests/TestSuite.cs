using NUnit.Framework;


namespace Tests
{
    public class TestSuite
    {
        [Test]
        public void CanInitGameState()
        {

            #region  Delagate listener values
            // here we set up a simple system where a delagate listner stores the new value 
            // each time the method it subscribes to changes
            int _coins = 0;
            int coins_change(int coins)
            {
                _coins = coins;
                return _coins;
            }

            int _stars = 0;
            int stars_change(int stars)
            {
                _stars = stars;
                return _stars;
            }
            #endregion

            //grab the instance
            var gameStateService = GameStateService.Get();

            //init the State at 0
            gameStateService.Init();

            //subscribe events
            gameStateService.State.Coins_changed += coins_change;
            gameStateService.State.Stars_changed += stars_change;

            //set the values in the state, which will trigger the delegate to update our subscribed values 
            gameStateService.State.Coins = 10;
            gameStateService.State.Stars = 0;

            // do some tests against the subscribed values, not the state directly
            var gameState = gameStateService.State;
            Assert.That(_coins, Is.EqualTo(10));
            Assert.That(_stars, Is.EqualTo(0));

            //unsubscribe
            gameStateService.State.Coins_changed -= coins_change;
            gameStateService.State.Stars_changed -= stars_change;
        }

        [Test]
        public void CanObserveGameStateChanges()
        {
            #region  Delagate listener values
            // here we set up a simple system where a delagate listner stores the new value 
            // each time the method it subscribes to changes
            int _coins = 0;
            int coins_change(int coins)
            {
                _coins = coins;
                return _coins;
            }

            int _stars = 0;
            int stars_change(int stars)
            {
                _stars = stars;
                return _stars;
            }
            #endregion

            //grab the instance
            var gameStateService = GameStateService.Get();

            //init the State at 0
            gameStateService.Init();

            //subscribe events
            gameStateService.State.Coins_changed += coins_change;
            gameStateService.State.Stars_changed += stars_change;

            //set the values in the state, which will trigger the delegate to update our subscribed values 
            gameStateService.State.Coins = 10;
            gameStateService.State.Stars = 0;

            // do some tests against the subscribed values, not the state directly
            var gameState = gameStateService.State;
            var stateObserverCalled = false;


            stateObserverCalled = true;
            ShopService.Get().UseCoins(2);
            Assert.That(_coins, Is.EqualTo(8));

            Assert.That(stateObserverCalled, "Obsever not called");

            //unsubscribe
            gameStateService.State.Coins_changed -= coins_change;
            gameStateService.State.Stars_changed -= stars_change;

        }

        [Test]
        public void CanObserveConsistentGameStateChanges()
        {
            #region  Delagate listener values
            // here we set up a simple system where a delagate listner stores the new value 
            // each time the method it subscribes to changes
            int _coins = 0;
            int coins_change(int coins)
            {
                _coins = coins;
                return _coins;
            }

            int _stars = 0;
            int stars_change(int stars)
            {
                _stars = stars;
                return _stars;
            }

            bool _boost = false;
            bool boost_state(bool boost)
            {
                _boost = boost;
                return _boost;
            }

            float _btr = 0;
            float boost_time_remaining(float btr)
            {
                _btr = btr;
                return _btr;
            }
            #endregion

            //grab the instance
            var gameStateService = GameStateService.Get();

            //init the State
            gameStateService.Init();

            //subscribe events
            gameStateService.State.Coins_changed += coins_change;
            gameStateService.State.Stars_changed += stars_change;
            gameStateService.State.Boost_changed += boost_state;
            gameStateService.State.Boost_time_changed += boost_time_remaining;

            //set the values in the state, which will trigger the delegate to update our subscribed values 
            gameStateService.State.Coins = 10;
            gameStateService.State.Stars = 0;
            gameStateService.State.IsBoosted = true;
            gameStateService.State.Boost_Timer = 0;

            var stateObserverCalled = false;
            var shopService = ShopService.Get();

            // do some tests against the subscribed values, not the state directly
            float boost_time = 10f;
            shopService.BuyStars(1, 1, boost_time);

            void StateValidator()
            {
                stateObserverCalled = true;
                var gameState = gameStateService.State;
                //we bought a star and are now boosted
                Assert.That(_stars, Is.EqualTo(1));
                Assert.That(_coins, Is.EqualTo(9));
                Assert.That(_btr, Is.EqualTo(boost_time));
                Assert.That(_boost, Is.EqualTo(true));
            }

            StateValidator();

            //unsubscribe
            gameStateService.State.Coins_changed -= coins_change;
            gameStateService.State.Stars_changed -= stars_change;
            gameStateService.State.Boost_changed -= boost_state;
            gameStateService.State.Boost_time_changed -= boost_time_remaining;

            Assert.That(stateObserverCalled, "Obsever not called");
        }

        [Test]
        public void BoostTest()
        {
            #region  Delagate listener values
            // here we set up a simple system where a delagate listner stores the new value 
            // each time the method it subscribes to changes
            int _coins = 0;
            int coins_change(int coins)
            {
                _coins = coins;
                return _coins;
            }

            int _stars = 0;
            int stars_change(int stars)
            {
                _stars = stars;
                return _stars;
            }

            bool _boost = false;
            bool boost_state(bool boost)
            {
                _boost = boost;
                return _boost;
            }

            float _btr = 0;
            float boost_time_remaining(float btr)
            {
                _btr = btr;
                return _btr;
            }
            #endregion

            //grab the instance
            var gameStateService = GameStateService.Get();

            //init the State at 0
            gameStateService.Init();

            //subscribe events
            gameStateService.State.Coins_changed += coins_change;
            gameStateService.State.Stars_changed += stars_change;
            gameStateService.State.Boost_changed += boost_state;
            gameStateService.State.Boost_time_changed += boost_time_remaining;


            //set the values in the state, which will trigger the delegate to update our subscribed values 
            gameStateService.State.Coins = 10;
            gameStateService.State.Stars = 0;

            var stateObserverCalled = false;
            var shopService = ShopService.Get();

            //Buy a star, verify that the coins were debited, then grant a "Boost" to the player for X time
            // by default set to 10 seconds, and adding 10 more each time a star is bought
            float boost_time = 10f;
            shopService.BuyStars(1, 1, boost_time);

            void StateValidator()
            {
                stateObserverCalled = true;
                var gameState = gameStateService.State;
                Assert.That(_stars, Is.EqualTo(1));
                Assert.That(_coins, Is.EqualTo(9));
                Assert.That(_boost, Is.EqualTo(true));
                Assert.That(_btr, Is.GreaterThan(0));
            }

            StateValidator();

            //now if we set the boost time remianing to 0, we should see the boost flag become false
            gameStateService.State.Boost_Timer = 0;

            Assert.That(_btr, Is.EqualTo(0));
            Assert.That(_boost, Is.EqualTo(false));

            //Unsubscribe
            gameStateService.State.Coins_changed -= coins_change;
            gameStateService.State.Stars_changed -= stars_change;
            gameStateService.State.Boost_changed -= boost_state;
            gameStateService.State.Boost_time_changed -= boost_time_remaining;
        }

    }
}