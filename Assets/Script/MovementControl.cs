using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public float cubeSpeed;
    public GameObject blocker;
    private bool isMove = false;
    private Vector3 direction = Vector3.zero;
    private GameObject checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        checkpoint = GameObject.Find("Checkpoint");
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
            transform.Translate(direction * cubeSpeed * Time.deltaTime);
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

    private void StopMotion()
    {
        isMove = false;
        transform.Translate(-direction * 0.1f);
    }

    private void SpawnBlock()
    {
        Vector3 pos = checkpoint.transform.position - new Vector3(0f, 1f, 0f);
        Instantiate(blocker, pos, Quaternion.identity);
    }

}
