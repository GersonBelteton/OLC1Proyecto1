using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace OLC1Proyecto1
{
    public partial class Form1 : Form
    {
        AnalizadorLexico al = new AnalizadorLexico();
        List<Image> imagenes = new List<Image>();
        List<Image> imagenesAFD = new List<Image>();
        Boolean mostrarAFN = false;
        Boolean mostrarAFD = false;
        Boolean mostrarTT = false;
        public static RichTextBox[] txtEntrada = new RichTextBox[20];
        int contadorImagen = 0;
        public Form1()
        {
            InitializeComponent();
            nuevaPestana();
        }
        int i = 0;
        private void Button1_Click(object sender, EventArgs e)
        {
            //Nueva pestaña

            try
            {
                nuevaPestana();
            }
            catch(Exception )
            {
                MessageBox.Show("Demasiadas pestañas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }

        public void nuevaPestana()
        {



            tabControl.TabPages.Add("pestaña" + (i + 1));

            //  RichTextBox txtEntrada = new RichTextBox();
            tabControl.TabPages[i].Controls.Add(txtEntrada[i] = new RichTextBox());

            txtEntrada[i].SetBounds(0, 0, 492, 411);

            i++;
        }


        private void BtnAbrir_Click_1(object sender, EventArgs e)
        {
            TextReader leerArchivo;
            openFileDialog1.Filter = "er files (*.er)|*.er";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //   txtPrueba.Text = openFileDialog1.FileName;

                leerArchivo = new StreamReader(openFileDialog1.FileName);
                //  txtEntrada.Text = leerArchivo.ReadToEnd();
                int x = tabControl.TabPages.IndexOf(tabControl.SelectedTab);
                txtEntrada[x].Text = leerArchivo.ReadToEnd();
                tabControl.SelectedTab.Text = Path.GetFileName(openFileDialog1.FileName.ToString());
                leerArchivo.Close();

            }
        }

        private void BtnGuardar_Click_1(object sender, EventArgs e)
        {
            int x = tabControl.TabPages.IndexOf(tabControl.SelectedTab);

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "er files (*.er)|*.er";
            if (guardar.ShowDialog() == DialogResult.OK)
            {

      
                StreamWriter escribir = new StreamWriter(guardar.FileName);

                foreach (object line in txtEntrada[x].Lines)
                {
                    escribir.WriteLine(line);

                }

                escribir.Close();
            }
        }

        private void BtnAnalizar_Click(object sender, EventArgs e)
        {
            
            al.Analizar(txtEntrada[tabControl.TabPages.IndexOf(tabControl.SelectedTab)].Text);
          //  imagenes = al.getListaImg();
            //imagenes.Add(al.getListaImg()[al.getListaImg().Count - 1]);
            contadorImagen = al.getListaImg().Count() - 1;
            mostrarAFN = true;
            mostrarImagen();
          //  imagenAfn.Image = imagenes[imagenes.Count()-1];
            
            if (al.getListaTok().Count() > 0 && al.getListaEr().Count() == 0)
            {
                MessageBox.Show("Se ha generado una lista de Tokens", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if(al.getListaTok().Count() == 0 && al.getListaEr().Count() > 0)
            {
                MessageBox.Show("Se ha generado una lista de Errores", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if(al.getListaTok().Count() > 0 && al.getListaEr().Count() > 0)
            {
                MessageBox.Show("Se ha generado una lista de Tokens y de Errores", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //System.ArgumentNullException
        private void BtnGuardarTok_Click(object sender, EventArgs e)
        {


            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "xml files (*.xml)|*.xml";
            if (guardar.ShowDialog() == DialogResult.OK)
            {

                String fileName = guardar.FileName;

                try
                {

                    Token[] l = al.getListaTok().ToArray();

                    if (l.Count() > 0)
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlElement root = doc.CreateElement("ListaTokens");
                        doc.AppendChild(root);

                        for (int i = 0; i < l.Count(); i++)
                        {
                            XmlElement token = doc.CreateElement("token" + (i + 1));
                            root.AppendChild(token);

                            XmlElement nombre = doc.CreateElement("nombre");
                            nombre.AppendChild(doc.CreateTextNode(l[i].getToken()));
                            token.AppendChild(nombre);

                            XmlElement valor = doc.CreateElement("valor");
                            valor.AppendChild(doc.CreateTextNode(l[i].getLexema()));
                            token.AppendChild(valor);

                            XmlElement fila = doc.CreateElement("fila");
                            fila.AppendChild(doc.CreateTextNode(l[i].getFila().ToString()));
                            token.AppendChild(fila);
                           

                            XmlElement columna = doc.CreateElement("columna");
                            columna.AppendChild(doc.CreateTextNode(l[i].getColumna().ToString()));
                            token.AppendChild(columna);
                        }


                        doc.Save(fileName);
                    }
                }
                catch(System.ArgumentNullException )
                {
                    MessageBox.Show("Lista de Tokens vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

       
        }

        private void BtnGuardarEr_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "xml files (*.xml)|*.xml";
            if (guardar.ShowDialog() == DialogResult.OK)
            {

                String fileName = guardar.FileName;

                try
                {

                    Error[] l = al.getListaEr().ToArray();

                    if (l.Count() > 0)
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlElement root = doc.CreateElement("ListaErrores");
                        doc.AppendChild(root);

                        for (int i = 0; i < l.Count(); i++)
                        {
                            XmlElement error = doc.CreateElement("Error" + (i + 1));
                            root.AppendChild(error);

         

                            XmlElement valor = doc.CreateElement("valor");
                            valor.AppendChild(doc.CreateTextNode(l[i].getLexema()));
                            error.AppendChild(valor);

                            XmlElement fila = doc.CreateElement("fila");
                            fila.AppendChild(doc.CreateTextNode(l[i].getFila().ToString()));
                            error.AppendChild(fila);
                            int a = 2;

                            XmlElement columna = doc.CreateElement("columna");
                            columna.AppendChild(doc.CreateTextNode(l[i].getColumna().ToString()));
                            error.AppendChild(columna);
                        }


                        doc.Save(fileName);
                    }
                }
                catch (System.ArgumentNullException )
                {
                    MessageBox.Show("Lista de Errores vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            contadorImagen--;
            mostrarImagen();
        }

        public void mostrarImagen()
        {

            if(contadorImagen < 0)
            {
                contadorImagen = 0;
            }else if(contadorImagen == al.getListaImg().Count())
            {
                contadorImagen = al.getListaImg().Count() - 1;
            }

            if (mostrarAFN)
            {
                imagenAfn.Image = al.getListaImg()[contadorImagen];
            }else if (mostrarAFD)
            {
                imagenAfn.Image = al.getListaImgAFD()[contadorImagen];
            }
            else if(mostrarTT)
            {
                imagenAfn.Image = al.getListaImgTT()[contadorImagen];
            }
            

        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            contadorImagen++;
            mostrarImagen();
        }

        private void BtnAFN_Click(object sender, EventArgs e)
        {
            mostrarAFN = true;
            mostrarAFD = false;
            mostrarTT = false;
            mostrarImagen();
        }

        private void BtnAFD_Click(object sender, EventArgs e)
        {
            mostrarAFN = false;
            mostrarAFD = true;
            mostrarTT = false;
            mostrarImagen();
        }

        private void BtnTT_Click(object sender, EventArgs e)
        {
            mostrarAFN = false;
            mostrarAFD = false;
            mostrarTT = true;
            mostrarImagen();
        }
    }
}
