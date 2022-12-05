using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourPathGenerator
{
    private enum Buildings
    {
        TBT,
        SMD,
        PRZ,
        MRT,
        Res90U,
        UCU,
        FSS,
        OTrain,
        BSC,
        CRX,
        STEM,
        CBY,
        SITE
    }

    public static Path GetFrenchPath() {
        return getTourPath(getFrenchInfo(), getFrenchAudio());
    }
    
    public static Path GetEnglishPath() {
        return getTourPath(getEnglishInfo(), getEnglishAudio());
    }

    private static Path getTourPath(Dictionary<Buildings, string[]> dialogueInfo, Dictionary<Buildings, AudioClip[]> dialogAudio)
    {
        Path path = new Path();

        //segment 1: Tabaret
        Waypoint way1_1 = new Waypoint(new GPSCoords(45.42456813170738f, -75.6859576372278f), 1);

        Waypoint[] waypoints1 = new Waypoint[] { way1_1 };
        PathSegment seg1 = new PathSegment(waypoints1);

        Dialogue dia1 = new Dialogue("Tabaret", dialogueInfo[Buildings.TBT], dialogAudio[Buildings.TBT]);
        PointOfInterest poi1 = new PointOfInterest("ua-bfb49c915c938cfc02d425bba471b290", "TBT", new GPSCoords(45.424553434090974f, -75.68598078289368f), dia1);
        poi1.BuildingImagePath = "Images/tbt";
        poi1.BuildingName = "Tabaret Hall";

        //segment 2: Simard
        Waypoint way2_1 = new Waypoint(new GPSCoords(45.42452298657076f, -75.68591748761722f), 1);
        Waypoint way2_2 = new Waypoint(new GPSCoords(45.42439678207031f, -75.68582066299066f), 2);
        Waypoint way2_3 = new Waypoint(new GPSCoords(45.42427785834053f, -75.68571865133055f), 3);
        Waypoint way2_4 = new Waypoint(new GPSCoords(45.42417228298274f, -75.68561663967043f), 4);
        Waypoint way2_5 = new Waypoint(new GPSCoords(45.42404243714795f, -75.6855319181142f), 5);
        Waypoint way2_6 = new Waypoint(new GPSCoords(45.42389681530589f, -75.68540570029744f), 6);
        Waypoint way2_7 = new Waypoint(new GPSCoords(45.423754833648225f, -75.68536074600655f), 7);

        Waypoint[] waypoints2 = new Waypoint[] { way2_1, way2_2, way2_3, way2_4, way2_5, way2_6, way2_7 };
        PathSegment seg2 = new PathSegment(waypoints2);

        Dialogue dia2 = new Dialogue("Simard and Hamelin Halls", dialogueInfo[Buildings.SMD], dialogAudio[Buildings.SMD]);
        PointOfInterest poi2 = new PointOfInterest("ua-dd0c7a71ddd870cfde1b637a449f1a68", "SMD", new GPSCoords(45.42369764735622f, -75.68534683603103f), dia2);
        poi2.BuildingImagePath = "Images/smd";
        poi2.BuildingName = "Simard and Hamelin Hall";

        //segment 3: Perez
        Waypoint way3_1 = new Waypoint(new GPSCoords(45.42372449563585f, -75.68531406269784f), 1);
        Waypoint way3_2 = new Waypoint(new GPSCoords(45.423649257307304f, -75.68523279917198f), 2);
        Waypoint way3_3 = new Waypoint(new GPSCoords(45.423540040200336f, -75.68514289059019f), 3);
        Waypoint way3_4 = new Waypoint(new GPSCoords(45.42342596877411f, -75.68502013079579f), 4);
        Waypoint way3_5 = new Waypoint(new GPSCoords(45.423467228678284f, -75.68493713825876f), 5);

        Waypoint[] waypoints3 = new Waypoint[] { way3_1, way3_2, way3_3, way3_4, way3_5 };
        PathSegment seg3 = new PathSegment(waypoints3);

        Dialogue dia3 = new Dialogue("Perez Hall", dialogueInfo[Buildings.PRZ], dialogAudio[Buildings.PRZ]);
        PointOfInterest poi3 = new PointOfInterest("ua-6a9eb96ae5a65bd61f6e49aff234cadd", "PRZ", new GPSCoords(45.423489072144754f, -75.68494405430351f), dia3);
        poi3.BuildingImagePath = "Images/prz";
        poi3.BuildingName = "Parez Hall";

        //segment 4: Morriset
        Waypoint way4_1 = new Waypoint(new GPSCoords(45.42346890997606f, -75.68494944118413f), 1);
        Waypoint way4_2 = new Waypoint(new GPSCoords(45.42343862522054f, -75.68502978794896f), 2);
        Waypoint way4_3 = new Waypoint(new GPSCoords(45.42333315060046f, -75.6849509290872f), 3);
        Waypoint way4_4 = new Waypoint(new GPSCoords(45.423224542864915f, -75.68486016700099f), 4);
        Waypoint way4_5 = new Waypoint(new GPSCoords(45.423114890612275f, -75.68476940491482f), 5);
        Waypoint way4_6 = new Waypoint(new GPSCoords(45.42304178899223f, -75.68467715492558f), 5);

        Waypoint[] waypoints4 = new Waypoint[] { way4_1, way4_2, way4_3, way4_4, way4_5, way4_6 };
        PathSegment seg4 = new PathSegment(waypoints4);

        Dialogue dia4 = new Dialogue("Morriset Library", dialogueInfo[Buildings.MRT], dialogAudio[Buildings.MRT]);
        PointOfInterest poi4 = new PointOfInterest("ua-ba0620616227078a8a68c50fdd714149", "MRT", new GPSCoords(45.423087393707114f, -75.68463110327154f), dia4);
        poi4.BuildingImagePath = "Images/mrt";
        poi4.BuildingName = "Morisset Library";

        //segment 5: 90U
        Waypoint way5_1 = new Waypoint(new GPSCoords(45.42305745362539f, -75.6847024492701f), 1);
        Waypoint way5_2 = new Waypoint(new GPSCoords(45.422926914818916f, -75.68461168718393f), 2);
        Waypoint way5_3 = new Waypoint(new GPSCoords(45.42284232551114f, -75.68453431622521f), 3);
        Waypoint way5_4 = new Waypoint(new GPSCoords(45.42279115413995f, -75.68449860655195f), 4);
        Waypoint way5_5 = new Waypoint(new GPSCoords(45.42271909641621f, -75.68444950575123f), 5);
        Waypoint way5_6 = new Waypoint(new GPSCoords(45.42263555111413f, -75.68448075171533f), 6);

        Waypoint[] waypoints5 = new Waypoint[] { way5_1, way5_2, way5_3, way5_4, way5_5, way5_6 };
        PathSegment seg5 = new PathSegment(waypoints5);

        Dialogue dia5 = new Dialogue("90 U", dialogueInfo[Buildings.Res90U], dialogAudio[Buildings.Res90U]);
        PointOfInterest poi5 = new PointOfInterest("ua-bd38a26e00f58f18c2697cfda7993b97", "90U", new GPSCoords(45.422619886354354f, -75.6845417557385f), dia5);
        poi5.BuildingImagePath = "Images/90u";
        poi5.BuildingName = "90 University Residence";

        //segment 6: UCU
        Waypoint way6_1 = new Waypoint(new GPSCoords(45.42263241815559f, -75.68447628799723f), 1);
        Waypoint way6_2 = new Waypoint(new GPSCoords(45.422617797714196f, -75.68437659849273f), 2);
        Waypoint way6_3 = new Waypoint(new GPSCoords(45.42251649883774f, -75.68427095737601f), 3);
        Waypoint way6_4 = new Waypoint(new GPSCoords(45.42246219421056f, -75.68419209851426f), 4);

        Waypoint[] waypoints6 = new Waypoint[] { way6_1, way6_2, way6_3, way6_4 };
        PathSegment seg6 = new PathSegment(waypoints6);

        Dialogue dia6 = new Dialogue("University Center (UCU)", dialogueInfo[Buildings.UCU], dialogAudio[Buildings.UCU]);
        PointOfInterest poi6 = new PointOfInterest("ua-61701ae599e1ac2fff38db6bbeb71691", "UCU", new GPSCoords(45.422452795327466f, -75.6841623404532f), dia6);
        poi6.BuildingImagePath = "Images/ucu";
        poi6.BuildingName = "University Center";

        //segment 7: FSS
        Waypoint way7_1 = new Waypoint(new GPSCoords(45.42243383251971f, -75.68419768445995f), 1);
        Waypoint way7_2 = new Waypoint(new GPSCoords(45.4223265257399f, -75.68410648935537f), 2);
        Waypoint way7_3 = new Waypoint(new GPSCoords(45.42220980585308f, -75.68400456541492f), 3);
        Waypoint way7_4 = new Waypoint(new GPSCoords(45.42212885482213f, -75.683934827982f), 4);
        Waypoint way7_5 = new Waypoint(new GPSCoords(45.42203660815698f, -75.68387313717594f), 5);
        Waypoint way7_6 = new Waypoint(new GPSCoords(45.421934948392234f, -75.68376048439968f), 6);
        Waypoint way7_7 = new Waypoint(new GPSCoords(45.42189729658109f, -75.68370415801155f), 7);

        Waypoint[] waypoints7 = new Waypoint[] { way7_1, way7_2, way7_3, way7_4, way7_5, way7_6, way7_7 };
        PathSegment seg7 = new PathSegment(waypoints7);

        Dialogue dia7 = new Dialogue("FSS", dialogueInfo[Buildings.FSS], dialogAudio[Buildings.FSS]);
        PointOfInterest poi7 = new PointOfInterest("ua-d39695084e8f1e7b07f2bf9cd70f870a", "FSS", new GPSCoords(45.42187282288526f, -75.68368270033561f), dia7);
        poi7.BuildingImagePath = "Images/fss";
        poi7.BuildingName = "Faculty of Social Sciences";
        
        //segment 8: LRT
        Waypoint way8_1 = new Waypoint(new GPSCoords(45.42194550463739f, -75.68359014472165f), 1);
        Waypoint way8_2 = new Waypoint(new GPSCoords(45.421996490834424f, -75.68340335015502f), 2);
        Waypoint way8_3 = new Waypoint(new GPSCoords(45.4219542451316f, -75.68322900855952f), 3);
        Waypoint way8_4 = new Waypoint(new GPSCoords(45.42187412388421f, -75.68300693057475f), 4);
        Waypoint way8_5 = new Waypoint(new GPSCoords(45.42182459432897f, -75.68283051348405f), 5);
        Waypoint way8_6 = new Waypoint(new GPSCoords(45.42178671875738f, -75.68261466198484f), 6);
        Waypoint way8_7 = new Waypoint(new GPSCoords(45.421769500103494f, -75.68246547384669f), 7);
        Waypoint way8_8 = new Waypoint(new GPSCoords(45.421619515530956f, -75.68233766002706f), 8);
        Waypoint way8_9 = new Waypoint(new GPSCoords(45.421462521902f, -75.68221184329393f), 9);
        Waypoint way8_10 = new Waypoint(new GPSCoords(45.42133776772691f, -75.68217190147398f), 10);
        Waypoint way8_11 = new Waypoint(new GPSCoords(45.42122703064545f, -75.68215592474652f), 11);
        Waypoint way8_12 = new Waypoint(new GPSCoords(45.421172362892264f, -75.68221384038354f), 12);
        Waypoint way8_13 = new Waypoint(new GPSCoords(45.42110367769138f, -75.68224379674751f), 13);

        Waypoint[] waypoints8 = new Waypoint[] { way8_1, way8_2, way8_3, way8_4, way8_5, way8_6, way8_7, way8_8, way8_9, way8_10, way8_11, way8_12, way8_13 };
        PathSegment seg8 = new PathSegment(waypoints8);

        Dialogue dia8 = new Dialogue("O-Train uOttawa Station", dialogueInfo[Buildings.OTrain], dialogAudio[Buildings.OTrain]);
        PointOfInterest poi8 = new PointOfInterest("ua-221c88f0ea0d0d4560aa795e313fb2df", "OTRAIN", new GPSCoords(45.42106239089179f, -75.68224828242504f), dia8);
        poi8.BuildingImagePath = "Images/otrain";
        poi8.BuildingName = "O-Train Station";
        
        //segment 9: Biosciences
        Waypoint way9_1 = new Waypoint(new GPSCoords(45.42115414029608f, -75.68222182874727f), 1);
        Waypoint way9_2 = new Waypoint(new GPSCoords(45.42124104800955f, -75.68208203238204f), 2);
        Waypoint way9_3 = new Waypoint(new GPSCoords(45.421287305286356f, -75.68189230874353f), 3);
        Waypoint way9_4 = new Waypoint(new GPSCoords(45.421360195463826f, -75.6816586491045f), 4);

        Waypoint[] waypoints9 = new Waypoint[] { way9_1, way9_2, way9_3, way9_4 };
        PathSegment seg9 = new PathSegment(waypoints9);

        Dialogue dia9 = new Dialogue("Biosciences", dialogueInfo[Buildings.BSC], dialogAudio[Buildings.BSC]);
        PointOfInterest poi9 = new PointOfInterest("ua-152ac567954511a9b903e4d5fad2fb25", "BSC", new GPSCoords(45.42133094736899f, -75.68160565806315f), dia9);
        poi9.BuildingImagePath = "Images/bsc";
        poi9.BuildingName = "Biosciences Complex";
        
        //segment 10: CRX
        Waypoint way10_1 = new Waypoint(new GPSCoords(45.42140138570245f, -75.6816020645416f), 1);
        Waypoint way10_2 = new Waypoint(new GPSCoords(45.421454111519f, -75.68159673928068f), 2);
        Waypoint way10_3 = new Waypoint(new GPSCoords(45.421543822228216f, -75.6815548003711f), 3);
        Waypoint way10_4 = new Waypoint(new GPSCoords(45.42161671207449f, -75.68147691382477f), 4);
        Waypoint way10_5 = new Waypoint(new GPSCoords(45.42168259320076f, -75.68133312327768f), 5);

        Waypoint[] waypoints10 = new Waypoint[] { way10_1, way10_2, way10_3, way10_4, way10_5 };
        PathSegment seg10 = new PathSegment(waypoints10);

        Dialogue dia10 = new Dialogue("Learning Crossroads", dialogueInfo[Buildings.CRX], dialogAudio[Buildings.CRX]);
        PointOfInterest poi10 = new PointOfInterest("ua-ba948d19751c853afcb6aa76a7b806bf", "CRX", new GPSCoords(45.42173726045992f, -75.68131914364116f), dia10);
        poi10.BuildingImagePath = "Images/crx";
        poi10.BuildingName = "Learning Crossroads";
        
        //segment 11: STEM
        Waypoint way11_1 = new Waypoint(new GPSCoords(45.421713957912495f, -75.68120056189126f), 1);
        Waypoint way11_2 = new Waypoint(new GPSCoords(45.42163865397028f, -75.6810610870254f), 2);
        Waypoint way11_3 = new Waypoint(new GPSCoords(45.421503106621195f, -75.68091356553266f), 3);
        Waypoint way11_4 = new Waypoint(new GPSCoords(45.421378854598665f, -75.68081700601016f), 4);
        Waypoint way11_5 = new Waypoint(new GPSCoords(45.421226363106385f, -75.68068021335326f), 5);
        Waypoint way11_6 = new Waypoint(new GPSCoords(45.42104939715457f, -75.68052464523367f), 6);
        Waypoint way11_7 = new Waypoint(new GPSCoords(45.42091573100801f, -75.68047636546308f), 7);
        Waypoint way11_8 = new Waypoint(new GPSCoords(45.42078206455171f, -75.68053269185121f), 8);
        Waypoint way11_9 = new Waypoint(new GPSCoords(45.42060697984025f, -75.68055146731369f), 9);

        Waypoint[] waypoints11 = new Waypoint[] { way11_1, way11_2, way11_3, way11_4, way11_5, way11_6, way11_7, way11_8, way11_9 };
        PathSegment seg11 = new PathSegment(waypoints11);

        Dialogue dia11 = new Dialogue("STEM", dialogueInfo[Buildings.STEM], dialogAudio[Buildings.STEM]);
        PointOfInterest poi11 = new PointOfInterest("ua-3a202a65407286a8ba810dfa8a5ef26a", "STM", new GPSCoords(45.420556148692285f, -75.68057560720175f), dia11);
        poi11.BuildingImagePath = "Images/stm";
        poi11.BuildingName = "STEM Complex";
        
        //segment 12: CBY
        Waypoint way12_1 = new Waypoint(new GPSCoords(45.42063898460803f, -75.68054073846669f), 1);
        Waypoint way12_2 = new Waypoint(new GPSCoords(45.4208008908263f, -75.68050855195919f), 2);
        Waypoint way12_3 = new Waypoint(new GPSCoords(45.42085360437856f, -75.68042540348148f), 3);
        Waypoint way12_4 = new Waypoint(new GPSCoords(45.42078582980231f, -75.68030470407834f), 4);
        Waypoint way12_5 = new Waypoint(new GPSCoords(45.42065592830381f, -75.68020009792895f), 5);
        Waypoint way12_6 = new Waypoint(new GPSCoords(45.420420598740975f, -75.68000429667497f), 6);
        Waypoint way12_7 = new Waypoint(new GPSCoords(45.42023045173779f, -75.67984336413745f), 7);
        Waypoint way12_8 = new Waypoint(new GPSCoords(45.42013383217179f, -75.67973397670409f), 8);
        Waypoint way12_9 = new Waypoint(new GPSCoords(45.42005103110523f, -75.67960773187033f), 9);
        Waypoint way12_10 = new Waypoint(new GPSCoords(45.419979851143935f, -75.67955806177181f), 10);

        Waypoint[] waypoints12 = new Waypoint[] { way12_1, way12_2, way12_3, way12_4, way12_5, way12_6, way12_7, way12_8, way12_9, way12_10 };
        PathSegment seg12 = new PathSegment(waypoints12);

        Dialogue dia12 = new Dialogue("Colonel By Hall", dialogueInfo[Buildings.CBY], dialogAudio[Buildings.CBY]);
        PointOfInterest poi12 = new PointOfInterest("ua-97a668f0e931c8d931c56bc3c9721f5a", "CBY", new GPSCoords(45.41996688056417f, -75.67955100334834f), dia12);
        poi12.BuildingImagePath = "Images/cby";
        poi12.BuildingName = "Colonel By Hall";
        
        //segment 13: SITE
        Waypoint way13_1 = new Waypoint(new GPSCoords(45.41998565408678f, -75.67951008034f), 1);
        Waypoint way13_2 = new Waypoint(new GPSCoords(45.419974358156075f, -75.67935719442936f), 2);
        Waypoint way13_3 = new Waypoint(new GPSCoords(45.41993858769395f, -75.67917480422017f), 3);
        Waypoint way13_4 = new Waypoint(new GPSCoords(45.419865164042776f, -75.67900046063785f), 4);
        Waypoint way13_5 = new Waypoint(new GPSCoords(45.41978420965007f, -75.67889585448846f), 5);

        Waypoint[] waypoints13 = new Waypoint[] { way13_1, way13_2, way13_3, way13_4, way13_5 };
        PathSegment seg13 = new PathSegment(waypoints13);

        Dialogue dia13 = new Dialogue("SITE", dialogueInfo[Buildings.SITE], dialogAudio[Buildings.SITE]);
        PointOfInterest poi13 = new PointOfInterest("ua-af8105afd4e84870f91fcf42fbf6330a", "STE", new GPSCoords(45.41976350036815f, -75.67887707902575f), dia13);
        poi13.BuildingImagePath = "Images/ste";
        poi13.BuildingName = "SITE";
        
        //assemble path
        PathSegment[] segments = { seg1, seg2, seg3, seg4, seg5, seg6, seg7, seg8, seg9, seg10, seg11, seg12, seg13 };
        PointOfInterest[] pois = { poi1, poi2, poi3, poi4, poi5, poi6, poi7, poi8, poi9, poi10, poi11, poi12, poi13 };

        path.SetSegmentsAndPOIs(segments, pois);

        return path;
    }

    private static Dictionary<Buildings, string[]> getEnglishInfo()
    {
        Dictionary<Buildings, string[]> dialogueInfo = new Dictionary<Buildings, string[]>();

        //segment 1: Tabaret
        dialogueInfo.Add(Buildings.TBT, new string[] {"This is Tabaret Hall!",
                                                        "This building is named after former president of Bytown College Joseph-Henri Tabaret. Bytown College became an official university under his leadership and for this reason he is widely regarded as its founder.", 
                                                        "There are several classrooms found in Tabaret Hall; however the building is dedicated mostly to the university’s administration. It is here where you can find services including Registrar and Admissions, Human resources, and Infoservice.",
                                                        "Tabaret Hall is the most iconic building found at the university. You may have already seen it before as it is this building off which the uOttawa logo is based."
                                                     });

        //segment 2: Simard
        dialogueInfo.Add(Buildings.SMD, new string[] {"Here are Hamelin and Simard Hall!", 
                                                        "Hamelin and Simard are two buildings that have a connecting hallway that bridges the two of them together. This bridge actually contains a classroom known as The Batcave which holds roughly 250 students.", 
                                                        "Hamelin is home to many departments in the Faculty of Arts including Modern Languages, Linguistics, Philosophy, and Religious Studies",
                                                        "Simard is home to the Faculty of Arts, French Department, and Department of Geography. Also found here on its bottom floor is Café Alt, a space run by students where movies, slam poetry and open mic nights happen often."
                                                     });
        
        //segment 3: Perez
        dialogueInfo.Add(Buildings.PRZ, new string[] {"This is Perez Hall!",
                                                        "Perez is home to the university’s Department of Music. It houses its very own music library as well as practice rooms and auditoriums.", 
                                                        "It also features the Piano Pedagogy Research Laboratory that measures and digitizes performance based on both visual and auditory responses to study and improve the process of piano learning and teaching."
                                                     });
        
        //segment 4: Morriset
        dialogueInfo.Add(Buildings.MRT, new string[] {"Welcome to Morisset Library!",
                                                        "Morisset is uOttawa’s main graduate and undergraduate library. With a total of 6 floors and 2 basement levels, Morisset features a plethora of research material to help you discover the information you need. It is also the most energy efficient library found in all of Canada!",
                                                        "In Morisset, there are many different types of study areas to fit your personal needs including quiet study spaces, communal study areas, and reservable rooms for collaborative work!"
                                                     });
        
        //segment 5: 90U
        dialogueInfo.Add(Buildings.Res90U, new string[] {"Welcome to the 90 University Residence complex!",
                                                            "90U is one of uOttawa’s many residence buildings that offer housing to students in search of accommodations closer to campus life. Other residences include Annex, Hyman-Soloway, 45 Mann, Friel, Thompson, Henderson, LeBlanc, Marchand, and Stanton, Rideau.",
                                                            "Housing in one of uOttawa’s residence buildings is even guaranteed to all first year students who apply before June 1st of the given year. Rooms are offered on a first come, first served basis so be sure to apply as soon as possible to secure a room of your choice!",
                                                            "Residences are both on and off campus, with off campus residences being within at most a 5-10 min walk to the university grounds."
                                                         });
        
        //segment 6: UCU
        dialogueInfo.Add(Buildings.UCU, new string[] {"This is the University Center!",
                                                        "The University Center is an all purpose area for communities on campus to gather in a common space. The Terminus, located on the second floor of UCU, is a gathering space commonly used by many of the diverse clubs at uOttawa.",
                                                        "Many services can also be found here including the Health Promotion Centre, Community Life Service, and Career Development Centre. The campus Bookstore can also be found here where you can buy and rent textbooks or shop for some uOttawa merch. The offices for the student union, UOSU, are in the basement and they’re where new students can get their U-Pass bus passes and get informed on various services offered to them.",
                                                        "Also featured in the University Center is the award-winning Dining Hall with a wide variety of fresh meals prepared on site. It is not only zero-waste, but can also accommodate almost any dietary need including allergies (nuts, tree nuts, gluten or wheat, dairy, egg) and restrictions (vegan, vegetarian, halal)."
                                                     });
        
        //segment 7: FSS
        dialogueInfo.Add(Buildings.FSS, new string[] {"Welcome to the Faculty of Social Sciences!",
                                                        "The Faculty of Social Sciences is the largest faculty at uOttawa and includes programs such as criminology, psychology, Conflict Studies & Human Rights, economy, political sciences and feminist and gender studies.The living wall is a focal point of this building that can be seen when entering the building and extends up to the 6th floor. In fact, it is the largest biofilter in all of North America!",
                                                        "At FSS, you can find classrooms and offices throughout the building along with plenty of spaces for either personal study or group collaboration. It is also home to the Inspire psychology lab that enables students to apply modern equipment to study various branches of psychology.",
                                                        "FSS is also home to the International Exchange office which allows students to earn their degree while also experiencing the cultures that different institutions have to offer both within and outside of Canada."
                                                     });

        //segment 8: LRT
        dialogueInfo.Add(Buildings.OTrain, new string[] {"This is the O-Train station!",
                                                            "Here is the O-Train station located right here on the uOttawa campus! Using the U-Pass service included with your university fees, you can board the O-Train or any OC Transpo and STO buses by simply tapping your U-Pass on the card reader.",
                                                            "Taking the train eastbound, you can even stop at Lees station where you’ll find uOttawa’s athletic facilities including our sports playing field!"
                                                        });
       
        //segment 9: Biosciences
        dialogueInfo.Add(Buildings.BSC, new string[] {"This is the Biosciences Complex!",
                                                        "The Bioscience Complex is home to the Faculty of Science and hosts plenty of biology and biochemistry labs where students can experience hands-on learning and research practice.",
                                                        "The Bioscience Complex is where the Husky Courtyard is located. It is known as a living classroom as it is a simulation of a Canadian boreal forest and wetland environment where students can learn to study and identify the flora. Despite being a slice of nature, its outdoor seating and internet access makes for a great spot to study.",
                                                        "Additionally, the Bioscience Complex is home to the living laboratory aquarium where sea-ing is believing! Using ecologically sustainable methods, a coral reef environment allows students a rare and personal view of the intricate interactions occurring continually between the living organisms that inhabit the reef. By studying these interactions in a controlled environment, students and researchers will continue to positively impact the health and welfare of aquatic life and in particular the highly endangered natural coral reefs.",
                                                        "Students also study plants at the Faculty of Science greenhouse located on the roof of the Bioscience Complex. This is where researchers and students find new methods to study and research these plants that come from all around the globe!"
                                                     });

        //segment 10: CRX
        dialogueInfo.Add(Buildings.CRX, new string[] {"Here is the Learning Crossroads!",
                                                        "The Learning Crossroads is a modern, open concept building designed to provide students with ample study spaces. It features a wide range of study spaces including individual study spaces, open concept study areas, and reservable private study rooms. These rooms are open to all students and can be found on every floor.",
                                                        "In addition to the many study spaces, CRX also features a variety of places to eat whether you’re on the go or in between classes including Tim Hortons, the Thai Express, and the Paramount."
                                                     });

        //segment 11: STEM
        dialogueInfo.Add(Buildings.STEM, new string[] {"Welcome to the STEM Complex!",
                                                        "The recently constructed Science, Technology, Engineering, And Mathematics (STEM) building is the largest facility on campus. It features a variety of new research and teaching labs with an approach focused on hands-on learning.",
                                                        "The STEM complex contains several classrooms but also features engineering labs including structural and mechanical in underground levels as well as science labs including chemistry and physics on higher floors.",
                                                        "The STEM complex also features the Makerspace, a student run facility where students have the opportunity to design, prototype, and build their own creations for free. Included in the Makerspace are 3D printers, Arduinos, laser cutters and much more. Whether it's for class work or a personal project, the Makerspace allows you to develop and create your products and ideas.",
                                                        "The Brunsfield Centre, a machining shop open to students, can also be found here. Students can get training to use the lathes, mills, drill presses, bandsaws, and welding.",
                                                        "STEM is also the home of the JMTS team space, where the university’s competitive engineering teams are. There are many different teams students can join including Rocketry, Formula SAE, Supermileage, Concrete Toboggan, BAJA, Kelpie Robotics, and Mars Rover."
                                                      });
        
        //segment 12: CBY
        dialogueInfo.Add(Buildings.CBY, new string[] {"Here is Colonel By Hall!",
                                                        "Colonel By Hall is home to the Faculty of Engineering and houses the Departments of Chemical, Mechanical, and Civil Engineering. There are many classrooms, computer and science labs, and study spaces located throughout the building.",
                                                        "The basement floor is home to the Student Lounge and the Engineering Students’ Society who provide many services and organize events for students."
                                                     });
        
        //segment 13: SITE
        dialogueInfo.Add(Buildings.SITE, new string[] {"Welcome to SITE!",
                                                        "The School of Information Technology and Engineering (SITE) is one of the 3 main engineering buildings at uOttawa. It is also home to the School of Electrical Engineering and Computer Science (EECS) whose programs include Electrical, Software, and Computer Engineering as well as Computer Science.",
                                                        "SITE contains plenty of spacious lecture halls and computer labs, the largest of which can be found on the ground floor. It also features plenty of study areas and spaces for breaks with a full service Tim Hortons."
                                                      });
        
        return dialogueInfo;
    }

    private static Dictionary<Buildings, AudioClip[]> getEnglishAudio()
    {
        Dictionary<Buildings, AudioClip[]> dialogAudio = new Dictionary<Buildings, AudioClip[]>();

        //segment 1: Tabaret
        dialogAudio.Add(Buildings.TBT, new AudioClip[] { Resources.Load<AudioClip>("Audio/TBT/This_is_Tabaret_Hall_386"), 
                                                            Resources.Load<AudioClip>("Audio/TBT/This_building_is_nam_877"), 
                                                            Resources.Load<AudioClip>("Audio/TBT/There_are_several_cl_810"), 
                                                            Resources.Load<AudioClip>("Audio/TBT/Tabaret_Hall_is_the__525")
                                                       });
        
        
        //segment 2: Simard
        dialogAudio.Add(Buildings.SMD, new AudioClip[] { Resources.Load<AudioClip>("Audio/SMD/Here_are_Hamelin_and_161"), 
                                                            Resources.Load<AudioClip>("Audio/SMD/Hamelin_and_Simard_a_808"), 
                                                            Resources.Load<AudioClip>("Audio/SMD/Hamelin_is_home_to_m_409"), 
                                                            Resources.Load<AudioClip>("Audio/SMD/Simard_is_home_to_th_585")
                                                       });
        
        //segment 3: Perez
        dialogAudio.Add(Buildings.PRZ, new AudioClip[] { Resources.Load<AudioClip>("Audio/PRZ/This_is_Perez_Hall_608"),
                                                            Resources.Load<AudioClip>("Audio/PRZ/Perez_is_home_to_the_918"), 
                                                            Resources.Load<AudioClip>("Audio/PRZ/It_also_features_the_770")
                                                       });
        
        //segment 4: Morriset
        dialogAudio.Add(Buildings.MRT, new AudioClip[] { Resources.Load<AudioClip>("Audio/MRT/Welcome_to_Morisset__279"),
                                                            Resources.Load<AudioClip>("Audio/MRT/Morisset_is_uOttawa_324"), 
                                                            Resources.Load<AudioClip>("Audio/MRT/In_Morisset_there_a_430")
                                                       });
        
        //segment 5: 90U
        dialogAudio.Add(Buildings.Res90U, new AudioClip[] { Resources.Load<AudioClip>("Audio/90U/Welcome_to_the_90_Un_729"), 
                                                            Resources.Load<AudioClip>("Audio/90U/90U_is_one_of_uOttaw_426"), 
                                                            Resources.Load<AudioClip>("Audio/90U/Housing_in_one_of_uO_392"), 
                                                            Resources.Load<AudioClip>("Audio/90U/Residences_are_both__147")
                                                          });
        
        //segment 6: UCU
        dialogAudio.Add(Buildings.UCU, new AudioClip[] { Resources.Load<AudioClip>("Audio/UCU/This_is_the_Universi_361"),
                                                            Resources.Load<AudioClip>("Audio/UCU/The_University_Cente_541"),
                                                            Resources.Load<AudioClip>("Audio/UCU/Many_services_can_al_601"), 
                                                            Resources.Load<AudioClip>("Audio/UCU/Also_featured_in_the_935")
                                                       });
        
        //segment 7: FSS
        dialogAudio.Add(Buildings.FSS, new AudioClip[] { Resources.Load<AudioClip>("Audio/FSS/Welcome_to_the_Facul_371"),
                                                            Resources.Load<AudioClip>("Audio/FSS/The_Faculty_of_Socia_410"),
                                                            Resources.Load<AudioClip>("Audio/FSS/At_F_S_S_you_can_fi_286"),
                                                            Resources.Load<AudioClip>("Audio/FSS/F_S_S_is_also_home_t_472")
                                                       });
        
        //segment 8: LRT
        dialogAudio.Add(Buildings.OTrain, new AudioClip[] { Resources.Load<AudioClip>("Audio/O-Train/This_is_the_OTrain__493"),
                                                            Resources.Load<AudioClip>("Audio/O-Train/Here_is_the_OTrain__468"),
                                                            Resources.Load<AudioClip>("Audio/O-Train/Taking_the_train_eas_123")
                                                          });
        
        //segment 9: Biosciences
        dialogAudio.Add(Buildings.BSC, new AudioClip[] { Resources.Load<AudioClip>("Audio/BSC/This_is_the_Bioscien_473"),
                                                            Resources.Load<AudioClip>("Audio/BSC/The_Bioscience_Compl_564"),
                                                            Resources.Load<AudioClip>("Audio/BSC/The_Bioscience_Compl_640"),
                                                            Resources.Load<AudioClip>("Audio/BSC/Additionally_the_Bi_503"),
                                                            Resources.Load<AudioClip>("Audio/BSC/By_studying_these_in_813")
                                                       });
        
        //segment 10: CRX
        dialogAudio.Add(Buildings.CRX, new AudioClip[] { Resources.Load<AudioClip>("Audio/CRX/Here_is_the_Learning_229"),
                                                            Resources.Load<AudioClip>("Audio/CRX/The_Learning_Crossro_153"),
                                                            Resources.Load<AudioClip>("Audio/CRX/In_addition_to_the_m_130")
                                                       });
        
        //segment 11: STEM
        dialogAudio.Add(Buildings.STEM, new AudioClip[] { Resources.Load<AudioClip>("Audio/STM/Welcome_to_the_STEM__263"),
                                                            Resources.Load<AudioClip>("Audio/STM/The_recently_constru_492"),
                                                            Resources.Load<AudioClip>("Audio/STM/The_STEM_complex_con_925"),
                                                            Resources.Load<AudioClip>("Audio/STM/The_STEM_complex_als_217"),
                                                            Resources.Load<AudioClip>("Audio/STM/The_Brunsfield_Centr_692"), 
                                                            Resources.Load<AudioClip>("Audio/STM/STEM_is_also_the_home_674")
                                                        });
        
        //segment 12: CBY
        dialogAudio.Add(Buildings.CBY, new AudioClip[] { Resources.Load<AudioClip>("Audio/CBY/Here_is_Colonel_By_H_331"),
                                                            Resources.Load<AudioClip>("Audio/CBY/Colonel_By_Hall_is_h_400"),
                                                            Resources.Load<AudioClip>("Audio/CBY/The_basement_floor_i_902")
                                                       });
        
        //segment 13: SITE
        dialogAudio.Add(Buildings.SITE, new AudioClip[] { Resources.Load<AudioClip>("Audio/STE/Welcome_to_SITE_380"),
                                                            Resources.Load<AudioClip>("Audio/STE/The_School_of_Inform_907"),
                                                            Resources.Load<AudioClip>("Audio/STE/SITE_contains_plenty_581")
                                                        });
        
        return dialogAudio;
    }

    private static Dictionary<Buildings, string[]> getFrenchInfo()
    {
        Dictionary<Buildings, string[]> dialogueInfo = new Dictionary<Buildings, string[]>();

        //segment 1: Tabaret
        dialogueInfo.Add(Buildings.TBT, new string[] {"Bienvenue au Pavillon Tabaret!",
                                                        "Le pavillon a été nommé après Joseph-Henri Tabaret, l’ancien president du college Bytown. Le collège bytown est devenu une université officielle sous sa direction et pour cette raison, il est largement considéré comme son fondateur.", 
                                                        "L'édifice contient plusieurs salles de classe. Cependant, il est principalement dédié à l’administration de l'université. C’est ici ou vous pouvez trouver des services tels que le registraire, l’infoAdmission, les mentor régionaux, les ressources humaines et l’infoservice.",
                                                        "Tabaret Hall est le bâtiment le plus iconique de l'université. Vous l'avez peut-être déjà vu auparavant, car c’est cet édifice qui inspire le logo de l'Université d'Ottawa."
                                                     });

        //segment 2: Simard
        dialogueInfo.Add(Buildings.SMD, new string[] {"Voici les pavillions Hamelin et Simard!", 
                                                        "Hamelin et Simard sont deux bâtiments qui ont un couloir communiquant qui les relie ensemble. Ce pont contient une salle de classe connue sous le nom de “The Batcave” qui accueille environ 250 élèves.", 
                                                        "Hamelin abrite de nombreux départements de la Faculté des arts, notamment les langues modernes, la linguistique, la philosophie et les études religieuses.",
                                                        "Simard abrite la Faculté des arts, le Département de français et le Département de géographie. On y trouve aussi au rez-de-chaussée, le Café Alt, un espace géré par des étudiants où des soirées à micro ouvert ainsi que des soirées de films et de poésie slam s’y produisent souvent."
                                                     });
        
        //segment 3: Perez
        dialogueInfo.Add(Buildings.PRZ, new string[] {"Voici le Pavillon Pérez!",
                                                        "Pérez abrite le département de musique de l'université. Il abrite sa propre bibliothèque musicale ainsi que des salles de répétition et des auditoriums.", 
                                                        "Il comprend également le laboratoire de recherche sur la pédagogie du piano qui mesure et numérise les performances en fonction des réponses visuelles et auditives pour étudier et améliorer le processus d'apprentissage et d'enseignement du piano."
                                                     });
        
        //segment 4: Morriset
        dialogueInfo.Add(Buildings.MRT, new string[] {"Bienvenue à la Bibliothèque Morisset!",
                                                        "Morisset est la principale bibliothèque des études supérieures et de premier cycle de l'Université d'Ottawa. Avec un total de 6 étages et 2 niveaux de sous-sol, Morisset dispose d'une pléthore de matériel de recherche pour vous aider à découvrir les informations dont vous avez besoin. C'est aussi la bibliothèque la plus éconergétique de tout le Canada!",
                                                        "À Morisset, il existe de nombreux types d'espaces d'étude pour répondre à vos besoins personnels, notamment des espaces d'étude calmes, des espaces d'étude communs et des salles pour le travail collaboratif sous réservation!"
                                                     });
        
        //segment 5: 90U
        dialogueInfo.Add(Buildings.Res90U, new string[] {"Bienvenue au complexe de Résidence Universitaire 90!",
                                                            "Le 90U est l'un des nombreux immeubles de résidence de l'Université d'Ottawa qui offrent des logements aux étudiants à la recherche d'un logement plus proche de la vie du campus. Les autres résidences comprennent Annex, Hyman-Soloway, 45 Mann, Friel, Thompson, Henderson, LeBlanc, Marchand et Stanton, Rideau.",
                                                            "L'hébergement dans l'une des résidences de l'Université d'Ottawa est même garanti à tous les étudiants de première année qui postulent avant le 1er juin de l'année en question. Les chambres sont disponibles selon le principe du premier arrivé, premier servi, alors assurez-vous de postuler dès que possible pour réserver une chambre de votre choix!",
                                                            "Les résidences sont à la fois sur et hors campus, les résidences hors campus se trouvant à 5-10 minutes à pied maximum du terrain de l'université."
                                                         });
        
        //segment 6: UCU
        dialogueInfo.Add(Buildings.UCU, new string[] {"Voici le Centre Universitaire!",
                                                        "Le Centre universitaire est une zone polyvalente permettant aux communautés du campus de se rassembler dans un espace commun. Le Terminus, situé au deuxième étage de l'UCU, est un espace de rassemblement couramment utilisé par de nombreux clubs divers à l'Université d'Ottawa.",
                                                        "De nombreux services peuvent également être trouvés ici, y compris le Centre de promotion de la santé, le Service de vie communautaire et le Centre de développement de carrière. La librairie du campus se trouve également ici où vous pouvez acheter et louer des manuels ou acheter des produits dérivés de l'Université d'Ottawa. Les bureaux du syndicat étudiant, UOSU, sont au sous-sol et c'est là que les nouveaux étudiants peuvent obtenir leurs laissez-passer d'autobus U-Pass et s'informer sur les différents services qui leur sont offerts.",
                                                        "Le centre universitaire comprend également la comptine universitaire ou vous pouvez trouver une grande variété de plats frais préparés sur place. Non seulement il est zéro déchet, mais il peut également répondre à presque tous les besoins alimentaires, y compris les allergies (noix, gluten ou blé, produits laitiers, œufs) et les restrictions (végétalien, végétarien, halal)."
                                                     });
        
        //segment 7: FSS
        dialogueInfo.Add(Buildings.FSS, new string[] {"Bienvenue à la Faculté des sciences sociales!",
                                                        "La Faculté des sciences sociales est la plus grande faculté de l'Université d'Ottawa et comprend des programmes tels que la criminologie, la psychologie, les études sur les conflits et les droits de la personne, l'économie, les sciences politiques et les études féministes et de genre. Le mur vivant est un point central de ce bâtiment qui peut être vu à l'entrée du bâtiment et s'étend jusqu'au 6ème étage. En fait, c'est le plus grand biofiltre de toute l'Amérique du Nord!",
                                                        "A FSS, vous pouvez trouver des salles de classe et des bureaux dans tout le bâtiment ainsi que de nombreux espaces pour l'étude personnelle ou la collaboration en groupe. Il abrite également le laboratoire de psychologie Inspire qui permet aux étudiants d'utiliser des équipements modernes pour étudier diverses branches de la psychologie.",
                                                        "FSS abrite également le bureau d'échange international qui permet aux étudiants d'obtenir leur diplôme tout en découvrant les cultures que différentes institutions ont à offrir à l'intérieur et à l'extérieur du Canada."
                                                     });

        //segment 8: LRT
        dialogueInfo.Add(Buildings.OTrain, new string[] {"Voici la gare de l'O-Train!",
                                                            "Voici la station O-Train située ici même sur le campus de l'uOttawa! En utilisant le service U-Pass inclus avec vos frais universitaires, vous pouvez monter à bord de l'O-Train ou de n'importe quel autobus d'OC Transpo et de la STO en tapant simplement votre U-Pass sur le lecteur de carte.",
                                                            "En prenant le train vers l'est, vous pouvez même vous arrêter à la station Lees où vous trouverez les établissements sportives de l'Université d'Ottawa, y compris notre terrain de sport!"
                                                        });
       
        //segment 9: Biosciences
        dialogueInfo.Add(Buildings.BSC, new string[] {"Voici le Complexe des Biosciences!",
                                                        "Le complexe des biosciences abrite la Faculté des sciences et abrite de nombreux laboratoires de biologie et de biochimie où les étudiants peuvent faire de l’apprentissage pratique et pratiquer de la recherche.",
                                                        "Le complexe des biosciences est l'endroit où se trouve la cour Husky. Il s'agit d'une salle de classe vivante, car il s'agit d'une simulation d'un milieu boréal canadien et d'un milieu humide où les élèves peuvent apprendre à étudier et à identifier la flore. Bien qu'il s'agisse d'une tranche de nature, ses sièges extérieurs et son accès Internet en font un endroit idéal pour étudier.",
                                                        "De plus, le Complexe Bioscience abrite le laboratoire d'aquarium vivant! En utilisant des méthodes écologiquement durables, un environnement de récif corallien permet aux étudiants une vision rare et personnelle des interactions complexes qui se produisent continuellement entre les organismes vivants qui habitent le récif. En étudiant ces interactions dans un environnement contrôlé, les étudiants et les chercheurs continueront d'avoir un impact positif sur la santé et le bien-être de la vie aquatique et en particulier sur les récifs coralliens naturels très menacés.",
                                                        "Les étudiants étudient également les plantes qu’on peut trouver à la serre de la Faculté des sciences située sur le toit du Complexe des biosciences. C'est là que chercheurs et étudiants trouvent de nouvelles méthodes pour étudier et rechercher ces plantes venues du monde entier!"
                                                     });

        //segment 10: CRX
        dialogueInfo.Add(Buildings.CRX, new string[] {"Voici le carrefour des apprentissages!",
                                                        "Le carrefour des apprentissages est un bâtiment moderne à concept ouvert conçu pour offrir aux étudiants de vastes espaces d'étude. Il dispose d'un large éventail d'espaces d'étude, y compris des espaces d'étude individuels, des zones d'étude ouverte et des salles d'étude privées sous réservation. Ces salles sont ouvertes à tous les étudiants et se trouvent à tous les étages.",
                                                        "En plus des nombreux espaces d'étude, CRX propose également une variété d'endroits pour manger, que vous soyez en déplacement ou entre deux cours, notamment Tim Hortons, le Thaï Express et le Paramount."
                                                     });

        //segment 11: STEM
        dialogueInfo.Add(Buildings.STEM, new string[] {"Bienvenue au complexe STEM!",
                                                        "Le bâtiment récemment construit pour les sciences, la technologie, l'ingénierie et les mathématiques (STEM) est la plus grande installation du campus. Il propose une variété de nouveaux laboratoires de recherche et d'enseignement avec une approche axée sur l'apprentissage pratique.",
                                                        "Le complexe STEM contient plusieurs salles de classe, mais comprend également des laboratoires d'ingénierie, notamment de structure et de mécanique dans les niveaux souterrains, ainsi que des laboratoires de sciences, notamment de chimie et de physique aux étages supérieurs.",
                                                        "Le complexe STEM comprend également le Makerspace, une installation gérée par des étudiants où les étudiants ont la possibilité de concevoir, de prototyper et de construire leurs propres créations gratuitement. Le Makerspace comprend des imprimantes 3D, des Arduinos, des découpeurs laser et bien plus encore. Que ce soit pour un travail en classe ou un projet personnel, le Makerspace vous permet de développer et de créer vos produits et vos idées.",
                                                        "Le Brunsfield Centre, un atelier d'usinage ouvert aux étudiants, se trouve également ici. Les étudiants peuvent suivre une formation pour utiliser les tours, les fraiseuses, les perceuses à colonne, les scies à ruban et le soudage.",
                                                        "STEM abrite également l'espace d'équipe ECJM, où se trouvent les équipes compétitives d'ingénierie de l'université. Il existe de nombreuses équipes différentes que les étudiants peuvent rejoindre, notamment Rocketry, Formula SAE, Supermileage, Concrete Toboggan, BAJA, Kelpie Robotics et Mars Rover."
                                                      });
        
        //segment 12: CBY
        dialogueInfo.Add(Buildings.CBY, new string[] {"Voici le Pavillon Colonel By!",
                                                        "Le pavillon colonel by abrite la Faculté de génie et abrite les départements de génie chimique, mécanique et civil. Il y a de nombreuses salles de classe, des laboratoires informatiques et scientifiques et des espaces d'étude répartis dans tout le bâtiment.",
                                                        "Le sous-sol abrite le Student Lounge et l'association des étudiants en génie qui offrent de nombreux services et organisent des événements pour les étudiants."
                                                     });
        
        //segment 13: SITE
        dialogueInfo.Add(Buildings.SITE, new string[] {"Bienvenue sur EITE!",
                                                        "L'École d'ingénierie et de technologie de l'information (EITE) est l'un des 3 principaux bâtiments d'ingénierie de l'Université d'Ottawa. Il abrite également l'École de génie électrique et d'informatique (EECS) dont les programmes comprennent le génie électrique, logiciel et informatique ainsi que la science informatique.",
                                                        "EITE contient de nombreuses salles de cours magistrales spacieuses et des laboratoires informatiques, dont le plus grand se trouve au rez-de-chaussée. Il dispose également de nombreux espaces d'étude et d'espaces de pause avec un service complet Tim Hortons."
                                                      });
        
        return dialogueInfo;
    }

    private static Dictionary<Buildings, AudioClip[]> getFrenchAudio()
    {
        Dictionary<Buildings, AudioClip[]> dialogAudio = new Dictionary<Buildings, AudioClip[]>();

        //segment 1: Tabaret
        dialogAudio.Add(Buildings.TBT, new AudioClip[] { Resources.Load<AudioClip>("Audio/TBT/Bienvenue_au_Pavillo_913"), 
                                                            Resources.Load<AudioClip>("Audio/TBT/Le_pavillon_a_t__950"), 
                                                            Resources.Load<AudioClip>("Audio/TBT/Ldifice_contient__254"), 
                                                            Resources.Load<AudioClip>("Audio/TBT/Tabaret_Hall_est_le__250")
                                                       });
        
        
        //segment 2: Simard
        dialogAudio.Add(Buildings.SMD, new AudioClip[] { Resources.Load<AudioClip>("Audio/SMD/Voici_les_pavillions_365"), 
                                                            Resources.Load<AudioClip>("Audio/SMD/Hamelin_et_Simard_so_996"), 
                                                            Resources.Load<AudioClip>("Audio/SMD/Hamelin_abrite_de_no_814"), 
                                                            Resources.Load<AudioClip>("Audio/SMD/Simard_abrite_la_Fac_485")
                                                       });
        
        //segment 3: Perez
        dialogAudio.Add(Buildings.PRZ, new AudioClip[] { Resources.Load<AudioClip>("Audio/PRZ/Voici_le_Pavillon_P__848"),
                                                            Resources.Load<AudioClip>("Audio/PRZ/Prez_abrite_le_d_561"), 
                                                            Resources.Load<AudioClip>("Audio/PRZ/Il_comprend_galeme_795")
                                                       });
        
        //segment 4: Morriset
        dialogAudio.Add(Buildings.MRT, new AudioClip[] { Resources.Load<AudioClip>("Audio/MRT/Bienvenue__la_Bibl_172"),
                                                            Resources.Load<AudioClip>("Audio/MRT/Morisset_est_la_prin_559"), 
                                                            Resources.Load<AudioClip>("Audio/MRT/_Morisset_il_exis_972")
                                                       });
        
        //segment 5: 90U
        dialogAudio.Add(Buildings.Res90U, new AudioClip[] { Resources.Load<AudioClip>("Audio/90U/Bienvenue_au_complex_113"), 
                                                            Resources.Load<AudioClip>("Audio/90U/Le_90U_est_lun_des__739"), 
                                                            Resources.Load<AudioClip>("Audio/90U/Lhbergement_dans__588"), 
                                                            Resources.Load<AudioClip>("Audio/90U/Les_rsidences_sont_840")
                                                          });
        
        //segment 6: UCU
        dialogAudio.Add(Buildings.UCU, new AudioClip[] { Resources.Load<AudioClip>("Audio/UCU/Voici_le_Centre_Univ_875"),
                                                            Resources.Load<AudioClip>("Audio/UCU/Le_Centre_universita_692"),
                                                            Resources.Load<AudioClip>("Audio/UCU/De_nombreux_services_591"), 
                                                            Resources.Load<AudioClip>("Audio/UCU/Le_centre_universita_362")
                                                       });
        
        //segment 7: FSS
        dialogAudio.Add(Buildings.FSS, new AudioClip[] { Resources.Load<AudioClip>("Audio/FSS/Bienvenue__la_Facu_991"),
                                                            Resources.Load<AudioClip>("Audio/FSS/La_Facult_des_scie_968"),
                                                            Resources.Load<AudioClip>("Audio/FSS/A_F_S_S_vous_pouvez_340"),
                                                            Resources.Load<AudioClip>("Audio/FSS/F_S_S_abrite_galem_307")
                                                       });
        
        //segment 8: LRT
        dialogAudio.Add(Buildings.OTrain, new AudioClip[] { Resources.Load<AudioClip>("Audio/O-Train/Voici_la_gare_de_lO_213"),
                                                            Resources.Load<AudioClip>("Audio/O-Train/Voici_la_station_OT_238."),
                                                            Resources.Load<AudioClip>("Audio/O-Train/En_prenant_le_train__742")
                                                          });
        
        //segment 9: Biosciences
        dialogAudio.Add(Buildings.BSC, new AudioClip[] { Resources.Load<AudioClip>("Audio/BSC/Voici_le_Complexe_de_894"),
                                                            Resources.Load<AudioClip>("Audio/BSC/Le_Complexe_des_bios_650"),
                                                            Resources.Load<AudioClip>("Audio/BSC/Le_complexe_des_bios_263"),
                                                            Resources.Load<AudioClip>("Audio/BSC/De_plus_le_Complexe_639"),
                                                            Resources.Load<AudioClip>("Audio/BSC/Les_tudiants_tud_914")
                                                       });
        
        //segment 10: CRX
        dialogAudio.Add(Buildings.CRX, new AudioClip[] { Resources.Load<AudioClip>("Audio/CRX/Voici_le_carrefour_d_208"),
                                                            Resources.Load<AudioClip>("Audio/CRX/Le_carrefour_des_app_642"),
                                                            Resources.Load<AudioClip>("Audio/CRX/En_plus_des_nombreux_590")
                                                       });
        
        //segment 11: STEM
        dialogAudio.Add(Buildings.STEM, new AudioClip[] { Resources.Load<AudioClip>("Audio/STM/Bienvenue_au_complex_844"),
                                                            Resources.Load<AudioClip>("Audio/STM/Le_btiment_rcemm_353"),
                                                            Resources.Load<AudioClip>("Audio/STM/Le_complexe_STEM_con_859"),
                                                            Resources.Load<AudioClip>("Audio/STM/Le_complexe_STEM_con_651"),
                                                            Resources.Load<AudioClip>("Audio/STM/Le_Brunsfield_Centre_630"), 
                                                            Resources.Load<AudioClip>("Audio/STM/STEM_abrite_galeme_589")
                                                        });
        
        //segment 12: CBY
        dialogAudio.Add(Buildings.CBY, new AudioClip[] { Resources.Load<AudioClip>("Audio/CBY/Voici_le_Pavillon_Co_917"),
                                                            Resources.Load<AudioClip>("Audio/CBY/Le_pavillon_colonel__260"),
                                                            Resources.Load<AudioClip>("Audio/CBY/Le_soussol_abrite_l_264")
                                                       });
        
        //segment 13: SITE
        dialogAudio.Add(Buildings.SITE, new AudioClip[] { Resources.Load<AudioClip>("Audio/STE/Bienvenue_sur_E_I_T__721"),
                                                            Resources.Load<AudioClip>("Audio/STE/Lcole_dingnier_418"),
                                                            Resources.Load<AudioClip>("Audio/STE/E_I_T_E_contient_de__497")
                                                        });
        
        return dialogAudio;
    }

}
