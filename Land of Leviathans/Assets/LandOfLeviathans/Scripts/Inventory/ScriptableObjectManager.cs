#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace LoL
{
    public static class ScriptableObjectManager 
    {       
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            if(Resources.Load(typeof(T).ToString()) == null)
            {
                string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/" + typeof(T).ToString()
                    + ".asset"
                    );

                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;
            }
            else
            {
                Debug.Log(typeof(T).ToString() + " already created!");
            }
        }

        [MenuItem("Assets/Inventory/Create Items List Scriptable Object")]
        public static void CreateItemsList()
        {
            ScriptableObjectManager.CreateAsset<ItemsScriptableObject>();
        }

        [MenuItem("Assets/Inventory/Create Consumables List Scriptable Object")]
        public static void CreateConsumables()
        {
            ScriptableObjectManager.CreateAsset<ConsumablesScriptableObject>();
        }


        [MenuItem("Assets/Inventory/Create Spell Items List Scriptable Object")]
        public static void CreateSpellItemsList()
        {
            ScriptableObjectManager.CreateAsset<SpellItemScriptableObject>();
        }

        [MenuItem("Assets/Inventory/Create Weapon List Scriptable Object")]
        public static void CreateWeaponList()
        {
            ScriptableObjectManager.CreateAsset<WeaponScriptableObject>();
        }

        [MenuItem("Assets/Inventory/Create Armor List Scriptable Object")]
        public static void CreateArmorList()
        {
            ScriptableObjectManager.CreateAsset<ArmorScriptableObject>();
        }

        [MenuItem("Assets/Inventory/Create NPC List Scriptable Object")]
        public static void CreateNPCList()
        {
            ScriptableObjectManager.CreateAsset<NPCScriptableObject>();
        }

        [MenuItem("Assets/Inventory/Create Interactions List Scriptable Object")]
        public static void CreateInteractionsList()
        {
            ScriptableObjectManager.CreateAsset<InteractionsScriptableObject>();
        }
    }
}
#endif