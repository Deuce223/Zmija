using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication54
{
    public partial class Form2 : Form
    {
        
        int rezultat;
 
        public void setRezultat(int skor)
        {

            this.rezultat = skor;
            label3.Text = "Tvoj skor je  " + rezultat;


        }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string imeIgraca = textBox1.Text.Trim();
            
            if(imeIgraca == "")
            {

                imeIgraca = "Nepoznat igrac";

            }

            StreamReader vidi = new StreamReader("prazan.txt");
            if (vidi.ReadToEnd() == "")
            {
                
                vidi.Close();
               
                StreamWriter upisi = new StreamWriter("prazan.txt");

                upisi.Write(imeIgraca + " " + rezultat);
                upisi.Close();


            }

            else
            {
                vidi.Close();

                StreamWriter upisi = new StreamWriter("prazan.txt", append: true);

                upisi.Write("\r\n" + imeIgraca + " " + rezultat);
                upisi.Close();

            }

            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void Form2_Load(object sender, EventArgs e)
        {


            StreamReader otvori = new StreamReader("prazan.txt");

            string ceo = otvori.ReadToEnd();

            otvori.Close();

            if (ceo != "")
            {

                string[] deloviBaze = ceo.Split(new[] { "\r\n" }, StringSplitOptions.None);


                IComparer C2 = new Moja();

                Array.Sort(deloviBaze, C2);


                for (int i = 0; i < deloviBaze.Length; i++)
                {
                    if(i == panel1.Controls.Count)
                    {
                        break;
                    }

                    string[] pomoc = deloviBaze[i].Split(' ');

                    //  panel1.Controls[panel1.Controls.Count - 1 - i].Text = panel1.Controls[panel1.Controls.Count - 1 - i].Text + " " + Regex.Replace(deloviBaze[i], @"\d+", String.Empty).TrimEnd();
                    //  panel2.Controls[panel1.Controls.Count - 1 - i].Text = Regex.Match(deloviBaze[i], @"\d+").Value;

                    string pomocni = "";
                    for(int k = 0; k < pomoc.Length - 1; k++)
                    {

                        pomocni = pomocni + pomoc[k] + " ";

                    }

                    panel1.Controls[panel1.Controls.Count - 1 - i].Text = panel1.Controls[panel1.Controls.Count - 1 - i].Text + " " + pomocni;
                    panel2.Controls[panel1.Controls.Count - 1 - i].Text = pomoc[pomoc.Length - 1];


                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();


        }
    }
    public class Moja : IComparer
    {
        public int Compare(object a, object b)
        {
            string[] prvi = ((string)a).Split(' ');
            string[] drugi = ((string)b).Split(' ');

            if (Int32.Parse(prvi[prvi.Length - 1]) > Int32.Parse(drugi[drugi.Length - 1]))
            {

                return -1;


            }

            else if (Int32.Parse(prvi[prvi.Length - 1]) > Int32.Parse(drugi[drugi.Length - 1]))
            {

                return 1;


            }

            else
            {

                return 0;
            }


            //if (Int32.Parse(Regex.Match((string)a, @"\d+").Value) > Int32.Parse(Regex.Match((string)b, @"\d+").Value))
            //{

            //    return -1;

            //}


            //else if (Int32.Parse(Regex.Match((string)a, @"\d+").Value) < Int32.Parse(Regex.Match((string)b, @"\d+").Value))
            //{


            //    return 1;

            //}


            //else
            //{

            //    return 0;


            //}

        }


    }


}

