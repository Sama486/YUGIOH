using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace BlazorServerSignalRApp.Server.Hubs
{
  public class ChatHub : Hub
  {
    public async Task SendMessage(string user, string user2, string message, DateTime time)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, user2, message, time);
    }
  }
}