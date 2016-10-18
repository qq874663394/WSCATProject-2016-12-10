using System;
using System.Windows.Forms;
using WSCATProject.Warehouse;
using WSCATProject.Sales;
using WSCATProject.Base;

namespace WSCATProject
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SalesTicketForm());
        }
    }
}
