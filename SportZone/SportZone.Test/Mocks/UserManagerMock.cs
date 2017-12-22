using Microsoft.AspNetCore.Identity;
using Moq;
using SportZone.Data.Models;

namespace SportZone.Test.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<User>> New
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}