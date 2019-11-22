using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoDiscretaII
{
    class NodoVertice
    { 
        //nombre
        public string vertex;
        public List<string> conexiones;
        public NodoVertice()
        {
            conexiones = new List<string>();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Inserte el archivo que contiene el primer grafo");
            string dir_primergrafo = Console.ReadLine();
            StreamReader streamReader = new StreamReader(dir_primergrafo);
            int cantVertex1 = Convert.ToInt16(streamReader.ReadLine());
            streamReader.Close();
            var lista1 = new List<NodoVertice>();
            try
            {
                lista1 = Comprobacion(dir_primergrafo);
            }
            catch
            {
                Console.WriteLine("Ingrese un archivo válido");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Console.WriteLine("Inserte el archivo que contiene el segundo grafo");
            string dir_segundografo = Console.ReadLine();
            streamReader = new StreamReader(dir_segundografo);
            int cantVertex2 = Convert.ToInt16(streamReader.ReadLine());
            streamReader.Close();
            var lista2 = new List<NodoVertice>();
            try
            {
                lista2 = Comprobacion(dir_segundografo);
            }
            catch
            {
                Console.WriteLine("Ingrese un archivo válido");
                Console.ReadKey();
                Environment.Exit(0);
            }

            try
            {
                if (cantVertex1 == cantVertex2)
                {
                    string[,] matriz1 = new string[cantVertex1 + 1, cantVertex1 + 1];
                    string[,] matriz2 = new string[cantVertex2 + 1, cantVertex2 + 1];
                }
                else
                {
                    Console.WriteLine("Los grafos no son isomorfos, no tienen la misma cantidad de vértices");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            catch
            {
                Console.WriteLine("El archivo ingresado no tiene el formato correcto");
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (!MismasAristas(lista1, lista2))
            {
                Console.WriteLine("No son isomorfos, no tienen la misma cantidad de aristas");
                Console.ReadKey();
                Environment.Exit(0);
            }

            List<int> grados1 = new List<int>();
            List<int> grados2 = new List<int>();

            grados1 = GenerarGrados(lista1, grados1);
            grados2 = GenerarGrados(lista2, grados2);

            if (MismosGrados(grados1, grados2))
            {
                Console.Clear();
                Console.WriteLine("No son isomorfos, no tienen la misma cantidad de vértices con el mismo grado");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        static bool MismosGrados(List<int> grados1, List<int> grados2)
        {
            for (int i = 0; i < grados1.Count; i++)
            {
                if (grados1[i] != grados2[i])
                {
                    return false;
                }
            }
            return true;
        }
        static List<int> GenerarGrados(List<NodoVertice> lista, List<int> grados)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                grados.Add(lista[i].conexiones.Count);
            }

            grados.Sort();
            return grados;
        }
        static bool MismasAristas(List<NodoVertice> lista1, List<NodoVertice> lista2)
        {
            int aristas1 = 0;
            int aristas2 = 0;

            foreach (var item in lista1)
            {
                aristas1 += item.conexiones.Count;
            }

            foreach (var item in lista2)
            {
                aristas2 += item.conexiones.Count;
            }

            if (aristas1 == aristas2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static List<NodoVertice> Comprobacion(string dir_grafo)
        {
            var lector = new StreamReader(dir_grafo);
            var linea = lector.ReadLine();
            var lista = new List<NodoVertice>();
            try
            {
                while (linea != null)
                {
                    if (!(linea.Split(',').Length == 1))
                    {
                        if (ExisteVertex(lista, linea.Split(',')[0]))
                        {
                            AgregarConexion(lista, linea.Split(',')[0], linea.Split(',')[1]);
                            if (ExisteVertex(lista, linea.Split(',')[1]))
                            {
                                AgregarConexion(lista, linea.Split(',')[1], linea.Split(',')[0]);
                            }
                            else
                            {
                                var actual2 = new NodoVertice();
                                actual2.vertex = linea.Split(',')[1];
                                actual2.conexiones.Add(linea.Split(',')[0]);
                                lista.Add(actual2);
                            }
                        }
                        else if (ExisteVertex(lista, linea.Split(',')[1]))
                        {
                            AgregarConexion(lista, linea.Split(',')[1], linea.Split(',')[0]);
                            if (ExisteVertex(lista, linea.Split(',')[0]))
                            {
                                AgregarConexion(lista, linea.Split(',')[0], linea.Split(',')[1]);
                            }
                            else
                            {
                                var actual2 = new NodoVertice();
                                actual2.vertex = linea.Split(',')[0];
                                actual2.conexiones.Add(linea.Split(',')[1]);
                                lista.Add(actual2);
                            }
                        }
                        else
                        {
                            var actual = new NodoVertice();
                            actual.vertex = linea.Split(',')[0];
                            actual.conexiones.Add(linea.Split(',')[1]);
                            lista.Add(actual);
                            if (ExisteVertex(lista, linea.Split(',')[1]))
                            {
                                AgregarConexion(lista, linea.Split(',')[1], linea.Split(',')[0]);
                            }
                            else
                            {
                                var actual2 = new NodoVertice();
                                actual2.vertex = linea.Split(',')[1];
                                actual2.conexiones.Add(linea.Split(',')[0]);
                                lista.Add(actual2);
                            }
                        }
                    }
                    linea = lector.ReadLine();
                }
                lector.Close();
            }
            catch
            {
                Console.WriteLine("El archivo ingresado no tiene el formato correcto");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return lista;
        }
        static void AgregarConexion(List<NodoVertice> lista, string vertex, string conexion)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].vertex == vertex)
                {
                    if (lista[i].conexiones.BinarySearch(conexion) < 0)
                    {
                        lista[i].conexiones.Add(conexion);
                    }
                }
            }
        }
        static bool ExisteVertex(List<NodoVertice> listavertex, string vertex)
        {
            for (int i = 0; i < listavertex.Count; i++)
            {
                if (listavertex[i].vertex == vertex)
                {
                    return true;
                }
            }
            return false;
        }
        //Función que comprueba si 2 grafos tenen la misma cantidad de vértices con base en el primer dato en los array
    }
}
