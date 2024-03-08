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
    public class ChargeStationServicesTests
    {
        private Guid _dummySmartGroupId1;
        private Guid _dummyChargeStationId1;

        private IChargeStationRepository _chargeStationRepo;
        private IConnectorServices _connectorServices;
        private IRepository _repo;

        private ChargeStationServices _chargeStationServices;

        [SetUp]
        public void Setup()
        {
            _dummySmartGroupId1 = Guid.NewGuid();
            _dummyChargeStationId1 = Guid.NewGuid();

            _repo = Substitute.For<IRepository>();
            _chargeStationRepo = Substitute.For<IChargeStationRepository>();
            _connectorServices = Substitute.For<IConnectorServices>();

            _chargeStationServices = new ChargeStationServices(_repo, _connectorServices, _chargeStationRepo);
        }

        [Test]
        public async Task Create_ChargeStationWithOneConnector_SuccessResults()
        {
            var dummyChargeStation = GetDummyChargeStation();

            _repo.ExistAsync(Arg.Any<Expression<Func<SmartGroup, bool>>>()).Returns(true);
            _repo.Create(Arg.Any<ChargeStation>()).Returns(dummyChargeStation);

            var response = await _chargeStationServices.Create(dummyChargeStation.SmartGroupId, new ChargeStationDTO()
            {
                Name = dummyChargeStation.Name,
                Connectors = dummyChargeStation.Connectors
            });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(true));
                Assert.That(response.Data.Name, Is.EqualTo(dummyChargeStation.Name));
                Assert.That(response.Data.ChargeStationId, Is.EqualTo(dummyChargeStation.ChargeStationId));
                Assert.That(response.Data.SmartGroupId, Is.EqualTo(dummyChargeStation.SmartGroupId));
                Assert.That(response.Data.Connectors[0].ConnectorId, Is.EqualTo(dummyChargeStation.Connectors[0].ConnectorId));
                Assert.That(response.Data.Connectors[0].ChargeStationId, Is.EqualTo(dummyChargeStation.Connectors[0].ChargeStationId));
                Assert.That(response.Data.Connectors[0].MaxCurrentInAmps, Is.EqualTo(dummyChargeStation.Connectors[0].MaxCurrentInAmps));
            });
        }

        [Test]
        public async Task Create_ChargeStationWithMoreThanFiveConnector_FailResults()
        {
            var dummyChargeStation = GetDummyChargeStation();
            dummyChargeStation.Connectors.AddRange(
                    new List<Connector>()
                    {
                        new Connector()
                        {
                            ConnectorId = 1,
                            MaxCurrentInAmps = 1,
                        },
                        new Connector()
                        {
                            ConnectorId = 1,
                            MaxCurrentInAmps = 1,
                        },
                        new Connector()
                        {
                            ConnectorId = 1,
                            MaxCurrentInAmps = 1,
                        },
                        new Connector()
                        {
                            ConnectorId = 1,
                            MaxCurrentInAmps = 1,
                        },
                        new Connector()
                        {
                            ConnectorId = 1,
                            MaxCurrentInAmps = 1,
                        },
                        new Connector()
                        {
                            ConnectorId = 1,
                            MaxCurrentInAmps = 1,
                        },
                    }
                );

            _repo.ExistAsync(Arg.Any<Expression<Func<SmartGroup, bool>>>()).Returns(true);
            _repo.Create(Arg.Any<ChargeStation>()).Returns(dummyChargeStation);

            var response = await _chargeStationServices.Create(dummyChargeStation.SmartGroupId, new ChargeStationDTO()
            {
                Name = dummyChargeStation.Name,
                Connectors = dummyChargeStation.Connectors
            });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(false));
                Assert.That(response.Message, Is.EqualTo("Cannot create a charge station. Number of Connectors should be less than 5"));
            });
        }

        [Test]
        public async Task Create_SmartGroupIdDoesntExist_FailResults()
        {
            var dummyChargeStation = GetDummyChargeStation();

            _repo.ExistAsync(Arg.Any<Expression<Func<SmartGroup, bool>>>()).Returns(false);

            var response = await _chargeStationServices.Create(dummyChargeStation.SmartGroupId, new ChargeStationDTO()
            {
                Name = dummyChargeStation.Name,
                Connectors = dummyChargeStation.Connectors
            });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(false));
                Assert.That(response.Message, Is.EqualTo("Cannot create a charge station because group doesn't exist."));
            });
        }

        [Test]
        public async Task Update_ChargeStationName_SuccessResults()
        {
            var dummyChargeStation = GetDummyChargeStation();

            _repo.ExistAsync(Arg.Any<Expression<Func<SmartGroup, bool>>>()).Returns(true);
            _repo.GetById<ChargeStation>(Arg.Any<Guid>()).Returns(dummyChargeStation);

            dummyChargeStation.Name = "Ken Follett";

            var response = await _chargeStationServices.Update(dummyChargeStation.ChargeStationId, new ChargeStationDTO()
            {
                Name = dummyChargeStation.Name,
            });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(true));
                Assert.That(response.Data.Name, Is.EqualTo(dummyChargeStation.Name));
            });
        }

        [Test]
        public async Task Delete_ChargeStationName_SuccessResults()
        {
            var dummyChargeStation = GetDummyChargeStation();

            _repo.ExistAsync(Arg.Any<Expression<Func<SmartGroup, bool>>>()).Returns(true);
            _repo.GetById<ChargeStation>(Arg.Any<Guid>()).Returns(dummyChargeStation);

            dummyChargeStation.Name = "Ken Follett";

            var response = await _chargeStationServices.Update(dummyChargeStation.ChargeStationId, new ChargeStationDTO()
            {
                Name = dummyChargeStation.Name,
            });


            Assert.Multiple(() =>
            {
                Assert.That(response.IsOk, Is.EqualTo(true));
                Assert.That(response.Data.Name, Is.EqualTo(dummyChargeStation.Name));
            });
        }

        private ChargeStation GetDummyChargeStation()
        {
            return new ChargeStation()
            {
                ChargeStationId = _dummyChargeStationId1,
                SmartGroupId = _dummySmartGroupId1,
                Name = "JRR Tolkien",
                Connectors = new List<Connector>()
                {
                    new Connector()
                    {
                        ChargeStationId = _dummyChargeStationId1,
                        ConnectorId = 1,
                        MaxCurrentInAmps = 10,
                    }
                },
            };
        }
    }
}
