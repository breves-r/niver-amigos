using NiverAmigos.Database;
using NiverAmigos.Entidade;

namespace NiverAmigos.Application
{
    public class AmigoManager
    {
        AmigoDatabase db = new AmigoDatabase();

        public List<Amigo> ObterTodos()
        {
            return db.ObterTodos();
        }

        public List<Amigo> ObterAniversariantes()
        {
            return db.ObterAniversariantes();
        }

        public Amigo ObterPorId(int id)
        {
            return db.ObterPorId(id);
        }

        public void Salvar(Amigo amigo)
        {
            db.Salvar(amigo);
        }

        public void Atualizar(Amigo amigo)
        {
            db.Update(amigo);
        }
        public void Excluir(int id)
        {
            db.Delete(id);
        }
        public List<Amigo> Search(string nome)
        {
            return db.ObterPorNome(nome);
        }
    }
}