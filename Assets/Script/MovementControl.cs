using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementControl : MonoBehaviour
{
    public float cubeSpeed;
    public GameObject blocker;
    public Vector3 sizeMax;
    public GameObject[] spawnBlockList;
    public int levelNum;
    private bool isMove = false;
    private bool isBlockMove = false;
    private Vector3 m_direction = Vector3.zero;
    private Vector3 m_blockDirect = Vector3.zero;
    private GameObject checkpoint;
    private int spawnCounter = 0;
    private Transform m_blockTransform;
    //private int levelNum;

    // Start is called before the first frame update
    void Start()
    {
        checkpoint = GameObject.Find("Checkpoint");
        sizeMax -= Vector3.one;
        //levelNum = PlayerPrefs.GetInt("levelNum", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            GetInputAxis();
        }

        if (isMove)
        {
            Move(transform, m_direction, ref isMove);
        }
    }

    private void GetInputAxis()
    {
        if (Input.GetAxis("Horizontal") > 0f)
        {
            isMove = true;
            m_direction = Vector3.right;
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            isMove = true;
            m_direction = Vector3.left;
        }
        else if (Input.GetAxis("Vertical") > 0f)
        {
            isMove = true;
            m_direction = Vector3.forward;
        }
        else if (Input.GetAxis("Vertical") < 0f)
        {
            isMove = true;
            m_direction = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            isMove = true;
            m_direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            isMove = true;
            m_direction = Vector3.down;
        }
    }

    private void Move(Transform localTransform, Vector3 direct, ref bool moveCheck)
    {
        localTransform.position += direct * cubeSpeed * Time.deltaTime;
        if (localTransform.position.x > sizeMax.x)
        {
            localTransform.position = new Vector3(sizeMax.x, localTransform.position.y, localTransform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (localTransform.position.x < 0f)
        {
            localTransform.position = new Vector3(0f, localTransform.position.y, localTransform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (localTransform.position.y > sizeMax.y)
        {
            localTransform.position = new Vector3(localTransform.position.x, sizeMax.y, localTransform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (localTransform.position.y < 0f)
        {
            localTransform.position = new Vector3(localTransform.position.x, 0f, localTransform.position.z);
            StopMotion(transform, ref moveCheck);
        }
        if (localTransform.position.z > sizeMax.z)
        {
            localTransform.position = new Vector3(localTransform.position.x, localTransform.position.y, sizeMax.z);
            StopMotion(transform, ref moveCheck);
        }
        if (localTransform.position.z < 0f)
        {
            localTransform.position = new Vector3(localTransform.position.x, localTransform.position.y, 0f);
            StopMotion(transform, ref moveCheck);
        }
    }

    private void PushBlock(MovementControlBlock mcb)
    {
        mcb.m_direction = m_direction;
        mcb.m_isMove = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unbreakable")
        {
            StopMotion(transform, ref isMove);
        }
        else if (collision.gameObject.tag == "Breakable")
        {
            StopMotion(transform, ref isMove);
            Destroy(collision.gameObject);
            SpawnBlock();
        }
        else if (collision.gameObject.tag == "Slide")
        {
            MovementControlBlock mcb = collision.gameObject.GetComponent<MovementControlBlock>();
            PushBlock(mcb);
            StopMotion(transform, ref isMove);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Unbreakable")
        {
            StopMotion(transform, ref isMove);
        }
        else if (collision.gameObject.tag == "Breakable")
        {
            StopMotion(transform, ref isMove);
            Destroy(collision.gameObject);
            SpawnBlock();
        }
        else if (collision.gameObject.tag == "Slide")
        {
            MovementControlBlock mcb = collision.gameObject.GetComponent<MovementControlBlock>();
            PushBlock(mcb);
            StopMotion(transform, ref isMove);
        }
    }

    private void StopMotion(Transform localTransform, ref bool moveCheck)
    {
        StopPlayerMotion(localTransform, ref moveCheck);
    }

    private void StopPlayerMotion(Transform localTransform, ref bool moveCheck)
    {
        moveCheck = false;
        Vector3 pos = localTransform.position;
        pos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
        localTransform.position = pos;
        m_direction = Vector3.zero;
    }

    public void SpawnBlock()
    {
        if (spawnCounter < spawnBlockList.Length)
        {
            spawnBlockList[spawnCounter].SetActive(true);
            spawnCounter++;
        }

    }

    public void Checkpoint()
    {
        levelNum++;
        //PlayerPrefs.SetInt("levelNum", levelNum);
        SceneManager.LoadScene("level" + levelNum.ToString());
    }

}
