namespace ApiClientExample;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddHttpClient();
        services.AddScoped<IApiService, ApiService>();
        var serviceProvider = services.BuildServiceProvider();

        var apiService = serviceProvider.GetRequiredService<IApiService>();

        // Пример загрузки файла
        await using (var fileStream = File.OpenRead("test.txt"))
        {
            var uploadResponse = await apiService.UploadFileAsync(
                "upload",
                fileStream,
                "test.txt",
                new Dictionary<string, string> { { "description", "Sample file" } }
            );
            Console.WriteLine($"Upload status: {uploadResponse.StatusCode}");
        }

        // Пример выгрузки файла
        await using (var outputStream = File.Create("downloaded.txt"))
        {
            await apiService.DownloadFileAsync("files/sample.pdf", outputStream);
            Console.WriteLine("File downloaded!");
        }
    }
}
