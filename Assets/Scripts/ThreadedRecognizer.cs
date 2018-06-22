using System;
using System.Collections.Generic;
using UnityEngine;

public class ThreadedRecognizer
{
    IRecognizer recognizer;

    Color[] texture;
    int width;
    int height;

    private System.ComponentModel.BackgroundWorker BackgroundWorker1
    = new System.ComponentModel.BackgroundWorker();

    public List<RecognizedItem> Result
    {
        get; private set;
    }

    public bool Completed
    {
        get; private set;
    }

    public void Recognize(IRecognizer recognizer, Color[] texture, int width, int height)
    {

        this.recognizer = recognizer;
        this.texture = texture;
        this.width = width;
        this.height = height;
        InitializeBackgroundWorker();

        // Start the asynchronous operation.  
        BackgroundWorker1.RunWorkerAsync(recognizer);
    }

    void InitializeBackgroundWorker()
    {
        // Attach event handlers to the BackgroundWorker object.  
        BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
        BackgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
    }

    void BackgroundWorker1_DoWork(
        object sender,
        System.ComponentModel.DoWorkEventArgs e)
    {
        IRecognizer rec = (IRecognizer)e.Argument;
        Color[,] pixels = new Color[width, height];
        //Convert array of pixels from one dimesnional to two dimensional based on height and width
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++)
            {
                pixels[i, j] = texture[width * j + i];
            }

        }
        // Return the value through the Result property.  
        e.Result = rec.Recognize(pixels);
    }

    void BackgroundWorker1_RunWorkerCompleted(
        object sender,
        System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        // Access the result through the Result property.  
        Result = (List<RecognizedItem>)e.Result;
        Completed = true;
    }
}
