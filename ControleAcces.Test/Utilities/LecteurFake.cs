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
}