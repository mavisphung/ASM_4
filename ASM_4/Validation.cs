using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_4
{
    public class Validation
    {
        public static bool isNumber(string strNumber, string obj)
        {
            bool result = false;
            try
            {
                int number = int.Parse(strNumber);
                if (number <= 0)
                    throw new ArgumentOutOfRangeException(" must be > 0");
                else
                    result = true;

            }
            catch (ArgumentOutOfRangeException ae)
            {
                MessageBox.Show(obj + ae.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(obj + " must be numberic");
            }
            return result;
        }
    }
}
