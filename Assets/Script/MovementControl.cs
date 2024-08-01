using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public float cubeSpeed;
    private bool isMove = false;
    private Vector3 direction = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
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
            isMove = false;
            transform.Translate(-direction * 0.1f);
        }
    }

}
