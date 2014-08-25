﻿using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class DagueEntrainement : BaseSword
    {
        //public override int NiveauAttirail { get { return 0; } }

        public override int AosStrengthReq { get { return Dague_Force0; } }
        public override int AosMinDamage { get { return Dague_MinDam0; } }
        public override int AosMaxDamage { get { return Dague_MaxDam0; } }
        public override double AosSpeed { get { return Dague_Vitesse; } }
        public override float MlSpeed { get { return 3.75f; } }

        public override int OldStrengthReq { get { return 40; } }
        public override int OldMinDamage { get { return 6; } }
        public override int OldMaxDamage { get { return 34; } }
        public override int OldSpeed { get { return 30; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 100; } }

        [Constructable]
        public DagueEntrainement()
            : base(0x1494)
        {
            Weight = 2.0;
            Name = "Dague d'entraînement";
        }

        public DagueEntrainement(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    public class LanceEntrainement : BaseSpear
    {
        //public override int NiveauAttirail { get { return 0; } }

        public override int AosStrengthReq { get { return Lance_Force0; } }
        public override int AosMinDamage { get { return Lance_MinDam0; } }
        public override int AosMaxDamage { get { return Lance_MaxDam0; } }
        public override double AosSpeed { get { return Lance_Vitesse; } }
        public override float MlSpeed { get { return 3.75f; } }

        public override int OldStrengthReq { get { return 40; } }
        public override int OldMinDamage { get { return 6; } }
        public override int OldMaxDamage { get { return 34; } }
        public override int OldSpeed { get { return 30; } }

        public override int DefHitSound { get { return 0x237; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 100; } }

        [Constructable]
        public LanceEntrainement()
            : base(0x1495)
        {
            Weight = 2.0;
            Name = "Lance d'entraînement";
        }

        public LanceEntrainement(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    public class MasseEntrainement : BaseBashing
    {
        //public override int NiveauAttirail { get { return 0; } }

        public override int AosStrengthReq { get { return Masse_Force0; } }
        public override int AosMinDamage { get { return Masse_MinDam0; } }
        public override int AosMaxDamage { get { return Masse_MaxDam0; } }
        public override double AosSpeed { get { return Masse_Vitesse; } }
        public override float MlSpeed { get { return 3.75f; } }

        public override int OldStrengthReq { get { return 40; } }
        public override int OldMinDamage { get { return 6; } }
        public override int OldMaxDamage { get { return 34; } }
        public override int OldSpeed { get { return 30; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 100; } }

        [Constructable]
        public MasseEntrainement()
            : base(0x1496)
        {
            Weight = 2.0;
            Name = "Masse d'entraînement";
        }

        public MasseEntrainement(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    public class BatonEntrainement : BaseStaff
    {
        //public override int NiveauAttirail { get { return 0; } }

        public override int AosStrengthReq { get { return Baton_Force0; } }
        public override int AosMinDamage { get { return Baton_MinDam0; } }
        public override int AosMaxDamage { get { return Baton_MaxDam0; } }
        public override double AosSpeed { get { return Baton_Vitesse; } }
        public override float MlSpeed { get { return 3.75f; } }

        public override int OldStrengthReq { get { return 40; } }
        public override int OldMinDamage { get { return 6; } }
        public override int OldMaxDamage { get { return 34; } }
        public override int OldSpeed { get { return 30; } }

        public override int DefHitSound { get { return 0x237; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 100; } }

        [Constructable]
        public BatonEntrainement()
            : base(0x1497)
        {
            Weight = 6.0;
            Name = "Bâton d'entraînement";
        }

        public BatonEntrainement(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    public class EpeeEntrainement : BaseSword
    {
        //public override int NiveauAttirail { get { return 0; } }

        public override int AosStrengthReq { get { return Lame_Force0; } }
        public override int AosMinDamage { get { return Lame_MinDam0; } }
        public override int AosMaxDamage { get { return Lame_MaxDam0; } }
        public override double AosSpeed { get { return Lame_Vitesse; } }
        public override float MlSpeed { get { return 3.75f; } }

        public override int OldStrengthReq { get { return 40; } }
        public override int OldMinDamage { get { return 6; } }
        public override int OldMaxDamage { get { return 34; } }
        public override int OldSpeed { get { return 30; } }

        public override int DefHitSound { get { return 0x237; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 100; } }

        [Constructable]
        public EpeeEntrainement()
            : base(0x1498)
        {
            Weight = 6.0;
            Name = "Épée d'entrainement";
        }

        public EpeeEntrainement(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
