using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Applicaion.EventSourcedNormalizers
{
    public class UserHistoryData
    {

        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string When { get; set; }
        public string Who { get; set; }

        public string Ip { get; set; }
    }
}
