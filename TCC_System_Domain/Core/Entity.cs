using System;

namespace TCC_System_Domain.Core
{
    public abstract class Entity
    {
        public string RecordCreatedBy { get; private set; }
        public string RecordUpdatedBy { get; private set; }
        public DateTime? RecordCreationDate { get; private set; }
        public DateTime? RecordUpdateDate { get; private set; }

        public void SetRecordCreatedBy(string user)
        {
            RecordCreatedBy = user;
        }

        public void SetRecordUpdatedBy(string user)
        {
            RecordUpdatedBy = user;
        }

        public void SetRecordCreationDate()
        {
            RecordCreationDate = DateTime.Now;
        }

        public void SetRecordUpdateDate()
        {
            RecordUpdateDate = DateTime.Now;
        }
    }
}
