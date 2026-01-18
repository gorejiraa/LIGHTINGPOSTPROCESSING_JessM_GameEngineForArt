using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneToggle : MonoBehaviour
{
    private static SceneToggle instance;

    public TextMeshProUGUI hintText;

    // Set these in the Inspector
    public Color dayColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);   // dark for bright day
    public Color nightColor = new Color(0.9f, 0.9f, 0.9f, 0.8f); // light for dark night

    void Awake()
    {
        // Prevent duplicates when switching scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateTextColor();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            string current = SceneManager.GetActiveScene().name;

            if (current == "Day")
                SceneManager.LoadScene("Night");
            else if (current == "Night")
                SceneManager.LoadScene("Day");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateTextColor();
    }

    void UpdateTextColor()
    {
        if (!hintText) return;

        string current = SceneManager.GetActiveScene().name;

        if (current == "Day")
            hintText.color = dayColor;
        else if (current == "Night")
            hintText.color = nightColor;
    }
}
