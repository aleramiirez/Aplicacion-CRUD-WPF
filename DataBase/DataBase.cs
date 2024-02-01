using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2Evaluacion.DataBase
{
    public class DataBase
    {
        
        public static MySqlConnection conexion =
            new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=root;database=northwind");
        //ACUERDATE DE CAMBIAR EL PUERTO DEL 3307 AL 3306
    }
}
