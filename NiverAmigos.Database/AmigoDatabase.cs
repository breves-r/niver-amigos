using NiverAmigos.Entidade;
using System.Data;
using System.Data.SqlClient;

namespace NiverAmigos.Database
{
    public class AmigoDatabase
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";

        public List<Amigo> ObterTodos()
        {
            List<Amigo> result = new List<Amigo>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Amigos ORDER BY datepart(dy,dateadd(d,- DATEPART(dy, getdate()),Aniversario ))";

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var data = command.ExecuteReader();

                if(data.HasRows)
                {
                    while(data.Read())
                    {
                        Amigo amigo = new Amigo();
                        amigo.Id = Convert.ToInt32(data["Id"].ToString());
                        amigo.Nome = data["Nome"].ToString();
                        amigo.Sobrenome = data["Sobrenome"].ToString();
                        amigo.Aniversario = Convert.ToDateTime(data["Aniversario"].ToString());

                        result.Add(amigo);
                    }
                }

                connection.Close();

                return result;
            }

        }

        public List<Amigo> ObterAniversariantes()
        {
            List<Amigo> result = new List<Amigo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Amigos";
                var hoje = DateTime.Now;

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var data = command.ExecuteReader();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        var niver = Convert.ToDateTime(data["Aniversario"].ToString());

                        if (hoje.Day == niver.Day && hoje.Month == niver.Month) 
                        {
                            Amigo amigo = new Amigo();
                            amigo.Id = Convert.ToInt32(data["Id"].ToString());
                            amigo.Nome = data["Nome"].ToString();
                            amigo.Sobrenome = data["Sobrenome"].ToString();
                            amigo.Aniversario = Convert.ToDateTime(data["Aniversario"].ToString());

                            result.Add(amigo);
                        }
                    }
                }

                connection.Close();

                return result;
            }

        }

        public Amigo ObterPorId(int id)
        {
            Amigo result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 1 or 1=1
                var sql = $"SELECT * FROM Amigos WHERE Id = @Param1;";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var param = new SqlParameter("@Param1", SqlDbType.Int);
                param.Value = id;
                command.Parameters.Add(param);

                var data = command.ExecuteReader();

                if (data.HasRows)
                {
                    if (data.Read())
                    {
                        result = new Amigo();
                        result.Id = Convert.ToInt32(data["Id"].ToString());
                        result.Nome = data["Nome"].ToString();
                        result.Sobrenome = data["Sobrenome"].ToString();
                        result.Aniversario = Convert.ToDateTime(data["Aniversario"].ToString());
                    }
                }

                connection.Close();

                return result;
            }
        }

        public List<Amigo> ObterPorNome(string nome)
        {
            List<Amigo> result = new List<Amigo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = $"SELECT * FROM Amigos WHERE UPPER(Nome) = '{nome.ToUpper()}' OR UPPER(Sobrenome) = '{nome.ToUpper()}'";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var data = command.ExecuteReader();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        Amigo amigo = new Amigo();
                        amigo.Id = Convert.ToInt32(data["Id"].ToString());
                        amigo.Nome = data["Nome"].ToString();
                        amigo.Sobrenome = data["Sobrenome"].ToString();
                        amigo.Aniversario = Convert.ToDateTime(data["Aniversario"].ToString());

                        result.Add(amigo);
                    }
                }

                connection.Close();

                return result;
            }
        }

        public void Salvar(Amigo amigo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = @$"INSERT INTO Amigos(Nome, Sobrenome, Aniversario)
                             VALUES(@P1, @P2, @P3);   
                            ";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var p1 = new SqlParameter("@P1", SqlDbType.VarChar);
                p1.Value = amigo.Nome;
                command.Parameters.Add(p1);

                var p2 = new SqlParameter("@P2", SqlDbType.VarChar);
                p2.Value = amigo.Sobrenome;
                command.Parameters.Add(p2);

                var p3 = new SqlParameter("@P3", SqlDbType.DateTime);
                p3.Value = amigo.Aniversario;
                command.Parameters.Add(p3);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(Amigo amigo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = @$"UPDATE Amigos SET 
                            Nome = @P1, Sobrenome = @P2, Aniversario = @P3
                            WHERE Id = @P4;   
                            ";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var p1 = new SqlParameter("@P1", SqlDbType.VarChar);
                p1.Value = amigo.Nome;
                command.Parameters.Add(p1);

                var p2 = new SqlParameter("@P2", SqlDbType.VarChar);
                p2.Value = amigo.Sobrenome;
                command.Parameters.Add(p2);

                var p3 = new SqlParameter("@P3", SqlDbType.DateTime);
                p3.Value = amigo.Aniversario;
                command.Parameters.Add(p3);

                var p4 = new SqlParameter("@P4", SqlDbType.Int);
                p4.Value = amigo.Id;
                command.Parameters.Add(p4);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = @$"DELETE FROM Amigos WHERE Id = @P1;   
                            ";

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                var param = new SqlParameter("@P1", SqlDbType.Int);
                param.Value = id;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}