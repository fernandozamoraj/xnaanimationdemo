using System;
using System.Collections.Generic;
using System.Text;
using FzGameBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimationDemo
{
    public class PacmanSprite : Sprite
    {
        #region Constructor

        public PacmanSprite(int x, int y, int maxXPos, int maxYPos)
        {
            Location.X = x;
            Location.Y = y;
            _MaxXPos = maxXPos;
            _MaxYPos = maxYPos;
            Size.X = 32;
            Size.Y = 32;
        }

        public PacmanSprite(int x, int y, int maxXPos, int maxYPos,  Keys left, Keys right, Keys up, Keys down)
        {
            _SpriteType = SpriteType.PacMan;
            _MaxXPos = maxXPos;
            _MaxYPos = maxYPos;
            Location.X = x;
            Location.Y = y;
            _UpKey = up;
            _DownKey = down;
            _LeftKey = left;
            _RightKey = right;
            Size.X = 32;
            Size.Y = 32;           
        }

        #endregion

        #region Fields

        protected int MAX_SPEED = 3;
        protected int MIN_SPEED = 1;
        protected Keys _UpKey;
        protected Keys _DownKey;
        protected Keys _LeftKey;
        protected Keys _RightKey;
        protected int _MaxXPos;
        protected int _MaxYPos;
        protected SpriteType _SpriteType = SpriteType.None;
        protected Game1 _TheGame;
        protected bool _LeftKeyDown = false;
        protected bool _RightKeyDown = false;
        protected bool _UpKeyDown = false;
        protected bool _DownKeyDown = false;
        protected Direction _CurrentDirection = Direction.Left;
        protected float _Speed = 2;
        protected static Random  _RandomNumberGenerator = new Random();
        protected Queue<Vector2> _MovesQueue = new Queue<Vector2>();
        protected Vector2        _Target;
                
        #endregion

        #region Methods

        public virtual void IncreaseSpeed()
        {
            _Speed++;
            if (_Speed > MAX_SPEED)
            {
                _Speed = MAX_SPEED;
            }
        }

        public virtual void DecreaseSpeed()
        {
            _Speed--;
            if (_Speed < MIN_SPEED)
            {
                _Speed = MAX_SPEED;
            }
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            if (_TheGame == null)
            {
                _TheGame = Game1.GetTheGameInstance();
            }

            GetKeyStates();
            Advance(gameTime, elapsedTime);
        }
        
        private void GetKeyStates()
        {
            _LeftKeyDown = _RightKeyDown = _UpKeyDown = _DownKeyDown = false;

            if (_SpriteType == SpriteType.PacMan)
            {
                if (_TheGame.GameState != GameState.GameOver)
                {
                    KeyboardState keyState = Keyboard.GetState();

                    if (keyState.IsKeyDown(_LeftKey))
                    {
                        _LeftKeyDown = true;
                    }
                    if (keyState.IsKeyDown(_RightKey))
                    {
                        _RightKeyDown = true;
                    }
                    if (keyState.IsKeyDown(_UpKey))
                    {
                        _UpKeyDown = true;
                    }
                    if (keyState.IsKeyDown(_DownKey))
                    {
                        _DownKeyDown = true;
                    }

                    GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                    if (gamePadState.ThumbSticks.Left.X < 0)
                    {
                        _LeftKeyDown = true;
                    }
                    if (gamePadState.ThumbSticks.Left.X > 0)
                    {
                        _RightKeyDown = true;
                    }
                    if (gamePadState.ThumbSticks.Left.Y > 0)
                    {
                        _UpKeyDown = true;
                    }
                    if (gamePadState.ThumbSticks.Left.Y < 0)
                    {
                        _DownKeyDown = true;
                    }
                }
                else
                {
                    PerformAI();
                }
            }
            else
            {
                PerformAI();
            }
        }

        public virtual void ResetAfterKill()
        {
            if (_SpriteType == SpriteType.PacMan)
            {
                Location = new Vector2(9 * 32, 16 * 32);
            }
            else
            {
                Location = new Vector2(9 * 32, 6 * 32);
                _MovesQueue.Clear();
            }
        }

        protected virtual void PerformAI()
        {

                        
        }

        protected virtual void Advance(double gameTime, double elapsedTime)
        {
            //base.Update(gameTime, elapsedTime);

            bool getNextFrame = false;

            Vector2 newLocation = new Vector2(Location.X, Location.Y);
            Direction newDirection = _CurrentDirection;
            Direction prevDirection = _CurrentDirection;
            bool keyDown = false;

            //Set the velocity of the sprite based on which keys are pressed
            if (_LeftKeyDown)
            {
                newLocation.X += -_Speed;
                newDirection = Direction.Left;
                keyDown = true;

            }
            else if (_RightKeyDown)
            {
                newLocation.X += _Speed;
                newDirection = Direction.Right;
                keyDown = true;
            }
            else if (_UpKeyDown)
            {
                newLocation.Y += -_Speed;
                newDirection = Direction.Up;
                keyDown = true;
            }
            else if (_DownKeyDown)
            {
                newLocation.Y += _Speed;
                newDirection = Direction.Down;
                keyDown = true;
            }

            bool moved = false;

            if (keyDown && _TheGame.TheMaze.CanMoveThere(newLocation.X, newLocation.Y, prevDirection, newDirection, _Speed, _SpriteType))
            {
                Location.X = newLocation.X;
                Location.Y = newLocation.Y;
                _CurrentDirection = newDirection;
                getNextFrame = true;
                moved = true;
            }

            if (keyDown == false || moved == false)
            {
                if (_CurrentDirection == Direction.Left)
                {
                    newLocation.X += -_Speed;
                    newDirection = Direction.Left;
                }
                else if (_CurrentDirection == Direction.Right)
                {
                    newLocation.X += _Speed;
                    newDirection = Direction.Right;
                }
                else if (_CurrentDirection == Direction.Up)
                {
                    newLocation.Y += -_Speed;
                    newDirection = Direction.Up;
                }
                else if (_CurrentDirection == Direction.Down)
                {
                    newLocation.Y += _Speed;
                    newDirection = Direction.Down;
                }

                if (_TheGame.TheMaze.CanMoveThere(newLocation.X, newLocation.Y, prevDirection, newDirection, _Speed, _SpriteType))
                {
                    Location.X = newLocation.X;
                    Location.Y = newLocation.Y;

                    _CurrentDirection = newDirection;

                    getNextFrame = true;
                    moved = true;
                }
            }

            if (Location.X < -16)
            {
                Location.X = (32 * 18)+16;
            }
            if (Location.X > (32 * 18)+16)
            {
                Location.X = -16;
            }


            //This code insures that pac man stays in the middle of the maze path

            if (_CurrentDirection == Direction.Down || _CurrentDirection == Direction.Up)
            {
                float newX = (Convert.ToSingle(Math.Floor((Location.X + 16f) / 32))) * 32f;
                if (newX != Location.X)
                {
                    Location.X = newX;
                }
            }

            if (_CurrentDirection == Direction.Left || _CurrentDirection == Direction.Right)
            {
                float newY = (Convert.ToSingle(Math.Floor((Location.Y + 16f) / 32))) * 32f;
                if (newY != Location.X)
                {
                    Location.Y = newY;
                }
            }

            if (getNextFrame)
            {
                if (_TheGame.IsInPowerMode && _SpriteType != SpriteType.PacMan)
                {
                    SetActiveAnimation((int)_CurrentDirection+4);
                }
                else
                {
                    SetActiveAnimation((int)_CurrentDirection);
                }

                
                _CurrentFrameTexture = GetCurrentAnimation().GetNextFrame(gameTime);
            }
            else
            {
                if (_SpriteType == SpriteType.PacMan)
                {
                    _CurrentFrameTexture = GetCurrentAnimation().GetCurrentFrame();               
                }
                else
                {
                    if (_TheGame.IsInPowerMode)
                    {
                        SetActiveAnimation((int)_CurrentDirection + 4);
                    }
                    else
                    {
                        SetActiveAnimation((int)_CurrentDirection);
                    }

                    _CurrentFrameTexture = GetCurrentAnimation().GetNextFrame(gameTime);
                }
                
            }         
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            GameSettings gameSettings = GameSettings.GetGameSettings();

            if (_SpriteType == SpriteType.PacMan)
            {
                string[] textureNames = new string[] { "Pacman/PACMAN_LEFT", "Pacman/PACMAN_RIGHT", "Pacman/PACMAN_UP", "Pacman/PACMAN_DOWN" };

                for (int i = 0; i < textureNames.Length; i++)
                {

                    Animation animation = new Animation(content, true, .08f, 1f);

                    for (int j = 1; j <= 4; j++)
                    {
                        animation.AddTexture(gameSettings.ImagesPath + textureNames[i] + j.ToString());
                    }

                    AddAnimation(animation);
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        #endregion
    }
}
