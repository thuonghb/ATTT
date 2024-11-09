internal static class HillHelpers
{
    // Hàm đệ quy tính định thức của ma trận n x n
    static int Determinant(int[,] matrix, int n)
    {
        if (n == 1) // Ma trận 1x1
        {
            return matrix[0, 0];
        }
        if (n == 2) // Ma trận 2x2
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        int det = 0;
        int sign = 1; // Biến dấu (-1)^(i+j)

        // Tạo ma trận con cho từng phần tử của dòng đầu tiên
        for (int i = 0; i < n; i++)
        {
            int[,] subMatrix = GetSubMatrix(matrix, 0, i, n);
            det += sign * matrix[0, i] * Determinant(subMatrix, n - 1);
            sign = -sign; // Đổi dấu cho các phần tử kế tiếp
        }

        return det;
    }

    // Hàm tạo ma trận con bằng cách bỏ dòng row và cột col
    static int[,] GetSubMatrix(int[,] matrix, int row, int col, int n)
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
}