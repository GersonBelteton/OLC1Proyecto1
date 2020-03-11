using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class Transicion
    {

        private Estado inicial;
        private Estado final;
        private String simbolo;

        public Transicion(Estado inicial, Estado final, String simbolo)
        {
            this.inicial = inicial;
            this.final = final;
            this.simbolo = simbolo;
        }

        public Estado getInicial()
        {
            return inicial;
        }

        public Estado getFinal()
        {
            return final;
        }

        public String getSimbolo()
        {
            return simbolo;
        }

        public void setInicial(Estado inicial)
        {
            this.inicial = inicial;
        }

        public void setFinal(Estado final)
        {
            this.final = final;
        }

        public void setSimbolo(String simbolo)
        {
            this.simbolo = simbolo;
        }
    }
}
