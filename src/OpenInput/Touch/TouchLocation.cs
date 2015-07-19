namespace OpenInput.Touch
{
    using System;

    /// <summary>
    /// Provides methods and properties for interacting with a touch location 
    /// on a touch screen device.
    /// </summary>
    public struct TouchLocation : IEquatable<TouchLocation>
    {
        // TODO: Add previus position and state

        /// <summary> Gets the ID of the touch location. </summary>
        public int Id { get; internal set; }

        /// <summary> Gets the x-position of the touch location. </summary>
        public int X { get; internal set; }

        /// <summary> Gets the y-position of the touch location. </summary>
        public int Y { get; internal set; }

        /// <summary> Gets the state of the touch location. </summary>
        public TouchLocationState State { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchLocation"/> struct.
        /// </summary>
        public TouchLocation(int id, TouchLocationState state, int x, int y)
        {
            this.Id = id;
            this.State = state;
            this.X = x;
            this.Y = y;
        }

        public static bool operator !=(TouchLocation value1, TouchLocation value2)
        {
            return (value1.Id != value2.Id) && (value1.X != value2.X) && (value1.Y != value2.Y) && (value1.State != value2.State);
        }

        public static bool operator ==(TouchLocation value1, TouchLocation value2)
        {
            return (value1.Id == value2.Id) && (value1.X == value2.X) && (value1.Y == value2.Y) && (value1.State == value2.State);
        }

        public override bool Equals(object obj) => (obj is TouchLocation) && this.Equals((TouchLocation)obj);
        public bool Equals(TouchLocation other)
        {
            return (this.Id == other.Id) && (this.X == other.X) && (this.Y == other.Y) && (this.State == other.State);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
