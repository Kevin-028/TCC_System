using System;
using System.Collections.Generic;
using System.Text;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;

namespace TCC_System_Data
{
    public class MessageRepository : Repository<MessageAction>, IMessageRepository
    {
        public MessageRepository(TCC_Context context) : base(context)
        {

        }
    }
}
