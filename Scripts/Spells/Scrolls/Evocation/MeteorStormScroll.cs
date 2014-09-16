using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class MeteorSwarmScroll : SpellScroll
	{
		[Constructable]
		public MeteorSwarmScroll() : this( 1 )
		{
		}

		[Constructable]
		public MeteorSwarmScroll( int amount ) : base( MeteorSwarmSpell.spellID, 0x1F63, amount )
		{
            Name = "�vocation: M�t�ores";
		}

		public MeteorSwarmScroll( Serial serial ) : base( serial )
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

            Name = "�vocation: M�t�ores";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new MeteorSwarmScroll( amount ), amount );
		}*/
	}
}