using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class TransicionAFD
    {
        private EstadoAFD inicial;
        private EstadoAFD final;
        private String simbolo;

        public TransicionAFD (EstadoAFD inicial, EstadoAFD final, String simbolo)
        {
            this.inicial = inicial;
            this.final = final;
            this.simbolo = simbolo;
        }

        public EstadoAFD getInicial()
        {
            return inicial;
        }

        public EstadoAFD getFinal()
        {
            return final;
        }

        public String getSimbolo()
        {
            return simbolo;
        }

        public void setInicial(EstadoAFD inicial)
        {
            this.inicial = inicial;
        }

        public void setFinal(EstadoAFD final)
        {
            this.final = final;
        }

        public void setSimbolo(String simbolo)
        {
            this.simbolo = simbolo;
        }

    }
}
