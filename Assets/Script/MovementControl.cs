using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public float cubeSpeed;
    public GameObject blocker;
    public Vector3 sizeMax;
    private bool isMove = false;
    private Vector3 direction = Vector3.zero;
    private GameObject checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        checkpoint = GameObject.Find("Checkpoint");
        sizeMax -= Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                isMove = true;
                direction = Vector3.right;
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                isMove = true;
                direction = Vector3.left;
            }
            else if(Input.GetAxis("Vertical") > 0f)
            {
                isMove = true;
                direction = Vector3.forward;
            }
            else if (Input.GetAxis("Vertical") < 0f)
            {
                isMove = true;
                direction = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                isMove = true;
                direction = Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                isMove = true;
                direction = Vector3.down;
            }
        }

        if (isMove)
        {
            transform.position += direction * cubeSpeed * Time.deltaTime;
            if(transform.position.x > sizeMax.x)
            {
                transform.position = new Vector3(sizeMax.x,transform.position.y, transform.position.z);
                StopMotion();
            }
            if (transform.position.x < 0f)
            {
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                StopMotion();
            }
            if (transform.position.y > sizeMax.y)
            {
                transform.position = new Vector3(transform.position.x, sizeMax.y, transform.position.z);
                StopMotion();
            }
            if (transform.position.y < 0f)
            {
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                StopMotion();
            }
            if (transform.position.z > sizeMax.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, sizeMax.z);
                StopMotion();
            }
            if (transform.position.z < 0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                StopMotion();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Unbreakable")
        {
            StopMotion();
        }
        else if(collision.gameObject.tag == "Breakable")
        {
            StopMotion();
            Destroy(collision.gameObject);
            SpawnBlock();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Unbreakable")
        {
            StopMotion();
        }
        else if (collision.gameObject.tag == "Breakable")
        {
            StopMotion();
            Destroy(collision.gameObject);
            SpawnBlock();
        }
    }

    private void StopMotion()
    {
        isMove = false;
        Vector3 pos = transform.position;
        pos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
        transform.position = pos;
    }

    private void SpawnBlock()
    {
        Vector3 pos = checkpoint.transform.position - new Vector3(0f, 1f, 0f);
        Instantiate(blocker, pos, Quaternion.identity);
    }

}
