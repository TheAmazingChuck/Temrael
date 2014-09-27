using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class SummonFamiliarScroll : SpellScroll
	{
		[Constructable]
		public SummonFamiliarScroll() : this( 1 )
		{
		}

		[Constructable]
        public SummonFamiliarScroll(int amount)
            : base(SummonFamiliarSpell.m_SpellID, 0x226B, amount)
		{
            Name = "N�cromancie: Minion";
		}

		public SummonFamiliarScroll( Serial serial ) : base( serial )
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

            Name = "N�cromancie: Minion";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new SummonFamiliarScroll( amount ), amount );
		}*/
	}
}