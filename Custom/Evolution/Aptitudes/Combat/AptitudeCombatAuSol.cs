﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;

namespace Server
{
    public class AptitudeCombatAuSol
    {
        private static string m_name = "Combat au sol";
        private static AptitudesEntry m_entry = Aptitudes.m_AptitudeEntries[(int)NAptitude.CombatAuSol];
        private static int m_tooltip = 3006302;
        private static string m_description = "Augmente les dégâts au sol.";
        private static string m_note = string.Empty;
        private static int m_image = 0;

        private static String[] m_descriptionNiveau = new String[]
            {
                "+3% de Dégâts", 
                "+6% de Dégâts", 
                "+9% de Dégâts", 
                "+12% de Dégâts", 
                "+15% de Dégâts", 
                "+18% de Dégâts", 
                "+21% de Dégâts", 
                "+24% de Dégâts", 
                "+27% de Dégâts",
                "+30% de Dégâts", 
                "+33% de Dégâts",
                "+36% de Dégâts"
            };

        public static AptitudeInfo AptitudeInfo = new AptitudeInfo(
                        m_name,
                        m_entry,
                        m_tooltip,
                        m_description,
                        m_descriptionNiveau,
                        m_note,
                        m_image
                    );
    }
}
