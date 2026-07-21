using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Helpers
{
    public class SiteHelper
    {
        public static string GetZoneShowName(string zone)
        {
            switch (zone)
            {
                case "A":
                    return "大慶總院";
                case "G":
                    return "中興院區";
                case "I":
                    return "尊榮醫療中心";
                default:
                    return "";
            }
        }

        public static string InteractionNote(int sign)
        {
            switch (sign)
            {
                case 1:
                    return "Major";
                case 2:
                    return "Moderate";
                case 3:
                    return "Minor";
                case 4:
                    return "Major";
                case 5:
                    return "Minor";
                default:
                    return string.Empty;
            }
        }

        public static string ControlDrugNote(string ctrlDrug)
        {
            switch (ctrlDrug)
            {
                case "N":
                    return "非管制藥品";
                case "1":
                    return "管制藥第1級";
                case "2":
                    return "管制藥第2級";
                case "3":
                    return "管制藥第3級";
                case "4":
                    return "管制藥第4級";
                case "A":
                    return "不需會診";
                case "B":
                    return "需會診";
                case "C":
                    return "限醫師使用";
                default:
                    return string.Empty;
            }
        }

        public static string HighAlertNote(int highAlert)
        {
            switch (highAlert)
            {
                case 0:
                    return "非高警訊藥品";
                case 1:
                    return "高警訊藥品";
                case 2:
                    return "化療注射劑";
                case 3:
                    return "兒童專用藥";
                case 4:
                    return "人體臨床試驗藥品";
                default:
                    return string.Empty;
            }
        }

        public static string Pregnancy(string pregnancy)
        {
            switch (pregnancy)
            {
                case "N":
                    return "N/A";
                case "A":
                    return "A";
                case "B":
                    return "B";
                case "C":
                    return "C";
                case "D":
                    return "D";
                case "X":
                    return "X";
                default:
                    return string.Empty;
            }
        }

        public static string DrugUsage(int usageNo)
        {
            switch (usageNo)
            {
                case 1000: return "HS"; //睡前使用一次
                case 1002: return "BIDHS"; //每日三次 早晚及睡前使用
                case 1110: return "QD"; //每日一次 早上 飯後使用
                case 8001: return "PRN"; //PRN
                case 9001: return "STAT"; //立刻使用
                case 9999: return "CONT"; //持續使用
                default: return ""; 
            }
        }

        /// <summary>
        /// Way of Drug
        /// </summary>
        public static string DrugWay(int wayNo)
        {
            switch (wayNo)
            {
                case 1010: return "AD"; //右耳
                case 1011: return "AS"; //左耳
                case 1012: return "AU"; //每耳
                case 1013: return "ET"; //氣切內
                case 1014: return "GAR"; //漱口用
                case 1015: return "HD"; //皮下灌注
                case 1016: return "ID"; //皮內注射
                case 1017: return "IA"; //動脈注射或關節注射
                case 1018: return "IE"; //脊髓硬膜內注射
                case 1019: return "IM"; //肌肉注射
                case 1020: return "IV"; //靜脈注射
                case 1021: return "IP"; //腹腔注射
                case 1022: return "ICV"; //腦室注射
                case 1023: return "IMP"; //植入
                case 1024: return "INHL"; //吸入
                case 1025: return "IS"; //滑膜內注射
                case 1026: return "IT"; //椎骨內注射
                case 1027: return "IVA"; //靜脈添加
                case 1028: return "IVD"; //靜脈點滴滴入
                case 1029: return "IVI"; //玻璃體內注射
                case 1030: return "IVP"; //靜脈注入
                case 1031: return "LA"; //局部麻醉
                case 1032: return "LI"; //局部注射
                case 1033: return "NA"; //鼻用
                case 1034: return "OD"; //右眼
                case 1035: return "OS"; //左眼
                case 1036: return "OU"; //每眼
                case 1037: return "PO"; //口服
                case 1038: return "SC"; //皮下注射
                case 1039: return "SCI"; //結膜下注射
                case 1040: return "SKIN"; //皮膚用
                case 1041: return "SL"; //舌下
                case 1042: return "SPI"; //脊髓
                case 1043: return "RECT"; //肛門用
                case 1044: return "TOPI"; //局部塗擦
                case 1045: return "TPN"; //全靜脈營養劑
                case 1046: return "VAG"; //陰道用
                case 1047: return "IRRI"; //沖洗
                case 1048: return "EXT"; //外用
                case 1049: return "XX"; //其他
                case 1050: return "IPL"; //肋膜內注射
                case 1051: return "PEIT"; //經皮酒精注射治療
                case 1052: return "ICSI"; //細胞漿內單精子注射(不孕)
                case 1053: return "AC injection"; //前房內注射
                case 1054: return "B"; //口頰用
                case 1055: return "EP"; //硬脊膜外腔注射
                default: return "";
            }
        }

        public static string GetZoneStr(string ward)
        {
            switch (ward)
            {
                case "NSR12":
                case "NSR11":
                case "NSR10":
                case "NSR9":
                    return "G";
                default:
                    return "A";
            }
        }
    }
}