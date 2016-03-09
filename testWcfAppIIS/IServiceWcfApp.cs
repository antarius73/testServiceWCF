using ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace testWcfAppIIS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IServiceWcfApp" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceWcfApp
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetEmployee/{employeeId}", ResponseFormat = WebMessageFormat.Json)]
        Employee GetEmployee(string employeeId);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllEmployees/", ResponseFormat = WebMessageFormat.Json)]
        List<Employee> GetAllEmployees();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "UpdateEmployeeName/{employeeId}/{employeeName}")]
        void UpdateEmployeeName(string employeeId, string employeeName);
    }
}
