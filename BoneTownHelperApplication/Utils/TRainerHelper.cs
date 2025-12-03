using System;
using System.Threading;
using System.Threading.Tasks;
using Memory;

namespace BoneTownHelperApplication.Utils {
    public static class TRainerHelper {
        public const string ProcessName = "BoneTown";
        public const string ModuleName = "BoneTown.exe";
        
        private const string Balls_Sexy_Progress = ModuleName + "+0x00532A28,0x2B8,0x470";
        private const string Balls_Sexy_Count    = ModuleName + "+0x00532A28,0x2B8,0x474";

        private const string Health = ModuleName + "+0x00532A28,0x2B8,0x46C";
        private const string Effect_Countdown_Weed = ModuleName + "+0x00532A28,0x2B8,0x5C4";
        private const string Effect_Countdown_Crack = ModuleName + "+0x00532A28,0x2B8,0x5D4";
        private const string Effect_Climax = ModuleName + "+0x00532A28,0x2B8,0x808";

        public const string Money = ModuleName + "+0x00532A28,0x2B8,0x478";
        
        public const string Beer    = ModuleName + "+0x00532A28,0x2B8,0x5A0";
        public const string Whiskey = ModuleName + "+0x00532A28,0x2B8,0x5A4";
        public const string Weed    = ModuleName + "+0x00532A28,0x2B8,0x5A8";
        public const string Shroom  = ModuleName + "+0x00532A28,0x2B8,0x5AC";
        public const string Peyote  = ModuleName + "+0x00532A28,0x2B8,0x5B0";
        public const string Frog    = ModuleName + "+0x00532A28,0x2B8,0x5B4";
        public const string Crack   = ModuleName + "+0x00532A28,0x2B8,0x5B8";
        
        public const string XAxis   = ModuleName + "+0x00532A28,0x2B8,0x7C4";
        public const string YAxis   = ModuleName + "+0x00532A28,0x2B8,0x7C8";
        public const string ZAxis   = ModuleName + "+0x00532A28,0x2B8,0x7CC";
        
        //人物朝向的角度
        private const string DegreePersonFront  = ModuleName + "+0x00532A28,0x2B8,0x7D0";
        //鼠标左右移动的角度
        private const string DegreeMouseLeftRight = ModuleName + "+0x00532A28,0xA0,0x3AC";
        
        //灯光
        private const string LampLight = ModuleName + "+0x00633CC4,0x34";
        
        //环境亮度
        private const string Brightness_Ground_Green2 = ModuleName + "+0x00633D9C,0x14,0x1C8";
        private const string Brightness_Ground_Purper2 = ModuleName + "+0x00633D9C,0x14,0x1CC";
        private const string Brightness_Ground_Yellow2 = ModuleName + "+0x00633D9C,0x14,0x1D0";
        private const string Brightness_Ground_Green = ModuleName + "+0x00633D9C,0x14,0x1D4";
        private const string Brightness_Ground_Purper = ModuleName + "+0x00633D9C,0x14,0x1D8";
        private const string Brightness_Ground_Yellow = ModuleName + "+0x00633D9C,0x14,0x1DC";


