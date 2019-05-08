using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_AccesoDatos;
using Tienda_Entidades;

namespace Tienda_LogicaNegocio
{
    public class VehiculoBol
    {
        //Instanciamos nuestra clase ProductoDal para poder utilizar sus miembros
        private VehiculoDal _vehiculoDal = new VehiculoDal();
        //
        //El uso de la clase StringBuilder nos ayudara a devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        //
        //Creamos nuestro método para Insertar un nuevo Producto, observe como este método tampoco valida los el contenido
        //de las propiedades, sino que manda a llamar a una Función que tiene como tarea única hacer esta validación
        //
        public void Registrar(EVehiculo vechiculo)
        {
            if (ValidarVehiculo(vechiculo))
            {
                if (_vehiculoDal.GetBycodigo(vechiculo.Codigo) == null)
                {
                    _vehiculoDal.Insert(vechiculo);
                }
                else
                    _vehiculoDal.Update(vechiculo);

            }
        }

        public List<EVehiculo> Todos()
        {
            return _vehiculoDal.GetAll();
        }

        public EVehiculo TraerPorId(int codigoVehiculo)
        {
            stringBuilder.Clear();

            if (codigoVehiculo == 0) stringBuilder.Append("Por favor proporcione un valor de Codigo valido");

            if (stringBuilder.Length == 0)
            {
                return _vehiculoDal.GetBycodigo(codigoVehiculo);
            }
            return null;
        }

        public void Eliminar(int idProduct)
        {
            stringBuilder.Clear();

            if (idProduct == 0) stringBuilder.Append("Por favor proporcione un valor de codigo valido");

            if (stringBuilder.Length == 0)
            {
                _vehiculoDal.Delete(idProduct);
            }
        }

        private bool ValidarVehiculo(EVehiculo vehiculo)
        {
            stringBuilder.Clear();
            
            if (string.IsNullOrEmpty(vehiculo.Descripcion)) stringBuilder.Append("El campo Descripción es obligatorio");
            if (string.IsNullOrEmpty(vehiculo.Modelo)) stringBuilder.Append(Environment.NewLine + "El campo Modelo es obligatorio");
            // if (producto.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            if (string.IsNullOrEmpty(vehiculo.Marca)) stringBuilder.Append(Environment.NewLine + "El campo Marca es obligatorio");

            return stringBuilder.Length == 0;
        }
    }
}
