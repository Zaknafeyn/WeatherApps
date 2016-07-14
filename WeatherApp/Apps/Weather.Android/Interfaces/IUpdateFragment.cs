namespace Weather.AndroidApp.Interfaces
{

    public interface IUpdateFragment
    {
        void Update(object data);
    }

    public interface IUpdateFragment<in TData> : IUpdateFragment
    {
        void Update(TData data);
    }
}