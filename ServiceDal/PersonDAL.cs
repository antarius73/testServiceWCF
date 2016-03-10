using ServiceModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Xml;
using System.Linq;
using System.Collections.ObjectModel;
using System.Globalization;

[assembly: CLSCompliant(true)]
namespace ServiceDal
{
    public sealed class PersonDal
    {
        #region singleton

        private static PersonDal _instance;

        private static Hashtable _contextsList = new Hashtable();

        public static PersonDal Instance
        {
            get
            {
                PersonDal context;

                // contexte non web
                if (HttpContext.Current == null || HttpContext.Current.Session == null) context = PersonDal._instance ?? (PersonDal._instance = new PersonDal());
                else
                {
                    // Contexte Web
                    if (!_contextsList.ContainsKey(HttpContext.Current.Session.SessionID)) _contextsList.Add(HttpContext.Current.Session.SessionID, new PersonDal());
                    context = (PersonDal)_contextsList[HttpContext.Current.Session.SessionID];
                }

                return context;
            }
        }

        private PersonDal() { }

        #endregion

        public static PersonModel GetPersonDetail(int id)
        {
            Collection<PersonModel> persons = PersonDal.GetPersons(id);

            if (persons == null || persons.Count == 0) return new PersonModel();

            return persons[0];
        }

        public static Collection<PersonModel> GetPersons()
        {
            return PersonDal.GetPersons(null);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de portée")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Vérifier si les requêtes SQL présentent des failles de sécurité")]
        private static Collection<PersonModel> GetPersons(int? id)
        {
            Collection<PersonModel> persons = null;

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SELECT BusinessEntityID, Title, FirstName, MiddleName, LastName, ModifiedDate");
            queryBuilder.AppendLine("FROM Person.Person");
            if (id != null) queryBuilder.AppendLine("WHERE BusinessEntityID = @beID");

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    if (id != null) command.Parameters.Add("@beID", SqlDbType.Int).Value = id;

                    using (DataTable dt = new DataTable() { Locale = CultureInfo.CurrentCulture, })
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dt);

                            if (da != null)
                            {
                                persons = new Collection<PersonModel>((from DataRow row in dt.Rows
                                                                               select new PersonModel()
                                                                               {
                                                                                   BusinessEntityId = row.Field<int>("BusinessEntityID"),
                                                                                   FirstName = row.Field<string>("FirstName"),
                                                                                   LastName = row.Field<string>("LastName"),
                                                                                   MiddleName = row.Field<string>("MiddleName"),
                                                                                   Title = row.Field<string>("Title"),
                                                                                   LastDateModif = row.Field<DateTime>("ModifiedDate"),
                                                                               }).ToList());
                            }
                        }
                    }
                }
            }

            return persons;
        }

        public static void UpdateEmployeeName(int employeeId, string employeeName)
        {
            string sqlCommand = "update Person.Person set LastName=@LastName, ModifiedDate=GETDATE() WHERE BusinessEntityID=@BusinessEntityID";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@BusinessEntityID", SqlDbType.Int);
                    command.Parameters["@BusinessEntityID"] = new SqlParameter("@BusinessEntityID", employeeId);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters["@LastName"] = new SqlParameter("@LastName", employeeName);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
