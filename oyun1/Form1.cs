using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oyun1
{
    public partial class Form1 : Form
    {


        int sure = 0;
        int durum = 0; // 0 ise bir taş yukarıdan aşağı akmıyor demektir
                       // 1 ise hareket var demektir.
        int pozisyon_x; // 0-7
        int pozisyon_y; // 0-11
        string harf;  // hareket eden harf

        string harf1;
        string harf2;
        string harf3;
        string harf4;

        int puan;

        string[,] harita = new string[12, 8];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string kelime = textBox1.Text;

            harf1 = kelime.Substring(0, 1);
            harf2 = kelime.Substring(1, 1);
            harf3 = kelime.Substring(2, 1);
            harf4 = kelime.Substring(3, 1);

            int i, j;

            for (i = 0; i < 12; i++)
                for (j = 0; j < 8; j++) harita[i, j] = "1";
            puan = 0;
            label3.Text = Convert.ToString(puan);
            timer1.Enabled = false;
            form_yenile();
            timer1.Enabled = true;
            sure = 0;
            timer2.Enabled = true;
            harekete_basla();


        }

        private void form_yenile()
        {
            this.Refresh();
            Graphics cizim;

            cizim = this.CreateGraphics();
            Pen kalem = new Pen(Color.Blue, 1);

            cizim.DrawLine(kalem, 1, 1, 241, 1);
            cizim.DrawLine(kalem, 1, 31, 241, 31);
            cizim.DrawLine(kalem, 1, 61, 241, 61);
            cizim.DrawLine(kalem, 1, 91, 241, 91);
            cizim.DrawLine(kalem, 1, 121, 241, 121);
            cizim.DrawLine(kalem, 1, 151, 241, 151);
            cizim.DrawLine(kalem, 1, 181, 241, 181);
            cizim.DrawLine(kalem, 1, 211, 241, 211);
            cizim.DrawLine(kalem, 1, 241, 241, 241);
            cizim.DrawLine(kalem, 1, 271, 241, 271);
            cizim.DrawLine(kalem, 1, 301, 241, 301);
            cizim.DrawLine(kalem, 1, 331, 241, 331);
            cizim.DrawLine(kalem, 1, 361, 241, 361);

            cizim.DrawLine(kalem, 1, 1, 1, 361);
            cizim.DrawLine(kalem, 31, 1, 31, 361);
            cizim.DrawLine(kalem, 61, 1, 61, 361);
            cizim.DrawLine(kalem, 91, 1, 91, 361);
            cizim.DrawLine(kalem, 121, 1, 121, 361);
            cizim.DrawLine(kalem, 151, 1, 151, 361);
            cizim.DrawLine(kalem, 181, 1, 181, 361);
            cizim.DrawLine(kalem, 211, 1, 211, 361);
            cizim.DrawLine(kalem, 241, 1, 241, 361);

            int i, j;
            for (i = 0; i < 12; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if (harita[i, j] != "1")
                    {

                        Pen kalem1 = new Pen(Color.Red, 2);

                        int x1 = j * 30 + 2;
                        int y1 = i * 30 + 2;
                        cizim.DrawRectangle(kalem1, x1, y1, 29, 29);
                        cizim.DrawString(harita[i, j], new Font("Tahoma", 15), new SolidBrush(Color.White), x1, y1);

                    }
                }
            }





        }

        

        private void harekete_basla()
        {
            Random rasgele = new Random();
            pozisyon_x = rasgele.Next(0, 8);
            pozisyon_y = 0;
            int h = rasgele.Next(1, 5);
            if (h == 1) harf = harf1; ;
            if (h == 2) harf = harf2;
            if (h == 3) harf = harf3;
            if (h == 4) harf = harf4;
            harita[pozisyon_y, pozisyon_x] = harf;
            durum = 1;
            form_yenile();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (durum == 1)
            {
                if (pozisyon_y < 11)
                {
                    if ((harita[pozisyon_y + 1, pozisyon_x]) == "1")
                    {
                        harita[pozisyon_y, pozisyon_x] = "1";

                        harita[pozisyon_y + 1, pozisyon_x] = harf;
                        form_yenile();
                        pozisyon_y++;

                    }
                    else
                    {
                        durum = 0;
                        kelimeyi_kontrol_et();

                    }
                }
                else
                {
                    durum = 0;
                    kelimeyi_kontrol_et();
                }


            }
            else
            {
                kelimeyi_kontrol_et();
                harekete_basla();   

            }

        }

        private void kelimeyi_kontrol_et()
        {
            int i, j;
            for (i = 0; i < 12; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if (harita[i, j] == harf1)
                    {
                        if(((j + 3) <= 7) && (harita[i,j+1]==harf2))
                        {
                            if (harita[i, j + 2] == harf3)
                                if (harita[i, j + 3] == harf4)
                                {
                                
                                    int k, m;
                                   
                                    m = j;
                                    for(k=i;k>=1;k--)
                                    {
                                        harita[k, m] = harita[k-1 ,m];
                                        harita[k, m+1] = harita[k-1 , m+1];
                                        harita[k, m+2] = harita[k-1 , m+2];
                                        harita[k, m+3] = harita[k-1 , m +3];

                                    }
                                    puan++;
                                    label3.Text = Convert.ToString(puan);

                                    form_yenile();
                                }
                        }

                        if(((j-3)>=0) && (harita[i,j-1]==harf2))
                        {
                            if (harita[i, j - 2] == harf3)
                                if (harita[i, j - 3] == harf4)
                                {

                                    int k, m;

                                    m = j;
                                    for (k = i; k >= 1; k--)
                                    {
                                        harita[k, m] = harita[k - 1, m];
                                        harita[k, m - 1] = harita[k - 1, m - 1];
                                        harita[k, m - 2] = harita[k - 1, m - 2];
                                        harita[k, m - 3] = harita[k - 1, m - 3];

                                    }
                                    puan=puan+3;
                                    label3.Text = Convert.ToString(puan);

                                    form_yenile();
                                }


                        }

                        if (((i+ 3) <= 11) && (harita[i+1, j] == harf2))
                        {
                            if (harita[i+2, j] == harf3)
                                if (harita[i+3, j] == harf4)
                                {

                                    harita[i + 3, j] = "1";
                                    harita[i + 2, j] = "1";
                                    harita[i + 1, j] = "1";
                                    harita[i , j] = "1";
                                    puan = puan + 10;
                                    label3.Text = Convert.ToString(puan);

                                    form_yenile();
                                }


                        }


                        if (((i - 3) >= 0) && (harita[i - 1, j] == harf2))
                        {
                            if (harita[i - 2, j] == harf3)
                                if (harita[i - 3, j] == harf4)
                                {

                                    harita[i - 3, j] = "1";
                                    harita[i - 2, j] = "1";
                                    harita[i - 1, j] = "1";
                                    harita[i, j] = "1";
                                    puan = puan + 10;
                                    label3.Text = Convert.ToString(puan);

                                    form_yenile();
                                }


                        }

                    }
                }
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if ((pozisyon_y < 11) && ((pozisyon_x-1)>=0))
                {
                    if ((harita[pozisyon_y, pozisyon_x - 1]) == "1")
                    {
                        harita[pozisyon_y, pozisyon_x] = "1";

                        harita[pozisyon_y, pozisyon_x - 1] = harf;
                        pozisyon_x--;
                        form_yenile();


                    }
                }

            }
            if (keyData == Keys.Right)
            {

                if ((pozisyon_x + 1) < 8)
                {
                    if ((harita[pozisyon_y, pozisyon_x + 1]) == "1")
                    {
                        harita[pozisyon_y, pozisyon_x] = "1";

                        harita[pozisyon_y, pozisyon_x + 1] = harf;
                        pozisyon_x++;
                        form_yenile();

                    }
                }

            }

            if (keyData == Keys.Down)
            {
                if ((pozisyon_y +1)< 11)
                {
                    if ((harita[pozisyon_y + 1, pozisyon_x]) == "1")
                    {
                        harita[pozisyon_y, pozisyon_x] = "1";

                        harita[pozisyon_y + 1, pozisyon_x] = harf;
                        pozisyon_y++;
                        form_yenile();

                    }
                }

            }


            return base.ProcessCmdKey(ref msg, keyData);
        }  // processcmdkey

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            yardim y1 = new yardim();
            y1.ShowDialog();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sure++;
            label5.Text = sure.ToString();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
           
        }
    } // form1
} // namespace oyun1
