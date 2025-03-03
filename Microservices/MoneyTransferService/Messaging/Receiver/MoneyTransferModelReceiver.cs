using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankAccountService.MoneyTransfer.Common.Models;
using BankAccountService.MoneyTransfer.Messaging.Options;
using BankAccountService.MoneyTransfer.Messaging.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BankAccountService.MoneyTransfer.Messaging.Receiver
{
    public class MoneyTransferModelReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IMoneyTransferService _moneyTransferService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public MoneyTransferModelReceiver(IMoneyTransferService moneyTransferService, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _moneyTransferService = moneyTransferService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var moneyTransferModel = JsonConvert.DeserializeObject<MoneyTransferModel>(content);

                HandleMessage(moneyTransferModel);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(MoneyTransferModel moneyTransferModel)
        {
            _moneyTransferService.TransferMoney(moneyTransferModel);
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}