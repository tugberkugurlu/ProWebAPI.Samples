using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace AsyncDatabaseCall.Models {

    public static class Extensions {

        public static IEnumerable<T> Select<T>(
            this SqlDataReader reader, Func<SqlDataReader, T> projection) {

            while (reader.Read()) {
                yield return projection(reader);
            }
        }
    }
}