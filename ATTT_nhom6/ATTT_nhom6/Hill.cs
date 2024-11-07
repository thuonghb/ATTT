using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATTT_nhom6
{
    public partial class Hill : Form
    {

        public Hill()
        {
            InitializeComponent();
        }

        private void mahoa_Click(object sender, EventArgs e)
        {
            // Lấy bản rõ từ RichTextBox
            string plainText = richTextBox1.Text;
            int matrixSize;

            // Kiểm tra kích thước ma trận
            if (!int.TryParse(textBox4.Text, out matrixSize) || matrixSize <= 0)
            {
                MessageBox.Show("Vui lòng nhập kích thước ma trận hợp lệ.");
                return;
            }

            // Tạo khóa ma trận
            int[,] keyMatrix = new int[matrixSize, matrixSize];

            // Nhập các giá trị cho ma trận khóa sử dụng đệ quy
            if (!FillMatrixRecursively(keyMatrix, matrixSize, 0, 0))
            {
                MessageBox.Show("Vui lòng nhập tất cả các giá trị hợp lệ cho ma trận khóa.");
                return;
            }

            // Kiểm tra xem bản rõ có trống không
            if (string.IsNullOrWhiteSpace(plainText))
            {
                MessageBox.Show("Xin vui lòng nhập bản rõ.");
                return;
            }

            // Mã hóa với khóa (Sử dụng mã hóa Hill)
            string cipherText = HillCipherEncrypt(plainText, keyMatrix);
            richTextBox3.Text = cipherText;
        }
        private void giaima_Click(object sender, EventArgs e)
            {
                // Lấy bản mã từ RichTextBox
                string cipherText = richTextBox3.Text;
                int matrixSize;

                // Kiểm tra kích thước ma trận
                if (!int.TryParse(textBox4.Text, out matrixSize) || matrixSize <= 0)
                {
                    MessageBox.Show("Vui lòng nhập kích thước ma trận hợp lệ.");
                    return;
                }

                // Tạo ma trận khóa
                int[,] keyMatrix = new int[matrixSize, matrixSize];

                // Nhập các giá trị cho ma trận khóa sử dụng đệ quy
                if (!FillMatrixRecursively(keyMatrix, matrixSize, 0, 0))
                {
                    MessageBox.Show("Vui lòng nhập tất cả các giá trị hợp lệ cho ma trận khóa.");
                    return;
                }

                // Kiểm tra xem bản mã có trống không
                if (string.IsNullOrWhiteSpace(cipherText))
                {
                    MessageBox.Show("Vui lòng nhập bản mã.");
                    return;
                }

                // Giải mã với khóa (Sử dụng giải mã Hill)
                string decryptedText = HillCipherDecrypt(cipherText, keyMatrix);
                richTextBox1.Text = decryptedText;
            }
    

        private string HillCipherEncrypt(string plainText, int[,] keyMatrix)
        {
            // Chuyển bản rõ thành các giá trị số (a=0, b=1, ..., z=25)
            int[] plainTextValues = new int[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextValues[i] = plainText[i] - 'a';
            }

            // Chia bản rõ thành các vector 2x1
            int[,] vector = new int[2, 1];
            string cipherText = "";

            for (int i = 0; i < plainText.Length; i += 2)
            {
                vector[0, 0] = plainTextValues[i];
                if (i + 1 < plainText.Length)
                {
                    vector[1, 0] = plainTextValues[i + 1];
                }
                else
                {
                    vector[1, 0] = 0; // Pad với 'a' nếu không đủ cặp
                }

                // Mã hóa
                int[,] cipherVector = MultiplyMatrix(keyMatrix, vector);

                // Chuyển đổi lại các giá trị thành chuỗi ký tự
                cipherText += (char)(cipherVector[0, 0] % 26 + 'a');
                cipherText += (char)(cipherVector[1, 0] % 26 + 'a');
            }

            return cipherText;
        }

        // Hàm giải mã Hill
        private string HillCipherDecrypt(string cipherText, int[,] keyMatrix)
        {
            // Tính nghịch đảo của ma trận khóa
            int[,] inverseKeyMatrix = GetMatrixInverse(keyMatrix);

            // Chuyển bản mã thành giá trị số (a=0, b=1, ..., z=25)
            int[] cipherTextValues = new int[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                cipherTextValues[i] = cipherText[i] - 'a';
            }

            // Chia bản mã thành các vector 2x1
            int[,] vector = new int[2, 1];
            string decryptedText = "";

            for (int i = 0; i < cipherText.Length; i += 2)
            {
                vector[0, 0] = cipherTextValues[i];
                vector[1, 0] = cipherTextValues[i + 1];

                // Giải mã
                int[,] decryptedVector = MultiplyMatrix(inverseKeyMatrix, vector);

                // Chuyển đổi lại các giá trị thành ký tự
                decryptedText += (char)(decryptedVector[0, 0] % 26 + 'a');
                decryptedText += (char)(decryptedVector[1, 0] % 26 + 'a');
            }

            return decryptedText;
        }

        // Hàm nhân ma trận với vector
        private int[,] MultiplyMatrix(int[,] matrix, int[,] vector)
        {
            int[,] result = new int[2, 1];
            for (int i = 0; i < 2; i++)
            {
                result[i, 0] = 0;
                for (int j = 0; j < 2; j++)
                {
                    result[i, 0] += matrix[i, j] * vector[j, 0];
                }
                result[i, 0] %= 26; // Phép toán modulo 26
            }
            return result;
        }

        // Hàm lấy ma trận nghịch đảo (modulo 26)
        private int[,] GetMatrixInverse(int[,] matrix)
        {
            int determinant = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            int modInverse = ModInverse(determinant, 26);
            int[,] inverseMatrix = new int[2, 2];
            inverseMatrix[0, 0] = matrix[1, 1] * modInverse % 26;
            inverseMatrix[0, 1] = -matrix[0, 1] * modInverse % 26;
            inverseMatrix[1, 0] = -matrix[1, 0] * modInverse % 26;
            inverseMatrix[1, 1] = matrix[0, 0] * modInverse % 26;

            // Đảm bảo rằng các giá trị âm trở thành giá trị dương trong modulo 26
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (inverseMatrix[i, j] < 0) inverseMatrix[i, j] += 26;
                }
            }
            return inverseMatrix;
        }

        // Hàm tính nghịch đảo số trong modulo 26
        private int ModInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1) return x;
            }
            return -1; // Không tìm thấy nghịch đảo
        }

        // Hàm nhập giá trị cho ma trận sử dụng đệ quy
        private bool FillMatrixRecursively(int[,] matrix, int size, int row, int col)
        {
            if (row == size) return true; // Đã đi hết các hàng
            if (col == size)
            {
                // Chuyển sang hàng tiếp theo khi đã đi hết cột
                return FillMatrixRecursively(matrix, size, row + 1, 0);
            }

            // Kiểm tra và lấy giá trị từ TextBox của bảng điều khiển
            string controlName = "textBoxMatrix" + row + col;
            TextBox textBox = panel1.Controls[controlName] as TextBox; // Cố gắng lấy TextBox
            if (textBox != null && int.TryParse(textBox.Text, out int value))
            {
                matrix[row, col] = value; // Lưu giá trị vào ma trận
                return FillMatrixRecursively(matrix, size, row, col + 1); // Tiến đến cột tiếp theo
            }

            return false; // Nếu không có TextBox hoặc giá trị không hợp lệ
        }

    }
}
