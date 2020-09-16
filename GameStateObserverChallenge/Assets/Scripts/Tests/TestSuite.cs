using NUnit.Framework;

namespace Tests
{
    public class TestSuite
    {
        [Test]
        public void CanInitGameState()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10,0);

            var gameState = gameStateService.State;
        
            Assert.That(gameState.Coins, Is.EqualTo(10));
            Assert.That(gameState.Stars, Is.EqualTo(0));
        }

        [Test]
        public void CanObserveGameStateChanges()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10,0);

            var gameState = gameStateService.State;
            var stateObserverCalled = false;
            gameState.CoinsChanged += () =>
            {
                stateObserverCalled = true;
                Assert.That(gameState.Coins, Is.EqualTo(8));
            };
            
            ShopService.Get().UseCoins(2);

            Assert.That(stateObserverCalled, "Obsever not called");
        }
    
        [Test]
        public void CanObserveConsistentGameStateChanges()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10,0);

            var stateObserverCalled = false;
            void StateValidator()
            {
                stateObserverCalled = true;
                var gameState = gameStateService.State;
                Assert.That(gameState.Stars, Is.EqualTo(1));
                Assert.That(gameState.Coins, Is.EqualTo(9));
            }

            gameStateService.State.CoinsChanged += StateValidator;
            gameStateService.State.StarsChanged += StateValidator;

            var shopService = ShopService.Get();
            shopService.BuyStars(1, 1);
            
            Assert.That(stateObserverCalled, "Obsever not called");
        }
    }
}