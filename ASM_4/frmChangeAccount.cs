using ASM_4.DBUtil;
using ASM_4.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_4
{
    public partial class frmChangeAccount : Form
    {
        Employee emp;
        BookManager bm;
        public frmChangeAccount(Employee e)
        {
            InitializeComponent();
            bm = new BookManager();
            this.emp = e;
            LoadData();
        }

        private void LoadData()
        {
            lblUserID.Text = emp.ID;
            lblPassword.Text = emp.Password;
            lblRole.Text = emp.Role.ToString();
        }

        private void btnChangePassword_Click(object sender, EventArgs ex)
        {
            string newPassword = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            if (newPassword.Equals(confirmPassword))
            {
                bool r = bm.UpdateEmployee(new Employee
                {
                    ID = lblUserID.Text,
                    Password = confirmPassword,
                    Role = true,
                });
                string result = r ? "successfully" : "failed";
                MessageBox.Show("Update " + result);
                Employee emp = bm.FindEmployee(lblUserID.Text);
                lblPassword.Text = emp.Password;
                lblRole.Text = emp.Role.ToString();
            } else
            {
                MessageBox.Show("Password is not correct");
            }
        }

        private void frmChangeAccount_Load(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
