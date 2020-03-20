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
    class AFN
    {

        List<TokenExp> lista;
        List<Estado> estados;
        List<String> terminales;
        List<Transicion> transiciones;
        List<Image> imagenesAFD;
        List<Image> imagenesTT;
        public static List<Image> imagenes = new List<Image>();
        public static int contadorImagen = 0;
        String pathImg;
        public void generarAFN(List<ExprecionRegular> listaER)
        {


            for( int i = 0; i < listaER.Count(); i++)
            {
                llenarLista(listaER[i]);
                mostrarLista();
                int contadorEstado = 0;
                 estados = new List<Estado>();
                 transiciones= new List<Transicion>();
                for(int j = 0; j<lista.Count()-1; j++)
                {
                    Console.WriteLine("si entra bucle for");
                    if (lista[j].isBinario())
                    {

                        Console.WriteLine("si entra es binario");
                        if (!lista[j].isTerminal() )
                        {
                            //&& lista[j + 1].isTerminal() && lista[j + 2].isTerminal())||(!lista[j].isTerminal() && lista[j + 1].isEstGenerada() && lista[j + 2].isEstGenerada())
                            Console.WriteLine("si entra no es terminal");
                            if (lista[j + 1].isTerminal() && lista[j + 2].isTerminal())
                            {

                                Console.WriteLine("si entra terminal terminal");
                                if (lista[j].getToken().Equals("sOr") && !lista[j].isEstGenerada())
                                {
                                    for (int cont = 0; cont < 6; cont++)
                                    {
                                        estados.Add(new Estado(contadorEstado));
                                        contadorEstado++;
                                    }
                                /*    Transicion transicion1 = new Transicion(estados[contadorEstado - 6], estados[contadorEstado - 5], "ep");
                                    Transicion transicion2 = new Transicion(estados[contadorEstado - 6], estados[contadorEstado - 4], "ep");

                                    transiciones.Add(transicion1);
                                    transiciones.Add(transicion2);

                                    estados[contadorEstado - 6].getListTrans().Add(transicion1);
                                    estados[contadorEstado - 6].getListTrans().Add(transicion2);*/
                                    transiciones.Add(new Transicion(estados[contadorEstado - 6], estados[contadorEstado - 5], "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 6], estados[contadorEstado - 4], "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 5], estados[contadorEstado - 3], lista[j + 1].getLexema()));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 2], lista[j + 2].getLexema()));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 1], "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(estados[contadorEstado - 6]);
                                    lista[j].setFinal(estados[contadorEstado-1]);

             

                                    reducirLista(j, 2);

                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;

                                }
                                else if (lista[j].getToken().Equals("sPunto") && !lista[j].isEstGenerada())
                                {
                                    for (int cont = 0; cont < 3; cont++)
                                    {
                                        estados.Add(new Estado(contadorEstado));
                                        contadorEstado++;
                                    }

                                    transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 2], lista[j + 1].getLexema()));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado-1], lista[j + 2].getLexema()));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(estados[contadorEstado - 3]);
                                    lista[j].setFinal(estados[contadorEstado-1]);


                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;
                                }
                                mostrarLista();
                            }else if(lista[j + 1].isTerminal() && lista[j + 2].isEstGenerada())
                            {
                                Console.WriteLine("si entra terminal est generada");
                                if (lista[j].getToken().Equals("sOr") && !lista[j].isEstGenerada())
                                {
                                    for (int cont = 0; cont < 4; cont++)
                                    {
                                        estados.Add(new Estado(contadorEstado));
                                        contadorEstado++;
                                    }

                                    transiciones.Add(new Transicion(estados[contadorEstado - 4], lista[j+2].getInicial(), "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 3], "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 2], lista[j+1].getLexema()));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado -1], "ep"));
                                    transiciones.Add(new Transicion(lista[j+2].getFinal(), estados[contadorEstado -1], "ep"));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(estados[contadorEstado - 4]);
                                    lista[j].setFinal(estados[contadorEstado-1]);


                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;
                                }
                                else if(lista[j].getToken().Equals("sPunto") && !lista[j].isEstGenerada())
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;

                                    transiciones.Add(new Transicion(estados[contadorEstado - 1], lista[j + 2].getInicial(), lista[j+1].getLexema()));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(estados[contadorEstado - 1]);
                                    lista[j].setFinal(lista[j+2].getFinal());


              

                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;

                                }
                                mostrarLista();
                            }else if(lista[j + 1].isEstGenerada() && lista[j + 2].isTerminal())
                            {

                                Console.WriteLine("si entra est generada terminal");
                                if (lista[j].getToken().Equals("sOr") && !lista[j].isEstGenerada())
                                {
                                    for (int cont = 0; cont < 4; cont++)
                                    {
                                        estados.Add(new Estado(contadorEstado));
                                        contadorEstado++;
                                    }

                                    transiciones.Add(new Transicion(estados[contadorEstado - 4], lista[j + 1].getInicial(), "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 3], "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 2], lista[j + 2].getLexema()));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));
                                    transiciones.Add(new Transicion(lista[j + 1].getFinal(), estados[contadorEstado - 1], "ep"));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(estados[contadorEstado - 4]);
                                    lista[j].setFinal(estados[contadorEstado - 1]);

               

                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);


                                    j = -1;
                                }
                                else if(lista[j].getToken().Equals("sPunto") && !lista[j].isEstGenerada())
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;

                                    transiciones.Add(new Transicion( lista[j + 1].getFinal(), estados[contadorEstado - 1], lista[j + 2].getLexema()));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(lista[j+1].getInicial());
                                    lista[j].setFinal(estados[contadorEstado - 1]);

                         

                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;
                                }
                                mostrarLista();
                            }else if(lista[j + 1].isEstGenerada() && lista[j + 2].isEstGenerada())
                            {

                                Console.WriteLine("si entra est generada est generada");
                                if (lista[j].getToken().Equals("sOr") && !lista[j].isEstGenerada())
                                {

                                    Console.WriteLine("si entra est generada est generada or");
                                    for (int cont = 0; cont < 2; cont++)
                                    {
                                        estados.Add(new Estado(contadorEstado));
                                        contadorEstado++;
                                    }

                                    transiciones.Add(new Transicion(estados[contadorEstado - 2], lista[j + 1].getInicial(), "ep"));
                                    transiciones.Add(new Transicion(estados[contadorEstado - 2], lista[j + 2].getInicial(), "ep"));
                                    transiciones.Add(new Transicion(lista[j + 1].getFinal(), estados[contadorEstado - 1], "ep"));
                                    transiciones.Add(new Transicion(lista[j + 2].getFinal(), estados[contadorEstado - 1], "ep"));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(estados[contadorEstado -2]);
                                    lista[j].setFinal(estados[contadorEstado - 1]);


                           

                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;
                                }
                                else if (lista[j].getToken().Equals("sPunto") && !lista[j].isEstGenerada())
                                {
                                 
                                  //  lista[j + 1].setFinal(lista[j + 2].getInicial()) ;

                                    //lista[j + 1].getFinal().setNumero(lista[j + 2].getInicial().getNumero());
                                    transiciones.Add(new Transicion(lista[j + 1].getFinal(), lista[j + 2].getInicial(), "ep"));

                                    lista[j].setEstGenerada(true);
                                    lista[j].setInicial(lista[j+1].getInicial());
                                    lista[j].setFinal(lista[j+2].getFinal());
                                    /*for (int k = 1; (j + k + 2) < lista.Count(); k++)
                                    {
                                        if (lista[k] != null)
                                        {
                                            lista[j + k] = lista[j + k + 2];
                                        }
                                    }
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 2);
                                    */

                                    reducirLista(j, 2);
                                    lista.RemoveAt(lista.Count() - 1);
                                    lista.RemoveAt(lista.Count() - 1);
                                    j = -1;

                                }
                                mostrarLista();
                            }
                           
                        }
                    }
                    else 
                    {
                        if (!lista[j].isTerminal() && lista[j + 1].isTerminal() )
                        {


                            if (lista[j].getToken().Equals("sPor"))
                            {
                                for (int cont = 0; cont < 4; cont++)
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;
                                }

                                transiciones.Add(new Transicion(estados[contadorEstado -4], estados[contadorEstado - 3], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 2], lista[j+1].getLexema()));
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 3], "ep"));

                                lista[j].setEstGenerada(true);
                                lista[j].setInicial(estados[contadorEstado - 4]);
                                lista[j].setFinal(estados[contadorEstado - 1]);

                                /*for (int k = 1; (j + k + 1) < lista.Count(); k++)
                                {
                                    if (lista[k] != null)
                                    {
                                        lista[j + k] = lista[j + k + 1];
                                    }
                                }
                                lista.RemoveAt(lista.Count() - 1);
                            */
                                
                                reducirLista(j, 1);
                                lista.RemoveAt(lista.Count() - 1);
                                


                                j = -1;
                            }
                            else if (lista[j].getToken().Equals("sMas"))
                            {
                                for (int cont = 0; cont < 4; cont++)
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;
                                }

                                transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 3], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 2], lista[j+1].getLexema()));
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 3], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));

                                lista[j].setEstGenerada(true);
                                lista[j].setInicial(estados[contadorEstado - 4]);
                                lista[j].setFinal(estados[contadorEstado - 1]);

                                reducirLista(j, 1);
                                lista.RemoveAt(lista.Count() - 1);
                                j = -1;

                            }
                            else if (lista[j].getToken().Equals("sInterrogacion"))
                            {

                                for (int cont = 0; cont < 6; cont++)
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;
                                }
                                transiciones.Add(new Transicion(estados[contadorEstado - 6], estados[contadorEstado - 5], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 6], estados[contadorEstado - 4], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 5], estados[contadorEstado - 3], lista[j+1].getLexema()));
                                transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 2], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));

                                lista[j].setEstGenerada(true);
                                lista[j].setInicial(estados[contadorEstado - 6]);
                                lista[j].setFinal(estados[contadorEstado - 1]);

                                reducirLista(j, 1);
                                lista.RemoveAt(lista.Count() - 1);



                                j = -1;

                            }
                        }else if (!lista[j].isTerminal() && lista[j + 1].isEstGenerada())
                        {
                            if (lista[j].getToken().Equals("sPor"))
                            {
                                for (int cont = 0; cont < 2; cont++)
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;
                                }

                                transiciones.Add(new Transicion(estados[contadorEstado - 2], lista[j+1].getInicial(), "ep"));   
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(lista[j+1].getFinal(), estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(lista[j+1].getFinal(), lista[j+1].getInicial(), "ep"));

                                lista[j].setEstGenerada(true);
                                lista[j].setInicial(estados[contadorEstado - 2]);
                                lista[j].setFinal(estados[contadorEstado - 1]);

                                /*for (int k = 1; (j + k + 1) < lista.Count(); k++)
                                {
                                    if (lista[k] != null)
                                    {
                                        lista[j + k] = lista[j + k + 1];
                                    }
                                }
                                lista.RemoveAt(lista.Count() - 1);
                                */
                                reducirLista(j, 1);
                                lista.RemoveAt(lista.Count() - 1);
                                j = -1;
                            }
                            else if (lista[j].getToken().Equals("sMas"))
                            {
                                for (int cont = 0; cont < 2; cont++)
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;
                                }

                                transiciones.Add(new Transicion(estados[contadorEstado - 2], lista[j+1].getInicial(), "ep"));
                                transiciones.Add(new Transicion(lista[j+1].getFinal(), estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(lista[j + 1].getFinal(), lista[j + 1].getInicial(), "ep"));
                                

                                lista[j].setEstGenerada(true);
                                lista[j].setInicial(estados[contadorEstado - 2]);
                                lista[j].setFinal(estados[contadorEstado - 1]);

                                reducirLista(j, 1);
                                lista.RemoveAt(lista.Count() - 1);
                                j = -1;
                            }
                            else if (lista[j].getToken().Equals("sInterrogacion"))
                            {
                                for (int cont = 0; cont < 4; cont++)
                                {
                                    estados.Add(new Estado(contadorEstado));
                                    contadorEstado++;
                                }
                                transiciones.Add(new Transicion(estados[contadorEstado - 4], lista[j+1].getInicial(), "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 4], estados[contadorEstado - 3], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 3], estados[contadorEstado - 2], "ep"));
                                transiciones.Add(new Transicion(estados[contadorEstado - 2], estados[contadorEstado - 1], "ep"));
                                transiciones.Add(new Transicion(lista[j+1].getFinal(), estados[contadorEstado - 1], "ep"));
                                

                                lista[j].setEstGenerada(true);
                                lista[j].setInicial(estados[contadorEstado - 4]);
                                lista[j].setFinal(estados[contadorEstado - 1]);

                                reducirLista(j, 1);
                                lista.RemoveAt(lista.Count() - 1);



                                j = -1;
                            }
                        }
                    }
                    if(lista.Count() == 1)
                    {
                        lista[0].getInicial().setEstInicial(true);
                        lista[0].getFinal().setEstAceptacion(true);
                    }

                }
                
                String codigo = generarCodigoGraphviz();
                generarImagenGraphviz(codigo, "afn"+contadorImagen);
                contadorImagen++;
                Console.WriteLine("contador imagen" + contadorImagen);
                llenarTransicionesPorEstado();

                AFD afd = new AFD();
                afd.generarAFD(estados, terminales);
                imagenesAFD = afd.getListaImg();
                imagenesTT = afd.getListaImgTT();

            }

            
        } 

        public void llenarTransicionesPorEstado()
        {
            for(int i = 0; i < transiciones.Count(); i++)
            {
                transiciones[i].getInicial().getListTrans().Add(transiciones[i]);
            }

            for(int i = 0; i < estados.Count(); i++)
            {
                Console.WriteLine("Estado " + estados[i].getNumero());
                for(int j = 0; j < estados[i].getListTrans().Count(); j++)
                {
                    Console.Write(estados[i].getListTrans()[j].getSimbolo() + ", ");
                }
                Console.WriteLine("\n");
            }
        }

        public void reducirLista(int contador, int num)
        {
            // num: numero de elementos que se eliminarian: binario 2, no binario 1

            for (int k = 1; (contador + k + num) < lista.Count(); k++)
            {
                if (lista[k] != null)
                {
                    string token = lista[contador + k + num].getToken();
                    string lexema = lista[contador + k + num].getLexema();
                    Boolean terminal = lista[contador + k + num].isTerminal();
                    Boolean estGenerada = lista[contador + k + num].isEstGenerada();
                    Boolean binario = lista[contador + k + num].isBinario();

                    if(lista[contador + k + num].getInicial() != null && lista[contador + k + num].getFinal() != null)
                    {
                        int numInicial = lista[contador + k + num].getInicial().getNumero();
                        int numFinal = lista[contador + k + num].getFinal().getNumero();
                        Estado inicial = new Estado(numInicial);
                        Estado final = new Estado(numFinal);


                        lista[contador + k] = new TokenExp(token, lexema, terminal, estGenerada, binario, inicial, final);
                    }
                    else
                    {

                        lista[contador + k] = new TokenExp(token, lexema, terminal, estGenerada, binario);
                    }
                  
                    /*
                    lista.RemoveAt(lista.Count() - 1);
                    if(num == 2)
                    {
                        lista.RemoveAt(lista.Count() - 2);
                    }
                    */
                    //lista[contador + k] = lista[contador + k + num];
                }
            }
        }

        public void generarImagenGraphviz(String grafo, String nombre)
        {
            
            string path = Application.StartupPath;
            if (File.Exists(path + "\\" + nombre + ".txt"))
            {
                File.Delete(path + "\\" + nombre + ".txt");

            }

            File.WriteAllText(path + "\\" + nombre + ".txt", grafo);
            var comand = "dot -Tjpg \"" + path + "\\" + nombre + ".txt\" -o \"" + path + "\\" + nombre +  ".jpg\" ";
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

                imagenes.Add(img);
              //  this.imagenGrafo.Image = img;
            }
            else
            {
                Console.WriteLine("archivo no encpntrado");
            }

        }


        public String generarCodigoGraphviz()
        {
            String grafo = "digraph G { \n rankdir = LR;\n";

            for(int i = 0; i < estados.Count(); i++)
            {
                grafo = grafo + "nodo" + estados[i].getNumero() + "[label = \"" + estados[i].getNumero() +"\"];\n";
                if (estados[i].isEstInicial())
                {
                    grafo = grafo + "nodo" + estados[i].getNumero() + "[style=filled fillcolor = green];\n ";
                }
                else if (estados[i].isEstAceptacion())
                {
                    grafo = grafo + "nodo" + estados[i].getNumero() + "[style=filled fillcolor = blue];\n ";

                }
                


            }


            for(int i = 0; i < transiciones.Count(); i++)
            {
                grafo = grafo + "nodo" + transiciones[i].getInicial().getNumero() + "-> nodo" + transiciones[i].getFinal().getNumero()+"[label=\""+transiciones[i].getSimbolo()+"\"];\n";
            }
            grafo = grafo + "}";

            Console.WriteLine(grafo);
            return grafo;
        }

        public List<Image> getListaImg()
        {
            return imagenes;
        }

        public List<Image> getListaImgAFD()
        {
            return imagenesAFD;
        }

        public List<Image> getListaImgTT()
        {
            return imagenesTT;
        }

        public void mostrarLista()
        {

            Console.Write("TokenExp");
            for(int i = 0; i < lista.Count(); i++)
            {
                Console.Write(lista[i].getLexema());
            }
            Console.WriteLine("\n");
        }


        public void llenarLista(ExprecionRegular listaToken)
        {
            lista = new List<TokenExp>();
            terminales = new List<String>();
            Token[] tokens = listaToken.getExprecion().ToArray();
            for (int i =0; i < tokens.Count(); i++)
            {
                if (!tokens[i].getLexema().Equals("{") && !tokens[i].getLexema().Equals("}"))
                {
                    if (tokens[i].getToken().Equals("sPunto") || tokens[i].getToken().Equals("sOr"))
                    {
                        lista.Add(new TokenExp(tokens[i].getToken(), tokens[i].getLexema(), false, false, true));
                    }
                    else if (tokens[i].getToken().Equals("sPor") || tokens[i].getToken().Equals("sMas") || tokens[i].getToken().Equals("sInterrogacion"))
                    {
                        lista.Add(new TokenExp(tokens[i].getToken(), tokens[i].getLexema(), false, false, false));
                    }
                    else
                    {
                        lista.Add(new TokenExp(tokens[i].getToken(), tokens[i].getLexema(), true, false, false));
                        Boolean existe = false;

                        if(terminales.Count() > 0)
                        {
                            for (int j = 0; j < terminales.Count(); j++)
                            {
                                if (tokens[i].getLexema().Equals(terminales[j]))
                                {
                                    existe = true;
                                    break;
                                }
                            }
                            if (!existe)
                            {
                                terminales.Add(tokens[i].getLexema());
                            }
                        }
                        else
                        {
                            terminales.Add(tokens[i].getLexema());
                        }
               
                       
                    }
                }

                
            } 

        }
    }
}
