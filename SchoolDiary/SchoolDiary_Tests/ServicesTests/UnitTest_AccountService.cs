using Microsoft.Extensions.Options;
using Moq;
using SchoolDiary.Domain.Services;
using SchoolDiary.Helpers;
using SchoolDiary.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SchoolDiary_Tests.ServicesTests
{
    [Collection("Database collection")]
    public class UnitTest_AccountService
    {
        private readonly TestsFixture _fixture;
        private readonly AccountService _accountService;
        public UnitTest_AccountService(TestsFixture fixture)
        {
            _fixture = fixture;
            var passwordHasherMock = new Mock<IPasswordHasher>();
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            _accountService = new AccountService(_fixture.db, passwordHasherMock.Object, appSettingsMock.Object);
        }
        [Fact]
        public async void Test_BaseRegister()
        {

        }
    }
}
