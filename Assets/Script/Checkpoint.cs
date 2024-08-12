using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float m_stayTime;
    private float m_stayTimer = 0f;
    private GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        m_stayTimer += Time.deltaTime;
        if(other.gameObject.tag == "Player" && m_stayTimer >= m_stayTime)
        {
            m_player.GetComponent<MovementControl>().Checkpoint();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_stayTimer = 0f;
    }
}
