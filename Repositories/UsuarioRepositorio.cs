using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
	public class UsuarioRepositorio : IUsuarioRepository
	{
		private readonly SistemaTarefasDBContex _DbContex;
		public UsuarioRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
		{
			_DbContex = sistemaTarefasDBContex;
		}
		public async Task<UsuarioModel> BuscarPorID(int id)
		{
			return await _DbContex.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
		{
			return await _DbContex.Usuarios.ToListAsync();
		}
		public async Task<UsuarioModel> Adicionar(UsuarioModel model)
		{
			await _DbContex.Usuarios.AddAsync(model);
			await _DbContex.SaveChangesAsync();

			return model;

		}

		public async Task<bool> Apagar(int id)
		{
			UsuarioModel userId = await BuscarPorID(id);
			if (userId == null)
			{
				throw new Exception($"Usuario para o ID {id} não foi encontrado no banco");
			}

			_DbContex.Usuarios.Remove(userId);
			await _DbContex.SaveChangesAsync();
			return true;
		}

		public async Task<UsuarioModel> Atualizar(UsuarioModel model, int id)
		{
			UsuarioModel userId = await BuscarPorID(id);
			if(userId == null)
			{
				throw new Exception($"Usuario para o ID {id} não foi encontrado no banco");
			}
			userId.Nome = model.Nome;
			userId.Email = model.Email;
			_DbContex.Usuarios.Update(userId);
			await _DbContex.SaveChangesAsync();

			return userId;
		}

	}
}
