using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Polymorphism: Implements IScorable so it can be handled generically
public class Brick : MonoBehaviour, IScorable
{
    public UnityEvent<Brick> onDestroyed;
    public int PointValue;

    void Start()
    {
        ApplyColorByPoints();
    }

    // IScorable implementation
    public int GetPoints() => PointValue;

    private void OnCollisionEnter(Collision other)
    {
        // Notify listeners (MainManager) with a reference to this brick
        onDestroyed.Invoke(this);

        // Disable after a short delay to allow for ball physics response
        StartCoroutine(DisableAfterDelay(0.2f));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Object pooling friendly
    }

    private void ApplyColorByPoints()
    {
        var renderer = GetComponentInChildren<Renderer>();
        if (renderer == null) return;

        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", PointValue switch
        {
            1 => Color.green,
            2 => Color.yellow,
            5 => Color.blue,
            _ => Color.red
        });

        renderer.SetPropertyBlock(block);
    }
}

