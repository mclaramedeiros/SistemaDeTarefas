using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Controllers
{
		[Route("api/[controller]")]
		[ApiController]
	public class UsuarioController : Controller
	{
		private readonly IUsuarioRepository _usuarioRepository;
		public UsuarioController(IUsuarioRepository usuarioRepository)
		{
			_usuarioRepository = usuarioRepository;
		}
		[HttpGet]
		public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios() 
		{
			List<UsuarioModel> usuarios = await _usuarioRepository.BuscarTodosUsuarios();
			return Ok(usuarios);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<UsuarioModel>> BuscarPorID(int id)
		{
			UsuarioModel usuario = await _usuarioRepository.BuscarPorID(id);
			return Ok(usuario);
		}
		[HttpPost]
		public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuario)
		{
			await _usuarioRepository.Adicionar(usuario);
			return Ok(usuario);
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
		{
			usuarioModel.Id = id;
			UsuarioModel usuario = await _usuarioRepository.Atualizar(usuarioModel, id);
			return Ok(usuario);
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult<UsuarioModel>> Deletar(int id)
		{
			return Ok(await _usuarioRepository.Apagar(id));
		}
	}
}
