using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _spreadingForse;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadious;

    private void OnEnable()
    {
        _cubeSpawner.CubesCreated += Detonate;
        _cubeSpawner.CubeDisappearing += Detonate;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubesCreated -= Detonate;
        _cubeSpawner.CubeDisappearing -= Detonate;

    }

    private void Detonate(List<Cube> cubes)
    {
        Vector3 force;

        foreach (Cube cube in cubes)
        {
            force = new Vector3(Random.value, Random.value, Random.value) * _spreadingForse;
            cube.AddForse(force);
        }
    }

    private void Detonate(Cube cube)
    {
        Vector3 explodePosition = cube.transform.position;
        List<Cube> explodableObjects = GetExplodableObjects(explodePosition);

        foreach (var explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce, _explosionRadious, explodePosition);
    }

    private List<Cube> GetExplodableObjects(Vector3 explodePosition)
    {
        List<Cube> explodableObjects = new List<Cube>();

        Collider[] hits = Physics.OverlapSphere(explodePosition, _explosionRadious);

        foreach (Collider hit in hits)
            if (hit.TryGetComponent<Cube>(out Cube cub))
                explodableObjects.Add(cub);

        return explodableObjects;
    }
}