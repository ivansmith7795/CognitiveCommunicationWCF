using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CognitiveCommunicationWCF
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "ChangeComm/{ticketnum}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Suggestion> ChangeComm(string ticketnum);

        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }


}
