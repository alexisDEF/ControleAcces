using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAcces
{
    public interface ILed
    {
        void Flash(bool rouge, bool vert, bool bleu);
    }
}
