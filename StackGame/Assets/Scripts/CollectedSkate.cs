using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using CASP.SoundManager;
public class CollectedSkate : MonoBehaviour
{
    [SerializeField] GameObject Simple;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject Spray;
    [SerializeField] GameObject Graffiti;
    [SerializeField] GameObject Neon;
    [SerializeField] GameObject Fly;
    
    private Sequence _sequence;

    public PriceModel price;
    void Start()
    {
        price = GetComponent<PriceModel>();
        _anim = transform.GetComponent<Animator>();
        Simple = this.gameObject.transform.transform.GetChild(0).gameObject;
        Spray = this.gameObject.transform.transform.GetChild(1).gameObject;
        Graffiti = this.gameObject.transform.transform.GetChild(2).gameObject;
        Neon = this.gameObject.transform.transform.GetChild(3).gameObject;
        Fly = this.gameObject.transform.transform.GetChild(4).gameObject;
        // SoundManager.instance.Play("Background",true);
    }

    void Update()
    {
        _anim.SetFloat("TurnFloat", Input.GetAxis("Horizontal"));
        Mathf.Clamp(transform.rotation.z, -25, 25);
    }
    void OnCollisionEnter(Collision other)
    {
        //Collectable
        if (other.transform.CompareTag("Collectable"))
        {
            EventHolder.Instance.SkateCollided(other.transform);
            EventHolder.Instance.PriceChangeEvent();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //Enemy Obstacle
        if (other.CompareTag("Obstacle"))
        {

            EventHolder.Instance.ObstacleCollided(transform);
            EventHolder.Instance.OnPriceChange();
            Debug.Log("Colliding To Enemy");
        }

        //Change to Spray
        if (other.transform.CompareTag("Spray"))
        {
            SoundManager.instance.Play("Down",true);
            price.Price += 5;
            EventHolder.Instance.OnPriceChange();
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(true);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Graffiti
        if (other.transform.CompareTag("Graffiti"))
        {
            price.Price += 5;
            EventHolder.Instance.OnPriceChange();
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(true);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Neon
        if (other.transform.CompareTag("Neon"))
        {
            price.Price += 5;
            EventHolder.Instance.OnPriceChange();
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(true);
            Fly.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Fly
        if (other.transform.CompareTag("Fly"))
        {
            price.Price += 5;
            EventHolder.Instance.OnPriceChange();
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(true);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }
    }
}
