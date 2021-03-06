using UnityEngine;

public class Player2 : PlayerBase
{
    public Transform transformPlayer2 = null;

    private void Start()
    {
        speed = normalSpeed;
    }

    void FixedUpdate()
    {
        if (playerIndex == -1)
        {
            Vector3 direction = Vector3.zero;
            direction.x = Input.GetAxis("Horizontal");
            transform.position += direction * (speed * Time.deltaTime);

        }
    }

    void Update()
    {
        if (playerIndex == -1)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                ChangePlayer();
                Debug.Log("MUDEI PRO PLAYER 1");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_jumping == false)
                {
                    Jump();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                isInteracting = true;
            }

            if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                isInteracting = false;
            }

            if (Input.GetKey(KeyCode.E) == false || Input.GetKeyDown(KeyCode.Alpha2))
            {
                isHolding = false;
            }

        }
    }

    public override void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _jumping = true;
    }

    public override void ChangePlayer()
    {
        player1.playerIndex = 1;
        playerIndex = 1;
        _camera2.SetActive(false);
        _camera1.SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FloorButton"))
        {
            Button button = other.gameObject.GetComponent<Button>();

            if (button != null)
            {
                button.Pressed();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            Box box = other.gameObject.GetComponent<Box>();

            if (box != null)
            {
                if (_jumping == false)
                {
                    if (isInteracting)
                    {
                        isHolding = true;
                        speed = holdingSpeed;
                        box.transform.SetParent(transformPlayer2);
                        box.BeingManipulated();
                    }

                    if (isInteracting == false)
                    {
                        isHolding = false;
                        speed = normalSpeed;
                        box.transform.parent = null;
                        box.Freeze();
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Button"))
        {
            Button button = other.gameObject.GetComponent<Button>();

            if (button != null)
            {
                if (isInteracting)
                {
                    if(isHolding == false)
                    {
                        button.Pressed();
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("FloorButton"))
        {
            Button button = other.gameObject.GetComponent<Button>();

            if (button != null)
            {
                button.Pressed();
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            Box box = other.gameObject.GetComponent<Box>();

            if (box != null)
            {
                box.Freeze();
            }
        }

        if (other.gameObject.CompareTag("FloorButton"))
        {
            FloorButton button = other.gameObject.GetComponent<FloorButton>();

            if (button != null)
            {
                button.Unpressed();
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (_jumping)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.localPosition, Vector3.down, out hit, 2.0f, 1 << 8))
                {
                    if (hit.collider == other.collider)
                    {
                        Debug.Log("LIBEROU PULO");
                        _jumping = false;

                    }
                }
            }

            if (other.gameObject.CompareTag("Box"))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.localPosition, Vector3.down, out hit, 2.0f, 1 << 10))
                {
                    if (hit.collider == other.collider)
                    {
                        Debug.Log("LIBEROU PULO");
                        _jumping = false;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.localPosition, Vector3.down, out hit, 2.0f, 1 << 8))
            {
                if (hit.collider == other.collider)
                {
                    Debug.Log("LIBEROU PULO");
                    _jumping = true;

                }
            }
        }

        if (other.gameObject.CompareTag("Box"))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.localPosition, Vector3.down, out hit, 2.0f, 1 << 10))
            {
                if (hit.collider == other.collider)
                {
                    Debug.Log("LIBEROU PULO");
                    _jumping = true;
                }
            }
        }
    }
}

