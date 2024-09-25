using Microsoft.AspNetCore.Identity;
using System;



namespace Whatsapp_Api.Core.Models;

public partial class User  : BaseEntity
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public bool PhoneVerific { get; set; }
    public string? ProfilePhoto { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Message> MessagesSender { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessagesReceiver { get; set; } = new List<Message>();

    public virtual ICollection<GroupMembers> GroupMembers { get; set; } = new List<GroupMembers>();
}


