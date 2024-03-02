namespace ControleAcces.Test.Utilities;

internal class LecteurFake : ILecteur
{
    private bool _badgeDétectéAuProchainAppel;

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
    public void Flash(bool rouge, bool vert, bool bleu)
    {
        // Simuler l'émission d'un flash blanc
        if (!rouge && !vert && !bleu)
        {
            // Ne fait rien dans ce contexte
        }
    }
}