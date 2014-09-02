﻿using System;
using System.Collections;
using Server.Mobiles;
using Server.Items;

namespace Server.TechniquesCombat
{
    public class Assassinat
    {

        public static Hashtable mobilesList = new Hashtable();

        // Ressources
        private PlayerMobile m_Caster;
        public Assassinat(PlayerMobile caster)
        {
            m_Caster = caster;
            Use();
        }


        // Modifiables.
        private const int m_ManaCost = 10;

        // Valeur utilisée par a l'extérieur.
        public const int MultiplicateurBonusDegat = 3;


        TimeSpan CalculDuree()
        {
            int Hours = 0;
            int Minutes = 0;
            int Seconds = 0;

            // Insert formula here...
            Seconds = 20;

            return new TimeSpan(Hours, Minutes, Seconds);
        }


        private void Use()
        {
            if ((!m_Caster.Deleted && m_Caster.Alive))
            {
                if (m_Caster.Mana >= m_ManaCost)
                {
                    m_Caster.Mana -= m_ManaCost;
                    BonusTimer m_TimerDuration = new BonusTimer(CalculDuree(), m_Caster);
                }
                else
                {
                    m_Caster.SendMessage("Vous n'avez pas assez de mana pour utiliser cette technique !");
                }
            }
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Timer qui ajoute un bonus, puis le retire quand le temps est écoulé.
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private class BonusTimer : Timer
        {
            private PlayerMobile m_Caster;
            private BaseWeapon m_Weapon;




            private void AjoutBonus()
            {
                if (mobilesList.Contains(m_Caster))
                {
                    mobilesList.Remove(m_Caster);
                    mobilesList.Add(m_Caster, null);
                }
                else
                {
                    mobilesList.Add(m_Caster, null);
                }
            }


            private void RetraitBonus()
            {
                if (mobilesList.Contains(m_Caster))
                {
                    mobilesList.Remove(m_Caster);
                }
            }


            public BonusTimer(TimeSpan duration, PlayerMobile caster)
                : base(duration)
            {
                m_Caster = caster;

                Priority = TimerPriority.TwoFiftyMS;

                CommencerTechnique();
            }

            // Pas touche ---v

            private bool DetruitCorrectement;

            public void CommencerTechnique()
            {
                AjoutBonus();
                DetruitCorrectement = false;
                Start();
            }

            protected override void OnTick()
            {
                RetraitBonus();
                DetruitCorrectement = true;
                Stop();
            }

            ~BonusTimer()
            {
                if (DetruitCorrectement == false)
                {
                    RetraitBonus();
                }
            }
        }
    }
}
