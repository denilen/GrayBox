using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using GrpcGreeter;

namespace gb.grpc.client
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:5002");

            // создаем клиента
            var client = new Greeter.GreeterClient(channel);

            Console.Write("Введите имя: ");

            var name = Console.ReadLine();

            // обмениваемся сообщениями с сервером
            var reply = await client.SayHelloAsync(new HelloRequest { Name = name });

            Console.WriteLine("Ответ сервера: " + reply.Message);
            Console.ReadKey();
        }
    }
}
