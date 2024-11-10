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
    public partial class PlayFair : Form
    {
        playfairCipher playfair = new playfairCipher();
        public PlayFair()
        {
            InitializeComponent();
        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            string khoa = txtKey.Text;
            string vanBan = txtVanBan.Text;

            playfair.TaoMaTran(khoa);
            txtKetQua.Text = playfair.MaHoa(vanBan);
        }

        private void btnGM_Click(object sender, EventArgs e)
        {
            string khoa = txtKey.Text;
            string banMa = txtVanBan.Text;

            playfair.TaoMaTran(khoa);
            txtKetQua.Text = playfair.GiaiMa(banMa);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
