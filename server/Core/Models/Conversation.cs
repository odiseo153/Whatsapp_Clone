

using System.Text.Json.Serialization;

namespace Whatsapp_Api.Core.Models;


public partial class Conversation : BaseEntity
{

    public string? LastMessage { get; set; }
    public string? SenderId { get; set; }
    public string? ReceiverId { get; set; }
    public virtual User? Sender { get; set; }
    public virtual User? Receiver { get; set; }
    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

}





