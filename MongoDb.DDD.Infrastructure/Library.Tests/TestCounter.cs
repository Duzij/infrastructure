using Infrastructure.Core;
using System;

namespace Library.Tests
{
    public class TestCounter : Entity<string>
    {
        public int CounterValue { get; private set; }

        public static TestCounter Create(string idValue, int value)
        {
            return new TestCounter(new CounterId(idValue), value);
        }

        public void IncrementValue()
        {
            this.CounterValue++;
        }

        public void IncrementValueWithEvent()
        {
            this.CounterValue = this.CounterValue + 1;
            var oldValue = this.CounterValue - 1;
            var newValue = this.CounterValue;
            AddEvent(new ValueIncremented(this.Id.Value, oldValue, newValue));
        }

        public TestCounter(CounterId counterId, int value)
        {
            this.Id = counterId;
            this.CounterValue = value;
        }

        public override void CheckState()
        {
        }

        public void UpdateCounterWithValue(int i)
        {
            this.CounterValue = i;
            AddEvent(new ValueUpdated(this.Id.Value, CounterValue));
        }
    }

    internal class ValueUpdated
    {
        public string CounterId { get; set; }
        public int CounterValue { get; set; }

        public ValueUpdated(string counterId, int counterValue)
        {
            this.CounterId = counterId;
            this.CounterValue = counterValue;
        }
    }

    public class ValueIncremented
    {
        public string CounterId { get;  set; }
        public int OldCounterValue { get;  set; }
        public int NewCounterValue { get;  set; }

        public ValueIncremented(string counterId, int oldCounterValue, int newCounterValue)
        {
            this.CounterId = counterId;
            this.OldCounterValue = oldCounterValue;
            this.NewCounterValue = newCounterValue;
        }
    }
}
