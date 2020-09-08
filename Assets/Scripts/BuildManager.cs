using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [SerializeField] private NodeUI nodeUI;
    private Node selectedNode;

    private TurretBlueprint turretToBuild;
    public GameObject buildEffect;
    public GameObject sellEffect;

    void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; }}
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }


    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
