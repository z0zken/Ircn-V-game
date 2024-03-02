using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;
public class TileMannager : MonoBehaviour
{
    const string TILE_RULE_SOIL = "Rule_Soil";
    [SerializeField] private GameObject gameObjectContainerPlant;
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap interactedMap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile_Hoe;
    [SerializeField] private Tile interactedTile_Water;
    [SerializeField] private Tile interactedTile_Plant;
    [SerializeField] private Tile interactedTile_Default;
    [SerializeField]
    public GameObject plant;
    public GameObject plant_corn;
    public GameObject plant_carrot;
    public GameObject plant_pumpkin;
    public GameObject plant_blueStarFruit;

    [SerializeField]
    public GameObject contain_Plant;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);  

        if (tile != null)
        {
            ///Debug.Log(tile.name);
            if (tile.name == TILE_RULE_SOIL)
            {
                return true;
            }
        }
        else
        {
            //Debug.Log("title is null");
        }
        return false;
    }
   
    public void SetPlant(Vector3Int position, CollectablePlant collectablePlant)
    {
        if (!IsHoed(position))
            throw new System.Exception("error: must be hoed before planting");
        if (IsPlanted(position))
            throw new System.Exception("error: there are plant in this position");
        float objectWeight = plant.GetComponent<SpriteRenderer>().bounds.size.x;
        float objectHeight = plant.GetComponent<SpriteRenderer>().bounds.size.y;
        GameObject plantTemp = plant_corn;
        switch (collectablePlant)
        {
            case CollectablePlant.PLANT_CORN: 
                plantTemp = plant_corn;
                break;
            case CollectablePlant.PLANT_PUMPKIN: 
                plantTemp = plant_pumpkin;
                break;
            case CollectablePlant.PLANT_CARROT:
                plantTemp = plant_carrot;
                break;
            case CollectablePlant.PLANT_BLUE_STAR_FRUIT:
                plantTemp = plant_blueStarFruit;
                break;
        }
        if (plantTemp == plant_corn)
        {
            GameObject childObject = Instantiate(plantTemp, new Vector3(position.x + objectWeight / 2, position.y + objectHeight + 0.20f, 0), Quaternion.identity);
            childObject.transform.parent = gameObjectContainerPlant.transform;
        }
        else
        {
            GameObject childObject = Instantiate(plantTemp, new Vector3(position.x + objectWeight / 2, position.y + objectHeight / 2 + 0.2f, 0), Quaternion.identity);
            childObject.transform.parent = gameObjectContainerPlant.transform;
        }
        SetPlanted(position);

    }

    public void ActionHoeing(Vector3Int position)
    {
        if (!IsDefault(position))
        {
            Debug.Log("Can't hoe which place not default: "+ interactedMap.GetTile(position));

            return;
        }
        SetHoed(position);
    }

    public void ActionWatering(Vector3Int position)
    {
        if (!IsPlanted(position))
        {
            Debug.Log("Can't water which place not be planted: " + interactedMap.GetTile(position));
            return;
        }
        SetWatered(position);
    }
    public void ActionAxing(Vector3Int position)
    {

        float objectWeight = plant.GetComponent<SpriteRenderer>().bounds.size.x;
        float objectHeight = plant.GetComponent<SpriteRenderer>().bounds.size.y;
        // 
        var positionCase1 = new Vector3(position.x + objectWeight / 2, position.y + objectHeight + 0.20f, 0);
        var positionCase2 = new Vector3(position.x + objectWeight / 2, position.y + objectHeight / 2 + 0.2f, 0);

        Transform[] objChild = contain_Plant.transform.GetComponentsInChildren<Transform>();
        // listing
        for (int i= 0; i < objChild.Length; i++)
        {
            var positionTemp = objChild[i].localPosition;
            if((positionTemp.x == positionCase1.x && positionTemp.y == positionCase1.y )|| (positionTemp.x == positionCase2.x && positionTemp.y == positionCase2.y))
            {
                Destroy(objChild[i].gameObject);
                break;
            }
        }
        SetDefault(position);
    }



    // check this tile is hoed or not








    public bool IsHoed(Vector3Int position)
    {
        return interactedMap.GetTile(position) == interactedTile_Hoe;
    }
    public bool IsPlanted(Vector3Int position)
    {
        return interactedMap.GetTile(position) == interactedTile_Plant;
    }
    public bool IsWatered(Vector3Int position)
    {
        return interactedMap.GetTile(position) == interactedTile_Water;
    }
    public bool IsDefault(Vector3Int position)
    {
        return interactedMap.GetTile(position) == null;
    }
    public void SetHoed(Vector3Int position)
    {
        interactedMap.SetTile(position, interactedTile_Hoe);
    }
    public void SetPlanted(Vector3Int position)
    {
        interactedMap.SetTile(position, interactedTile_Plant);
    }
    public void SetDefault(Vector3Int position)
    {
        interactedMap.SetTile(position, null);
    }
    public void SetWatered(Vector3Int position)
    {
        interactedMap.SetTile(position, interactedTile_Water);
    }
}
