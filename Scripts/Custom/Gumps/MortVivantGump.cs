﻿using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using System.Collections;
using Server.Misc;

namespace Server.Gumps
{
    class MortVivantGump : Gump
    {
        private TMobile m_From;

        public MortVivantGump(TMobile from)
            : base(0, 0)
        {
            m_From = from;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            AddBackground(80, 72, 420, 500, 3600);
            AddBackground(90, 82, 400, 480, 9200);
            AddBackground(100, 92, 380, 460, 3500);
            AddBackground(115, 105, 350, 330, 9300);
            AddBackground(115, 435, 350, 100, 9300);

            AddImage(39, 53, 10440);
            AddImage(459, 53, 10441);

            AddImage(125, 110, 95);
            AddImage(132, 119, 96);
            AddImage(268, 119, 96);
            AddImage(445, 110, 97);

            AddHtml(240, 105, 200, 20, "<h3><basefont color=#025a>Mort Vivant<basefont></h3>", false, false);

            AddButton(130, 130, 441, 441, 1, GumpButtonType.Reply, 0);

            AddHtml(120, 440, 340, 90, "Cliquez l'image ci-haut pour devenir un mort-vivant. Un mort-vivant peut temporairement revenir a la vie en se nourissant de l'ame des vivants. Plus la decomposition se prolonge, plus cela sera difficile donc n'attendez pas trop longtemps !", true, true);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (from.Deleted)
                return;

            switch (info.ButtonID)
            {
                case 1:
                    if (!(m_From.MortCurrentState == MortState.MortVivant))
                    {
                        m_From.MortVivant = true;
                        m_From.MortCurrentState = MortState.MortVivant;

                        Server.Mobiles.TMobile.MortVivantEvoTimer timer = new Server.Mobiles.TMobile.MortVivantEvoTimer(m_From);
                        m_From.MortVivantTimer = timer;
                        timer.Start();

                        m_From.Hits = (m_From.HitsMax / 2) + Utility.RandomMinMax(-5, 5);
                        m_From.Frozen = true;
                        //if (Temrael.beta)
                        //    m_From.Map = Map.Ilshenar;
                        m_From.Location = m_From.EndroitMort;
                        m_From.Mort = false;
                        m_From.Resurrect();
                        m_From.Frozen = false;

                        if (m_From.Corpse != null)
                        {
                            ArrayList list = new ArrayList();

                            foreach (Item item in m_From.Corpse.Items)
                            {
                                list.Add(item);
                            }

                            foreach (Item item in list)
                            {
                                if (item.Layer == Layer.Hair || item.Layer == Layer.FacialHair)
                                    item.Delete();

                                if (item is BaseRaceGumps || (m_From.Corpse is Corpse && ((Corpse)m_From.Corpse).EquipItems.Contains(item)))
                                {
                                    if (!m_From.EquipItem(item))
                                        m_From.AddToBackpack(item);
                                }
                                else
                                {
                                    m_From.AddToBackpack(item);
                                }
                            }

                            m_From.Corpse.Delete();
                        }

                        m_From.CheckStatTimers();
                        m_From.CheckRaceGump();
                    }
                    break;
            }
        }
    }
}
