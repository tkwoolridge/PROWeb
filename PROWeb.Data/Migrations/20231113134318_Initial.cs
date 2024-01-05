using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentFlags",
                columns: table => new
                {
                    AssessmentFlagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlagDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Short = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentFlags", x => x.AssessmentFlagId);
                });

            migrationBuilder.CreateTable(
                name: "Births",
                columns: table => new
                {
                    BirthId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Births", x => x.BirthId);
                });

            migrationBuilder.CreateTable(
                name: "Constituencies",
                columns: table => new
                {
                    ConstituencyNo = table.Column<int>(type: "int", nullable: false),
                    BogusNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConstituencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constituencies", x => x.ConstituencyNo);
                    table.UniqueConstraint("AK_Constituencies_BogusNo", x => x.BogusNo);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "DriverLicenses",
                columns: table => new
                {
                    DriverLicenseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenseType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AssessmentNo = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLicenses", x => x.DriverLicenseId);
                });

            migrationBuilder.CreateTable(
                name: "FormTypes",
                columns: table => new
                {
                    FormTypeId = table.Column<int>(type: "int", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTypes", x => x.FormTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Immigrations",
                columns: table => new
                {
                    ImmigrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImmigrationRecordId = table.Column<int>(type: "int", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatusDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AuditChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditAddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusAcquired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeceased = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immigrations", x => x.ImmigrationId);
                });

            migrationBuilder.CreateTable(
                name: "OldPROUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CanUseAdministration = table.Column<bool>(type: "bit", nullable: false),
                    CanChangeFormStatus = table.Column<bool>(type: "bit", nullable: false),
                    CanSearchRegistrySnapshots = table.Column<bool>(type: "bit", nullable: false),
                    CanRunReports = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdateVoterRegistration = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldPROUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parishes",
                columns: table => new
                {
                    ParishNo = table.Column<int>(type: "int", nullable: false),
                    ParishName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishes", x => x.ParishNo);
                });

            migrationBuilder.CreateTable(
                name: "PROOffices",
                columns: table => new
                {
                    PROOfficeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address4 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PO1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PO2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PO3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearEndMonth = table.Column<int>(type: "int", nullable: false),
                    YearEndDay = table.Column<int>(type: "int", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ElectionYear = table.Column<int>(type: "int", nullable: false),
                    NextElectionsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextAdvancedPollDate = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROOffices", x => x.PROOfficeId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationOrigins",
                columns: table => new
                {
                    RegistrationOriginId = table.Column<int>(type: "int", nullable: false),
                    OriginDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationOrigins", x => x.RegistrationOriginId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationStatuses",
                columns: table => new
                {
                    RegistrationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationStatuses", x => x.RegistrationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "VoterFlags",
                columns: table => new
                {
                    FlagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlagDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Short = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Ineligible = table.Column<bool>(type: "bit", nullable: false),
                    GraydOutInReports = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterFlags", x => x.FlagId);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    AssessmentNo = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HouseNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ConstituencyNo = table.Column<int>(type: "int", nullable: false),
                    ParishNo = table.Column<int>(type: "int", nullable: false),
                    IsBogus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.AssessmentNo);
                    table.ForeignKey(
                        name: "FK_Assessments_Constituencies_ConstituencyNo",
                        column: x => x.ConstituencyNo,
                        principalTable: "Constituencies",
                        principalColumn: "ConstituencyNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessments_Parishes_ParishNo",
                        column: x => x.ParishNo,
                        principalTable: "Parishes",
                        principalColumn: "ParishNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: true),
                    BirthId = table.Column<int>(type: "int", nullable: true),
                    ImmigrationId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Initial = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NewLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaidenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessmentNo = table.Column<int>(type: "int", nullable: false),
                    OldAssessmentNo = table.Column<int>(type: "int", nullable: true),
                    IsBogusNo = table.Column<bool>(type: "bit", nullable: false),
                    BogusNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OldBogusNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneHome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneWork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneMobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverLicense = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    WasBornIn = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CommonwealthCitizen = table.Column<bool>(type: "bit", nullable: false),
                    BermudianStatusGranted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredAsElector = table.Column<bool>(type: "bit", nullable: false),
                    IsBermudianStatusGranted = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationStatusId = table.Column<int>(type: "int", nullable: false),
                    FormTypeId = table.Column<int>(type: "int", nullable: false),
                    RegistrationOriginId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Assessments_AssessmentNo",
                        column: x => x.AssessmentNo,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Assessments_OldAssessmentNo",
                        column: x => x.OldAssessmentNo,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentNo");
                    table.ForeignKey(
                        name: "FK_Registrations_Constituencies_BogusNo",
                        column: x => x.BogusNo,
                        principalTable: "Constituencies",
                        principalColumn: "BogusNo");
                    table.ForeignKey(
                        name: "FK_Registrations_Constituencies_OldBogusNo",
                        column: x => x.OldBogusNo,
                        principalTable: "Constituencies",
                        principalColumn: "BogusNo");
                    table.ForeignKey(
                        name: "FK_Registrations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    VoterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    BirthID = table.Column<int>(type: "int", nullable: true),
                    ImmigrationID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Initial = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaidenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: false),
                    AssessmentNo = table.Column<int>(type: "int", nullable: false),
                    IsEligible = table.Column<bool>(type: "bit", nullable: false),
                    IsBogusNo = table.Column<bool>(type: "bit", nullable: false),
                    BogusNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneHome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneWork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneMobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverLicense = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    WasBornIn = table.Column<bool>(type: "bit", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CommonwealthCitizen = table.Column<bool>(type: "bit", nullable: true),
                    BermudianStatusGranted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredAsElector = table.Column<bool>(type: "bit", nullable: true),
                    IsBermudianStatusGranted = table.Column<bool>(type: "bit", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => new { x.VoterId, x.RegistryYear });
                    table.ForeignKey(
                        name: "FK_Voters_Assessments_AssessmentNo",
                        column: x => x.AssessmentNo,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voters_Constituencies_BogusNo",
                        column: x => x.BogusNo,
                        principalTable: "Constituencies",
                        principalColumn: "BogusNo");
                    table.ForeignKey(
                        name: "FK_Voters_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DocumentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExportFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Voters_PersonId_RegistryYear",
                        columns: x => new { x.PersonId, x.RegistryYear },
                        principalTable: "Voters",
                        principalColumns: new[] { "VoterId", "RegistryYear" });
                });

            migrationBuilder.CreateTable(
                name: "VoterHistories",
                columns: table => new
                {
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterHistories", x => new { x.VoterId, x.RegistryYear, x.Created });
                    table.ForeignKey(
                        name: "FK_VoterHistories_Voters_VoterId_RegistryYear",
                        columns: x => new { x.VoterId, x.RegistryYear },
                        principalTable: "Voters",
                        principalColumns: new[] { "VoterId", "RegistryYear" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoterVoterFlag",
                columns: table => new
                {
                    FlagId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    RegistryYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterVoterFlag", x => new { x.FlagId, x.VoterId, x.RegistryYear });
                    table.ForeignKey(
                        name: "FK_VoterVoterFlag_VoterFlags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "VoterFlags",
                        principalColumn: "FlagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoterVoterFlag_Voters_VoterId_RegistryYear",
                        columns: x => new { x.VoterId, x.RegistryYear },
                        principalTable: "Voters",
                        principalColumns: new[] { "VoterId", "RegistryYear" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoterHistoryFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterHistoryFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoterHistoryFields_VoterHistories_VoterId_RegistryYear_Created",
                        columns: x => new { x.VoterId, x.RegistryYear, x.Created },
                        principalTable: "VoterHistories",
                        principalColumns: new[] { "VoterId", "RegistryYear", "Created" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ConstituencyNo",
                table: "Assessments",
                column: "ConstituencyNo");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ParishNo",
                table: "Assessments",
                column: "ParishNo");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId_RegistryYear",
                table: "Documents",
                columns: new[] { "PersonId", "RegistryYear" });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_AssessmentNo",
                table: "Registrations",
                column: "AssessmentNo");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_BogusNo",
                table: "Registrations",
                column: "BogusNo");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_CountryId",
                table: "Registrations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_OldAssessmentNo",
                table: "Registrations",
                column: "OldAssessmentNo");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_OldBogusNo",
                table: "Registrations",
                column: "OldBogusNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoterHistoryFields_VoterId_RegistryYear_Created",
                table: "VoterHistoryFields",
                columns: new[] { "VoterId", "RegistryYear", "Created" });

            migrationBuilder.CreateIndex(
                name: "IX_Voters_AssessmentNo",
                table: "Voters",
                column: "AssessmentNo");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_BogusNo",
                table: "Voters",
                column: "BogusNo");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_CountryId",
                table: "Voters",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_VoterVoterFlag_VoterId_RegistryYear",
                table: "VoterVoterFlag",
                columns: new[] { "VoterId", "RegistryYear" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AssessmentFlags");

            migrationBuilder.DropTable(
                name: "Births");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "DriverLicenses");

            migrationBuilder.DropTable(
                name: "FormTypes");

            migrationBuilder.DropTable(
                name: "Immigrations");

            migrationBuilder.DropTable(
                name: "OldPROUsers");

            migrationBuilder.DropTable(
                name: "PROOffices");

            migrationBuilder.DropTable(
                name: "RegistrationOrigins");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "RegistrationStatuses");

            migrationBuilder.DropTable(
                name: "VoterHistoryFields");

            migrationBuilder.DropTable(
                name: "VoterVoterFlag");

            migrationBuilder.DropTable(
                name: "VoterHistories");

            migrationBuilder.DropTable(
                name: "VoterFlags");

            migrationBuilder.DropTable(
                name: "Voters");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Constituencies");

            migrationBuilder.DropTable(
                name: "Parishes");
        }
    }
}
