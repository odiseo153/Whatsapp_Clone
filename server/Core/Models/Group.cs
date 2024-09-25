using System;
using System.Collections.Generic;


namespace Whatsapp_Api.Core.Models;

public partial class Group : BaseEntity
{
   
    public string Name { get; set; } = null!;

    public string? AdministratorId { get; set; }

    public string? Description { get; set; }

    public string? ImageGroup { get; set; }

    public virtual User? AdministratorUser { get; set; }

    public virtual ICollection<GroupMembers> GroupMembers { get; set; } = new List<GroupMembers>();
}
