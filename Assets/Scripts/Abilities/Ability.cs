using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum AbilityType { PHYSICAL, MAGICAL, CONDITION, DUALTYPE}

[CreateAssetMenu(menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName = "New Ability";
    public string description = "An ability";
    public int uses;
    public List<AbilityComponent> components = new List<AbilityComponent>();
    [HideInInspector]
    public bool requiresTarget;
    [HideInInspector]
    public int totalDamage;
    [HideInInspector]
    public AbilityType type;

    //public abstract void Initialize(GameObject obj);
    //public abstract void TriggerAbility();

    public void Awake()
    {
        SetValues();
    }

    public List<string> UseAbility(Unit caster, Unit target)
    {
        List<string> output = new List<string>();
        //if (targetSelfList.Count != components.Count)
        //{
        //    if (!requiresTarget)
        //    {
        //        target = caster;
        //    }
        //    foreach (AbilityComponent component in components)
        //    {
        //        output.Add(component.UseComponent(caster, target));
        //    }
        //    return output;
        //}

        //for (int ii = 0; ii < components.Count; ii++)
        //{
        //    AbilityComponent component = components[ii];
        //    Unit realTarget = target;
        //    if (targetSelfList[ii])
        //    {
        //        realTarget = caster;
        //    } 
        //    output.Add(component.UseComponent(caster, realTarget));
        //}
        //return output;
        foreach (AbilityComponent component in components)
        {
            Unit realTarget = target;
            if (component.targetSelf)
            {
                realTarget = caster;
            }
            output.Add(component.UseComponent(caster, realTarget));
        }
        return output;
    }

    public string GenerateToolTipText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{description}");
        foreach (AbilityComponent component in components)
        {
            sb.Append($"\n{component.GetDescription()}");
        }
        return sb.ToString();
    }

    public void SetValues()
    {
        requiresTarget = false;
        totalDamage = 0;
        type = AbilityType.CONDITION;
        if (components == null)
        {
            Debug.Log($"No components for {abilityName}");
            return;
        }

        foreach (AbilityComponent component in components)
        {
            if (!component.targetSelf)
            {
                requiresTarget = true;
                //return;
            }
            if (component is RegAttackComponent)
            {
                if (type == AbilityType.MAGICAL)
                {
                    type = AbilityType.DUALTYPE;
                }
                else
                {
                    type = AbilityType.PHYSICAL;
                }
                totalDamage += ((RegAttackComponent)component).baseDamage;
            }
            if (component is MAttackComponent)
            {
                if (type == AbilityType.PHYSICAL)
                {
                    type = AbilityType.DUALTYPE;
                }
                else
                {
                    type = AbilityType.MAGICAL;
                }
                totalDamage += ((MAttackComponent)component).baseDamage;
            }
        }
    }
}
