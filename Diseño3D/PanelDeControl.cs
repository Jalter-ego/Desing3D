using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diseño3D
{
    public partial class PanelDeControl : Form
    {
        private Game game;
        public PanelDeControl(Game game)
        {
            InitializeComponent();
            this.game = game;
            textX.Text = game.posX.ToString();
            textY.Text = game.posY.ToString();
            textZ.Text = game.posZ.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float x = float.Parse(textX.Text);
            float y = float.Parse(textY.Text);
            float z = float.Parse(textZ.Text);

            game.setCentro(x, y, z);
        }

        private void textX_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
