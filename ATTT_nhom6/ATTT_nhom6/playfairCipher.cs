using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTT_nhom6
{
    internal class playfairCipher
    {
        private char[,] maTran = new char[5, 5];

        // Tạo ma trận từ khóa
        public void TaoMaTran(string khoa)
        {
            bool[] kyTuDaSuDung = new bool[26];
            khoa = khoa.ToUpper().Replace("J", "I");
            int x = 0, y = 0;

            // Thêm các ký tự từ khóa vào ma trận
            foreach (char c in khoa)
            {
                if (c != ' ' && !kyTuDaSuDung[c - 'A'])
                {
                    maTran[x, y++] = c;
                    kyTuDaSuDung[c - 'A'] = true;
                    if (y == 5) { x++; y = 0; }
                }
            }

            // Thêm các ký tự còn lại từ A đến Z
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!kyTuDaSuDung[c - 'A'] && c != 'J')
                {
                    maTran[x, y++] = c;
                    if (y == 5) { x++; y = 0; }
                }
            }
        }

        // Tìm vị trí của ký tự trong ma trận
        private int[] TimViTri(char ch)
        {
            if (ch == 'J') ch = 'I';
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (maTran[i, j] == ch)
                        return new int[] { i, j };
                }
            }
            return null;
        }

        // Mã hóa một cặp ký tự
        private string MaHoaCapKyTu(char a, char b)
        {
            int[] viTriA = TimViTri(a);
            int[] viTriB = TimViTri(b);

            if (viTriA[0] == viTriB[0])
            {
                return "" + maTran[viTriA[0], (viTriA[1] + 1) % 5] + maTran[viTriB[0], (viTriB[1] + 1) % 5];
            }
            else if (viTriA[1] == viTriB[1])
            {
                return "" + maTran[(viTriA[0] + 1) % 5, viTriA[1]] + maTran[(viTriB[0] + 1) % 5, viTriB[1]];
            }
            else
            {
                return "" + maTran[viTriA[0], viTriB[1]] + maTran[viTriB[0], viTriA[1]];
            }
        }

        // Mã hóa văn bản
        public string MaHoa(string banRo)
        {
            banRo = banRo.ToUpper().Replace("J", "I").Replace(" ", "");
            StringBuilder banMa = new StringBuilder();

            for (int i = 0; i < banRo.Length; i += 2)
            {
                char a = banRo[i];
                char b = (i + 1 < banRo.Length) ? banRo[i + 1] : 'X';
                if (a == b)
                {
                    banMa.Append(MaHoaCapKyTu(a, 'X'));
                    i--;
                }
                else
                {
                    banMa.Append(MaHoaCapKyTu(a, b));
                }
            }
            return banMa.ToString();
        }

        // Giải mã một cặp ký tự
        private string GiaiMaCapKyTu(char a, char b)
        {
            int[] viTriA = TimViTri(a);
            int[] viTriB = TimViTri(b);

            if (viTriA[0] == viTriB[0])
            {
                return "" + maTran[viTriA[0], (viTriA[1] + 4) % 5] + maTran[viTriB[0], (viTriB[1] + 4) % 5];
            }
            else if (viTriA[1] == viTriB[1])
            {
                return "" + maTran[(viTriA[0] + 4) % 5, viTriA[1]] + maTran[(viTriB[0] + 4) % 5, viTriB[1]];
            }
            else
            {
                return "" + maTran[viTriA[0], viTriB[1]] + maTran[viTriB[0], viTriA[1]];
            }
        }

        // Giải mã văn bản
        public string GiaiMa(string banMa)
        {
            banMa = banMa.ToUpper().Replace(" ", "");
            StringBuilder banRo = new StringBuilder();

            for (int i = 0; i < banMa.Length; i += 2)
            {
                banRo.Append(GiaiMaCapKyTu(banMa[i], banMa[i + 1]));
            }
            return banRo.ToString();
        }
    }
}

