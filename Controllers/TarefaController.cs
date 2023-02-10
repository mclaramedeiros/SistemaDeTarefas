using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Controllers
{
		[Route("api/[controller]")]
		[ApiController]
	public class TarefaController : Controller
	{
		private readonly ITarefaRepository _tarefaRepository;
		public TarefaController(ITarefaRepository tarefaRepository)
		{
			_tarefaRepository = tarefaRepository;
		}
		[HttpGet]
		public async Task<ActionResult<List<TarefaModel>>> ListarTodasTarefas() 
		{
			List<TarefaModel> tarefas = await _tarefaRepository.BuscarTodasTarefas();
			return Ok(tarefas);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<TarefaModel>> BuscarPorID(int id)
		{
			TarefaModel tarefa = await _tarefaRepository.BuscarPorID(id);
			return Ok(tarefa);
		}
		[HttpPost]
		public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefa)
		{
			await _tarefaRepository.Adicionar(tarefa);
			return Ok(tarefa);
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int id)
		{
			tarefaModel.Id = id;
			
			TarefaModel tarefa = await _tarefaRepository.Atualizar(tarefaModel, id);
			return Ok(tarefa);
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult<TarefaModel>> Deletar(int id)
		{
			return Ok(await _tarefaRepository.Apagar(id));
		}
	}
}
