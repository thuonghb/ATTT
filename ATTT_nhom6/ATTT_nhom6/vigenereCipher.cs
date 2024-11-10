using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTT_nhom6
{
    internal class vigenereCipher
    {
        public string MaHoa(string vanBan, string khoa)
        {
            string ketQua = string.Empty;
            vanBan = vanBan.ToUpper();
            khoa = khoa.ToUpper();

            for (int i = 0, j = 0; i < vanBan.Length; i++)
            {
                char c = vanBan[i];
                if (c < 'A' || c > 'Z')
                {
                    ketQua += c;
                    continue;
                }
                ketQua += (char)((c + khoa[j] - 2 * 'A') % 26 + 'A');
                j = ++j % khoa.Length;
            }
            return ketQua;
        }

        // Phương thức giải mã
        public string GiaiMa(string vanBan, string khoa)
        {
            string ketQua = string.Empty;
            vanBan = vanBan.ToUpper();
            khoa = khoa.ToUpper();

            for (int i = 0, j = 0; i < vanBan.Length; i++)
            {
                char c = vanBan[i];
                if (c < 'A' || c > 'Z')
                {
                    ketQua += c;
                    continue;
                }
                ketQua += (char)((c - khoa[j] + 26) % 26 + 'A');
                j = ++j % khoa.Length;
            }
            return ketQua;
        }

        // Kiểm tra khóa chỉ chứa ký tự chữ cái
        public bool KiemTraKhoa(string khoa)
        {
            foreach (char c in khoa)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }
    }
}

