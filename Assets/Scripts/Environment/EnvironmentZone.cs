using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentZone : MonoBehaviour
{
    protected List<Rigidbody2D> m_ObjectsAffected;

    // Start is called before the first frame update
    void Start()
    {
        m_ObjectsAffected = new List<Rigidbody2D>();

        DoStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ObjectsAffected.Count > 0)
        {
            DoUpdate();
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag == "Floatable")
        {
            m_ObjectsAffected.Add(collider2D.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Floatable")
        {
            m_ObjectsAffected.Remove(collider2D.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    protected virtual void DoStart()
    {

    }

    protected virtual void DoUpdate()
    {

    }
}
