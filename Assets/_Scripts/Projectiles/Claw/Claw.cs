using UnityEngine;

public class Claw : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        Destroy(this.gameObject);
    }
}
