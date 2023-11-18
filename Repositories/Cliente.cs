using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Repositories
{
    public class Cliente
    {
        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;


        public Cliente(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
        }

        public async Task<List<Models.Cliente>> GetAll()
        {
            List<Models.Cliente> clientes = new List<Models.Cliente>();

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "select id, nome, datanascimento from cliente";
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {
                        Models.Cliente cliente = new Models.Cliente();
                        cliente.Id = Convert.ToInt32(dr["id"]);
                        cliente.Nome = dr["nome"].ToString();

                        if (!(dr["datanascimento"] is DBNull))
                        {
                            cliente.DataNascimento = (DateTime)dr["datanascimento"];
                        }

                        clientes.Add(cliente);
                    }
                }
            }

            return clientes;
        }

        public async Task<Models.Cliente> GetById(int id)
        {
            Models.Cliente cliente = new Models.Cliente();

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "select id, nome, datanascimento from cliente where id = @id;";
                    _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {
                        cliente.Id = Convert.ToInt32(dr["id"]);
                        cliente.Nome = dr["nome"].ToString();
                        if (dr["datanascimento"] is DBNull)
                            cliente.DataNascimento = null;
                        else
                            cliente.DataNascimento = (DateTime)dr["datanascimento"];
                    }
                }
            }

            return cliente;
        }

        public async Task Add(Models.Cliente cliente)
        {
            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "insert into cliente (nome, datanascimento) values (@nome, @datanascimento); select convert(int, scope_identity())";
                    _cmd.Parameters.Add(new SqlParameter("nome", SqlDbType.VarChar)).Value = cliente.Nome;
                    if (cliente.DataNascimento != null)
                    {
                        _cmd.Parameters.Add(new SqlParameter("datanascimento", SqlDbType.Date)).Value = cliente.DataNascimento;
                    } else
                    {
                        _cmd.Parameters.Add(new SqlParameter("datanascimento", SqlDbType.Date)).Value = DBNull.Value;
                    }
                    cliente.Id = (int) await _cmd.ExecuteScalarAsync();
                }
            }
        }

        public async Task<bool> Update(Models.Cliente cliente)
        {
            int linhasAfetadas = 0;

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "update cliente set nome = @nome, datanascimento = @datanascimento where id = @id;";
                    _cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int)).Value = cliente.Id;
                    _cmd.Parameters.Add(new SqlParameter("nome", SqlDbType.VarChar)).Value = cliente.Nome;
                    if (cliente.DataNascimento != null)
                    {
                        _cmd.Parameters.Add(new SqlParameter("datanascimento", SqlDbType.Date)).Value = cliente.DataNascimento;
                    }
                    else
                    {
                        _cmd.Parameters.Add(new SqlParameter("datanascimento", SqlDbType.Date)).Value = DBNull.Value;
                    }
                    linhasAfetadas = await _cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas > 0;
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas = 0;

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "delete from cliente where id = @id";
                    _cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int)).Value = id;
                    
                    linhasAfetadas = await _cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas > 0;
        }
    }
}