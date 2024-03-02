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

        private bool _isRedFlashEmitted;

        public void Flash(bool rouge, bool vert, bool bleu)
        {
            if(rouge)
                _isRedFlashEmitted = true;

            if (!vert && !bleu)
            {
                _isWhiteFlashEmitted = true;
            }
        }

        public bool IsWhiteFlashEmitted()
        {
            return _isWhiteFlashEmitted;
        }

        public bool IsRedFlashEmitted()
        {
            return _isRedFlashEmitted;
        }
    }
}
