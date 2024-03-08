using NSubstitute;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.Domain.Services;
using SmartCharging.API.DTOs;

namespace Tests.Domain.Services.Tests
{
    public class SmartGroupServicesTests
    {
        private Guid _dummySmartGroupId;

        [SetUp]
        public void Setup()
        {
           _dummySmartGroupId = Guid.NewGuid();
        }

        [Test]
        public async Task Create_SmartGroupWithCapacityInArmsGreaterThanZero_SuccessResults()
        {
            var dummySmartGroup = GetDummySmartGroup();
            var _repository = Substitute.For<IRepository>();
            var _smartGroupsRepository = Substitute.For<ISmartGroupsRepository>();

            _repository.Create(Arg.Any<SmartGroup>()).Returns(dummySmartGroup);
            var service = new SmartGroupServices(_repository, _smartGroupsRepository);

            var response = await service.Create(new SmartGroupDTO() { Name = "Test", CapacityInAmps = 10 });
            
            
            Assert.Multiple(() =>
            {
                Assert.That(response.Message, Is.EqualTo("Group has been created"));
                Assert.That(response.IsOk, Is.EqualTo(true));
                Assert.That(response.Data.Name, Is.EqualTo(dummySmartGroup.Name));
                Assert.That(response.Data.CapacityInAmps, Is.EqualTo(dummySmartGroup.CapacityInAmps));
                Assert.That(response.Data.SmartGroupId, Is.EqualTo(dummySmartGroup.SmartGroupId));
            });
        }

        [Test]
        public async Task Create_SmartGroupWithZeroCapacityInArmsGreater_FailResults()
        {
            var dummySmartGroup = GetDummySmartGroup();

            var _repository = Substitute.For<IRepository>();
            var _smartGroupsRepository = Substitute.For<ISmartGroupsRepository>();

            _repository.Create(Arg.Any<SmartGroup>()).Returns(dummySmartGroup);
            var service = new SmartGroupServices(_repository, _smartGroupsRepository);

            var response = await service.Create(new SmartGroupDTO() { Name = "Test", CapacityInAmps = 0 });


            Assert.Multiple(() =>
            {
                Assert.That(response.Message, Is.EqualTo("Unable to create group. Capacity in Amps must be greater than zero."));
                Assert.That(response.IsOk, Is.EqualTo(false));
            });
        }

        [Test]
        public async Task Update_SmartGroupWithZeroCapacityInArmsGreater_SuccessResults()
        {
            var dummySmartGroup = GetDummySmartGroup();

            var _repository = Substitute.For<IRepository>();
            var _smartGroupsRepository = Substitute.For<ISmartGroupsRepository>();

            _repository.GetById<SmartGroup>(Arg.Any<Guid>()).Returns(dummySmartGroup);
            var service = new SmartGroupServices(_repository, _smartGroupsRepository);

            var newSmartGroup = new SmartGroupDTO() { Name = "Robert C. Martin", CapacityInAmps = 5 };

            var response = await service.Update(dummySmartGroup.SmartGroupId, newSmartGroup);


            Assert.Multiple(() =>
            {
                Assert.That(response.Message, Is.EqualTo("Group has been updated"));
                Assert.That(response.IsOk, Is.EqualTo(true));
                Assert.That(response.Data.Name, Is.EqualTo(newSmartGroup.Name));
                Assert.That(response.Data.CapacityInAmps, Is.EqualTo(newSmartGroup.CapacityInAmps));
            });
        }

        [Test]
        public async Task Update_SmartGroupWithZeroCapacityInArmsGreater_FailResults()
        {
            var dummySmartGroup = GetDummySmartGroup();

            var _repository = Substitute.For<IRepository>();
            var _smartGroupsRepository = Substitute.For<ISmartGroupsRepository>();

            _repository.GetById<SmartGroup>(Arg.Any<Guid>()).Returns(dummySmartGroup);
            var service = new SmartGroupServices(_repository, _smartGroupsRepository);

            var newSmartGroup = new SmartGroupDTO() { Name = "Anakin Skywalker Loves Children", CapacityInAmps = 0 };

            var response = await service.Update(dummySmartGroup.SmartGroupId, newSmartGroup);


            Assert.Multiple(() =>
            {
                Assert.That(response.Message, Is.EqualTo("Unable to update group. Capacity in Amps must be greater than zero."));
                Assert.That(response.IsOk, Is.EqualTo(false));
            });
        }

        [Test]
        public async Task Delete_SmartGroupWithZeroCapacityInArmsGreater_SuccessResults()
        {
            var dummySmartGroup = GetDummySmartGroup();

            var _repository = Substitute.For<IRepository>();
            var _smartGroupsRepository = Substitute.For<ISmartGroupsRepository>();

            var service = new SmartGroupServices(_repository, _smartGroupsRepository);

            var response = await service.Delete(dummySmartGroup.SmartGroupId);


            Assert.Multiple(() =>
            {
                Assert.That(response.Message, Is.EqualTo("Group has been deleted."));
                Assert.That(response.IsOk, Is.EqualTo(true));
            });
        }

        private SmartGroup GetDummySmartGroup()
        {
            return new SmartGroup()
            {
                SmartGroupId = _dummySmartGroupId,
                Name = "Luke Skywalker",
                CapacityInAmps = 10,
            };
        }
    }
}
