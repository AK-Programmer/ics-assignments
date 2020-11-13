//Author: Adar Kahiri
//File Name: Obstacle.cs
//Project Name: mP9
//Creation Date: Nov. 13, 2020
//Modified Date: Nov. 13, 2020
//Description: This class stores the data of each obstacle–namely its position and whether it's visible on the board.

using System;
namespace mP9
{
    public class Obstacle
    {

        private int pos;
        private bool isVisible; 

        //Pre: the position must be inside the board. This is ensured elsewhere.
        //Post: none
        //Description: this is a basic constructor that sets the position of the obstacle.
        public Obstacle(int pos)
        {
            this.pos = pos;
            isVisible = false;
        }

        //Pre: none
        //Post: returns the isVisible atttribute.
        //Description: this method is used to determine whether an obstacle is visible or not
        public bool GetIsVisible()
        {
            return isVisible;
        }

        //Pre: none
        //Post: none
        //Description: this method toggles the isVisible attribute
        public void ToggleVisibility()
        {
            isVisible = !isVisible;
        }

        //Pre: none
        //Post: returns the position of the obstacle
        //Description: get method for the pos attribute. 
        public int GetPos()
        {
            return pos;
        }
    }
}
