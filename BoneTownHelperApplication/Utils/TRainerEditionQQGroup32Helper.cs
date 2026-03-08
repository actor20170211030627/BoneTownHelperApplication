using System;
using Memory;

namespace BoneTownHelperApplication.Utils {
    public static class TRainerEditionQQGame32Helper {
        public const string ProcessName = "BoneTown32";
        public const string ModuleName = "BoneTown32.exe";
        
        //沉溺进度条
        private const string Drown_ProgressBar = ModuleName + "+0x00339C6C,0x50,0x0,0x50,0x10,0x50,0x0,0xA0";
        //加载的地图🗺️名称
        private const string Map_Name          = ModuleName + "+0x00326080";
        //是否能淹死
        private const string Is_Drown_Deadable = ModuleName + "+0x00357F38,0x148,0x481";
        //是否沉溺, Byte: 0, 1
        private const string Is_Drown_Byte = ModuleName + "+0x00357F38,0x148,0x482";

        private const string Balls_Sexy_Progress = ModuleName + "+0x00357F38,0x148,0x4A0";
        private const string Balls_Sexy_Count    = ModuleName + "+0x00357F38,0x148,0x4A4";

        private const string Health = ModuleName + "+0x00357F38,0x148,0x49C";
        private const string Effect_Countdown_Weed = ModuleName + "+0x00357F38,0x148,0x638";
        private const string Effect_Countdown_Crack = ModuleName + "+0x00357F38,0x148,0x648";
        private const string Effect_Climax = ModuleName + "+0x00357F38,0x148,0x898";
        
        public const string Money = ModuleName + "+0x00357F38,0x148,0x4A8";
        
        public const string Beer    = ModuleName + "+0x00357F38,0x148,0x614";
        public const string Whiskey = ModuleName + "+0x00357F38,0x148,0x618";
        public const string Weed    = ModuleName + "+0x00357F38,0x148,0x61C";
        public const string Shroom  = ModuleName + "+0x00357F38,0x148,0x620";
        public const string Peyote  = ModuleName + "+0x00357F38,0x148,0x624";
        public const string Frog    = ModuleName + "+0x00357F38,0x148,0x628";
        public const string Crack   = ModuleName + "+0x00357F38,0x148,0x62C";
        
        public const string XAxis   = ModuleName + "+0x00357F38,0x148,0x84C";
        public const string YAxis   = ModuleName + "+0x00357F38,0x148,0x850";
        public const string ZAxis   = ModuleName + "+0x00357F38,0x148,0x854";
        
        //人物朝向的角度
        private const string DegreePersonFront  = ModuleName + "+0x00357F38,0x148,0x858";
        //鼠标上下移动的角度
        private const string DegreeMouseUpDown = ModuleName + "+0x00381E0C,0xBC,0x3BC";
        //鼠标左右移动的角度
        private const string DegreeMouseLeftRight = ModuleName + "+0x00381E0C,0xBC,0x3C4";
        
        //灯光
        private const string LampLight = ModuleName + "+0x00483640,0x48";
        
        //环境亮度
        private const string Brightness_Ground_Green2 = ModuleName + "+0x???,0x??";
        private const string Brightness_Ground_Purper2 = ModuleName + "+0x???,0x??";
        private const string Brightness_Ground_Yellow2 = ModuleName + "+0x???,0x??";
        private const string Brightness_Ground_Green = ModuleName + "+0x???,0x??";
        private const string Brightness_Ground_Purper = ModuleName + "+0x???,0x??";
        private const string Brightness_Ground_Yellow = ModuleName + "+0x???,0x??";


        public const string StrAbout = TRainerHelper.StrAbout;

        public const string StrBrightness = TRainerHelper.StrBrightness;

        //Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林)
        public static readonly float[] CoordinateMissionaryBeach2FirmWoodForest = TRainerHelper.CoordinateMissionaryBeach2FirmWoodForest;

        //Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地)
        public static readonly float[] CoordinateMissionaryBeach2GabachoHeights = TRainerHelper.CoordinateMissionaryBeach2GabachoHeights;

        //Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩)
        public static readonly float[] CoordinateFirmWoodForest2MissionaryBeach = TRainerHelper.CoordinateFirmWoodForest2MissionaryBeach;

        //Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园)
        public static readonly float[] CoordinateFirmWoodForest2HomelandTrailerPark = TRainerHelper.CoordinateFirmWoodForest2HomelandTrailerPark;

        //Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林)
        public static readonly float[] CoordinateHomelandTrailerPark2FirmWoodForest = TRainerHelper.CoordinateHomelandTrailerPark2FirmWoodForest;

