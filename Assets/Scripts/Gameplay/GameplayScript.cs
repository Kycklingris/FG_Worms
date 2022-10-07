using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayScript : MonoBehaviour
{
    public GameObject wormPrefab;

    public float mapWidth;
    public float mapHeight;
    public float startCheckHeight;
    public float waterLevel;
    public Material redMaterial;
    public Material blueMaterial;

    public GameObject pauseMenu;
    public GameObject lossMenu;

    private List<Team> teams = new List<Team>();
    private int teamIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        this.teams.Add(new Team(4, this.redMaterial, this.mapWidth, this.mapHeight, this.startCheckHeight, this.waterLevel, wormPrefab));
        this.teams.Add(new Team(4, this.blueMaterial, this.mapWidth, this.mapHeight, this.startCheckHeight, this.waterLevel, wormPrefab));

        NextTurn();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            this.pauseMenu.SetActive(true);
        }
    }

    public void NextTurn()
    {
        this.teamIndex++;
        if (this.teamIndex == this.teams.Count)
        {
            this.teamIndex = 0;
        }

        this.teams[this.teamIndex].NextTurn();
    }

    public void Kill(WormBehaviourController worm)
    {
        foreach (var team in this.teams)
        {
            if (team.Kill(worm))
            {
                break;
            }
        }

        this.CheckWin();
    }

    void CheckWin()
    {
         for (int i = 0; i < this.teams.Count; i++)
         {
            if (this.teams[i].TeamHealth() <= 0)
            {
                this.lossMenu.SetActive(true);
                var name = this.lossMenu.transform.Find("TeamName").gameObject.GetComponent<TMP_Text>().text = "Team " + (i + 1).ToString();
            }
         }
    }
}
