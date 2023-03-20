using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour
{
    private InputHandler _input;
    private GameObject _leader;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool rotateTowardsMouse;

    [SerializeField]
    private Camera camera;

    
    private void Start()
    {
        _input = GetComponent<InputHandler>();
        _leader = GetComponent<Squad>().GetLeader().gameObject;
        Debug.Log(_leader);
    }
    
    
    //Update is called once per frame
    private void Update()
    {
        _leader = GetComponent<Squad>().GetLeader().gameObject;
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        //Move in the direction we are aiming 
        var movementVector = MoveTowardTarget(targetVector);
        if (!rotateTowardsMouse)
            RotateTowardMovementVector(movementVector);
        else
            RotateTowardMouseVector();

    }

    
    private void RotateTowardMouseVector()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = _leader.transform.position.y;
            _leader.transform.LookAt(target);
        }
    }



    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementVector);
        _leader.transform.rotation = Quaternion.RotateTowards(_leader.transform.rotation, rotation, rotateSpeed);
    }



    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        targetVector = Vector3.Normalize(targetVector);
        var targetPosition = _leader.transform.position + targetVector * speed;
        _leader.transform.position = targetPosition;
        return targetVector;
    }
}
