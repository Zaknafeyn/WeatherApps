using Android.App;
using Android.Content;

namespace Weather.AndroidApp.AppServices
{
    public interface IAppLogger
    {
        void Init(Context context, Application application);
        void Log(string message);
        bool Initialized { get; }
    }
}