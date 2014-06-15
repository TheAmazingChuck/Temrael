﻿using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections;

namespace Server.Spells
{
    public class MontureCelesteMiracle : ReligiousSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Monture Celeste", "",
                SpellCircle.Sixth,
                17,
                9032,
                false
            );

        public override int RequiredAptitudeValue { get { return 6; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Fanatisme }; } }

        public override bool Invocation { get { return true; } }

        public MontureCelesteMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if ((Caster.Followers + 1) > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duration = GetDurationForSpell(30, 1.8);
                if (Caster is TMobile)
                {
                    SpellHelper.Summon(new Horse(), Caster, 0x217, duration, false, false);
                }
                else
                {
                SpellHelper.Summon(new Horse(), Caster, 0x217, duration, false, false);
                }
            }

            FinishSequence();
        }
    }
}
