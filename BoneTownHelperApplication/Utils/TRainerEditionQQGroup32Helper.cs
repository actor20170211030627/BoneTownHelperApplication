using System;

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

        private const string Clothing_Health = ModuleName + "+0x00357F38,0x148,0x4AC";

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
        
        private const string pauseDaylight = ModuleName + "+0x00687E9C,0x8";
        //环境亮度
        private const string Brightness_SkyEdge_Inner_Green_Light = ModuleName + "+0x003530A4,0x2C0";
        private const string Brightness_SkyEdge_Inner_Blue_Light = ModuleName + "+0x003530A4,0x2C4";
        private const string Brightness_SkyEdge_Inner_Yellow_Light = ModuleName + "+0x003530A4,0x2C8";
        private const string Brightness_SkyEdge_Light = ModuleName + "+0x003378FC,0x0,0x230";
        private const string Brightness_SkyEdge_Color = ModuleName + "+0x003378FC,0x0,0xD7C";
        private const string Brightness_Ground_Green2 = ModuleName + "+0x00687E9C,0x14,0x1E0";
        private const string Brightness_Ground_Purper2 = ModuleName + "+0x00687E9C,0x14,0x1E4";
        private const string Brightness_Ground_Yellow2 = ModuleName + "+0x00687E9C,0x14,0x1E8";
        private const string Brightness_Ground_Green = ModuleName + "+0x00687E9C,0x14,0x1EC";
        private const string Brightness_Ground_Purper = ModuleName + "+0x00687E9C,0x14,0x1F0";
        private const string Brightness_Ground_Yellow = ModuleName + "+0x00687E9C,0x14,0x1F4";
        private const string Brightness_Object_Light = ModuleName + "+0x00688204,0x60";


        public const string StrAbout = TRainerHelper.StrAbout;

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


        // 原来的变量 → 改成属性，赋值时自动触发同步
        private static bool _isPlayAng = true;
        //是否播放Ang
        public static bool IsPlayAng{
            get => _isPlayAng;
            set {
                _isPlayAng = value;
                TRainerHelper.IsPlayAng = value;
            }
        }


        /// <summary>
        /// 获取当前是第几个Map
        /// </summary>
        /// <returns>-1, [1, 9]</returns>
        public static int GetMapPosition() {
            return TRainerHelper.GetMapPosition(Map_Name);
        }

        public static void MoneyAdd() {
            TRainerHelper.MoneyAdd(Money);
        }

        public static void BeerAdd() {
            TRainerHelper.BeerAdd(Beer);
        }

        public static void WhiskeyAdd() {
            TRainerHelper.WhiskeyAdd(Whiskey);
        }

        public static void NugAdd() {
            TRainerHelper.NugAdd(Weed);
        }

        public static void ShroomAdd() {
            TRainerHelper.ShroomAdd(Shroom);
        }

        public static void PeyoteAdd() {
            TRainerHelper.PeyoteAdd(Peyote);
        }

        public static void FrogAdd() {
            TRainerHelper.FrogAdd(Frog);
        }

        public static void CrackAdd() {
            TRainerHelper.CrackAdd(Crack);
        }


        public static void ZAxisEdit(bool isUp, int value) {
            TRainerHelper.ZAxisEdit(isUp, value, ZAxis);
        }

        //jj性感度加到最大
        public static void BallsAdd210() {
            TRainerHelper.BallsAdd210(Balls_Sexy_Count, Balls_Sexy_Progress);
        }

        /// <summary>
        /// 是否冻结无限健康
        /// </summary>
        /// <param name="isFreezeHealth"></param>
        public static void FreezeHealth(bool isFreezeHealth) {
            TRainerHelper.FreezeHealth(isFreezeHealth, Health);
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
            TRainerHelper.SetHighJump(height, isFreezeHighJump, isPlayAng, Effect_Countdown_Weed);
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
            TRainerHelper.SetFastRun(speed, isFreezeFastRun, isPlayAng, Effect_Countdown_Crack);
        }

        /// <summary>
        /// 获取 Clothing_Health
        /// </summary>
        /// <returns></returns>
        public static int GetClothing_Health() {
            return MemoryDllUtils.ReadInt(Clothing_Health);
        }
        
        /// <summary>
        /// 写入 Clothing_Health, [-1, int.max]
        /// </summary>
        /// <param name="value"></param>
        public static bool SetClothing_Health(int value) {
            return MemoryDllUtils.WriteInt(Clothing_Health, value);
        }

        /// <summary>
        /// 冻结快感进度
        /// </summary>
        /// <param name="isFreezeClimax"></param>
        public static void FreezeClimax(bool isFreezeClimax) {
            TRainerHelper.FreezeClimax(isFreezeClimax, Effect_Climax);
        }

        /// <summary>
        /// 冻结潜水
        /// </summary>
        /// <param name="isFreezeDiving"></param>
        public static void FreezeDiving(bool isFreezeDiving) {
            TRainerHelper.FreezeDiving(isFreezeDiving, Drown_ProgressBar, Is_Drown_Deadable, Is_Drown_Byte);
        }


        /// <summary>
        /// 人物向左平移 or 向右平移
        /// </summary>
        /// <param name="isRight"></param>
        /// <param name="value"></param>
        public static void GoRightOrLeft(bool isRight, int value) {
            TRainerHelper.GoRightOrLeft(isRight, value, XAxis, YAxis, DegreeMouseLeftRight, DegreePersonFront);
        }

        /// <summary>
        /// 人物向前平移 or 往后退
        /// </summary>
        /// <param name="isFront"></param>
        /// <param name="value"></param>
        public static void GoFrontOrBack(bool isFront, int value) {
            TRainerHelper.GoFrontOrBack(isFront, value, XAxis, YAxis, DegreeMouseLeftRight, DegreePersonFront);
        }

        /// <summary>
        /// 中止日/夜循环
        /// </summary>
        /// <param name="isPause">是否 中止日/夜循环</param>
        /// <param name="isFreeze">是否冻结</param>
        public static void PauseDaylight(bool isPause, bool isFreeze, bool isPlayAng) {
            TRainerHelper.PauseDaylight(isPause, isFreeze, isPlayAng, pauseDaylight);
        }

        /// <summary>
        /// 灯光设置
        /// </summary>
        /// <param name="isOpen">是否打开</param>
        public static void LampLightSet(bool isOpen, bool isPlayAng) {
            TRainerHelper.LampLightSet(isOpen, isPlayAng, LampLight);
        }


        /// <summary>
        /// 环境亮度设置
        /// </summary>
        /// <param name="position">第几个亮度[0~5]</param>
        public static void BrightnessSet(int position, bool isPlayAng) {
            TRainerHelper.BrightnessSet(position, isPlayAng,
                Brightness_SkyEdge_Inner_Green_Light, Brightness_SkyEdge_Inner_Blue_Light, Brightness_SkyEdge_Inner_Yellow_Light,
                Brightness_SkyEdge_Light, Brightness_SkyEdge_Color,
                Brightness_Ground_Green2, Brightness_Ground_Purper2, Brightness_Ground_Yellow2,
                Brightness_Ground_Green, Brightness_Ground_Purper, Brightness_Ground_Yellow,
                Brightness_Object_Light);
        }


        /// <summary>
        /// 瞬移到坐标
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="failureStr"></param>
        public static bool Teleport(float[] coordinate, string failureStr) {
            return TRainerHelper.Teleport(coordinate, failureStr, XAxis, YAxis, ZAxis);
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
            return TRainerHelper.SetDegreePersonFront(degree, DegreePersonFront);
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