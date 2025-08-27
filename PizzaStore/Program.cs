using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseInMemoryDatabase("PizzaStoreDb"));


builder.Services.AddScoped<IToppingService, ToppingService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    context.Database.EnsureCreated();
}

app.Run();