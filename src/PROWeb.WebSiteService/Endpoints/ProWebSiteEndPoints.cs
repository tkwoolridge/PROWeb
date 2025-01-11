using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using PROWeb.WebSiteService.Authentication;
using PROWeb.WebSiteService.Authentication.Models;
using PROWeb.WebSiteService.Contracts.Requests;
using PROWeb.WebSiteService.Contracts.Responses;
using PROWeb.WebSiteService.Services;
using PROWeb.WebSiteService.Configurations;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace PROWeb.WebSiteService.Endpoints
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

            //app.MapGet($"/{Configuration.Routs.LoginUserResponseSchema}", GenerateLoginUserResponseSchema).WithName("GetLoginUserResponseSchema");

            //app.MapGet($"/{Configuration.Routs.LoginUserRequestSchema}", GenerateLoginUserRequestSchema).WithName("GetLoginUserRequestSchema");

            var authGroup = app.MapGroup(string.Empty)
                .RequireAuthorization();

            authGroup.MapGet($"/{Configuration.Routs.Constituencies}", GetConstituenciesAsync)
                .WithName("GetConstituencies");

            authGroup.MapGet($"/{Configuration.Routs.Assessments}", GetAssessmentsAsync)
                .WithName("GetAssessments");

            authGroup.MapGet($"/{Configuration.Routs.Voter}", GetVoterAsync)
               .WithName("GetVoter");
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public static  IResult Login([FromBody]LoginRequest request, IJwtTokenGenerator tokenGenerator, IOptions<JwtSettings> options)
        {
            if(!options.Value.UserName.Equals(request.UserName) ||
               !options.Value.Password.Equals(request.Password))
            {
                return TypedResults.BadRequest("Invalid credentials.");
            }

            return TypedResults.Ok(
                new LoginResponse
                {
                    Token = tokenGenerator.GenerateToken()
                });
        }

        [ProducesResponseType(typeof(List<ConstituencyResponse>), 200)]
        public static async Task<IResult> GetConstituenciesAsync(IProService service)
        {
            var result = await service.GetConstituenciesAsync();

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


            if (result?.Count == 0)
            {
                return TypedResults.BadRequest("Assessment not found.");
            }

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
            var result = await service.GetVoterAsync(
                firstName, 
                lastName, 
                dateOfBirth);

            if(result is null)
            {
                return TypedResults.BadRequest("Voter not found.");
            }

            return TypedResults.Ok(result);
        }

        [ExcludeFromDescription]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateConstituenciesResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(List<ConstituencyResponse>));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public static IResult GenerateVoterResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(VoterResponse));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateAssessmentsResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(List<AssessmentResponse>));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateLoginUserResponseSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(LoginResponse));
            return ToJsonResult(schema.ToString());
        }

        [ExcludeFromDescription]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK, contentType: "application/json")]
        public static IResult GenerateLoginUserRequestSchema()
        {
            JSchema schema = GetGenerator().Generate(typeof(LoginRequest));
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
