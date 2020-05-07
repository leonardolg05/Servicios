using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WSGateway.Model
{
    public class Router
    {
        public List<Route> Routes { get; set; }
        public Destination AuthenticationService { get; set; }


        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
            AuthenticationService = JsonLoader.Deserialize<Destination>(Convert.ToString(router.authenticationService));

        }
        public async Task<string> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            string basePath = '/' + path.Split('/')[1];
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Destination destination;
            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                return "The path could not be found.";
            }

            if (destination.RequiresAuthentication)
            {
                string token = request.Headers["Authorization"];
                request.Query.Append(new KeyValuePair<string, StringValues>("Authorization", new StringValues(token)));
                
                //request.Headers.Add("Authorization", token);
                //request.Headers.Add("Authorization", "Bearer " + token);
                //HttpResponseMessage authResponse = await AuthenticationService.SendRequest(request);
                //if (!authResponse.IsSuccessStatusCode) return ConstructErrorMessage("Authentication failed.");
            }

            //return await destination.SendRequest(request);
            return await destination.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }
    }
}
