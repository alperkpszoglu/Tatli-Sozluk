using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukAppCommon
{
    public class SozlukAppConstants
    {
        // rabbitMQ
        public const string RabbitHost = "localhost";
        public const string DefaulExchange = "direct";

        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangeQueueName = "UserEmailChangeQueue";

    }
}
