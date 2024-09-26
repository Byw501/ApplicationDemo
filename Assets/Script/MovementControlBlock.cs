using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControlBlock : MonoBehaviour
{
    public float cubeSpeed;
    private Vector3 sizeMax;
    public Vector3 m_direction;
    public GameObject m_player;
    public bool m_isMove = false;
    MovementControl m_playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        if(m_player == null)
        {
            m_player = GameObject.Find("Cube");
        }
        m_playerMovement = m_player.GetComponent<MovementControl>();
        sizeMax = m_playerMovement.sizeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isMove)
        {
            Move(m_direction, ref m_isMove);
        }
    }

    public void Move(Vector3 direct, ref bool moveCheck)
    {
        transform.position += direct * cubeSpeed * Time.deltaTime;
        if (transform.position.x > sizeMax.x)
        {
            transform.position = new Vector3(sizeMax.x, transform.position.y, transform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (transform.position.x < 0f)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (transform.position.y > sizeMax.y)
        {
            transform.position = new Vector3(transform.position.x, sizeMax.y, transform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (transform.position.z > sizeMax.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, sizeMax.z);
            StopMotion(transform, ref moveCheck);
        }
        if (transform.position.z < 0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            StopMotion(transform, ref moveCheck);
        }
    }

    private void StopMotion(Transform localTransform, ref bool moveCheck)
    {
        moveCheck = false;
        Vector3 pos = localTransform.position;
        pos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
        localTransform.position = pos;
        m_direction = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unbreakable")
        {
            StopMotion(transform, ref m_isMove);
        }
        else if (collision.gameObject.tag == "Breakable")
        {
            StopMotion(transform, ref m_isMove);
            Destroy(collision.gameObject);
            m_playerMovement.SpawnBlock();
        }
        else if (collision.gameObject.tag == "Slide")
        {
            MovementControlBlock mcb = collision.gameObject.GetComponent<MovementControlBlock>();
            PushBlock(mcb);
            StopMotion(transform, ref m_isMove);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Unbreakable")
        {
            StopMotion(transform, ref m_isMove);
        }
        else if (collision.gameObject.tag == "Breakable")
        {
            StopMotion(transform, ref m_isMove);
            Destroy(collision.gameObject);
            m_playerMovement.SpawnBlock();
        }
        else if (collision.gameObject.tag == "Slide")
        {
            MovementControlBlock mcb = collision.gameObject.GetComponent<MovementControlBlock>();
            PushBlock(mcb);
            StopMotion(transform, ref m_isMove);
        }
    }

    private void PushBlock(MovementControlBlock mcb)
    {
        if (!m_isMove)
        {
            mcb.m_direction = m_direction;
            mcb.m_isMove = true;
        }

    }
}
