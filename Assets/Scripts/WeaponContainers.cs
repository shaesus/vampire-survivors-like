using UnityEngine;

public class WeaponContainers : MonoBehaviour
{
    public static WeaponContainers Instance { get; private set; }

    public WeaponContainer[] weaponContainers;

    public int LastWeaponIndex { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LastWeaponIndex = 0;
    }

    public void InitializeContainer(Sprite placeholder, string name, string type, float damage)
    {
        if (LastWeaponIndex < 8)
        {
            var container = weaponContainers[LastWeaponIndex];
            container.gameObject.SetActive(true);
            container.Placeholder.gameObject.SetActive(true);
            container.Placeholder.sprite = placeholder;
            container.InfoPanel.InitializePanel(name, type, damage);

            LastWeaponIndex++;
        }
    }
}
