﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;

namespace Server
{
    public class ClasseChampion
    {
        private static ClasseType m_Classe = ClasseType.Champion;
        private static ClasseBranche m_ClasseBranche = ClasseBranche.Guerrier;
        private static int m_Image = 431;
        private static int m_Tooltip = 0;
        private static string m_Name = "Champion";
        private static string m_Role = "À venir";
        private static AlignementB m_Alignement = AlignementB.Aucun;

        private static String[] m_Noms = new String[]
            {
                "Combattant",
                "Justicier",
                "Champion",
                "Hero"
            };

        private static ClasseAptitudes[] m_firstApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 3),
                new ClasseAptitudes(Aptitude.PortArme, 3),
                new ClasseAptitudes(Aptitude.PortBouclier, 2),
                new ClasseAptitudes(Aptitude.TueurDeMonstre, 2)
            };

        private static ClasseAptitudes[] m_secondApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 4),
                new ClasseAptitudes(Aptitude.PortArme, 4),
                new ClasseAptitudes(Aptitude.PortBouclier, 3),
                new ClasseAptitudes(Aptitude.TueurDeMonstre, 4)
            };

        private static ClasseAptitudes[] m_thirdApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 5),
                new ClasseAptitudes(Aptitude.PortArme, 5),
                new ClasseAptitudes(Aptitude.PortBouclier, 4),
                new ClasseAptitudes(Aptitude.TueurDeMonstre, 6)
            };

        private static ClasseAptitudes[] m_fourthApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 6),
                new ClasseAptitudes(Aptitude.PortArme, 6),
                new ClasseAptitudes(Aptitude.PortBouclier, 5),
                new ClasseAptitudes(Aptitude.TueurDeMonstre, 8)
            };

        public static ClasseInfo ClasseInfo = new ClasseInfo(
                m_Classe,
                m_firstApt,
                m_secondApt,
                m_thirdApt,
                m_fourthApt,
                m_Name,
                m_Noms,
                m_Role,
                m_ClasseBranche,
                m_Image,
                m_Tooltip
            );
    }
}
