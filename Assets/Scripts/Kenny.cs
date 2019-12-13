using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class Kenny : EditorWindow
{
    static Kenny window;
    float windowWidth = 200;
    float windowHeight = 200;
    Texture2D terrainImage;
    int hBlock, vBlock;

    public float duration;

    Rabbits rabbitsJ = new Rabbits();

    [MenuItem("Kenny/Map Editor")]
    static void OpenWindow()
    {
        if (window == null)
        {
            window = (Kenny)GetWindow(typeof(Kenny));
            window.name = "SliderEditor";
            window.wantsMouseEnterLeaveWindow = true;
            window.minSize = new Vector2(window.windowWidth, window.windowHeight);
            window.Show();
        }
        else
        {
            window.Focus();
        }

        window.wantsMouseMove = true;
        EditorApplication.modifierKeysChanged += window.Repaint;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        DrawSizeSetting();
        DrawTerrainsChoosing();
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
                for (int j = 0; j < vBlock; j++)
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
            if (GameObject.FindGameObjectWithTag("KennyTiles") != null)
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
        GUILayout.BeginHorizontal();
        terrainImage = (Texture2D)EditorGUILayout.ObjectField("", terrainImage, typeof(Texture2D), GUILayout.Width(80));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Place"))
        {
            Debug.Log("Place");
        }
        if (GUILayout.Button("Place All"))
        {
            Debug.Log(Resources.Load(terrainImage.name) != null);
            //GameObject tiles = GameObject.FindGameObjectWithTag("KennyTiles");
            //for (int i = 0; i < tiles.transform.childCount; i++)
            //{
            //    tiles.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(terrainImage.name);
            //}
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
}
