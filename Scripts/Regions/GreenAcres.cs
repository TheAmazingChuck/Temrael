using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Spells;
//using Server.Spells.Chivalry;

namespace Server.Regions
{
	public class GreenAcres : ZoneInterne
	{
		public GreenAcres( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			if ( from.AccessLevel == AccessLevel.Player )
				return false;
			else
				return base.AllowHousing( from, p );
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			//if ( ( s is GateTravelSpell || s is RecallSpell || s is MarkSpell || s is SacredJourneySpell ) && m.AccessLevel == AccessLevel.Player )
            if ((s is GateTravelSpell || s is RecallSpell || s is MarkSpell) && m.AccessLevel == AccessLevel.Player)
			{
				m.SendMessage( "You cannot cast that spell here." );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast( m, s );
			}
		}
	}
}
