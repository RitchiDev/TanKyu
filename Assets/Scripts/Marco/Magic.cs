using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("GetHit", SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
    

    void Update()
    {
        
    }
}
