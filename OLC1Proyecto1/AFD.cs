using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace OLC1Proyecto1
{
    class AFD
    {
        int contadorEstado = 0;
        List<EstadoAFD> listaEstadosAFD;
        List<TransicionAFD> transiciones;

        public static List<Image> imagenes = new List<Image>();
        public static List<Image> tablas = new List<Image>();
        public static int contadorImagen = 0;
        String pathImg;
        public void generarAFD(List<Estado> estados, List<String> terminales)
        {

            listaEstadosAFD = new List<EstadoAFD>();
            transiciones = new List<TransicionAFD>();
            Estado inicial = obtenerEstadoInicial(estados);
           
            listaEstadosAFD.Add(cerradura(inicial));
            listaEstadosAFD[listaEstadosAFD.Count() - 1].setInicial(true);

            for(int j = 0; j < listaEstadosAFD.Count(); j++)
            {
                for (int i = 0; i < terminales.Count(); i++)
                {
                    List<Estado> estadosAFN = mueve(listaEstadosAFD[j], terminales[i]);

                    if(estadosAFN.Count() > 0)
                    {
                        EstadoAFD estadoAFD = cerradura(estadosAFN);

                        if (!estadoAFD.isAgregado())
                        {
                            listaEstadosAFD.Add(estadoAFD);
                            estadoAFD.setAgregado(true);

                        /*    for(int t = 0; t<estadoAFD.getEstadosAFN().Count(); t++)
                            {
                                Console.Write(estadoAFD.getEstadosAFN()[t].getNumero()+", ");
                            }*/
                        }
                    }
                 
                    
                }
            }


            String grafo = generarCodigoGraphviz();
            generarImagenGraphviz(grafo, "afd"+contadorImagen);
            llenarTransicionesPorEstado();

            String tabla = generarCodigoGraphvizTT(terminales);
            generarImagenGraphviz(tabla, "tt" + contadorImagen);
            contadorImagen++;

         

           /* List<Estado> estadosAFN = mueve(estadoAFD, terminales[0]);
            EstadoAFD estadoAFD2 = cerradura(estadosAFN);
            List<Estado> estadosAFN = mueve(estadoAFD, terminales[1]);
            EstadoAFD estadoAFD3 = cerradura(estadosAFN);
            List<Estado> estadosAFN = mueve(estadoAFD2, terminales[0]);
            EstadoAFD estadoAFD4 = cerradura(estadosAFN);
            List<Estado> estadosAFN = mueve(estadoAFD2, terminales[1]);
            EstadoAFD estadoAFD5 = cerradura(estadosAFN);*/
        }

        public EstadoAFD compararEstadosAFD(EstadoAFD estadoAFD)
        {
            Boolean iguales = true;
            Boolean encontrado = false;
            for(int i = 0; i < listaEstadosAFD.Count(); i++)
            {
                iguales = true;
                if(listaEstadosAFD[i].getEstadosAFN().Count() == estadoAFD.getEstadosAFN().Count())
                {
                    for (int j = 0; j < estadoAFD.getEstadosAFN().Count(); j++)
                    {
                        encontrado = false;
                        for (int k = 0; k < listaEstadosAFD[i].getEstadosAFN().Count(); k++)
                        {
                            if (estadoAFD.getEstadosAFN()[j].getNumero() == listaEstadosAFD[i].getEstadosAFN()[k].getNumero())
                            {
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado)
                        {
                            iguales = false;
                            break;
                        }
                    }
                    if (iguales)
                    {

                        return listaEstadosAFD[i];


                    }
                }

      
            }
            return estadoAFD;
        }

        public Estado obtenerEstadoInicial(List<Estado> estados)
        {
            for(int i = 0; i < estados.Count(); i++)
            {
                if (estados[i].isEstInicial())
                {
                    return estados[i];
                }
            }
            return null;
        }

        public EstadoAFD cerradura(Estado estadoAFN)
        {
            EstadoAFD estadoAFD = new EstadoAFD(contadorEstado);

            estadoAFD.getEstadosAFN().Add(estadoAFN);
            if (estadoAFN.isEstAceptacion())
            {
                estadoAFD.setAceptacion(true);
            }
            for (int j = 0; j < estadoAFD.getEstadosAFN().Count(); j++)
            {
                for (int i = 0; i < estadoAFD.getEstadosAFN()[j].getListTrans().Count(); i++)
                {
                    if (estadoAFD.getEstadosAFN()[j].getListTrans()[i].getSimbolo().Equals("ep"))
                    {
                        estadoAFD.getEstadosAFN().Add(estadoAFD.getEstadosAFN()[j].getListTrans()[i].getFinal());
                        if (estadoAFD.getEstadosAFN()[j].getListTrans()[i].getFinal().isEstAceptacion())
                        {
                            estadoAFD.setAceptacion(true);
                        }
                    }
                }
            }
            Console.WriteLine("cerradura de: "+ estadoAFN.getNumero());
            Console.WriteLine("\n");
            for (int i = 0; i < estadoAFD.getEstadosAFN().Count(); i++)
            {
                Console.Write(estadoAFD.getEstadosAFN()[i].getNumero() + ", ");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Estado "+ estadoAFD.getNombre());
            contadorEstado++;
            return estadoAFD;
     
        }

        public EstadoAFD cerradura(List<Estado> estadosAFN)
        {
            int contador = 0;
            EstadoAFD estadoAFD = new EstadoAFD(contadorEstado);
           
            for(int k = 0; k < estadosAFN.Count(); k++)
            {
                estadoAFD.getEstadosAFN().Add(estadosAFN[k]);
                if (estadosAFN[k].isEstAceptacion())
                {
                    estadoAFD.setAceptacion(true);
                }
                for(int j = contador; j < estadoAFD.getEstadosAFN().Count(); j=contador)
                {
                    
                    for(int i = 0; i < estadoAFD.getEstadosAFN()[j].getListTrans().Count(); i++)
                    {
                        if (estadoAFD.getEstadosAFN()[j].getListTrans()[i].getSimbolo().Equals("ep"))
                        {
                            estadoAFD.getEstadosAFN().Add(estadoAFD.getEstadosAFN()[j].getListTrans()[i].getFinal());
                            if (estadoAFD.getEstadosAFN()[j].getListTrans()[i].getFinal().isEstAceptacion())
                            {
                                estadoAFD.setAceptacion(true);
                            }
                        }
                    }

                    contador++;
                }
            }

            Console.WriteLine("cerradura de: ");
            for(int i = 0; i < estadosAFN.Count(); i++)
            {

                Console.Write(estadosAFN[i].getNumero() + ", ");
            }

            Console.WriteLine("\n");
            for(int i = 0; i < estadoAFD.getEstadosAFN().Count(); i++)
            {
                Console.Write(estadoAFD.getEstadosAFN()[i].getNumero() + ", ");
            }
            Console.WriteLine("\n");
            estadoAFD = compararEstadosAFD(estadoAFD);

            transiciones[transiciones.Count() - 1].setFinal(estadoAFD);
            Console.WriteLine("Estado " + estadoAFD.getNombre());
            contadorEstado++;
            return estadoAFD;

        }


        public List<Estado> mueve(EstadoAFD estadoAFD, String terminal)
        {
            List<Estado> estadosAFN = new List<Estado>();
            for(int i = 0; i < estadoAFD.getEstadosAFN().Count(); i++)
            {
                for(int j = 0; j < estadoAFD.getEstadosAFN()[i].getListTrans().Count(); j++)
                {
                    if (estadoAFD.getEstadosAFN()[i].getListTrans()[j].getSimbolo().Equals(terminal))
                    {
                        estadosAFN.Add(estadoAFD.getEstadosAFN()[i].getListTrans()[j].getFinal());
                    }
                }
            }

            if(estadosAFN.Count() > 0)
            {
                TransicionAFD transicion = new TransicionAFD(estadoAFD, null, terminal);
                transiciones.Add(transicion);
            }

           Console.WriteLine("mueve de " + estadoAFD.getNombre() + " con " + terminal);
            for(int i = 0; i < estadosAFN.Count(); i++)
            {
                Console.Write(estadosAFN[i].getNumero() + ", ");
               
            }

            Console.WriteLine();
       
            return estadosAFN;
        }


        public void llenarTransicionesPorEstado()
        {
            for (int i = 0; i < transiciones.Count(); i++)
            {
                
                transiciones[i].getInicial().getListTrans().Add(transiciones[i]);
            }
        }
        public String generarCodigoGraphviz()
        {
            //AFD
            String grafo = "digraph G { \n rankdir = LR;\n";

            for (int i = 0; i < listaEstadosAFD.Count(); i++)
            {
                grafo = grafo + "nodo" + listaEstadosAFD[i].getNombre() + "[label = \"" + listaEstadosAFD[i].getNombre() + "\"];\n";
                if (listaEstadosAFD[i].isInicial())
                {
                    grafo = grafo + "nodo" + listaEstadosAFD[i].getNombre() + "[style=filled fillcolor = green];\n ";
                }
                else if (listaEstadosAFD[i].isAceptacion())
                {
                    grafo = grafo + "nodo" + listaEstadosAFD[i].getNombre() + "[style=filled fillcolor = blue];\n ";

                }
            }


            for (int i = 0; i < transiciones.Count(); i++)
            {
                grafo = grafo + "nodo" + transiciones[i].getInicial().getNombre() + "-> nodo" + transiciones[i].getFinal().getNombre() + "[label=\"" + transiciones[i].getSimbolo() + "\"];\n";
            }
            grafo = grafo + "}";

            Console.WriteLine(grafo);
            return grafo;
        }

        public String generarCodigoGraphvizTT(List<String> terminales)
        {
            //Tabla de transicion
            Boolean encontrado = false;
            String grafo = "digraph G { \n";
            grafo = grafo + "node_A [shape=record label= \"{Estado|"; 
            for(int i = 0; i < listaEstadosAFD.Count(); i++)
            {
                grafo = grafo + listaEstadosAFD[i].getNombre();

                if(i < listaEstadosAFD.Count() - 1)
                {
                    grafo = grafo + "|";
                }
                
            }
            grafo = grafo + "}|";

            for(int i = 0; i < terminales.Count(); i++)
            {
                grafo = grafo + "{ ";
                grafo = grafo + terminales[i] + "|";

                for(int j = 0; j < listaEstadosAFD.Count(); j++)
                {
                    encontrado = false;

                   for(int k = 0; k < listaEstadosAFD[j].getListTrans().Count(); k++)
                    {
                        if (listaEstadosAFD[j].getListTrans()[k].getSimbolo().Equals(terminales[i]))
                        {
                            grafo = grafo + listaEstadosAFD[j].getListTrans()[k].getFinal().getNombre();

                            if(j < listaEstadosAFD.Count() - 1)
                            {
                                grafo = grafo + "|";
                            }
                         
                            encontrado = true;
                            break;
                        }
                    }

                    if (!encontrado)
                    {
                        grafo = grafo + "-";
                        if (j < listaEstadosAFD.Count() - 1)
                        {
                            grafo = grafo + "|";
                        }

                    }
                }

                grafo = grafo + "}";

                if (i < terminales.Count() - 1)
                {
                    grafo = grafo + "|";
                }
            }
            grafo = grafo + "\"];\n}";
            //"label=\"shape = record |{ above | middle | below}| right\"];";

            Console.WriteLine(grafo);

            return grafo;
        }

        public void generarImagenGraphviz(String grafo, String nombre)
        {

            string path = Application.StartupPath;
            if (File.Exists(path + "\\" + nombre + ".txt"))
            {
                File.Delete(path + "\\" + nombre + ".txt");

            }

            File.WriteAllText(path + "\\" + nombre + ".txt", grafo);
            var comand = "dot -Tjpg \"" + path + "\\" + nombre + ".txt\" -o \"" + path + "\\" + nombre + ".jpg\" ";
            var procStartInfo = new ProcessStartInfo("cmd", "/C" + comand);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();

            String var = path + "\\" + nombre + ".jpg";
            Console.Write(var);
            if (File.Exists(var))
            {

                pathImg = var;
                Console.WriteLine("esta encontrando el archivo");
                Image img = Image.FromFile(var.Replace("\"", ""));
                //this.img = img;
                if (nombre.Equals("afd" + contadorImagen))
                {
                    imagenes.Add(img);
                }
                else
                {
                    tablas.Add(img);
                }
                
                //  this.imagenGrafo.Image = img;
            }
            else
            {
                Console.WriteLine("archivo no encpntrado");
            }

        }

        public List<Image> getListaImg()
        {
            return imagenes;
        }

        public List<Image> getListaImgTT()
        {
            return tablas;
        }


    }
}
