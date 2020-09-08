using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;                    //цвет объекта при наведении мыши
    public Color notEnoughMoneyColor;           //цвет выделение при недостаточном количестве денег

    [HideInInspector]
    public GameObject turret;                  //имеется ли на node-объекте турель
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgrated = false;

    private Renderer rend;
    private Color startColor;                   //обычный цвет объекта

    private BuildManager buildManager;
    public Vector3 positionOffset;              //сдвиг от позиции node для постройти турели

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;
        
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseOver()
    {
        if (!buildManager.CanBuild)
            return;
        if(turret != null || !buildManager.HasMoney)
        {
            rend.material.color = notEnoughMoneyColor;
        }
        else
        {
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    
    private void BuildTurret(TurretBlueprint blueprint)
    {
        if(blueprint == null)
        {
            return;
        }

        if (PlayerStats.Money < blueprint.cost)
            return;

        PlayerStats.Money -= blueprint.cost;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;
    }


    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
            return;

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Transform oldTurretTransform = turret.transform;
        //Destroy an old turret
        Destroy(turret);

        //Build a new Upgrated turret
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        isUpgrated = true;

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, oldTurretTransform.position, oldTurretTransform.rotation);
        turret = _turret;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        isUpgrated = false;

        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        Destroy(turret);
        turretBlueprint = null;
    }
}
