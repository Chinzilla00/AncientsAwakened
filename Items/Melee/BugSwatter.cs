using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BugSwatter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bug Swatter");
			Tooltip.SetDefault(@"Does extra damage to creepy crawlies");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice (0, 1, 0, 0);
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.type == NPCID.Bee
                || target.type == NPCID.BeeSmall
                || target.type == NPCID.BigHornetFatty
                || target.type == NPCID.BigHornetHoney
                || target.type == NPCID.BigHornetLeafy
                || target.type == NPCID.BigHornetSpikey
                || target.type == NPCID.BigHornetStingy
                || target.type == NPCID.GiantMossHornet
                || target.type == NPCID.Hornet
                || target.type == NPCID.HornetFatty
                || target.type == NPCID.HornetHoney
                || target.type == NPCID.HornetSpikey
                || target.type == NPCID.LittleHornetStingy
                || target.type == NPCID.LittleMossHornet
                || target.type == NPCID.MossHornet
                || target.type == NPCID.TinyMossHornet
                || target.type == NPCID.VortexHornet
                || target.type == NPCID.VortexHornetQueen
                || target.type == NPCID.QueenBee
                || target.type == NPCID.LightningBug
                || target.type == NPCID.StardustSpiderBig
                || target.type == NPCID.StardustSpiderSmall
                || target.type == NPCID.WallCreeper
                || target.type == NPCID.WallCreeperWall
                || target.type == NPCID.BlackRecluse
                || target.type == NPCID.BlackRecluseWall)
            {
                item.damage = damage * 3;
            }
            else
            {
                item.damage = 30;
            }
        }
	}
}
