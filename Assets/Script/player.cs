using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Script;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
public class player : MonoBehaviour
{
    // declare 
    
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public string item_hold;
    [SerializeField]
    public string player_position= DeclareVariable.POSITION_PLAYER_DOWN;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    AudioSource audioData;
    [SerializeField]
    Slot_UI slot_select_item;
    [SerializeField]
    Slot_UI slot_budget_player;
    // declare const animation
    // speed default
    // declare inventory

    // declare inventory add remove, sort, item, ...
    /*
      here is the variable help manage inventory, item and buget
     */
    public Inventory inventory;
    // const speed
    private float SPEED_DEFAULT = 3f;
    // declare select item index
    public int select_item_index = 0;
    // lock just 1 method run at time RightClickEvent
    private static bool isRightClickEventRunning = false;


    // current action
    //private string current_action = DeclareVariable.ANIMATION_PLAYER_IDLE_DOWN;
    private string current_position = DeclareVariable.POSITION_PLAYER_DOWN;
    // declare variable

    private float moveHorizontal;
    private float moveVertical;
    // awake
    private void Awake()
    {
        inventory = new Inventory(21);
        // give player money
        inventory.PlayerBudget = 200;
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //gameObject.hideFlag
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        Moving();

        // tool
        /*        Hoeing();
                Axeing();*/
        RightClickEvent();

        UpdatePosition();

        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3Int position = new Vector3Int((int)transform.position.x,
                (int)transform.position.y, 0);

        }
        // holding item
        SelectItem();
        UpdateSelectItem();
        UpdateBudget();
    }
    void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Inventory_UI.instance.unSelectItem();
            if (select_item_index == 0)
            {
                select_item_index = inventory.items.Count - 1;
            }
            else
            {
                --select_item_index;
            }
            Inventory_UI.instance.selectItem();
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory_UI.instance.unSelectItem();
            if (select_item_index == inventory.items.Count - 1)
            {
                select_item_index = 0;
            }
            else
            {
                ++select_item_index;
            }
            Inventory_UI.instance.selectItem();
            return;
        }
    }
    void UpdateSelectItem()
    {
        slot_select_item.SetItem(inventory.items[select_item_index]);   
    }
    void UpdateBudget()
    {
        slot_budget_player.SetQuantity(inventory.PlayerBudget);
    }
    void UpdatePosition()
    {
        var horizontal = moveHorizontal;
        var vertical = moveVertical;
        if (horizontal == 0 && vertical == 0) return;
        if (horizontal > 0  && vertical == 0) current_position = DeclareVariable.POSITION_PLAYER_RIGHT ;
        if (horizontal == 0 && vertical > 0) current_position = DeclareVariable.POSITION_PLAYER_UP ;
        if (horizontal < 0 && vertical == 0) current_position = DeclareVariable.POSITION_PLAYER_LEFT ;
        if (horizontal == 0 && vertical < 0) current_position = DeclareVariable.POSITION_PLAYER_DOWN ;
        //return "";
    }
    void Moving()
    {
        if (moveHorizontal == 0 && moveVertical == 0)
        {
           
            //audioData.Stop();
            audioData.Play();
        }
        else
        {
          

            
        }
        if (animator.GetBool("isHoe")) return;
        
        Vector3 direction = new Vector3(moveHorizontal, moveVertical);
        AnimateMove(direction);
        transform.position += direction * speed * Time.deltaTime;
        
    }
/*    void Axeing()
    {
        if (inventory.items[select_item_index].type != CollectableType.TOOL_AXE) return;
        if (!(!animator.GetBool("isMoving") && !animator.GetBool("isHoe") && !animator.GetBool("isAxe"))) return;
        if (Input.GetMouseButtonDown(0))
        {
            {
                StartCoroutine(PlayAnimation("isAxe"));      
            }
        }
    }*/
    void RightClickEvent()
    {
        try
        {
            if (animator.GetBool("isMoving")) return;
            if (isRightClickEventRunning) return;
            if (Input.GetMouseButtonDown(0))
            {
                isRightClickEventRunning = true;
                var selectItem = inventory.items[select_item_index].type;
                StartCoroutine(PlayAnimation(selectItem));
                isRightClickEventRunning = false;
            }
        }
        catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
       
    }
