using Matrix;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Roster;
using System.Threading.Tasks;

namespace ChatXApp.Droid.XmppUtilities
{   // TODO: 
    public class AccountsList
    {
        private XmppClient xmppClient;

        public AccountsList(XmppClient client)
        {
            xmppClient = client;
        }

        // TODO: to implement
        /*public async Task<Iq> GetAccountsList()
        {
            // TODO: do checks if client is connected to server?
            var roster = await xmppClient.RequestRosterAsync();
            return roster;

            // get all rosterItems (list of contacts)
            var rosterItems
                = roster
                    .Query
                    .Cast<Roster>()
                    .GetRoster();

            enumerate over the items and build your contact list or GUI
            foreach (var ri in rosterItems)
            {
                // we use AutoMapper here to map the XMPP
                // rosterItem to our Contact ViewModel

                // var contact = mapper.Map<Contact>(ri);
                // contacts.AddOrReplace(contact, c => c.Jid == contact.Jid);
            }
        }*/
    }
}