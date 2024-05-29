using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _separationChance = 1f;

    public event Action<Cube> Clicked;

    public float SeparationChance => _separationChance;

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }

    public void SetSeparationChance(float separationChance)
    {
        _separationChance = separationChance;
    }

    public void SerColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void AddForse(Vector3 force)
    {
        GetComponent<Rigidbody>().AddForce(force);
    }

    public void AddExplosionForce(float explosionForce, float explosionRadious, Vector3 explodePosition)
    {
        GetComponent<Rigidbody>().AddExplosionForce(explosionForce / _separationChance, explodePosition, explosionRadious / _separationChance);
    }
}