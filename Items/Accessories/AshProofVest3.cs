using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Materials;

namespace AAMod.Items.Accessories
{
    public class AshProofVest3 : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 36;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override void UpdateInventory(Player player)
        {
            if (item.type == ModContent.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[item.type].Frame == 5)
                {
                    item.TurnToAir();
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[item.type].Frame == 5)
                {
                    item.TurnToAir();
                }
            }
            if (item.accessory)
            {
                player.buffImmune[mod.BuffType("BurningAsh")] = true;
                if (player.GetModPlayer<AAPlayer>().ZoneInferno && !Main.dayTime && !AAWorld.downedAkuma)
                {
                    if (Main.rand.Next(3600) == 0)
                    {
                        if (item.type == ModContent.ItemType<AshProofVest3>())
                        {
                            Main.PlaySound(SoundID.Item34);
                            item.type = ModContent.ItemType<AshProofVest2>();
                            item.CloneDefaults(ModContent.ItemType<AshProofVest2>());
                            item.stack++;
                            item.stack--;
                        }
                        else if (item.type == ModContent.ItemType<AshProofVest2>())
                        {
                            Main.PlaySound(SoundID.Item34);
                            item.type = ModContent.ItemType<AshProofVest1>();
                            item.CloneDefaults(ModContent.ItemType<AshProofVest1>());
                            item.stack++;
                            item.stack--;
                        }
                        else
                        {
                            Main.PlaySound(SoundID.Item34);
                            item.type = ModContent.ItemType<AshProofVest0>();
                            item.CloneDefaults(ModContent.ItemType<AshProofVest0>());
                            item.stack++;
                            item.stack--;
                        }
                    }
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash-Proof Vest");
            Tooltip.SetDefault(@"Temporary accessory to completly remove Ash Rain");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<HydraClaw>(), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}