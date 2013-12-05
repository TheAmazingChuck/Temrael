using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells
{
	public class MurDeFeuSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mur De Feu", "In Flam Grav",
				SpellCircle.Fifth,
				215,
				9041,
				false,
				Reagent.BlackPearl,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
            );

        public override int RequiredAptitudeValue { get { return 4; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] {NAptitude.Adjuration }; } }

        public MurDeFeuSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
            }
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				int dx = Caster.Location.X - p.X;
				int dy = Caster.Location.Y - p.Y;
				int rx = (dx - dy) * 44;
				int ry = (dx + dy) * 44;

				bool eastToWest;

				if ( rx >= 0 && ry >= 0 )
				{
					eastToWest = false;
				}
				else if ( rx >= 0 )
				{
					eastToWest = true;
				}
				else if ( ry >= 0 )
				{
					eastToWest = true;
				}
				else
				{
					eastToWest = false;
				}

				Effects.PlaySound( p, Caster.Map, 0x20C );

				int itemID = eastToWest ? 0x398C : 0x3996;

                TimeSpan duration = GetDurationForSpell(0.7);

				for ( int i = -3; i <= 3; ++i )
				{
					Point3D loc = new Point3D( eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z );

					new InternalItem( this, itemID, loc, Caster, Caster.Map, duration, i );
				}
			}

			FinishSequence();
		}

		[DispellableField]
		public class InternalItem : Item
        {
            private Spell m_Spell;
			private Timer m_Timer;
			private DateTime m_End;
			private Mobile m_Caster;

			public override bool BlocksFit{ get{ return true; } }

            public InternalItem(Spell spell, int itemID, Point3D loc, Mobile caster, Map map, TimeSpan duration, int val)
                : base(itemID)
            {
                bool canFit = SpellHelper.AdjustField(ref loc, map, 12, false);

                Visible = false;
                Movable = false;
                Light = LightType.Circle300;

                MoveToWorld(loc, map);

                if (caster.InLOS(this))
                    Visible = true;
                else
                    Delete();

                if (!this.Deleted && VerifyOtherFields(caster))
                    Delete();

                if (Deleted)
                    return;

                m_Spell = spell;
                m_Caster = caster;

                m_End = DateTime.Now + duration;

                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
                m_Timer.Start();
            }

            public bool VerifyOtherFields(Mobile caster)
            {
                Map map = this.Map;
                bool test = false;

                IPooledEnumerable eable = map.GetItemsInRange(this.Location, 0);

                if (this.Deleted)
                    return false;

                foreach (Item item in eable)
                {
                    if (item != null && this == item)
                        continue;

                    if (item != null && (item is MurDeFeuSpell.InternalItem || item is GeyserSpell.InternalItem || item is MurDeHaieSpell.InternalItem || item is MurDeParalysieSpell.InternalItem || item is MurDePierreSpell.InternalItem || item is MurDEnergieSpell.InternalItem))
                    {
                        caster.SendMessage("Vous ne pouvez pas lancer un mur de feu au m�me endroit qu'un autre mur.");
                        test = true;
                    }
                }

                return test;
            }

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 1 ); // version

				writer.Write( m_Caster );
				writer.WriteDeltaTime( m_End );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch ( version )
				{
					case 1:
					{
						m_Caster = reader.ReadMobile();

						goto case 0;
					}
					case 0:
					{
						m_End = reader.ReadDeltaTime();

						m_Timer = new InternalTimer( this, TimeSpan.Zero, true, true );
						m_Timer.Start();

						break;
					}
				}
			}

            public override bool OnMoveOver(Mobile m)
            {
                if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, m) && m_Caster.CanBeHarmful(m, false))
                {
                    m_Caster.DoHarmful(m);

                    int damage = Utility.Random(30, 40);

                    damage = (int)SpellHelper.AdjustValue(m_Caster, damage, NAptitude.Sorcellerie);

                    AOS.Damage(m, m_Caster, damage, 0, 100, 0, 0, 0);
                    m.PlaySound(0x208);
                    this.Delete();
                }

                return true;
            }

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;
				private bool m_InLOS, m_CanFit;

                private static Queue m_Queue = new Queue();

				public InternalTimer( InternalItem item, TimeSpan delay, bool inLOS, bool canFit ) : base( delay, TimeSpan.FromSeconds( 1.0 ) )
				{
					m_Item = item;
					m_InLOS = inLOS;
					m_CanFit = canFit;

                    Priority = TimerPriority.TwoFiftyMS;
				}

				protected override void OnTick()
				{
					if ( m_Item.Deleted )
						return;

                    if (m_Item == null)
                        return;

					if ( !m_Item.Visible )
					{
						if ( m_InLOS && m_CanFit )
							m_Item.Visible = true;
						else
							m_Item.Delete();

						if ( !m_Item.Deleted )
						{
							m_Item.ProcessDelta();
							Effects.SendLocationParticles( EffectItem.Create( m_Item.Location, m_Item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 5029 );
						}
					}
                    else if (DateTime.Now > m_Item.m_End)
                    {
                        m_Item.Delete();
                        Stop();
                    }
                    else
                    {
                        Map map = m_Item.Map;
                        Mobile caster = m_Item.m_Caster;

                        if (map != null && caster != null)
                        {
                            foreach (Mobile m in m_Item.GetMobilesInRange(0))
                            {
                                if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
                                    m_Queue.Enqueue(m);
                            }

                            bool todelete = false;

                            while (m_Queue.Count > 0)
                            {
                                Mobile m = (Mobile)m_Queue.Dequeue();

                                caster.DoHarmful(m);

                                double damage = Utility.RandomMinMax(30, 40);

                                damage = (int)SpellHelper.AdjustValue(caster, damage, NAptitude.Sorcellerie);

                                AOS.Damage(m, caster, (int)damage, 0, 100, 0, 0, 0);
                                m.PlaySound(0x208);

                                todelete = true;
                            }

                            if (todelete)
                            {
                                m_Item.Delete();
                                Stop();
                            }
                        }
                    }
				}
			}
		}

		private class InternalTarget : Target
		{
            private MurDeFeuSpell m_Owner;

            public InternalTarget(MurDeFeuSpell owner)
                : base(12, true, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}