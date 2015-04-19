using System;
using System.Collections.Generic;
using System.Text;
using FzGameBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationDemo
{
    public class PowerPellet : Sprite
    {
        public bool IsActive = false;

        public PowerPellet(int x, int y)
        {
            Location.X = x;
            Location.Y = y;
            Size.X = 32;
            Size.Y = 32;
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Animation animation = new Animation(content, true, .4f, 1.0f);

            animation.AddTexture(GameSettings.GetGameSettings().ImagesPath + "Map/Tile33");
            animation.AddTexture(GameSettings.GetGameSettings().ImagesPath + "Map/Tile34");

            AddAnimation(animation);

            SetActiveAnimation(0);

            IsActive = true;
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            base.Update(gameTime, elapsedTime);
        }



        public override void Draw()
        {
            if (_CurrentFrameTexture != null)
            {
                base.Draw();
            }
        }
    
    }
}
