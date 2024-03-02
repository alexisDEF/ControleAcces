using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAcces.Test.Utilities
{
    internal class LedSpy : ILed
    {
        private bool _isWhiteFlashEmitted;

        public void Flash(bool rouge, bool vert, bool bleu)
        {
            if (!rouge && !vert && !bleu)
            {
                _isWhiteFlashEmitted = true;
            }
        }

        public bool IsWhiteFlashEmitted()
        {
            return _isWhiteFlashEmitted;
        }
    }
}
