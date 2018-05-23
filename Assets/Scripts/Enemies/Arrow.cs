using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int ArrowDamage = 1;
    public float MaxDistance = 10;
    public float ArrowSpeed = 10;

    private Vector2 _parentPosition;
    

    private void Start()
    {
        _parentPosition = GetComponentInParent<GameObject>().transform.position;
    }

    private void Update()
    {
        if (Mathf.Abs(Vector2.Distance(_parentPosition, this.transform.position)) > MaxDistance)
            Destroy(this.gameObject);  // Destroy Arrow
        else
            transform.Translate(new Vector2(-ArrowSpeed / 10, 0));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
            Destroy(this.gameObject);  // Destroy Arrow

        if (col.gameObject.CompareTag("Player"))  // Check if hit player
        {
            GetComponentInParent<ArcherShot>()._hasHit = true;  // Stops Archer of shooting
            
            col.GetComponent<PlayerHealth>().CurrentHealth -= ArrowDamage;  // Reduce player health equal to arrow damage

            Destroy(this.gameObject);  // Destroy Arrow
        }
    }
}
