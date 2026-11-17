using System;
using System.Collections.Generic;

namespace RestaurantTycoon
{
    [Serializable]
    public sealed class RestaurantState
    {
        public int Day = 1;
        public int Cash = 500;
        public int Ingredients = 20;
        public int Reputation = 50;
        public int Seats = 4;
        public int Staff = 1;
        public int MealsServed;
    }

    public sealed class RestaurantEngine
    {
        public RestaurantState State { get; private set; }

        public RestaurantEngine(RestaurantState initial = null)
        {
            State = initial == null ? new RestaurantState() : CopyValidated(initial);
        }

        public bool BuyIngredients(int quantity)
        {
            if (quantity <= 0 || quantity > 10000) return false;
            long cost = (long)quantity * 5;
            if (cost > State.Cash) return false;
            State.Cash -= (int)cost;
            State.Ingredients += quantity;
            return true;
        }

        public bool HireStaff()
        {
            if (State.Cash < 150 || State.Staff >= 20) return false;
            State.Cash -= 150;
            State.Staff++;
            return true;
        }

        public bool AddSeats()
        {
            if (State.Cash < 100 || State.Seats >= 100) return false;
            State.Cash -= 100;
            State.Seats += 2;
            return true;
        }

        // Deterministic daily simulation: no ad or purchase is required to progress.
        public int AdvanceDay(int demand)
        {
            if (demand < 0 || demand > 10000) throw new ArgumentOutOfRangeException(nameof(demand));
            int capacity = Math.Min(State.Seats, State.Staff * 4);
            int served = Math.Min(demand, Math.Min(capacity, State.Ingredients));
            State.Ingredients -= served;
            State.MealsServed += served;
            State.Cash += served * 15;
            State.Cash = Math.Max(0, State.Cash - State.Staff * 10);
            State.Reputation = Math.Max(0, Math.Min(100, State.Reputation + (served == demand ? 2 : -3)));
            State.Day++;
            return served;
        }

        public RestaurantState Snapshot() => CopyValidated(State);

        private static RestaurantState CopyValidated(RestaurantState state)
        {
            if (state.Day < 1 || state.Cash < 0 || state.Ingredients < 0 ||
                state.Reputation < 0 || state.Reputation > 100 ||
                state.Seats < 1 || state.Seats > 100 ||
                state.Staff < 1 || state.Staff > 20 || state.MealsServed < 0)
                throw new ArgumentException("Invalid restaurant state");
            return new RestaurantState
            {
                Day = state.Day, Cash = state.Cash, Ingredients = state.Ingredients,
                Reputation = state.Reputation, Seats = state.Seats,
                Staff = state.Staff, MealsServed = state.MealsServed
            };
        }
    }
}
