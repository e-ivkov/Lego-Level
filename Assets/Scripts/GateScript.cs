using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour
{
    public int MaxDefensePoints;
    int defensePoints;
    public GameObject text;

    public int DefensePoints
    {
        get
        {
            return defensePoints;
        }

        set
        {
            text.GetComponent<TextMesh>().text = value.ToString();
            defensePoints = value;
        }
    }

    private void Start()
    {
        DefensePoints = MaxDefensePoints;
    }
}
