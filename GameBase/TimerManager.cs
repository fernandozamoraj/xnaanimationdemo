using System;
using System.Collections.Generic;
using System.Text;

namespace GameBase
{
    public class TimerManager
    {
        #region Constructor

        private static TimerManager _TheInstance;
        
        public static TimerManager GetTheTimerManager()
        {
            if (_TheInstance == null)
            {
                _TheInstance = new TimerManager();
            }

            return _TheInstance;
        }

        private TimerManager()
        {

        }

        #endregion

        #region Nested Classes
        
        private class Timer
        {
            #region Fields

            float _LifeSpan;
            DateTime _StartTime;

            #endregion Fields

            #region Constructor

            public Timer(DateTime start, float lifeSpan)
            {
                _LifeSpan = lifeSpan;
                _StartTime = start;
            }

            #endregion

            public bool IsAlive
            {
                get
                {
                    bool result = false;

                    if (DateTime.Now >= _StartTime)
                    {
                        TimeSpan span = DateTime.Now - _StartTime;

                        if (span.TotalSeconds <= _LifeSpan)
                        {
                            result = true;
                        }
                    }

                    return result;
                }
            }

            public float TimeRemaining
            {
                get
                {
                    float timeRemaining = 0f;

                    if (DateTime.Now >= _StartTime)
                    {
                        DateTime temp = _StartTime;
                        DateTime endTime = temp.Add(new TimeSpan(0, 0, 0, 0, Convert.ToInt32( _LifeSpan * 1000)));

                        TimeSpan span = endTime - DateTime.Now;
                         timeRemaining = Convert.ToSingle( span.TotalSeconds );
                    }

                    return timeRemaining;
                }
            
            }
        }

        #endregion

        #region Fields

        Dictionary<string, Timer> _Timers = new Dictionary<string,Timer>();
        
        #endregion

        #region Methods

        public void ClearAllTimers()
        {
            _Timers.Clear();
        }

        public void StarTimer(string timerName, float lifeSpan)
        {
            if (_Timers.ContainsKey(timerName))
            {
                _Timers.Remove(timerName);
            }

            _Timers.Add(timerName, new Timer(DateTime.Now, lifeSpan));

        }

        public bool IsAlive(string timerName)
        {
            bool result = false;

            if (_Timers.ContainsKey(timerName))
            {
                result = _Timers[timerName].IsAlive;
            }

            return result;
        }

        public float TimeRemaining(string timerName)
        {
            float time = 0f;

            if (_Timers.ContainsKey(timerName))
            {
                time = _Timers[timerName].TimeRemaining;
            }

            return time;
        }

        #endregion
    }
}
