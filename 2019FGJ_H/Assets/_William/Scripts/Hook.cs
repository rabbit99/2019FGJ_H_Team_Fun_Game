using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public William.Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D " + collision.name + "  "+ collision.transform.position);
        if(collision.tag == "wood") 
        {
            m_player.Hit(collision.transform.position);
        }
    }
}
