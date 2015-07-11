namespace OpenInput
{
    using System;

    public class TextInput
    {
        public string Result { get; internal set; }

        public bool AllowNewLine { get; set; }

        public TextInput()
        {
            this.Result = string.Empty;

            this.AllowNewLine = false;
        }

        public void Process(KeyPressEventArgs e)
        {
            // TODO: I would like to add support to other letters/symbols like åäö and symbols from other languages
            if (InputHelper.IsLetter(e.Key))
                Result += e.KeyChar;

            if (e.Key == Keys.Back && Result.Length > 0)
            {
                Result = Result.Remove(Result.Length - 1);
            }

            if (AllowNewLine && e.Key == Keys.Enter)
            {
                Result += Environment.NewLine;
            }
        }
    }
}
