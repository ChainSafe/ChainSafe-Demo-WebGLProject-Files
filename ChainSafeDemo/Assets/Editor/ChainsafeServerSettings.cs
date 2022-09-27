using UnityEditor;
using System.Collections;
using UnityEngine;

public class ChainSafeServerSettings : EditorWindow
{
    string ProjectID = "Please Enter Your Project ID";
    string ChainID = "Please Enter Your Chain ID";
    private Texture2D m_Logo = null;
    
    // checks if data is already entered
    void Awake()
    {
        if ((PlayerPrefs.GetString("ProjectID") != ("Please Enter Your Project ID")) && (PlayerPrefs.GetString("ProjectID") != ("")))
        {
        ProjectID = PlayerPrefs.GetString("ProjectID");
        }

        if ((PlayerPrefs.GetString("ChainID") != ("Please Enter Your Chain ID")) && (PlayerPrefs.GetString("ChainID") != ("")))
        {
        ChainID = PlayerPrefs.GetString("ChainID");
        }
    }

    // Initializes window
    [MenuItem("Window/ChainSafeServerSettings")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(ChainSafeServerSettings));
    }

    // called when menu is opened, loads Chainsafe Logo
    void OnEnable()
    {
        m_Logo = (Texture2D)Resources.Load("chainsafemenulogo", typeof(Texture2D));
    }
    
    // displayed content
    void OnGUI()
    {
        // image
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label(m_Logo, GUILayout.MaxWidth(250f), GUILayout.MaxHeight(250f));
        EditorGUILayout.EndVertical();
        // text
        GUILayout.Label ("Welcome To The ChainSafe SDK!", EditorStyles.boldLabel);
        GUILayout.Label ("Here you can enter all the information needed to get your game started on the blockchain!", EditorStyles.label);
        // inputs
        ProjectID = EditorGUILayout.TextField ("Project ID", ProjectID);
        ChainID = EditorGUILayout.TextField ("Chain ID", ChainID);
        // buttons
        if (GUILayout.Button("Check Out Our Docs!"))
        {
            Application.OpenURL("https://docs.gaming.chainsafe.io/");
        }
        if (GUILayout.Button("Save Settings"))
        {
            Debug.Log("Settings Saved!");
            PlayerPrefs.SetString("ProjectID", ProjectID); // API Key
            PlayerPrefs.SetString("ChainID", ChainID); // Chain ID that gets called in the network file
        }
    }
}