using System;
using System.ComponentModel;
using System.Drawing;

namespace sorec_gamma.modules.UTILS
{
    public class HorsServiceEventArgs : ProgressChangedEventArgs
    {
        public HorsServiceEventArgs(int progressPercentage, object userState)
            : base(progressPercentage, userState)
        {
        }

        public Color color { get; set; }
        public string terminalStatus { get; set; }
        public string errorMsg { get; set; }
        public bool horsServiceVisibility { get; set; }
        public bool printerError { get; set; }
    }
}