        public const string StrAbout = "游戏操作说明:\n" +
                                       "Shift + ~\t    : 老版本游戏开启控制台(可将鼠标切换出游戏界面)\n" +
                                       "Alt + Tab\t    : 从游戏中切换到其它程序\n" +
                                       "1 ~ 7 \t    : 切换食物\n" +
                                       "C     \t    : 按住就是防御\n" +
                                       "E     \t    : 吃东西，可以用鼠标滚轮切换食物(眼睛下吃东西有FBI)\n" +
                                       "F     \t    : 切换武器(显示/隐藏)\n" +
                                       "Q     \t    : 换装/换武器\n" +
                                       "Shift + 鼠左: 技能攻击\n" +
                                       "鼠标左键 \t   : 普攻\n" +
                                       "鼠标中键 \t   : 按住就是防御\n" +
                                       "鼠标右键 \t   : 跳起来双手攻击\n" +
                                       "空格   \t   : 跳跃\n" +
                                       "\n" + 
                                       "1.本软件针对英文版, 因为我没有中文版本. (这游戏剧情不难, 汉化版也只汉化了菜单那几个按钮, 所以没有安装中文版)\n" +
                                       "2.如果你使用了另外的修改器, 可以和其他修改器混用.\n" +
                                       "3.使用示例:\n" +
                                       "  1.和妹子友好交流的时候, 也可以平移, 能够看见更多细节哟(^_^).\n" +
                                       "  2.打Boss的时候也可以直接平移到他头顶, 然后一直在空中放闪电技能(在空中的时候不要走动, 否则会掉下来).\n" +
                                       "4.有问题请在百度贴吧发帖子反馈: https://tieba.baidu.com/f?kw=bonetown, (我想起来的时候会去看看).\n" +
                                       "5.杀毒软件报毒: 请自己添加进白名单.\n" +
                                       "6.作者 actor2015\n" +
                                       "7.版本 20251118 & v1.2.1\n" +
                                       "\n" +
                                       "Game Operation instructions\n" +
                                       "Shift + ~ \t\t     : Old version game open the console (you can switch the mouse out of the game interface)\n" +
                                       "Alt + Tab \t     : Switch from the game to other programs\n" +
                                       "1 ~ 7 \t\t     : Switch foods\n" +
                                       "C     \t\t     : Pressing down is defense\n" +
                                       "E     \t\t     : Eat, you can use the mouse wheel to switch foods.(There's some FBI under eyes when eating.)\n" +
                                       "F     \t\t     : Switch weapons (Show/Hide)\n" +
                                       "Q     \t\t     : Change outfits/change weapons\n" +
                                       "Shift + Mouse Left      : Skill attack\n" +
                                       "Left mouse button      : Normal attack\n" +
                                       "Middle mouse button: Press and hold to defend\n" +
                                       "Right mouse button   : Jump up and attack with both hands\n" +
                                       "Space \t\t     : Jump\n" +
                                       "\n" +
                                       "1.This trainer not support Chinese menu version game.\n" +
                                       "2.If you use other trainers, you can use this with others.\n" +
                                       "3.Use example:\n" +
                                       "  1.If you make with the girl, you can translation too, and you can see more details(^_^).\n" +
                                       "  2.If you hit boss, you can translation to hi's head, and release lightning(don't move, or you will drop down).\n" +
                                       "4.If you have any issues, Pls issue at https://tieba.baidu.com/f?kw=bonetown(Chinese webside) to feedback.(Pls explain you country and issues in webside, i will see sometimes.)\n" +
                                       "5.If the antivirus software reports an error, Pls add this to whitelist.\n" +
                                       "6.Author actor2015\n" +
                                       "7.Version 20251118 & v1.2.1";

        public const string StrBrightness = "亮度修改说明:\n" +
                                            "前提: 游戏在白天/黑夜转换的时候也在修改亮度, 所以:\n" +
                                            "    1.开灯后, 有时候灯会闪动, 这是正常现象.\n" +
                                            "    2.设置亮度后, 人物周围亮度会突然改变, 这是正常现象.\n" +
                                            "所以:\n" +
                                            "    当人物周围亮度突然改变后, 等游戏将亮度修改完, 再自行点击下方修改亮度.\n" +
                                            "另外:\n" +
                                            "    地图7(蘑菇沼泽🍄)无法修改亮度, 因为这个地图里亮度一直在变, 无法插手.\n" +
                                            "\n" +
                                            "Brightness modification instructions:\n" +
                                            "Premise: The game also modifies the brightness when time to switch the day / night, so:\n" +
                                            "    1.After turning on the light, sometimes it flashes. This is a normal phenomenon.\n" +
                                            "    2.After setting the brightness, the brightness around the character will suddenly change. This is a normal phenomenon.\n" +
                                            "So:\n" +
                                            "    When the brightness around the character suddenly changes, wait for the game to modify the brightness, and then click the button below to modify the brightness by yourself.\n" +
                                            "Also:\n" +
                                            "    Map 7(Mushroom Marsh🍄) cannot have its brightness modified, because the brightness in this map is constantly changing.";

        //Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林)
        public static readonly float[] CoordinateMissionaryBeach2FirmWoodForest = { 1066.876f, -347.3445f, 50.521f };

