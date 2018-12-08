using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamChat.Core;

namespace XamChat.ViewModel
{
    public class ViewModelBase : BaseViewModel
    {
        ChatService service;
        public ChatService Service => service ?? (service = DependencyService.Resolve<ChatService>());
    }
}
