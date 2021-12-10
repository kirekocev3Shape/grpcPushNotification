using Models;

namespace PublishSubscribe
{
    public class Subscriber : IDisposable
    {
        Subscription<CaseMessage> _caseToken;
        EventAggregator _eventAggregator;

        public event Action<CaseMessage>? CaseChanged;

        public Subscriber(EventAggregator eve)
        {
            _eventAggregator = eve;
            _caseToken = eve.Subscribe<CaseMessage>(this.CaseAction);
        }

        private void CaseAction(CaseMessage caseMessage)
        {
            CaseChanged?.Invoke(caseMessage);
        }

        public void Dispose()
        {
            _eventAggregator.UnSbscribe(_caseToken);
        }
    }
}
