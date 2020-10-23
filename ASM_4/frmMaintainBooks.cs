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
    public partial class frmMaintainBooks : Form
    {

        BookManager bm;
        DataTable dtBooks;
        public frmMaintainBooks()
        {
            InitializeComponent();
            bm = new BookManager();

            LoadData();
        }

        public void LoadData()
        {
            dtBooks = bm.GetBooks();
            dtBooks.PrimaryKey = new DataColumn[] { dtBooks.Columns["ID"] };
            txtBookID.ReadOnly = true;
            txtBookName.ReadOnly = true;
            txtPrice.ReadOnly = true;



            txtBookID.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();

            txtBookID.DataBindings.Add("text", dtBooks, "ID");
            txtBookName.DataBindings.Add("text", dtBooks, "BookName");
            txtPrice.DataBindings.Add("text", dtBooks, "Price");

            dgvBookList.DataSource = dtBooks;
            bsBooks.DataSource = dgvBookList.DataSource;
            bnBooks.BindingSource = bsBooks;
        }

        private void frmMaintainBooks_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string bookID = txtBookID.Text;
            string bookname = txtBookName.Text;
            string strPrice = txtPrice.Text;
            if (!Validation.isNumber(bookID, "BookID")
                    || !Validation.isNumber(strPrice, "Price"))
            {
                return;
            }

            int id = int.Parse(bookID);
            float price = float.Parse(strPrice);
            Book b = new Book
            {
                ID = id,
                Name = bookname,
                Price = price,
            };

            bool r = bm.AddNewBook(b);
            string result = r ? "successfully" : "failed";
            MessageBox.Show("Add " + result);
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string bookID = txtBookID.Text;
            string bookname = txtBookName.Text;
            string strPrice = txtPrice.Text;
            if (!Validation.isNumber(bookID, "BookID")
                    || !Validation.isNumber(strPrice, "Price"))
            {
                return;
            }

            int id = int.Parse(bookID);
            float price = float.Parse(strPrice);
            Book b = new Book
            {
                ID = id,
                Name = bookname,
                Price = price,
            };

            bool r = bm.UpdateBook(b);
            string result = r ? "successfully" : "failed";
            MessageBox.Show("Update " + result);
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtBookID.ReadOnly = false;
            txtBookName.ReadOnly = false;
            txtPrice.ReadOnly = false;
        }

        private void dgvBookList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBookID.ReadOnly = true;
            txtBookName.ReadOnly = true;
            txtPrice.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string bookID = txtBookID.Text;
            if (!Validation.isNumber(bookID, "BookID"))
            {
                return;
            }

            int id = int.Parse(bookID);
            bool r = bm.DeleteBook(id);
            string result = r ? "successfully" : "failed";
            MessageBox.Show("Delete " + result);
            LoadData();
        }

        private void GetBooksByTitle(string s)
        {
            dtBooks = bm.GetBooksByTitle(s);
            //Xóa ràng buộc dữ liệu trên các textbox để binding lại lần sau
            txtBookID.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();

            //Ràng buộc dữ liệu trên textbox
            txtBookID.DataBindings.Add("Text", dtBooks, "id");
            txtBookName.DataBindings.Add("Text", dtBooks, "BookName");
            txtPrice.DataBindings.Add("Text", dtBooks, "Price");

            //ràng buộc dữ liệu cho GridView
            dgvBookList.DataSource = dtBooks;

            //Tính tổng số lượng sách
            lblTotalQuantity.Text = "Total Quantity: " +
                                        dtBooks.Compute("SUM(Price)", string.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string strText = textBox1.Text;
            if (strText.Length == 0)
            {
                LoadData();
            } else
            {
                GetBooksByTitle(strText);
            }
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmBookReport fbr = new frmBookReport();
            fbr.ShowDialog();
        }
    }
}
