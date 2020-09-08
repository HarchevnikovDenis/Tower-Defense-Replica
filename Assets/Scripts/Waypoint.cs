using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public static Transform[] points;                   //массив контрольных точек

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);          //контрольные точки берутся из дочерних объектов
        }
    }
}
