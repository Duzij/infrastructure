using Infrastructure.Core;

namespace Library.Tests
{
    public class TestCounter : Aggregate<string>
    {
        public int CounterValue { get; private set; }

        public static TestCounter Create(string idValue, int value)
        {
            return new TestCounter(new CounterId(idValue), value);
        }

        public void IncrementValue()
        {
            CounterValue++;
        }

        public void IncrementValueWithEvent()
        {
            CounterValue++;
            var oldValue = CounterValue - 1;
            var newValue = CounterValue;
            AddEvent(new ValueIncremented(Id.Value, oldValue, newValue));
        }

        public TestCounter(CounterId counterId, int value)
        {
            Id = counterId;
            CounterValue = value;
            AddEvent(new CounterCreatedEvent(counterId, value));
        }

        public override void CheckState()
        {
        }

        public void UpdateCounterWithValue(int i)
        {
            CounterValue = i;
            AddEvent(new ValueUpdated(Id.Value, CounterValue));
        }
    }

    internal class CounterCreatedEvent
    {
        private readonly CounterId counterId;
        private readonly int value;

        public CounterCreatedEvent()
        {
        }

        public CounterCreatedEvent(CounterId counterId, int value)
        {
            this.counterId = counterId;
            this.value = value;
        }
    }

    internal class ValueUpdated
    {
        public string CounterId { get; set; }
        public int CounterValue { get; set; }

        public ValueUpdated(string counterId, int counterValue)
        {
            CounterId = counterId;
            CounterValue = counterValue;
        }
    }

    public class ValueIncremented
    {
        public string CounterId { get; set; }
        public int OldCounterValue { get; set; }
        public int NewCounterValue { get; set; }

        public ValueIncremented(string counterId, int oldCounterValue, int newCounterValue)
        {
            CounterId = counterId;
            OldCounterValue = oldCounterValue;
            NewCounterValue = newCounterValue;
        }
    }
}
