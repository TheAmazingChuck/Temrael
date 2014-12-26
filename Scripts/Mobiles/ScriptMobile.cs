﻿using Server.Engines.Hiding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Spells;

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
            double damage = amount;

            SacrificeSpell.GetOnHitEffect(this, ref damage);

            DernierSouffleSpell.GetOnHitEffect(this, ref damage);

            AdrenalineSpell.GetOnHitEffect(this, ref damage);

            Stam -= (int)(amount * 0.60);

            base.Damage((int)damage, from);
        }

    }
}
