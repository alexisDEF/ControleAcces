using ControleAcces.Test.Utilities;

namespace ControleAcces.Test
{
    public class ControleAccesTest
    {
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
    }
}