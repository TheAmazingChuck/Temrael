using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBlacksmithy : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Forge;	}
		}

		/*public override int GumpTitleNumber
		{
			get { return 1044002; } // <CENTER>BLACKSMITHY MENU</CENTER>
		}*/

        public override string GumpTitleString
        {
            get { return "Menu de Forge"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBlacksmithy();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefBlacksmithy() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
			/*
			
			base( MinCraftEffect, MaxCraftEffect, Delay )
			
			MinCraftEffect	: The minimum number of time the mobile will play the craft effect
			MaxCraftEffect	: The maximum number of time the mobile will play the craft effect
			Delay			: The delay between each craft effect
			
			Example: (3, 6, 1.7) would make the mobile do the PlayCraftEffect override
			function between 3 and 6 time, with a 1.7 second delay each time.
			
			*/ 
		}

		private static Type typeofAnvil = typeof( AnvilAttribute );
		private static Type typeofForge = typeof( ForgeAttribute );

		public static void CheckAnvilAndForge( Mobile from, int range, out bool anvil, out bool forge )
		{
			anvil = false;
			forge = false;

			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				bool isAnvil = ( type.IsDefined( typeofAnvil, false ) || item.ItemID == 4015 || item.ItemID == 4016 || item.ItemID == 0x2DD5 || item.ItemID == 0x2DD6 );
				bool isForge = ( type.IsDefined( typeofForge, false ) || item.ItemID == 4017 || (item.ItemID >= 6522 && item.ItemID <= 6569) || item.ItemID == 0x2DD8 );

				if ( isAnvil || isForge )
				{
					if ( (from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS( item ) )
						continue;

					anvil = anvil || isAnvil;
					forge = forge || isForge;

					if ( anvil && forge )
						break;
				}
			}

			eable.Free();

			for ( int x = -range; (!anvil || !forge) && x <= range; ++x )
			{
				for ( int y = -range; (!anvil || !forge) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; (!anvil || !forge) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID;

						bool isAnvil = ( id == 4015 || id == 4016 || id == 0x2DD5 || id == 0x2DD6 );
						bool isForge = ( id == 4017 || (id >= 6522 && id <= 6569) || id == 0x2DD8 );

						if ( isAnvil || isForge )
						{
							if ( (from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS( new Point3D( from.X+x, from.Y+y, tiles[i].Z + (tiles[i].Height/2) + 1 ) ) )
								continue;

							anvil = anvil || isAnvil;
							forge = forge || isForge;
						}
					}
				}
			}
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckTool( tool, from ) )
				return 1048146; // If you have a tool equipped, you must use that tool.
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			bool anvil, forge;
			CheckAnvilAndForge( from, 2, out anvil, out forge );

			if ( anvil && forge )
				return 0;

			return 1044267; // You must be near an anvil and a forge to smith items.
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no animation, instant sound
			if ( from.Body.Type == BodyType.Human && !from.Mounted )
				from.Animate( 9, 5, 1, true, false, 0 );
			new InternalTimer( from ).Start();

			//from.PlaySound( 0x2A );
		}

		// Delay to synchronize the sound with the hit on the anvil
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x2A );
			}
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			/*
			Synthax for a SIMPLE craft item
			AddCraft( ObjectType, Group, MinSkill, MaxSkill, ResourceType, Amount, Message )
			
			ObjectType		: The type of the object you want to add to the build list.
			Group			: The group in wich the object will be showed in the craft menu.
			MinSkill		: The minimum of skill value
			MaxSkill		: The maximum of skill value
			ResourceType	: The type of the resource the mobile need to create the item
			Amount			: The amount of the ResourceType it need to create the item
			Message			: String or Int for Localized.  The message that will be sent to the mobile, if the specified resource is missing.
			
			Synthax for a COMPLEXE craft item.  A complexe item is an item that need either more than
			only one skill, or more than only one resource.
			
			Coming soon....
			*/

            int index = 0;

			#region Ringmail
            index = AddCraft(typeof(RingmailGloves), "Armure d'Anneaux", "Gants d'Anneaux", 15.0, 30.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(RingmailLegs), "Armure d'Anneaux", "Jambieres d'Anneaux", 15.0, 30.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(RingmailArms), "Armure d'Anneaux", "Brassards d'Anneaux", 15.0, 30.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(RingmailChest), "Armure d'Anneaux", "Cuirasse d'Anneaux", 15.0, 30.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(Bascinet), "Armure d'Anneaux", "Bascinet", 15.0, 30.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(Helmet), "Armure d'Anneaux", "Casque", 15.0, 30.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(NorseHelm), "Armure d'Anneaux", "Casque Norrois", 15.0, 30.0, typeof(FerIngot), "Lingots", 15, 1044037);
            #endregion

            #region Drow
            index = AddCraft(typeof(DrowHelm), "Armure d'Anneaux", "Courrone d'Alfar", 20.0, 50.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(DrowGorget), "Armure d'Anneaux", "Gorget d'Aflar", 20.0, 50.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(DrowArms), "Armure d'Anneaux", "Brassards d'Alfar", 20.0, 50.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(DrowLeggings), "Armure d'Anneaux", "Jambi�res d'Alfar", 20.0, 50.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(DrowTunic), "Armure d'Anneaux", "Tunique d'Alfar", 20.0, 50.0, typeof(FerIngot), "Lingots", 15, 1044037);
            #endregion

            #region Bourgeon
            index = AddCraft(typeof(BourgeonLeggings), "Armure d'Anneaux", "Jambieres", 20.0, 40.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(BourgeonGreaves), "Armure d'Anneaux", "Brassards", 20.0, 40.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(BourgeonTunic), "Armure d'Anneaux", "Cuirasse", 20.0, 40.0, typeof(FerIngot), "Lingots", 18, 1044037);
            #endregion

            #region Maillons
            index = AddCraft(typeof(MaillonsLeggings), "Armure d'Anneaux", "Jambieres de Maillons", 25.0, 45.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(MaillonsGreaves), "Armure d'Anneaux", "Brassards de Maillons", 25.0, 45.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(MaillonsTunic), "Armure d'Anneaux", "Cuirasse de Maillons", 25.0, 45.0, typeof(FerIngot), "Lingots", 18, 1044037);
            #endregion

            #region Maillures
            index = AddCraft(typeof(MailluresLeggings), "Armure de Chaines", "Jambieres de Maillures", 25.0, 45.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(MailluresGreaves), "Armure de Chaines", "Brassards de Maillures", 25.0, 45.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(MailluresTunic), "Armure de Chaines", "Cuirasse de Maillures", 25.0, 45.0, typeof(FerIngot), "Lingots", 18, 1044037);
            #endregion

            #region Chainmail
            index = AddCraft( typeof( ChainCoif ), "Armure de Chaines", "Casque de Chaines", 30.0, 50.0, typeof( FerIngot ), "Lingots", 10, 1044037 );

			index = AddCraft( typeof( ChainLegs ), "Armure de Chaines", "Jambieres de Chaines", 30.0, 50.0, typeof( FerIngot ), "Lingots", 18, 1044037 );

			index = AddCraft( typeof( ChainChest ), "Armure de Chaines", "Cuirasse de Chaines", 30.0, 50.0, typeof( FerIngot ), "Lingots", 20, 1044037 );
			#endregion

            #region Chaine Elfique
            index = AddCraft(typeof(ElfiqueChaineLeggings), "Armure de Chaines", "Jambieres Elfiques", 35.0, 55.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(ElfiqueChaineTunic), "Armure de Chaines", "Cuirasse Elfiques", 35.0, 55.0, typeof(FerIngot), "Lingots", 20, 1044037);
            #endregion

            #region Mailles
            index = AddCraft(typeof(MaillesHelm), "Armure de Chaines", "Casque de Mailles", 35.0, 60.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(MaillesLeggings), "Armure de Chaines", "Jambieres de Mailles", 35.0, 60.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(MaillesTunic), "Armure de Chaines", "Cuirasse de Mailles", 35.0, 60.0, typeof(FerIngot), "Lingots", 20, 1044037);
            #endregion

            #region Platemail
            index = AddCraft(typeof(PlateArms), "Armure de Plaque", "Brassards de Plaque", 40.0, 70.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(PlateGloves), "Armure de Plaque", "Gants de Plaque", 40.0, 70.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(PlateGorget), "Armure de Plaque", "Gorget de Plaque", 40.0, 70.0, typeof(FerIngot), "Lingots", 10, 1044037);
            
            index = AddCraft(typeof(PlateLegs), "Armure de Plaque", "Jambieres de Plaque", 40.0, 70.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(PlateChest), "Armure de Plaque", "Cuirasse de Plaque", 40.0, 70.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(FemalePlateChest), "Armure de Plaque", "Cuirasse F�minine", 40.0, 70.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(PlateHelm), "Armure de Plaque", "Casque de Plaque", 40.0, 70.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(CloseHelm), "Armure de Plaque", "Casque Clos", 40.0, 70.0, typeof(FerIngot), "Lingots", 15, 1044037);

			#endregion

            #region Plaque Elfique
            index = AddCraft(typeof(ElfiquePlaqueGorget), "Armure de Plaque", "Gorget de Plaque Elfique", 45.0, 75.0, typeof(FerIngot), "Lingots", 10, 1044037);
            
            index = AddCraft(typeof(ElfiquePlaqueLeggings), "Armure de Plaque", "Jambieres de Plaque Elfique", 45.0, 75.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(ElfiquePlaqueTunic), "Armure de Plaque", "Cuirasse de Plaque Elfique", 45.0, 75.0, typeof(FerIngot), "Lingots", 25, 1044037);
            #endregion

            #region Plaque Gothique
            index = AddCraft(typeof(BrassardsGothique), "Armure de Plaque", "Brassards de Plaque Gothique", 50.0, 80.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(CuirasseGothique), "Armure de Plaque", "Cuirasse de Plaque Gothique", 50.0, 80.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CasqueGothique), "Armure de Plaque", "Casque � Cornes Gothique", 50.0, 80.0, typeof(FerIngot), "Lingots", 15, 1044037);
            #endregion

            #region Plaque Barbare
            index = AddCraft(typeof(PlaqueBarbareGreaves), "Armure de Plaque", "Brassards de Plaque Barbare", 50.0, 80.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(PlaqueBarbareGorget), "Armure de Plaque", "Gorget de Plaque Barbare", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(PlaqueBarbareLeggings), "Armure de Plaque", "Jambieres de Plaque Barbare", 50.0, 80.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(PlaqueBarbareTunic), "Armure de Plaque", "Cuirasse de Plaque Barbare", 50.0, 80.0, typeof(FerIngot), "Lingots", 25, 1044037);
            #endregion

            #region Plaque Orne
            index = AddCraft(typeof(BrassardsOrne), "Armure de Plaque", "Brassards de Plaque Orn�", 60.0, 90.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(CuirasseOrne), "Armure de Plaque", "Cuirasse de Plaque Orn�", 60.0, 90.0, typeof(FerIngot), "Lingots", 25, 1044037);
            #endregion

            #region Plaque Decorer
            index = AddCraft(typeof(BrassardsDecorer), "Armure de Plaque", "Brassards de Plaque D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(GantsDecorer), "Armure de Plaque", "Gants de Plaque D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(GorgetDecorer), "Armure de Plaque", "Gorget de Plaque D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(JambieresDecorer), "Armure de Plaque", "Jambieres de Plaque D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(CuirasseDecorer), "Armure de Plaque", "Cuirasse de Plaque D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CasqueDecorer), "Armure de Plaque", "Casque de Plaque D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(CasqueClosDecorer), "Armure de Plaque", "Casque Clos D�cor�", 65.0, 95.0, typeof(FerIngot), "Lingots", 15, 1044037);
            #endregion

            #region Plaque Noble
            index = AddCraft(typeof(PlaqueChevalierGreaves), "Armure de Plaque", "Brassards de Plaque Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(PlaqueChevalierGloves), "Armure de Plaque", "Gants de Plaque Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(PlaqueChevalierGorget), "Armure de Plaque", "Gorget de Plaque Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(PlaqueChevalierLeggings), "Armure de Plaque", "Jambieres de Plaque Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(PlaqueChevalierTunic), "Armure de Plaque", "Cuirasse de Plaque Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(PlaqueChevalierHelm), "Armure de Plaque", "Casque de Plaque Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 15, 1044037);
            #endregion

            #region Plaque Daedric
            index = AddCraft(typeof(ArmureDaedricGreaves), "Armure de Plaque", "Brassards de Plaque Daedric", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(ArmureDaedricGloves), "Armure de Plaque", "Gants de Plaque Daedric", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(ArmureDaedricGorget), "Armure de Plaque", "Gorget de Plaque Daedric", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(ArmureDaedricLeggings), "Armure de Plaque", "Jambieres de Plaque Daedric", 70.0, 100.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(ArmureDaedricTunic), "Armure de Plaque", "Cuirasse de Plaque Daedric", 70.0, 100.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(ArmureDaedricHelm), "Armure de Plaque", "Casque de Plaque Daedric", 70.0, 100.0, typeof(FerIngot), "Lingots", 15, 1044037);
            #endregion

            #region Armure Barbare
            index = AddCraft(typeof(LeggingsBarbare), "Armure Diverses", "Jambieres de Plaque Legere", 30.0, 50.0, typeof(FerIngot), "Lingots", 20, 1044037);

            index = AddCraft(typeof(TunicBarbare), "Armure Diverses", "Cuirasse de Plaque Legere", 30.0, 50.0, typeof(FerIngot), "Lingots", 25, 1044037);
            #endregion

            #region Armures Diverses
            index = AddCraft(typeof(TuniqueChaine), "Armure Diverses", "Bourgeon de Chaines", 20.0, 40.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CuirasseReligieuse), "Armure Diverses", "Cuirasse de Templier", 50.0, 80.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(Cuirasse), "Armure Diverses", "Cuirasse", 60.0, 90.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CuirasseBarbare), "Armure Diverses", "Cuirasse Ancienne", 50.0, 80.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CuirasseNordique), "Armure Diverses", "Cuirasse Nordique", 60.0, 90.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CuirasseDraconique), "Armure Diverses", "Cuirasse Draconique", 70.0, 100.0, typeof(FerIngot), "Lingots", 25, 1044037);

            index = AddCraft(typeof(CasqueNordique), "Armure Diverses", "Casque Nordique", 60.0, 90.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(CasqueSudiste), "Armure Diverses", "Casque Nomade", 60.0, 90.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(CasqueCorne), "Armure Diverses", "Casque � Cornes", 70.0, 100.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(Brassards), "Armure Diverses", "Brassards", 60.0, 90.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BrassardsChaotique), "Armure Diverses", "Brassards Chaotique", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);
            #endregion

			#region Shields
            index = AddCraft(typeof(Buckler), "Boucliers", "Bouclet", 10.0, 30.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(BronzeShield), "Boucliers", "Bouclier Orn�", 20.0, 40.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(MetalShield), "Boucliers", "Bouclier", 30.0, 60.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(WoodenKiteShield), "Boucliers", "Bouclier de Bois", 40.0, 70.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(BouclierGarde), "Boucliers", "Bouclier M�talique", 50.0, 80.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(MetalKiteShield), "Boucliers", "Bouclier Crois�", 50.0, 80.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(ChaosShield), "Boucliers", "Bouclier du Chaos", 40.0, 70.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(OrderShield), "Boucliers", "Bouclier d'Ordre", 50.0, 80.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierComte), "Boucliers", "Bouclier de Karmilide", 50.0, 80.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierMarquis), "Boucliers", "Bouclier de Far�ligue", 50.0, 80.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierDuc), "Boucliers", "Bouclier d'Horl� ", 50.0, 80.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierNordique), "Boucliers", "Bouclier Nordique", 60.0, 90.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierElfique), "Boucliers", "Bouclier Elfique", 60.0, 90.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierChevaleresque), "Boucliers", "Bouclier Elfique", 60.0, 90.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierVieux), "Boucliers", "Vieux Pavois", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(HeaterShield), "Boucliers", "Pavois", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierDecorer), "Boucliers", "Bouclier Decorer", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(BouclierPavoisNoir), "Boucliers", "Bouclier Royal", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);
			#endregion

			#region Bladed

            //�p�es Courtes

            index = AddCraft(typeof(Astoria), "Armes Tranchantes", "Astoria", 10.0, 40.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Biliome), "Armes Tranchantes", "Biliome", 20.0, 50.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Runire), "Armes Tranchantes", "Runire", 30.0, 60.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Myliron), "Armes Tranchantes", "Myliron", 40.0, 70.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Vorlame), "Armes Tranchantes", "Vorlame", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Dawn), "Armes Tranchantes", "Dawn", 60.0, 90.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Lerise), "Armes Tranchantes", "Lerise", 70.0, 100.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Gerumir), "Armes Tranchantes", "Gerumir", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);

            //�p�es Longues

            index = AddCraft(typeof(Broadsword), "Armes Tranchantes", "�p�e", 10.0, 40.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Rodere), "Armes Tranchantes", "Rod�re", 30.0, 60.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Dravene), "Armes Tranchantes", "Drav�ne", 40.0, 70.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Draglast), "Armes Tranchantes", "Draglast", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Merlarme), "Armes Tranchantes", "Merlarme", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Narvegne), "Armes Tranchantes", "Narv�gne", 60.0, 90.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(Hectmore), "Armes Tranchantes", "Hectmore", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);

            //Sabres

            index = AddCraft(typeof(Sabre), "Armes Tranchantes", "Sabre", 20.0, 50.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Mersang), "Armes Tranchantes", "Mersang", 30.0, 60.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Raghash), "Armes Tranchantes", "Raghash", 40.0, 70.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Prisienne), "Armes Tranchantes", "Prisienne", 50.0, 80.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Cutlass), "Armes Tranchantes", "Cutlass", 60.0, 90.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Scimitar), "Armes Tranchantes", "Scimitar", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);

            //�p�es Lourdes

            index = AddCraft(typeof(Vifcoupe), "Armes Tranchantes", "Vifcoupe", 10.0, 40.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Auderre), "Armes Tranchantes", "Aud�rre", 10.0, 40.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Batarde), "Armes Tranchantes", "Batarde", 30.0, 60.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Tranchevil), "Armes Tranchantes", "Tranchevil", 30.0, 60.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Ventmore), "Armes Tranchantes", "Ventmore", 30.0, 60.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Excalior), "Armes Tranchantes", "Excalior", 40.0, 70.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Conquise), "Armes Tranchantes", "Conquise", 40.0, 70.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Atargne), "Armes Tranchantes", "Atargne", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Nerfille), "Armes Tranchantes", "Nerfille", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Querquoise), "Armes Tranchantes", "Querquoise", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Nhilarte), "Armes Tranchantes", "Nhilarte", 50.0, 80.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Abysse), "Armes Tranchantes", "Abysse", 60.0, 90.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(Dorleane), "Armes Tranchantes", "Dorleane", 60.0, 90.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Luminera), "Armes Tranchantes", "Luminera", 70.0, 100.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Couliere), "Armes Tranchantes", "Couli�re", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);

            //Claymore

            index = AddCraft(typeof(Rougegorge), "Armes Tranchantes", "Rougegorge", 20.0, 50.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(Monarque), "Armes Tranchantes", "Monarque", 30.0, 60.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(Claymore), "Armes Tranchantes", "Claymore", 30.0, 60.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(VikingSword), "Armes Tranchantes", "�p�e Lourde", 40.0, 70.0, typeof(FerIngot), "Lingots", 15, 1044037);

            index = AddCraft(typeof(Courbelle), "Armes Tranchantes", "Courbelle", 40.0, 70.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Tranchor), "Armes Tranchantes", "Tranchor", 50.0, 80.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Flamberge), "Armes Tranchantes", "Flamberge", 50.0, 80.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Sombrimur), "Armes Tranchantes", "Sombrimur", 50.0, 80.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Marquaise), "Armes Tranchantes", "Marquaise", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Mortimer), "Armes Tranchantes", "Mortimer", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Espadon), "Armes Tranchantes", "Espadon", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Zweihander), "Armes Tranchantes", "Zweihander", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Morsame), "Armes Tranchantes", "Morsame", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Granlame), "Armes Tranchantes", "Granlame", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);

            //Doubles

            index = AddCraft(typeof(Mirilione), "Armes Tranchantes", "Mirilione", 30.0, 60.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Niropie), "Armes Tranchantes", "Niropie", 70.0, 100.0, typeof(FerIngot), "Lingots", 6, 1044037);

            //Doubles Elfiques

            index = AddCraft(typeof(Zarel), "Armes Tranchantes", "Zarel", 30.0, 60.0, typeof(FerIngot), "Lingots", 6, 1044037);

            index = AddCraft(typeof(Sefrio), "Armes Tranchantes", "Sefrio", 50.0, 80.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Ferel), "Armes Tranchantes", "Ferel", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);

            #endregion

            #region Axes
            index = AddCraft(typeof(Hachette), "Armes Tranchantes", "Hachette", 0.0, 30.0, typeof(FerIngot), "Lingots", 10, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Axe), "Armes Tranchantes", "Hache", 10.0, 40.0, typeof(FerIngot), "Lingots", 10, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(HachetteDouble), "Armes Tranchantes", "Hachette Double", 20.0, 50.0, typeof(FerIngot), "Lingots", 8, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Luminar), "Armes Tranchantes", "Luminar", 30.0, 60.0, typeof(FerIngot), "Lingots", 10, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Loragne), "Armes Tranchantes", "Loragne", 30.0, 60.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Montorgne), "Armes Tranchantes", "Montorgne", 40.0, 70.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(WarAxe), "Armes Tranchantes", "Hache de Guerre", 50.0, 80.0, typeof(FerIngot), "Lingots", 14, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Orcarinia), "Armes Tranchantes", "Orcarinia", 50.0, 80.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Minarque), "Armes Tranchantes", "Minarque", 60.0, 90.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Grochette), "Armes Tranchantes", "Grochette", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Noctame), "Armes Tranchantes", "Noctame", 70.0, 100.0, typeof(FerIngot), "Lingots", 10, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(HacheDouble), "Armes Tranchantes", "Hache M�talique", 20.0, 50.0, typeof(FerIngot), "Lingots", 18, 1044037);

            index = AddCraft(typeof(Morgrove), "Armes Tranchantes", "Morgrove", 20.0, 50.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Venmar), "Armes Tranchantes", "Venmar", 30.0, 60.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(TwoHandedAxe), "Armes Tranchantes", "Hache Double", 40.0, 70.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(LargeBattleAxe), "Armes Tranchantes", "Hache Barbare", 50.0, 80.0, typeof(FerIngot), "Lingots", 14, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Morgate), "Armes Tranchantes", "Morgate", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Coupecrane), "Armes Tranchantes", "Coupecrane", 60.0, 90.0, typeof(FerIngot), "Lingots", 14, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Tranchecorps), "Armes Tranchantes", "Tranchecorps", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Elvetrine), "Armes Tranchantes", "Elvetrine", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Viftranche), "Armes Tranchantes", "Viftranche", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);


            index = AddCraft(typeof(Furagne), "Armes Tranchantes", "Furagne", 30.0, 60.0, typeof(FerIngot), "Lingots", 4, 1044037);
            AddRes(index, typeof(Log), "B�che", 1, 1044351);

            index = AddCraft(typeof(Duxtranche), "Armes Tranchantes", "Duxtranche", 50.0, 80.0, typeof(FerIngot), "Lingots", 6, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Biliane), "Armes Tranchantes", "Biliane", 70.0, 100.0, typeof(FerIngot), "Lingots", 6, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

			#endregion

            #region Fencing

            index = AddCraft(typeof(Dagger), "Armes Perforantes", "Dague", 5.0, 25.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(ButcherKnife), "Armes Perforantes", "Couteau de Boucher", 10.0, 20.0, typeof(FerIngot), "Lingots", 2, 1044037);

            index = AddCraft(typeof(Cleaver), "Armes Perforantes", "Cleaver", 10.0, 20.0, typeof(FerIngot), "Lingots", 2, 1044037);

            index = AddCraft(typeof(Safrine), "Armes Perforantes", "Safrine", 20.0, 50.0, typeof(FerIngot), "Lingots", 2, 1044037);

            index = AddCraft(typeof(Dentsombre), "Armes Perforantes", "Dentsombre", 30.0, 60.0, typeof(FerIngot), "Lingots", 2, 1044037);

            index = AddCraft(typeof(Lozure), "Armes Perforantes", "Lozure", 40.0, 70.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Basilarda), "Armes Perforantes", "Basilarda", 40.0, 70.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Osseuse), "Armes Perforantes", "Osseuse", 50.0, 80.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Serpentine), "Armes Perforantes", "Serpentine", 50.0, 80.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Elvine), "Armes Perforantes", "Elvine", 60.0, 90.0, typeof(FerIngot), "Lingots", 4, 1044037);

            index = AddCraft(typeof(Brillaume), "Armes Perforantes", "Brillaume", 60.0, 90.0, typeof(FerIngot), "Lingots", 4, 1044037);

            index = AddCraft(typeof(Dracourbe), "Armes Perforantes", "Dracourbe", 70.0, 100.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Spadasine), "Armes Perforantes", "Spadasine", 70.0, 100.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Ecorchette), "Armes Perforantes", "�corchette", 70.0, 100.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Poignard), "Armes Perforantes", "Poignard", 30.0, 50.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Eblame), "Armes Perforantes", "Eblame", 50.0, 80.0, typeof(FerIngot), "Lingots", 4, 1044037);

            index = AddCraft(typeof(Imperlame), "Armes Perforantes", "Imperlame", 70.0, 100.0, typeof(FerIngot), "Lingots", 3, 1044037);

            index = AddCraft(typeof(Fleuret), "Armes Perforantes", "Fleuret", 10.0, 40.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Percille), "Armes Perforantes", "Percille", 20.0, 50.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Rapiere), "Armes Perforantes", "Rapiere", 30.0, 60.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Cuivardise), "Armes Perforantes", "Cuivardise", 40.0, 70.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Lyzardese), "Armes Perforantes", "Lyzard�se", 50.0, 80.0, typeof(FerIngot), "Lingots", 8, 1044037);

            index = AddCraft(typeof(Estoc), "Armes Perforantes", "Estoc", 60.0, 90.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Musareche), "Armes Perforantes", "Musar�che", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);

            index = AddCraft(typeof(Brette), "Armes Perforantes", "Brette", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);

            #endregion

            #region Pole Arms

            index = AddCraft(typeof(Bardiche), "Armes d'Hastes", "Bardiche", 20.0, 50.0, typeof(FerIngot), "Lingots", 18, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Scythe), "Armes d'Hastes", "Faux", 30.0, 60.0, typeof(FerIngot), "Lingots", 14, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Vougue), "Armes d'Hastes", "Vougue", 40.0, 70.0, typeof(FerIngot), "Lingots", 18, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(ExecutionersAxe), "Armes Tranchantes", "Gardiche", 60.0, 90.0, typeof(FerIngot), "Lingots", 14, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Cythe), "Armes d'Hastes", "Cythe", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Guisarme), "Armes d'Hastes", "Guisarme", 70.0, 100.0, typeof(FerIngot), "Lingots", 18, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);


            index = AddCraft(typeof(Halberd), "Armes d'Hastes", "Hallebarde", 30.0, 60.0, typeof(FerIngot), "Lingots", 20, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Bardine), "Armes d'Hastes", "Bardine", 40.0, 70.0, typeof(FerIngot), "Lingots", 20, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Hastiche), "Armes d'Hastes", "Hastiche", 50.0, 80.0, typeof(FerIngot), "Lingots", 20, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Helbarde), "Armes d'Hastes", "Helbarde", 60.0, 90.0, typeof(FerIngot), "Lingots", 20, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Granbarde), "Armes d'Hastes", "Granbarde", 70.0, 100.0, typeof(FerIngot), "Lingots", 20, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);


            index = AddCraft(typeof(Lancel), "Armes d'Hastes", "Lancel", 10.0, 40.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Spear), "Armes d'Hastes", "Lance", 20.0, 50.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Terricharde), "Armes d'Hastes", "Terricharde", 30.0, 60.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Percetronc), "Armes d'Hastes", "Percetronc", 40.0, 70.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(ShortSpear), "Armes d'Hastes", "Hastone", 50.0, 80.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(WarFork), "Armes d'Hastes", "Hastal", 50.0, 80.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Lance), "Armes d'Hastes", "Lance de Joute", 60.0, 90.0, typeof(FerIngot), "Lingots", 20, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(DoubleLance), "Armes d'Hastes", "Double Lance", 60.0, 90.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Piculame), "Armes d'Hastes", "Piculame", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Percecoeur), "Armes d'Hastes", "Percecoeur", 70.0, 100.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Pitchfork), "Armes d'Hastes", "Fourche", 20.0, 50.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Pique), "Armes d'Hastes", "Pique", 20.0, 50.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Trident), "Armes d'Hastes", "Trident", 30.0, 60.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Racuris), "Armes d'Hastes", "Racuris", 40.0, 70.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Transpercille), "Armes d'Hastes", "Transpercille", 50.0, 80.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Mascarate), "Armes d'Hastes", "Mascarate", 60.0, 90.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Turione), "Armes d'Hastes", "Turione", 70.0, 100.0, typeof(FerIngot), "Lingots", 14, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

			#endregion

			#region Bashing
            index = AddCraft(typeof(Mace), "Armes Contondantes", "Mace", 20.0, 50.0, typeof(FerIngot), "Lingots", 6, 1044037);

            index = AddCraft(typeof(WarMace), "Armes Contondantes", "Masse de Guerre", 30.0, 60.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(Maul), "Armes Contondantes", "Maul", 40.0, 70.0, typeof(FerIngot), "Lingots", 10, 1044037);

            index = AddCraft(typeof(Brisecrane), "Armes Contondantes", "Brisecrane", 40.0, 70.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(WarHammer), "Armes Contondantes", "Marteau", 40.0, 70.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 3, 1044351);

            index = AddCraft(typeof(Massue), "Armes Contondantes", "Massue", 40.0, 70.0, typeof(FerIngot), "Lingots", 10, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Granmace), "Armes Contondantes", "Granmace", 50.0, 80.0, typeof(FerIngot), "Lingots", 14, 1044037);

            index = AddCraft(typeof(Ecracheur), "Armes Contondantes", "Ecracheur", 50.0, 80.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Ecraseface), "Armes Contondantes", "�craseface", 60.0, 90.0, typeof(FerIngot), "Lingots", 4, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(MarteauGuerre), "Armes Contondantes", "Marteau de Guerre", 60.0, 90.0, typeof(FerIngot), "Lingots", 12, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Fleau), "Armes Contondantes", "Fl�au", 60.0, 90.0, typeof(FerIngot), "Lingots", 16, 1044037);

            index = AddCraft(typeof(Malert), "Armes Contondantes", "Malette", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

            index = AddCraft(typeof(Defonceur), "Armes Contondantes", "D�fonceur", 70.0, 100.0, typeof(FerIngot), "Lingots", 8, 1044037);
            AddRes(index, typeof(Log), "B�ches", 2, 1044351);

            index = AddCraft(typeof(Broyeur), "Armes Contondantes", "Broyeur", 70.0, 100.0, typeof(FerIngot), "Lingots", 16, 1044037);
            AddRes(index, typeof(Log), "B�ches", 4, 1044351);

			#endregion

            #region Poings

            index = AddCraft(typeof(Griffes), "Armes de Poings", "Griffes", 30.0, 60.0, typeof(FerIngot), "Lingots", 12, 1044037);
            index = AddCraft(typeof(Katar), "Armes de Poings", "Katar", 50.0, 80.0, typeof(FerIngot), "Lingots", 13, 1044037);
            index = AddCraft(typeof(Katara), "Armes de Poings", "Katara", 70.0, 100.0, typeof(FerIngot), "Lingots", 14, 1044037);

            #endregion

			// Set the overridable material
			SetSubRes( typeof( FerIngot ), "Fer" );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( FerIngot ),			"Fer", 00.0, 1044267 );
			AddSubRes( typeof( CuivreIngot ),   	"Cuivre", 40.0, 1044268 );
			AddSubRes( typeof( BronzeIngot ),   	"Bronze", 40.0, 1044268 );
			AddSubRes( typeof( AcierIngot ),		"Acier", 60.0, 1044268 );
			AddSubRes( typeof( ArgentIngot ),		"Argent", 60.0, 1044268 );
			AddSubRes( typeof( OrIngot ),			"Or", 60.0, 1044268 );
			AddSubRes( typeof( MytherilIngot ),		"Mytheril", 80.0, 1044268 );
			AddSubRes( typeof( LuminiumIngot ),		"Luminium", 80.0, 1044268 );
			AddSubRes( typeof( ObscuriumIngot ),	"Obscurium", 80.0, 1044268 );
            AddSubRes( typeof( MystiriumIngot ),    "Mystirium", 90.0, 1044268);
            AddSubRes( typeof( DominiumIngot ),     "Dominium", 90.0, 1044268);
            AddSubRes( typeof( VenariumIngot),        "Venarium", 90.0, 1044268);
            AddSubRes( typeof( EclariumIngot ),     "Eclarium", 100.0, 1044268);
            AddSubRes( typeof( AtheniumIngot ),     "Athenium", 100.0, 1044268);
            AddSubRes( typeof( UmbrariumIngot ),    "Umbrarium", 100.0, 1044268);

			SetSubRes2( typeof( RegularScales ),    "�cailles" );

			AddSubRes2( typeof( RegularScales ),	"�cailles", 0.0, 1044268 );
			AddSubRes2( typeof( NordiqueScales ),	"�cailles Nordiques", 0.0, 1044268 );
			AddSubRes2( typeof( DesertiqueScales ),	"�cailles D�sertiques", 0.0, 1044268 );
			AddSubRes2( typeof( MaritimeScales ),	"�cailles Maritimes", 0.0, 1044268 );
			AddSubRes2( typeof( VolcaniqueScales ),	"�cailles Volcaniques", 0.0, 1044268 );
			AddSubRes2( typeof( AncienScales ),		"�cailles Anciennes", 0.0, 1044268 );
            AddSubRes2( typeof( WyrmScales ),       "�cailles Wyrmiques", 0.0, 1044268);

			Resmelt = true;
			Repair = true;
			MarkOption = true;
			CanEnhance = Core.AOS;
		}
	}

	public class ForgeAttribute : Attribute
	{
		public ForgeAttribute()
		{
		}
	}

	public class AnvilAttribute : Attribute
	{
		public AnvilAttribute()
		{
		}
	}
}