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
                    // Declara a fila a ser usada
                    channel.QueueDeclare(queue: "meu_queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    for (int i = 0; i < 10; i++)
                    {
                        // Prepara a mensagem a ser enviada
                        Console.WriteLine("Informe o código do funcionário");
                        int meuInt = int.Parse(Console.ReadLine());
                        string meuDataHora = DateTime.Now.ToString();

                        // Concatena o inteiro e a data e hora em uma única string
                        string mensagem = $"{meuInt};{meuDataHora}";

                        // Converte a mensagem em um array de bytes
                        var body = Encoding.UTF8.GetBytes(mensagem);

                        // Publica a mensagem na fila
                        channel.BasicPublish(exchange: "",
                                             routingKey: "meu_queue",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine($"Mensagem enviada: {i}", mensagem);
                    }
                }
            }
        }
    }
}