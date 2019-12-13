using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class KennyEditor : EditorWindow
{
    static KennyEditor window;
    float windowWidth = 200;
    float windowHeight = 200;
    Texture2D terrainImage;
    int hBlock, vBlock;

    public float duration;

    Rabbits rabbitsJ = new Rabbits();

    [MenuItem("Kenny/Map Editor")]
    static void OpenWindow()
    {
        window = (KennyEditor)GetWindow(typeof(KennyEditor));
        window.name = "SliderEditor";
        window.wantsMouseEnterLeaveWindow = true;
        window.minSize = new Vector2(window.windowWidth, window.windowHeight);
        window.Show();
    }
    //private void OnEnable()
    //{
    //    SunsetMusicImporter.OnLoaded += SunsetMusicImporter_OnLoaded;
    //}
    //private void OnDisable()
    //{
    //    SunsetMusicImporter.OnLoaded -= SunsetMusicImporter_OnLoaded;
    //}
    //private void SunsetMusicImporter_OnLoaded()
    //{
    //    var property = SunsetSaveSystem.saveOb.gameProperty;
    //    switch (property.level)
    //    {
    //        case "easy":
    //            difficulty = Difficult.EASY;
    //            break;
    //        case "normal":
    //            difficulty = Difficult.NORMAL;
    //            break;
    //        case "hard":
    //            difficulty = Difficult.HARD;
    //            break;
    //    }
    //    Color newColor;
    //    buttonSkinType = (property.buttonType == "color") ? SkinType.Color : SkinType.Texture;
    //    if (property.buttonType == "color")
    //    {
    //        if (ColorUtility.TryParseHtmlString(property.buttonValue, out newColor))
    //        {
    //            buttonColor = newColor;
    //        }
    //        buttonImage = null;
    //    }
    //    else
    //    {
    //        buttonImage = Resources.Load<Texture2D>("sprite/" + property.buttonValue);
    //        buttonColor = Color.black;
    //    }
    //    laneSkinType = (property.laneType == "color") ? SkinType.Color : SkinType.Texture;
    //    if (property.laneType == "color")
    //    {
    //        if (ColorUtility.TryParseHtmlString(property.laneValue, out newColor)) laneColor = newColor;
    //        laneImage = null;
    //    }
    //    else
    //    {
    //        laneImage = Resources.Load<Texture2D>("sprite/" + property.laneValue);
    //        laneColor = Color.black;
    //    }
    //    noteSkinType = (property.noteType == "color") ? SkinType.Color : SkinType.Texture;
    //    if (property.noteType == "color")
    //    {
    //        if (ColorUtility.TryParseHtmlString(property.noteVale, out newColor)) noteColor = newColor;
    //        laneImage = null;
    //    }
    //    else
    //    {
    //        noteImage = Resources.Load<Texture2D>("sprite/" + property.noteVale);
    //        noteColor = Color.black;
    //    }
    //    Repaint();
    //}

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        DrawSizeSetting();
        DrawTerrainsChoosing();
        DrawPlayerViewConsole();
        DrawTapConsole();
        DrawMusicSpeed();
        DrawSave();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
    void DrawSizeSetting()
    {
        EditorGUILayout.LabelField("Map Size Setting", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("block number(Horizontal) : ", GUILayout.Width(150));
        hBlock = EditorGUILayout.IntField(hBlock, GUILayout.Width(100));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("block number(Vertical) : ", GUILayout.Width(150));
        vBlock = EditorGUILayout.IntField(vBlock, GUILayout.Width(100));
        if (GUILayout.Button("Set", GUILayout.Width(50)))
        {
            GameObject kennyMap = new GameObject();
            kennyMap.name = "KennyTiles";
            kennyMap.tag = "KennyTiles";
            for (int i = 0; i < hBlock; i++)
            {
                for(int j = 0; j < vBlock; j++)
                {
                    GameObject kBlock = Instantiate(GameObject.FindGameObjectWithTag("KennySquare"));
                    kBlock.transform.position = new Vector2(i, j);
                    kBlock.GetComponent<SpriteRenderer>().enabled = true;
                    kBlock.transform.parent = kennyMap.transform;
                }
            }
        }
        if (GUILayout.Button("Clear All", GUILayout.Width(100)))
        {
            if(GameObject.FindGameObjectWithTag("KennyTiles") != null)
            {
                DestroyImmediate(GameObject.FindGameObjectWithTag("KennyTiles"));
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("* 1 block = 100x100 pixel", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    void DrawTerrainsChoosing()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Terrains Creator", EditorStyles.boldLabel);
        //GUILayout.BeginHorizontal();
        //difficulty = (Difficult)EditorGUILayout.EnumPopup("difficulty level :", difficulty, GUILayout.Width(300));
        //GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        terrainImage = (Texture2D)EditorGUILayout.ObjectField("", terrainImage, typeof(Texture2D), GUILayout.Width(80));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Place All"))
        {
            Debug.Log("Place All");
        }
        GUILayout.EndHorizontal();
    }
    void DrawPlayerViewConsole()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Editor view", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Topview"))
        {
            Debug.Log("top view");
        }
        if (GUILayout.Button("Playerview"))
        {
            Debug.Log("player view");
        }
        GUILayout.EndHorizontal();
    }
    void DrawMusicSpeed()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("0.25x"))
        {
            Debug.Log("0.25x");
        }
        if (GUILayout.Button("0.5x"))
        {
            Debug.Log("0.5x");
        }
        if (GUILayout.Button("1x"))
        {
            Debug.Log("1x");
        }
        if (GUILayout.Button("2x"))
        {
            Debug.Log("2x");
        }
        GUILayout.EndHorizontal();
    }
    void DrawTapConsole()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Note Creator/Setter", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Tap"))
        {
            Debug.Log("tap");
        }
        if (GUILayout.Button("Slide"))
        {
            Debug.Log("slide");
        }
        if (GUILayout.Button("<-"))
        {
            Debug.Log("<-");
        }
        if (GUILayout.Button("->"))
        {
            Debug.Log("->");
        }
        if (GUILayout.Button("Close Lane"))
        {
            Debug.Log("close lane");
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Delete"))
        {
            Debug.Log("delete");
        }
        if (GUILayout.Button("Drag"))
        {
            Debug.Log("drag");
        }
        if (GUILayout.Button("Select path"))
        {
            Debug.Log("select path");
        }
        GUILayout.EndHorizontal();
    }
    void DrawSave()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Export to JSON", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Save"))
        //{
        //    SunsetSaveSystem.Init();
        //    SunsetSaveSystem.SetNameLevelAndLane(AudioTrackManager.Instance.audioSource.clip.name, difficulty.ToString());
        //    SunsetSaveSystem.SetNoteList(noteCreator.tapNotes, noteCreator.swipeNotes, noteCreator.slideNotes, noteCreator.closeLaneNotes);
        //    SunsetSaveSystem.SetPlaneList(noteCreator.slideNotes);
        //    SunsetSaveSystem.SetGrid(GridData.Instance.GetGridInfo());
        //    SunsetSaveSystem.SetMusicInfo(AudioTrackManager.Instance.GetAudioTrackInfo());
        //    SunsetSaveSystem.SetGameProperty(GetGameProperty());
        //    SunsetSaveSystem.Save(JsonUtility.ToJson(SunsetSaveSystem.saveOb));
        //}
        if (GUILayout.Button("Rabby Save"))
        {
            string line = JsonUtility.ToJson(rabbitsJ.GetRabbitsInfo());
            File.WriteAllText(@"./Assets/test.json", line);
        }
        if (GUILayout.Button("Add Rabby"))
        {
            rabbitsJ.addRabbit();
        }
        GUILayout.EndHorizontal();
    }
    //private void Update()
    //{
    //    if (trackManager == null) return;
    //    if (trackManager.audioSource == null) return;
    //    if (trackManager.audioSource.isPlaying)
    //    {
    //        track = trackManager.currentTime;
    //        Repaint();
    //    }
    //    else
    //    {
    //        trackManager.currentTime = track;
    //        trackManager.audioSource.time = track;
    //    }

    //}
    void SelectGameView()
    {
        var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var gvWnd = EditorWindow.GetWindow(gvWndType);
    }
}
