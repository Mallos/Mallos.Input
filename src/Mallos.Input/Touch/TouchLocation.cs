namespace Mallos.Input.Touch
{
    using System;
    using System.Numerics;
    
    /// <summary>
    /// Provides methods and properties for interacting with a touch location 
    /// on a touch screen device.
    /// </summary>
    public struct TouchLocation : IEquatable<TouchLocation>
    {
        // TODO: Add previus position and state

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchLocation"/> struct.
        /// </summary>
        public TouchLocation(int id, TouchLocationState state, Vector2 position)
        {
            this.Id = id;
            this.State = state;
            this.Position = position;
        }

        /// <summary>
        /// Gets the ID of the touch location.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the position of the touch location.
        /// </summary>
        public Vector2 Position { get; }
        
        /// <summary>
        /// Gets the state of the touch location.
        /// </summary>
        public TouchLocationState State { get; }

        public static bool operator !=(TouchLocation value1, TouchLocation value2)
            => (value1.Id != value2.Id)
            && (value1.Position != value2.Position)
            && (value1.State != value2.State);

        public static bool operator ==(TouchLocation value1, TouchLocation value2)
            => (value1.Id == value2.Id)
            && (value1.Position == value2.Position)
            && (value1.State == value2.State);

        public override bool Equals(object obj)
            => (obj is TouchLocation) && this.Equals((TouchLocation)obj);

        public bool Equals(TouchLocation other)
            => (this.Id == other.Id)
            && (this.Position == other.Position)
            && (this.State == other.State);

        public override int GetHashCode() => this.Id;
    }
}
