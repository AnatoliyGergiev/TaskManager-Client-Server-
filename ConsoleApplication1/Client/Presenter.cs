using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Presenter
    {
        Model _model = new Model();
        IView _view;
        public SynchronizationContext uiContext;

        public Presenter(IView view)
        {
            _view = view;
            _view.OnConnect+= new EventHandler<string>(ConnectMethod);
            _view.OnExchange+=new EventHandler<string>(ExchangeMethod);
            _view.OnReceive+= new EventHandler(ReceiveMethod);
            _view.OnDisconnect += new EventHandler(DisconnectMethod);
            uiContext = SynchronizationContext.Current;
        }
        public async void ConnectMethod(object sender, string s)
        {
            await Task.Run(() =>
            {
                uiContext.Send(d => _view.Message(_model.Connect(s)), null);
            });
        }
        public async void ExchangeMethod(object sender, string s)
        {
            await Task.Run(() =>
            {
                _model.Exchange(s);
            });
        }
        public async void ReceiveMethod(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                foreach (var t in _model.Receive())//ls)
                    uiContext.Send(d => _view.ListBox1.Items.Add(t), null);
                uiContext.Send(d => _view.ListBox1.Items.RemoveAt(0), null);
            });
        }
        public void DisconnectMethod(object sender, EventArgs e)
        {
            _model.Disconnect();
        }
    }
}
