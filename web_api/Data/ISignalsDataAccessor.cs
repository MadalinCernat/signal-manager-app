using SignalManagerAppWebApi.Models;

namespace SignalManagerAppWebApi.Data
{
    public interface ISignalsDataAccessor
    {
        void AddSignal(Signal newSignal);
        void DeleteSignal(string signalId);
        List<Signal> ReadSignals();
    }
}