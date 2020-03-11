using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class ExprecionRegular
    {
       private  String nombre;
        private LinkedList<Token> exprecion;


        public ExprecionRegular(String nombre, LinkedList<Token> exprecion)
        {
            this.nombre = nombre;
            this.exprecion = exprecion;
        }

        public ExprecionRegular()
        {
            exprecion = new LinkedList<Token>();
        }

        public String getNombre()
        {
            return nombre;
        }

        public LinkedList<Token> getExprecion()
        {
            return exprecion;
        }


        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void setExprecion(LinkedList<Token> exprecion)
        {
            this.exprecion = exprecion;
        }
    }
}
