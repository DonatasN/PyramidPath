using System;
using System.IO;
using System.Linq;

namespace PyramidPath
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileNmae = @"C:\Users\Donatas\Desktop\pyramid\pyramid.txt";
            int fileLineCOunt = File.ReadLines(fileNmae).Count();

            int[,] pyramid = new int[fileLineCOunt, fileLineCOunt];
            int sum = 0;

            int rowCounter = 0;
            int columnCounter = 0;

            var lines = File.ReadAllLines(fileNmae);

            foreach (var line in lines)
            {
                string[] tokens = line.Split(' ');
                columnCounter = 0;

                foreach(var token in tokens)
                {
                    pyramid[rowCounter, columnCounter] = Convert.ToInt32(token);
                    columnCounter++;
                }
                rowCounter++;
            }

            int leftChild = 0;
            int leftChildIndex = 0;
            int rightChild = 0;
            int rightChildIndex = 0;

            for (int i = 0; i < fileLineCOunt - 1; i++)
            {
                for (int j = 0; j < fileLineCOunt - 1; j++)
                {
                    leftChild = pyramid[i+1, j];
                    rightChild = pyramid[i+1, j+1];

                    if ((checkIfOddNumber(pyramid[i, j]) == checkIfOddNumber(leftChild) && checkIfOddNumber(pyramid[i, j]) == checkIfOddNumber(rightChild)) || (leftChild == 0 && rightChild == 0))
                    {
                        pyramid[i, j] = 0;
                    }
                }
            }

            bool parentIsOdd = checkIfOddNumber(pyramid[0, 0]);
            int lastParentIndex = 0;
            int currentSelectedNumber = 0;

            int[] paths = new int[fileLineCOunt];
            paths[0] = pyramid[0, 0];

            sum = pyramid[0, 0];

            int row = 1;

            while (row < fileLineCOunt)
            {
                parentIsOdd = checkIfOddNumber(pyramid[row - 1, lastParentIndex]);
                leftChildIndex = lastParentIndex;
                rightChildIndex = lastParentIndex + 1;

                leftChild = pyramid[row, leftChildIndex];
                rightChild = pyramid[row, rightChildIndex];

                if (parentIsOdd != checkIfOddNumber(leftChild) && parentIsOdd != checkIfOddNumber(rightChild))
                {
                    if (leftChild > rightChild)
                    {
                        currentSelectedNumber = leftChild;
                        lastParentIndex = leftChildIndex;
                    }
                    else
                    {
                        currentSelectedNumber = rightChild;
                        lastParentIndex = rightChildIndex;
                    }
                }
                else
                {
                    if (parentIsOdd != checkIfOddNumber(leftChild))
                    {
                        currentSelectedNumber = leftChild;
                        lastParentIndex = leftChildIndex;
                    }

                    if (parentIsOdd != checkIfOddNumber(rightChild))
                    {
                        currentSelectedNumber = rightChild;
                        lastParentIndex = rightChildIndex;
                    }
                }
                paths[row] = currentSelectedNumber;
                sum = sum + currentSelectedNumber;
                row++;
            }


            Console.WriteLine("Path numbers:");
            //string pathString = "";

            string pathString = string.Join(" -> ", paths);

            Console.WriteLine("{0}", pathString);

            Console.WriteLine("Total path sum: {0}", sum);
            Console.ReadKey();
        }

        static bool checkIfOddNumber(int number)
        {
            if (number%2 == 0)
            {
                return false;
            }

            return true;
        }
    }
}
