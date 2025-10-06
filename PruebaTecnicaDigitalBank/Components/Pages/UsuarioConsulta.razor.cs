using Microsoft.AspNetCore.Components;
using PruebaTecnicaDigitalBank.Model;
using PruebaTecnicaDigitalBank.Services;

namespace PruebaTecnicaDigitalBank.Components.Pages
{
    public partial class UsuarioConsulta
    {
        [Inject]
        private UsuarioService UsuarioService { get; set; } = default!;

        private List<UsuarioData> usuarios = new();
        private bool isLoading = false;
        private bool mostrarConfirmacion = false;
        private int usuarioAEliminarId = 0;
        private string usuarioAEliminarNombre = string.Empty;
        private string mensaje = string.Empty;
        private bool mensajeExito = false;

        protected override async Task OnInitializedAsync()
        {
            await CargarUsuarios();
        }

        private async Task CargarUsuarios()
        {
            isLoading = true;
            usuarios = await UsuarioService.ObtenerTodos();
            isLoading = false;
        }

        private void ConfirmarEliminar(int id, string nombre)
        {
            usuarioAEliminarId = id;
            usuarioAEliminarNombre = nombre;
            mostrarConfirmacion = true;
        }

        private void CancelarEliminar()
        {
            usuarioAEliminarId = 0;
            usuarioAEliminarNombre = string.Empty;
            mostrarConfirmacion = false;
        }

        private async Task EliminarUsuario()
        {
            mostrarConfirmacion = false;
            
            var exito = await UsuarioService.Eliminar(usuarioAEliminarId);
            
            if (exito)
            {
                mensaje = $"Usuario {usuarioAEliminarNombre} eliminado correctamente";
                mensajeExito = true;
                await CargarUsuarios();
            }
            else
            {
                mensaje = $"Error al eliminar el usuario {usuarioAEliminarNombre}";
                mensajeExito = false;
            }

            usuarioAEliminarId = 0;
            usuarioAEliminarNombre = string.Empty;

            // Limpiar el mensaje después de 3 segundos
            await Task.Delay(3000);
            mensaje = string.Empty;
        }
    }
}
