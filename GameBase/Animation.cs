using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FzGameBase
{
    public class Animation
    {
        #region Fields

        private List<Texture2D> _TextTures = new List<Texture2D>();
        private ContentManager _ContentManager;
        private bool _ContinuosLoop;
        private int  _CurrentFrame = -1;
        private double _LastFrameIncrement = -1;
        private double _FrameInterval = -1;
        private float _Scale = 1;

        #endregion

        #region Properties

        public bool IsActive
        {
            get
            {
                return _CurrentFrame > -1;
            }
        }

        public float Scale
        {

            get
            {
                return _Scale;
            }
        }
        #endregion

        #region Constructors

        public Animation(ContentManager contentManager, bool continuosLoop, double frameInterval, float scale)
        {
            _ContinuosLoop = continuosLoop;
            _FrameInterval = (frameInterval > 0 ? frameInterval : 100);
             _ContentManager = contentManager;
             _Scale = scale;
        }

        #endregion

        #region Methods

        public void Activate()
        {
            _CurrentFrame = 0;
        }

        public void DeActivate()
        {
            _CurrentFrame = -1;
        }

        /// <summary>
        /// Gets the current frame
        /// The idea with current frame and next frame is 
        /// </summary>
        /// <returns></returns>
        public Texture2D GetCurrentFrame()
        {
            if (_CurrentFrame > -1 && _CurrentFrame < _TextTures.Count)
            {
                return _TextTures[_CurrentFrame];
            }

            return null;
        }

        /// <summary>
        /// Gets Next Frame if enough time has elapsed otherwise it 
        /// returns the current frame
        /// </summary>
        /// <param name="gameTime">Game Time in seconds</param>
        /// <returns></returns>
        public Texture2D GetNextFrame(double gameTime)
        {
            Texture2D result = null;

            if (IsActive)
            {
                if (_LastFrameIncrement < 0)
                {
                    _LastFrameIncrement = gameTime;
                }

                double interval = gameTime - _LastFrameIncrement;

                //Increment the frame only once enough time has elapsed
                if (interval >= _FrameInterval)
                {
                    _LastFrameIncrement = gameTime;
                    _CurrentFrame++;
                }

                //Adjust the current frame if exceeds upper limit
                if (_CurrentFrame + 1 > _TextTures.Count)
                {
                    if (_ContinuosLoop)
                    {
                        _CurrentFrame = 0;
                    }
                    else
                    {
                        _CurrentFrame = -1;
                    }
                }

                if (_CurrentFrame > -1)
                {
                    result = _TextTures[_CurrentFrame];
                }
            }

            return result;
        }

        public void AddTexture(string assetName)
        {
            Texture2D newTextTure =  _ContentManager.Load<Texture2D>(assetName);
            _TextTures.Add(newTextTure);
        }

        #endregion
    }
}
