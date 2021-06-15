using LibrosGMP.Conexiones;
using LibrosGMP.Models;
using LibrosGMP.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace LibrosGMP.ViewModels
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private Conexion conexionDeDatos;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<Libro> listaLibros;
        public ObservableCollection<Libro> ListaLibros
        {
            get { return listaLibros; }
            set
            {
                listaLibros = value;
                OnPropertyChanged("ListaLibros");
            }
        }

        private string nombre1;
        public string Nombre1
        {
            get { return nombre1; }
            set
            {
                nombre1 = value;
                OnPropertyChanged("Nombre1");
            }
        }

        private string telefono1;
        public string Telefono1
        {
            get { return telefono1; }
            set
            {
                telefono1 = value;
                OnPropertyChanged("Telefono1");
            }
        }

        private string libro1;
        public string Libro1
        {
            get { return libro1; }
            set
            {
                libro1 = value;
                OnPropertyChanged("Libro1");
            }
        }

        private string accion1;
        public string Accion1
        {
            get { return accion1; }
            set
            {
                accion1 = value;
                OnPropertyChanged("Accion1");
            }
        }

        private string nombre2;
        public string Nombre2
        {
            get { return nombre2; }
            set
            {
                nombre2 = value;
                OnPropertyChanged("Nombre2");
            }
        }

        private string telefono2;
        public string Telefono2
        {
            get { return telefono2; }
            set
            {
                telefono2 = value;
                OnPropertyChanged("Telefono2");
            }
        }

        private string libro2;
        public string Libro2
        {
            get { return libro2; }
            set
            {
                libro2 = value;
                OnPropertyChanged("Libro2");
            }
        }

        private string accion2;
        public string Accion2
        {
            get { return accion2; }
            set
            {
                accion2 = value;
                OnPropertyChanged("Accion2");
            }
        }

        private bool activarEdicion;
        public bool ActivarEdicion
        {
            get { return activarEdicion; }
            set
            {
                activarEdicion = value;
                OnPropertyChanged("ActivarEdicion");
            }
        }

        private bool activarEdicionEditar;
        public bool ActivarEdicionEditar
        {
            get { return activarEdicionEditar; }
            set
            {
                activarEdicionEditar = value;
                OnPropertyChanged("ActivarEdicionEditar");
            }
        }

        private bool activarEdicionActualizar;
        public bool ActivarEdicionActualizar
        {
            get { return activarEdicionActualizar; }
            set
            {
                activarEdicionActualizar = value;
                OnPropertyChanged("ActivarEdicionActualizar");
            }
        }

        private bool activarEdicionEliminar;
        public bool ActivarEdicionEliminar
        {
            get { return activarEdicionEliminar; }
            set
            {
                activarEdicionEliminar = value;
                OnPropertyChanged("ActivarEdicionEliminar");
            }
        }

        private Libro libroSeleccionado;
        public Libro LibroSeleccionado
        {
            get { return libroSeleccionado; }
            set
            {
                libroSeleccionado = value;
                OnPropertyChanged("ContactoSeleccionado");

                if (libroSeleccionado == null)
                {
                    ActivarEdicion = false;
                    ActivarEdicionEditar = false;
                    ActivarEdicionActualizar = false;
                    ActivarEdicionEliminar = false;
                }
                else
                {
                    ActivarEdicion = false;
                    ActivarEdicionEditar = true;
                    ActivarEdicionActualizar = false;
                    ActivarEdicionEliminar = true;
                    Nombre1 = libroSeleccionado.Nombre;
                    Telefono1 = libroSeleccionado.Telefono;
                    Libro1 = libroSeleccionado.TLibro;
                    Accion1 = libroSeleccionado.Accion;
                }
            }
        }

        public ICommand ComandoEditar { get; set; }
        public ICommand ComandoActualizar { get; set; }
        public ICommand ComandoEliminar { get; set; }
        public ICommand ComandoGuardar { get; set; }

        public MainPageModel()
        {
            conexionDeDatos = new Conexion();

            CSiDBExiste();

            ListaLibros = conexionDeDatos.LeerLibros();

            ComandoEditar = new Command(AccionEditar);
            ComandoActualizar = new Command(AccionActualizar);
            ComandoEliminar = new Command(AccionEliminar);
            ComandoGuardar = new Command(AccionGuardar);
        }

        private async void AccionGuardar(object obj)
        {
            Libro libro = new Libro
            {
                Nombre = Nombre2,
                Telefono = Telefono2,
                TLibro = Libro2,
                Accion = Accion2
            };

            conexionDeDatos.addLibro(libro);
            ListaLibros.Add(libro);

            Nombre2 = string.Empty;
            Telefono2 = string.Empty;
            Libro2 = string.Empty;
            Accion2 = string.Empty;
            showDialog();
        }

        private async void showDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Guardar",
                Content = "Conacto guardado",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private void AccionEliminar(object obj)
        {
            conexionDeDatos.borrarLibro(libroSeleccionado.Id);
            ListaLibros.Remove(libroSeleccionado);

            Nombre1 = string.Empty;
            Telefono1 = string.Empty;
            Libro1 = string.Empty;
            Accion1 = string.Empty;
        }

        private void AccionActualizar(object obj)
        {
            int i = listaLibros.IndexOf(libroSeleccionado);

            libroSeleccionado.Nombre = Nombre1;
            libroSeleccionado.Nombre = Nombre1;
            libroSeleccionado.Telefono = Telefono1;
            libroSeleccionado.TLibro = Libro1;
            libroSeleccionado.Accion = Accion1;

            conexionDeDatos.modificarLibro(libroSeleccionado);
            ListaLibros[i] = libroSeleccionado;
        }

        private void AccionEditar(object obj)
        {
            ActivarEdicion = true;
            ActivarEdicionEditar = false;
            ActivarEdicionActualizar = true;
        }

        private async void CSiDBExiste()
        {
            bool dbExist = await conexionDeDatos.compruebaSiExisteBD("Libro.db");

            if (!dbExist)
            {
                //await CreateDatabaseAsync();
                conexionDeDatos.CreateDatabase();

                conexionDeDatos.addPrimerosLibros();
            }

        }
    }
}
