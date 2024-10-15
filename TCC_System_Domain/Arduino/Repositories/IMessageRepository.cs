using System;
using System.Collections.Generic;
using System.Text;
using TCC_System_Domain.Core;

namespace TCC_System_Domain.Arduino.Repositories
{
    public interface IMessageRepository : IRepository<MessageAction>
    {
        MessageAction GetByProject(Guid id);
        MessageAction GetByAPI(Guid id);


    }
}
