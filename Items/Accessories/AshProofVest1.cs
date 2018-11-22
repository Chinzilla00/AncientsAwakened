using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    public class AshProofVest1 : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 36;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }

        public override void UpdateInventory(Player player)
        {
            if (item.type == mod.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[item.type].Frame == 5)
                {
                    item.TurnToAir();
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (item.type == mod.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[item.type].Frame == 5)
                {
                    item.TurnToAir();
                }
            }
            if (item.accessory)
            {
                player.GetModPlayer<AAPlayer>().AshRemover = true;
                if (player.GetModPlayer<AAPlayer>().ZoneInferno && !Main.dayTime && ((!AAWorld.downedAkuma && !Main.expertMode) || (!AAWorld.downedAkumaA && Main.expertMode)))
                {
                    if (Main.rand.Next(3600) == 0)
                    {
                        if (item.type == mod.ItemType<AshProofVest3>())
                        {
                            Main.PlaySound(SoundID.Item34);
                            item.type = mod.ItemType<AshProofVest2>();
                            item.CloneDefaults(mod.ItemType<AshProofVest2>());
                            item.stack++;
                            item.stack--;
                        }
                        else if (item.type == mod.ItemType<AshProofVest2>())
                        {
                            Main.PlaySound(SoundID.Item34);
                            item.type = mod.ItemType<AshProofVest1>();
                            item.CloneDefaults(mod.ItemType<AshProofVest1>());
                            item.stack++;
                            item.stack--;
                        }
                        else
                        {
                            Main.PlaySound(SoundID.Item34);
                            item.type = mod.ItemType<AshProofVest0>();
                            item.CloneDefaults(mod.ItemType<AshProofVest0>());
                            item.stack++;
                            item.stack--;
                        }
                    }
                }
            }
            else
            {
                player.GetModPlayer<AAPlayer>().AshRemover = false;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash-Proof Vest");
            Tooltip.SetDefault(@"Temporary accessory to completly remove Ash Rain");
        }
    }
}