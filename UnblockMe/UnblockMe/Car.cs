using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe {
    class Car {

        public int width;
        public int id;
        public bool alignment;
        public int[] position = new int[2];

        public Car (int id, int[] position, bool alignment = false, int width = 2) {
            this.id = id;
            this.position = position;
            if (id % 2 == 0) {
                this.alignment = false;
            } else {
                this.alignment = true;
            }
            this.width = width;
        }

        public void move (int lenght) {
            // (-1)
            if (lenght < 0) {
                //if horizontal car
                if (alignment) {
                    // y + (-1) = go Left
                    position[1] += lenght;
                    // Console.Write (" Go Left, \n");
                } else {
                    // x + (-1) = go Up
                    position[0] += lenght;
                    // Console.Write (" Go Up, \n");
                }
            }
            // right and bottom (length == 1)
            else if (lenght > 0) {
                if (alignment) {
                    position[1] += lenght;
                    // Console.Write (" Go Right, \n");
                } else {
                    position[0] += lenght;
                    // Console.Write (" Go Down, \n");
                }
            }
        }
    }
}