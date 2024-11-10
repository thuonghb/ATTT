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
    public partial class Affine : Form
    {
        AffineCipher affine = new AffineCipher();
        public Affine()
        {
            InitializeComponent();
        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(txtKhoaA.Text);
                int b = int.Parse(txtKhoaB.Text);
                string vanBan = txtVanBan.Text;

                string banMa = affine.MaHoaChuoi(vanBan, a, b);
                txtKetQua.Text = banMa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnGM_Click(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(txtKhoaA.Text);
                int b = int.Parse(txtKhoaB.Text);
                string banMa = txtVanBan.Text;

                string banRo = affine.GiaiMaChuoi(banMa, a, b);
                txtKetQua.Text = banRo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
