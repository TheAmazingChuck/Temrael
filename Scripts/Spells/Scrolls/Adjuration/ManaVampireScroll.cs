using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class ManaVampireScroll : SpellScroll
	{
		[Constructable]
		public ManaVampireScroll() : this( 1 )
		{
		}

		[Constructable]
		public ManaVampireScroll( int amount ) : base( ManaVampireSpell.spellID, 0x1F61, amount )
		{
            Name = "Adjuration: Drain Vampirique";
		}

		public ManaVampireScroll( Serial serial ) : base( serial )
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

            Name = "Adjuration: Drain Vampirique";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new ManaVampireScroll( amount ), amount );
		}*/
	}
}