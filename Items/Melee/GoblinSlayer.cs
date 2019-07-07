using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class GoblinSlayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Slayer");
			Tooltip.SetDefault(@"Can be swung with left click and thrust forward with a right click
'The blade of a legendary goblin slayer'");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.melee = true;
			item.width = 46;
			item.height = 46;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice (0, 1, 0, 0);
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 3;
            }
            else
            {
                item.useStyle = 1;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.type == NPCID.GoblinArcher
                || target.type == NPCID.GoblinPeon
                || target.type == NPCID.GoblinScout
                || target.type == NPCID.GoblinSorcerer
                || target.type == NPCID.GoblinSummoner
                || target.type == NPCID.GoblinThief
                || target.type == NPCID.GoblinWarrior
                || target.type == NPCID.DD2GoblinBomberT1
                || target.type == NPCID.DD2GoblinBomberT2
                || target.type == NPCID.DD2GoblinBomberT3
                || target.type == NPCID.DD2GoblinT1
                || target.type == NPCID.DD2GoblinT2
                || target.type == NPCID.DD2GoblinBomberT3
                || target.type == NPCID.BoundGoblin
                || target.type == NPCID.GoblinTinkerer)
            {
                item.damage = 60;
                target.AddBuff(BuffID.Bleeding, 400);
            }
            else
            {
                item.damage = 30;
            }
        }
	}
}
