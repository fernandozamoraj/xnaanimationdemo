using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace AnimationDemo
{
    public class GameSettings
    {
        #region Fields

        private string _ImagesPath = string.Empty;
        private string _SoundPath = string.Empty;
        
        #endregion

        #region Properties

        public string ImagesPath
        {
            get
            {
                return _ImagesPath;
            }
        }

        public string SoundPath
        {
            get
            {
                return _SoundPath;
            }
        }
        #endregion



        private static GameSettings _GameSettings = null;

        public static GameSettings GetGameSettings()
        {
            if (_GameSettings == null)
            {
                _GameSettings = new GameSettings();
            }

            return _GameSettings;
        }

        #region Constructor

        private GameSettings()
        {
            //Form whatever reason images require path without the full path
            //while sounds require the full path
            //beyond me at this point
            _ImagesPath = "Data/Images/";
            _SoundPath = "Data\\Sounds\\";
        
        }

        private string GetApplicationPath()
        {
            // This is the full directory and exe name
            String fullAppName = Assembly.GetExecutingAssembly().GetName().CodeBase;

            // This strips off the exe name
            String fullAppPath = Path.GetDirectoryName(fullAppName);

            return fullAppPath;
        }
        #endregion
    }
}
