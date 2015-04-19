using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace FzGameBase
{
    /// <summary>
    /// Sprite represents a 2d shape that has a location, velocity
    /// </summary>
    public class Sprite
    {
        #region Fields

        public Vector2 Velocity;
        public Vector2 Location;
        public Vector2 Size;
        public List<Animation> _Animations = new List<Animation>();
        protected int _ActiveAnimation = -1;
        protected Texture2D _CurrentFrameTexture;
        protected double _Scale = 1;
        protected SpriteBatch _SpriteBatch;

        #endregion

        #region Animation Methods

        protected Animation GetCurrentAnimation()
        {
            return _Animations[_ActiveAnimation];
        }

        protected void AddAnimation(Animation animation)
        {
            _Animations.Add(animation);

            if (_Animations.Count == 1)
            {
                SetActiveAnimation(0);
            }
        }

        protected void ClearAnimations()
        {
            _Animations.Clear();
        }

        public void SetActiveAnimation(int idx)
        {

            _ActiveAnimation = idx;

            if (_Animations[_ActiveAnimation].IsActive == false)
            {
                _Animations[_ActiveAnimation].Activate();
            }
        }

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch
        {
            get
            {
                return _SpriteBatch;
            }
            set 
            {
                _SpriteBatch = value;
            }
        }

        #endregion

        public Sprite()
        {

        }

        public Sprite(float x, float y, float width, float height)
        {
            Location.X = x;
            Location.Y = y;
            Size.X = width;
            Size.Y = height;
        }

        public virtual void Update(double gameTime, double elapsedTime)
        {
            //Move the sprite based on the velocity
            Location.X += Velocity.X * (float)elapsedTime;
            Location.Y += Velocity.Y * (float)elapsedTime;
            _CurrentFrameTexture = _Animations[_ActiveAnimation].GetNextFrame(gameTime);
        }

        public virtual void Draw()
        {
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_CurrentFrameTexture, new Rectangle((int)(Location.X + .5f), (int)(Location.Y + .5f), (int)Size.X, (int)Size.Y), Color.White);
            //Game1.SpriteBatch.Draw(_CurrentFrameTexture, Location, new Rectangle((int)(Location.X + .5f), (int)(Location.Y + .5f), (int)Size.X, (int)Size.Y), Color.White, 0f, new Vector2(Size.X/2, Size.Y/2), _Animations[_ActiveAnimation].Scale, SpriteEffects.None, .1f);
            _SpriteBatch.End();
        }

        public virtual void Load(ContentManager content) 
        {

            _CurrentFrameTexture = _Animations[_ActiveAnimation].GetCurrentFrame();
        }


    }
}
