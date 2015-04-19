using System;
using System.Collections.Generic;
using System.Text;
using FzGameBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimationDemo.Goblins
{
    public class GhostBase : PacmanSprite
    {
        #region Constructor

        //Use this constructor for the goblin
        public GhostBase(SpriteType spriteType, int x, int y, int maxXPos, int maxYPos):base(x, y, maxXPos, maxXPos)
        {
            _SpriteType = spriteType;
            _CurrentDirection = Direction.Left;
            _Speed = 2;
        }

        #endregion

        #region Fields

        

        #endregion

        
        

        

        protected virtual void GetKeyStates()
        {
            _LeftKeyDown = _RightKeyDown = _UpKeyDown = _DownKeyDown = false;

            PerformAI();
        }

        public override void ResetAfterKill()
        {
//            Location = new Vector2(9 * 32, 6 * 32);
            Location = new Vector2(16 * 32, 16 * 32);
            _MovesQueue.Clear();
        }

        /// <summary>
        /// Tracks PacMan
        /// The more moves the least likely it is to catch pacman because he could be long
        /// gone. Shorter move spans mean more frequent adjustments
        /// </summary>
        /// <param name="maxMoves"></param>
        protected virtual void PerformAdvancedAI(int maxMoves, bool guardExits)
        {
            _LeftKeyDown = _RightKeyDown = _UpKeyDown = _DownKeyDown = false;

            if (_MovesQueue.Count < 1)
            {
                if (_TheGame.IsInPowerMode)
                {
                    //This code is to get away as far as possible from power pacman
                    float x = 0f;
                    float y = 0f;

                    if (Game1.Player.Location.X < 32 * 8)
                    {
                        x = 16 * 32;
                    }

                    if (Game1.Player.Location.Y < 32 * 10)
                    {
                        y = 20 * 32;
                    }

                    Vector2 target = new Vector2(x, y);

                    _MovesQueue = _TheGame.TheMaze.FindPath(this.Location, target, maxMoves);
                }
                else if (guardExits)
                {
                    //This AI guards the exit points
                    //This code is to get away as far as possible from power pacman
                    float x = 0f;
                    float y = 0f;

                    if (Location.X < 32 * 8)
                    {
                        x = 16 * 32;
                    }

                    x = _RandomNumberGenerator.Next(32 * 16);
                    y = _RandomNumberGenerator.Next(32 * 20);

                    Vector2 target = new Vector2(x, y);

                    _MovesQueue = _TheGame.TheMaze.FindPath(this.Location, target, maxMoves);
                }
                else
                {
                    _MovesQueue = _TheGame.TheMaze.FindPath(this.Location, Game1.Player.Location, maxMoves);
                }
            }

            if (_MovesQueue.Count < 1)
                return;

            _Target = _MovesQueue.Peek();

            Vector2 currentPosition = _TheGame.TheMaze.GetRowAndCol(this.Location);

            if (_Target.X == currentPosition.X && _Target.Y == currentPosition.Y)
            {
                _MovesQueue.Dequeue();
                if (_MovesQueue.Count > 0)
                {
                    _Target = _MovesQueue.Peek();
                }
                else
                {
                    return;
                }
            }

            //Old logic used to call for > and < comparison
            //but that was inneficient because if the diff value
            //was greater than 1 we could end up stuck
            //Furthermore only vertical and horizontal moves are allowed
            if (_Target.X - 1 == currentPosition.X && _Target.Y == currentPosition.Y)
            {
                _RightKeyDown = true;
            }
            else if (_Target.X + 1 == currentPosition.X && _Target.Y == currentPosition.Y)
            {
                _LeftKeyDown = true;
            }
            else if (_Target.Y + 1 == currentPosition.Y && _Target.X == currentPosition.X)
            {
                _UpKeyDown = true;
            }
            else if (_Target.Y - 1 == currentPosition.Y && _Target.X == currentPosition.X)
            {
                _DownKeyDown = true;
            }

            //Oh oH we are stuck there fore lets clear the Queue
            if (!_RightKeyDown && !_LeftKeyDown && !_DownKeyDown && !_UpKeyDown)
            {
                if (_MovesQueue.Count > 0)
                {
                    _MovesQueue.Clear();
                }
            }
        }

        protected override void PerformAI()
        {

            if (_TheGame.IsInPowerMode)
            {
                PerformAdvancedAI(3 * (int)_SpriteType, false);
            }

            if (_SpriteType == SpriteType.Pinkey)
            {
                PerformAdvancedAI(10, false);
            }
            else if (_SpriteType == SpriteType.Blinkey)
            {
                PerformAdvancedAI(20, false);
            }
            else
            {
                PerformAdvancedAI((_RandomNumberGenerator.Next(8) + 2) * 2, true);
            }
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            GameSettings gameSettings = GameSettings.GetGameSettings();

            string[] textureNames = null;

            if (_SpriteType == SpriteType.Blinkey)
            {
                textureNames = new string[] { "Blinky/GHOSTA_LEFT", "Blinky/GHOSTA_RIGHT", "Blinky/GHOSTA_UP", "Blinky/GHOSTA_DOWN" };
            }
            if (_SpriteType == SpriteType.Pinkey)
            {
                textureNames = new string[] { "Pinky/GHOSTA_LEFT", "Pinky/GHOSTA_RIGHT", "Pinky/GHOSTA_UP", "Pinky/GHOSTA_DOWN" };
            }
            if (_SpriteType == SpriteType.Clyde)
            {
                textureNames = new string[] { "Clyde/GHOSTA_LEFT", "Clyde/GHOSTA_RIGHT", "Clyde/GHOSTA_UP", "Clyde/GHOSTA_DOWN" };
            }
            if (_SpriteType == SpriteType.Inky)
            {
                textureNames = new string[] { "Inky/GHOSTA_LEFT", "Inky/GHOSTA_RIGHT", "Inky/GHOSTA_UP", "Inky/GHOSTA_DOWN" };
            }

            int i = 0;

            for (i = 0; i < textureNames.Length; i++)
            {

                Animation animation = new Animation(content, true, .08f, 1f);

                for (int j = 1; j <= 2; j++)
                {
                    animation.AddTexture(gameSettings.ImagesPath + textureNames[i] + j.ToString());
                }

                AddAnimation(animation);
            }

            textureNames = new string[] { "BlueGhost/GHOSTA_LEFT", "BlueGhost/GHOSTA_RIGHT", "BlueGhost/GHOSTA_UP", "BlueGhost/GHOSTA_DOWN" };

            for (i = 0; i < textureNames.Length; i++)
            {
                Animation animation = new Animation(content, true, .08f, 1f);

                for (int j = 1; j <= 2; j++)
                {
                    animation.AddTexture(gameSettings.ImagesPath + textureNames[i] + j.ToString());
                }

                AddAnimation(animation);
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
    

}