        //Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地)
        public static readonly float[] CoordinateMissionaryBeach2GabachoHeights = { 1177.695f, 433.6344f, 56.082f };

        //Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩)
        public static readonly float[] CoordinateFirmWoodForest2MissionaryBeach = { 1066.192f, -372.8936f, 105.096f };

        //Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园)
        public static readonly float[] CoordinateFirmWoodForest2HomelandTrailerPark = { 920.4601f, -812.3506f, 105.376f };

        //Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林)
        public static readonly float[] CoordinateHomelandTrailerPark2FirmWoodForest = { 716.3653f, -602.741f, 138.2291f };

        //Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩)
        public static readonly float[] CoordinateGabachoHeights2MissionaryBeach = { 248.1642f, 1012.699f, 286.917f };

        //Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)
        public static readonly float[] CoordinateGabachoHeights2HavajoIndianReservation = { -19.91007f, 997.032f, 275.5724f };

        //Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山)
        public static readonly float[] CoordinateGabachoHeights2NobbingHill = { -307.834f, 1045.434f, 291.167f };

        //Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地)
        public static readonly float[] CoordinateHavajoIndianReservation2GabachoHeights = { 1101.682f, 1483.074f, 53.1938f };

        //Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心)
        public static readonly float[] CoordinateNobbingHill2DownTown = { -646.0f, 146f, 414.574f };

        //Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地)
        public static readonly float[] CoordinateNobbingHill2GabachoHeights = { -968.0f, 367f, 414.574f };

        //Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽)
        public static readonly float[] CoordinateNobbingHill2MushroomMarsh = { -821.0f, -108.0f, 409.991f };

        //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan)
        public static readonly float[] CoordinateMushroomMarsh_Satan = { -1318.774f, -120.5363f, 459.324f };

        //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife)
        public static readonly float[] CoordinateMushroomMarsh_SatanWife = { -1379.268f, -332.1819f, 493.792f };

        //Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山)
        public static readonly float[] CoordinateMushroomMarsh2NobbingHill = { -1027.47f, 98.30942f, 415.667f };

        //Map8 大富翁RonJ
        public static readonly float[] CoordinateDowntown_RonJEntrance = { -318.0f, -436f, 633.894f };

        //Map8 天使
        public static readonly float[] CoordinateDowntown_Angle = { 15.0f, -938.6f, 771.5678f };

        //Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点
        public static readonly float[] CoordinateDownTown2ManIsland = { -858.6f, -636.0f, 650.2626f };

        //Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点
        public static readonly float[] CoordinateDownTown2NobbingHill = { -291.0f, -426.6f, 626.894f };

        //Map9(Man Island 曼岛)→高塔入口(Man Needle)
        public static readonly float[] CoordinateManIsland_ManNeedle = { -145.5f, 532.0f, 883.1036f };

        //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
        public static readonly float[] CoordinateManIsland2DownTown = { -409.55f, 103.70f, 710.8217f };


        /// <summary>
        ///                             大晚上亮度   傍晚亮度   大白天亮度
        /// Lamp_Light                  3F800000    00000000    00000000
        /// SkyEdge_Color               FF1A0000    FF803B73    FFD9A6A6    //天上亮度, 最主要参数
        /// SkyEdge_Light               00000000    3F400000    3F800000
        /// Object_Light                00000000    00000000    3F800000    //晚上改的时候, 右侧参数会亮一些
        /// SkyEdge_Inner_Green_Light   00000000    3EE66666    3F266666    //视野范围内远处山区(大晚上改成白天: 远方下阵雨的效果)
        /// SkyEdge_Inner_Blue_Light    00000000    3E6B851F    3F266666
        /// SkyEdge_Inner_Yellow_Light  3DCCCCCD    3F000000    3F59999A
        /// Ground_Green2               3DF5C28F    3F19999A    3F333333    //方圆100米颜色亮度(改了后效果比较好)
        /// Ground_Purper2              3E428F5C    3F0CCCCD    3F333333
        /// Ground_Yellow2              3F266666    3F0CCCCD    3F266666
        /// Ground_Green                3D75C28F    3EE66666    3F19999A    //地面颜色亮度(改了后效果一般)
        /// Ground_Purper               3DE147AE    3E4CCCCD    3F19999A
        /// Ground_Yellow               3E99999A    3E800000    3F000000
        /// </summary>
        public static readonly long[] LampBrightness = { 0x00000000, 0x3F800000 };
        private static readonly long[] Ground_Green2 = { 0x3DF5C28F, 0x3F19999A, 0x3F333333 };
        private static readonly long[] Ground_Purper2 = { 0x3E428F5C, 0x3F0CCCCD, 0x3F333333 };
        private static readonly long[] Ground_Yellow2 = { 0x3F266666, 0x3F0CCCCD, 0x3F266666 };
        private static readonly long[] Ground_Green = { 0x3D75C28F, 0x3EE66666, 0x3F19999A };
        private static readonly long[] Ground_Purper = { 0x3DE147AE, 0x3E4CCCCD, 0x3F19999A };
        private static readonly long[] Ground_Yellow = { 0x3E99999A, 0x3E800000, 0x3F000000 };


