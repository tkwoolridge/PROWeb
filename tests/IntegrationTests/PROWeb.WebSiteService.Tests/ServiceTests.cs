using PROWeb.WebSiteService.Contracts.Requests;
using PROWeb.WebSiteService.Tests.Configurations;
using System.Net.Http.Json;
using PROWeb.WebSiteService.Configurations;
using PROWeb.WebSiteService.Contracts.Responses;
using FluentAssertions;

namespace PROWeb.WebSiteService.Tests
{
    public class ServiceTests : IClassFixture<ServiceTestFactory>
    {
        private readonly ServiceTestFactory _factory;

        public ServiceTests(ServiceTestFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Service_OnCorrectCredentials_LogInWillSucceed()
        {
            //Arrange
            var client = CreateClient();

            //Action
            var response = await client.PostAsJsonAsync($"/{Configuration.Routs.Login}", new LoginRequest
            {
                UserName = "CloudBurst",
                Password = "Test"
            });

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            string? accessToken = loginResponse?.Token;

            //Assert
            accessToken.Should().NotBeNullOrEmpty();  
        }

        [Fact]
        public async Task Service_OnRequestConstituencies_Constituencies_Are_Returned()
        {
            //Arrange
            var client = await CreateAuthenticatedClientAsync();

            //Action
            var response = await client.GetFromJsonAsync<List<ConstituencyResponse>>($"/{Configuration.Routs.Constituencies}");

            //Assert
            response.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task Service_OnRequestAssessments_Assessments_Are_Returned()
        {
            //Arrange
            var client = await CreateAuthenticatedClientAsync();

            //Action
            var response = await client.GetFromJsonAsync<List<AssessmentResponse>>($"/{Configuration.Routs.Assessments}?street=Tribe Road");

            //Assert
            response.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task Service_OnVoterRequest_VoterIsReturned()
        {
            //Arrange
            var client = await CreateAuthenticatedClientAsync();

            //Action
            var response = await client.GetFromJsonAsync<VoterResponse>($"/{Configuration.Routs.Voter}?registryYear=2023&&firstName=Tenia&lastName=Woolridge&DateOfBirth=03/21/1980");

            //Assert
            response.Should().NotBeNull();
        }

        private async Task AddAuthenticationHeadersAsync(HttpClient client)
        {
            var response = await client.PostAsJsonAsync(Configuration.Routs.Login, new LoginRequest
            {
                UserName = "CloudBurst",
                Password = "Test"
            });

            var loginResponse  = await response.Content.ReadFromJsonAsync<LoginResponse>();
            string? accessToken = loginResponse?.Token;


            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        }

        private HttpClient CreateClient()
        {
            return _factory.CreateClient();
        }

        private async Task<HttpClient> CreateAuthenticatedClientAsync()
        {
            var client = CreateClient();
            await AddAuthenticationHeadersAsync(client);

            return client;
        }
    }
}
