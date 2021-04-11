using UnityEditor;
using UnityEngine;

public class JennyEditor : EditorWindow
{
    private const float width = 315;
    private const float height = 550;

    private static JennyEditor editorWindow = default;

    private GameObject container = default;
    private GameObject go = default;
    private int amount = default;
    private bool vertical = default;
    private bool horizontal = default;

    [MenuItem("JennyEditor/Editor_1")]
    private static void OpenWindow()
    {
        if (editorWindow == null)
        {
            editorWindow = GetWindow<JennyEditor>();
            editorWindow.minSize = new Vector2(width, height);
            editorWindow.Show();
        }
        else
        {
            editorWindow.Focus();
        }
    }

    private void OnGUI()
    {
        ObjectGenerater();
    }

    private void ObjectGenerater()
    {
        EditorGUILayout.LabelField("Object Generator", EditorStyles.boldLabel);

        // Select object
        GUILayout.BeginHorizontal();
        GUILayout.Label("Object", GUILayout.Width(50));
        go = (GameObject)EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        // Setting
        GUILayout.BeginHorizontal();
        GUILayout.Label("Amount", GUILayout.Width(50));
        amount = EditorGUILayout.IntField(amount);
        vertical = GUILayout.Toggle(!horizontal, "vertical");
        horizontal = GUILayout.Toggle(!vertical, "horizontal");
        GUILayout.EndHorizontal();

        // Generate
        if (GUILayout.Button("Generate"))
        {
            if (container == null)
            {
                container = new GameObject("GO Container");
            }

            for (int i = 0; i < amount; i++)
            {
                GameObject newObject = Instantiate(go, container.transform);

                if (vertical)
                {
                    newObject.transform.position = new Vector2(0, i);
                }
                else
                {
                    newObject.transform.position = new Vector2(i, 0);
                }
            }
        }

        // Delete
        if (GUILayout.Button("Clear"))
        {
            DestroyImmediate(container);
        }
    }
}