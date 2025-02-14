using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using PROWeb.WebService.Authentication;
using PROWeb.WebService.Authentication.Models;
using PROWeb.WebService.Contracts.Requests;
using PROWeb.WebService.Contracts.Responses;
using PROWeb.WebService.Services;
using PROWeb.WebService.Configurations;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using Serilog;
using PROWeb.WebService.Authentication.Extensions;
using PROWeb.WebService.Repositories;
using PROWeb.WebService.Models.Enums;

namespace PROWeb.WebService.Endpoints
{
    public static class ProWebSiteEndpoints
    {
        public static void MapProWebSiteEndpoints(this WebApplication app)
        {
            app.MapPost($"/{Configuration.Routs.Login}", Login).WithName("LoginUser");

            app.MapGet($"/{Configuration.Routs.HealthCheck}", HealthCheck).WithName("HealthCheck");

            //app.MapGet($"/{Configuration.Routs.VoterResponseSchema}", GenerateVoterResponseSchema).WithName("GetVoterResponseSchema");

            //app.MapGet($"/{Configuration.Routs.ConstituenciesResponseSchema}", GenerateConstituenciesResponseSchema).WithName("GetConstituenciesResponseSchema");

            //app.MapGet($"/{Configuration.Routs.AssessmentsResponseSchema}", GenerateAssessmentsResponseSchema).WithName("GetAssessmentsResponseSchema");

            //app.MapGet($"/{Configuration.Routs.LoginUserRequestSchema}", GenerateLoginUserRequestSchema).WithName("GenerateLoginUserRequestSchema");

            //app.MapGet($"/{Configuration.Routs.LoginUserResponseSchema}", GenerateLoginUserResponseSchema).WithName("GetLoginUserResponseSchema");

            //app.MapGet($"/{Configuration.Routs.RatepayerRequestSchema}", GetRatepayerResponseSchema).WithName("GetRatepayerResponseSchema");

            //app.MapGet($"/{Configuration.Routs.JPVoterRequestSchema}", GenerateJPVoterResponseSchema).WithName("GenerateJPVoterResponseSchema");

            var websiteGroup = app.MapGroup(string.Empty)
                .RequireAuthorization(p => p.RequireRole(ServiceUserRoles.PROWebsite.ToString()));

            websiteGroup.MapGet($"/{Configuration.Routs.Constituencies}", GetConstituenciesAsync)
                .WithName("GetConstituencies");

            websiteGroup.MapGet($"/{Configuration.Routs.Assessments}", GetAssessmentsAsync)
                .WithName("GetAssessments");

            websiteGroup.MapGet($"/{Configuration.Routs.Ratepayers}", GetRatepayersAsync)
                .WithName("GetRatepayers");

            websiteGroup.MapGet($"/{Configuration.Routs.JPVoters}", GetJPVotersAsync)
               .WithName("GetJPVoters");

            websiteGroup.MapGet($"/{Configuration.Routs.Voter}", GetVoterAsync)
               .WithName("GetVoter");
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static  IResult Login([FromBody]LoginRequest request, IJwtTokenGenerator tokenGenerator, IUsersService service)
        {
            var user = service.FindByUserName(request.UserName);

            if(user is null ||
              !user.Password!.Equals(request.Password))
            {
                string message = $"Invalid credentials for user {request.UserName}!";

                Log.Warning(message);

                return TypedResults.BadRequest("Invalid credentials.");
            }

            var strToken = tokenGenerator.GenerateToken(user);
            var token = strToken.DeserializeToken();

            return TypedResults.Ok(
                new LoginResponse
                {
                    Token = strToken,
                    Expires = long.Parse(token.Claims.First(c => c.Type == "exp").Value)
                });
        }

        [ProducesResponseType(typeof(List<ConstituencyResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static async Task<IResult> GetConstituenciesAsync(IProService service)
        {
            var result = await service.GetConstituenciesAsync();

            if(result.Count == 0)
            {
                string message = $"Requested constituencies were not found!";

                Log.Warning(message);
                return TypedResults.BadRequest(message);
            }

            Log.Information("Constituencies successfully retrieved!");

            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AssessmentResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static async Task<IResult> GetAssessmentsAsync(
            [FromQuery] string? assessmentNo,
            [FromQuery] string? houseName,
            [FromQuery] string? street,
            [FromQuery] string? houseNo,
            [FromQuery] string? parish,
            [FromQuery] string? postalCode,
            [FromQuery] int? constituencyNo,
            [FromQuery] string? constituencyName,
            IProService service)
        {
            var result = await service.GetAssessmentsAsync(
                assessmentNo,
                houseName,
                street,
                houseNo,
                parish,
                postalCode,
                constituencyNo,
                constituencyName);


            if (result.Count == 0)
            {
                string message = $"Request assessments were not found!";

                Log.Warning(message);
                return TypedResults.BadRequest(message);
            }

            Log.Information($"Request assessments were successfully retrieved!");

            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VoterResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static async Task<IResult> GetVoterAsync(
            [FromQuery, BindRequired] string firstName,
            [FromQuery, BindRequired] string lastName,
            [FromQuery, BindRequired] DateTime dateOfBirth,
            IProService service)
        {
            var dobYear = DateTime.Now.Year - 130;

            if(dateOfBirth.Year < dobYear)
            {
                string message = $"Voter with first name: {firstName}, last name: {lastName} and DOB: {dateOfBirth.ToString("dd/MM/yyyy")} has DOB outside of acceptable range!";

                return TypedResults.BadRequest(message);
            }

            var result = await service.GetVoterAsync(
                firstName, 
                lastName, 
                dateOfBirth);

            if(result is null)
            {
                string message = $"Voter with first name: {firstName}, last name: {lastName} and dob: {dateOfBirth.ToString("dd/MM/yyyy")}  was not found!";

                Log.Warning(message);
                return TypedResults.BadRequest(message);
            }

            Log.Information($"Voter with first name: {firstName}, last name: {lastName} and dob: {dateOfBirth.ToString("dd/MM/yyyy")} was successfully retrieved!");

            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RatepayerResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static async Task<IResult> GetRatepayersAsync(
            [FromQuery, BindRequired] int corporationId,
            [FromQuery, BindRequired] string assessmentNo,
            IProService service)
        {
            var result = await service.GetRatepayerAsync(
                corporationId,
                assessmentNo);

            if (result.Count == 0)
            {
                string message = $"Request ratepayers were not found!";

                Log.Warning(message);
                return TypedResults.BadRequest(message);
            }

            Log.Information($"Request ratepayers were successfully retrieved!");

            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<JPVoterResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static async Task<IResult> GetJPVotersAsync(
            [FromQuery, BindRequired] int constituencyNo,
            IProService service)
        {
            var result = await service.GetJPVotersAsync(constituencyNo);

            if (result.Count == 0)
            {
                string message = $"JP voters were not found!";

                Log.Warning(message);
                return TypedResults.BadRequest(message);
            }

            Log.Information($"Request JP voters were successfully retrieved!");

            return TypedResults.Ok(result);
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateConstituenciesResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(List<ConstituencyResponse>));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public static IResult GenerateVoterResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(VoterResponse));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateAssessmentsResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(List<AssessmentResponse>));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateLoginUserResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(LoginResponse));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateLoginUserRequestSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(LoginRequest));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GetRatepayerResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(RatepayerResponse));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateJPVoterResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(JPVoterResponse));
            return ToJsonResult(schema.ToString());
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public static IResult HealthCheck()
        {
            return TypedResults.Ok("OK!");
        }

        private static IResult ToJsonResult(string result)
        {
            return TypedResults.Text(result, "application/json");
        }

        private static JSchemaGenerator GetGenerator()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            generator.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return generator;
        }
    }
}
