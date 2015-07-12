namespace OpenInput
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct KeyboardState
    {
        public Keys[] Keys { get; internal set; }

        public KeyboardState(Keys[] keys)
        {
            this.Keys = keys;
        }

        public Keys[] GetPressedKeys()
        {
            return Keys;
        }

        public bool IsKeyDown(Keys key)
        {
            return Keys.Where(e => e == key).Count() == 1;
        }

        public bool IsKeyUp(Keys key)
        {
            return Keys.Where(e => e == key).Count() == 0;
        }

        public Tuple<Keys[], Keys[]> Compare(KeyboardState state)
        {
            if (state.Keys == null)
                return new Tuple<Keys[], Keys[]>(new Keys[] { }, new Keys[] { });

            // Would it be faster to assume the size of the array? then resize it.
            var odds1 = new List<Keys>();
            var odds2 = new List<Keys>();

            foreach (var key in Keys)
            {
                if (Array.IndexOf(state.Keys, key) == -1)
                    odds1.Add(key);
            }

            foreach (var key in state.Keys)
            {
                if (Array.IndexOf(Keys, key) == -1)
                    odds2.Add(key);
            }

            return new Tuple<Keys[], Keys[]>(odds1.ToArray(), odds2.ToArray());
        }

        public override string ToString()
        {
            return JoinKeysToString(Keys);
        }

        public static string JoinKeysToString(Keys[] keys)
        {
            string result = "{ ";
            if (keys.Length > 0)
            {
                if (keys.Length > 1)
                {
                    for (int i = 0; i < keys.Length - 1; i++)
                        result += Enum.GetName(typeof(Keys), keys[i]) + ", ";
                }
                result += Enum.GetName(typeof(Keys), keys[keys.Length - 1]);
            }
            result += " }";
            return result;
        }
    }
}
