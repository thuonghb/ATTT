using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTT_nhom6
{
    internal class AffineCipher
    {
        // Hàm tìm nghịch đảo modular
        public static int TimNghichDao(int a, int n)
        {
            int r1 = n, r2 = a;
            int t1 = 0, t2 = 1;
            while (r2 > 0)
            {
                int q = r1 / r2;

                int r = r1 - q * r2;
                r1 = r2;
                r2 = r;

                int t = t1 - q * t2;
                t1 = t2;
                t2 = t;
            }

            // Nếu r1 == 1, t1 là nghịch đảo
            if (r1 == 1)
                return (t1 + n) % n; // Đảm bảo kết quả là số dương
            else
                return -1; // Không có nghịch đảo
        }

        // Mã hóa ký tự
        public static char MaHoaKyTu(char kyTu, int a, int b)
        {
            if (char.IsLetter(kyTu))
            {
                kyTu = char.ToUpper(kyTu);
                int x = kyTu - 'A';
                int y = (a * x + b) % 26;
                return (char)(y + 'A');
            }
            return kyTu;
        }

        // Giải mã ký tự
        public static char GiaiMaKyTu(char kyTu, int a, int b)
        {
            if (char.IsLetter(kyTu))
            {
                kyTu = char.ToUpper(kyTu);
                int y = kyTu - 'A';
                int aNghichDao = TimNghichDao(a, 26);
                if (aNghichDao == -1)
                    throw new ArgumentException($"Không có nghịch đảo modular cho a = {a}");

                int x = (aNghichDao * (y - b + 26)) % 26;
                return (char)(x + 'A');
            }
            return kyTu;
        }

        // Mã hóa chuỗi
        public string MaHoaChuoi(string vanBan, int a, int b)
        {
            string ketQua = "";
            foreach (char kyTu in vanBan)
                ketQua += MaHoaKyTu(kyTu, a, b);
            return ketQua;
        }

        // Giải mã chuỗi
        public string GiaiMaChuoi(string vanBan, int a, int b)
        {
            string ketQua = "";
            foreach (char kyTu in vanBan)
                ketQua += GiaiMaKyTu(kyTu, a, b);
            return ketQua;
        }
    }
}
