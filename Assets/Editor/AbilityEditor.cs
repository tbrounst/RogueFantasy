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
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Add Reg Attack"))
        {
            CreateComponenet(CreateInstance<RegAttackComponent>());
        }
        if (GUILayout.Button("Add Magic Attack"))
        {
            CreateComponenet(CreateInstance<MAttackComponent>());
        }
        if (GUILayout.Button("Add Buff"))
        {
            CreateComponenet(CreateInstance<RegBuffComponent>());
        }
        if (GUILayout.Button("Add Debuff"))
        {
            CreateComponenet(CreateInstance<RegDebuffComponent>());
        }
    }

    private void CreateComponenet(AbilityComponent input)
    {
        int index = ability.components.Count;
        ability.components.Add(input);
        ability.components[index].name = ability.name + index.ToString();
        AssetDatabase.AddObjectToAsset(ability.components[index], ability);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(ability));
    }

}