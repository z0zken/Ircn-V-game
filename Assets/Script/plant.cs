using Assets.Script;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class plant : MonoBehaviour
{
    // declare have
    //Plant Behaviour ;
    [SerializeField] int period;
    [SerializeField] bool continue_plant;
    [SerializeField] public Sprite[] sprites;

    // declare plant type
    [SerializeField]
    public CollectablePlant collectablePlant;

    // declare const product
    [SerializeField]
    GameObject product;

    // declare const sprite

    [SerializeField]
    SpriteRenderer spriteRenderer;

    // sai số khi trồng bắp vì asset bắp dài gấp đôi caya khác
    const float CONST_APPROXIMATELY = 0.5f;
    /// <summary>
    /// condition to grown up
    /// </summary>
    bool isEnoughTime = false;
    bool isWater = false;


    //Status status;

    private float elapsedTime = 0f;
    public float countdownTime = 60f;

    int plant_level = 0;
    //public Sprite sprite1;
    // Start is called before the first frame update
    void Start()
    {

        countdownTime = 3 * period;
        spriteRenderer.sprite = sprites[0];
    }
    
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        // Debug.Log($"Time here {elapsedTime}, reaching point {countdownTime} and result is {elapsedTime >= countdownTime}");
        // Kiểm tra nếu đã đạt đến thời gian đếm ngược
        // && plant_level < sprites.Length
        CheckingIsWater();
        CheckingIsEnoughTime();
        levelUpPlant();
    }

    void CheckingIsWater()
    {
        // ignore if watered
        if(collectablePlant == CollectablePlant.PLANT_CORN)
        {
            
            if (GameMannager.instance.tileMannager.IsWatered
               (new Vector3Int((int)Mathf.Floor(transform.position.x - CONST_APPROXIMATELY),
               (int)Mathf.Floor(transform.position.y - CONST_APPROXIMATELY))))
            {
                isWater = true;
                return;
            };
            isWater = false;
        }

        if (GameMannager.instance.tileMannager.IsWatered
               (new Vector3Int((int)Mathf.Floor(transform.position.x),
               (int)Mathf.Floor(transform.position.y))))
        {
            isWater = true;
            return;
        };
        isWater = false;
    }
    void CheckingIsEnoughTime()
    {
        if (elapsedTime >= countdownTime)
            isEnoughTime = true;
        else isEnoughTime = false;
    }
    public void levelUpPlant()
    {

        if (isWater && 
            isEnoughTime &&
            plant_level < sprites.Length - 1)
        {
                
            plant_level++;
            // Thực hiện các xử lý khi đạt đến thời gian cần
            //Debug.Log("Đã đạt đến thời gian đếm ngược!");

            // Reset thời gian để bắt đầu lại (nếu cần)
            isWater = false;
            isEnoughTime=false;
            DestroyIconStatus("IconLackOfWater(Clone)");
            spriteRenderer.sprite = sprites[plant_level];
            elapsedTime = 0f;
            if (collectablePlant == CollectablePlant.PLANT_CORN)
            {
                GameMannager.instance.tileMannager.
                    SetPlanted(new Vector3Int((int)Mathf.Floor(transform.position.x - CONST_APPROXIMATELY),
                    (int)Mathf.Floor(transform.position.y - CONST_APPROXIMATELY)));
            }
            else
            {
                GameMannager.instance.tileMannager.
                    SetPlanted(new Vector3Int((int)Mathf.Floor(transform.position.x),
                    (int)Mathf.Floor(transform.position.y)));
            }
                
        }
        if(!isWater &&
            isEnoughTime &&
            (plant_level < sprites.Length - 1))
        {
            if (!IsIconStatus("IconLackOfWater(Clone)"))
            {
                InitiateIconStatus(GameMannager.instance.prefabContainer.prefabWaterContainer);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (plant_level != sprites.Length - 1) return;
        if (collision != null && collision.tag == DeclareVariable.TAG_PLAYER)
        {
            float randomX = Random.Range(-2f, 2f);
            float randomY = Random.Range(-2f, 2f);
            var newPosition = new Vector3 (transform.position.x + randomX, transform.position.y + randomY, 0);
            Instantiate(product,
               newPosition,
                Quaternion.identity);

            if (continue_plant)
            {
                --plant_level;
                spriteRenderer.sprite = sprites[plant_level];
                elapsedTime = 0;
            }
            else
            {
                Destroy(this.gameObject);
            }
            
            //Debug.Log("yes player");
        }
        else
        {
            //Debug.Log("NOT player");
        }
    }
    private void OnDestroy()
    {
        GameMannager.instance.tileMannager.
            SetDefault(new Vector3Int((int)Mathf.Floor(transform.position.x), 
            (int)Mathf.Floor(transform.position.y)));
    }

    private void InitiateIconStatus(GameObject gameObjectTemp)
    {
        var child = Instantiate(gameObjectTemp
                    , transform.position, Quaternion.identity);
        child.transform.parent = transform;
    }
    private void DestroyIconStatus(string name)
    {
        if(gameObject.transform.Find(name)?.gameObject)
            Destroy(gameObject.transform.Find(name).gameObject);
    }
    private bool IsIconStatus(string name)
    {
        if (gameObject.transform.Find(name))
            return true;
        return false;
    }
    enum Status
    {
        none, lackOfWater, readyToHarvest
    }
}