/*    void Hoeing()
    {
        if (inventory.items[select_item_index].type != CollectableType.TOOL_HOE) return;
        if (!(!animator.GetBool("isMoving") && !animator.GetBool("isHoe") && !animator.GetBool("isAxe"))) return;
        if (Input.GetMouseButtonDown(0))
        {
            
           
            {
                StartCoroutine(PlayAnimation("isHoe"));
                
            }
        }
    }*/
    void AnimateMove(Vector3 direction)
    {
        if (animator != null)
        {
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    IEnumerator PlayAnimation(CollectableType collectableType)
    {
        // declare
        var animationStr = string.Empty;
       
        speed = 0;
        // check is tool
        switch (collectableType)
        {
            case CollectableType.TOOL_HOE:
                animationStr = "isHoe";
                break;
            case CollectableType.TOOL_WATERING:
                animationStr = "isWatering";
                break;
            case CollectableType.TOOL_AXE:
                animationStr = "isAxe";
                break;
            default:
                break;
        }
        // Kích hoạt animation
        animator.SetBool(animationStr, true);

        // Đợi cho đến khi animation kết thúc
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Tắt animation
        animator.SetBool(animationStr, false);
        speed = SPEED_DEFAULT;
        // start hoe  
        // set position x
        var currentPosition = current_position;
        float x = 0f;
        float y = 0f;

        switch (current_position)
        {
            case DeclareVariable.POSITION_PLAYER_RIGHT:
                x = math.floor( transform.position.x + 1f );
                y = math.floor(transform.position.y);
                break;
            case DeclareVariable.POSITION_PLAYER_UP:
                x = math.floor(transform.position.x);
                y = math.floor(transform.position.y + 1f); 
                break;
            case DeclareVariable.POSITION_PLAYER_LEFT:
                x = math.floor(transform.position.x - 1f);
                y = math.floor(transform.position.y); 
                break;
            case DeclareVariable.POSITION_PLAYER_DOWN:
                x = math.floor(transform.position.x);
                y = math.floor(transform.position.y - 1f); 
                break;
        }
        Vector3Int position = new Vector3Int((int) x, (int) y);
        Debug.Log($"This is player position {transform.position}");
        Debug.Log($"This is hoe position {position}");
        if (GameMannager.instance.tileMannager.IsInteractable(position))
        {
            switch (collectableType)
            {
                case CollectableType.TOOL_HOE:
                    GameMannager.instance.tileMannager.ActionHoeing(position);
                    break;
                case CollectableType.TOOL_AXE:
                    GameMannager.instance.tileMannager.ActionAxing(position);
                    break;
                case CollectableType.TOOL_WATERING:
                    GameMannager.instance.tileMannager.ActionWatering(position);
                    break;
                case CollectableType.SEED_PUMPKIN:
                    // plant
                    GameMannager.instance.tileMannager.SetPlant(position, CollectablePlant.PLANT_PUMPKIN);
                    // decrease number of seed
                    this.inventory.items[select_item_index].ThrowQuantity(1);
                    break;
                case CollectableType.SEED_CORN:
                    // plant
                    GameMannager.instance.tileMannager.SetPlant(position, CollectablePlant.PLANT_CORN);

                    // decrease number of seed
                    this.inventory.items[select_item_index].ThrowQuantity(1);
                    break;
                case CollectableType.SEED_CARROT:
                    // plant
                    GameMannager.instance.tileMannager.SetPlant(position, CollectablePlant.PLANT_CARROT);
                    // decrease number of seed
                    this.inventory.items[select_item_index].ThrowQuantity(1);
                    break;
                case CollectableType.SEED_BLUE_STAR_FRUIT:
                    // plant
                    GameMannager.instance.tileMannager.SetPlant(position, CollectablePlant.PLANT_BLUE_STAR_FRUIT);
                    // decrease number of seed
                    this.inventory.items[select_item_index].ThrowQuantity(1);
                    break;
                default: break;
            }
            //Thread.Sleep(200);
            
        }
    }



}
