using System.Diagnostics;
using Services.Interfaces;

namespace Services.Service
{
    public class DebugLogger : ILogger
    {
        public void Log(string logMessage)
        {
            Debug.WriteLine($"Debug message - {logMessage}");
        }
    }
}