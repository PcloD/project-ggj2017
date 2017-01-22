using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu( "Utilities/HUDFPS")]
public class HUDFPS : MonoBehaviour
{
    public bool updateColor = true; // Do you want the color to change if the FPS gets low
    public  float frequency = 0.5F; // The update frequency of the fps
    public int nbDecimal = 1; // How many decimal do you want to display

    private float accum   = 0f; // FPS accumulated over the interval
    private int   frames  = 0; // Frames drawn over the interval
    private Color color = Color.white; // The color of the GUI, depending of the FPS ( R < 10, Y < 30, G >= 30 )
    private string sFPS = ""; // The fps formatted into a string.

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine( FPS() );
    }

    void Update()
    {
        accum += Time.timeScale/ Time.deltaTime;
        ++frames;
    }

    IEnumerator FPS()
    {
        // Infinite loop executed every "frenquency" secondes.
        while( true )
        {
            // Update the FPS
            float fps = accum/frames;
            sFPS = fps.ToString( "f" + Mathf.Clamp( nbDecimal, 0, 10 ) );

            //Update the color
            color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.red : Color.yellow);

            accum = 0.0F;
            frames = 0;

            GUI.color = updateColor ? color : Color.white;
            text.color = updateColor ? color : Color.white;
            text.text = sFPS;

            yield return new WaitForSeconds( frequency );
        }
    }
}