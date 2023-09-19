using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using RGVA;
using DG.Tweening;
using UnityEngine.PlayerLoop;

public class Shooting : Singleton<Shooting>
{
    public float gunRange;
    public float bulletSpeed;
    public float dragSensitivity;
    public float crosshairSense;

    public GameObject gunModel;


    public GameObject aimCube;
    public Transform gunBarrel;
    public Camera cam;
    public RectTransform crosshair;

    private WaitForSeconds chDuration;


    public Vector3 chPos;

    public bool keepShooting = false;


    [Button]
    private void InitializeVariables()
    {
        gunBarrel = transform.GetChild(0).transform;
        cam = FindObjectOfType<Camera>();
    }
    private void OnEnable()
    {
        InputManager.OnInputDown += Aim;
        InputManager.OnInputUp += StopShooting;
        InputManager.OnDrag += MoveCrosshair;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        InputManager.OnInputDown -= Aim;
        InputManager.OnInputUp -= StopShooting;
        InputManager.OnDrag -= MoveCrosshair;
    }

    private void FixedUpdate()
    {
        MousePosition3D.Instance.MoveTarget();
    }

    private void Update()
    {
        gunBarrel.parent.transform.LookAt(aimCube.transform.position);


        if (!(GameEvents.Instance.cooldownCircle.fillAmount < 1f) && keepShooting)
        {
            Shoot();

        }
    }


    void StopShooting()
    {
        keepShooting = false;

    }



    void Shoot()
    {
        AudioManager.Instance.Play("GunShot");

        PoolManager.Instance.Dequeue(ePoolType.MuzzleFlash, gunBarrel.position);

        Recoil();

        GameEvents.Instance.cooldownCircle.fillAmount = 0;
        GameObject bullet = PoolManager.Instance.Dequeue(ePoolType.Bullet, gunBarrel.transform.position);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();


        rb.velocity = transform.TransformDirection(0, 0, bulletSpeed);


    }

    [SerializeField] Vector3 m_recoilZone;
    [SerializeField] Vector3 m_recoilBase;
    [SerializeField] Vector3 m_recoilRotate;
    [SerializeField] Vector3 m_recoilRotateBase;
    void Recoil()
    {
        Sequence recoil = DOTween.Sequence();
        recoil.Append(gunModel.transform.DOLocalMove(m_recoilZone, 0f));
        recoil.Append(gunModel.transform.DOLocalRotate(m_recoilRotate, 0f));
        recoil.Append(gunModel.transform.DOLocalRotate(m_recoilRotateBase, 0.2f, RotateMode.FastBeyond360));

        recoil.Append(gunModel.transform.DOLocalMove(m_recoilBase, 0.2f));
    }

    void Aim(Vector2 i_points)
    {
        keepShooting = true;

        crosshair.position = new Vector3(i_points.x, i_points.y, 0f);
        chPos = crosshair.position;
        MousePosition3D.Instance.MoveTarget();

        gunBarrel.parent.transform.LookAt(aimCube.transform.position);






    }

    
    private void MoveCrosshair(Vector2 i_points)
    {
        crosshair.position = new Vector3(chPos.x + i_points.x, chPos.y + i_points.y, 0f);
    }



}
