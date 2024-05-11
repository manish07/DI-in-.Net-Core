var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISingleton, DIDemoService>();
builder.Services.AddScoped<IScoped, DIDemoService>();
builder.Services.AddTransient<ITransient, DIDemoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
