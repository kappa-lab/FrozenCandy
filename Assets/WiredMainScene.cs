using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class WiredMainScene : MonoBehaviour {

    public PlayableDirector director;
    public int index = 0;
    public int[] keyFrames = new int[3] { 0,10,15};
    public float alpha = 1f;
    public GameObject waterfall;
    public Color color;
    // Use this for initialization
    void Start () {
        Debug.Log(director);
        color = Color.white;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            director.enabled = !director.enabled;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            index = Mathf.Min(index, keyFrames.Length - 1);
            director.time = keyFrames[index];
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            director.time = keyFrames[index];
            director.Play();
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            color.a -= .06f;
            if (color.a < 0) color.a = 0;
            updateColor();
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            color.a += .06f;
            if (color.a > 1) color.a = 1;
            updateColor();
             return;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            color.b -= .06f;
            if (color.b < 0) color.b = 0;
            updateColor(); 
            return;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            color.r -= .06f;
            color.g -= .06f;
            if (color.r < 0) color.r = 0;
            if (color.g < 0) color.g = 0;
            updateColor();
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            color.r += .06f;
            color.g += .06f;
            color.b += .06f;
            if (color.r > 1) color.r = 1;
            if (color.g > 1) color.g = 1;
            if (color.b > 1) color.b = 1;
            updateColor();
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            color.r = 255 / 255f;
            color.g = 196 / 255f;
            color.b = 248 / 255f;
            updateColor();
            return;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            color.r = 87 / 255f;
            color.g = 168 / 255f;
            color.b = 163 / 255f;
            updateColor();
            return;
        }

    }
    void updateColor() {
        var ps = waterfall.GetComponentsInChildren<ParticleSystem>();
        foreach (var p in ps)
        {
            var main = p.main;
            main.startColor = new ParticleSystem.MinMaxGradient(
                color
            );
        }
    }
}
