﻿using Server.Engines.Hiding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Spells;
using Server.Items;

namespace Server.Mobiles
{
    public class ScriptMobile : Mobile
    {
        [CommandProperty(AccessLevel.Batisseur)]
        public Detection Detection
        {
            get;
            set;
        }

        public ScriptMobile()
        {
            Detection = new Detection(this);
        }

        public ScriptMobile(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); //version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            Detection = new Detection(this);
        }

        protected override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);

            ActiverTestsDetection();
        }

        protected override bool OnMove(Direction d)
        {
            ActiverTestsDetection();

            return base.OnMove(d);
        }

        public override bool CanSee(Mobile m)
        {
            try
            {
                ScriptMobile sm = m as ScriptMobile;
                if (sm.Detection[this] == DetectionStatus.Visible)
                    return true;
            }
            catch { }

            return base.CanSee(m);
        }

        public void ActiverTestsDetection()
        {
            //Le systeme de detection fonctionne juste pour les joueurs.
            if (AccessLevel > AccessLevel.Player) return;
            Detection.DetecterAlentours();
            if (Hidden)
                Detection.TesterPresenceAlentours();
        }

        public override void OnHiddenChanged()
        {
            base.OnHiddenChanged();
            Detection.ResetAlentours();
        }

        public override void Damage(int amount)
        {
            Damage(amount, null);
        }

        public override void Damage(int amount, Mobile from)
        {
            OnDamageDurabilityLoss(from);

            double damage = amount;

            SacrificeSpell.GetOnHitEffect(this, ref damage);

            DernierSouffleSpell.GetOnHitEffect(this, ref damage);

            AdrenalineSpell.GetOnHitEffect(this, ref damage);

            Stam -= (int)(amount * 0.60);

            if (BandageContext.m_Table.Contains(this))
                BandageContext.GetContext(this).Slip();

            base.Damage((int)damage, from);
        }

        private const double ChancePerteDura = 0.5;

        public void OnAttackDurabilityLoss()
        {
            if (Utility.RandomDouble() < ((1 / 6) * ChancePerteDura) && Utility.RandomDouble() < ((double)((BaseWeapon)this.Weapon).Speed / (double)BaseWeapon.MaxWeaponSpeed))
            {
                ((BaseWeapon)this.Weapon).Durability -= 1;
            }
        }

        public void OnDamageDurabilityLoss(Mobile atk)
        {
            if (Utility.RandomDouble() < ((double)((BaseWeapon)this.Weapon).Speed / (double)BaseWeapon.MaxWeaponSpeed * ChancePerteDura))
            {
                switch (Utility.Random(6))
                {
                    case 0: if (HeadArmor != null) (HeadArmor as BaseArmor).Durability -= 1; break;
                    case 1: if (NeckArmor != null) (NeckArmor as BaseArmor).Durability -= 1; break;
                    case 2: if (ChestArmor != null) (ChestArmor as BaseArmor).Durability -= 1; break;
                    case 3: if (ArmsArmor != null) (ArmsArmor as BaseArmor).Durability -= 1; break;
                    case 4: if (HandArmor != null) (HandArmor as BaseArmor).Durability -= 1; break;
                    case 5: if (LegsArmor != null) (LegsArmor as BaseArmor).Durability -= 1; break;
                    case 6: if (ShieldArmor != null) (ShieldArmor as BaseArmor).Durability -= 1; break;
                }
            }
        }
    }
}
