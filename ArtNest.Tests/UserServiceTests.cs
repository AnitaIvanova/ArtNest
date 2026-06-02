using ARTNEST.BLL;
using ArtNest.Tests.Mocks;
using Xunit;

namespace ArtNest.Tests
{
    public class UserServiceTests
    {
        private UserService CreateService()
        {
            var mock = new MockUserRepository();
            return new UserService(mock);
        }

        [Fact]
        public void RegisterUser_WithValidData_ReturnsTrue()
        {
            var service = CreateService();

            bool result = service.RegisterUser("Anita", "anita@test.com", "password123");

            Assert.True(result);
        }

        [Fact]
        public void RegisterUser_WithBlankFields_ReturnsFalse()
        {
            var service = CreateService();

            Assert.False(service.RegisterUser("", "anita@test.com", "password123"));
            Assert.False(service.RegisterUser("Anita", "", "password123"));
            Assert.False(service.RegisterUser("Anita", "anita@test.com", ""));
        }

        [Fact]
        public void RegisterUser_WithDuplicateEmail_ReturnsFalse()
        {
            var service = CreateService();
            service.RegisterUser("Anita", "anita@test.com", "password123");

          
            bool result = service.RegisterUser("Someone", "anita@test.com", "different");

            Assert.False(result);
        }

        [Fact]
        public void LoginUser_WithCorrectPassword_ReturnsUser()
        {
            var service = CreateService();
            service.RegisterUser("Anita", "anita@test.com", "password123");

            var user = service.LoginUser("anita@test.com", "password123");

            Assert.NotNull(user);
            Assert.Equal("anita@test.com", user!.Email);
        }

        [Fact]
        public void LoginUser_WithWrongPassword_ReturnsNull()
        {
            var service = CreateService();
            service.RegisterUser("Anita", "anita@test.com", "password123");

            var user = service.LoginUser("anita@test.com", "wrongpassword");

            Assert.Null(user);
        }

        [Fact]
        public void LoginUser_WithUnknownEmail_ReturnsNull()
        {
            var service = CreateService();

            var user = service.LoginUser("nobody@test.com", "password123");

            Assert.Null(user);
        }

        [Fact]
        public void UpdateUserSettings_WithValidData_ReturnsTrue()
        {
            var service = CreateService();
            service.RegisterUser("Anita", "anita@test.com", "password123");
            var user = service.LoginUser("anita@test.com", "password123");

            bool result = service.UpdateUserSettings(user!.Id, "Anita Updated", "anita@test.com", null);

            Assert.True(result);
        }

        [Fact]
        public void UpdateUserSettings_WithEmailTakenByAnotherUser_ReturnsFalse()
        {
            var service = CreateService();
            service.RegisterUser("Anita", "anita@test.com", "password123");
            service.RegisterUser("Bob", "bob@test.com", "password456");

            var bob = service.LoginUser("bob@test.com", "password456");

            
            bool result = service.UpdateUserSettings(bob!.Id, "Bob", "anita@test.com", null);

            Assert.False(result);
        }

        [Fact]
        public void UpdateUserSettings_ChangedPassword_AllowsLoginWithNewPassword()
        {
            var service = CreateService();
            service.RegisterUser("Anita", "anita@test.com", "oldpassword");
            var user = service.LoginUser("anita@test.com", "oldpassword");

            service.UpdateUserSettings(user!.Id, "Anita", "anita@test.com", "newpassword");

      
            Assert.Null(service.LoginUser("anita@test.com", "oldpassword"));
            Assert.NotNull(service.LoginUser("anita@test.com", "newpassword"));
        }
    }
}