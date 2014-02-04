﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;

namespace Server
{
    public class ClasseGuerrier
    {
        private static ClasseType m_Classe = ClasseType.Guerrier;
        private static ClasseBranche m_ClasseBranche = ClasseBranche.Guerrier;
        private static int m_Image = 344;
        private static int m_Tooltip = 0;
        private static string m_Name = "Guerrier";
        private static string m_Role = "À venir";
        private static AlignementB m_Alignement = AlignementB.Aucun;

        private static String[] m_Noms = new String[]
            {
                "Recrue",
                "Homme d'Armes",
                "Guerrier",
                "Maitre d'Armes"
            };

        private static ClasseAptitudes[] m_firstApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 3),
                new ClasseAptitudes(NAptitude.PortArme, 3),
                new ClasseAptitudes(NAptitude.PortBouclier, 2),
                new ClasseAptitudes(NAptitude.CombatAuSol, 1)
            };

        private static ClasseAptitudes[] m_secondApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 4),
                new ClasseAptitudes(NAptitude.PortArme, 4),
                new ClasseAptitudes(NAptitude.PortBouclier, 3),
                new ClasseAptitudes(NAptitude.CombatAuSol, 4)
            };

        private static ClasseAptitudes[] m_thirdApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 5),
                new ClasseAptitudes(NAptitude.PortArme, 5),
                new ClasseAptitudes(NAptitude.PortBouclier, 4),
                new ClasseAptitudes(NAptitude.CombatAuSol, 6)
            };

        private static ClasseAptitudes[] m_fourthApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 6),
                new ClasseAptitudes(NAptitude.PortArme, 6),
                new ClasseAptitudes(NAptitude.PortBouclier, 5),
                new ClasseAptitudes(NAptitude.CombatAuSol, 8)
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
