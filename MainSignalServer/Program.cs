using System.Text.Json;
using MainSignalServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebPush;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PizzaStore API",
        Description = "Making the Pizzas you love",
        Version = "v1"
    });
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
    
});
builder.Services.AddSignalR();
builder.Services.AddDbContext<SubscriptionDb>(options => options.UseInMemoryDatabase("items"));


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1"); });
app.UseCors("MyAllowedOrigins");


app.MapHub<MainHub>("/current-time");

string subject = "mailto:erick.emao@gmail.com";
string publicKey = "BI8i-gq-JMMmkzDUdEemTvGNOF7v2YlQgzxo0UcCLLXkgYCWWdtfRQuM_U7oy6gWFF2zI5HjZkdlVMs9xxiJjvM";
string privateKey = "Gm0grF11D9p4uQC2FW8I0LXk4dKP1MPGrLhqu4pEiFQ";


app.MapPost("/subscribe", async (SubscriptionDb db, RequestSubs subscriptionReq) =>
{
    await db.AddAsync(new SubRequest
    {
        User = subscriptionReq.User,
        Endpoint = subscriptionReq.Endpoint,
        auth = subscriptionReq.keys.auth,
        P256dh = subscriptionReq.keys.P256dh
    });

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/", async (SubscriptionDb db, int user) =>
{
    var subscription = await db.Subscriptions.FindAsync(user);
    var susbcription = new PushSubscription
    {
        Auth = subscription.auth,
        Endpoint = subscription.Endpoint,
        P256DH = subscription.P256dh
    };
    var obj = new NotoficationResponse
    {
        Title = "Crack",
        Body = "TE has flipado",
    };

    var vapidDetails = new VapidDetails(subject, publicKey, privateKey);
    var webPushClient = new WebPushClient();

    await webPushClient.SendNotificationAsync(susbcription, JsonSerializer.Serialize(obj), vapidDetails);
    return Results.Ok(susbcription);
});

app.MapGet("/", async (SubscriptionDb db) =>
{
    var subscriptions = await db.Subscriptions.ToListAsync();
    return Results.Ok(subscriptions);
});

app.Run();