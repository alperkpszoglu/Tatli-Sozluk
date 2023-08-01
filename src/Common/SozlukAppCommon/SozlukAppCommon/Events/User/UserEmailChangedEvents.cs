using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukAppCommon.Events.User
{
    public class UserEmailChangedEvents
    {
        // we dont take id bc email address is unique
        public string NewEmailAddress { get; set; }
        public string OldEmailAddress { get; set; }

    }
}
