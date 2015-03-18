using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using UnityEngine.UI;

[Serializable]
public class GamestrapUIFlat : EditorWindow
{
    //Current values in the editor window
    private Font font;
    private Color normal;
    private Color highlighted;
    private Color pressed;
    private Color disabled;
    private Color detail;
    private bool showColors = true;
    private bool shadow, gradient;

    private Vector2 scrollPos;

    private List<Color[]> colors;
    private int recursiveLevel;

    [MenuItem("Window/Gamestrap UI Flat Kit")]
    public static void ShowWindow()
    {
        GamestrapUIFlat gsFlat = (GamestrapUIFlat)EditorWindow.GetWindow(typeof(GamestrapUIFlat), false, "GS - Flat");
        gsFlat.minSize = new Vector2(215f, 260f);
    }

    void OnGUI()
    {
        if (colors == null)
        {
            SetUpColors();
        }
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        showColors = EditorGUILayout.Foldout(showColors, "Color List");
        if (showColors)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            // Saves bg color to set it back later
            Color defaultBg = GUI.backgroundColor;
            // Counter manages the colors per row it should show
            int counter = 0;
            foreach (Color[] color in colors)
            {
                GUI.backgroundColor = color[0]; // Sets the button color
                if (GUILayout.Button("") && color.Length >= 5)
                {
                    SetColors(color);
                }
                counter++;
                if (counter % 5 == 0)
                {
                    // Start a new row each 5
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = defaultBg; // Resets the color background

            GUILayout.EndVertical();
        }
        normal = EditorGUILayout.ColorField("Normal", normal);
        highlighted = EditorGUILayout.ColorField("Highlighted", highlighted);
        pressed = EditorGUILayout.ColorField("Pressed", pressed);
        disabled = EditorGUILayout.ColorField("Disabled", disabled);
        detail = EditorGUILayout.ColorField("Detail", detail);
        if (GUILayout.Button("Apply Colors"))
        {
            AssignColorsToSelection();
        }

        GUILayout.Label("Font", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        font = (Font)EditorGUILayout.ObjectField( font, typeof(Font), false);
        if (GUILayout.Button("Apply"))
        {
            AssignFontToSelection();
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Effects", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        shadow = EditorGUILayout.ToggleLeft("Shadow", shadow, GUILayout.MinWidth(75));
        gradient = EditorGUILayout.ToggleLeft( "Gradient", gradient, GUILayout.MinWidth(75));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Activate/Deactivate"))
        {
            ActivateEffects();
        }
        EditorGUILayout.EndScrollView();
    }

    #region Helper methods that are used on the OnGUI

    #region Activate/Deactivate Effects
    /// <summary>
    /// Sets the color to the UI elements based on what types of components they have
    /// </summary>
    private void ActivateEffects()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            ActivateEffects(go);
            // This resets the element so it updates the colors in the editor
            go.SetActive(false);
            go.SetActive(true);
        }
    }

    public void ActivateEffects(GameObject gameObject)
    {
        if (gameObject.GetComponent<GamestrapFlatGradient>())
        {
            GamestrapFlatGradient gradientComponent = gameObject.GetComponent<GamestrapFlatGradient>();
            if (gradient != gradientComponent.enabled)
            {
                EditorUtility.SetDirty(gradientComponent);
                gradientComponent.enabled = gradient;
            }
        }
        if (gameObject.GetComponent<Shadow>())
        {
            Shadow shadowComponent = gameObject.GetComponent<Shadow>();
            if (shadow != shadowComponent.enabled)
            {
                EditorUtility.SetDirty(shadowComponent);
                shadowComponent.enabled = shadow;
            }
        }
        if (gameObject.transform.childCount > 0) // Recursive search for components
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                ActivateEffects(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
    #endregion

    #region Set Font

    private void AssignFontToSelection(){
        foreach (GameObject go in Selection.gameObjects)
        {
            AssignFontToSelection(go);
            // This resets the element so it updates the colors in the editor
            go.SetActive(false);
            go.SetActive(true);
        }
    }

    private void AssignFontToSelection(GameObject gameObject)
    {
        if (gameObject.GetComponent<Text>())
        {
            Text text = gameObject.GetComponent<Text>();
            if (font)
            {
                Undo.RecordObject(text, "Change Font");
                text.font = font;
                EditorUtility.SetDirty(text);
            }
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            AssignFontToSelection(gameObject.transform.GetChild(i).gameObject);
        }
    }

    #endregion

    #region Assign Color Section
    /// <summary>
    /// Sets the color to the UI elements based on what types of components they have
    /// </summary>
    private void AssignColorsToSelection()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            recursiveLevel = 0;
            AssignColorsToSelection(go);
            // This resets the element so it updates the colors in the editor
            go.SetActive(false);
            go.SetActive(true);
        }
    }

    public void AssignColorsToSelection(GameObject gameObject)
    {
        recursiveLevel++;
        if (gameObject.GetComponent<Button>())
        {
            Button button = gameObject.GetComponent<Button>();
            SetColorBlock(button);
            SetDetailColor(gameObject);
            EditorUtility.SetDirty(button);
        }
        else if (gameObject.GetComponent<InputField>())
        {
            InputField input = gameObject.GetComponent<InputField>();
            SetColorBlock(input);
            input.selectionColor = highlighted;
            Undo.RecordObject(input.textComponent, "Change Text color");
            input.textComponent.color = pressed;
            Undo.RecordObject(input.placeholder, "Change Placeholder color");
            input.placeholder.color = highlighted;
            EditorUtility.SetDirty(input);
            EditorUtility.SetDirty(input.textComponent);
            EditorUtility.SetDirty(input.placeholder);
        }
        else if (gameObject.GetComponent<Scrollbar>())
        {
            Scrollbar sb = gameObject.GetComponent<Scrollbar>();
            SetColorBlock(sb);
            Undo.RecordObject(gameObject.GetComponent<Image>(), "Change Image color");
            gameObject.GetComponent<Image>().color = disabled;
            EditorUtility.SetDirty(sb);
            EditorUtility.SetDirty(gameObject.GetComponent<Image>());
        }
        else if (gameObject.GetComponent<Slider>())
        {
            Slider slider = gameObject.GetComponent<Slider>();
            SetColorBlock(slider);
            Undo.RecordObject(slider.fillRect.gameObject.GetComponent<Image>(), "Change Image color");
            slider.fillRect.gameObject.GetComponent<Image>().color = normal;
            SetTextColorRecursive(gameObject);
            EditorUtility.SetDirty(slider);
            EditorUtility.SetDirty(slider.fillRect.gameObject.GetComponent<Image>());
        }
        else if (gameObject.GetComponent<Toggle>())
        {
            Toggle toggle = gameObject.GetComponent<Toggle>();
            SetColorBlock(toggle);
            Undo.RecordObject(toggle.graphic, "Change Image color");
            toggle.graphic.color = normal;
            SetTextColorRecursive(gameObject);
            EditorUtility.SetDirty(toggle);
            EditorUtility.SetDirty(toggle.graphic);
        }
        else if (gameObject.transform.childCount > 0) // Recursive search for components
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                AssignColorsToSelection(gameObject.transform.GetChild(i).gameObject);
            }
        }
        else if (recursiveLevel == 1)
        {
            if (gameObject.GetComponent<Image>())
            {
                Image image = gameObject.GetComponent<Image>();
                Undo.RecordObject(image, "Change color");
                image.color = normal;
                EditorUtility.SetDirty(image);
            }
            else if (gameObject.GetComponent<Text>())
            {
                Text text = gameObject.GetComponent<Text>();
                Undo.RecordObject(text, "Change color");
                text.color = normal;
                EditorUtility.SetDirty(text);
            }
        }
    }

    /// <summary>
    /// Sets all of the current variables to a ColorBlock and returns it
    /// </summary>
    /// <param name="cb">The color block from the UI element</param>
    /// <returns>The color block with the new values</returns>
    public void SetColorBlock(Selectable button)
    {
        Undo.RecordObject(button, "Change ColorBlock");
        ColorBlock cb = button.colors;
        cb.normalColor = normal;
        cb.highlightedColor = highlighted;
        cb.pressedColor = pressed;
        cb.disabledColor = disabled;
        button.colors = cb;
    }

    public void SetColors(Color[] color)
    {
        if (color.Length < 5)
        {
            Debug.LogError("Array too short, the color Array needs to be of length count 5");
            return;
        }
        normal = color[0];
        highlighted = color[1];
        pressed = color[2];
        disabled = color[3];
        detail = color[4];
    }

    /// <summary>
    /// Searches if the GameObject has children and if the children have components type Image or Text.
    /// If they do then it will assign the variable detail to the Color of the image or text.
    /// </summary>
    /// <param name="go">UI GameObject with children</param>
    private void SetDetailColor(GameObject go)
    {
        int children = go.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject child = go.transform.GetChild(i).gameObject;
            if (child.GetComponent<Image>())
            {
                Undo.RecordObject(child.GetComponent<Image>(), "Change Image color");
                child.GetComponent<Image>().color = detail;
                EditorUtility.SetDirty(child.GetComponent<Image>());
            }
            if (child.GetComponent<Text>())
            {
                Text t = child.GetComponent<Text>();
                Undo.RecordObject(t, "Change Text color");
                t.color = detail;
                EditorUtility.SetDirty(t);
            }
        }
    }

    /// <summary>
    /// Looks recursively for component Text in the GameObjects children 
    /// and also changes the color to the Detail variable if it finds any.
    /// </summary>
    /// <param name="go">UI GameObject with children</param>
    private void SetTextColorRecursive(GameObject go)
    {
        int children = go.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            GameObject child = go.transform.GetChild(i).gameObject;
            if (child.GetComponent<Text>())
            {
                Text t = child.GetComponent<Text>();
                Undo.RecordObject(t, "Change Text color");
                t.color = normal;
                EditorUtility.SetDirty(t);
            }
            SetTextColorRecursive(child);
        }
    }
    #endregion

    #endregion

    #region Color suggestion methods
    /// <summary>
    /// Adds all of the color arrays the editor window will suggest.
    /// </summary>
    public void SetUpColors()
    {
        colors = new List<Color[]>();
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(74, 37, 68)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(206, 20, 90)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(141, 39, 137)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(37, 82, 102)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(41, 165, 220)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(126, 209, 232)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(54, 148, 104)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(134, 192, 63)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(211, 218, 33)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(255, 204, 0)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(255, 153, 0)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(255, 173, 67)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(242, 110, 37)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(255, 102, 0)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(239, 106, 65)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(230, 36, 45)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(137, 24, 16)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(239, 101, 101)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(134, 98, 57)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(91, 54, 21)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(192, 150, 109)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(200, 200, 200)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(128, 128, 128)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(51, 51, 51)));
        colors.Add(GetFlatColorDefault(GamestrapHelper.ColorRGBInt(21, 21, 21)));
    }

    /// <summary>
    /// Helper methods to create a color array
    /// </summary>
    /// <param name="baseColor">Base color of what the UI will look like</param>
    /// <returns></returns>
    public static Color[] GetFlatColorDefault(Color baseColor)
    {
        Color highlighted = Color.Lerp(baseColor, Color.white, 0.3f);
        Color pressed = Color.Lerp(baseColor, Color.black, 0.6f);
        Color disabled = GamestrapHelper.ColorRGBInt(224, 224, 224);
        Color detail = Color.white;
        return new Color[] { baseColor, highlighted, pressed, disabled, detail };
    }
    #endregion
}
