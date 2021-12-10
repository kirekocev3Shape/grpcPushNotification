using System.Collections;

namespace PublishSubscribe
{
    public class EventAggregator
    {
        private readonly Dictionary<Type, IList> _subscriber;

        public EventAggregator()
        {
            _subscriber = new Dictionary<Type, IList>();
        }

        public void Publish<TMessageType>(TMessageType message)
        {
            var type = typeof(TMessageType);
            IList actionlst;
            if (_subscriber.ContainsKey(type))
            {
                actionlst = new List<Subscription<TMessageType>>(_subscriber[type].Cast<Subscription<TMessageType>>());
                foreach (Subscription<TMessageType> a in actionlst)
                {
                    a.Action(message);
                }
            }
        }

        public Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            var type = typeof(TMessageType);
            var actiondetail = new Subscription<TMessageType>(action, this);

            if (!_subscriber.TryGetValue(type, out var actionlst))
            {
                actionlst = new List<Subscription<TMessageType>> { actiondetail };
                _subscriber.Add(type, actionlst);
            }
            else
            {
                actionlst.Add(actiondetail);
            }

            return actiondetail;
        }

        public void UnSbscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            var type = typeof(TMessageType);
            if (_subscriber.ContainsKey(type))
            {
                _subscriber[type].Remove(subscription);
            }
        }
    }
}
