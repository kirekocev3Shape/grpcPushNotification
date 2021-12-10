using grpcServer.Services;
using Models;
using PublishSubscribe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddSingleton<EventAggregator>();
builder.Services.AddSingleton<DataBase>();
builder.Services.AddTransient<Publisher>(sp=>new Publisher(sp.GetService<EventAggregator>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors();
app.UseGrpcWeb();

app.UseEndpoints(endpoints => { endpoints.MapGrpcService<PushService>().EnableGrpcWeb().RequireCors("AllowAll"); });

app.Run();
