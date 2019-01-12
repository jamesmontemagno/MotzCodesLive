#if BLAZOR
using Blazor.Extensions;
#else
using Microsoft.AspNetCore.SignalR.Client;
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using XamChat.Core.EventHandlers;

namespace XamChat.Core
{
    public class ChatService
    {
        public event EventHandler<MessageEventArgs> OnReceivedMessage;
        public event EventHandler<MessageEventArgs> OnEnteredOrExited;
        public event EventHandler<MessageEventArgs> OnConnectionClosed;

        HubConnection hubConnection;
        Random random;

        bool IsConnected { get; set; }
        Dictionary<string, string> ActiveChannels { get; } = new Dictionary<string, string>();


        public void Init(string urlRoot, bool useHttps)
        {
            random = new Random();
            var url = $"http{(useHttps ? "s" : string.Empty)}://{urlRoot}/hubs/chat";
            hubConnection = new HubConnectionBuilder()
#if BLAZOR
            .WithUrl(url,
            opt =>
            {
                opt.LogLevel = SignalRLogLevel.Trace; // Client log level
                opt.SkipNegotiation = true;
                opt.Transport = HttpTransportType.WebSockets; // Which transport you want to use for this connection
            })
#else 
            .WithUrl(url)
#endif
            .Build();

#if BLAZOR
           hubConnection.OnClose(async (error) =>
#else
           hubConnection.Closed += async (error) =>
#endif
            {
                OnConnectionClosed?.Invoke(this, new MessageEventArgs("Connection closed..."));
                IsConnected = false;
                await Task.Delay(random.Next(0, 5) * 1000);
                try
                {
                    await ConnectAsync();
                }
                catch (Exception ex)
                {
                    // Exception!
                    Debug.WriteLine(ex);
                }
#if BLAZOR
            });
#else
            };
#endif

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var finalMessage = $"{user} says {message}";
                OnReceivedMessage?.Invoke(this, new MessageEventArgs(finalMessage));
#if BLAZOR
                return Task.CompletedTask;
#endif
            });

            hubConnection.On<string>("EnteredOrLeft", (message) =>
            {
                OnEnteredOrExited?.Invoke(this, new MessageEventArgs(message));
#if BLAZOR
                return Task.CompletedTask;
#endif
            });
        }

        public async Task ConnectAsync()
        {
            if (IsConnected)
                return;

            await hubConnection.StartAsync();
            IsConnected = true;
        }

        public async Task DisconnectAsync()
        {
            if (!IsConnected)
                return;

            await hubConnection.StopAsync();

            ActiveChannels.Clear();
            IsConnected = false;
        }

        public async Task LeaveChannelAsync(string group, string userName)
        {
            if (!IsConnected || !ActiveChannels.ContainsKey(group))
                return;

#if BLAZOR
            await hubConnection.InvokeAsync("RemoveFromGroup", group, userName);
#else            
            await hubConnection.SendAsync("RemoveFromGroup", group, userName);
#endif
            ActiveChannels.Remove(group);
        }

        public async Task JoinChannelAsync(string group, string userName)
        {
            if (!IsConnected || ActiveChannels.ContainsKey(group))
                return;

#if BLAZOR
            await hubConnection.InvokeAsync("AddToGroup", group, userName);
#else            
            await hubConnection.SendAsync("AddToGroup", group, userName);
#endif
            ActiveChannels.Add(group, userName);

        }

        public async Task SendMessageAsync(string group, string userName, string message)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Not connected");

            await hubConnection.InvokeAsync("SendMessageGroup",
                    group,
                    userName,
                    message);
        }

        public List<string> GetRooms()
        {
            return new List<string>
                        {
                                ".NET",
                                "ASP.NET",
                                "Xamarin"
                        };
        }
    }
}
