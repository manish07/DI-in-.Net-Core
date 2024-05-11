1. Create a .net MVC application `dotnet new mvc -o DIExamples`
2. Look for changes in `DIDemo.cs`
```
public interface ISingleton
{
    Guid Value();
}

public interface IScoped
{
    Guid Value();
}

public interface ITransient
{
    Guid Value();
}

public class DIDemoService : ISingleton, IScoped, ITransient
{
    private Guid _guid = Guid.NewGuid();

    public Guid Value() => _guid;
}
```
4. Changes in `Program.cs`
```
// Register DI services
builder.Services.AddSingleton<ISingleton, DIDemoService>();
builder.Services.AddScoped<IScoped, DIDemoService>();
builder.Services.AddTransient<ITransient, DIDemoService>();
```
```
app.MapGet("didemo", (ISingleton singletonService1, 
            ISingleton singletonService2,
            IScoped scopedService1, 
            IScoped scopedService2,
            ITransient transientService1,
            ITransient transientService2) =>{
                return new{
                    singletonService1 = singletonService1.Value(),
                    singletonService2 = singletonService2.Value(),
                    scopedService1 = scopedService1.Value(),
                    scopedService2 = scopedService2.Value(),
                    transientService1 = transientService1.Value(),
                    transientService2 = transientService2.Value()
                };
            });
```
6. Run : `dotnet run`
7. Check in browser: http://localhost:5199/didemo
8. Output:
```
{
  "singletonService1": "9d8fb755-4a54-400e-af64-40730c667bf9",
  "singletonService2": "9d8fb755-4a54-400e-af64-40730c667bf9",
  "scopedService1": "2c92123f-0e3c-4efe-8907-2da849136aed",
  "scopedService2": "2c92123f-0e3c-4efe-8907-2da849136aed",
  "transientService1": "0b083c3c-3ac9-42ca-af8e-2a6c3e004760",
  "transientService2": "9302a11d-55ea-4d66-8718-3bb11913032f"
}
```
   
