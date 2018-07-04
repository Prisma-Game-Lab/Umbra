using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float MaxDistance = 10;
    public float ArrowSpeed = 10;

    private Vector2 _initialPosition;

    private void Start()
    {
        _initialPosition = this.transform.position;
    }

    private void Update()
    {
        if (Mathf.Abs(Vector2.Distance(_initialPosition, this.transform.position)) > MaxDistance)
            Destroy(this.gameObject);  // Destroy Arrow
        else
            transform.Translate(new Vector2(-ArrowSpeed / 10, 0));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9) {
            Destroy(this.gameObject);
        }

        if (col.gameObject.CompareTag("Player"))  // Check if hit player
        {
            GetComponentInParent<ArcherShot>()._hasHit = true;  // Stops Archer of shooting
            Destroy(this.gameObject);  // Destroy Arrow
        }
    }
}
