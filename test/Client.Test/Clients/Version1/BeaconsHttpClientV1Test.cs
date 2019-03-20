using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using System.Threading.Tasks;
using Xunit;
using Beacons.Persistence;
using Beacons.Logic;
using Beacons.Services.Version1;
using System;

namespace Beacons.Clients.Version1
{
    public class BeaconsHttpClientV1Test
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", 8080
        );

        private BeaconsMemoryPersistence _persistence;
        private BeaconsController _controller;
        private BeaconsHttpClientV1 _client;
        private BeaconsHttpServiceV1 _service;
        private BeaconsClientV1Fixture _fixture;

        public BeaconsHttpClientV1Test()
        {
            _persistence = new BeaconsMemoryPersistence();
            _controller = new BeaconsController();
            _client = new BeaconsHttpClientV1();
            _service = new BeaconsHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("beacons", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("beacons", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("beacons", "client", "http", "default", "1.0"), _client,
                new Descriptor("beacons", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);

            _client.Configure(HttpConfig);
            _client.SetReferences(references);

            _fixture = new BeaconsClientV1Fixture(_client);

            _service.OpenAsync(null).Wait();
            _client.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestCalculatePositionsAsync()
        {
            await _fixture.TestCalculatePositionsAsync();
        }

    }
}
