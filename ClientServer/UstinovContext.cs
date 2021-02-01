using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClientServer
{
    public partial class UstinovContext : DbContext
    {
        public UstinovContext()
        {
        }

        public UstinovContext(DbContextOptions<UstinovContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<MailPost> MailPosts { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserDatum> UserData { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        [DbFunction(@"ValidUser")]
        public static int ValidUser(string login, string pass)
        {
            throw new NotSupportedException("Direct calls are not supported");
        }
        [DbFunction(@"ParcelsTable")]
        public IQueryable<ParcelsWindow.Parcel> ParcelsTable(int id) => FromExpression(() => (ParcelsTable(id)));
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-7535NI5;Database=Ustinov;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PK__Client__213EE77489CAEB48");

                entity.ToTable("Client");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.CFn)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("c_FN");

                entity.Property(e => e.CIndex)
                    .HasColumnType("decimal(9, 0)")
                    .HasColumnName("c_index");

                entity.Property(e => e.CPassportNumber).HasColumnName("c_passport_number");

                entity.Property(e => e.CPassportSeries).HasColumnName("c_passport_series");

                entity.HasOne(d => d.CIndexNavigation)
                    .WithMany(p => p.Clients)
                    .HasPrincipalKey(p => p.MIndex)
                    .HasForeignKey(d => d.CIndex)
                    .HasConstraintName("FK__Client__c_index__656C112C");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GId)
                    .HasName("PK__Groups__49FB61C4460B8B49");

                entity.Property(e => e.GId).HasColumnName("g_id");

                entity.Property(e => e.GGroup)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("g_group");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.IId)
                    .HasName("PK__Inventor__98F919BA4B2B3CE1");

                entity.ToTable("Inventory");

                entity.Property(e => e.IId).HasColumnName("i_id");

                entity.Property(e => e.IAmount).HasColumnName("i_amount");

                entity.Property(e => e.IFragile)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("i_fragile");

                entity.Property(e => e.IPrice)
                    .HasColumnType("money")
                    .HasColumnName("i_price");

                entity.Property(e => e.IProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("i_product_name");
            });

            modelBuilder.Entity<MailPost>(entity =>
            {
                entity.HasKey(e => e.MId)
                    .HasName("PK__MailPost__7C8D7D29BF5F15D0");

                entity.ToTable("MailPost");

                entity.HasIndex(e => e.MIndex, "UQ__MailPost__1037C7CAE6E9AF63")
                    .IsUnique();

                entity.Property(e => e.MId).HasColumnName("m_id");

                entity.Property(e => e.MAddress)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("m_address");

                entity.Property(e => e.MIndex)
                    .HasColumnType("decimal(9, 0)")
                    .HasColumnName("m_index");

                entity.Property(e => e.MLastUpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("m_last_update_time");

                entity.Property(e => e.MPhone)
                    .HasColumnType("decimal(11, 0)")
                    .HasColumnName("m_phone");

                entity.Property(e => e.MPostamatCount).HasColumnName("m_postamat_count");
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PK__Parcel__82E06B91675FA156");

                entity.ToTable("Parcel");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.Property(e => e.PInventory).HasColumnName("p_inventory");

                entity.Property(e => e.POffice).HasColumnName("p_office");

                entity.Property(e => e.PReciever).HasColumnName("p_reciever");

                entity.Property(e => e.PTariff)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("p_tariff");

                entity.Property(e => e.PTax)
                    .HasColumnType("money")
                    .HasColumnName("p_tax");

                entity.HasOne(d => d.PInventoryNavigation)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.PInventory)
                    .HasConstraintName("FK__Parcel__p_invent__71D1E811");

                entity.HasOne(d => d.POfficeNavigation)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.POffice)
                    .HasConstraintName("FK__Parcel__p_office__6FE99F9F");

                entity.HasOne(d => d.PRecieverNavigation)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.PReciever)
                    .HasConstraintName("FK__Parcel__p_reciev__70DDC3D8");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RId)
                    .HasName("PK__Roles__C4762327057C9B50");

                entity.Property(e => e.RId).HasColumnName("r_id");

                entity.Property(e => e.RRole)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("r_role");
            });

            modelBuilder.Entity<UserDatum>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__UserData__B51D3DEABD0A92C6");

                entity.HasIndex(e => e.UInfo, "UQ__UserData__1605AA38B5B7FC32")
                    .IsUnique();

                entity.HasIndex(e => e.ULogin, "UQ__UserData__9344391DF5255669")
                    .IsUnique();

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.UInfo).HasColumnName("u_info");

                entity.Property(e => e.ULogin)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("u_login");

                entity.Property(e => e.UPassHash)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("u_pass_hash");

                entity.Property(e => e.URole).HasColumnName("u_role");

                entity.HasOne(d => d.UInfoNavigation)
                    .WithOne(p => p.UserDatum)
                    .HasForeignKey<UserDatum>(d => d.UInfo)
                    .HasConstraintName("FK__UserData__u_info__1AD3FDA4");

                entity.HasOne(d => d.URoleNavigation)
                    .WithMany(p => p.UserData)
                    .HasForeignKey(d => d.URole)
                    .HasConstraintName("FK__UserData__u_role__1BC821DD");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.UgId)
                    .HasName("PK__UserGrou__3F051BC31EC2A153");

                entity.ToTable("UserGroup");

                entity.Property(e => e.UgId).HasColumnName("ug_id");

                entity.Property(e => e.UgGroup).HasColumnName("ug_group");

                entity.Property(e => e.UgRole).HasColumnName("ug_role");

                entity.HasOne(d => d.UgGroupNavigation)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UgGroup)
                    .HasConstraintName("FK__UserGroup__ug_gr__1332DBDC");

                entity.HasOne(d => d.UgRoleNavigation)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UgRole)
                    .HasConstraintName("FK__UserGroup__ug_ro__14270015");
            });

            modelBuilder.Entity<ParcelsWindow.Parcel>(entity => entity.HasNoKey());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
