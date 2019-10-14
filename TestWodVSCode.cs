using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HelloWorld.Function
{
    public static class TestWodVSCode
    {
        [FunctionName("TestWodVSCode")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "put","post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Teste Wod inicio da function");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult(new {Mensagem = $"Ola, {name}"})
                : new BadRequestObjectResult(new { Error = "Informe um nome valido" });
        }
    }
}
