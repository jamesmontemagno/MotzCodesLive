using MvvmHelpers;
using Xamarin.Forms;
using XamChat.Model;

using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;
using XamChat.Helpers;

namespace XamChat.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
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
            if (DesignMode.IsDesignModeEnabled)
                return;

            Title = Settings.Group;

            ChatMessage = new ChatMessage();
            Messages = new ObservableRangeCollection<ChatMessage>();
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            random = new Random();

            Service.Init(Settings.ServerIP);

            Service.OnReceivedMessage += (sender, args) =>
            {
                SendLocalMessage(args.Message);
            };

            Service.OnEnteredOrExited += (sender, args) =>
            {
                SendLocalMessage(args.Message);
            };

            Service.OnConnectionClosed += (sender, args) =>
            {
                SendLocalMessage(args.Message);  
            };
        }



        async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {                
                await Service.ConnectAsync();
                await Service.JoinChannelAsync(Settings.Group, Settings.UserName);
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
            await Service.LeaveChannelAsync(Settings.Group, Settings.UserName);
            await Service.DisconnectAsync();
            IsConnected = false;
            SendLocalMessage("Disconnected...");
        }

        async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await Service.SendMessageAsync(Settings.Group,
                    Settings.UserName,
                    ChatMessage.Message);

                ChatMessage.Message = string.Empty;
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
