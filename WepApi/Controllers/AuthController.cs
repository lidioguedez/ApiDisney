using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Errors.Model;
using System.Text;
using WebApi.Dtos;
using WebApi.Errors;
using WebApi.Extensions;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ISendGridEnviar _sendGridEnviar;
       
        public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService, ISendGridEnviar sendGridEnviar, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _sendGridEnviar = sendGridEnviar;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

            if (usuario == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, false);

            if (!resultado.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401, "Nombre de Usuario o Contraseña incorrectas"));
            }

            var roles = await _userManager.GetRolesAsync(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _tokenService.CreateToken(usuario, roles),
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Imagen = usuario.Imagen,
                Admin = roles.Contains("ADMIN") ? true : false
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto registrarDto)
        {
            var user = await _userManager.FindByEmailAsync(registrarDto.Email);


            if (user != null)
            {
                return BadRequest("El Email ya esta registrado.");
            }

            var usuario = new Usuario
            {
                Email = registrarDto.Email,
                UserName = registrarDto.Username,
                Nombre = registrarDto.Nombre,
                Apellido = registrarDto.Apellido,
                Imagen = registrarDto.Imagen
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);

            if (!resultado.Succeeded)
            {
                string err = "";

                foreach (var e in resultado.Errors)
                {
                    err = err + e.Code + " ";
                }

                return BadRequest(new CodeErrorResponse(400, err));
            }

            var dataEmail = new SendGridEnt();
            var roles = await _userManager.GetRolesAsync(usuario);

            dataEmail.EmailDestinatario = usuario.Email;
            dataEmail.NombreDestinatario = usuario.Nombre + " " + usuario.Apellido;
            dataEmail.Nombre = usuario.Nombre;
            dataEmail.Token = _tokenService.CreateToken(usuario, roles);

            _sendGridEnviar.EnviarEmail(dataEmail);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Token = dataEmail.Token,
                Email = usuario.Email,
                Imagen = usuario.Imagen,
                Username = usuario.UserName,
                Admin = false
            };

        }

        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {

            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(usuario);           

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Imagen = usuario.Imagen,
                Token = _tokenService.CreateToken(usuario, roles),
                Admin = roles.Contains("ADMIN") ? true : false
            };
        }
        
    }
}
