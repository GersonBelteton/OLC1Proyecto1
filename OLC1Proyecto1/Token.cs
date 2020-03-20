using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class Token
    {


        public enum tipo
        {

            prConj,
            sLlaveIzq,
            sLlaveDer,
            sDosPuntos,
            sOr,
            sComa,
            sPunto,
            sPorcentaje,
            sInterrogacion,
            sPor,
            sMas,
            sMenos,
            sMayor,
            sMenor,
            sVirgulilla,
            sPuntoyComa,
            identificador,
            numero,
            cadena,
            caracter,
            comentario,
            comentarioML,
            nada
        }

        private tipo token;
        private String lexema;
        private int fila, columna;
        private Boolean agregadoListaTerminales;
        public Token(tipo token, String lexema, int fila, int columna)
        {
            this.token = token;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;
          
        }

        public String getToken()
        {

            switch (token)
            {
                case tipo.cadena:
                    return "cadena";
                case tipo.sPorcentaje:
                    return "sPorcentaje";
                case tipo.sInterrogacion:
                    return "sInterrogacion";
                case tipo.prConj:
                    return "prConj";
                case tipo.sOr:
                    return "sOr";
                case tipo.sVirgulilla:
                    return "sVirgulilla";
                case tipo.sDosPuntos:
                    return "sDosPuntos";
                case tipo.sLlaveDer:
                    return "sLlaveDer";
                case tipo.sLlaveIzq:
                    return "sLlaveIzq";
                case tipo.sComa:
                    return "sComa";
                case tipo.sPunto:
                    return "sPunto";
                case tipo.sPor:
                    return "sPor";
                case tipo.sMas:
                    return "sMas";
                case tipo.sMenos:
                    return "sMenos";
                case tipo.sMayor:
                    return "sMayor";
                case tipo.sMenor:
                    return "sMenor";
                case tipo.sPuntoyComa:
                    return "sPuntoyComa";
                case tipo.caracter:
                    return "caracter";
                case tipo.numero:
                    return "numero";
                case tipo.identificador:
                    return "identificador";
                case tipo.comentario:
                    return "comentario";
                case tipo.comentarioML:
                    return "comentarioML";
                case tipo.nada:
                    return "";

                default:
                    return "Error";

            }

        }

     

  
        public String getLexema()
        {
            return lexema;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }
    }
}
