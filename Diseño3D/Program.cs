using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseño3D
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());*/
            using (var game = new Game())
            {
                game.Run(60.0);  
            }
        }
    }
}
