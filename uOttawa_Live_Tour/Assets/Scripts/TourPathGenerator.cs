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

        //segment 6: UCU
        Waypoint way6_1 = new Waypoint(new GPSCoords(45.42263241815559f, -75.68447628799723f), 1);
        Waypoint way6_2 = new Waypoint(new GPSCoords(45.422617797714196f, -75.68437659849273f), 2);
        Waypoint way6_3 = new Waypoint(new GPSCoords(45.42251649883774f, -75.68427095737601f), 3);
        Waypoint way6_4 = new Waypoint(new GPSCoords(45.42246219421056f, -75.68419209851426f), 4);

        Waypoint[] waypoints6 = new Waypoint[] { way6_1, way6_2, way6_3, way6_4 };
        PathSegment seg6 = new PathSegment(waypoints6);

        Dialogue dia6 = new Dialogue("University Center (UCU)", dialogueInfo[Buildings.UCU], dialogAudio[Buildings.UCU]);
        PointOfInterest poi6 = new PointOfInterest("ua-61701ae599e1ac2fff38db6bbeb71691", "UCU", new GPSCoords(45.422452795327466f, -75.6841623404532f), dia6);

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

        //segment 9: Biosciences
        Waypoint way9_1 = new Waypoint(new GPSCoords(45.42115414029608f, -75.68222182874727f), 1);
        Waypoint way9_2 = new Waypoint(new GPSCoords(45.42124104800955f, -75.68208203238204f), 2);
        Waypoint way9_3 = new Waypoint(new GPSCoords(45.421287305286356f, -75.68189230874353f), 3);
        Waypoint way9_4 = new Waypoint(new GPSCoords(45.421360195463826f, -75.6816586491045f), 4);

        Waypoint[] waypoints9 = new Waypoint[] { way9_1, way9_2, way9_3, way9_4 };
        PathSegment seg9 = new PathSegment(waypoints9);

        Dialogue dia9 = new Dialogue("Biosciences", dialogueInfo[Buildings.BSC], dialogAudio[Buildings.BSC]);
        PointOfInterest poi9 = new PointOfInterest("ua-152ac567954511a9b903e4d5fad2fb25", "BSC", new GPSCoords(45.42133094736899f, -75.68160565806315f), dia9);

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

    private static Dictionary<Buildings, AudioClip[]> getFrenchAudio()
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

}