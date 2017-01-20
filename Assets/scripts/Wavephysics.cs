using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavephysics : MonoBehaviour {

    [SerializeField] float m_MinFreq;
    [SerializeField] float m_MaxFreq;
    [SerializeField] float m_StartFreq;
    float m_frequency=2.0f;
    [SerializeField] float m_amplitude = 1.0f;
    [SerializeField] float m_Width = 1.0f;
    [SerializeField] float m_AirHeight = 0.2f;

    [SerializeField] float m_AdjustmentSpeed = 1.0f;

    public float GetWidth() { return m_Width; }
    public float GetMaxHeight() { return m_amplitude * 0.5f + m_AirHeight; }
	// Use this for initialization
	void Start () {
        m_frequency = m_StartFreq;
	}
	
	// Update is called once per frame
	void Update () {
        m_frequency += Input.GetAxis("Horizontal") * Time.deltaTime * m_AdjustmentSpeed;
        m_frequency = Mathf.Clamp(m_frequency, m_MinFreq, m_MaxFreq);
	}

    public float GetHeightAtX(float x)
    {
        x = Mathf.Clamp(x, -m_Width * 0.5f, m_Width * 0.5f);
        float frequency = m_frequency;
        x /= m_Width;
        if (!Application.isPlaying)
        {
            frequency = m_StartFreq;
        }
        return -(Mathf.Sin(x * frequency * Mathf.PI) * m_amplitude);
    }

    void OnDrawGizmos()
    {
        float interval = 0.005f;
        Gizmos.color = Color.green;
        for (float t = interval; t < 1.0f; t += interval)
        {
            float heightt = (t - 0.5f) * m_Width;
            float heightt2 = ((t- interval) - 0.5f) * m_Width;
            float y1 = GetHeightAtX(heightt);
            float y2 = GetHeightAtX(heightt2);
            Vector3 p1 = transform.position + new Vector3(heightt, y1, 0.0f);
            Vector3 p2 = transform.position + new Vector3(heightt2, y2, 0.0f);
            Gizmos.DrawLine(p1, p2);
        }
        Gizmos.DrawSphere(transform.position + new Vector3(-0.5f * m_Width, GetHeightAtX(-0.5f*m_Width), 0.0f), 0.01f);
        Gizmos.DrawSphere(transform.position + new Vector3(0.5f * m_Width, GetHeightAtX(0.5f*m_Width), 0.0f), 0.01f);
    }
}
