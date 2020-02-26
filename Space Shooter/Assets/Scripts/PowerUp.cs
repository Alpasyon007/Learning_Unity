using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                switch (powerupID) {
                    case 0:
                        player.TripleShot();
                        break;
                    case 1:
                        player.SpeedUp();
                        break;
                    case 2:
                        player.Shield();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
