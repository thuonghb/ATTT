using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATTT_nhom6
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void mãHóaHillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hill hill = new Hill();
            this.Hide();
            hill.Show();
        }

        private void mãHóaCaserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mãCaeserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caeser caeser = new Caeser();
            this.Hide();
            caeser.ShowDialog();
        }

        private void mãHóaĐơnBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DonBang donBang = new DonBang();
            donBang.ShowDialog();
        }

        private void mãHóaPlayFairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayFair playFair = new PlayFair();
            playFair.ShowDialog();
        }

        private void mãAffineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Affine affine = new Affine();
            affine.ShowDialog();
        }

        private void mãVigenereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vigenere vigenere = new Vigenere();
            vigenere.ShowDialog();
        }

        private void mãHóaHillToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hill hill = new Hill();
            hill.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
