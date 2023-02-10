using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
	public class TarefaRepositorio : ITarefaRepository
	{
		private readonly SistemaTarefasDBContex _DbContex;
		public TarefaRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
		{
			_DbContex = sistemaTarefasDBContex;
		}
		public async Task<List<TarefaModel>> BuscarTodasTarefas()
		{
			return await _DbContex.Tarefas
				.Include(x => x.Usuario)
				.ToListAsync();
		}

		public async Task<TarefaModel> BuscarPorID(int id)
		{
			return await _DbContex.Tarefas
				.Include(x => x.Usuario)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
		{
			await _DbContex.Tarefas.AddAsync(tarefa);
			await _DbContex.SaveChangesAsync();

			return tarefa;
		}

		public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
		{
			TarefaModel tarefaPorId = await BuscarPorID(id);
			if(tarefa == null)
			{
				throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
			}
			tarefaPorId.Name = tarefa.Name;
			tarefaPorId.Descricao = tarefa.Descricao;
			tarefaPorId.Status = tarefa.Status;
			tarefaPorId.UsuarioId = tarefa.UsuarioId;

			_DbContex.Tarefas.Update(tarefaPorId);
			await _DbContex.SaveChangesAsync();

			return tarefaPorId;

		}
		public async Task<bool> Apagar(int id)
		{
			TarefaModel tarefa = await BuscarPorID(id);
			if(tarefa == null)
			{
				throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
			}

			_DbContex.Tarefas.Remove(tarefa);
			await _DbContex.SaveChangesAsync();
			return true;
		}
	}
}
