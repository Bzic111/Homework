using System;
using System.Collections;
using System.Collections.Generic;

namespace testingRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = {6,4,3,7,2,1,9,8,5 };
            int left=0, right=0;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                left += arr[i];
                for (int j = i+1; j < arr.Length - 1; j++)
                {
                    right += arr[j];
                }
            }



            //for (int i = arr.Length - 1; i > 0; i--)
            //{
            //    for (int j = 0; j < i; j++)
            //    {
            //        if (arr[j]>arr[j+1])
            //        {
            //            int temp = arr[j];
            //            arr[j] = arr[j + 1];
            //            arr[j + 1] = temp;
            //        }
            //    }
            //}
            foreach (var item in arr)
            {
                Console.WriteLine(item);

            }
        }
    }
}
