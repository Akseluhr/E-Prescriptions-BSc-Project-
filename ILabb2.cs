using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace WCFLabb2AkselVilgot
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILabb2
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        XElement GetTestData();

        [OperationContract]
        XElement GetAllInterchanges();

        [OperationContract]
        XElement FilterByInterchangeID(int id);

        [OperationContract]
        XElement FilterByInterchangeNode(string node);

        [OperationContract]
        XElement FilterByInterchangeIDAndNode(int id, string node);

        [OperationContract]
        XElement FilterByInterchangeNodeValue(string node, string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
