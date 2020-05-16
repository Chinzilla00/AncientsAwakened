using Terraria.ID;

namespace AAMod.Items.Boss.Djinn
{
    public class Sandagger : BaseAAItem
	{
		public override void SetDefaults()
		{

            item.damage = 15;            
            item.ranged = true;
            item.width = 14;
            item.height = 14;
			item.useTime = 8;
            item.maxStack = 999;
			item.useAnimation = 8;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
			item.value = 8;
			item.rare = ItemRarityID.Orange;
			item.shootSpeed = 9f;
			item.shoot = mod.ProjectileType ("Sandagger");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.consumable = true;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandagger");
            Tooltip.SetDefault("");
        }
    }
}
