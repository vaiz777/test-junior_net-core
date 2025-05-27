// Data/SiswaRepository.cs
using MySql.Data.MySqlClient;
using System.Data;
using ExportApp.Models;

namespace ExportApp.Data
{
    public class SiswaRepository : ISiswaRepository
    {
        private readonly string _connectionString;

        public SiswaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public List<Siswa> GetRanking()
        {
            var result = new List<Siswa>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("GetSiswaRanking", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Siswa
                {
                    Id = reader.GetInt32("Id"),
                    Nama = reader.GetString("Nama"),
                    Nilai = reader.GetInt32("Nilai"),
                    RankNilai = reader.GetInt32("RankNilai")
                });
            }

            return result;
        }
    }
}
