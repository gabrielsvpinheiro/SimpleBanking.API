using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using SimpleBanking.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace SimpleBanking.API.Tests.Controllers
{
    public class BalanceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BalanceControllerTests(WebApplicationFactory<Program> factory) 
        {
            _client = factory.CreateClient();
        }
    }
}
