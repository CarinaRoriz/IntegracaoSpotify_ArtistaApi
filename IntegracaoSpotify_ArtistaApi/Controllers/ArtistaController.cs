using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IntegracaoSpotify_ArtistaApi.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntegracaoSpotify_ArtistaApi.Controllers
{
    [Route("v1/artistas")]
    [ApiController]
    public class ArtistaController : ControllerBase
    {
        [HttpGet("busca")]
        public async Task<ActionResult<BuscaArtistas>> GetArtista(string nomeArtista)
        {
            string token = Request.Headers["Authorization"];

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            UriBuilder builder = new UriBuilder("https://api.spotify.com/v1/search");
            builder.Query = $"q={nomeArtista}&type=artist";

            HttpResponseMessage resp = await client.GetAsync(builder.Uri);
            string msg = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BuscaArtistas>(msg.Replace("\n", ""));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artistas>> GetArtistaPorId(string id)
        {
            string token = Request.Headers["Authorization"];

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            UriBuilder builder = new UriBuilder($"https://api.spotify.com/v1/artists/{id}");

            HttpResponseMessage resp = await client.GetAsync(builder.Uri);
            string msg = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Artistas>(msg.Replace("\n", ""));
        }

        [HttpGet("{id}/albuns")]
        public async Task<ActionResult<Albuns>> GetAlbunsPorArtista(string id)
        {
            string token = Request.Headers["Authorization"];

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            UriBuilder builder = new UriBuilder($"https://api.spotify.com/v1/artists/{id}/albums");

            HttpResponseMessage resp = await client.GetAsync(builder.Uri);
            string msg = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Albuns>(msg.Replace("\n", ""));
        }
    }
}