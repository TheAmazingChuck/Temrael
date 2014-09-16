using System;
using Server;

namespace Server.Spells
{
	public class SpellInfo
	{
		private string m_Name;
		private string m_Mantra;
		private SpellCircle m_Circle;
        private int m_Action;
        private int m_ManaCost;
        private TimeSpan m_castTime = new TimeSpan(0, 0, -1);
        private SkillName m_SkillForCasting;
        private int m_MinSkillForCasting;
        private bool m_AllowTown;
		private Type[] m_Reagents;
		private int[] m_Amounts;

        private int m_LeftHandEffect, m_RightHandEffect;


        public SpellInfo(string name, string mantra, SpellCircle circle, int action)
            : this(name, mantra, circle, action, 0, 0, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, true, Reagent.Bloodmoss)
        {
        }

        public SpellInfo(string name, string mantra, SpellCircle circle, params Type[] regs)
            : this(name, mantra, circle, 16, 0, 0, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, true, regs)
		{
		}

        public SpellInfo(string name, string mantra, SpellCircle circle, bool allowTown, params Type[] regs)
            : this(name, mantra, circle, 16, 0, 0, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, allowTown, regs)
		{
		}

        public SpellInfo(string name, string mantra, SpellCircle circle, int action, params Type[] regs)
            : this(name, mantra, circle, action, 0, 0, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, true, regs)
		{
		}

        public SpellInfo(string name, string mantra, SpellCircle circle, int action, bool allowTown, params Type[] regs)
            : this(name, mantra, circle, action, 0, 0, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, allowTown, regs)
		{
		}

        public SpellInfo(string name, string mantra, SpellCircle circle, int action, int handEffect, params Type[] regs)
            : this(name, mantra, circle, action, handEffect, handEffect, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, true, regs)
		{
		}

        public SpellInfo(string name, string mantra, SpellCircle circle, int action, int handEffect, bool allowTown, params Type[] regs)
            : this(name, mantra, circle, action, handEffect, handEffect, 0, TimeSpan.FromSeconds(1), SkillName.ArtMagique, 0, allowTown, regs)
		{
		}

        public SpellInfo(string name, string mantra, SpellCircle circle, int action, int handEffect, int manaCost, TimeSpan castTime, SkillName skillForCasting, int minSkillForCasting, bool allowTown, params Type[] regs)
            : this(name, mantra, circle, action, handEffect, handEffect, manaCost, castTime, skillForCasting, minSkillForCasting, allowTown, regs)
        {
        }

        public SpellInfo(string name, string mantra, SpellCircle circle, int action, int leftHandEffect, int rightHandEffect, int manaCost, TimeSpan castTime, SkillName skillForCasting, int minSkillForCasting, bool allowTown, params Type[] regs)
		{
			m_Name = name;
			m_Mantra = mantra;
			m_Circle = circle;
			m_Action = action;
            m_ManaCost = manaCost;
            m_SkillForCasting = skillForCasting;
            m_MinSkillForCasting = minSkillForCasting;
            m_AllowTown = allowTown;
            m_Reagents = regs;

			m_LeftHandEffect = leftHandEffect;
			m_RightHandEffect = rightHandEffect;

            if (regs != null)
            {
                m_Amounts = new int[regs.Length];

                for (int i = 0; i < regs.Length; ++i)
                    m_Amounts[i] = 1;
            }
		}

		public int Action{ get{ return m_Action; } set{ m_Action = value; } }
		public bool AllowTown{ get{ return m_AllowTown; } set{ m_AllowTown = value; } }
		public int[] Amounts{ get{ return m_Amounts; } set{ m_Amounts = value; } }
		public SpellCircle Circle{ get{ return m_Circle; } set{ m_Circle = value; } }
		public string Mantra{ get{ return m_Mantra; } set{ m_Mantra = value; } }
		public string Name{ get{ return m_Name; } set{ m_Name = value; } }
		public Type[] Reagents{ get{ return m_Reagents; } set{ m_Reagents = value; } }
		public int LeftHandEffect{ get{ return m_LeftHandEffect; } set{ m_LeftHandEffect = value; } }
		public int RightHandEffect{ get{ return m_RightHandEffect; } set{ m_RightHandEffect = value; } }
        public SkillName skillForCasting { get { return m_SkillForCasting; } set { m_SkillForCasting = value; } }
        public int minSkillForCasting { get { return m_MinSkillForCasting; } set { m_MinSkillForCasting = value; } }
        public int manaCost { get { return m_ManaCost; } set { m_ManaCost = value; } }
        public TimeSpan castTime { get { return m_castTime; } set { if (value.Seconds <= 60 && value.Seconds >= 0) m_castTime = value; } }
	}
}