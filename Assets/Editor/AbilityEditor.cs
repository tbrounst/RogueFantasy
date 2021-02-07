using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ability))]
public class AbilityEditor : Editor
{
    private Ability ability;

    void OnEnable()
    {
        ability = (Ability)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add Reg Attack"))
        {
            ability.components.Add(null);
            int index = ability.components.Count - 1;
            ability.components[index] = CreateInstance<RegAttackComponent>();
            AssetDatabase.AddObjectToAsset(ability.components[index], ability);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(index));
        }
        if (GUILayout.Button("Add Magic Attack"))
        {
            ability.components.Add(null);
            int index = ability.components.Count - 1;
            ability.components[index] = CreateInstance<MAttackComponent>();
            AssetDatabase.AddObjectToAsset(ability.components[index], ability);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(index));
        }
        if (GUILayout.Button("Add Buff"))
        {
            ability.components.Add(null);
            int index = ability.components.Count - 1;
            ability.components[index] = CreateInstance<RegBuffComponent>();
            AssetDatabase.AddObjectToAsset(ability.components[index], ability);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(index));
        }
        if (GUILayout.Button("Add Debuff"))
        {
            ability.components.Add(null);
            int index = ability.components.Count - 1;
            ability.components[index] = CreateInstance<RegDebuffComponent>();
            AssetDatabase.AddObjectToAsset(ability.components[index], ability);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(index));
        }
    }

}