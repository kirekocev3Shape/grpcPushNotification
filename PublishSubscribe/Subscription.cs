namespace PublishSubscribe
{
    public class Subscription<TMessage> : IDisposable
    {
        public Action<TMessage> Action { get; private set; }
        private readonly EventAggregator EventAggregator;
        
        public Subscription(Action<TMessage> action, EventAggregator eventAggregator)
        {
            Action = action;
            EventAggregator = eventAggregator;
        }

        public void Dispose()
        {
            EventAggregator.UnSbscribe(this);
        }
    }
}
