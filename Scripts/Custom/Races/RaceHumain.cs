using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class RaceHumain : BaseRace
    {
        #region Apparence
        public override bool AllowHair { get { return true; } }
        public override bool AllowFacialHair { get { return true; } }

        private static int[] m_Hues = new int[]
            {
                1023,
                1002,
                1013
            };

        public override int[] Hues { get { return m_Hues; } }
        public override string Name { get { return "Humain"; } }
        public override string NameF { get { return "Humaine"; } }
        #endregion

        #region Caract
        public override int Str { get { return 80; } }
        public override int Con { get { return 80; } }
        public override int Dex { get { return 80; } }
        public override int Cha { get { return 90; } }
        public override int Int { get { return 90; } }
        #endregion

        #region Gump
        public override int Image { get { return 342; } }
        public override int Tooltip { get { return 3006423; } }
        public override string Description { get { return "Vous �tes partout. Apr�s quelques si�cles de guerres et conqu�tes, vous voil� les ma�tres du monde. Ou presque. Certains obstacles se dressent toujours sur la route de l'humanit� et il sera votre devoir en tant que citoyen du royaume de Temrael de les d�cimer. La route de la conqu�te fut longue et difficile et il est parfois ais� pour certains d'entres vous de regarder les autres peuples de haut. Par contre, certains d'entre vous adoptent une id�e plus libertine. Une id�e nouvelle de cohabitation avec les autres peuples soumis au royaume."; } }
        #endregion

        #region Bonus
        public override Aptitude Bonus { get { return Aptitude.PointSup; } }
        public override int BonusNbr { get { return 1; } }
        public override string BonusDescr { get { return "La grande diversite des Capiceen vous permet d'avoir un point d'aptitude supplementaire a la creation de votre personnage."; } }
        #endregion

        #region Constructeur
        public RaceHumain()
		{
        }
        #endregion
    }
}