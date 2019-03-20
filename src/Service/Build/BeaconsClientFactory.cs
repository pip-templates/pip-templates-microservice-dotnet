using PipServices3.Commons.Refer;
using PipServices3.Components.Build;
using Beacons.Clients.Version1;

namespace Beacons.Build
{
    public class BeaconsClientFactory : Factory
    {
        public static Descriptor NullClientDescriptor = new Descriptor("beacons", "client", "null", "*", "1.0");
        public static Descriptor DirectClientDescriptor = new Descriptor("beacons", "client", "direct", "*", "1.0");
        public static Descriptor HttpClientDescriptor = new Descriptor("beacons", "client", "http", "*", "1.0");

        public BeaconsClientFactory()
        {
            RegisterAsType(BeaconsClientFactory.NullClientDescriptor, typeof(BeaconsNullClientV1));
            RegisterAsType(BeaconsClientFactory.DirectClientDescriptor, typeof(BeaconsDirectClientV1));
            RegisterAsType(BeaconsClientFactory.HttpClientDescriptor, typeof(BeaconsHttpClientV1));
        }
    }
}
