using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Entidades;

namespace Tienda_AccesoDatos
{
    public class VehiculoDal
    {
       

        public void Insert(EVehiculo vehiculo)
        {
            
            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ToString()))
            {
                cnx.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Vehiculo (Descripcion, Modelo, Marca) VALUES (@descripcion, @modelo, @marca)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    //El primero de los cambios significativos con respecto al ejemplo descargado es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EProducto de nuestra capa
                    //de entidades...
                    cmd.Parameters.AddWithValue("@descripcion", vehiculo.Descripcion);
                    cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                    cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);

                    cmd.ExecuteNonQuery();
                }
            }
        }

       
        public List<EVehiculo> GetAll()
        {
            
            List<EVehiculo> vehiculos = new List<EVehiculo>();

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ToString()))
            {
                cnx.Open();

                const string sqlQuery = "SELECT * FROM Vehiculo ORDER BY Codigo ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //chek
                        //Instanciamos al objeto Eproducto para llenar sus propiedades
                        EVehiculo Vehiculo = new EVehiculo
                        {
                            Codigo = Convert.ToInt32(dataReader["Codigo"]),
                            Descripcion = Convert.ToString(dataReader["Descripcion"]),
                            Modelo = Convert.ToString(dataReader["Modelo"]),
                            Marca = Convert.ToString(dataReader["Marca"])
                        };
                        //
                        //Insertamos el objeto Producto dentro de la lista Productos
                        vehiculos.Add(Vehiculo);
                    }
                }
            }
            return vehiculos;
        }

      
        public EVehiculo GetBycodigo(int codigoVehiculo)
        {
            //var cadena = ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ConnectionString))
            {
                cnx.Open();
                
                const string sqlGetById = "SELECT * FROM Vehiculo WHERE Codigo = @codigo";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnx))
                {
                    //
                    //Utilizamos el valor del parámetro idProducto para enviarlo al parámetro declarado en la consulta
                    //de selección SQL
                    cmd.Parameters.AddWithValue("@codigo", codigoVehiculo);
                    //
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EVehiculo Vehiculo = new EVehiculo
                        {
                           Codigo = Convert.ToInt32(dataReader["Codigo"]),
                            Descripcion = Convert.ToString(dataReader["Descripcion"]),
                            Modelo = Convert.ToString(dataReader["Modelo"]),
                            Marca = Convert.ToString(dataReader["Marca"])
                        };

                        return Vehiculo;
                    }
                }
            }

            return null;
        }


      // check
        public void Update(EVehiculo Vehiculo)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ToString()))
            {
                cnx.Open();
                const string sqlQuery =
                    "UPDATE Vehiculo SET Descripcion = @descripcion, Modelo = @modelo, Marca = @marca WHERE Codigo= @codigo";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    cmd.Parameters.AddWithValue("@descripcion", Vehiculo.Descripcion);
                    cmd.Parameters.AddWithValue("@modelo", Vehiculo.Modelo);
                    cmd.Parameters.AddWithValue("@marca", Vehiculo.Marca);
                    cmd.Parameters.AddWithValue("@codigo", Vehiculo.Codigo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //check
        public void Delete(int codigovehiculo)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["miPrimeraVezConnectionString"].ToString()))
            {
                cnx.Open();
                const string sqlQuery = "DELETE FROM Vehiculo WHERE Codigo = @codigo";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigovehiculo);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
