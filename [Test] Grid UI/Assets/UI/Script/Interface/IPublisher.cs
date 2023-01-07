namespace Inventory
{
    public interface IPublisher
    {
        void Subscribe(ISubscriber subscriber);
        void Publish();
        void Unsubscribe(ISubscriber subscriber);
    }
}