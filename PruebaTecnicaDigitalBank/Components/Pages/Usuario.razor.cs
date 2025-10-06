using Microsoft.AspNetCore.Components;
using PruebaTecnicaDigitalBank.Model;
using PruebaTecnicaDigitalBank.Services;

namespace PruebaTecnicaDigitalBank.Components.Pages
{
    public partial class Usuario
    {
        [Inject]
        private UsuarioService UsuarioService { get; set; } = default!;

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Parameter]
        public int? IdUsuario { get; set; }

        private UsuarioData usuario = new();
        private bool mensajeGuardado = false;
        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            if (IdUsuario.HasValue)
            {
                isLoading = true;
                var usuarioExistente = await UsuarioService.ObtenerPorId(IdUsuario.Value);
                if (usuarioExistente != null)
                {
                    usuario = new UsuarioData
                    {
                        Id = usuarioExistente.Id,
                        Nombre = usuarioExistente.Nombre,
                        FechaNacimiento = usuarioExistente.FechaNacimiento,
                        Sexo = usuarioExistente.Sexo
                    };
                }
                isLoading = false;
            }
        }

        private async Task HandleValidSubmit()
        {
            if (usuario == null)
            {
                return;
            }

            isLoading = true;
            bool success = false;

            if (IdUsuario.HasValue)
            {
                success = await UsuarioService.Actualizar(usuario);
            }
            else
            {
                success = await UsuarioService.Agregar(usuario);
            }

            isLoading = false;

            if (success)
            {
                mensajeGuardado = true;
                
                // Redirigir a la página de consulta después de 1.5 segundos
                await Task.Delay(1500);
                Navigation.NavigateTo("/usuarios");
            }
        }
    }
}
