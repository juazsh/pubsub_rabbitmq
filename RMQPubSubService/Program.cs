using RMQPubSubService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PublishingService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/publish", async (string message, PublishingService publisher) =>
{
    await publisher.Publish(message);
    return Results.Ok();
});

app.UseHttpsRedirection();
app.Run();

