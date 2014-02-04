﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;

namespace Server
{
    public class ClasseArcher
    {
        private static ClasseType m_Classe = ClasseType.Archer;
        private static ClasseBranche m_ClasseBranche = ClasseBranche.Guerrier;
        private static int m_Image = 187;
        private static int m_Tooltip = 0;
        private static string m_Name = "Archer";
        private static string m_Role = "À venir";
        private static AlignementB m_Alignement = AlignementB.Aucun;

        private static String[] m_Noms = new String[]
            {
                "Tireur",
                "Archer",
                "Tireur Émérite",
                "Maître Archer"
            };

        private static ClasseAptitudes[] m_firstApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 1),
                new ClasseAptitudes(NAptitude.PortArme, 1),
                new ClasseAptitudes(NAptitude.PortArmeDistance, 3),
                new ClasseAptitudes(NAptitude.TirPrecis, 2),
                new ClasseAptitudes(NAptitude.CombatAuSol, 1)
            };

        private static ClasseAptitudes[] m_secondApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 2),
                new ClasseAptitudes(NAptitude.PortArme, 1),
                new ClasseAptitudes(NAptitude.PortArmeDistance, 4),
                new ClasseAptitudes(NAptitude.TirPrecis, 4),
                new ClasseAptitudes(NAptitude.CombatAuSol, 3)
            };

        private static ClasseAptitudes[] m_thirdApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 2),
                new ClasseAptitudes(NAptitude.PortArme, 1),
                new ClasseAptitudes(NAptitude.PortArmeDistance, 5),
                new ClasseAptitudes(NAptitude.TirPrecis, 6),
                new ClasseAptitudes(NAptitude.CombatAuSol, 5)
            };

        private static ClasseAptitudes[] m_fourthApt = new ClasseAptitudes[]
            {
                new ClasseAptitudes(NAptitude.PortArmure, 3),
                new ClasseAptitudes(NAptitude.PortArme, 2),
                new ClasseAptitudes(NAptitude.PortArmeDistance, 6),
                new ClasseAptitudes(NAptitude.TirPrecis, 8),
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
