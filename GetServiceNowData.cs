using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CognitiveCommunicationWCF
{
    public class GetServiceNowData
    {
        public async Task<string> GetChangeInfoREST(string changeTicket)
        {
            using (HttpClient rest = new HttpClient())

            {

                Uri sNowURI = new Uri("https://domain.service-now.com/api/now/table/change_request?sysparm_query=number=" + changeTicket);

                rest.BaseAddress = sNowURI;

                rest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(

                "Basic",

                Convert.ToBase64String(

                ASCIIEncoding.ASCII.GetBytes(

                string.Format("{0}:{1}", "USER", "PASSWORD"))));

                rest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await rest.GetAsync(sNowURI);

                response.EnsureSuccessStatusCode();


                Task<string> contents = response.Content.ReadAsStringAsync();

                return contents.Result;
            }
        }

        public async Task<string> GetCIInfoREST(string CISysID)
        {
            using (HttpClient rest = new HttpClient())

            {

                Uri sNowURI = new Uri("https://domain.service-now.com/api/now/table/cmdb_ci/" + CISysID);

                rest.BaseAddress = sNowURI;

                rest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(

                "Basic",

                Convert.ToBase64String(

                ASCIIEncoding.ASCII.GetBytes(

                string.Format("{0}:{1}", "USER", "PASSWORD"))));

                rest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await rest.GetAsync(sNowURI);

                response.EnsureSuccessStatusCode();


                Task<string> contents = response.Content.ReadAsStringAsync();

                return contents.Result;
            }
        }

        public async Task<string> GetGroupInfoREST(string userGroupID)
        {
            using (HttpClient rest = new HttpClient())

            {

                Uri sNowURI = new Uri("https://domain.service-now.com/api/now/table/sys_user_group/" + userGroupID);

                rest.BaseAddress = sNowURI;

                rest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(

                "Basic",

                Convert.ToBase64String(

                ASCIIEncoding.ASCII.GetBytes(

                string.Format("{0}:{1}", "USER", "PASSWORD"))));

                rest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await rest.GetAsync(sNowURI);

                response.EnsureSuccessStatusCode();


                Task<string> contents = response.Content.ReadAsStringAsync();

                return contents.Result;
            }
        }

    }
}