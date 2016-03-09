using ServiceDal;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace testWcfAppIIS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ServiceWcfApp" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez ServiceWcfApp.svc ou ServiceWcfApp.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ServiceWcfApp : IServiceWcfApp
    {
        public List<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> arrEmployees = SqlManager.GetAllEmployeesDetails();
                if (arrEmployees == null)
                {
                    throw new FaultException("EMPLOYEE NOT CODE");
                }
                return arrEmployees;

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }

        public Employee GetEmployee(string employeeId)
        {
            try
            {
                Employee employee = SqlManager.GetEmployeeDetails(Convert.ToInt32(employeeId));
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

        public void UpdateEmployeeName(string employeeId, string employeeName)
        {
            try
            {
                SqlManager.UpdateEmployeeName(Convert.ToInt32(employeeId), employeeName);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }
    }
}
