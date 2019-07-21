using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss
{
    public class FireFrostMedallion : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Frost Medallion");
            Tooltip.SetDefault(@"Doubles your stats during a Blizzard or Sandstorm");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 50;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateEquip(Player p)
        {
			if(p.ZoneSandstorm || (p.ZoneRain && p.ZoneSnow))
			{
				p.meleeDamage *= 2f;
				p.rangedDamage *= 2f;
				p.magicDamage *= 2f;
				p.minionDamage *= 2f;
				p.thrownDamage *= 2f;
				p.meleeCrit *= 2;
				p.rangedCrit *= 2;
				p.magicCrit += 2;
				p.thrownCrit *= 2;
			}
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ArcticMedallion", 1);
            recipe.AddIngredient(null, "SandstormMedallion", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<Djinn.SandstormMedallion>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Serpent.ArcticMedallion>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}