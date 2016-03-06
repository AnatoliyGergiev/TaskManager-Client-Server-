using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    interface IView
    {
        event EventHandler<string> OnExchange;
        event EventHandler OnReceive;
        event EventHandler<string> OnConnect;
        event EventHandler OnDisconnect;
        ListBox ListBox1 { get; }
        void Message(string msg);
    }
}
