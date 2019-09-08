using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe
{
    class Car
    {

        public int width;
        public int id;
        public bool alignment;
        public int[] position;

        public Car(int id,int[] position,bool alignment = false,int width = 2)
        {
            this.id = id;
            this.position = position;
            this.alignment = alignment;
            this.width = width;
        }

        public void move(int lenght)
        {
            if (lenght < 0)
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
            else if (lenght > 0)
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
        }
    }
}