        private static readonly System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer();
        //程序集名称: BoneTownHelperApplication
        private static readonly string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;


        public static void MoneyAdd() {
            int money = MemoryDllUtils.ReadInt(Money);
            bool isSuccess = MemoryDllUtils.WriteInt(Money, money + 1000);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("钱+1000失败!");
            }
        }

        public static void BeerAdd() {
            int beer = MemoryDllUtils.ReadInt(Beer);
            bool isSuccess = MemoryDllUtils.WriteInt(Beer, beer + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("啤酒+100失败!");
            }
        }

        public static void WhiskeyAdd() {
            int whiskey = MemoryDllUtils.ReadInt(Whiskey);
            bool isSuccess = MemoryDllUtils.WriteInt(Whiskey, whiskey + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("威士忌+100失败!");
            }
        }

        public static void NugAdd() {
            int nug = MemoryDllUtils.ReadInt(Weed);
            bool isSuccess = MemoryDllUtils.WriteInt(Weed, nug + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("大麻+100失败!");
            }
        }

        public static void ShroomAdd() {
            int shroom = MemoryDllUtils.ReadInt(Shroom);
            bool isSuccess = MemoryDllUtils.WriteInt(Shroom, shroom + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("蘑菇+100失败!");
            }
        }

        public static void PeyoteAdd() {
            int peyote = MemoryDllUtils.ReadInt(Peyote);
            bool isSuccess = MemoryDllUtils.WriteInt(Peyote, peyote + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("仙人掌+100失败!");
            }
        }

        public static void FrogAdd() {
            int frog = MemoryDllUtils.ReadInt(Frog);
            bool isSuccess = MemoryDllUtils.WriteInt(Frog, frog + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("青蛙+100失败!");
            }
        }

        public static void CrackAdd() {
            int crack = MemoryDllUtils.ReadInt(Crack);
            bool isSuccess = MemoryDllUtils.WriteInt(Crack, crack + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("可卡因+100失败!");
            }
        }


