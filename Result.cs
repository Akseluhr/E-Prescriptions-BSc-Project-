using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2AkselVilgot
{
    public class Result 
    {
        /// <summary>
        /// Egenskapen håller i de olika resultatet av de queries som utförs
        /// Till denna egenskap läggs dagens datum till som attribut, varje gång den nyttjas (i.e. ska skrivas ut)
        /// </summary>
        public XElement Results
        {
            get; 
            set; 
        }
        //Instans av labb2Client för senare metodanrop
        Labb2Service.Labb2Client labb2Client = new Labb2Service.Labb2Client();

        /// <summary>
        /// Hämtar och skriver ut alla interchanges i konsollen
        /// Nyckelvärden plockas även ut från dessa
        /// </summary>
        public void GetAll()
        {
            Results = labb2Client.GetAllInterchanges();
            Results.SetAttributeValue("Date", DateTime.Now); 
            Console.WriteLine(Results);
            GetRelevantInformation(Results);
        }
        /// <summary>
        /// Hämtar och skriver ut alla test-interchanges i konsollen
        /// Nyckelvärden plockas även ut från dessa
        /// </summary>
        public void GetTestData()
        {
            Results = labb2Client.GetTestData();
            Results.SetAttributeValue("Date", DateTime.Now);
            Console.WriteLine(Results);
            GetRelevantInformation(Results);
           // Console.WriteLine(Results);
        }
        /// <summary>
        /// Hämtar och skriver ut det interchange i konsollen som matchar input-ID
        /// Nyckelvärden plockas även ut från dessa
        /// </summary>
        public void GetFilteredByID(int id)
        {
            Results = labb2Client.FilterByInterchangeID(id);
            Results.SetAttributeValue("Date", DateTime.Now);
            Console.WriteLine(Results);
            GetRelevantInformation(Results);
        }
        /// <summary>
        /// Hämtar och skriver ut de noder som skickas in som input från alla interchanges 
        /// </summary>
        /// <param name="node"></param>
        public void GetFilteredByNode(string node)
        {
            Results = labb2Client.FilterByInterchangeNode(node);
            Results.SetAttributeValue("Date", DateTime.Now);
            Console.WriteLine(Results);
        }
        /// <summary>
        /// Hämtar och srkiver ut den nod och dess värde från ett specifikt interchange mha ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        public void GetFilteredByIDandNode(int id, string node)
        {
            Results = labb2Client.FilterByInterchangeIDAndNode(id, node);
            Results.SetAttributeValue("Date", DateTime.Now);
            Console.WriteLine(Results);
        }
        /// <summary>
        /// Hämtar och skriver ut det interchange eller de interchanges där node och nodevalue förekommer.
        /// Nyckelvärden skrivs även ut i konsollen
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeValue"></param>
        public void GetFilteredByNodeValue(string node, string nodeValue)
        {
            Results = labb2Client.FilterByInterchangeNodeValue(node, nodeValue);
            Results.SetAttributeValue("Date", DateTime.Now);
            Console.WriteLine(Results);
            GetRelevantInformation(Results);
        }
        /// <summary>
        /// Metod för att hämta och skriva ut nyckelvärden från interchanges
        /// Egenskapen resultat skickas in i metoden och baserat på vad denna innehåller för interchange(s) skrivs olika resulat ut
        /// i res finns de element som vi sedan ska iterera över. variabeln a motsvarar ett interchange. för varje a (interchange) traverserar loopen och hämtar de relevanta nyckelvärdena
        /// Felhantering då doseringsbeskrivning saknas eller
        /// </summary>
        /// <param name="xml"></param>
        public void GetRelevantInformation(XElement xml)
        {
            
            try
            {
                IEnumerable<XElement> res =
                    from a in xml.Descendants("Interchange")
                    select a;

                foreach(XElement a in res)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Patient: " + a.Element("NewPrescriptionMessage").Element("SubjectOfCare").Element("PatientMatchingInfo").Element("PersonNameDetails").Element("StructuredPersonName").Element("FirstGivenName").Value);
                    Console.WriteLine("Physician: " + a.Element("NewPrescriptionMessage").Element("PrescriptionMessage").Element("MessageSender").Element("HealthcareAgent").Element("HealthcareParty").Element("HealthcarePerson").Value);
                    Console.WriteLine("Medicine: " + a.Element("NewPrescriptionMessage").Element("PrescriptionSet").Element("PrescriptionItemDetails").Element("PrescribedMedicinalProduct").Element("MedicinalProduct").Element("ManufacturedProductId").Element("ProductId").Value);

                    try
                    {
                        Console.WriteLine("Dosage: " + a.Element("NewPrescriptionMessage").Element("PrescriptionSet").Element("PrescriptionItemDetails").Element("PrescribedMedicinalProduct").Element("InstructionsForUse").Element("UnstructuredInstructionsForUse").Element("UnstructuredDosageAdmin").Value);

                    }
                    catch
                    {
                        Console.WriteLine("Dosage: Missing");

                    }
                }

            }
            catch
            {
                Console.WriteLine("There's something wrong with the system.");
            }
            Console.WriteLine();

            //foreach (var b in xml.Nodes())
            //{
            //    Console.WriteLine("Patient: {0}, ", labb2Client.FilterByInterchangeNode("FirstGivenName").Value); //, "Sten Frisk"));
            //    Console.WriteLine("Physician: {0}, ", labb2Client.FilterByInterchangeNode("Name").Value);
            //    Console.WriteLine("Medicine: {0}", labb2Client.FilterByInterchangeNode("ProductId").Value);
            //    Console.WriteLine("Dosage: {0}, ", labb2Client.FilterByInterchangeNode("UnstructuredDosageAdmin").Value);
            //}
        }
    }
}
