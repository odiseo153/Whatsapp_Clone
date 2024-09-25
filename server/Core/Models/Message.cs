



namespace Whatsapp_Api.Core.Models;

public partial class Message : BaseEntity
{
  
    public string? Content { get; set; }

    public DateTime SendDate { get; set; } = DateTime.Now;

    public bool? Read { get; set; } = false;

    public string? SenderId { get; set; }

    public string? ReceiverId { get; set; }

    public string? ConversationId { get; set; }

    public virtual Conversation? Conversation { get; set; }

    public virtual User? ReceiverUser { get; set; }

    public virtual User? SenderUser { get; set; }
}
