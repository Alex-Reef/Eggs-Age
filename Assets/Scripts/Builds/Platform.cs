using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject OwnerPlayer;
    public GameObject Build;
    private int Health = 100;
    public TextMesh HealthBar;
    private List<GameObject> unitForCapturing = new List<GameObject>();
    public BuildsController.PlatformType Type;

    public void Building(GameObject build)
    {
        GameObject newBuild = Instantiate(build, transform.position, Quaternion.identity);
        Build = newBuild;
    }

    private void Start()
    {
        HealthBar.text = "";
        InvokeRepeating("CheckCapturing", 1, 1);
    }

    private void OnDestroy()
    {
        OwnerPlayer = null;
        Build = null;
        HealthBar = null;
    }

    private void Update()
    {
        HealthBar.transform.rotation = Camera.main.transform.rotation;
        if (Build)
            HealthBar.text = "";
        if (Health == 100)
            HealthBar.text = "";
    }

    public void CheckCapturing()
    {
        if (!Build)
        {
            unitForCapturing.Clear();
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
            bool OwnerUnitFound = false;
            GameObject Player = null;
            if (!Build)
            {
                if (colliders.Length > 0)
                {
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject.GetComponent<Unit>())
                        {
                            if (collider.gameObject.GetComponent<Unit>().OwnerPlayer == OwnerPlayer)
                            {
                                OwnerUnitFound = true;
                            }
                            else
                            {
                                Player = collider.gameObject.GetComponent<Unit>().OwnerPlayer;
                                unitForCapturing.Add(collider.gameObject);
                            }
                        }
                    }
                    if (!OwnerUnitFound && Player)
                    {
                        if (Health <= 0)
                            Capture(Player);
                        else
                        {
                            Health -= 5;
                            HealthBar.text = Health.ToString();
                            StartCoroutine("HideHealthBar", 1.5f);
                        }
                    }
                    if (!Player && OwnerUnitFound)
                    {
                        if (Health < 100)
                        {
                            Health += 5;
                            HealthBar.text = Health.ToString();
                            StartCoroutine("HideHealthBar", 1.5f);
                        }
                        else
                        {
                            Health = 100;
                            HealthBar.text = Health.ToString();
                            StartCoroutine("HideHealthBar", 1.5f);
                        }
                    }
                }
            }
        }
    }

    IEnumerator HideHealthBar(float delay)
    {
        yield return new WaitForSeconds(delay);
        HealthBar.text = "";
    }

    public void Capture(GameObject player)
    {
        if (OwnerPlayer)
        {
            OwnerPlayer.GetComponent<Player>().Platforms.Remove(this.gameObject);
        }
        OwnerPlayer = player;
        player.GetComponent<Player>().Platforms.Add(this.gameObject);
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = player.GetComponent<Player>().PlayerMaterial;
        Health = 5;
        HealthBar.text = Health.ToString();
        StartCoroutine("HideHealthBar", 1.5f);

        foreach(var unit in unitForCapturing)
        {
            unit.GetComponent<Unit>().Task = UnitsController.UnitTasks.None;
        }
        unitForCapturing.Clear();
    }
}
