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
    public partial class frmLogin : Form
    {
        BookManager bm;
        DataTable dtBooks;
        public frmLogin()
        {
            InitializeComponent();
            bm = new BookManager();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string id = txtUserID.Text;
            string password = txtPassword.Text;
            if (bm.CheckLogin(id, password))
            {
                Employee employee = bm.FindEmployee(id);
                if (employee.Role)
                {
                    //neu61 Role = true
                    frmMaintainBooks fm = new frmMaintainBooks();
                    fm.ShowDialog();
                }
                else
                {
                    //Nếu role = false
                    frmChangeAccount frmChangeAccount = new frmChangeAccount(employee);
                    frmChangeAccount.ShowDialog();
                }
            } else
            {
                MessageBox.Show("Wrong id or password");
                return;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
