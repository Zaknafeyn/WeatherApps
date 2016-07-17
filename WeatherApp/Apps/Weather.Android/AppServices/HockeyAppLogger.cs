using Android.App;
using Android.Content;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;

namespace Weather.AndroidApp.AppServices
{
    public class HockeyAppLogger : IAppLogger
    {
        public void Init(Context context, Application application)
        {
            if (Initialized)
                return;

            CrashManager.Register(context);
            MetricsManager.Register(context, application);

            Log("Application is initializing...");

            Initialized = true;
        }

        public void Log(string message)
        {
            HockeyApp.MetricsManager.TrackEvent(message);
        }

        public bool Initialized { get; private set; }
    }
}