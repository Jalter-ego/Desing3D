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
        private Objeto selectObjet;
        public PanelDeControl(Game game)
        {
            InitializeComponent();
            this.game = game;
            textX.Text = 0.ToString();
            textY.Text = 0.ToString();
            textZ.Text = 0.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float x = float.Parse(textX.Text);
            float y = float.Parse(textY.Text);
            float z = float.Parse(textZ.Text);

            selectObjet.SetCentro(x, y, z);
        }

        private void textX_TextChanged(object sender, EventArgs e)
        {

        }

        private void objetUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectObjet = this.game.objet_U;
            textX.Text = this.selectObjet.cx.ToString();
            textY.Text = this.selectObjet.cy.ToString();
            textZ.Text = this.selectObjet.cz.ToString();
        }

        private void objetU2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectObjet = this.game.objet_U2;
            textX.Text = this.selectObjet.cx.ToString();
            textY.Text = this.selectObjet.cy.ToString();
            textZ.Text = this.selectObjet.cz.ToString();
        }
    }
}
