using System;

namespace TCC_System_Domain.Core
{
    public class DomainNotification : IDomainEvent
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime DataOcorrencia { get; private set; }
        public int Versao { get; private set; }

        public DomainNotification(string key, string value)
        {
            Versao = 1;
            Key = key;
            Value = value;
            DataOcorrencia = DateTime.Now;
        }
    }
}
