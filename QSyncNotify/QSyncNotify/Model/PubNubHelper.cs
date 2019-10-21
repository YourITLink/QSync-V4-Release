using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PubnubApi;
using Toast;
using System.Windows;

namespace QSyncNotify.Model
{
    public class PubNubHelper
    {
        Pubnub pubnub;
        private readonly string ChannelName = "win-notification";

        public void Init()
        {
            //Init
            PNConfiguration pnConfiguration = new PNConfiguration
            {
                PublishKey = "pub-c-4a022680-c04a-4dde-8556-5e8bb74f1136",
                SubscribeKey = "sub-c-b6a90444-9b2b-11e9-ab0f-d62d90a110cf",
                Secure = true
            };
            pubnub = new Pubnub(pnConfiguration);
            //Subscribe
            pubnub.Subscribe<string>()
           .Channels(new string[] {
               ChannelName
           })
           .WithPresence()
           .Execute();
        }
        //Publish a message
        public void Publish()
        {
            JsonMsg Person = new JsonMsg
            {
                Name = "John Doe",
                Description = "Description",
                Date = DateTime.Now.ToString()
            };
            //Convert to string
            string arrayMessage = JsonConvert.SerializeObject(Person);
            pubnub.Publish()
                .Channel(ChannelName)
                .Message(arrayMessage)
                .Async(new PNPublishResultExt((result, status) => { }));
        }
        //listen to the channel
        public void Listen()
        {
            SubscribeCallbackExt listenerSubscribeCallack = new SubscribeCallbackExt(
            (pubnubObj, message) =>
            {
            //Call the notification windows from the UI thread
            Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                //Show the message as a WPF window message like WIN-10 toast
                NotificationWindow ts = new NotificationWindow();
                //Convert the message to JSON
                JsonMsg bsObj = JsonConvert.DeserializeObject<JsonMsg>(message.Message.ToString());
                    string messageBoxText = "Name: " + bsObj.Name + Environment.NewLine + "Description: " + bsObj.Description + Environment.NewLine + "Date: " + bsObj.Date;
                    ts.NotifText.Text = messageBoxText;
                    ts.Show();
                }));
            },
            (pubnubObj, presence) =>
            {
            // handle incoming presence data
        },
            (pubnubObj, status) =>
            {
            // the status object returned is always related to subscribe but could contain
            // information about subscribe, heartbeat, or errors
            // use the PNOperationType to switch on different options
        });

            pubnub.AddListener(listenerSubscribeCallack);
        }
    }
}


