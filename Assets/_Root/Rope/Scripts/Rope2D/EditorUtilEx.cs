#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class EditorUtilEx
{
    /// <summary>
    /// Draw a large separator with optional section header
    /// </summary>
    public static bool SubSection(
        string header = null)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", new GUIStyle(GUI.skin.box) {fixedHeight = 1, stretchWidth = true}, GUILayout.Height(1));
        //EditorGUILayout.Space();

        if (header != null)
            EditorGUILayout.LabelField(header,
                new GUIStyle(EditorStyles.largeLabel) {alignment = TextAnchor.MiddleCenter, fontSize = 15, richText = true},
                GUILayout.Height(18));

        bool clicked = GUI.Button(GUILayoutUtility.GetLastRect(), GUIContent.none, GUIStyle.none);
        return clicked;
    }


    /// <summary>
    /// Draw a large separator with optional section header
    /// </summary>
    public static bool Section(
        string header = null)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();

        if (header != null)
            EditorGUILayout.LabelField(header,
                new GUIStyle(EditorStyles.largeLabel) {alignment = TextAnchor.MiddleCenter, fontSize = 20, richText = true},
                GUILayout.Height(30));

        bool clicked = GUI.Button(GUILayoutUtility.GetLastRect(), GUIContent.none, GUIStyle.none);
        return clicked;
    }

    /// <summary>
    /// Disable groups
    /// </summary>
    public static void DisabledSection(
        System.Action onGUI = null,
        System.Func<bool> isDisabled = null)
    {
        EditorGUI.BeginDisabledGroup(isDisabled?.Invoke() ?? true);
        onGUI?.Invoke();
        EditorGUI.EndDisabledGroup();
    }

    /// <summary>
    /// Draw a boxed section for all ...GUI... calls in the callback
    /// </summary>
    public static bool MiniBoxedSection(
        string header = null,
        System.Action onGUI = null)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if (header != null)
            EditorGUILayout.LabelField(header,
                new GUIStyle(EditorStyles.largeLabel) {alignment = TextAnchor.MiddleCenter, fontSize = 14, richText = true},
                GUILayout.Height(20));
        bool clicked = GUI.Button(GUILayoutUtility.GetLastRect(), GUIContent.none, GUIStyle.none);
        onGUI?.Invoke();
        EditorGUILayout.EndVertical();
        return clicked;
    }

    private static GUIStyle myButtonStyle;

    private static GUIStyle MyButtonStyle => myButtonStyle ?? (myButtonStyle = new GUIStyle(EditorStyles.toolbarButton)
    {
        fontSize = 11,
        font = EditorStyles.label.font,
    });
    
    public static bool MyButton(string title, Color? color = null, params GUILayoutOption[] options)
    {
        return MyButton(new GUIContent(title), color, options);
    }
    
    public static bool MyButton(GUIContent guiContent, Color? color = null, params GUILayoutOption[] options)
    {
        var c = GUI.color;
        GUI.color = color ?? c;
        var b = GUILayout.Button(guiContent, MyButtonStyle, options);
        GUI.color = c;
        return b;
    }
    
    public static Rect GetInnerGuiPosition(
        this SceneView sceneView)
    {
        var position = sceneView.position;
        position.x = position.y = 0;
        position.height -= EditorStyles.toolbar.fixedHeight;
        return position;
    }
}
#endif