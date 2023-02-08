namespace sorec_gamma.modules.UTILS
{
    public class ExecCommandResult
    {
        private bool succeed;
        private string message;

        public string ErrorMessage
        {
            get { return message; }
            set { message = value; }
        }

        public bool Succeed
        {
            get { return succeed; }
            set { succeed = value; }
        }
    }
}
