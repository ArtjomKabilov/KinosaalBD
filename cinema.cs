using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVorm
{
    public partial class cinema :Form
    {
        static string conn_KinoDB = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\Kinosaal23-master\AppData\Database1.mdf;Integrated Security=True";
        SqlConnection connect_to_DB = new SqlConnection(conn_KinoDB);
        SqlCommand command;
        SqlDataAdapter adapter;
        DataGridView dataGridView;
        DataTable tabel;
        string[] Filmid;
        int Id;
        int index;
        List<PictureBox> kavaBoxList;
        public static int m = 1;
        //public List<string> listOfMovies;
        //public List<string> listOfMovies_2;
        Button btn_scroll_back;
        Button btn_scroll_next;
        Button choose;
        Button btn, btn2;
        public PictureBox picture;
        Button select;
        public string rt;
        public Label nameMovie;
        public cinema()
        {
            connect_to_DB.Open();
            tabel = new DataTable();
            dataGridView = new DataGridView();
            adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Movie]", connect_to_DB);
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;

            Filmid = new string[tabel.Rows.Count];
            var index = 0;
            foreach (DataRow row in tabel.Rows)
            {
                var film = row["image"];
                Filmid[index++] = $"{film}";
            }
            connect_to_DB.Close();

            kavaBoxList = new List<PictureBox>();
            foreach (var f in Filmid)
            {
                PictureBox pic = new PictureBox
                {
                    Image = Image.FromFile(@"..\..\image\" + f)
                };
                kavaBoxList.Add(pic);

            };
            index = 0;
            picture = new PictureBox
            {
                Image = kavaBoxList[index].Image,
                Location = new Point(200, 200),
                Size = new Size(150, 250),
                SizeMode = PictureBoxSizeMode.StretchImage


            };
            this.Controls.Add(picture);
            picture.MouseDown += Kava_MouseDown;
            MessageBox.Show("Vajuta hiire paremat või vasakut nuppu", "Paremale või vasakule liikumiseks");

            btn = new Button()
            {
                Size = new Size(75, 75),
                Location = new Point(50, 100),
                Text = "Admin"
            };
            this.Controls.Add(btn);
            this.Controls.Add(btn2);
            btn.MouseClick += Btn_MouseClick;
            //listOfMovies_2 = new List<string>() { "SpongeBob", "Jumanji", "StarWars"};
            this.Size = new System.Drawing.Size(600,800);
            this.BackgroundImage = new Bitmap(@"../../image/kos.jpg");
            Label lbl = new Label()
            {
                Text = "Tere tulemast kinno!",
                Location = new System.Drawing.Point(150, 30),
                Size = new Size(240, 40),
                Font = new Font("Arial", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(39, 61, 167)

            };
            Label lbl2 = new Label()
            {
                Text = "Valige film",
                Location = new System.Drawing.Point(150, 70),
                Size = new Size(240, 40),
                Font = new Font("Arial", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(39, 61, 167)

            };
            this.Controls.Add(lbl);
            this.Controls.Add(lbl2);
            /*nameMovie = new Label()
            {
                Text = listOfMovies_2[m],
                Font = new Font(Font.FontFamily, 20),
                Location = new Point(200, 70),
                BackColor = Color.Purple,
                ForeColor = Color.White,
                AutoSize = true
            };
            List<string> lists = new List<string>();

            lists.Add("sb.jpg");
            lists.Add("zw.jpg");
            lists.Add("jm.jpg");
            */
            btn_scroll_back = new Button()
            {
                Size = new Size(75, 75),
                Location = new Point(130, 260),
                Text = "Left"
            };

            //btn_scroll_back.Click += Btn_scroll_back_Click;


            btn_scroll_next = new Button()
            {
                Size = new Size(75, 75),
                Location = new Point(350, 260),
                Text = "Right"
            };

           // btn_scroll_next.Click += Btn_scroll_next_Click;


            /*picture = new PictureBox()
            {
                ImageLocation = ($"../../image/{lists[2]}"),
                Location = new Point(200, 200),
                Size = new Size(150, 250),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            this.Controls.Add(picture);
            picture.MouseClick += Picture_MouseClick;*/


            select = new Button()
            {
                Text = "Select movie",
                Size = new Size(150, 250),
                Location = new Point(200,400),
                Font = new Font(Font.FontFamily, 10),
            };

            
            select.Hide();

            this.Controls.Add(choose);
            this.Controls.Add(select);
            this.Controls.Add(btn_scroll_back);
            this.Controls.Add(btn_scroll_next);



           // listOfMovies = new List<string>() { "sb.jpg", "jm.jpg", "zw.jpg" };





        }


        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {   
            admin op = new admin();
            op.Show();
        }

        private void Picture_MouseClick(object sender, MouseEventArgs e)
        {
            room saal = new room("Valige koht", "", "Väike", "Keskmine", "Suur");
            saal.StartPosition = FormStartPosition.CenterScreen;
            saal.ShowDialog();
        }

        /*private void Select_Click(object sender, EventArgs e)
        {
            MyForm uus_aken = new MyForm("Choose a hall", "", "Small", "Middle", "Big", "VIP");
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.ShowDialog();
        }*/

        /*public void Btn_scroll_next_Click(object sender, EventArgs e)
        {
            rt = M();
        }

        public void Btn_scroll_back_Click(object sender, EventArgs e)
        {
            rt = M1();
        }*/

        /*public string M()
        {
            if (m <= 1)
            {
                btn_scroll_back.Show();
                m++;
                picture.ImageLocation = ($"../../image/{listOfMovies[m]}");
                nameMovie.Text = listOfMovies_2[m];
            }
            if (m == 2)
            {
                btn_scroll_next.Hide();
            }

            return nameMovie.Text;
        }

        public string M1()
        {
            if (m > 0)
            {
                btn_scroll_next.Show();
                m--;
                picture.ImageLocation = ($"../../image/{listOfMovies[m]}");
                nameMovie.Text = listOfMovies_2[m];
            }
            if (m == 0)
            {
                btn_scroll_back.Hide();
            }
            return nameMovie.Text;
        }*/
        private void Kava_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    if (index >= Filmid.Count() - 1)
                    {
                        index = Filmid.Count() - 1;
                    }
                    else
                    { index++; }
                    break;
                case MouseButtons.Left:
                    if (index <= 0)
                    {
                        index = 0;
                    }
                    else
                    { index--; }
                    break;
            }
            picture.Image = kavaBoxList[index].Image;
            //filminimetus = Film(kavaBoxList[index].ToString());
        }
    }
}
