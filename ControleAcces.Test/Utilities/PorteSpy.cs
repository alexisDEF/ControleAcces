namespace ControleAcces.Test.Utilities;

internal class PorteSpy : IPorte
{
    public ushort NombreAppelsMéthodeOuvrir { get; private set; }

    public bool EstOuverte { get; set; } = false;

    public void Ouvrir()
    {
        NombreAppelsMéthodeOuvrir++;
        EstOuverte &= !EstOuverte;
    }
}