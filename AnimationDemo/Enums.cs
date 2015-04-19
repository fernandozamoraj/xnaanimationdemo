
namespace AnimationDemo
{ 

    public enum Direction
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3   
    }

    public enum SpriteType
    { 
        None = 0,
        Inky = 1,
        Blinkey = 2,
        Pinkey = 3,
        Clyde = 4,
        PacMan = 5
    }

    public enum GameState
    { 
        GameOver,
        StartingLife,
        Playing,
    }
}