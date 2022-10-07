using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : ScriptableObject // required for instantiate
{
    

    private List<WormBehaviourController> worms = new List<WormBehaviourController>();
    private int wormIndex = 0;

    public Team(int wormsCount, Material material, float x, float y, float startHeight, float waterLevel, GameObject wormPrefab)
    {
        for (int i = 0; i < wormsCount; i++)
        {
            while(true)
            {
                var xPos = Random.Range(x / 2, -x / 2);
                var yPos = Random.Range(y / 2, -y / 2);

                (bool, WormBehaviourController) res = Team.TrySpawnWorm(xPos, yPos, startHeight, waterLevel, material, wormPrefab);

                if (res.Item1)
                {
                    this.worms.Add(res.Item2);
                    break;
                }
            }
        }
    }

    static (bool, WormBehaviourController) TrySpawnWorm(float x, float z, float spawnHeight, float waterLevel, Material material, GameObject wormPrefab)
    {
        var hits = Physics.RaycastAll(new Vector3(x, spawnHeight, z), Vector3.down, Mathf.Infinity, Physics.AllLayers);

        var nonWater = false;
        var highest = 0.0f;

        foreach (var hit in hits)
        {
            if (!hit.transform.gameObject.CompareTag("Water") && hit.point.y > waterLevel) // If hitpoint is non water and isn't under the water level
            {
                nonWater = true;
                if (hit.point.y > highest) // Keeps track of the highest point under the spawn point
                {
                    highest = hit.point.y;
                }
            }

            if (!hit.transform.gameObject.CompareTag("Water") && !hit.transform.gameObject.CompareTag("WorldObject")) // Stops, say two worms from spawning on each other.
            {
                return (false, null);
            }
        }

        if (nonWater) // If the highest point isn't water
        {
            var worm = Instantiate(wormPrefab, new Vector3(x, highest + 1.0f, z), Quaternion.identity);
            var wormBehaviourController = worm.GetComponent<WormBehaviourController>();
            wormBehaviourController.SetMaterial(material);
            return (true, wormBehaviourController);
        }

        return (false, null);
    }

    public void NextTurn()
    {
        this.wormIndex++;
        if (this.wormIndex >= this.worms.Count)
        {
            this.wormIndex = 0;
        }

        this.worms[this.wormIndex].isActive = true;
    }

    public bool Kill(WormBehaviourController worm)
    {
        foreach (var aliveWorm in this.worms)
        {
            if (aliveWorm == worm)
            {
                var wasActive = aliveWorm.isActive;
                this.worms.Remove(aliveWorm);
                aliveWorm.gameObject.BroadcastMessage("Death");

                if (wasActive)
                {
                    GameObject.Find("GameplayController").GetComponent<GameplayScript>().NextTurn();
                }

                return true;
            }
        }

        return false;
    }

    public int TeamHealth()
    {
        var combined = 0;
        foreach (var worm in this.worms)
        {
            combined += worm.health;
        }

        return combined;
    }
}
