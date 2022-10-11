using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject zoomCamera;

    void OnEnable()
    {
        Sniper.RecursiveEnable(this.gameObject.transform);
    }

    static void RecursiveEnable(Transform transform)
    {
        foreach (Transform tran in transform)
        {
            tran.gameObject.SetActive(true);
            Sniper.RecursiveEnable(tran);
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            this.Shoot();
        }
    }

    void Shoot()
    {
        var location = this.zoomCamera.transform.position;

        var forward = this.zoomCamera.transform.rotation * Vector3.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(location, forward, out hitInfo))
        {
            var tmp = Instantiate(this.hitPrefab, hitInfo.transform.position, Quaternion.identity);
            if (hitInfo.transform.gameObject.CompareTag("Worm"))
            {
                tmp.GetComponent<HitMarker>().Attack(hitInfo.transform.gameObject, forward);
            } else
            {
                tmp.SendMessage("OnLanded", this.gameObject);
            }
        } else // If the shot misses everything
        {
            GameObject.Find("GameplayController").GetComponent<GameplayScript>().NextTurn();
        }
            this.SendMessageUpwards("SentShot");
    }
}
