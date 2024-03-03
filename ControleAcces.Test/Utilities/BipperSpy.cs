using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAcces.Test.Utilities
{
    public class BipperSpy : IBipper
    {
        public bool IsBipEmitted { get; private set; } = false;
        public bool IsDoubleBipEmitted { get; private set; } = false;

        public void EmitBip()
        {
            IsBipEmitted = true;
        }

        public void EmitDoubleBip()
        { IsDoubleBipEmitted = true; }

        public void Bip()
        {

        }
    }
}
