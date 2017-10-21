using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    [CreateAssetMenu(menuName ="Algorithms/Hilbert")]
    public class HilbertCurve : ScriptableObject
    {        
        /*
         * The Hilbert space filling curve is a one dimensional curve which visits every point within a two dimensional space. 
         * It may be thought of as the limit of a sequence of curves which are traced through the space. 
         * Curve H1 has four vertices at the center of each quarter of the unit square. 
         * Curve H2 has 16 vertices each at the centre of a sixteenth of the unit square.
         * The next member of the family comes from treating each quarter as the whole and repeating the operation. 
         * The actual Hilbert Curve is not a member of this family, it is the limit that the sequence approaches.         
         */
        public int m_x;//bot leftx
        public int m_y;//bot lefty
        public int m_xis;//rightvector
        public int m_xjs;//rightvector
        public int m_yis;//upvector
        public int m_yjs;//upvector
        [Range(0, 8)]
        public int m_n;//number of iterations
        [HideInInspector]
        public List<Vector2> m_Points;//store points         

        private void OnValidate()
        {
            m_Points = new List<Vector2>();
            hilbert(m_x, m_y, m_xis, m_xjs, m_yis, m_yjs, m_n);
        }

        private void hilbert(int x0, int y0, int xis, int xjs, int yis, int yjs, int n)
        {
            /*0, 0, 300, 0, 0, 300, 4
            /* x0 and y0 are the coordinates of the bottom left corner */
            /* xis & xjs are the i & j components of the unit x vector this frame */
            /* yis & yjs are the i & j components of the unit y vector this frame */
            if (n <= 0)
            {                
                var x = x0 + (xis + yis) / 2;
                var y = y0 + (xjs + yjs) / 2;
                m_Points.Add(new Vector2(x, y));
            }
            else
            {
                hilbert(x0, y0, yis / 2, yjs / 2, xis / 2, xjs / 2, n - 1); //first pos? yes.
                hilbert(x0 + xis / 2, y0 + xjs / 2, xis / 2, xjs / 2, yis / 2, yjs / 2, n - 1);//this goes up?
                hilbert(x0 + xis / 2 + yis / 2, y0 + xjs / 2 + yjs / 2, xis / 2, xjs / 2, yis / 2, yjs / 2, n - 1); //this goes right?
                hilbert(x0 + xis / 2 + yis, y0 + xjs / 2 + yjs, -yis / 2, -yjs / 2, -xis / 2, -xjs / 2, n - 1);//this goes down?
            }
        }
    }
}