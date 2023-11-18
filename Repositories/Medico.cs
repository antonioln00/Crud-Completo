using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Repositories
{
    public class Medico
    {
        
        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;

        public Medico(string connectionString) 
        {
            
            _conn = new SqlConnection(connectionString);
            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
        }

        public async Task<List<Models.Medico>> GetAll()
        {
            List<Models.Medico> medicos = new List<Models.Medico>();

            using (_conn)
            {
                await _conn.OpenAsync(); 
                    
                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "select id, nome, crm, especialidade from medico;";
                    
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {
                        Models.Medico medico = new Models.Medico();
                        medico.Id = (int)dr["id"];
                        medico.Nome = (string)dr["nome"];
                        medico.CRM = (string)dr["crm"];
                        if (dr["especialidade"] != DBNull.Value)
                        {
                            medico.Especialidade = (string)dr["especialidade"];
                        }

                        medicos.Add(medico);
                    }
                }
            }

            return medicos;
        }

        public async Task<Models.Medico> GetById(int id)
        {
            Models.Medico medico = new Models.Medico();

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "select id, nome, crm, especialidade from medico where id = @id;";
                    _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    if (await dr.ReadAsync())
                    {
                        medico.Id = (int)dr["id"];
                        medico.Nome = (string)dr["nome"];
                        medico.CRM = (string)dr["crm"];
                        medico.Especialidade = (string)dr["especialidade"];
                    }
                }
            }

            return medico;
        }

        public async Task Add(Models.Medico medico)
        {
            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "insert into medico (nome, crm, especialidade) values (@nome, @crm, @especialidade); select convert(int, scope_identity())";
                    _cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = medico.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@crm", SqlDbType.VarChar)).Value = medico.CRM;
                    _cmd.Parameters.Add(new SqlParameter("@especialidade", SqlDbType.VarChar)).Value = medico.Especialidade;

                   medico.Id = (int) await _cmd.ExecuteScalarAsync();
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas = 0;

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "delete from medico where id = @id";
                    _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;


                    linhasAfetadas = await _cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas > 0;
        }

        public async Task<bool> Update(Models.Medico medico)
        {
            int linhasAfetadas = 0;

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "update medico set nome = @nome, crm = @crm, especialidade = @especialidade where id = @id";
                    _cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = medico.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@crm", SqlDbType.VarChar)).Value = medico.CRM;
                    _cmd.Parameters.Add(new SqlParameter("@especialidade", SqlDbType.VarChar)).Value = medico.Especialidade;

                    _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = medico.Id;

                    linhasAfetadas = await _cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas > 0;
        }
    }
}