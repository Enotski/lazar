using Lazar.Domain.Core.Models.Administration;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Tests.Mocks {
    internal class MockITokenRepository {
        public static Mock<ITokenRepository> GetMock() {
            var mock = new Mock<ITokenRepository>();
            // Set up
            mock.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns((IEnumerable<Claim> claims, string issuer, string audience, string key, DateTime expires) => "token");

            mock.Setup(x => x.GenerateRefreshToken())
                .Returns(() => "RefreshToken");
            return mock;
        }
    }
}
