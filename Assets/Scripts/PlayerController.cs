//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class PlayerController : MonoBehaviour {
//    [SerializeField]
//    GameObject[] paddles;
//
//    bool frontRight = false;
//    bool backRight = true;
//
//    [SerializeField]
//    float speed = 100f;
//    [SerializeField]
//    float rotation = 0f;
//    [SerializeField]
//    float speedIncrement = 20f;
//    [SerializeField]
//    float rotationIncrement = 5f;
//
//    Rigidbody2D rb;
//
//    // Start is called before the first frame update
//    void Start() {
//        rb = GetComponent<Rigidbody2D>();
//    }
//
//    // Update is called once per frame
//    void Update() {
//        if (Input.GetKeyDown(KeyCode.A)) {
//
//            if (frontRight) {
//                frontRight = !frontRight;
//                paddles[0].SetActive(false);
//                paddles[1].SetActive(true);
//            }
//            else {
//                if (speed <= 200) {
//                    speed += speedIncrement;
//                }
//                else {
//                    speed = 220;
//                }
//                //rb.velocity = (transform.right / 10 + transform.up) * Time.deltaTime * speed;
//                rb.AddForceAtPosition(transform.up * Time.deltaTime * speed, paddles[1].transform.position, ForceMode2D.Impulse);
//            }
//        }
//        else if (Input.GetKeyDown(KeyCode.D)) {
//            if (!frontRight) {
//                frontRight = !frontRight;
//                paddles[0].SetActive(true);
//                paddles[1].SetActive(false);
//            }
//            else {
//                if (speed <= 200) {
//                    speed += speedIncrement;
//                }
//                else {
//                    speed = 220;
//                }
//                //rb.velocity = (-transform.right / 10 + transform.up) * Time.deltaTime * speed;
//                rb.AddForceAtPosition(transform.up * Time.deltaTime * speed, paddles[0].transform.position, ForceMode2D.Impulse);
//            }
//        }
//
//        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
//
//            if (backRight) {
//                rotation = 0;
//                backRight = !backRight;
//                paddles[2].SetActive(false);
//                paddles[3].SetActive(true);
//            }
//            else {
//                rotation -= rotationIncrement;
//                rb.angularVelocity = rotation;
//                //rb.AddForceAtPosition(transform.up * Time.deltaTime * 30, paddles[3].transform.position, ForceMode2D.Impulse);
//
//            }
//        }
//        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
//            if (!backRight) {
//                rotation = 0;
//                backRight = !backRight;
//                paddles[2].SetActive(true);
//                paddles[3].SetActive(false);
//            }
//            else {
//                rotation += rotationIncrement;
//                rb.angularVelocity = rotation;
//                //rb.AddForceAtPosition(transform.up * Time.deltaTime * 30, paddles[2].transform.position, ForceMode2D.Impulse);
//            }
//        }
//
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, EventListener
{
    [SerializeField]
    GameObject[] paddles;

    bool frontRight = false;
    bool backRight = true;

    [SerializeField]
    float frontPower = 2;
    [SerializeField]
    float backPower = 1.5f;

    Rigidbody2D rb;

    bool m_RaceStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(EventManager.Instance != null)
        {
            EventManager.Instance.RegisterListener(EventType.e_RaceStarted, this);
        }
    }

    // Update is called once per frame
    void Update() 
    {
        if(!m_RaceStarted)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            if (frontRight)
            {
                frontRight = !frontRight;
                paddles[0].SetActive(false);
                paddles[1].SetActive(true);
            }
            else
            {
                rb.AddForceAtPosition(transform.up * frontPower, paddles[1].transform.position, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!frontRight)
            {
                frontRight = !frontRight;
                paddles[0].SetActive(true);
                paddles[1].SetActive(false);
            }
            else
            {
                rb.AddForceAtPosition(transform.up * frontPower, paddles[0].transform.position, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (backRight)
            {
                backRight = !backRight;
                paddles[2].SetActive(false);
                paddles[3].SetActive(true);
            }
            else
            {
                // magnitude = 0
                rb.AddForceAtPosition(transform.up * backPower, paddles[3].transform.position, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!backRight)
            {
                backRight = !backRight;
                paddles[2].SetActive(true);
                paddles[3].SetActive(false);
            }
            else
            {
                rb.AddForceAtPosition(transform.up * backPower, paddles[2].transform.position, ForceMode2D.Impulse);
            }
        }
    }

    public void OnEventReceived(GameEvent gameEvent)
    {
        m_RaceStarted = true;
    }
}
