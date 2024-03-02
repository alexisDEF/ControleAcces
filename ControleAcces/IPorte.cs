namespace ControleAcces;

public interface IPorte
{
    bool EstOuverte { get; set; }
    void Ouvrir();
}