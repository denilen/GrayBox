using AddMongoMetaData.DiExtension;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.RegisterMongo(builder.Configuration);

// builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
