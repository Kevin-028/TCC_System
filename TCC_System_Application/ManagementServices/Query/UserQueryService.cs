using Dapper;
using System.Collections.Generic;
using System.Linq;
using TCC_System_Domain.Core.Auth.JsonObjects;
using TCC_System_Domain.Management;

namespace TCC_System_Application.ManagementServices.Query
{
    public interface IUserQueryService
    {
        IEnumerable<UserViewModel> GetAll();
        UserViewModel GetUsersByLogin(string login);
        UserViewModel GetForID(int id);
        UserViewModel GetForIdUserClaim(int id);
        List<string> GetEmailUserClaim(string nomeClaim, string site);
        List<string> GetEmailbyNotification(int id);
        UserJson ObterUserEAcessosPorLogin(string login);
        List<string> GetEmailbyIssuer(int id, string issuer);
    }
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository Repository;
        public UserQueryService(IUserRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return Repository.GetDbConnection().Query<UserViewModel>
                (CreateSQLQuery() + " ORDER BY ID");
        }

        public UserViewModel GetForID(int id)
        {
            var sql = CreateSQLQuery() + " where ID = @id ";

            return Repository.GetDbConnection().QuerySingleOrDefault<UserViewModel>(sql, new { id });
        }

        public UserViewModel GetForIdUserClaim(int id)
        {

            var sql = CreateSQLQuery() + " where Id = @id ";
            sql += @"SELECT UC.ClaimID, C.NomeClaim FROM AUTH_UserClaims UC " +
                "INNER JOIN AUTH_Claims C on UC.ClaimID = C.ClaimID  where UC.UsuarioID = @id";

            var mult = Repository.GetDbConnection().QueryMultiple(sql, new { id });

            UserViewModel user = mult.ReadFirst<UserViewModel>();
            user.ClaimViewModel = mult.Read<ClaimViewModel>();

            return user;
        }
        public List<string> GetEmailUserClaim(string nomeClaim, string site)
        {
            var sql = @"
                SELECT TOP (1000) U.Email
                    FROM [db_FU_LLS].[dbo].[AUTH_Users] U

		                inner join AUTH_UserClaims UC on U.Id = UC.UsuarioID
                        INNER JOIN AUTH_Claims C on UC.ClaimID = C.ClaimID

		                inner join AUTH_UserClaims D on U.Id = D.UsuarioID
                        INNER JOIN AUTH_Claims E on D.ClaimID = E.ClaimID

		        WHERE C.NomeClaim = @nomeClaim AND E.NomeClaim = @site
            ";


            var users = Repository.GetDbConnection().Query<string>(sql, new { nomeClaim, site });

            return users.ToList();
        }
        public UserJson ObterUserEAcessosPorLogin(string login)
        {
            login = login.Substring(login.IndexOf(@"\") + 1);

            var sql = @"
                SELECT GroupName as DomainLogin, [Login],[Name] as Nome, Email
                FROM AUTH_Users
                WHERE [Login] = @login
            ";
            sql += @"
                 SELECT UPPER(C.NomeClaim) FROM AUTH_UserClaims UC
                    INNER JOIN AUTH_Claims C on UC.ClaimID = C.ClaimID 
	                INNER JOIN AUTH_Users U on UC.UsuarioID = U.Id 
                 WHERE U.Login = @login
            ";

            var mult = Repository.GetDbConnection().QueryMultiple(sql, new { login });

            UserJson user = mult.ReadFirst<UserJson>();
            user.SetListClaims(mult.Read<string>().ToList());

            return user;
        }

        public UserViewModel GetUsersByLogin(string login)
        {
            var sql = CreateSQLQuery() + " where Login = @login";

            return Repository.GetDbConnection().QuerySingleOrDefault<UserViewModel>(sql, new { login });
        }

        public List<string> GetEmailbyIssuer(int id, string issuer)
        {
            var sql = @"SELECT U.Email FROM LLS_ActionPlans A
	                        INNER JOIN AUTH_Users U on A.Issuer = U.Name
	                    WHERE A.Issuer = @issuer AND A.Id = @id"
            ;
            var users = Repository.GetDbConnection().Query<string>(sql, new { id, issuer });



            return users.ToList();
        }

        public List<string> GetEmailbyNotification(int id)
        {

            var sql = @"SELECT U.EMAIL FROM LLS_ACTIONPLANS A
	                        INNER JOIN LLS_NOTIFICATIONS N ON A.NOTIFYID = N.ID
	                        INNER JOIN AUTH_USERS U ON U.GROUPNAME+'\'+U.[LOGIN] = N.RECORDUPDATEDBY
                        WHERE A.NOTIFYID = @id"
            ;

            var users = Repository.GetDbConnection().Query<string>(sql, new { id });

            return users.ToList();
        }

        private string CreateSQLQuery()
        {
            return @"SELECT * FROM AUTH_Users ";
        }

    }

}
