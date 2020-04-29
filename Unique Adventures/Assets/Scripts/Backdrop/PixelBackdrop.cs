using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBackdrop : MonoBehaviour
{
    //a neighbors class for assigning the neighboring pixels
    public class Neighbors
    {
        public PixelBackdrop top;
        public PixelBackdrop topRight;
        public PixelBackdrop right;
        public PixelBackdrop bottomRight;
        public PixelBackdrop bottom;
        public PixelBackdrop bottomLeft;
        public PixelBackdrop left;
        public PixelBackdrop topLeft;
    }

    public Neighbors neighbors;

    public bool gameOfLifeMode;
    public bool isAliveGoL;
    public bool nextGenerationisAlive;

    public Material _material;

    //initalizer for the pixel
    public void Init(Neighbors neighborsArg)
    {
        neighbors = neighborsArg;
        _material = GetComponent<Renderer>().material;
    }

    //initilize game of life mode
    public void InitGameOfLifePixel()
    {
        gameOfLifeMode = true;
        isAliveGoL = true;
        nextGenerationisAlive = true;
        NewGameGoL();
    }

    //start a new GoL with the pixel
    public void NewGameGoL()
    {
        if (Randomizer.RandomInt(1, 100) > 50) { LifeGoL(); } else { DeathGoL(); }
        nextGenerationisAlive = isAliveGoL ? true : false;
    }

    //preps this pixel to be alive or dead according to the rules of Game of Life
    public void NextGenerationSetupGoL()
    {
        //check to see the number of neighbors alive
        int neighborsAlive = GetNeighborsAliveGoL();

        //if this cell is alive in the game of life...
        if (isAliveGoL)
        {
            //if there are fewer than two neighbors, the pixel dies by lonliness
            //if there are more than three neighbors, the pixel dies by overcrowding
            if (neighborsAlive < 2 || neighborsAlive > 3) { nextGenerationisAlive = false; }
            //if there two to three neighbors, the cell continues to live
        }
        //if this cell is dead in the game of life...
        else if(!isAliveGoL)
        {
            //any dead cell with exactly three neighbors comes to life
            if (neighborsAlive == 3) { nextGenerationisAlive = true; }
        }
    }

    //implements the next generation of pixels according to the tag of the next generation setup
    public void NextGenerationImplementGoL()
    {
        if (nextGenerationisAlive && !isAliveGoL) { LifeGoL(); }
        else if (!nextGenerationisAlive && isAliveGoL) { DeathGoL(); }
    }

    //check to see how many neighbors are alive in the game of life
    public int GetNeighborsAliveGoL()
    {
        int neighborsAlive = 0;

        //check to see if the neighboring pixel is there and then check to see if it is alive
        if (neighbors.top != null && neighbors.top.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.topRight != null && neighbors.topRight.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.right != null && neighbors.right.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.bottomRight != null && neighbors.bottomRight.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.bottom != null && neighbors.bottom.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.bottomLeft != null && neighbors.bottomLeft.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.left != null && neighbors.left.isAliveGoL) { neighborsAlive += 1; }
        if (neighbors.topLeft != null && neighbors.topLeft.isAliveGoL) { neighborsAlive += 1; }

        return neighborsAlive;
    }

    //adds a random pattern to the GoL backdrop
    public void AddRandomPatternGoL()
    {
        int patternNumber = Randomizer.RandomInt(0, 2);

        if (patternNumber == 0) { BlockPatternGoL(); }
        else if (patternNumber == 1) { BoatPatternGoL(); }
        else if (patternNumber == 2) { BlinkerPatternGoL(); }
    }

    //implements the block pattern in the game of life
    public void BlockPatternGoL()
    {
        InvertSingleNeighborGoL(this);
        InvertSingleNeighborGoL(neighbors.top);
        InvertSingleNeighborGoL(neighbors.topRight);
        InvertSingleNeighborGoL(neighbors.right);
        //InvertSingleNeighborGoL(neighbors.bottomRight);
        //InvertSingleNeighborGoL(neighbors.bottom);
        //InvertSingleNeighborGoL(neighbors.bottomLeft);
        //InvertSingleNeighborGoL(neighbors.left);
        //InvertSingleNeighborGoL(neighbors.topLeft);
    }

    //implements the boat pattern in the game of life
    public void BoatPatternGoL()
    {
        //InvertSingleNeighborGoL(this);
        InvertSingleNeighborGoL(neighbors.top);
        //InvertSingleNeighborGoL(neighbors.topRight);
        InvertSingleNeighborGoL(neighbors.right);
        //InvertSingleNeighborGoL(neighbors.bottomRight);
        InvertSingleNeighborGoL(neighbors.bottom);
        //InvertSingleNeighborGoL(neighbors.bottomLeft);
        InvertSingleNeighborGoL(neighbors.left);
        InvertSingleNeighborGoL(neighbors.topLeft);
    }

    //implements the block pattern in the game of life
    public void BlinkerPatternGoL()
    {
        InvertSingleNeighborGoL(this);
        InvertSingleNeighborGoL(neighbors.top);
        //InvertSingleNeighborGoL(neighbors.topRight);
        //InvertSingleNeighborGoL(neighbors.right);
        //InvertSingleNeighborGoL(neighbors.bottomRight);
        InvertSingleNeighborGoL(neighbors.bottom);
        //InvertSingleNeighborGoL(neighbors.bottomLeft);
        //InvertSingleNeighborGoL(neighbors.left);
        //InvertSingleNeighborGoL(neighbors.topLeft);
    }

    public void InvertSingleNeighborGoL(PixelBackdrop pixelArg)
    {
        //if null... return
        if (pixelArg == null) { return; }

        //if alive... kill the neighbor. if dead... bring back to life
        if (pixelArg.isAliveGoL) { pixelArg.DeathGoL(); }
        else if(!pixelArg.isAliveGoL) { pixelArg.LifeGoL(); }
    }

    //swaps the life/death status of each neighboring cell
    public void InvertAllNeighborsGoL()
    {
        InvertSingleNeighborGoL(neighbors.top);
        InvertSingleNeighborGoL(neighbors.topRight);
        InvertSingleNeighborGoL(neighbors.right);
        InvertSingleNeighborGoL(neighbors.bottomRight);
        InvertSingleNeighborGoL(neighbors.bottom);
        InvertSingleNeighborGoL(neighbors.bottomLeft);
        InvertSingleNeighborGoL(neighbors.left);
        InvertSingleNeighborGoL(neighbors.topLeft);
    }

    //what the cell does when it dies in game of life
    public void DeathGoL()
    {
        isAliveGoL = false;
        nextGenerationisAlive = false;
        ChangeColor(Color.black);
    }

    //what the cell does when it is alive in game of life
    public void LifeGoL()
    {
        isAliveGoL = true;
        nextGenerationisAlive = true;
        ChangeColor(Color.white);
    }

    //change the color of the pixel
    public void ChangeColor(Color newColor)
    {
        newColor.a = 0.5f;
        _material.color = newColor;
    }

}
