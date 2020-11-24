using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RecruitmentManagementDataAccess.Models
{
    public partial class RecruitmentManagementContext : DbContext
    {
        public RecruitmentManagementContext()
        {
        }

        public RecruitmentManagementContext(DbContextOptions<RecruitmentManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblApplicantActiveMaster> TblApplicantActiveMaster { get; set; }
        public virtual DbSet<TblApplicantRequisition> TblApplicantRequisition { get; set; }
        public virtual DbSet<TblApplicantRequisitionClient> TblApplicantRequisitionClient { get; set; }
        public virtual DbSet<TblApplicantRequisitionStaffing> TblApplicantRequisitionStaffing { get; set; }
        public virtual DbSet<TblApplicants> TblApplicants { get; set; }
        public virtual DbSet<TblCities> TblCities { get; set; }
        public virtual DbSet<TblClients> TblClients { get; set; }
        public virtual DbSet<TblClientsContactDetails> TblClientsContactDetails { get; set; }
        public virtual DbSet<TblCountries> TblCountries { get; set; }
        public virtual DbSet<TblDepartmentMaster> TblDepartmentMaster { get; set; }
        public virtual DbSet<TblDesignationMaster> TblDesignationMaster { get; set; }
        public virtual DbSet<TblEmployeePanelMapping> TblEmployeePanelMapping { get; set; }
        public virtual DbSet<TblEmployees> TblEmployees { get; set; }
        public virtual DbSet<TblGmapMaster> TblGmapMaster { get; set; }
        public virtual DbSet<TblInterviewEmployeeMapping> TblInterviewEmployeeMapping { get; set; }
        public virtual DbSet<TblInterviewEmployeeMappingStaffing> TblInterviewEmployeeMappingStaffing { get; set; }
        public virtual DbSet<TblInterviewManagement> TblInterviewManagement { get; set; }
        public virtual DbSet<TblInterviewManagementStaffing> TblInterviewManagementStaffing { get; set; }
        public virtual DbSet<TblJobPortal> TblJobPortal { get; set; }
        public virtual DbSet<TblJobTypeMaster> TblJobTypeMaster { get; set; }
        public virtual DbSet<TblLocationMaster> TblLocationMaster { get; set; }
        public virtual DbSet<TblLogin> TblLogin { get; set; }
        public virtual DbSet<TblLoginClient> TblLoginClient { get; set; }
        public virtual DbSet<TblNoticePeriodMaster> TblNoticePeriodMaster { get; set; }
        public virtual DbSet<TblPanelGroupMaster> TblPanelGroupMaster { get; set; }
        public virtual DbSet<TblQualificationMaster> TblQualificationMaster { get; set; }
        public virtual DbSet<TblRequisition> TblRequisition { get; set; }
        public virtual DbSet<TblRequisitionClientContactMappingStaffing> TblRequisitionClientContactMappingStaffing { get; set; }
        public virtual DbSet<TblRequisitionRecruiterMapping> TblRequisitionRecruiterMapping { get; set; }
        public virtual DbSet<TblRequisitionRecruiterMappingStaffing> TblRequisitionRecruiterMappingStaffing { get; set; }
        public virtual DbSet<TblRequisitionSkillMapping> TblRequisitionSkillMapping { get; set; }
        public virtual DbSet<TblRequisitionSkillMappingStaffing> TblRequisitionSkillMappingStaffing { get; set; }
        public virtual DbSet<TblRequisitionStaffing> TblRequisitionStaffing { get; set; }
        public virtual DbSet<TblRoleMaster> TblRoleMaster { get; set; }
        public virtual DbSet<TblSalaryBreakupTemplates> TblSalaryBreakupTemplates { get; set; }
        public virtual DbSet<TblSelectedApplicants> TblSelectedApplicants { get; set; }
        public virtual DbSet<TblSelectedApplicantsStaffing> TblSelectedApplicantsStaffing { get; set; }
        public virtual DbSet<TblSkillMaster> TblSkillMaster { get; set; }
        public virtual DbSet<TblStates> TblStates { get; set; }
        public virtual DbSet<TblVendorContacts> TblVendorContacts { get; set; }
        public virtual DbSet<TblVendors> TblVendors { get; set; }
        public virtual DbSet<TblWorkFlowMaster> TblWorkFlowMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=YAMUNA\\SQLDEV2016;Initial Catalog=RecruitmentManagement;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblApplicantActiveMaster>(entity =>
            {
                entity.HasKey(e => e.ApplicantActiveId);

                entity.ToTable("tbl_ApplicantActiveMaster");

                entity.Property(e => e.ApplicantActiveId).HasColumnName("ApplicantActiveID");

                entity.Property(e => e.ApplicantActive)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblApplicantRequisition>(entity =>
            {
                entity.HasKey(e => e.ApplicantRequisitionId)
                    .HasName("PK_tbl_ApplicantRequisition1");

                entity.ToTable("tbl_ApplicantRequisition");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ExpectedCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NegotiatedCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.RecruiterComment).HasMaxLength(1024);

                entity.Property(e => e.Status)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TentativeJoiningDate).HasColumnType("date");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.TblApplicantRequisition)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_tbl_Applicants");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblApplicantRequisitionCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblApplicantRequisitionModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_tbl_Employees1");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblApplicantRequisition)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_tbl_Requisition");
            });

            modelBuilder.Entity<TblApplicantRequisitionClient>(entity =>
            {
                entity.HasKey(e => e.ApplicantRequisitionId)
                    .HasName("PK_tbl_ApplicantRequisition_Client1");

                entity.ToTable("tbl_ApplicantRequisition_Client");

                entity.Property(e => e.ApplicantRequisitionId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ExpectedCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NegotiatedCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.RecruiterComment).HasMaxLength(1024);

                entity.Property(e => e.Status)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TentativeJoiningDate).HasColumnType("date");

                entity.Property(e => e.TentativeOnBoardingDate).HasColumnType("date");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.TblApplicantRequisitionClientApplicant)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Client_tbl_Applicants1");

                entity.HasOne(d => d.ApplicantRequisition)
                    .WithOne(p => p.TblApplicantRequisitionClientApplicantRequisition)
                    .HasForeignKey<TblApplicantRequisitionClient>(d => d.ApplicantRequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Client_tbl_Applicants");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblApplicantRequisitionClientCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Client_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblApplicantRequisitionClientModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Client_tbl_Employees1");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblApplicantRequisitionClient)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Client_tbl_Requisition");
            });

            modelBuilder.Entity<TblApplicantRequisitionStaffing>(entity =>
            {
                entity.HasKey(e => e.ApplicantRequisitionId);

                entity.ToTable("tbl_ApplicantRequisition_Staffing");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ExpectedCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NegotiatedCtc).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.RecruiterComment).HasMaxLength(1024);

                entity.Property(e => e.Status)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TentativeJoiningDate).HasColumnType("date");

                entity.Property(e => e.TentativeOnboardingDate).HasColumnType("date");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.TblApplicantRequisitionStaffing)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Staffing_tbl_Applicants");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblApplicantRequisitionStaffingCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Staffing_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblApplicantRequisitionStaffingModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Staffing_tbl_Employees1");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblApplicantRequisitionStaffing)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ApplicantRequisition_Staffing_tbl_Requisition");
            });

            modelBuilder.Entity<TblApplicants>(entity =>
            {
                entity.HasKey(e => e.ApplicantId);

                entity.ToTable("tbl_Applicants");

                entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");

                entity.Property(e => e.ApplicantActive)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentCtc)
                    .HasColumnName("CurrentCTC")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(64);

                entity.Property(e => e.EmployeeEmailId).HasMaxLength(128);

                entity.Property(e => e.ExpectedCtc)
                    .HasColumnName("ExpectedCTC")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Experience)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.JobType)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningTime)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LocationPreference)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.NoticePeriod)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PanNo).HasMaxLength(128);

                entity.Property(e => e.PassportNo).HasMaxLength(128);

                entity.Property(e => e.PhoneNumber).HasMaxLength(128);

                entity.Property(e => e.Qualification)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.RelevantExperience)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ShortlistedBy)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SkillsandProficiency)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCities>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK_cities");

                entity.ToTable("tbl_Cities");

                entity.Property(e => e.CityId).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("country_code")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasColumnName("state_code")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("state_id");
            });

            modelBuilder.Entity<TblClients>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.ToTable("tbl_Clients");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Addressline1)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Addressline2)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AnnualRevenue).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ContractEndDate).HasColumnType("datetime");

                entity.Property(e => e.ContractStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.JobBoardPassword)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.JobBoardUrl)
                    .HasColumnName("JobBoardURL")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.JobBoardUserId)
                    .HasColumnName("JobBoardUserID")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NdaendDate)
                    .HasColumnName("NDAEndDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.NdastartDate)
                    .HasColumnName("NDAStartDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfBusiness)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblClientsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_Clients_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblClientsModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_Clients_tbl_Employees1");
            });

            modelBuilder.Entity<TblClientsContactDetails>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__tbl_Clie__5C66259BF2C9B20E");

                entity.ToTable("tbl_ClientsContactDetails");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPersonDesignation)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPersonEmailId)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNo).HasMaxLength(128);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblClientsContactDetails)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_Clien__Clien__3587F3E0");
            });

            modelBuilder.Entity<TblCountries>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbl_Countries");

                entity.Property(e => e.Capital)
                    .HasColumnName("capital")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Emoji)
                    .HasColumnName("emoji")
                    .HasMaxLength(191)
                    .IsUnicode(false);

                entity.Property(e => e.EmojiU)
                    .HasColumnName("emojiU")
                    .HasMaxLength(191)
                    .IsUnicode(false);

                entity.Property(e => e.Flag)
                    .HasColumnName("flag")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.Iso2)
                    .HasColumnName("iso2")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Iso3)
                    .HasColumnName("iso3")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Native)
                    .HasColumnName("native")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phonecode)
                    .HasColumnName("phonecode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.WikiDataId)
                    .HasColumnName("wikiDataId")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDepartmentMaster>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PK_Admin.Department");

                entity.ToTable("tbl_DepartmentMaster");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDesignationMaster>(entity =>
            {
                entity.HasKey(e => e.DesignationId)
                    .HasName("PK_Admin.Designation");

                entity.ToTable("tbl_DesignationMaster");

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmployeePanelMapping>(entity =>
            {
                entity.HasKey(e => e.EmployeePanelMappingId)
                    .HasName("PK_Admin.EmployeePanelMapping");

                entity.ToTable("tbl_EmployeePanelMapping");

                entity.Property(e => e.EmployeePanelMappingId).HasColumnName("EmployeePanelMappingID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.PanelGroupId).HasColumnName("PanelGroupID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblEmployeePanelMapping)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_EmployeePanelMapping_tbl_Employees");

                entity.HasOne(d => d.PanelGroup)
                    .WithMany(p => p.TblEmployeePanelMapping)
                    .HasForeignKey(d => d.PanelGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_EmployeePanelMapping_tbl_PanelGroupMaster");
            });

            modelBuilder.Entity<TblEmployees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("tbl_Employees");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasMaxLength(128);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReportingManagerId).HasColumnName("ReportingManagerID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Employees_tbl_DepartmentMaster");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Employees_tbl_DesignationMaster");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Employees_tbl_RoleMaster");
            });

            modelBuilder.Entity<TblGmapMaster>(entity =>
            {
                entity.HasKey(e => e.GmapId);

                entity.ToTable("tbl_GMapMaster");

                entity.Property(e => e.GmapId).HasColumnName("GmapID");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Lattiude)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblInterviewEmployeeMapping>(entity =>
            {
                entity.HasKey(e => e.InterviewEmployeeMappingId);

                entity.ToTable("tbl_InterviewEmployeeMapping");

                entity.Property(e => e.InterviewEmployeeMappingId).HasColumnName("InterviewEmployeeMappingID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.InterviewId).HasColumnName("InterviewID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblInterviewEmployeeMapping)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_InterviewEmployeeMapping_tbl_Employees");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.TblInterviewEmployeeMapping)
                    .HasForeignKey(d => d.InterviewId)
                    .HasConstraintName("FK_tbl_InterviewEmployeeMapping_tbl_InterviewManagement");
            });

            modelBuilder.Entity<TblInterviewEmployeeMappingStaffing>(entity =>
            {
                entity.HasKey(e => e.InterviewEmployeeMappingId);

                entity.ToTable("tbl_InterviewEmployeeMappingStaffing");

                entity.Property(e => e.InterviewEmployeeMappingId).HasColumnName("InterviewEmployeeMappingID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.InterviewId).HasColumnName("InterviewID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblInterviewEmployeeMappingStaffing)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_InterviewEmployeeMappingStaffing_tbl_Employees");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.TblInterviewEmployeeMappingStaffing)
                    .HasForeignKey(d => d.InterviewId)
                    .HasConstraintName("FK_tbl_InterviewEmployeeMappingStaffing_tbl_InterviewManagement_Staffing");
            });

            modelBuilder.Entity<TblInterviewManagement>(entity =>
            {
                entity.HasKey(e => e.InterviewId);

                entity.ToTable("tbl_InterviewManagement");

                entity.Property(e => e.InterviewId).HasColumnName("InterviewID");

                entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");

                entity.Property(e => e.ApplicantRequisitionId).HasColumnName("ApplicantRequisitionID");

                entity.Property(e => e.Comments).HasMaxLength(1024);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailGuid).HasColumnName("EmailGUID");

                entity.Property(e => e.FeedBack).HasMaxLength(1024);

                entity.Property(e => e.InterviewDate).HasColumnType("datetime");

                entity.Property(e => e.InterviewPanel)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModeOfInterview)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecruitersFeedBack)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.RoundName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SkillRatings)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Venue).HasMaxLength(1024);

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.TblInterviewManagement)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_InterviewManagement_tbl_Applicants");

                entity.HasOne(d => d.ApplicantRequisition)
                    .WithMany(p => p.TblInterviewManagement)
                    .HasForeignKey(d => d.ApplicantRequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_InterviewManagement_tbl_ApplicantRequisition");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblInterviewManagementCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_InterviewManagement_tbl_Employees2");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblInterviewManagementModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_InterviewManagement_tbl_Employees1");
            });

            modelBuilder.Entity<TblInterviewManagementStaffing>(entity =>
            {
                entity.HasKey(e => e.InterviewId);

                entity.ToTable("tbl_InterviewManagement_Staffing");

                entity.Property(e => e.InterviewId).HasColumnName("InterviewID");

                entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");

                entity.Property(e => e.ApplicantRequisitionId).HasColumnName("ApplicantRequisitionID");

                entity.Property(e => e.Comments).HasMaxLength(1024);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailGuid).HasColumnName("EmailGUID");

                entity.Property(e => e.FeedBack).HasMaxLength(1024);

                entity.Property(e => e.InterviewDate).HasColumnType("datetime");

                entity.Property(e => e.InterviewPanel)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModeOfInterview)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecruitersFeedBack)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.RoundName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SkillRatings)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Venue).HasMaxLength(1024);

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.TblInterviewManagementStaffing)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_InterviewManagement_Staffing_tbl_Applicants");

                entity.HasOne(d => d.ApplicantRequisition)
                    .WithMany(p => p.TblInterviewManagementStaffing)
                    .HasForeignKey(d => d.ApplicantRequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_InterviewManagement_Staffing_tbl_ApplicantRequisition");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblInterviewManagementStaffingCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_InterviewManagement_Staffing_tbl_Employees2");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblInterviewManagementStaffingModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_InterviewManagement_Staffing_tbl_Employees1");
            });

            modelBuilder.Entity<TblJobPortal>(entity =>
            {
                entity.HasKey(e => e.PortalId)
                    .HasName("PK__tbl_JobP__B87D5813D128ACF5");

                entity.ToTable("tbl_JobPortal");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PortalName).HasMaxLength(128);
            });

            modelBuilder.Entity<TblJobTypeMaster>(entity =>
            {
                entity.HasKey(e => e.JobTypeId);

                entity.ToTable("tbl_JobTypeMaster");

                entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");

                entity.Property(e => e.JobType)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLocationMaster>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK_Location");

                entity.ToTable("tbl_LocationMaster");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("StateID");
            });

            modelBuilder.Entity<TblLogin>(entity =>
            {
                entity.HasKey(e => e.LoginId);

                entity.ToTable("tbl_Login");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblLogin)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Login_tbl_Employees");
            });

            modelBuilder.Entity<TblLoginClient>(entity =>
            {
                entity.HasKey(e => e.LoginClientId)
                    .HasName("PK__tbl_Logi__66D79B9435F335DA");

                entity.ToTable("tbl_LoginClient");

                entity.Property(e => e.LoginClientId).HasColumnName("LoginClientID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.TblLoginClient)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_LoginClient_tbl_ClientsContactDetails");
            });

            modelBuilder.Entity<TblNoticePeriodMaster>(entity =>
            {
                entity.HasKey(e => e.NoticePeriodId);

                entity.ToTable("tbl_NoticePeriodMaster");

                entity.Property(e => e.NoticePeriodId).HasColumnName("NoticePeriodID");

                entity.Property(e => e.NoticePeriod)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPanelGroupMaster>(entity =>
            {
                entity.HasKey(e => e.PanelGroupId)
                    .HasName("PK_Admin.PanelGroup");

                entity.ToTable("tbl_PanelGroupMaster");

                entity.Property(e => e.PanelGroupId).HasColumnName("PanelGroupID");

                entity.Property(e => e.PanelGroupName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblQualificationMaster>(entity =>
            {
                entity.HasKey(e => e.QualificationId);

                entity.ToTable("tbl_QualificationMaster");

                entity.Property(e => e.QualificationId).HasColumnName("QualificationID");

                entity.Property(e => e.Qualification)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRequisition>(entity =>
            {
                entity.HasKey(e => e.RequistionId);

                entity.ToTable("tbl_Requisition");

                entity.Property(e => e.RequistionId).HasColumnName("RequistionID");

                entity.Property(e => e.CancelComments)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Comments).HasMaxLength(512);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOwner)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.JoiningTenure).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Location)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerComments).HasMaxLength(512);

                entity.Property(e => e.ManagerEmployeeId).HasColumnName("ManagerEmployeeID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecruiterComment).HasMaxLength(512);

                entity.Property(e => e.RecruiterLeadId).HasColumnName("RecruiterLeadID");

                entity.Property(e => e.Status)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.YearsofExperience).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblRequisition)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_tbl_Requisition_tbl_Clients");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblRequisitionCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_Requisition_tbl_Employees1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblRequisition)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Requisition_tbl_DepartmentMaster");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.TblRequisition)
                    .HasForeignKey(d => d.DesignationId)
                    .HasConstraintName("FK_tbl_Requisition_tbl_DesignationMaster");

                entity.HasOne(d => d.ManagerEmployee)
                    .WithMany(p => p.TblRequisitionManagerEmployee)
                    .HasForeignKey(d => d.ManagerEmployeeId)
                    .HasConstraintName("FK_tbl_Requisition_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblRequisitionModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_Requisition_tbl_Employees2");

                entity.HasOne(d => d.RecruiterLead)
                    .WithMany(p => p.TblRequisitionRecruiterLead)
                    .HasForeignKey(d => d.RecruiterLeadId)
                    .HasConstraintName("FK_tbl_Requisition_tbl_Employees3");
            });

            modelBuilder.Entity<TblRequisitionClientContactMappingStaffing>(entity =>
            {
                entity.HasKey(e => e.RequisitionContactId)
                    .HasName("PK__tbl_Requ__4C16C81BACB1F376");

                entity.ToTable("tbl_RequisitionClientContactMapping_Staffing");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.TblRequisitionClientContactMappingStaffing)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionClientContactMapping_Staffing_tbl_ClientsContactDetails");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblRequisitionClientContactMappingStaffing)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionClientContactMapping_Staffing_tbl_Requisition_Staffing");
            });

            modelBuilder.Entity<TblRequisitionRecruiterMapping>(entity =>
            {
                entity.HasKey(e => e.RequisitionEmployeeAssigneeId);

                entity.ToTable("tbl_RequisitionRecruiterMapping");

                entity.Property(e => e.RequisitionEmployeeAssigneeId).HasColumnName("RequisitionEmployeeAssigneeID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblRequisitionRecruiterMapping)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionRecruiterMapping_tbl_Employees");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblRequisitionRecruiterMapping)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionRecruiterMapping_tbl_Requisition");
            });

            modelBuilder.Entity<TblRequisitionRecruiterMappingStaffing>(entity =>
            {
                entity.HasKey(e => e.RequisitionEmployeeAssigneeId);

                entity.ToTable("tbl_RequisitionRecruiterMapping_Staffing");

                entity.Property(e => e.RequisitionEmployeeAssigneeId).HasColumnName("RequisitionEmployeeAssigneeID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblRequisitionRecruiterMappingStaffing)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionRecruiterMapping_Staffing_tbl_Employees");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblRequisitionRecruiterMappingStaffing)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionRecruiterMapping_Staffing_tbl_Requisition_Staffing");
            });

            modelBuilder.Entity<TblRequisitionSkillMapping>(entity =>
            {
                entity.HasKey(e => e.RequisitionSkillId);

                entity.ToTable("tbl_RequisitionSkillMapping");

                entity.Property(e => e.RequisitionSkillId).HasColumnName("RequisitionSkillID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblRequisitionSkillMapping)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionSkillMapping_tbl_Requisition");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TblRequisitionSkillMapping)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_tbl_RequisitionSkillMapping_tbl_SkillMaster");
            });

            modelBuilder.Entity<TblRequisitionSkillMappingStaffing>(entity =>
            {
                entity.HasKey(e => e.RequisitionSkillId);

                entity.ToTable("tbl_RequisitionSkillMapping_Staffing");

                entity.Property(e => e.RequisitionSkillId).HasColumnName("RequisitionSkillID");

                entity.Property(e => e.RequisitionId).HasColumnName("RequisitionID");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.HasOne(d => d.Requisition)
                    .WithMany(p => p.TblRequisitionSkillMappingStaffing)
                    .HasForeignKey(d => d.RequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_RequisitionSkillMapping_Staffing_tbl_Requisition");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TblRequisitionSkillMappingStaffing)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_tbl_RequisitionSkillMapping_Staffing_tbl_SkillMaster");
            });

            modelBuilder.Entity<TblRequisitionStaffing>(entity =>
            {
                entity.HasKey(e => e.RequistionId);

                entity.ToTable("tbl_Requisition_Staffing");

                entity.Property(e => e.RequistionId).HasColumnName("RequistionID");

                entity.Property(e => e.Budget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CancelComments)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Comments).HasMaxLength(512);

                entity.Property(e => e.ContractPeriod).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOwner)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.JoiningTenure).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Location)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerComments).HasMaxLength(512);

                entity.Property(e => e.ManagerEmployeeId).HasColumnName("ManagerEmployeeID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecruiterComment).HasMaxLength(512);

                entity.Property(e => e.RecruiterLeadId).HasColumnName("RecruiterLeadID");

                entity.Property(e => e.Status)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Tier).HasMaxLength(128);

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.YearsofExperience).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TblRequisitionStaffing)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_Clients");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblRequisitionStaffingCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_Employees1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblRequisitionStaffing)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_DepartmentMaster");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.TblRequisitionStaffing)
                    .HasForeignKey(d => d.DesignationId)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_DesignationMaster");

                entity.HasOne(d => d.ManagerEmployee)
                    .WithMany(p => p.TblRequisitionStaffingManagerEmployee)
                    .HasForeignKey(d => d.ManagerEmployeeId)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblRequisitionStaffingModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_Employees2");

                entity.HasOne(d => d.RecruiterLead)
                    .WithMany(p => p.TblRequisitionStaffingRecruiterLead)
                    .HasForeignKey(d => d.RecruiterLeadId)
                    .HasConstraintName("FK_tbl_Requisition_Staffing_tbl_Employees3");
            });

            modelBuilder.Entity<TblRoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_Admin.Role");

                entity.ToTable("tbl_RoleMaster");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSalaryBreakupTemplates>(entity =>
            {
                entity.HasKey(e => e.SalarayTemplateId);

                entity.ToTable("tbl_SalaryBreakupTemplates");

                entity.Property(e => e.SalarayTemplateId).HasColumnName("SalarayTemplateID");

                entity.Property(e => e.TemplateName)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSelectedApplicants>(entity =>
            {
                entity.HasKey(e => e.SelectedApplicantsId)
                    .HasName("PK__tbl_Sele__A82BE5152A84FF59");

                entity.ToTable("tbl_SelectedApplicants");

                entity.Property(e => e.SelectedApplicantsId).HasColumnName("SelectedApplicantsID");

                entity.Property(e => e.ApplicantJoining)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.JoinedDate).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicantRequisition)
                    .WithMany(p => p.TblSelectedApplicants)
                    .HasForeignKey(d => d.ApplicantRequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_SelectedApplicants_tbl_ApplicantRequisition");
            });

            modelBuilder.Entity<TblSelectedApplicantsStaffing>(entity =>
            {
                entity.HasKey(e => e.SelectedApplicantsId)
                    .HasName("PK__tbl_Sele__A82BE515BB03E118");

                entity.ToTable("tbl_SelectedApplicants_Staffing");

                entity.Property(e => e.SelectedApplicantsId).HasColumnName("SelectedApplicantsID");

                entity.Property(e => e.ApplicantJoining)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientOnboardingDate).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsOnboarded)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JoinedDate).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicantRequisition)
                    .WithMany(p => p.TblSelectedApplicantsStaffing)
                    .HasForeignKey(d => d.ApplicantRequisitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_SelectedApplicants_Staffing_tbl_ApplicantRequisition_Staffing");
            });

            modelBuilder.Entity<TblSkillMaster>(entity =>
            {
                entity.HasKey(e => e.SkillId)
                    .HasName("PK_Admin.Skill");

                entity.ToTable("tbl_SkillMaster");

                entity.Property(e => e.SkillId).HasColumnName("SkillID");

                entity.Property(e => e.SkillName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblStates>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbl_States");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("country_code")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.FipsCode)
                    .HasColumnName("fips_code")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Flag)
                    .HasColumnName("flag")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.Iso2)
                    .HasColumnName("iso2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.WikiDataId)
                    .HasColumnName("wikiDataId")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblVendorContacts>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("tbl_VendorContacts");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPersonEmailId)
                    .HasColumnName("ContactPersonEmailID")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ContactphoneNo).HasMaxLength(128);

                entity.Property(e => e.Designation)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.TblVendorContacts)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_VendorContacts_tbl_Vendors");
            });

            modelBuilder.Entity<TblVendors>(entity =>
            {
                entity.HasKey(e => e.VendorId);

                entity.ToTable("tbl_Vendors");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ContractEndDate).HasColumnType("datetime");

                entity.Property(e => e.ContractStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.NdaendDate)
                    .HasColumnName("NDAEndDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.NdastartDate)
                    .HasColumnName("NDAStartDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TypeofBusiness)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.VendorName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblVendorsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tbl_Vendors_tbl_Employees");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblVendorsModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tbl_Vendors_tbl_Employees1");
            });

            modelBuilder.Entity<TblWorkFlowMaster>(entity =>
            {
                entity.HasKey(e => e.WorkFlowId);

                entity.ToTable("tbl_WorkFlowMaster");

                entity.Property(e => e.WorkFlowId).HasColumnName("WorkFlowID");

                entity.Property(e => e.WorkFlowName)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
