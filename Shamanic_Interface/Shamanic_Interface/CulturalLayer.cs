using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shamanic_Interface
{
    class CulturalPair
    {

        private string action { get; set; }
        private string culture { get; set; }

        public CulturalPair(string action, string culture)
        {
            this.action = action;
            this.culture = culture;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CulturalPair pair = (CulturalPair)obj;
            if ((System.Object)pair == null)
            {
                return false;
            }

            return (this.action.Equals(pair.action) && this.culture.Equals(pair.culture));
        }

        public static bool operator ==(CulturalPair a, CulturalPair b)
        {
            return (a.action == b.action && a.culture == b.culture);
        }

        public static bool operator !=(CulturalPair a, CulturalPair b)
        {
            return (a.action != b.action || a.culture != b.culture);
        }

        public override int GetHashCode()
        {
            return action.GetHashCode() ^ culture.GetHashCode();
        }
    }

    public class CulturalLayer
    {

        private Dictionary<CulturalPair, string> culturalGestures;
        private Dictionary<string, string> customGestures;

        public CulturalLayer()
        {
            culturalGestures = new Dictionary<CulturalPair, string>();
            customGestures = new Dictionary<string, string>();
        }

        public void AddCustomGesture(string action, string gestureName)
        {
            try
            {
                customGestures.Add(action, gestureName);
            }
            catch (System.ArgumentException e)
            {
                customGestures[action] = gestureName;
            }
        }

        public void AddCultureGesture(string action, string culture, string gestureName)
        {
            if (!customGestures.ContainsKey(action))
            {
                AddCustomGesture(action, gestureName);
            }

            CulturalPair cultPair = new CulturalPair(action, culture);
            try
            {
                culturalGestures.Add(cultPair, gestureName);
            }
            catch (ArgumentException e)
            {
                culturalGestures[cultPair] = gestureName;
            }
        }

        public string GetGestureName(string action, string culture)
        {
            CulturalPair pair = new CulturalPair(action, culture);
            string value;
            if (culturalGestures.TryGetValue(pair, out value))
            {
                return value;
            }
            return customGestures[action];
        }


        public List<string> GetGesturesNames(List<string> actions, string culture)
        {
            return actions.ConvertAll(action => GetGestureName(action, culture));
        }
    }
}
