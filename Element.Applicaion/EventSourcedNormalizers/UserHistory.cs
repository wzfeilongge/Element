using Element.Core.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Element.Applicaion.EventSourcedNormalizers
{
    public static class UserHistory
    {

        public static IList<UserHistoryData> HistoryData { get; set; }



        public static IList<UserHistoryData> ToJavaScriptUserHistory(IList<StoredEvent> storedEvents)
        {

            HistoryData = new List<UserHistoryData>();

            UserHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);

            var list = new List<UserHistoryData>();

            var last = new UserHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new UserHistoryData
                {
                    Id =  change.Id,
                    Name = change.Name,
                    Email =  change.Email,
                    Phone = change.Phone,
                    Action =  change.Action,
                    When = change.When,
                    Who = change.Who,
                    Ip=  change.Ip,
                };
                list.Add(jsSlot);
                last = change;
            }

            return list;
        }


        private static void UserHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                if (e == null)
                {
                    return;
                }
                var slot = new UserHistoryData();
                dynamic values;
                switch (e.MessageType)
                {
                    case "UserRegisterEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Email = values["Email"];
                        slot.Phone = values["Phone"];
                        slot.Name = values["Name"];
                        slot.Action = "UserRegisterEvent";
                        slot.When = e.Timestamp.ToString();
                        slot.Id = e.Id.ToString();
                        slot.Who = values["Name"];
                        break;

                    case "ChangePwdEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
                        slot.Action = "ChangePwdEvent";
                        slot.When = e.Timestamp.ToString();                        
                        slot.Who = values["Name"];
                        slot.Ip = values["Ip"];
                        slot.Id = e.Id.ToString();
                        break;
                }
                HistoryData.Add(slot);
            }
        }

















    }
}
