namespace ControleAcces.Test.Utilities;

internal class LecteurFake : ILecteur, IBipper, ILed
{
    private bool _badgeDétectéAuProchainAppel;
    private bool _erreur;
    private readonly bool _porteEstOuverte;
    private readonly BipperSpy _bipperSpy;
    private readonly LedSpy _ledSpy;

    public LecteurFake(bool porteEstOuverte = false, BipperSpy bipperSpy = null, LedSpy ledSpy = null)
    {
        _porteEstOuverte = porteEstOuverte;
        _bipperSpy = bipperSpy;
        _ledSpy = ledSpy;
    }

    public bool BadgeDétecté
    {
        get
        {
            var réponse = _badgeDétectéAuProchainAppel;
            _badgeDétectéAuProchainAppel = false;
            return réponse;
        }
    }

    public void SimulerPrésentationBadge()
    {
        _badgeDétectéAuProchainAppel = true;
    }

    public void Bip()
    {
        if (_erreur)
        {
            // Bip violet en cas d'erreur
            Flash(false, false, true); // Flash violet en cas d'erreur
        }
        else
        {
            // Bip normal en fonction de l'état de la porte
            if (_porteEstOuverte)
            {
                // Bip simple et flash vert si la porte est ouverte
                _bipperSpy.EmitBip();
                _ledSpy.Flash(false, true, false); // Flash vert
            }
            else
            {
                // Bip double et flash rouge si la porte est fermée
                _bipperSpy.EmitDoubleBip();
                _ledSpy.Flash(true, false, false); // Flash rouge
            }
        }
    }

    public void SimulerErreur()
    {
        _erreur = true;
    }

    public void Flash(bool rouge, bool vert, bool bleu)
    {
    }
}