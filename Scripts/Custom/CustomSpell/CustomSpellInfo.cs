﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Spells;

namespace Server.Custom.CustomSpell
{
    public class InfoSpell : Server.Spells.SpellInfo
    {
        // Commun à tous les spells.
        private TimeSpan m_castTime = new TimeSpan(0, 0, -1);
        public TimeSpan castTime { get { return m_castTime; } set { if (value.Seconds <= 60 && value.Seconds >= 0) m_castTime = value; } }

        public StyleSpell style = StyleSpell.Unsetted;


        // S'assure que tous ses membres ont été initialisés correctement.
        public void CheckValidity()
        {
            if (!(   castTime.Seconds >= 0 &&
                     style != StyleSpell.Unsetted))
            {
                throw new Exception("BaseSpellInfo initialisé incorrectement :" + Name + ".");
            }
        }


        private InfoSpell(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, StyleSpell Style, params Type[] regs)
            : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, true, regs)
        {
            castTime = CastTime;
            style = Style;

            CheckValidity();
        }


        #region Spécialisations d'InfoBaseSpell pour chaque type de spell.
        public class Targetted : InfoSpell
        {
            // Membres aditionnels.
            private int m_NbTarget = 1;
            public int nbTarget { get { return m_NbTarget; } }

            private int m_Range = 10;
            public int range { get { return m_Range; } }

            // Permet de passer à travers la fonction Effect() à chaque fois qu'on click un target.
            private bool m_unEffectParTarget = false;
            public bool unEffectParTarget { get { return m_unEffectParTarget; } }


            public Targetted(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, int NbTarget, bool unEffectParTarget, int Range, params Type[] regs)
                : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, CastTime, StyleSpell.Targetted, regs)
            {
                if (NbTarget <= 3 && NbTarget >= 1) m_NbTarget = NbTarget;
                if (range <= 20 && range >= 1) m_Range = range;
                m_unEffectParTarget = unEffectParTarget;
            }
        }

        public class TargettedTimer : InfoSpell
        {
            // Membres aditionnels.
            private int m_NbTarget = 1;
            public int nbTarget { get { return m_NbTarget; }}

            private int m_Range = 10;
            public int range { get { return m_Range; }}

            private TimeSpan m_duree = TimeSpan.FromSeconds(0);
            public TimeSpan duree { get { return m_duree; } }

            private TimerPriority m_intervale = TimerPriority.OneSecond;
            public TimerPriority intervale { get { return m_intervale; } }

            // Permet de passer à travers la fonction Effect() à chaque fois qu'on click un target.
            public bool unEffectParTarget = false;


            public TargettedTimer(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, int NbTarget, bool unEffectParTarget, int Range, TimeSpan Duree, TimerPriority Intervale, params Type[] regs)
                : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, CastTime, StyleSpell.TargettedTimer, regs)
            {
                if (NbTarget <= 3 && NbTarget >= 1) m_NbTarget = NbTarget;
                if (range <= 20 && range >= 1) m_Range = range;
                m_duree = Duree;
                m_intervale = Intervale;
            }
        }

        public class AoE : InfoSpell
        {
            // Membres aditionnels.

            public AoE(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, params Type[] regs)
                : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, CastTime, StyleSpell.AoE, regs)
            {

            }
        }

        public class AoETimer : InfoSpell
        {
            // Membres aditionnels.

            public AoETimer(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, params Type[] regs)
                : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, CastTime, StyleSpell.AoETimer, regs)
            {

            }
        }

        public class Self : InfoSpell
        {
            // Membres aditionnels.

            public Self(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, params Type[] regs)
                : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, CastTime, StyleSpell.AoETimer, regs)
            {

            }
        }

        public class SelfTimer : InfoSpell
        {
            // Membres aditionnels.

            public SelfTimer(string Name, string Formule, SpellCircle Cercle, int Action, int HandEffect, int ManaCost, SkillName SkillUtilise, int NiveauSkillReq, TimeSpan CastTime, params Type[] regs)
                : base(Name, Formule, Cercle, Action, HandEffect, ManaCost, SkillUtilise, NiveauSkillReq, CastTime, StyleSpell.AoETimer, regs)
            {

            }
        }
        #endregion

    }
}
