var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "solveEquation",
    pattern: "{controller=Tool}/{action=Solve}/{a?}/{b?}/{c?}"
);

app.UseStaticFiles();

app.MapControllerRoute(
    name: "gameSet",
    pattern: "Game/Set,{n}",
    defaults: new { controller = "Game", action = "Set" }
);

app.MapControllerRoute(
    name: "gameGuess",
    pattern: "Game/Guess,{guess}",
    defaults: new { controller = "Game", action = "Guess" }
);

app.Run();
