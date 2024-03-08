using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.Domain.Services;
using SmartCharging.API.DTOs;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Tests.Domain.Services.Tests
{
    public class ConnectorServicesServicesTests
    {
        private Guid _dummySmartGroupId1;
        private Guid _dummySmartGroupId2;
        private Guid _dummyChargeStationId1;
        private Guid _dummyChargeStationId2;

        private IRepository _repo;
        private IChargeStationRepository _chargeStationRepository;
        private ISmartGroupsRepository _smartGroupRepo;

        private ConnectorServices _connectorServices;

        [SetUp]
        public void Setup()
        {
            _dummySmartGroupId1 = Guid.NewGuid();
            _dummyChargeStationId1 = Guid.NewGuid();

            _repo = Substitute.For<IRepository>();
            _chargeStationRepository = Substitute.For<IChargeStationRepository>();
            _smartGroupRepo = Substitute.For<ISmartGroupsRepository>();

            _connectorServices = new ConnectorServices(_repo, _smartGroupRepo, _chargeStationRepository);
        }

        [Test]
        public async Task Create_OneConnector_SuccessResults()
        {
            var dummyConnector = GetDummyConnector();
            var dummyChargeStation = GetDummyChargeStationWithConnectors();

            _repo.ExistAsync(Arg.Any<Expression<Func<ChargeStation, bool>>>()).Returns(true);

            _chargeStationRepository.GetChargeStationByIdWithConnectors(Arg.Any<Guid>()).Returns(dummyChargeStation);
            _repo.GetById<SmartGroup>(Arg.Any<Guid>()).Returns(GetDummyGroup());

            var response = await _connectorServices.CreateAsync(_dummyChargeStationId1, new ConnectorDTO() { MaxCurrentInAmps = dummyConnector.MaxCurrentInAmps });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(true));
                Assert.That(response.Data.ChargeStationId, Is.EqualTo(_dummyChargeStationId1));
                Assert.That(response.Data.MaxCurrentInAmps, Is.EqualTo(dummyChargeStation.Connectors[0].MaxCurrentInAmps));
            });
        }

        [Test]
        public async Task Create_MoreThanFiveConnectors_FailResults()
        {
            var dummyConnector = GetDummyConnector();
            var dummyChargeStation = GetDummyChargeStationWithConnectors();

            _repo.ExistAsync(Arg.Any<Expression<Func<ChargeStation, bool>>>()).Returns(true);

            dummyChargeStation.Connectors.Add(dummyConnector);
            dummyChargeStation.Connectors.Add(dummyConnector);
            dummyChargeStation.Connectors.Add(dummyConnector);
            dummyChargeStation.Connectors.Add(dummyConnector);
            dummyChargeStation.Connectors.Add(dummyConnector);

            _chargeStationRepository.GetChargeStationByIdWithConnectors(Arg.Any<Guid>()).Returns(dummyChargeStation);
            _repo.GetById<SmartGroup>(Arg.Any<Guid>()).Returns(GetDummyGroup());

            var response = await _connectorServices.CreateAsync(_dummyChargeStationId1, new ConnectorDTO() { MaxCurrentInAmps = dummyConnector.MaxCurrentInAmps });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(false));
                Assert.That(response.Message, Is.EqualTo("Cannot create more than 5 connectors to a charge station."));
            });
        }

        [Test]
        public async Task Create_MaxCurrentInAmpsIsZero_FailResults()
        {
            var response = await _connectorServices.CreateAsync(_dummyChargeStationId1, new ConnectorDTO() { MaxCurrentInAmps = 0 });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(false));
                Assert.That(response.Message, Is.EqualTo("Cannot create a conector because max current in amps must be greater than zero"));
            });
        }

        [Test]
        public async Task Create_OneConnectorMaxCurrentInAmpsExceedsGroupAmps_FailResults()
        {
            var dummyConnector = GetDummyConnector();
            dummyConnector.MaxCurrentInAmps = 100;
            var dummyChargeStation = GetDummyChargeStationWithConnectors();
            dummyChargeStation.Connectors[0].MaxCurrentInAmps = 100;

            _repo.ExistAsync(Arg.Any<Expression<Func<ChargeStation, bool>>>()).Returns(true);
            _chargeStationRepository.GetChargeStationByIdWithConnectors(Arg.Any<Guid>()).Returns(dummyChargeStation);
            _repo.GetById<SmartGroup>(Arg.Any<Guid>()).Returns(GetDummyGroup());

            var response = await _connectorServices.CreateAsync(_dummyChargeStationId1, new ConnectorDTO() { MaxCurrentInAmps = dummyConnector.MaxCurrentInAmps });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(false));
                Assert.That(response.Message, Is.EqualTo("Cannot create more connectors because max current" +
                    " in amps is greater than Capacity in Amps of the belonging group."));
            });
        }

        [Test]
        public async Task Create_ChargeStationDoesntExist_FailResults()
        {
            var dummyConnector = GetDummyConnector();

            _repo.ExistAsync(Arg.Any<Expression<Func<ChargeStation, bool>>>()).Returns(false);

            var response = await _connectorServices.CreateAsync(_dummyChargeStationId1, new ConnectorDTO() { MaxCurrentInAmps = dummyConnector.MaxCurrentInAmps });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(false));
                Assert.That(response.Message, Is.EqualTo("Cannot create a conector because charge station doesn't exist."));
            });
        }

        private Connector GetDummyConnector()
        {
            return new Connector()
            {
                ChargeStationId = _dummyChargeStationId1,
                MaxCurrentInAmps = 10
            };
        }

        private ChargeStation GetDummyChargeStationWithConnectors()
        {
            return new ChargeStation()
            {
                ChargeStationId = _dummyChargeStationId1,
                SmartGroupId = _dummySmartGroupId1,
                Name = "JRR Tolkien",
                Connectors = new List<Connector>()
                {
                    GetDummyConnector()
                },
            };
        }
        private SmartGroup GetDummyGroup()
        {
            return new SmartGroup()
            {
                SmartGroupId = _dummySmartGroupId1,
                Name = "JRR Tolkien",
                CapacityInAmps = 10,
            };
        }
    }
}
