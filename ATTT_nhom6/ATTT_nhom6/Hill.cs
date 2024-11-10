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
            // Nếu ma trận chỉ có 1 phần tử, định thức là chính phần tử đó
            if (n == 1)
                return maTran[0, 0];

            // Nếu ma trận 2x2, sử dụng công thức đơn giản
            if (n == 2)
                return maTran[0, 0] * maTran[1, 1] - maTran[0, 1] * maTran[1, 0];

            int dinhThuc = 0;
            int dau = 1; // Biến để thay đổi dấu trong tính định thức

            // Vòng lặp để tính định thức theo phương pháp khai triển Laplace
            for (int i = 0; i < n; i++)
            {
                // Lấy ma trận con bằng cách bỏ hàng 0 và cột i
                int[,] maTranCon = LayMaTranCon(maTran, 0, i, n);

                // Tính định thức của ma trận con và nhân với phần tử và dấu tương ứng
                dinhThuc += dau * maTran[0, i] * TinhDinhThuc(maTranCon, n - 1);

                // Thay đổi dấu cho lần tính tiếp theo
                dau = -dau;
            }

            return dinhThuc;
        }

        // Hàm lấy ma trận con bằng cách loại bỏ hàng và cột chỉ định
        private int[,] LayMaTranCon(int[,] maTran, int hang, int cot, int n)
        {
            int[,] maTranCon = new int[n - 1, n - 1];
            int hangCon = 0, cotCon = 0;

            // Lấy các phần tử không thuộc hàng và cột bị loại bỏ
            for (int i = 0; i < n; i++)
            {
                if (i == hang) continue; // Bỏ qua hàng chỉ định
                cotCon = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == cot) continue; // Bỏ qua cột chỉ định
                    maTranCon[hangCon, cotCon] = maTran[i, j];
                    cotCon++;
                }
                hangCon++;
            }

            return maTranCon;
        }

        // Hàm tìm số nghịch đảo của a theo modulo m
        private int ModNghichDao(int a, int m)
        {
            a = a % m;
            // Tìm giá trị x sao cho (a * x) % m == 1
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1; // Nếu không có nghịch đảo
        }

        // Hàm tính ma trận nghịch đảo theo mod 26
        private int[,] MaTranNghichDaoMod26(int[,] maTran, int n)
        {
            // Tính định thức của ma trận
            int dinhThuc = TinhDinhThuc(maTran, n);

            // Tính nghịch đảo của định thức theo mod 26
            int dinhThucInv = ModNghichDao(dinhThuc % 26, 26);

            int[,] maTranPhu = new int[n, n];

            // Tính ma trận phụ đại số của ma trận ban đầu
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int[,] maTranCon = LayMaTranCon(maTran, i, j, n); // Lấy ma trận con
                    int dinhThucCon = TinhDinhThuc(maTranCon, n - 1); // Tính định thức của ma trận con
                    maTranPhu[j, i] = (int)Math.Pow(-1, i + j) * dinhThucCon % 26; // Tạo ma trận phụ
                    if (maTranPhu[j, i] < 0) maTranPhu[j, i] += 26; // Đảm bảo giá trị dương
                }
            }

            // Tính ma trận nghịch đảo bằng cách nhân ma trận phụ với nghịch đảo định thức
            int[,] maTranNghichDao = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    maTranNghichDao[i, j] = (maTranPhu[i, j] * dinhThucInv) % 26;
                    if (maTranNghichDao[i, j] < 0) maTranNghichDao[i, j] += 26; // Đảm bảo giá trị dương
                }
            }

            return maTranNghichDao;
        }

        // Sự kiện khi nhấn nút "Thoát"
        private void Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void sizekey_Click(object sender, EventArgs e)
        {
            txtvanban.Enabled = true;
            txtkq.Enabled = true;
            khoa.Enabled = true;
        }

        private void mahoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Lấy ma trận khóa từ giao diện người dùng
                string[] dongMaTran = khoa.Text.Trim().Split('\n');
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

                // Kiểm tra tính hợp lệ của ma trận khóa (định thức không được bằng 0)
                int dinhThuc = TinhDinhThuc(maTranKhoa, kichThuocMaTran);
                if (dinhThuc == 0)
                {
                    MessageBox.Show("Ma trận khóa không hợp lệ! Định thức bằng 0.");
                    return;
                }

                // Xử lý văn bản gốc
                string vanBanGoc = txtvanban.Text.ToUpper();
                vanBanGoc = vanBanGoc.Replace(" ", ""); // Loại bỏ khoảng trắng

                // Thêm ký tự 'X' để độ dài văn bản chia hết cho kích thước ma trận
                while (vanBanGoc.Length % kichThuocMaTran != 0)
                {
                    vanBanGoc += "X";
                }

                string vanBanMaHoa = "";
                // Mã hóa từng khối văn bản theo kích thước của ma trận
                for (int k = 0; k < vanBanGoc.Length; k += kichThuocMaTran)
                {
                    int[] vectorVanBanGoc = new int[kichThuocMaTran];
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vectorVanBanGoc[i] = vanBanGoc[k + i] - 'A'; // Chuyển ký tự thành số từ 0 đến 25
                    }

                    int[] vectorMaHoa = new int[kichThuocMaTran];
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vectorMaHoa[i] = 0;
                        for (int j = 0; j < kichThuocMaTran; j++)
                        {
                            vectorMaHoa[i] += maTranKhoa[i, j] * vectorVanBanGoc[j];
                        }
                        vectorMaHoa[i] = vectorMaHoa[i] % 26; // Áp dụng modulo 26
                    }

                    // Chuyển số về ký tự và tạo chuỗi kết quả
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vanBanMaHoa += (char)(vectorMaHoa[i] + 'A');
                    }
                }

                // Hiển thị kết quả mã hóa
                txtkq.Text = vanBanMaHoa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void giaima_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Lấy ma trận khóa từ giao diện người dùng
                string[] dongMaTran = khoa.Text.Trim().Split('\n');
                int kichThuocMaTran = dongMaTran.Length;
                int[,] maTranKhoa = new int[kichThuocMaTran, kichThuocMaTran];

                // Chuyển ma trận khóa từ chuỗi nhập vào thành mảng số nguyên
                for (int i = 0; i < kichThuocMaTran; i++)
                {
                    string[] dong = dongMaTran[i].Trim().Split(' ');
                    for (int j = 0; j < kichThuocMaTran; j++)
                    {
                        maTranKhoa[i, j] = int.Parse(dong[j]);
                    }
                }

                // Tính ma trận nghịch đảo theo modulo 26
                int[,] maTranNghichDaoKhoa = MaTranNghichDaoMod26(maTranKhoa, kichThuocMaTran);

                // Lấy văn bản đã mã hóa từ giao diện
                string vanBanMaHoa = txtvanban.Text.ToUpper();

                string vanBanGoc = "";
                // Giải mã từng khối văn bản
                for (int k = 0; k < vanBanMaHoa.Length; k += kichThuocMaTran)
                {
                    int[] vectorMaHoa = new int[kichThuocMaTran];
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vectorMaHoa[i] = vanBanMaHoa[k + i] - 'A'; // Chuyển ký tự thành số từ 0 đến 25
                    }

                    int[] vectorVanBanGoc = new int[kichThuocMaTran];
                    // Nhân ma trận nghịch đảo với vector mã hóa
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vectorVanBanGoc[i] = 0;
                        for (int j = 0; j < kichThuocMaTran; j++)
                        {
                            vectorVanBanGoc[i] += maTranNghichDaoKhoa[i, j] * vectorMaHoa[j];
                        }
                        vectorVanBanGoc[i] = vectorVanBanGoc[i] % 26; // Áp dụng modulo 26
                    }

                    // Chuyển số về ký tự và tạo chuỗi kết quả
                    for (int i = 0; i < kichThuocMaTran; i++)
                    {
                        vanBanGoc += (char)(vectorVanBanGoc[i] + 'A');
                    }
                }

                // Hiển thị kết quả giải mã
                txtkq.Text = vanBanGoc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}