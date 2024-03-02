using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 1.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float yPosition = Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(transform.position.x, startPosition.y + yPosition, transform.position.z);
    }

}
