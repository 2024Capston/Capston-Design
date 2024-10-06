using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Burst.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChasingEnemy : MonoBehaviour
{
    // 이동 속력, 회전 속력, 점프력
    [SerializeField] private float _walkSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 2f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;
    private MeshRenderer _meshRenderer;
    private Renderer _objectRenderer;
    private bool _isStealth = false;

    [SerializeField]
    private Transform _targetTransform;

    // 플레이어 조작에 쓰이는 보조 변수
    private Vector3 _pastPosition;
    private float _pitchAngle;
    
    private void Awake()
    {
        
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _objectRenderer = GetComponent<Renderer>();
        StartCoroutine(ChooseTarget());
    }
    private void Update()
    {
        if (_targetTransform == null) 
        {
            _rigidbody.velocity = new Vector3(0,0,0);
            return;
        }
        // 이동
        Vector3 moveDir = (_targetTransform.position - transform.position).normalized * _walkSpeed;
        _rigidbody.velocity = new Vector3(moveDir.x, _rigidbody.velocity.y, moveDir.z);

    }
    IEnumerator ChooseTarget()
    {
        while(true) 
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
            int _stopCount=0;
            foreach (GameObject go in players)
            {
                PlayerController playerController = go.GetComponent<PlayerController>();
                Debug.Log($"{go.name}");
                if (playerController._isStop.Value)
                {
                    _stopCount+=1;
                }
                else
                {
                    _targetTransform = playerController.transform;
                }
            }
                Debug.Log($"{_stopCount}");
            if (_stopCount>=2)
            {
                Transform closestTarget=null;
                float maxDistance = Mathf.Infinity;
                foreach (GameObject go in players)
                {
                    // PlayerController playerController = go.GetComponent<PlayerController>();
                    float targetDistance = Vector3.Distance(transform.position, go.transform.position);
                    
                    if (targetDistance<maxDistance)
                    {
                        closestTarget = go.transform;
                        maxDistance = targetDistance;
                    }
                }
                _targetTransform = closestTarget.transform;
            }
            yield return new WaitForSeconds(1);
        }
        
        // Transform _plocarr = MultiplayerManager.Instance.LocalPlayer.transform
        // _gma = MultiplayerManager.Instance.LocalPlayer.PlayerColor.Value

        // _targetTransform = 
    }


    public void OnStealth()
    {
        _isStealth = true;
        _objectRenderer.enabled = _isStealth;
    }
    public void OffStealth()
    {
        _isStealth = false;
        _objectRenderer.enabled = _isStealth;
    }
}
