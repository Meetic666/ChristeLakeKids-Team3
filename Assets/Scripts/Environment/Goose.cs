using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour
{
    public Transform m_PatrolPointsParent;

    public float m_ThrustIntervals;
    public float m_ThrustForce;
    public float m_PatrolEpsilon;

    List<Transform> m_PatrolPoints;
    int m_CurrentPatrolIndex;

    Rigidbody2D m_Rigidbody;

    float m_ThrustTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_PatrolPoints = new List<Transform>();
        foreach(Transform childTransform in m_PatrolPointsParent)
        {
            m_PatrolPoints.Add(childTransform);
        }

        m_CurrentPatrolIndex = 0;

        m_Rigidbody = GetComponent<Rigidbody2D>();

        m_ThrustTimer = Random.Range(0.0f, 200.0f * m_ThrustIntervals) / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Transform currentPatrolPoint = m_PatrolPoints[m_CurrentPatrolIndex];
        Vector3 positionDelta = currentPatrolPoint.position - transform.position;

        m_ThrustTimer -= Time.deltaTime;

        if(m_ThrustTimer <= 0.0f)
        {
            m_ThrustTimer = m_ThrustIntervals;

            transform.up = -positionDelta.normalized;
            m_Rigidbody.AddForce(positionDelta.normalized * m_ThrustForce, ForceMode2D.Impulse);

            m_Rigidbody.angularVelocity = 0.0f;
        }

        if(positionDelta.magnitude <= m_PatrolEpsilon)
        {
            m_CurrentPatrolIndex++;
            m_CurrentPatrolIndex %= m_PatrolPoints.Count;
        }
    }
}
