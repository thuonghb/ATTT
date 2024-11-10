using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATTT_nhom6
{
    public partial class RSA : Form
    {
        private long publicKeyE;
        private long publicKeyN;
        private long privateKeyD;
        public RSA()
        {
            InitializeComponent();
        }
        private void GenerateKeys(int p, int q, int e)
        {
            long n = (long)p * q;                 // n = p * q
            long phi = (long)(p - 1) * (q - 1);   // phi(n) = (p - 1) * (q - 1)

            publicKeyE = e;
            publicKeyN = n;
            privateKeyD = ModInverse(e, phi);     // Tính khóa riêng d
        }

        // Hàm tính nghịch đảo modular
        private long ModInverse(long e, long phi)
        {
            long d = 0, x1 = 0, x2 = 1, y1 = 1, tempPhi = phi;

            while (e > 0)
            {
                long temp1 = tempPhi / e;
                long temp2 = tempPhi - temp1 * e;
                tempPhi = e;
                e = temp2;

                long x = x2 - temp1 * x1;
                x2 = x1;
                x1 = x;

                long y = d - temp1 * y1;
                d = y1;
                y1 = y;
            }

            if (tempPhi == 1)
            {
                return d + phi;
            }

            return -1;
        }

        // Hàm mã hóa văn bản
        private long Encrypt(long message, long e, long n)
        {
            return ModPow(message, e, n);
        }

        // Hàm giải mã văn bản
        private long Decrypt(long cipher, long d, long n)
        {
            return ModPow(cipher, d, n);
        }

        // Hàm tính lũy thừa modulus
        private long ModPow(long baseValue, long exponent, long modulus)
        {
            long result = 1;
            baseValue = baseValue % modulus;
            while (exponent > 0)
            {
                if ((exponent % 2) == 1)
                    result = (result * baseValue) % modulus;
                exponent = exponent >> 1;
                baseValue = (baseValue * baseValue) % modulus;
            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ TextBox
                int p = int.Parse(txtP.Text);
                int q = int.Parse(txtQ.Text);
                int eValue = int.Parse(txtE.Text);

                // Gọi hàm sinh khóa
                GenerateKeys(p, q, eValue);

                // Hiển thị khóa công khai và khóa riêng
                txtPublicKey.Text = $"(e, n): ({publicKeyE}, {publicKeyN})";
                txtPrivateKey.Text = $"(d, n): ({privateKeyD}, {publicKeyN})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPlaintext.Text))
                {
                    // Mã hóa văn bản
                    long plaintext = long.Parse(txtPlaintext.Text);
                    long ciphertext = Encrypt(plaintext, publicKeyE, publicKeyN);
                    txtCiphertext.Text = ciphertext.ToString();
                }
                else if (!string.IsNullOrEmpty(txtCiphertext.Text))
                {
                    // Giải mã văn bản
                    long ciphertext = long.Parse(txtCiphertext.Text);
                    long plaintext = Decrypt(ciphertext, privateKeyD, publicKeyN);
                    txtPlaintext.Text = plaintext.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
