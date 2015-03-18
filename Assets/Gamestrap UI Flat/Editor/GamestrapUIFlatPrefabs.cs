using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Custom Menu Items to create the prefabs directly from the Unity menu
/// </summary>
public static class GamestrapUIFlatPrefabs
{
    private const string dir = "GameObject/Gamestrap Flat/";
    private const int priority = 10;

    [MenuItem(dir + "Button",false,priority)]
    static void CreateButton(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "Button");
    }

    [MenuItem(dir + "Button with Icon",false,priority)]
    static void CreateButtonIcon(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "ButtonIcon");
    }

    [MenuItem(dir + "Icon Button",false,priority)]
    static void CreateIconButton(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "IconButton");
    }

    [MenuItem(dir + "InputField",false,priority)]
    static void CreateInputField(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "InputField");
    }

    [MenuItem(dir + "Scrollbar",false,priority)]
    static void CreateScrollbar(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "Scrollbar");
    }

    [MenuItem(dir + "Slider",false,priority)]
    static void CreateSlider(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "Slider");
    }

    [MenuItem(dir + "Toggle Slider",false,priority)]
    static void CreateSliderToggle(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "SliderToggle");
    }

    [MenuItem(dir + "Toggle Check",false,priority)]
    static void CreateToggleCheck(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "ToggleCheck");
    }

    [MenuItem(dir + "Toggle Radio Button",false,priority)]
    static void CreateToggleRadio(MenuCommand menuCommand)
    {
        InstantiateFlatGameObject(menuCommand, "ToggleRadio");
    }

    static void InstantiateFlatGameObject(MenuCommand menuCommand, string name)
    {
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Gamestrap UI Flat/Prefabs/" + name + ".prefab", typeof(GameObject));
        if (!prefab)
        {
            Debug.LogError("Prefab '" + name + ".prefab' missing from 'Assets/Gamestrap UI Flat/Prefabs/', make sure you have placed the prefab where it should be at.");
            return;
        }
        GameObject go = (GameObject) PrefabUtility.InstantiatePrefab(prefab);


        if (menuCommand.context)
        {
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        }
        else if (Selection.activeObject is GameObject) {
            GameObjectUtility.SetParentAndAlign(go, Selection.activeObject as GameObject);
        }

        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
