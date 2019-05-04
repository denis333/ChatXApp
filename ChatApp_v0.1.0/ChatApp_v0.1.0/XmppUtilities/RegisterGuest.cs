using System.Threading.Tasks;
using Matrix;
using Matrix.Xmpp.Register;
using Matrix.Xmpp.XData;

namespace ChatApp_v0._1._0.XmppUtilities
{
    // TODO
    public class RegisterGuest : IRegister
    {
        private XmppClient guestClient;
        public RegisterGuest()
        {
            guestClient = new XmppClient
            {
                Username = "guest1",    // TODO: this name shall be dynamic
                Password = "1111",
                XmppDomain = "myDomain@example.com"
            };
        }

        public bool RegisterNewAccount => true;

        public async Task<Register> RegisterAsync(Register register)
        {
            return await Task<Register>.Run(() =>
            {
                register.RemoveAll<Data>();
                register.Username = guestClient.Username;
                register.Password = guestClient.Password;

                return register;
            });
        }
    }
}