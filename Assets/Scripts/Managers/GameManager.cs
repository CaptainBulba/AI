using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The GameManager keeps track of global information for the game, such as the players and the location of the gaol.
// The GameManager is a Singleton (see Nystrom, chapter II.6), there is only single instance of it.

public class GameManager : MonoBehaviour
{
    // This is the single instance of the class
    private static GameManager instance = null;

    // Keep track of all the players
    private const int numSedans = 10;

    private const int numPolice = 2;

    [SerializeField]
    GameObject sedanPrefab;
    [SerializeField]
    GameObject policePrefab;
    [SerializeField]
    GameObject prison;

    private string policeTag = "Police";
    private string sedanTag = "Sedan";
    private string obstacleTag = "Obstacle";
    private string wallTag = "Wall";
    private string jailedTag = "Jailed";

    private float catchDistance = 0.1f;
    private float jailedDistance = 3f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        // Create all the players.
        for (int i = 0; i < numSedans; i++)
        {
            Instantiate(sedanPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
        }

        for (int i = 0; i < numPolice; i++)
        {
            Instantiate(policePrefab, new Vector3(10f, 0f, 0f), Quaternion.identity);
        }
    }

    public GameObject GetPrison()
    {
        return prison;
    }

    public string GetJailedTag()
    {
        return jailedTag;
    }

    public string GetPoliceTag()
    {
        return policeTag;
    }

    public string GetSedanTag()
    {
        return sedanTag;
    }

    public string ObstacleTag()
    {
        return obstacleTag;
    }

    public string GetWallTag()
    {
        return wallTag;
    }

    public float GetCatchDistance()
    {
        return catchDistance;
    }

    public float GetJailedDistance()
    {
        return jailedDistance;
    }

    // Return the single instance of the class
    public static GameManager Instance()
    {
        return instance;
    }
}
