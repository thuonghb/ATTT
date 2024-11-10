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
    public partial class Diffie_Hellman : Form
    {
        public Diffie_Hellman()
        {
            InitializeComponent();
        }

        private long ModPow(long baseValue, long exp, long mod)
        {
            long result = 1;
            baseValue = baseValue % mod;

            while (exp > 0)
            {
                if ((exp % 2) == 1) // Nếu lũy thừa là số lẻ
                {
                    result = (result * baseValue) % mod;
                }
                exp = exp >> 1; // Chia lũy thừa cho 2
                baseValue = (baseValue * baseValue) % mod;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Khai báo biến cho giá trị q, alpha, Xa, Xb
                long q, alpha, Xa, Xb;

                // Kiểm tra và chuyển đổi giá trị q
                if (!long.TryParse(txtQ.Text, out q) || q <= 0)
                {
                    MessageBox.Show("Giá trị của số nguyên tố q không hợp lệ!", "Lỗi đầu vào", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra và chuyển đổi giá trị alpha
                if (!long.TryParse(txtAlpha.Text, out alpha) || alpha <= 0)
                {
                    MessageBox.Show("Giá trị của alpha không hợp lệ!", "Lỗi đầu vào", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra và chuyển đổi giá trị khóa riêng Alice (Xa)
                if (!long.TryParse(txtPrivateXA.Text, out Xa) || Xa <= 0)
                {
                    MessageBox.Show("Giá trị của khóa riêng Alice (Xa) không hợp lệ!", "Lỗi đầu vào", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra và chuyển đổi giá trị khóa riêng Bob (Xb)
                if (!long.TryParse(txtPrivateXB.Text, out Xb) || Xb <= 0)
                {
                    MessageBox.Show("Giá trị của khóa riêng Bob (Xb) không hợp lệ!", "Lỗi đầu vào", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tính toán khóa công khai Y_A và Y_B
                long Ya = ModPow(alpha, Xa, q); // Y_A = alpha^Xa mod q
                long Yb = ModPow(alpha, Xb, q); // Y_B = alpha^Xb mod q

                // Hiển thị khóa công khai lên giao diện
                txtPublicYa.Text = Ya.ToString(); // Hiển thị khóa công khai của Alice
                txtPublicYb.Text = Yb.ToString(); // Hiển thị khóa công khai của Bob

                // Tính toán khóa bí mật chung K
                long Ka = ModPow(Yb, Xa, q); // Khóa bí mật Alice tính
                long Kb = ModPow(Ya, Xb, q); // Khóa bí mật Bob tính

                // Hiển thị khóa bí mật lên giao diện
                txtSecretKeyA.Text = Ka.ToString(); // Hiển thị khóa bí mật của Alice
                txtSecretKeyB.Text = Kb.ToString(); // Hiển thị khóa bí mật của Bob

                // Kiểm tra nếu khóa bí mật của Alice và Bob trùng khớp
                if (Ka == Kb)
                {
                    lblResult.Text = "Khóa bí mật của Alice và Bob trùng khớp!";
                    lblResult.ForeColor = System.Drawing.Color.Green; // Đặt màu xanh nếu đúng
                }
                else
                {
                    lblResult.Text = "Khóa bí mật của Alice và Bob không khớp!";
                    lblResult.ForeColor = System.Drawing.Color.Red;   // Đặt màu đỏ nếu sai
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ trong quá trình tính toán
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
