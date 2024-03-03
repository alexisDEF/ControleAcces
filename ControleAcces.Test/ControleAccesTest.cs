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
            // ETANT DONNE un lecteur ayant détecté un badge
            // ET une porte lui étant liée
            var lecteur = new LecteurFake();
            lecteur.SimulerPrésentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteur);

            // ALORS cette porte s'ouvre
            Assert.Equal(1, porte.NombreAppelsMéthodeOuvrir);
        }

        [Fact]
        public void CasSansInterrogation()
        {
            // ETANT DONNE un lecteur ayant détecté un badge
            // ET une porte lui étant liée
            var lecteur = new LecteurFake();
            lecteur.SimulerPrésentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // ALORS cette porte ne s'ouvre pas
            Assert.Equal(0, porte.NombreAppelsMéthodeOuvrir);
        }

        [Fact]
        public void CasSansPrésentation()
        {
            // ETANT DONNE un lecteur
            // ET une porte lui étant liée
            var lecteur = new LecteurFake();
            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteur);

            // ALORS cette porte ne s'ouvre pas
            Assert.Equal(0, porte.NombreAppelsMéthodeOuvrir);
        }

        [Fact]
        public void CasPrésentationPuisRien()
        {
            // ETANT DONNE un lecteur ayant détecté un badge
            // ET une porte lui étant liée
            var lecteur = new LecteurFake();
            lecteur.SimulerPrésentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur deux fois
            moteur.Interroger(lecteur);
            moteur.Interroger(lecteur);

            // ALORS cette porte s'ouvre une fois
            Assert.Equal(1, porte.NombreAppelsMéthodeOuvrir);
        }

        [Fact]
        public void CasPlusieursPortes()
        {
            // ETANT DONNE un lecteur ayant détecté un badge
            // ET deux portes lui étant liées
            var lecteur = new LecteurFake();
            lecteur.SimulerPrésentationBadge();

            var porte1 = new PorteSpy();
            var porte2 = new PorteSpy();
            var moteur = new MoteurOuverture(porte1, porte2);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteur);

            // ALORS ces portes s'ouvrent
            Assert.Equal(1, porte1.NombreAppelsMéthodeOuvrir);
            Assert.Equal(1, porte2.NombreAppelsMéthodeOuvrir);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CasPlusieursLecteurs(int indexLecteurDétectantBadge)
        {
            // ETANT DONNE deux lecteurs
            // ET que le second a détecté un badge
            // ET une porte lui étant liée
            var lecteurs = new [] {new LecteurFake(), new LecteurFake()};
            lecteurs[indexLecteurDétectantBadge].SimulerPrésentationBadge();

            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);

            // QUAND le moteur d'ouverture interroge ce lecteur
            moteur.Interroger(lecteurs.Cast<ILecteur>().ToArray());

            // ALORS cette porte s'ouvre une fois
            Assert.Equal(1, porte.NombreAppelsMéthodeOuvrir);
        }
        #endregion

        #region Feature 1
        //Le lecteur possède une LED, accessible via ILecteur en appelant la méthode void Flash(bool r, bool g, bool b)
        //Si un badge est détecté, un flash blanc est émis.
        [Fact]
        public void CasBadgeDetecte_FlashEmis()
        {
            // Etant donné un lecteur avec un badge détecté
            var lecteur = new LecteurFake();
            lecteur.SimulerPrésentationBadge();
            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);
            var lecteurLed = new LedSpy();
            var lecteurWithLed = new LecteurAvecLed(lecteur, lecteurLed);

            // Quand la méthode Flash est appelée
            lecteurWithLed.Flash(false, false, false);

            // Un flash blanc est émis
            Assert.True(lecteurLed.IsWhiteFlashEmitted());
        }

        [Fact] 
        public void CasPorteFermee_FlashRouge()
        {
            // Etant donné une porte fermée
            var porte = new PorteSpy();
            var ledSpy = new LedSpy();
            var lecteurAvecLed = new LecteurAvecLed(new LecteurFake(), ledSpy);

            // Quand la méthode flash est appelée
            lecteurAvecLed.Flash(true, false, false);

            // Le flash rouge est émis
            Assert.True(ledSpy.IsRedFlashEmitted());
        }

        #endregion

        #region Feature 2
        //Le lecteur possède un Bipper accessible via la méthode void Bip()
        //Si la porte s'ouvre, un Bip et un flash vert sont émis.
        //Si elle ne s'ouvre pas, deux Bips et un flash rouge sont émis.
        //Si une erreur se produit, un Bip et un flash violet.

        [Fact]
        public void CasPorteOuverte_BipEtFlashVertEmis()
        {
            // Etant donné une porte qui s'ouvre
            var porte = new PorteSpy() { EstOuverte = true };
            var ledSpy = new LedSpy();
            var bipperSpy = new BipperSpy();
            var lecteur = new LecteurFake(porte.EstOuverte, bipperSpy, ledSpy);

            // Quand le Bip est appelé
            lecteur.Bip();

            // Alors un bip est émis et un flash vert est émis
            Assert.True(bipperSpy.IsBipEmitted);
            Assert.True(ledSpy.IsGreenFlashEmitted());
        }

        [Fact]
        public void CasPorteFermee_DoubleBipEtFlashRouge()
        {
            // Etant donné une porte fermée
            var porte = new PorteSpy() { EstOuverte = false };
            var ledSpy = new LedSpy();
            var bipperSpy = new BipperSpy();
            var lecteur = new LecteurFake(porte.EstOuverte, bipperSpy, ledSpy);

            // Quand le Bip est appelé
            lecteur.Bip();

            // Alors un double bip est émis et un flash rouge est émis
            Assert.True(bipperSpy.IsDoubleBipEmitted);
            Assert.True(ledSpy.IsRedFlashEmitted());
        }

        [Fact]
        public void CasErreur_BipEtFlashVioletEmis()
        {
            // Etant donné une erreur qui se produit
            var ledSpy = new LedSpy();
            var bipperSpy = new BipperSpy();
            var lecteur = new LecteurFake(porteEstOuverte: true, bipperSpy); // Peu importe l'état de la porte ici, car c'est l'erreur qui doit déclencher le bip violet

            // Quand le Bip est appelé
            lecteur.SimulerErreur(); // Simuler une erreur
            lecteur.Bip();

            // Alors un bip et un flash violet sont émis
            Assert.True(bipperSpy.IsBipEmitted);
            Assert.True(ledSpy.IsVioletFlashEmitted);
        }
        #endregion
    }
}