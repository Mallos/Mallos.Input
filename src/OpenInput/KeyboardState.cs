namespace OpenInput
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents specific state of a keyboard.
    /// </summary>
    public struct KeyboardState
    {
        public static readonly KeyboardState Empty = new KeyboardState(null);

        /// <summary> 
        /// Gets all the currently pressed keys. 
        /// </summary>
        public Keys[] Keys { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardState"/> struct.
        /// </summary>
        public KeyboardState(Keys[] keys)
        {
            this.Keys = keys ?? new Keys[0];
        }

        /// <summary>
        /// Returns whether a specified key is currently being pressed.
        /// </summary>
        public bool IsKeyDown(Keys key) => this.Keys.Where(e => e == key).Count() == 1;

        /// <summary>
        /// Returns whether a specified key is currently being released.
        /// </summary>
        public bool IsKeyUp(Keys key) => this.Keys.Where(e => e == key).Count() == 0;

        /// <summary>
        /// Compares two KeyboardStates and returns the compared keys.
        /// </summary>
        /// <returns>
        /// Item1: Keys that are in this state and not the other.
        /// Item2: Keys that are in state, but not in this one. 
        /// </returns>
        public Tuple<Keys[], Keys[]> CompareBoth(KeyboardState state)
        {
            if (Keys == null || state.Keys == null)
                return new Tuple<Keys[], Keys[]>(new Keys[] { }, new Keys[] { });

            // Would it be faster to assume the size of the array? then resize it.
            List<Keys> odds1 = new List<Keys>();
            List<Keys> odds2 = new List<Keys>();

            foreach (Keys key in this.Keys)
            {
                if (Array.IndexOf(state.Keys, key) == -1)
                {
                    odds1.Add(key);
                }
            }

            foreach (Keys key in state.Keys)
            {
                if (Array.IndexOf(this.Keys, key) == -1)
                {
                    odds2.Add(key);
                }
            }

            return new Tuple<Keys[], Keys[]>(odds1.ToArray(), odds2.ToArray());
        }

        /// <summary>
        /// Compares two KeyboardStates and returns the compared keys.
        /// </summary>
        /// <returns> Keys that are in this state and not the other. </returns>
        public Keys[] Compare(KeyboardState state)
        {
            if (this.Keys == null || state.Keys == null)
            {
                return new Keys[] { };
            }

            // Would it be faster to assume the size of the array? then resize it.
            List<Keys> odds1 = new List<Keys>();

            foreach (Keys key in this.Keys)
            {
                if (Array.IndexOf(state.Keys, key) == -1)
                {
                    odds1.Add(key);
                }
            }
            
            return odds1.ToArray();
        }

        public override string ToString() => (this.Keys == null) ? "[]" : JoinKeysToString(this.Keys);

        public static string JoinKeysToString(Keys[] keys)
        {
            string result = "[ ";
            if (keys.Length > 0)
            {
                if (keys.Length > 1)
                {
                    for (int i = 0; i < keys.Length - 1; i++)
                    {
                        result += Enum.GetName(typeof(Keys), keys[i]) + ", ";
                    }
                }
                result += Enum.GetName(typeof(Keys), keys[keys.Length - 1]);
            }
            result += " ]";
            return result;
        }
    }
}
