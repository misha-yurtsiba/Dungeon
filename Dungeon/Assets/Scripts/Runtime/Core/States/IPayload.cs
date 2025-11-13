public interface IPayload<T> : IExitable
{
    void Enter(T payload);
}