using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public static GameMannager instance;
    public TileMannager tileMannager;
    public Environment_Mannagement environment_Mannagement;
    public PrefabContainer prefabContainer;
    // public player playerr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        tileMannager = GetComponent<TileMannager>();
        environment_Mannagement = GetComponent<Environment_Mannagement>();
        prefabContainer = GetComponent<PrefabContainer>();
       // playerr = GetComponent<player>();

    }
}
