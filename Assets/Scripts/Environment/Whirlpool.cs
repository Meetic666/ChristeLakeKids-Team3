using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : EnvironmentZone
{
    public float m_MinCurrentForce;
    public float m_MaxCurrentForce;

    public float m_MinTorque;
    public float m_MaxTorque;

    float m_Radius;

    protected override void DoStart()
    {
        m_Radius = gameObject.GetComponent<CircleCollider2D>().radius;
    }

    protected override void DoUpdate()
    {
        transform.Rotate(0.0f, 0.0f, m_MaxTorque * Time.deltaTime);
    }

    protected override void DoUpdateObjectsAffected()
    {
        foreach (Rigidbody2D affectedObject in m_ObjectsAffected)
        {
            Vector3 deltaPosition = affectedObject.transform.position - transform.position;
            float distance = deltaPosition.magnitude;

            float currentForce = Mathf.Lerp(m_MaxCurrentForce, m_MinCurrentForce, distance / m_Radius);
            Vector3 currentDirection = -deltaPosition.normalized;

            affectedObject.AddForce(currentDirection * currentForce * Time.deltaTime);

            float currentTorque = Mathf.Lerp(m_MaxTorque, m_MinTorque, distance / m_Radius);
            affectedObject.AddTorque(currentTorque * Time.deltaTime);
        }
    }
}
