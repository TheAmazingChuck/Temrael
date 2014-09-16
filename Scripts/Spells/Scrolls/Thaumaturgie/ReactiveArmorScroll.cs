using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class ReactiveArmorScroll : SpellScroll
	{
		[Constructable]
		public ReactiveArmorScroll() : this( 1 )
		{
		}

		[Constructable]
		public ReactiveArmorScroll( int amount ) : base( ReactiveArmorSpell.spellID, 0x1F2D, amount )
		{
            Name = "Thaumaturgie: Armure Magique";
		}

		public ReactiveArmorScroll( Serial ser ) : base(ser)
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

            Name = "Thaumaturgie: Armure Magique";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new ReactiveArmorScroll( amount ), amount );
		}*/
	}
}