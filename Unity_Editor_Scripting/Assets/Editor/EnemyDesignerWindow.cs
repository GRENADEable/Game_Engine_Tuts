using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{
    #region Public Variables

    #region Datas
    public static MageData MageInfo { get { return _mageData; } }
    public static RogueData RogueInfo { get { return _rogueData; } }
    public static WarriorData WarriorInfo { get { return _warriorData; } }
    #endregion

    #endregion

    #region Private Variables

    #region Textures
    private Texture2D _headerSectionTex;
    private Texture2D _mageSectionTex;
    private Texture2D _rogueSectionTex;
    private Texture2D _warriorSectionTex;
    #endregion

    private Color _headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    #region Rects
    private Rect _headerSection;
    private Rect _mageSection;
    private Rect _rogueSection;
    private Rect _warriorSection;
    #endregion

    #region Datas
    private static MageData _mageData;
    private static RogueData _rogueData;
    private static WarriorData _warriorData;
    #endregion

    #endregion

    #region Unity Callbacks
    void OnEnable()
    {
        InitTex();
        InitData();
    }

    /// <summary>
    /// Similar to any Update Function.
    /// Not once per frame. Called 1 or more times per interaction.
    /// </summary>
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawRogueSettings();
        DrawWarriorSettings();
    }
    #endregion

    #region My Functions

    /// <summary>
    /// Intialise the Scriptable Objects
    /// </summary>
    public static void InitData()
    {
        _mageData = (MageData)CreateInstance(typeof(MageData));
        _rogueData = (RogueData)CreateInstance(typeof(RogueData));
        _warriorData = (WarriorData)CreateInstance(typeof(WarriorData));
    }

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600f, 300f);
        window.maxSize = new Vector2(1200f, 600);
        window.Show();
    }

    /// <summary>
    /// Intialise Texture2D values.
    /// </summary>
    void InitTex()
    {
        _headerSectionTex = new Texture2D(1, 1);
        _headerSectionTex.SetPixel(0, 0, _headerSectionColor);
        _headerSectionTex.Apply();

        _mageSectionTex = Resources.Load<Texture2D>("Icons/Tex_Mage_Editor");
        _rogueSectionTex = Resources.Load<Texture2D>("Icons/Tex_Rogue_Editor");
        _warriorSectionTex = Resources.Load<Texture2D>("Icons/Tex_Warrior_Editor");
    }

    /// <summary>
    /// Defines Rect values and paints textures based on Rects
    /// </summary>
    void DrawLayouts()
    {
        // Header Section
        _headerSection.x = 0f;
        _headerSection.y = 0f;
        _headerSection.width = Screen.width;
        _headerSection.height = 50f;

        // Mage Section
        _mageSection.x = 0f;
        _mageSection.y = 50f;
        _mageSection.width = position.width / 3f;
        _mageSection.height = position.height - 50f;

        // Rogue Section
        _rogueSection.x = position.width / 3f;
        _rogueSection.y = 50f;
        _rogueSection.width = position.width / 3f;
        _rogueSection.height = position.height;

        // Warrior Section
        _warriorSection.x = (position.width / 3f) * 2f;
        _warriorSection.y = 50f;
        _warriorSection.width = position.width / 3f;
        _warriorSection.height = position.height;

        // Drawing the Textures
        GUI.DrawTexture(_headerSection, _headerSectionTex);
        GUI.DrawTexture(_mageSection, _mageSectionTex);
        GUI.DrawTexture(_rogueSection, _rogueSectionTex);
        GUI.DrawTexture(_warriorSection, _warriorSectionTex);
    }

    /// <summary>
    /// Draw contents for header
    /// </summary>
    void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);

        GUILayout.Label("Enemy Designer");

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents for mage region
    /// </summary>
    void DrawMageSettings()
    {
        GUILayout.BeginArea(_mageSection);

        GUILayout.Label("Mage");

        _mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup("Damage Type:", _mageData.dmgType);
        _mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup("Weapon:", _mageData.wpnType);

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents for rogue region
    /// </summary>
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(_rogueSection);

        GUILayout.Label("Rogue");

        _rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup("Damage Type:", _rogueData.wpnType);
        _rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup("Strategy Type:", _rogueData.strategyType);

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents for warrior region
    /// </summary>
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(_warriorSection);

        GUILayout.Label("Warrior");

        _warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup("Calss Type:", _warriorData.classType);
        _warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup("Weapon Type:", _warriorData.wpnType);

        GUILayout.EndArea();
    }
    #endregion
}