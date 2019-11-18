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
        public int Nombre;
        public string Caminos;
    }
    class Program
    {

        static void Main(string[] args)
        {
            List<int> listaDeGrados1 = new List<int>();
            List<int> listaDeGrados2 = new List<int>();
            Console.Clear();
            Console.WriteLine("Inserte el archivo que contiene el primer grafo");
            string dir_primergrafo = Console.ReadLine();
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

            var grafoCompleto1 = new string[lista1.Count / 2];
            grafoCompleto1 = ObtenerGrafoCompleto(grafoCompleto1, lista1);
            listaDeGrados1 = ObtenerListaGrados(grafoCompleto1, listaDeGrados1);
            

            Console.WriteLine("Inserte el archivo que contiene el segundo grafo");
            string dir_segundografo = Console.ReadLine();
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
            var grafoCompleto2 = new string[lista2.Count / 2];
            grafoCompleto2 = ObtenerGrafoCompleto(grafoCompleto2, lista2);
            listaDeGrados2 = ObtenerListaGrados(grafoCompleto2, listaDeGrados2);

            switch (MismoNumVert(dir_primergrafo, dir_segundografo))
            {
                case 2:
                    Console.WriteLine("No son isomorfos, no tienen la misma cantidad de vértices");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                case 3:
                    Console.WriteLine("Los archivos ingresados no tienen el formato correcto");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }

            if (lista1.Count != lista2.Count)
            {
                Console.WriteLine("No son isomorfos, no tienen la misma cantidad de aristas");
                Console.ReadKey();
                Environment.Exit(0);
            }
            //grado = GrafoCompleto1[5].Split(',').Length - 1;
            for (int i = 0; i < listaDeGrados1.Count; i++)
            {
                // AQUI COMPARAMOS UNO VS UNO
                if (listaDeGrados1[i] != listaDeGrados2[i])
                {
                    Console.Clear();
                    Console.WriteLine("No son isomorfos, no tienen la misma cantidad de vértices con el mismo grado");
                    Console.ReadKey();
                    Environment.Exit(0);
                    // son diferentes
                    //no son isomorfos UwU
                }
            }

            //funcion iso
            foreach (var item1 in listaDeGrados1)
            {
                //COMPARAMOS UNO VS TODOS LOS DEMAS DE EL OTRO GRAFO
                foreach (var item2 in listaDeGrados2)
                {
                    if (item1 == item2)
                    {
                        //item1 = item2
                        //item1 = item2
                    }
                    
                }
            }
            //nodos Grafos
            //[0]->1,2.split() -> int REPRESENTA EL GRADO DE TU VERTICE 
            //[1]->0,2
            //[2]->3,1
            //[3]->0,2

            //switch (MismoNumVert(arregloGrafo1, arregloGrafo)
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
                        var actual = new NodoVertice();
                        actual.Nombre = int.Parse(linea.Split(',')[0]);
                        actual.Caminos = linea.Split(',')[1];
                        var inverso = new NodoVertice();
                        inverso.Nombre = int.Parse(linea.Split(',')[1]);
                        inverso.Caminos = linea.Split(',')[0];
                        lista.Add(actual);
                        lista.Add(inverso);
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

        static string[] ObtenerGrafoCompleto(string[] grafo, List<NodoVertice> lista)
        {
            foreach (var VerticeActual in lista)
            {
                //tomamos el nombre 
                //primer dato.
                //el camino (hacia donde va) + ,
                grafo[VerticeActual.Nombre] += VerticeActual.Caminos + ",";
            }
            return grafo;
        }

        static List<int> ObtenerListaGrados(string[] grafoCompleto, List<int> listaDeGrados)
        {
            foreach (var item in grafoCompleto)
            {
                if (item != null)
                {
                    listaDeGrados.Add(item.Split(',').Length - 1);
                }
                else
                {
                    break;
                }
            }
            listaDeGrados.Sort();
            return listaDeGrados;
        }

        //Función que comprueba si 2 grafos tenen la misma cantidad de vértices con base en el primer dato en los array
        static int MismoNumVert(string dir_grafo1, string dir_grafo2)
        { 
            try
            {
                StreamReader streamReader = new StreamReader(dir_grafo1);
                int cantVertex1 = Convert.ToInt16(streamReader.ReadLine());
                streamReader.Close();
                streamReader = new StreamReader(dir_grafo2);
                int cantVertex2 = Convert.ToInt16(streamReader.ReadLine());
                if (cantVertex1 == cantVertex2)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 3;
            }
        }
    }
}
