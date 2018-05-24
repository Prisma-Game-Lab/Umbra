using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int ArrowDamage = 1;
    public float MaxDistance = 10;
    public float ArrowSpeed = 10;

    //public LayerMask[] LayersToHit;

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
    
    /*
    private bool CheckCollision(Collider2D target)
    {
        for (int i = 0; i < LayersToHit.Length; i++)
            if (target.GetComponent<GameObject>().layer == LayersToHit[i])
                return true;

        return false;
    }
    */

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.layer == 9)
            Destroy(this.gameObject);  // Destroy Arrow
            

        /*
        if (CheckCollision(col) == true)
            Destroy(this.gameObject);
        */

        if (col.gameObject.CompareTag("Player"))  // Check if hit player
        {
            GetComponentInParent<ArcherShot>()._hasHit = true;  // Stops Archer of shooting
            
            col.GetComponent<PlayerHealth>().CurrentHealth -= ArrowDamage;  // Reduce player health equal to arrow damage

            Destroy(this.gameObject);  // Destroy Arrow
        }
    }
}
