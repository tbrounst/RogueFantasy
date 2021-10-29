using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public Party playerParty;
    public int cash;
    public int level;

    [SerializeField] public UnityEvent LevelUpEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp(int cost)
    {
        if (cash > cost)
        {
            cash -= cost;
            level++;
            LevelUpEvent.Invoke();
        }
    }
}
