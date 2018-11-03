using MvvmHelpers;
using Xamarin.Forms;
using XamChat.Model;

using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;

namespace XamChat.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        HubConnection hubConnection;

        public ChatMessage ChatMessage { get; }

        public ObservableRangeCollection<ChatMessage> Messages { get; }

        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SetProperty(ref isConnected, value);
                });
            }
        }

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }

        Random random;
        public ChatViewModel()
        {
            ChatMessage = new ChatMessage();
            Messages = new ObservableRangeCollection<ChatMessage>();
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            random = new Random();

            var ip = "localhost";
            if (Device.RuntimePlatform == Device.Android)
                ip = "10.0.2.2";

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{ip}:5000/chatHub")
                .Build();

            hubConnection.Closed += async (error) =>
            {
                SendLocalMessage("Connection Closed...");
                IsConnected = false;
                await Task.Delay(random.Next(0, 5) * 1000);
                await Connect();
            };

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var finalMessage = $"{user} says {message}";
                SendLocalMessage(finalMessage);
            });
        }



        async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {                
                await hubConnection.StartAsync();
                IsConnected = true;
                SendLocalMessage("Connected...");
            }
            catch (Exception ex)
            {
                SendLocalMessage($"Connection error: {ex.Message}");
            }
        }

        async Task Disconnect()
        {
            if (!IsConnected)
                return;

            await hubConnection.StopAsync();
            IsConnected = false;
            SendLocalMessage("Disconnected...");
        }

        async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await hubConnection.InvokeAsync("SendMessage",
                    ChatMessage.User,
                    ChatMessage.Message );
            }
            catch (Exception ex)
            {
                SendLocalMessage($"Send failed: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SendLocalMessage(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(new ChatMessage
                {
                    Message = message
                });
            });
        }

    }
}
