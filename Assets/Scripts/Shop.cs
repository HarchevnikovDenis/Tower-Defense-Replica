using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    [SerializeField] private TurretBlueprint standartTurret;
    [SerializeField] private TurretBlueprint missileLauncher;
    [SerializeField] private TurretBlueprint lazer;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLazerBeamer()
    {
        buildManager.SelectTurretToBuild(lazer);
    }
}
