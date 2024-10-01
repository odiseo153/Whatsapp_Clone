using System;
using Whatsapp_Api.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace Whatsapp_Api.Infraestructure.Context;

public partial class SocialMediaContext : DbContext
{
    public SocialMediaContext()
    {
    }

    public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<GroupMembers> GroupMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=odiseo\\ODISEO;Database=SocialMedia;User Id=odiseo;Password=Padomo153;TrustServerCertificate=True;");
     */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conversa__3213E83F51898DD2");

            entity.ToTable("Conversacion");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.LastMessage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SenderId).HasColumnName("senderId");
            entity.Property(e => e.ReceiverId).HasColumnName("receiverId");

            entity.HasOne(d => d.Sender).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Conversac__usuar__286302EC");

        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grupo__3213E83F0F0F9A30");

            entity.ToTable("Grupo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AdministratorId);
            entity.Property(e => e.ImageGroup)
                .HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.AdministratorUser).WithMany(p => p.Groups)
                .HasForeignKey(d => d.AdministratorId)
                .HasConstraintName("FK__Grupo__administr__33D4B598");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mensaje__3213E83F55A40578");

            entity.ToTable("Mensaje");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Content);
            entity.Property(e => e.ConversationId);
            entity.Property(e => e.SenderId);
            entity.Property(e => e.Image)
            .HasColumnType("text");

            entity.Property(e => e.Read)
                .HasDefaultValue(false);
            entity.Property(e => e.SendDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ReceiverId);


            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK__Mensaje__convers__300424B4");

            entity.HasOne(d => d.ReceiverUser).WithMany(p => p.MessagesReceiver)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK__Mensaje__destina__2F10007B");

            entity.HasOne(d => d.SenderUser).WithMany(p => p.MessagesSender)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Mensaje__remiten__2E1BDC42");
        });

        modelBuilder.Entity<GroupMembers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Miembro___3213E83F72AB58AC");

            entity.ToTable("Miembro_Grupo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Date_In)
                .HasColumnType("datetime")
                .HasColumnName("fecha_incorporacion");
            entity.Property(e => e.GroupId).HasColumnName("grupo_id");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.UserId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Miembro_G__grupo__37A5467C");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Miembro_G__usuar__38996AB5");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83FDB7129C3");

            entity.ToTable("Usuario");
        });

     
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
