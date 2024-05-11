
public interface ISingleton{
    Guid Value();
}

public interface IScoped{
    Guid Value();
}

public interface ITransient{
    Guid Value();
}

public class DIDemoService : ISingleton, IScoped, ITransient
{
    private Guid _guid = Guid.NewGuid();

    public Guid Value() => _guid;
}