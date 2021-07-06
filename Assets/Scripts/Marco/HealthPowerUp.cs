using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject HPpowerup;
    bool timer;
    int count;

    private void OnTriggerEnter2D(Collider2D col)
    {
    if (timer = false)
        if (col.gameObject.tag == "PlayerOne" || col.gameObject.tag == "PlayerTwo")
        {
            col.gameObject.GetComponent<Player>().ChangeHealth(1f);
            timer = true;
        }
    }
    void Update()
    {
        if (timer)
        {
            HPpowerup.SetActive(false);
            count++;
            if (count > 400)
            {
                count = 0;
                HPpowerup.SetActive(true);
                timer = false;
            }
        }
    }
}
