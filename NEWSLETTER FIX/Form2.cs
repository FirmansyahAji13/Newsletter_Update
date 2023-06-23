using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;

namespace NEWSLETTER_FIX
{
    public partial class Form2 : Form
    {
        private const string connectionString = "Host=localhost; Port=5432; Database=JT-Apps ;Username=postgres; Password=timotius";
        private NpgsqlConnection connection;
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }

        private void tbjudul_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pbnewsletterheading.Image.Save(ms, pbnewsletterheading.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            string judul = tbjudul.Text;
            string deskripsi = tbdeskripsi.Text;
            string link_berita = tblink.Text;
            DateTime tanggal = dtptanggal.Value;

            string query = "UPDATE newsletter SET judul=@judul, deskripsi=@deskripsi, link_berita=@link_berita, tanggal=@tanggal, foto=@foto WHERE newsletter_id= @newsletter_id";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@judul", judul);
                command.Parameters.AddWithValue("@deskripsi", deskripsi);
                command.Parameters.AddWithValue("@link_berita", link_berita);
                command.Parameters.AddWithValue("@tanggal", tanggal);
                command.Parameters.AddWithValue("@foto", imageBytes);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    {
                        MessageBox.Show("Data berhasil diperbarui!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void tbdeskripsi_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtptanggal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lbllink_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pbnewsletterheading_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbnewsletterheading.Image = new Bitmap(openFileDialog.FileName);
            }
        }
    }
}
