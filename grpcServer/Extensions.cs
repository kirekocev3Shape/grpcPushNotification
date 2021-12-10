using Google.Protobuf.WellKnownTypes;
using PublishSubscribe;

namespace grpcServer
{
    public static class Extensions
    {
        public static Protos.Case ToProto(this Models.Case @case)
        {
            return new Protos.Case
            {
                Id = @case.Id.ToString(),
                Name = @case.Name,
                Value = @case.Value,
                Time = Timestamp.FromDateTime(@case.TimeStemp)
            };
        }

        public static Models.Case ToModel(this Protos.Case @case)
        {
            return new Models.Case
            {
                Id = new Guid(@case.Id),
                Name = @case.Name,
                Value = @case.Value,
                TimeStemp = @case.Time.ToDateTime()
            };
        }

        public static Protos.CaseAction ToProto(this CaseAction caseAction)
        {
            switch (caseAction)
            {
                case CaseAction.Add:
                    return Protos.CaseAction.Add;
                case CaseAction.Remove:
                    return Protos.CaseAction.Remove;
                default:
                    throw new ArgumentOutOfRangeException(nameof(caseAction));
            }
        }
    }
}
