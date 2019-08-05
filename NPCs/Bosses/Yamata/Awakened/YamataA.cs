using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using BaseMod;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataA : Yamata
	{
		bool cheated = false;
        private bool tenthHealth = false;
        private bool threeQuarterHealth = false;
        private bool HalfHealth = false;

        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            displayName = "Shin'en Yamata";
            Main.npcFrameCount[npc.type] = 7;			
        }	

        public override void SetDefaults()
        {
			base.SetDefaults();
			isAwakened = true;
            npc.value = Item.sellPrice(0, 40, 0, 0);
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");		
            bossBag = mod.ItemType("YamataBag");
            npc.defense = 999999;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            musicPriority = MusicPriority.BossHigh;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * .7f);
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                cheated = true;
                if (Main.netMode != 1) BaseUtility.Chat("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE", new Color(146, 30, 68));
            }
            npc.damage = 0;
            return false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            if (!AAWorld.downedYamata && !cheated)
            {
                if (Main.netMode != 1) BaseUtility.Chat("NO...! IMPOSSIBLE! EVEN IN MY AWAKENED FORM?! YOU MUST HAVE CHEATED! GYAAAAAAH..! FINE! TAKE YOUR LOOT! I'M OUTTA HERE..!", new Color(146, 30, 68));
                if (Main.netMode != 1) BaseUtility.Chat("The defeat of Yamata causes the fog in the mire to lift.", Color.Indigo);
            }
            if (AAWorld.downedYamata && !cheated)
            {
                if (Main.netMode != 1) BaseUtility.Chat("NOOOOOOOOOOOOOO!!! YOU LITTLE BRAT!!! I ALMOST HAD YOU THIS TIME!!! FINE, take your stuff, See if I care!", new Color(146, 30, 68));
            }
            if (!Main.expertMode && !cheated)
            {
                if (Main.netMode != 1) BaseUtility.Chat("HAH! Nice try! Come back in Expert mode when you don’t have to cheat to beat me! All your loot is MINE still!", new Color(146, 30, 68));
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                if (!AAWorld.downedYamata)
                {
                    Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("DreadRune"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("YamataATrophy"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("YamataAMask"));
                }

                BaseAI.DropItem(npc, mod.ItemType("YamataATrophy"), 1, 1, 15, true);
                
                npc.DropBossBags();
                AAWorld.downedYamata = true;
                if (AAWorld.downedShen)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SpaceStone"));
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.YamataADust>();
            int dust2 = mod.DustType<Dusts.YamataADust>();
            if (npc.life <= 0)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;

            }
            if (!AAWorld.downedYamata)
            {
                if (npc.life <= (npc.lifeMax / 4 * 3) && threeQuarterHealth == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("YOU'RE STILL FIGHTING?! WHAT THE HELL IS WRONG WITH YOU?!", new Color(146, 30, 68));
                    threeQuarterHealth = true;
                }
                if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("I'VE HAD IT UP TO HERE WITH YOUR SHENANIGANS!!! EAT VENOM YOU LITTLE HELLSPAWN!!!", new Color(146, 30, 68));
                    HalfHealth = true;
                }
                if (npc.life <= npc.lifeMax / 10 && tenthHealth == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("STOP IT STOP IT STOP IT!!! I'M NOT GONNA LET YOU WIN!!!", new Color(146, 30, 68));
                    tenthHealth = true;
                }
            }
            if (AAWorld.downedYamata)
            {
                if (npc.life <= (npc.lifeMax / 4 * 3) && threeQuarterHealth == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("You're an annoying little bugger, you know!", new Color(146, 30, 68));
                    threeQuarterHealth = true;
                }
                if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("DIE! WHY WON'T YOU JUST DIE ALREADY?!", new Color(146, 30, 68));
                    HalfHealth = true;
                }
                if (npc.life <= npc.lifeMax / 10 && tenthHealth == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("STOP IT!!! I'M NOT GONNA LOSE AGAIN!!!", new Color(146, 30, 68));
                    tenthHealth = true;
                }
            }

            if (npc.life <= npc.lifeMax / 2 && !spawnHaruka)
            {
                spawnHaruka = true;
                if (AAWorld.downedYamata)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Looks like I gotta come in and save your rear end again, dad.", new Color(72, 78, 117));
                    if (Main.netMode != 1) BaseUtility.Chat("That's my girl..!", new Color(146, 30, 68));
                    AAModGlobalNPC.SpawnBoss(playerTarget, mod.NPCType("HarukaY"), false, 0, 0);
                    return;
                }
                if (Main.netMode != 1) BaseUtility.Chat("Oh, sweetie..! Care to help daddy thrash this little worm?!", new Color(146, 30, 68));
                if (Main.netMode != 1) BaseUtility.Chat("Sigh...yes dad.", new Color(72, 78, 117));
                AAModGlobalNPC.SpawnBoss(playerTarget, mod.NPCType("HarukaY"), false, 0, 0);
            }
        }

        public bool spawnHaruka = false;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else if (npc.frameCounter < 25)
            {
                npc.frame.Y = 4 * frameHeight;
            }
            else if (npc.frameCounter < 30)
            {
                npc.frame.Y = 5 * frameHeight;
            }
            else if (npc.frameCounter < 35)
            {
                npc.frame.Y = 6 * frameHeight;
            }else
            {
                npc.frameCounter = 0;
            }
        }	
	}
}