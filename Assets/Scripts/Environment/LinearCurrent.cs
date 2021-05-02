using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCurrent : EnvironmentZone
{
    public Vector3 m_CurrentDirection;
    public float m_CurrentForce;

    protected override void DoStart()
    {
        m_CurrentDirection.Normalize();
    }

    protected override void DoUpdateObjectsAffected()
    {
        foreach (Rigidbody2D affectedObject in m_ObjectsAffected)
        {
            affectedObject.AddForce(m_CurrentDirection * m_CurrentForce * Time.deltaTime);
        }
    }
}
