using ASM_4.DBUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm
{
    class Program
    {
        static void Main(string[] args)
        {
            BookManager bm = new BookManager();
            string id = "chihuy";
            string password = "123456";
            if (bm.CheckLogin(id, password))
            {
                Console.WriteLine("Successful");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
    }
}
