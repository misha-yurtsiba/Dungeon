using Zenject;

public class StatesFactory
{
    private readonly IInstantiator _instantiator;

    public StatesFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public T Create<T>()
    {
        return _instantiator.Instantiate<T>();
    }
}