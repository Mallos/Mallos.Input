namespace OpenInput
{
    public abstract partial class Device
    {
        public string Name { get; private set; }
        
        protected Device(string name)
        {
            this.Name = name;
        }
    }
}
