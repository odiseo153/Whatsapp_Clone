using System;
using System.Collections.Generic;



namespace Whatsapp_Api.Core.Models;

public partial class GroupMembers : BaseEntity
{

    public string? GroupId { get; set; }

    public string? UserId { get; set; }

    public string? Rol { get; set; }

    public DateTime Date_In { get; set; }

    public virtual Group? Group { get; set; }

    public virtual User? User { get; set; }
}
