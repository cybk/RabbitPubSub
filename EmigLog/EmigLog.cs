using RabbitMQ.Client;
using System;
using System.Text;

namespace EmigLog
{
    public class EmigLog
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var conn = factory.CreateConnection();
            using var channel = conn.CreateModel();

            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            var mess = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(mess);
            channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);

            Console.WriteLine($"[x] sent {mess}");

            Console.WriteLine("Press [enter to exit]");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return  ((args.Length > 0) ? string.Join(" ", args) : "info: Hello World!!");
        }
    }
}
