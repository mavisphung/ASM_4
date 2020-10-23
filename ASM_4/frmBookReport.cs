using ASM_4.DBUtil;
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
    public partial class frmBookReport : Form
    {

        BookManager bm;
        DataTable dtBooks;

        public frmBookReport()
        {
            InitializeComponent();
            bm = new BookManager();
            LoadData();
        }

        public void LoadData()
        {
            dtBooks = bm.GetBooks();
            dtBooks.PrimaryKey = new DataColumn[] { dtBooks.Columns["ID"] };

            bsBooks.DataSource = dtBooks;
            dgvBooks.DataSource = bsBooks;
            bsBooks.Sort = "Price ASC";
        }

        private void frmBookReport_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
