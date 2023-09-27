using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Tests.Mocks {
    internal class MockIAuthRepository {
        public static Mock<IAuthRepository> GetMock() {
            var mock = new Mock<IAuthRepository>();
            // Set up
            mock.Setup(x => x.GetAuthModelAsync(It.IsAny<string>()))
                .ReturnsAsync((string login) => TestData.AuthModels.FirstOrDefault(x => x.Login == login));

            mock.Setup(x => x.AddAsync(It.IsAny<AuthModel>()))
                .Callback((AuthModel model) =>
                    TestData.AuthModels.Add(model)
                );
            mock.Setup(x => x.UpdateAsync(It.IsAny<AuthModel>()))
                .Callback(((AuthModel upModel) => {
                    var currModel = TestData.AuthModels.FirstOrDefault(x => x.Id == upModel.Id);
                    if (currModel == null)
                        throw new NullReferenceException("model not found");
                    currModel.Update(upModel.RefreshToken, upModel.RefreshTokenExpiryTime);
                }));
            mock.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .Callback((string login) => 
                {
                    TestData.AuthModels = TestData.AuthModels.Where(x => x.Login != login).ToList();
                });
            mock.Setup(x => x.DeleteAsync(It.IsAny<AuthModel>()))
                .Callback((AuthModel model) => {
                    TestData.AuthModels = TestData.AuthModels.Where(x => x.Login != model.Login).ToList();
                });

            return mock;
        }
    }
}
