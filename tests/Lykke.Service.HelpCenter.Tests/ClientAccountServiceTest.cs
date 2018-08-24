using Lykke.Service.HelpCenter.Services.Services;
using Xunit;

namespace Lykke.Service.HelpCenter.Tests
{
    public class ClientAccountServiceTest
    {
        [Fact]
        public void GetNameTest()
        {
            string FromEmail(string x) => SupportClientsService.GetName(default, x);

            Assert.Equal(nameof(GetNameTest), SupportClientsService.GetName(nameof(GetNameTest), default));
            Assert.Equal(default, FromEmail(null));
            Assert.Equal(default, FromEmail(" "));
            Assert.Equal("Niek", FromEmail("Niek"));
            Assert.Equal("Info", FromEmail("info@lykke.com"));
            Assert.Equal("Some One", FromEmail("some.one@lykke.com"));
            Assert.Equal("Some Two", FromEmail("some_two@lykke.com"));
            Assert.Equal("Some Three", FromEmail("some-three@lykke.com"));
            Assert.Equal("R Rabbit", FromEmail("r.rabbit@lykke.com"));
            Assert.Equal("Crypto123", FromEmail("crypto123@lykke.com"));
        }
    }
}
