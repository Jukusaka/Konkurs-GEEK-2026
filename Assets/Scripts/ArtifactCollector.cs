using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactCollector : MonoBehaviour
{
    [SerializeField] private DialougeBox _dialougeBox;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _eKey;
    private PlayerMovement _player;

    private bool _playerInRange = false;
    private bool _playerPickUp = false;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            _dialougeBox.text = _description;
            _dialougeBox.EnableTextBox();
            _playerPickUp = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            _eKey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            _eKey.SetActive(false);
            _dialougeBox.DisableTextBox();
            if (_playerPickUp)
            {
                _player.UpdateArtifactCounter();
                Destroy(gameObject);
            }
        }
    }
}