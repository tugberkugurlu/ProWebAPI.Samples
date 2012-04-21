using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace AsyncDatabaseCall.Models {

    public class GalleryContext : IGalleryContext {

        private readonly string selectStatement = "SELECT * FROM Cars";
        private readonly string spName = "sp$GetCars";

        public IEnumerable<Car> GetCars() {

            var connectionString = ConfigurationManager.ConnectionStrings["CarGalleryConnStr"].ConnectionString;

            using (var conn = new SqlConnection(connectionString)) {
                using (var cmd = new SqlCommand()) {

                    cmd.Connection = conn;
                    cmd.CommandText = selectStatement;
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader()) {

                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }

        public async Task<IEnumerable<Car>> GetCarsAsync() {

            var connectionString = ConfigurationManager.ConnectionStrings["CarGalleryConnStr"].ConnectionString;
            var asyncConnectionString = new SqlConnectionStringBuilder(connectionString) {
                AsynchronousProcessing = true
            }.ToString();

            using (var conn = new SqlConnection(asyncConnectionString)) {
                using (var cmd = new SqlCommand()) {

                    cmd.Connection = conn;
                    cmd.CommandText = selectStatement;
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (var reader = await cmd.ExecuteReaderAsync()) {

                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }

        public IEnumerable<Car> GetCarsViaSP() {

            var connectionString = ConfigurationManager.ConnectionStrings["CarGalleryConnStr"].ConnectionString;

            using (var conn = new SqlConnection(connectionString)) {
                using (var cmd = new SqlCommand()) {

                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader()) {

                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }

        public async Task<IEnumerable<Car>> GetCarsViaSPAsync() {

            var connectionString = ConfigurationManager.ConnectionStrings["CarGalleryConnStr"].ConnectionString;
            var asyncConnectionString = new SqlConnectionStringBuilder(connectionString) {
                AsynchronousProcessing = true
            }.ToString();

            using (var conn = new SqlConnection(asyncConnectionString)) {
                using (var cmd = new SqlCommand()) {

                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        
                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }

        //private helpers

        private Car carBuilder(SqlDataReader reader) {

            return new Car {

                Id = int.Parse(reader["Id"].ToString()),
                Make = reader["Make"] is DBNull ? null : reader["Make"].ToString(),
                Model = reader["Model"] is DBNull ? null : reader["Model"].ToString(),
                Year = int.Parse(reader["Year"].ToString()),
                Price = float.Parse(reader["Price"].ToString()),
            };
        }
    }
}