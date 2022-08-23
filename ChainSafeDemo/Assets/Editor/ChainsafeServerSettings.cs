using UnityEditor;
using UnityEngine;

public class ChainsafeServerSettings : EditorWindow
{
    // variables
    string APIKey = "Enter Your API Key";
    string GameName = "Enter Your Game Name";
    string ChainID = "Enter Chain ID";
    private Texture2D m_Logo = null;

    // initializes window
    [MenuItem("Window/Chainsafe Server Settings")]
    public static void ShowWindow()
    {
        GetWindow<ChainsafeServerSettings>("Chainsafe Server Settings");
    }

    // called when menu is opened, loads the chainsafe logo
    void OnEnable()
    {
        m_Logo = (Texture2D)Resources.Load("chainsafemenulogo",typeof(Texture2D));
    }

    // called when menu is closed, makes the post call
    void OnDisable()
    {
        SendStatsToServer();
    }

    // displayed content
    void OnGUI()
    {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label(m_Logo, GUILayout.MaxWidth(250f), GUILayout.MaxHeight(250f));
        EditorGUILayout.EndVertical();
        GUILayout.Label("Welcome To The ChainSafe SDK!", EditorStyles.boldLabel);
        GUILayout.Label("Here you can enter all the information needed to get your game started on the blockchain!");
        APIKey = EditorGUILayout.TextField("API Key:", APIKey);
        GameName = EditorGUILayout.TextField("Game Name:", GameName);
        ChainID = EditorGUILayout.TextField("Chain ID:", ChainID);
        if (GUILayout.Button("Check Out Our Docs!"))
        {
            Application.OpenURL("https://docs.gaming.chainsafe.io/");
        }
    }

    // post call to server
    void SendStatsToServer()
    {

    }
}