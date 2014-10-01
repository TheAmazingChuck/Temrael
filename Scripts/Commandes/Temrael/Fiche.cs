﻿using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Scripts.Commands
{
    class Fiche
    {
        public static void Initialize()
        {
            CommandSystem.Register("Fiche", AccessLevel.Player, new CommandEventHandler(Fiche_OnCommand));
        }

        [Usage("Fiche")]
        [Description("Ouvre le menu de fiche qui regrouppe les informations du personnage.")]
        public static void Fiche_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is TMobile)
            {
                TMobile from = (TMobile)e.Mobile;

                if (from is TMobile)
                {
                    from.SendGump(new FicheRaceGump(from));
                }
            }
        }
    }
}