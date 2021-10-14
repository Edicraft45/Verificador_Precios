using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private int segundos = 0;

        private String codigo = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, 0);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, pictureBox1.Height + 10);
            pictureBox2.Location = new Point(this.Width/2 - pictureBox2.Width/2,this.Height/2);
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //MessageBox.Show("vamos a buscar el producto "+codigo);
                try
                {
                    MySqlConnection servidor;
                    servidor = new MySqlConnection("server = 127.0.0.1; user = root; database = verificador_precios; SSL Mode = None; ");
                    servidor.Open();
                    string query = "SELECT producto_nombre, producto_precio, producto_stock, producto_imagen FROM productos WHERE producto_codigo =" + codigo + ";";
                    //MessageBox.Show(query);
                    MySqlCommand consulta;
                    consulta = new MySqlCommand(query, servidor);
                    MySqlDataReader resultado = consulta.ExecuteReader();

                    if (resultado.HasRows)
                    {
                        resultado.Read();
                        //MessageBox.Show(resultado.GetString(1));
                        label1.Visible = false;
                        label2.Visible = true;
                        label3.Visible = false;
                        label4.Visible = false;
                        label5.Visible = true;
                        //label6.Visible = false;
                        pictureBox2.Visible=false;
                        pictureBox3.Visible = true;
                        label2.Text = resultado.GetString(0)+Environment.NewLine+"Precio:"+resultado.GetString(1) +
                            Environment.NewLine + "Stock:" + resultado.GetString(2);
                        pictureBox3.ImageLocation = resultado.GetString(3);
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        segundos = 0;
                        timer1.Enabled = true;
                    }
                    else
                    {
                        label3.Visible = true;
                        label4.Visible = true;
                        label2.Visible = false;
                        label1.Visible = false;
                        label5.Visible = false;
                        //label6.Visible = false;
                        pictureBox2.Visible = false;
                        pictureBox3.Visible = false;
                        pictureBox4.Visible = true;
                        segundos = 0;
                        timer1.Enabled = true;
                        //MessageBox.Show("Llame al supervisor, el producto no fue encontrado");
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                codigo = "";
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
            segundos++;

            if (segundos == 4)
            {
                timer1.Enabled = false;
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                label1.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                //label6.Visible = false;
                label2.Text = "";
            }
		}

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}