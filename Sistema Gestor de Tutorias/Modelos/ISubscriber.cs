using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public interface ISubscriber<in TMessage>
    {
        void HandleMessage(TMessage message);
    }
}
