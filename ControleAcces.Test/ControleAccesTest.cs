using ControleAcces.Test.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleAcces.Test
{
    public class ControleAccesTest
    {
        #region MVP
        [Fact]
        public void CasNominal()
        {
            // ETANT DONNE un lecteur ayant d�tect� un badge
            // ET une porte lui �tant li�e
            var lecteur = new LecteurFake();
            lecteur.SimulerPr�sentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteur);

            // ALORS cette porte s'ouvre
            Assert.Equal(1, porte.NombreAppelsM�thodeOuvrir);
        }

        [Fact]
        public void CasSansInterrogation()
        {
            // ETANT DONNE un lecteur ayant d�tect� un badge
            // ET une porte lui �tant li�e
            var lecteur = new LecteurFake();
            lecteur.SimulerPr�sentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // ALORS cette porte ne s'ouvre pas
            Assert.Equal(0, porte.NombreAppelsM�thodeOuvrir);
        }

        [Fact]
        public void CasSansPr�sentation()
        {
            // ETANT DONNE un lecteur
            // ET une porte lui �tant li�e
            var lecteur = new LecteurFake();
            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteur);

            // ALORS cette porte ne s'ouvre pas
            Assert.Equal(0, porte.NombreAppelsM�thodeOuvrir);
        }

        [Fact]
        public void CasPr�sentationPuisRien()
        {
            // ETANT DONNE un lecteur ayant d�tect� un badge
            // ET une porte lui �tant li�e
            var lecteur = new LecteurFake();
            lecteur.SimulerPr�sentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur deux fois
            moteur.Interroger(lecteur);
            moteur.Interroger(lecteur);

            // ALORS cette porte s'ouvre une fois
            Assert.Equal(1, porte.NombreAppelsM�thodeOuvrir);
        }

        [Fact]
        public void CasPlusieursPortes()
        {
            // ETANT DONNE un lecteur ayant d�tect� un badge
            // ET deux portes lui �tant li�es
            var lecteur = new LecteurFake();
            lecteur.SimulerPr�sentationBadge();

            var porte1 = new PorteSpy();
            var porte2 = new PorteSpy();
            var moteur = new MoteurOuverture(porte1, porte2);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteur);

            // ALORS ces portes s'ouvrent
            Assert.Equal(1, porte1.NombreAppelsM�thodeOuvrir);
            Assert.Equal(1, porte2.NombreAppelsM�thodeOuvrir);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CasPlusieursLecteurs(int indexLecteurD�tectantBadge)
        {
            // ETANT DONNE deux lecteurs
            // ET que le second a d�tect� un badge
            // ET une porte lui �tant li�e
            var lecteurs = new [] {new LecteurFake(), new LecteurFake()};
            lecteurs[indexLecteurD�tectantBadge].SimulerPr�sentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteurs.Cast<ILecteur>().ToArray());

            // ALORS cette porte s'ouvre une fois
            Assert.Equal(1, porte.NombreAppelsM�thodeOuvrir);
        }
        #endregion

        #region Feature 1
        //Le lecteur poss�de une LED, accessible via ILecteur en appelant la m�thode void Flash(bool r, bool g, bool b)
        //Si un badge est d�tect�, un flash blanc est �mis.
        [Fact]
        public void CasBadgeDetecte_FlashEmis()
        {
            // Etant donn� un lecteur avec un badge d�tect�
            var lecteur = new LecteurFake();
            lecteur.SimulerPr�sentationBadge();
            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);
            var lecteurLed = new LedSpy();
            var lecteurWithLed = new LecteurAvecLed(lecteur, lecteurLed);

            // Quand la m�thode Flash est appel�e
            lecteurWithLed.Flash(false, false, false);

            // Un flash blanc est �mis
            Assert.True(lecteurLed.IsWhiteFlashEmitted());
        }

        [Fact] 
        public void CasPorteFermee_FlashRouge()
        {
            // Etant donn� une porte ferm�e
            var porte = new PorteSpy();
            var ledSpy = new LedSpy();
            var lecteurAvecLed = new LecteurAvecLed(new LecteurFake(), ledSpy);

            // Quand la m�thode flash est appel�e
            lecteurAvecLed.Flash(true, false, false);

            // Le flash rouge est �mis
            Assert.True(ledSpy.IsRedFlashEmitted());
        }

        #endregion

        #region Feature 2
        //Le lecteur poss�de un Bipper accessible via la m�thode void Bip()
        //Si la porte s'ouvre, un Bip et un flash vert sont �mis.
        //Si elle ne s'ouvre pas, deux Bips et un flash rouge sont �mis.
        //Si une erreur se produit, un Bip et un flash violet.

        [Fact]
        public void CasPorteOuverte_BipEtFlashVertEmis()
        {
            // Etant donn� une porte qui s'ouvre
            var porte = new PorteSpy() { EstOuverte = true };
            var ledSpy = new LedSpy();
            var bipperSpy = new BipperSpy();
            var lecteur = new LecteurFake(porte.EstOuverte, bipperSpy, ledSpy);

            // Quand le Bip est appel�
            lecteur.Bip();

            // Alors un bip est �mis et un flash vert est �mis
            Assert.True(bipperSpy.IsBipEmitted);
            Assert.True(ledSpy.IsGreenFlashEmitted());
        }

        [Fact]
        public void CasPorteFermee_DoubleBipEtFlashRouge()
        {
            // Etant donn� une porte ferm�e
            var porte = new PorteSpy() { EstOuverte = false };
            var ledSpy = new LedSpy();
            var bipperSpy = new BipperSpy();
            var lecteur = new LecteurFake(porte.EstOuverte, bipperSpy, ledSpy);

            // Quand le Bip est appel�
            lecteur.Bip();

            // Alors un double bip est �mis et un flash rouge est �mis
            Assert.True(bipperSpy.IsDoubleBipEmitted);
            Assert.True(ledSpy.IsRedFlashEmitted());
        }

        [Fact]
        public void CasErreur_BipEtFlashVioletEmis()
        {
            // Etant donn� une erreur qui se produit
            var ledSpy = new LedSpy();
            var bipperSpy = new BipperSpy();
            var lecteur = new LecteurFake(porteEstOuverte: true, bipperSpy); // Peu importe l'�tat de la porte ici, car c'est l'erreur qui doit d�clencher le bip violet

            // Quand le Bip est appel�
            lecteur.SimulerErreur(); // Simuler une erreur
            lecteur.Bip();

            // Alors un bip et un flash violet sont �mis
            Assert.True(bipperSpy.IsBipEmitted);
            Assert.True(ledSpy.IsVioletFlashEmitted);
        }
        #endregion
    }
}