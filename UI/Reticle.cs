using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    [SerializeField] private Image rectileImage;
    [SerializeField] private Transform rectileTransform;

    public bool IsDisplayed { get { return rectileImage.enabled; } }

    public void Hide()
    {
        rectileImage.enabled = false;
    }

    public void Show()
    {
        rectileImage.enabled = true;
    }

    public void SetPosition(Vector3 position, Vector3 normal)
    {
        rectileTransform.position = position;
        rectileTransform.rotation = Quaternion.FromToRotation(Vector3.forward, normal);
    }
}
