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
        // Hàm đệ quy tính định thức của ma trận n x n
        private int Determinant(int[,] matrix, int n)
        {
            if (n == 1)
                return matrix[0, 0];
            if (n == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            int det = 0;
            int sign = 1;

            for (int i = 0; i < n; i++)
            {
                int[,] subMatrix = GetSubMatrix(matrix, 0, i, n);
                det += sign * matrix[0, i] * Determinant(subMatrix, n - 1);
                sign = -sign;
            }

            return det;
        }

        // Hàm tạo ma trận con
        private int[,] GetSubMatrix(int[,] matrix, int row, int col, int n)
        {
            int[,] subMatrix = new int[n - 1, n - 1];
            int subRow = 0, subCol = 0;

            for (int i = 0; i < n; i++)
            {
                if (i == row) continue;
                subCol = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == col) continue;
                    subMatrix[subRow, subCol] = matrix[i, j];
                    subCol++;
                }
                subRow++;
            }

            return subMatrix;
        }

        // Hàm tính modulo nghịch đảo (sử dụng Euclid mở rộng)
        private int ModInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1; // Nếu không tìm thấy
        }

        // Hàm tính ma trận nghịch đảo mod 26
        private int[,] InverseMatrixMod26(int[,] matrix, int n)
        {
            int det = Determinant(matrix, n);
            int detInv = ModInverse(det % 26, 26);
            int[,] adjMatrix = new int[n, n]; // Ma trận phụ đại số (adjugate matrix)

            // Tính ma trận phụ đại số (adjugate matrix)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int[,] subMatrix = GetSubMatrix(matrix, i, j, n);
                    int subDet = Determinant(subMatrix, n - 1);
                    adjMatrix[j, i] = (int)Math.Pow(-1, i + j) * subDet % 26;
                    if (adjMatrix[j, i] < 0) adjMatrix[j, i] += 26; // Đảm bảo mod 26
                }
            }

            // Nhân ma trận phụ đại số với nghịch đảo định thức
            int[,] invMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    invMatrix[i, j] = (adjMatrix[i, j] * detInv) % 26;
                    if (invMatrix[i, j] < 0) invMatrix[i, j] += 26; // Đảm bảo mod 26
                }
            }

            return invMatrix;
        }


        private void mahoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ma trận khóa từ RichTextBox và chuyển thành ma trận nxn
                string[] matrixRows = richTextBox1.Text.Trim().Split('\n');
                int matrixSize = matrixRows.Length;
                int[,] keyMatrix = new int[matrixSize, matrixSize];

                for (int i = 0; i < matrixSize; i++)
                {
                    string[] row = matrixRows[i].Trim().Split(' ');
                    for (int j = 0; j < matrixSize; j++)
                    {
                        keyMatrix[i, j] = int.Parse(row[j]);
                    }
                }

                // Kiểm tra định thức của ma trận khóa
                int det = Determinant(keyMatrix, matrixSize);
                if (det == 0)
                {
                    MessageBox.Show("Ma trận khóa không hợp lệ! Định thức bằng 0.");
                    return;
                }

                // Lấy văn bản từ TextBox và chuyển đổi ký tự thành số
                string plainText = textBox1.Text.ToUpper();
                plainText = plainText.Replace(" ", ""); // Xóa khoảng trắng

                // Chèn padding nếu văn bản không đủ
                while (plainText.Length % matrixSize != 0)
                {
                    plainText += "X"; // Padding với 'X'
                }

                // Mã hóa từng khối văn bản
                string cipherText = "";
                for (int k = 0; k < plainText.Length; k += matrixSize)
                {
                    int[] plainVector = new int[matrixSize];
                    for (int i = 0; i < matrixSize; i++)
                    {
                        plainVector[i] = plainText[k + i] - 'A';
                    }

                    // Tính toán mã hóa y = (K * x) mod 26
                    int[] cipherVector = new int[matrixSize];
                    for (int i = 0; i < matrixSize; i++)
                    {
                        cipherVector[i] = 0;
                        for (int j = 0; j < matrixSize; j++)
                        {
                            cipherVector[i] += keyMatrix[i, j] * plainVector[j];
                        }
                        cipherVector[i] = cipherVector[i] % 26;
                    }

                    // Chuyển mã hóa thành ký tự
                    for (int i = 0; i < matrixSize; i++)
                    {
                        cipherText += (char)(cipherVector[i] + 'A');
                    }
                }

                // Hiển thị kết quả mã hóa
                textBox2.Text = cipherText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void giaima_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ma trận khóa từ RichTextBox và chuyển thành ma trận nxn
                string[] matrixRows = richTextBox1.Text.Trim().Split('\n');
                int matrixSize = matrixRows.Length;
                int[,] keyMatrix = new int[matrixSize, matrixSize];

                for (int i = 0; i < matrixSize; i++)
                {
                    string[] row = matrixRows[i].Trim().Split(' ');
                    for (int j = 0; j < matrixSize; j++)
                    {
                        keyMatrix[i, j] = int.Parse(row[j]);
                    }
                }

                // Tính ma trận nghịch đảo mod 26
                int[,] inverseKeyMatrix = InverseMatrixMod26(keyMatrix, matrixSize);

                // Lấy văn bản mã hóa từ TextBox
                string cipherText = textBox1.Text.ToUpper();

                // Giải mã từng khối văn bản
                string decryptedText = "";
                for (int k = 0; k < cipherText.Length; k += matrixSize)
                {
                    int[] cipherVector = new int[matrixSize];
                    for (int i = 0; i < matrixSize; i++)
                    {
                        cipherVector[i] = cipherText[k + i] - 'A';
                    }

                    // Tính toán giải mã x = (K^-1 * y) mod 26
                    int[] plainVector = new int[matrixSize];
                    for (int i = 0; i < matrixSize; i++)
                    {
                        plainVector[i] = 0;
                        for (int j = 0; j < matrixSize; j++)
                        {
                            plainVector[i] += inverseKeyMatrix[i, j] * cipherVector[j];
                        }
                        plainVector[i] = plainVector[i] % 26;
                    }

                    // Chuyển mã hóa thành ký tự
                    for (int i = 0; i < matrixSize; i++)
                    {
                        decryptedText += (char)(plainVector[i] + 'A');
                    }
                }

                // Hiển thị kết quả giải mã
                textBox2.Text = decryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();

        }
    }
}