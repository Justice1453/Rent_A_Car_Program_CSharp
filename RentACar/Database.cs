using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RentACar
{
    class Database
    {
        public SqlConnection baglanti = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=RentACar;Trusted_Connection=True;");
    }
}
