using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OLC1Proyecto1
{
    class AnalizadorLexico
    {
        List<Image> imagenes;
        List<Image> imagenesAFD;
        List<Image> imagenesTT;
        //fastcoloredtextbox
        LinkedList<Token> listaToken;
        LinkedList<Error> listaError;
        List<ExprecionRegular> listaER;
        ExprecionRegular exprecion;
        List<Conjunto> listaConj;
        Conjunto conjunto;
        int estado;
        int fila, columna;
        Boolean conj;
        Boolean expR;


        String cadenaAcumulada;

        public AnalizadorLexico()
        {
            estado = 0;
            cadenaAcumulada = "";
        }

        public void Analizar(String cadenaEntrada)
        {

            listaToken = new LinkedList<Token>();
            listaError = new LinkedList<Error>();
            listaER = new List<ExprecionRegular>();
            listaConj = new List<Conjunto>();
            fila = 1;
            columna = 0;
            estado = 0;
            cadenaAcumulada = "";

            conj = false;
            expR = false;

            cadenaEntrada = cadenaEntrada + "#";
            char[] c = cadenaEntrada.ToCharArray();

            for (int i = 0; i < cadenaEntrada.Length; i++)
            {
                columna++;
                switch (estado)
                {
                    case 0:
                        if (c[i] == ' ' || c[i] == '\n' || c[i] == '\r' || c[i] == '\t')
                        {
                            if (c[i] == '\n')
                            {
                                fila++;
                                columna = 0;
                            }
                            estado = 0;
                        }
                        else if (((int)c[i] >= 97 && (int)c[i] <= 122) || ((int)c[i] >= 65 && (int)c[i] <= 90))
                        {

                            //letra
                            cadenaAcumulada += c[i];
                            estado = 1;
                        }
                        else if ((int)c[i] >= 48 && (int)c[i] <= 57)
                        {
                            //numero
                            cadenaAcumulada += c[i];
                            estado = 2;
                        }
                        else if (c[i] == '"')
                        {
                            //cadena
                            //cadenaAcumulada += c[i];
                            estado = 3;
                        }

                        else if (c[i] == ':')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sDosPuntos, c[i], c[i + 1]);
                        }
                        else if (c[i] == ';')
                        {
                            expR = false;
                            conj = false;
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sPuntoyComa, c[i], c[i + 1]);
                        }
                        else if (c[i] == '{')
                        {

                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sLlaveIzq,c[i], c[i + 1]);
                        }
                        else if (c[i] == '}')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sLlaveDer, c[i], c[i + 1]);
                        }
                        else if (c[i] == '*')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sPor, c[i], c[i + 1]);

                        }
                        else if (c[i] == ',')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sComa, c[i], c[i + 1]);

                        }
                        else if (c[i] == '+')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sMas, c[i], c[i + 1]);

                        }
                        else if (c[i] == '>')
                        {
                            cadenaAcumulada += c[i];
                            if (c[i - 1] == '-')
                            {
                                expR = true;

                                if (!conj)
                                {
                                    // listaExprecion = new LinkedList<ExprecionRegular>();
                                    Token[] lista = listaToken.ToArray();
                                    exprecion = new ExprecionRegular();
                                    exprecion.setNombre(lista[lista.Count() - 2].getLexema());                         
                                    listaER.Add(exprecion);
                              
                                }
                                else
                                {
                                    Token[] lista = listaToken.ToArray();
                                    conjunto = new Conjunto();
                                    conjunto.setNombre(lista[lista.Count() - 2].getLexema());
                                    listaConj.Add(conjunto);
                                }
  
                                
                              
                            }
                         
                            agregarToken(Token.tipo.sMayor, c[i], c[i + 1]);

                        }
                        else if (c[i] == '<')
                        {
                            estado = 6;
                            
                        }
                        else if (c[i] == '-')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sMenos, c[i], c[i + 1]);

                        }
                        else if (c[i] == '|')
                        {

                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sOr, c[i], c[i + 1]);
                        }
                        else if (c[i] == '.')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sPunto, c[i], c[i + 1]);

                        }
                        else if (c[i] == '~')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sVirgulilla, c[i], c[i + 1]);

                        }
                        else if (c[i] == '%')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sPorcentaje, c[i], c[i + 1]);

                        }
                        else if (c[i] == '?')
                        {
                            cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.sInterrogacion, c[i], c[i + 1]);

                        }
                        else if (c[i] == '/')
                        {

                            estado = 4;

                        }
                        else
                        {

                      
                            if (c[i] == '#' && i == cadenaEntrada.Length - 1)
                            {
                                Console.WriteLine("final de la cadena");
                                mostrarListaExp();
                                mostrarListaConj();

                                generarAFN();
                            }
                            else
                            {
                              
                                cadenaAcumulada += c[i];
                                Console.WriteLine("Error léxico con: " + cadenaAcumulada);
                                agregarError();
                                estado = 0;
                                cadenaAcumulada = "";
                               
                            }
                        }
                        break;

                    case 1:

                        if ((((int)c[i] >= 97 && (int)c[i] <= 122) || ((int)c[i] >= 65 && (int)c[i] <= 90)) || ((int)c[i] >= 48 && (int)c[i] <= 57))
                        {
                            //letra-num
                            cadenaAcumulada += c[i];
                            estado = 1;

                        }
                        else
                        {

                            if ("CONJ".Equals(cadenaAcumulada))
                            {
                                conj = true;
                                agregarToken(Token.tipo.prConj, c[i-1], c[i]);
                                i--;
                                columna--;
                            }
                            else
                            {

                                agregarToken(Token.tipo.identificador, c[i-1], c[i]);
                                i--;
                                columna--;

                            }

                        }
                        break;

                    case 2:
                        if (((int)c[i] >= 48 && (int)c[i] <= 57) || c[i] == '.')
                        {
                            //numero
                            cadenaAcumulada += c[i];
                            estado = 2;
                        }
                        else
                        {
                            //cadenaAcumulada += c[i];
                            agregarToken(Token.tipo.numero, c[i-1], c[i]);
                            i--;
                            columna--;
                        }
                        break;

                    case 3:
                        if (c[i] == '"')
                        {
                            agregarToken(Token.tipo.cadena, c[i], c[i + 1]);
                        }
                        else
                        {
                            cadenaAcumulada += c[i];
                            estado = 3;
                        }

                        break;

                    case 4:
                        if (c[i] == '/')
                        {
                            estado = 5;
                        }
                        else
                        {
                           
                            cadenaAcumulada += c[i];
                            Console.WriteLine("Error léxico con: " + "/");
                            agregarError();
                            estado = 0;
                            cadenaAcumulada = "";
                           
                        }

                        break;

                    case 5:
                        if (c[i] == '\n')
                        {
                            agregarToken(Token.tipo.comentario, c[i], c[i + 1]);
                        }
                        else
                        {
                            cadenaAcumulada += c[i];
                            estado = 5;
                        }

                        break;

                    case 6:

                        if (c[i] == '!')
                        {
                            estado = 7;
                        }
                        else
                        {
                            cadenaAcumulada += c[i];
                            Console.WriteLine("Error léxico con: <");
                            agregarError();
                            estado = 0;
                            cadenaAcumulada = "";
            
                        }

                        break;

                    case 7:

                        if (c[i] == '!' && c[i + 1] == '>')
                        {
                            agregarToken(Token.tipo.comentarioML, c[i], c[i+1]);
                        }
                        else
                        {
                            cadenaAcumulada += c[i];
                            estado = 7;
                        }

                        break;
                }
            }

    
        }

        public void agregarToken(Token.tipo tipo , char actual, char siguiente)
        {

            Token token = new Token(tipo, cadenaAcumulada, fila, columna);
            listaToken.AddLast(token);

            if (token.getLexema() != ">")
            {
                if (!conj && expR)
                {
                    agregarExprecion(token);
                }
                else if (conj && expR)
                {
                    agregarConjunto(token, actual, siguiente);
                }
            }
            estado = 0;
            cadenaAcumulada = "";
        }


        public void agregarExprecion( Token token)
        {
            listaER[listaER.Count() - 1].getExprecion().AddLast(token);
            //   listaER[listaER.Count() - 1].setNombre("exprecion");

                

        }

        char anterior = ' ';
        Boolean conjComa = false;
        public void agregarConjunto(Token token, char actual, char siguiente)
        {
           // Console.WriteLine("actual: " +actual.ToString()+ "siguiente: " +siguiente.ToString());

           
                if (siguiente == ','|| (siguiente == ';' && conjComa))
                {
                        
                     conjComa = true;
                    listaConj[listaConj.Count()-1].getConjunto().AddLast(token.getLexema());
                 //  listaConj[listaConj.Count() - 1].setNombre("conjunto");
                }
            else if(siguiente == '~'|| actual == '~')
                {

                      conjComa = false;
                    if (siguiente == '~')
                    {
                          anterior = actual;

                         
                       // listaConj[listaConj.Count() - 1].AddLast(actual.ToString());
                    }else if(actual == '~')
                    {


                    //numero

                            Console.WriteLine(anterior + " " + siguiente);
                            for (int i = (int)anterior ; i <= (int)siguiente; i++)
                            {
                                listaConj[listaConj.Count() - 1].getConjunto().AddLast(((char)i).ToString());
                               //  listaConj[listaConj.Count() - 1].setNombre("conjunto");
                    }
                        
                    }

                }
            
        }

        public void mostrarListaExp()
        {
           for(int i = 0; i < listaER.Count(); i++)
            {
                Token[] tokens = listaER[i].getExprecion().ToArray();
                Console.Write(listaER[i].getNombre());
                for(int j = 0; j < tokens.Count(); j++)
                {

                    Console.Write(tokens[j].getLexema() + ", ");
                }
                Console.Write("\n");
                
            }
        }

        public void mostrarListaConj()
        {
            for (int i = 0; i < listaConj.Count(); i++)
            {
                String[] caracteres = listaConj[i].getConjunto().ToArray();
                Console.Write(listaConj[i].getNombre());
                for (int j = 0; j < caracteres.Count(); j++)
                {

                    Console.Write(caracteres[j].ToString() + ", ");
                }
                Console.Write("\n");

            }
        }
        public void agregarError()
        {
           
            listaError.AddLast(new Error(cadenaAcumulada, fila, columna));
            estado = 0;
            cadenaAcumulada = "";
        }

        public LinkedList<Token> getListaTok()
        {
            return listaToken;
        }

        public LinkedList<Error> getListaEr()
        {
            return listaError;
        }

        public List<ExprecionRegular> getListaExprecion()
        {
            return listaER;
        }

        public List<Conjunto> getListaConj()
        {
            return listaConj;
        }

        public void generarAFN()
        {
            AFN a = new AFN();
            a.generarAFN(listaER);
            
            imagenes = a.getListaImg();
            imagenesAFD = a.getListaImgAFD();
            imagenesTT = a.getListaImgTT();

        }

        public List<Image> getListaImgTT()
        {
            return imagenesTT;
        }

        public List<Image> getListaImgAFD()
        {
            return imagenesAFD;
        }
        public List<Image> getListaImg()
        {
            return imagenes;
        }

     
       

    }
}
