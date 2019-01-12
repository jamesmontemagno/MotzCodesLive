using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamChat.Core;
using XamChat.Core.EventHandlers;

namespace XamChat.BlazorClient.Components
{
	public class ChatComponentModel : BlazorComponent
	{

		[Inject] ChatService Service { get; set; }
		[Inject] IUriHelper UriHelper { get; set; }

		[Parameter] protected RenderFragment<MessageEventArgs> ItemTemplate { get; set; }
		[Parameter] protected RenderFragment<List<MessageEventArgs>> HeaderTemplate { get; set; }


		internal string UserName;
		internal string Server;
		internal string Room;
		internal List<MessageEventArgs> Messages { get; set; }
		internal string NewMessage;
		internal bool IsConnected;
		internal bool IsConnecting;
		internal bool IsRoomJoined;

		protected override async Task OnInitAsync()
		{
			await base.OnInitAsync();
			Messages = new List<MessageEventArgs>();
		}

		private void Service_OnReceivedMessage(object sender, MessageEventArgs e)
		{
			Messages.Add(e);
			StateHasChanged();
		}

		internal async Task ConnectServer(UIMouseEventArgs args)
		{
			if (string.IsNullOrWhiteSpace(Server) || string.IsNullOrWhiteSpace(UserName))
				return;

			IsConnecting = true;
			IsConnected = false;
			Service.OnReceivedMessage += Service_OnReceivedMessage;
			Service.Init(Server, Server.ToLower() == "localhost" ? false : true);
			try
			{
				await Service.ConnectAsync();

				IsConnecting = false;
				IsConnected = true;
				Messages.Add(new MessageEventArgs("You are connected..."));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.GetBaseException().Message);
				IsConnecting = false;
				IsConnected = false;
				Messages.Add(new MessageEventArgs(ex.GetBaseException().Message));
			}
		}

		internal async Task JoinRoom(string Room)
		{
			this.Room = Room;
			await Service.JoinChannelAsync(Room, UserName);
			IsRoomJoined = true;
		}

		internal async Task SendNewMessage(UIMouseEventArgs args)
		{
			await Service.SendMessageAsync(Room, UserName, NewMessage);
			NewMessage = "";
		}

		internal List<string> GetRooms()
		{
			return Service.GetRooms();
		}
	}
}
