using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication54
{
    public partial class Form1 : Form
    {
        //zadnji!
        
        bool statusIzvrsenja = true;

        static List<Point> lista = new List<Point>(); // lista koja sadrzi sva polja

        int score; // rezultat

        Smerovi pravac = Smerovi.Desno; // indikator pravca

        Point pozicijaDodavanja; // pozicija za poslednju kockicu koja se dodaje zmiji

        List<Control> kontrole = new List<Control>(); // lista kontrole koje cine zmiju


        public Form1()
        {
            InitializeComponent();
        }


        static Form1() //lista sa svim poljima
        {

            for (int i = 0; i <= 400; i += 20)
            {


                for (int j = 0; j <= 320; j += 20)
                {


                    lista.Add(new Point(i, j));

                }
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Pomeranje();

            statusIzvrsenja = true; //provera da li je izvrsena bar jedna kretnja prilikom promene pravca
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //// provere koje onemogucavaju zmiju da krene u suprotnom smeru

          
            if (statusIzvrsenja)
            {

                if (e.KeyCode == Keys.D)     //provera koje je dugme na tastaturi stisnuto
                {

                    if (pravac != Smerovi.Levo)
                    {
                        
                        pravac = Smerovi.Desno;
  
                    }


                }

                else if (e.KeyCode == Keys.A)
                {

                    if (pravac != Smerovi.Desno)
                    {
                       
                        pravac = Smerovi.Levo;
    

                    }

                }

                else if (e.KeyCode == Keys.S)
                {

                    if (pravac != Smerovi.Gore)
                    {
                        
                        pravac = Smerovi.Dole;

                    }

                }

                else if (e.KeyCode == Keys.W)
                {

                    if (pravac != Smerovi.Dole)
                    {
                        
                        pravac = Smerovi.Gore;

                    }

                }

                statusIzvrsenja = false;
                
            }
        }

        public void Pomeranje()
        {

            if (kontrole.Count == 0) 
            {

                for (int i = 1; i < this.Controls.Count; i++) //pravimo listu kontrola(krecemo od 1 posto ce 0-ta pozicija uvek biti meta)
                {

                    kontrole.Add(this.Controls[i]);
    

                }

            }

            pozicijaDodavanja = kontrole[kontrole.Count - 1].Location;  // pamti se pozicija zadnje kockice u slucaju da se zmija povecava
            Point novi = new Point();

            if (pravac == Smerovi.Desno)
            {

              novi = new Point(kontrole[0].Location.X + 20, kontrole[0].Location.Y);
               
            }


            else if (pravac == Smerovi.Levo)
            {

               novi = new Point(kontrole[0].Location.X - 20, kontrole[0].Location.Y);
               
            }

            else if (pravac == Smerovi.Gore)
            {

              novi = new Point(kontrole[0].Location.X, kontrole[0].Location.Y - 20);
               
            }

            else if (pravac == Smerovi.Dole)
            {

              novi = new Point(kontrole[0].Location.X, kontrole[0].Location.Y + 20);
                
            }


            kontrole[kontrole.Count - 1].Location = novi;  // pomeramo zadnju na pocetak
            Control D1 = kontrole[kontrole.Count - 1];    // to radimo i u listi kako bi imali evidenciju gde se koja kockica nalazi(poslednju premestamo na pocetak liste)
            kontrole.RemoveAt(kontrole.Count - 1);
            kontrole.Insert(0, D1);
    
            kontrole[0].BackColor = Color.Red;
            kontrole[1].BackColor = Color.Bisque;

           
            if (this.Controls[0].Location == kontrole[0].Location)  //ovde proveravamo da li je zmija pogodila metu
            {

                // ako jeste dodajemo kockicu
                score = score + 25;
                Button C1 = new Button();
                C1.Size = new Size(20, 20);
                C1.BackColor = Color.Bisque;
                C1.Location = pozicijaDodavanja;
                this.Controls.Add(C1);             
                kontrole.Add(C1);


                // zatim pomeramo metu na sledecu random lokaciju

                List<Point> kopija = lista.ToList(); // sva polja kopiramo u novu listu

                for (int zed = 1; zed < this.Controls.Count; zed++)
                {
                    Point poredba = this.Controls[zed].Location;

                    if (kopija.Contains(poredba))
                    {

                        kopija.Remove(poredba);  //izbacuju se zauzeta polja
                    }

                }
                Random broj = new Random();
                int nasumicniBroj = broj.Next(0, kopija.Count - 1);
                this.Controls[0].Location = kopija[nasumicniBroj];   // stavljamo metu na novu poziciju(nacumicni clan liste slobodnih polja)

            }

            else  // u slucaju da nije pogodila metu proverava se da li je izasla van granica ili je udarila u samu sebe
            {

                Granice();

            }
        }
        public void Granice()
        {

                if (kontrole[0].Location.X < 0 || kontrole[0].Location.X > 400 || kontrole[0].Location.Y < 0 || kontrole[0].Location.Y > 320)  
                {


                    timer1.Stop();
                    Form2 drugaForma = new Form2();
                    drugaForma.setRezultat(score);
                    drugaForma.ShowDialog();
                    drugaForma.Dispose();

                        DialogResult drugi;
                        drugi = MessageBox.Show("Igraj opet?", "poruka", MessageBoxButtons.OKCancel);
                        if (drugi == DialogResult.Cancel)  // izlazak iz igre
                        {

                        
                        this.Close();
                        
                        

                         }

                        else if (drugi == DialogResult.OK)  // igraj ponovo
                        {
                            //resetovanje igre/forme!


                            score = 0;
                            kontrole.Clear();
                            pravac = Smerovi.Desno;
                            List<Control> listaKontrola = new List<Control>(); // lista svih kontrola ukljucujuci i metu(za dispose)

                            foreach (Control kod in this.Controls) 
                            {

                            listaKontrola.Add(kod);

                            }

                            this.KeyDown -= this.Form1_KeyDown;   //ukljanjanje eventova
                            this.timer1.Tick -= this.timer1_Tick;
                            this.Controls.Clear();   //brisanje svih kontrola i

                            foreach (Control kont in listaKontrola)  // dispose svih kontrola 
                            {

                             kont.Dispose();

                            }


                    InitializeComponent();   // resetujemo pocetni izgled
                    

                }

        

            }

                else if (kontrole.Count >= 5)
            {

                for (int l = 5; l < kontrole.Count; l++)
                {

                    if (kontrole[0].Location == kontrole[l].Location)  // ako se sudar desio, proveravamo da li se u nizu sa prepravljenim koordinatama desilo poklapanje
                    {


                        timer1.Stop();
                        Form2 drugaForma = new Form2();
                        drugaForma.setRezultat(score);
                        drugaForma.ShowDialog();
                        drugaForma.Dispose();

                            DialogResult drugi;
                            drugi = MessageBox.Show("Igraj opet?", "poruka", MessageBoxButtons.OKCancel);
                            if (drugi == DialogResult.Cancel)  // izlazak iz igre
                            {

                                this.Close();

                            }

                            else if (drugi == DialogResult.OK)  // igraj ponovo
                            {
                                //resetovanje igre/forme!


                                score = 0;
                                pravac = Smerovi.Desno;


                                kontrole.Clear(); // resetujemo listu za kretanje

                                List<Control> listaKontrola = new List<Control>();
                                foreach(Control kod in this.Controls)
                                {

                                    listaKontrola.Add(kod);
                                }


                                this.KeyDown -= this.Form1_KeyDown;
                                this.timer1.Tick -= this.timer1_Tick;
                                this.Controls.Clear();
                                foreach (Control kont in listaKontrola)
                                {

                                kont.Dispose();

                                }

                                InitializeComponent();   // resetujemo pocetni izgled

                              }
                      }
                  }
             }

        }
        public enum Smerovi
        {

            Levo,
            Desno,
            Gore,
            Dole

        }

    }
}
   
  

