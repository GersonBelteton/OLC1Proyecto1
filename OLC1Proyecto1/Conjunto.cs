using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class Conjunto
    {
        private String nombre;
        private LinkedList<String> conjunto;

        public Conjunto(String nombre, LinkedList<String> conjunto)
        {
            this.nombre = nombre;
            this.conjunto = conjunto;
        }

        public Conjunto()
        {
            conjunto = new LinkedList<String>();
        }


        public String getNombre()
        {
            return nombre;
        }

        public LinkedList<String> getConjunto()
        {
            return conjunto;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void setConjunto(LinkedList<String> conjunto)
        {
            this.conjunto = conjunto;
        }
    }
}
