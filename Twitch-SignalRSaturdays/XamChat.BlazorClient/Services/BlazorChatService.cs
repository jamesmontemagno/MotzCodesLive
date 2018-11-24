using Blazor.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamChat.Core.EventHandlers;

namespace XamChat.BlazorClient.Services
{
	public class BlazorChatService
	{
		public event EventHandler<MessageEventArgs> OnReceivedMessage;
		public event EventHandler<MessageEventArgs> OnEnteredOrExited;
		public event EventHandler<MessageEventArgs> OnConnectionClosed;

		HubConnection hubConnection;
		Random random = new Random();

		bool IsConnected { get; set; }
		Dictionary<string, string> ActiveChannels { get; } = new Dictionary<string, string>();


		public void Init(string ip)
		{
			hubConnection = new HubConnectionBuilder()
					.WithUrl($"http://{ip}:5000/hubs/chat",
					opt =>
					{
						opt.LogLevel = SignalRLogLevel.Trace; // Client log level
						opt.SkipNegotiation = true;
						opt.Transport = HttpTransportType.WebSockets; // Which transport you want to use for this connection
					})
					.Build();

			hubConnection.OnClose( async (error) =>
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
					OnConnectionClosed?.Invoke(this, new MessageEventArgs($"Failed to reconnect: {ex.GetBaseException().Message}"));
				}
			});

			hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
			{
				var finalMessage = $"{user} says {message}";
				OnReceivedMessage?.Invoke(this, new MessageEventArgs(finalMessage));
				return Task.CompletedTask;
			});

			hubConnection.On<string>("EnteredOrLeft", (message) =>
			{
				OnEnteredOrExited?.Invoke(this, new MessageEventArgs(message));
				return Task.CompletedTask;
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

			await hubConnection.InvokeAsync("RemoveFromGroup", group, userName);
			ActiveChannels.Remove(group);
		}

		public async Task JoinChannelAsync(string group, string userName)
		{
			if (!IsConnected || ActiveChannels.ContainsKey(group))
				return;


			await hubConnection.InvokeAsync("AddToGroup", group, userName);
			ActiveChannels.Add(group, userName);

		}

		public async Task SendMessageAsync(string group, string userName, string message)
		{
			await hubConnection.InvokeAsync("SendMessageGroup",
							group,
							userName,
							message);
		}

		public List<string> GetRooms()
		{
			var Rooms = new List<string>
						{
								".NET",
								"ASP.NET",
								"Xamarin"
						};
			return Rooms;
		}
	}
}
