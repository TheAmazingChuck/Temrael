using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Engines.Races
{
	public class Capiceen : Humain
    {
        public static int RaceId { get { return 1; } }

        public override int Id { get { return RaceId; } }

        public override int Skin { get { return -1; } }

        public static int[] Hues = new int[]
            {
                1023,
                1002,
                1013
            };

        public override string Name { get { return "Capiceen"; } }
        public override string NameF { get { return "Capiceene"; } }

        public override int Image { get { return 342; } }
        public override int Tooltip { get { return 3006423; } }
        public override string Description { get { return "Vous �tes partout. Apr�s quelques si�cles de guerres et conqu�tes, vous voil� les ma�tres du monde. Ou presque. Certains obstacles se dressent toujours sur la route de l'humanit� et il sera votre devoir en tant que citoyen du royaume de Temrael de les d�cimer. La route de la conqu�te fut longue et difficile et il est parfois ais� pour certains d'entres vous de regarder les autres peuples de haut. Par contre, certains d'entre vous adoptent une id�e plus libertine. Une id�e nouvelle de cohabitation avec les autres peuples soumis au royaume."; } }

        public Capiceen(RaceSecrete r, int regHue, int secHue)
            : base(r, regHue, secHue)
        {
        }

        public Capiceen(int hue)
            : base(hue)
        {
        }

        public Capiceen(GenericReader reader) : base(reader)
        {
            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            
            writer.Write(0); //version
        }
    }
}