using System;

namespace testThreadTaskLoadSaveFile
{
    public class Matrix
    {
        public string Name { get; set; }
        private float[,] matrixArr;
        public float[,] MatrixArr
        {
            get { return matrixArr; }
            set { matrixArr = value; }
        }

        public Matrix() { }

        public Matrix(int H, int W, string Name = "")
        {
            matrixArr = new float[H, W];
            this.Name = Name;
        }

        public void GenerateRandomMatrixFloat(float min = -1, float max = 1)
        {
            Random random = new Random();
            for (int h = 0; h < MatrixArr.GetLongLength(0); h++)
            {
                for (int w = 0; w < MatrixArr.GetLongLength(1); w++)
                {
                    MatrixArr[h, w] = (float)(random.NextDouble() * (min * -1 + max) + min);
                }
            }
        }
        public void GenerateRandomMatrixInt(int min = -1, int max = 1)
        {
            Random random = new Random();
            for (int h = 0; h < MatrixArr.GetLongLength(0); h++)
            {
                for (int w = 0; w < MatrixArr.GetLongLength(1); w++)
                {
                    MatrixArr[h, w] = (random.Next(min, max + 1));
                }
            }
        }

        public void ShowMatrix()
        {
            //Console.WriteLine(Name);
            //for (int h = 0; h < MatrixArr.GetLongLength(0); h++)
            //{
            //    for (int w = 0; w < MatrixArr.GetLongLength(1); w++)
            //    {
            //        Console.Write($"[{MatrixArr[h,w],-7:G2}] ");
            //    }
            //    Console.WriteLine();
            //}
            Console.Write(this);
        }

        public float SummAllElements()
        {
            float sum = 0;
            for (int h = 0; h < MatrixArr.GetLongLength(0); h++)
            {
                for (int w = 0; w < MatrixArr.GetLongLength(1); w++)
                {
                    sum += MatrixArr[h, w];
                }
            }
            return sum;
        }

        public static Matrix Mult(Matrix A, Matrix B)
        {
            throw new Exception();
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            return Mult(A, B);
        }

        public override string ToString()
        {
            string res;
            res = Name + "\n";
            for (int h = 0; h < MatrixArr.GetLongLength(0); h++)
            {
                for (int w = 0; w < MatrixArr.GetLongLength(1); w++)
                {
                    res += $"[{MatrixArr[h, w],-7:G2}] ";
                }
                res += "\n";
            }
            return res;

        }

    }

}
