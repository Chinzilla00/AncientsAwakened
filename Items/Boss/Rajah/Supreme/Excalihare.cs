using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class Excalihare : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 500;
			item.melee = true;
			item.width = 80;
			item.height = 80;
			item.useTime = 27;
			item.useAnimation = 27;
			item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Excalihare");
            item.scale = 1.1f;
            item.shootSpeed = 14f;
            item.knockBack = 6.5f;
            item.expert = true; item.expertOnly = true;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity14;
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(Terraria.ModLoader.ModContent.BuffType<Buffs.InfinityOverload>(), 120);
        }
    }
}
