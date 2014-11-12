﻿using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Accounting;
using Server.Engines.Mort;

namespace Server.Items
{
    public class CreationStone : Item
    {
        public override bool CanBeAltered
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public CreationStone()
            : base(0xEDE)
        {
            Movable = false;
            Name = "Creation";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from is PlayerMobile && from.InRange(Location, 4))
            {
                PlayerMobile tMob = from as PlayerMobile;

                //tMob.StatistiquesLibres = 0;
                //tMob.AptitudesLibres = 0;
                //tMob.CompetencesLibres = 0;
                tMob.StatCap = 225;

                tMob.RawStr = 25;
                tMob.RawDex = 25;
                tMob.RawInt = 25;

                Account act = tMob.Account as Account;

                if (act.GetTag("XP") != "")
                {
                    tMob.Experience.XP = Convert.ToInt32(act.GetTag("XP"));
                    act.SetTag("XP", "");
                }

                if (act.Created < new DateTime(2013, 6, 11) && act.GetTag("XPBeta") != "True")
                {
                    tMob.Experience.XP = 10000;
                    act.SetTag("XPBeta", "True");
                }

                tMob.Title = "";
                tMob.MortEngine.MortEvo = MortEvo.Aucune;

                tMob.CloseGump(typeof(CreationRaceGump));

                tMob.SendGump(new CreationRaceGump(tMob, new CreationInfos()));
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }

        public CreationStone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}

