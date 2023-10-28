using System;
using AutoMapper;
using Moq;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Core.Services;

namespace PartyRoom.Test.Services
{
	public class RoomServiceTest
	{
        private Mock<IRoomRepository> roomRepositoryMock;
        private Mock<IUserRoomRepository> userRoomRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IProfileRepository> profileRepositoryMock;
        private Mock<IMapper> mapperMock;

        public RoomServiceTest()
        {
             roomRepositoryMock = new Mock<IRoomRepository>();
             userRoomRepositoryMock = new Mock<IUserRoomRepository>();
             userRepositoryMock = new Mock<IUserRepository>();
             profileRepositoryMock = new Mock<IProfileRepository>();
            mapperMock = new Mock<IMapper>();
        }


        [Fact]
        public async Task ConnectToRoomAsync_ThrowsException_WhenLinkDoesNotExist()
        {
            // Arrange
            var roomService = new RoomService(
                mapperMock.Object,
                roomRepositoryMock.Object,
                userRoomRepositoryMock.Object,
                userRepositoryMock.Object,
                profileRepositoryMock.Object
            );

            var userId = Guid.NewGuid();
            var invalidLink = "nonexistentLink";

            roomRepositoryMock.Setup(repo => repo.ExistsLinkAsync(invalidLink)).ReturnsAsync(false);

         
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomService.ConnectToRoomAsync(userId, invalidLink);
            });
        }


        [Fact]
        public async Task ConnectToRoomAsync_WhenLinkExist_StartedFalse()
        {
            // Arrange
            var roomService = new RoomService(
                mapperMock.Object,
                roomRepositoryMock.Object,
                userRoomRepositoryMock.Object,
                userRepositoryMock.Object,
                profileRepositoryMock.Object
                );
            var userId = Guid.NewGuid();
            var roomId = Guid.NewGuid();
            var link = "123";

            roomRepositoryMock.Setup(repo => repo.ExistsLinkAsync(link)).ReturnsAsync(true);

            var room = new Room
            {
                AuthorId = Guid.NewGuid(),
                Id = roomId,
                Link = link,
                Name = "TestRoom",
                IsStarted = false,
                StartDate = DateTime.Today,
                FinishDate = DateTime.Today.AddDays(1)
            };

            roomRepositoryMock.Setup(repo => repo.GetByLinkAsync(link)).ReturnsAsync(room);
            userRepositoryMock.Setup(repo => repo.ExistsAsync(userId)).ReturnsAsync(true);
            userRoomRepositoryMock.Setup(repo => repo.ConsistsUserAsync(userId, roomId)).ReturnsAsync(false);

            await roomService.ConnectToRoomAsync(userId, link);

            roomRepositoryMock.Verify(repo => repo.GetByLinkAsync(link), Times.Once);
            userRoomRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<UserRoom>()), Times.Once);
            userRoomRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ConnectToRoomAsync_WhenLinkExist_StartedTrue()
        {
            // Arrange
            var roomService = new RoomService(
                mapperMock.Object,
                roomRepositoryMock.Object,
                userRoomRepositoryMock.Object,
                userRepositoryMock.Object,
                profileRepositoryMock.Object
                );
            var userId = Guid.NewGuid();
            var roomId = Guid.NewGuid();
            var link = "123";

            roomRepositoryMock.Setup(repo => repo.ExistsLinkAsync(link)).ReturnsAsync(true);

            var room = new Room
            {
                AuthorId = Guid.NewGuid(),
                Id = roomId,
                Link = link,
                Name = "TestRoom",
                IsStarted = true,
                StartDate = DateTime.Today,
                FinishDate = DateTime.Today.AddDays(1)
            };

            roomRepositoryMock.Setup(repo => repo.GetByLinkAsync(link)).ReturnsAsync(room);
            userRepositoryMock.Setup(repo => repo.ExistsAsync(userId)).ReturnsAsync(true);
            userRoomRepositoryMock.Setup(repo => repo.ConsistsUserAsync(userId, roomId)).ReturnsAsync(false);
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await roomService.ConnectToRoomAsync(userId, link);
            });


        }
    }
}
  


