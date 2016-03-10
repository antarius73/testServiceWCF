using ServiceDal;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestWcfAppIis
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ServiceWcfApp" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez ServiceWcfApp.svc ou ServiceWcfApp.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ServiceWcfApp : IServiceWcfApp
    {
        public Collection<PersonModel> ListAll()
        {
            try
            {
                Collection<PersonModel> arrEmployees = null;

                try
                {
                    arrEmployees = PersonDal.GetPersons();
                    if (arrEmployees == null) throw new FaultException("EMPLOYEE NOT CODE");
                }
                catch (Exception e)
                {
                    throw (e);
                }

                return arrEmployees;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }

        public PersonModel GetDetail(string personId)
        {
            try
            {
                PersonModel employee = PersonDal.GetPersonDetail(Convert.ToInt32(personId, CultureInfo.CurrentCulture));
                if (employee == null)
                {
                    throw new FaultException("EMPLOYEE NOT CODE");
                }
                return employee;

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }

        public void UpdateName(string personId, string name)
        {
            try
            {
                PersonDal.UpdateEmployeeName(Convert.ToInt32(personId, CultureInfo.CurrentCulture), name);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }
    }
}
