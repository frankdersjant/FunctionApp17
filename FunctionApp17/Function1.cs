using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace FunctionApp17
{
    public  class Function1
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Function1(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            AZEntity Bastiaan = new AZEntity();
            Bastiaan.Id = 1;

            _applicationDbContext.Add<AZEntity>(Bastiaan);
            try
            {
                _applicationDbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                string t = ex.Message;
            }



            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";


            return new OkObjectResult("ok");
        }
    }
}
