﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;

namespace Server
{
    public class ClasseAssassin
    {
        private static ClasseType m_Classe = ClasseType.Assassin;
        private static ClasseBranche m_ClasseBranche = ClasseBranche.Roublard;
        private static int m_Image = 284;
        private static int m_Tooltip = 0;
        private static string m_Name = "Assassin";
        private static string m_Role = "À venir";
        private static AlignementB m_Alignement = AlignementB.Aucun;

        private static String[] m_Noms = new String[]
            {
                "Executeur",
                "Meurtrier",
                "Tueur",
                "Assassin"
            };

        private static ClasseAptitudes[] m_firstApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 1),
                new ClasseAptitudes(Aptitude.PortArme, 3),
                new ClasseAptitudes(Aptitude.Assassinat, 3),
                new ClasseAptitudes(Aptitude.MouvementCache, 1)
            };

        private static ClasseAptitudes[] m_secondApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 1),
                new ClasseAptitudes(Aptitude.PortArme, 4),
                new ClasseAptitudes(Aptitude.Assassinat, 6),
                new ClasseAptitudes(Aptitude.MouvementCache, 3)
            };

        private static ClasseAptitudes[] m_thirdApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 2),
                new ClasseAptitudes(Aptitude.PortArme, 5),
                new ClasseAptitudes(Aptitude.Assassinat, 9),
                new ClasseAptitudes(Aptitude.MouvementCache, 6)
            };

        private static ClasseAptitudes[] m_fourthApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(Aptitude.PortArmure, 3),
                new ClasseAptitudes(Aptitude.PortArme, 6),
                new ClasseAptitudes(Aptitude.Assassinat, 12),
                new ClasseAptitudes(Aptitude.MouvementCache, 8)
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
