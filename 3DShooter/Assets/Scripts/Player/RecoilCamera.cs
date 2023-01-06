using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilCamera : MonoBehaviour
{
    [Space, Header("Recoil")]
    [SerializeField] private float rotationSpeed = 6f;
    [SerializeField] private float returnSpeed = 25f;

    [Space, Header("Hipfire")]
    [SerializeField] private Vector3 recoilRotation = new Vector3(2f, 2f, 2f);

    [Space, Header("Scoping")]
    [SerializeField] private Vector3 recoilRotationScoping = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private bool isScoping = false;

    private Vector3 currentRotation = new();
    private Vector3 rotation = new();

    private void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rotation = Vector3.Slerp(rotation, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void Init(RecoilActions recoilActions)
    {
        recoilActions.onRecoil += Fire;
    }

    private void Fire()
    {
        if (isScoping)
        {
            currentRotation += new Vector3(-recoilRotationScoping.x, Random.Range(-recoilRotationScoping.y, recoilRotationScoping.y), Random.Range(-recoilRotationScoping.z, recoilRotationScoping.z));
        }
        else
        {
            currentRotation += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
        }
    }
}
