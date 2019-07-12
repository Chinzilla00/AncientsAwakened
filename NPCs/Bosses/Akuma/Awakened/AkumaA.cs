using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;
using System.IO;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AkumaA : Akuma
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaA";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 3;
        }

		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.damage = 80;
            npc.defense = 270;
            npc.lifeMax = 700000;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2");
            bossBag = mod.ItemType("AkumaBag");
            if (AAWorld.downedAllAncients)
            {
                npc.damage = 90;
                npc.defense = 280;
                npc.lifeMax = 750000;
            }
            isAwakened = true;
        }
    }

    [AutoloadBossHead]
    public class AkumaAArms : AkumaA
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaAArms";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 60;
            npc.height = 60;
            npc.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }

    [AutoloadBossHead]
    public class AkumaABody : AkumaAArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaABody";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    [AutoloadBossHead]
    public class AkumaABody1 : AkumaAArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaABody1";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    [AutoloadBossHead]
    public class AkumaATail : AkumaAArms
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaATail";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}
