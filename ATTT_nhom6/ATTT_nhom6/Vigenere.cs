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
    public partial class Vigenere : Form
    {
        vigenereCipher vigenere = new vigenereCipher();
        public Vigenere()
        {
            InitializeComponent();
        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            string vanBan = txtVanBan.Text;
            string khoa = txtKey.Text;

            if (vigenere.KiemTraKhoa(khoa))
            {
                txtKetQua.Text = vigenere.MaHoa(vanBan, khoa);
            }
            else
            {
                MessageBox.Show("Khóa không hợp lệ! Vui lòng nhập khóa chỉ chứa các ký tự chữ cái.");
            }
        }

        private void btnGM_Click(object sender, EventArgs e)
        {
            string vanBan = txtVanBan.Text;
            string khoa = txtKey.Text;

            if (vigenere.KiemTraKhoa(khoa))
            {
                txtKetQua.Text = vigenere.GiaiMa(vanBan, khoa);
            }
            else
            {
                MessageBox.Show("Khóa không hợp lệ! Vui lòng nhập khóa chỉ chứa các ký tự chữ cái.");
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
