#region Using Statements
using System;
using System.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using AnimationDemo.Goblins;
using GameBase;

#endregion

namespace AnimationDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields

        int _Lives = 4;

        public GameState GameState;
        GraphicsDeviceManager _Graphics;
        ContentManager _Content;
        SpriteBatch _SpriteBatch;
        Texture2D _BackgroundTexture;
        Texture2D _LivesTexture;
        static PacmanSprite _Player;
        PacmanSprite _Blinky;
        PacmanSprite _Pinky;
        PacmanSprite _Clyde;
        PacmanSprite _Inky;
        public Maze TheMaze;
        private DateTime _PowerModeStarted = new DateTime(DateTime.Now.Year -1, DateTime.Now.Month, DateTime.Now.Day);
        TimerManager _TimerManager = TimerManager.GetTheTimerManager();
        private string TimerNameForPowerMode = "POWERMODE";
        private string TimerNameForStartLife = "STARTLIFE";
        private SoundPlayer _SoundPacChomp;
        private SoundPlayer _SoundGhostEaten;
        private SoundPlayer _SoundExtraPac;
        private SoundPlayer _SoundKilled;
       
        public bool IsInPowerMode 
        {
            get
            {
                return _TimerManager.IsAlive(TimerNameForPowerMode);
            }            
        }

        #endregion

        public static PacmanSprite Player
        {
            get
            {
                return _Player;
            }
        }

        #region Constructor

        public Game1()
        {
            GameState = GameState.Playing;
            _Graphics = new GraphicsDeviceManager(this);
            _Graphics.PreferredBackBufferHeight = 700;
            _Graphics.PreferredBackBufferWidth = 700;
            _Content = new ContentManager(Services);
            _Player = new PacmanSprite(9*32, 16*32, Window.ClientBounds.Width, Window.ClientBounds.Height, Keys.Left, Keys.Right, Keys.Up, Keys.Down);
            _Blinky = new GhostBase(SpriteType.Blinkey, 9 * 32, 6 * 32, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _Pinky = new GhostBase(SpriteType.Pinkey, 9 * 32, 6 * 32, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _Clyde = new GhostBase(SpriteType.Clyde, 9 * 32, 6 * 32, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _Inky = new GhostBase(SpriteType.Inky, 9 * 32, 6 * 32, Window.ClientBounds.Width, Window.ClientBounds.Height);
            
            TheMaze = new Maze();

            TheMaze.OnAtePowerPellet += new EventHandler(TheMaze_OnAtePowerPellet);
            TheMaze.OnAtePellet += new EventHandler(TheMaze_OnAtePellet);
        }


        #endregion

        void TheMaze_OnAtePowerPellet(object sender, EventArgs e)
        {
            _TimerManager.StarTimer(TimerNameForPowerMode, 7f);

            _Player.IncreaseSpeed();
            _SoundPacChomp.Play();
        }

        void TheMaze_OnAtePellet(object sender, EventArgs e)
        {
            _SoundPacChomp.Play();
        }


        #region Static Methods
        private static Game1 _TheGame;

        public static Game1 GetTheGameInstance()
        {
            if (_TheGame == null)
            {
                _TheGame = new Game1();
            }

            return _TheGame;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _SpriteBatch = new SpriteBatch(_Graphics.GraphicsDevice);
            _Player.SpriteBatch = _SpriteBatch;
            _Blinky.SpriteBatch = _SpriteBatch;
            _Pinky.SpriteBatch = _SpriteBatch;
            _Clyde.SpriteBatch = _SpriteBatch; 
            _Inky.SpriteBatch = _SpriteBatch;

            GameSettings settings = GameSettings.GetGameSettings();
            _SoundPacChomp = new SoundPlayer(settings.SoundPath + "pacchomp.wav");
            _SoundGhostEaten = new SoundPlayer(settings.SoundPath + "GHOSTEATEN.wav");
            _SoundExtraPac  = new SoundPlayer(settings.SoundPath + "extrapac.wav");
            _SoundKilled = new SoundPlayer(settings.SoundPath + "killed.wav");

            

            base.Initialize();
        }


        /// <summary>
        /// Load your graphics content.  If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                GameSettings settings = GameSettings.GetGameSettings();
                _BackgroundTexture = _Content.Load<Texture2D>(settings.ImagesPath + "backdrop00");
                _LivesTexture = _Content.Load<Texture2D>(settings.ImagesPath + "Pacman/PACMAN_LEFT1");
               
            }

            _Player.Load(_Content);
            _Blinky.Load(_Content);
            _Pinky.Load(_Content);
            _Clyde.Load(_Content);
            _Inky.Load(_Content);
            TheMaze.Load(_Content);
        }


        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
                // TODO: Unload any ResourceManagementMode.Automatic content
                _Content.Unload();
            }

            // TODO: Unload any ResourceManagementMode.Manual content
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (_TimerManager.IsAlive(TimerNameForStartLife))
            {
                return;
            }

            _Player.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            _Blinky.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            _Pinky.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            _Clyde.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            _Inky.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);

            if (!TheMaze.CheckIfPelletsRemain())
            {
                TheMaze.InitMaze();
                _Inky.ResetAfterKill();
                _Blinky.ResetAfterKill();
                _Pinky.ResetAfterKill();
                _Clyde.ResetAfterKill();
                _Player.ResetAfterKill();
                _TimerManager.ClearAllTimers();
                _TimerManager.StarTimer(TimerNameForStartLife, 1f);
            }

            TheMaze.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            
            DetectCollisions();

            if (gameTime.TotalGameTime.TotalSeconds < .5f)
            {
                _TimerManager.StarTimer(TimerNameForStartLife, 2);
            }

            base.Update(gameTime);
        }

        //Simple collision detection for now 
        //TODO: Needs to be a little more refined so that sprites actually touch
        //Also need to play collision animation
        private void DetectCollisions()
        {
            if (_Lives < 1)
                return;

            if (TheMaze.SpritesCollide(_Player, _Blinky) ||
                TheMaze.SpritesCollide(_Player, _Pinky) ||
                TheMaze.SpritesCollide(_Player, _Inky) ||
                TheMaze.SpritesCollide(_Player, _Clyde))
            {
                if (!IsInPowerMode && _Lives  > 0)
                {
                    _SoundKilled.Play();
                    _Lives--;
                    _Player.ResetAfterKill();
                    _Pinky.ResetAfterKill();
                    _Inky.ResetAfterKill();
                    _Clyde.ResetAfterKill();
                    _Blinky.ResetAfterKill();
                    _TimerManager.StarTimer(TimerNameForStartLife, 2f);
                }
                else
                {
                    bool killedGhost = false;

                    if (TheMaze.SpritesCollide(_Player, _Blinky))
                    {
                        _Blinky.ResetAfterKill();
                        killedGhost = true;
                    }
                    if (TheMaze.SpritesCollide(_Player, _Pinky))
                    {
                        _Pinky.ResetAfterKill();
                        killedGhost = true;
                    }
                    if (TheMaze.SpritesCollide(_Player, _Inky))
                    {
                        _Inky.ResetAfterKill();
                        killedGhost = true;
                    }
                    if (TheMaze.SpritesCollide(_Player, _Clyde))
                    {
                        _Clyde.ResetAfterKill();
                        killedGhost = true;
                    }
                    if (killedGhost)
                    {
                        _SoundGhostEaten.Play();
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawBackground();
            TheMaze.Draw(_SpriteBatch);

            if (_Lives > 0)
            {
                _Player.Draw();
            }


            if (_TimerManager.IsAlive(TimerNameForPowerMode))
            {
                float timeRemaining = _TimerManager.TimeRemaining(TimerNameForPowerMode);
                
                if(timeRemaining <= 2f)
                {
                    //Create a flash effect when time remaining is 2 seconds or less
                    int intTimeRemaing = Convert.ToInt32( timeRemaining * 1000);

                    int tempVal = intTimeRemaing % 250;

                    if(tempVal > 125)
                    {
                        _Blinky.Draw();
                        _Pinky.Draw();
                        _Clyde.Draw();
                        _Inky.Draw();
                    }
                }
                else
                {
                    _Blinky.Draw();
                    _Pinky.Draw();
                    _Clyde.Draw();
                    _Inky.Draw();
                }
            }
            else
            {
                _Blinky.Draw();
                _Pinky.Draw();
                _Clyde.Draw();
                _Inky.Draw();            
            }
            DrawDashBoard(_SpriteBatch);

            base.Draw(gameTime);
        }

        private void DrawDashBoard(SpriteBatch spriteBatch)
        {

            _SpriteBatch.Begin();
            for (int i = 0; i < _Lives; i++)
            {
                int x = (19 * 32) + (i * 16) + 2;
                int y = 32;
                _SpriteBatch.Draw(_LivesTexture, new Rectangle(x, y, _LivesTexture.Width/2, _LivesTexture.Height/2), Color.White);
            
            }
            _SpriteBatch.End();
            
            
        }

        private void DrawBackground()
        {
            _SpriteBatch.Begin();
           // _SpriteBatch.Draw(_BackgroundTexture, new Rectangle(0, 0, Window.ClientBounds.Right, Window.ClientBounds.Bottom), Color.White);
            _SpriteBatch.End();
        }

        #endregion
    }
}
