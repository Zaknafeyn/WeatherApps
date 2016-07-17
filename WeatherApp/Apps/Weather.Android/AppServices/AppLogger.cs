using Android.App;
using Android.Content;

namespace Weather.AndroidApp.AppServices
{
    public class AppLogger : IAppLogger
    {
        const string tag = "application logger";
        public void Init(Context context, Application application)
        {
            Initialized = true;
        }

        public void Log(string message)
        {
            Android.Util.Log.Info(tag, message);
        }

        public bool Initialized { get; private set; }
    }
}