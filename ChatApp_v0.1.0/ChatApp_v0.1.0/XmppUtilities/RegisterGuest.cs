using System.Threading.Tasks;
using Matrix;
using Matrix.Xmpp.Register;
using Matrix.Xmpp.XData;

namespace ChatApp_v0._1._0.XmppUtilities
{
    public class RegisterGuest : IRegister
    {
        private XmppClient guestClient;
        public RegisterGuest(string username)
        {
            guestClient = new XmppClient
            {
                Username = username,
                Password = "1111",
                XmppDomain = "defaultDomain.com"
            };
        }

        public RegisterGuest(XmppClient xmppClient)
        {
            guestClient = xmppClient;
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