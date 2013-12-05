using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class Provisioner : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
        public Provisioner()
            : base("Approvisionneur")
		{
			SetSkill( SkillName.Survie, 45.0, 68.0 );
			SetSkill( SkillName.Tactiques, 45.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBProvisioner() );

			if ( IsTokunoVendor )
				m_SBInfos.Add( new SBSEHats() );
		}

		public Provisioner( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}