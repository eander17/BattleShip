using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eander17Battleship
{
    class Ship
    {
        //length of ship, defined in constructor. 
        int length;
        char shipType; 

        /// <summary>
        /// bowX = col, bowY = row, sternX = col, sternY = row
        /// </summary>
        int bowX, bowY, sternX, sternY; 

        //constructor method. 
        public Ship(int l, char type)
        {
            length = l;
            shipType = type; 
        }

        //sets x and y value for Bow. 
        public void SetBow(int r, int c)
        {
            bowY = r;
            bowX = c; 
        }

        //sets x and y value for stern
        public void SetStern(int r, int c)
        {
            sternY = r;
            sternX = c; 

        }


        /// <summary>
        /// getter methods. returns the specified values. 
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return length; 
        }

        public char GetShipType()
        {
            return shipType;
        }

        public int GetBowX()
        {
            return bowX; //col
        }

        public int GetBowY()
        {
            return bowY; //row
        }

        public int GetSternX()
        {
            return sternX; //col
        }

        public int GetSternY()
        {
            return sternY; //row
        }

    }
}
