using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoDiscretaII
{
    class Program
    {
        static void Main(string[] args)
        {
        Inicio:
            Console.Clear();
            Console.WriteLine("Inserte el archivo que contiene el primer grafo");
            string dir_primergrafo = Console.ReadLine();
            string[] arregloGrafo1;
            try
            {
                StreamReader streamReader = new StreamReader(dir_primergrafo);
                string contenidoArchivo = streamReader.ReadToEnd();
                streamReader.Close();
                arregloGrafo1 = contenidoArchivo.Split('\r');
                for (int i = 0; i < arregloGrafo1.Length; i++)
                {
                    if (arregloGrafo1[i][0] == '\n')
                    {
                        arregloGrafo1[i] = arregloGrafo1[i].Remove(0, 1);
                    }
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Por favor ingrese un nombre de arhivo válido");
                Console.ReadKey();
                Console.Clear();
                goto Inicio;
            }
        SegundoGrafo:
            Console.WriteLine("Inserte el archivo que contiene el segundo grafo");
            string dir_segundografo = Console.ReadLine();
            string[] arregloGrafo2;
            try
            {
                StreamReader streamReader = new StreamReader(dir_segundografo);
                string contenidoArchivo = streamReader.ReadToEnd();
                streamReader.Close();
                arregloGrafo2 = contenidoArchivo.Split('\r');
                for (int i = 0; i < arregloGrafo1.Length; i++)
                {
                    if (arregloGrafo2[i][0] == '\n')
                    {
                        arregloGrafo2[i] = arregloGrafo2[i].Remove(0, 1);
                    }
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Por favor ingrese un nombre de arhivo válido");
                Console.ReadKey();
                Console.Clear();
                goto SegundoGrafo;
            }

            switch (MismoNumVert(arregloGrafo1, arregloGrafo2))
            {
                case 2:
                    Console.WriteLine("Los grafos no son isomorfos debido a que no tienen la misma cantidad de vértices");
                    Console.WriteLine("Si desea verificar otros grafos por favor ingrese 1");
                    try
                    {
                        int respuesta = Convert.ToInt32(Console.ReadLine());
                        if (respuesta == 1)
                        {
                            goto Inicio;
                        }
                    }
                    catch
                    {
                        Environment.Exit(0);
                    }
                    Environment.Exit(0);
                    break;
                case 3:
                    Console.WriteLine("Los archivos .txt no tienen el formato correcto, por favor ingrese archivos correctos");
                    Console.ReadKey();
                    goto Inicio;
            }

            List<string> listaGrafo1 = new List<string>();
            for (int i = 1; i < arregloGrafo1.Length; i++)
            {
                if (!ValorExistente(arregloGrafo1[i][0], listaGrafo1))
                {
                    listaGrafo1.Add(arregloGrafo1[i][0].ToString());
                }
                else if (!ValorExistente(arregloGrafo1[i][2], listaGrafo1))
                {
                    listaGrafo1.Add(arregloGrafo1[i][2].ToString());
                }
            }
            Console.ReadKey();
        }

        //Función que comprueba si 2 grafos tenen la misma cantidad de vértices con base en el primer dato en los array
        static int MismoNumVert(string[] arregloGrafo1, string[] arregloGrafo2)
        {
            try
            {
                int cantVertGrafo1 = Convert.ToInt32(arregloGrafo1[0]);
                int cantVertGrafo2 = Convert.ToInt32(arregloGrafo2[0]);
                if (cantVertGrafo1 == cantVertGrafo2)
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

        static bool ValorExistente(char value, List<string> listaGrafo)
        {
            foreach (string values in listaGrafo)
            {
                if (values == value.ToString())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
