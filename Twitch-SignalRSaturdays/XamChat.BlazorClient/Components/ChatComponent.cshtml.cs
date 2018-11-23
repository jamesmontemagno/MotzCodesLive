using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.Configuration;
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
		[Inject] IConfiguration Configuration { get; set; }

		[Parameter] protected RenderFragment<MessageEventArgs> ItemTemplate { get; set; }
		[Parameter] protected RenderFragment<List<MessageEventArgs>> HeaderTemplate { get; set; }


		private const string UserName = "Blazor App";

		internal string Room;
		internal List<MessageEventArgs> Messages { get; set; }
		internal string NewMessage;
		internal bool IsConnected;

		protected override async Task OnInitAsync()
		{
			await base.OnInitAsync();
			Messages = new List<MessageEventArgs>();
			Service.OnReceivedMessage += Service_OnReceivedMessage;
			Service.Init(Configuration["ChatServer"]);
			await Service.ConnectAsync();
			IsConnected = true;
			Messages.Add(new MessageEventArgs("You are connected..."));
		}

		private void Service_OnReceivedMessage(object sender, MessageEventArgs e)
		{
			Messages.Add(e);
			StateHasChanged();
		}

		internal async Task JoinRoom(UIChangeEventArgs args)
		{
			Room = args.Value.ToString();
			await Service.JoinChannelAsync(Room, UserName);
		}

		internal async Task SendNewMessage(UIChangeEventArgs args)
		{
			NewMessage = args.Value.ToString();
			await Service.SendMessageAsync(Room, "Blazor App", NewMessage);
		}
	}
}
