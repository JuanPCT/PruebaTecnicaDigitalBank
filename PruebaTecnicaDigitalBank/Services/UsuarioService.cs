using PruebaTecnicaDigitalBank.Model;
using System.Net.Http.Json;

namespace PruebaTecnicaDigitalBank.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UsuarioService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7098/api";
        }

        public async Task<List<UsuarioData>> ObtenerTodos()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/usuario");
                response.EnsureSuccessStatusCode();
                var usuarios = await response.Content.ReadFromJsonAsync<List<UsuarioData>>();
                return usuarios ?? new List<UsuarioData>();
            }
            catch (Exception)
            {
                return new List<UsuarioData>();
            }
        }

        public async Task<UsuarioData?> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/usuario/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UsuarioData>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Agregar(UsuarioData usuario)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/usuario", usuario);
                response.EnsureSuccessStatusCode();
                var resultado = await response.Content.ReadFromJsonAsync<UsuarioData>();
                if (resultado != null)
                {
                    usuario.Id = resultado.Id;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Actualizar(UsuarioData usuario)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/usuario/{usuario.Id}", usuario);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/usuario/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}