using System;
using System.Collections.Generic;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.ContextMenus;
using Server.Mobiles;
using System.Text.RegularExpressions;

namespace Server.Items
{
	public enum ClothingQuality
	{
		Low,
		Regular,
		Exceptional
	}

	public interface IArcaneEquip
	{
		bool IsArcane{ get; }
		int CurArcaneCharges{ get; set; }
		int MaxArcaneCharges{ get; set; }
	}

	public abstract class BaseClothing : BaseWearable, IDyable, IScissorable, ICraftable, IWearableDurability
	{
		public virtual bool CanFortify{ get{ return true; } }

		private int m_MaxDurability;
		private int m_Durability;
		private Mobile m_Crafter;
        private string m_CrafterName;
		private ClothingQuality m_Quality;
		private bool m_PlayerConstructed;
		protected CraftResource m_Resource;
		private int m_StrReq = -1;

		private AosAttributes m_AosAttributes;
		private AosArmorAttributes m_AosClothingAttributes;
		private AosSkillBonuses m_AosSkillBonuses;

        private Layer m_BaseLayer;

        public virtual bool Disguise { get { return false; } }

        [CommandProperty(AccessLevel.Batisseur)]
        public Layer BaseLayer
        {
            get { return m_BaseLayer; }
            set { m_BaseLayer = value; InvalidateProperties(); }
        }

