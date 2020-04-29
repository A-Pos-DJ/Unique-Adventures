using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackdropManager : MonoBehaviour
{
    private bool initalized = false;

    public float nextGenTimerGoL = 0f;

    bool gameOfLifeMode;
    int additionalPatternThreshold = 40;
    float nextGenerationDelayGoL = 0.05f;

    private int sizeX = 100;
    private int sizeY = 30;

    private PixelBackdrop[,] pixels;


    //initalize the backdrop
    public void Init()
    {
        pixels = new PixelBackdrop[sizeX, sizeY];

        //loop though the entire pixel array and assign the corralating pixels to it, then initalize them
        for (int idxY = 0; idxY <= sizeY - 1; idxY++)
        {
            for (int idxX = 0; idxX <= sizeX - 1; idxX++)
            {
                pixels[idxX, idxY] = gameObject.transform.GetChild(idxY).transform.GetChild(idxX).GetComponent<PixelBackdrop>();
            }
        }

        for (int idxY = 0; idxY <= sizeY - 1; idxY++)
        {
            for (int idxX = 0; idxX <= sizeX - 1; idxX++)
            {
                PixelBackdrop.Neighbors currentNeighbors = GetPixelNeighbors(idxX, idxY);
                pixels[idxX, idxY].Init(currentNeighbors);
            }
        }

        InitGameOfLife();

        initalized = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if the backdrop is not initalized... return
        if (!initalized) { return; }

        //if game of life mode is on
        if (gameOfLifeMode)
        {
            //if the number of pixels reamining are less than the threshold, then preform a random splash
            if (GetPercentageAliveGoL() <= additionalPatternThreshold)
            {
                RandomSplashGoL();
            }

            //if the next generation is supposed to occur...
            if (nextGenTimerGoL > nextGenerationDelayGoL)
            {
                NextGenerationSetupGoL();
                NextGenerationImplementGoL();
                nextGenTimerGoL = 0;
            }
        }

        //randomSplashTimerGoL += Time.deltaTime;
        nextGenTimerGoL += Time.deltaTime;
    }

    //locates and returns all pixel neighbors, returns null if there are no neighbors around the pixel
    public PixelBackdrop.Neighbors GetPixelNeighbors(int xIndex, int yIndex)
    {
        //check each side and corner of a pixel to see if there should be a pixel, then assign it. else assign null
        PixelBackdrop.Neighbors neighbors = new PixelBackdrop.Neighbors();

        neighbors.top = IsThereAPixelAtIndex(xIndex, yIndex - 1) ? pixels[xIndex, yIndex - 1] : null;
        neighbors.topRight = IsThereAPixelAtIndex(xIndex + 1, yIndex - 1) ? pixels[xIndex + 1, yIndex - 1] : null;
        neighbors.right = IsThereAPixelAtIndex(xIndex + 1, yIndex) ? pixels[xIndex + 1, yIndex] : null;
        neighbors.bottomRight = IsThereAPixelAtIndex(xIndex + 1, yIndex + 1) ? pixels[xIndex + 1, yIndex + 1] : null;
        neighbors.bottom = IsThereAPixelAtIndex(xIndex, yIndex + 1) ? pixels[xIndex, yIndex + 1] : null;
        neighbors.bottomLeft = IsThereAPixelAtIndex(xIndex - 1, yIndex + 1) ? pixels[xIndex - 1, yIndex + 1] : null;
        neighbors.left = IsThereAPixelAtIndex(xIndex - 1, yIndex) ? pixels[xIndex - 1, yIndex] : null;
        neighbors.topLeft = IsThereAPixelAtIndex(xIndex - 1, yIndex - 1) ? pixels[xIndex - 1, yIndex - 1] : null;

        return neighbors;
    }

    //checks to see if there is supposed to be a pixel in the index specified
    bool IsThereAPixelAtIndex(int xIndexArg, int yIndexArg)
    {
        bool xlocationConfirmed = false;
        bool ylocationConfirmed = false;

        //check to see if the index requested falls inside of the sizeX and sizeY variables.
        // flag false if it is not within the bounds
        xlocationConfirmed = xIndexArg >= 0 && xIndexArg <= sizeX - 1 ? true : false;
        ylocationConfirmed = yIndexArg >= 0 && yIndexArg <= sizeY - 1 ? true : false;

        //return true if both locations have been confirmed true, else return false
        return xlocationConfirmed && ylocationConfirmed ? true : false;
    }

    //initalize game of life algorithm
    public void InitGameOfLife()
    {
        //set game of life mode to be true
        gameOfLifeMode = true;

        //loop though the entire pixel array and assign the corralating pixels to it, then turn on game of life mode
        for (int idxY = 0; idxY <= sizeY - 1; idxY++)
        {
            for (int idxX = 0; idxX <= sizeX - 1; idxX++)
            {
                pixels[idxX, idxY].InitGameOfLifePixel();
            }
        }
    }

    //brings a small set of pixels back to life
    public void RandomSplashGoL()
    {
        int randomX = Randomizer.RandomInt(0, sizeX - 1);
        int randomY = Randomizer.RandomInt(0, sizeY - 1);

        pixels[randomX, randomY].AddRandomPatternGoL();
    }

    //gets the percent of pixels alive (as int)
    public int GetPercentageAliveGoL()
    {
        int pixelsTotal = 0;
        int pixelsAlive = 0;

        //loop though the entire pixel array
        for (int idxY = 0; idxY <= sizeY - 1; idxY++)
        {
            for (int idxX = 0; idxX <= sizeX - 1; idxX++)
            {
                if (pixels[idxX, idxY].isAliveGoL) { pixelsAlive++; }
                pixelsTotal++;
            }
        }

        return pixelsAlive / pixelsTotal;
    }


    //a cellular automata game of life patten next generation sequence setup
    public void NextGenerationSetupGoL()
    {
        //loop though the entire pixel array
        for (int idxY = 0; idxY <= sizeY - 1; idxY++)
        {
            for (int idxX = 0; idxX <= sizeX - 1; idxX++)
            {
                pixels[idxX, idxY].NextGenerationSetupGoL();
            }
        }
    }

    //game of life next generation implementaion
    public void NextGenerationImplementGoL()
    {
        //loop though the entire pixel array
        for (int idxY = 0; idxY <= sizeY - 1; idxY++)
        {
            for (int idxX = 0; idxX <= sizeX - 1; idxX++)
            {
                pixels[idxX, idxY].NextGenerationImplementGoL();
            }
        }
    }

}
