using LibrosGMP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace LibrosGMP.Conexiones
{
    public class Conexion
    {
        string path;
       
        SQLite.SQLiteConnection conn;

        public Conexion()
        {

        }

        public async Task<bool> compruebaSiExisteBD(string nombreBD)
        {
            bool bdExiste = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(nombreBD);
            }
            catch (Exception)
            {
                bdExiste = false;
            }

            return bdExiste;
        }
      
        public void CreateDatabase()
        {
            getConnection();

            conn.CreateTable<Libro>();
            conn.Close();
        }

        public void addPrimerosLibros()
        {
            var listaLibros = new List<Libro>()
            {
                new Libro()
                {
                    Nombre = "El camino",
                    Telefono = "669044233",
                    TLibro = "1224356264545278",
                    Accion = "Regalar"
                },

                new Libro()
                {
                    Nombre = "El quijote",
                    Telefono = "669044233",
                    TLibro = "1224356264545278",
                    Accion = "Prestar"
                }
            };

            getConnection();

            conn.InsertAll(listaLibros);

            conn.Close();
        }

        public ObservableCollection<Libro> LeerLibros()
        {
            getConnection();

            var query = conn.Table<Libro>();

            ObservableCollection<Libro> ListaDeLibros = new ObservableCollection<Libro>(query.ToList<Libro>());

            return ListaDeLibros;
        }

        public void addLibro(Libro libro)
        {
            getConnection();

            conn.Insert(libro);

            conn.Close();
        }

        public void borrarLibro(int Identificador)
        {
            getConnection();

            var libro = conn.Table<Libro>().Where(x => x.Id == Identificador).FirstOrDefault();
            if (libro != null)
            {
                conn.Delete(libro);
            }
        }

        public void modificarLibro(Libro newLibro)
        {
            getConnection();

            var libro = conn.Table<Libro>().Where(x => x.Id == newLibro.Id).FirstOrDefault();
            if (libro != null)
            {
                conn.Update(newLibro);
            }
        }

        private void getConnection()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Libro.db");

            conn = new SQLite.SQLiteConnection(path);
        }
    }
}
