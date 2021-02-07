using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetAllAbilities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sos = Resources.LoadAll("Abilities 1");
        foreach(var s in sos)
        {
            Ability ability = (Ability)s;
            Debug.Log(ability.components.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
