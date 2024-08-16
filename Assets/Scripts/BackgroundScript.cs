using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private DifficultyRiser difficulty;
    private Material material;
    private float offset = 0;
    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        difficulty = DifficultyRiser.Instance;
    }
    private void Update()
    {
        offset -= moveSpeed * Time.deltaTime * difficulty.GetCurrentMultiplier();
        material.mainTextureOffset = new Vector2(0, offset);
    }
}
