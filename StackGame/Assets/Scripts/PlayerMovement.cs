using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] GameObject Simple;
    [SerializeField] GameObject Spray;  
    [SerializeField] GameObject Graffiti;
    [SerializeField] GameObject Neon;
    [SerializeField] GameObject Fly;
    [SerializeField] Animator _anim;
    private Rigidbody _rb;

    private Sequence _sequence;
    private Vector3 _direction;
    private Transform _transform;
    private bool _isMoving = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = transform.GetComponentInChildren<Animator>();
        _transform = transform;
        SkateHolder.Instance.skateList.Add(transform.GetChild(0));
        Simple = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        Spray = this.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
        Graffiti = this.gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
        Neon = this.gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
        Fly = this.gameObject.transform.GetChild(0).transform.GetChild(4).gameObject;
    }

    void FixedUpdate()
    {
        if (_isMoving) Movement();
    }

    private void Movement()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal") * speedMultiplier, 0, verticalSpeed) * Time.fixedDeltaTime;
        _transform.Translate(_direction.x, 0, _direction.z);
        _anim.SetFloat ("TurnFloat",Input.GetAxis("Horizontal"));
        var localPosition = _transform.localPosition;
        localPosition = new Vector3(Mathf.Clamp(localPosition.x, -3f, 3f), localPosition.y, localPosition.z);
        Quaternion rot = transform.localRotation;
        transform.localRotation = rot;
        rot.y = Mathf.Clamp(transform.eulerAngles.y, -10, 10);
        _transform.localPosition = localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _isMoving = false;
            EventHolder.Instance.FinishCollider();
        }
        //Change to Spray
        if (other.transform.CompareTag("Spray"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(true);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Graffiti
        if (other.transform.CompareTag("Graffiti"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(true);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
        }

        //Change to Neon
        if (other.transform.CompareTag("Neon"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(true);
            Fly.gameObject.SetActive(false);
        }

        //Change to Fly
        if (other.transform.CompareTag("Fly"))
        {
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Collectable"))
        {
            EventHolder.Instance.SkateCollided(other.transform);
        } 
    }
}