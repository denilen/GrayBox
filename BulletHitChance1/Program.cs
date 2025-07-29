var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/bullet", (int playerSkill, int distance) =>
{
    // Suppose your data on the chance of hitting are stored in the range from 0 to 1
    const double baseHitChance = 0.8;     // The basic chance of hitting
    const double distanceModifier = 0.05; // Modifier for shooting range

    // We calculate the final chance of getting
    var hitChance = baseHitChance - distance * distanceModifier + playerSkill * 0.02;

    // We guarantee that the chance is within acceptable limits
    hitChance = Math.Max(0.0, Math.Min(1.0, hitChance));

    // We convert the chance to get into interest
    hitChance *= 100;

    return hitChance;
})
.WithName("GetBullet")
.WithOpenApi();

app.Run();
