using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScript : MonoBehaviour
{
    public GameObject wormPrefab;

    public float mapWidth;
    public float mapHeight;
    public float startCheckHeight;
    public float waterLevel;
    public Material redMaterial;
    public Material blueMaterial;

    private List<Team> teams = new List<Team>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        this.teams.Add(new Team(4, this.redMaterial, this.mapWidth, this.mapHeight, this.startCheckHeight, this.waterLevel, wormPrefab));
        this.teams.Add(new Team(4, this.blueMaterial, this.mapWidth, this.mapHeight, this.startCheckHeight, this.waterLevel, wormPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
