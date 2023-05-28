namespace ChatApp.Models.Message
{
	public class MessageViewModel
	{

		public string Sender { get; set; } = null!;

		public string MessageText { get; set; } = null!;
	}

	public class ChatViewModel
	{
		public MessageViewModel CurrentMessage { get; set; } = null!;

		public List<MessageViewModel> Messages { get; set; } = null!;
	}
}
