using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class EstadoAFD
    {
        private int nombre;
        private List<Estado> estadosAFN;
        private List<TransicionAFD> transiciones;
        private Boolean agregado;
        private Boolean inicial;
        private Boolean aceptacion;
        public EstadoAFD(int nombre)
        {
            this.nombre = nombre;
            agregado = false;
            inicial = false;
            aceptacion = false;
            estadosAFN = new List<Estado>();
            transiciones = new List<TransicionAFD>();
        }

        public int getNombre()
        {
            return nombre;
        }

        public List<Estado> getEstadosAFN()
        {
            return estadosAFN;
        }

        public List<TransicionAFD> getListTrans()
        {
            return transiciones;
        }

        public Boolean isAgregado()
        {
            return agregado;
        }

        public Boolean isInicial()
        {
            return inicial;
        }

        public Boolean isAceptacion()
        {
            return aceptacion;
        }

        public void setNombre(int nombre)
        {
            this.nombre = nombre;
        }

        public void setAgregado(Boolean agregado)
        {
            this.agregado = agregado;
        }

        public void setInicial(Boolean inicial)
        {
            this.inicial = inicial;
        }

        public void setAceptacion(Boolean aceptacion)
        {
            this.aceptacion = aceptacion;
        }
        
    }
}
