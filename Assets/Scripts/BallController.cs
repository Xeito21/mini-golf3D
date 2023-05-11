using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public float changeAngleSpeed;
    public float lineLength;
    public Slider powerSlider;
    public TextMeshProUGUI puttCountLabel;
    public float minHoleTime;
    public Transform startBall;
    public LevelManager levelManager;

    private LineRenderer line;
    private Rigidbody ball;
    private float angle;
    private float powerUpTime;
    private float power;
    private int putts;
    private float holeTime;
    private Vector3 lastPosition;

    private void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        line = GetComponent<LineRenderer>();
        startBall.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        //Debug.Log(ball.velocity.magnitude);
        if(ball.velocity.magnitude < 0.01f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                ChangeAngle(-1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                ChangeAngle(1);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Putt();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                PowerUp();
            }
            UpdateLinePositions();
        }
        else
        {
            line.enabled = false;
        }

    }

    private void ChangeAngle(int direction)
    {
        angle += changeAngleSpeed * Time.deltaTime * direction;
    }

    private void UpdateLinePositions()
    {
        if (holeTime == 0) {line.enabled = true; }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * lineLength);
    }

    private void Putt()
    {
        lastPosition = transform.position;
        ball.AddForce(Quaternion.Euler(0, angle, 0) * Vector3.forward * maxPower * power, ForceMode.Impulse);
        power = 0;
        powerSlider.value = 0;
        powerUpTime = 0;
        putts++;
        puttCountLabel.text = putts.ToString();
    }

    private void PowerUp()
    {
        powerUpTime += Time.deltaTime;
        power = Mathf.PingPong(powerUpTime, 1);
        powerSlider.value = power;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Hole")
        {
            CountHoleTime();
        }
    }

    private void CountHoleTime()
    {
        holeTime += Time.deltaTime;
        if(holeTime >= minHoleTime)
        {
            levelManager.NextPlayer(putts);
            holeTime = 0;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hole")
        {
            LeftHole();
        }
    }

    private void LeftHole()
    {
        holeTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Out")
        {
            transform.position = lastPosition;
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }

    public void SetupBall(Color color)
    {
        transform.position = startBall.position;
        angle = startBall.rotation.eulerAngles.y;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        line.material.SetColor("_Color", color);
        line.enabled = true;
        putts = 0;
        puttCountLabel.text = "0";
    }

}
