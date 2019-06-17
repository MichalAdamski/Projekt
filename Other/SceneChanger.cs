using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public static void ChangeToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
