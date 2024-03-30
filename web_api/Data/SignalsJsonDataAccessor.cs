using Newtonsoft.Json;
using SignalManagerAppWebApi.Models;

namespace SignalManagerAppWebApi.Data
{
    public class SignalsJsonDataAccessor : ISignalsDataAccessor
    {
        private readonly string _filePath;

        public SignalsJsonDataAccessor(string filePath)
        {
            _filePath = filePath;
        }

        public List<Signal> ReadSignals()
        {
            string json = File.ReadAllText(_filePath);
            List<Signal> signals = JsonConvert.DeserializeObject<List<Signal>>(json);
            return signals;
        }

        public void AddSignal(Signal newSignal)
        {
            List<Signal> signals = ReadSignals();
            signals.Add(newSignal);
            string json = JsonConvert.SerializeObject(signals, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void DeleteSignal(string signalId)
        {
            List<Signal> signals = ReadSignals();
            Signal signalToRemove = signals.FirstOrDefault(s => s.Id == signalId);
            if (signalToRemove != null)
            {
                signals.Remove(signalToRemove);
                string json = JsonConvert.SerializeObject(signals, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            else
            {
                throw new ArgumentException($"Signal with ID '{signalId}' not found.");
            }
        }
    }
}
