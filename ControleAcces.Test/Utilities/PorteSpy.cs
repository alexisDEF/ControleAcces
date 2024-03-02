namespace ControleAcces.Test.Utilities;

internal class PorteSpy : IPorte
{
    public ushort NombreAppelsMéthodeOuvrir { get; private set; }

    public void Ouvrir()
    {
        NombreAppelsMéthodeOuvrir++;
    }
}