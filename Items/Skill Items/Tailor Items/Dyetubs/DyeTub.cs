using System;
using System.Collections.Generic;
using Server;
using Server.Multis;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public interface IDyable
	{
		bool Dye( Mobile from, DyeTub sender );
	}

	public class DyeTub : Item, ISecurable
	{
        private static int[] m_WaterTiles = new int[]
			{
				168, 169, 170, 171
			};

        private static int[] m_StaticTiles = new int[]
            {
                2881, 2882, 2883, 2884, 3707, 4088, 4089, 5453,
                6038, 6039, 6040, 6041, 6042, 6043, 6044, 6044,
                6046, 6047, 6048, 6049, 6050, 6051, 6052, 6053,
                6054, 6055, 6056, 6057, 6058, 6059, 6060, 6061,
                6062, 6063, 6064, 6065, 6066, 8093, 8094, 8196,
                4090
            };

        private int m_Charges;

		private bool m_Redyable;
		//private int m_DyedHue;
		private SecureLevel m_SecureLevel;

		public virtual CustomHuePicker CustomHuePicker{ get{ return null; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; }
        }

		public virtual bool AllowRunebooks
		{
			get{ return false; }
		}

		public virtual bool AllowFurniture
		{
			get{ return false; }
		}

		public virtual bool AllowStatuettes
		{
			get{ return false; }
		}

		public virtual bool AllowLeather
		{
			get{ return false; }
		}

		public virtual bool AllowDyables
		{
			get{ return true; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

            writer.Write( (int) m_Charges);
			writer.Write( (int)m_SecureLevel );
			writer.Write( (bool) m_Redyable );
			//writer.Write( (int) m_DyedHue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
                case 2:
                {
                    m_Charges = reader.ReadInt();
                    goto case 1;
                }
				case 1:
				{
					m_SecureLevel = (SecureLevel)reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					m_Redyable = reader.ReadBool();
					//m_DyedHue = reader.ReadInt();

					break;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Redyable
		{
			get
			{
				return m_Redyable;
			}
			set
			{
				m_Redyable = value;
			}
		}

		/*[CommandProperty( AccessLevel.GameMaster )]
		public int DyedHue
		{
			get
			{
				return m_DyedHue;
			}
			set
			{
				if ( m_Redyable )
				{
					m_DyedHue = value;
					Hue = value;
				}
			}
		}*/
		
		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get
			{
				return m_SecureLevel;
			}
			set
			{
				m_SecureLevel = value;
			}
		}

		[Constructable] 
		public DyeTub() : base( 0xFAB )
		{
			Weight = 10.0;
			m_Redyable = true;
            m_Charges = 5;
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public DyeTub( Serial serial ) : base( serial )
		{
		}

		// Select the clothing to dye.
		public virtual int TargetMessage{ get{ return 500859; } }

		// You can not dye that.
		public virtual int FailMessage{ get{ return 1042083; } }

		public override void OnDoubleClick( Mobile from )
		{
            if (m_Charges > 0)
            {
                if (from.InRange(this.GetWorldLocation(), 1))
                {
                    from.SendLocalizedMessage(TargetMessage);
                    from.Target = new InternalTarget(this);
                }
                else
                {
                    from.SendLocalizedMessage(500446); // That is too far away.
                }
            }
            else
            {
                from.SendMessage("Choisissez un contenant d'eau pour remplir le bac.");
                from.Target = new InternalEmptyTarget(this);
            }
		}

        private class InternalEmptyTarget : Target
        {
            private DyeTub m_Tub;

            public InternalEmptyTarget(DyeTub tub)
                : base(1, true, TargetFlags.None)
            {
                m_Tub = tub;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if( targeted is LandTarget )
			    {
				    int tileID = ( (LandTarget)targeted ).TileID;
                    int i = 0;

				    PlayerMobile player = from as PlayerMobile;

				    if( player != null )
				    {
                        bool contains = false;

                        try
                        {
                            for (i = 0; !contains && i < m_WaterTiles.Length; i++)
                                if (tileID == m_WaterTiles[i])
                                    contains = true;

                            if (contains)
                            {
                                m_Tub.ItemID = 0xFAB;
                                m_Tub.Charges = 5;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("******BUG DE DYES/PITCHER******");
                            Console.WriteLine(m_WaterTiles[i + 1]);
                            Console.WriteLine(m_WaterTiles[i]);
                            Console.WriteLine(tileID);
                            Console.WriteLine(i);
                        }
				    }
			    }
                else if (targeted is StaticTarget)
                {
                    bool contains = false;
                    int i = 0;

                    StaticTarget target = (StaticTarget)targeted;

                    try
                    {
                        for (i = 0; !contains && i < m_StaticTiles.Length; i++)
                        {
                            //contains = (target.ItemID >= m_StaticTiles[i] && target.ItemID <= m_StaticTiles[i + 1]);
                            if (target.ItemID == m_StaticTiles[i])
                                contains = true;
                        }

                        if (contains)
                        {
                            m_Tub.ItemID = 0xFAB;
                            m_Tub.Charges = 5;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("******BUG DE DYES/PITCHER******");
                        Console.WriteLine(m_StaticTiles[i]);
                        Console.WriteLine(target.ItemID);
                        Console.WriteLine(i);
                    }
                }
                else if (targeted is BaseBeverage)
                {
                    if (((BaseBeverage)targeted).Quantity > 0)
                    {
                        m_Tub.ItemID = 0xFAB;
                        m_Tub.Charges = 5;
                    }
                }
            }
        }

		private class InternalTarget : Target
		{
			private DyeTub m_Tub;

			public InternalTarget( DyeTub tub ) : base( 1, false, TargetFlags.None )
			{
				m_Tub = tub;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item item = (Item)targeted;

					if ( item is IDyable && m_Tub.AllowDyables )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
							from.SendLocalizedMessage( 500446 ); // That is too far away.
						else if ( item.Parent is Mobile )
							from.SendLocalizedMessage( 500861 ); // Can't Dye clothing that is being worn.
                        else if (((IDyable)item).Dye(from, m_Tub))
                        {
                            from.PlaySound(0x23E);
                            if (m_Tub.Charges > 1)
                            {
                                m_Tub.Charges -= 1;
                            }
                            else
                            {
                                m_Tub.Hue = 0;
                                m_Tub.Charges = 0;
                                m_Tub.ItemID = 0xe83;
                            }
                        }
					}
					else if ( (FurnitureAttribute.Check( item ) || (item is PotionKeg)) && m_Tub.AllowFurniture )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendLocalizedMessage( 500446 ); // That is too far away.
						}
						else
						{
							bool okay = ( item.IsChildOf( from.Backpack ) );

							if ( !okay )
							{
								if ( item.Parent == null )
								{
									BaseHouse house = BaseHouse.FindHouseAt( item );

									if ( house == null || ( !house.IsLockedDown( item ) && !house.IsSecure( item ) ) )
										from.SendLocalizedMessage( 501022 ); // Furniture must be locked down to paint it.
									else if ( !house.IsCoOwner( from ) )
										from.SendLocalizedMessage( 501023 ); // You must be the owner to use this item.
									else
										okay = true;
								}
								else
								{
									from.SendLocalizedMessage( 1048135 ); // The furniture must be in your backpack to be painted.
								}
							}

							if ( okay )
							{
								item.Hue = m_Tub.Hue;
								from.PlaySound( 0x23E );
                                if (m_Tub.Charges > 1)
                                {
                                    m_Tub.Charges -= 1;
                                }
                                else
                                {
                                    m_Tub.Hue = 0;
                                    m_Tub.Charges = 0;
                                    m_Tub.ItemID = 0xe83;
                                }
							}
						}
					}
					else if ( (item is Runebook || item is RecallRune ) && m_Tub.AllowRunebooks )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendLocalizedMessage( 500446 ); // That is too far away.
						}
						else if ( !item.Movable )
						{
							from.SendLocalizedMessage( 1049776 ); // You cannot dye runes or runebooks that are locked down.
						}
						else
						{
							item.Hue = m_Tub.Hue;
							from.PlaySound( 0x23E );
                            if (m_Tub.Charges > 1)
                            {
                                m_Tub.Charges -= 1;
                            }
                            else
                            {
                                m_Tub.Hue = 0;
                                m_Tub.Charges = 0;
                                m_Tub.ItemID = 0xe83;
                            }
						}
					}
					else if ( item is MonsterStatuette && m_Tub.AllowStatuettes )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendLocalizedMessage( 500446 ); // That is too far away.
						}
						else if ( !item.Movable )
						{
							from.SendLocalizedMessage( 1049779 ); // You cannot dye statuettes that are locked down.
						}
						else
						{
							item.Hue = m_Tub.Hue;
							from.PlaySound( 0x23E );
                            if (m_Tub.Charges > 1)
                            {
                                m_Tub.Charges -= 1;
                            }
                            else
                            {
                                m_Tub.Hue = 0;
                                m_Tub.Charges = 0;
                                m_Tub.ItemID = 0xe83;
                            }
						}
					}
					else if ( (item is BaseArmor && (((BaseArmor)item).MaterialType == ArmorMaterialType.Leather || ((BaseArmor)item).MaterialType == ArmorMaterialType.Studded) || item is ElvenBoots || item is WoodlandBelt) && m_Tub.AllowLeather )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendLocalizedMessage( 500446 ); // That is too far away.
						}
						else if ( !item.Movable )
						{
							from.SendLocalizedMessage( 1042419 ); // You may not dye leather items which are locked down.
						}
						else if ( item.Parent is Mobile )
						{
							from.SendLocalizedMessage( 500861 ); // Can't Dye clothing that is being worn.
						}
						else
						{
							item.Hue = m_Tub.Hue;
							from.PlaySound( 0x23E );
                            if (m_Tub.Charges > 1)
                            {
                                m_Tub.Charges -= 1;
                            }
                            else
                            {
                                m_Tub.Hue = 0;
                                m_Tub.Charges = 0;
                                m_Tub.ItemID = 0xe83;
                            }
						}
					}
					else
					{
						from.SendLocalizedMessage( m_Tub.FailMessage );
					}
				}
				else
				{
					from.SendLocalizedMessage( m_Tub.FailMessage );
				}
			}
		}
	}
}