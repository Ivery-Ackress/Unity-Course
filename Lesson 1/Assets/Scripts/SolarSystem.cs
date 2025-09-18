using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    private SolarSystem solarSystem;

    [Serializable]
    private class Orbit
    {
        [SerializeField] private float orbitRadius;
        [SerializeField] private Vector3 orbitRotation;
        [SerializeField] private GameObject planet;
        [SerializeField] private Vector3 planetRotation;
        [SerializeField] private float rotationRate;
        [SerializeField] private float planetSpeed;
        
        public float OrbitRadius => orbitRadius;
        public Vector3 OrbitRotation => orbitRotation;
        public GameObject Planet => planet;
        public Vector3 PlanetRotation => planetRotation;
        public float RotationRate => rotationRate;
        public float PlanetSpeed => planetSpeed;
    }

    [SerializeField] private bool isFixedUpdate;
    [SerializeField] private List<Orbit> orbits;

    public bool IsFixedUpdate => isFixedUpdate;
    private float timer;

    private void Awake()
    {
        solarSystem = GetComponent<SolarSystem>();
        timer = 0;
    }
    
    private void Update()
    {
        if (solarSystem.IsFixedUpdate)
            return;
        timer += Time.deltaTime;
        UpdateOrbit();
    }

    private void FixedUpdate()
    {
        if (!solarSystem.IsFixedUpdate)
            return;
        timer += Time.deltaTime;
        UpdateOrbit();
    }

    private void UpdateOrbit()
    {
        foreach (var orbit in orbits)
        {
            var nowPosition = solarSystem.transform.position;

            var newXPosition = nowPosition.x
                + orbit.OrbitRadius * (-Math.Cos(orbit.PlanetSpeed * timer) * Math.Cos(orbit.OrbitRotation.x) * Math.Cos(orbit.OrbitRotation.y)
                + Math.Sin(orbit.PlanetSpeed * timer) * Math.Sin(orbit.OrbitRotation.y));
            var newYPosition = nowPosition.y
                + orbit.OrbitRadius * (-Math.Cos(orbit.PlanetSpeed * timer) * Math.Cos(orbit.OrbitRotation.x) * Math.Sin(orbit.OrbitRotation.y)
                - Math.Sin(orbit.PlanetSpeed * timer) * Math.Cos(orbit.OrbitRotation.y));
            var newZPosition = nowPosition.z
                + orbit.OrbitRadius * (Math.Cos(orbit.PlanetSpeed * timer) * Math.Sin(orbit.OrbitRotation.x));

            orbit.Planet.transform.position = new((float)newXPosition, (float)newYPosition, (float)newZPosition);
            orbit.Planet.transform.Rotate(orbit.PlanetRotation * orbit.RotationRate);
        }
    }
}
