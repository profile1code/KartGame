//James Osborn
//PlayerInput Class, Karting Final Project
//This class takes a player's input and turns it into forces,
//which are then applied to the kart in game.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerMain : MonoBehaviour {

    [SerializeField] Vector3 texture;
    //all variables that can be used for info for debugging + other classes
    [SerializeField] private float gripConstant = 0.65f;
    private float rollingFriction = 0.05f;
    [SerializeField] private float horsepower = 800f;
    [SerializeField] private float brakepower = 2000f;
    private float airDensity = 1f;
    private float frontalArea = 0.75f;
    public float mass = 150;
    [SerializeField] public float gearRatio = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundFriction;
    [SerializeField] private float drag;
    [SerializeField] public float velocity;
    [SerializeField] public float MPH;
    [SerializeField] public float RPM;
    [SerializeField] private float rotationalForce;
    [SerializeField] private float radius;
    [SerializeField] private float forwardForce;
    [SerializeField] private float backwardForce;
    [SerializeField] private float steeringInput;
    [SerializeField] private float accelerationInput;
    [SerializeField] private float brakingInput;
    [SerializeField] private float turnRate;
    [SerializeField] private float turnRateSave;
    [SerializeField] private bool lastFrameInAir;
    [SerializeField] private Vector3 momentumSave;
    [SerializeField] public Vector3 rotationSave;
    [SerializeField] private float currentPower;

    [SerializeField] private float forwardBackDistance;
    [SerializeField] private float sideDistance;

    [SerializeField] private float distance;
    private float timeinAir;
    public Vector3 bodyPosition;


    [SerializeField] LayerMask roadLayer;
    [SerializeField] LayerMask playerLayer;

    Rigidbody body;
    Transform transform;
    // Start is called before the first frame update
    void Start() {
       body = GetComponent<Rigidbody>();
       transform = GetComponent<Transform>();
       startDriving();
    }

    public void startDriving() {
        velocity = 0.0f;
        body.velocity = new Vector3(0f, 0f, 0f);
        momentumSave = Vector3.zero;
        rotationSave = Vector3.zero;
        turnRateSave = 0.0f;
        GameObject.Find("Ghost").GetComponent<Ghost>().resetLap();
        
       
    }
    // Update is called once per frame
    void Update() {

        takeInputs();
        findForces();
        applyForces();

    }
    //only gets user input
    private void takeInputs() {
        steeringInput = Input.GetAxis("Horizontal");
        bool accelerationW = Input.GetKey("up") || Input.GetKey(KeyCode.W);
        bool brakingS = Input.GetKey("down") || Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.Space)) {
            startDriving();
        }

        gearRatio = GameObject.Find("Player").GetComponent<Gearing>().getRatio();
        setPerformance();
        
        
        if (accelerationW) {
            //accelerationInput = Input.GetAxis("Axis 10");
            accelerationInput = 1.0f;
        }
        else {
            accelerationInput = 0.0f;
        }

        if (brakingS) {
            //accelerationInput = Input.GetAxis("Axis 10");
            brakingInput = 1.0f;
        }
        else {
            //brakingInput = Input.GetAxis("Axis 9");
            brakingInput = 0.0f;
        } 
    }

    private void findForces() {
        //Finds the force applied to the kart in the forward/backwards direction
        bool leftOnTrack = GameObject.Find("Left").GetComponent<TireBehavior>().isOnTrack;
        bool rightOnTrack = GameObject.Find("Right").GetComponent<TireBehavior>().isOnTrack;
        
        
        velocity = body.velocity.magnitude;
        bodyPosition = body.position;
        MPH = velocity * 2.23693629f;
        
        
        //friction forces applied away from the kart
        drag = 0.5f * airDensity * frontalArea * velocity * velocity;
        groundFriction = (mass * 9.81f * gripConstant + drag) * rollingFriction;

        //horsepower change
        getPower();

        forwardForce = (accelerationInput * currentPower);
        backwardForce = (brakingInput * brakepower) + drag + groundFriction;

        rotationalForce = steeringInput * (gripConstant * mass * 9.81f + drag + (groundFriction / rollingFriction));

        
        //Finds force applied to the kart to make it rotate F = mV^2/r or r = mV^2/F

        //radius has a minimum radius so that there isn't a problem with turning infinitely fast
        
        radius = (mass * velocity * velocity) / rotationalForce;

        if (radius < 3f) {
            if (radius > -3f) {
                radius = Mathf.Abs(radius) / radius * 3f;
            }
        }
        //finds the rate (int degrees/sec) that it rotates
        turnRate = velocity / (2 * Mathf.PI * radius) * 360f;

        if (!leftOnTrack) {
            if (turnRate > 0) {
               turnRate /= 1.5f; 
            }
            else {
                turnRate /= 1.1f;
            }
        }

        if (!rightOnTrack) {
            if (turnRate < 0) {
                turnRate /= 1.5f;
            }
            else {
                turnRate /= 1.1f;
            }
        }

    }
    //puts the forces on the kart

    void FixedUpdate() {
        
        rotationSave = transform.localEulerAngles;

        bool frontOnGround = GameObject.Find("Front").GetComponent<TireBehavior>().onGround;
        bool backOnGround = GameObject.Find("Back").GetComponent<TireBehavior>().onGround;
        bool leftOnGround = GameObject.Find("Left").GetComponent<TireBehavior>().onGround;
        bool rightOnGround = GameObject.Find("Right").GetComponent<TireBehavior>().onGround;

        float frontDistance = GameObject.Find("Front").GetComponent<TireBehavior>().distance;
        float backDistance = GameObject.Find("Back").GetComponent<TireBehavior>().distance;
        float leftDistance = GameObject.Find("Left").GetComponent<TireBehavior>().distance;
        float rightDistance = GameObject.Find("Right").GetComponent<TireBehavior>().distance;

        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        distance = 0.7f;
        if (Physics.Raycast(downRay, out hit, 1000f, ~playerLayer)) {

            distance = transform.position.y - hit.point.y;
            texture = hit.textureCoord;
        }

        if (backOnGround || frontOnGround || leftOnGround || rightOnGround) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
        

        //rotates the character to fit with the ground
        
        
        if (distance < 1f) {
            forwardBackDistance = frontDistance - backDistance;
            
            transform.Rotate(new Vector3(Mathf.Asin(forwardBackDistance) * 0.5f, 0f, 0f));
            
            sideDistance = leftDistance - rightDistance;

            transform.Rotate(new Vector3(0f, 0f, (Mathf.Asin(sideDistance) * 0.5f)));
            
            if (Physics.Raycast(transform.position, Vector3.down, 3f, roadLayer)) {
                //if (forwardBackDistance < 0.05 && forwardBackDistance > -0.05 && frontDistance > 0.67 && backDistance > 0.67) {
                //    transform.Translate(Vector3.down * 0.015f);
                //}
                if (distance < 0.6f) {
                    transform.Translate(Vector3.down * -0.012f);
                }
                if (distance > 0.6f) {
                    transform.Translate(Vector3.down * 0.012f);
                }
            }

        }

        //moves character up when low enough to stop it hitting ground
        float min = Mathf.Max(frontDistance, backDistance);
        if (min < 0.6f) {
                transform.position = new Vector3(transform.position.x, transform.position.y + (0.6f - min), transform.position.z);
            }


        
        if (!isGrounded) {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y - (9.81f * Time.deltaTime), body.velocity.z);
            
        } 

        if (distance < 1f || isGrounded) {
            body.AddForce(transform.forward * forwardForce);
        }


    }
    void applyForces() { 
        float slow;
        float amountTurned;
        
        //the amount of speed the friction will remove (v = Ft/m)
        if (isGrounded) {

        if (lastFrameInAir) {
                float angleTurned = turnRateSave * timeinAir * Mathf.PI / 180f;
                float velocityMaintainRatio = Mathf.Cos(angleTurned);
                momentumSave *= velocityMaintainRatio;
                body.velocity = momentumSave;
                lastFrameInAir = false;
            }

            slow = (backwardForce / mass) * Time.deltaTime;
            amountTurned = turnRate * Time.deltaTime;
            turnRateSave = turnRate;
            transform.Rotate(0, amountTurned, 0);
            body.velocity = transform.forward * (body.velocity.magnitude - slow);
            momentumSave = body.velocity;
            timeinAir = 0f;
            

            
            
            
        }
        else {
            slow = drag * Time.deltaTime;
            amountTurned = turnRateSave * Time.deltaTime;
            transform.Rotate(0, amountTurned, 0);
            lastFrameInAir = true;
            timeinAir += Time.deltaTime;
            
            //need to make character land with the momentum changing depending on the angle
        }
        
        

    }

    void setPerformance() {
        string kartType = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart();

        if (kartType.Equals("Rental")) {
            horsepower = 250f;
            brakepower = 1200f;
            gripConstant = 0.5f;
            mass = 180f;
            body.mass = 180f;
            rollingFriction = 0.02f;
        }
        if (kartType.Equals("Sprint")) {
            horsepower = 800f;
            brakepower = 1800f;
            gripConstant = 0.65f;
            mass = 160f;
            body.mass = 160f;
            rollingFriction = 0.1f;
        }
        if (kartType.Equals("Shifter")) {
            horsepower = 800f;
            brakepower = 2350;
            gripConstant = 0.65f;
            mass = 170f;
            body.mass = 170f;
            rollingFriction = 0.1f;
        }
    }

        void getPower() {
            
            string kart = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart();
            
            if (kart.Equals("Shifter") || kart.Equals("Sprint")) {
                
                RPM = 3000 + (velocity * gearRatio * 150);
                if (RPM > 13500f) {
                    RPM = 0f;
                }
                currentPower = (horsepower / 3.5f ) + (horsepower * RPM / 12500) - ((RPM * RPM * RPM / 20) / (horsepower * horsepower * horsepower));
        
                if (RPM > 11500) {
                    currentPower -= (RPM - 11500) / 6;
                }
                
            }

            if (kart.Equals("Rental")) {
                RPM = 800 + (velocity * gearRatio * 75);
                if (RPM > 4500f) {
                    RPM = 0f;
                }
                currentPower = horsepower - (horsepower * horsepower * horsepower / (RPM * RPM));
            }

            if (RPM < 100f) {
                currentPower = 0f;
            }
                

        }

}

