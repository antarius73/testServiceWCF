using ServiceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[assembly: CLSCompliant(true)]
namespace TestWcfAppIis
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IServiceWcfApp" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceWcfApp
    {
        [OperationContract]
        [WebGet(UriTemplate = "Person/GetDetail/{personId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PersonModel GetDetail(string personId);

        [OperationContract]
        [WebGet(UriTemplate = "Person/ListAll/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Collection<PersonModel> ListAll();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Person/UpdateName/{personId}/{name}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateName(string personId, string name);
    }
}