        //Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩)
        public static readonly float[] CoordinateGabachoHeights2MissionaryBeach = TRainerHelper.CoordinateGabachoHeights2MissionaryBeach;

        //Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)
        public static readonly float[] CoordinateGabachoHeights2HavajoIndianReservation = TRainerHelper.CoordinateGabachoHeights2HavajoIndianReservation;

        //Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山)
        public static readonly float[] CoordinateGabachoHeights2NobbingHill = TRainerHelper.CoordinateGabachoHeights2NobbingHill;

        //Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地)
        public static readonly float[] CoordinateHavajoIndianReservation2GabachoHeights = TRainerHelper.CoordinateHavajoIndianReservation2GabachoHeights;

        //Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心)
        public static readonly float[] CoordinateNobbingHill2DownTown = TRainerHelper.CoordinateNobbingHill2DownTown;

        //Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地)
        public static readonly float[] CoordinateNobbingHill2GabachoHeights = TRainerHelper.CoordinateNobbingHill2GabachoHeights;

        //Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽)
        public static readonly float[] CoordinateNobbingHill2MushroomMarsh = TRainerHelper.CoordinateNobbingHill2MushroomMarsh;

        //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan)
        public static readonly float[] CoordinateMushroomMarsh_Satan = TRainerHelper.CoordinateMushroomMarsh_Satan;

        //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife)
        public static readonly float[] CoordinateMushroomMarsh_SatanWife = TRainerHelper.CoordinateMushroomMarsh_SatanWife;

        //Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山)
        public static readonly float[] CoordinateMushroomMarsh2NobbingHill = TRainerHelper.CoordinateMushroomMarsh2NobbingHill;

        //Map8 大富翁RonJ
        public static readonly float[] CoordinateDowntown_RonJEntrance = TRainerHelper.CoordinateDowntown_RonJEntrance;

        //Map8 天使
        public static readonly float[] CoordinateDowntown_Angle = TRainerHelper.CoordinateDowntown_Angle;

        //Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点
        public static readonly float[] CoordinateDownTown2ManIsland = TRainerHelper.CoordinateDownTown2ManIsland;

        //Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点
        public static readonly float[] CoordinateDownTown2NobbingHill = TRainerHelper.CoordinateDownTown2NobbingHill;

        //Map8(DownTown 市中心)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点
        public static readonly float[] CoordinateDownTown2HomelandTrailerPark = TRainerEditionXDGameHelper.CoordinateDownTown2HomelandTrailerPark;

        //Map9(Man Island 曼岛)→高塔入口(Man Needle)
        public static readonly float[] CoordinateManIsland_ManNeedle = TRainerHelper.CoordinateManIsland_ManNeedle;

        //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
        public static readonly float[] CoordinateManIsland2DownTown = TRainerHelper.CoordinateManIsland2DownTown;


        /// <summary>
        ///                             大晚上亮度   傍晚亮度   大白天亮度
        /// 下方参数没搞, CE上修改了也没效果, 不知道啥原因???
        /// </summary>
        private static readonly long[] LampBrightness = TRainerHelper.LampBrightness;
        private static readonly long[] Ground_Green2 = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Purper2 = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Yellow2 = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Green = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Purper = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Yellow = { 0x00000000, 0x00000000, 0x00000000 };

        //地图🗺️名称
        private static readonly string Map1_Name = TRainerHelper.Map1_Name;
        private static readonly string Map2_Name = TRainerHelper.Map2_Name;
        private static readonly string Map3_Name = TRainerHelper.Map3_Name;
        private static readonly string Map4_Name = TRainerHelper.Map4_Name;
        private static readonly string Map5_Name = TRainerHelper.Map5_Name;
        private static readonly string Map6_Name = TRainerHelper.Map6_Name;
        private static readonly string Map7_Name = TRainerHelper.Map7_Name;
        private static readonly string Map8_Name = TRainerHelper.Map8_Name;
        private static readonly string Map9_Name = TRainerHelper.Map9_Name;



        //是否播放Ang
        public static bool IsPlayAng = true;


