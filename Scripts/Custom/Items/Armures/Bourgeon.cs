﻿using System;
using Server.Items;

namespace Server.Items
{
    public class BourgeonGreaves : BaseArmor
    {
        //public override int NiveauAttirail { get { return Bourgeon_Niveau; } }

        public override int BasePhysicalResistance { get { return ArmorBourgeon.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ArmorBourgeon.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ArmorBourgeon.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ArmorBourgeon.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ArmorBourgeon.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorBourgeon.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorBourgeon.max_Durabilite; } }

        public override int AosStrReq { get { return ArmorBourgeon.force_Requise; } }
        public override int AosDexBonus { get { return ArmorBourgeon.malus_Dex; } }

        public override int ArmorBase { get { return 30; } }
        public override int RevertArmorBase { get { return 4; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public BourgeonGreaves()
            : base(0x2858)
        {
            Weight = 2.0;
            Name = "Brassards Bourgeon";
        }

        public BourgeonGreaves(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
    public class BourgeonLeggings : BaseArmor
    {
        //public override int NiveauAttirail { get { return Bourgeon_Niveau; } }

        public override int BasePhysicalResistance { get { return ArmorBourgeon.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ArmorBourgeon.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ArmorBourgeon.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ArmorBourgeon.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ArmorBourgeon.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorBourgeon.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorBourgeon.max_Durabilite; } }

        public override int AosStrReq { get { return ArmorBourgeon.force_Requise; } }
        public override int AosDexBonus { get { return ArmorBourgeon.malus_Dex; } }

        public override int ArmorBase { get { return 30; } }
        public override int RevertArmorBase { get { return 4; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public BourgeonLeggings()
            : base(0x2859)
        {
            Weight = 2.0;
            Name = "Jambières Bourgeon";
        }

        public BourgeonLeggings(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
    public class BourgeonTunic : BaseArmor
    {
        //public override int NiveauAttirail { get { return Bourgeon_Niveau; } }

        public override int BasePhysicalResistance { get { return ArmorBourgeon.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ArmorBourgeon.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ArmorBourgeon.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ArmorBourgeon.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ArmorBourgeon.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorBourgeon.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorBourgeon.max_Durabilite; } }

        public override int AosStrReq { get { return ArmorBourgeon.force_Requise; } }
        public override int AosDexBonus { get { return ArmorBourgeon.malus_Dex; } }

        public override int ArmorBase { get { return 30; } }
        public override int RevertArmorBase { get { return 4; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public BourgeonTunic()
            : base(0x285A)
        {
            Weight = 2.0;
            Name = "Tunique Bourgeon";
        }

        public BourgeonTunic(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
