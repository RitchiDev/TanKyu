using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    [Range (-1f, -10f)]
    [SerializeField]private float scrollSpeed = -4;
    Vector2 startPosition;
    [Range(-50, 50)]
    [SerializeField] private float length = 13;
    
    void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, length);
        transform.position = startPosition + Vector2.right * newPosition;
    }
}
