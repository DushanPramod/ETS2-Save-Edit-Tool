using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.HelpDataClasses
{
    [Serializable]
    public class TruckDetail
    {
        public static Dictionary<string, string> seriesToNameDictionary = new Dictionary<string, string>()
        {
            {"daf.xf", "DAF XF"},
            {"daf.xf_euro6", "DAF XF EURO 6"},
            {"iveco.hiway", "IVECO Hiway"},
            {"iveco.stralis", "IVECO Stralis"},
            {"man.tgx", "MAN TGX"},
            {"man.tgx_euro6", "MAN TGX EURO 6"},
            {"mercedes.actros", "Mercedes Actros"},
            {"mercedes.actros2014", "Mercedes Actros 2014"},
            {"renault.magnum", "Renault Magnum"},
            {"renault.premium", "Renault Premium"},
            {"renault.t", "Renault T"},
            {"scania.r", "Scania R"},
            {"scania.r_2016", "Scania R 2016"},
            {"scania.s_2016", "Scania S 2016"},
            {"scania.streamline", "Scania Streamline"},
            {"volvo.fh16", "Volvo FH16"},
            {"volvo.fh16_2012", "Volvo FH16 2012"}
        };

        public static Dictionary<string, string> nameToSeriesDictionary =
            seriesToNameDictionary.ToDictionary((i) => i.Value, (i) => i.Key);

        public static Dictionary<string, Dictionary<string, string>> seresToEngineDictionary =
            new Dictionary<string, Dictionary<string, string>>()
            {
                {"daf.xf", new Dictionary<string, string> {
                    {"PACCAR MX265", "mx265"},
                    {"PACCAR MX300", "mx300"},
                    {"PACCAR MX340", "mx340"},
                    {"PACCAR MX375", "mx375"}
                }},
                {"daf.xf_euro6", new Dictionary<string, string> {
                    {"MX-11 270 Euro 6, 2017", "mx11_270"},
                    {"MX-11 320 Euro 6", "mx11_320"},
                    {"MX-11 330 Euro 6, 2017", "mx11_330"},
                    {"MX-13 303 Euro 6", "mx13_303"},
                    {"MX-13 340 Euro 6", "mx13_340"},
                    {"MX-13 355 Euro 6, 2017", "mx13_355"},
                    {"MX-13 375 Euro 6", "mx13_375"},
                    {"MX-13 390 Euro 6, 2017", "mx13_390"}
                }},
                {"iveco.hiway", new Dictionary<string, string> {
                    {"Cursor 9 310", "cursor9_310hp"},
                    {"Cursor 9 330", "cursor9_330hp"},
                    {"Cursor 9 360", "cursor9_360hp"},
                    {"Cursor 9 400", "cursor9_400hp"},
                    {"Cursor 11 420", "cursor11_420hp"},
                    {"Cursor 11 460", "cursor11_460hp"},
                    {"Cursor 13 500", "cursor13_500hp"},
                    {"Cursor 13 560", "cursor13_560hp"}
                }},
                {"iveco.stralis", new Dictionary<string, string> {
                    {"Cursor 8 310", "cursor8_310hp"},
                    {"Cursor 8 330", "cursor8_330hp"},
                    {"Cursor 8 360", "cursor8_360hp"},
                    {"Cursor 10 420", "cursor10_420hp"},
                    {"Cursor 10 450", "cursor10_450hp"},
                    {"Cursor 13 500", "cursor13_500hp"},
                    {"Cursor 13 560", "cursor13_560hp"}
                }},
                {"man.tgx", new Dictionary<string, string> {
                    {"D2066 235", "d2066_235"},
                    {"D2066 265", "d2066_265"},
                    {"D2066 294", "d2066_294"},
                    {"D2676 324", "d2676_324"},
                    {"D2676 353", "d2676_353"},
                    {"D2676 397", "d2676_397"},
                    {"D2868 500", "d2868_500"}
                }},
                {"man.tgx_euro6", new Dictionary<string, string> {
                    {"D2066 LF67 265 Euro 6", "d2066_265"},
                    {"D2066 LF61 294 Euro 6", "d2066_294"},
                    {"D2676 LF26 324 Euro 6", "d2676_324"},
                    {"D2676 LF25 353 Euro 6", "d2676_353"},
                    {"D3876 LF02 382 Euro 6", "d3876_382"},
                    {"D3876 LF01 412 Euro 6", "d3876_412"},
                    {"D3876 LF03 471 Euro 6", "d3876_471"}
                }},
                {"mercedes.actros", new Dictionary<string, string> {
                    {"OM 541 V6 BlueTec5 235", "1832ls"},
                    {"OM 541 V6 BlueTec5 265", "1836ls"},
                    {"OM 541 V6 BlueTec5 300", "1841ls"},
                    {"OM 541 V6 BlueTec5 320", "1844ls"},
                    {"OM 541 V6 BlueTec5 335", "1846ls"},
                    {"OM 541 V6 BlueTec5 350", "1848ls"},
                    {"OM 542 V8 BlueTec5 375", "1851ls"},
                    {"OM 542 V8 BlueTec5 405", "1855ls"},
                    {"OM 542 V8 BlueTec5 440", "1860ls"}
                }},
                {"mercedes.actros2014", new Dictionary<string, string> {
                    {"OM 471 Euro VI 310", "engine_1842"},
                    {"OM 471 Euro VI 330", "engine_1845"},
                    {"OM 471 Euro VI 350", "engine_1848"},
                    {"OM 471 Euro VI 375", "engine_1851"},
                    {"OM 473 Euro VI 380", "engine_1852"},
                    {"OM 473 Euro VI 425", "engine_1858"},
                    {"OM 473 Euro VI 460", "engine_1863"}
                }},
                {"renault.magnum", new Dictionary<string, string> {
                    {"440DXi Euro5", "dxi13_440"},
                    {"480DXi Euro5", "dxi13_480"},
                    {"520DXi Euro5", "dxi13_520"}
                }},
                {"renault.premium", new Dictionary<string, string> {
                    {"380DXi Euro5", "dxi11_380"},
                    {"430DXi Euro5", "dxi11_430"},
                    {"460DXi Euro5", "dxi11_460"}
                }},
                {"renault.t", new Dictionary<string, string> {
                    {"DTI 11 380 Euro 6", "dti11_380"},
                    {"DTI 11 430 Euro 6", "dti11_430"},
                    {"DTI 11 460 Euro 6", "dti11_460"},
                    {"DTI 13 440 Euro 6", "dti13_440"},
                    {"DTI 13 480 Euro 6", "dti13_480"},
                    {"DTI 13 520 Euro 6", "dti13_520"}
                }},
                {"scania.r", new Dictionary<string, string> {
                    {"DC12 18 380 Euro 5", "dc12_380"},
                    {"DC12 15 420 Euro 5", "dc12_420"},
                    {"DC13 06 360 Euro 5", "dc13_360"},
                    {"DC13 05 400 Euro 5", "dc13_400"},
                    {"DC13 10 440 Euro 5", "dc13_440"},
                    {"DC13 109 440 Euro 6", "dc13_440_2"},
                    {"DC13 07 480 Euro 5", "dc13_480"},
                    {"DC13 110 480 Euro 6", "dc13_480_2"},
                    {"DC16 19 500 Euro 5 V8", "dc16_500"},
                    {"DC16 18 560 Euro 5  V8", "dc16_560"},
                    {"DC16 17 620 Euro 5  V8", "dc16_620"},
                    {"DC16 21 730 Euro 5 EEV  V8", "dc16_730"}
                }},
                {"scania.r_2016", new Dictionary<string, string> {
                    {"DC13 149 370 Euro 6", "dc13_370"},
                    {"DC13 141 410 Euro 6", "dc13_410"},
                    {"DC13 148 450 Euro 6", "dc13_450"},
                    {"DC13 155 500 Euro 6", "dc13_500"},
                    {"DC16 116 520 Euro 6 V8", "dc16_520"},
                    {"DC16 117 580 Euro 6 V8", "dc16_580"},
                    {"DC16 118 650 Euro 6 V8", "dc16_650"},
                    {"DC16 107 730 Euro 6 V8", "dc16_730"}
                }},
                {"scania.s_2016", new Dictionary<string, string> {
                    {"DC13 149 370 Euro 6", "dc13_370"},
                    {"DC13 141 410 Euro 6", "dc13_410"},
                    {"DC13 148 450 Euro 6", "dc13_450"},
                    {"DC13 155 500 Euro 6", "dc13_500"},
                    {"DC16 116 520 Euro 6 V8", "dc16_520"},
                    {"DC16 117 580 Euro 6 V8", "dc16_580"},
                    {"DC16 118 650 Euro 6 V8", "dc16_650"},
                    {"DC16 107 730 Euro 6 V8", "dc16_730"}
                }},
                {"scania.streamline", new Dictionary<string, string> {
                    {"DC12 18 380 Euro 5", "dc12_380"},
                    {"DC12 15 420 Euro 5", "dc12_420"},
                    {"DC13 114 360 Euro 5", "dc13_360"},
                    {"DC13 116 370 Euro 6", "dc13_370"},
                    {"DC13 113 400 Euro 5", "dc13_400"},
                    {"DC13 115 410 Euro 6", "dc13_410"},
                    {"DC13 112 440 Euro 5", "dc13_440"},
                    {"DC13 109 440 Euro 6", "dc13_440_2"},
                    {"DC13 124 450 Euro 6", "dc13_450"},
                    {"DC13 111 480 Euro 5", "dc13_480"},
                    {"DC13 110 480 Euro 6", "dc13_480_2"},
                    {"DC13 125 490 Euro 6", "dc13_490"},
                    {"DC16 19 500 Euro 5 V8", "dc16_500"},
                    {"DC16 101 520 Euro 6 V8", "dc16_520"},
                    {"DC16 18 560 Euro 5 V8", "dc16_560"},
                    {"DC16 102 580 Euro 6 V8", "dc16_580"},
                    {"DC16 17 620 Euro 5 V8", "dc16_620"},
                    {"DC16 21 730 Euro 5 EEV V8", "dc16_730"},
                    {"DC16 103 730 Euro 6 V8", "dc16_730_2"}
                }},
                {"volvo.fh16", new Dictionary<string, string> {
                    {"D13C420 Euro 5 EEV", "d13c420"},
                    {"D13C460 Euro 5 EEV", "d13c460"},
                    {"D13C500 Euro 5 EEV", "d13c500"},
                    {"D13C540 Euro 5 EEV", "d13c540"},
                    {"D16G540 Euro 5 EEV", "d16g540"},
                    {"D16G600 Euro 5 EEV", "d16g600"},
                    {"D16G700 Euro 5 EEV", "d16g700"},
                    {"D16G750 Euro 5 EEV", "d16g750"}
                }},
                {"volvo.fh16_2012", new Dictionary<string, string> {
                    {"D13C420 Euro 5 EEV", "d13c420"},
                    {"D13C460 Euro 5 EEV", "d13c460"},
                    {"D13C500 Euro 5 EEV", "d13c500"},
                    {"D13C540 Euro 5 EEV", "d13c540"},
                    {"D13K460 Euro 6", "d13k460"},
                    {"D16G540 Euro 5 EEV", "d16g540"},
                    {"D16G600 Euro 5 EEV", "d16g600"},
                    {"D16G700 Euro 5 EEV", "d16g700"},
                    {"D16G750 Euro 5 EEV", "d16g750"}
                }},
            };

        public static Dictionary<string, Dictionary<string, string>> seresToTransmissionDictionary =
            new Dictionary<string, Dictionary<string, string>>()
            {
                {"daf.xf", new Dictionary<string, string>
                    {
                        {"ZF 12AS2330TD", "12_speed"},
                        {"ZF 12AS2530TO", "12_speed_over"},
                        {"ZF 12AS2331TD R", "12_speed_ret"},
                        {"ZF 12AS2531TO R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },

                {"daf.xf_euro6", new Dictionary<string, string>
                    {
                        {"ZF 12AS2330TD", "12_speed"},
                        {"ZF 12AS2530TO", "12_speed_over"},
                        {"ZF 12AS2331TD R", "12_speed_ret"},
                        {"ZF 12AS2531TO R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 12TX2420TD", "zf_12tx2420td"},
                        {"ZF 12TX2421TD R", "zf_12tx2421td"},
                        {"ZF 12TX2610TO", "zf_12tx2610to"},
                        {"ZF 12TX2611TO R", "zf_12tx2611to"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"},
                        {"ZF 16TX2640TO", "zf_16tx2640to"},
                        {"ZF 16TX2641TO R", "zf_16tx2641to"}
                    }
                },
                {"iveco.hiway", new Dictionary<string, string>
                    {
                        {"ZF 12AS2330TD", "12_speed"},
                        {"ZF 12AS2530TO", "12_speed_over"},
                        {"ZF 12AS2331TD R", "12_speed_r"},
                        {"ZF 12AS2531TO R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },
                {"iveco.stralis", new Dictionary<string, string>
                    {
                        {"ZF 12AS2330TD", "12_speed"},
                        {"ZF 12AS2530TO", "12_speed_over"},
                        {"ZF 12AS2331TD R", "12_speed_ret"},
                        {"ZF 12AS2531TO R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },
                {"man.tgx", new Dictionary<string, string>
                    {
                        {"ZF 12AS2330TD", "12_speed"},
                        {"ZF 12AS3140TO", "12_speed_over"},
                        {"ZF 12AS2331TD R", "12_speed_ret"},
                        {"ZF 12AS3141TO R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },
                {"man.tgx_euro6", new Dictionary<string, string>
                    {
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"TipMatic 12AS2330 DD", "zf_12as2330td"},
                        {"TipMatic 12AS2331 DD R", "zf_12as2331td"},
                        {"TipMatic 12AS3140 OD", "zf_12as3140to"},
                        {"TipMatic 12AS3141 OD R", "zf_12as3141to"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },
                {"mercedes.actros", new Dictionary<string, string>
                    {
                        {"PowerShift G281-12", "12_speed"},
                        {"PowerShift G330-12", "12_speed_over"},
                        {"PowerShift G281-12 R", "12_speed_ret"},
                        {"PowerShift G330-12 R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"PowerShift G280-16", "g280_16"},
                        {"PowerShift G280-16 R", "g280_16_r"}
                    }
                },
                {"mercedes.actros2014", new Dictionary<string, string>
                    {
                        {"PowerShift G281-12", "12_speed"},
                        {"PowerShift G330-12", "12_speed_over"},
                        {"PowerShift G281-12 R", "12_speed_ret"},
                        {"PowerShift G330-12 R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"PowerShift G280-16", "g280_16"},
                        {"PowerShift G280-16 R", "g280_16_r"}
                    }
                },
                {"renault.magnum", new Dictionary<string, string>
                    {
                        {"Optidriver AT2412D", "12_speed"},
                        {"Optidriver ATO2612D", "12_speed_over"},
                        {"Optidriver AT2412D R", "12_speed_ret"},
                        {"Optidriver ATO2612D R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },
                {"renault.premium", new Dictionary<string, string>
                    {
                        {"Optidriver AT2412D", "12_speed"},
                        {"Optidriver ATO2612D", "12_speed_over"},
                        {"Optidriver AT2412D R", "12_speed_ret"},
                        {"Optidriver ATO2612D R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"ZF 16AS2630TO", "zf_16as2630to"},
                        {"ZF 16AS2631TO R", "zf_16as2631to"}
                    }
                },
                {"renault.t", new Dictionary<string, string>
                    {
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"Optidriver AT2612F", "at2612f"},
                        {"Optidriver AT2612F R", "at2612fr"},
                        {"Optidriver ATO2612F", "ato2612f"},
                        {"Optidriver ATO2612F R", "ato2612fr"},
                        {"Optidriver Xtended ATO2614F", "ato2614f"},
                        {"Optidriver Xtended ATO2614F R", "ato2614fr"}
                    }
                },
                {"scania.r", new Dictionary<string, string>
                    {
                        {"Opticruise GRS 905", "12_speed"},
                        {"Opticruise GRSO 905", "12_speed_over"},
                        {"Opticruise GRS 905R", "12_speed_ret"},
                        {"Opticruise GRSO 905R", "12_speed_ret_over"},
                        {"Scania GA867", "allison"},
                        {"Scania GA867 R", "allison_retarder"},
                        {"Opticruise GRSO 925", "grso925"},
                        {"Opticruise GRSO 925R", "grso925_r"}
                    }
                },
                {"scania.r_2016", new Dictionary<string, string>
                    {
                        {"Scania GA867", "allison"},
                        {"Scania GA867 R", "allison_retarder"},
                        {"Opticruise GRS 905", "grs905"},
                        {"Opticruise GRS 905R", "grs905_r"},
                        {"Opticruise GRSO 905", "grso905"},
                        {"Opticruise GRSO 905R", "grso905_r"},
                        {"Opticruise GRSO 925", "grso925"},
                        {"Opticruise GRSO 925R", "grso925_r"}
                    }
                },
                {"scania.s_2016", new Dictionary<string, string>
                    {
                        {"Scania GA867", "allison"},
                        {"Scania GA867 R", "allison_retarder"},
                        {"Opticruise GRS 905", "grs905"},
                        {"Opticruise GRS 905R", "grs905_r"},
                        {"Opticruise GRSO 905", "grso905"},
                        {"Opticruise GRSO 905R", "grso905_r"},
                        {"Opticruise GRSO 925", "grso925"},
                        {"Opticruise GRSO 925R", "grso925_r"}
                    }
                },
                {"scania.streamline", new Dictionary<string, string>
                    {
                        {"Opticruise GRS 905", "12_speed"},
                        {"Opticruise GRSO 905", "12_speed_over"},
                        {"Opticruise GRS 905R", "12_speed_ret"},
                        {"Opticruise GRSO 905R", "12_speed_ret_over"},
                        {"Scania GA867", "allison"},
                        {"Scania GA867 R", "allison_retarder"},
                        {"Opticruise GRSO 925", "grso925"},
                        {"Opticruise GRSO 925R", "grso925_r"}
                    }
                },
                {"volvo.fh16", new Dictionary<string, string>
                    {
                        {"I-Shift AT2812D", "12_speed"},
                        {"I-Shift ATO3512D", "12_speed_over"},
                        {"I-Shift AT2812D R", "12_speed_ret"},
                        {"I-Shift ATO3512D R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"I-Shift ATO3512F + ASO-ULC", "ato3512f_aso"},
                        {"I-Shift ATO3512F R + ASO-ULC", "ato3512f_r_aso"}
                    }
                },
                {"volvo.fh16_2012", new Dictionary<string, string>
                    {
                        {"I-Shift AT2812D", "12_speed"},
                        {"I-Shift ATO3512D", "12_speed_over"},
                        {"I-Shift AT2812D R", "12_speed_ret"},
                        {"I-Shift ATO3512D R", "12_speed_ret_over"},
                        {"Allison 4500", "allison"},
                        {"Allison 4500 R", "allison_retarder"},
                        {"I-Shift ATO3512F + ASO-ULC", "ato3512f_aso"},
                        {"I-Shift ATO3512F R + ASO-ULC", "ato3512f_r_aso"}
                    }
                }
            };



        public static string getSeriesToName(string s)
        {
            if (seriesToNameDictionary.ContainsKey(s))
            {
                return seriesToNameDictionary[s];
            }

            return null;
        }

        public static string getNameToSeries(string s)
        {
            if (nameToSeriesDictionary.ContainsKey(s))
            {
                return nameToSeriesDictionary[s];
            }

            return null;
        }

        public static string getEngineToEngineName(string series,string engine)
        {
            foreach (var VARIABLE in seresToEngineDictionary[series])
            {
                if (VARIABLE.Value == engine)
                {
                    return VARIABLE.Key;
                }
            }

            return null;
        }

        public static string getTransmissionToTransmissionName(string series, string transmission)
        {
            foreach (var VARIABLE in seresToTransmissionDictionary[series])
            {
                if (VARIABLE.Value == transmission)
                {
                    return VARIABLE.Key;
                }
            }

            return null;
        }

        public static string getEngineName(string series, string engine)
        {
            foreach (var VARIABLE in  seresToEngineDictionary[series])
            {
                if (VARIABLE.Value == engine)
                {
                    return VARIABLE.Key;
                }
            }
            return null;
        }

        public static string getTransmissionName(string series, string transmission)
        {
            foreach (var VARIABLE in seresToTransmissionDictionary[series])
            {
                if (VARIABLE.Value == transmission)
                {
                    return VARIABLE.Key;
                }
            }
            return null;
        }
    }
}
