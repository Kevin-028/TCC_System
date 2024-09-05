using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace TCC_System_Domain.Core
{
    public sealed class ActiveDirectoryUser
    {
        public string Email { get; private set; }
        public string Name { get; private set; }

        public ActiveDirectoryUser(string userLogin)
        {
            userLogin = AdjustUserLogin(userLogin);
            SearchUserInformationOnActiveDirectory(userLogin);
        }

        public static string AdjustUserLogin(string userLogin)
        {
            return userLogin.Substring(userLogin.IndexOf(@"\") + 1).ToUpper();
        }

        private void SearchUserInformationOnActiveDirectory(string userlogin)
        {
            Dictionary<string, string> PropriedadesAD = new Dictionary<string, string>();

            using (DirectoryEntry root = new DirectoryEntry(@"LDAP://group.pirelli.com"))
            {
                using (DirectorySearcher searcher = new DirectorySearcher(root, "(&(objectClass=user)(objectCategory=person))"))
                {
                    // filtrar consulta para um único usuário
                    searcher.Filter = string.Format("(sAMAccountName={0})", userlogin);

                    // filtrar propriedades
                    searcher.PropertiesToLoad.Add("mail");
                    searcher.PropertiesToLoad.Add("givenname");
                    searcher.PropertiesToLoad.Add("sn");

                    // Executa Consulta
                    SearchResult result = searcher.FindOne();

                    if (result != null)
                    {
                        foreach (System.Collections.DictionaryEntry item in result.Properties)
                        {
                            PropriedadesAD.Add(item.Key.ToString(), ((ResultPropertyValueCollection)item.Value)[0].ToString());
                        }
                    }

                    // Recupera Informações
                    if (PropriedadesAD.Count > 0)
                    {
                        Email = PropriedadesAD.First(x => x.Key.Equals("mail")).Value;
                        Name = PropriedadesAD.First(x => x.Key.Equals("givenname")).Value + " " + PropriedadesAD.First(x => x.Key.Equals("sn")).Value;
                    }
                }
            }

            PropriedadesAD = null;
        }

        public static ActiveDirectoryUser GetActiveDirectoryUserInformation(string userLogin)
        {
            return userLogin != null ? new ActiveDirectoryUser(userLogin) : null;
        }
    }
}
