using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpikeControler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spikes;
    private bool areSpikesEnabled;
    [SerializeField] private List<Sprite> _spikeSprites;
    private int randNum = 0;

    public void ToggleSpikes()
    {
        randNum = Random.Range(120, 600);
        areSpikesEnabled = !areSpikesEnabled;
        for (int i = 0; i < _spikes.Count; i++)
        {
            _spikes[i].GetComponent<SpriteRenderer>().sprite = areSpikesEnabled ? _spikeSprites[0] : _spikeSprites[1];
            _spikes[i].tag = areSpikesEnabled ? "Enemy" : "Untagged";
            _spikes[i].GetComponent<Collider2D>().isTrigger = !areSpikesEnabled;
        }
    }

    private void Start()
    {
        randNum = Random.Range(120, 600);
    }

    private void FixedUpdate()
    {
        randNum--;
        if (randNum < 0)
        {
            ToggleSpikes();
        }
    }
}
