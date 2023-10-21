using System.Configuration;
using System.Data;
using System.Text.Json.Serialization;
using System.Xml;
using AcquireXModel;
using AquirexServices.interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace AquirexServices.implementation
{
    public class RestProducts : IRestProducts
    {
        private AuthToken authToken;
       
        public async Task<Products> GetProducts(string endpoint)
        {
            Products Products;
            if (authToken == null)
            {
                authToken = await this.GetAuthToken();
            }
            var rest = new RestClient(ConfigurationManager.AppSettings["product_base_url"]);
            var request = new RestRequest(endpoint, Method.Get);
            request.AddHeader("Authorization", $"Bearer {authToken.AccessToken}");
            RestResponse response = rest.Execute(request);
            if (response != null && !response.IsSuccessStatusCode)
            {
                //Logger for the Exception
                throw new Exception("API Call Failed");
            }


            // Based on the response XML or JSON it will decide 
            // Return response in Products Object
            if (response.Content.StartsWith("<"))
            {

                var dotNetXmlDeserializer = new DotNetXmlDeserializer();
                Products = dotNetXmlDeserializer.Deserialize<Products>(response);

            }
            else
            {
                Products = JsonConvert.DeserializeObject<Products>(response.Content);
            }


            return Products;

        }

        private async Task<AuthToken> GetAuthToken()
        {
            try
            {
                var options = new RestClientOptions(ConfigurationManager.AppSettings["auth_base_url"]);
                var client = new RestClient(options);
                var request = new RestRequest("/realms/ctesting/protocol/openid-connect/token", Method.Post);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", ConfigurationManager.AppSettings["grant_type"]);
                request.AddParameter("scope", ConfigurationManager.AppSettings["scope"]);
                request.AddParameter("client_id", ConfigurationManager.AppSettings["client_id"]);
                request.AddParameter("client_secret", ConfigurationManager.AppSettings["client_secret"]);
                RestResponse response = client.Execute(request);
                if (!response.IsSuccessStatusCode)
                {
                    //Logger 
                    throw new Exception("Auth Token Failed");
                }

                authToken = JsonConvert.DeserializeObject<AuthToken>(response.Content);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            return authToken;
        }
    }
}