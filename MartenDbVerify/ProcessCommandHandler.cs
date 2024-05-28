using Marten;

namespace MartenDbVerify
{
    public class ProcessCommandHandler(IDocumentSession session)
    {
        private readonly IDocumentSession _session = session;

        public async Task Handle(CompleteStep1Command command)
        {
            var processIdentifier = "your_process_identifier"; // Replace with your logic to get process identifier
            var process = new ProcessAggregator();
            process.CompleteStep1(command.Data);

            var streamId = Guid.NewGuid();
            StreamIdStore.AddOrUpdate(processIdentifier, streamId);

            _session.Events.StartStream<ProcessAggregator>(streamId, new Step1Completed(command.Data));
            await _session.SaveChangesAsync();
        }

        public async Task Handle(CompleteStep2Command command)
        {
            var processIdentifier = "your_process_identifier"; // Replace with your logic to get process identifier
            var streamId = StreamIdStore.GetStreamId(processIdentifier);
            if (streamId == Guid.Empty) throw new InvalidOperationException("Stream not found for this process identifier");

            var process = await _session.Events.AggregateStreamAsync<ProcessAggregator>(streamId);
            process.CompleteStep2(command.Data);

            _session.Events.Append(streamId, new Step2Completed(command.Data));
            await _session.SaveChangesAsync();
        }

        public async Task Handle(CompleteStep3Command command)
        {
            var processIdentifier = "your_process_identifier"; // Replace with your logic to get process identifier
            var streamId = StreamIdStore.GetStreamId(processIdentifier);
            if (streamId == Guid.Empty) throw new InvalidOperationException("Stream not found for this process identifier");

            var process = await _session.Events.AggregateStreamAsync<ProcessAggregator>(streamId);
            process.CompleteStep3(command.Data);

            _session.Events.Append(streamId, new Step3Completed(command.Data));
            await _session.SaveChangesAsync();
        }

        public async Task Handle(CompleteStep4Command command)
        {
            var processIdentifier = "your_process_identifier"; // Replace with your logic to get process identifier
            var streamId = StreamIdStore.GetStreamId(processIdentifier);
            if (streamId == Guid.Empty) throw new InvalidOperationException("Stream not found for this process identifier");

            var process = await _session.Events.AggregateStreamAsync<ProcessAggregator>(streamId);
            process.CompleteStep4(command.Data);

            _session.Events.Append(streamId, new Step4Completed(command.Data));
            await _session.SaveChangesAsync();
        }
    }

    public static class StreamIdStore
    {
        private static readonly Dictionary<string, Guid> _store = new();

        public static void AddOrUpdate(string processIdentifier, Guid streamId)
        {
            _store[processIdentifier] = streamId;
        }

        public static Guid GetStreamId(string processIdentifier)
        {
            return _store.ContainsKey(processIdentifier) ? _store[processIdentifier] : Guid.Empty;
        }
    }



    //public class ProcessCommandHandler(IDocumentSession documentSession)
    //{
    //    private readonly IDocumentSession documentSession = documentSession;

    //    public async Task Handle(CompleteStep1Command command)
    //    {
    //        var processAggregator = new ProcessAggregator();
    //        processAggregator.CompleteStep1(command.Data);
    //        documentSession.Events.StartStream<ProcessAggregator>(processAggregator.Id,new CompleteStep1Command(command.Data));
    //        await documentSession.SaveChangesAsync();
    //    }

    //    public async Task Handle(CompleteStep2Command command)
    //    {
    //        var processAggregator = new ProcessAggregator();
    //        processAggregator.CompleteStep2(command.Data);
    //        documentSession.Events.StartStream<ProcessAggregator>(processAggregator.Id, new CompleteStep2Command(command.Data));
    //        await documentSession.SaveChangesAsync();
    //    }


    //    public async Task Handle(CompleteStep3Command command)
    //    {
    //        var processAggregator = new ProcessAggregator();
    //        processAggregator.CompleteStep3(command.Data);
    //        documentSession.Events.StartStream<ProcessAggregator>(processAggregator.Id, new CompleteStep3Command(command.Data));
    //        await documentSession.SaveChangesAsync();
    //    }


    //    public async Task Handle(CompleteStep4Command command)
    //    {
    //        var processAggregator = new ProcessAggregator();
    //        processAggregator.CompleteStep4(command.Data);
    //        documentSession.Events.StartStream<ProcessAggregator>(processAggregator.Id, new CompleteStep4Command(command.Data));
    //        await documentSession.SaveChangesAsync();
    //    }

    //}
}
