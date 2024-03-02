using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
public class Environment_Mannagement : MonoBehaviour
{
    public static Environment_Mannagement instance;
    // declare 
    public int count_distance_date { get; private set; }
    public int count_date { get; private set; }
    public int count_month { get; private set; }
    public int count_year { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // another day
    public void TurnDay()
    {
        count_distance_date++;
        count_date++;
        if (count_date >= 31 && count_month >= 4)
        {
            count_date = 1;
            count_month = 1;
            count_year++;
            return;
        }
        if (count_date >= 31) {
            count_date = 1;
            ++count_month;
            return;
        }
        ++count_date;
    }
    // get season
    public string getSeason()
    {
        if (count_year == 1) return DeclareVariable.SEASON_SPRING;
        if (count_year == 2) return DeclareVariable.SEASON_SUMMER;
        if (count_year == 3) return DeclareVariable.SEASON_FALL;
        if (count_year == 4) return DeclareVariable.SEASON_WINTER;
        return "";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
