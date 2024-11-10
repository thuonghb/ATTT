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
        // Hàm sinh khóa RSA (e, d, n)
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

            if (r > 1)
                throw new ArgumentException("e không có nghịch đảo modular");

            if (t < 0)
                t += phi;

            return t;
        }

        // Hàm mã hóa chuỗi ký tự
        private string EncryptString(string plaintext, long e, long n)
        {
            StringBuilder encryptedText = new StringBuilder();
            foreach (char ch in plaintext.ToUpper())
            {
                if (char.IsLetter(ch))
                {
                    long asciiValue = ch - 'A';            // Chuyển ký tự thành giá trị từ 0 đến 25 (A = 0, B = 1, ...)
                    long encryptedAscii = Encrypt(asciiValue, e, n);  // Mã hóa giá trị này bằng RSA
                    encryptedText.Append((char)((encryptedAscii % 26) + 'A'));  // Chuyển số đã mã hóa về ký tự A-Z
                }
                else
                {
                    encryptedText.Append(ch);  // Giữ nguyên ký tự không phải là chữ cái
                }
            }
            return encryptedText.ToString();  // Trả về chuỗi ký tự đã mã hóa
        }

        // Hàm giải mã chuỗi ký tự
        private string DecryptString(string ciphertext, long d, long n)
        {
            StringBuilder decryptedText = new StringBuilder();
            foreach (char ch in ciphertext.ToUpper())
            {
                if (char.IsLetter(ch))
                {
                    long encryptedAscii = ch - 'A';        // Chuyển ký tự thành giá trị từ 0 đến 25
                    long decryptedAscii = Decrypt(encryptedAscii, d, n);  // Giải mã
                    decryptedText.Append((char)((decryptedAscii % 26) + 'A'));  // Chuyển số đã giải mã thành ký tự A-Z
                }
                else
                {
                    decryptedText.Append(ch);  // Giữ nguyên ký tự không phải là chữ cái
                }
            }
            return decryptedText.ToString();  // Trả về chuỗi ký tự đã giải mã
        }

        // Hàm mã hóa một giá trị số
        private long Encrypt(long message, long e, long n)
        {
            return ModPow(message, e, n);
        }

        // Hàm giải mã một giá trị số
        private long Decrypt(long cipher, long d, long n)
        {
            return ModPow(cipher, d, n);
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
                int p = int.Parse(txtP.Text);
                int q = int.Parse(txtQ.Text);
                int eValue = int.Parse(txtE.Text);

                GenerateKeys(p, q, eValue);

                txtPublicKey.Text = $"(e, n): ({publicKeyE}, {publicKeyN})";
                txtPrivateKey.Text = $"(d, n): ({privateKeyD}, {publicKeyN})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPlaintext.Text))
                {
                    // Mã hóa văn bản
                    string plaintext = txtPlaintext.Text;
                    string ciphertext = EncryptString(plaintext, publicKeyE, publicKeyN);
                    txtCiphertext.Text = ciphertext;
                }
                else if (!string.IsNullOrEmpty(txtCiphertext.Text))
                {
                    // Giải mã văn bản
                    string ciphertext = txtCiphertext.Text;
                    string plaintext = DecryptString(ciphertext, privateKeyD, publicKeyN);
                    txtPlaintext.Text = plaintext;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
