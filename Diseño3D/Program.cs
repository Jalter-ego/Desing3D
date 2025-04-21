using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseño3D
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                //PanelDeControl panel = new PanelDeControl(game);
                //panel.Show();
                game.Run(60.0);  
            }
        }
    }
}