        /// <summary>
        /// 获取当前是第几个Map
        /// </summary>
        /// <returns>-1, [1, 9]</returns>
        public static int GetMapPosition() {
            string mapName = MemoryDllUtils.ReadString(Map_Name, 37);
            if (string.IsNullOrEmpty(mapName)) return -1;
            if (mapName.Equals(Map1_Name, StringComparison.OrdinalIgnoreCase)) return 1;
            if (mapName.Equals(Map2_Name, StringComparison.OrdinalIgnoreCase)) return 2;
            if (mapName.Equals(Map3_Name, StringComparison.OrdinalIgnoreCase)) return 3;
            if (mapName.Equals(Map4_Name, StringComparison.OrdinalIgnoreCase)) return 4;
            if (mapName.Equals(Map5_Name, StringComparison.OrdinalIgnoreCase)) return 5;
            if (mapName.Equals(Map6_Name, StringComparison.OrdinalIgnoreCase)) return 6;
            if (mapName.Equals(Map7_Name, StringComparison.OrdinalIgnoreCase)) return 7;
            if (mapName.Equals(Map8_Name, StringComparison.OrdinalIgnoreCase)) return 8;
            if (mapName.Equals(Map9_Name, StringComparison.OrdinalIgnoreCase)) return 9;
            return -1;
        }

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
                Console.WriteLine("小块大麻Nug🍃+100失败!");
            }
        }

        public static void ShroomAdd() {
            int shroom = MemoryDllUtils.ReadInt(Shroom);
            bool isSuccess = MemoryDllUtils.WriteInt(Shroom, shroom + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("迷幻蘑菇🍄Shroom+100失败!");
            }
        }

        public static void PeyoteAdd() {
            int peyote = MemoryDllUtils.ReadInt(Peyote);
            bool isSuccess = MemoryDllUtils.WriteInt(Peyote, peyote + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("乌羽玉仙人掌的干燥茎块(Peyote Button)🌵+100失败!");
            }
        }

        public static void FrogAdd() {
            int frog = MemoryDllUtils.ReadInt(Frog);
            bool isSuccess = MemoryDllUtils.WriteInt(Frog, frog + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("蟾蜍Toad🐸+100失败!");
            }
        }

        public static void CrackAdd() {
            int crack = MemoryDllUtils.ReadInt(Crack);
            bool isSuccess = MemoryDllUtils.WriteInt(Crack, crack + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("可卡因块(Rock)+100失败!");
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
            bool isSuccess = MemoryDllUtils.WriteInt(Balls_Sexy_Count, 10);
            if (!isSuccess) {
                Console.WriteLine("jj性感度(Balls_Sexy_Count)加到最大(10)失败!");
                return;
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
        /// 获取跳高效果
        /// </summary>
        public static float GetHighJump() {
            return MemoryDllUtils.ReadFloat(Effect_Countdown_Weed);
        }

        /// <summary>
        /// 设置跳高效果
        /// </summary>
        /// <param name="height">高度[0~1]</param>
        /// <param name="isFreezeHighJump"></param>
        /// <param name="isPlayAng"></param>
        public static void SetHighJump(float height, bool isFreezeHighJump, bool isPlayAng) {
            if (height > 1) {
                height = 1;
            } else if (height < 0) {
                height = 0;
            }
            if (isFreezeHighJump) {
                bool isSuccess = MemoryDllUtils.FreezeValue(Effect_Countdown_Weed, "float", height);
                if (isSuccess) {
                    if (isPlayAng) PlayAng();
                } else {
                    Console.WriteLine("设置跳高效果 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Effect_Countdown_Weed);
                MemoryDllUtils.WriteFloat(Effect_Countdown_Weed, height);
            }
        }

        /// <summary>
        /// 获取快跑效果
        /// </summary>
        public static float GetFastRun() {
            return MemoryDllUtils.ReadFloat(Effect_Countdown_Crack);
        }

        /// <summary>
        /// 设置快跑效果
        /// </summary>
        /// <param name="speed">速度[0~1]</param>
        /// <param name="isFreezeFastRun"></param>
        /// <param name="isPlayAng"></param>
        public static void SetFastRun(float speed, bool isFreezeFastRun, bool isPlayAng) {
            if (speed > 1) {
                speed = 1;
            } else if (speed < 0) {
                speed = 0;
            }
            if (isFreezeFastRun) {
                bool isSuccess = MemoryDllUtils.FreezeValue(Effect_Countdown_Crack, "float", speed);
                if (isSuccess) {
                    if (isPlayAng) PlayAng();
                } else {
                    Console.WriteLine("设置快跑效果 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Effect_Countdown_Crack);
                MemoryDllUtils.WriteFloat(Effect_Countdown_Crack, speed);
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
        /// 冻结潜水
        /// </summary>
        /// <param name="isFreezeDiving"></param>
        public static void FreezeDiving(bool isFreezeDiving) {
            if (isFreezeDiving) {
                bool isSuccess0 = MemoryDllUtils.FreezeValue(Drown_ProgressBar, "long", 0);
                bool isSuccess1 = MemoryDllUtils.FreezeValue(Is_Drown_Deadable, "byte", 0);
                bool isSuccess2 = MemoryDllUtils.FreezeValue(Is_Drown_Byte, "byte", 0);
                if (isSuccess0 && isSuccess1 && isSuccess2) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结潜水 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Drown_ProgressBar);
                MemoryDllUtils.UnfreezeValue(Is_Drown_Deadable);
                MemoryDllUtils.UnfreezeValue(Is_Drown_Byte);
                bool isSuccess0 = MemoryDllUtils.WriteByte(Drown_ProgressBar, 0);
                bool isSuccess1 = MemoryDllUtils.WriteByte(Is_Drown_Deadable, 0);   //重置, 否则可能会一下水就死
                bool isSuccess2 = MemoryDllUtils.WriteByte(Is_Drown_Byte, 0);
                if (isSuccess0 && isSuccess1 && isSuccess2) {
                    // PlayAng();
                } else {
                    Console.WriteLine("取消冻结 & 重置潜水 失败!");
                }
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
            y = isRight ? y - (float)Math.Sin(degreeMouseLeftRight) * value : y + (float)Math.Sin(degreeMouseLeftRight) * value;
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
        /// <param name="position">第几个亮度[0~2]</param>
        /// <param name="isFreeze">是否冻结值</param>
        public static void BrightnessSet(int position, bool isFreeze, bool isPlayAng) {
            //TODO: 没有地址, 先屏蔽
        }


        /// <summary>
        /// 瞬移到坐标
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="failureStr"></param>
        public static bool Teleport(float[] coordinate, string failureStr) {
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, coordinate[0]);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, coordinate[1]);
            bool isSuccessZ = MemoryDllUtils.WriteFloat(ZAxis, coordinate[2]);
            if (isSuccessX && isSuccessY && isSuccessZ) {
                PlayAng();
                return true;
            }
            Console.WriteLine(failureStr);
            return false;
        }


        //播放ang
        public static void PlayAng() {
            if (IsPlayAng) TRainerHelper.PlayAng();
        }

        //播放click
        public static void PlayClick() {
            TRainerHelper.PlayClick();
        }

        //播放activate
        public static void PlayActivate(bool isTRainerOpen) {
            TRainerHelper.PlayActivate(isTRainerOpen);
        }

        
        
        /// <summary>
        /// 设置人物朝向 (从N开始, N右侧最小, N左侧最大)
        /// </summary>
        /// <param name="degree">弧度: (0 ~ 2π), 不是角度: (0° ~ 360°)</param>
        /// <returns></returns>
        public static bool SetDegreePersonFront(float degree) {
            return MemoryDllUtils.WriteFloat(DegreePersonFront, degree);
        }

        /// <summary>
        /// 获取鼠标左右旋转角度 (从N开始, N右侧最小, N左侧最大)
        /// </summary>
        /// <returns>返回弧度: (0 ~ 2π), 不是角度: (0° ~ 360°)</returns>
        private static float GetDegreeMouseLeftRight() {
            return MemoryDllUtils.ReadFloat(DegreeMouseLeftRight);
        }
        
        /// <summary>
        /// 设置鼠标左右旋转角度 (从N开始, N右侧最小, N左侧最大)
        /// </summary>
        /// <param name="degree">弧度: (0 ~ 2π), 不是角度: (0° ~ 360°)</param>
        /// <returns></returns>
        public static bool SetDegreeMouseLeftRight(float degree) {
            return MemoryDllUtils.WriteFloat(DegreeMouseLeftRight, degree);
        }
        
        /// <summary>
        /// 设置鼠标上下旋转角度 (弧度:±π/2.136, 角度:±84.27°), 为了防止视角卡死(万向节锁)而设定的常见安全限制)
        /// </summary>
        /// <param name="degree">弧度: (-π/2.136 ~ π/2.136), 不是角度: (-84.27° ~ 84.27°)</param>
        /// <returns></returns>
        public static bool SetDegreeMouseUpDown(float degree) {
            return MemoryDllUtils.WriteFloat(DegreeMouseUpDown, degree);
        }
        
        
        /// <summary>
        /// 获取开关图片
        /// </summary>
        /// <param name="isOn">是否打开</param>
        /// <returns></returns>
        public static Uri GetSwitchUri(bool isOn) {
            return TRainerHelper.GetSwitchUri(isOn);
        }
    }
}