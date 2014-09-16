using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class HealScroll : SpellScroll
	{
		[Constructable]
		public HealScroll() : this( 1 )
		{
		}

		[Constructable]
		public HealScroll( int amount ) : base( HealSpell.spellID, 0x1F31, amount )
		{
            Name = "Thaumaturgie: Soins";
		}

		public HealScroll( Serial serial ) : base( serial )
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

            Name = "Thaumaturgie: Soins";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new HealScroll( amount ), amount );
		}*/
	}
}