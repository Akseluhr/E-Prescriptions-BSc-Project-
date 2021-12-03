using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace WCFLabb2AkselVilgot
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Labb2Service : ILabb2
    {
        static XElement _testData;
        static XElement _interchanges;

        public Labb2Service()
        {
            using (WebClient webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString(Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vdGVzdERhdGEuanNvbg==")));
                _testData = JsonConvert.DeserializeObject<XElement>(jsonString);
                string jsonString2 = webClient.DownloadString(Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vaWNzLmpzb24=")));
                _interchanges = JsonConvert.DeserializeObject<XElement>(jsonString2);
            }
            
        }

        /// <summary>
        /// Hämtar Test-XML
        /// </summary>
        /// <returns></returns>
        public XElement GetTestData()
        {
            return _testData;
            
        }
        /// <summary>
        /// Hämtar alla Interchanges
        /// </summary>
        /// <returns></returns>
        public XElement GetAllInterchanges()
        {
                return _interchanges;
        }
        /// <summary>
        /// Hämtar det interchange från testdata som matchar input-ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XElement FilterByInterchangeID(int id)
        {
            
            XElement xml = GetTestData();
            XElement filteredXmlById = new XElement("Resultat",
                                from b in xml.Descendants("Interchange")
                                where b.Element("MessageRoutingAddress").Element("InterchangeRef").Value == Convert.ToString(id)
                                select b);
            return filteredXmlById;
        }

        /// <summary>
        /// Hämtar de noder i testdata som matchar användar-inputnoden
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public XElement FilterByInterchangeNode(string node)
        {
            XElement xml = GetTestData();
            XElement filteredXmlByNode = new XElement("Resultat",
                                            from b in xml.Descendants(node)
                                          //  where (string)b.Element(node) == node
                                            group b by b.Element(node) into newXml
                                            select newXml);
                                            
            return filteredXmlByNode;
        }
        /// <summary>
        /// hämtar de noder och värden från ett särskilt interchange (mha ID) för att sedan returnera de noder som matchar användar-inputnoden
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public XElement FilterByInterchangeIDAndNode(int id, string node)
        {
            XElement xml = GetTestData();
            XElement filteredXmlById = FilterByInterchangeID(id);
            XElement filteredXmlByNodeAndId = new XElement("Resultat",
                                            from b in filteredXmlById.Descendants(node)
                                                //  where (string)b.Element(node) == node
                                            group b by node into newXml
                                            select newXml);
            return filteredXmlByNodeAndId;
        }
        /// <summary>
        /// Returnerar hela interchanges där ett visst nodvärde matchar input, dvs där inputvärdet finns i ett eller flera interchange(s)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public XElement FilterByInterchangeNodeValue(string node, string value)
        {
            XElement xml = GetTestData();
            XElement filteredXmlByNodeValue = new XElement("Resultat",
                                            from b in xml.Descendants("Interchange")
                                            from x in b.Descendants(node).Take(1)
                                            where x.Value == value                                        
                                            select b);
            return filteredXmlByNodeValue;
        }



        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


    }
}
