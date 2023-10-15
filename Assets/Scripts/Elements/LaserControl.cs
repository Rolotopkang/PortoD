using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private float colorIntensity = 4.3f;
    private float beamColorEnhance = 1;
    [SerializeField] private float LaserCenterOffset = 1f;

    [SerializeField] private float maxLength = 100;
    // [SerializeField] private float thickness = 0.9f;
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;
    [SerializeField] private LayerMask _layerMask;

    private LineRenderer _lineRenderer;
    private bool hitbox;
    private GameObject _convertLaser;
    
    
    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();

        _lineRenderer.material.color = _color * colorIntensity;
        ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            Renderer r = particleSystem.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor",_color * (colorIntensity + beamColorEnhance));
        }
    }

    private void Start()
    {
        UpdateEndPosition();
    }

    private void Update()
    {
        UpdateEndPosition();
    }

    private void UpdateEndPosition()
    {
        Vector2 startPotion = transform.position;
        float rotationZ = transform.rotation.eulerAngles.z;
        rotationZ *= Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(rotationZ), MathF.Sin(rotationZ));

        Vector2 offset = direction.normalized * LaserCenterOffset;
        Vector2 raycastStartPoint = startPotion + offset;
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, direction.normalized,maxLength,_layerMask);
        float length = maxLength;
        float laserEndRotation = 180;
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Box") || hit.collider.gameObject.CompareTag("Portal"))
            {
                    hitbox = true;
            }
            else
            {
                    hitbox = false;
            }
            length = (hit.point - startPotion).magnitude;
            laserEndRotation = Vector2.Angle(direction, hit.normal);

            if (hit.collider.gameObject.CompareTag("Box") || hit.collider.gameObject.CompareTag("LaserReceiver") || hit.collider.gameObject.CompareTag("Portal") )
            {
                _convertLaser = hit.collider.gameObject;
                _convertLaser.transform.GetComponent<ConvertLaser>().Convert(true , hit.point);
            }
            else
            {
                if (_convertLaser != null)
                {
                    _convertLaser.transform.GetComponent<ConvertLaser>().Convert(false , hit.point);
                    _convertLaser = null;
                }
            }
        }
        
        




        _lineRenderer.SetPosition(1,new Vector2(length,0));

        Vector2 endPosition = startPotion + length * direction;
        startVFX.transform.position = startPotion;
        if (hitbox)
        {
            endVFX.transform.gameObject.SetActive(false);
        }
        else
        {
            endVFX.transform.gameObject.SetActive(true);
            endVFX.transform.position = endPosition;
            endVFX.transform.rotation = quaternion.Euler(0,0,laserEndRotation);
        }

    }

    private void UpdatePosition(Vector2 startPosition, Vector2 direction)
    {
        direction = direction.normalized;
        transform.position = startPosition;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation =Quaternion.Euler(0,0,rotationZ * Mathf.Rad2Deg);
    }
    
}
