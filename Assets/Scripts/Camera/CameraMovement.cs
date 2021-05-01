using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject m_TargetObject;

    public float m_HorizontalAccelerationThreshold;
    public float m_VerticalAccelerationThreshold;

    public float m_HorizontalDecelerationThreshold;
    public float m_VerticalDecelerationThreshold;

    public float m_MaxHorizontalSpeed;
    public float m_MaxVerticalSpeed;

    public float m_HorizontalAcceleration;
    public float m_VerticalAcceleration;

    public float m_HorizontalDeceleration;
    public float m_VerticalDeceleration;

    float m_CurrentHorizontalSpeed;
    float m_CurrentVerticalSpeed;

    bool m_HorizontalAccelerationEnabled;
    bool m_VerticalAccelerationEnabled;

    bool m_HorizontalDecelerationEnabled;
    bool m_VerticalDecelerationEnabled;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHorizontalSpeed = 0.0f;
        m_CurrentVerticalSpeed = 0.0f;

        m_HorizontalAccelerationEnabled = false;
        m_VerticalAccelerationEnabled = false;

        m_HorizontalDecelerationEnabled = false;
        m_VerticalDecelerationEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = m_TargetObject.transform.position - transform.position;
        float deltaTime = Time.deltaTime;

        UpdateHorizontalSpeed(Mathf.Abs(delta.x), ref deltaTime);
        UpdateVerticalSpeed(Mathf.Abs(delta.y), ref deltaTime);

        Vector3 newPosition = transform.position;

        newPosition.x += Mathf.Sign(delta.x) * m_CurrentHorizontalSpeed * deltaTime;
        newPosition.y += Mathf.Sign(delta.y) * m_CurrentVerticalSpeed * deltaTime;

        transform.position = newPosition;
    }

    void UpdateHorizontalSpeed(float horizontalDelta, ref float deltaTime)
    {
        if (horizontalDelta >= m_HorizontalAccelerationThreshold)
        {
            m_HorizontalAccelerationEnabled = true;
            m_HorizontalDecelerationEnabled = false;
        }
        else if (horizontalDelta < m_HorizontalDecelerationThreshold)
        {
            m_HorizontalAccelerationEnabled = false;
            m_HorizontalDecelerationEnabled = true;
        }

        if (m_HorizontalAccelerationEnabled)
        {
            m_CurrentHorizontalSpeed += m_HorizontalAcceleration * deltaTime;

            if (m_CurrentHorizontalSpeed > m_MaxHorizontalSpeed)
            {
                m_CurrentHorizontalSpeed = m_MaxHorizontalSpeed;
            }
        }
        else if (m_HorizontalDecelerationEnabled)
        {
            m_CurrentHorizontalSpeed -= m_HorizontalDeceleration * deltaTime;

            if (m_CurrentHorizontalSpeed < 0.0f)
            {
                m_CurrentHorizontalSpeed = 0.0f;
            }
        }
    }

    void UpdateVerticalSpeed(float verticalDelta, ref float deltaTime)
    {
        if (verticalDelta >= m_VerticalAccelerationThreshold)
        {
            m_VerticalAccelerationEnabled = true;
            m_VerticalDecelerationEnabled = false;
        }
        else if (verticalDelta < m_VerticalDecelerationThreshold)
        {
            m_VerticalAccelerationEnabled = false;
            m_VerticalDecelerationEnabled = true;
        }

        if (m_VerticalAccelerationEnabled)
        {
            m_CurrentVerticalSpeed += m_VerticalAcceleration * deltaTime;

            if (m_CurrentVerticalSpeed > m_MaxVerticalSpeed)
            {
                m_CurrentVerticalSpeed = m_MaxVerticalSpeed;
            }
        }
        else if (m_VerticalDecelerationEnabled)
        {
            m_CurrentVerticalSpeed -= m_VerticalDeceleration * deltaTime;

            if (m_CurrentVerticalSpeed < 0.0f)
            {
                m_CurrentVerticalSpeed = 0.0f;
            }
        }
    }
}
