using System;
using System.Collections.Generic;
using UnityEngine;

public class ThreadedRecognizer
{
    IRecognizer recognizer;

    Color[,] texture;

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

    public void Recognize(IRecognizer recognizer, Color[,] texture)
    {

        this.recognizer = recognizer;
        this.texture = texture;
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
        // Return the value through the Result property.  
        e.Result = rec.Recognize(texture);
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
