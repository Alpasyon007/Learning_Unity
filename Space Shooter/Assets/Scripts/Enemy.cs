using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;

    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        if (_player == null) {
            Debug.LogError("The Player is NULL.");
        }
        if (_animator == null) {
            Debug.LogError("The Animator is NULL.");
        }
    }
    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f) {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f,8f), 7, 0);
            transform.position = spawnPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (_player != null) {    
                _player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }
        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            if (_player != null) {
                _player.Score(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }
    }  
}
