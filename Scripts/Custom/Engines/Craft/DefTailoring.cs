using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefTailoring : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Couture; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044005; } // <CENTER>TAILORING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefTailoring() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public static bool IsNonColorable(Type type)
		{
			for (int i = 0; i < m_TailorNonColorables.Length; ++i)
			{
				if (m_TailorNonColorables[i] == type)
				{
					return true;
				}
			}

			return false;
		}

		private static Type[] m_TailorNonColorables = new Type[]
			{
				typeof( OrcHelm )
			};

		private static Type[] m_TailorColorables = new Type[]
			{
				typeof( GozaMatEastDeed ), typeof( GozaMatSouthDeed ),
				typeof( SquareGozaMatEastDeed ), typeof( SquareGozaMatSouthDeed ),
				typeof( BrocadeGozaMatEastDeed ), typeof( BrocadeGozaMatSouthDeed ),
				typeof( BrocadeSquareGozaMatEastDeed ), typeof( BrocadeSquareGozaMatSouthDeed )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( type != typeof( Cloth ) && type != typeof( UncutCloth ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TailorColorables.Length; ++i )
				contains = ( m_TailorColorables[i] == type );

			return contains;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
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
			int index = -1;

			#region Hats
            index = AddCraft(typeof(SkullCap), "Chapeaux", "Bandeau", 0.0, 20.0, typeof(Cloth), "Tissu", 2, 1044287);
            index = AddCraft(typeof(Bandana), "Chapeaux", "Bandana", 0.0, 25.0, typeof(Cloth), "Tissu", 2, 1044287);
            index = AddCraft(typeof(Turban), "Chapeaux", "Turban", 10.0, 20.0, typeof(Cloth), "Tissu", 5, 1044287);
            index = AddCraft(typeof(TurbanLong), "Chapeaux", "Turban Long", 20.0, 30.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(TurbanFeminin), "Chapeaux", "Turban Feminin", 30.0, 40.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(TurbanProtecteur), "Chapeaux", "Turban Protecteur", 40.0, 50.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(TurbanVoile), "Chapeaux", "Turban Voil�", 50.0, 60.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(TurbanAmple), "Chapeaux", "Turban Ample", 50.0, 60.0, typeof(Cloth), "Tissu", 7, 1044287);
            index = AddCraft(typeof(TurbanNoble), "Chapeaux", "Turban Noble", 60.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(FloppyHat), "Chapeaux", "Large Chapeau", 6.2, 31.2, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(Cap), "Chapeaux", "Coiffe", 10.0, 30.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(WideBrimHat), "Chapeaux", "Chapeau � large bord", 10.0, 30.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(StrawHat), "Chapeaux", "Chapeau de Paille", 20.0, 40.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TallStrawHat), "Chapeaux", "Grand Chapeau de Paille", 20.0, 40.0, typeof(Cloth), "Tissu", 13, 1044287);
            index = AddCraft(typeof(WizardsHat), "Chapeaux", "B�ret", 7.2, 32.2, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(Bonnet), "Chapeaux", "Bonnet", 30.0, 50.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(FeatheredHat), "Chapeaux", "Chapeau � plume", 40.0, 60.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(TricorneHat), "Chapeaux", "Tricone", 40.0, 60.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(JesterHat), "Chapeaux", "Chapeau des Fols", 50.0, 70.0, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(ChapeauCourt), "Chapeaux", "Chapeau de Court", 60.0, 80.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(ChapeauPlume), "Chapeaux", "Chapeau Mousquetaire", 70.0, 90.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(ChapeauMelon), "Chapeaux", "Chapeau Melon", 70.0, 90.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(ChapeauNoble), "Chapeaux", "B�ret", 80.0, 90.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(ChapeauLoup), "Chapeaux", "T�te de Loup", 60.0, 80.0, typeof(Leather), "Cuir", 6, 1044463);
			#endregion
			
			#region Coussins
			index = AddCraft(typeof(CoussinB), "Coussins", "Petit coussin carr�", 20.0, 40.0, typeof(Cloth), "Tissu", 3, 1044287);
            AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinA), "Coussins", "Gros oreiller", 40.0, 60.0, typeof(Cloth), "Tissu", 4, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinC), "Coussins", "Gros oreiller de velours", 50.0, 70.0, typeof(Cloth), "Tissu", 4, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinD), "Coussins", "Gros oreiller carr�", 50.0, 70.0, typeof(Cloth), "Tissu", 4, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinE), "Coussins", "Petit coussin rond clair", 0.0, 30.0, typeof(Cloth), "Tissu", 3, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinF), "Coussins", "petit coussin rond sombre", 0.0, 30.0, typeof(Cloth), "Tissu", 3, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinG), "Coussins", "Petit coussin carr� � pompon", 30.0, 50.0, typeof(Cloth), "Tissu", 3, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinH), "Coussins", "Autre petit coussin carr� � pompon", 30.0, 50.0, typeof(Cloth), "Tissu", 3, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinI), "Coussins", "Gros coussin carr�", 40.0, 60.0, typeof(Cloth), "Tissu", 3, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
			index = AddCraft(typeof(CoussinJ), "Coussins", "Gros coussin carr� droit", 0.0, 60.0, typeof(Cloth), "Tissu", 3, 1044287);
			AddRes(index, typeof(Feather), "Plumes", 5, 1044563);
            #endregion
			
            #region Toges
            index = AddCraft(typeof(Robe), "Toges & Manteaux", "Toge", 0.0, 30.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(TogeSoutane), "Toges & Manteaux", "Soutane", 20.0, 40.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogePelerin), "Toges & Manteaux", "Toge P�lerine", 30.0, 50.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeReligieuse), "Toges & Manteaux", "Toge Religieuse", 30.0, 50.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeNomade), "Toges & Manteaux", "Toge de Nomade", 40.0, 60.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeOrient), "Toges & Manteaux", "Toge d'Orient", 40.0, 60.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(Toge), "Toges & Manteaux", "Toge Arcanique", 40.0, 60.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeVoyage), "Toges & Manteaux", "Toge Voyage", 40.0, 60.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeDecore), "Toges & Manteaux", "Toge D�cor�e", 50.0, 70.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeDiciple), "Toges & Manteaux", "Toge Moniale", 50.0, 70.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeElfique), "Toges & Manteaux", "Toge d'Elfe", 50.0, 70.0, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(TogeGoetie), "Toges & Manteaux", "Toge Sinistre", 50.0, 80.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(TogeDrow), "Toges & Manteaux", "Toge d'Alfar", 50.0, 80.0, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(TogeHautElfe), "Toges & Manteaux", "Toge d'Haut Elfe", 60.0, 80.0, typeof(Cloth), "Tissu", 18, 1044287); 
            index = AddCraft(typeof(TogeAmple), "Toges & Manteaux", "Toge Ample", 70.0, 90.0, typeof(Cloth), "Tissu", 18, 1044287);
            index = AddCraft(typeof(TogeMystique), "Toges & Manteaux", "Toge Mystique", 70.0, 90.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(TogeArchiMage), "Toges & Manteaux", "Toge d'Archimage", 80.0, 100.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(TogeFeminine), "Toges & Manteaux", "Toge de Pr�trise", 90.0, 110.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(TogeSorcier), "Toges & Manteaux", "Toge de Sorcier", 90.0, 110.0, typeof(Cloth), "Tissu", 18, 1044287);
            index = AddCraft(typeof(TogeOr), "Toges & Manteaux", "Toge d'Or", 90.0, 120.0, typeof(Cloth), "Tissu", 18, 1044287);
            index = AddCraft(typeof(ManteauPardessus), "Toges & Manteaux", "Vieux Manteau", 30.0, 60.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(ManteauTabar), "Toges & Manteaux", "Manteau d'Arme", 40.0, 70.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(ManteauCourt), "Toges & Manteaux", "Manteau Court", 60.0, 80.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(ManteauLong), "Toges & Manteaux", "Manteau Long", 70.0, 90.0, typeof(Cloth), "Tissu", 13, 1044287);
            index = AddCraft(typeof(ManteauRaye), "Toges & Manteaux", "Manteau Ray�", 80.0, 100.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(ManteauNoble), "Toges & Manteaux", "Manteau Noble", 90.0, 120.0, typeof(Cloth), "Tissu", 18, 1044287);
            #endregion

            #region Capes
            index = AddCraft(typeof(CapeCourte), "Capes", "Cape Courte", 10.0, 40.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(CapeVoyage), "Capes", "Cape de Voyage", 20.0, 50.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeBarbare), "Capes", "Cape Barbare", 30.0, 60.0, typeof(Leather), "Cuir", 14, 1044463);
            index = AddCraft(typeof(CapeNordique), "Capes", "Cape Nordique", 30.0, 60.0, typeof(Leather), "Cuir", 14, 1044463);
            index = AddCraft(typeof(Cloak), "Capes", "Cape", 40.0, 70.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeEtendard), "Capes", "Cape �tendard", 40.0, 70.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeCapuche), "Capes", "Cape � Capuche", 40.0, 70.0, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(CapeCol), "Capes", "Cape � Col", 40.0, 70.0, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(CapeColLong), "Capes", "Cape � Long Col", 50.0, 80.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(CapeSolide), "Capes", "Cape Solide", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeEpauliere), "Capes", "Cape � �pauli�res", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeDecore), "Capes", "Cape D�cor�e", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeLongue), "Capes", "Cape Longue", 60.0, 90.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(CapeTrainee), "Capes", "Cape � Train�e", 60.0, 90.0, typeof(Cloth), "Tissu", 17, 1044287);
            index = AddCraft(typeof(CapeCagoule), "Capes", "Cape � Cagoule", 60.0, 90.0, typeof(Cloth), "Tissu", 15, 1044287);
            index = AddCraft(typeof(CapeNoble), "Capes", "Cape Noble", 70.0, 100.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(Voile), "Capes", "Voile", 70.0, 100.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(CapeFeminine), "Capes", "Cape F�minine", 70.0, 100.0, typeof(Cloth), "Tissu", 13, 1044287);
            index = AddCraft(typeof(CapeFourrure), "Capes", "Cape de Cuir", 80.0, 110.0, typeof(Leather), "Cuir", 14, 1044463);
            index = AddCraft(typeof(CapePoil), "Capes", "Cape de Poil", 80.0, 110.0, typeof(Leather), "Cuir", 14, 1044463);
            index = AddCraft(typeof(CapeJarl), "Capes", "Cape de Fourrure", 90.0, 120.0, typeof(Leather), "Cuir", 14, 1044463);
            index = AddCraft(typeof(CapePlume), "Capes", "Cape � Plumes", 90.0, 120.0, typeof(Feather), "Plumes", 60, 1044563);
            index = AddCraft(typeof(CapeSombre), "Capes", "Cape Sombre", 90.0, 120.0, typeof(Cloth), "Tissu", 60, 1044287);
            #endregion

            #region Robes
            index = AddCraft(typeof(RobeDechire), "Robes", "Robe d�chir�e", 0.0, 5.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(RobeDrow), "Robes", "Robe Ancienne", 5.0, 10.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(RobePetite), "Robes", "Petite Robe", 10.0, 20.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(RobeSoubrette), "Robes", "Robe de Soubrette", 10.0, 20.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(PlainDress), "Robes", "Robe Simple", 15.0, 30.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeSimple), "Robes", "Robe", 15.0, 30.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeOrdinaire), "Robes", "Robe Ordinaire", 15.0, 30.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeGamine), "Robes", "Robe de Gamine", 20.0, 50.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeEnfantine), "Robes", "Robe Enfantine", 20.0, 50.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeDomestique), "Robes", "Robe de Domestique", 25.0, 55.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeBohemienne), "Robes", "Robe Boh�mienne", 25.0, 55.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeOrcish), "Robes", "Robe Orcish", 25.0, 55.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeGitane), "Robes", "Robe de Gitane", 25.0, 55.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeACeinture), "Robes", "Robe a Ceinture", 30.0, 60.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeSansManches), "Robes", "Robe Sans Manches", 30.0, 60.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeDemoiselle), "Robes", "Robe de Demoiselle", 30.0, 60.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeFleurit), "Robes", "Robe Fleurit", 35.0, 65.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeServeuse), "Robes", "Robe de Serveuse", 35.0, 65.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeServante), "Robes", "Robe de Servante", 35.0, 65.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeAubergiste), "Robes", "Robe d'Aubergiste", 40.0, 70.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeSobre), "Robes", "Robe Sobre", 40.0, 70.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RobeOrne), "Robes", "Robe Orn�", 40.0, 70.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(RobeAmusante), "Robes", "Robe Amusante", 45.0, 75.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeElegante), "Robes", "Robe �l�gante", 45.0, 75.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeSeduisante), "Robes", "Robe S�duisante", 45.0, 75.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeOrient), "Robes", "Robe d'Orient", 50.0, 80.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeOrientale), "Robes", "Robe de Nomades", 50.0, 80.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeBourgeoise), "Robes", "Robe Bourgeoise", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeGrande), "Robes", "Grande Robe", 55.0, 85.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(RobeLarge), "Robes", "Large Robe", 55.0, 85.0, typeof(Cloth), "Tissu", 18, 1044287);
            index = AddCraft(typeof(RobeAmple), "Robes", "Robe Ample", 55.0, 85.0, typeof(Cloth), "Tissu", 18, 1044287);
            index = AddCraft(typeof(RobeAvecCorset), "Robes", "Robe Avec Corset", 60.0, 90.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeACorset), "Robes", "Robe � Corset", 60.0, 90.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeCorsetAmple), "Robes", "Robe � Corset Ample", 60.0, 90.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(RobeCharmante), "Robes", "Robe Charmante", 65.0, 95.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeAttrayante), "Robes", "Robe Attrayante", 65.0, 95.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeDore), "Robes", "Robe Dor�", 70.0, 100.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeNoble), "Robes", "Robe Noble", 70.0, 100.0, typeof(Cloth), "Tissu", 18, 1044287);
            index = AddCraft(typeof(RobeCourt), "Robes", "Robe de Court", 75.0, 105.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeAraneide), "Robes", "Robe Aran�ide", 75.0, 105.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeCourteDrow), "Robes", "Robe Courte Elfe Noire", 75.0, 105.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(RobeNymph), "Robes", "Robe Nymph", 75.0, 105.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(RobeAraignee), "Robes", "Robe Araign�e", 80.0, 110.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(RobeAntique), "Robes", "Robe Antique", 80.0, 110.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(FancyDress), "Robes", "Robe D�cor�", 80.0, 110.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeElfique), "Robes", "Robe Elfique", 85.0, 115.0, typeof(Cloth), "Tissu", 13, 1044287);
            index = AddCraft(typeof(RobeAmpleElfique), "Robes", "Robe Ample Elfique", 85.0, 115.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(RobeElfe), "Robes", "Robe d'Elfe", 85.0, 115.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(RobeElfeNoir), "Robes", "Robe d'Alfar", 90.0, 120.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(RobeJuponElfique), "Robes", "Robe � Jupon Elfique", 90.0, 120.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(RobeSombre), "Robes", "Robe Sombre", 90.0, 120.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(RobeOuverte), "Robes", "Robe Ouverte", 90.0, 120.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(Robetrainante), "Robes", "Robe Trainante", 90.0, 120.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(RobeCourte), "Robes", "Robe Courte", 90.0, 120.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(RobeBoheme), "Robes", "Robe Boh�me", 95.0, 125.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(RobeDentelle), "Robes", "Robe avec Dentelle", 95.0, 125.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(RobeTrainee), "Robes", "Robe avec Train�e", 95.0, 125.0, typeof(Cloth), "Tissu", 16, 1044287);
            index = AddCraft(typeof(RobeMariage), "Robes", "Robe de Mariage", 95.0, 125.0, typeof(Cloth), "Tissu", 16, 1044287);
            #endregion

            #region Shirts
            index = AddCraft(typeof(ChandailCourtDechire), "Tuniques & Chemises", "Chandail D�chir�", 0.0, 10.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(ChandailDechire), "Tuniques & Chemises", "Chemise D�chir�", 0.0, 10.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(ChandailLongDechire), "Tuniques & Chemises", "Chandail Long D�chir�", 0.0, 10.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(TuniqueDechire), "Tuniques & Chemises", "Tunique D�chir�e", 0.0, 10.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TuniqueLongueDechire), "Tuniques & Chemises", "Tunique Longue D�chir�e", 0.0, 10.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(TabarDechire), "Tuniques & Chemises", "Tabar D�chir�", 0.0, 10.0, typeof(Cloth), "Tissu", 10, 1044287);

            index = AddCraft(typeof(ChandailCourtBarbare), "Tuniques & Chemises", "Chandail Court Barbare", 5.0, 20.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(ChandailLongBarbare), "Tuniques & Chemises", "Chandail Long Barbare", 5.0, 20.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailVieux), "Tuniques & Chemises", "Vieux Chandail", 10.0, 30.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(SoutienGorge), "Tuniques & Chemises", "Petit Soutien Gorge", 20.0, 40.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailSoutienGorge), "Tuniques & Chemises", "Soutien Gorge", 20.0, 40.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(Chandail), "Tuniques & Chemises", "Chandail", 30.0, 50.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(Shirt), "Tuniques & Chemises", "Chandail Sans Manches", 30.0, 50.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailSombre), "Tuniques & Chemises", "Chandail Sombre", 40.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(FancyShirt), "Tuniques & Chemises", "Chandail � manches longues", 40.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailBordel), "Tuniques & Chemises", "Chandail de Bordel", 50.0, 80.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailDecore), "Tuniques & Chemises", "Chandail Decor�", 60.0, 90.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailCourt), "Tuniques & Chemises", "Chandail Court", 60.0, 90.0, typeof(Cloth), "Tissu", 7, 1044287);
            index = AddCraft(typeof(ChandailMarin), "Tuniques & Chemises", "Chandail Marin", 70.0, 100.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailCombat), "Tuniques & Chemises", "Chandail de Combat", 80.0, 110.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailFeminin), "Tuniques & Chemises", "Chandail F�minin", 80.0, 110.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChandailNoble), "Tuniques & Chemises", "Chandail Noble", 90.0, 120.0, typeof(Cloth), "Tissu", 8, 1044287);

            index = AddCraft(typeof(ChemiseOrient), "Tuniques & Chemises", "Chemise d'Orient", 10.0, 30.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChemiseCol), "Tuniques & Chemises", "Chemise � Col", 30.0, 50.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChemiseReligieuse), "Tuniques & Chemises", "Chemise Religieuse", 40.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(Chemiselacee), "Tuniques & Chemises", "Chemise Lac�e", 40.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChemiseBourgeoise), "Tuniques & Chemises", "Chemise Bourgeoise", 50.0, 80.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(ChemiseGaine), "Tuniques & Chemises", "Chemise � Gaine", 60.0, 90.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(ChemiseLongue), "Tuniques & Chemises", "Chemise � manches longues", 70.0, 100.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(ChemiseElfique), "Tuniques & Chemises", "Chemise Elfique", 70.0, 100.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(ChemiseAmple), "Tuniques & Chemises", "Chemise Ample", 80.0, 110.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(ChemiseNoble), "Tuniques & Chemises", "Chemise Noble", 90.0, 120.0, typeof(Cloth), "Tissu", 10, 1044287);

            index = AddCraft(typeof(CorsetPetit), "Tuniques & Chemises", "Petit Corset", 10.0, 30.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(CorsetOuvert), "Tuniques & Chemises", "Corset Ouvert", 30.0, 50.0, typeof(Cloth), "Tissu", 7, 1044287);
            index = AddCraft(typeof(Corset), "Tuniques & Chemises", "Corset Simple", 50.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(CorsetLong), "Tuniques & Chemises", "Corset Long", 60.0, 90.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(CorsetAmple), "Tuniques & Chemises", "Corset Ample", 70.0, 100.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(CorsetSombre), "Tuniques & Chemises", "Corset Sombre", 80.0, 110.0, typeof(Cloth), "Tissu", 8, 1044287);

            index = AddCraft(typeof(Doublet), "Tuniques & Chemises", "Doublet", 30.0, 50.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(DoubletBouton), "Tuniques & Chemises", "Doublet � Boutons", 50.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(DoubletAmple), "Tuniques & Chemises", "Doublet Ample", 60.0, 90.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(DoubletFeminin), "Tuniques & Chemises", "Doublet d'Alfar", 70.0, 100.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(DoubletArmure), "Tuniques & Chemises", "Doublet Armur�", 80.0, 110.0, typeof(Cloth), "Tissu", 9, 1044287);

            index = AddCraft(typeof(TuniqueOuverte), "Tuniques & Chemises", "Tunique Ouverte", 0.0, 20.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(TuniquePardessus), "Tuniques & Chemises", "Tunique de Voyage", 10.0, 30.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(Tunic), "Tuniques & Chemises", "Tunique", 20.0, 50.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(TuniquePaysanne), "Tuniques & Chemises", "Tunique Simple", 30.0, 60.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(TuniqueVoyage), "Tuniques & Chemises", "Tunique de Voyage", 40.0, 70.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(Tunique), "Tuniques & Chemises", "Large Tunique", 40.0, 70.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(TuniqueAmple), "Tuniques & Chemises", "Tunique Ample", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniquePirate), "Tuniques & Chemises", "Tunique de Pirate", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(JesterSuit), "Tuniques & Chemises", "Tunique des Fols", 60.0, 90.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniqueOrientale), "Tuniques & Chemises", "Tunique Orientale", 60.0, 90.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniqueNomade), "Tuniques & Chemises", "Tunique de Nomade", 70.0, 100.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniqueBourgeoise), "Tuniques & Chemises", "Tunique Bourgeoise", 70.0, 100.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniquePage), "Tuniques & Chemises", "Tunique de Page", 80.0, 110.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniqueAssassin), "Tuniques & Chemises", "Tunique d'Assassin", 80.0, 110.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TuniqueNoble), "Tuniques & Chemises", "Tunique Noble", 90.0, 120.0, typeof(Cloth), "Tissu", 14, 1044287);

            index = AddCraft(typeof(Veston), "Tuniques & Chemises", "Veston", 10.0, 30.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(VesteCuir), "Tuniques & Chemises", "Veste de Cuir", 30.0, 50.0, typeof(Leather), "Cuir", 10, 1044463);
            index = AddCraft(typeof(VestePoil), "Tuniques & Chemises", "Veste de Poil", 40.0, 60.0, typeof(Leather), "Cuir", 12, 1044463);
            index = AddCraft(typeof(Veste), "Tuniques & Chemises", "Veste", 60.0, 80.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(VesteLourde), "Tuniques & Chemises", "Veste Ample", 70.0, 90.0, typeof(Cloth), "Tissu", 12, 1044287);

            index = AddCraft(typeof(Surcoat), "Tuniques & Chemises", "Surcot", 30.0, 60.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TabarCourt), "Tuniques & Chemises", "Tabar Court", 50.0, 80.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TabarReligieux), "Tuniques & Chemises", "Tabar Religieux", 70.0, 90.0, typeof(Cloth), "Tissu", 14, 1044287);
            index = AddCraft(typeof(TabarLong), "Tuniques & Chemises", "Tabar Long", 80.0, 100.0, typeof(Cloth), "Tissu", 18, 1044287);

			#endregion

			#region Pants
            index = AddCraft(typeof(PantalonsDechires), "Pantalons & Jupes", "Pantalons D�chir�s", 0.0, 10.0, typeof(Cloth), "Tissu", 5, 1044287);
            index = AddCraft(typeof(PantalonsPauvre), "Pantalons & Jupes", "Pantalons Pauvres", 0.0, 10.0, typeof(Cloth), "Tissu", 6, 1044287);

            index = AddCraft(typeof(ShortPants), "Pantalons & Jupes", "Pantalons Courts", 5.0, 20.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(LongPants), "Pantalons & Jupes", "Pantalons Longs", 10.0, 30.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(PantalonsOuvert), "Pantalons & Jupes", "Pantalons Ouvert", 20.0, 50.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(PantalonsOrient), "Pantalons & Jupes", "Pantalons d'Orient", 30.0, 60.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(PantalonsNordique), "Pantalons & Jupes", "Pantalons Nordiques", 40.0, 70.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(PantalonsNomade), "Pantalons & Jupes", "Pantalons de Nomades", 40.0, 70.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(Pantalons), "Pantalons & Jupes", "Pantalons Simples", 50.0, 80.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(PantalonsCourts), "Pantalons & Jupes", "Pantalons Amples Courts", 50.0, 80.0, typeof(Cloth), "Tissu", 7, 1044287);
            index = AddCraft(typeof(PantalonsLongs), "Pantalons & Jupes", "Pantalons Amples Longs", 60.0, 90.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(PantalonsCuir), "Pantalons & Jupes", "Pantalons de Cuir", 70.0, 100.0, typeof(Leather), "Cuir", 8, 1044463);
            index = AddCraft(typeof(PantalonsMoulant), "Pantalons & Jupes", "Pantalons Moulants", 80.0, 110.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(PantalonsArmure), "Pantalons & Jupes", "Pantalons Armur�", 90.0, 120.0, typeof(Leather), "Cuir", 9, 1044463);

            index = AddCraft(typeof(Kilt), "Pantalons & Jupes", "Kilt", 10.0, 30.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(TuniqueKilt), "Pantalons & Jupes", "Long Kilt", 30.0, 50.0, typeof(Cloth), "Tissu", 12, 1044287);

            index = AddCraft(typeof(JupeOuvrier), "Pantalons & Jupes", "Jupe Dechir�", 0.0, 10.0, typeof(Cloth), "Tissu", 3, 1044287);
            index = AddCraft(typeof(Jupette), "Pantalons & Jupes", "Jupette", 5.0, 20.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(JupeCourte), "Pantalons & Jupes", "Jupe Courte", 10.0, 30.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(Skirt), "Pantalons & Jupes", "Jupe Simple", 20.0, 40.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Jupe), "Pantalons & Jupes", "Jupe", 20.0, 50.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(JupeHakama), "Pantalons & Jupes", "Hakama", 30.0, 60.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(JupeCourteBarbare), "Pantalons & Jupes", "Jupe Courte Barbare", 30.0, 60.0, typeof(Leather), "Cuir", 4, 1044463);
            index = AddCraft(typeof(JupeLongueBarbare), "Pantalons & Jupes", "Jupe Longue Barbare", 30.0, 60.0, typeof(Leather), "Cuir", 6, 1044463);
            index = AddCraft(typeof(JupeCuir), "Pantalons & Jupes", "Jupe de Cuir", 40.0, 70.0, typeof(Leather), "Cuir", 6, 1044463);
            index = AddCraft(typeof(JupeOrcish), "Pantalons & Jupes", "Jupe Orcish", 40.0, 70.0, typeof(Leather), "Cuir", 6, 1044463);
            index = AddCraft(typeof(JupeNomade), "Pantalons & Jupes", "Jupe Nomade", 40.0, 70.0, typeof(Leather), "Cuir", 6, 1044463);
            index = AddCraft(typeof(JupeOuverte), "Pantalons & Jupes", "Jupe Ouverte", 50.0, 80.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(JupeDecore), "Pantalons & Jupes", "Jupe Decor�", 50.0, 80.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(JupeLongue), "Pantalons & Jupes", "Jupe Longue", 60.0, 90.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(JupeAPans), "Pantalons & Jupes", "Jupe a Pans", 60.0, 90.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(JupeOrient), "Pantalons & Jupes", "Jupe d'Orient", 70.0, 100.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(JupeAmple), "Pantalons & Jupes", "Jupe Ample", 70.0, 100.0, typeof(Cloth), "Tissu", 12, 1044287);
            index = AddCraft(typeof(JupeGrande), "Pantalons & Jupes", "Grande Jupe", 80.0, 110.0, typeof(Cloth), "Tissu", 13, 1044287);
            index = AddCraft(typeof(JupeBordel), "Pantalons & Jupes", "Jupe de Bordel", 80.0, 110.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(JupeNoble), "Pantalons & Jupes", "Jupe Noble", 90.0, 120.0, typeof(Cloth), "Tissu", 13, 1044287);

			#endregion

			#region Misc
            index = AddCraft(typeof(BodySash), "Accessoires", "Ceinture de Torse", 0.0, 10.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(Cocarde), "Accessoires", "Cocarde", 20.0, 40.0, typeof(Cloth), "Tissu", 5, 1044287);
            index = AddCraft(typeof(CeintureTorseGrande), "Accessoires", "Large Ceinture de Torse", 30.0, 50.0, typeof(Cloth), "Tissu", 8, 1044287);

            index = AddCraft(typeof(HalfApron), "Accessoires", "Demi-Tablier", 10.0, 30.0, typeof(Cloth), "Tissu", 6, 1044287);
            index = AddCraft(typeof(FullApron), "Accessoires", "Tablier", 25.0, 45.0, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TablierBarbare), "Accessoires", "Tablier Barbare", 40.0, 50.0, typeof(Cloth), "Tissu", 10, 1044287);

            index = AddCraft(typeof(SousVetement), "Accessoires", "Sous-Vetement", 10.0, 30.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(JartellesBlanches), "Accessoires", "Jartelles", 25.0, 45.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(JartellesNoir), "Accessoires", "Jartelles Sombres", 30.0, 50.0, typeof(Cloth), "Tissu", 11, 1044287);
            index = AddCraft(typeof(Jartelles), "Accessoires", "Jartelles Compl�tes", 40.0, 60.0, typeof(Cloth), "Tissu", 14, 1044287);

            
            
            index = AddCraft(typeof(Bracer), "Accessoires", "Bracer", 30.1, 50.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(BrassardsFeminins), "Accessoires", "Brassards Feminins", 40.0, 60.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(BrassardsCommun), "Accessoires", "Brassards", 50.0, 70.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(BrassardsSimples), "Accessoires", "Brassards Simples", 60.0, 80.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(GantsSombres), "Accessoires", "Gants Sombres", 70.0, 90.0, typeof(Cloth), "Tissu", 6, 1044287);

            index = AddCraft(typeof(Foulard), "Accessoires", "Foulard", 20.0, 40.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(CagouleGorget), "Accessoires", "Petite Cagoule", 30.0, 50.0, typeof(Cloth), "Tissu", 3, 1044287);
            index = AddCraft(typeof(FoulardProtecteur), "Accessoires", "Foulard Protecteur", 30.0, 50.0, typeof(Cloth), "Tissu", 7, 1044287);
            index = AddCraft(typeof(FoulardNoble), "Accessoires", "Foulard Noble", 40.0, 60.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(CagouleCuir), "Accessoires", "Cagoule de Cuir", 50.0, 70.0, typeof(Leather), "Cuir", 3, 1044463);
            index = AddCraft(typeof(Cagoule), "Accessoires", "Cagoule", 50.0, 70.0, typeof(Cloth), "Tissu", 7, 1044287);
            index = AddCraft(typeof(Capuche), "Accessoires", "Capuche", 60.0, 80.0, typeof(Cloth), "Tissu", 8, 1044287);
            index = AddCraft(typeof(CapucheGrande), "Accessoires", "Grande Capuche", 60.0, 80.0, typeof(Cloth), "Tissu", 9, 1044287);
            index = AddCraft(typeof(CagouleGrande), "Accessoires", "Grande Cagoule", 70.0, 90.0, typeof(Cloth), "Tissu", 9, 1044287);

            index = AddCraft(typeof(Plume), "Accessoires", "Plume", 0.0, 0.0, typeof(Feather), "Plume", 1, 1044563);
            index = AddCraft(typeof(BandagesBras), "Accessoires", "Bandages de Bras", 10.0, 30.0, typeof(Cloth), "Tissu", 2, 1044287);
            index = AddCraft(typeof(BandagesTorse), "Accessoires", "Bandages de Torse", 20.0, 40.0, typeof(Cloth), "Tissu", 3, 1044287);
            index = AddCraft(typeof(BandagesJambes), "Accessoires", "Bandages de Jambes", 30.0, 50.0, typeof(Cloth), "Tissu", 4, 1044287);

            index = AddCraft(typeof(BandeauDroit), "Accessoires", "Bandeau d'Oeil Droit", 10.0, 30.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(BandeauAveugle), "Accessoires", "Bandeau d'Aveugle", 10.0, 30.0, typeof(Cloth), "Tissu", 4, 1044287);
            index = AddCraft(typeof(BandeauGauche), "Accessoires", "Bandeau d'Oeil Gauche", 10.0, 30.0, typeof(Cloth), "Tissu", 4, 1044287);

            index = AddCraft(typeof(Hamac), "Accessoires", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(LitDeCamp), "Accessoires", "Lit De Camp", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(OreillerDePaille), "Accessoires", "Oreiller De Paille", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            AddRes(index, typeof(SheafOfHay), "Paille (Sheaf of Hay)", 5, 1044563);
            index = AddCraft(typeof(ChandailSurpendu), "Accessoires", "Chandail Surpendu", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(PantalonSurpendu), "Accessoires", "Pantalon Surpendu", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(JupeSurpendu), "Accessoires", "Jupe Surpendu", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(HaillonSurpendu), "Accessoires", "Haillon Surpendu", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RideauSurpendu), "Accessoires", "Rideau Surpendu", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            #endregion

            #region Rideaux & Tapis
            index = AddCraft(typeof(RideauSurSocle), "Rideaux & Tapis", "Rideau Sur Socle", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RideauDroit), "Rideaux & Tapis", "Rideau Droit", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RideauBlanc), "Rideaux & Tapis", "Rideau Blanc", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RideauBlancSimple), "Rideaux & Tapis", "Rideau Blanc Simple", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(RideauDessus), "Rideaux & Tapis", "Rideau Dessus", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisBrun), "Rideaux & Tapis", "Tapis Brun", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisVert), "Rideaux & Tapis", "Tapis Vert", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisBleuFleuri), "Rideaux & Tapis", "Tapis Bleu Fleuri", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisRougeDeLys), "Rideaux & Tapis", "Tapis Rouge De Lys", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisBleuAvecMotifs), "Rideaux & Tapis", "Tapis Bleu Avec Motifs", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisJauneAvecMotifs), "Rideaux & Tapis", "Tapis Jaune Avec Motifs", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TapisBleuBourgogne), "Rideaux & Tapis", "Tapis Bleu Bourgogne", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Rideaux & Tapis", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Rideaux & Tapis", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Rideaux & Tapis", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Rideaux & Tapis", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Rideaux & Tapis", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Rideaux & Tapis", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            #endregion

            #region Draperie
            index = AddCraft(typeof(DrapGris), "Draperie", "Drap Gris", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(DrapBlanc), "Draperie", "Drap Blanc", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            #endregion

            /*#region Taxidermie
            index = AddCraft(typeof(TeteOurs), "Draperie", "Tete d'Ours", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TeteCerf), "Draperie", "Tete de Cerf", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(PoissonPlanche), "Draperie", "Poisson sur Planche", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TeteOgre), "Draperie", "Tete d'Ogre", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TeteOrc), "Draperie", "Tete d'Orc", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TeteOursPolaire), "Draperie", "Tete d'Ours Polaire", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(TeteTroll), "Draperie", "Tete de Troll", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            index = AddCraft(typeof(Hamac), "Draperie", "Hamac", 50, 80, typeof(Cloth), "Tissu", 10, 1044287);
            #endregion*/

            

			// Set the overridable material
			SetSubRes(typeof(Leather), "Cuir");

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes(typeof(Leather), "Cuir", 00.0, 1049311);
            AddSubRes(typeof(ReptilienLeather), "Cuir Reptilien", 50.0, 1049311);
            AddSubRes(typeof(NordiqueLeather), "Cuir Nordique", 50.0, 1049311);
            AddSubRes(typeof(DesertiqueLeather), "Cuir D�sertique", 50.0, 1049311);
            AddSubRes(typeof(MaritimeLeather), "Cuir Maritime", 60.0, 1049311);
            AddSubRes(typeof(VolcaniqueLeather), "Cuir Volcanique", 60.0, 1049311);
            AddSubRes(typeof(GeantLeather), "Cuir G�ant", 60.0, 1049311);
            AddSubRes(typeof(MinotaureLeather), "Cuir Minotaure", 70.0, 1049311);
            AddSubRes(typeof(OphidienLeather), "Cuir Ophidien", 70.0, 1049311);
            AddSubRes(typeof(ArachnideLeather), "Cuir Arachnide", 70.0, 1049311);
            AddSubRes(typeof(MagiqueLeather), "Cuir Magique", 80.0, 1049311);
            AddSubRes(typeof(AncienLeather), "Cuir Ancien", 80.0, 1049311);
            AddSubRes(typeof(DemoniaqueLeather), "Cuir Demoniaque", 80.0, 1049311);
            AddSubRes(typeof(DragoniqueLeather), "Cuir Dragonique", 90.0, 1049311);
            AddSubRes(typeof(LupusLeather), "Cuir Lupus", 90.0, 1049311);

			MarkOption = true;
			Repair = Core.AOS;
			CanEnhance = Core.AOS;
		}
	}
}