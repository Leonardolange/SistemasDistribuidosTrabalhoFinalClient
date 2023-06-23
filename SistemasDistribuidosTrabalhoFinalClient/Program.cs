using RabbitMQ.Client;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace SistemasDistribuidosTrabalhoFinalClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "ponto_queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    for (int i = 0; i < 10; i++)
                    {
                        string meuDataHora = DateTime.Now.ToString();

                        string mensagem = $"{i};{meuDataHora}";

                        var body = Encoding.UTF8.GetBytes(mensagem);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "ponto_queue",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine($"Mensagem enviada: {i}", mensagem);
                    }
                }
            }
        }
    }
}