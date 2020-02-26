using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShot = false;
    private bool _speedUp = false;
    private bool _shield = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManeger;
    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManeger = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null) {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManeger == null) {
            Debug.LogError("The UI Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update() {
        Movement();
        Laser();
    }

    void Movement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

        //More optimal way doing the code commented out code above
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //Cleaner altenative to the optimal code
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
    
        

        //if (transform.position.y >= 0) {
        //    transform.position = new Vector3(transform.position.x, 0, 0);
        //} else if (transform.position.y <= -3.8f) {
        //    transform.position = new Vector3(transform.position.x, -3.8f, 0);
        //};

        //Alternative more optimal way to clamp two values
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f , 0), 0);

        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void Laser() {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) {
            _canFire = Time.time + _fireRate;
            if(_tripleShot == true) {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.6f, 0, 0), Quaternion.identity);
            } else {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            
        }
    }

    public void Damage() {
        if (_shield) {
            _shieldVisualizer.gameObject.SetActive(false);
            _shield = false;
            return;
        }
        _lives--;
        _uiManeger.UpdateLives(_lives);
        if (_lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShot() {
        _tripleShot = true;
        StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown() {
        while (_tripleShot) {
            yield return new WaitForSeconds(5.0f);
            _tripleShot = false;
        }
    }

    public void SpeedUp() {
        _speedUp = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedUpRotuine());
    }

    IEnumerator SpeedUpRotuine() {
        while (_speedUp) {
            yield return new WaitForSeconds(5.0f);
            _speed /= _speedMultiplier;
            _speedUp = false;
        }
    }

    public void Shield() {
        _shield = true;
        _shieldVisualizer.gameObject.SetActive(true);
    }

    public void Score(int points) {
        _score += points;
        _uiManeger.UpdateScore(_score);
    }

}
