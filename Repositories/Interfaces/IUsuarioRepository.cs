using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositories.Interfaces
{
	public interface IUsuarioRepository
	{
		Task<List<UsuarioModel>>BuscarTodosUsuarios();
		Task<UsuarioModel>BuscarPorID(int id);
		Task<UsuarioModel>Adicionar(UsuarioModel model);
		Task<UsuarioModel>Atualizar(UsuarioModel model, int id);
		Task<bool> Apagar(int id);
	}
}
