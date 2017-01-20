using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveballmover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate() {


        Waveball[] balls = FindObjectsOfType<Waveball>();
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].m_PostCollidePos = new Vector2(balls[i].transform.position.x, balls[i].transform.position.y);
            float closestdist = float.MaxValue;
            float closestrad = 0;
            float closestpenetration = 0;
            Vector2 closest = Vector2.zero;
            bool collided = false;
            for (int j = 0; j < balls.Length; j++)
            {
                if (j != i)
                {
                    Vector3 diff = (balls[i].transform.position - balls[j].transform.position);
                    float mag = diff.magnitude;
                    if (mag < closestdist && mag < balls[i].m_Radius + balls[j].m_Radius && closestpenetration < (balls[i].m_Radius + balls[j].m_Radius) - mag)
                    {
                        closestdist = mag;
                        closestpenetration = (balls[i].m_Radius + balls[j].m_Radius) - mag;
                        closest = new Vector2(diff.x, diff.y);
                        closestrad = balls[j].m_Radius;
                        collided = true;
                    }
                }
            }
            if (collided)
            {
                balls[i].m_PostCollidePos += closest.normalized * closestpenetration * 0.5f;
            }
        }
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].Move();
        }
    }
}
