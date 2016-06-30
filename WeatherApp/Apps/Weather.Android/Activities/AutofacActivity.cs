using Android.App;
using Android.OS;
using Autofac;

namespace Weather.Android.Activities
{
    public abstract class AutofacActivity : Activity
    {

        private static ContainerBuilder ContainerBuilder { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
    }
}