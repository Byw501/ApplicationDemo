using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 3f;
    public Vector3 normalView;
    public Vector3 normalViewRot;
    public Vector3 frontView;
    public Vector3 frontViewRot;
    public Vector3 leftView;
    public Vector3 leftViewRot;

    private int direction = 1;
    private bool isRotate = false;
    private Vector3[] viewArraryPos;
    private Vector3[] viewArraryRot;
    private int rotateCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        viewArraryPos = new Vector3[3];
        viewArraryPos[0] = normalView;
        viewArraryPos[1] = frontView;
        viewArraryPos[2] = leftView;

        viewArraryRot = new Vector3[3];
        viewArraryPos[0] = normalViewRot;
        viewArraryPos[1] = frontViewRot;
        viewArraryPos[2] = leftViewRot;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            rotateCounter++;
            int index = rotateCounter % 3;
            transform.position = viewArraryPos[index];
            transform.localEulerAngles = viewArraryRot[index];
        }
        else
        {
            isRotate = false;
        }
        if(Input.GetAxis("Mouse X") > 0f && Input.GetMouseButton(1))
        {
            isRotate = true;
            direction = 1;
        }
        else if(Input.GetAxis("Mouse X") < 0f && Input.GetMouseButton(1))
        {
            isRotate = true;
            direction = -1;
        }
        else
        {
            isRotate = false;
        }

        if (isRotate)
        {
            float rotation = rotationSpeed * direction * Time.deltaTime;
            transform.RotateAround(player.transform.position, Vector3.up, rotation);
        }
    }
}
