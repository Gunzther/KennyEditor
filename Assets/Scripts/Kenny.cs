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
    Texture2D terrainImage, enviImage;
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
        DrawEnviChoosing();
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

            GameObject kennyEnviPlace = new GameObject();
            kennyEnviPlace.name = "KennyEnvironments";
            kennyEnviPlace.tag = "KennyEnvironments";

            for (int i = 0; i < hBlock; i++)
            {
                for (int j = 0; j < vBlock; j++)
                {
                    GameObject kBlock = Instantiate(GameObject.FindGameObjectWithTag("KennySquare"));
                    GameObject kBlock2 = Instantiate(GameObject.FindGameObjectWithTag("KennySquare"));

                    kBlock.transform.position = new Vector2(i+0.5f, j+0.5f);
                    kBlock2.transform.position = new Vector2(i + 0.5f, j + 0.5f);

                    SpriteRenderer srBlock2 = kBlock2.GetComponent<SpriteRenderer>();
                    srBlock2.sprite = null;
                    srBlock2.sortingLayerName = "1";

                    kBlock.GetComponent<SpriteRenderer>().enabled = true;
                    srBlock2.enabled = true;

                    kBlock.transform.parent = kennyMap.transform;
                    kBlock2.transform.parent = kennyEnviPlace.transform;
                }
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Delete All"))
        {
            if (GameObject.FindGameObjectWithTag("KennyTiles") != null)
            {
                DestroyImmediate(GameObject.FindGameObjectWithTag("KennyTiles"));
            }
            if (GameObject.FindGameObjectWithTag("KennyEnvironments") != null)
            {
                DestroyImmediate(GameObject.FindGameObjectWithTag("KennyEnvironments"));
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
            Sprite terrain = Resources.Load<Sprite>("Terrains/" + terrainImage.name);
            if(terrain != null)
            {
                GameObject tiles = GameObject.FindGameObjectWithTag("KennyTiles");
                if (tiles != null)
                {
                    for (int i = 0; i < tiles.transform.childCount; i++)
                    {
                        tiles.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sprite = terrain;
                    }
                }
                else
                {
                    Debug.Log("Map hasn't generated.");
                }
            }
        }
        if (GUILayout.Button("Reset All"))
        {
            Sprite terrain = Resources.Load<Sprite>("Terrains/grid");
            if (terrain != null)
            {
                GameObject tiles = GameObject.FindGameObjectWithTag("KennyTiles");
                if (tiles != null)
                {
                    for (int i = 0; i < tiles.transform.childCount; i++)
                    {
                        tiles.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sprite = terrain;
                    }
                }
            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawEnviChoosing()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Environments Creator", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        enviImage = (Texture2D)EditorGUILayout.ObjectField("", enviImage, typeof(Texture2D), GUILayout.Width(80));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Place"))
        {
            Debug.Log("Place");
        }
        if (GUILayout.Button("Place All"))
        {
            Sprite envi = Resources.Load<Sprite>("Environments/" + enviImage.name);
            if (envi != null)
            {
                GameObject tiles = GameObject.FindGameObjectWithTag("KennyEnvironments");
                if (tiles != null)
                {
                    for (int i = 0; i < tiles.transform.childCount; i++)
                    {
                        tiles.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sprite = envi;
                    }
                }
                else
                {
                    Debug.Log("Map hasn't generated.");
                }
            }
        }
        if (GUILayout.Button("Reset All"))
        {
            GameObject tiles = GameObject.FindGameObjectWithTag("KennyEnvironments");
            if (tiles != null)
            {
                for (int i = 0; i < tiles.transform.childCount; i++)
                {
                    tiles.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sprite = null;
                }
            }
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
