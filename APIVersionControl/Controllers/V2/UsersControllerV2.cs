using APIVersionControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIVersionControl.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";
        private const string AppID = "63e54492725d260772f072c0";
        //Instacioa de http cliente ara peticiones http asincronas
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //ruta 1
        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetUsersData")]
        public async Task<IActionResult> GetUsersDataAsync()
        {
            //Limpiar cabeceras
            _httpClient.DefaultRequestHeaders.Clear();

            //solicitud añadiendo la cabecera
            _httpClient.DefaultRequestHeaders.Add("app-id", AppID);

            var response = await _httpClient.GetStreamAsync(ApiTestURL);//Lista de usuarios

            //Serializar JSON para pasar data al cliente
            var usersData = await JsonSerializer.DeserializeAsync<UsersResponseData>(response);

            //Limitar envio de informacion, lista de usuarios
            var users = usersData?.data;

            return Ok(users);

        }

    }
}
