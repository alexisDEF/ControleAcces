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

        private bool _isGreenFlashEmitted;

        private bool _isBlueFlashEmitted;

        public void Flash(bool rouge, bool vert, bool bleu)
        {
            if (rouge)
                _isRedFlashEmitted = true;

            else if (vert)
                _isGreenFlashEmitted = true;

            else if (bleu)
                _isBlueFlashEmitted = true;

            else
                _isWhiteFlashEmitted = true;
        }
        public bool IsGreenFlashEmitted()
        {
            return _isGreenFlashEmitted;
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
