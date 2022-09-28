using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = .25f;
    private float _currentSpeed;
    [SerializeField] float _turnSpeed = 2f;
    private float _currentTurnSpeed;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentSpeed = _maxSpeed;
        _currentTurnSpeed = _turnSpeed;
    }

    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
    }

    public void MultiplySpeed(float amount, float duration)
    {
        _currentSpeed = _maxSpeed * amount;
        _currentTurnSpeed = _turnSpeed * amount;
        Debug.Log("Speed: " + _currentSpeed);
        Debug.Log("TurnSpeed: " + _currentTurnSpeed);
        StartCoroutine(MultiplySpeedCountdown(duration));
    }

    private IEnumerator MultiplySpeedCountdown(float duration)
    {
        float normalizedTime = 0f;
        while(normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        _currentSpeed = _maxSpeed;
        _currentTurnSpeed = _turnSpeed;
        Debug.Log("CurrentSpeed: " + _currentSpeed);
        Debug.Log("TurnSpeed: " + _currentTurnSpeed);
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _currentSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _currentTurnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }
}
