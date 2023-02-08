using sorec_gamma;

namespace CS_CLIB
{
    public class Tracing
    {
        public Tracing()
        {
        }
        public Tracing(string relpath)
        {
        }

        public void addLog(string line, int MsgType)
        {
            if (MsgType == 1)
            {
                ApplicationContext.Logger.Info(line);
            }
            else
            {
                ApplicationContext.Logger.Error(line);
            }
        }
        
    }
}
