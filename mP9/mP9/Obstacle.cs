using System;
namespace mP9
{
    public class Obstacle
    {

        private int pos;
        private bool isVisible; 


        public Obstacle(int pos)
        {
            this.pos = pos;
            isVisible = false;
        }


        public bool GetIsVisible()
        {
            return isVisible;
        }

        public void ToggleVisibility()
        {
            isVisible = !isVisible;
        }

        public int GetPos()
        {
            return pos;
        }
    }
}
