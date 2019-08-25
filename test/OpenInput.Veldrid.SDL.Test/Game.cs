namespace OpenInput.Test
{
    class Game : BaseGame
    {
        private readonly VeldridDeviceSet deviceSet;
        private readonly TestContext testContext;

        public Game()
        {
            this.deviceSet = new VeldridDeviceSet();
            this.testContext = new TestContext(this.deviceSet);
        }

        protected override void Draw(Veldrid.CommandList cl)
        {
            // FIXME: Can I get the "last" state from Veldrid?
            this.deviceSet.UpdateSnapshot(this.LastInputSnapshot);

            this.testContext.Update(1f / 60);
            this.testContext.AddImGuiStuff();
        }
    }
}
