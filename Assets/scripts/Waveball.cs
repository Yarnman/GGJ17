using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveball : MonoBehaviour {
    public float m_Radius;
    [SerializeField] float m_Gravity;
    [SerializeField] float m_BounceFactor;

    Vector2 m_OldPos;
    public Vector2 m_PostCollidePos;
    Wavephysics m_Wave;
	void Start ()
    {
        m_Wave = FindObjectOfType<Wavephysics>();
        m_OldPos = new Vector2(transform.position.x, transform.position.y);
        m_PostCollidePos = m_OldPos;
    }

    public void Move()
    {
        Vector2 pos2d = m_PostCollidePos;
        Vector2 newoldpos = pos2d;
        pos2d += (pos2d - m_OldPos);
        pos2d.y -= m_Gravity;
        m_OldPos = newoldpos;
        float interval = 0.01f;

        Vector2 closestpoint = Vector2.zero;
        float closestdist = float.MaxValue;
        float closestt = 0;
        for (float f = -m_Radius; f <= m_Radius; f += interval)
        {
            float checkx = pos2d.x + f;
            Vector2 p = new Vector2(checkx, m_Wave.GetHeightAtX(checkx));
            if ((p - pos2d).magnitude < closestdist)
            {
                closestdist = (p - pos2d).magnitude;
                closestpoint = p;
                closestt = checkx;
            }
        }

        if (closestdist < m_Radius)
        {
            float othert = closestt - 0.01f;
            Vector2 normpoll = new Vector2(othert, m_Wave.GetHeightAtX(othert));

            Vector2 norm = normpoll - closestpoint;
            norm = (new Vector2(norm.y, -norm.x)).normalized;

            //Vector2 dir = (pos2d - closestpoint);
            pos2d += norm * (m_Radius - closestdist);
        }

        float wavew = m_Wave.GetWidth();
        float x = pos2d.x;
        float y = pos2d.y;
        if (x - m_Radius <= -wavew * 0.5f)
        {
            x = -wavew * 0.5f + m_Radius;
        }
        else if (x + m_Radius >= wavew * 0.5f)
        {
            x = wavew * 0.5f - m_Radius;
        }

        float wavey = m_Wave.GetHeightAtX(x);
        if (y - m_Radius <= wavey)
        {
            y = wavey + m_Radius;
        }
        if (y + m_Radius >= m_Wave.GetMaxHeight())
        {
            y = m_Wave.GetMaxHeight() - m_Radius;
        }
        pos2d.x = x;
        pos2d.y = y;
        m_PostCollidePos = pos2d;
        transform.position = new Vector3(pos2d.x, pos2d.y, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
