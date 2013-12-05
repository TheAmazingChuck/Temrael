using System;
using System.Reflection;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Spells
{
    public class TransfertSpell : ReligiousSpell
    {
        public static Hashtable m_Timers = new Hashtable();

        private static SpellInfo m_Info = new SpellInfo(
                "Transfert", "Tyros Otil Wun",
                SpellCircle.Eighth,
                212,
                9041
            );

        public override int RequiredAptitudeValue { get { return 6; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] { NAptitude.Monial }; } }

        private class PossessTarget : Target
        {
            private TransfertSpell spell;

            public PossessTarget(TransfertSpell spella)
                : base(-1, false, TargetFlags.None)
            {
                spell = spella;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (spell.CheckSequence())
                {
                    TMobile pm = from as TMobile;

                    if (o is BaseCreature)
                    {
                        BaseCreature creature = (BaseCreature)o;

                        if (creature.Controlled && creature.ControlMaster != null && !creature.Summoned)
                        {
                            Mobile master = (Mobile)creature.ControlMaster;

                            if (from.Skills[SkillName.ArtMagique].Value < master.Skills[SkillName.Dressage].Value)
                            {
                                from.SendMessage("Vous devez avoir un niveau plus �lev� de Spirit Speak que celui d'Animal Taming du ma�tre de la cr�ature.");
                            }
                            else
                            {
                                from.DoHarmful(master);

                                creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502799, creature.NetState); // It seems to accept you as master.

                                creature.SetControlMaster(from);
                                creature.IsBonded = false;

                                creature.FixedParticles(14186, 10, 20, 5013, 1441, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                                creature.PlaySound(497);
                            }
                        }
                    }
                }

                spell.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                spell.FinishSequence();
            }
        }

        public TransfertSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new PossessTarget(this);
        }
    }
}