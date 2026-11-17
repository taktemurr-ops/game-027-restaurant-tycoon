using NUnit.Framework;
using RestaurantTycoon;
using System;

public sealed class RestaurantEngineTests
{
    [Test] public void NewGameStartsWithPlayableResources()
    {
        var game = new RestaurantEngine();
        Assert.Greater(game.State.Cash, 0);
        Assert.Greater(game.State.Ingredients, 0);
    }

    [Test] public void IngredientsCannotBeBoughtWithoutMoney()
    {
        var game = new RestaurantEngine();
        Assert.IsFalse(game.BuyIngredients(101));
        Assert.AreEqual(500, game.State.Cash);
    }

    [Test] public void BuyingIngredientsDeductsCorrectAmount()
    {
        var game = new RestaurantEngine();
        Assert.IsTrue(game.BuyIngredients(2));
        Assert.AreEqual(490, game.State.Cash);
        Assert.AreEqual(22, game.State.Ingredients);
    }

    [Test] public void ServiceLimitedBySeatsStaffAndStock()
    {
        var game = new RestaurantEngine();
        Assert.AreEqual(4, game.AdvanceDay(20));
        Assert.AreEqual(16, game.State.Ingredients);
        Assert.AreEqual(4, game.State.MealsServed);
    }

    [Test] public void HiringIncreasesDailyCapacity()
    {
        var game = new RestaurantEngine();
        Assert.IsTrue(game.HireStaff());
        Assert.IsTrue(game.AddSeats());
        Assert.AreEqual(6, game.AdvanceDay(10));
    }

    [Test] public void SnapshotDoesNotExposeMutableGameState()
    {
        var game = new RestaurantEngine();
        var snapshot = game.Snapshot();
        snapshot.Cash = 9999;
        Assert.AreEqual(500, game.State.Cash);
    }

    [Test] public void InvalidSaveDataIsRejected()
    {
        Assert.Throws<ArgumentException>(() => new RestaurantEngine(new RestaurantState { Day = -1 }));
    }

    [Test] public void NegativeDemandIsRejected()
    {
        var game = new RestaurantEngine();
        Assert.Throws<ArgumentOutOfRangeException>(() => game.AdvanceDay(-1));
    }
}
