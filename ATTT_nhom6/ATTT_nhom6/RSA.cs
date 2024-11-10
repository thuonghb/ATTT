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
            privateKeyD = ExtendedEuclidean(e, phi);  // Tính khóa riêng d bằng Euclide mở rộng
        }

        // Hàm Euclide mở rộng để tìm nghịch đảo modular của e theo phi(n)
        private long ExtendedEuclidean(long e, long phi)
        {
            long t = 0, newT = 1;
            long r = phi, newR = e;

            while (newR != 0)
            {
                long quotient = r / newR;

                // Cập nhật t và r
                (t, newT) = (newT, t - quotient * newT);
                (r, newR) = (newR, r - quotient * newR);
            }

            // Nếu r > 1 thì e không có nghịch đảo modular
            if (r > 1)
                throw new ArgumentException("e không có nghịch đảo modular");

            // Nếu t < 0 thì cộng thêm phi để có kết quả dương
            if (t < 0)
                t += phi;

            return t; // t chính là d (nghịch đảo modular của e theo phi)
        }

        // Hàm mã hóa văn bản
        private long Encrypt(long message, long e, long n)
        {
            return ModPow(message, e, n); // Mã hóa: message^e mod n
        }

        // Hàm giải mã văn bản
        private long Decrypt(long cipher, long d, long n)
        {
            return ModPow(cipher, d, n); // Giải mã: cipher^d mod n
        }

        // Hàm tính lũy thừa modular
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

        // Nút sinh khóa
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
    }
}