		[CommandProperty( AccessLevel.Batisseur )]
		public int MaxDurability
		{
			get{ return m_MaxDurability; }
			set{ m_MaxDurability = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public int Durability
		{
			get 
			{
				return m_Durability;
			}
			set 
			{
				if ( value != m_Durability && MaxDurability > 0 )
				{
					m_Durability = value;

					if ( m_Durability < 0 )
						Delete();
					else if ( m_Durability > MaxDurability )
						m_Durability = MaxDurability;

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

        [CommandProperty(AccessLevel.Batisseur)]
        public string CrafterName
        {
            get { return m_CrafterName; }
            set { m_CrafterName = value; InvalidateProperties(); }
        }

		[CommandProperty( AccessLevel.Batisseur )]
		public int StrRequirement
		{
			get{ return ( m_StrReq == -1 ? (Core.AOS ? BaseStrReq : OldStrReq) : m_StrReq ); }
			set{ m_StrReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public ClothingQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public bool PlayerConstructed
		{
			get{ return m_PlayerConstructed; }
			set{ m_PlayerConstructed = value; }
		}

		public virtual CraftResource DefaultResource{ get{ return CraftResource.None; } }

		[CommandProperty( AccessLevel.Batisseur )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public AosArmorAttributes ClothingAttributes
		{
			get{ return m_AosClothingAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.Batisseur )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		public virtual int ArtifactRarity{ get{ return 0; } }

		public override bool CanEquip( Mobile from )
		{
			if( from.AccessLevel < AccessLevel.Batisseur )
			{
                if( !AllowMaleWearer && !from.Female )
				{
					if( AllowFemaleWearer )
						from.SendLocalizedMessage( 1010388 ); // Only females can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else if( !AllowFemaleWearer && from.Female )
				{
					if( AllowMaleWearer )
						from.SendLocalizedMessage( 1063343 ); // Only males can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else
				{
					int strReq = ComputeStatReq( StatType.Str );

                    if (from.RawStr < strReq)
					{
						from.SendLocalizedMessage( 500213 ); // You are not strong enough to equip that.
						return false;
					}
				}
			}

			return base.CanEquip( from );
		}

		public virtual int BaseStrReq{ get{ return 10; } }
		public virtual int OldStrReq{ get{ return 0; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		public virtual bool AllowMaleWearer{ get{ return true; } }
		public virtual bool AllowFemaleWearer{ get{ return true; } }
		public virtual bool CanBeBlessed{ get{ return true; } }

		public int ComputeStatReq( StatType type )
		{
			int v;

			//if ( type == StatType.Str )
				v = StrRequirement;

			return AOS.Scale( v, 100 - GetLowerStatReq() );
		}
            
		public static void ValidateMobile( Mobile m )
		{
			for ( int i = m.Items.Count - 1; i >= 0; --i )
			{
				if ( i >= m.Items.Count )
					continue;

				Item item = m.Items[i];

				if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;

                    if ( !clothing.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.Batisseur )
					{
						if ( clothing.AllowFemaleWearer )
							m.SendLocalizedMessage( 1010388 ); // Only females can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( clothing );
					}
					else if ( !clothing.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.Batisseur )
					{
						if ( clothing.AllowMaleWearer )
							m.SendLocalizedMessage( 1063343 ); // Only males can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( clothing );
					}
				}
			}
		}

		public int GetLowerStatReq()
		{
			if ( !Core.AOS )
				return 0;

			return m_AosClothingAttributes.LowerStatReq;
		}

		public override void OnAdded(IEntity parent)
		{
			Mobile mob = parent as Mobile;

			if ( mob != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.AddTo( mob );

				mob.CheckStatTimers();
			}

			base.OnAdded( parent );
		}

		public override void OnRemoved(IEntity parent)
		{
			Mobile mob = parent as Mobile;

			if ( mob != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				mob.RemoveStatMod( modName + "Str" );
				mob.RemoveStatMod( modName + "Dex" );
				mob.RemoveStatMod( modName + "Int" );

                if (parent is TMobile)
                {
                    TMobile from = (TMobile)parent;
                    from.BonusHits -= Attributes.BonusHits;
                    from.BonusStam -= Attributes.BonusStam;
                    from.BonusMana -= Attributes.BonusMana;

                    if (Disguise)
                        from.Identities.DisguiseHidden = false;
                }

				mob.CheckStatTimers();
			}

			base.OnRemoved( parent );
		}

		public virtual int OnHit( BaseWeapon weapon, int damageTaken )
		{
			int Absorbed = Utility.RandomMinMax( 1, 4 );

			damageTaken -= Absorbed;

			if ( damageTaken < 0 ) 
				damageTaken = 0;

			if ( 25 > Utility.Random( 100 ) ) // 25% chance to lower durability
			{
				if ( Core.AOS && m_AosClothingAttributes.SelfRepair > Utility.Random( 10 ) )
				{
					Durability += 2;
				}
				else
				{
					int wear;

					if ( weapon.Type == WeaponType.Bashing )
						wear = Absorbed / 2;
					else
						wear = Utility.Random( 2 );

					if ( wear > 0 && m_MaxDurability > 0 )
					{
						if ( m_Durability >= wear )
						{
							Durability -= wear;
							wear = 0;
						}
						else
						{
							wear -= Durability;
							Durability = 0;
						}

						if ( wear > 0 )
						{
							if ( m_MaxDurability > wear )
							{
								MaxDurability -= wear;

								if ( Parent is Mobile )
									((Mobile)Parent).LocalOverheadMessage( MessageType.Regular, 0x3B2, 1061121 ); // Your equipment is severely damaged.
							}
							else
							{
								Delete();
							}
						}
					}
				}
			}

			return damageTaken;
		}

		public BaseClothing( int itemID, Layer layer ) : this( itemID, layer, 0 )
		{
		}

		public BaseClothing( int itemID, Layer layer, int hue ) : base( itemID )
		{
			Layer = layer;
			Hue = hue;

			m_Resource = DefaultResource;
			m_Quality = ClothingQuality.Regular;

			m_Durability = m_MaxDurability = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			m_AosAttributes = new AosAttributes( this );
			m_AosClothingAttributes = new AosArmorAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
		}

		public override void OnAfterDuped( Item newItem )
		{
			BaseClothing clothing = newItem as BaseClothing;

			if ( clothing == null )
				return;

			clothing.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			clothing.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
			clothing.m_AosClothingAttributes = new AosArmorAttributes( newItem, m_AosClothingAttributes );
		}

		public BaseClothing( Serial serial ) : base( serial )
		{
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			if ( base.AllowEquipedCast( from ) )
				return true;

			return ( m_AosAttributes.SpellChanneling != 0 );
		}

		public void UnscaleDurability()
		{
			int scale = 100 + m_AosClothingAttributes.DurabilityBonus;

			m_Durability = ( ( m_Durability * 100 ) + ( scale - 1 ) ) / scale;
			m_MaxDurability = ( ( m_MaxDurability * 100 ) + ( scale - 1 ) ) / scale;

			InvalidateProperties();
		}

		public void ScaleDurability()
		{
			int scale = 100 + m_AosClothingAttributes.DurabilityBonus;

			m_Durability = ( ( m_Durability * scale ) + 99 ) / 100;
			m_MaxDurability = ( ( m_MaxDurability * scale ) + 99 ) / 100;

			InvalidateProperties();
		}

		public override bool CheckPropertyConfliction( Mobile m )
		{
			if ( base.CheckPropertyConfliction( m ) )
				return true;

			if ( Layer == Layer.Pants )
				return ( m.FindItemOnLayer( Layer.InnerLegs ) != null );

			if ( Layer == Layer.Shirt )
				return ( m.FindItemOnLayer( Layer.InnerTorso ) != null );

			return false;
		}

		private string GetNameString()
		{
			string name = this.Name;

			if ( name == null )
				name = String.Format( "#{0}", LabelNumber );

			return name;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			int oreType;

			switch ( m_Resource )
			{
				case CraftResource.Cuivre:  		oreType = 1053108; break; // dull copper
				case CraftResource.Bronze:  		oreType = 1053107; break; // shadow iron
				case CraftResource.Acier:			oreType = 1053106; break; // copper
				case CraftResource.Argent:			oreType = 1053105; break; // bronze
				case CraftResource.Or:  			oreType = 1053104; break; // golden
				case CraftResource.Mytheril:		oreType = 1053103; break; // agapite
				case CraftResource.Luminium:    	oreType = 1053102; break; // verite
				case CraftResource.Obscurium:		oreType = 1053101; break; // valorite
                case CraftResource.Mystirium:       oreType = 1053101; break; // valorite
                case CraftResource.Dominium:        oreType = 1053101; break; // valorite
                case CraftResource.Eclarium:        oreType = 1053101; break; // valorite
                case CraftResource.Venarium:        oreType = 1053101; break; // valorite
                case CraftResource.Athenium:        oreType = 1053101; break; // valorite
                case CraftResource.Umbrarium:       oreType = 1053101; break; // valorite

				/*case CraftResource.SpinedLeather:	oreType = 1061118; break; // spined
				case CraftResource.HornedLeather:	oreType = 1061117; break; // horned
				case CraftResource.BarbedLeather:	oreType = 1061116; break; // barbed
				case CraftResource.RedScales:		oreType = 1060814; break; // red
				case CraftResource.YellowScales:	oreType = 1060818; break; // yellow
				case CraftResource.BlackScales:		oreType = 1060820; break; // black
				case CraftResource.GreenScales:		oreType = 1060819; break; // green
				case CraftResource.WhiteScales:		oreType = 1060821; break; // white
				case CraftResource.BlueScales:		oreType = 1060815; break; // blue*/
				default: oreType = 0; break;
			}

			if ( oreType != 0 )
				list.Add( 1053099, "#{0}t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
			else if ( Name == null )
				list.Add( LabelNumber );
			else
				list.Add( Name );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			//base.GetProperties( list );

            string couleur = "";
            RareteItem rarete = RareteItem.Normal;

            switch (Rarete)
            {
                case RareteItem.Mediocre:
                    rarete = RareteItem.Mediocre;
                    couleur = "333333";
                    break;
                case RareteItem.Normal:
                    rarete = RareteItem.Normal;
                    couleur = "999999";
                    break;
                case RareteItem.Magique:
                    rarete = RareteItem.Magique;
                    couleur = "003366";
                    break;
                case RareteItem.Rare:
                    rarete = RareteItem.Rare;
                    couleur = "993300";
                    break;
                case RareteItem.Legendaire:
                    rarete = RareteItem.Legendaire;
                    couleur = "5A4A31";
                    break;
                default: couleur = "999999"; break;
            }

            if (Identified)
            {
                string[] s = Regex.Split(GetType().ToString(),@".");
                string t = s[s.Length - 1];
                if (Name == null)
                    list.Add(1060393, "{0}\t{1}", couleur, t);
                else
                    list.Add(1060393, "{0}\t{1}", couleur, Name);
                //list.Add(1060394, "{0}\t{1}", couleur, rarete.ToString());
                list.Add(1060394, "{0}\t{1}", couleur, Quality.ToString());

                if (m_CrafterName != null)
                    list.Add(1060394, "{0}\t{1}", couleur, "Fabriqué par: " + m_CrafterName); // Fabriqué par: ~1_NAME~

                if (m_AosSkillBonuses != null)
                    m_AosSkillBonuses.GetProperties(list, couleur);

                int prop;

                //if ((prop = ArtifactRarity) > 0)
                //  list.Add(1061078, prop.ToString()); // artifact rarity ~1_val~

                if ((prop = m_AosAttributes.AttackChance) != 0)
                    list.Add(1060401, "{0}\t{1}", couleur, prop.ToString()); // hit chance increase ~1_val~%

                if ((prop = m_AosAttributes.WeaponDamage) != 0)
                    list.Add(1060402, "{0}\t{1}", couleur, prop.ToString()); // damage increase ~1_val~%

                if ((prop = m_AosAttributes.DefendChance) != 0)
                    list.Add(1060408, "{0}\t{1}", couleur, prop.ToString()); // defense chance increase ~1_val~%

                if ((prop = m_AosAttributes.WeaponSpeed) != 0)
                    list.Add(1060486, "{0}\t{1}", couleur, prop.ToString()); // swing speed increase ~1_val~%

                if ((prop = m_AosAttributes.ReflectPhysical) != 0)
                    list.Add(1060442, "{0}\t{1}", couleur, prop.ToString()); // reflect physical damage ~1_val~%

                if ((prop = m_AosAttributes.BonusStr) != 0)
                    list.Add(1060485, "{0}\t{1}", couleur, prop.ToString()); // strength bonus ~1_val~

                if ((prop = m_AosAttributes.BonusDex) != 0)
                    list.Add(1060409, "{0}\t{1}", couleur, prop.ToString()); // dexterity bonus ~1_val~

                if ((prop = m_AosAttributes.BonusInt) != 0)
                    list.Add(1060432, "{0}\t{1}", couleur, prop.ToString()); // intelligence bonus ~1_val~

                if ((prop = m_AosAttributes.BonusHits) != 0)
                    list.Add(1060431, "{0}\t{1}", couleur, prop.ToString()); // hit point increase ~1_val~

                if ((prop = m_AosAttributes.BonusStam) != 0)
                    list.Add(1060484, "{0}\t{1}", couleur, prop.ToString()); // stamina increase ~1_val~

                if ((prop = m_AosAttributes.BonusMana) != 0)
                    list.Add(1060439, "{0}\t{1}", couleur, prop.ToString()); // mana increase ~1_val~

                if ((prop = m_AosAttributes.RegenHits) != 0)
                    list.Add(1060444, "{0}\t{1}", couleur, prop.ToString()); // hit point regeneration ~1_val~

                if ((prop = m_AosAttributes.RegenStam) != 0)
                    list.Add(1060443, "{0}\t{1}", couleur, prop.ToString()); // stamina regeneration ~1_val~

                if ((prop = m_AosAttributes.RegenMana) != 0)
                    list.Add(1060440, "{0}\t{1}", couleur, prop.ToString()); // mana regeneration ~1_val~

                if ((prop = m_AosAttributes.EnhancePotions) != 0)
                    list.Add(1060411, "{0}\t{1}", couleur, prop.ToString()); // enhance potions ~1_val~%

                if ((prop = m_AosAttributes.CastRecovery) != 0)
                    list.Add(1060412, "{0}\t{1}", couleur, prop.ToString()); // faster cast recovery ~1_val~

                if ((prop = m_AosAttributes.CastSpeed) != 0)
                    list.Add(1060413, "{0}\t{1}", couleur, prop.ToString()); // faster casting ~1_val~

                if ((prop = m_AosAttributes.LowerManaCost) != 0)
                    list.Add(1060433, "{0}\t{1}", couleur, prop.ToString()); // lower mana cost ~1_val~%

                if ((prop = m_AosAttributes.LowerRegCost) != 0)
                    list.Add(1060434, "{0}\t{1}", couleur, prop.ToString()); // lower reagent cost ~1_val~%

                //if ((prop = m_AosAttributes.Luck) != 0)
                //    list.Add("<h3><basefont color=#" + couleur + ">Chance:" + prop.ToString() + "<basefont></h3>"); // luck ~1_val~

                //if ((prop = m_AosAttributes.SpellChanneling) != 0)
                //    list.Add(1060482); // spell channeling

                if ((prop = m_AosAttributes.SpellDamage) != 0)
                    list.Add(1060483, "{0}\t{1}", couleur, prop.ToString()); // spell damage increase ~1_val~%

                if ((prop = m_AosAttributes.NightSight) != 0)
                    list.Add(1060434, couleur); // night sight

                if ((prop = m_AosClothingAttributes.MageArmor) != 0)
                    list.Add(1060437, couleur); // mage armor

                AddARProperties(list, couleur);

                if ((prop = m_AosClothingAttributes.LowerStatReq) != 0)
                    list.Add(1060435, "{0}\t{1}", couleur, prop.ToString()); // lower requirements ~1_val~%

                if ((prop = ComputeStatReq(StatType.Str)) > 0)
                    list.Add(1061170, "{0}\t{1}", couleur, prop.ToString()); // strength requirement ~1_val~

                if ((prop = m_AosClothingAttributes.SelfRepair) != 0)
                    list.Add(1060450, "{0}\t{1}", couleur, prop.ToString()); // self repair ~1_val~

                if ((prop = m_AosClothingAttributes.DurabilityBonus) > 0)
                    list.Add(1060410, "{0}\t{1}", couleur, prop.ToString()); // durability ~1_val~%

                if (m_Durability >= 0 && m_MaxDurability > 0)
                    list.Add(1060639, "{0}t{1}t{2}", couleur, m_Durability, m_MaxDurability); // durability ~1_val~ / ~2_val~
            }
            else
            {
                string[] s = Regex.Split(GetType().ToString(),@".");
                string t = s[s.Length - 1];
                if (Name == null)
                    list.Add(1060393, "{0}\t{1}", couleur, t);
                else
                    list.Add(1060393, "{0}\t{1}", couleur, Name);
                //list.Add(1060394, "{0}\t{1}", couleur, rarete.ToString());
                list.Add(1060395, couleur);
            }
		}

        public void AddARProperties(ObjectPropertyList list, string couleur)
        {
            double v = PhysicalResistance;

            if (v != 0)
                list.Add(1060448, "{0}\t{1}", couleur, v.ToString()); // physical resist ~1_val~%

            v = MagieResistance;

            if (v != 0)
                list.Add(1060446, "{0}\t{1}", couleur, v.ToString()); // energy resist ~1_val~%
        }

		public override void OnSingleClick( Mobile from )
		{
			List<EquipInfoAttribute> attrs = new List<EquipInfoAttribute>();

			AddEquipInfoAttributes( from, attrs );

			int number;

			if ( Name == null )
			{
				number = LabelNumber;
			}
			else
			{
				this.LabelTo( from, Name );
				number = 1041000;
			}

			if ( attrs.Count == 0 && Crafter == null && Name != null )
				return;

			EquipmentInfo eqInfo = new EquipmentInfo( number, m_Crafter, false, attrs.ToArray() );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		public virtual void AddEquipInfoAttributes( Mobile from, List<EquipInfoAttribute> attrs )
		{
			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			#region Factions
			if ( FactionItemState != null )
				attrs.Add( new EquipInfoAttribute( 1041350 ) ); // faction item
			#endregion

			if ( m_Quality == ClothingQuality.Exceptional )
				attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );
		}

		#region Serialization
		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Resource			= 0x00000001,
			Attributes			= 0x00000002,
			ClothingAttributes	= 0x00000004,
			SkillBonuses		= 0x00000008,
			Resistances			= 0x00000010,
			MaxDurability		= 0x00000020,
			Durability			= 0x00000040,
			PlayerConstructed	= 0x00000080,
			Crafter				= 0x00000100,
			Quality				= 0x00000200,
			StrReq				= 0x00000400,
            CrafterName         = 0x00000800

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            writer.Write((int)m_BaseLayer);

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.Resource,			m_Resource != DefaultResource );
			SetSaveFlag( ref flags, SaveFlag.Attributes,		!m_AosAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.ClothingAttributes,!m_AosClothingAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.SkillBonuses,		!m_AosSkillBonuses.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.MaxDurability,		m_MaxDurability != 0 );
			SetSaveFlag( ref flags, SaveFlag.Durability,			m_Durability != 0 );
			SetSaveFlag( ref flags, SaveFlag.PlayerConstructed,	m_PlayerConstructed != false );
			SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
            SetSaveFlag( ref flags, SaveFlag.CrafterName,       m_CrafterName != null);
			SetSaveFlag( ref flags, SaveFlag.Quality,			m_Quality != ClothingQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.StrReq,			m_StrReq != -1 );
            

			writer.WriteEncodedInt( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.Resource ) )
				writer.WriteEncodedInt( (int) m_Resource );

			if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
				m_AosAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.ClothingAttributes ) )
				m_AosClothingAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
				m_AosSkillBonuses.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.MaxDurability ) )
				writer.WriteEncodedInt( (int) m_MaxDurability );

			if ( GetSaveFlag( flags, SaveFlag.Durability ) )
				writer.WriteEncodedInt( (int) m_Durability );

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				writer.Write( (Mobile) m_Crafter );

            if (GetSaveFlag(flags, SaveFlag.CrafterName))
                writer.Write((string)m_CrafterName);

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				writer.WriteEncodedInt( (int) m_Quality );

			if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
				writer.WriteEncodedInt( (int) m_StrReq );
		}

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_BaseLayer = (Layer)reader.ReadInt();
            
            SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();

            if (GetSaveFlag(flags, SaveFlag.Resource))
                m_Resource = (CraftResource)reader.ReadEncodedInt();
            else
                m_Resource = DefaultResource;

            if (GetSaveFlag(flags, SaveFlag.Attributes))
            {
                m_AosAttributes = new AosAttributes(this, reader);
                m_AosAttributes = new AosAttributes(this);
            }
            else
                m_AosAttributes = new AosAttributes(this);

            if (GetSaveFlag(flags, SaveFlag.ClothingAttributes))
            {
                m_AosClothingAttributes = new AosArmorAttributes(this, reader);
                m_AosClothingAttributes = new AosArmorAttributes(this);
            }
            else
                m_AosClothingAttributes = new AosArmorAttributes(this);

            if (GetSaveFlag(flags, SaveFlag.SkillBonuses))
                m_AosSkillBonuses = new AosSkillBonuses(this, reader);
            else
                m_AosSkillBonuses = new AosSkillBonuses(this);

            if (GetSaveFlag(flags, SaveFlag.MaxDurability))
                m_MaxDurability = reader.ReadEncodedInt();

            if (GetSaveFlag(flags, SaveFlag.Durability))
                m_Durability = reader.ReadEncodedInt();

            if (GetSaveFlag(flags, SaveFlag.Crafter))
                m_Crafter = reader.ReadMobile();

            if (GetSaveFlag(flags, SaveFlag.CrafterName))
                m_CrafterName = reader.ReadString();

            if (GetSaveFlag(flags, SaveFlag.Quality))
                m_Quality = (ClothingQuality)reader.ReadEncodedInt();
            else
                m_Quality = ClothingQuality.Regular;

            if (GetSaveFlag(flags, SaveFlag.StrReq))
                m_StrReq = reader.ReadEncodedInt();
            else
                m_StrReq = -1;

            if (GetSaveFlag(flags, SaveFlag.PlayerConstructed))
                m_PlayerConstructed = true;

            if (m_MaxDurability == 0 && m_Durability == 0)
                m_Durability = m_MaxDurability = Utility.RandomMinMax(InitMinHits, InitMaxHits);

            Mobile parent = Parent as Mobile;

            if (parent != null)
            {
                if (Core.AOS)
                    m_AosSkillBonuses.AddTo(parent);

                parent.CheckStatTimers();
            }
        }
		#endregion

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = sender.Hue;

			return true;
		}

		public virtual bool Scissor( Mobile from, Scissors scissors )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
				return false;
			}

			CraftSystem system = DefTailoring.CraftSystem;

			CraftItem item = system.CraftItems.SearchFor( GetType() );

			if ( item != null && item.Resources.Count == 1 && item.Resources.GetAt( 0 ).Amount >= 2 )
			{
				try
				{
					Type resourceType = null;

					CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

					if ( info != null && info.ResourceTypes.Length > 0 )
						resourceType = info.ResourceTypes[0];

					if ( resourceType == null )
						resourceType = item.Resources.GetAt( 0 ).ItemType;

					Item res = (Item)Activator.CreateInstance( resourceType );

					ScissorHelper( from, res, m_PlayerConstructed ? (item.Resources.GetAt( 0 ).Amount / 2) : 1 );

					res.LootType = LootType.Regular;

					return true;
				}
				catch
				{

				}
			}

			from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
			return false;
		}

		#region ICraftable Members

        public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality)quality;

            if (makersMark)
            {
                Crafter = from;
                m_CrafterName = from.Name;
            }

			if ( DefaultResource != CraftResource.None )
			{
				Type resourceType = typeRes;

				if ( resourceType == null )
					resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

				Resource = CraftResources.GetFromType( resourceType );
			}
			else
			{
				Hue = resHue;
			}

			PlayerConstructed = true;

			CraftContext context = craftSystem.GetContext( from );

            RareteInit.InitItem(this, quality, Crafter);

			if ( context != null && context.DoNotColor )
				Hue = 0;

			return quality;
		}

		#endregion
	}
}