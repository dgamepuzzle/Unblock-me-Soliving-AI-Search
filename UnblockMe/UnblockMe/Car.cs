using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe
{
    class Car
    {
        public static int count = 0;
        public int width;
        public int id;
        public bool alignment;
        public int[] position;

        public Car(int[] position,bool alignment = false,int width = 2)
        {
            this.id = count;
            count += 1;

            this.position = position;
            this.alignment = alignment;
            this.width = width;
        }

        public void move(bool direction,int lenght)
        {
            if (direction)
            {
                if (alignment)
                {
                    position[1] += lenght;
                }
                else
                {
                    position[0] += lenght;
                }
            }
            else
            {
                if (alignment)
                {
                    position[1] -= lenght;
                }
                else
                {
                    position[0] -= lenght;
                }
            }
        }
    }
}
