using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_VotersView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW VotersView AS
                SELECT 
                VoterId
                ,RegistryYear
                ,FirstName
                ,LastName
                ,MiddleName
                ,MaidenName
                ,(RTRIM(IsNull(LastName,'')) + ' ' + RTRIM(IsNull(FirstName,'')) + ' ' + RTRIM(IsNull(MiddleName,''))) As FullName
                ,Gender
                ,DateOfBirth
                ,IsEligible
                ,Email
                ,ContactPhone
                ,DriverLicense
                ,DATEDIFF(year, DateOfBirth, GETDATE()) -
                CASE
                    WHEN (
                        MONTH(GETDATE()) > MONTH(DateOfBirth)
                        ) THEN 1
                    WHEN (
                        MONTH(GETDATE()) = MONTH(DateOfBirth) AND
                        DAY(GETDATE()) > DAY(DateOfBirth)
                        ) THEN 1
                    ELSE 0
                END AS Age
				,AgeGroupDescription as AgeGroup
                ,Assessments.AssessmentNo
                ,Address1
                ,HouseNo
                ,Address2
                ,PostalCode
                ,Assessments.ConstituencyNo
                ,ConstituencyName
                ,Assessments.ParishNo
                ,ParishName
                ,IsBogus As IsBogusAssessment
                ,HouseNo + ' ' + LEFT(Address2,CASE WHEN CHARINDEX('(',Address2)<1 THEN LEN(Address2) ELSE CHARINDEX('(',Address2)-2  END) + ', ' + RTRIM(LTRIM(REPLACE(ParishName,'PARISH',''))) AS [Address]
                FROM 
                Voters 
                LEFT JOIN Assessments ON Assessments.AssessmentNo = Voters.AssessmentNo
				LEFT JOIN AgeGroups ON 
				DATEDIFF(year, DateOfBirth, GETDATE()) -
                CASE
                    WHEN (
                        MONTH(GETDATE()) > MONTH(DateOfBirth)
                        ) THEN 1
                    WHEN (
                        MONTH(GETDATE()) = MONTH(DateOfBirth) AND
                        DAY(GETDATE()) > DAY(DateOfBirth)
                        ) THEN 1
                    ELSE 0
                END
				BETWEEN AgeGroups.AgeGroupFrom AND AgeGroups.AgeGroupTo 
                LEFT JOIN Constituencies ON Assessments.ConstituencyNo = Constituencies.ConstituencyNo
                LEFT JOIN Parishes ON Parishes.ParishNo = Assessments.ParishNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW VotersView");
        }
    }
}
