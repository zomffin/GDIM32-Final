using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Transform clockHandTransform;
    [SerializeField] private float rotationSpeed = 180f; // degrees per second

    private bool isActive = false;

    private void Update()
    {
        if (!isActive) return;

        clockHandTransform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    public void SetActive(bool value)
    {
        isActive = value;
    }
}