using Noggolloquy;
using Noggolloquy.Tests;

namespace Noggolloquy
{
    public class ProtocolDefinition_NoggolloquyTests : IProtocolRegistration
    {
        public readonly static ProtocolKey ProtocolKey = new ProtocolKey(1);
        public readonly static ProtocolDefinition Definition = new ProtocolDefinition(
            key: ProtocolKey,
            nickname: "NoggolloquyTests");

        public void Register()
        {
            NoggolloquyRegistration.Register(TestGenericObject_Registration.Instance);
            NoggolloquyRegistration.Register(TestObject_Registration.Instance);
            NoggolloquyRegistration.Register(ObjectToRef_Registration.Instance);
        }
    }
}
