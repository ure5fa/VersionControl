using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            //listusers.DataSource = users;
            //'listUsers.ValueMember = "ID";
            //''listUsers.DisplayMember = "FullName";

            label1.Text = Resource1.Lastname; // label1
            label2.Text = Resource1.Utónév; // label2
            button1.Text = Resource1.Add; // button1
            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                LastName = textBox1.Text,
                FirstName = textBox2.Text
            };
            users.Add(u);
        }
    }
}
