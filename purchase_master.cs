using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class purchase_master : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Uzah01\Desktop\WindowsFormsApp2\Inventory.mdf;Integrated Security=True");
        public purchase_master()
        {
            InitializeComponent();
        }

        private void purchase_master_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            fill_product_name();
            fill_dealer_name();
        }


        public void fill_product_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["product_name"].ToString());
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {  
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name where product_name='"+ comboBox1.Text +"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label3.Text = dr["units"].ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void fill_dealer_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dealer_info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["dealer_name"].ToString());
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int i;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from stock where product_name='" + comboBox1.Text +"'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            i = Convert.ToInt32(dt1.Rows.Count.ToString());

            if (i==0)
            {
                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "insert into purchase_master values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','" + textBox4.Text + "')";
                cmd4.ExecuteNonQuery();

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into stock values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "')";
                cmd3.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into purchase_master values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','" + textBox4.Text + "')";
                cmd2.ExecuteNonQuery();

                SqlCommand cmd5 = con.CreateCommand();
                cmd5.CommandType = CommandType.Text;
                cmd5.CommandText = "update stock set product_qty=product_qty + '"+ textBox1.Text + "' where product_name='"+ comboBox1.Text +"'";
                cmd5.ExecuteNonQuery();
            }

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into purchase_master values('"+ comboBox1.Text +"','"+ textBox1.Text +"','"+ label3.Text + "','"+ textBox2.Text + "','"+ textBox3.Text + "','"+ dateTimePicker1.Value.ToString("dd-MM-yyyy") +"','"+ comboBox2.Text +"','"+ comboBox3.Text + "','"+ dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','"+ textBox4.Text +"')";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Record inserted successfully");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}