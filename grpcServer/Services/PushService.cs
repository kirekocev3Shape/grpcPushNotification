using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using grpcServer.Protos;
using Models;
using PublishSubscribe;

namespace grpcServer.Services
{
    public class PushService : PushNotification.PushNotificationBase
    {
        private readonly ILogger<PushService> _logger;
        private readonly EventAggregator _eventAggregator;
        private readonly Publisher _publisher;
        private readonly DataBase _db;

        public PushService(ILogger<PushService> logger, EventAggregator eventAggregator, Publisher publisher, DataBase db)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
            _publisher = publisher;
            _db = db;
        }

        public override async Task Subscribe(TopicRequest request, IServerStreamWriter<TickerUpdateResponse> responseStream, ServerCallContext context)
        {
            using var sub = new Subscriber(_eventAggregator);
            sub.CaseChanged += async caseMassage => await WriteCaseChangesAsync(responseStream, caseMassage);
            
            _logger.LogInformation($"Subscription started. {context.Peer}");
            
            await AwaitCancellation(context.CancellationToken);

            _logger.LogInformation($"Subscription stoped. {context.Peer}");
        }

        public override Task<GetAllResponse> GetAll(Empty request, ServerCallContext context)
        {
            _logger.LogInformation($"Get all cases. {context.Peer}");

            var response = new GetAllResponse();
            response.Cases.AddRange(_db.Cases.Select(c => c.ToProto()));
            return Task.FromResult(response);
        }

        public override Task<Empty> Add(Protos.Case request, ServerCallContext context)
        {
            _logger.LogInformation($"Add case. {context.Peer}");

            var modelCase = request.ToModel();
            _db.Cases.Add(modelCase);
            _publisher.PublishMessage(new CaseMessage { Case = modelCase, CaseAction = PublishSubscribe.CaseAction.Add});
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Remove(CaseIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Remove case. {context.Peer}");

            var caseModel = _db.Cases.FirstOrDefault(c => c.Id == new Guid(request.Id));
            if (caseModel != null)
            {
                _db.Cases.Remove(caseModel);
                _publisher.PublishMessage(new CaseMessage { Case = caseModel, CaseAction = PublishSubscribe.CaseAction.Remove });
            }
            return Task.FromResult(new Empty());
        }

        private async Task WriteCaseChangesAsync(IServerStreamWriter<TickerUpdateResponse> stream, CaseMessage caseMessage)
        {
            if (caseMessage == null)
                return;

            try
            {
                _logger.LogInformation($"Write message to stream.");

                await stream.WriteAsync(new TickerUpdateResponse 
                {                     
                    Case = caseMessage.Case?.ToProto(),
                    CaseAction = caseMessage.CaseAction.ToProto(),
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to write message: {e.Message}");
            }
        }

        private static Task AwaitCancellation(CancellationToken token)
        {
            var completion = new TaskCompletionSource<object>();
            _ = token.Register(() => completion.SetResult(null));
            return completion.Task;
        }
    }
}
