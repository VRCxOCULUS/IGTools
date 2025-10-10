using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Align
    {



        public static uint To16(uint number)
        {
            var remainder = number % 16;
            if (remainder == 0)
            {
                return number;
            }
            else
            {
                return number + remainder;
            }
        }

        public static int To16(int number)
        {
            var remainder = number % 16;
            if (remainder == 0)
            {
                return number;
            }
            else
            {
                return number + remainder;
            }
        }



        public static uint To8(uint number)
        {
            var remainder = number % 8;
            if (remainder == 0)
            {
                return 0;
            }
            else
            {
                return number + remainder;
            }
        }

        public static int To8(int number)
        {
            var remainder = number % 8;
            if (remainder == 0)
            {
                return 0;
            }
            else
            {
                return number + remainder;
            }
        }



        public static uint To4(uint number)
        {
            var remainder = number % 4;
            if (remainder == 0)
            {
                return 0;
            }
            else
            {
                return number + remainder;
            }
        }

        public static int To4(int number)
        {
            var remainder = number % 4;
            if (remainder == 0)
            {
                return 0;
            }
            else
            {
                return number + remainder;
            }
        }
    }
}
