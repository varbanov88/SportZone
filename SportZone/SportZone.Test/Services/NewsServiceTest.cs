using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Data.Models;
using SportZone.Services.Newz.Implementations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportZone.Test.Services
{
    public class NewsSserviceTest
    {
        public NewsSserviceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncSouldReturnCorrectResultWithFilterAndOreder()
        {
            //Arange
            var db = this.PrepareDb();

            var newsOne = new News { Id = 1, Title = "first", PublishDate = DateTime.Now.AddMinutes(-5) };
            var newsTwo = new News { Id = 2, Title = "second", PublishDate = DateTime.Now.AddMinutes(-6) };
            var newsThree = new News { Id = 3, Title = "third", PublishDate = DateTime.Now.AddMinutes(-5), LastEditedDate = DateTime.Now.AddMinutes(-1) };

            db.AddRange(newsOne, newsTwo, newsThree);
            await db.SaveChangesAsync();

            var newsService = new NewsService(db);

            //Act
            var result = await newsService.AllAsync("t");

            //Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3 && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsyncShouldOneNews()
        {
            //Arange
            var db = this.PrepareDb();

            var newsOne = new News { Id = 1 };
            var newsTwo = new News { Id = 2 };
            var newsThree = new News { Id = 3 };

            db.AddRange(newsOne, newsTwo, newsThree);
            await db.SaveChangesAsync();
            var newsService = new NewsService(db);

            //Act
            var result = await newsService.GetByIdAsync(3);

            //Assert
            result.Id.Should().Be(3);
        }

        private SportZoneDbContext PrepareDb()
        {
            var dbOptions = new DbContextOptionsBuilder<SportZoneDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SportZoneDbContext(dbOptions);
        }
    }
}