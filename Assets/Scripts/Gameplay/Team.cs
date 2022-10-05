using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour // required for instantiate
{
    

    private List<WormBehaviourController> worms = new List<WormBehaviourController>();

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

        foreach (var hit in hits)
        {
            if (!hit.transform.gameObject.CompareTag("Water"))
            {
                if (hit.point.y > waterLevel)
                {
                    nonWater = true;
                }
            }

            if (!hit.transform.gameObject.CompareTag("Water") && !hit.transform.gameObject.CompareTag("WorldObject")) // Stops say two worms from spawning on each other.
            {
                return (false, null);
            }
        }

        if (nonWater)
        {
            var worm = Instantiate(wormPrefab, new Vector3(x, spawnHeight, z), Quaternion.identity);
            return (true, worm.GetComponent<WormBehaviourController>());
        }

        return (false, null);
    }
}
