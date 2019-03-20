using Beacons.Build;
using PipServices3.Container;
using PipServices3.Rpc.Build;

namespace Beacons.Container
{
    public class BeaconsProcess : ProcessContainer
    {
        public BeaconsProcess()
            : base("beacons", "Beacons microservice")
        {
            _factories.Add(new DefaultRpcFactory());
            _factories.Add(new BeaconsServiceFactory());
        }
    }
}
