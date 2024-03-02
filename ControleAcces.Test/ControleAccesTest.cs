using ControleAcces.Test.Utilities;

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
            var reader = new LecteurFake();
            reader.SimulerPr�sentationBadge();
            var porte = new PorteSpy();
            var moteur = new MoteurOuverture(porte);
            var readerLed = new LedSpy();
            var readerWithLed = new ReaderWithLed(reader, readerLed);

            // Quand la m�thode Flash est appel�e
            readerWithLed.Flash(false, false, false);

            // Un flash blanc est �mis
            Assert.True(readerLed.IsWhiteFlashEmitted());
        }


        #endregion

        #region Feature 2
        #endregion
    }
}