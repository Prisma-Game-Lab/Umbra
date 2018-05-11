using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damage = 1;
    public float MaxDistance = 10;
    public float ArrowSpeed = 10;

    private Vector2 _parentPosition;

    private void Start()
    {
        _parentPosition = GetComponentInParent<GameObject>().transform.position;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Vector2.Distance(_parentPosition, this.transform.position)) > MaxDistance)
            Destroy(this.gameObject);
        else
            transform.Translate(new Vector2(-ArrowSpeed / 10, 0));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
            Destroy(this.gameObject);

        if (col.gameObject.CompareTag("Player"))
        {
            // Damage Player
            Destroy(this.gameObject);
        }
    }
}
