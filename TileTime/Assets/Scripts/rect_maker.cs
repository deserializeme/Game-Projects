using UnityEngine;

namespace RectMaker
{
    public struct Rect
    {
        public float scale;

        public Color color;
        public float minX;
        public float minY;
        public float maxX;
        public float maxY;

        public Vector3 center;

        public Rect(float scale, Color color, Vector3 center)
        {
           
            this.color = color;
            
            this.scale = scale;

            this.center = center;

            //directions say to make it a rectangle, so we're making it a rectangle

            // DEF: a plane figure with four straight sides and four right angles, 
            //especially one with unequal adjacent sides, in contrast to a square.
            this.minX = 0 - this.scale;
            this.minY = 0 - this.scale/2;
            this.maxX = 0 + this.scale;
            this.maxY = 0 + this.scale/2;
        }
    };

}
