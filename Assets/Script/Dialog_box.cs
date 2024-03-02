using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dialog_box : MonoBehaviour
{
    // Start is called before the first frame update
    bool check = true;
    float speed;
    void Start()
    {
        speed = 10f;
    }
    private float elapsedTime = 0f;
    
    [SerializeField]
    public float countdownTime = 1f;
    [SerializeField]
    float range = 5f;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    CollectablePlant plant;

    // Update is called once per frame
    private void Awake()
    {
        
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        var positionTemp = transform.position;
        //positionTemp.y += 0.2f;
        if (elapsedTime >= countdownTime)
        {
            if(check)
            {
                positionTemp.y += range;
                transform.position = Vector3.MoveTowards(transform.position, positionTemp, speed * Time.deltaTime);
            }
            else
            {
                positionTemp.y -= range;
                transform.position = Vector3.MoveTowards(transform.position, positionTemp, speed * Time.deltaTime);
            }
            elapsedTime = 0;
            check = !check;
        }
    }
    void EventOnlick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject parentGameObject = transform.parent.gameObject;
        }
            
    }
}
