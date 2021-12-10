using Models;

namespace PublishSubscribe
{
    public class Publisher
    {
        private readonly EventAggregator _eventAggregator;
        public Publisher(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void PublishMessage(CaseMessage newCase)
        {
            _eventAggregator.Publish(newCase);
        }
    }
}
