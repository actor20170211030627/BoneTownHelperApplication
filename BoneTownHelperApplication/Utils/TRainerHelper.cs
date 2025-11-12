using System;
using System.Threading;
using System.Threading.Tasks;

namespace BoneTownHelperApplication.Utils {
    public static class TRainerHelper {
        public const string ProcessName = "BoneTown";
        private const string ModuleName = "BoneTown.exe";
        public const string Money = ModuleName + "+0x00532A28,0x2B8,0x478";
        public const string Beer = ModuleName + "+0x00532A28,0x2B8,0x5A0";
        public const string Whiskey = ModuleName + "+0x00532A28,0x2B8,0x5A4";
        public const string Weed = ModuleName + "+0x00532A28,0x2B8,0x5A8";
        public const string Shroom = ModuleName + "+0x00532A28,0x2B8,0x5AC";
        public const string Peyote = ModuleName + "+0x00532A28,0x2B8,0x5B0";
        public const string Frog = ModuleName + "+0x00532A28,0x2B8,0x5B4";
        public const string Crack = ModuleName + "+0x00532A28,0x2B8,0x5B8";
        private const string Degree = ModuleName + "+0x00532A28,0x2B8,0x7A8";
        public const string XAxis = ModuleName + "+0x00532A28,0x2B8,0x7C4";
        public const string YAxis = ModuleName + "+0x00532A28,0x2B8,0x7C8";
        public const string ZAxis = ModuleName + "+0x00532A28,0x2B8,0x7CC";
        // private const string Degree  = ModuleName + "+0x00532A28,0x2B8,0x7D0";


        public const string StrAbout = "游戏操作说明:\n" +
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
                                       "7.版本 20230507 & v1.0\n" +
                                       "\n" +
                                       "Game Operation instructions\n" +
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
                                       "7.Version 20230507 & v1.0";

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


        public static readonly System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer();


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


        /// <summary>
        /// 人物向左平移 or 向右平移
        /// </summary>
        /// <param name="isRight"></param>
        /// <param name="value"></param>
        public static void GoRightOrLeft(bool isRight, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            float y = MemoryDllUtils.ReadFloat(YAxis);
            double rotateDegree = GetRotateDegree();
            x = isRight ? x + (float)Math.Cos(rotateDegree) * value : x - (float)Math.Cos(rotateDegree) * value;
            y = isRight ? y + (float)Math.Sin(rotateDegree) * value * -1 : y - (float)Math.Sin(rotateDegree) * value * -1;
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
            double rotateDegree = GetRotateDegree();
            x = isFront ? x + (float)Math.Sin(rotateDegree) * value : x - (float)Math.Sin(rotateDegree) * value;
            y = isFront ? y + (float)Math.Cos(rotateDegree) * value : y - (float)Math.Cos(rotateDegree) * value;
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, x);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, y);
            if (isSuccessX && isSuccessY) {
                PlayAng();
            } else {
                Console.WriteLine(isFront ? "人物向前平移失败!" : "人物向后平移失败!");
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


        /// <summary>
        /// 获取人物左右旋转角度 (从E开始, E右侧最小, E左侧最大)
        /// minDegree = 1.49568E-05(0.0000149568 ‌), degreeI = 930803445
        /// maxDegree =             6.283183,           degreeI = 1086918614
        /// </summary>
        /// <returns>返回弧度: (-1 ~ 1), 不是角度: (0~360)</returns>
        public static double GetRotateDegree() {
            if (false) {
                // 1086918614 - 930803445 = 156115169
                // 156115169 / 360°       = 433653.2472222222/°
                //180°左右的值: 1078572423, 与最大值差距: 8346191,    与最小值差距: 147768978, 数值极不合理, 算出角度偏差巨大!
                int rotateDegree = MemoryDllUtils.ReadInt(Degree);
                if (rotateDegree < 930803445) {
                    Console.WriteLine($"发现更小的人物左右旋转角度: {rotateDegree}");
                    return 0.0D;
                }

                if (rotateDegree > 1086918614) {
                    Console.WriteLine($"发现更大的人物左右旋转角度: {rotateDegree}");
                    return 360.0D;
                }
                return (rotateDegree - 930803445) / 433653.2472222222D;
            }

            // 6.283183 - 0.0000149568 = 6.2831680432
            // 6.2831680432 / 360°     = 0.0174532445644444/°
            // 6.2831680432 / 2π       = 0.99999725235231137462218436295753 ≈ 1
            float rotateDegreeF = MemoryDllUtils.ReadFloat(Degree);
            if (false) {
                if (rotateDegreeF < 0.0000149568f) {
                    Console.WriteLine($"发现更小的人物左右旋转角度: {rotateDegreeF}");
                    return 0.0D;
                }

                if (rotateDegreeF > 6.283183f) {
                    Console.WriteLine($"发现更大的人物左右旋转角度: {rotateDegreeF}");
                    return 360.0D;
                }
                //0~360°
                // double degree = (rotateDegreeF - 0.0000149568f) / 0.0174532445644444D;
                // return (rotateDegreeF - 0.0000149568f) / 0.99999725235231137462218436295753D;
            }

            //原来读取到的就是 0 ~ 2π ...
            return rotateDegreeF;
        }

        /// <summary>
        /// 获取人物左右旋转角度
        /// minDegree = 1.49568E-05(0.0000149568 ‌), degreeI = 930803445
        /// maxDegree =             6.283183,           degreeI = 1086918614
        /// </summary>
        public static void PrintDegree() {
            float minDegree = 100, maxDegree = -100;
            Task.Factory.StartNew((Action)(() => {
                while (true) {
                    if (minDegree == 0) {
                        minDegree = 100;
                        Console.WriteLine($"minDegree to 100!");
                    }

                    float degree = MemoryDllUtils.ReadFloat(Degree);
                    int degreeI = MemoryDllUtils.ReadInt(Degree);
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