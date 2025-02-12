using Common.Repositories;
using DAL.Entities;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class RecetteService : BaseService, IRecetteRepository<Recette>
    {
        public RecetteService(IConfiguration config) : base(config, "Main-DB") { }

        public void Delete(Guid recette_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Recette_Delete";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(recette_id), recette_id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Recette> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Recette_GetAll";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader.ToRecette();
                        }
                    }
                }
            }
        }

        public Recette Get(Guid recette_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Recette_GetById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(recette_id), recette_id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.ToRecette();
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(recette_id));
                        }
                    }
                }
            }
        }

        public IEnumerable<Recette> GetFromUser(Guid user_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Recette_GetByUserId";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(user_id), user_id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader.ToRecette();
                        }
                    }
                }
            }
        }

        public Guid Insert(Recette recette)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Recette_Insert";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Recette.Titre), recette.Titre);
                    command.Parameters.AddWithValue(nameof(Recette.Description), (object?)recette.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue(nameof(Recette.Instructions), recette.Instructions);
                    command.Parameters.AddWithValue("user_id", (object?)recette.CreatedBy ?? DBNull.Value);
                    connection.Open();
                    return (Guid)command.ExecuteScalar();
                }
            }
        }

        public void Update(Guid recette_id, Recette recette)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_Recette_Update";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(recette_id), recette_id);
                    command.Parameters.AddWithValue(nameof(Recette.Titre), recette.Titre);
                    command.Parameters.AddWithValue(nameof(Recette.Description), (object?)recette.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue(nameof(Recette.Instructions), recette.Instructions);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
