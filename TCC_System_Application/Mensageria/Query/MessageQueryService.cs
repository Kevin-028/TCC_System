using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCC_System_Domain.Arduino.Repositories;
using System.Linq;

namespace TCC_System_Application.Mensageria
{
    public interface IMessageQueryService
    {
        Task<List<MessageVM>> GetMessagebyProject(Guid projectId);
        Task<List<MessageVM>> GetMessagebyProjectModule(Guid id, string mod);
    }

    public class MessageQueryService : IMessageQueryService
    {
       private readonly IMessageRepository Repository;

        public MessageQueryService(IMessageRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<MessageVM>> GetMessagebyProject(Guid id)
        {
            string sql = CreatSQLMessage() + " WHERE ProjectID = @id";
            IEnumerable<MessageVM> result = Repository.GetDbConnection().Query<MessageVM>(sql, new { id });

            return result.ToList();
        }  
        public async Task<List<MessageVM>> GetMessagebyProjectModule(Guid id, string mod)
        {
            string sql = CreatSQLMessage() + " WHERE ProjectID = @id AND Type = @mod";
            IEnumerable<MessageVM> result = Repository.GetDbConnection().Query<MessageVM>(sql, new { id,mod });

            return result.ToList();
        }


        private string CreatSQLMessage()
        {
            return @"Select * FROM MessageAction ";
        }
    }
}
