﻿using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using Server.Engines.Equitation;

namespace Server.Scripts.Commands
{
    class Test
    {
        public static void Initialize()
        {
            CommandSystem.Register("Test", AccessLevel.Batisseur, new CommandEventHandler(Test_OnCommand));
        }

        public static void PublicOverheadRunicMessage(Mobile mob, MessageType type, int hue, int font, string text, bool noLineOfSight)
        {
            if (mob != null && mob.Map != null)
            {
                Packet p = null;
                IPooledEnumerable eable = mob.Map.GetClientsInRange(mob.Location);

                foreach (NetState state in eable)
                {
                    if (state.Mobile.CanSee(mob) && (noLineOfSight || state.Mobile.InLOS(mob)))
                    {
                        if (p == null)
                        {
                            p = new AsciiMessage(mob.Serial, mob.Body, type, hue, font, mob.Name, text);
                            p.Acquire();
                        }
                        state.Send(p);
                    }
                }
                Packet.Release(p);
                eable.Free();
            }
        }

        [Usage("Test")]
        [Description("Test de scripts")]
        public static void Test_OnCommand(CommandEventArgs e)
        {
            PlayerMobile from = (PlayerMobile) e.Mobile;

            Equitation.CheckEquitation(from, EquitationType.BeingAttacked);

            //from.SendGump(new MagieGump(from, Classe.Aucune));
            //from.SendGump(new GuerrierGump(from, Classe.Aucune));
            //from.SendGump(new RoublardGump(from, Classe.Aucune));
            //from.SendGump(new ClericGump(from, Classe.Aucune));

            //PublicOverheadRunicMessage(from, MessageType.Regular, from.SpeechHue, 8, "CORP POR", false);

            //from.SendMessage(XP.MakeXPparNiveau126().ToString());

            //from.SendGump(new MortDefinitiveGump((PlayerMobile)from));

            //from.SendGump(new MortVivantGump((PlayerMobile)from));

            //if (from is PlayerMobile)
            //    from.SendGump(new CreationGump(((PlayerMobile)from), CreationTabs.Race, new List<Server.Gumps.CreationGump.PaperPreviewItem>(), Races.Aucun, ClasseArbre.Aucun, new Server.Gumps.CreationGump.CreationStatsPreview(), Metier.Aucun, Server.Gumps.CreationGump.DestinationsDepart.Aucune, 0, ClasseSociale.Aucune, Metier.Aucun));

            //from.SendGump(new CoteGump(((PlayerMobile)from), ((PlayerMobile)from)));

            //from.SendGump(new CompetenceGump(from, Server.Gumps.CompetenceGump.CompDomaines.Aucun, false));
        }
    }
}