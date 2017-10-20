using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HilbertCurve : MonoBehaviour
{
    public List<Vector2> m_Points;
    /*
     * The Hilbert space filling curve is a one dimensional curve which visits every point within a two dimensional space. 
     * It may be thought of as the limit of a sequence of curves which are traced through the space. 
     * Curve H1 has four vertices at the center of each quarter of the unit square. 
     * Curve H2 has 16 vertices each at the centre of a sixteenth of the unit square.
       The next member of the family comes from treating each quarter as the whole and repeating the operation. 
       The actual Hilbert Curve is not a member of this family, it is the limit that the sequence approaches.
     * 
     * 
     * 
     */
    public int m_x;

    public int m_y;
    public int m_xis;
    public int m_xjs;
    public int m_yis;
    public int m_yjs;
    public int m_n;

    [ContextMenu("Execute")]
    void Execute()
    {
        m_Points = new List<Vector2>();
        hilbert(m_x, m_y, m_xis, m_xjs, m_yis, m_yjs, m_n);
    }
    private void Start()
    {
        StartCoroutine(GO());
    }
    void hilbert(int x0, int y0, int xis, int xjs, int yis, int yjs, int n)
    {
        /*0, 0, 300, 0, 0, 300, 4
        /* x0 and y0 are the coordinates of the bottom left corner */
        /* xis & xjs are the i & j components of the unit x vector this frame */
        /* yis & yjs are the i & j components of the unit y vector this frame */
        if (n <= 0)
        {
            Draw(x0 + (xis + yis) / 2, y0 + (xjs + yjs) / 2);
        }
        else
        {
            hilbert(x0, y0, yis / 2, yjs / 2, xis / 2, xjs / 2, n - 1); //first pos? yes.
            hilbert(x0 + xis / 2, y0 + xjs / 2, xis / 2, xjs / 2, yis / 2, yjs / 2, n - 1);//this goes up?
            hilbert(x0 + xis / 2 + yis / 2, y0 + xjs / 2 + yjs / 2, xis / 2, xjs / 2, yis / 2, yjs / 2, n - 1); //this goes right?
            hilbert(x0 + xis / 2 + yis, y0 + xjs / 2 + yjs, -yis / 2, -yjs / 2, -xis / 2, -xjs / 2, n - 1);//this goes down?
        }

    }

    void Draw(int x, int y)
    {
        m_Points.Add(new Vector2(x, y));
    }
    private void OnDrawGizmos()
    {
        DrawLines();
    }

    [ContextMenu("Draw")]
    public void DrawLines()
    {
        for(int current = 0; current < m_Points.Count-1; current++)
        {
            Gizmos.DrawLine(m_Points[current], m_Points[current + 1]);
        }
    }

    public IEnumerator GO()
    {
        
        int current = 0;
        Vector3 goalPosition = m_Points[m_Points.Count - 1];
        Vector3 destination = m_Points[current];
        Vector3 start = transform.position;
        
        while (transform.position != goalPosition )
        {
            var distRemaining = Vector3.Distance(transform.position, destination);
            var distTraveled = Vector3.Distance(start, transform.position);
            var distTotal = Vector3.Distance(start, destination);
            var fracJourney = distTraveled / distTotal;

            if (distRemaining < .01)
            {
                start = transform.position;
                destination = m_Points[current++];
            }

            transform.position = Vector3.Lerp(transform.position, destination, 1);


            yield return null;
        }

    }

}