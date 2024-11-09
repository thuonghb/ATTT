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

        

        private void mãHóaCaserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mãCaeserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caeser caeser = new Caeser();
            caeser.MdiParent = this;
            caeser.Show();
        }

        private void mãHóaĐơnBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DonBang donBang = new DonBang();
            donBang.MdiParent = this; donBang.Show();
            
        }

        private void mãHóaPlayFairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayFair playFair = new PlayFair();
            playFair.MdiParent = this; playFair.Show();
            
        }

        private void mãAffineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Affine affine = new Affine();
            affine.MdiParent = this;
            affine.Show();
        }

        private void mãVigenereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vigenere vigenere = new Vigenere();
            vigenere.MdiParent = this; vigenere.Show();
            
        }

        private void mãHóaHillToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hill hill = new Hill();
            hill.MdiParent = this; hill.Show();
           
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mãRSAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RSA rsa = new RSA();
            this.Hide();
            rsa.Show();
        }
    }
}
