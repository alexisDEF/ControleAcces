using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAcces.Test.Utilities
{
    internal class LecteurAvecLed : ILecteur
    {
        private readonly ILecteur _lecteur;
        private readonly ILed _led;

        public LecteurAvecLed(ILecteur lecteur, ILed led)
        {
            _lecteur = lecteur;
            _led = led;
        }

        public bool BadgeDétecté => _lecteur.BadgeDétecté;

        public void Flash(bool rouge, bool vert, bool bleu)
        {
            _led.Flash(rouge, vert, bleu);
        }
    }
}
