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
        private int TinhDinhThuc(int[,] maTran, int n)
        {
            if (n == 1)
                return maTran[0, 0];
            if (n == 2)
                return maTran[0, 0] * maTran[1, 1] - maTran[0, 1] * maTran[1, 0];

            int dinhThuc = 0;
            int dau = 1;

            for (int i = 0; i < n; i++)
            {
                int[,] maTranCon = LayMaTranCon(maTran, 0, i, n);
                dinhThuc += dau * maTran[0, i] * TinhDinhThuc(maTranCon, n - 1);
                dau = -dau;
            }

            return dinhThuc;
        }

        private int[,] LayMaTranCon(int[,] maTran, int hang, int cot, int n)
        {
            int[,] maTranCon = new int[n - 1, n - 1];
            int hangCon = 0, cotCon = 0;

            for (int i = 0; i < n; i++)
            {
                if (i == hang) continue;
                cotCon = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == cot) continue;
                    maTranCon[hangCon, cotCon] = maTran[i, j];
                    cotCon++;
                }
                hangCon++;
            }

            return maTranCon;
        }

        private int ModNghichDao(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1;
        }

        private int[,] MaTranNghichDaoMod26(int[,] maTran, int n)
        {
            int dinhThuc = TinhDinhThuc(maTran, n);
            int dinhThucInv = ModNghichDao(dinhThuc % 26, 26);
            int[,] maTranPhu = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int[,] maTranCon = LayMaTranCon(maTran, i, j, n);
                    int dinhThucCon = TinhDinhThuc(maTranCon, n - 1);
                    maTranPhu[j, i] = (int)Math.Pow(-1, i + j) * dinhThucCon % 26;
                    if (maTranPhu[j, i] < 0) maTranPhu[j, i] += 26;
                }
            }

            int[,] maTranNghichDao = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    maTranNghichDao[i, j] = (maTranPhu[i, j] * dinhThucInv) % 26;
                    if (maTranNghichDao[i, j] < 0) maTranNghichDao[i, j] += 26;
                }
            }

            return maTranNghichDao;
        }

        private void GiaiMa_Click(object sender, EventArgs e)
        {
            
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }


        private void sizekey_Click(object sender, EventArgs e)
        {

        }

        private void mahoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                string[] dongMaTran = richTextBox1.Text.Trim().Split('\n');
                int kichThuocMaTran = dongMaTran.Length;
                int[,] maTranKhoa = new int[kichThuocMaTran, kichThuocMaTran];

                for (int i = 0; i < kichThuocMaTran; i++)
                {
                    string[] dong = dongMaTran[i].Trim().Split(' ');
                    for (int j = 0; j < kichThuocMaTran; j++)
                    {
                        maTranKhoa[i, j] = int.Parse(dong[j]);
                    }
                }

                int dinhThuc = TinhDinhThuc(maTranKhoa, kichThuocMaTran);
                if (dinhThuc == 0)
                {
                    MessageBox.Show("Ma trận khóa không hợp lệ! Định thức bằng 0.");
                    return;
                }

                string vanBanGoc = textBox1.Text.ToUpper();
                vanBanGoc = vanBanGoc.Replace(" ", "");

                while (vanBanGoc.Length % kichThuocMaTran != 0)
                {
                    vanBanGoc += "X";
                }

                string vanBanMaHoa = "";
                for (int k = 0; k < vanBanGoc.Length; k += kichThuocMaTran)
                {
                    int[] vectorVanBanGoc = new int[kichThuocMaTran];
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vectorVanBanGoc[i] = vanBanGoc[k + i] - 'A';
                    }

                    int[] vectorMaHoa = new int[kichThuocMaTran];
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vectorMaHoa[i] = 0;
                        for (int j = 0; j < kichThuocMaTran; j++)
                        {
                            vectorMaHoa[i] += maTranKhoa[i, j] * vectorVanBanGoc[j];
                        }
                        vectorMaHoa[i] = vectorMaHoa[i] % 26;
                    }

                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vanBanMaHoa += (char)(vectorMaHoa[i] + 'A');
                    }
                }

                textBox2.Text = vanBanMaHoa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void giaima_Click_1(object sender, EventArgs e)
        {

        }
    }
}