using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public partial class DriverLicenseLearningSupportContext : DbContext, IDriverLicenseLearningSupportContext
{
    public DriverLicenseLearningSupportContext()
    {
    }

    public DriverLicenseLearningSupportContext(DbContextOptions<DriverLicenseLearningSupportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CoursePackage> CoursePackages { get; set; }

    public virtual DbSet<CoursePackageReservation> CoursePackageReservations { get; set; }

    public virtual DbSet<Curriculum> Curricula { get; set; }

    public virtual DbSet<ExamGrade> ExamGrades { get; set; }

    public virtual DbSet<ExamHistory> ExamHistories { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<LicenseRegisterForm> LicenseRegisterForms { get; set; }

    public virtual DbSet<LicenseRegisterFormStatus> LicenseRegisterFormStatuses { get; set; }

    public virtual DbSet<LicenseType> LicenseTypes { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RollCallBook> RollCallBooks { get; set; }

    public virtual DbSet<SimulationSituation> SimulationSituations { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TeachingSchedule> TeachingSchedules { get; set; }

    public virtual DbSet<TheoryExam> TheoryExams { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    public virtual DbSet<WeekdaySchedule> WeekdaySchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return config["ConnectionStrings:DefaultDB"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__AB6E6165B472F400");

            entity.ToTable("Account");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_RoleId");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__CAA247C8CA97D3E0");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .HasMaxLength(255)
                .HasColumnName("address_id");
            entity.Property(e => e.City)
                .HasMaxLength(155)
                .HasColumnName("city");
            entity.Property(e => e.District)
                .HasMaxLength(155)
                .HasColumnName("district");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(20)
                .HasColumnName("zipcode");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__2975AA28146CE859");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.LastModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_date");
            entity.Property(e => e.StaffId)
                .HasMaxLength(200)
                .HasColumnName("staff_id");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Staff).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Blog_StaffId");

            entity.HasMany(d => d.Tags).WithMany(p => p.Blogs)
                .UsingEntity<Dictionary<string, object>>(
                    "BlogTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BlogTag_TagId"),
                    l => l.HasOne<Blog>().WithMany()
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BlogTag_BlogId"),
                    j =>
                    {
                        j.HasKey("BlogId", "TagId").HasName("PK__Blog_Tag__5D5CC0032C9E123B");
                        j.ToTable("Blog_Tag");
                        j.IndexerProperty<int>("BlogId").HasColumnName("blog_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E7957687F31EA180");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.AvatarImage)
                .HasMaxLength(100)
                .HasColumnName("avatar_image");
            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");

            entity.HasOne(d => d.Blog).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK_Comment_BlogId");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__8F1EF7AE7CE0EC09");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .HasMaxLength(200)
                .HasColumnName("course_id");
            entity.Property(e => e.CourseDesc).HasColumnName("course_desc");
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(255)
                .HasColumnName("course_title");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.TotalHoursRequired).HasColumnName("total_hours_required");
            entity.Property(e => e.TotalKmRequired).HasColumnName("total_km_required");
            entity.Property(e => e.TotalMonth).HasColumnName("total_month");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.Courses)
                .HasForeignKey(d => d.LicenseTypeId)
                .HasConstraintName("FK_Course_LicenseTypeId");

            entity.HasMany(d => d.Curricula).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseCurriculum",
                    r => r.HasOne<Curriculum>().WithMany()
                        .HasForeignKey("CurriculumId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseCurriculum_CurriculumnId"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseCurriculum_CourseId"),
                    j =>
                    {
                        j.HasKey("CourseId", "CurriculumId").HasName("PK__Course_C__FE6B74692C19CFC6");
                        j.ToTable("Course_Curriculum");
                        j.IndexerProperty<string>("CourseId")
                            .HasMaxLength(200)
                            .HasColumnName("course_id");
                        j.IndexerProperty<int>("CurriculumId").HasColumnName("curriculum_id");
                    });

            entity.HasMany(d => d.Mentors).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseMentor",
                    r => r.HasOne<Staff>().WithMany()
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseMentor_MentorId"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseMentor_CourseId"),
                    j =>
                    {
                        j.HasKey("CourseId", "MentorId").HasName("PK__Course_M__A143D041FEA62714");
                        j.ToTable("Course_Mentor");
                        j.IndexerProperty<string>("CourseId")
                            .HasMaxLength(200)
                            .HasColumnName("course_id");
                        j.IndexerProperty<string>("MentorId")
                            .HasMaxLength(200)
                            .HasColumnName("mentor_id");
                    });
        });

        modelBuilder.Entity<CoursePackage>(entity =>
        {
            entity.HasKey(e => e.CoursePackageId).HasName("PK__Course_P__64F76B381AFBF480");

            entity.ToTable("Course_Package");

            entity.Property(e => e.CoursePackageId)
                .HasMaxLength(200)
                .HasColumnName("course_package_id");
            entity.Property(e => e.AgeRequired).HasColumnName("age_required");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.CourseId)
                .HasMaxLength(200)
                .HasColumnName("course_id");
            entity.Property(e => e.CoursePackageDesc)
                .HasMaxLength(255)
                .HasColumnName("course_package_desc");
            entity.Property(e => e.SessionHour).HasColumnName("session_hour");
            entity.Property(e => e.TotalSession).HasColumnName("total_session");

            entity.HasOne(d => d.Course).WithMany(p => p.CoursePackages)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_CoursePackage_CourseId");
        });

        modelBuilder.Entity<CoursePackageReservation>(entity =>
        {
            entity.HasKey(e => e.CoursePackageReservationId).HasName("PK__Course_P__7DF5634750A1111B");

            entity.ToTable("Course_Package_Reservation");

            entity.Property(e => e.CoursePackageReservationId)
                .HasMaxLength(200)
                .HasColumnName("course_package_reservation_id");
            entity.Property(e => e.CoursePackageId)
                .HasMaxLength(200)
                .HasColumnName("course_package_id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.MemberId)
                .HasMaxLength(200)
                .HasColumnName("member_id");
            entity.Property(e => e.PaymentAmmount).HasColumnName("payment_ammount");
            entity.Property(e => e.PaymentTypeId).HasColumnName("payment_type_id");
            entity.Property(e => e.ReservationStatusId).HasColumnName("reservation_status_id");
            entity.Property(e => e.StaffId)
                .HasMaxLength(200)
                .HasColumnName("staff_id");

            entity.HasOne(d => d.CoursePackage).WithMany(p => p.CoursePackageReservations)
                .HasForeignKey(d => d.CoursePackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursePackageReservation_CoursePackageId");

            entity.HasOne(d => d.Member).WithMany(p => p.CoursePackageReservations)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursePackageReservation_MemberId");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.CoursePackageReservations)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursePackageReservation_PaymentTypeId");

            entity.HasOne(d => d.ReservationStatus).WithMany(p => p.CoursePackageReservations)
                .HasForeignKey(d => d.ReservationStatusId)
                .HasConstraintName("FK_CoursePackageReservation_StatusId");

            entity.HasOne(d => d.Staff).WithMany(p => p.CoursePackageReservations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursePackageReservation_StaffId");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.CurriculumId).HasName("PK__Curricul__17583C76045A6E74");

            entity.ToTable("Curriculum");

            entity.Property(e => e.CurriculumId).HasColumnName("curriculum_id");
            entity.Property(e => e.CurriculumDesc)
                .HasMaxLength(155)
                .HasColumnName("curriculum_desc");
            entity.Property(e => e.CurriculumDetail).HasColumnName("curriculum_detail");
        });

        modelBuilder.Entity<ExamGrade>(entity =>
        {
            entity.HasKey(e => e.ExamGradeId).HasName("PK__Exam_Gra__D98866AC1636F3F5");

            entity.ToTable("Exam_Grade");

            entity.Property(e => e.ExamGradeId).HasColumnName("exam_grade_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.MemberId)
                .HasMaxLength(200)
                .HasColumnName("member_id");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.SelectedAnswerId).HasColumnName("selected_answer_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.TheoryExamId).HasColumnName("theory_exam_id");

            entity.HasOne(d => d.Member).WithMany(p => p.ExamGrades)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_ExamGrade_MemberId");

            entity.HasOne(d => d.Question).WithMany(p => p.ExamGrades)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamGrade_QuestionId");

            entity.HasOne(d => d.TheoryExam).WithMany(p => p.ExamGrades)
                .HasForeignKey(d => d.TheoryExamId)
                .HasConstraintName("FK_ExamGrade_TheoryExamId");
        });

        modelBuilder.Entity<ExamHistory>(entity =>
        {
            entity.HasKey(e => e.ExamHistoryId).HasName("PK__Exam_His__DAC610E731015851");

            entity.ToTable("Exam_History");

            entity.Property(e => e.ExamHistoryId).HasColumnName("exam_history_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IsPassed).HasColumnName("is_passed");
            entity.Property(e => e.MemberId)
                .HasMaxLength(200)
                .HasColumnName("member_id");
            entity.Property(e => e.TheoryExamId).HasColumnName("theory_exam_id");
            entity.Property(e => e.TotalGrade).HasColumnName("total_grade");
            entity.Property(e => e.TotalQuestion).HasColumnName("total_question");
            entity.Property(e => e.TotalRightAnswer).HasColumnName("total_right_answer");
            entity.Property(e => e.TotalTime).HasColumnName("total_time");
            entity.Property(e => e.WrongParalysisQuestion).HasColumnName("wrong_paralysis_question");

            entity.HasOne(d => d.Member).WithMany(p => p.ExamHistories)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_ExamHistory_MemberId");

            entity.HasOne(d => d.TheoryExam).WithMany(p => p.ExamHistories)
                .HasForeignKey(d => d.TheoryExamId)
                .HasConstraintName("FK_History_TheoryExamId");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__FeedBack__7A6B2B8CE04EB09F");

            entity.ToTable("FeedBack");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.CourseId)
                .HasMaxLength(200)
                .HasColumnName("course_id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.MemberId)
                .HasMaxLength(200)
                .HasColumnName("member_id");
            entity.Property(e => e.RatingStar).HasColumnName("rating_star");
            entity.Property(e => e.StaffId)
                .HasMaxLength(200)
                .HasColumnName("staff_id");

            entity.HasOne(d => d.Course).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_FeedBack_CourseId");

            entity.HasOne(d => d.Member).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_FeedBack_MemberId");

            entity.HasOne(d => d.Staff).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_FeedBack_StaffId");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobTitleId).HasName("PK__Job_Titl__7872318F3317742A");

            entity.ToTable("Job_Title");

            entity.Property(e => e.JobTitleId).HasColumnName("job_title_id");
            entity.Property(e => e.JobTitleDesc)
                .HasMaxLength(155)
                .HasColumnName("job_title_desc");
        });

        modelBuilder.Entity<LicenseRegisterForm>(entity =>
        {
            entity.HasKey(e => e.LicenseFormId).HasName("PK__License___CBEF3B1410AC8B3C");

            entity.ToTable("License_Register_Form");

            entity.Property(e => e.LicenseFormId).HasColumnName("license_form_id");
            entity.Property(e => e.AvailableLicenseType)
                .HasMaxLength(20)
                .HasColumnName("available_license_type");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.HealthCertificationImage)
                .HasMaxLength(100)
                .HasColumnName("health_certification_image");
            entity.Property(e => e.IdentityCardImage)
                .HasMaxLength(100)
                .HasColumnName("identity_card_image");
            entity.Property(e => e.IdentityCardIssuedBy)
                .HasMaxLength(200)
                .HasColumnName("identity_card_issued_by");
            entity.Property(e => e.IdentityCardIssuedDate)
                .HasColumnType("datetime")
                .HasColumnName("identity_card_issued_date");
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(15)
                .HasColumnName("identity_number");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.LicenseFormDesc)
                .HasMaxLength(255)
                .HasColumnName("license_form_desc");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.LicenseTypeIssuedDate)
                .HasColumnType("datetime")
                .HasColumnName("license_type_issued_date");
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(200)
                .HasColumnName("permanent_address");
            entity.Property(e => e.RegisterFormStatusId).HasColumnName("register_form_status_id");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.LicenseRegisterForms)
                .HasForeignKey(d => d.LicenseTypeId)
                .HasConstraintName("FK_LicenseTypeId");

            entity.HasOne(d => d.RegisterFormStatus).WithMany(p => p.LicenseRegisterForms)
                .HasForeignKey(d => d.RegisterFormStatusId)
                .HasConstraintName("FK_LicenseRegisterFormId");
        });

        modelBuilder.Entity<LicenseRegisterFormStatus>(entity =>
        {
            entity.HasKey(e => e.RegisterFormStatusId).HasName("PK__License___BD2B9B64750B4243");

            entity.ToTable("License_Register_Form_Status");

            entity.Property(e => e.RegisterFormStatusId).HasColumnName("register_form_status_id");
            entity.Property(e => e.RegisterFormStatusDesc)
                .HasMaxLength(155)
                .HasColumnName("register_form_status_desc");
        });

        modelBuilder.Entity<LicenseType>(entity =>
        {
            entity.HasKey(e => e.LicenseTypeId).HasName("PK__License___8130CC24988D8EF0");

            entity.ToTable("License_Type");

            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.LicenseTypeDesc)
                .HasMaxLength(155)
                .HasColumnName("license_type_desc");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__B29B8534C64D3083");

            entity.ToTable("Member");

            entity.Property(e => e.MemberId)
                .HasMaxLength(200)
                .HasColumnName("member_id");
            entity.Property(e => e.AddressId)
                .HasMaxLength(255)
                .HasColumnName("address_id");
            entity.Property(e => e.AvatarImage)
                .HasMaxLength(100)
                .HasColumnName("avatar_image");
            entity.Property(e => e.DateBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(155)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(155)
                .HasColumnName("last_name");
            entity.Property(e => e.LicenseFormId).HasColumnName("license_form_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");

            entity.HasOne(d => d.Address).WithMany(p => p.Members)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Member_AddressId");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_Member_Email");

            entity.HasOne(d => d.LicenseForm).WithMany(p => p.Members)
                .HasForeignKey(d => d.LicenseFormId)
                .HasConstraintName("FK_Member_LicenseRegisterFormId");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("PK__Payment___8C1ABD6FA90653CA");

            entity.ToTable("Payment_Type");

            entity.Property(e => e.PaymentTypeId).HasColumnName("payment_type_id");
            entity.Property(e => e.PaymentTypeDesc)
                .HasMaxLength(155)
                .HasColumnName("payment_type_desc");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549A0DFC484");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsParalysis).HasColumnName("is_Paralysis");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.QuestionAnswerDesc).HasColumnName("question_answer_desc");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LicenseTypeId)
                .HasConstraintName("FK_Question_LicenseTypeId");

            entity.HasMany(d => d.TheoryExams).WithMany(p => p.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "ExamQuestion",
                    r => r.HasOne<TheoryExam>().WithMany()
                        .HasForeignKey("TheoryExamId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExamQuestion_TheoryExamId"),
                    l => l.HasOne<Question>().WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExamQuestion_QuestionId"),
                    j =>
                    {
                        j.HasKey("QuestionId", "TheoryExamId").HasName("PK__Exam_Que__ECD57A9A4EC7A09D");
                        j.ToTable("Exam_Question");
                        j.IndexerProperty<int>("QuestionId").HasColumnName("question_id");
                        j.IndexerProperty<int>("TheoryExamId").HasColumnName("theory_exam_id");
                    });
        });

        modelBuilder.Entity<QuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.QuestionAnswerId).HasName("PK__Question__BDF60D1FD283ACB8");

            entity.ToTable("Question_Answer");

            entity.Property(e => e.QuestionAnswerId).HasColumnName("question_answer_id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.IsTrue).HasColumnName("is_true");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionAnswer_QuestionId");
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.HasKey(e => e.ReservationStatusId).HasName("PK__Reservat__4C8E982307DD6AE8");

            entity.ToTable("Reservation_Status");

            entity.Property(e => e.ReservationStatusId).HasColumnName("reservation_status_id");
            entity.Property(e => e.ReservationStatusDesc)
                .HasMaxLength(50)
                .HasColumnName("reservation_status_desc");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC10B3C7F0");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(155)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RollCallBook>(entity =>
        {
            entity.HasKey(e => e.RollCallBookId).HasName("PK__Roll_Cal__A0382E6A69A73C77");

            entity.ToTable("Roll_Call_Book");

            entity.Property(e => e.RollCallBookId).HasColumnName("roll_call_book_id");
            entity.Property(e => e.CancelMessage)
                .HasMaxLength(200)
                .HasColumnName("cancel_message");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasColumnName("comment");
            entity.Property(e => e.IsAbsence).HasColumnName("isAbsence");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.MemberId)
                .HasMaxLength(200)
                .HasColumnName("member_id");
            entity.Property(e => e.MemberTotalSession).HasColumnName("member_total_session");
            entity.Property(e => e.TeachingScheduleId).HasColumnName("teaching_schedule_id");
            entity.Property(e => e.TotalHoursDriven).HasColumnName("total_hours_driven");
            entity.Property(e => e.TotalKmDriven).HasColumnName("total_km_driven");

            entity.HasOne(d => d.Member).WithMany(p => p.RollCallBooks)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RollCallBook_MemberId");

            entity.HasOne(d => d.TeachingSchedule).WithMany(p => p.RollCallBooks)
                .HasForeignKey(d => d.TeachingScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RollCallBook_TeachingScheduleId");
        });

        modelBuilder.Entity<SimulationSituation>(entity =>
        {
            entity.HasKey(e => e.SimulationId).HasName("PK__Simulati__FBA6E531A688CAEC");

            entity.ToTable("Simulation_Situation");

            entity.Property(e => e.SimulationId).HasColumnName("simulation_id");
            entity.Property(e => e.ImageResult)
                .HasMaxLength(200)
                .HasColumnName("image_result");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.SimulationVideo)
                .HasMaxLength(200)
                .HasColumnName("simulation_video");
            entity.Property(e => e.TimeResult).HasColumnName("time_result");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.SimulationSituations)
                .HasForeignKey(d => d.LicenseTypeId)
                .HasConstraintName("FK_SimulationSituation_LicenseTypeId");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__971A01BBA7FE4BDE");

            entity.ToTable("Slot");

            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.SlotDesc)
                .HasMaxLength(155)
                .HasColumnName("slot_desc");
            entity.Property(e => e.SlotName)
                .HasMaxLength(100)
                .HasColumnName("slot_name");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9C9C86AEAC");

            entity.Property(e => e.StaffId)
                .HasMaxLength(200)
                .HasColumnName("staff_id");
            entity.Property(e => e.AddressId)
                .HasMaxLength(255)
                .HasColumnName("address_id");
            entity.Property(e => e.AvatarImage)
                .HasMaxLength(100)
                .HasColumnName("avatar_image");
            entity.Property(e => e.DateBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(155)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.JobTitleId).HasColumnName("job_title_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(155)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.SelfDescription).HasColumnName("self_description");

            entity.HasOne(d => d.Address).WithMany(p => p.Staff)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Staff_AddressId");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_Staff_Email");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.Staff)
                .HasForeignKey(d => d.JobTitleId)
                .HasConstraintName("FK_Staff_JobTitleId");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__4296A2B6DD290537");

            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.TagName)
                .HasMaxLength(155)
                .HasColumnName("tag_name");
        });

        modelBuilder.Entity<TeachingSchedule>(entity =>
        {
            entity.HasKey(e => e.TeachingScheduleId).HasName("PK__Teaching__6BFF5CD9E5F69FB8");

            entity.ToTable("Teaching_Schedule");

            entity.Property(e => e.TeachingScheduleId).HasColumnName("teaching_schedule_id");
            entity.Property(e => e.CancelMessage)
                .HasMaxLength(200)
                .HasColumnName("cancel_message");
            entity.Property(e => e.CoursePackageId)
                .HasMaxLength(200)
                .HasColumnName("course_package_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsCancel).HasColumnName("is_cancel");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.StaffId)
                .HasMaxLength(200)
                .HasColumnName("staff_id");
            entity.Property(e => e.TeachingDate)
                .HasColumnType("datetime")
                .HasColumnName("teaching_date");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.WeekdayScheduleId).HasColumnName("weekday_schedule_id");

            entity.HasOne(d => d.CoursePackage).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.CoursePackageId)
                .HasConstraintName("FK_TeachingSchedule_CoursePackageId");

            entity.HasOne(d => d.Slot).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.SlotId)
                .HasConstraintName("FK_TeachingSchedule_SlotId");

            entity.HasOne(d => d.Staff).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_TeachingSchedule_StaffId");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_TeachingSchedule_VehicleId");

            entity.HasOne(d => d.WeekdaySchedule).WithMany(p => p.TeachingSchedules)
                .HasForeignKey(d => d.WeekdayScheduleId)
                .HasConstraintName("FK_TeachingSchedule_WeekdayScheduleId");
        });

        modelBuilder.Entity<TheoryExam>(entity =>
        {
            entity.HasKey(e => e.TheoryExamId).HasName("PK__Theory_E__2176FD3D161384D6");

            entity.ToTable("Theory_Exam");

            entity.Property(e => e.TheoryExamId).HasColumnName("theory_exam_id");
            entity.Property(e => e.IsMockExam).HasColumnName("is_mock_exam");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.TotalAnswerRequired).HasColumnName("total_answer_required");
            entity.Property(e => e.TotalQuestion).HasColumnName("total_question");
            entity.Property(e => e.TotalTime).HasColumnName("total_time");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.TheoryExams)
                .HasForeignKey(d => d.LicenseTypeId)
                .HasConstraintName("FK_PracticeExam_LicenseTypeId");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__F2947BC1477EC6FF");

            entity.ToTable("Vehicle");

            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RegisterDate)
                .HasColumnType("datetime")
                .HasColumnName("register_date");
            entity.Property(e => e.VehicleImage)
                .HasMaxLength(155)
                .HasColumnName("vehicle_image");
            entity.Property(e => e.VehicleLicensePlate)
                .HasMaxLength(155)
                .HasColumnName("vehicle_license_plate");
            entity.Property(e => e.VehicleName)
                .HasMaxLength(155)
                .HasColumnName("vehicle_name");
            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleTypeId)
                .HasConstraintName("FK_Vehicle_TypeId");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.VehicleTypeId).HasName("PK__Vehicle___2A007218C9F70977");

            entity.ToTable("Vehicle_Type");

            entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.VehicleTypeDesc)
                .HasMaxLength(155)
                .HasColumnName("vehicle_type_desc");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.VehicleTypes)
                .HasForeignKey(d => d.LicenseTypeId)
                .HasConstraintName("FK_VehicleType_LicenseTypeId");
        });

        modelBuilder.Entity<WeekdaySchedule>(entity =>
        {
            entity.HasKey(e => e.WeekdayScheduleId).HasName("PK__Weekday___287B8C741C31F2E4");

            entity.ToTable("Weekday_Schedule");

            entity.Property(e => e.WeekdayScheduleId).HasColumnName("weekday_schedule_id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(200)
                .HasColumnName("course_id");
            entity.Property(e => e.Friday)
                .HasColumnType("datetime")
                .HasColumnName("friday");
            entity.Property(e => e.Monday)
                .HasColumnType("datetime")
                .HasColumnName("monday");
            entity.Property(e => e.Saturday)
                .HasColumnType("datetime")
                .HasColumnName("saturday");
            entity.Property(e => e.Sunday)
                .HasColumnType("datetime")
                .HasColumnName("sunday");
            entity.Property(e => e.Thursday)
                .HasColumnType("datetime")
                .HasColumnName("thursday");
            entity.Property(e => e.Tuesday)
                .HasColumnType("datetime")
                .HasColumnName("tuesday");
            entity.Property(e => e.Wednesday)
                .HasColumnType("datetime")
                .HasColumnName("wednesday");
            entity.Property(e => e.WeekdayScheduleDesc)
                .HasMaxLength(200)
                .HasColumnName("weekday_schedule_desc");

            entity.HasOne(d => d.Course).WithMany(p => p.WeekdaySchedules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_WeekdaySchedule_CourseId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
