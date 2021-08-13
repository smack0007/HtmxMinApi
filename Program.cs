using System.Reflection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var staticFilesPath = app.Environment.IsDevelopment()
    ? Directory.GetCurrentDirectory()
    : Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);

Console.WriteLine($"Serving files from {staticFilesPath}");

app.UseFileServer(new FileServerOptions  
{  
    FileProvider = new PhysicalFileProvider(staticFilesPath),  
    RequestPath = "",  
    EnableDefaultFiles = true  
});

app.MapGet("/person", () => {
    return "<div class=\"name\">Zachary</div>";
});

app.Run();
