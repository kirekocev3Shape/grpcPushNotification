using Models;

namespace PublishSubscribe
{
    public class CaseMessage
    {
        public Case? Case { get; set; }
        public CaseAction CaseAction{ get; set;}
    }

    public enum CaseAction
    {
        Add,
        Remove
    }
}
