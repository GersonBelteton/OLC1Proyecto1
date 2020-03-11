using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1Proyecto1
{
    class TokenExp
    {

        private String token;
        private String lexema;
        private Boolean terminal, estGenerada, binario;
        private Estado inicial, final;


        public TokenExp(String token, String lexema, Boolean terminal, Boolean estGenerada, Boolean binario, Estado inicial, Estado final)
        {
            this.token = token;
            this.lexema = lexema;
            this.terminal = terminal;
            this.estGenerada = estGenerada;
            this.binario = binario;
            this.inicial = inicial;
            this.final = final;
        }

        public TokenExp(String token, String lexema, Boolean terminal, Boolean estGenerada, Boolean binario)
        {
            this.token = token;
            this.lexema = lexema;
            this.terminal = terminal;
            this.estGenerada = estGenerada;
            this.binario = binario;

        }
        /*   public TokenExp()
           {

           }*/

        public String getToken()
        {
            return token;
        }
        public String getLexema()
        {
            return lexema;
        }
        public Boolean isTerminal()
        {
            return terminal;
        }
        public Boolean isEstGenerada()
        {
            return estGenerada;
        }
        public Boolean isBinario()
        {
            return binario;
        }
        public Estado getInicial()
        {
            return inicial;
        }
        public Estado getFinal()
        {
            return final;
        }


        public void setToken(String token)
        {
            this.token = token;
        }
        public void setLexema(String lexema)
        {
            this.lexema = lexema;
        }
        public void setTerminal(Boolean terminal)
        {
            this.terminal = terminal;
        }
        public void setEstGenerada(Boolean estGenerada)
        {
            this.estGenerada = estGenerada;
        }
        public void setBinario(Boolean binario)
        {
            this.binario = binario;
        }
        public void setInicial(Estado inicial)
        {
            this.inicial = inicial;
        }
        public void setFinal(Estado final)
        {
            this.final = final;
        }
    }
}
