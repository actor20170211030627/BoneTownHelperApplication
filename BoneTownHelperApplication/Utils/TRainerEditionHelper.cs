using System;
using Memory;

namespace BoneTownHelperApplication.Utils {
    public static class TRainerEditionHelper {
        public const string ProcessName = TRainerHelper.ProcessName;
        private const string ModuleName = TRainerHelper.ModuleName;
        
        private const string Balls_Sexy_Progress = ModuleName + "+0x00354318,0x2E0,0x474";
        private const string Balls_Sexy_Count    = ModuleName + "+0x00354318,0x2E0,0x478";

        private const string Health = ModuleName + "+0x00354318,0x2E0,0x470";
        private const string Effect_Countdown_Weed = ModuleName + "+0x00354318,0x2E0,0x5DC";
        private const string Effect_Countdown_Crack = ModuleName + "+0x00354318,0x2E0,0x5EC";
        private const string Effect_Climax = ModuleName + "+0x00354318,0x2E0,0x834";
        
        public const string Money = ModuleName + "+0x00354318,0xA4,0x54,0x47C";
        
        public const string Beer    = ModuleName + "+0x00354318,0xA4,0x54,0x5B8";
        public const string Whiskey = ModuleName + "+0x00354318,0xA4,0x54,0x5BC";
        public const string Weed    = ModuleName + "+0x00354318,0xA4,0x54,0x5C0";
        public const string Shroom  = ModuleName + "+0x00354318,0x2E0,0x5C4";
        public const string Peyote  = ModuleName + "+0x00354318,0x2E0,0x5C8";
        public const string Frog    = ModuleName + "+0x00354318,0x2E0,0x5CC";
        public const string Crack   = ModuleName + "+0x00354318,0x2E0,0x5D0";
        
        public const string XAxis   = ModuleName + "+0x00354318,0x2E0,0x7E8";
        public const string YAxis   = ModuleName + "+0x00354318,0x2E0,0x7EC";
        public const string ZAxis   = ModuleName + "+0x00354318,0x2E0,0x7F0";
        
        //人物朝向的角度
        private const string DegreePersonFront  = ModuleName + "+0x00354318,0xA4,0xE04";
        //鼠标左右移动的角度
        private const string DegreeMouseLeftRight = ModuleName + "+0x00354318,0xA4,0x3AC";
        
        //灯光
        private const string LampLight = ModuleName + "+0x???,0x??";
        
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

        //Map9(Man Island 曼岛)→高塔入口(Man Needle)
        public static readonly float[] CoordinateManIsland_ManNeedle = TRainerHelper.CoordinateManIsland_ManNeedle;

        //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
        public static readonly float[] CoordinateManIsland2DownTown = TRainerHelper.CoordinateManIsland2DownTown;


        /// <summary>
        ///                             大晚上亮度   傍晚亮度   大白天亮度
        /// 下方参数没搞, CE上修改了也没效果, 不知道啥原因???
        /// </summary>
        private static readonly long[] LampBrightness = { 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Green2 = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Purper2 = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Yellow2 = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Green = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Purper = { 0x00000000, 0x00000000, 0x00000000 };
        private static readonly long[] Ground_Yellow = { 0x00000000, 0x00000000, 0x00000000 };
        

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
            TRainerHelper.PlayAng();
        }

        //播放click
        public static void PlayClick() {
            TRainerHelper.PlayClick();
        }

        //播放activate
        public static void PlayActivate(bool isTRainerOpen) {
            TRainerHelper.PlayActivate(isTRainerOpen);
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
            return TRainerHelper.GetSwitchUri(isOn);
        }
    }
}