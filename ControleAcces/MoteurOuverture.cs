namespace ControleAcces;

public class MoteurOuverture
{
    private readonly IPorte[] _portes;

    public MoteurOuverture(params IPorte[] portes)
    {
        _portes = portes;
    }

    public void Interroger(params ILecteur[] lecteurs)
    {
        foreach (var lecteur in lecteurs.Where(lecteur => lecteur.BadgeDétecté))
        {
            foreach (var porte in _portes)
                porte.Ouvrir();
        }
    }
}