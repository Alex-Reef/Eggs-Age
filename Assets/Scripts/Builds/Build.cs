using UnityEngine;

public class Build : MonoBehaviour
{
    public int Health;
    public GameObject Platform;
    public TextMesh HealthBar;
    public BuildsController.BuildType BuildType;

    private void Start()
    {
        Health = 100;
        HealthBar.text = Health.ToString();
    }

    public void GetDamage(int damage)
    {
        if (Health <= 0)
            Destroy(this.gameObject);
        else
        {
            Health -= damage;
            HealthBar.text = Health.ToString();
        }
    }

    public void OnDestroy()
    {
        Platform.GetComponent<Platform>().Build = null;
    }
}
