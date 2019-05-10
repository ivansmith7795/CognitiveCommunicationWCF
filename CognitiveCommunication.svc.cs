using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Search;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CognitiveCommunicationWCF
{

    public class CognitiveCommunication : IService1
    {
        public void GetOptions()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");
        }

        public List<Suggestion> ChangeComm(string changeNumber)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            //grab the information about this change ticket from ServiceNow
            GetServiceNowData SDData = new GetServiceNowData();
            Task<string> changeRequest = SDData.GetChangeInfoREST(changeNumber);
            string changeJsonResponse = changeRequest.Result;

            dynamic json = JsonConvert.DeserializeObject(changeJsonResponse);

            string changeBody = "";
            string changeGroup = "";
            string CISysID = "";
            string changecommCI = "";
            DateTime changeStart = new DateTime();
            DateTime changeEnd = new DateTime();

            string jsonStart = json.result[0].start_date.ToString();

            //Debug.WriteLine(jsonStart);

            if (json != null)
            {
                changeBody += json.result[0].description.ToString().Trim();
                changeGroup += json.result[0].assignment_group.value.ToString().Trim();
                CISysID += json.result[0].cmdb_ci.value.ToString().Trim();
                changeStart = DateTime.ParseExact(json.result[0].start_date.ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); 
                changeEnd = DateTime.ParseExact(json.result[0].end_date.ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                throw new WebFaultException<string>(
                string.Format("There is no ticket in ServiceNow for '{0}'.", changeNumber),
                HttpStatusCode.NotFound);

            }

            //Lets grab ths CI information
            if (CISysID != "")
            {
                Task<string> changeCI = SDData.GetCIInfoREST(CISysID);
                string CIJson = changeCI.Result;

                dynamic CIjson = JsonConvert.DeserializeObject(CIJson);

                if (CIjson != null)
                {

                    changecommCI += CIjson.result.sys_class_name.ToString().Trim();
                }
              
            }

            //Lets see if we can detect and apps
            GetApplications getApps = new GetApplications();
            List<string> apps = getApps.GetApps(changeBody);

            //Now lets build the query for this change
            QueryBuilder queryBuilder = new QueryBuilder();
            BooleanQuery query = new BooleanQuery();

            if (apps.Count() > 0)
            {
                string changeApps = String.Join(" ", apps.ToArray());
                query = queryBuilder.GetCommQuery(changeBody, changeGroup, changeApps, changecommCI);
            }
            else
            {
                query = queryBuilder.GetCommQuery_NoApps(changeBody, changeGroup, changecommCI);
            }

            //Finally, execute the query against our Lucene database of change communications

            string indexFileLocation = @"C:\Models\ChangeData\";

            Lucene.Net.Store.Directory dir = FSDirectory.Open(indexFileLocation);
            IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(dir, true));

            TopDocs td = searcher.Search(query, 5);

            ScoreDoc[] hits = td.ScoreDocs;

            List<Suggestion> suggestions = new List<Suggestion>();

            TextProcessor processor = new TextProcessor();

            foreach (ScoreDoc searchResult in hits)
            {
                int docId = searchResult.Doc;
                double score = Convert.ToDouble(searchResult.Score);

                Lucene.Net.Documents.Document doc = searcher.Doc(docId);

                Suggestion suggestion = new Suggestion();

                suggestion.changeNumber = doc.Get("changenumber");
                suggestion.changeGroup = doc.Get("change_group_name");
                suggestion.release = changeStart;
                suggestion.archive = changeEnd;
                suggestion.title = doc.Get("title");
                suggestion.application = doc.Get("application");
                suggestion.summaryHTML = processor.Convert2Text(suggestion, doc.Get("communication_descriptionHTML"));
                suggestion.summaryText = processor.GenerateDescriptionText(suggestion.summaryHTML);
                suggestion.bodyHTML = processor.Convert2Text(suggestion, doc.Get("communication_bodyHTML"));
                suggestion.bodyHTML += "<div id='referenceinfo'><p>Reference No.: <strong>" + changeNumber + "</strong></p><p>&nbsp;</p><p>&nbsp;</p>";

                suggestions.Add(suggestion);

            }

            return suggestions;
        }

      
    }
}