        public static void XAxisEdit(bool isEast, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(XAxis, isEast ? x + value : x - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isEast ? "向东+5失败!" : "向西+5失败!");
            }
        }

        public static void YAxisEdit(bool isNorth, int value) {
            float y = MemoryDllUtils.ReadFloat(YAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(YAxis, isNorth ? y + value : y - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isNorth ? "向北+5失败!" : "向南+5失败!");
            }
        }

        public static void ZAxisEdit(bool isUp, int value) {
            float z = MemoryDllUtils.ReadFloat(ZAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(ZAxis, isUp ? z + value : z - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isUp ? "高度+5失败!" : "高度-5失败!");
            }
        }

        //jj性感度加到最大
        public static void BallsAdd210() {
            //TODO: byte(9), 2bytes(10), int(10)实际都能写入成功, 但代码返回失败???
            // bool isSuccess = MemoryDllUtils.WriteByte(Balls_Sexy_Count, 0xa);
            bool isSuccess = MemoryDllUtils.WriteInt(Balls_Sexy_Count, 10);
            if (isSuccess) {
                Console.WriteLine("jj性感度(Balls_Sexy_Count)加到最大(10)失败!");
                // return;
            }
            isSuccess = MemoryDllUtils.WriteFloat(Balls_Sexy_Progress, 10f);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("jj性感进度(Balls_Sexy_Progress)加到最大(10)失败!");
            }
        }

        /// <summary>
        /// 是否冻结无限健康
        /// </summary>
        /// <param name="isFreezeHealth"></param>
        public static void FreezeHealth(bool isFreezeHealth) {
            if (isFreezeHealth) {
                bool isSuccess = MemoryDllUtils.FreezeValue(Health, "float", 1f);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结无限健康 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Health);
            }
        }

        /// <summary>
        /// 是否冻结跳高效果
        /// </summary>
        /// <param name="isFreezeHighJump"></param>
        public static void FreezeHighJump(bool isFreezeHighJump) {
            if (isFreezeHighJump) {
                bool isSuccess = MemoryDllUtils.FreezeValue(Effect_Countdown_Weed, "float", 1f);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结跳高效果 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Effect_Countdown_Weed);
            }
        }

        /// <summary>
        /// 是否冻结快跑效果
        /// </summary>
        /// <param name="isFreezeFastRun"></param>
        public static void FreezeFastRun(bool isFreezeFastRun) {
            if (isFreezeFastRun) {
                bool isSuccess = MemoryDllUtils.FreezeValue(Effect_Countdown_Crack, "float", 1f);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结快跑效果 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Effect_Countdown_Crack);
            }
        }

        /// <summary>
        /// 冻结快感进度
        /// </summary>
        /// <param name="isFreezeClimax"></param>
        public static void FreezeClimax(bool isFreezeClimax) {
            if (isFreezeClimax) {
                bool isSuccess = MemoryDllUtils.FreezeValue(Effect_Climax, "float", 1f);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结快感进度 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Effect_Climax);
            }
        }


        /// <summary>
        /// 人物向左平移 or 向右平移
        /// </summary>
        /// <param name="isRight"></param>
        /// <param name="value"></param>
        public static void GoRightOrLeft(bool isRight, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            float y = MemoryDllUtils.ReadFloat(YAxis);
            // double degreePersonFront = GetDegreePersonFront();
            float degreeMouseLeftRight = GetDegreeMouseLeftRight();
            bool isSetDegreePersonFrontSuccess = SetDegreePersonFront(degreeMouseLeftRight);
            x = isRight ? x + (float)Math.Cos(degreeMouseLeftRight) * value : x - (float)Math.Cos(degreeMouseLeftRight) * value;
            y = isRight ? y + (float)Math.Sin(degreeMouseLeftRight) * value * -1 : y - (float)Math.Sin(degreeMouseLeftRight) * value * -1;
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, x);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, y);
            if (isSuccessX && isSuccessY) {
                PlayAng();
            } else {
                Console.WriteLine(isRight ? "人物向右平移失败!" : "人物向左平移失败!");
            }
        }

        /// <summary>
        /// 人物向前平移 or 往后退
        /// </summary>
        /// <param name="isFront"></param>
        /// <param name="value"></param>
        public static void GoFrontOrBack(bool isFront, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            float y = MemoryDllUtils.ReadFloat(YAxis);
            // float degreePersonFront = GetDegreePersonFront();
            float degreeMouseLeftRight = GetDegreeMouseLeftRight();
            bool isSetDegreePersonFrontSuccess = SetDegreePersonFront(degreeMouseLeftRight);
            x = isFront ? x + (float)Math.Sin(degreeMouseLeftRight) * value : x - (float)Math.Sin(degreeMouseLeftRight) * value;
            y = isFront ? y + (float)Math.Cos(degreeMouseLeftRight) * value : y - (float)Math.Cos(degreeMouseLeftRight) * value;
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, x);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, y);
            if (isSuccessX && isSuccessY) {
                PlayAng();
            } else {
                Console.WriteLine(isFront ? "人物向前平移失败!" : "人物向后平移失败!");
            }
        }

        /// <summary>
        /// 灯光设置
        /// </summary>
        /// <param name="isOpen">是否打开</param>
        /// <param name="isFreeze">是否冻结值</param>
        public static void LampLightSet(bool isOpen, bool isFreeze, bool isPlayAng) {
            bool isSuccess;
            if (isFreeze) {
                //一直闪, 应该是修改间隔25ms还是太久了
                isSuccess = MemoryDllUtils.FreezeValue(LampLight, "long", LampBrightness[isOpen ? 1 : 0]);
            } else {
                MemoryDllUtils.UnfreezeValue(LampLight);
                isSuccess = MemoryDllUtils.WriteLong(LampLight, LampBrightness[isOpen ? 1 : 0]);
            }
            if (isSuccess) {
                if (isPlayAng) PlayAng();
            } else {
                Console.WriteLine("灯光亮度设置 失败!");
            }
        }


        /// <summary>
        /// 环境亮度设置
        /// </summary>
        /// <param name="position">第几个亮度</param>
        /// <param name="isFreeze">是否冻结值</param>
        public static void BrightnessSet(int position, bool isFreeze) {
            bool isSuccess;
            if (isFreeze) {
                //一直闪, 应该是修改间隔25ms还是太久了
                bool isSuccess0 = MemoryDllUtils.FreezeValue(Brightness_Ground_Green2, "long", Ground_Green2[position]);
                bool isSuccess1 = MemoryDllUtils.FreezeValue(Brightness_Ground_Purper2, "long", Ground_Purper2[position]);
                bool isSuccess2 = MemoryDllUtils.FreezeValue(Brightness_Ground_Yellow2, "long", Ground_Yellow2[position]);
                bool isSuccess3 = MemoryDllUtils.FreezeValue(Brightness_Ground_Green, "long", Ground_Green[position]);
                bool isSuccess4 = MemoryDllUtils.FreezeValue(Brightness_Ground_Purper, "long", Ground_Purper[position]);
                bool isSuccess5 = MemoryDllUtils.FreezeValue(Brightness_Ground_Yellow, "long", Ground_Yellow[position]);
                isSuccess = isSuccess0 && isSuccess1&& isSuccess2 && isSuccess3 && isSuccess4 && isSuccess5;
            } else {
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Green2);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Purper2);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Yellow2);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Green);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Purper);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Yellow);
                bool isSuccess0 = MemoryDllUtils.WriteLong(Brightness_Ground_Green2, Ground_Green2[position]);
                bool isSuccess1 = MemoryDllUtils.WriteLong(Brightness_Ground_Purper2, Ground_Purper2[position]);
                bool isSuccess2 = MemoryDllUtils.WriteLong(Brightness_Ground_Yellow2, Ground_Yellow2[position]);
                bool isSuccess3 = MemoryDllUtils.WriteLong(Brightness_Ground_Green, Ground_Green[position]);
                bool isSuccess4 = MemoryDllUtils.WriteLong(Brightness_Ground_Purper, Ground_Purper[position]);
                bool isSuccess5 = MemoryDllUtils.WriteLong(Brightness_Ground_Yellow, Ground_Yellow[position]);
                isSuccess = isSuccess0 && isSuccess1&& isSuccess2 && isSuccess3 && isSuccess4 && isSuccess5;
            }
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("环境亮度设置 失败!");
            }
        }
        public static void BrightnessSet2(int position) {
            bool isSuccess;
            // byte[] lpBuffer = new byte[4];
            // int nSize = 8;
            // lpBuffer = BitConverter.GetBytes(Convert.ToInt64(write));
            byte[] lpBuffer = BitConverter.GetBytes(Ground_Green2[position]);
            UIntPtr code1 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Green2, "");
            if (code1 == UIntPtr.Zero || code1.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code1, lpBuffer, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer2 = BitConverter.GetBytes(Ground_Purper2[position]);
            UIntPtr code2 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Purper2, "");
            if (code2 == UIntPtr.Zero || code2.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code2, lpBuffer2, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer3 = BitConverter.GetBytes(Ground_Yellow2[position]);
            UIntPtr code3 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Yellow2, "");
            if (code3 == UIntPtr.Zero || code3.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code3, lpBuffer3, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer4 = BitConverter.GetBytes(Ground_Green[position]);
            UIntPtr code4 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Green, "");
            if (code4 == UIntPtr.Zero || code4.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code4, lpBuffer4, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer5 = BitConverter.GetBytes(Ground_Purper[position]);
            UIntPtr code5 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Purper, "");
            if (code5 == UIntPtr.Zero || code5.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code5, lpBuffer5, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer6 = BitConverter.GetBytes(Ground_Yellow[position]);
            UIntPtr code6 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Yellow, "");
            if (code6 == UIntPtr.Zero || code6.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code6, lpBuffer6, (UIntPtr) 8, IntPtr.Zero);

            isSuccess = true;
            
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("环境亮度设置 失败!");
            }
        }


        /// <summary>
        /// 瞬移到坐标
        /// </summary>
        /// <param name="coordinate"></param>
        public static void Teleport(float[] coordinate, string failureStr) {
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, coordinate[0]);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, coordinate[1]);
            bool isSuccessZ = MemoryDllUtils.WriteFloat(ZAxis, coordinate[2]);
            if (isSuccessX && isSuccessY && isSuccessZ) {
                PlayAng();
            } else {
                Console.WriteLine(failureStr);
            }
        }


        //播放ang
        public static void PlayAng() {
            Uri uri = new Uri("Resources/Medias/ang.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(SoundPlayer, uri);
            SoundPlayerUtils.Play(SoundPlayer);
        }

        //播放click
        public static void PlayClick() {
            Uri uri = new Uri($"Resources/Medias/click.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(SoundPlayer, uri);
            SoundPlayerUtils.Play(SoundPlayer);
        }

        //播放activate
        public static void PlayActivate(bool isTRainerOpen) {
            Uri uri = isTRainerOpen ? new Uri($"Resources/Medias/activate.wav", UriKind.Relative)
                :  new Uri($"Resources/Medias/deactivate.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(SoundPlayer, uri);
            SoundPlayerUtils.Play(SoundPlayer);
        }


        /// <summary>
        /// 获取人物正前方角度 (从E开始, E右侧最小, E左侧最大)
        /// minDegree = 1.49568E-05(0.0000149568 ‌), degreeI = 930803445
        /// maxDegree =             6.283183,           degreeI = 1086918614
        /// </summary>
        /// <returns>返回弧度: (0 ~ 2π), 不是角度: (0°~360°)</returns>
        private static float GetDegreePersonFront() {
            return MemoryDllUtils.ReadFloat(DegreePersonFront);
        }
        private static bool SetDegreePersonFront(float degree) {
            return MemoryDllUtils.WriteFloat(DegreePersonFront, degree);
        }

        /// <summary>
        /// 获取鼠标左右旋转角度 (从E开始, E右侧最小, E左侧最大)
        /// </summary>
        /// <returns>返回弧度: (0 ~ 2π), 不是角度: (0°~360°)</returns>
        private static float GetDegreeMouseLeftRight() {
            return MemoryDllUtils.ReadFloat(DegreeMouseLeftRight);
        }


        /// <summary>
        /// 获取开关图片
        /// </summary>
        /// <param name="isOn">是否打开</param>
        /// <returns></returns>
        public static Uri GetSwitchUri(bool isOn) {
            if (isOn) {
                return new Uri($"pack://application:,,,/{AssemblyName};component/Resources/Images/icon_switch_green2.png");
            }
            return new Uri($"pack://application:,,,/{AssemblyName};component/Resources/Images/icon_switch_lightyellow.png");
        }
        

        /// <summary>
        /// 获取人物正前方角度
        /// minDegree = 1.49568E-05(0.0000149568 ‌), degreeI = 930803445
        /// maxDegree =             6.283183,           degreeI = 1086918614
        /// </summary>
        public static void PrintDegreePersonFront() {
            float minDegree = 100, maxDegree = -100;
            Task.Factory.StartNew((Action)(() => {
                while (true) {
                    if (minDegree == 0) {
                        minDegree = 100;
                        Console.WriteLine($"minDegree to 100!");
                    }

                    float degree = MemoryDllUtils.ReadFloat(DegreePersonFront);
                    int degreeI = MemoryDllUtils.ReadInt(DegreePersonFront);
                    if (degree < minDegree) {
                        minDegree = degree;
                        Console.WriteLine($"minDegree = {minDegree}, degreeI = {degreeI}");
                    } else if (degree > maxDegree) {
                        maxDegree = degree;
                        Console.WriteLine($"maxDegree = {maxDegree}, degreeI = {degreeI}");
                    }

                    Thread.Sleep(10);
                }
            }));
        }
    }
}